using System.Collections.Generic;
using System.Linq;
using MazeBankBot.App.Helpers;
using NUnit.Framework;

namespace UnitTests.Helpers
{
    public class NumberRepresentationHelperTests
    {
        [Test]
        public void Test_NumberToWords_WorksCorrectly()
        {
            var cases = new Dictionary<int, string>
            {
                {100, "one hundred"},
                {1_000, "one thousand"},
                {1_000_000, "one million"},
                {13, "thirteen"},
                {7, "seven"},
                {1_123_001, "one million one hundred and twenty-three thousand and one"},
            };

            foreach (var x in cases)
            {
                var result = NumberRepresentationHelper.NumberToWords(x.Key).Trim();
                Assert.AreEqual(x.Value, result);
            }
        }

        [Test]
        public void Test_NumberToSingleWordArray_WorksCorrectly()
        {
            var cases = new Dictionary<int, List<string>>
            {
                {100, new List<string> {"one", "zero", "zero"}},
                {1_000, new List<string> {"one", "zero", "zero", "zero"}},
                {21, new List<string> {"two", "one"}},
            };

            foreach (var x in cases)
            {
                var result = NumberRepresentationHelper.NumberToSingleWordArray(x.Key)
                    .Select(x => x.Trim());

                Assert.AreEqual(x.Value, result);
            }
        }
    }
}