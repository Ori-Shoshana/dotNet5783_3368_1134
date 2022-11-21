
using static BO.Enums;

namespace BO;

public class OrderForList
{
    int ID { get; set; }
    string CustomerName { get; set; }
    OrderStatus Status {get; set;}
    int AmountOfItems { get; set; } 
    double TotalPrice { get; set; }
    public override string ToString() => $@"
    ID : {ID}
    Customer Name : {CustomerName}
    Amount Of Items : {AmountOfItems}
    TotalPrice : {TotalPrice}
    Total Price : {TotalPrice}
    Status : {Status}
        ";

}
