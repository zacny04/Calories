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
     /// Interaction logic for ShowListWindow.xaml
     /// </summary>
     public partial class ShowListWindow : Window
     {
         public ShowListWindow(Lists list)
         {
             InitializeComponent();
 
             ListName.Text = list.Name;
 
             ListProductsLV.DataContext = list.ProductsLists;
 
            int calories_sum = 0;
 
             foreach(ProductsLists pl in list.ProductsLists)
             {
                 if(pl.Products.calories != null)
                 {
                     calories_sum += pl.Amount * (int)pl.Products.calories;
                 }
                 
             }
 
             CaloriesCount.Text = "£¹czna iloœæ kalorii: " + calories_sum.ToString();
             
         }
     }
 }