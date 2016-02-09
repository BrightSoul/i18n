using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using i18n.Web.Controllers;
using System.Linq;

namespace i18n.Tests
{
    [TestClass]
    public class LocalizationControllerTests
    {
        [TestMethod]
        public void GetRequestedLanguages_should_return_the_languages_parsing_the_raw_value_case_insensitevely_according_to_RFC2616_sec14_4_and_ignoring_the_quality_values()
        {
            //Arrange
            var rawValue = "it-IT,it;q=0.8,en-us;q=0.6,en;q=0.4";
            var expectedLanguages = new[] { "it-IT", "it", "en-us", "en" };

            //Act
            var actualLanguages = LocalizationController.GetPreferredLanguages(rawValue);

            //Assert
            CollectionAssert.AreEqual(expectedLanguages, actualLanguages.ToList());
        }
        [TestMethod]
        public void GetRequestedLanguages_should_return_an_empty_set_when_the_raw_value_is_empty()
        {
            //Arrange
            var rawValue = string.Empty;
            var expectedLanguages = new string[] { };

            //Act
            var actualLanguages = LocalizationController.GetPreferredLanguages(rawValue);

            //Assert
            CollectionAssert.AreEqual(expectedLanguages, actualLanguages.ToList());
        }

        [TestMethod]
        public void GetSupportedLanguages_should_split_the_raw_value_and_return_the_correct_set_of_languages()
        {
            //Arrange
            var rawValue = "en,it,ar";
            var expectedLanguages = new [] { "en", "it", "ar" };

            //Act
            var actualLanguages = LocalizationController.GetSupportedLanguages(rawValue);

            //Assert
            CollectionAssert.AreEqual(expectedLanguages, actualLanguages.ToList());
        }
        [TestMethod]
        public void SuggestLanguage_should_return_the_first_preferred_language_that_s_also_a_supported_language()
        {
            //Arrange
            var supportedLanguages = new[] { "en", "it", "ar" };
            var preferredLanguages = new[] { "it-IT", "it", "en-us", "en" };
            var expectedSelection = "it";

            //Act
            var actualSelection = LocalizationController.SuggestLanguage(supportedLanguages, preferredLanguages);

            //Assert
            Assert.AreEqual(expectedSelection, actualSelection);
        }
    }
}
