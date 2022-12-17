using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualReality.Models
{
    
    public class House
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Price { get; set; }
        public String Location { get; set; } 
        public int SquareMeter { get; set; }
        public bool Furnished { get; set; }
        public int Floor { get; set; }

        // 3D Model link
    }
}
