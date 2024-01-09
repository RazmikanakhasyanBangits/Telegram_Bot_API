using Core.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Core.Services.Bot.Helper;

public class CommandSwitcher
{
    public static Dictionary<string, Func<long, string>> CommandDictionary { get; set; }
    public CommandSwitcher(IBankService bankService)
    {
        CommandDictionary = new Dictionary<string, Func<long, string>>
        {
            {"/all", x=> bankService.GetAll() },
            {"/allBest", x=> bankService.GetAllBest() },
            {"/available", x=> bankService.GetAvailable()},
        };
    }
}
