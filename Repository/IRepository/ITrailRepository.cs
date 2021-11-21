using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParksAPI.Models.Repository.IRepository
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetTrails();
        ICollection<Trail> GetTrailsInNationalPark(int NationalParkId);
        Trail GetTrail(int TrailId);
        bool TrailExists(string name);
        bool TrailExists(int id);
        bool CreateTrail(Trail o);
        bool UpdateTrail(Trail o);
        bool DeleteTrail(Trail o);
        bool Save();
    }
}
