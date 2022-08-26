using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MVClogin2.Areas.Identity.Data;
using MVClogin2.Models;
using MVClogin2.Sql.Data;
using System.Collections.Generic;
using System.Linq;

namespace MVClogin2.Sql
{
    public class EntityWorker : ISqlWorker
    {
        private IWebHostEnvironment _env;
        private DbContextOptions<CustomDbContext> optionsCustom;
        private DbContextOptions<ApplicationDbContext> optionsApplication;
        private DbContextOptionsBuilder optionsBuilder;
        public EntityWorker(IWebHostEnvironment env)
        {
            _env = env;
            optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsApplication = new DbContextOptions<ApplicationDbContext>();
            optionsCustom = new DbContextOptions<CustomDbContext>();
            
        }
        public List<CalibrationModel> getCalibrations(string id)
        {
            throw new System.NotImplementedException();
        }

        public string getIdByUsername(string username)
        {
            ///and this
            var context = new ApplicationDbContext(optionsApplication);
            var id = context.Users
                .Where(n => n.UserName == username);
            return id.ToString();
        }

        public List<UserModel> getListOfMembers()
        {
            throw new System.NotImplementedException();
        }

        public bool InsertCalibration(CalibrationModel model)
        {
            ///this
            using CustomDbContext context = new CustomDbContext(optionsCustom);
            context.AddAsync(model);
            context.SaveChanges();
            return true;
        }

        public bool tryAuthenticate(string username, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
