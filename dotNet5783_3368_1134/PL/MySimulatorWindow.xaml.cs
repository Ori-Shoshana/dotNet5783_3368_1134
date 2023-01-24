using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.VisualBasic;

namespace PL;


public partial class MySimulatorWindow : Window
{

    public static readonly DependencyProperty MyTrackerProperty =
        DependencyProperty.Register("OrderCurrent", typeof(BO.OrderTracking), typeof(MySimulatorWindow));
    public BO.OrderTracking OrderCurrent
    {
        get { return (BO.OrderTracking)GetValue(MyTrackerProperty); }
        set { SetValue(MyTrackerProperty, value); }
    }

    public static readonly DependencyProperty MyTrackerPropertyNext =
     DependencyProperty.Register("OrderCurrentFuture", typeof(BO.Enums.OrderStatus?), typeof(MySimulatorWindow));
    public BO.Enums.OrderStatus? OrderCurrentFuture
    {
        get { return (BO.Enums.OrderStatus)GetValue(MyTrackerPropertyNext); }
        set { SetValue(MyTrackerPropertyNext, value); }
    }

    public static readonly DependencyProperty MyTimeProperty =
        DependencyProperty.Register("Time", typeof(string), typeof(MySimulatorWindow));
    public string Time
    {
        get { return (string)GetValue(MyTimeProperty); }
        set { SetValue(MyTimeProperty, value); }
    }


    public static readonly DependencyProperty MyButtonProperty =
        DependencyProperty.Register("close", typeof(string), typeof(MySimulatorWindow));
    public string close
    {
        get { return (string)GetValue(MyButtonProperty); }
        set { SetValue(MyButtonProperty, value); }
    }

    public static readonly DependencyProperty MyEstimatedTimeProperty =
        DependencyProperty.Register("estimatedTime", typeof(int), typeof(MySimulatorWindow));
    public int estimatedTime
    {
        get { return (int)GetValue(MyEstimatedTimeProperty); }
        set { SetValue(MyEstimatedTimeProperty, value); }
    }

    public static readonly DependencyProperty MymaxBarProperty =
        DependencyProperty.Register("maxBar", typeof(int), typeof(MySimulatorWindow));

    public int maxBar
    {
        get { return (int)GetValue(MymaxBarProperty); }
        set { SetValue(MymaxBarProperty, value); }
    }

    public static readonly DependencyProperty MyBarProperty =
       DependencyProperty.Register("BarProgress", typeof(int), typeof(MySimulatorWindow));

    public int BarProgress
    {
        get { return (int)GetValue(MyBarProperty); }
        set { SetValue(MyBarProperty, value); }
    }

    public int BackTime = 0;

    DispatcherTimer timer = new DispatcherTimer();

    BlApi.IBl? bl = BlApi.Factory.Get();

    public MySimulatorWindow()
    {
        BarProgress = 0;
        Time = "00:00:00";
        close = "Close";
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += Timer_Tick;
        OrderCurrentFuture = null;
        InitializeComponent();
    }

    private void Timer_Tick(object sender, EventArgs e)///////////
    {
        BarProgress++;
        estimatedTime--;
        if (estimatedTime < 0)
        {
            Simulator.StopSimulation();
            timer.Stop();
            estimatedTime = 0;
            MessageBox.Show("The simulation finished!!!");
        }
        else
        {
            Time = TimeSpan.FromSeconds(++BackTime).ToString(@"hh\:mm\:ss");
        }
    }

    private async void Close(object sender, RoutedEventArgs e)///////////
    {
        if (MessageBox.Show("Are you sure?", "Just making sure", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel) == MessageBoxResult.OK)
        {
            Simulator.StopSimulation();
            timer.Stop();
            await Ramaining_Time();
            Close();
        }
    }

    private async Task Ramaining_Time()
    {
        while (estimatedTime != 0)
        {
            BarProgress++;
            estimatedTime--;
            close = "closing in " + estimatedTime;
            await Task.Delay(1000);
        }
    }


    private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {

    }

    private void CurrentOrder(BO.OrderTracking a)
    {
        if (!CheckAccess())
        {
            Action<BO.OrderTracking> d = CurrentOrder;
            Dispatcher.BeginInvoke(d, new BO.OrderTracking()
            {
                ID = a.ID,
                Tracking = a.Tracking,
                Status = a.Status
            });
        }
        else
        {
            OrderCurrent = a;
            if (a.Status == BO.Enums.OrderStatus.Confirmed)
            {
                OrderCurrentFuture = BO.Enums.OrderStatus.Sent;
            }
            else if (a.Status == BO.Enums.OrderStatus.Sent)
            {
                OrderCurrentFuture = BO.Enums.OrderStatus.Deliverd;
            }
            else
            {
                OrderCurrentFuture = null;
            }
        }
    }

    private void EstimatedTime(int a)
    {
        if (!CheckAccess())
        {
            Action<int> d = EstimatedTime;
            Dispatcher.BeginInvoke(d, new object[] { a });
        }
        else
        {
            estimatedTime = a;
            maxBar = a;
            BarProgress = 0;
        }
    }

    private void Start_Button(object sender, RoutedEventArgs e)
    {
        timer.Start();

        Simulator.UpdateSimulation(SimulationData);
        Simulator.StartSimulation();
    }

    private void SimulationData(object sender, Tuple<BO.Order, int> e)
    {
        EstimatedTime(e.Item2);
        CurrentOrder(bl.Order.Track(e.Item1.ID));
    }

    private void MyWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }
}
