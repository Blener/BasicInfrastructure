using System.Collections.Generic;
using BasicInfrastructureExtensions.Extensions;
using BasicInfrastructureExtensions.Helpers;
using BasicInfrastructurePersistence.Tests.TestEntities;
using Xunit;
using Xunit2.Should;

namespace BasicInfrastructurePersistence.Tests.Extensions
{
    public class EnumExtensionsTests
    {
        [Theory]
        [InlineData("Number One", TestEnum.FirstItem)]
        [InlineData("SecondItem", TestEnum.SecondItem)]
        [InlineData("Third Item", TestEnum.Third_Item)]
        [InlineData("Fourth-Item", TestEnum.FourthItem)]
        [InlineData("Fifth_Item", TestEnum.FifthItem)]
        //TODO Checar o funcionamento do localized description
        //[InlineData("This is a Localized Description", TestEnum.SixthItem)]
        public void MustGetDescription(string expected, TestEnum value)
        {
            value.GetDescription().ShouldBe(expected);
        }

        [Fact]
        public void MustReturnListWithDescription()
        {
            EnumHelper.EnumToList<TestEnum>().ShouldBe(new List<string>()
            {
                "Number One",
                "SecondItem", 
                "Third Item", 
                "Fourth-Item",
                "Fifth_Item"
            });
        }
        [Fact]
        public void MustReturnListWithToString()
        {
            EnumHelper.EnumToList<TestEnum>(false).ShouldBe(new List<string>()
            {
                "FirstItem",
                "SecondItem", 
                "Third_Item",
                "FourthItem",
                "FifthItem"
            });
        }
    }
}
