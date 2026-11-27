Imports AwesomeAssertions
Imports Gravo
Imports NUnit.Framework

<TestFixture>
Public Class KanaRomajiTests

    <TestCase("ka", "か")>
    <TestCase("ki", "き")>
    <TestCase("ku", "く")>
    <TestCase("ke", "け")>
    <TestCase("ko", "こ")>
    <TestCase("sa", "さ")>
    <TestCase("shi", "し")>
    <TestCase("su", "す")>
    <TestCase("se", "せ")>
    <TestCase("so", "そ")>
    <TestCase("ta", "た")>
    <TestCase("chi", "ち")>
    <TestCase("tsu", "つ")>
    <TestCase("te", "て")>
    <TestCase("to", "と")>
    <TestCase("na", "な")>
    <TestCase("ni", "に")>
    <TestCase("nu", "ぬ")>
    <TestCase("ne", "ね")>
    <TestCase("no", "の")>
    <TestCase("ha", "は")>
    <TestCase("hi", "ひ")>
    <TestCase("fu", "ふ")>
    <TestCase("he", "へ")>
    <TestCase("ho", "ほ")>
    <TestCase("ma", "ま")>
    <TestCase("mi", "み")>
    <TestCase("mu", "む")>
    <TestCase("me", "め")>
    <TestCase("mo", "も")>
    <TestCase("ya", "や")>
    <TestCase("yu", "ゆ")>
    <TestCase("yo", "よ")>
    <TestCase("ra", "ら")>
    <TestCase("ri", "り")>
    <TestCase("ru", "る")>
    <TestCase("re", "れ")>
    <TestCase("ro", "ろ")>
    <TestCase("wa", "わ")>
    <TestCase("wo", "を")>
    <TestCase("n", "ん")>
    <TestCase("kya", "きゃ")>
    <TestCase("kyu", "きゅ")>
    <TestCase("kyo", "きょ")>
    <TestCase("sha", "しゃ")>
    <TestCase("shu", "しゅ")>
    <TestCase("sho", "しょ")>
    <TestCase("cha", "ちゃ")>
    <TestCase("chu", "ちゅ")>
    <TestCase("cho", "ちょ")>
    <TestCase("nya", "にゃ")>
    <TestCase("nyu", "にゅ")>
    <TestCase("nyo", "にょ")>
    <TestCase("hya", "ひゃ")>
    <TestCase("hyu", "ひゅ")>
    <TestCase("hyo", "ひょ")>
    <TestCase("mya", "みゃ")>
    <TestCase("rya", "りゃ")>
    <TestCase("gya", "ぎゃ")>
    <TestCase("ja", "じゃ")>
    <TestCase("ju", "じゅ")>
    <TestCase("jo", "じょ")>
    <TestCase("bya", "びゃ")>
    <TestCase("pya", "ぴゃ")>
    <TestCase("ji", "じ")>
    <TestCase("di", "ぢ")>
    <TestCase("zu", "ず")>
    <TestCase("da", "だ")>
    <TestCase("ji", "じ")>
    Public Sub RomajiToKana_SingleSyllable_ReturnsKana(romaji As String, expected As String)
        Dim result = KanaRomaji.RomajiToKana(romaji)
        result.Kana.Should().Be(expected)
        result.Remaining.Should().Be("")
    End Sub

    <Test>
    Public Sub RomajiToKana_SokuonDoubling_ReturnsCorrect()
        Dim result = KanaRomaji.RomajiToKana("kitte")
        result.Kana.Should().Be("きって")
        result.Remaining.Should().Be("")
    End Sub

    <Test>
    Public Sub RomajiToKana_DoubleN_ReturnsCorrect()
        Dim result = KanaRomaji.RomajiToKana("shinnen")
        result.Kana.Should().Be("しんねん")
        result.Remaining.Should().Be("")
    End Sub

    <Test>
    Public Sub RomajiToKana_DoubleN_ReturnsんWithRestN()
        Dim result = KanaRomaji.RomajiToKana("nn")
        result.Kana.Should().Be("ん")
        result.Remaining.Should().Be("n")
    End Sub

    <Test>
    Public Sub RomajiToKana_DoubleE_ReturnsCorrect()
        Dim result = KanaRomaji.RomajiToKana("keeki")
        result.Kana.Should().Be("けえき")
        result.Remaining.Should().Be("")
    End Sub

    <Test>
    Public Sub RomajiToKana_MacronDoubleE_ReturnsCorrect()
        Dim result = KanaRomaji.RomajiToKana("kēki")
        result.Kana.Should().Be("けえき")
        result.Remaining.Should().Be("")
    End Sub

    <Test>
    Public Sub RomajiToKana_DiacriticDakuten_ReturnsCorrect()
        Dim result = KanaRomaji.RomajiToKana("taberu")
        result.Kana.Should().Be("たべる")
        result.Remaining.Should().Be("")
    End Sub

    <Test>
    Public Sub RomajiToKana_PartialInput_ReturnsRest()
        Dim result = KanaRomaji.RomajiToKana("k")
        result.Kana.Should().Be("")
        result.Remaining.Should().Be("k")
    End Sub

    <Test>
    Public Sub RomajiToKana_Empty_ReturnsEmpty()
        Dim result = KanaRomaji.RomajiToKana("")
        result.Kana.Should().Be("")
        result.Remaining.Should().Be("")
    End Sub

    <Test>
    Public Sub KanaToRomaji_KatakanaLongVowel_ReturnsCorrect()
        KanaRomaji.KanaToRomaji("ケーキ").Should().Be("keeki")
    End Sub

    <Test>
    Public Sub KanaToRomaji_HiraganaDiacriticDakuten_ReturnsCorrect()
        KanaRomaji.KanaToRomaji("たべる").Should().Be("taberu")
    End Sub

    <Test>
    Public Sub KanaToRomaji_DoubleN_ReturnsCorrect()
        KanaRomaji.KanaToRomaji("しんねん").Should().Be("shinnen")
    End Sub

    <Test>
    Public Sub CanonicalForm_Katakana_Equals_Romaji()
        KanaRomaji.CanonicalForm("ケーキ").Should().Be(KanaRomaji.CanonicalForm("keeki"))
    End Sub

    <Test>
    Public Sub CanonicalForm_HiraganaLongVowel_Equals_Romaji()
        KanaRomaji.CanonicalForm("けえき").Should().Be(KanaRomaji.CanonicalForm("keeki"))
    End Sub

    <Test>
    Public Sub CanonicalForm_DiacriticDakuten_ReturnsCorrect()
        KanaRomaji.CanonicalForm("たべる").Should().Be("taberu")
    End Sub

    <Test>
    Public Sub CanonicalForm_Empty_ReturnsEmpty()
        KanaRomaji.CanonicalForm("").Should().Be("")
    End Sub

    <Test>
    Public Sub CanonicalForm_MixedCaseRomaji_Lowercased()
        KanaRomaji.CanonicalForm("KaTaKaNa").Should().Be("katakana")
    End Sub

    <Test>
    Public Sub CanonicalForm_NonKanaNonRomaji_PassedThroughLowercased()
        KanaRomaji.CanonicalForm("123").Should().Be("123")
    End Sub
End Class
