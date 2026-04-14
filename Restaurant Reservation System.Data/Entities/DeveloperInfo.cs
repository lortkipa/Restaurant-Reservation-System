using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant_Reservation_System.Data.Entities
{
    [Table("DeveloperInfos")]
    public class DeveloperInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PersonId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Role { get; set; } = null!;
        //[Required]
        //[MaxLength(500)]
        //public string Bio { get; set; } = null!;
        [MaxLength(500)]
        public string? GithubLink { get; set; }
        [MaxLength(500)]
        public string? LinkedinLink { get; set; }
        [MaxLength(500)]
        public string? PortfolioLink { get; set; }

        // DeveloperInfo => Person
        public Person? Person {  get; set; }
    }
}
