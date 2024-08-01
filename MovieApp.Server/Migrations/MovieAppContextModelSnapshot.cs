﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieApp.Server.Models;

#nullable disable

namespace MovieApp.Server.Migrations
{
    [DbContext(typeof(MovieAppContext))]
    partial class MovieAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MovieApp.Server.Models.Actor", b =>
                {
                    b.Property<int>("ActorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActorId"));

                    b.Property<byte[]>("Aangepast")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PictureId")
                        .HasColumnType("int");

                    b.HasKey("ActorId");

                    b.HasIndex("PictureId");

                    b.ToTable("Actors");

                    b.HasData(
                        new
                        {
                            ActorId = 1,
                            Bio = "Kevin Spacey Fowler, better known by his stage name Kevin Spacey, is an American actor of screen and stage, film director, producer, screenwriter and singer. He began his career as a stage actor during the 1980s before obtaining supporting roles in film and television. He gained critical acclaim in the early 1990s that culminated in his first Academy Award for Best Supporting Actor for the neo-noir crime thriller The Usual Suspects (1995), and an Academy Award for Best Actor for midlife crisis-themed drama American Beauty (1999).\r\n",
                            DateOfBirth = new DateOnly(1959, 7, 26),
                            Location = "Baltimore",
                            Name = "Kevin Spacey",
                            Nationality = "USA",
                            PictureId = 4
                        },
                        new
                        {
                            ActorId = 2,
                            Bio = "Mena Alexandra Suvari was born in Newport, Rhode Island, the youngest of four children. She is the daughter of Ando Suvari, a psychiatrist, and the former Candice Chambers, a nurse. Mena's first name comes from her British aunt named after the \"House of Mena\" Hotel (at the base of the pyramids in Egypt); her last name is Estonian. Suvari grew up in an old stone mansion that she insists was haunted. The family later relocated to Charleston, South Carolina, where her brothers lined up to attend the Citadel (a military college). Mena, meanwhile, was entertaining dreams of becoming an archaeologist, astronaut, or doctor. Her interests took a turn for the... less cerebral, however, when a modeling agency stopped by her all-girls school to offer classes. At age 12, after receiving a few pointers on her runway strut, Suvari attended a modeling convention and was snapped up by the Manhattan-based Wilhelmina agency. She later moved to L.A. under their children's theatrical division WeeWillys, which began her acting career.",
                            DateOfBirth = new DateOnly(1979, 2, 13),
                            Location = "New York",
                            Name = "Mena Suvari",
                            Nationality = "USA",
                            PictureId = 5
                        },
                        new
                        {
                            ActorId = 3,
                            Bio = "Annette Bening was born on May 29, 1958 in Topeka, Kansas, the youngest of four children. Her family moved to California when she was young, and she grew up there. She graduated from San Francisco State University and began her acting career with the American Conservatory Theatre in San Francisco, eventually moving to New York where she acted on the stage (including a Tony-award nomination in 1987 for her work in the Broadway play \"Coastal Disturbances\") and got her first film roles, in a few TV movies.\r\n",
                            DateOfBirth = new DateOnly(1958, 5, 29),
                            Location = "Hollywood",
                            Name = "Anette bening",
                            Nationality = "USA",
                            PictureId = 6
                        },
                        new
                        {
                            ActorId = 4,
                            Bio = "Ryan Thomas Gosling (/ˈɡɒslɪŋ/ GOSS-ling;[1] born November 12, 1980) is a Canadian actor. Prominent in both independent films and major studio features, his films have grossed over $2 billion worldwide. Gosling has received various accolades, including a Golden Globe Award, and nominations for three Academy Awards and two British Academy Film Awards.\r\n\r\n",
                            DateOfBirth = new DateOnly(1980, 11, 12),
                            Location = "Hollywood",
                            Name = "Ryan Gosling",
                            Nationality = "USA",
                            PictureId = 7
                        },
                        new
                        {
                            ActorId = 5,
                            Bio = "Ana Celia de Armas Caso (Spanish pronunciation: [ˈana ˈselya ðe ˈaɾmas ˈkaso]; born 30 April 1988) is a Cuban and Spanish actress. She began her career in Cuba with a leading role in the romantic drama Una rosa de Francia (2006). At the age of 18, she moved to Madrid, Spain, and starred in the popular drama El Internado for six seasons from 2007 to 2010.",
                            DateOfBirth = new DateOnly(1988, 4, 30),
                            Location = "Hollywood",
                            Name = "Ana De Armas",
                            Nationality = "USA",
                            PictureId = 8
                        },
                        new
                        {
                            ActorId = 6,
                            Bio = "Harrison Ford (born July 13, 1942) is an American actor. He has been a leading man in films of several genres, and is regarded as an American cultural icon.[1] His films have grossed more than $5.4 billion in North America and more than $9.3 billion worldwide.[2][3][4] Ford is the recipient of various accolades, including the AFI Life Achievement Award, the Cecil B. DeMille Award, an Honorary César, and an Honorary Palme d'Or, in addition to an Academy Award nomination.[5][6]",
                            DateOfBirth = new DateOnly(1942, 7, 13),
                            Location = "Wyoming",
                            Name = "Harrison Ford",
                            Nationality = "USA",
                            PictureId = 9
                        },
                        new
                        {
                            ActorId = 7,
                            Bio = "Toni Collette is an Academy Award-nominated Australian actress, best known for her roles in The Sixth Sense (1999) and Little Miss Sunshine (2006).\r\n",
                            DateOfBirth = new DateOnly(1972, 11, 1),
                            Location = "Sydney",
                            Name = "Toni Collette",
                            Nationality = "Australian",
                            PictureId = 10
                        },
                        new
                        {
                            ActorId = 8,
                            Bio = "Milly Shapiro is an American actress and singer. She starred in the 2018 horror film Hereditary and originated the role of Matilda Wormwood in the Broadway production of Matilda. She has also played Sally Brown in an Off-Broadway production of You're a Good Man, Charlie Brown. In 2021 she co-created the band AFTERxCLASS writing and singing for the band.",
                            DateOfBirth = new DateOnly(2002, 7, 12),
                            Location = "Tampa",
                            Name = "Milly Shapiro",
                            Nationality = "USA",
                            PictureId = 11
                        },
                        new
                        {
                            ActorId = 9,
                            Bio = "Alex Wolff, an award-winning actor, musician, singer, and composer, was born on November 1, 1997 in New York, New York. He is the son of actress Polly Draper and jazz pianist Michael Wolff. His elder brother is actor and musician Nat Wolff.\r\n\r\nHe is most known for his work on The Naked Brothers Band (2007), Mr. Troop Mom (2008), In Treatment (2008), HairBrained (2013), Stella's Last Weekend (2018), Patriots Day (2016), Hereditary (2018), Jumanji: The Next Level (2019), and The Cat and the Moon (2019).",
                            DateOfBirth = new DateOnly(1997, 11, 1),
                            Location = "New York",
                            Name = "Alex Wolff",
                            Nationality = "USA",
                            PictureId = 12
                        });
                });

            modelBuilder.Entity("MovieApp.Server.Models.ActorMovie", b =>
                {
                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("ActorId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("ActorMovies");

                    b.HasData(
                        new
                        {
                            ActorId = 1,
                            MovieId = 1
                        },
                        new
                        {
                            ActorId = 2,
                            MovieId = 1
                        },
                        new
                        {
                            ActorId = 3,
                            MovieId = 1
                        },
                        new
                        {
                            ActorId = 4,
                            MovieId = 2
                        },
                        new
                        {
                            ActorId = 5,
                            MovieId = 2
                        },
                        new
                        {
                            ActorId = 6,
                            MovieId = 2
                        },
                        new
                        {
                            ActorId = 7,
                            MovieId = 3
                        },
                        new
                        {
                            ActorId = 8,
                            MovieId = 3
                        },
                        new
                        {
                            ActorId = 9,
                            MovieId = 3
                        });
                });

            modelBuilder.Entity("MovieApp.Server.Models.Director", b =>
                {
                    b.Property<int>("DirectorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DirectorId"));

                    b.Property<byte[]>("Aangepast")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PictureId")
                        .HasColumnType("int");

                    b.HasKey("DirectorId");

                    b.HasIndex("PictureId");

                    b.ToTable("Directors");

                    b.HasData(
                        new
                        {
                            DirectorId = 1,
                            Bio = "Samuel Alexander Mendes was born on August 1, 1965 in Reading, England, UK to parents James Peter Mendes, a retired university lecturer, and Valerie Helene Mendes, an author who writes children's books. Their marriage didn't last long, James divorced Sam's mother in 1970 when Sam was just 5-years-old. Sam was educated at Cambridge University and joined the Chichester Festival Theatre following his graduation in 1987. Afterwards, he directed Judi Dench in \"The Cherry Orchard\", for which he won a Critics Circle Award for Best Newcomer. He then joined the Royal Shakespeare Company, where he directed such productions as \"Troilus and Cressida\" with Ralph Fiennes and \"Richard III\". In 1992, he became artistic director of the reopened Donmar Warehouse in London, where he directed such productions as \"The Glass Menagerie\" and the revival of the musical \"Cabaret\", which earned four Tony Awards including one for Best Revival of a Musical. He also directed \"The Blue Room\" starring Nicole Kidman. In 1999, he got the chance to direct his first feature film, American Beauty (1999). The movie earned 5 Academy Awards including Best Picture and Best Director for Mendes, which is a rare feat for a first-time film director.\r\n",
                            DateOfBirth = new DateOnly(1965, 8, 1),
                            Location = "Hollywood",
                            Name = "Sam Mendes",
                            Nationality = "UK",
                            PictureId = 1
                        },
                        new
                        {
                            DirectorId = 2,
                            Bio = "Denis Villeneuve OC CQ RCA (French: [dəni vilnœv]; born October 3, 1967) is a Canadian filmmaker. He has received seven Canadian Screen Awards as well as nominations for three Academy Awards, five BAFTA Awards, and two Golden Globe Awards. Villeneuve's films have grossed over $1.8 billion worldwide.",
                            DateOfBirth = new DateOnly(1967, 10, 3),
                            Location = "Gentilly",
                            Name = "Denis Villeneuve",
                            Nationality = "Canadian",
                            PictureId = 2
                        },
                        new
                        {
                            DirectorId = 3,
                            Bio = "Ari Aster is an American film director, screenwriter, and producer. He is known for writing and directing the A24 horror films Hereditary (2018) and Midsommar (2019). Aster was born into a Jewish family in New York City on July 15, 1986, the son of a poet mother and musician father. He has a younger brother. He recalled going to see his first movie, Dick Tracy, when he was four years old. The film featured a scene where a character fired a Tommy gun in front of a wall of fire. Aster reportedly jumped from his seat and \"ran six New York City blocks\" while his mother tried to catch him. In his early childhood, Aster's family briefly lived in England, where his father opened a jazz nightclub in Chester. Aster enjoyed living there, but the family returned to the U.S. and settled in New Mexico when he was 10 years old.",
                            DateOfBirth = new DateOnly(1986, 7, 15),
                            Location = "New Mexico",
                            Name = "Ari Aster",
                            Nationality = "USA",
                            PictureId = 3
                        });
                });

            modelBuilder.Entity("MovieApp.Server.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            ImageId = 1,
                            ImagePath = "Sam_Mendes_in_2022-2.jpg"
                        },
                        new
                        {
                            ImageId = 2,
                            ImagePath = "Denis_Villeneuve_Cannes_2018.jpg"
                        },
                        new
                        {
                            ImageId = 3,
                            ImagePath = "Ari_Aster,_2018_(crop).jpg"
                        },
                        new
                        {
                            ImageId = 4,
                            ImagePath = "kevinspacey.jpeg"
                        },
                        new
                        {
                            ImageId = 5,
                            ImagePath = "menasuvari.jpeg"
                        },
                        new
                        {
                            ImageId = 6,
                            ImagePath = "anettebening.jpeg"
                        },
                        new
                        {
                            ImageId = 7,
                            ImagePath = "ryangosling.jpeg"
                        },
                        new
                        {
                            ImageId = 8,
                            ImagePath = "anadearmas.png"
                        },
                        new
                        {
                            ImageId = 9,
                            ImagePath = "harrisonford.jpeg"
                        },
                        new
                        {
                            ImageId = 10,
                            ImagePath = "tonicollette.jpeg"
                        },
                        new
                        {
                            ImageId = 11,
                            ImagePath = "millyshapiro.jpeg"
                        },
                        new
                        {
                            ImageId = 12,
                            ImagePath = "alexwolff.jpg"
                        },
                        new
                        {
                            ImageId = 13,
                            ImagePath = "american_beauty.jpg"
                        },
                        new
                        {
                            ImageId = 14,
                            ImagePath = "blade_runner.jpg"
                        },
                        new
                        {
                            ImageId = 15,
                            ImagePath = "hereditary.jpg"
                        });
                });

            modelBuilder.Entity("MovieApp.Server.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovieId"));

                    b.Property<byte[]>("Aangepast")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DirectorId")
                        .HasColumnType("int");

                    b.Property<string>("Genres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PictureId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("ReleaseDate")
                        .HasColumnType("date");

                    b.HasKey("MovieId");

                    b.HasIndex("DirectorId");

                    b.HasIndex("PictureId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            Description = "A sexually frustrated suburban father has a mid-life crisis after becoming infatuated with his daughter's best friend.\r\n\r\n",
                            DirectorId = 1,
                            Genres = "[9,19,4]",
                            Name = "American Beauty",
                            PictureId = 13,
                            ReleaseDate = new DateOnly(2000, 2, 2)
                        },
                        new
                        {
                            MovieId = 2,
                            Description = "Young Blade Runner K's discovery of a long-buried secret leads him to track down former Blade Runner Rick Deckard, who's been missing for thirty years.\r\n\r\n",
                            DirectorId = 2,
                            Genres = "[21,0]",
                            Name = "Blade Runner 2049",
                            PictureId = 14,
                            ReleaseDate = new DateOnly(2017, 10, 4)
                        },
                        new
                        {
                            MovieId = 3,
                            Description = "A grieving family is haunted by tragic and disturbing occurrences.\r\n\r\n",
                            DirectorId = 3,
                            Genres = "[14]",
                            Name = "Hereditary",
                            PictureId = 15,
                            ReleaseDate = new DateOnly(2018, 6, 8)
                        });
                });

            modelBuilder.Entity("MovieApp.Server.Models.Actor", b =>
                {
                    b.HasOne("MovieApp.Server.Models.Image", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("MovieApp.Server.Models.ActorMovie", b =>
                {
                    b.HasOne("MovieApp.Server.Models.Actor", "Actor")
                        .WithMany("ActorMovies")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieApp.Server.Models.Movie", "Movie")
                        .WithMany("ActorMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieApp.Server.Models.Director", b =>
                {
                    b.HasOne("MovieApp.Server.Models.Image", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("MovieApp.Server.Models.Movie", b =>
                {
                    b.HasOne("MovieApp.Server.Models.Director", "Director")
                        .WithMany("Movies")
                        .HasForeignKey("DirectorId");

                    b.HasOne("MovieApp.Server.Models.Image", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId");

                    b.Navigation("Director");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("MovieApp.Server.Models.Actor", b =>
                {
                    b.Navigation("ActorMovies");
                });

            modelBuilder.Entity("MovieApp.Server.Models.Director", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("MovieApp.Server.Models.Movie", b =>
                {
                    b.Navigation("ActorMovies");
                });
#pragma warning restore 612, 618
        }
    }
}
