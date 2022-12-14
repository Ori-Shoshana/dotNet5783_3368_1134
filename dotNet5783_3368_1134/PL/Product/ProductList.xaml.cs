using BlApi;
using BO;
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
        IBl bl = new BlImplementation.BL();

        public ProductList()
        {
            InitializeComponent();
            IBl bl1 = new BlImplementation.BL();

            ProductListview.ItemsSource = bl1.Product.GetProducts();

            for (int i = 0; i < 5; i++)
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
            BO.ProductForList product = new BO.ProductForList();
            product.ID = 0;
            product.Name = "";           
            product.Category = null;
            product.Price = 0;
            new Product.UpdateProduct(product).Show();
            Close();
        }

        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList product = new BO.ProductForList();
        
            product = (BO.ProductForList)ProductListview.SelectedItem;
            new Product.UpdateProduct(product).Show();
            Close();
          
        }

    }
}
