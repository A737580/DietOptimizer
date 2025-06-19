namespace DietOptimizer
{
    partial class ResultsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            chartRadar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            lblTotalValues = new Label();
            lblTotal = new Label();
            lblTitle = new Label();
            btnClose = new Button();
            dataGridView = new DataGridView();
            tabPage2 = new TabPage();
            chartComparison = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartNutrientsPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tabPage3 = new TabPage();
            btnAnalyzeSensitivity = new Button();
            txtSensitivityStep = new TextBox();
            txtSensitivityMax = new TextBox();
            lblSensitivityStep = new Label();
            lblSensitivityMax = new Label();
            lblSensitivityMin = new Label();
            chartSensitivity = new System.Windows.Forms.DataVisualization.Charting.Chart();
            groupBox1 = new GroupBox();
            txtSensitivityFixedCarbs = new TextBox();
            txtSensitivityFixedFat = new TextBox();
            txtSensitivityFixedProtein = new TextBox();
            lblSensitivityFixedCarbs = new Label();
            lblSensitivityFixedProtein = new Label();
            lblSensitivityFixedFat = new Label();
            txtSensitivityMin = new TextBox();
            cmbSensitivityNutrient = new ComboBox();
            lblSensitivityNutrient = new Label();
            dietTab = new TabPage();
            swapRightLb = new Label();
            swapLeftLb = new Label();
            additionalReceptionPanel = new Panel();
            additionalReceptionPB = new PictureBox();
            additionalReceptionWeightLb = new Label();
            additionalReceptionKcalLb = new Label();
            additionalReceptionLb = new Label();
            lunchPanel = new Panel();
            lunchPB = new PictureBox();
            lunchWeightLb = new Label();
            lunchKcalLb = new Label();
            lunchLb = new Label();
            dinnerPanel = new Panel();
            dinnerPB = new PictureBox();
            dinnerWeightLb = new Label();
            dinnerKcalLb = new Label();
            dinnerLb = new Label();
            breakfastPanel = new Panel();
            breakfatPB = new PictureBox();
            breakfastWeightLb = new Label();
            breakfastKcalLb = new Label();
            breakfastLb = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartRadar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartComparison).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartNutrientsPie).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartSensitivity).BeginInit();
            groupBox1.SuspendLayout();
            dietTab.SuspendLayout();
            additionalReceptionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)additionalReceptionPB).BeginInit();
            lunchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lunchPB).BeginInit();
            dinnerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dinnerPB).BeginInit();
            breakfastPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)breakfatPB).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(dietTab);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(3, 2, 3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(899, 443);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(chartRadar);
            tabPage1.Controls.Add(lblTotalValues);
            tabPage1.Controls.Add(lblTotal);
            tabPage1.Controls.Add(lblTitle);
            tabPage1.Controls.Add(btnClose);
            tabPage1.Controls.Add(dataGridView);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Margin = new Padding(3, 2, 3, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 2, 3, 2);
            tabPage1.Size = new Size(891, 415);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Результат";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // chartRadar
            // 
            chartArea1.Name = "ChartArea1";
            chartRadar.ChartAreas.Add(chartArea1);
            chartRadar.Enabled = false;
            legend1.Name = "Legend1";
            chartRadar.Legends.Add(legend1);
            chartRadar.Location = new Point(534, 45);
            chartRadar.Margin = new Padding(3, 2, 3, 2);
            chartRadar.Name = "chartRadar";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartRadar.Series.Add(series1);
            chartRadar.Size = new Size(328, 281);
            chartRadar.TabIndex = 21;
            chartRadar.Text = "chart1";
            // 
            // lblTotalValues
            // 
            lblTotalValues.AutoSize = true;
            lblTotalValues.Location = new Point(40, 262);
            lblTotalValues.Name = "lblTotalValues";
            lblTotalValues.Size = new Size(79, 15);
            lblTotalValues.TabIndex = 6;
            lblTotalValues.Text = "lblTotalValues";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(40, 240);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(46, 15);
            lblTotal.TabIndex = 5;
            lblTotal.Text = "lblTotal";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(40, 36);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(43, 15);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "lblTitle";
            // 
            // btnClose
            // 
            btnClose.Location = new Point(282, 274);
            btnClose.Margin = new Padding(3, 2, 3, 2);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(109, 22);
            btnClose.TabIndex = 3;
            btnClose.Text = "button1";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(40, 64);
            dataGridView.Margin = new Padding(3, 2, 3, 2);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(473, 160);
            dataGridView.TabIndex = 2;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(chartComparison);
            tabPage2.Controls.Add(chartNutrientsPie);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Margin = new Padding(3, 2, 3, 2);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3, 2, 3, 2);
            tabPage2.Size = new Size(891, 415);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Оптимальность";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // chartComparison
            // 
            chartArea2.Name = "ChartArea1";
            chartComparison.ChartAreas.Add(chartArea2);
            chartComparison.Enabled = false;
            legend2.Name = "Legend1";
            chartComparison.Legends.Add(legend2);
            chartComparison.Location = new Point(333, 43);
            chartComparison.Margin = new Padding(3, 2, 3, 2);
            chartComparison.Name = "chartComparison";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartComparison.Series.Add(series2);
            chartComparison.Size = new Size(531, 307);
            chartComparison.TabIndex = 1;
            chartComparison.Text = "chart1";
            // 
            // chartNutrientsPie
            // 
            chartArea3.Name = "ChartArea1";
            chartNutrientsPie.ChartAreas.Add(chartArea3);
            chartNutrientsPie.Enabled = false;
            legend3.Name = "Legend1";
            chartNutrientsPie.Legends.Add(legend3);
            chartNutrientsPie.Location = new Point(24, 43);
            chartNutrientsPie.Margin = new Padding(3, 2, 3, 2);
            chartNutrientsPie.Name = "chartNutrientsPie";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            chartNutrientsPie.Series.Add(series3);
            chartNutrientsPie.Size = new Size(328, 281);
            chartNutrientsPie.TabIndex = 0;
            chartNutrientsPie.Text = "chart1";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(btnAnalyzeSensitivity);
            tabPage3.Controls.Add(txtSensitivityStep);
            tabPage3.Controls.Add(txtSensitivityMax);
            tabPage3.Controls.Add(lblSensitivityStep);
            tabPage3.Controls.Add(lblSensitivityMax);
            tabPage3.Controls.Add(lblSensitivityMin);
            tabPage3.Controls.Add(chartSensitivity);
            tabPage3.Controls.Add(groupBox1);
            tabPage3.Controls.Add(txtSensitivityMin);
            tabPage3.Controls.Add(cmbSensitivityNutrient);
            tabPage3.Controls.Add(lblSensitivityNutrient);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Margin = new Padding(3, 2, 3, 2);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(891, 415);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Анализ устойчивости";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnAnalyzeSensitivity
            // 
            btnAnalyzeSensitivity.Location = new Point(198, 316);
            btnAnalyzeSensitivity.Margin = new Padding(3, 2, 3, 2);
            btnAnalyzeSensitivity.Name = "btnAnalyzeSensitivity";
            btnAnalyzeSensitivity.Size = new Size(131, 22);
            btnAnalyzeSensitivity.TabIndex = 13;
            btnAnalyzeSensitivity.Text = "Рассчитать";
            btnAnalyzeSensitivity.UseVisualStyleBackColor = true;
            btnAnalyzeSensitivity.Click += btnAnalyzeSensitivity_Click;
            // 
            // txtSensitivityStep
            // 
            txtSensitivityStep.Location = new Point(220, 154);
            txtSensitivityStep.Margin = new Padding(3, 2, 3, 2);
            txtSensitivityStep.Name = "txtSensitivityStep";
            txtSensitivityStep.Size = new Size(110, 23);
            txtSensitivityStep.TabIndex = 23;
            // 
            // txtSensitivityMax
            // 
            txtSensitivityMax.Location = new Point(220, 130);
            txtSensitivityMax.Margin = new Padding(3, 2, 3, 2);
            txtSensitivityMax.Name = "txtSensitivityMax";
            txtSensitivityMax.Size = new Size(110, 23);
            txtSensitivityMax.TabIndex = 22;
            // 
            // lblSensitivityStep
            // 
            lblSensitivityStep.AutoSize = true;
            lblSensitivityStep.Location = new Point(53, 160);
            lblSensitivityStep.Name = "lblSensitivityStep";
            lblSensitivityStep.Size = new Size(29, 15);
            lblSensitivityStep.TabIndex = 21;
            lblSensitivityStep.Text = "Шаг";
            // 
            // lblSensitivityMax
            // 
            lblSensitivityMax.AutoSize = true;
            lblSensitivityMax.Location = new Point(53, 135);
            lblSensitivityMax.Name = "lblSensitivityMax";
            lblSensitivityMax.Size = new Size(145, 15);
            lblSensitivityMax.TabIndex = 20;
            lblSensitivityMax.Text = "Максимальное значение";
            // 
            // lblSensitivityMin
            // 
            lblSensitivityMin.AutoSize = true;
            lblSensitivityMin.Location = new Point(53, 110);
            lblSensitivityMin.Name = "lblSensitivityMin";
            lblSensitivityMin.Size = new Size(141, 15);
            lblSensitivityMin.TabIndex = 19;
            lblSensitivityMin.Text = "Минимальное значение";
            // 
            // chartSensitivity
            // 
            chartSensitivity.BackColor = Color.WhiteSmoke;
            chartArea4.Name = "ChartArea1";
            chartSensitivity.ChartAreas.Add(chartArea4);
            chartSensitivity.Enabled = false;
            legend4.Name = "Legend1";
            chartSensitivity.Legends.Add(legend4);
            chartSensitivity.Location = new Point(394, 99);
            chartSensitivity.Margin = new Padding(3, 2, 3, 2);
            chartSensitivity.Name = "chartSensitivity";
            series4.ChartArea = "ChartArea1";
            series4.IsVisibleInLegend = false;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            chartSensitivity.Series.Add(series4);
            chartSensitivity.Size = new Size(366, 205);
            chartSensitivity.TabIndex = 18;
            chartSensitivity.Text = "chart1";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtSensitivityFixedCarbs);
            groupBox1.Controls.Add(txtSensitivityFixedFat);
            groupBox1.Controls.Add(txtSensitivityFixedProtein);
            groupBox1.Controls.Add(lblSensitivityFixedCarbs);
            groupBox1.Controls.Add(lblSensitivityFixedProtein);
            groupBox1.Controls.Add(lblSensitivityFixedFat);
            groupBox1.Location = new Point(53, 179);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(276, 124);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Зафиксировать нутриенты при расчете";
            // 
            // txtSensitivityFixedCarbs
            // 
            txtSensitivityFixedCarbs.Location = new Point(121, 83);
            txtSensitivityFixedCarbs.Margin = new Padding(3, 2, 3, 2);
            txtSensitivityFixedCarbs.Name = "txtSensitivityFixedCarbs";
            txtSensitivityFixedCarbs.Size = new Size(110, 23);
            txtSensitivityFixedCarbs.TabIndex = 15;
            // 
            // txtSensitivityFixedFat
            // 
            txtSensitivityFixedFat.Location = new Point(121, 58);
            txtSensitivityFixedFat.Margin = new Padding(3, 2, 3, 2);
            txtSensitivityFixedFat.Name = "txtSensitivityFixedFat";
            txtSensitivityFixedFat.Size = new Size(110, 23);
            txtSensitivityFixedFat.TabIndex = 14;
            // 
            // txtSensitivityFixedProtein
            // 
            txtSensitivityFixedProtein.Location = new Point(121, 34);
            txtSensitivityFixedProtein.Margin = new Padding(3, 2, 3, 2);
            txtSensitivityFixedProtein.Name = "txtSensitivityFixedProtein";
            txtSensitivityFixedProtein.Size = new Size(110, 23);
            txtSensitivityFixedProtein.TabIndex = 13;
            // 
            // lblSensitivityFixedCarbs
            // 
            lblSensitivityFixedCarbs.AutoSize = true;
            lblSensitivityFixedCarbs.Location = new Point(18, 83);
            lblSensitivityFixedCarbs.Name = "lblSensitivityFixedCarbs";
            lblSensitivityFixedCarbs.Size = new Size(60, 15);
            lblSensitivityFixedCarbs.TabIndex = 10;
            lblSensitivityFixedCarbs.Text = "Углеводы";
            // 
            // lblSensitivityFixedProtein
            // 
            lblSensitivityFixedProtein.AutoSize = true;
            lblSensitivityFixedProtein.Location = new Point(18, 34);
            lblSensitivityFixedProtein.Name = "lblSensitivityFixedProtein";
            lblSensitivityFixedProtein.Size = new Size(40, 15);
            lblSensitivityFixedProtein.TabIndex = 8;
            lblSensitivityFixedProtein.Text = "Белок";
            // 
            // lblSensitivityFixedFat
            // 
            lblSensitivityFixedFat.AutoSize = true;
            lblSensitivityFixedFat.Location = new Point(18, 58);
            lblSensitivityFixedFat.Name = "lblSensitivityFixedFat";
            lblSensitivityFixedFat.Size = new Size(41, 15);
            lblSensitivityFixedFat.TabIndex = 9;
            lblSensitivityFixedFat.Text = "Жиры";
            // 
            // txtSensitivityMin
            // 
            txtSensitivityMin.Location = new Point(220, 105);
            txtSensitivityMin.Margin = new Padding(3, 2, 3, 2);
            txtSensitivityMin.Name = "txtSensitivityMin";
            txtSensitivityMin.Size = new Size(110, 23);
            txtSensitivityMin.TabIndex = 16;
            // 
            // cmbSensitivityNutrient
            // 
            cmbSensitivityNutrient.FormattingEnabled = true;
            cmbSensitivityNutrient.Location = new Point(220, 80);
            cmbSensitivityNutrient.Margin = new Padding(3, 2, 3, 2);
            cmbSensitivityNutrient.Name = "cmbSensitivityNutrient";
            cmbSensitivityNutrient.Size = new Size(110, 23);
            cmbSensitivityNutrient.TabIndex = 15;
            cmbSensitivityNutrient.SelectedIndexChanged += cmbSensitivityNutrient_SelectedIndexChanged;
            // 
            // lblSensitivityNutrient
            // 
            lblSensitivityNutrient.AutoSize = true;
            lblSensitivityNutrient.Location = new Point(56, 82);
            lblSensitivityNutrient.Name = "lblSensitivityNutrient";
            lblSensitivityNutrient.Size = new Size(103, 15);
            lblSensitivityNutrient.TabIndex = 14;
            lblSensitivityNutrient.Text = "Выбор нутриента";
            // 
            // dietTab
            // 
            dietTab.Controls.Add(swapRightLb);
            dietTab.Controls.Add(swapLeftLb);
            dietTab.Controls.Add(additionalReceptionPanel);
            dietTab.Controls.Add(lunchPanel);
            dietTab.Controls.Add(dinnerPanel);
            dietTab.Controls.Add(breakfastPanel);
            dietTab.Location = new Point(4, 24);
            dietTab.Name = "dietTab";
            dietTab.Padding = new Padding(3);
            dietTab.Size = new Size(891, 415);
            dietTab.TabIndex = 3;
            dietTab.Text = "Рацион";
            dietTab.UseVisualStyleBackColor = true;
            // 
            // swapRightLb
            // 
            swapRightLb.AutoSize = true;
            swapRightLb.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            swapRightLb.Location = new Point(473, 387);
            swapRightLb.Name = "swapRightLb";
            swapRightLb.Size = new Size(25, 25);
            swapRightLb.TabIndex = 5;
            swapRightLb.Text = ">";
            swapRightLb.Click += swapRightLb_Click;
            // 
            // swapLeftLb
            // 
            swapLeftLb.AutoSize = true;
            swapLeftLb.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            swapLeftLb.Location = new Point(390, 387);
            swapLeftLb.Name = "swapLeftLb";
            swapLeftLb.Size = new Size(25, 25);
            swapLeftLb.TabIndex = 4;
            swapLeftLb.Text = "<";
            swapLeftLb.Click += swapLeftLb_Click;
            // 
            // additionalReceptionPanel
            // 
            additionalReceptionPanel.BackColor = Color.LightGoldenrodYellow;
            additionalReceptionPanel.Controls.Add(additionalReceptionPB);
            additionalReceptionPanel.Controls.Add(additionalReceptionWeightLb);
            additionalReceptionPanel.Controls.Add(additionalReceptionKcalLb);
            additionalReceptionPanel.Controls.Add(additionalReceptionLb);
            additionalReceptionPanel.Location = new Point(462, 221);
            additionalReceptionPanel.Name = "additionalReceptionPanel";
            additionalReceptionPanel.Size = new Size(412, 163);
            additionalReceptionPanel.TabIndex = 3;
            // 
            // additionalReceptionPB
            // 
            additionalReceptionPB.Image = Properties.Resources.additionalReception;
            additionalReceptionPB.Location = new Point(235, 33);
            additionalReceptionPB.Name = "additionalReceptionPB";
            additionalReceptionPB.Size = new Size(162, 117);
            additionalReceptionPB.SizeMode = PictureBoxSizeMode.StretchImage;
            additionalReceptionPB.TabIndex = 5;
            additionalReceptionPB.TabStop = false;
            // 
            // additionalReceptionWeightLb
            // 
            additionalReceptionWeightLb.AutoSize = true;
            additionalReceptionWeightLb.Location = new Point(11, 93);
            additionalReceptionWeightLb.Name = "additionalReceptionWeightLb";
            additionalReceptionWeightLb.Size = new Size(26, 15);
            additionalReceptionWeightLb.TabIndex = 4;
            additionalReceptionWeightLb.Text = "Вес";
            // 
            // additionalReceptionKcalLb
            // 
            additionalReceptionKcalLb.AutoSize = true;
            additionalReceptionKcalLb.Location = new Point(11, 56);
            additionalReceptionKcalLb.Name = "additionalReceptionKcalLb";
            additionalReceptionKcalLb.Size = new Size(81, 15);
            additionalReceptionKcalLb.TabIndex = 2;
            additionalReceptionKcalLb.Text = "Килакалорий";
            // 
            // additionalReceptionLb
            // 
            additionalReceptionLb.AutoSize = true;
            additionalReceptionLb.Location = new Point(120, 11);
            additionalReceptionLb.Name = "additionalReceptionLb";
            additionalReceptionLb.Size = new Size(178, 15);
            additionalReceptionLb.TabIndex = 0;
            additionalReceptionLb.Text = "Дополнительный прием пищи";
            // 
            // lunchPanel
            // 
            lunchPanel.BackColor = Color.LightCoral;
            lunchPanel.Controls.Add(lunchPB);
            lunchPanel.Controls.Add(lunchWeightLb);
            lunchPanel.Controls.Add(lunchKcalLb);
            lunchPanel.Controls.Add(lunchLb);
            lunchPanel.Location = new Point(462, 25);
            lunchPanel.Name = "lunchPanel";
            lunchPanel.Size = new Size(412, 163);
            lunchPanel.TabIndex = 2;
            // 
            // lunchPB
            // 
            lunchPB.Image = Properties.Resources.lunch;
            lunchPB.Location = new Point(235, 32);
            lunchPB.Name = "lunchPB";
            lunchPB.Size = new Size(162, 117);
            lunchPB.SizeMode = PictureBoxSizeMode.StretchImage;
            lunchPB.TabIndex = 4;
            lunchPB.TabStop = false;
            // 
            // lunchWeightLb
            // 
            lunchWeightLb.AutoSize = true;
            lunchWeightLb.Location = new Point(11, 84);
            lunchWeightLb.Name = "lunchWeightLb";
            lunchWeightLb.Size = new Size(26, 15);
            lunchWeightLb.TabIndex = 3;
            lunchWeightLb.Text = "Вес";
            // 
            // lunchKcalLb
            // 
            lunchKcalLb.AutoSize = true;
            lunchKcalLb.Location = new Point(11, 41);
            lunchKcalLb.Name = "lunchKcalLb";
            lunchKcalLb.Size = new Size(81, 15);
            lunchKcalLb.TabIndex = 2;
            lunchKcalLb.Text = "Килакалорий";
            // 
            // lunchLb
            // 
            lunchLb.AutoSize = true;
            lunchLb.Location = new Point(196, 7);
            lunchLb.Name = "lunchLb";
            lunchLb.Size = new Size(35, 15);
            lunchLb.TabIndex = 0;
            lunchLb.Text = "Обед";
            // 
            // dinnerPanel
            // 
            dinnerPanel.BackColor = Color.Chartreuse;
            dinnerPanel.Controls.Add(dinnerPB);
            dinnerPanel.Controls.Add(dinnerWeightLb);
            dinnerPanel.Controls.Add(dinnerKcalLb);
            dinnerPanel.Controls.Add(dinnerLb);
            dinnerPanel.Location = new Point(15, 221);
            dinnerPanel.Name = "dinnerPanel";
            dinnerPanel.Size = new Size(412, 163);
            dinnerPanel.TabIndex = 1;
            // 
            // dinnerPB
            // 
            dinnerPB.Image = Properties.Resources.dinner;
            dinnerPB.Location = new Point(238, 33);
            dinnerPB.Name = "dinnerPB";
            dinnerPB.Size = new Size(162, 117);
            dinnerPB.SizeMode = PictureBoxSizeMode.StretchImage;
            dinnerPB.TabIndex = 4;
            dinnerPB.TabStop = false;
            // 
            // dinnerWeightLb
            // 
            dinnerWeightLb.AutoSize = true;
            dinnerWeightLb.Location = new Point(6, 93);
            dinnerWeightLb.Name = "dinnerWeightLb";
            dinnerWeightLb.Size = new Size(26, 15);
            dinnerWeightLb.TabIndex = 3;
            dinnerWeightLb.Text = "Вес";
            // 
            // dinnerKcalLb
            // 
            dinnerKcalLb.AutoSize = true;
            dinnerKcalLb.Location = new Point(6, 56);
            dinnerKcalLb.Name = "dinnerKcalLb";
            dinnerKcalLb.Size = new Size(81, 15);
            dinnerKcalLb.TabIndex = 2;
            dinnerKcalLb.Text = "Килакалорий";
            // 
            // dinnerLb
            // 
            dinnerLb.AutoSize = true;
            dinnerLb.Location = new Point(187, 11);
            dinnerLb.Name = "dinnerLb";
            dinnerLb.Size = new Size(37, 15);
            dinnerLb.TabIndex = 0;
            dinnerLb.Text = "Ужин";
            // 
            // breakfastPanel
            // 
            breakfastPanel.BackColor = Color.LightSkyBlue;
            breakfastPanel.Controls.Add(breakfatPB);
            breakfastPanel.Controls.Add(breakfastWeightLb);
            breakfastPanel.Controls.Add(breakfastKcalLb);
            breakfastPanel.Controls.Add(breakfastLb);
            breakfastPanel.Location = new Point(15, 25);
            breakfastPanel.Name = "breakfastPanel";
            breakfastPanel.Size = new Size(412, 163);
            breakfastPanel.TabIndex = 0;
            // 
            // breakfatPB
            // 
            breakfatPB.Image = Properties.Resources.breakfast;
            breakfatPB.Location = new Point(238, 32);
            breakfatPB.Name = "breakfatPB";
            breakfatPB.Size = new Size(162, 117);
            breakfatPB.SizeMode = PictureBoxSizeMode.StretchImage;
            breakfatPB.TabIndex = 5;
            breakfatPB.TabStop = false;
            // 
            // breakfastWeightLb
            // 
            breakfastWeightLb.AutoSize = true;
            breakfastWeightLb.Location = new Point(6, 84);
            breakfastWeightLb.Name = "breakfastWeightLb";
            breakfastWeightLb.Size = new Size(26, 15);
            breakfastWeightLb.TabIndex = 2;
            breakfastWeightLb.Text = "Вес";
            // 
            // breakfastKcalLb
            // 
            breakfastKcalLb.AutoSize = true;
            breakfastKcalLb.Location = new Point(6, 41);
            breakfastKcalLb.Name = "breakfastKcalLb";
            breakfastKcalLb.Size = new Size(81, 15);
            breakfastKcalLb.TabIndex = 1;
            breakfastKcalLb.Text = "Килакалорий";
            // 
            // breakfastLb
            // 
            breakfastLb.AutoSize = true;
            breakfastLb.Location = new Point(175, 7);
            breakfastLb.Name = "breakfastLb";
            breakfastLb.Size = new Size(50, 15);
            breakfastLb.TabIndex = 0;
            breakfastLb.Text = "Завтрак";
            // 
            // ResultsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(899, 443);
            Controls.Add(tabControl1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "ResultsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Результаты оптимизации";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartRadar).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartComparison).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartNutrientsPie).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartSensitivity).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            dietTab.ResumeLayout(false);
            dietTab.PerformLayout();
            additionalReceptionPanel.ResumeLayout(false);
            additionalReceptionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)additionalReceptionPB).EndInit();
            lunchPanel.ResumeLayout(false);
            lunchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)lunchPB).EndInit();
            dinnerPanel.ResumeLayout(false);
            dinnerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dinnerPB).EndInit();
            breakfastPanel.ResumeLayout(false);
            breakfastPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)breakfatPB).EndInit();
            ResumeLayout(false);
        }

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private DataGridView dataGridView;
        private Button btnClose;
        private Label lblTotalValues;
        private Label lblTotal;
        private Label lblTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRadar;
        private Button btnAnalyzeSensitivity;
        private TextBox txtSensitivityStep;
        private TextBox txtSensitivityMax;
        private Label lblSensitivityStep;
        private Label lblSensitivityMax;
        private Label lblSensitivityMin;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSensitivity;
        private GroupBox groupBox1;
        private TextBox txtSensitivityFixedCarbs;
        private TextBox txtSensitivityFixedFat;
        private TextBox txtSensitivityFixedProtein;
        private Label lblSensitivityFixedCarbs;
        private Label lblSensitivityFixedProtein;
        private Label lblSensitivityFixedFat;
        private TextBox txtSensitivityMin;
        private ComboBox cmbSensitivityNutrient;
        private Label lblSensitivityNutrient;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartComparison;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNutrientsPie;
        private TabPage dietTab;
        private Label swapRightLb;
        private Label swapLeftLb;
        private Panel additionalReceptionPanel;
        private Panel lunchPanel;
        private Panel dinnerPanel;
        private Panel breakfastPanel;
        private Label additionalReceptionLb;
        private Label lunchLb;
        private Label dinnerLb;
        private Label breakfastLb;
        private Label additionalReceptionWeightLb;
        private Label additionalReceptionKcalLb;
        private Label lunchWeightLb;
        private Label lunchKcalLb;
        private Label dinnerWeightLb;
        private Label dinnerKcalLb;
        private Label breakfastWeightLb;
        private Label breakfastKcalLb;
        private PictureBox additionalReceptionPB;
        private PictureBox lunchPB;
        private PictureBox dinnerPB;
        private PictureBox breakfatPB;
    }
}