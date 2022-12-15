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

namespace PL
{
    /// <summary>
    /// Interaction logic for UFManager.xaml
    /// </summary>
    public partial class UFManager : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        /// <summary>
        /// showing and hiding the fields
        /// </summary>
        public UFManager()
        {
            InitializeComponent();
            Order_Id.Visibility = Visibility.Hidden;
            Product_Id.Visibility = Visibility.Hidden;
            Amount.Visibility = Visibility.Hidden;

            LaberOrderId.Visibility = Visibility.Hidden;
            LabelProductId.Visibility = Visibility.Hidden;
            LabelAmount.Visibility = Visibility.Hidden;

            UpdateTheOrder.Visibility = Visibility.Hidden;
        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        /// <summary>
        /// checking the password
        /// </summary>
        private void Check_Password_Click(object sender, RoutedEventArgs e)
        {
            int temp;
            int.TryParse(Password.Text, out temp);
            if(temp == 76567)
            {
                Check_Password.Visibility = Visibility.Hidden;
                Password.Visibility = Visibility.Hidden;
                LabelPassword.Visibility = Visibility.Hidden;
                Order_Id.Visibility = Visibility.Visible;
                Product_Id.Visibility = Visibility.Visible;
                Amount.Visibility = Visibility.Visible;
               
                LaberOrderId.Visibility = Visibility.Visible;
                LabelProductId.Visibility = Visibility.Visible;
                LabelAmount.Visibility = Visibility.Visible;
                
                UpdateTheOrder.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("The password is incorrect");
            }
        }

        private void Order_Id_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        /// <summary>
        /// updating the order (by the data that the manager entered
        /// </summary>
        private void UpdateTheOrder_Click(object sender, RoutedEventArgs e)
        {
            int temp1, temp2, temp3;
            int.TryParse(Order_Id.Text, out temp1);
            int.TryParse(Product_Id.Text , out temp2);
            int.TryParse(Amount.Text, out temp3);
            try
            {
                bl?.Order.updateOrederM(temp1, temp2, temp3);
            }
            catch (BO.VeriableNotExistException ex)
            {
                MessageBox.Show("❌  " + ex.Message);
            }
            catch (BO.VariableIsSmallerThanZeroExeption ex)
            {
                MessageBox.Show("❌  " + ex.Message);
            }
            catch (DO.IdNotExistException ex)
            {
                MessageBox.Show("❌  " + ex.Message);
            }
        }
        /// <summary>
        /// get back to the products list 
        /// </summary>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();
        }
    }
}
