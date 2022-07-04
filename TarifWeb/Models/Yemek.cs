using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TarifWeb.Models
{
    public class Yemek
    {
        [Key]
        public int YemekId { get; set; }
        [Required]
        public string YemekAd { get; set; }
        [Required]
        public string YemekAciklamasi { get; set; }
        public string YemekFotografi { get; set; }
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }
        public List<Yorum> Yorum { get; set; }
    }
}