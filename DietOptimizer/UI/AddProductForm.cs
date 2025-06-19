using DietOptimizer.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DietOptimizer
{
    public partial class AddProductForm : Form
    {
        public Product NewProduct { get; private set; }

        public AddProductForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Добавить новый продукт";
            this.Size = new Size(300, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            var lblName = new Label { Text = "Название:", Left = 20, Top = 20, Width = 100 };
            var txtName = new TextBox { Left = 120, Top = 20, Width = 150 };

            var lblProtein = new Label { Text = "Белки (г):", Left = 20, Top = 50, Width = 100 };
            var txtProtein = new TextBox { Left = 120, Top = 50, Width = 150 };

            var lblFat = new Label { Text = "Жиры (г):", Left = 20, Top = 80, Width = 100 };
            var txtFat = new TextBox { Left = 120, Top = 80, Width = 150 };

            var lblCarbs = new Label { Text = "Углеводы (г):", Left = 20, Top = 110, Width = 100 };
            var txtCarbs = new TextBox { Left = 120, Top = 110, Width = 150 };

            var lblKcal = new Label { Text = "Ккал:", Left = 20, Top = 140, Width = 100 };
            var txtKcal = new TextBox { Left = 120, Top = 140, Width = 150 };

            var lblPrice = new Label { Text = "Цена:", Left = 20, Top = 170, Width = 100 };
            var txtPrice = new TextBox { Left = 120, Top = 170, Width = 150 };

            var btnAdd = new Button { Text = "Добавить", Left = 100, Top = 200, Width = 80 };
            var btnCancel = new Button { Text = "Отмена", Left = 190, Top = 200, Width = 80 };

            btnAdd.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Введите название продукта");
                    return;
                }

                if (!double.TryParse(txtProtein.Text, out double protein) ||
                    !double.TryParse(txtFat.Text, out double fat) ||
                    !double.TryParse(txtCarbs.Text, out double carbs) ||
                    !double.TryParse(txtKcal.Text, out double kcal) ||
                    !double.TryParse(txtPrice.Text, out double price))
                {
                    MessageBox.Show("Введите корректные числовые значения");
                    return;
                }

                NewProduct = new Product
                {
                    Name = txtName.Text,
                    Protein = protein,
                    Fat = fat,
                    Carbs = carbs,
                    Kcal = kcal,
                    Price = price
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblProtein);
            this.Controls.Add(txtProtein);
            this.Controls.Add(lblFat);
            this.Controls.Add(txtFat);
            this.Controls.Add(lblCarbs);
            this.Controls.Add(txtCarbs);
            this.Controls.Add(lblKcal);
            this.Controls.Add(txtKcal);
            this.Controls.Add(lblPrice);
            this.Controls.Add(txtPrice);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnCancel);
        }
    }
}