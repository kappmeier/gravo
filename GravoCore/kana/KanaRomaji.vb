Imports System.Text

''' <summary>
''' Hepburn kana/romaji converter. Converts romaji to hiragana and kana to romaji, with canonical forms for comparison.
''' </summary>
Public Module KanaRomaji

    Private ReadOnly Syllables As New Dictionary(Of String, String)(StringComparer.Ordinal) From {
        {"a", "あ"}, {"i", "い"}, {"u", "う"}, {"e", "え"}, {"o", "お"},
        {"ka", "か"}, {"ki", "き"}, {"ku", "く"}, {"ke", "け"}, {"ko", "こ"},
        {"sa", "さ"}, {"shi", "し"}, {"su", "す"}, {"se", "せ"}, {"so", "そ"},
        {"ta", "た"}, {"chi", "ち"}, {"tsu", "つ"}, {"te", "て"}, {"to", "と"},
        {"na", "な"}, {"ni", "に"}, {"nu", "ぬ"}, {"ne", "ね"}, {"no", "の"},
        {"ha", "は"}, {"hi", "ひ"}, {"fu", "ふ"}, {"he", "へ"}, {"ho", "ほ"},
        {"ma", "ま"}, {"mi", "み"}, {"mu", "む"}, {"me", "め"}, {"mo", "も"},
        {"ya", "や"}, {"yu", "ゆ"}, {"yo", "よ"},
        {"ra", "ら"}, {"ri", "り"}, {"ru", "る"}, {"re", "れ"}, {"ro", "ろ"},
        {"wa", "わ"}, {"wi", "ゐ"}, {"we", "ゑ"}, {"wo", "を"},
        {"ga", "が"}, {"gi", "ぎ"}, {"gu", "ぐ"}, {"ge", "げ"}, {"go", "ご"},
        {"za", "ざ"}, {"ji", "じ"}, {"zu", "ず"}, {"ze", "ぜ"}, {"zo", "ぞ"},
        {"da", "だ"}, {"di", "ぢ"}, {"du", "づ"}, {"de", "で"}, {"do", "ど"},
        {"ba", "ば"}, {"bi", "び"}, {"bu", "ぶ"}, {"be", "べ"}, {"bo", "ぼ"},
        {"pa", "ぱ"}, {"pi", "ぴ"}, {"pu", "ぷ"}, {"pe", "ぺ"}, {"po", "ぽ"},
        {"kya", "きゃ"}, {"kyu", "きゅ"}, {"kyo", "きょ"},
        {"sha", "しゃ"}, {"shu", "しゅ"}, {"sho", "しょ"},
        {"cha", "ちゃ"}, {"chu", "ちゅ"}, {"cho", "ちょ"},
        {"nya", "にゃ"}, {"nyu", "にゅ"}, {"nyo", "にょ"},
        {"hya", "ひゃ"}, {"hyu", "ひゅ"}, {"hyo", "ひょ"},
        {"mya", "みゃ"}, {"myu", "みゅ"}, {"myo", "みょ"},
        {"rya", "りゃ"}, {"ryu", "りゅ"}, {"ryo", "りょ"},
        {"gya", "ぎゃ"}, {"gyu", "ぎゅ"}, {"gyo", "ぎょ"},
        {"ja", "じゃ"}, {"ju", "じゅ"}, {"jo", "じょ"},
        {"bya", "びゃ"}, {"byu", "びゅ"}, {"byo", "びょ"},
        {"pya", "ぴゃ"}, {"pyu", "ぴゅ"}, {"pyo", "ぴょ"},
        {"n", "ん"}
    }

    Private ReadOnly KanaToRomajiMap As New Dictionary(Of String, String)(StringComparer.Ordinal)

    Private ReadOnly SmallKana As New HashSet(Of String)(StringComparer.Ordinal) From {
        "ゃ", "ゅ", "ょ", "ぁ", "ぃ", "ぅ", "ぇ", "ぉ", "ゎ", "っ"
    }

    Private ReadOnly LongVowelMap As New Dictionary(Of String, String)(StringComparer.Ordinal) From {
        {"ā", "aa"}, {"ī", "ii"}, {"ū", "uu"}, {"ē", "ee"}, {"ō", "oo"}
    }

    Sub New()
        For Each kv In Syllables
            KanaToRomajiMap(kv.Value) = kv.Key
        Next
    End Sub

    ''' <summary>
    ''' Converts romaji to hiragana using greedy longest match. Returns the converted kana and any remaining
    ''' unconvertible input.
    ''' </summary>
    Public Function RomajiToKana(romaji As String) As (Kana As String, Remaining As String)
        If String.IsNullOrEmpty(romaji) Then Return ("", "")

        Dim input = romaji.ToLowerInvariant()
        If input = "nn" Then Return ("ん", "n")
        input = ExpandMacrons(input)

        Dim result = New StringBuilder()
        Dim pos = 0
        While pos < input.Length
            Dim consumed = ConsumeSyllableAt(input, pos, result)
            If consumed = 0 Then Return (result.ToString(), input.Substring(pos))
            pos += consumed
        End While
        Return (result.ToString(), "")
    End Function

    Private Function ExpandMacrons(input As String) As String
        Dim sb = New StringBuilder(input.Length)
        For Each c In input
            Dim longForm As String = Nothing
            If LongVowelMap.TryGetValue(c.ToString(), longForm) Then sb.Append(longForm) Else sb.Append(c)
        Next
        Return sb.ToString()
    End Function

    Private Function ConsumeSyllableAt(input As String, pos As Integer, result As StringBuilder) As Integer
        Dim maxLen = Math.Min(4, input.Length - pos)
        For len As Integer = maxLen To 1 Step -1
            Dim consumed = TryMatchAt(input, pos, len, result)
            If consumed > 0 Then Return consumed
        Next
        Return 0
    End Function

    Private Function TryMatchAt(input As String, pos As Integer, len As Integer, result As StringBuilder) As Integer
        Const SokuonConsonants = "kstcpgbdz"
        Dim slice = input.Substring(pos, len)
        If len = 1 AndAlso pos + 1 < input.Length AndAlso SokuonConsonants.Contains(slice) AndAlso
           SokuonConsonants.Contains(input.Substring(pos + 1, 1)) Then
            result.Append("っ"c)
            Return 1
        End If
        Dim kana As String = Nothing
        If Syllables.TryGetValue(slice, kana) Then
            result.Append(kana)
            Return len
        End If
        Return 0
    End Function

    ''' <summary>
    ''' Converts kana to Hepburn romaji. Normalizes katakana to hiragana and expands the long-vowel mark to repeat the
    '''  previous vowel.
    ''' </summary>
    Public Function KanaToRomaji(kana As String) As String
        If String.IsNullOrEmpty(kana) Then Return ""

        Dim normalized = NormalizeKatakanaToHiragana(kana)
        Dim result = New StringBuilder()
        Dim i = 0
        Dim lastVowel = ""
        While i < normalized.Length
            Dim c = normalized.Substring(i, 1)
            ' Long-vowel mark
            If c = "ー" Then
                If lastVowel <> "" Then result.Append(lastVowel)
                i += 1
                Continue While
            End If
            ' Try two-char digraph match
            If i + 1 < normalized.Length Then
                Dim pair = normalized.Substring(i, 2)
                Dim pairRomaji = KanaPairToRomaji(pair)
                If pairRomaji IsNot Nothing Then
                    result.Append(pairRomaji)
                    lastVowel = ExtractVowel(pairRomaji)
                    i += 2
                    Continue While
                End If
            End If
            ' Single char
            Dim romaji As String = Nothing
            If KanaToRomajiMap.TryGetValue(c, romaji) Then
                result.Append(romaji)
                lastVowel = ExtractVowel(romaji)
            Else
                result.Append(c)
            End If
            i += 1
        End While
        Return result.ToString()
    End Function

    ''' <summary>
    ''' Returns a canonical lowercase form. Converts kana to romaji and round-trips romaji through kana so all variants
    ''' compare equal.
    ''' </summary>
    Public Function CanonicalForm(text As String) As String
        If String.IsNullOrEmpty(text) Then Return ""
        If ContainsKana(text) Then
            Return KanaToRomaji(text).ToLowerInvariant()
        End If
        Dim converted = RomajiToKana(text)
        If converted.Kana = "" AndAlso converted.Remaining <> "" Then
            Return text.ToLowerInvariant()
        End If
        Return KanaToRomaji(converted.Kana).ToLowerInvariant()
    End Function

    Private Function ContainsKana(text As String) As Boolean
        For Each c In text
            Dim code = AscW(c)
            If (code >= &H3040 AndAlso code <= &H309F) OrElse (code >= &H30A0 AndAlso code <= &H30FF) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function NormalizeKatakanaToHiragana(text As String) As String
        Dim sb = New StringBuilder(text.Length)
        For Each c In text
            Dim code = AscW(c)
            ' U+30FC (prolonged sound mark ー) is shared between the scripts, keep it
            If code = &H30FC Then
                sb.Append(c)
            ElseIf code >= &H30A0 AndAlso code <= &H30FF Then
                sb.Append(ChrW(code - &H60))
            Else
                sb.Append(c)
            End If
        Next
        Return sb.ToString()
    End Function

    Private Function KanaPairToRomaji(pair As String) As String
        If pair.Length <> 2 Then Return Nothing
        Dim first = pair.Substring(0, 1)
        Dim second = pair.Substring(1, 1)
        If Not SmallKana.Contains(second) Then Return Nothing
        Dim firstRomaji As String = Nothing
        If Not KanaToRomajiMap.TryGetValue(first, firstRomaji) Then Return Nothing

        Dim key = first & second
        Select Case key
            Case "きゃ" : Return "kya"
            Case "きゅ" : Return "kyu"
            Case "きょ" : Return "kyo"
            Case "しゃ" : Return "sha"
            Case "しゅ" : Return "shu"
            Case "しょ" : Return "sho"
            Case "ちゃ" : Return "cha"
            Case "ちゅ" : Return "chu"
            Case "ちょ" : Return "cho"
            Case "にゃ" : Return "nya"
            Case "にゅ" : Return "nyu"
            Case "にょ" : Return "nyo"
            Case "ひゃ" : Return "hya"
            Case "ひゅ" : Return "hyu"
            Case "ひょ" : Return "hyo"
            Case "みゃ" : Return "mya"
            Case "みゅ" : Return "myu"
            Case "みょ" : Return "myo"
            Case "りゃ" : Return "rya"
            Case "りゅ" : Return "ryu"
            Case "りょ" : Return "ryo"
            Case "ぎゃ" : Return "gya"
            Case "ぎゅ" : Return "gyu"
            Case "ぎょ" : Return "gyo"
            Case "じゃ" : Return "ja"
            Case "じゅ" : Return "ju"
            Case "じょ" : Return "jo"
            Case "びゃ" : Return "bya"
            Case "びゅ" : Return "byu"
            Case "びょ" : Return "byo"
            Case "ぴゃ" : Return "pya"
            Case "ぴゅ" : Return "pyu"
            Case "ぴょ" : Return "pyo"
            Case Else : Return Nothing
        End Select
    End Function

    Private Function ExtractVowel(romaji As String) As String
        If String.IsNullOrEmpty(romaji) Then Return ""
        Dim last = romaji.Substring(romaji.Length - 1, 1)
        If "aiueo".Contains(last) Then Return last
        Return ""
    End Function
End Module
