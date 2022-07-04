using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TarifWeb.Models
{
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }
        [Required]
        public string KategoriAd { get; set; }
        public List<Yemek> Yemek { get; set; }
    }
}
