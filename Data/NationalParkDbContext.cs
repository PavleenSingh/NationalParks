using Microsoft.EntityFrameworkCore;
using ParksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParksAPI.Data
{
    public class NationalParkDbContext:DbContext
    {
        public NationalParkDbContext(DbContextOptions<NationalParkDbContext> options):base(options)
        {

        }
        public DbSet<NationalPark> NationalParks { get; set; }
        public DbSet<Trail> Trails { get; set; }

    }
}
