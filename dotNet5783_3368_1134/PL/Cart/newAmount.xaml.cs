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
namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for newAmount.xaml
    /// </summary>
    public partial class newAmount : Window
    {
            BlApi.IBl? bl = BlApi.Factory.Get();

            int ID;
            BO.Cart cart = new BO.Cart();
            public newAmount(BO.Cart c, int id)
            {
                InitializeComponent();
                ID = id;
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
                        cart = bl?.Cart.Update(cart, ID, temp)!;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        Close();
                    }
                    MessageBox.Show("update");
                Close();
                }

            }

        }
    }
