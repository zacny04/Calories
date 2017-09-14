using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Threading;

namespace Calories
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private CaloriesEntities db;
        /// <summary>
        /// Start window with listview showing products lists
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();


            Thread oTh = new Thread(new ThreadStart(() =>
            {
                db = new CaloriesEntities();
                db.Lists.Load();
                Dispatcher.Invoke(() =>
                {
                    ProductsListView.DataContext = db.Lists.Local;
                });
            }));
            oTh.IsBackground = true;
            if (!oTh.IsAlive)
            {
                oTh.Start();
            }
        }

        private void ShowProducts_Button(object sender, RoutedEventArgs e)
        {
            ProductsWindow pw = new ProductsWindow();
            pw.ShowDialog();
        }

        private void AddList_Button(object sender, RoutedEventArgs e)
        {
            AddListWindow alW = new AddListWindow();
            alW.Closing += AlW_Closing;
            alW.ShowDialog();
        }

        private void AlW_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Lists.Load();
        }
        private void DeleteList_Button(object sender, RoutedEventArgs e)
         {
             if(ProductsListView.SelectedItems.Count == 0)
             {
                 Lists l = ProductsListView.SelectedItem as Lists;
                 db.Lists.Remove(l);
                 db.SaveChanges();
             }
         }
    }
}