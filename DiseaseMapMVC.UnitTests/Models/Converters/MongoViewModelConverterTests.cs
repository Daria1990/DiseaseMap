using NUnit.Framework;
using DiseaseMapMVC.Models.Converter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiseaseMapMVC.UnitTests
{
    public abstract class MongoViewModelConverterTests<TEntity, TViewModel>
    {
        protected abstract IMongoViewModelConverter<TEntity, TViewModel> GetConverter();

        [Test]
        public void ConvertViewModelToEntity_EntityIsNull_ThrowException()
        {
            var converter = GetConverter();

            Assert.Catch<ArgumentNullException>(() => converter.Convert(default(TViewModel), default(TEntity)));
        }
    }
}
