using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Server.Models;

namespace MovieApp.Server.Repositories.Seeding
{
    public class ImageSeeding : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            Image sammendes = new Image {
                ImageId = 1,
                ImagePath = "Sam_Mendes_in_2022-2.jpg",
            };

            Image denisvilleneuve = new Image
            {
                ImageId = 2,
                ImagePath = "Denis_Villeneuve_Cannes_2018.jpg",
            };

            Image ariaster = new Image
            {
                ImageId = 3,
                ImagePath = "Ari_Aster,_2018_(crop).jpg",
            };

            Image kevinspacey = new Image
            {
                ImageId = 4,
                ImagePath = "kevinspacey.jpeg",
            };

            Image menasuvari = new Image
            {
                ImageId = 5,
                ImagePath = "menasuvari.jpeg",
            };

            Image annettebening = new Image
            {
                ImageId = 6,
                ImagePath = "anettebening.jpeg",
            };

            Image ryangosling = new Image
            {
                ImageId = 7,
                ImagePath = "ryangosling.jpeg",
            };

            Image anadearmas = new Image
            {
                ImageId = 8,
                ImagePath = "anadearmas.png",
            };

            Image harrisonford = new Image
            {
                ImageId = 9,
                ImagePath = "harrisonford.jpeg",
            };

            Image tonicollette = new Image
            {
                ImageId = 10,
                ImagePath = "tonicollette.jpeg",
            };

            Image millyshapiro = new Image
            {
                ImageId = 11,
                ImagePath = "millyshapiro.jpeg",
            };

            Image alexwolff = new Image
            {
                ImageId = 12,
                ImagePath = "alexwolff.jpg",
            };

            Image americanbeauty = new Image
            {
                ImageId = 13,
                ImagePath = "american_beauty.jpg",
            };

            Image bladerunner = new Image
            {
                ImageId = 14,
                ImagePath = "blade_runner.jpg",
            };

            Image hereditary = new Image
            {
                ImageId = 15,
                ImagePath = "hereditary.jpg",
            };

            builder.HasData(
                sammendes, 
                denisvilleneuve, 
                ariaster, 
                kevinspacey, 
                menasuvari,
                annettebening,
                ryangosling,
                anadearmas,
                harrisonford,
                tonicollette,
                millyshapiro,
                alexwolff,
                americanbeauty,
                bladerunner,
                hereditary
                ); 
        }
    }
}
