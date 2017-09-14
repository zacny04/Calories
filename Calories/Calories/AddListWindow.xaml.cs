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
            foreach (Products item in ProductsLVS.SelectedItems)
            {

                ProductsLists pl = new ProductsLists();
                pl.Lists = list;
                pl.Products = item;
                prodList.Add(pl);
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
                ProductsLists pl = ProductsListsLV.SelectedItems[0] as ProductsLists;
                if (pl != null)
                {
                    prodList.Remove(pl);
                }
            }
        }

        private void AddAll_Button(object sender, RoutedEventArgs e)
        {
            foreach (Products item in ProductsLVS.Items)
            {

                ProductsLists pl = new ProductsLists();
                pl.Lists = list;
                pl.Products = item;
                prodList.Add(pl);
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
        }
    }
}