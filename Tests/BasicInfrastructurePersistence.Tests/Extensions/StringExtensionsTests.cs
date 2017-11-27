using System;
using BasicInfrastructureExtensions.Extensions;
using Xunit;
using Xunit2.Should;

namespace BasicInfrastructurePersistence.Tests.Extensions
{

    public class StringExtensionTests
    {
        [Theory]
        [InlineData("CamelHump", "camelhump")]
        [InlineData("CamelHump", "camelhuMP")]
        [InlineData("CamelHump", "CAMELHUMP")]
        [InlineData("CamelHump", "CamelHump")]
        public void ShouldEqualsIgnoreCase(string word, string value)
        {
            word.EqualsIgnoreCase(value).ShouldBeTrue();
        }

        [Theory]
        [InlineData("CamelHump", "CamelHum")]
        [InlineData("CamelHump", "CamelHump.")]
        [InlineData("CamelHump", "CamellHump")]
        public void ShouldNotEqualsIgnoreCase(string word, string value)
        {
            word.EqualsIgnoreCase(value).ShouldBeFalse();
        }

        [Theory]
        [InlineData("CamelHump", "camel")]
        [InlineData("CamelHump", "camelhum")]
        [InlineData("CamelHump", "CAMEL")]
        [InlineData("CamelHump", "CamELhuMp")]
        public void ShouldContainsIgnoreCase(string word, string value)
        {
            word.ContainsIgnoreCase(value).ShouldBeTrue();
        }

        [Theory]
        [InlineData("CamelHump", "Cem")]
        [InlineData("CamelHump", "CamelHump.")]
        [InlineData("CamelHump", "CamelHe")]
        [InlineData("CamelHump", "ump.")]
        public void ShouldNotContainsIgnoreCase(string word, string value)
        {
            word.ContainsIgnoreCase(value).ShouldBeFalse();
        }

        [Theory]
        [InlineData("CamelHump", "camel")]
        [InlineData("CamelHump", "camelhump")]
        [InlineData("CamelHump", "CAMEL")]
        [InlineData("CamelHump", "CamelHum")]
        public void ShouldStartsWithIgnoreCase(string word, string value)
        {
            word.StartsWithIgnoreCase(value).ShouldBeTrue();
        }

        [Theory]
        [InlineData("CamelHump", "Hum")]
        [InlineData("Camel", "CamelHump.")]
        [InlineData("Camel", "CamelHe")]
        [InlineData("Camel", "amelHump")]
        public void ShouldNotStartsWithIgnoreCase(string word, string value)
        {
            word.StartsWithIgnoreCase(value).ShouldBeFalse();
        }

        [Theory]
        [InlineData("Camel", "EL")]
        [InlineData("Camel", "el")]
        [InlineData("Camel", "MeL")]
        public void ShouldEndsWithIgnoreCase(string word, string value)
        {
            word.EndsWithIgnoreCase(value).ShouldBeTrue();
        }

        [Theory]
        [InlineData("Camel", "ca")]
        [InlineData("Camel", "ela")]
        [InlineData("Camel", "eel")]
        [InlineData("Camel", "cMEL")]
        public void ShouldNotEndsWithIgnoreCase(string word, string value)
        {
            word.EndsWithIgnoreCase(value).ShouldBeFalse();
        }

        [Theory]
        [InlineData("10", 10)]
        [InlineData("-98", -98)]
        public void MustConvertToInt(string toConvert, int expected)
        {
            var newInt = toConvert.ToInt();
            Assert.IsType<int>(newInt);
            newInt.ShouldBe(expected);
        }

        [Theory]
        [InlineData("1.5")]
        [InlineData("1,5")]
        [InlineData("1a")]
        [InlineData("NaN")]
        public void MustThrowFormatExceptionWhenConvertingToInt(string toConvert)
        {
            Assert.Throws<FormatException>(() => toConvert.ToInt());
        }

        [Theory]
        [InlineData("1.5", 0)]
        [InlineData("1,5", 0)]
        [InlineData("1a", 1)]
        [InlineData("NaN", -100)]
        public void MustReturnDefaultValueWhenConvertingToInt(string toConvert, int value)
        {
            toConvert.ToInt(value).ShouldBe(value);
        }


        [Theory]
        [InlineData("-10", -10)]
        [InlineData("-98,3", -98.3)]
        [InlineData("1,8", 1.8)]
        public void MustConvertToDouble(string toConvert, double expected)
        {
            var newDouble = toConvert.ToDouble();
            Assert.IsType<double>(newDouble);
            newDouble.ShouldBe(expected);
        }


        [Theory]
        [InlineData("1a")]
        [InlineData("NaN")]
        public void MustThrowFormatExceptionWhenConvertingToDouble(string toConvert)
        {
            Assert.Throws<FormatException>(() => toConvert.ToDouble());
        }

        [Theory]
        [InlineData("NaN")]
        public void ShouldBeNanOrInfinity(string toConvert)
        {
            double.Parse(toConvert).IsNanOrInfinity().ShouldBeTrue();
        }

        [Theory]
        [InlineData("1")]
        [InlineData("-1123")]
        public void ShouldNotBeNanOrInfinity(string toConvert)
        {
            double.Parse(toConvert).IsNanOrInfinity().ShouldBeFalse();
        }

        [Theory]
        [InlineData("1a", 1)]
        [InlineData("NaN", -100)]
        public void MustReturnDefaultValueWhenConvertingToDouble(string toConvert, double value)
        {
            toConvert.ToDouble(value).ShouldBe(value);
        }

        [Fact]
        public void TestarToDouble()
        {
            var newDouble = "1,63".ToDouble(null);
            Assert.IsType<double>(newDouble);
            newDouble.ShouldBe(1.63d);

            //Assert.Throws<FormatException>(() => "NaN".ToDouble(null));
            "NaN".ToDouble(1).ShouldBe(1d);
        }

        [Theory]
        [InlineData("10", 10L)]
        [InlineData("-98", -98L)]
        public void MustConvertToLong(string toConvert, long expected)
        {
            var newLong = toConvert.ToLong();
            Assert.IsType<long>(newLong);
            newLong.ShouldBe(expected);
        }

        [Theory]
        [InlineData("1.5")]
        [InlineData("1,5")]
        [InlineData("1a")]
        [InlineData("NaN")]
        public void MustThrowFormatExceptionWhenConvertingToLong(string toConvert)
        {
            Assert.Throws<FormatException>(() => toConvert.ToLong());
        }

        [Theory]
        [InlineData("1.5", 0L)]
        [InlineData("1,5", 0L)]
        [InlineData("1a", 1L)]
        [InlineData("NaN", -100L)]
        public void MustReturnDefaultValueWhenConvertingToLong(string toConvert, long value)
        {
            toConvert.ToLong(value).ShouldBe(value);
        }

        [Theory]
        [InlineData("2017-01-01", 2017, 01, 01)]
        [InlineData("2017-01-01T13:44:59", 2017, 1, 1, 13, 44, 59)]
        [InlineData("05/01/2017", 2017, 01, 05)]
        public void MustConvertToDateTimeTicks(string toConvert, int year, int month, int day, int hour = 0,
            int minute = 0, int second = 0)
        {
            var dateTimeTicks = toConvert.ToDateTimeTicks();
            Assert.IsType<long>(dateTimeTicks);
            var expectedDate = new DateTime(year, month, day, hour, minute, second);
            dateTimeTicks.ShouldBe(expectedDate.Ticks);
        }

        [Theory]
        [InlineData("2017-01-01", 2017, 01, 01)]
        [InlineData("2017-01-01T13:44:59", 2017, 1, 1, 13, 44, 59)]
        [InlineData("05/01/2017", 2017, 01, 05)]
        public void MustConvertToDateTime(string toConvert, int year, int month, int day, int hour = 0, int minute = 0,
            int second = 0)
        {
            var dateTime = toConvert.ToDateTime();
            Assert.IsType<DateTime>(dateTime);
            var expectedDate = new DateTime(year, month, day, hour, minute, second);
            dateTime.ShouldBe(expectedDate);
        }

        [Theory]
        [InlineData("1a")]
        [InlineData("NaN")]
        public void MustThrowFormatExceptionWhenConvertingToDateTimeTicks(string toConvert)
        {
            Assert.Throws<FormatException>(() => toConvert.ToDateTimeTicks());
        }

        [Theory]
        [InlineData("separatedWords", "separated Words")]
        [InlineData("SeparatedWords", "Separated Words")]
        [InlineData("separated_Words", "separated Words")]
        [InlineData("separated_words", "separated words")]
        [InlineData("separated_wordsAnd-More words", "separated words And More words")]
        [InlineData("another-word", "another word")]
        [InlineData("two words", "two words")]
        public void MustConvertToSeparateWords(string value, string expected)
        {
            value.ToSeparatedWords().ShouldBeEqual(expected);
        }

        [Theory]
        [InlineData("ONEWORD")]
        [InlineData("oneword")]
        [InlineData("onew*ord")]
        [InlineData("onew/ord")]
        public void MustNotConvertToSeparateWords(string value)
        {
            value.ToSeparatedWords().ShouldBeEqual(value);
        }
    }
}
