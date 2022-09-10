using System.ComponentModel.DataAnnotations;

namespace WebApiTest.DTO.Users
{
    public class UserUpdateInfo
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
    }
}
