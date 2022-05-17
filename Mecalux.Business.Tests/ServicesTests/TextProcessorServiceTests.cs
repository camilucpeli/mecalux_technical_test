using Mecalux.Business.Services;
using Mecalux.DTOs;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Mecalux.Business.Tests.ServicesTests
{
    public class TextProcessorServiceTests
    {
        private TextProcessorService _textProcessorService;

        [SetUp]
        public void Setup()
        {
            _textProcessorService = new TextProcessorService();
        }

        [Test]
        [TestCase("stool scramble uniform definition neutral gradient weapon election carrot soft", OrderOptions.AlphabeticAsc,
            new [] { "carrot", "definition", "election", "gradient", "neutral", "scramble", "soft", "stool", "uniform", "weapon" })]
        [TestCase("stool scramble uniform definition neutral gradient weapon election carrot soft", OrderOptions.AlphabeticDesc, 
            new [] { "weapon", "uniform", "stool", "soft", "scramble", "neutral", "gradient", "election", "definition", "carrot" })]
        [TestCase("study sculpture creation disagree timetable question uncertainty fox glove friend", OrderOptions.AlphabeticAsc,
            new[] { "creation", "disagree", "fox", "friend", "glove", "question", "sculpture", "study", "timetable", "uncertainty" })]
        [TestCase("study sculpture creation disagree timetable question uncertainty fox glove friend", OrderOptions.AlphabeticDesc,
            new[] { "uncertainty", "timetable", "study", "sculpture", "question", "glove", "friend", "fox", "disagree", "creation" })]
        public void GetOrderedTextAlphabeticallyTest(string textToOrder, OrderOptions orderOption, string[] expected)
        {
            var actual = _textProcessorService.GetOrderedText(textToOrder, orderOption.ToString());
            Assert.IsTrue(Enumerable.SequenceEqual(actual, expected));
        }

        [Test]
        [TestCase("stool scramble uniform definition neutral gradient weapon election carrot soft", OrderOptions.LenghtAsc)]
        [TestCase("study sculpture creation disagree timetable question uncertainty fox glove friend", OrderOptions.LenghtAsc)]
        public void GetOrderedTextByLenghtTest(string textToOrder, OrderOptions orderOption)
        {
            var actual= _textProcessorService.GetOrderedText(textToOrder, orderOption.ToString());

            Assert.IsTrue(AreWordsOrderedByLenght(actual));
        }

        private bool AreWordsOrderedByLenght(List<string> actual)
        {
            for (int i = 0; i < actual.Count - 1; i++)
            {
                if (actual.ElementAt(i).Length > actual.ElementAt(i + 1).Length) return false;
            }

            return true;
        }

        [Test]
        [TestCase("stool scramble uniform definition neutral gradient weapon election carrot soft", 0, 9, 10)]
        [TestCase("study-sculpture   crea tion-- -disagree timetable", 4, 6, 5)]
        [TestCase("   -study-sculpture   crea tion-- -disagree timetable", 5, 9, 5)]
        public void GetStatisticsTest(string textToAnalyze, int expectedHyphens, int expectedSpaces, int expectedWords)
        {
            var result = _textProcessorService.GetStatistics(textToAnalyze);

            var actualHyphens = result.HyphensQuantity;
            var actualSpaces = result.SpacesQuantity;
            var actualWords = result.WordQuantity;


            Assert.AreEqual(expectedWords, actualWords, "The number of words calculated is not correct.");
            Assert.AreEqual(expectedHyphens, actualHyphens, "The number of hyphens calculated is not correct.");
            Assert.AreEqual(expectedSpaces, actualSpaces, "The number of spaces calculated is not correct.");
        }
    }
}
