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
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public OrderTracking()
        {
            InitializeComponent();
            //List<BO.OrderTracking?> ListOrderTracking = new List<BO.OrderTracking?>();
            List<BO.OrderForList?> ListOrder = new List<BO.OrderForList?>();
            ListOrder = (List<BO.OrderForList?>)bl.Order.GetOrders();
           var ListOrderTracking = (List<BO.OrderTracking?>)ListOrder.Select((item) => new BO.OrderTracking
            {
                ID = (int)item?.ID!,
                Status = bl.Order.Track(item.ID).Status,
                Tracking = bl.Order.Track(item.ID).Tracking
            }).ToList()!;
            OrderTackingListView.ItemsSource = ListOrderTracking;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();
        }

        private void OrderTackingListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList? order = new BO.OrderForList();
            BO.OrderTracking? order1 = new BO.OrderTracking();

            order1 = (BO.OrderTracking)OrderTackingListView.SelectedItem;
            if ((BO.OrderTracking)OrderTackingListView.SelectedItem != null)
            {
                new Order.UpdateOrder(order, order1).Show();
                Close();
            }
        }
    }
}
