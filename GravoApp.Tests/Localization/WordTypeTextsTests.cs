using FluentAssertions;
using GravoApp.Localization;
using GravoApp.Tests.Support;
using NUnit.Framework;

namespace GravoApp.Tests.Localization;

public class WordTypeTextsTests
{
    [TestCase("Substantive", "T3")]
    [TestCase("Verb", "T4")]
    [TestCase("Adjective", "T5")]
    [TestCase("Simple", "T6")]
    [TestCase("Adverb", "T7")]
    [TestCase("SetPhrase", "T8")]
    [TestCase("Example", "T9")]
    public void Display_KnownEnumName_UsesWordTypeConstant(string name, string expected) =>
        WordTypeTexts.Display(Fakes.Texts(), name).Should().Be(expected);

    [TestCase("WORD_TYPE_SUBSTANTIVE", "T3")]
    [TestCase("WORD_TYPE_VERB", "T4")]
    [TestCase("WORD_TYPE_ADJECTIVE", "T5")]
    [TestCase("WORD_TYPE_SIMPLE", "T6")]
    [TestCase("WORD_TYPE_ADVERB", "T7")]
    [TestCase("WORD_TYPE_SET_PHRASE", "T8")]
    [TestCase("WORD_TYPE_EXAMPLE", "T9")]
    public void Display_DatabaseKey_UsesWordTypeConstant(string key, string expected) =>
        WordTypeTexts.Display(Fakes.Texts(), key).Should().Be(expected);

    [Test]
    public void Display_UnknownName_ReturnsName() =>
        WordTypeTexts.Display(Fakes.Texts(), "TestType").Should().Be("TestType");
}
