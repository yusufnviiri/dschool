﻿using victors.Actions;
using victors.Models.Context;
using victors.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace victors.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly UserActions _userActions = new();

        public ChangePasswordController(ApplicationDbContext db)
        {
            _db = db;

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChange change)
        {
            var data = HttpContext.User.Identity as ClaimsIdentity;
            if (data is not null)
            {
                var userData = data.Claims;
                var user = new User
                {
                    UserName = userData.FirstOrDefault(k => k.Type == ClaimTypes.NameIdentifier)?.Value,
                    Password = userData.FirstOrDefault(k => k.Type == ClaimTypes.Role)?.Value,
                };
                var userRef = await _userActions.GetUser(user, _db);
                user.UserId = userRef.UserId;
                if (change.OldPassword.ToLower() == change.NewPassword.ToLower() || change.NewPassword == string.Empty)
                {
                    return BadRequest("New Password is same with old password or new password is missing");
                }
                else
                {
                    var passwordChange = new PasswordChange
                    {
                        OldPassword = change.OldPassword,
                        NewPassword = change.NewPassword,
                        ConfirmPassword = change.ConfirmPassword,
                        UserName = user.UserName
                    };
                    await _userActions.UpdatePassword(_db, passwordChange);
                }
            }
            return Ok("Password Updated Successfully....You can Login with your new password");
        }
    }
}
