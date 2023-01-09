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
    /// Interaction logic for CartList.xaml
    /// </summary>
    public partial class CartList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart dataCart = new BO.Cart();
        public CartList(BO.Cart? cart)
        {
            InitializeComponent();
            DataContext = cart;
            dataCart = cart!;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dataCart.CustomerName = Name.Text;
            dataCart.CustomerAdress = Adress.Text;
            dataCart.CustomerEmail = Email.Text;
            try
            {
                bl?.Cart.Confirmation(dataCart);
                Confirmation.Visibility = Visibility.Hidden;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Succeded");
            Close();
        }

       
        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CartListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrderItem? p = (OrderItem?)CartListView.SelectedItem;
            new Cart.newAmount(dataCart, p.ProductID).Show();
            Close();
        }
    }
}
