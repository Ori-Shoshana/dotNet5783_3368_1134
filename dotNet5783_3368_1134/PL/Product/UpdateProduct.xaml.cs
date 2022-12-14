using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Window
    {
        IBl bl = new BlImplementation.BL();
        
        public UpdateProduct(BO.ProductForList product)
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
            if(product.ID != 0)
            {
                AddBottun.Visibility = Visibility.Hidden;
                ID.Text = product.ID.ToString();
                Name.Text = product.Name;
                Price.Text = product.Price.ToString();
                CategorySelector.Text = product.Category.ToString();
                TextBoxLable.Content = "Update product:";
            }
            else
            {
                UpdateBottun.Visibility = Visibility.Hidden;
                TextBoxLable.Content = "Add product:";
            }
        }

        private void GoBackToList_Click(object sender, RoutedEventArgs e)
        {
            new Product.ProductList().Show();
            Close();
        }

        private void UpdateBottun_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            int tempInt1 = 0;
            BO.Product product1 = new BO.Product();
            int.TryParse(ID.Text, out tempInt1);
            product1.ID = tempInt1;
            product1.Name = Name.Text;
            int.TryParse(Price.Text, out tempInt1);
            product1.Price = tempInt1;
            product1.Category = (BO.Enums.ProductCategory?)CategorySelector.SelectedItem;
            int.TryParse(InStock.Text, out tempInt1);
            product1.InStock = tempInt1;
             try
                {
                    bl.Product.Update(product1);
                    check = true;
                }
             catch (VariableIsSmallerThanZeroExeption ex)
                {
                    MessageBox.Show("❌  " + ex.Message);
                }
            catch (VariableIsNullExeption ex)
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
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            int tempInt1 = 0;
            BO.Product product1 = new BO.Product();
            int.TryParse(ID.Text, out tempInt1);
            product1.ID = tempInt1;
            product1.Name = Name.Text;
            int.TryParse(Price.Text, out tempInt1);
            product1.Price = tempInt1;
            product1.Category = (BO.Enums.ProductCategory?)CategorySelector.SelectedItem;
            int.TryParse(InStock.Text, out tempInt1);
            product1.InStock = tempInt1;
            try
            {
                bl.Product.Add(product1);
                check = true;
            }
            catch (VariableIsSmallerThanZeroExeption ex)
            {
                MessageBox.Show("❌  "+ ex.Message);
            }
            catch (VariableIsNullExeption ex)
            {
                MessageBox.Show("❌  " + ex.Message);
            }
            catch (DO.IdAlreadyExistException ex)
            {
                MessageBox.Show("❌  " + ex.Message);
            }
            if (check == true)
            {
                AddBottun.Visibility = Visibility.Hidden;
            }
        }
      
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
