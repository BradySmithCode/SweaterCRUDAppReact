using System.ComponentModel.DataAnnotations;

namespace aspnetserver.Data
{
    internal sealed class Sweater
    {
        [Key]
        public int SweaterId { get; set; }

        [Required]
        [MaxLength(25)]
        public string manufacturer { get; set; } = string.Empty;

        [Required]
        public int quantity { get; set; }



    }
}
