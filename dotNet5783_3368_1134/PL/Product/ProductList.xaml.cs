
using PL.Cart;
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
using System.Windows.Shapes;


namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<BO.ProductForList?> products
        {
            get { return (ObservableCollection<BO.ProductForList?>)GetValue(productsProperty); }
            set { SetValue(productsProperty, value); }
        }
        public static readonly DependencyProperty productsProperty = DependencyProperty.Register(
        "products", typeof(ObservableCollection<BO.ProductForList?>), typeof(ProductList), new PropertyMetadata(default(List<BO.ProductForList?>)));

        /// <summary>
        ///  showing all the products  
        /// </summary>
        public ProductList()
        {
            products = new ObservableCollection<BO.ProductForList?>(bl.Product.GetProductForList());
            InitializeComponent();

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
                ProductListview.ItemsSource = bl?.Product.GetProductForList(a => a?.Category.ToString() == CategorySelector.SelectedItem.ToString());
            }
            else
            {
                ProductListview.ItemsSource = bl?.Product.GetProductForList();
            }
        }

        /// <summary>
        ///  get back to the main window
        /// </summary>
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            new Order.PathToProductAndOrder().Show();
            Close();
        }
   
        /// <summary>
        /// open the window of adding product   
        /// </summary>
        private void AddButton_Click(object sender, RoutedEventArgs e) 
        {
            BO.Product product = new BO.Product();
            new Product.UpdateProduct(product.ID, false, addToProducts).Show();
        }

        /// <summary>
        /// open an option to update a product (after double click on product) 
        /// </summary>
        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList? product1 = new BO.ProductForList();
       
            if ((BO.ProductForList)ProductListview.SelectedItem != null)
            {
                product1 = (BO.ProductForList)ProductListview.SelectedItem;
                new Product.UpdateProduct(product1.ID, false, UpdateToProducts).Show();
            }      
        }
        private void UpdateToProducts(int productID)
        {
            var x = ProductListview.SelectedIndex;
            products[x] = (bl?.Product.GetProductForList(a => a?.ProductID == productID).First());
        }

        private void addToProducts(int productID)
        {
            BO.ProductForList? p = (bl?.Product.GetProductForList(a => a?.ProductID == productID)!).First();
            products.Add(p);
        }
    }
}
