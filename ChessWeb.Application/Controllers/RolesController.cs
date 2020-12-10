﻿using System.Collections.Generic;
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
            if (!string.IsNullOrEmpty(name))
            {
                var result = await _roleManager.CreateAsync(new UserRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }
         
        [HttpPost]
        public async Task<IActionResult> Delete(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
 
        public IActionResult UserList() => View(_userManager.Users.ToList());
 
        public async Task<IActionResult> Edit(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if(user!=null)
            {
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
 
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string name, List<string> roles)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user!=null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);
 
                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);
 
                return RedirectToAction("UserList");
            }
 
            return NotFound();
        }
    }
}