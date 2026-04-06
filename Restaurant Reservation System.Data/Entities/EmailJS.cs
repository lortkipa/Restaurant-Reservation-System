using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant_Reservation_System.Data.Entities
{
    [Table("EmailJSConfigs")]
    public class EmailJS
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [MaxLength(50)]
        public string? ServiceId { get; set; }
        [MaxLength(50)]
        public string? TemplateId { get; set; }
        [MaxLength(50)]
        public string? PublicKey { get; set; }

        // EmailJS => User
        public User User { get; set; } = null!;
    }
}
