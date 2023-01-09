using PL.Order;
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
        public List<BO.OrderItem?> cartItems
        {
            get { return (List<BO.OrderItem?>) GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }
        public static readonly DependencyProperty cartProperty = DependencyProperty.Register(
        "cartItems", typeof(List<BO.OrderItem?>), typeof(CartList), new PropertyMetadata(default(List<BO.OrderItem?>)));

        public BO.Cart? cart
        {
            get { return (BO.Cart?)GetValue(cartPropertyTotal); }
            set { SetValue(cartPropertyTotal, value); }
        }
        public static readonly DependencyProperty cartPropertyTotal = DependencyProperty.Register(
        "cart", typeof(BO.Cart), typeof(CartList), new PropertyMetadata(default(BO.Cart?)));

        BO.Cart dataCart = new BO.Cart();
        public CartList(BO.Cart? cart1)
        {
            cartItems = new();
            cart = new();
            InitializeComponent();
            cartItems = cart1.Items!;
            cart = cart1;
            dataCart = cart1;
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

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            BO.OrderItem? item =  (sender as Button)?.DataContext as BO.OrderItem;
            int c = item.Amount;
            item.Amount = 0;
            try
            {
                dataCart = bl?.Cart.Update(dataCart, item.ProductID,c)!;
                item.Amount = c;
                if (item?.Amount == 0)
                {
                    dataCart?.Items?.Remove(item);
                    MessageBox.Show("deleted !");
                }
                else
                    MessageBox.Show("updated !");
            }
            catch (Exception ex)
            {
                item.Amount = c;
                MessageBox.Show(ex.Message);
            }
            Close();
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
