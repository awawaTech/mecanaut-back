using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using System.Collections.Generic;

namespace AwawaTech.Mecanaut.API.InventoryManagement.Domain.Model.ValueObjects
{
    public class StockLevel
    {
        public int Current { get; private set; }
        public int Minimum { get; private set; }
        public StockStatus Status { get; private set; }

        public StockLevel(int current, int minimum)
        {
            Current = current;
            Minimum = minimum;
            Status = DetermineStatus(current, minimum);
        }

        public StockLevel Update(int newQuantity)
        {
            return new StockLevel(newQuantity, this.Minimum);
        }

        public StockLevel UpdateMinimum(int newMinimum)
        {
            return new StockLevel(this.Current, newMinimum);
        }

        private static StockStatus DetermineStatus(int current, int minimum)
        {
            if (current <= 0) return StockStatus.OutOfStock;
            if (current < minimum) return StockStatus.Low;
            return StockStatus.Normal;
        }

        protected IEnumerable<object> GetEqualityComponents()
        {
            yield return Current;
            yield return Minimum;
            yield return Status;
        }
    }

    public enum StockStatus
    {
        Normal,
        Low,
        OutOfStock
    }
} 