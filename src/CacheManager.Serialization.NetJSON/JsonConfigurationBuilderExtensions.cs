namespace CacheManager.Serialization.NetJSON
{
    using Core;
    
    using global::NetJSON;
    using static Core.Utility.Guard;

    /// <summary>
    /// Extensions for the configuration builder for the <code>NetJSON</code> based <see cref="CacheManager.Core.Internal.ICacheSerializer"/>.
    /// </summary>
    public static class JsonConfigurationBuilderExtensions
    {
        /// <summary>
        /// Configures the cache manager to use the <code>NetJSON</code> based cache serializer.
        /// </summary>
        /// <param name="part">The configuration part.</param>
        /// <returns>The builder instance.</returns>
        public static ConfigurationBuilderCachePart WithNetJSONSerializer(this ConfigurationBuilderCachePart part)
        {
            NotNull(part, nameof(part));

            return part.WithSerializer(typeof(NetJSONCacheSerializer));
        }

        /// <summary>
        /// Configures the cache manager to use the <code>NetJSON</code> based cache serializer.
        /// </summary>
        /// <param name="part">The configuration part.</param>
        /// <param name="settings">The settings to be used during serialization/deserialization.</param>	
        /// <returns>The builder instance.</returns>
        public static ConfigurationBuilderCachePart WithNetJSONSerializer(this ConfigurationBuilderCachePart part, NetJSONSettings settings)
        {
            NotNull(part, nameof(part));

            return part.WithSerializer(typeof(NetJSONCacheSerializer), settings);
        }

        /// <summary>
        /// Configures the cache manager to use the <code>NetJSON</code> based cache serializer with compression.
        /// </summary>
        /// <param name="part">The configuration part.</param>
        /// <returns>The builder instance.</returns>
        public static ConfigurationBuilderCachePart WithGzNetJSONSerializer(this ConfigurationBuilderCachePart part)
        {
            NotNull(part, nameof(part));

            return part.WithSerializer(typeof(GzNetJSONCacheSerializer));
        }

        /// <summary>
        /// Configures the cache manager to use the <code>NetJSON</code> based cache serializer with compression.
        /// </summary>
        /// <param name="part">The configuration part.</param>
        /// <param name="settings">The settings to be used during serialization/deserialization.</param>  
        /// <returns>The builder instance.</returns>
        public static ConfigurationBuilderCachePart WithNetJSONJsonSerializer(this ConfigurationBuilderCachePart part, NetJSONSettings settings)
        {
            NotNull(part, nameof(part));

            return part.WithSerializer(typeof(GzNetJSONCacheSerializer), settings);
        }
    }
}