using System.Collections.Generic;
using ChessWeb.Domain.Entities;

namespace ChessWeb.Application.ViewModels.Role
{
    public class ChangeRoleViewModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public List<UserRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel()
        {
            AllRoles = new List<UserRole>();
            UserRoles = new List<string>();
        }
    }
}