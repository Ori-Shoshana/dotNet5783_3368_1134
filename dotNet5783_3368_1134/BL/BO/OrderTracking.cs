
using System.Diagnostics;
using System.Xml.Linq;
using static BO.Enums;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus? Status {get; set;}
    public List<(DateTime, string)>? Tracking { get; set;}

    public override string ToString() => $@"
    ID : {ID}
    status : {Status}
    Tracking : {Tracking}
    ";
}
