using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParksAPI.Models.Dtos
{
    public class NationalParkDTO
    {
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public DateTime Created { get; set; }
        public byte[] Picture { get; set; }

        public DateTime Established { get; set; }

    }
}
