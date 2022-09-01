using Microsoft.AspNetCore.Mvc.RazorPages;
using MVClogin2.Models;
using MVClogin2.Sql;
using System.Collections.Generic;

namespace MVClogin2.Areas.Chat.Pages
{
    public class ChatPageModel : PageModel
    {
        private ISqlWorker sqlWorker;
        public List<UserMessage> list;
        public ChatPageModel(ISqlWorker sqlWorker)
        {
            this.sqlWorker = sqlWorker;
        }
        public void OnGet()
        {
            list = sqlWorker.LoadMessageFromDb();
        }
    }
}
