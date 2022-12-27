﻿
using DalApi;

namespace Dal;

internal sealed class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    public IOrder Order => new DalOrder();

    public IOrderItem OrderItem { get; }

    public IProduct Product { get; }

    private DalList() { }
}
