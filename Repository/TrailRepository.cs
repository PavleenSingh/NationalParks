using ParksAPI.Data;
using ParksAPI.Models.Repository.IRepository;
using ParksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace ParksAPI.Models.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly NationalParkDbContext nd;
        public TrailRepository(NationalParkDbContext db)
        {
            nd = db;
        }
        public bool CreateTrail(Trail obj)
        {
            nd.Trails.Add(obj);
            return Save();
        }

        public bool DeleteTrail(Trail obj)
        {
            nd.Trails.Remove(obj);
            return Save();
        }

        public Trail GetTrail(int TrailId)
        {
            return nd.Trails.Include(n => n.NationalPark).FirstOrDefault(n => n.Id == TrailId); 
        }

        public ICollection<Trail> GetTrails()
        {
            return nd.Trails.Include(n => n.NationalPark).OrderBy(n => n.Name).ToList();
        }

        public bool TrailExists(string name)
        {
            bool check= nd.Trails.Any(n => n.Name.ToLower().Trim() == name.ToLower().Trim());
            return check;
        }

        public bool TrailExists(int id)
        {
            bool check = nd.Trails.Any(n => n.Id == id);
            return check;
        }

        public bool Save()
        {
            return nd.SaveChanges()>=0?true:false;
        }

        public bool UpdateTrail(Trail obj)
        {
            nd.Trails.Update(obj);
            return Save();
        }

        public ICollection<Trail> GetTrailsInNationalPark(int NationalParkId)
        {
            return nd.Trails.Include(n => n.NationalPark).Where(n => n.NationalParkId == NationalParkId).ToList();
        }
    }
}
