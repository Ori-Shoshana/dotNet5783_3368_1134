
using System.Diagnostics;
using System.Xml.Linq;
using static BO.Enums;

namespace BO;
/// <summary>
/// properties of order tracking
/// </summary>
public class OrderTracking
{
    /// <summary>
    /// order id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// order status
    /// </summary>
    public OrderStatus? Status { get; set; }
    /// <summary>
    /// list that contains date and order status description
    /// </summary>
    public List<(DateTime?, string?)>? Tracking { get; set; }
    /// <summary>
    /// prints all the data about order tracking
    /// </summary>
    public override string ToString()
    {
        string st = "ID: " + ID + "\nStatus:" + Status + "\nTracking: ";
        int i = 1;
        if (Tracking != null)
        { 
            foreach (var track in Tracking)
                {
                    st += i + ": " + track.Item1;
                    st += " " + track.Item2;
                    i++;
                }
        }
        return st;
    }
}
