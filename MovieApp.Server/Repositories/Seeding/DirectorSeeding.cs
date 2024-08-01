using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Server.Models;

namespace MovieApp.Server.Repositories.Seeding
{
    public class DirectorSeeding : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder
                .Property(a => a.Aangepast).HasColumnType("timestamp");

            builder
                .Property(a => a.Aangepast)
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();

            Director sammendes = new Director
            {
                DirectorId = 1,
                Name = "Sam Mendes",
                DateOfBirth = new DateOnly(1965, 8, 1),
                Bio = "Samuel Alexander Mendes was born on August 1, 1965 in Reading, England, UK to parents James Peter Mendes, a retired university lecturer, and Valerie Helene Mendes, an author who writes children's books. Their marriage didn't last long, James divorced Sam's mother in 1970 when Sam was just 5-years-old. Sam was educated at Cambridge University and joined the Chichester Festival Theatre following his graduation in 1987. Afterwards, he directed Judi Dench in \"The Cherry Orchard\", for which he won a Critics Circle Award for Best Newcomer. He then joined the Royal Shakespeare Company, where he directed such productions as \"Troilus and Cressida\" with Ralph Fiennes and \"Richard III\". In 1992, he became artistic director of the reopened Donmar Warehouse in London, where he directed such productions as \"The Glass Menagerie\" and the revival of the musical \"Cabaret\", which earned four Tony Awards including one for Best Revival of a Musical. He also directed \"The Blue Room\" starring Nicole Kidman. In 1999, he got the chance to direct his first feature film, American Beauty (1999). The movie earned 5 Academy Awards including Best Picture and Best Director for Mendes, which is a rare feat for a first-time film director.\r\n",
                PictureId = 1,
                Location = "Hollywood",
                Nationality = "UK"
            };

            Director denisvilleneuve = new Director
            {
                DirectorId = 2,
                Name = "Denis Villeneuve",
                DateOfBirth = new DateOnly(1967, 10, 3),
                Bio = "Denis Villeneuve OC CQ RCA (French: [dəni vilnœv]; born October 3, 1967) is a Canadian filmmaker. He has received seven Canadian Screen Awards as well as nominations for three Academy Awards, five BAFTA Awards, and two Golden Globe Awards. Villeneuve's films have grossed over $1.8 billion worldwide.",
                PictureId = 2,
                Location = "Gentilly",
                Nationality = "Canadian",
            };

            Director ariaster = new Director
            {
                DirectorId = 3,
                Name = "Ari Aster",
                DateOfBirth = new DateOnly(1986, 7, 15),
                Bio = "Ari Aster is an American film director, screenwriter, and producer. He is known for writing and directing the A24 horror films Hereditary (2018) and Midsommar (2019). Aster was born into a Jewish family in New York City on July 15, 1986, the son of a poet mother and musician father. He has a younger brother. He recalled going to see his first movie, Dick Tracy, when he was four years old. The film featured a scene where a character fired a Tommy gun in front of a wall of fire. Aster reportedly jumped from his seat and \"ran six New York City blocks\" while his mother tried to catch him. In his early childhood, Aster's family briefly lived in England, where his father opened a jazz nightclub in Chester. Aster enjoyed living there, but the family returned to the U.S. and settled in New Mexico when he was 10 years old.",
                PictureId = 3,
                Location = "New Mexico",
                Nationality = "USA"
            };

            builder.HasData(sammendes, denisvilleneuve, ariaster);
        }
    }
}
