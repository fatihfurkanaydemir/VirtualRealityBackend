﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualReality.Models
{
    
    public class House
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Price { get; set; }
        public String Latitude { get; set; }
        public String Longitude { get; set; }
        public int SquareMeter { get; set; }
        public bool Furnished { get; set; }
        public int Floor { get; set; }
        public string AssetLink { get; set; }
        public string ImageLink { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public ApplicationUser User { get; set; }

    }
}
