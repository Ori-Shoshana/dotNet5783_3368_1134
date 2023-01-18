using BO;
using DO;
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
        private Action<int> Action;
        BlApi.IBl? bl = BlApi.Factory.Get();
      
        /// <summary>
        /// entering the product update window
        /// </summary>
        public BO.Product? product
        {
            get { return (BO.Product)GetValue(productProperty); }
            set { SetValue(productProperty, value); }
        }
        public static readonly DependencyProperty productProperty = DependencyProperty.Register(
        "product", typeof(BO.Product), typeof(UpdateProduct), new PropertyMetadata(default(BO.Product)));
     
        /// <summary>
        /// initialize the update product window
        /// </summary>
        public UpdateProduct(int product_id, bool check, Action<int> action)
        {
            product = new();
            InitializeComponent();
            this.Action = action;
            if (product_id != 0)
            {
                product = bl?.Product.ProductDetailsM(product_id);
            }
            
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProductCategory));
            if(product_id != 0 && check == false)
            {
                GoBackToProductItem.Visibility = Visibility.Hidden;
                AddBottun.Visibility = Visibility.Hidden;
                GoBackToProductItem.Visibility = Visibility.Hidden;
                TextBoxLable.Content = "Update product:";
            }
            if(product_id == 0 && check == false)
            {
                GoBackToProductItem.Visibility= Visibility.Hidden;  
                UpdateBottun.Visibility = Visibility.Hidden;
                GoBackToProductItem.Visibility = Visibility.Hidden;
                TextBoxLable.Content = "Add product:";
            }
            if(check == true) 
            {
                AddBottun.Visibility = Visibility.Hidden;
                UpdateBottun.Visibility = Visibility.Hidden;
                GoBackToList.Visibility = Visibility.Hidden;
                ID.IsReadOnly = true;
                Name.IsReadOnly = true;
                Price.IsReadOnly = true;
                Category.IsReadOnly = true;
                CategorySelector.Visibility = Visibility.Hidden;
                Amount.IsReadOnly = true;
                TextBoxLable.Content = "See product:";
            }
            
        }
        
        /// <summary>
        /// back button to return to get back to the list
        /// </summary>
        private void GoBackToList_Click(object sender, RoutedEventArgs e)
        {
            //new Product.ProductList().Show();
            Close();
        }
 
        /// <summary>
        /// updating product (by the data that the user entered
        /// </summary>
        private void UpdateBottun_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;        
             try
                {
                    bl?.Product.Update(product);
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
                Close();
            }
            Action?.Invoke(product.ID);
        }
 
        /// <summary>
        /// adding a product to the products list
        /// </summary>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            try
            {
                Random random = new Random();
                product.ID = random.Next(100000, 999999);
                bl?.Product.Add(product);
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
                Close();
            }
            Action?.Invoke(product.ID);
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

        private void GoBackToProductItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
