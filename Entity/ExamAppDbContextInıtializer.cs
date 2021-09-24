using ExamApp.DataProviders;
using ExamApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;

namespace ExamApp.Entity
{
    class ExamAppContextInitializer : CreateDatabaseIfNotExists<ExamAppDbContext>
    {
        protected override void Seed(ExamAppDbContext context)
        {
            context.UserRoles.Add(new UserRole() { Role = UserRole.RoleEnum.admin});
            context.UserRoles.Add(new UserRole() { Role = UserRole.RoleEnum.academician});
            context.UserRoles.Add(new UserRole() { Role = UserRole.RoleEnum.student});
            context.SaveChanges();
            UserRole role = context.UserRoles.Find(1);
            context.Users.Add(new User()
            {
                Name = "admin",
                Surname = "admin",
                Password = "root",
                EMail = "admin@root.me",
                UserRoleID = role.Id,
                UserRole = role
            });
            context.SaveChanges();
        }
    }
}
