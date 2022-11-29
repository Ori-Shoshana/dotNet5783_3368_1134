﻿
using static BO.Enums;

namespace BO;

public class OrderForList
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public OrderStatus Status {get; set;}
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
    ID : {ID}
    Customer Name : {CustomerName}
    Amount Of Items : {AmountOfItems}
    Total Price : {TotalPrice}
    Status : {Status}
        ";

}
