using PL.Order;
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
using static System.Collections.Specialized.BitVector32;

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for newAmount.xaml
    /// </summary>
    public partial class newAmount : Window
    {
        private Action<BO.Cart>? Action;

        BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.OrderItem? ID_Binding
        {
            get { return (BO.OrderItem?)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }
        public static readonly DependencyProperty IDProperty = DependencyProperty.Register(
        "ID_Binding", typeof(BO.OrderItem), typeof(newAmount), new PropertyMetadata(default(BO.OrderItem?)));

        
        BO.Cart cart = new BO.Cart();
        public newAmount(BO.Cart c, BO.OrderItem? item, Action<BO.Cart>? action )
        {
            ID_Binding = new();
            InitializeComponent();
            this.Action = action!;
            ID_Binding = item;
            cart = c;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    int temp;
                    int.TryParse(TextBox.Text, out temp);
                    cart = bl?.Cart.Update(cart, ID_Binding.ProductID, temp)!;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Action?.Invoke(cart);
            }
        }

    }
}
