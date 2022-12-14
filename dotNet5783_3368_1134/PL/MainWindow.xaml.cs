using PL.Product;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// button for entering to the options (see the list of products)
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Product.ProductList().Show();
            Close();
        }

        Random rnd = new Random();

        //private void Button_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    //Work out where the button is going to move to.
        //    double newLeft = rnd.Next(Convert.ToInt32(cnv.ActualWidth - btn.ActualWidth));
        //    double newTop = rnd.Next(Convert.ToInt32(cnv.ActualHeight*10 - btn.ActualHeight));

        //    //Create the animations for left and top
        //    DoubleAnimation animLeft = new DoubleAnimation(Canvas.GetLeft(btn), newLeft, new Duration(TimeSpan.FromSeconds(1)));
        //    DoubleAnimation animTop = new DoubleAnimation(Canvas.GetTop(btn), newTop, new Duration(TimeSpan.FromSeconds(1)));

        //    //Set an easing function so the button will quickly move away, then slow down
        //    //as it reaches its destination.
        //    animLeft.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
        //    animTop.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };

        //    //Start the animation.
        //    btn.BeginAnimation(Canvas.LeftProperty, animLeft, HandoffBehavior.SnapshotAndReplace);
        //    btn.BeginAnimation(Canvas.TopProperty, animTop, HandoffBehavior.SnapshotAndReplace);          
        //}

        //private void btn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    new CatchMe.CatchMeIfYouCan().Show();  
        //}

        private void btn_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// (bonous) entering to the Update For Manager window
        /// </summary>
        private void UpdateForManager(object sender, RoutedEventArgs e)
        {
            new UFManager().Show();
            Close();
        }
    }
}
