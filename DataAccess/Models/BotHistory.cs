using System;


namespace DataAccess.Models
{
    public partial class BotHistory
    {
        public int Id { get; set; }
        public string ChatId { get; set; }
        public string CommandText { get; set; }
        public string Reply { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
