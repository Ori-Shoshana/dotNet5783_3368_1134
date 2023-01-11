﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;
using DO;
static class XmlTools
{
    static string? s_dir = Directory.GetParent(System.IO.Directory.GetCurrentDirectory())?.FullName + @"\xml\";
    static XmlTools()
    {
        if (!Directory.Exists(s_dir))
            Directory.CreateDirectory(s_dir);
    }

    #region Extension Fuctions
    public static T? ToEnumNullable<T>(this XElement element, string name) where T : struct, Enum =>
        Enum.TryParse<T>((string?)element.Element(name), out var result) ? (T?)result : null;

    public static DateTime? ToDateTimeNullable(this XElement element, string name) =>
        DateTime.TryParse((string?)element.Element(name), out var result) ? (DateTime?)result : null;

    public static double? ToDoubleNullable(this XElement element, string name) =>
        double.TryParse((string?)element.Element(name), out var result) ? (double?)result : null;

    public static int? ToIntNullable(this XElement element, string name) =>
        int.TryParse((string?)element.Element(name), out var result) ? (int?)result : null;
    #endregion

    #region SaveLoadWithXElement
    public static void SaveListToXMLElement(XElement rootElem, string entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            rootElem.Save(filePath);
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            throw new Exception($"fail to create xml file: {filePath}", ex);
        }
    }

    public static XElement LoadListFromXMLElement(string entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);
            XElement rootElem = new(entity);
            rootElem.Save(filePath);
            return rootElem;
        }
        catch (Exception ex)
        {
            //new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            throw new Exception($"fail to load xml file: {filePath}", ex);
        }
    }
    #endregion

    #region SaveLoadWithXMLSerializer
    //static readonly bool s_writing = false;
    public static void SaveListToXMLSerializer<T>(List<T?> list, string entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            //using XmlWriter writer = XmlWriter.Create(file, new XmlWriterSettings() { Indent = true });

            XmlSerializer serializer = new(typeof(List<T?>));
            //if (s_writing)
            //    serializer.Serialize(writer, list);
            //else
            serializer.Serialize(file, list);
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex); 
            throw new Exception($"fail to create xml file: {filePath}", ex);
        }
    }

    public static List<T?> LoadListFromXMLSerializer<T>(string entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T?>));
            return x.Deserialize(file) as List<T?> ?? new();
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            throw new Exception($"fail to load xml file: {filePath}", ex);
        }
    }
    public static void SaveConfigXElement(string entity, int serial)
    {
        string filePath = $"{s_dir}Config.xml";
        try
        {
            XElement? config = XElement.Load(filePath);
            config.Element(entity)!.Value = serial.ToString();
            config.Save(filePath);
        }
        catch (Exception ex)
        {
            throw new Exception($"fail to load xml file: {filePath}", ex);
        }
    }

    public static DO.Product ConvertProduct_Xml_to_D0(this XElement xElem)
    {
        return new DO.Product
        {
            ProductID = int.Parse(xElem.Element("ID")!.Value),
            ProductName = xElem.Element("Brand")!.Value,
            Category = (Enums.productCategory?)Enum.Parse(typeof(DO.Enums.productCategory), xElem.Element("Category")!.Value),
            InStock = int.Parse(xElem.Element("InStock")!.Value),
            Price = double.Parse(xElem.Element("Price")!.Value)
        };
    }

    public static XElement LoadConfig()
    {
        string filePath = $"{s_dir}Config.xml";
        try
        {
            XElement? config = XElement.Load(filePath);
            return config;
        }
        catch (Exception ex)
        {
            throw new Exception($"fail to load xml file: {filePath}", ex);
        }
        #endregion
    }
}
