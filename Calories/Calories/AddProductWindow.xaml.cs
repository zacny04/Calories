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
        public AddProductWindow()
        {
            InitializeComponent();
        }

        private void pAddButton_Click(object sender, RoutedEventArgs e)
        {
            Regex nameRegex = new Regex(namePattern), numberRegex = new Regex(numberPattern);
            if (nameRegex.IsMatch(pNameTextBox.Text) && numberRegex.IsMatch(pWeightTextBox.Text) && numberRegex.IsMatch(pCaloriesTextBox.Text))
            {
                using (CaloriesEntities db = new CaloriesEntities())
                {
                    Products product = new Products
                    {
                        Name = pNameTextBox.Text
                    };
                    try
                    {
                        product.weight = Int32.Parse(pWeightTextBox.Text);
                        product.calories = Int32.Parse(pCaloriesTextBox.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }                    
                    db.Products.Add(product);
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
                SystemSounds.Hand.Play();
            }
        }

        private void pCancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    public class IsValidString : IValueConverter
    {
        private string namePattern = @"(^$)|(^[\w\s]+$)";
        private string numberPattern = @"(^$)|(^[\d]+$)";
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
