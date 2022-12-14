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
        BlApi.IBl? bl = BlApi.Factory.Get();
        /// <summary>
        ///  showing all the products  
        /// </summary>
        public ProductList()
        {
            InitializeComponent();
            BlApi.IBl? bl1 = BlApi.Factory.Get();

            ProductListview.ItemsSource = bl1?.Product.GetProducts();

            for (int i = 0; i < 5; i++)
            {
                CategorySelector.Items.Add($"{(BO.Enums.ProductCategory)i}");
            }
            CategorySelector.Items.Add("All");
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        /// <summary>
        ///  showing the products of a specific category 
        /// </summary>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BlApi.IBl? bl = BlApi.Factory.Get();
            if (CategorySelector.SelectedItem.ToString() != "All")
            {
                ProductListview.ItemsSource = bl?.Product.GetProducts(a => a?.Category.ToString() == CategorySelector.SelectedItem.ToString());
            }
            else
            {
                ProductListview.ItemsSource = bl?.Product.GetProducts();
            }
        }

        /// <summary>
        ///  get back to the main window
        /// </summary>
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();
        }
        /// <summary>
        /// open the window of adding product   
        /// </summary>
        private void AddButton_Click(object sender, RoutedEventArgs e) 
        {
            BO.ProductForList product = new BO.ProductForList();
            new Product.UpdateProduct(product).Show();
            Close();
        }
        /// <summary>
        /// open an option to update a product (after double click on product) 
        /// </summary>
        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList product = new BO.ProductForList();
        
            product = (BO.ProductForList)ProductListview.SelectedItem;
            if ((BO.ProductForList)ProductListview.SelectedItem != null)
            {
                new Product.UpdateProduct(product).Show();
                Close();
            }      
        }

    }
}
