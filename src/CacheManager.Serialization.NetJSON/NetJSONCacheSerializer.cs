namespace CacheManager.Serialization.NetJSON
{
	using System;
	using System.Text;
	using Core;
	using Core.Internal;
	using global::NetJSON;
	using static Core.Utility.Guard;

    /// <summary>
    /// Implements the <see cref="ICacheSerializer"/> contract using <c>NetJSON</c>.
    /// </summary>
    public class NetJSONCacheSerializer : ICacheSerializer
    {
        private readonly NetJSONSettings settings;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="NetJSONCacheSerializer"/> class.
        /// </summary>
        public NetJSONCacheSerializer() : this(NetJSONSettings.CurrentSettings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetJSONCacheSerializer"/> class
        /// with custom settings for serialization/deserialization 
        /// </summary>
        /// <param name="settings">The settings to used during serialization/deserialization.</param>	  
        public NetJSONCacheSerializer(NetJSONSettings settings)
        {
            NotNull(settings, nameof(settings));
            this.settings = settings;
        }
        
        /// <inheritdoc/>
        public virtual byte[] Serialize<T>(T value)
        {
            var stringValue = NetJSON.Serialize(value, this.settings);
            
            return Encoding.UTF8.GetBytes(stringValue);
        }

        /// <inheritdoc/>
        public virtual object Deserialize(byte[] data, Type target)
        {
            var stringValue = Encoding.UTF8.GetString(data, 0, data.Length);

            return NetJSON.Deserialize(target ,stringValue, this.settings);
        }

        /// <inheritdoc/>
        public byte[] SerializeCacheItem<T>(CacheItem<T> value)
        {
            NotNull(value, nameof(value));
            var jsonValue = this.Serialize(value.Value);
            var jsonItem = JsonCacheItem.FromCacheItem(value, jsonValue);

            return this.Serialize(jsonItem);
        }

        /// <inheritdoc/>
        public CacheItem<T> DeserializeCacheItem<T>(byte[] value, Type valueType)
        {
            var jsonItem = (JsonCacheItem)this.Deserialize(value, typeof(JsonCacheItem));
            EnsureNotNull(jsonItem, "Could not deserialize cache item");

            var deserializedValue = this.Deserialize(jsonItem.Value, valueType);

            return jsonItem.ToCacheItem<T>(deserializedValue);
        }
    }
}