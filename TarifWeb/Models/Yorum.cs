using System.ComponentModel.DataAnnotations;

namespace TarifWeb.Models
{
    public class Yorum
    {
        [Key]
        public int YorumId { get; set; }
        [Required]
        public string YorumBasligi { get; set; }
        [Required]
        public string YorumAciklama { get; set; }
        public double YemekDerecesi { get; set; }
        public int YemekId { get; set; }
        public Yemek Yemek { get; set; }
    }
}