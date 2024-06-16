using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    ActorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ActorId);
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    DirectorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.DirectorId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectorId = table.Column<int>(type: "int", nullable: true),
                    Genres = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movies_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "DirectorId");
                });

            migrationBuilder.CreateTable(
                name: "ActorMovies",
                columns: table => new
                {
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovies", x => new { x.ActorId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_ActorMovies_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "ActorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "ActorId", "Bio", "DateOfBirth", "Location", "Name", "Nationality", "Picture" },
                values: new object[,]
                {
                    { 1, "Kevin Spacey Fowler, better known by his stage name Kevin Spacey, is an American actor of screen and stage, film director, producer, screenwriter and singer. He began his career as a stage actor during the 1980s before obtaining supporting roles in film and television. He gained critical acclaim in the early 1990s that culminated in his first Academy Award for Best Supporting Actor for the neo-noir crime thriller The Usual Suspects (1995), and an Academy Award for Best Actor for midlife crisis-themed drama American Beauty (1999).\r\n", new DateOnly(1959, 7, 26), "Baltimore", "Kevin Spacey", "USA", "kevinspacey.jpeg" },
                    { 2, "Mena Alexandra Suvari was born in Newport, Rhode Island, the youngest of four children. She is the daughter of Ando Suvari, a psychiatrist, and the former Candice Chambers, a nurse. Mena's first name comes from her British aunt named after the \"House of Mena\" Hotel (at the base of the pyramids in Egypt); her last name is Estonian. Suvari grew up in an old stone mansion that she insists was haunted. The family later relocated to Charleston, South Carolina, where her brothers lined up to attend the Citadel (a military college). Mena, meanwhile, was entertaining dreams of becoming an archaeologist, astronaut, or doctor. Her interests took a turn for the... less cerebral, however, when a modeling agency stopped by her all-girls school to offer classes. At age 12, after receiving a few pointers on her runway strut, Suvari attended a modeling convention and was snapped up by the Manhattan-based Wilhelmina agency. She later moved to L.A. under their children's theatrical division WeeWillys, which began her acting career.", new DateOnly(1979, 2, 13), "New York", "Mena Suvari", "USA", "menasuvari.jpeg" },
                    { 3, "Annette Bening was born on May 29, 1958 in Topeka, Kansas, the youngest of four children. Her family moved to California when she was young, and she grew up there. She graduated from San Francisco State University and began her acting career with the American Conservatory Theatre in San Francisco, eventually moving to New York where she acted on the stage (including a Tony-award nomination in 1987 for her work in the Broadway play \"Coastal Disturbances\") and got her first film roles, in a few TV movies.\r\n", new DateOnly(1958, 5, 29), "Hollywood", "Anette bening", "USA", "anettebening.jpeg" },
                    { 4, "Ryan Thomas Gosling (/ˈɡɒslɪŋ/ GOSS-ling;[1] born November 12, 1980) is a Canadian actor. Prominent in both independent films and major studio features, his films have grossed over $2 billion worldwide. Gosling has received various accolades, including a Golden Globe Award, and nominations for three Academy Awards and two British Academy Film Awards.\r\n\r\n", new DateOnly(1980, 11, 12), "Hollywood", "Ryan Gosling", "USA", "ryangosling.jpeg" },
                    { 5, "Ana Celia de Armas Caso (Spanish pronunciation: [ˈana ˈselya ðe ˈaɾmas ˈkaso]; born 30 April 1988) is a Cuban and Spanish actress. She began her career in Cuba with a leading role in the romantic drama Una rosa de Francia (2006). At the age of 18, she moved to Madrid, Spain, and starred in the popular drama El Internado for six seasons from 2007 to 2010.", new DateOnly(1988, 4, 30), "Hollywood", "Ana De Armas", "USA", "anadearmas.jpeg" },
                    { 6, "Harrison Ford (born July 13, 1942) is an American actor. He has been a leading man in films of several genres, and is regarded as an American cultural icon.[1] His films have grossed more than $5.4 billion in North America and more than $9.3 billion worldwide.[2][3][4] Ford is the recipient of various accolades, including the AFI Life Achievement Award, the Cecil B. DeMille Award, an Honorary César, and an Honorary Palme d'Or, in addition to an Academy Award nomination.[5][6]", new DateOnly(1942, 7, 13), "Wyoming", "Harrison Ford", "USA", "harissonford.jpeg" },
                    { 7, "Toni Collette is an Academy Award-nominated Australian actress, best known for her roles in The Sixth Sense (1999) and Little Miss Sunshine (2006).\r\n", new DateOnly(1972, 11, 1), "Sydney", "Toni Collette", "Australian", "tonicollette.jpeg" },
                    { 8, "Milly Shapiro is an American actress and singer. She starred in the 2018 horror film Hereditary and originated the role of Matilda Wormwood in the Broadway production of Matilda. She has also played Sally Brown in an Off-Broadway production of You're a Good Man, Charlie Brown. In 2021 she co-created the band AFTERxCLASS writing and singing for the band.", new DateOnly(2002, 7, 12), "Tampa", "Milly Shapiro", "USA", "millyshapiro.jpeg" },
                    { 9, "Alex Wolff, an award-winning actor, musician, singer, and composer, was born on November 1, 1997 in New York, New York. He is the son of actress Polly Draper and jazz pianist Michael Wolff. His elder brother is actor and musician Nat Wolff.\r\n\r\nHe is most known for his work on The Naked Brothers Band (2007), Mr. Troop Mom (2008), In Treatment (2008), HairBrained (2013), Stella's Last Weekend (2018), Patriots Day (2016), Hereditary (2018), Jumanji: The Next Level (2019), and The Cat and the Moon (2019).", new DateOnly(1997, 11, 1), "New York", "Alex Wolff", "USA", "alexwolff.jpeg" }
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "DirectorId", "Bio", "DateOfBirth", "Location", "Name", "Nationality", "Picture" },
                values: new object[,]
                {
                    { 1, "Samuel Alexander Mendes was born on August 1, 1965 in Reading, England, UK to parents James Peter Mendes, a retired university lecturer, and Valerie Helene Mendes, an author who writes children's books. Their marriage didn't last long, James divorced Sam's mother in 1970 when Sam was just 5-years-old. Sam was educated at Cambridge University and joined the Chichester Festival Theatre following his graduation in 1987. Afterwards, he directed Judi Dench in \"The Cherry Orchard\", for which he won a Critics Circle Award for Best Newcomer. He then joined the Royal Shakespeare Company, where he directed such productions as \"Troilus and Cressida\" with Ralph Fiennes and \"Richard III\". In 1992, he became artistic director of the reopened Donmar Warehouse in London, where he directed such productions as \"The Glass Menagerie\" and the revival of the musical \"Cabaret\", which earned four Tony Awards including one for Best Revival of a Musical. He also directed \"The Blue Room\" starring Nicole Kidman. In 1999, he got the chance to direct his first feature film, American Beauty (1999). The movie earned 5 Academy Awards including Best Picture and Best Director for Mendes, which is a rare feat for a first-time film director.\r\n", new DateOnly(1965, 8, 1), "Hollywood", "Sam Mendes", "UK", "Sam_Mendes_in_2022-2.jpg" },
                    { 2, "Denis Villeneuve OC CQ RCA (French: [dəni vilnœv]; born October 3, 1967) is a Canadian filmmaker. He has received seven Canadian Screen Awards as well as nominations for three Academy Awards, five BAFTA Awards, and two Golden Globe Awards. Villeneuve's films have grossed over $1.8 billion worldwide.", new DateOnly(1967, 10, 3), "Gentilly", "Denis Villeneuve", "Canadian", "Denis_Villeneuve_Cannes_2018.jpg" },
                    { 3, "Ari Aster is an American film director, screenwriter, and producer. He is known for writing and directing the A24 horror films Hereditary (2018) and Midsommar (2019). Aster was born into a Jewish family in New York City on July 15, 1986, the son of a poet mother and musician father. He has a younger brother. He recalled going to see his first movie, Dick Tracy, when he was four years old. The film featured a scene where a character fired a Tommy gun in front of a wall of fire. Aster reportedly jumped from his seat and \"ran six New York City blocks\" while his mother tried to catch him. In his early childhood, Aster's family briefly lived in England, where his father opened a jazz nightclub in Chester. Aster enjoyed living there, but the family returned to the U.S. and settled in New Mexico when he was 10 years old.", new DateOnly(1986, 7, 15), "New Mexico", "Ari Aster", "USA", "Ari_Aster,_2018_(crop).jpg" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Description", "DirectorId", "Genres", "Name", "Picture", "ReleaseDate" },
                values: new object[,]
                {
                    { 1, "A sexually frustrated suburban father has a mid-life crisis after becoming infatuated with his daughter's best friend.\r\n\r\n", 1, "[9,19,4]", "American Beauty", "american_beauty.jpg", new DateOnly(2000, 2, 2) },
                    { 2, "Young Blade Runner K's discovery of a long-buried secret leads him to track down former Blade Runner Rick Deckard, who's been missing for thirty years.\r\n\r\n", 2, "[21,0]", "Blade Runner 2049", "blade_runner.jpg", new DateOnly(2017, 10, 4) },
                    { 3, "A grieving family is haunted by tragic and disturbing occurrences.\r\n\r\n", 3, "[14]", "Hereditary", "hereditary.jpg", new DateOnly(2018, 6, 8) }
                });

            migrationBuilder.InsertData(
                table: "ActorMovies",
                columns: new[] { "ActorId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 2 },
                    { 5, 2 },
                    { 6, 2 },
                    { 7, 3 },
                    { 8, 3 },
                    { 9, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovies_MovieId",
                table: "ActorMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorMovies");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Directors");
        }
    }
}
