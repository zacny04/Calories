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
using System.Data.Entity;

namespace Calories
{
    /// <summary>
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        private CaloriesEntities db;
        /// <summary>
        /// Window containing all products in Products table
        /// </summary>
        public ProductsWindow()
        {
            InitializeComponent();
            db = new CaloriesEntities();
            db.Products.Load();
            ProductsLV.DataContext = db.Products.Local;
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            Products product = new Products();
            AddProductWindow addPWindow = new AddProductWindow(product);
            addPWindow.Closing += AddPWindow_Closing;
            addPWindow.ShowDialog();

        }

        private void AddPWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Products.Load();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Products p = ((ListViewItem)sender).Content as Products;
            AddProductWindow addPWindow = new AddProductWindow(p);
            addPWindow.Closing += AddPWindow_Closing;
            addPWindow.ShowDialog();
        }
    }
}