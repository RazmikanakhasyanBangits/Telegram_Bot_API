using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public class UserActivityHistory
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public long UserExternalId { get; set; }
    public string Bio { get; set; }
    public string Description { get; set; }
    public ICollection<ChatDetail> ChatDetails { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}
