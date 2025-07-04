using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using System.Collections.Generic;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        public Money(decimal amount, string currency = "USD")
        {
            Amount = amount;
            Currency = currency;
        }

        protected IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
} 