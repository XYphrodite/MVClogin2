using MVClogin2.Models;
using System.Collections.Generic;

namespace MVClogin2.Sql
{
    public interface ISqlWorker
    {
        List<CalibrationModel> getCalibrations(string id);
        List<UserModel> getListOfMembers();
        string getIdByUsername(string username);
        bool InsertCalibration( CalibrationModel model, string username);
        bool tryAuthenticate(string username, string password);
        void AddDefault();
        void SaveMessageToDb(UserMessage message);
        List<UserMessage> LoadMessageFromDb();

    }
}