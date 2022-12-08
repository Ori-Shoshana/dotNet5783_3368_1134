using BlApi;
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


namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        public ProductList()
        {
            InitializeComponent();
            IBl bl = new BlImplementation.BL();
            ProductListview.ItemsSource = bl.Product.GetProducts();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IBl bl = new BlImplementation.BL();
            ProductListview.ItemsSource = bl.Product.GetProducts(a => a?.Category.ToString() == CategorySelector.SelectedItem.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e) => new Product.AddNewProduct().Show();

    }
}
