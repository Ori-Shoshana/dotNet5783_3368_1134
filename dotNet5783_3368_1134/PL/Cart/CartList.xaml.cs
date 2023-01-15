using PL.Order;
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
    /// Interaction logic for CartList.xaml
    /// </summary>
    public partial class CartList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private Action<int> Action;

        public List<BO.OrderItem?>? cartItems
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
        public CartList(BO.Cart? cart1 , Action<int> action)
        {
            if (cart1?.Items != null)
            {
                cartItems = new List<BO.OrderItem?>(cart1?.Items);
            }
            cart = new();
            InitializeComponent();
            this.Action = action;
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

  
        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BO.OrderItem? item = (sender as Button)?.DataContext as BO.OrderItem;
            new Cart.newAmount(dataCart, item, UpdateToOrders).Show();
        }

        private void UpdateToOrders(BO.Cart? ProductID1)
        {
                MessageBox.Show(ex.Message);
        }

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
