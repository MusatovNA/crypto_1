﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_1
{
    public class ExchangeRateClass
    {
        public class ExchangeRateInf
        {
            public Status status { get; set; }
            public Datum[] data { get; set; }
        }

        public class Status
        {
            public DateTime timestamp { get; set; }
            public int error_code { get; set; }
            public object error_message { get; set; }
            public int elapsed { get; set; }
            public int credit_count { get; set; }
            public object notice { get; set; }
            public int total_count { get; set; }
        }

        public class Datum
        {
            public int id { get; set; }
            public string name { get; set; }
            public string symbol { get; set; }
            public string slug { get; set; }
            public int num_market_pairs { get; set; }
            public DateTime date_added { get; set; }
            public string[] tags { get; set; }

            public object tvl_ratio { get; set; }
            public DateTime last_updated { get; set; }
            public Quote quote { get; set; }
        }

        public class Quote
        {
            public RUB RUB { get; set; }
            public USD USD { get; set; }

        }

        public class RUB
        {
            public decimal price { get; set; }
            public float volume_24h { get; set; }
            public float volume_change_24h { get; set; }
            public float percent_change_1h { get; set; }
            public float percent_change_24h { get; set; }
            public float percent_change_7d { get; set; }
            public float percent_change_30d { get; set; }
            public float percent_change_60d { get; set; }
            public float percent_change_90d { get; set; }
            public float market_cap { get; set; }
            public float market_cap_dominance { get; set; }
            public float fully_diluted_market_cap { get; set; }
            public object tvl { get; set; }
            public DateTime last_updated { get; set; }
        }
        public class USD
        {
            public decimal price { get; set; }
            public float volume_24h { get; set; }
            public float volume_change_24h { get; set; }
            public float percent_change_1h { get; set; }
            public float percent_change_24h { get; set; }
            public float percent_change_7d { get; set; }
            public float percent_change_30d { get; set; }
            public float percent_change_60d { get; set; }
            public float percent_change_90d { get; set; }
            public float market_cap { get; set; }
            public float market_cap_dominance { get; set; }
            public float fully_diluted_market_cap { get; set; }
            public object tvl { get; set; }
            public DateTime last_updated { get; set; }
        }

    }
}






