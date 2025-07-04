using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using System.Collections.Generic;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.ValueObjects
{
    public class PartCode 
    {
        public string Value { get; private set; }

        public PartCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Part code cannot be empty");

            Value = code.Trim().ToUpper();
        }

        protected IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
} 