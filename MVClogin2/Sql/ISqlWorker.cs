using MVClogin2.Models;
using System.Collections.Generic;

namespace MVClogin2.Sql
{
    public interface ISqlWorker
    {
        List<CalibrationModel> getCalibrations(string id);
        List<UserModel> getListOfMembers();
        bool InsertCalibration( CalibrationModel model, string username);
        bool tryAuthenticate(string username, string password);
        void AddDefault();

    }
}