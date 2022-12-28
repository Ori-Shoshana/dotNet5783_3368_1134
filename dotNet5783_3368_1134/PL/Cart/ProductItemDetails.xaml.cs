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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for ProductItemDetails.xaml
    /// </summary>
    public partial class ProductItemDetails : Window
    {
        public ProductItemDetails(BO.ProductItem? product)
        {
            InitializeComponent();

            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));

            if (product?.InStock == false)
            {
                InStock.Text = "false";
            }
            else 
                InStock.Text = "true";  
            Name.Text = product?.Name;
            Price.Text = product?.Price.ToString();
            CategorySelector.Text = product?.Category.ToString();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Cart.ProductItemList().Show();
            Close();
        }

        private void ID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_ClickForCart(object sender, RoutedEventArgs e)
        {

        }
    }
}
