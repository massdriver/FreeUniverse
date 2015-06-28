using System;
using System.Collections.Generic;
using System.Text;

namespace FreeUniverse.Common.Market
{
    public enum MarketContractType
    {
        FixedSell,
        FixedBuy
    }

    public class MarketContract
    {
        public MarketContractType type { get; set; }
        public MarketItem item { get; set; }
        public DateTime expireDate { get; set; }
    }
}
