using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
 
 namespace Calories
 {
     class ListCaloriesConverter : IValueConverter
     {
         public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
         {
             if(value is Lists ls)
             {
                 int calories = 0;
                 foreach(ProductsLists pl in ls.ProductsLists)
                 {
                     if (pl.Products.calories != null)
                     {
                         calories += pl.Amount* (int) pl.Products.calories;
                     }
                     
                 }
                 return calories.ToString();
             }
 
             return "0";
         }
 
         public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
         {
             throw new NotImplementedException();
         }
     }
 
     class ProductsListCaloriesConverter : IValueConverter
     {
         public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
         {
             if (value is ProductsLists pl)
             {
                 int calories = 0;
 
                 if (pl.Products.calories != null)
                 {
                     calories += pl.Amount* (int) pl.Products.calories;
                 }
                 return calories.ToString();
             }
 
             return "0";
         }
 
         public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
         {
             throw new NotImplementedException();
         }
     }
 
 }