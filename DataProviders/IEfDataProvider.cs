using ExamApp.Models;
using System;
using System.Collections.Generic;
using static ExamApp.Models.UserRole;

namespace ExamApp.DataProviders
{
    public interface IEfDataProvider
    {
        bool AddUser(object AddToUser);
        bool DeleteUser(object DeleteToUser);
        bool UpdateUser(object UpdateToUser);
        List<object> AllUsers();
        object FindUserById(string Id);
        List<object> FindUsersByName(string Name);
        List<object> FindUsersBySurname(string Surname);
        List<object> FindUsersByFullName(string Name, string Surname);
        List<object> FindUsersByRole(UserRole.RoleEnum Role);
        object FindUserByEMail(string EMail);
        object FindUserByIP(string IP);
        bool AddUserRole(object AddToUserRole);
        bool DeleteUserRole(object DeleteToUserRole);
        bool UpdateUserRole(object UpdateToUserRole);
        List<object> AllUserRoles();
        object FindUserRoleById(int Id);
        object FindUserRoleByRole(RoleEnum Role);
        bool AddMessage(object AddToMessage);
        List<object> AllMessages();
        object FindMessageById(int Id);
        List<object> FindMessageByMCode(int MCode);
        List<object> FindMessageByData(byte[] Data);
        List<object> FindMessageByTime(DateTime Time);
        List<object> FindMessageBySenderUser(object SenderUser);
        List<object> FindMessageByReceiverUser(object ReceiverUser);
    }
}