using BO;
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
        BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Order? orderBinding
        {
            get { return (BO.Order)GetValue(orderProperty); }
            set { SetValue(orderProperty, value); }
        }
        public static readonly DependencyProperty orderProperty = DependencyProperty.Register(
        "orderBinding", typeof(BO.Order), typeof(UpdateOrder), new PropertyMetadata(default(BO.Order)));

        int ID1;

        public UpdateOrder(BO.OrderForList order, BO.OrderTracking order1)
        {
            orderBinding = new();
            InitializeComponent();


            BO.Order? MyOrder = new BO.Order();
            if(order.Status != null)
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
                GoBackToListOrder.Visibility = Visibility.Hidden;
                orderBinding = MyOrder;
            }
            ListUpdateOrder.ItemsSource = MyOrder.Items;

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

        private void UpdateBottun_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            int tempInt1 = 0;
            int.TryParse(ID.Text, out tempInt1);

            try
            {
                bl?.Order.UpdateDelivery(tempInt1);

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
                new Order.OrderList().Show();
                Close();
            }

        }

        private void UpdateBottunShiping_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            int tempInt1 = 0;
            int.TryParse(ID.Text, out tempInt1);

            try
            {
                bl?.Order.ShippingUpdate(tempInt1);

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
                new Order.OrderList().Show();
                Close();
            }
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

        private void GoBackToListOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            int? id = ID1;
            new Order.OrderTracking(id).Show();
            Close();
        }

        private void GoBackToListOrder_Click(object sender, RoutedEventArgs e)
        {
            new Order.OrderList().Show();
            Close();
        }

        private void ListUpdateOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

      
    }
}
