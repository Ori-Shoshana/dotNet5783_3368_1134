using BlApi;
using BlImplementation;
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
    /// Interaction logic for UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Window
    {
        IBl bl = new BlImplementation.BL();

        public UpdateProduct()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
        }

        private void GoBackToList_Click(object sender, RoutedEventArgs e)
        {
            new Product.ProductList().Show();
            Close();
        }

        private void AddBottun_Click(object sender, RoutedEventArgs e)
        {
            int tempInt1 = 0;
            BO.Product product1 = new BO.Product();
            int.TryParse(ID.Text, out tempInt1);
            product1.ID = tempInt1;
            product1.Name = Name.Text;
            int.TryParse(Price.Text, out tempInt1);
            product1.Price = tempInt1;
            product1.Category = (BO.Enums.ProductCategory?)CategorySelector.SelectedItem;
            int.TryParse(InStock.Text, out tempInt1);
            product1.InStock = tempInt1;
            bl.Product.Update(product1);
            AddBottun.Visibility = Visibility.Hidden;
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
