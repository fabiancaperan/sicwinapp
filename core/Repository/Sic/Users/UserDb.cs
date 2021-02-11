using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using core.Entities.UserData;

namespace core.Repository.Sic.Users
{
    public class UserDb
    {
        public List<UserEditModel> GetUsers()
        {
            using var db = new dbContext();
            return db.User.Where(s => s.isInactive.Equals(false))
                .Select(s => new UserEditModel { userName = s.userName, isAdmin = s.isAdmin }).ToList();
        }

        public bool UpsertUser(string userName, bool isAdmin)
        {
            var isNew = true;
            using var db = new dbContext();
            var user = db.User.FirstOrDefault(s => s.userName.Equals(userName));
            if (user != null)
            {
                user.isAdmin = isAdmin;
                user.isInactive = false;
                isNew = false;
            }
            else
            {
                db.User.Add(new UserModel { userName = userName, isAdmin = isAdmin, DateCreated = DateTime.Now, isInactive = false });
            }
            db.SaveChanges();
            return isNew;
        }

        public bool DeleteUsers(string userName)
        {
            using var db = new dbContext();
            var user = db.User.FirstOrDefault(s => s.userName.Equals(userName));
            if (user != null)
            {
                user.isInactive = true;
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
