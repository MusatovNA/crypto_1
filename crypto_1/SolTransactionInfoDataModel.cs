using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_1
{
    internal class SolTransactionInfoDataModel
    {

        public class SolTransInfo
        {
            public int block_time { get; set; }
            public int id { get; set; }
            public string jsonrpc { get; set; }
            public Meta meta { get; set; }
            public Result result { get; set; }
            public int slot { get; set; }
            public Transaction1 transaction { get; set; }
            public string version { get; set; }
        }

        public class Meta
        {
            public int compute_units_consumed { get; set; }
            public object err { get; set; }
            public int fee { get; set; }
            public object[] inner_instructions { get; set; }
            public string[] log_messages { get; set; }
            public long[] post_balances { get; set; }
            public object[] post_token_balances { get; set; }
            public long[] pre_balances { get; set; }
            public object[] pre_token_balances { get; set; }
            public object[] rewards { get; set; }
            public Status status { get; set; }
        }

        public class Status
        {
            public object _ok { get; set; }
        }

        public class Result
        {
            public int block_time { get; set; }
            public Meta1 meta { get; set; }
            public int slot { get; set; }
            public Transaction transaction { get; set; }
            public string version { get; set; }
        }

        public class Meta1
        {
            public int compute_units_consumed { get; set; }
            public object err { get; set; }
            public int fee { get; set; }
            public object[] inner_instructions { get; set; }
            public string[] log_messages { get; set; }
            public long[] post_balances { get; set; }
            public object[] post_token_balances { get; set; }
            public long[] pre_balances { get; set; }
            public object[] pre_token_balances { get; set; }
            public object[] rewards { get; set; }
            public Status1 status { get; set; }
        }

        public class Status1
        {
            public object _ok { get; set; }
        }

        public class Transaction
        {
            public Message message { get; set; }
            public string[] signatures { get; set; }
        }

        public class Message
        {
            public Account_Keys[] account_keys { get; set; }
            public Instruction[] instructions { get; set; }
            public string recent_blockhash { get; set; }
        }

        public class Account_Keys
        {
            public string pubkey { get; set; }
            public bool signer { get; set; }
            public string source { get; set; }
            public bool writable { get; set; }
        }

        public class Instruction
        {
            public Parsed parsed { get; set; }
            public string program { get; set; }
            public string program_id { get; set; }
            public object stack_height { get; set; }
        }

        public class Parsed
        {
            public Info info { get; set; }
            public string type { get; set; }
        }

        public class Info
        {
            public string account { get; set; }
            public string _base { get; set; }
            public string owner { get; set; }
            public string seed { get; set; }
            public int space { get; set; }
            public long lamports { get; set; }
            public string new_split_account { get; set; }
            public string stake_account { get; set; }
            public string stake_authority { get; set; }
        }

        public class Transaction1
        {
            public Message1 message { get; set; }
            public string[] signatures { get; set; }
        }

        public class Message1
        {
            public Account_Keys1[] account_keys { get; set; }
            public Instruction1[] instructions { get; set; }
            public string recent_blockhash { get; set; }
        }

        public class Account_Keys1
        {
            public string pubkey { get; set; }
            public bool signer { get; set; }
            public string source { get; set; }
            public bool writable { get; set; }
        }

        public class Instruction1
        {
            public Parsed1 parsed { get; set; }
            public string program { get; set; }
            public string program_id { get; set; }
            public object stack_height { get; set; }
        }

        public class Parsed1
        {
            public Info1 info { get; set; }
            public string type { get; set; }
        }

        public class Info1
        {
            public string account { get; set; }
            public string _base { get; set; }
            public string owner { get; set; }
            public string seed { get; set; }
            public int space { get; set; }
            public long lamports { get; set; }
            public string new_split_account { get; set; }
            public string stake_account { get; set; }
            public string stake_authority { get; set; }
        }

    }
}
