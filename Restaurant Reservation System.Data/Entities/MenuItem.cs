using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant_Reservation_System.Data.Entities
{
    [Table("MenuItems")]
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MenuId { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsAvaiable { get; set; }
        //[Required]
        public string? ImageUrl { get; set; }

        // MenuItems => Menu
        public Menu? Menu { get; set; }
    }
}