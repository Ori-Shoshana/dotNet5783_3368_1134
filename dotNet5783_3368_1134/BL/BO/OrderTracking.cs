
using System.Diagnostics;
using System.Xml.Linq;
using static BO.Enums;

namespace BO;

internal class OrderTracking
{
    int ID { get; set; }
    OrderStatus status {get; set;}
    public List<Tuple<DateTime, string>>? Tracking { get; set;}

    public override string ToString() => $@"
    ID : {ID}
    status : {status}
    Tracking : {Tracking}
    ";
}
