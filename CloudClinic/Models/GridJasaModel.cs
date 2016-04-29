﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    //public class GridJasaModel
    //{
    //    private ClinicContext db = new ClinicContext();

    //    public List<BillingJasa> GetJasa(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
    //    {
    //        //var doc = db.BillingJasa.Load(HttpContext.Current.Server.MapPath("~/App_Data/Players.xml"));
    //        total = db.BillingJasa.Count();
    //        var records = (from p in db.BillingJasa
    //                       select new BillingJasa
    //                       {
    //                           Gigi = p.Gigi,
    //                           TindakanId = p.TindakanId,
    //                           Total = p.Total.Value
    //                       }).AsQueryable();

    //        if (!string.IsNullOrWhiteSpace(searchString))
    //        {
    //            records = records.Where(p => p.Name.Contains(searchString) || p.PlaceOfBirth.Contains(searchString));
    //        }

    //        if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
    //        {
    //            if (direction.Trim().ToLower() == "asc")
    //            {
    //                records = SortHelper.OrderBy(records, sortBy);
    //            }
    //            else
    //            {
    //                records = SortHelper.OrderByDescending(records, sortBy);
    //            }
    //        }

    //        if (page.HasValue && limit.HasValue)
    //        {
    //            int start = (page.Value - 1) * limit.Value;
    //            records = records.Skip(start).Take(limit.Value);
    //        }

    //        return records.ToList();
    //    }

    //    public void Save(Player player)
    //    {
    //        string filePath = HttpContext.Current.Server.MapPath("~/App_Data/Players.xml");
    //        var doc = XDocument.Load(filePath);
    //        if (player.ID > 0)
    //        {
    //            XElement xplayer = (from p in doc.Descendants("Player")
    //                                where int.Parse(p.Element("ID").Value) == player.ID
    //                                select p).FirstOrDefault();
    //            if (xplayer != null)
    //            {
    //                xplayer.Element("Name").Value = player.Name;
    //                xplayer.Element("PlaceOfBirth").Value = player.PlaceOfBirth;
    //                xplayer.Element("DateOfBirth").Value = player.DateOfBirth;
    //            }
    //        }
    //        else
    //        {
    //            int? maxId = (from p in doc.Descendants("Player") select int.Parse(p.Element("ID").Value)).OrderByDescending(p => p).FirstOrDefault();
    //            int newId = maxId.HasValue ? (maxId.Value + 1) : 1;
    //            XElement node = new XElement("Player");
    //            node.Add(new XElement("ID", newId));
    //            node.Add(new XElement("Name", player.Name));
    //            node.Add(new XElement("PlaceOfBirth", player.PlaceOfBirth));
    //            node.Add(new XElement("DateOfBirth", player.DateOfBirth));
    //            doc.Root.Add(node);
    //        }
    //        doc.Save(filePath);
    //    }

    //    public void Remove(int id)
    //    {
    //        string filePath = HttpContext.Current.Server.MapPath("~/App_Data/Players.xml");
    //        var doc = XDocument.Load(filePath);
    //        XElement xplayer = (from p in doc.Descendants("Player")
    //                            where int.Parse(p.Element("ID").Value) == id
    //                            select p).FirstOrDefault();
    //        if (xplayer != null)
    //        {
    //            xplayer.Remove();
    //            doc.Save(filePath);
    //        }
    //    }
    //}
}