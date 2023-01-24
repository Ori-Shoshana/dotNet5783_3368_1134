using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Simulator
{
    private static BackgroundWorker Worker = new BackgroundWorker();

    private const int V = 1;
    private static IBl bl = Factory.Get();

    private static event EventHandler<Tuple<Order, int>>? updateSimulation;


    private static volatile bool isSimulationStoped = false;
    //private static Thread? thread;



    public static void UpdateSimulation(EventHandler<Tuple<Order, int>> action)
    {
        updateSimulation += action;
    }

    public static void StartSimulation()
    {
        Worker.DoWork += (sender, e) => { simulation(); };
        //thread = new Thread(simulation);
        //thread.Start();
        Worker.WorkerSupportsCancellation = true;
        Worker.RunWorkerAsync();

        isSimulationStoped = false;
    }

    public static void StopSimulation()
    {
        //isSimulationStoped = true;
        //thread?.Interrupt();
        isSimulationStoped = true;
        if (Worker.IsBusy)
        {
            Worker.CancelAsync();
        }
    }

    private static void sleep(int seconds)
    {
        try { Thread.Sleep(seconds * 1000); } catch (ThreadInterruptedException) { }
    }

    private static void simulation()
    {
        while (!isSimulationStoped && bl.Order.PriorityOrder() != null)
        {
            var order = bl.Order.OrderDetails(bl.Order.PriorityOrder() ?? throw new NullReferenceException());
            var timeToHandle = new Random().Next(3, 10);
            var aproximateTime = new Random().Next(timeToHandle, timeToHandle);

            updateSimulation?.Invoke(null, new Tuple<Order, int>(order, aproximateTime));

            sleep(timeToHandle);
            if (isSimulationStoped)
                break;

            if (order.Status == BO.Enums.OrderStatus.Confirmed)
                bl.Order.ShippingUpdate(order.ID);
            else if (order.Status == Enums.OrderStatus.Sent)
                bl.Order.UpdateDelivery(order.ID);
            else
            {
                StopSimulation();
              
            }
        }
        StopSimulation();

    }
}