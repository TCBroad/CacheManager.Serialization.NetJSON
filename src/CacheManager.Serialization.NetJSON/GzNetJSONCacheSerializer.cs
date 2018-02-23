namespace CacheManager.Serialization.NetJSON
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using Core.Internal;
    using Core.Utility;
    
    using global::NetJSON;

    /// <summary>
    /// Implements the <see cref="ICacheSerializer"/> contract using <c>NetJSON</c> and the <see cref="GZipStream "/> loseless compression.
    /// </summary>
    public class GzNetJSONCacheSerializer : NetJSONCacheSerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GzNetJSONCacheSerializer"/> class.
        /// </summary>
        public GzNetJSONCacheSerializer() : base(NetJSONSettings.CurrentSettings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetJSONCacheSerializer"/> class
        /// with custom settings for serialization/deserialization 
        /// </summary>
        /// <param name="settings">The settings to used during serialization/deserialization.</param>	  
        public GzNetJSONCacheSerializer(NetJSONSettings settings) : base(settings)
        {
        }

        /// <inheritdoc/>
        public override object Deserialize(byte[] data, Type target)
        {
            var compressedData = this.Unzip(data);

            return base.Deserialize(compressedData, target);
        }

        /// <inheritdoc/>
        public override byte[] Serialize<T>(T value)
        {
            var data = base.Serialize(value);

            return this.Zip(data);
        }

        /// <summary>
        /// Compress the serialized <paramref name="data"/> using <see cref="GZipStream "/>.
        /// </summary>
        /// <param name="data">The data to compress.</param>
        /// <returns>The compressed data.</returns>
        protected virtual byte[] Zip(byte[] data)
        {
            Guard.NotNull(data, nameof(data));

            using (var bytesBuilder = new MemoryStream())
            {
                using (var gzWriter = new GZipStream(bytesBuilder, CompressionMode.Compress))
                {
                    gzWriter.Write(data, 0, data.Length);
                }

                return bytesBuilder.ToArray();
            }
        }

        /// <summary>
        /// Decompress the <paramref name="compressedData"/> into the base serialized data.
        /// </summary>
        /// <param name="compressedData">The data to be decompressed.</param>
        /// <returns>The uncompressed data.</returns>
        protected virtual byte[] Unzip(byte[] compressedData)
        {
            Guard.NotNull(compressedData, nameof(compressedData));

            using (var inputStream = new MemoryStream(compressedData))
            using (var gzReader = new GZipStream(inputStream, CompressionMode.Decompress))
            using (var bytesBuilder = new MemoryStream())
            {
                gzReader.CopyTo(bytesBuilder);
                return bytesBuilder.ToArray();
            }
        }
    }
}