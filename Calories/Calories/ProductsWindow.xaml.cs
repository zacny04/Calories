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

namespace Calories
{
    /// <summary>
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        /// <summary>
        /// Window containing all products in Products table
        /// </summary>
        public ProductsWindow()
        {
            InitializeComponent();
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            Products product = new Products();
            AddProductWindow addPWindow = new AddProductWindow(product);
            addPWindow.ShowDialog();
        }
    }
}
