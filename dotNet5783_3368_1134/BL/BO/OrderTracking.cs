
using System.Diagnostics;
using System.Xml.Linq;
using static BO.Enums;

namespace BO;

internal class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus status {get; set;}
    public List<Tuple<DateTime, string>>? Tracking { get; set;}

    public override string ToString() => $@"
    ID : {ID}
    status : {status}
    Tracking : {Tracking}
    ";
}
