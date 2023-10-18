using System;

namespace Shared.Model
{
    public class CurrenciesConvertDetails
    {
       
        public string To { get; set; }

        public decimal Value { get; set; }

    }

    public class FromToConverter : CurrenciesConvertDetails
    {
        public string From { get; set; }
        public string BestBank { get; set; }
        
        public DateTime LastUpdated { get; set; }
    }
}
