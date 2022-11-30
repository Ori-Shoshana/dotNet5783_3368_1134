using BO;
using System.ComponentModel;

namespace BlApi;
/// <summary>
/// cart interface with statement of the functions Add, Update, Confirmation 
/// </summary>
public interface ICart
{
    /// <summary>
    /// decleration of function Add
    /// Adds a product to the cart and updates the quntity and the prices
    /// </summary>
    /// the function receives cart and id of a product
    /// returns cart
    public Cart Add(Cart C, int id);
   
    /// <summary>
    /// decleration of function update
    /// the function updates the product amount 
    /// and the total price of the order item and the total price of the cart
    /// </summary>
    /// the function receives cart and id of a product and the product amount
    /// returns cart
    public Cart Update(Cart C, int ID,int amount);
    
    /// <summary>
    /// decleration of function confirmation
    /// the function receives cart
    /// Adds an order items updates the quntity and the prices
    /// checks the integrity of the data
    /// place an order and update the inventory
    /// </summary>

    public void Confirmation(Cart C);

}
