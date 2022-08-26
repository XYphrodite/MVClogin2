using Microsoft.AspNetCore.Hosting;
using MVClogin2.Models;
using System.Collections.Generic;

namespace MVClogin2.Sql
{
    public interface ISqlWorker
    {
        List<CalibrationModel> getCalibrations(string id);
        string getIdByUsername(string username);
        List<UserModel> getListOfMembers();
        bool InsertCalibration( CalibrationModel model);
        bool tryAuthenticate(string username, string password);
    }
}