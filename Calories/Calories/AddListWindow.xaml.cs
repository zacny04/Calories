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
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Calories
{
    /// <summary>
    /// Interaction logic for AddListWindow.xaml
    /// </summary>
    public partial class AddListWindow : Window
    {
        private CaloriesEntities db;
        private Lists list;
        private ObservableCollection<ProductsLists> prodList;
        /// <summary>
        /// Constructor of Window for adding new List
        /// </summary>
        public AddListWindow()
        {
            InitializeComponent();

            list = new Lists
            {
                Creation_date = DateTime.Now
            };

            prodList = new ObservableCollection<ProductsLists>();

            db = new CaloriesEntities();
            db.Products.Load();
            ProductsListsLV.DataContext = prodList;
            ProductsLVS.DataContext = db.Products.Local;
        }

        private void AddSelected_Button(object sender, RoutedEventArgs e)
        {
            var products = from pr in prodList select pr.Products;

            foreach (Products item in ProductsLVS.SelectedItems)
            {
                if (!products.Contains(item))
                {
                    ProductsLists pl = new ProductsLists
                    {
                        Lists = list,
                        Products = item
                    };
                    prodList.Add(pl);
                }
            }
        }

        private void RemoveAll_Button(object sender, RoutedEventArgs e)
        {
            prodList.Clear();
        }

        private void RemoveSelected_Button(object sender, RoutedEventArgs e)
        {
            while (ProductsListsLV.SelectedItems.Count > 0)
            {
                if (ProductsListsLV.SelectedItems[0] is ProductsLists pl)
                {
                    prodList.Remove(pl);
                }
            }
        }

        private void AddAll_Button(object sender, RoutedEventArgs e)
        {
            var products = from pr in prodList select pr.Products;
            foreach (Products item in ProductsLVS.Items)
            {
                if (!products.Contains(item))
                {
                    ProductsLists pl = new ProductsLists
                    {
                        Lists = list,
                        Products = item
                    };
                    prodList.Add(pl);
                }
            }
        }

        private void AddList_Button(object sender, RoutedEventArgs e)
        {
            list.Name = lNameTextBox.Text;
            foreach (ProductsLists pl in prodList)
            {
                list.ProductsLists.Add(pl);
            }
            db.Lists.Add(list);
            db.SaveChanges();
            Close();
        }

        private void Close_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
         private void Amount_LostFocus(object sender, RoutedEventArgs e)
         {
             TextBox tb = sender as TextBox;
             if(!Regex.IsMatch(tb.Text,"^[0-9]{1,}$"))
             {
                 MessageBox.Show("Wprowadzona wartość nie jest liczbą", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                 tb.Text = "0";
             } else
             {
                 ListViewItem lvI = GetAncestorOfType<ListViewItem>(tb);
                 if(lvI != null)
                 {
                     ProductsLists pl = lvI.Content as ProductsLists;
                     pl.Amount = int.Parse(tb.Text);
                 }
             }
         }
        private T GetAncestorOfType<T>(FrameworkElement child) where T : FrameworkElement
        {
             var parent = VisualTreeHelper.GetParent(child);
             if (parent != null && !(parent is T))
                 return (T)GetAncestorOfType<T>((FrameworkElement)parent);
             return (T)parent;
        }
    }
}