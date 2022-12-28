using BO;
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

        public UpdateOrder(BO.OrderForList order, BO.OrderTracking order1)
        {
            InitializeComponent();

            BO.Order? MyOrder = new BO.Order();
            if(order.Status != null)
            {
                MyOrder = bl.Order.OrderDetails(order.ID);
                GoBackToListOrderTracking.Visibility = Visibility.Hidden;

            }
            if (order1.Status != null)
            {
                MyOrder = bl.Order.OrderDetails(order1.ID);
                UpdateBottun.Visibility = Visibility.Hidden;
                GoBackToListOrder.Visibility = Visibility.Hidden;
            }

            ID.Text = MyOrder.ID.ToString();
            Name.Text = MyOrder.CustomerName;
            Email.Text = MyOrder.CustomerEmail;
            OrderDate.Text = MyOrder.OrderDate.ToString();
            ShipDate.Text = MyOrder.ShipDate.ToString();
            DelivoryDate.Text = MyOrder.DeliveryDate.ToString();
            Adress.Text = MyOrder.CustomerAdress;
            Status.Text = MyOrder.Status.ToString();
            TotalPrice.Text = MyOrder.TotalPrice.ToString();
        }

       

        private void Status_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UpdateBottun_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            int tempInt1 = 0;
            BO.Order? Order1 = new BO.Order();
            int.TryParse(ID.Text, out tempInt1);
            Order1.ID = tempInt1;

            //Order1.OrderDate = OrderDate.Text;
            //Order1.ShipDate = ShipDate.Text;
            //Order1.DeliveryDate = DelivoryDate.Text;

            try
            {
                bl?.Order.UpdateDelivery(Order1.ID);

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
                UpdateBottun.Visibility = Visibility.Hidden;
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
            new Order.OrderTracking().Show();
            Close();
        }

        private void GoBackToListOrder_Click(object sender, RoutedEventArgs e)
        {
            new Order.OrderList().Show();
            Close();
        }
    }
}
