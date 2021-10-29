using DiseaseMapMVC.Models.Cache;
using DiseaseMongoModel;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiseaseMapMVC.UnitTests
{
    [TestFixture]
    public class CacheRepositoryTests
    {
        private FakeEntity _fakeEntity = new FakeEntity { Id = Guid.NewGuid().ToString(), Name = "Fake" };

        private IMemoryCacheWrapper CreateMemoryCacheWrapper()
        {
            var memoryCacheWrapper = new Mock<IMemoryCacheWrapper>().SetupAllProperties();
            memoryCacheWrapper.Setup(m => m.Set(It.IsAny<object>(), It.IsAny<BaseEntity>(), new MemoryCacheEntryOptions()));

            var memoryCache = new Mock<IMemoryCache>();
            memoryCacheWrapper.Object._MemoryCache = memoryCache.Object;

            return memoryCacheWrapper.Object;
        }

        [Test]
        public void Create_IfAddInRepositorySetInCache_ReturnsTrue()
        {
            var memoryCacheWrapper = CreateMemoryCacheWrapper();

            var repository = new Mock<IRepository<BaseEntity>>();
            repository.Setup(r => r.Create(It.IsAny<BaseEntity>())).Returns(true);

            var cacheRepository = new CacheRepository<BaseEntity>(memoryCacheWrapper, repository.Object);
            var isCreated = cacheRepository.Create(_fakeEntity);

            Assert.IsTrue(isCreated);
        }


        [Test]
        public void Update_IfUpdateInRepositorySetInCache_ReturnsTrue()
        {
            var memoryCacheWrapper = CreateMemoryCacheWrapper();

            var repository = new Mock<IRepository<BaseEntity>>();
            repository.Setup(r => r.Update(It.IsAny<BaseEntity>())).Returns(true);

            var cacheRepository = new CacheRepository<BaseEntity>(memoryCacheWrapper, repository.Object);
            var isUpdated = cacheRepository.Update(_fakeEntity);

            Assert.IsTrue(isUpdated);
        }

        [Test]
        public void Delete_IfRemoveInRepositoryRemoveInCache_ReturnsTrue()
        {
            var memoryCacheWrapper = CreateMemoryCacheWrapper();

            var repository = new Mock<IRepository<BaseEntity>>();
            repository.Setup(r => r.Delete(It.IsAny<BaseEntity>())).Returns(true);

            var cacheRepository = new CacheRepository<BaseEntity>(memoryCacheWrapper, repository.Object);
            var isDeleted = cacheRepository.Delete(_fakeEntity);

            Assert.IsTrue(isDeleted);
        }
    }
}
