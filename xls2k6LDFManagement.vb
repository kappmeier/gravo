Public Class xlsLDFManagement
	Inherits xlsLDFBaseEx

	Private m_cLanguages As Collection

	Private m_ldfSelected As xlsLanguageInfo
	Private m_sSelectedFileName As String
	Private m_sPath As String

	Private htLDF As Hashtable

	Public Sub New()
		MyBase.New()
		m_cLanguages = New Collection
		htLDF = New Hashtable
		LDFPath = Application.StartupPath()
	End Sub

	Public ReadOnly Property Languages() As Collection
		Get
			Return m_cLanguages
		End Get
	End Property

	Public Overrides Sub LoadLDF(ByVal sPath As String)
		MyBase.LoadLDF(sPath)
	End Sub

	Public Property LDFPath() As String
		Get
			Return m_sPath
		End Get
		Set(ByVal sPath As String)
			'Application.StartupPath() & "\" &  Language& ".ldf"
			' TODO Alle Dateien im Path laden
			QuickLoad = True
			htLDF.Clear()
			m_sPath = sPath
			Me.m_cLanguages = New Collection

			' Laden der Main-Infos in eine Collection
			LoadLDF(sPath & "\" & "italian_std" & ".ldf")
			htLDF.Add(LanguageInfo, "italian_std")
			m_cLanguages.Add(LanguageInfo)

			LoadLDF(sPath & "\" & "english_std" & ".ldf")
			htLDF.Add(LanguageInfo, "english_std")
			m_cLanguages.Add(LanguageInfo)

			LoadLDF(sPath & "\" & "french_std" & ".ldf")
			htLDF.Add(LanguageInfo, "french_std")
			m_cLanguages.Add(LanguageInfo)

			LoadLDF(sPath & "\" & "latin_std" & ".ldf")
			htLDF.Add(LanguageInfo, "latin_std")
			m_cLanguages.Add(LanguageInfo)
			QuickLoad = False
		End Set
	End Property

	Public ReadOnly Property LDFFileName() As String
		Get
			Return Me.m_sSelectedFileName & ".ldf"
		End Get
	End Property

	Public ReadOnly Property LDFFullPath() As String
		Get
			Return Me.m_sPath & "\" & Me.m_sSelectedFileName & ".ldf"
		End Get
	End Property

	Public Sub SelectLD(ByVal sLanguage As String, ByVal sType As String)
		Dim i As Integer		  ' Index

		For i = 1 To Me.m_cLanguages.Count
			If m_cLanguages.Item(i).Name = sLanguage And m_cLanguages.Item(i).type = sType Then
				m_ldfSelected = m_cLanguages.Item(i)
				m_sSelectedFileName = htLDF.Item(m_ldfSelected)
			End If
		Next i
		LoadLDF(LDFFullPath)		  ' laden
	End Sub
End Class
