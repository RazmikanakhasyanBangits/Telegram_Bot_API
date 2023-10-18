using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class Bank
    {
        public int ID { get; set; }
        public string BankName { get; set; }
        public string BankURL { get; set; }

        //public ICollection<Rate> Rates { get; set; }
    }
}
