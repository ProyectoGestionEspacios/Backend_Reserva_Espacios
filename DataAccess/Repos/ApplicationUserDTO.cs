using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Repos
{
    [NotMapped]
    public class ApplicationUserDTO
    {

        public string? ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }


        public string UserName { get; set; }

    }
}