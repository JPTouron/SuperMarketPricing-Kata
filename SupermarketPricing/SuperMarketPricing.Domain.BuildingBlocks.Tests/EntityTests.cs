using AutoFixture;
using AutoFixture.AutoMoq;
using SuperMarketPricing.Domain.BuildingBlocks.Base;
using Xunit;

namespace SuperMarketPricing.Domain.BuildingBlocks.Tests
{
    public class EntityTests
    {
        private MyEntity e1;

        private MyEntity e2;

        private IFixture fixture;

        public EntityTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
        }

        [Fact]
        public void Create_ContainsSpecifiedId()
        {
            e1 = new MyEntity(10);
            Assert.Equal(10, e1.Id);
        }

        [Fact]
        public void WhenEntityIsNotObjectAndIdsAreSame_EqualsIsFalse()
        {
            CreateSameEntities();
            var o = new object();
            Assert.False(e1.Equals(o));
        }

        [Fact]
        public void WhenEntityIsObjectAndIdsAreSame_EqualsIsTrue()
        {
            CreateSameEntities();
            var o = (object)e2;
            Assert.True(e1.Equals(o));
        }

        [Fact]
        public void WhenIdsAreDifferent_EqualsIsFalse()
        {
            CreateDifferentEntities();

            Assert.False(e1.Equals(e2));
            Assert.False(e2.Equals(e1));
        }

        [Fact]
        public void WhenIdsAreDifferent_HashCodeIsDifferent()
        {
            CreateDifferentEntities();
            Assert.NotEqual(e1.GetHashCode(), e2.GetHashCode());
        }

        [Fact]
        public void WhenIdsAreSame_EqualsIsTrue()
        {
            CreateSameEntities();
            Assert.True(e1.Equals(e2));
            Assert.True(e2.Equals(e1));
        }

        [Fact]
        public void WhenIdsAreSame_HashCodeIsSame()
        {
            CreateSameEntities();
            Assert.Equal(e1.GetHashCode(), e2.GetHashCode());
        }

        private void CreateDifferentEntities()
        {
            e1 = new MyEntity(10);
            e2 = new MyEntity(11);
        }

        private void CreateSameEntities()
        {
            e1 = new MyEntity(10);
            e2 = new MyEntity(10);
        }

        public class MyEntity : Entity<int>
        {
            public MyEntity(int id) : base(id)
            {
            }
        }
    }
}