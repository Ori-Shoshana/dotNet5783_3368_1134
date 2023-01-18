
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

        /// <summary>
        /// shows order details
        /// </summary>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int? ID;
                int Temp;
                int.TryParse(TextBox.Text, out Temp);
                ID = Temp;
                try
                {
                    new Order.OrderTracking(ID).Show();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// initialize product item list
        /// </summary>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var ProductItemList = new Cart.ProductItemList();
            ProductItemList.WindowStartupLocation = WindowStartupLocation.Manual;
            ProductItemList.Left = this.Left;
            ProductItemList.Top = this.Top;
            ProductItemList.Show();
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new PL.MySimulatorWindow().Show();
        }
    }
}
