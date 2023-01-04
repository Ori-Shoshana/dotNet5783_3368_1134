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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for ProductItemList.xaml
    /// </summary>

    public partial class ProductItemList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart = new BO.Cart();

        public List<BO.ProductItem?> productItem = new List<ProductItem?>();

        public ProductItemList()
        {
            InitializeComponent();

            productItem = bl?.Product.GetProductItem().ToList()!;

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
            BlApi.IBl? bl = BlApi.Factory.Get();
            if (CategorySelector.SelectedItem.ToString() != "All")
            {
                ProductItemListView.ItemsSource = bl?.Product.GetProductItem(a => a?.Category.ToString() == CategorySelector.SelectedItem.ToString());
            }
            else
            {
                ProductItemListView.ItemsSource = bl?.Product.GetProductItem();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Cart.CartList(cart).Show();
        }

        private void ProductItemListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            BO.Product? product = new BO.Product();
            BO.ProductItem? productItem = new BO.ProductItem();

            productItem = (BO.ProductItem)ProductItemListView.SelectedItem;
            product = (bl?.Product.ProductDetailsM(productItem.ID));

            if (ProductItemListView.SelectedItem != null)
            {
                new Product.UpdateProduct(product,true).Show();
                Close();
            }


        }

        private void GridViewColumn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //BO.ProductItem product = new BO.ProductItem();
            //product = (ProductItem?)ProductItemListView.SelectedItem!;
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

    }
}
