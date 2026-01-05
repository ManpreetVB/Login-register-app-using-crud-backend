using UserManagement.DAL;
using UserManagement.Models;

namespace UserManagement.BAL
{
    public class UserBAL
    {
        private readonly UserDAL userDAL;
        public UserBAL(IConfiguration configuration)
        {
            userDAL = new UserDAL(configuration);
        }

        public List<UserDetails> GetAllUsers()
        {
            return userDAL.GetAllUsers();
        }
        public string AddUser(UserDetails userDetails)
        {
            if (string.IsNullOrEmpty(userDetails.UserName))
            {
                return "UserName is required";
            }
            if (string.IsNullOrEmpty(userDetails.Email))
            {
                return "Email is required";
            }
            return userDAL.AddUser(userDetails);
        }

        public string UpdateUser(UserDetails userDetails)
        {
            if (string.IsNullOrEmpty(userDetails.UserName))
                return "EmployeeName is required";

            if (string.IsNullOrEmpty(userDetails.Email))
                return "Email is required";

            return userDAL.UpdateUser(userDetails);
        }


        public UserDetails GetUserById(int userId)
        {
           return userDAL.GetUserById(userId);
        }

        public string DeleteUser(int userId)
        {
            return userDAL.DeleteUser(userId);
        }

        public UserDetails LoginUser(UserDetails userDetails)
        {
            
            return userDAL.LoginUser(userDetails);
        }

        public string RegisterUser(UserDetails userDetails)
        {

            return userDAL.RegisterUser(userDetails);
        }
    }
}
