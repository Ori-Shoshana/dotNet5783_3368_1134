
using BO;
using DO;
using PL.Product;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
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
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        private Action<int> Action;
        BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Order? orderBinding
        {
            get { return (BO.Order)GetValue(orderProperty); }
            set { SetValue(orderProperty, value); }
        }
        public static readonly DependencyProperty orderProperty = DependencyProperty.Register(
        "orderBinding", typeof(BO.Order), typeof(UpdateOrder), new PropertyMetadata(default(BO.Order)));

        public List<BO.OrderItem?> OrderItems
        {
            get { return (List<BO.OrderItem?>)GetValue(itemsProperty); }
            set { SetValue(itemsProperty, value); }
        }
        public static readonly DependencyProperty itemsProperty = DependencyProperty.Register(
        "OrderItems", typeof(List<BO.OrderItem?>), typeof(UpdateOrder), new PropertyMetadata(default(List<BO.OrderItem?>)));

        int ID1;

        public UpdateOrder(BO.OrderForList order, BO.OrderTracking order1, Action<int> action)
        {
            orderBinding = new();
            OrderItems = new();
            InitializeComponent();
            this.Action = action;

            BO.Order? MyOrder = new BO.Order();
            if (order.Status != null)
            {
                MyOrder = bl.Order.OrderDetails(order.ID);
                GoBackToListOrderTracking.Visibility = Visibility.Hidden;
                orderBinding = MyOrder;

            }
            if (order1.Status != null)
            {
                MyOrder = bl.Order.OrderDetails(order1.ID);
                ID1 = order1.ID;
                UpdateBottunDelivory.Visibility = Visibility.Hidden;
                UpdateBottunShiping.Visibility = Visibility.Hidden;
                GoBackToListOrder.Visibility = Visibility.Hidden;
                orderBinding = MyOrder;
            }
            OrderItems = MyOrder.Items!;

            if (MyOrder.Status == BO.Enums.OrderStatus.Deliverd)
            {
                UpdateBottunDelivory.Visibility = Visibility.Hidden;
                UpdateBottunShiping.Visibility = Visibility.Hidden;
            }
            if (MyOrder.Status == BO.Enums.OrderStatus.Sent)
            {
                UpdateBottunShiping.Visibility = Visibility.Hidden;
            }
            if (MyOrder.Status == BO.Enums.OrderStatus.Confirmed)
            {
                UpdateBottunDelivory.Visibility = Visibility.Hidden;
            }

        }

        private void Status_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// update delivery for an order
        /// </summary>
        private void UpdateBottun_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            try
            {
                bl?.Order.UpdateDelivery(orderBinding.ID);

                check = true;
            }
            catch (VeriableNotExistException ex)
            {

                MessageBox.Show("❌  " + ex.Message);
            }
            catch (DO.IdNotExistException ex)
            {
                MessageBox.Show("❌  " + ex.Message);
            }
            if (check == true)
            {
                UpdateBottunDelivory.Visibility = Visibility.Hidden;
                Close();
            }
            Action?.Invoke(orderBinding.ID);

        }

        /// <summary>
        /// update shipping for an order
        /// </summary>
        private void UpdateBottunShiping_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;

            try
            {
                bl?.Order.ShippingUpdate(orderBinding.ID);

                check = true;
            }
            catch (VeriableNotExistException ex)
            {

                MessageBox.Show("❌  " + ex.Message);
            }
            catch (DO.IdNotExistException ex)
            {
                MessageBox.Show("❌  " + ex.Message);
            }
            if (check == true)
            {
                UpdateBottunDelivory.Visibility = Visibility.Hidden;
                Close();
            }
            Action?.Invoke(orderBinding.ID);
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Adress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// back to the order details
        /// </summary>
        private void GoBackToListOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            int? id = ID1;
            new Order.OrderTracking(id).Show();
            Close();
        }

        private void GoBackToListOrder_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void ListUpdateOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
