namespace CacheManager.Serialization.NetJSON.Tests
{
    using System;
    using AutoFixture;
    using Core;
    using NUnit.Framework;
    using Shouldly;

    public class SerializerTests
    {
        private ICacheManager<TestObject> sut;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .WithRedisConfiguration("redis", "localhost:6379")
                .WithNetJSONSerializer()
                .WithRedisBackplane("redis")
                .WithRedisCacheHandle("redis")
                .Build();

            this.sut = CacheFactory.FromConfiguration<TestObject>(config);
        }

        [Test]
        public void When_told_to_cache_should_return_correct_value()
        {
            // arrange
            var sub = new Fixture()
                .Build<SubObject>()
                .Create();

            sub.Enum = TestEnum.Value2;
            
            var value = new Fixture()
                .Build<TestObject>()
                .With(x => x.SubObject, sub)
                .Create();

            this.sut.Add("test", value);

            // act
            var cachedValue = this.sut.Get("test");

            // assert
            cachedValue.ShouldBeSameAs(cachedValue);
        }
    }

    public class TestObject
    {
        public string String { get; set; }
        
        public string AnotherString { get; set; }
        
        public int AnInt { get; set; }
        
        public double Double { get; set; }
        
        public SubObject SubObject { get; set; }
    }

    public class SubObject
    {
        public string String { get; set; }
        
        public DateTime DateTime { get; set; }
        
        public bool Bool { get; set; }
        
        public TestEnum Enum { get; set; }
    }

    public enum TestEnum
    {
        Value1,
        Value2
    }
}