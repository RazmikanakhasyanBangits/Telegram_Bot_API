using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class BotHistory
    {


        public int ID { get; set; }
        public string ChatId { get; set; }
        public string CommandText { get; set; }
        public string Reply { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }

    }
}
