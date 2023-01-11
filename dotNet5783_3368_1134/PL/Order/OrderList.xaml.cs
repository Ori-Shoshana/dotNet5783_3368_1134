using BO;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<BO.OrderForList?> orderForList
        {
            get { return (ObservableCollection<BO.OrderForList?>)GetValue(OrderListProperty); }
            set { SetValue(OrderListProperty, value); }
        }
        public static readonly DependencyProperty OrderListProperty = DependencyProperty.Register(
        "orderForList", typeof(ObservableCollection<BO.OrderForList?>), typeof(OrderList), new PropertyMetadata(default(ObservableCollection<BO.OrderForList?>)));
        public OrderList()
        {
            orderForList = new ObservableCollection<BO.OrderForList?>(bl?.Order.GetOrders()!);
            InitializeComponent();
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
            BO.OrderTracking? order1 = new BO.OrderTracking();

            var order = (BO.OrderForList)OrderListView.SelectedItem;
            if ((BO.OrderForList)OrderListView.SelectedItem != null)
            {
                new Order.UpdateOrder(order,order1, UpdateToOrders).Show();
            }
        }

        private void UpdateToOrders(int orderID)
        {
            var x = OrderListView.SelectedIndex;
            orderForList[x] = (((bl?.Order.GetOrders(a => a?.OrderID == orderID).First())));
        }
    }
}
