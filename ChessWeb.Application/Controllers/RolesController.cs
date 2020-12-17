using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessWeb.Application.Constants;
using ChessWeb.Application.ViewModels.Role;
using ChessWeb.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChessWeb.Application.Controllers
{
    [Authorize(Roles = Roles.AdminRole)]
    public class RolesController : Controller
    {
        RoleManager<UserRole> _roleManager;
        UserManager<User> _userManager;
        public RolesController(RoleManager<UserRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index() => 
            View(_roleManager.Roles.ToList());
 
        public IActionResult Create() => 
            View();
        
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError("", "Пж, пустота не годится");
                return View();
            }

            if (string.Equals(name, Roles.AdminRole, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(name, Roles.PlayerRole, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("", $"Пж, {Roles.AdminRole} и {Roles.PlayerRole} уже забиты");
                return View();
            }
            
            var result = await _roleManager.CreateAsync(new UserRole(name));
            if (result.Succeeded)
                return RedirectToAction("Index");
                
            foreach (var error in result.Errors) 
                ModelState.AddModelError("", error.Description);
            return View();
        }
         
        [HttpPost]
        public async Task<IActionResult> Delete(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            if (role == null) 
                return NotFound();

            if (role.Name == Roles.AdminRole || role.Name == Roles.PlayerRole)
            {
                ModelState.AddModelError("", $"Пж, {role} не подлежит выпиливанию из множества ролей");
                return View("Index");
            }
            
            await _roleManager.DeleteAsync(role);
            return View("Index");
        }
 
        public IActionResult UserList() => 
            View(_userManager.Users.ToList());
 
        public async Task<IActionResult> Edit(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user == null) 
                return NotFound();
            
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();
            var model = new ChangeRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            };
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(string name, List<string> roles)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user == null) 
                return NotFound();
            
            var userRoles = await _userManager.GetRolesAsync(user);
            var addedRoles = roles.Except(userRoles);
            var removedRoles = userRoles.Except(roles);
 
            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
 
            return RedirectToAction("UserList");

        }
    }
}