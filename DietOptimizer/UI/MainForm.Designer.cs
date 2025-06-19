namespace DietOptimizer
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupProfile;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.NumericUpDown numAge;
        private System.Windows.Forms.ComboBox cmbActivity;
        private System.Windows.Forms.NumericUpDown numWeight;
        private System.Windows.Forms.NumericUpDown numHeight;


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
            dataGridViewProducts = new DataGridView();
            typeMethodCMB = new ComboBox();
            groupProfile = new GroupBox();
            heightLb = new Label();
            weightLb = new Label();
            ageLb = new Label();
            activityLb = new Label();
            genderLb = new Label();
            cmbGender = new ComboBox();
            numAge = new NumericUpDown();
            cmbActivity = new ComboBox();
            numWeight = new NumericUpDown();
            numHeight = new NumericUpDown();
            txtProtein = new TextBox();
            txtFat = new TextBox();
            txtCarbs = new TextBox();
            lblProtein = new Label();
            lblFat = new Label();
            lblCarbs = new Label();
            txtBudget = new TextBox();
            cmbProteinWeight = new ComboBox();
            cmbFatWeight = new ComboBox();
            cmbCarbsWeight = new ComboBox();
            lblBudget = new Label();
            lblProteinWeight = new Label();
            lblFatWeight = new Label();
            lblCarbsWeight = new Label();
            btnAddProduct = new Button();
            btnDeleteProduct = new Button();
            btnOptimize = new Button();
            tabPageWhatIf = new TabControl();
            tabPage1 = new TabPage();
            newbieCB = new CheckBox();
            pictureStatus = new PictureBox();
            groupBox3 = new GroupBox();
            rbMaximizeEnergy = new RadioButton();
            rbMinimizeWeight = new RadioButton();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).BeginInit();
            groupProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numWeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHeight).BeginInit();
            tabPageWhatIf.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureStatus).BeginInit();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewProducts
            // 
            dataGridViewProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewProducts.ColumnHeadersHeight = 29;
            dataGridViewProducts.Location = new Point(9, 205);
            dataGridViewProducts.Margin = new Padding(3, 2, 3, 2);
            dataGridViewProducts.Name = "dataGridViewProducts";
            dataGridViewProducts.RowHeadersWidth = 51;
            dataGridViewProducts.Size = new Size(676, 250);
            dataGridViewProducts.TabIndex = 0;
            // 
            // typeMethodCMB
            // 
            typeMethodCMB.Items.AddRange(new object[] { "Метод ветвей и границ", "Метод Гомори" });
            typeMethodCMB.Location = new Point(261, 101);
            typeMethodCMB.Margin = new Padding(3, 2, 3, 2);
            typeMethodCMB.Name = "typeMethodCMB";
            typeMethodCMB.Size = new Size(176, 23);
            typeMethodCMB.TabIndex = 0;
            typeMethodCMB.SelectedIndexChanged += cmbObjectiveType_SelectedIndexChanged;
            // 
            // groupProfile
            // 
            groupProfile.Controls.Add(heightLb);
            groupProfile.Controls.Add(weightLb);
            groupProfile.Controls.Add(ageLb);
            groupProfile.Controls.Add(activityLb);
            groupProfile.Controls.Add(genderLb);
            groupProfile.Controls.Add(cmbGender);
            groupProfile.Controls.Add(numAge);
            groupProfile.Controls.Add(cmbActivity);
            groupProfile.Controls.Add(numWeight);
            groupProfile.Controls.Add(numHeight);
            groupProfile.Location = new Point(9, 15);
            groupProfile.Margin = new Padding(3, 2, 3, 2);
            groupProfile.Name = "groupProfile";
            groupProfile.Padding = new Padding(3, 2, 3, 2);
            groupProfile.Size = new Size(247, 186);
            groupProfile.TabIndex = 0;
            groupProfile.TabStop = false;
            groupProfile.Text = "Профиль пользователя";
            // 
            // heightLb
            // 
            heightLb.AutoSize = true;
            heightLb.Location = new Point(6, 131);
            heightLb.Name = "heightLb";
            heightLb.Size = new Size(35, 15);
            heightLb.TabIndex = 23;
            heightLb.Text = "Рост:";
            // 
            // weightLb
            // 
            weightLb.AutoSize = true;
            weightLb.Location = new Point(6, 103);
            weightLb.Name = "weightLb";
            weightLb.Size = new Size(29, 15);
            weightLb.TabIndex = 22;
            weightLb.Text = "Вес:";
            // 
            // ageLb
            // 
            ageLb.AutoSize = true;
            ageLb.Location = new Point(6, 76);
            ageLb.Name = "ageLb";
            ageLb.Size = new Size(53, 15);
            ageLb.TabIndex = 21;
            ageLb.Text = "Возраст:";
            // 
            // activityLb
            // 
            activityLb.AutoSize = true;
            activityLb.Location = new Point(6, 44);
            activityLb.Name = "activityLb";
            activityLb.Size = new Size(121, 15);
            activityLb.TabIndex = 6;
            activityLb.Text = "Уровень активности:";
            // 
            // genderLb
            // 
            genderLb.AutoSize = true;
            genderLb.Location = new Point(9, 21);
            genderLb.Name = "genderLb";
            genderLb.Size = new Size(33, 15);
            genderLb.TabIndex = 5;
            genderLb.Text = "Пол:";
            // 
            // cmbGender
            // 
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.Location = new Point(48, 17);
            cmbGender.Margin = new Padding(3, 2, 3, 2);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(106, 23);
            cmbGender.TabIndex = 0;
            // 
            // numAge
            // 
            numAge.Location = new Point(61, 72);
            numAge.Margin = new Padding(3, 2, 3, 2);
            numAge.Minimum = new decimal(new int[] { 14, 0, 0, 0 });
            numAge.Name = "numAge";
            numAge.Size = new Size(52, 23);
            numAge.TabIndex = 1;
            numAge.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // cmbActivity
            // 
            cmbActivity.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbActivity.Location = new Point(128, 43);
            cmbActivity.Margin = new Padding(3, 2, 3, 2);
            cmbActivity.Name = "cmbActivity";
            cmbActivity.Size = new Size(106, 23);
            cmbActivity.TabIndex = 2;
            // 
            // numWeight
            // 
            numWeight.Location = new Point(39, 100);
            numWeight.Margin = new Padding(3, 2, 3, 2);
            numWeight.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numWeight.Minimum = new decimal(new int[] { 30, 0, 0, 0 });
            numWeight.Name = "numWeight";
            numWeight.Size = new Size(52, 23);
            numWeight.TabIndex = 3;
            numWeight.Value = new decimal(new int[] { 93, 0, 0, 0 });
            // 
            // numHeight
            // 
            numHeight.Location = new Point(46, 127);
            numHeight.Margin = new Padding(3, 2, 3, 2);
            numHeight.Maximum = new decimal(new int[] { 250, 0, 0, 0 });
            numHeight.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            numHeight.Name = "numHeight";
            numHeight.Size = new Size(52, 23);
            numHeight.TabIndex = 4;
            numHeight.Value = new decimal(new int[] { 180, 0, 0, 0 });
            // 
            // txtProtein
            // 
            txtProtein.Location = new Point(94, 18);
            txtProtein.Margin = new Padding(3, 2, 3, 2);
            txtProtein.Name = "txtProtein";
            txtProtein.Size = new Size(88, 23);
            txtProtein.TabIndex = 2;
            // 
            // txtFat
            // 
            txtFat.Location = new Point(94, 40);
            txtFat.Margin = new Padding(3, 2, 3, 2);
            txtFat.Name = "txtFat";
            txtFat.Size = new Size(88, 23);
            txtFat.TabIndex = 3;
            // 
            // txtCarbs
            // 
            txtCarbs.Location = new Point(94, 63);
            txtCarbs.Margin = new Padding(3, 2, 3, 2);
            txtCarbs.Name = "txtCarbs";
            txtCarbs.Size = new Size(88, 23);
            txtCarbs.TabIndex = 4;
            // 
            // lblProtein
            // 
            lblProtein.Location = new Point(7, 18);
            lblProtein.Name = "lblProtein";
            lblProtein.Size = new Size(88, 15);
            lblProtein.TabIndex = 5;
            lblProtein.Text = "Белки (г):";
            // 
            // lblFat
            // 
            lblFat.Location = new Point(7, 40);
            lblFat.Name = "lblFat";
            lblFat.Size = new Size(88, 15);
            lblFat.TabIndex = 6;
            lblFat.Text = "Жиры (г):";
            // 
            // lblCarbs
            // 
            lblCarbs.Location = new Point(7, 63);
            lblCarbs.Name = "lblCarbs";
            lblCarbs.Size = new Size(88, 15);
            lblCarbs.TabIndex = 7;
            lblCarbs.Text = "Углеводы (г):";
            // 
            // txtBudget
            // 
            txtBudget.Location = new Point(94, 90);
            txtBudget.Margin = new Padding(3, 2, 3, 2);
            txtBudget.Name = "txtBudget";
            txtBudget.Size = new Size(88, 23);
            txtBudget.TabIndex = 8;
            // 
            // cmbProteinWeight
            // 
            cmbProteinWeight.Location = new Point(94, 16);
            cmbProteinWeight.Margin = new Padding(3, 2, 3, 2);
            cmbProteinWeight.Name = "cmbProteinWeight";
            cmbProteinWeight.Size = new Size(88, 23);
            cmbProteinWeight.TabIndex = 9;
            // 
            // cmbFatWeight
            // 
            cmbFatWeight.Location = new Point(94, 41);
            cmbFatWeight.Margin = new Padding(3, 2, 3, 2);
            cmbFatWeight.Name = "cmbFatWeight";
            cmbFatWeight.Size = new Size(88, 23);
            cmbFatWeight.TabIndex = 10;
            // 
            // cmbCarbsWeight
            // 
            cmbCarbsWeight.Location = new Point(94, 67);
            cmbCarbsWeight.Margin = new Padding(3, 2, 3, 2);
            cmbCarbsWeight.Name = "cmbCarbsWeight";
            cmbCarbsWeight.Size = new Size(88, 23);
            cmbCarbsWeight.TabIndex = 11;
            // 
            // lblBudget
            // 
            lblBudget.Location = new Point(7, 92);
            lblBudget.Name = "lblBudget";
            lblBudget.Size = new Size(88, 15);
            lblBudget.TabIndex = 12;
            lblBudget.Text = "Бюджет:";
            // 
            // lblProteinWeight
            // 
            lblProteinWeight.Location = new Point(7, 20);
            lblProteinWeight.Name = "lblProteinWeight";
            lblProteinWeight.Size = new Size(88, 15);
            lblProteinWeight.TabIndex = 13;
            lblProteinWeight.Text = "Вес белков:";
            // 
            // lblFatWeight
            // 
            lblFatWeight.Location = new Point(6, 41);
            lblFatWeight.Name = "lblFatWeight";
            lblFatWeight.Size = new Size(88, 15);
            lblFatWeight.TabIndex = 14;
            lblFatWeight.Text = "Вес жиров:";
            // 
            // lblCarbsWeight
            // 
            lblCarbsWeight.Location = new Point(6, 63);
            lblCarbsWeight.Name = "lblCarbsWeight";
            lblCarbsWeight.Size = new Size(88, 15);
            lblCarbsWeight.TabIndex = 15;
            lblCarbsWeight.Text = "Вес углеводов:";
            // 
            // btnAddProduct
            // 
            btnAddProduct.Location = new Point(13, 467);
            btnAddProduct.Margin = new Padding(3, 2, 3, 2);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(88, 22);
            btnAddProduct.TabIndex = 16;
            btnAddProduct.Text = "Добавить";
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // btnDeleteProduct
            // 
            btnDeleteProduct.Location = new Point(109, 467);
            btnDeleteProduct.Margin = new Padding(3, 2, 3, 2);
            btnDeleteProduct.Name = "btnDeleteProduct";
            btnDeleteProduct.Size = new Size(88, 22);
            btnDeleteProduct.TabIndex = 17;
            btnDeleteProduct.Text = "Удалить";
            btnDeleteProduct.Click += btnDeleteProduct_Click;
            // 
            // btnOptimize
            // 
            btnOptimize.Location = new Point(555, 467);
            btnOptimize.Margin = new Padding(3, 2, 3, 2);
            btnOptimize.Name = "btnOptimize";
            btnOptimize.Size = new Size(105, 22);
            btnOptimize.TabIndex = 18;
            btnOptimize.Text = "Рассчитать";
            btnOptimize.Click += btnOptimize_Click;
            // 
            // tabPageWhatIf
            // 
            tabPageWhatIf.Controls.Add(tabPage1);
            tabPageWhatIf.Dock = DockStyle.Fill;
            tabPageWhatIf.Location = new Point(0, 0);
            tabPageWhatIf.Margin = new Padding(3, 2, 3, 2);
            tabPageWhatIf.Name = "tabPageWhatIf";
            tabPageWhatIf.SelectedIndex = 0;
            tabPageWhatIf.Size = new Size(1125, 526);
            tabPageWhatIf.TabIndex = 19;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(newbieCB);
            tabPage1.Controls.Add(pictureStatus);
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupProfile);
            tabPage1.Controls.Add(typeMethodCMB);
            tabPage1.Controls.Add(dataGridViewProducts);
            tabPage1.Controls.Add(btnOptimize);
            tabPage1.Controls.Add(btnDeleteProduct);
            tabPage1.Controls.Add(btnAddProduct);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Margin = new Padding(3, 2, 3, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 2, 3, 2);
            tabPage1.Size = new Size(1117, 498);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Диета";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // newbieCB
            // 
            newbieCB.AutoSize = true;
            newbieCB.Location = new Point(262, 142);
            newbieCB.Name = "newbieCB";
            newbieCB.Size = new Size(75, 19);
            newbieCB.TabIndex = 20;
            newbieCB.Text = "Новичок";
            newbieCB.UseVisualStyleBackColor = true;
            newbieCB.CheckedChanged += newbieCB_CheckedChanged;
            // 
            // pictureStatus
            // 
            pictureStatus.Location = new Point(697, 104);
            pictureStatus.Margin = new Padding(3, 2, 3, 2);
            pictureStatus.Name = "pictureStatus";
            pictureStatus.Size = new Size(411, 352);
            pictureStatus.TabIndex = 19;
            pictureStatus.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(rbMaximizeEnergy);
            groupBox3.Controls.Add(rbMinimizeWeight);
            groupBox3.Location = new Point(261, 15);
            groupBox3.Margin = new Padding(3, 2, 3, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 2, 3, 2);
            groupBox3.Size = new Size(216, 68);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "Цель";
            // 
            // rbMaximizeEnergy
            // 
            rbMaximizeEnergy.AutoSize = true;
            rbMaximizeEnergy.Location = new Point(5, 40);
            rbMaximizeEnergy.Margin = new Padding(3, 2, 3, 2);
            rbMaximizeEnergy.Name = "rbMaximizeEnergy";
            rbMaximizeEnergy.Size = new Size(209, 19);
            rbMaximizeEnergy.TabIndex = 1;
            rbMaximizeEnergy.TabStop = true;
            rbMaximizeEnergy.Text = "Максимизировать питательность";
            rbMaximizeEnergy.UseVisualStyleBackColor = true;
            // 
            // rbMinimizeWeight
            // 
            rbMinimizeWeight.AutoSize = true;
            rbMinimizeWeight.Location = new Point(5, 17);
            rbMinimizeWeight.Margin = new Padding(3, 2, 3, 2);
            rbMinimizeWeight.Name = "rbMinimizeWeight";
            rbMinimizeWeight.Size = new Size(165, 19);
            rbMinimizeWeight.TabIndex = 0;
            rbMinimizeWeight.TabStop = true;
            rbMinimizeWeight.Text = "Минимизация стоимости";
            rbMinimizeWeight.UseVisualStyleBackColor = true;
            rbMinimizeWeight.CheckedChanged += rbMinimizeWeight_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cmbProteinWeight);
            groupBox2.Controls.Add(cmbCarbsWeight);
            groupBox2.Controls.Add(lblCarbsWeight);
            groupBox2.Controls.Add(lblFatWeight);
            groupBox2.Controls.Add(txtBudget);
            groupBox2.Controls.Add(lblProteinWeight);
            groupBox2.Controls.Add(lblBudget);
            groupBox2.Controls.Add(txtProtein);
            groupBox2.Controls.Add(cmbFatWeight);
            groupBox2.Controls.Add(lblProtein);
            groupBox2.Controls.Add(txtFat);
            groupBox2.Controls.Add(lblFat);
            groupBox2.Controls.Add(txtCarbs);
            groupBox2.Controls.Add(lblCarbs);
            groupBox2.Location = new Point(482, 15);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(200, 121);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Корректировка плана";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1125, 526);
            Controls.Add(tabPageWhatIf);
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            Text = "Оптимизатор диеты";
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).EndInit();
            groupProfile.ResumeLayout(false);
            groupProfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAge).EndInit();
            ((System.ComponentModel.ISupportInitialize)numWeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHeight).EndInit();
            tabPageWhatIf.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureStatus).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dataGridViewProducts;
        private System.Windows.Forms.ComboBox typeMethodCMB;
        private System.Windows.Forms.TextBox txtProtein;
        private System.Windows.Forms.TextBox txtFat;
        private System.Windows.Forms.TextBox txtCarbs;
        private System.Windows.Forms.TextBox txtBudget;
        private System.Windows.Forms.ComboBox cmbProteinWeight;
        private System.Windows.Forms.ComboBox cmbFatWeight;
        private System.Windows.Forms.ComboBox cmbCarbsWeight;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Button btnOptimize;
        private System.Windows.Forms.Label lblProtein;
        private System.Windows.Forms.Label lblFat;
        private System.Windows.Forms.Label lblCarbs;
        private System.Windows.Forms.Label lblBudget;
        private System.Windows.Forms.Label lblProteinWeight;
        private System.Windows.Forms.Label lblFatWeight;
        private System.Windows.Forms.Label lblCarbsWeight;
        private TabControl tabPageWhatIf;
        private TabPage tabPage1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private RadioButton rbMaximizeEnergy;
        private RadioButton rbMinimizeWeight;
        private PictureBox pictureStatus;
        private CheckBox newbieCB;
        private Label heightLb;
        private Label weightLb;
        private Label ageLb;
        private Label activityLb;
        private Label genderLb;
    }
}