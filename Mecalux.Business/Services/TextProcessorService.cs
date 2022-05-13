using Mecalux.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mecalux.Business.Services
{
    public class TextProcessorService
    {
        /// <summary>
        /// Gets the available options for ordering a text.
        /// </summary>
        /// <returns>A list with all the order option.</returns>
        public List<OrderOptions> GetOrderOptions()
        {
            var orderOptions = (OrderOptions[])Enum.GetValues(typeof(OrderOptions));
            return new List<OrderOptions>(orderOptions);
        }

        /// <summary>
        /// Orders words in a text by the given Order Option.
        /// </summary>
        /// <param name="textToOrder">Text with words to order.</param>
        /// <param name="orderOption">Order option: AlphabeticAsc, AlphabeticDec, LenghtAsc.</param>
        /// <returns>A list with the ordered words.</returns>
        public List<string> GetOrderedText(string textToOrder, OrderOptions orderOption)
        {
            return orderOption switch
            {
                OrderOptions.AlphabeticAsc => GetTextAlphabeticAsc(textToOrder),
                OrderOptions.AlphabeticDesc => GetTextAlphabeticDesc(textToOrder),
                OrderOptions.LenghtAsc => GetTextLenghtAsc(textToOrder),
                _ => new List<string>(),
            };
        }

        /// <summary>
        /// Calculates the number of hyphens, blank spaces and words in the given string.
        /// </summary>
        /// <param name="textToAnalyze">String with text to analyze.</param>
        /// <returns>An object with the statistics information.</returns>
        public TextStatistics GetStatistics(string textToAnalyze)
        {
            return new TextStatistics()
            {
                HyphensQuantity = GetHyphensQuantity(textToAnalyze),
                SpacesQuantity = GetSpaceQuantity(textToAnalyze),
                WordQuantity = GetWordQuantity(textToAnalyze),
            };
        }

        private int GetHyphensQuantity(string textToAnalyze)
        {
            return textToAnalyze.Count(c => c == '-');
        }

        private int GetWordQuantity(string textToAnalyze)
        {
            var wordCount = 0;
            var index = 0;

            while (index < textToAnalyze.Length && char.IsWhiteSpace(textToAnalyze[index]))
                index++;

            while (index < textToAnalyze.Length)
            {
                while (index < textToAnalyze.Length && !char.IsWhiteSpace(textToAnalyze[index]))
                    index++;

                wordCount++;

                while (index < textToAnalyze.Length && char.IsWhiteSpace(textToAnalyze[index]))
                    index++;
            }

            return wordCount;
        }

        private int GetWordQuantitySimple(string textToAnalyze)
        {
            return textToAnalyze.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;
        }


        private int GetSpaceQuantity(string textToAnalyze)
        {
            return textToAnalyze.Count(c => c == ' ');
        }

        private List<string> GetTextAlphabeticAsc(string textToOrder)
        {
            var words = textToOrder.Split(' ').ToList();
            words.Sort();
            return words;
        }

        private List<string> GetTextAlphabeticDesc(string textToOrder)
        {
            var words = textToOrder.Split(' ').ToList();
            words.Sort();
            words.Reverse();
            return words;
        }

        private List<string> GetTextLenghtAsc(string textToOrder)
        {
            return textToOrder.Split(' ').OrderBy(x => x.Length).ToList();
        }
    
    
    }
}