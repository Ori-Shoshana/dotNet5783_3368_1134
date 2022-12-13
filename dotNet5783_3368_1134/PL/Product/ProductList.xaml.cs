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

            for (int i = 0; i < 4; i++)
            {
                CategorySelector.Items.Add($"{(BO.Enums.ProductCategory)i}");
            }
            CategorySelector.Items.Add("All");
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IBl bl = new BlImplementation.BL();
            if (CategorySelector.SelectedItem.ToString() != "All")
            {
                ProductListview.ItemsSource = bl.Product.GetProducts(a => a?.Category.ToString() == CategorySelector.SelectedItem.ToString());
            }
            else
            {
                ProductListview.ItemsSource = bl.Product.GetProducts();
            }
        }
     

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            int? X  =  null;
            new Product.UpdateProduct(X).Show();
            Close();
        }

        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var product = (BO.ProductForList)ProductListview.SelectedItem;
            new Product.UpdateProduct(product.ID).Show();
            Close();
        }
    }
}
