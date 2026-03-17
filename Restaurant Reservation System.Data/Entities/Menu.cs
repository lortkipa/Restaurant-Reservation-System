using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant_Reservation_System.Data.Entities
{
    [Table("Menus")]
    public class Menu
    {
        // public decimal TotalPrice => Dishes.Where(d => d.IsAvaiable).Sum(d => d.Price);

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        // Menu => MenuItems
        public ICollection<MenuItem> Dishes { get; set; } = new List<MenuItem>();
        // Menus => Restaurant
        public Restaurant? Restaurant { get; set; }
    }
}