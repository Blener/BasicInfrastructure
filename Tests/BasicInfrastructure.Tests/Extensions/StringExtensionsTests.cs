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

        [Theory]
        [InlineData("36.320.880/0001-20")]
        [InlineData("88.143.821/0001-28")]
        [InlineData("67.232.732/0001-88")]
        public void MustCnpjValidWithMask(string cnpjTest)
        {
            cnpjTest.IsCNPJ().ShouldBeTrue();
            cnpjTest.IsCpfOrCnpj().ShouldBeTrue();
        }

        [Theory]
        [InlineData("76748996000180")]
        [InlineData("80836586000168")]
        [InlineData("87738597000154")]
        public void MustCnpjValidJustNumbers(string cnpjTest)
        {
            cnpjTest.IsCNPJ().ShouldBeTrue();
            cnpjTest.IsCpfOrCnpj().ShouldBeTrue();
        }
        [Theory]
        [InlineData("36.320.880/0001-20")]
        [InlineData("88.143.821/0001-28")]
        [InlineData("67.232.732/0001-88")]
        [InlineData("")]
        public void MustFormatCnpj(string cnpj)
        {
            cnpj.ExtractNumbers().FormatCNPJ().ShouldBeEqual(cnpj);
        }

        [Theory]
        [InlineData("70.555.555/0001-93")]
        [InlineData("34.43.64/0001-64")]
        [InlineData("65.378.172@0001@49")]
        [InlineData("55.555.555/5555-55")]
        public void MustCnpjInvalidWithMask(string cnpjTest)
        {
            cnpjTest.IsCNPJ().ShouldBeFalse();
            cnpjTest.IsCpfOrCnpj().ShouldBeFalse();
        }
        [Theory]
        [InlineData("814547000178")]
        [InlineData("520555555787000119")]
        [InlineData("42693367999185")]
        [InlineData("55555555555")]
        public void MustCnpjInvalidJustNumbers(string cnpjTest)
        {
            cnpjTest.IsCNPJ().ShouldBeFalse();
            cnpjTest.IsCpfOrCnpj().ShouldBeFalse();
        }

        [Theory]
        [InlineData("482.496.596-94")]
        [InlineData("948.843.518-60")]
        [InlineData("689.173.690-06")]
        public void MustCpfValidWithMask(string cpfTest)
        {
            cpfTest.IsCPF().ShouldBeTrue();
            cpfTest.IsCpfOrCnpj().ShouldBeTrue();
        }
        [Theory]
        [InlineData("482.496.596-94")]
        [InlineData("948.843.518-60")]
        [InlineData("689.173.690-06")]
        [InlineData("")]
        public void MustFormatCpf(string cpf)
        {
            cpf.ExtractNumbers().FormatCPF().ShouldBeEqual(cpf);
        }
        [Theory]
        [InlineData("482.496.596-94")]
        [InlineData("948.843.518-60")]
        [InlineData("689.173.690-06")]
        [InlineData("36.320.880/0001-20")]
        [InlineData("88.143.821/0001-28")]
        [InlineData("67.232.732/0001-88")]
        [InlineData("")]
        public void MustFormatCpfOrCnpj(string item)
        {
            item.ExtractNumbers().FormatCpfOrCnpj().ShouldBeEqual(item);
        }

        [Theory]
        [InlineData("16812192635")]
        [InlineData("65325255290")]
        [InlineData("26954228818")]
        public void MustCpfValidJustNumbers(string cpfTest)
        {
            cpfTest.IsCPF().ShouldBeTrue();
            cpfTest.IsCpfOrCnpj().ShouldBeTrue();
        }

        [Theory]
        [InlineData("373.871.-97")]
        [InlineData("121.164.530")]
        [InlineData("121.567.530-99")]
        [InlineData("921.....530-ER")]
        public void MustCpfInvalidWithMask(string cpfTest)
        {
            cpfTest.IsCPF().ShouldBeFalse();
            cpfTest.IsCpfOrCnpj().ShouldBeFalse();
        }
        [Theory]
        [InlineData("152892652")]
        [InlineData("28134617412")]
        [InlineData("78 95 13 16 50")]
        public void MustCpfInvalidJustNumbers(string cpfTest)
        {
            cpfTest.IsCPF().ShouldBeFalse();
            cpfTest.IsCpfOrCnpj().ShouldBeFalse();
        }

        [Theory]
        [InlineData("219.77689.19-3")]
        [InlineData("040.78525.77-5")]
        [InlineData("511.90657.13-2")]
        public void MustPisValidWithMask(string pisTest)
        {
            pisTest.IsPIS().ShouldBeTrue();
        }

        [Theory]
        [InlineData("78807027118")]
        [InlineData("27944141090")]
        [InlineData("32216633212")]
        public void MustPisValidJustNumbers(string pisTest)
        {
            pisTest.IsPIS().ShouldBeTrue();
        }

        [Theory]
        [InlineData("939.78484.71-$")]
        [InlineData("965.24304.36-4785")]
        [InlineData("21.15452.85-0")]
        [InlineData("226.59272.52-2")]
        public void MustPisInvalidWithMask(string pisTest)
        {
            pisTest.IsPIS().ShouldBeFalse();
        }

        [Theory]
        [InlineData("65171159888")]
        [InlineData("7138525236")]
        [InlineData("285911108065")]
        public void MustPisInvalidJustNumbers(string pisTest)
        {
            pisTest.IsPIS().ShouldBeFalse();
        }

        [Theory]
        [InlineData("27410280", "27410-280")]
        [InlineData("80230010", "80230-010")]
        [InlineData("", "")]
        public void MustFormatCep(string value, string cep)
        {
            value.FormatCEP().ShouldBeEqual(cep);
        }

        [Theory]
        [InlineData("27410-280")]
        [InlineData("27410280")]
        [InlineData("80230-010")]
        [InlineData("80230010")]
        public void MustValidateCep(string cep)
        {
            cep.IsCEP().ShouldBeTrue();
        }

        [Theory]
        [InlineData("2741a-280")]
        [InlineData("2741a280")]
        [InlineData("802300-010")]
        [InlineData("802300010")]
        public void MustNotValidateCep(string cep)
        {
            cep.IsCEP().ShouldBeFalse();
        }

        [Theory]
        [InlineData("haidf324h 23h4iuhaiuh2340)*(&* asdasd", "haidf324h23h4iuhaiuh2340asdasd")]
        [InlineData(" !@#$\"%¨&*()_+=-'\\\t|/?;:.,][~`´{}§¬¢£³²¹°ºª/?", "")]
        public void TestSanitize(string str, string exp)
        {
            str.Sanitize().ShouldBeEqual(exp);
        }

        [Theory]
        [InlineData("haidf324h 23h4iuhaiuh2340)*(&* asdasd", "haidf324h 23h4iuhaiuh2340 asdasd")]
        public void TestSanitizeIgnoreSpacing(string str, string exp)
        {
            str.Sanitize(true).ShouldBeEqual(exp);
        }

    }
}
