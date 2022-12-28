using PL.Product;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// button for entering to the options (see the list of products)
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Order.PathToProductAndOrder().Show();
            Close();
        }

        Random rnd = new Random();

       

    

        private void btn_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// (bonous) entering to the Update For Manager window
        /// </summary>
        private void UpdateForManager(object sender, RoutedEventArgs e)
        {
            new UFManager().Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Order.OrderTracking().Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new Cart.ProductItemList().Show();
            Close();
        }
    }
}
