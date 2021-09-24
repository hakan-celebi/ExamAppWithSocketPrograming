using System;
using System.Collections.Generic;

namespace ExamApp.Models
{
    [Serializable]
    public class UserRole
    {
        public enum RoleEnum
        {
            admin,
            academician,
            student
        }
        public int Id { get; set; }
        public RoleEnum Role { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}