using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWorkshop.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Plates { get; set; }

        [Required]
        [StringLength(30)]
        public string Brand { get; set; }

        public string RepairList { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
