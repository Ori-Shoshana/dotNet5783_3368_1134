using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DalProduct : IProduct
{
    string productPath = @"Product";
    static XElement config = XmlTools.LoadConfig();
    static DO.Product? createProductfromXElement(XElement s)
    {
        return new DO.Product
        {
            ProductID = s.ToIntNullable("ProductID") ?? throw new FormatException("ProductID"),
            ProductName = (string?)s.Element("ProductName")!.Value,
            Category = s.ToEnumNullable<DO.Enums.productCategory>("Category"),
            Price = (double)s.Element("Price")!,
            InStock = (int)s.Element("InStock")!
        };
    }
    public int Add(Product product)
    {
        Random rnd = new Random();
        XElement product_root = XmlTools.LoadListFromXMLElement(productPath);
        if (product.ProductID == 0)
        {
            product.ProductID = rnd.Next(100000,999999);
            XmlTools.SaveConfigXElement("ProductID", product.ProductID);
        }
        XElement? prod = (from st in product_root.Elements()
                          where st.ToIntNullable("ProductID") == product.ProductID
                          select st).FirstOrDefault();
        if (prod != null)
            throw new Exception("ID already exist");
        product_root.Add(new XElement("Product",
                                   new XElement("ProductID", product.ProductID),
                                   new XElement("ProductName", product.ProductName),
                                   new XElement("Category", product.Category),
                                   new XElement("Price", product.Price),
                                   new XElement("InStock", product.InStock)
                                   ));
        XmlTools.SaveListToXMLElement(product_root, productPath);
        return product.ProductID; 
    }

    public void Delete(int prodId)
    {
        XElement product_root = XmlTools.LoadListFromXMLElement(productPath);

        XElement? prod = (from st in product_root.Elements()
                          where (int?)st.Element("ProductID") == prodId
                          select st).FirstOrDefault() ?? throw new Exception("Missing ID");
        prod.Remove();  //<==> Remove stud from studentsRootElem

        XmlTools.SaveListToXMLElement(product_root, productPath);

    }

    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func = null)
    {
        XElement? product_root = XmlTools.LoadListFromXMLElement(productPath);
        if (func != null)
        {
            return from s in product_root.Elements()
                   let prod = createProductfromXElement(s)
                   where func(prod)
                   select prod;
        }
        else
        {
            return from s in product_root.Elements()
                   select createProductfromXElement(s);
        }
    }

    public Product GetByDelegate(Func<Product?, bool>? func)
    {
        if (func == null)
            throw new Exception("missing function");

        XElement product_root = XmlTools.LoadListFromXMLElement(productPath);
        return ((from p in product_root.Elements()
                 where func(p.ConvertProduct_Xml_to_D0())
                 select p.ConvertProduct_Xml_to_D0()).FirstOrDefault());
    }

    public Product GetById(int productId)
    {
        List<DO.Product?> ListProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        var product = ListProduct.FirstOrDefault(x => x?.ProductID == productId);

        if (product == null)
            throw new DO.IdNotExistException("Product Id not found");
        else
            return (DO.Product)product;
    }

    public int ListLength()
    {
        List<DO.Product?> ListProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);
        return ListProduct.Count;
    }

    public void Update(Product product)
    {
        List<DO.Product?> ListProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        bool found = false;
        var foundProduct = ListProduct.FirstOrDefault(prod => prod?.ProductID == product.ProductID);
        if (foundProduct != null)
        {
            found = true;
            int index = ListProduct.IndexOf(foundProduct);
            ListProduct[index] = product;
        }
        if (found == false)
            throw new DO.IdNotExistException("order item id not found");
        XmlTools.SaveListToXMLSerializer(ListProduct, productPath);
    }
}
