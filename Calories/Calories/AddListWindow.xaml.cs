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
    /// Interaction logic for AddListWindow.xaml
    /// </summary>
    public partial class AddListWindow : Window
    {
        private CaloriesEntities db;
        private Lists list;
        public AddListWindow()
        {
            InitializeComponent();

            list = new Lists
            {
                Creation_date = DateTime.Now            
            };
            

            db = new CaloriesEntities();
            db.Products.Load();

            ProductsLVS.DataContext = db.Products.Local;
        }

        private void AddSelected_Button(object sender, RoutedEventArgs e)
        {
            foreach(Products item in ProductsLVS.SelectedItems)
            {
                ProductsLists pl = new ProductsLists();
                pl.Lists = list;
                pl.Products = item;
                list.ProductsLists.Add(pl);
            }
        }

        private void RemoveAll_Button(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveSelected_Button(object sender, RoutedEventArgs e)
        {

        }

        private void AddAll_Button(object sender, RoutedEventArgs e)
        {

        }

        private void AddList_Button(object sender, RoutedEventArgs e)
        {
            list.Name = lNameTextBox.Text;
            db.Lists.Add(list);
            db.SaveChanges();
        }
    }
}
