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

        /// <summary>
        /// returns list of order items
        /// </summary>
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

        /// <summary>
        /// initialize the items in cart list
        /// </summary>
        public CartList(BO.Cart? cart1 , Action<int> action)
        {
            if (cart1?.Items != null)
            {
                cartItems = new List<BO.OrderItem?>(cart1.Items);
            }
            cart = new();
            InitializeComponent();
            this.Action = action;
            cart = cart1;
            if(cart1 != null)
                dataCart = cart1;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        ///  button to confirm the shopping cart
        /// </summary>
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
       
        /// <summary>
        /// update new amount to the product in the cart
        /// </summary>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BO.OrderItem? item = (sender as Button)?.DataContext as BO.OrderItem;
            new Cart.newAmount(dataCart, item, UpdateToOrders).Show();
        }

        /// <summary>
        /// update the cart list
        /// </summary>
        private void UpdateToOrders(BO.Cart? ProductID1)
        {
                MessageBox.Show(ex.Message);
            //if(ProductID1 != null)
            //    cartItems = ProductID1.Items;
        }
        private void Decrease_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
