using FluentAssertions;
using GhostText.Services.Levenshteins;

namespace GhostText.Unit.Tests.Services.Levenshtein
{
    public class LevenshteinSeviceTests
    {
        private readonly ILevenshteinService levenshteinService;
        public LevenshteinSeviceTests()  =>
            this.levenshteinService = new LevenshteinService();
        
        [Theory]
        [InlineData("olam", "olma")]
        [InlineData("good", "gd")]
        [InlineData("yomon", "ymn")]
        public void ShouldBeLevenshteinDistanceEqualToTwo(string input1, string input2)
        {
            // given 
            int expectedResult=2;
            
            // when
            int actualResult = this.levenshteinService.LevenshteinDistance(input1, input2);
            
            // then
            actualResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("bad", "bd")]
        [InlineData("good", "goo")]
        [InlineData("best", "bst")]
        public void ShouldBeLevenshteinDistanceNotEqualToOne(string input1, string input2)
        {
            // given
            int expectedResult=1;
            
            // when
            int actualResult = this.levenshteinService.LevenshteinDistance(input1, input2);
            
            // then
            actualResult.Should().Be(expectedResult);
        }
    }
}