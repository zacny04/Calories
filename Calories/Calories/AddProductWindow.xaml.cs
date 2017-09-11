using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Media;

namespace Calories
{
    /// <summary>
    /// Logika interakcji dla klasy AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private string namePattern = @"(^[\w\s]+$)";
        private string numberPattern = @"(^[\d]+$)";
        private Products _product;
        /// <summary>
        /// Window to add or modify Product
        /// </summary>
        /// <param name="product">Product should be a new object if we call add Product or exact object if we're modyfing it</param>
        public AddProductWindow(Products product)
        {
            InitializeComponent();
            _product = product;
            if (_product.Id != 0)
            {
                this.Title = "Modify Product";
                pAddButton.Content = "Modify";
                this.DataContext = _product;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Regex nameRegex = new Regex(namePattern), numberRegex = new Regex(numberPattern);
            if (nameRegex.IsMatch(pNameTextBox.Text) && numberRegex.IsMatch(pWeightTextBox.Text) && numberRegex.IsMatch(pCaloriesTextBox.Text))
            {
                using (CaloriesEntities db = new CaloriesEntities())
                {
                    if (_product.Id != 0)
                    {
                        var result = db.Products.SingleOrDefault(b => b.Id == _product.Id);
                        if (result != null)
                        {
                            result.Name = pNameTextBox.Text;
                            try
                            {
                                result.weight = Int32.Parse(pWeightTextBox.Text);
                                result.calories = Int32.Parse(pCaloriesTextBox.Text);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            try
                            {
                                db.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        _product.Name = pNameTextBox.Text;
                        try
                        {
                            _product.weight = Int32.Parse(pWeightTextBox.Text);
                            _product.calories = Int32.Parse(pCaloriesTextBox.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        db.Products.Add(_product);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                this.DialogResult = true;
            }
            else
            {
                SystemSounds.Hand.Play();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
    /// <summary>
    /// Helper class to determine if user entered valid text 
    /// </summary>
    public class IsValidString : IValueConverter
    {
        private string namePattern = @"(^$)|(^[\w\s]+$)";
        private string numberPattern = @"(^$)|(^[\d]+$)";
        /// <summary>
        /// Basic Convert metohd of Helper class
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>true if value is correct, false if not</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Regex rgx;
            if (parameter != null && parameter is string)
            {
                if (parameter.ToString().Equals("pNameTextBox"))
                    rgx = new Regex(namePattern);
                else
                    return false;
            }
            else
            {
                rgx = new Regex(numberPattern);
            }
            if (rgx.IsMatch(value.ToString()))
                return true;
            else
                return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return String.Empty;
        }
    }
}
