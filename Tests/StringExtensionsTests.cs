using System;
using BasicInfrastructure.Extensions;
using Xunit.Should;
using Xunit;

namespace Tests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("a@b$c*d)e´ f123", "abcdef123")]
        public void ShouldSanitizeString(string dirty, string clean)
        {
            Assert.Equal(clean, dirty.Sanitize());
        }
    }
}
