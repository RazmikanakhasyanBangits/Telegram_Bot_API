using System;

namespace Service.Model.Models.BotHistory;

public class BotHistoryModel
{
    public int ID { get; set; }
    public string ChatId { get; set; }
    public string CommandText { get; set; }
    public string Reply { get; set; }
    public string UserId { get; set; }
    public DateTime Date { get; set; }
}
