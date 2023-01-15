using BO;
using PL.Product;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for ProductItemList.xaml
    /// </summary>
    public partial class ProductItemList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart = new BO.Cart();
        public ObservableCollection<BO.ProductItem?> productItem
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(productsProperty); }
            set { SetValue(productsProperty, value); }
        }
        public static readonly DependencyProperty productsProperty = DependencyProperty.Register(
        "productItem", typeof(ObservableCollection<BO.ProductItem?>), typeof(ProductItemList), new PropertyMetadata(default(List<BO.ProductItem?>)));

        /// <summary>
        /// initialize the product list 
        /// </summary>
        public ProductItemList()
        {
            productItem = new ObservableCollection<BO.ProductItem>(bl?.Product.GetProductItem()!)!;
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
        /// shows the list of products (You can filter by category)
        /// </summary>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategorySelector.SelectedItem.ToString() != "All")
            {
                ProductItemListView.ItemsSource = bl?.Product.GetProductItem(a => a?.Category.ToString() == CategorySelector.SelectedItem.ToString());
            }
            else
            {
                ProductItemListView.ItemsSource = bl?.Product.GetProductItem();
            }
        }

        /// <summary>
        /// opens the main window
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Action<int>? action = null;
            new Cart.CartList(cart, action).Show();
        }

        /// <summary>
        /// shows product details after double click on the product
        /// </summary>
        private void ProductItemListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            BO.Product? product = new BO.Product();
            BO.ProductItem? productItem = new BO.ProductItem();

            productItem = (BO.ProductItem)ProductItemListView.SelectedItem;
            product = (bl?.Product.ProductDetailsM(productItem.ID));
            Action<int>? action = null;
            if (ProductItemListView.SelectedItem != null)
            {
                new Product.UpdateProduct(productItem.ID, true, action).Show();
            }
        }

        private void GridViewColumn_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// adding product to the shopping cart
        /// </summary>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BO.ProductItem? product = new BO.ProductItem();
            product = (sender as Button)?.DataContext as BO.ProductItem;
            try
            { 
                cart = bl?.Cart.Add(cart, (int)product?.ID!)!;
                MessageBox.Show("Added !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void UpdateToProducts(int productID)
        {
            var x = ProductItemListView.SelectedIndex;
            productItem[x] = ((bl?.Product.GetProductItem(a => a?.ProductID == productID).First()));
        }

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
