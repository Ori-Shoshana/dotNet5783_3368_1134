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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for PathToProductAndOrder.xaml
    /// </summary>
    public partial class PathToProductAndOrder : Window
    {
        public PathToProductAndOrder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// shows product list
        /// </summary>
        private void Button_Click_Product(object sender, RoutedEventArgs e)
        {
            new Product.ProductList().Show();
            Close();
        }

        /// <summary>
        /// shows order list
        /// </summary>
        private void Button_Click_Order(object sender, RoutedEventArgs e)
        {
            new Order.OrderList().Show();
            Close();
        }

        /// <summary>
        /// back to main window
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();
        }
    }
}
