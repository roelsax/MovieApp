using System.ComponentModel.DataAnnotations;

namespace MovieApp.Server.DTOs
{
    public class ActorFormDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date of birth is required.")]
        public string DateOfBirth { get; set; }
        public string? Bio { get; set; }
        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        public string Nationality { get; set; }
        public IFormFile? Picture { get; set; }
    }
}
