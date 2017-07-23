using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundManager.BL;
using FundManager.Repositories.DTO;

namespace FundManager.Tests
{
    [TestFixture]
    public class BuilderFactoryTest
    {
        [Test]
        public void GetBuilder_PassNullAsParameter_ThrowException()
        {
            // Arrange
            var factory = new BuilderFactory();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => factory.GetBuilder(null));
        }

        [Test]
        public void GetBuilder_PassBondDto_ReturnsBondBuilder()
        {
            // Arrange
            var factory = new BuilderFactory();

            //Act
            var builder = factory.GetBuilder(new BondDTO());
            
            // Assert
            Assert.That(builder, Is.InstanceOf(typeof(BondBuilder)));
        }

        [Test]
        public void GetBuilder_PassEquityDto_ReturnsBondBuilder()
        {
            // Arrange
            var factory = new BuilderFactory();

            //Act
            var builder = factory.GetBuilder(new EquityDTO());

            // Assert
            Assert.That(builder, Is.InstanceOf(typeof(EquityBuilder)));
        }
    }
}
