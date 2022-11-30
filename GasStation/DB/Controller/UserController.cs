using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.DB.Controller
{
    public class UserController
    {

        public static string createUser(string username,UserType role, string password)
        {

            DataBaseContext context = new DataBaseContext();
            try
            {
                User user = new User();
                user.UserRole = role;
                user.Name = username;
                user.Password = Md5.hashPassword(password);
                context.Users.Add(user);
                context.SaveChanges();
                return null;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static bool LogUser(string username, string password)
        {
            DataBaseContext context = new DataBaseContext();
            User user = context.Users.Where(x => x.Name == username).FirstOrDefault();
            if (user == null)
                return false;
            else
            {
                password = Md5.hashPassword(password);
                if (user.Password == password)
                    return true;
                else
                    return false;
            }
        }

        public static string EditUser(User oldeUser,User newUser)
        {
            try
            {
                DataBaseContext context = new DataBaseContext();
                var user = context.Users.Where(x => x.ID == oldeUser.ID).FirstOrDefault();
                if (user != null)
                {
                    if(newUser.UserRole!=null)
                        user.UserRole = newUser.UserRole;
                    if (newUser.Name != null)
                        user.Name = newUser.Name;
                    if (newUser.Password != null)
                        user.Password = Md5.hashPassword(newUser.Password);
                    context.SaveChanges();
                }
                return null;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public static string Remove(User usr)
        {
            try
            {
                DataBaseContext context = new DataBaseContext();
                var user = context.Users.Where(x => x.ID == usr.ID).FirstOrDefault();
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
