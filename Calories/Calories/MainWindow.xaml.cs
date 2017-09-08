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

namespace Calories
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Lists> productsLists;
         /// <summary>
         /// Start window with listview showing products lists
         /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            productsLists = new ObservableCollection<Lists>();
            ProductsListView.DataContext = productsLists;
            using (var db = new CaloriesEntities())
            {
                var list = from l in db.Lists select l;
                foreach(var l in list)
                {
                    productsLists.Add(l);
                }
             }
        }
    }
}
