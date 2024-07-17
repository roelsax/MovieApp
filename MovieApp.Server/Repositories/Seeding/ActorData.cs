using MovieApp.Server.Models;

namespace MovieApp.Server.Repositories.Seeding
{
    public class ActorData
    {
        public static List<Actor> GetActors()
        {
            return new List<Actor>
            {
                 new Actor
            {
                ActorId = 1,
                Name = "Kevin Spacey",
                DateOfBirth = new DateOnly(1959, 7, 26),
                Bio = "Kevin Spacey Fowler, better known by his stage name Kevin Spacey, is an American actor of screen and stage, film director, producer, screenwriter and singer. He began his career as a stage actor during the 1980s before obtaining supporting roles in film and television. He gained critical acclaim in the early 1990s that culminated in his first Academy Award for Best Supporting Actor for the neo-noir crime thriller The Usual Suspects (1995), and an Academy Award for Best Actor for midlife crisis-themed drama American Beauty (1999).\r\n",
                Picture = "kevinspacey.jpeg",
                Location = "Baltimore",
                Nationality = "USA",
            },

             new Actor
            {
                ActorId = 2,
                Name = "Mena Suvari",
                DateOfBirth = new DateOnly(1979, 2, 13),
                Bio = "Mena Alexandra Suvari was born in Newport, Rhode Island, the youngest of four children. She is the daughter of Ando Suvari, a psychiatrist, and the former Candice Chambers, a nurse. Mena's first name comes from her British aunt named after the \"House of Mena\" Hotel (at the base of the pyramids in Egypt); her last name is Estonian. Suvari grew up in an old stone mansion that she insists was haunted. The family later relocated to Charleston, South Carolina, where her brothers lined up to attend the Citadel (a military college). Mena, meanwhile, was entertaining dreams of becoming an archaeologist, astronaut, or doctor. Her interests took a turn for the... less cerebral, however, when a modeling agency stopped by her all-girls school to offer classes. At age 12, after receiving a few pointers on her runway strut, Suvari attended a modeling convention and was snapped up by the Manhattan-based Wilhelmina agency. She later moved to L.A. under their children's theatrical division WeeWillys, which began her acting career.",
                Picture = "menasuvari.jpeg",
                Location = "New York",
                Nationality = "USA",
            },

            new Actor
            {
                ActorId = 3,
                Name = "Anette bening",
                DateOfBirth = new DateOnly(1958, 5, 29),
                Bio = "Annette Bening was born on May 29, 1958 in Topeka, Kansas, the youngest of four children. Her family moved to California when she was young, and she grew up there. She graduated from San Francisco State University and began her acting career with the American Conservatory Theatre in San Francisco, eventually moving to New York where she acted on the stage (including a Tony-award nomination in 1987 for her work in the Broadway play \"Coastal Disturbances\") and got her first film roles, in a few TV movies.\r\n",
                Picture = "anettebening.jpeg",
                Location = "Hollywood",
                Nationality = "USA",
            },

            new Actor
            {
                ActorId = 4,
                Name = "Ryan Gosling",
                DateOfBirth = new DateOnly(1980, 11, 12),
                Bio = "Ryan Thomas Gosling (/ˈɡɒslɪŋ/ GOSS-ling;[1] born November 12, 1980) is a Canadian actor. Prominent in both independent films and major studio features, his films have grossed over $2 billion worldwide. Gosling has received various accolades, including a Golden Globe Award, and nominations for three Academy Awards and two British Academy Film Awards.\r\n\r\n",
                Picture = "ryangosling.jpeg",
                Location = "Hollywood",
                Nationality = "USA",
            },

            new Actor
            {
                ActorId = 5,
                Name = "Ana De Armas",
                DateOfBirth = new DateOnly(1988, 4, 30),
                Bio = "Ana Celia de Armas Caso (Spanish pronunciation: [ˈana ˈselya ðe ˈaɾmas ˈkaso]; born 30 April 1988) is a Cuban and Spanish actress. She began her career in Cuba with a leading role in the romantic drama Una rosa de Francia (2006). At the age of 18, she moved to Madrid, Spain, and starred in the popular drama El Internado for six seasons from 2007 to 2010.",
                Picture = "anadearmas.png",
                Location = "Hollywood",
                Nationality = "USA"
            },

            new Actor
            {
                ActorId = 6,
                Name = "Harrison Ford",
                DateOfBirth = new DateOnly(1942, 7, 13),
                Bio = "Harrison Ford (born July 13, 1942) is an American actor. He has been a leading man in films of several genres, and is regarded as an American cultural icon.[1] His films have grossed more than $5.4 billion in North America and more than $9.3 billion worldwide.[2][3][4] Ford is the recipient of various accolades, including the AFI Life Achievement Award, the Cecil B. DeMille Award, an Honorary César, and an Honorary Palme d'Or, in addition to an Academy Award nomination.[5][6]",
                Picture = "harrisonford.jpeg",
                Location = "Wyoming",
                Nationality = "USA"
            },

            new Actor
            {
                ActorId = 7,
                Name = "Toni Collette",
                DateOfBirth = new DateOnly(1972, 11, 1),
                Bio = "Toni Collette is an Academy Award-nominated Australian actress, best known for her roles in The Sixth Sense (1999) and Little Miss Sunshine (2006).\r\n",
                Picture = "tonicollette.jpeg",
                Location = "Sydney",
                Nationality = "Australian",
            },

             new Actor
            {
                ActorId = 8,
                Name = "Milly Shapiro",
                DateOfBirth = new DateOnly(2002, 7, 12),
                Bio = "Milly Shapiro is an American actress and singer. She starred in the 2018 horror film Hereditary and originated the role of Matilda Wormwood in the Broadway production of Matilda. She has also played Sally Brown in an Off-Broadway production of You're a Good Man, Charlie Brown. In 2021 she co-created the band AFTERxCLASS writing and singing for the band.",
                Picture = "millyshapiro.jpeg",
                Location = "Tampa",
                Nationality = "USA"
            },

            new Actor
            {
                ActorId = 9,
                Name = "Alex Wolff",
                DateOfBirth = new DateOnly(1997, 11, 1),
                Bio = "Alex Wolff, an award-winning actor, musician, singer, and composer, was born on November 1, 1997 in New York, New York. He is the son of actress Polly Draper and jazz pianist Michael Wolff. His elder brother is actor and musician Nat Wolff.\r\n\r\nHe is most known for his work on The Naked Brothers Band (2007), Mr. Troop Mom (2008), In Treatment (2008), HairBrained (2013), Stella's Last Weekend (2018), Patriots Day (2016), Hereditary (2018), Jumanji: The Next Level (2019), and The Cat and the Moon (2019).",
                Picture = "alexwolff.jpg",
                Location = "New York",
                Nationality = "USA",
            }
        };
        }
    }
}
