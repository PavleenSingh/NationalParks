using ParksAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ParksAPI.Models.Trail;

namespace ParksAPI.Models.DTOs
{
    public class TrailDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }
       
        public DifficultyType Difficulty { get; set; }

        [Required]
        public int NationalParkId { get; set; }

        public NationalParkDTO NationalPark { get; set; }

    }
    
}
