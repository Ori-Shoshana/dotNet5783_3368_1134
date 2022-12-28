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

        public ProductItemList()
        {
            InitializeComponent();

            ProductItemListView.ItemsSource = bl?.Product.GetProductItem();

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
    }
}
