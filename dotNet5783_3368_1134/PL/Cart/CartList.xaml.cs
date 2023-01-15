using PL.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class CartList : Window , INotifyPropertyChanged
    {
        BlApi.IBl? bl = BlApi.Factory.Get();


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<BO.OrderItem> _cartItems;
        public ObservableCollection<BO.OrderItem> cartItems
        {
            get { return _cartItems; }
            set 
            { 
                _cartItems = value;
                OnPropertyChanged(nameof(cartItems));
            }
        }

        private BO.Cart _cart;
        public BO.Cart cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                OnPropertyChanged(nameof(cart));
            }
        }
   
 

        BO.Cart dataCart = new BO.Cart();
        public CartList(BO.Cart? cart1 , Action<int> action)
        {
            if (cart1?.Items != null)
            {
                cartItems = new ObservableCollection<BO.OrderItem>(cart1.Items!);
            }
            cart = new();
            InitializeComponent();
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
        private void AddAmount_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem? item = (sender as Button)?.DataContext as BO.OrderItem;
            try
            {
                int temp = item.Amount + 1;
                cart = bl?.Cart.Update(dataCart, item.ProductID, temp)!;
                cartItems = new ObservableCollection<BO.OrderItem>(cart.Items!);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem? item = (sender as Button)?.DataContext as BO.OrderItem;
            try
            {
                int temp = item.Amount - 1;
                cart = bl?.Cart.Update(dataCart, item.ProductID, temp)!;
                cartItems = new ObservableCollection<BO.OrderItem>(cart.Items!);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
