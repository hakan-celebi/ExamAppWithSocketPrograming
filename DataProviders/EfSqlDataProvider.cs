using ExamApp.DataProviders;
using ExamApp.Managers;
using ExamApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace ExamApp.Entity
{
    public class EfSqlDataProvider : IEfDataProvider
    {
        public EfSqlDataProvider()
        {
            Db = new ExamAppDbContext();
        }
        private ExamAppDbContext Db { get; set; }
        public bool AddMessage(object AddToMessage)
        {
            Models.Message message = AddToMessage as Models.Message;
            if (message != null)
            {
                try
                {
                    Db.Messages.Add(message);
                    Db.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool AddUser(object AddToUser)
        {
            User user = AddToUser as User;
            if (user != null)
            {
                if ((from U in Db.Users where U.EMail == user.EMail select U).FirstOrDefault() == null &&
                    (from U in Db.Users where U.IP == user.IP && U.UserRole.Role == user.UserRole.Role select U).FirstOrDefault() == null)
                {
                    if (user.UserRole.Role.Equals(UserRole.RoleEnum.student) || user.UserRole.Role.Equals(UserRole.RoleEnum.academician))
                    {
                        try
                        {
                            Db.Users.Add(user);
                            Db.SaveChanges();
                        }
                        catch
                        {
                            return false;
                        }
                        return true;
                    }
                    else if (!(SignInManager.LoggedUser is null) && SignInManager.LoggedUser.UserRole.Role.Equals(UserRole.RoleEnum.admin))
                    {
                        try
                        {
                            Db.Users.Add(user);
                            Db.SaveChanges();
                        }
                        catch
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public bool AddUserRole(object AddToUserRole)
        {
            if (!(SignInManager.LoggedUser is null) && SignInManager.LoggedUser.UserRole.Role.Equals(UserRole.RoleEnum.admin))
            {
                UserRole userRole = AddToUserRole as UserRole;
                if (userRole != null)
                {
                    try
                    {
                        Db.UserRoles.Add(userRole);
                        Db.SaveChanges();
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public bool DeleteUser(object DeleteToUser)
        {
            if (!(SignInManager.LoggedUser is null) && SignInManager.LoggedUser.UserRole.Role.Equals(UserRole.RoleEnum.admin))
            {
                User user = DeleteToUser as User;
                if (user != null)
                {
                    try
                    {
                        Db.Users.Remove(user);
                        Db.SaveChanges();
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public bool DeleteUserRole(object DeleteToUserRole)
        {
            if (!(SignInManager.LoggedUser is null) && SignInManager.LoggedUser.UserRole.Role.Equals(UserRole.RoleEnum.admin))
            {
                UserRole userRole = DeleteToUserRole as UserRole;
                if (userRole != null)
                {
                    try
                    {
                        Db.UserRoles.Remove(userRole);
                        Db.SaveChanges();
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
        public List<object> FindMessageByData(byte[] Data) => (from M in Db.Messages where M.Data == Data select M).ToList<object>();
        public object FindMessageById(int Id) => Db.Messages.Find(Id);
        public List<object> FindMessageByMCode(int MCode) => (from M in Db.Messages where M.MCode == MCode select M).ToList<object>();
        public List<object> FindMessageByTime(DateTime Time) => (from M in Db.Messages where M.Time == Time select M).ToList<object>();
        public List<object> FindMessageBySenderUser(object User) => (from M in Db.Messages where M.SenderUserID == ((User)(User)).Id select M).ToList<object>();
        public List<object> FindMessageByReceiverUser(object User) => (from M in Db.Messages where M.ReceiverUserID == ((User)(User)).Id select M).ToList<object>();
        public object FindUserByEMail(string EMail) => (from U in Db.Users where U.EMail == EMail select U).FirstOrDefault();
        public object FindUserById(string Id) => Db.Users.Find(Id);
        public List<object> FindUsersByName(string Name) => (from U in Db.Users where U.Name == Name select U).ToList<object>();
        public List<object> FindUsersBySurname(string Surname) => (from U in Db.Users where U.Surname == Surname select U).ToList<object>();
        public List<object> FindUsersByFullName(string Name, string Surname) => (from U in Db.Users where U.Name == Name && U.Surname == Surname select U).ToList<object>();
        public List<object> FindUsersByRole(UserRole.RoleEnum Role) => (from U in Db.Users where U.UserRole.Role == Role select U).ToList<object>();
        public object FindUserByIP(string IP) => (from U in Db.Users where U.IP == IP select U).FirstOrDefault();
        public object FindUserRoleById(int Id) => Db.UserRoles.Find(Id);
        public object FindUserRoleByRole(UserRole.RoleEnum Role) => (from Ur in Db.UserRoles where Ur.Role == Role select Ur).FirstOrDefault();
        public bool UpdateUser(object UpdateToUser)
        {
            User user = UpdateToUser as User;
            string mail = user.EMail;
            if (user != null && (from U in Db.Users where U.Id != user.Id && U.EMail == user.EMail select U).FirstOrDefault() is null)
            {                
                if (!(SignInManager.LoggedUser is null) 
                    && (SignInManager.LoggedUser.Id.Equals(user.Id) || SignInManager.LoggedUser.UserRole.Role.Equals(UserRole.RoleEnum.admin) ||
                    SignInManager.LoggedUser.UserRole.Role.Equals(UserRole.RoleEnum.academician)))
                {
                    try
                    {
                        Db.Entry(user).State = EntityState.Modified;
                        Db.SaveChanges();
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public bool UpdateUserRole(object UpdateToUserRole)
        {
            if (!(SignInManager.LoggedUser is null) && SignInManager.LoggedUser.UserRole.Role.Equals(UserRole.RoleEnum.admin))
            {
                UserRole userRole = UpdateToUserRole as UserRole;
                if (userRole != null)
                {
                    try
                    {
                        Db.Entry(userRole).State = EntityState.Modified;
                        Db.SaveChanges();
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public List<object> AllUsers() => Db.Users.ToList<object>();

        public List<object> AllUserRoles() => Db.UserRoles.ToList<object>();

        public List<object> AllMessages() => Db.Messages.ToList<object>();
    }
}