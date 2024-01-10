using System.Collections.Generic;

namespace Shared.Models.Static;

public static class BanksDictionary
{
    public static Dictionary<string, int> Banks = new()
    {
        { "AmeriaBankDataScrapper",1},
        { "EvocaBankDataScrapper",2},
        { "AcbaBankDataScrapper",3},
        { "InecoBankDataScrapper",4},
        { "UniBankDataScrapper",5},
    };
}
