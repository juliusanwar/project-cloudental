using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class Searcher
    {
        private readonly List<Pasien> list = new List<Pasien>();
        private IQueryable<Pasien> cachedList;

        public Searcher()
        {
            FillList();
        }

        public string SearchTerm { get; set; }

        public IQueryable<Pasien> GetSearchResults(string searchTerm)
        {
            if (String.IsNullOrEmpty(searchTerm))
            {
                //return empty list
                return new List<Pasien>().AsQueryable<Pasien>();
            }
            if (searchTerm != this.SearchTerm)
            {
                this.SearchTerm = searchTerm;
                cachedList = list.Where(r => r.UserName.ToString().Contains(searchTerm)).AsQueryable<Pasien>();
            }
            return cachedList;
        }

        private void FillList()
        {
            //TODO:  Fill your List properly here

            for (int i = 0; i < 10000; i++)
            {
                list.Add(new Pasien { PasienId = i, UserName = "A" + i.ToString() });
            }

        }
    }
}