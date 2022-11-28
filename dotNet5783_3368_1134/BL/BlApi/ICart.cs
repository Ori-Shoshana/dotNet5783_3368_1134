using BO;
using System.ComponentModel;

namespace BlApi;

public interface ICart
{

    public Cart Add(Cart C, int id);
    public Cart Update(Cart C, int ID,int amount);
    public void Confermation(Cart C);
}
