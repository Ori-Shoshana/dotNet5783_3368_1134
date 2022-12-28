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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public OrderList()
        {
            InitializeComponent();
            OrderListView.ItemsSource = bl?.Order.GetOrders();

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Order.PathToProductAndOrder().Show();
            Close();
        }

        private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList? order = new BO.OrderForList();
            BO.OrderTracking? order1 = new BO.OrderTracking();


            order = (BO.OrderForList)OrderListView.SelectedItem;
            if ((BO.OrderForList)OrderListView.SelectedItem != null)
            {
                new Order.UpdateOrder(order,order1).Show();
                Close();
            }
        }
    }
}
