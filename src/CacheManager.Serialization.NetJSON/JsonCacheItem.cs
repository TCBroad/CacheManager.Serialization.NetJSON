namespace CacheManager.Serialization.NetJSON
{
    using System;
    using Core;

    internal class JsonCacheItem
    {
        private DateTime CreatedUtc { get; set; }

        private ExpirationMode ExpirationMode { get; set; }

        private TimeSpan ExpirationTimeout { get; set; }

        private string Key { get; set; }

        private DateTime LastAccessedUtc { get; set; }

        private string Region { get; set; }

        public byte[] Value { get; private set; }
        
        public static JsonCacheItem FromCacheItem<TCacheValue>(CacheItem<TCacheValue> item, byte[] value)
            => new JsonCacheItem
            {
                CreatedUtc = item.CreatedUtc,
                ExpirationMode = item.ExpirationMode,
                ExpirationTimeout = item.ExpirationTimeout,
                Key = item.Key,
                LastAccessedUtc = item.LastAccessedUtc,
                Region = item.Region,
                Value = value
            };

        public CacheItem<T> ToCacheItem<T>(object value)
        {
            var item = string.IsNullOrWhiteSpace(this.Region) ?
                new CacheItem<T>(this.Key, (T)value, this.ExpirationMode, this.ExpirationTimeout) :
                new CacheItem<T>(this.Key, this.Region, (T)value, this.ExpirationMode, this.ExpirationTimeout);

            item.LastAccessedUtc = this.LastAccessedUtc;

            return item.WithCreated(this.CreatedUtc);
        }
    }
}