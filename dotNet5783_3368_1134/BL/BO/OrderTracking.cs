
using System.Diagnostics;
using System.Xml.Linq;
using static BO.Enums;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus? Status { get; set; }
    public List<(DateTime, string)> Tracking { get; set; }

    public override string ToString()
    {
        string st = "ID: " + ID + "\nStatus" + Status + "\nTracking:\n";
        int i = 1;
        if (Tracking != null)
        { 
            foreach (var track in Tracking)
                {
                    st += i + ":\n" + track.Item1;
                    st += "\n" + track.Item2;
                    i++;
                }
        }
       
        return st;
    }
}
