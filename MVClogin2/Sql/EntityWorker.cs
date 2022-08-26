using Microsoft.AspNetCore.Hosting;
using MVClogin2.Models;
using System.Collections.Generic;

namespace MVClogin2.Sql
{
    public class EntityWorker : ISqlWorker
    {
        private IWebHostEnvironment _env;
        public EntityWorker(IWebHostEnvironment env)
        {
            _env = env;
        }
        public List<CalibrationModel> getCalibrations(string id)
        {
            throw new System.NotImplementedException();
        }

        public string getIdByUsername(string username)
        {
            throw new System.NotImplementedException();
        }

        public List<UserModel> getListOfMembers()
        {
            throw new System.NotImplementedException();
        }

        public bool InsertCalibration(CalibrationModel model)
        {
            throw new System.NotImplementedException();
        }

        public bool tryAuthenticate(string username, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
