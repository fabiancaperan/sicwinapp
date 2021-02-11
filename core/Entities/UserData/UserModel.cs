using System;
using System.ComponentModel.DataAnnotations;

namespace core.Entities.UserData
{
    public class UserModel
    {
        [Key]
        public string userName { get; set; }
        public bool isAdmin { get; set; }
        public bool isInactive { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
