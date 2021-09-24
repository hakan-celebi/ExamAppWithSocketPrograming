using ExamApp.Entity;
using ExamApp.Models;

namespace ExamApp.Managers
{
    public static class SignInManager
    {
        private static User user;
        public static User LoggedUser { get => user; }
        public static bool IsLogged(this User LoggedUser) => LoggedUser.Equals(user);
        public static bool Language { get; set; }
        public static bool Login(this User LoginUser)
        {
            if (user is null)
            {
                EfSqlDataProvider provider = new EfSqlDataProvider();
                User tempUser = provider.FindUserByEMail(LoginUser.EMail) as User;
                if(tempUser != null && tempUser.Password.Equals(LoginUser.Password) && tempUser.UserRole.Role.Equals(LoginUser.UserRole.Role))
                {
                    user = LoginUser;
                    return LoginUser.IsLogged();
                }
            }
            return false;
        }
        public static bool Logout(this User LogoutUser)
        {
            if (LogoutUser.IsLogged())
            {
                user = null;
                return LogoutUser.IsLogged();
            }
            return false;
        }
    }
}
