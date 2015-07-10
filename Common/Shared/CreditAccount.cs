using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.Shared
{
    public sealed class CreditAccount : IBinarySerializable
    {
        public enum CreditType
        {
            Universal = 0,

            MaxCreditTypes
        }

        private ulong[] credits { get; set; }

        public CreditAccount()
        {
            credits = new ulong[(int)CreditAccount.CreditType.MaxCreditTypes];
        }

        public ulong this[CreditType type]
        {
            get
            {
                return credits[(int)type];
            }

            set
            {
                credits[(int)type] = value;
            }
        }

        public bool Draw(CreditType type, ulong value)
        {
            if (this[type] < value)
                return false;

            this[type] -= value;

            return true;
        }

        public void Add(CreditType type, ulong value)
        {
            // MH: add overflow check

            this[type] += value;
        }

        public void Write(System.IO.BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Read(System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
