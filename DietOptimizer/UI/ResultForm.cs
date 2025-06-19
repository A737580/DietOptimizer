using DietOptimizer.Models;
using DietOptimizer.Solvers;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Net.Mime.MediaTypeNames;

namespace DietOptimizer
{
    public partial class ResultsForm : Form
    {
        private OrToolsLpSolver _lpOptimizer = new OrToolsLpSolver();
        private SolutionResult _result;
        List<List<double>> _diets = new List<List<double>>();
        double _sumKcal = 0;
        double _sumWeight = 0;
        int _numberDiet = 0;
        public ResultsForm(SolutionResult result, UserProfile _currentUserProfile)
        {
            _result = result;
            _diets.Add(new List<double> { 0.4, 0.25, 0.25, 0.1 });
            _diets.Add(new List<double> { 0.25, 0.4, 0.25, 0.1 });
            _diets.Add(new List<double> { 0.25, 0.25, 0.4, 0.1 });
            InitializeComponent();
            InitializeRadarChart(_result, _currentUserProfile);
            InitializeResultsView(_result);
            InitializeOptimalityTab(_result);
            InitializeSensitivityTab();
            swapLeftLb.Visible = false;
            PrintInformationFromDiet();
        }

        private void InitializeRadarChart(SolutionResult result, UserProfile _currentUserProfile)
        {
            chartRadar.Series.Clear();
            chartRadar.ChartAreas.Clear();

            var area = chartRadar.ChartAreas.Add("RadarArea");
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.Enabled = AxisEnabled.False;

            // Серия для average
            var seriesAvg = chartRadar.Series.Add("Average");
            seriesAvg.IsVisibleInLegend = false; // убираем видимость легенды
            seriesAvg.ChartType = SeriesChartType.Radar;
            seriesAvg.BorderWidth = 2;
            seriesAvg.Color = Color.FromArgb(128, Color.LightBlue);
            seriesAvg["RadarDrawingStyle"] = "Area";

            // Серия для proposed
            var seriesProp = chartRadar.Series.Add("Proposed");
            seriesProp.IsVisibleInLegend = false; // убираем видимость легенды
            seriesProp.ChartType = SeriesChartType.Radar;
            seriesProp.BorderWidth = 2;
            seriesProp.Color = Color.FromArgb(128, Color.Orange);
            seriesProp["RadarDrawingStyle"] = "Area";


            // Вычисляем нормы нутриентов и сравниваем с рекомендованными средними
            var profileReq = ComputeMaintenanceNutrientRequirements(_currentUserProfile);

            var proposed = new Dictionary<string, double>
            {
                ["Белки"] = result.TotalProtein,
                ["Жиры"] = result.TotalFat,
                ["Углеводы"] = result.TotalCarbs
            };


            // Подготовим точки для радар-диаграммы
            string[] nutrients = { "Белки", "Жиры", "Углеводы" };
            var avgSeries = chartRadar.Series["Average"];
            var propSeries = chartRadar.Series["Proposed"];
            avgSeries.Points.Clear();
            propSeries.Points.Clear();

            //var area = chartRadar.ChartAreas["RadarArea"];
            area.AxisX.CustomLabels.Clear();

            for (int i = 0; i < nutrients.Length; i++)
            {
                string nut = nutrients[i];
                double avgVal = profileReq[nut];
                double propVal = proposed[nut];

                // Добавляем точки по порядку
                var ptAvg = avgSeries.Points.Add(avgVal);
                ptAvg.AxisLabel = nut;
                ptAvg.Label = avgVal.ToString("F0");

                var ptProp = propSeries.Points.Add(propVal);
                ptProp.AxisLabel = nut;
                ptProp.Label = propVal.ToString("F0");
            }
            chartRadar.Invalidate();

        }
        private void InitializeResultsView(SolutionResult result)
        {

            lblTitle.Text = result.ProblemType == ProblemType.Minimization ?
                    "Оптимальный набор продуктов (минимизация стоимости)" :
                    "Оптимальный набор продуктов (максимизация питательности)";
            lblTitle.Font = new System.Drawing.Font(Font.FontFamily, 12, FontStyle.Bold);
            lblTitle.AutoSize = true;


            dataGridView.AllowUserToAddRows = false;
            dataGridView.ReadOnly = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.Columns.Add("Product", "Продукт");
            dataGridView.Columns.Add("Amount", "Количество");
            dataGridView.Columns.Add("Protein", "Белки (г)");
            dataGridView.Columns.Add("Fat", "Жиры (г)");
            dataGridView.Columns.Add("Carbs", "Углеводы (г)");
            dataGridView.Columns.Add("Kcal", "Килокалорий)");
            dataGridView.Columns.Add("Cost", "Стоимость");

            double sumPrice = 0;

            foreach (var item in result.ProductAmounts.Where(x => x.Value > 0))
            {
                _sumKcal+=item.Key.Kcal* item.Value;
                _sumWeight += item.Key.Protein * item.Value + item.Key.Fat * item.Value+ item.Key.Carbs * item.Value;
                sumPrice += item.Key.Price * item.Value;
                dataGridView.Rows.Add(
                    item.Key.Name,
                    item.Value,
                    item.Key.Protein * item.Value,
                    item.Key.Fat * item.Value,
                    item.Key.Carbs * item.Value,
                    item.Key.Kcal * item.Value,
                    item.Key.Price * item.Value
                );
            }

            lblTotal.Text = "Итого:";
            lblTotal.Font = new System.Drawing.Font(Font.FontFamily, 10, FontStyle.Bold);
            lblTotal.AutoSize = true;


            lblTotalValues.Text = result.ProblemType == ProblemType.Minimization ?
                    $"Белки: {result.TotalProtein:F1}г (требуется: {result.ProteinRequirement:F1}г)\n" +
                    $"Жиры: {result.TotalFat:F1}г (требуется: {result.FatRequirement:F1}г)\n" +
                    $"Углеводы: {result.TotalCarbs:F1}г (требуется: {result.CarbsRequirement:F1}г)\n" +
                    $"Общая стоимость: {sumPrice:F2}" :
                    $"Белки: {result.TotalProtein:F1}г (вес: {result.ProteinWeight:F1})\n" +
                    $"Жиры: {result.TotalFat:F1}г (вес: {result.FatWeight:F1})\n" +
                    $"Углеводы: {result.TotalCarbs:F1}г (вес: {result.CarbsWeight:F1})\n" +
                    $"Обзее число килакалорий: {_sumKcal:F2} \n" +
                    $"Общая стоимость: {sumPrice:F2} (бюджет: {result.Budget:F2})\n" +
                    $"Общая питательная ценность: {_sumWeight:F2}";
            lblTotalValues.AutoSize = true;

            btnClose.Text = "Закрыть";
        }

        private Dictionary<string, double> ComputeMaintenanceNutrientRequirements(UserProfile p)
        {
            // 1. BMR по формуле Mifflin–St Jeor
            double bmr = p.Gender == Gender.Male
                ? 10 * p.WeightKg + 6.25 * p.HeightCm - 5 * p.Age + 5
                : 10 * p.WeightKg + 6.25 * p.HeightCm - 5 * p.Age - 161;

            // 2. Коэффициент активности
            double actFactor = p.ActivityLevel switch
            {
                ActivityLevel.Sedentary => 1.2,
                ActivityLevel.LightlyActive => 1.375,
                ActivityLevel.ModeratelyActive => 1.55,
                ActivityLevel.VeryActive => 1.725,
                ActivityLevel.ExtraActive => 1.9,
                _ => 1.2
            };

            // 3. Итоговые калории в день (TDEE)
            double maintenanceCalories = bmr * actFactor;

            // 4. Распределение макросов: 
            //    — Белки: 1.6 г на кг массы тела (минимум для поддержания мышц)
            //    — Жиры: 25% дневных калорий
            //    — Углеводы: остаток калорий
            double proteinGPerKg = 1.6;
            double proteinGrams = p.WeightKg * proteinGPerKg;
            double proteinCalories = proteinGrams * 4.0;

            double fatCalories = maintenanceCalories * 0.25;
            double fatGrams = fatCalories / 9.0;

            double carbsCalories = maintenanceCalories - (proteinCalories + fatCalories);
            double carbsGrams = carbsCalories > 0 ? carbsCalories / 4.0 : 0;

            return new Dictionary<string, double>
            {
                ["Белки"] = proteinGrams,
                ["Жиры"] = fatGrams,
                ["Углеводы"] = carbsGrams
            };
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // --- Методы для вкладки "оптимальность" ---

        private void InitializeOptimalityTab(SolutionResult result)
        {
            // Pie chart для нутриентов
            chartNutrientsPie.Series.Clear();
            chartNutrientsPie.ChartAreas.Clear();
            var pieArea = chartNutrientsPie.ChartAreas.Add("PieArea");
            pieArea.Position = new ElementPosition(0, 0, 50, 100);

            var pieSeries = chartNutrientsPie.Series.Add("NutrientsPie");
            pieSeries.ChartType = SeriesChartType.Pie;
            pieSeries.IsValueShownAsLabel = true;
            pieSeries.LabelFormat = "F0";
            pieSeries["PieLabelStyle"] = "Outside";
            pieSeries.LegendText = "#VALX: #VALY";

            chartNutrientsPie.Legends.Clear();
            chartNutrientsPie.Legends.Add("PieLegend")
                .Docking = Docking.Right;
            chartNutrientsPie.Legends[0].Name = "PieLegend";
            pieSeries.Legend = "PieLegend";

            // Column chart для сравнения LP vs Integer
            chartComparison.Series.Clear();
            chartComparison.ChartAreas.Clear();
            var colArea = chartComparison.ChartAreas.Add("ColArea");
            colArea.AxisX.Title = "Нутриенты";
            colArea.AxisY.Title = "Граммы";

            var sLp = chartComparison.Series.Add("Лучшие порции");
            var sInt = chartComparison.Series.Add("Целые порции");
            sLp.ChartType = SeriesChartType.Column;
            sInt.ChartType = SeriesChartType.Column;

            sLp.IsValueShownAsLabel = true;
            sInt.IsValueShownAsLabel = true;

            // Сдвиг столбцов друг относительно друга
            sLp["PointWidth"] = "0.4";
            sInt["PointWidth"] = "0.4";

            chartComparison.Legends.Clear();
            chartComparison.Legends.Add("ColLegend");
            chartComparison.Legends[0].Name = "ColLegend";

            // И для каждой серии:
            sLp.Legend = "ColLegend";
            sInt.Legend = "ColLegend";

            FillingCharts(result);
        }
        private void FillingCharts(SolutionResult result)
        {
            // Предполагаем, что _currentDietPlan и _currentIntegerPlan уже рассчитаны
            var selectedProducts = result.Products.Where(p => p.IsSelected).ToList();
            SolutionResult lpResult;

            if (_result.ProblemType == ProblemType.Minimization)
            {
                lpResult = _lpOptimizer.SolveMinimization(selectedProducts, result.ProteinRequirement, result.FatRequirement, result.CarbsRequirement) as SolutionResult;
            }
            else
            {
                if (result.ProteinWeight != 0 || result.FatWeight != 0 || result.CarbsWeight != 0)
                {
                    lpResult = _lpOptimizer.SolveMaximizationPriority(selectedProducts, result.Budget, result.ProteinWeight, result.FatWeight, result.CarbsWeight) as SolutionResult;
                }
                else
                { 
                    lpResult = _lpOptimizer.SolveMaximization(selectedProducts, result.Budget, result.ProteinRequirement, result.FatRequirement, result.CarbsRequirement) as SolutionResult;
                }
            }

            var intResult = result as SolutionResult;
            if (lpResult == null || intResult == null)
            {
                MessageBox.Show("Сначала рассчитайте оба варианта (LP и Integer).",
                                "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            UpdateOptimalityChartsManualX(lpResult, intResult);
        }
        private void UpdateOptimalityChartsManualX(SolutionResult lp, SolutionResult integer)
        {
            // --- Data Validation ---
            if (lp == null || integer == null)
            {
                MessageBox.Show("Не удалось получить одно или оба решения (LP/Integer) для построения графика сравнения.", "Ошибка данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Optionally clear the chart
                chartComparison.Series["Лучшие порции"].Points.Clear();
                chartComparison.Series["Целые порции"].Points.Clear();
                chartComparison.ChartAreas["ColArea"].AxisX.CustomLabels.Clear();
                return;
            }
            Debug.WriteLine($"--- Updating Comparison Chart (Manual X) ---");
            Debug.WriteLine($"LP Totals: P={lp.TotalProtein:F2}, F={lp.TotalFat:F2}, C={lp.TotalCarbs:F2}");
            Debug.WriteLine($"Int Totals: P={integer.TotalProtein:F2}, F={integer.TotalFat:F2}, C={integer.TotalCarbs:F2}");
            // 3.1 Pie chart
            var pie = chartNutrientsPie.Series["NutrientsPie"];
            pie.Points.Clear();
            pie.ChartArea = "PieArea";

            var nutrients = new[] { "Белки", "Жиры", "Углеводы" };
            var values = new[] { integer.TotalProtein, integer.TotalFat, integer.TotalCarbs };

            for (int i = 0; i < nutrients.Length; i++)
            {
                var pt = pie.Points.Add(values[i]);
                pt.Label = $"{values[i]:F0} г";
                pt.LegendText = nutrients[i];
                pt.AxisLabel = nutrients[i];
            }

            // --- Chart References ---
            var comparisonChart = chartComparison;
            // Ensure ChartArea and Series exist (or create them here if necessary)
            ChartArea colArea;
            if (comparisonChart.ChartAreas.IndexOf("ColArea") < 0)
                colArea = comparisonChart.ChartAreas.Add("ColArea");
            else
                colArea = comparisonChart.ChartAreas["ColArea"];

            Series seriesLp;
            if (comparisonChart.Series.IndexOf("Лучшие порции") < 0)
                seriesLp = comparisonChart.Series.Add("Лучшие порции");
            else
                seriesLp = comparisonChart.Series["Лучшие порции"];

            Series seriesInt;
            if (comparisonChart.Series.IndexOf("Целые порции") < 0)
                seriesInt = comparisonChart.Series.Add("Целые порции");
            else
                seriesInt = comparisonChart.Series["Целые порции"];

            // --- Clear Previous State ---
            seriesLp.Points.Clear();
            seriesInt.Points.Clear();
            colArea.AxisX.CustomLabels.Clear(); // Clear old custom labels

            // --- Configure Series ---
            seriesLp.ChartType = SeriesChartType.Column;
            seriesInt.ChartType = SeriesChartType.Column;
            seriesLp.ChartArea = colArea.Name; // Assign area
            seriesInt.ChartArea = colArea.Name;
            seriesLp.IsValueShownAsLabel = true; // Show value on column
            seriesInt.IsValueShownAsLabel = true;
            seriesLp.LabelFormat = "F1"; // Format for value label
            seriesInt.LabelFormat = "F1";

            // Set legend (ensure legend "ColLegend" exists or create it)
            if (comparisonChart.Legends.IndexOf("ColLegend") < 0)
            {
                comparisonChart.Legends.Add("ColLegend");
                comparisonChart.Legends["ColLegend"].Name = "ColLegend"; // Set name explicitly
            }
            seriesLp.Legend = "ColLegend";
            seriesInt.Legend = "ColLegend";


            // --- Column Appearance ---
            // Adjust width and offset for visual spacing
            double pointWidth = 0.4; // Width of a single column (adjust 0.1 to 0.9)
            double groupOffset = 0.22; // Offset from category center (adjust based on pointWidth)
            seriesLp.SetCustomProperty("PointWidth", pointWidth.ToString(CultureInfo.InvariantCulture));
            seriesInt.SetCustomProperty("PointWidth", pointWidth.ToString(CultureInfo.InvariantCulture));

            // --- Configure Axes for Manual Positioning ---
            // Y-Axis
            colArea.AxisY.Title = "Количество (г)";
            colArea.AxisY.MajorGrid.Enabled = true;
            colArea.AxisY.IsStartedFromZero = true;

            // X-Axis
            colArea.AxisX.Title = "Нутриенты";
            colArea.AxisX.MajorGrid.Enabled = false;
            colArea.AxisX.MajorTickMark.Enabled = false; // Hide major ticks if desired
            colArea.AxisX.Interval = 1; // Treat positions 1, 2, 3 as main points

            // **Label Configuration Revamp**
            colArea.AxisX.LabelStyle.Enabled = true; // KEEP LabelStyle enabled
            colArea.AxisX.LabelStyle.Interval = 0;   // PREVENT standard numeric labels from showing
            colArea.AxisX.LabelStyle.Format = "";    // Hide format for standard labels
            colArea.AxisX.LabelStyle.Angle = -45;

            // Set axis limits (adjust if more categories)
            colArea.AxisX.Minimum = 0;
            colArea.AxisX.Maximum = 4; // Needs to be > last category center + 0.5

            // --- Add Data and Custom Labels ---
            var lpValues = new[] { lp.TotalProtein, lp.TotalFat, lp.TotalCarbs };
            var integerValues = new[] { integer.TotalProtein, integer.TotalFat, integer.TotalCarbs };

            for (int i = 0; i < nutrients.Length; i++)
            {
                string categoryName = nutrients[i];
                double categoryCenterPosition = i + 1; // 1, 2, 3

                double lpVal = Math.Round(lpValues[i], 2);
                double intVal = Math.Round(integerValues[i], 2);

                // Add data points with offset X values
                seriesLp.Points.AddXY(categoryCenterPosition - groupOffset, lpVal);
                seriesInt.Points.AddXY(categoryCenterPosition + groupOffset, intVal);

                // Add Custom Label centered under the group
                CustomLabel nutrientLabel = new CustomLabel(
                    fromPosition: categoryCenterPosition - 0.5, // Start slightly before center
                    toPosition: categoryCenterPosition + 0.5,   // End slightly after center
                    text: categoryName,
                    labelRow: 0, // First row of labels
                    markStyle: LabelMarkStyle.None // No tick mark for the label
                                                   // gridTicks: GridTickTypes.None // Ensure no grid lines for label either
                );
                nutrientLabel.ForeColor = Color.Black; // Ensure label is visible
                //nutrientLabel.LabelAngle = -45; // Uncomment if needed

                colArea.AxisX.CustomLabels.Add(nutrientLabel);
                Debug.WriteLine($"Added CustomLabel: '{categoryName}' at position {categoryCenterPosition}");
            }

            // --- Redraw Chart ---
            comparisonChart.Invalidate();
        }
        // --- Методы для вкладки "Анализ оптимальности" ---
        private void InitializeSensitivityTab()
        {
            if (_result.ProblemType == ProblemType.Maximization)
            {
                tabControl1.TabPages.Remove(tabPage3);
                return;
            }

            // Заполняем ComboBox названиями нутриентов
            cmbSensitivityNutrient.Items.Add("Белки");
            cmbSensitivityNutrient.Items.Add("Жиры");
            cmbSensitivityNutrient.Items.Add("Углеводы");
            cmbSensitivityNutrient.SelectedIndex = 0; // По умолчанию выбираем Белки

            string selectedNutrient = cmbSensitivityNutrient.SelectedItem?.ToString() ?? "";
            double valueNutrient = 0;

            if (selectedNutrient == "Белки")
                valueNutrient = _result.ProteinRequirement;
            else if (selectedNutrient == "Жиры")
                valueNutrient = _result.FatRequirement;
            else
                valueNutrient = _result.CarbsRequirement;

            // Устанавливаем начальные значения (можно взять из первой вкладки или задать по умолчанию)
            txtSensitivityMin.Text = Math.Round(valueNutrient * 0.8,2).ToString(CultureInfo.InvariantCulture);
            txtSensitivityMax.Text = Math.Round(valueNutrient * 1.2,2).ToString(CultureInfo.InvariantCulture);
            txtSensitivityStep.Text = Math.Round((valueNutrient * 1.2 - valueNutrient * 0.8) / 11.0, 2).ToString(CultureInfo.InvariantCulture);

            // Устанавливаем начальные фиксированные значения (например, 50 для всех)
            txtSensitivityFixedProtein.Text = _result.ProteinRequirement.ToString();
            txtSensitivityFixedFat.Text = _result.FatRequirement.ToString();
            txtSensitivityFixedCarbs.Text = _result.CarbsRequirement.ToString();

            // Настройка графика
            var chartArea = chartSensitivity.ChartAreas["ChartArea1"];
            chartArea.AxisX.Title = "Требование"; // Общее название оси X
            chartArea.AxisY.Title = "Минимальная стоимость";
            chartArea.AxisX.Minimum = double.NaN; // Автоматический расчет минимума
            chartArea.AxisY.Minimum = double.NaN; // Автоматический расчет минимума

            chartSensitivity.Series["Series1"].Name = "TotalCost";
            var series = chartSensitivity.Series["TotalCost"];
            series.Points.Clear(); // Очищаем точки при инициализации
            series.BorderWidth = 2; // Толщина линии

            // Обновляем доступность полей при инициализации
            UpdateSensitivityFixedFields();
        }

        private void cmbSensitivityNutrient_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSensitivityFixedFields();
            // Опционально: обновить заголовок оси X на графике
            UpdateSensitivityChartAxisTitle();

            string selectedNutrient = cmbSensitivityNutrient.SelectedItem?.ToString() ?? "";
            double valueNutrient = 0;

            if (selectedNutrient == "Белки")
                valueNutrient = _result.ProteinRequirement;
            else if (selectedNutrient == "Жиры")
                valueNutrient = _result.FatRequirement;
            else
                valueNutrient = _result.CarbsRequirement;

            // Устанавливаем начальные значения (можно взять из первой вкладки или задать по умолчанию)
            txtSensitivityMin.Text = Math.Round(valueNutrient * 0.8,2).ToString(CultureInfo.InvariantCulture);
            txtSensitivityMax.Text = Math.Round(valueNutrient * 1.2,2).ToString(CultureInfo.InvariantCulture);
            txtSensitivityStep.Text = Math.Round((valueNutrient * 1.2 - valueNutrient * 0.8) / 11.0, 2).ToString(CultureInfo.InvariantCulture);

        }

        private void UpdateSensitivityFixedFields()
        {
            // Включаем/выключаем поля для фиксированных значений
            // Поле для выбранного нутриента выключается, остальные включаются
            string selectedNutrient = cmbSensitivityNutrient.SelectedItem?.ToString() ?? "";

            txtSensitivityFixedProtein.Enabled = (selectedNutrient != "Белки");
            txtSensitivityFixedFat.Enabled = (selectedNutrient != "Жиры");
            txtSensitivityFixedCarbs.Enabled = (selectedNutrient != "Углеводы");
        }

        private void UpdateSensitivityChartAxisTitle()
        {
            string selectedNutrient = cmbSensitivityNutrient.SelectedItem?.ToString() ?? "Нутриент";
            chartSensitivity.ChartAreas["ChartArea1"].AxisX.Title = $"Требование ({selectedNutrient})";
        }


        private void btnAnalyzeSensitivity_Click(object sender, EventArgs e)
        {
            // 1. Валидация и парсинг входных данных
            if (!ValidateAndParseSensitivityInputs(
                    out string varyingNutrient,
                    out double minReq, out double maxReq, out double step,
                    out double fixedProtein, out double fixedFat, out double fixedCarbs))
            {
                return; // Ошибка валидации, сообщение уже показано
            }

            // 2. Получение списка выбранных продуктов
            var selectedProducts = _result.Products.Where(p => p.IsSelected).ToList();
            if (!selectedProducts.Any())
            {
                MessageBox.Show("Выберите хотя бы один продукт на вкладке 'Диета'.", "Нет продуктов", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Выполнение анализа чувствительности
            try
            {
                List<Tuple<double, double>> analysisResults = PerformSensitivityAnalysis(
                    selectedProducts, varyingNutrient, minReq, maxReq, step,
                    fixedProtein, fixedFat, fixedCarbs);

                // 4. Отображение результатов на графике
                UpdateSensitivityChart(analysisResults);
                UpdateSensitivityChartAxisTitle(); // Обновить заголовок оси X
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при анализе чувствительности: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateAndParseSensitivityInputs(
            out string varyingNutrient,
            out double minReq, out double maxReq, out double step,
            out double fixedProtein, out double fixedFat, out double fixedCarbs)
        {
            // Инициализация выходных параметров значениями по умолчанию
            varyingNutrient = cmbSensitivityNutrient.SelectedItem?.ToString() ?? "";
            minReq = maxReq = step = 0;
            fixedProtein = fixedFat = fixedCarbs = 0;

            if (string.IsNullOrEmpty(varyingNutrient))
            {
                MessageBox.Show("Выберите нутриент для анализа.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Парсинг диапазона и шага
            if (!double.TryParse(txtSensitivityMin.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out minReq) || minReq < 0)
            {
                MessageBox.Show("Некорректное минимальное значение.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!double.TryParse(txtSensitivityMax.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out maxReq) || maxReq < 0)
            {
                MessageBox.Show("Некорректное максимальное значение.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!double.TryParse(txtSensitivityStep.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out step) || step <= 0)
            {
                MessageBox.Show("Некорректное значение шага (должно быть > 0).", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (minReq >= maxReq)
            {
                MessageBox.Show("Минимальное значение должно быть меньше максимального.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Парсинг фиксированных значений (только для активных полей)
            if (txtSensitivityFixedProtein.Enabled && (!double.TryParse(txtSensitivityFixedProtein.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out fixedProtein) || fixedProtein < 0))
            {
                MessageBox.Show("Некорректное фиксированное значение для белков.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtSensitivityFixedFat.Enabled && (!double.TryParse(txtSensitivityFixedFat.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out fixedFat) || fixedFat < 0))
            {
                MessageBox.Show("Некорректное фиксированное значение для жиров.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtSensitivityFixedCarbs.Enabled && (!double.TryParse(txtSensitivityFixedCarbs.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out fixedCarbs) || fixedCarbs < 0))
            {
                MessageBox.Show("Некорректное фиксированное значение для углеводов.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Метод для выполнения анализа
        private List<Tuple<double, double>> PerformSensitivityAnalysis(
            List<Product> selectedProducts, string varyingNutrient,
            double minReq, double maxReq, double step,
            double fixedProtein, double fixedFat, double fixedCarbs)
        {
            var results = new List<Tuple<double, double>>(); // Список пар (значение_требования, общая_стоимость)

            for (double currentReq = minReq; currentReq <= maxReq; currentReq += step)
            {
                double proteinReq = 0, fatReq = 0, carbsReq = 0;

                // Устанавливаем требования для текущей итерации
                switch (varyingNutrient)
                {
                    case "Белки":
                        proteinReq = currentReq;
                        fatReq = fixedFat;
                        carbsReq = fixedCarbs;
                        break;
                    case "Жиры":
                        proteinReq = fixedProtein;
                        fatReq = currentReq;
                        carbsReq = fixedCarbs;
                        break;
                    case "Углеводы":
                        proteinReq = fixedProtein;
                        fatReq = fixedFat;
                        carbsReq = currentReq;
                        break;
                }

                // Решаем задачу минимизации для текущих требований
                // Используем существующий метод SolveMinimization из DietOptimizer
                SolutionResult result = _lpOptimizer.SolveMinimization(selectedProducts, proteinReq, fatReq, carbsReq);

                // Если решение найдено (не null и имеет результаты), добавляем точку в список
                // Проверяем ProductAmounts, т.к. может быть null если решение не найдено (infeasible)
                if (result != null && result.ProductAmounts != null && result.ProductAmounts.Any())
                {
                    results.Add(Tuple.Create(currentReq, result.TotalCost));
                }
                else
                {
                    // Опционально: Логирование или уведомление о том, что для данного значения нет решения
                    Console.WriteLine($"Решение не найдено для {varyingNutrient} = {currentReq}");
                    // Можно добавить точку с NaN или пропустить её
                    // results.Add(Tuple.Create(currentReq, double.NaN));
                }
            }

            return results;
        }

        // Метод для обновления графика
        private void UpdateSensitivityChart(List<Tuple<double, double>> analysisResults)
        {
            var series = chartSensitivity.Series["TotalCost"];
            series.Points.Clear(); // Очищаем предыдущие точки

            if (analysisResults == null || !analysisResults.Any())
            {
                MessageBox.Show("Нет данных для отображения на графике.", "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var point in analysisResults)
            {
                // Добавляем точку на график (X = значение требования, Y = общая стоимость)
                // Пропускаем точки с NaN, если они были добавлены
                if (!double.IsNaN(point.Item2))
                {
                    series.Points.AddXY(point.Item1, point.Item2);
                }
            }

            // Обновляем оси для лучшего отображения (опционально)
            chartSensitivity.ChartAreas["ChartArea1"].AxisX.Minimum = double.NaN; // Авто
            chartSensitivity.ChartAreas["ChartArea1"].AxisY.Minimum = double.NaN; // Авто
            chartSensitivity.ChartAreas["ChartArea1"].RecalculateAxesScale();
        }

        private void swapLeftLb_Click(object sender, EventArgs e)
        {
            _numberDiet--;
            if (_numberDiet == 0)
            {
                swapLeftLb.Visible = false;
                swapRightLb.Visible = true;
            }
            PrintInformationFromDiet();
        }

        private void swapRightLb_Click(object sender, EventArgs e)
        {
            _numberDiet++;
            swapLeftLb.Visible = true;
            if (_numberDiet == _diets.Count()-1)
            {
                swapRightLb.Visible = false;
            }
            PrintInformationFromDiet();
        }

        private void PrintInformationFromDiet()
        {
            breakfastKcalLb.Text = $"Килокалорий:  {Math.Round(_sumKcal * _diets[_numberDiet][0], 2)}";
            breakfastWeightLb.Text = $"Вес:  {Math.Round(_sumWeight * _diets[_numberDiet][0], 2)}г";
            lunchKcalLb.Text = $"Килокалорий:  {Math.Round(_sumKcal * _diets[_numberDiet][1], 2)}";
            lunchWeightLb.Text = $"Вес:  {Math.Round(_sumWeight * _diets[_numberDiet][1], 2)}г";
            dinnerKcalLb.Text = $"Килокалорий:  {Math.Round(_sumKcal * _diets[_numberDiet][2], 2)}";
            dinnerWeightLb.Text = $"Вес:  {Math.Round(_sumWeight * _diets[_numberDiet][2],2)}г";
            additionalReceptionKcalLb.Text = $"Килокалорий:  {Math.Round(_sumKcal * _diets[_numberDiet][3],2)}";
            additionalReceptionWeightLb.Text = $"Вес:  {Math.Round(_sumWeight * _diets[_numberDiet][3],2)}г";
        }
    }
}