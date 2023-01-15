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
        int? ID;
   
        /// <summary>
        /// prints the order (finds her by id)
        /// </summary>
        public OrderTracking(int? id)
        {
            InitializeComponent();
            OrderTackingText.Text = (bl?.Order.Track((int)id!))?.ToString();
            ID = id;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Back to main window
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BO.OrderForList? order = new BO.OrderForList();
            BO.OrderTracking? order1 = new BO.OrderTracking();

            order1 = bl?.Order.Track((int)ID!);
            Action<int>? action = null;

            if(order1 != null)
            {
                new Order.UpdateOrder(order, order1, action).Show();
                Close();
            }
        }
        
    }
}
