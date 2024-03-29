﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
namespace Dal;

/// <summary>
/// class DalXml inherits IDal (initialize order / product / orderitem)
/// </summary>
sealed internal class DalXml : IDal
{
    private DalXml() { } // constructor stage 6
    public static IDal Instance { get; } = new DalXml(); // stage 6
    public IOrder Order { get; } = new DalOrder();
    public IProduct Product { get; } = new DalProduct();
    public IOrderItem OrderItem { get; } = new DalOrderItem();
}