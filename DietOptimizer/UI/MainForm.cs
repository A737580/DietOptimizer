using DietOptimizer.Models;
using DietOptimizer.Solvers;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace DietOptimizer
{
    public partial class MainForm : Form
    {
        private List<Product> _products = new List<Product>();
        private const string _dataFilePath = "products.txt";
        private UserProfile _currentUserProfile = new UserProfile();
        private IntegerSimplexSolver _dietOptimizer = new IntegerSimplexSolver();
        private IntegerSimplexSolverGomori _dietOptimizerGomori = new IntegerSimplexSolverGomori();
        public MainForm()
        {
            InitializeComponent();
            LoadProducts();
            SetupDefaultUIState();
        }

        private void LoadProducts()
        {
            if (File.Exists(_dataFilePath))
            {
                try
                {
                    var lines = File.ReadAllLines(_dataFilePath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 6)
                        {
                            _products.Add(new Product
                            {
                                Name = parts[0],
                                Protein = double.Parse(parts[1], CultureInfo.InvariantCulture),
                                Fat = double.Parse(parts[2], CultureInfo.InvariantCulture),
                                Carbs = double.Parse(parts[3], CultureInfo.InvariantCulture),
                                Kcal = double.Parse(parts[4], CultureInfo.InvariantCulture),
                                Price = double.Parse(parts[5], CultureInfo.InvariantCulture),
                                IsSelected = false
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
                }
            }
        }

        private void InitializeDataGridView()
        {
            dataGridViewProducts.DataSource = new BindingList<Product>(_products);

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
            {
                Name = "IsSelected",
                HeaderText = "Выбрать",
                DataPropertyName = "IsSelected",
                Width = 60
            };

            if (!dataGridViewProducts.Columns.Contains("IsSelected"))
            {
                dataGridViewProducts.Columns.Insert(0, checkBoxColumn);
            }

            foreach (DataGridViewColumn column in dataGridViewProducts.Columns)
            {
                if (column.Name != "IsSelected")
                {
                    column.ReadOnly = true;
                    column.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

            dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProducts.AllowUserToAddRows = false;
            dataGridViewProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void UpdateInputFields()
        {
            bool isMinimization = (rbMinimizeWeight.Checked ? ProblemType.Minimization : ProblemType.Maximization) == 0;


            lblProtein.Visible = isMinimization;
            txtProtein.Visible = isMinimization;
            lblFat.Visible = isMinimization;
            txtFat.Visible = isMinimization;
            lblCarbs.Visible = isMinimization;
            txtCarbs.Visible = isMinimization;
            typeMethodCMB.Visible = isMinimization;

            newbieCB.Visible = !isMinimization;
            lblBudget.Visible = !isMinimization;
            txtBudget.Visible = !isMinimization;
            lblProteinWeight.Visible = !isMinimization;
            cmbProteinWeight.Visible = !isMinimization;
            lblFatWeight.Visible = !isMinimization;
            cmbFatWeight.Visible = !isMinimization;
            lblCarbsWeight.Visible = !isMinimization;
            cmbCarbsWeight.Visible = !isMinimization;
        }
        private void InitializeObjectiveType()
        {
            UpdateInputFields();
        }
        private void InitializeNutrientWeights()
        {
            newbieCB.Checked = true;
            newbieCB.Visible = false;
            typeMethodCMB.SelectedIndex = 0;
            cmbProteinWeight.Items.AddRange(new object[] { "1.0", "1.5", "2.0", "2.5", "3.0" });
            cmbFatWeight.Items.AddRange(new object[] { "1.0", "1.5", "2.0", "2.5", "3.0" });
            cmbCarbsWeight.Items.AddRange(new object[] { "1.0", "1.5", "2.0", "2.5", "3.0" });
            cmbProteinWeight.SelectedIndex = 0;
            cmbFatWeight.SelectedIndex = 0;
            cmbCarbsWeight.SelectedIndex = 0;
        }
        private void InitializeStatusPicture()
        {
            pictureStatus.SizeMode = PictureBoxSizeMode.Zoom;
            pictureStatus.BorderStyle = BorderStyle.None;

            UpdateStatusImage(true);
        }
        private void SetupDefaultUIState()
        {
            InitializeDataGridView();
            InitializeObjectiveType();
            InitializeNutrientWeights();
            InitializeStatusPicture();
            rbMinimizeWeight.Checked = true;
            cmbGender.DataSource = Enum.GetValues(typeof(Gender));
            cmbActivity.DataSource = Enum.GetValues(typeof(ActivityLevel));
        }

        private void UpdateStatusImage(bool state)
        {

            pictureStatus.Image = state ? Properties.Resources.Minimization : Properties.Resources.Maximization;
        }

        private void SaveProducts()
        {
            try
            {
                var lines = _products.Select(p => $"{p.Name},{p.Protein.ToString(CultureInfo.InvariantCulture)}," +
                    $"{p.Fat.ToString(CultureInfo.InvariantCulture)},{p.Carbs.ToString(CultureInfo.InvariantCulture)}," +
                    $"{p.Price.ToString(CultureInfo.InvariantCulture)},{p.Kcal.ToString(CultureInfo.InvariantCulture)}");
                File.WriteAllLines(_dataFilePath, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}");
            }
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            using (var form = new AddProductForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _products.Add(form.NewProduct);
                    InitializeDataGridView();
                    SaveProducts();
                }
            }
        }
        private void UpdateDataGridBinding()
        {
            dataGridViewProducts.DataSource = new BindingList<Product>(_products);
        }
        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            var selectedIndices = dataGridViewProducts.SelectedRows
                .Cast<DataGridViewRow>()
                .Select(row => row.Index)
                .Where(index => index >= 0 && index < _products.Count)
                .OrderByDescending(i => i)
                .ToList();

            if (selectedIndices.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы один продукт для удаления");
                return;
            }

            if (MessageBox.Show($"Удалить {selectedIndices.Count} продуктов?",
                "Подтверждение", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            foreach (var index in selectedIndices)
            {
                _products.RemoveAt(index);
            }

            UpdateDataGridBinding();
            SaveProducts();
        }
        
        private bool UpdateProfileFromControls()
        {
            if (cmbGender.SelectedItem == null)
            {
                MessageBox.Show("Выберите пол.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbActivity.SelectedItem == null)
            {
                MessageBox.Show("Выберите уровень активности.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_currentUserProfile == null)
                _currentUserProfile = new UserProfile();

            try
            {
                _currentUserProfile.Gender = (Gender)cmbGender.SelectedItem;
                _currentUserProfile.Age = (int)numAge.Value;
                _currentUserProfile.WeightKg = (double)numWeight.Value;
                _currentUserProfile.HeightCm = (double)numHeight.Value;
                _currentUserProfile.ActivityLevel = (ActivityLevel)cmbActivity.SelectedItem;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении профиля: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        private SolutionResult SolveMinimizationProblem(List<Product> products, double proteinReq, double fatReq, double carbsReq)
        {
            if (typeMethodCMB.Text == "Метод ветвей и границ")
            {
                return _dietOptimizer.SolveMinimization(products, proteinReq, fatReq, carbsReq);
            }
            else
            {
                return _dietOptimizerGomori.MinimizeCost(products, proteinReq, fatReq, carbsReq);
            }
        }
        private SolutionResult SolveMaximizationProblem(List<Product> products, double budget, double proteinWeight, double fatWeight, double carbsWeight)
        {
            return _dietOptimizer.SolveMaximization(products, budget, proteinWeight, fatWeight, carbsWeight);
        }
        private void btnOptimize_Click(object sender, EventArgs e)
        {
            if (!UpdateProfileFromControls()) return;

            var selectedProducts = _products.Where(p => p.IsSelected).ToList();

            if (selectedProducts.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы один продукт для оптимизации.");
                return;
            }


            bool isMinimization = (rbMinimizeWeight.Checked ? ProblemType.Minimization : ProblemType.Maximization) == 0;

            try
            {
                SolutionResult result;
                if (isMinimization)
                {
                    if (!double.TryParse(txtProtein.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double proteinReq) ||
                        !double.TryParse(txtFat.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double fatReq) ||
                        !double.TryParse(txtCarbs.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double carbsReq))
                    {
                        MessageBox.Show("Введите корректные значения для белков, жиров и углеводов.");
                        return;
                    }

                    result = SolveMinimizationProblem(selectedProducts, proteinReq, fatReq, carbsReq);
                }
                else
                {
                    if (!double.TryParse(txtBudget.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double budget) || budget <= 0)
                    {
                        MessageBox.Show("Введите корректное значение бюджета.");
                        return;
                    }

                    if (newbieCB.Checked == true)
                    {
                        if (!double.TryParse(cmbProteinWeight.SelectedItem.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double proteinWeight) ||
                        !double.TryParse(cmbFatWeight.SelectedItem.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double fatWeight) ||
                        !double.TryParse(cmbCarbsWeight.SelectedItem.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double carbsWeight))
                        {
                            MessageBox.Show("Ошибка в значениях весов. Используйте точку как разделитель дробной части.");
                            return;
                        }
                        result = SolveMaximizationProblem(selectedProducts, budget, proteinWeight, fatWeight, carbsWeight);
                    }
                    else
                    {
                        if (!double.TryParse(txtProtein.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double proteinReq) ||
                        !double.TryParse(txtFat.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double fatReq) ||
                        !double.TryParse(txtCarbs.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double carbsReq))
                        {
                            MessageBox.Show("Ошибка в значениях весов. Используйте точку как разделитель дробной части.");
                            return;
                        }
                        result = _dietOptimizerGomori.MaximizeNutrients2(selectedProducts,proteinReq, fatReq, carbsReq, budget);
                    }
                }

                var form = new ResultsForm(result, _currentUserProfile);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при решении задачи: {ex.Message}");
            }
        }


        private void rbMinimizeWeight_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInputFields();
            UpdateStatusImage((rbMinimizeWeight.Checked ? ProblemType.Minimization : ProblemType.Maximization) == 0);
            newbieCB.Checked = true;
        }

        private void cmbObjectiveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInputFields();
        }
        
        private void newbieCB_CheckedChanged(object sender, EventArgs e)
        {
            if(newbieCB.Checked==true)
            {
                lblProtein.Visible = false;
                txtProtein.Visible = false;
                lblFat.Visible = false;
                txtFat.Visible = false;
                lblCarbs.Visible = false;
                txtCarbs.Visible = false;
                lblProteinWeight.Visible = true;
                cmbProteinWeight.Visible = true;
                lblFatWeight.Visible = true;
                cmbFatWeight.Visible = true;
                lblCarbsWeight.Visible = true;
                cmbCarbsWeight.Visible = true;
            }
            else
            {
                lblProtein.Visible = true;
                txtProtein.Visible = true;
                lblFat.Visible = true;
                txtFat.Visible = true;
                lblCarbs.Visible = true;
                txtCarbs.Visible = true;
                lblProteinWeight.Visible = false;
                cmbProteinWeight.Visible = false;
                lblFatWeight.Visible = false;
                cmbFatWeight.Visible = false;
                lblCarbsWeight.Visible = false;
                cmbCarbsWeight.Visible = false;
            }
        }
    }
}