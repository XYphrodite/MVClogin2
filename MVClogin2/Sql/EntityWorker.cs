using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVClogin2.Areas.Identity.Data;
using MVClogin2.Models;
using MVClogin2.Sql.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVClogin2.Sql
{
    public class EntityWorker : ISqlWorker
    {
        private DbContextOptions<CustomDbContext> optionsCustom;
        private DbContextOptions<ApplicationDbContext> optionsApplication;
        
        public EntityWorker()
        {
            optionsApplication = new DbContextOptions<ApplicationDbContext>();
            optionsCustom = new DbContextOptions<CustomDbContext>();
        }

        public List<CalibrationModel> getCalibrations(string id)
        {
            List<CalibrationModel> list = new List<CalibrationModel>();
            var context = new CustomDbContext(optionsCustom);
            var query = context.Calibrations
                .Where(c => c.UserId == id);
            foreach (var m in query)
            {
                CalibrationModel c = new CalibrationModel();
                c.UserId = m.UserId;
                if (c.UserId != id)
                    continue;
                c.Name = m.Name;
                c.Description = m.Description;
                c.dateTime = m.dateTime;
                list.Add(c);
            }
            return list;
        }

        private string getIdByUsername(string username)
        {
            var context = new ApplicationDbContext(optionsApplication);
            var query = context.Users
                .Where(n => n.UserName == username)
                .Select(n => n.Id);
            return query.FirstOrDefault().ToString();
        }

        public List<UserModel> getListOfMembers()
        {
            var context = new ApplicationDbContext(optionsApplication);
            var query = context.Users;
            List <UserModel> list = new List<UserModel>();
            foreach (var m in query)
            {
                UserModel u = new UserModel();
                u.username = m.UserName;
                u.firstname = m.firstName;
                u.lastname = m.lastName;
                list.Add(u);
            }
            return list;
        }

        public bool InsertCalibration(CalibrationModel model, string username)
        {
            var context = new CustomDbContext(optionsCustom);
            CalibrationModel m = new CalibrationModel
            {
                UserId = getIdByUsername(username),
                Name =model.Name,
                Description=model.Description,
                dateTime= DateTime.Now.ToString()
            };
            context.Calibrations.Add(m);
            context.SaveChanges();
            return true;
        }

        public bool tryAuthenticate(string username, string password)
        {
            var context = new ApplicationDbContext(optionsApplication);
            var query = context.Users
                .Where(n => n.UserName == username)
                .Where(n => n.PasswordHash == password);
            return query.Any();

        }
        public void AddDefault()
            {
            var context = new ApplicationDbContext(optionsApplication);
            var query = context.Users
                .Where(n => n.UserName == "admin@admin.ru");
            if (!query.Any())
            {
                ApplicationUser admin = new ApplicationUser
                {
                    UserName = "admin@admin.ru",
                    PasswordHash = "qweR4-",
                    Email = "admin@admin.ru",
                    EmailConfirmed = true,
                    NormalizedEmail = "admin@admin.ru".ToUpper(),
                    NormalizedUserName = "admin@admin.ru".ToUpper()
                };
                context.Users.Add(admin);
                IdentityRole identityRole = new IdentityRole { Name = "admin" };
                var query1 = context.Roles.Add(identityRole);
                ApplicationUser user = new ApplicationUser 
                { 
                    UserName = "user@user.ru",
                    PasswordHash = "qweR4-",
                    Email = "user@user.ru",
                    EmailConfirmed=true,
                    NormalizedEmail = "user@user.ru".ToUpper(),
                    NormalizedUserName = "user@user.ru".ToUpper()
                };
                context.Users.Add(user);
                context.SaveChanges();
                string adminId = context.Users
                    .Where(u => u.UserName == "admin@admin.ru")
                    .Select(u => u.Id)
                    .FirstOrDefault()
                    .ToString();
                string roleId = context.Roles
                    .Where(r => r.Name == "admin")
                    .Select(r => r.Id)
                    .FirstOrDefault()
                    .ToString();
                var query2 = context.UserRoles.Add(new IdentityUserRole<string> { RoleId = roleId, UserId = adminId });
                context.SaveChanges();
            }
        }
    }
}
