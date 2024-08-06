namespace MovieApp.Server.DTOs
{
    public class DirectorFormDTO
    {
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Nationality { get; set; }
        public IFormFile Picture { get; set; }
    }
}
