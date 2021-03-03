using SuperMarketPricing.Domain.BuildingBlocks.Base;
using System;

namespace SuperMarketPricing.Domain.BuildingBlocks
{
    public sealed class Maybe<T> : ValueObject<Maybe<T>>
    {
        public Maybe()
        {
            HasItem = false;
        }

        public Maybe(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            HasItem = true;
            Item = item;
        }

        private bool HasItem { get; }

        private T Item { get; }

        public override int GetHashCode()
        {
            return HasItem ? Item.GetHashCode() : 0;
        }

        public T GetValueOrFallback(T fallbackValue)
        {
            if (fallbackValue == null)
                throw new ArgumentNullException(nameof(fallbackValue));

            if (HasItem)
                return Item;
            else
                return fallbackValue;
        }

        public Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            if (HasItem)
                return new Maybe<TResult>(selector(Item));
            else
                return new Maybe<TResult>();
        }

        protected override bool InternalEquals(Maybe<T> other)
        {
            return Equals(Item, other.Item);
        }
    }
}