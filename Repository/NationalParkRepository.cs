using ParksAPI.Data;
using ParksAPI.Models.Repository.IRepository;
using ParksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ParksAPI.Models.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly NationalParkDbContext nd;
        public NationalParkRepository(NationalParkDbContext db)
        {
            nd = db;
        }
        public bool CreateNationalPark(NationalPark obj)
        {
            nd.NationalParks.Add(obj);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark obj)
        {
            nd.NationalParks.Remove(obj);
            return Save();
        }

        public NationalPark GetNationalPark(int NationalParkId)
        {
            return nd.NationalParks.FirstOrDefault(n => n.Id == NationalParkId);   
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return nd.NationalParks.OrderBy(n => n.Name).ToList();
        }

        public bool NationalParkExists(string name)
        {
            bool check= nd.NationalParks.Any(n => n.Name.ToLower().Trim() == name.ToLower().Trim());
            return check;
        }

        public bool NationalParkExists(int id)
        {
            bool check = nd.NationalParks.Any(n => n.Id == id);
            return check;
        }

        public bool Save()
        {
            return nd.SaveChanges()>=0?true:false;
        }

        public bool UpdateNationalPark(NationalPark obj)
        {
            nd.NationalParks.Update(obj);
            return Save();
        }
    }
}
