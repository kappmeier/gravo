DECLARE FUNCTION Methode4$ (Latein$, Deutsch$, Deutsch2$)
DECLARE FUNCTION Methode1$ (Latein$, Deutsch$, Genus$, Genitiv$)
DECLARE FUNCTION Methode2$ (Latein$, Deutsch$)
DECLARE FUNCTION Methode3$ (Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
DECLARE SUB BlauLektion1 ()
DECLARE SUB BlauLektion2 ()
DECLARE SUB BlauLektion3 ()
DECLARE SUB BlauLektion4 ()
DECLARE SUB BlauLektion5 ()
DECLARE SUB BlauLektion6 ()
DECLARE SUB BlauLektion7 ()
DECLARE SUB BlauLektion8 ()
DECLARE SUB BlauLektion9 ()
DECLARE SUB BlauLektion10 ()
DECLARE SUB BlauLektion11 ()
DECLARE SUB BlauLektion12 ()
DECLARE SUB BlauLektion13 ()
DECLARE SUB BlauLektion14 ()
DECLARE SUB BlauLektion15 ()
DECLARE SUB BlauLektion16 ()
DECLARE SUB BlauLektion17 ()
DECLARE SUB BlauLektion18 ()
DECLARE SUB BlauLektion19 ()
DECLARE SUB BlauLektion20 ()
DECLARE SUB BlauLektion21 ()
DECLARE SUB BlauLektion22 ()
DECLARE SUB BlauLektion23 ()
DECLARE SUB BlauLektion24 ()
DECLARE SUB BlauLektion25 ()
DECLARE SUB BlauLektion26 ()
DECLARE SUB BlauLektion27 ()
DECLARE SUB BlauLektion28 ()
DECLARE SUB BlauLektion29 ()
DECLARE SUB BlauLektion30 ()
DECLARE SUB BlauLektion31 ()
DECLARE SUB BlauLektion32 ()
DECLARE SUB BlauLektion33 ()
DECLARE SUB BlauLektion34 ()
DECLARE SUB BlauLektion35 ()
'Datei zum lernen lateinischer Vokabeln

GanzAmAnfang:
CLS
LOCATE 2, 6
COLOR 12, 0
PRINT "A u s w a h l"
LOCATE 4, 4
COLOR 7, 0
LINE INPUT "Bitte das Buch wÑhlen (Gelb, Blau) "; BuchWahl$
IF UCASE$(BuchWahl$) = "GELB" THEN
GOTO Gelb
ELSEIF UCASE$(BuchWahl$) = "BLAU" THEN
GOTO Blau
ELSE
GOTO GanzAmAnfang
END IF

Blau:
CLS
LOCATE 2, 6
COLOR 12, 0
PRINT "A u s w a h l"
LOCATE 4, 4
COLOR 7, 0
PRINT "Lektion WÑhlen:"
LOCATE 5, 4
PRINT "(1-35)"
INPUT "Wat' is Junge, wÑhl ma'"; LektionWahl
IF LektionWahl = 1 THEN
GOTO BL1
ELSEIF LektionWahl = 2 THEN
GOTO BL2
ELSEIF LektionWahl = 3 THEN
GOTO BL3
ELSEIF LektionWahl = 4 THEN
GOTO BL4
ELSEIF LektionWahl = 5 THEN
GOTO BL5
ELSEIF LektionWahl = 6 THEN
GOTO BL6
ELSEIF LektionWahl = 7 THEN
GOTO BL7
ELSEIF LektionWahl = 8 THEN
GOTO BL8
ELSEIF LektionWahl = 9 THEN
GOTO BL9
ELSEIF LektionWahl = 10 THEN
GOTO BL10
ELSEIF LektionWahl = 11 THEN
GOTO BL11
ELSEIF LektionWahl = 12 THEN
GOTO BL12
ELSEIF LektionWahl = 13 THEN
GOTO BL13
ELSEIF LektionWahl = 14 THEN
GOTO BL14
ELSEIF LektionWahl = 15 THEN
GOTO BL15
ELSEIF LektionWahl = 16 THEN
GOTO BL16
ELSEIF LektionWahl = 17 THEN
GOTO BL17
ELSEIF LektionWahl = 18 THEN
GOTO BL18
ELSEIF LektionWahl = 19 THEN
GOTO BL19
ELSEIF LektionWahl = 20 THEN
GOTO BL20
ELSEIF LektionWahl = 21 THEN
GOTO BL21
ELSEIF LektionWahl = 22 THEN
GOTO BL22
ELSEIF LektionWahl = 23 THEN
GOTO BL23
ELSEIF LektionWahl = 24 THEN
GOTO BL24
ELSEIF LektionWahl = 25 THEN
GOTO BL25
ELSEIF LektionWahl = 26 THEN
GOTO BL26
ELSEIF LektionWahl = 27 THEN
GOTO BL27
ELSEIF LektionWahl = 28 THEN
GOTO BL28
ELSEIF LektionWahl = 29 THEN
GOTO BL29
ELSEIF LektionWahl = 30 THEN
GOTO BL30
ELSEIF LektionWahl = 31 THEN
GOTO BL31
ELSEIF LektionWahl = 32 THEN
GOTO BL32
ELSEIF LektionWahl = 33 THEN
GOTO BL33
ELSEIF LektionWahl = 34 THEN
GOTO BL34
ELSEIF LektionWahl = 35 THEN
GOTO BL35
ELSE
GOTO Blau
END IF


BL1:
CALL BlauLektion1
GOTO HauptEnde

BL2:
CALL BlauLektion2
GOTO HauptEnde

BL3:
CALL BlauLektion3
GOTO HauptEnde

BL4:
CALL BlauLektion4
GOTO HauptEnde

BL5:
CALL BlauLektion5
GOTO HauptEnde

BL6:
CALL BlauLektion6
GOTO HauptEnde

BL7:
CALL BlauLektion7
GOTO HauptEnde

BL8:
CALL BlauLektion8
GOTO HauptEnde

BL9:
CALL BlauLektion9
GOTO HauptEnde

BL10:
CALL BlauLektion10
GOTO HauptEnde

BL11:
CALL BlauLektion11
GOTO HauptEnde

BL12:
CALL BlauLektion12
GOTO HauptEnde

BL13:
CALL BlauLektion13
GOTO HauptEnde

BL14:
CALL BlauLektion14
GOTO HauptEnde

BL15:
CALL BlauLektion15
GOTO HauptEnde

BL16:
CALL BlauLektion16
GOTO HauptEnde

BL17:
CALL BlauLektion17
GOTO HauptEnde

BL18:
CALL BlauLektion18
GOTO HauptEnde

BL19:
CALL BlauLektion19
GOTO HauptEnde

BL20:
CALL BlauLektion20
GOTO HauptEnde

BL21:
CALL BlauLektion21
GOTO HauptEnde

BL22:
CALL BlauLektion22
GOTO HauptEnde

BL23:
CALL BlauLektion23
GOTO HauptEnde

BL24:
CALL BlauLektion24
GOTO HauptEnde

BL25:
CALL BlauLektion25
GOTO HauptEnde

BL26:
CALL BlauLektion26
GOTO HauptEnde

BL27:
CALL BlauLektion27
GOTO HauptEnde

BL28:
CALL BlauLektion28
GOTO HauptEnde

BL29:
CALL BlauLektion29
GOTO HauptEnde

BL30:
CALL BlauLektion30
GOTO HauptEnde

BL31:
CALL BlauLektion31
GOTO HauptEnde

BL32:
CALL BlauLektion32
GOTO HauptEnde

BL33:
CALL BlauLektion33
GOTO HauptEnde

BL34:
CALL BlauLektion34
GOTO HauptEnde

BL35:
CALL BlauLektion35
GOTO HauptEnde

Gelb:

HauptEnde:
CLS
END

SUB BlauLektion1

BL0101:
Latein$ = "filius"
Deutsch$ = "Sohn"
Genus$ = "M"
Genitiv$ = "FILII"
Vok0101$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0102

BL0102:
Latein$ = "filia"
Deutsch$ = "Tochter"
Genus$ = "F"
Genitiv$ = "FILIAE"
Vok0102$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0103

BL0103:
Latein$ = "ancilla"
Deutsch$ = "Sklavin"
Genus$ = "F"
Genitiv$ = "ANCILLAE"
Vok0103$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0104

BL0104:
Latein$ = "dominus"
Deutsch$ = "Herr"
Genus$ = "M"
Genitiv$ = "DOMINI"
Vok0104$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0105

BL0105:
Latein$ = "domina"
Deutsch$ = "Herrin"
Genus$ = "F"
Genitiv$ = "DOMINAE"
Vok0105$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0106

BL0106:
Latein$ = "servus"
Deutsch$ = "Sklave"
Genus$ = "M"
Genitiv$ = "SERVI"
Vok0106$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0107

BL0107:
Latein$ = "currere"
Deutsch$ = "laufen"
Vok0107$ = Methode2$(Latein$, Deutsch$)
GOTO BL0108

BL0108:
Latein$ = "etiam"
Deutsch$ = "auch, sogar"
Deutsch2$ = "sogar, auch"
Vok0108$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0109

BL0109:
Latein$ = "vincere"
Deutsch$ = "siegen, besiegen"
Deutsch2$ = "besiegen, siegen"
Vok0109$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0110

BL0110:
Latein$ = "gaudere"
Deutsch$ = "sich freuen"
Vok0110$ = Methode2$(Latein$, Deutsch$)
GOTO BL0111

BL0111:
Latein$ = "nam"
Deutsch$ = "denn"
Vok0111$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0112

BL0112:
Latein$ = "victor"
Deutsch$ = "Sieger"
Genus$ = "M"
Genitiv$ = "VICTORIS"
Vok0112$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0113

BL0113:
Latein$ = "esse"
Deutsch$ = "sein"
Vok0113$ = Methode2$(Latein$, Deutsch$)
GOTO BL0114

BL0114:
Latein$ = "non"
Deutsch$ = "nicht"
Vok0114$ = Methode2$(Latein$, Deutsch$)
GOTO BL0115

BL0115:
Latein$ = "dolere"
Deutsch$ = "betrÅbt sein, bedauern"
Deutsch2$ = "bedauern, betrÅbt sein"
Vok0115$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0116

BL0116:
Latein$ = "et"
Deutsch$ = "und, auch"
Deutsch2$ = "auch, und"
Vok0116$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0117

BL0117:
Latein$ = "dicere"
Deutsch$ = "sagen"
Vok0117$ = Methode2$(Latein$, Deutsch$)
GOTO BL0118

BL0118:
Latein$ = "spes"
Deutsch$ = "Hoffnung"
Genus$ = "F"
Genitiv$ = "SPE"
Vok0110$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0119

BL0119:
Latein$ = "saepe (Adv.)"
Deutsch$ = "oft"
Vok0119$ = Methode2$(Latein$, Deutsch$)
GOTO BL0120

BL0120:
Latein$ = "fallere"
Deutsch$ = "tÑuschen, enttÑuschen"
Deutsch2$ = "enttÑuschen, tÑuschen"
Vok0120$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0121

BL0121:
Latein$ = "imperare"
Deutsch$ = "befehlen"
Vok0121$ = Methode2$(Latein$, Deutsch$)
GOTO BL0122

BL0122:
Latein$ = "parere"
Deutsch$ = "gehorchen"
Vok0122$ = Methode2$(Latein$, Deutsch$)
GOTO BL0123

BL0123:
Latein$ = "laborare"
Deutsch$ = "arbeiten"
Vok0120$ = Methode2$(Latein$, Deutsch$)
GOTO BL0121

BL0124:
Latein$ = "labor"
Deutsch$ = "Arbeit"
Genus$ = "M"
Genitiv$ = "LABORIS"
Vok0124$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0125

BL0125:
Latein$ = "placere"
Deutsch$ = "gefallen"
Vok0125$ = Methode2$(Latein$, Deutsch$)
GOTO BL0126

BL0126:
Latein$ = "canere"
Deutsch$ = "singen"
Vok0126$ = Methode2$(Latein$, Deutsch$)
GOTO BL0127

BL0127:
Latein$ = "vocare"
Deutsch$ = "rufen"
Vok0127$ = Methode2$(Latein$, Deutsch$)
GOTO BL0128

BL0128:
Latein$ = "audire"
Deutsch$ = "hîren"
Vok0128$ = Methode2$(Latein$, Deutsch$)
GOTO BL0129

BL0129:
Latein$ = "venire"
Deutsch$ = "kommen"
Vok0129$ = Methode2$(Latein$, Deutsch$)
GOTO BL0130

BL0130:
Latein$ = "quis?"
Deutsch$ = "wer?"
Vok0130$ = Methode2$(Latein$, Deutsch$)
GOTO BL0131

BL0131:
Latein$ = "quid?"
Deutsch$ = "was?"
Vok0131$ = Methode2$(Latein$, Deutsch$)
GOTO Ende01

Ende01:
IF Vok0101$ = "Falsch" THEN
GOTO BL0101
ELSEIF Vok0102$ = "Falsch" THEN
GOTO BL0102
ELSEIF Vok0103$ = "Falsch" THEN
GOTO BL0103
ELSEIF Vok0104$ = "Falsch" THEN
GOTO BL0104
ELSEIF Vok0105$ = "Falsch" THEN
GOTO BL0105
ELSEIF Vok0106$ = "Falsch" THEN
GOTO BL0106
ELSEIF Vok0107$ = "Falsch" THEN
GOTO BL0107
ELSEIF Vok0108$ = "Falsch" THEN
GOTO BL0108
ELSEIF Vok0109$ = "Falsch" THEN
GOTO BL0109
ELSEIF Vok0110$ = "Falsch" THEN
GOTO BL0110
ELSEIF Vok0111$ = "Falsch" THEN
GOTO BL0111
ELSEIF Vok0112$ = "Falsch" THEN
GOTO BL0112
ELSEIF Vok0113$ = "Falsch" THEN
GOTO BL0113
ELSEIF Vok0114$ = "Falsch" THEN
GOTO BL0114
ELSEIF Vok0115$ = "Falsch" THEN
GOTO BL0115
ELSEIF Vok0116$ = "Falsch" THEN
GOTO BL0116
ELSEIF Vok0117$ = "Falsch" THEN
GOTO BL0117
ELSEIF Vok0518$ = "Falsch" THEN
GOTO BL0118
ELSEIF Vok0119$ = "Falsch" THEN
GOTO BL0119
ELSEIF Vok0120$ = "Falsch" THEN
GOTO BL0120
ELSEIF Vok0121$ = "Falsch" THEN
GOTO BL0121
ELSEIF Vok0122$ = "Falsch" THEN
GOTO BL0122
ELSEIF Vok0123$ = "Falsch" THEN
GOTO BL0123
ELSEIF Vok0124$ = "Falsch" THEN
GOTO BL0124
ELSEIF Vok0125$ = "Falsch" THEN
GOTO BL0125
ELSEIF Vok0126$ = "Falsch" THEN
GOTO BL0126
ELSEIF Vok0127$ = "Falsch" THEN
GOTO BL0127
ELSEIF Vok0128$ = "Falsch" THEN
GOTO BL0128
ELSEIF Vok0129$ = "Falsch" THEN
GOTO BL0129
ELSEIF Vok0130$ = "Falsch" THEN
GOTO BL0130
ELSEIF Vok0131$ = "Falsch" THEN
GOTO BL0131
ELSE GOTO Letztende01
END IF

Letztende01:
END SUB

SUB BlauLektion10

BL1001:
Latein$ = "avunculus"
Deutsch$ = "Onkel"
Genus$ = "M"
Genitiv$ = "AVUNCULI"
Vok1001$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1002

BL1002:
Latein$ = "soror"
Deutsch$ = "Schwester"
Genus$ = "F"
Genitiv$ = "SOROREM"
Vok1002$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1003

BL1003:
Latein$ = "cui?"
Deutsch$ = "wem"
Vok1003$ = Methode2$(Latein$, Deutsch$)
GOTO BL1004

BL1004:
Latein$ = "dare"
Deutsch$ = "geben"
Vok1004$ = Methode2$(Latein$, Deutsch$)
GOTO BL1005

BL1005:
Latein$ = "miles"
Deutsch$ = "Soldat"
Genus$ = "M"
Genitiv$ = "MILITIS"
Vok1005$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1006

BL1006:
Latein$ = "legionarius"
Deutsch$ = "Legionssoldat"
Genus$ = "M"
Genitiv$ = "LEGIONARII"
Vok1006$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1007

BL1007:
Latein$ = "dic!"
Deutsch$ = "sag!"
Vok1007$ = Methode2$(Latein$, Deutsch$)
GOTO BL1008

BL1008:
Latein$ = "nonne?"
Deutsch$ = "etwa nicht?"
Vok1008$ = Methode2$(Latein$, Deutsch$)
GOTO BL1009

BL1009:
Latein$ = "cognoscere"
Deutsch$ = "erkennen, erfahren"
Deutsch2$ = "erfahren, erkennen"
Vok1009$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1010

BL1010:
Latein$ = "imperator"
Deutsch$ = "Feldherr, Kaiser"
Deutsch2$ = "Kaiser, Feldherr"
Genus$ = "M"
Genitiv$ = "IMPERATORIS"
Vok1010$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1011

BL1011:
Latein$ = "an?"
Deutsch$ = "etwa?"
Vok1011$ = Methode2$(Latein$, Deutsch$)
GOTO BL1012

BL1012:
Latein$ = "ignotus, a, um"
Deutsch$ = "unbekannt"
Vok1012$ = Methode2$(Latein$, Deutsch$)
GOTO BL1013

BL1013:
Latein$ = "vester, vestra, vestrum"
Deutsch$ = "euer"
Vok1013$ = Methode2$(Latein$, Deutsch$)
GOTO BL1014

BL1014:
Latein$ = "suus, a, um"
Deutsch$ = "sein, ihr"
Vok1014$ = Methode2$(Latein$, Deutsch$)
GOTO BL1015

BL1015:
Latein$ = "salve!"
Deutsch$ = "sei gegrÅ·t!, guten Tag!"
Vok1015$ = Methode2$(Latein$, Deutsch$)
GOTO BL1016

BL1016:
Latein$ = "victoria"
Deutsch$ = "Sieg"
Genus$ = "F"
Genitiv$ = "VICTORIAE"
Vok1016$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1017

BL1017:
Latein$ = "consulere (m. Dat.)"
Deutsch$ = "sorgen fÅr"
Vok1017$ = Methode2$(Latein$, Deutsch$)
GOTO BL1018

BL1018:
Latein$ = "pugna"
Deutsch$ = "Kampf, Schlacht"
Deutsch2$ = "Schlacht, Kampf"
Genus$ = "F"
Genitiv$ = "PUGNAE"
Vok1018$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1019

BL1019:
Latein$ = "hostis"
Deutsch$ = "Feind"
Genus$ = "M"
Genitiv$ = "HOSTIS"
Vok1019$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1020

BL1020:
Latein$ = "hinsidiae (Pl.)"
Deutsch$ = "Falle, Hinterhalt"
Deutsch2$ = "Hinterhalt, Falle"
Genus$ = "F"
Genitiv$ = "INSIDIARUM"
Vok1020$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1021

BL1021:
Latein$ = "planities"
Deutsch$ = "Ebene"
Genus$ = "F"
Genitiv$ = "PLANITIEI"
Vok1021$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1022

BL1022:
Latein$ = "acies"
Deutsch$ = "Herr, Schlacht"
Deutsch2$ = "Schlacht, Heer"
Genus$ = "F"
Genitiv$ = "ACIEI"
Vok1022$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1023

BL1023:
Latein$ = "instruere"
Deutsch$ = "aufstellen, ausrÅsten"
Deutsch2$ = "ausrÅsten, aufstellen"
Vok1023$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1024

BL1024:
Latein$ = "patria"
Deutsch$ = "Vaterland"
Genus$ = "F"
Genitiv$ = "PATRIAE"
Vok1024$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1025

BL1025:
Latein$ = "gloria"
Deutsch$ = "Ruhm"
Genus$ = "F"
Genitiv$ = "GLORIAE"
Vok1025$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1026

BL1026:
Latein$ = "terror"
Deutsch$ = "Schrecken"
Genus$ = "M"
Genitiv$ = "TERRORIS"
Vok1026$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1027

BL1027:
Latein$ = "copia"
Deutsch$ = "Menge, Vorrat"
Deutsch2$ = "Vorrat, Menge"
Genus$ = "F"
Genitiv$ = "COPIAE"
Vok1027$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1028

BL1028:
Latein$ = "copiae (Pl.)"
Deutsch$ = "Truppen, VorrÑte"
Deutsch2$ = "VorrÑte, Truppen"
Genus$ = "F"
Genitiv$ = "COPIARUM"
Vok1028$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1029

BL1029:
Latein$ = "submittere"
Deutsch$ = "zu Hilfe schicken"
Vok1029$ = Methode2$(Latein$, Deutsch$)
GOTO BL1030

BL1030:
Latein$ = "pugnare"
Deutsch$ = "kÑmpfen"
Vok1030$ = Methode2$(Latein$, Deutsch$)
GOTO BL1031

BL1031:
Latein$ = "captivus"
Deutsch$ = "Gefangener"
Genus$ = "M"
Genitiv$ = "CAPTIVII"
Vok1031$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO Ende10

Ende10:
IF Vok1001$ = "Falsch" THEN
GOTO BL1001
ELSEIF Vok1002$ = "Falsch" THEN
GOTO BL1002
ELSEIF Vok1003$ = "Falsch" THEN
GOTO BL1003
ELSEIF Vok1004$ = "Falsch" THEN
GOTO BL1004
ELSEIF Vok1005$ = "Falsch" THEN
GOTO BL1005
ELSEIF Vok1006$ = "Falsch" THEN
GOTO BL1006
ELSEIF Vok1007$ = "Falsch" THEN
GOTO BL1007
ELSEIF Vok1008$ = "Falsch" THEN
GOTO BL1008
ELSEIF Vok1009$ = "Falsch" THEN
GOTO BL1009
ELSEIF Vok1010$ = "Falsch" THEN
GOTO BL1010
ELSEIF Vok1011$ = "Falsch" THEN
GOTO BL1011
ELSEIF Vok1012$ = "Falsch" THEN
GOTO BL1012
ELSEIF Vok1013$ = "Falsch" THEN
GOTO BL1013
ELSEIF Vok1014$ = "Falsch" THEN
GOTO BL1014
ELSEIF Vok1015$ = "Falsch" THEN
GOTO BL1015
ELSEIF Vok1016$ = "Falsch" THEN
GOTO BL1016
ELSEIF Vok1017$ = "Falsch" THEN
GOTO BL1017
ELSEIF Vok1018$ = "Falsch" THEN
GOTO BL1018
ELSEIF Vok1019$ = "Falsch" THEN
GOTO BL1019
ELSEIF Vok1020$ = "Falsch" THEN
GOTO BL1020
ELSEIF Vok1021$ = "Falsch" THEN
GOTO BL1021
ELSEIF Vok1022$ = "Falsch" THEN
GOTO BL1022
ELSEIF Vok1023$ = "Falsch" THEN
GOTO BL1023
ELSEIF Vok1024$ = "Falsch" THEN
GOTO BL1024
ELSEIF Vok1025$ = "Falsch" THEN
GOTO BL1025
ELSEIF Vok1026$ = "Falsch" THEN
GOTO BL1026
ELSEIF Vok1027$ = "Falsch" THEN
GOTO BL1027
ELSEIF Vok1028$ = "Falsch" THEN
GOTO BL1028
ELSEIF Vok1029$ = "Falsch" THEN
GOTO BL1029
ELSEIF Vok1030$ = "Falsch" THEN
GOTO BL1030
ELSEIF Vok1031$ = "Falsch" THEN
GOTO BL1031
ELSE GOTO Letztende10
END IF

Letztende10:
END SUB

SUB BlauLektion11

BL1101:
Latein$ = "gemini (Pl.)"
Deutsch$ = "Zwillinge"
Genus$ = "F"
Genitiv$ = "GEMINORUM"
Vok1101$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1102

BL1102:
Latein$ = "servare"
Deutsch$ = "retten"
Vok1102$ = Methode2$(Latein$, Deutsch$)
GOTO BL1103

BL1103:
Latein$ = "pastor"
Deutsch$ = "Hirte"
Genus$ = "M"
Genitiv$ = "PASTORIS"
Vok1103$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1104

BL1104:
Latein$ = "casa"
Deutsch$ = "HÅtte"
Genus$ = "F"
Genitiv$ = "CASAE"
Vok1104$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1105

BL1105:
Latein$ = "uxor"
Deutsch$ = "Ehefrau"
Genus$ = "F"
Genitiv$ = "UXORIS"
Vok1105$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1106

BL1106:
Latein$ = "vivere"
Deutsch$ = "leben"
Vok1106$ = Methode2$(Latein$, Deutsch$)
GOTO BL1107

BL1107:
Latein$ = "primus, a, um"
Deutsch$ = "der erste"
Vok1107$ = Methode2$(Latein$, Deutsch$)
GOTO BL1108

BL1108:
Latein$ = "prcul (Adv.)"
Deutsch$ = "fern, von fern"
Deutsch2$ = "von fern, fern"
Vok1108$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1109

BL1109:
Latein$ = "ripa"
Deutsch$ = "Ufer"
Genus$ = "F"
Genitiv$ = "RIPAE"
Vok1109$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1110

BL1110:
Latein$ = "fluvius"
Deutsch$ = "Flu·"
Genus$ = "M"
Genitiv$ = "FLUVII"
Vok1110$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1111

BL1111:
Latein$ = "lupa"
Deutsch$ = "Wîlfin"
Genus$ = "F"
Genitiv$ = "LUPAE"
Vok1111$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1112

BL1112:
Latein$ = "plenus, a, um"
Deutsch$ = "voll"
Vok1112$ = Methode2$(Latein$, Deutsch$)
GOTO BL1113

BL1113:
Latein$ = "vita"
Deutsch$ = "Leben"
Genus$ = "F"
Genitiv$ = "VITAE"
Vok1113$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1114

BL1114:
Latein$ = "cura"
Deutsch$ = "Sorge, Pflege"
Deutsch2$ = "Pflege, Sorge"
Genus$ = "F"
Genitiv$ = "CURAE"
Vok1114$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1115

BL1115:
Latein$ = "frater"
Deutsch$ = "Bruder"
Genus$ = "M"
Genitiv$ = "FRATRIS"
Vok1115$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1116

BL1116:
Latein$ = "res gestae"
Deutsch$ = "Taten, Geschichte"
Deutsch2$ = "Geschichte, Taten"
Genus$ = "F"
Genitiv$ = "RERUM GESTARUM"
Vok1116$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1117

BL1117:
Latein$ = "memoria"
Deutsch$ = "Erinnerung, GedÑchtnis"
Deutsch2$ = "GedÑchtnis, Erinnerung"
Genus$ = "F"
Genitiv$ = "MEMORIAE"
Vok1117$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1118

BL1118:
Latein$ = "numerus"
Deutsch$ = "Zahl"
Genus$ = "M"
Genitiv$ = "NUMERI"
Vok1118$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1119

BL1119:
Latein$ = "cuius?"
Deutsch$ = "wessen"
Vok1119$ = Methode2$(Latein$, Deutsch$)
GOTO BL1120

BL1120:
Latein$ = "invenire"
Deutsch$ = "finden, entdecken"
Deutsch2$ = "entdecken, finden"
Vok1120$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1121

BL1121:
Latein$ = "annus"
Deutsch$ = "Jahr"
Genus$ = "M"
Genitiv$ = "ANNI"
Vok1121$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1122

BL1122:
Latein$ = "olim (Adv.)"
Deutsch$ = "einst"
Vok1122$ = Methode2$(Latein$, Deutsch$)
GOTO BL1123

BL1123:
Latein$ = "dolus"
Deutsch$ = "List"
Genus$ = "M"
Genitiv$ = "DOLI"
Vok1123$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1124

BL1124:
Latein$ = "misericordia"
Deutsch$ = "Mitleid"
Genus$ = "F"
Genitiv$ = "MISERICORDIAE"
Vok1124$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1125

BL1125:
Latein$ = "deponere"
Deutsch$ = "niederlegen"
Vok1125$ = Methode2$(Latein$, Deutsch$)
GOTO BL1126

BL1126:
Latein$ = "ira"
Deutsch$ = "Zorn"
Genus$ = "F"
Genitiv$ = "IRAE"
Vok1126$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1127

BL1127:
Latein$ = "punire"
Deutsch$ = "bestrafen"
Vok1127$ = Methode2$(Latein$, Deutsch$)
GOTO BL1128

BL1128:
Latein$ = "condere"
Deutsch$ = "grÅnden, verbergen"
Deutsch2$ = "verbergen, grÅnden"
Vok1128$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO Ende11

Ende11:
IF Vok1101$ = "Falsch" THEN
GOTO BL1101
ELSEIF Vok1102$ = "Falsch" THEN
GOTO BL1102
ELSEIF Vok1103$ = "Falsch" THEN
GOTO BL1103
ELSEIF Vok1104$ = "Falsch" THEN
GOTO BL1104
ELSEIF Vok1105$ = "Falsch" THEN
GOTO BL1105
ELSEIF Vok1106$ = "Falsch" THEN
GOTO BL1106
ELSEIF Vok1107$ = "Falsch" THEN
GOTO BL1107
ELSEIF Vok1108$ = "Falsch" THEN
GOTO BL1108
ELSEIF Vok1109$ = "Falsch" THEN
GOTO BL1109
ELSEIF Vok1110$ = "Falsch" THEN
GOTO BL1110
ELSEIF Vok1111$ = "Falsch" THEN
GOTO BL1111
ELSEIF Vok1112$ = "Falsch" THEN
GOTO BL1112
ELSEIF Vok1113$ = "Falsch" THEN
GOTO BL1113
ELSEIF Vok1114$ = "Falsch" THEN
GOTO BL1114
ELSEIF Vok1115$ = "Falsch" THEN
GOTO BL1115
ELSEIF Vok1116$ = "Falsch" THEN
GOTO BL1116
ELSEIF Vok1117$ = "Falsch" THEN
GOTO BL1117
ELSEIF Vok1118$ = "Falsch" THEN
GOTO BL1118
ELSEIF Vok1119$ = "Falsch" THEN
GOTO BL1119
ELSEIF Vok1120$ = "Falsch" THEN
GOTO BL1120
ELSEIF Vok1121$ = "Falsch" THEN
GOTO BL1121
ELSEIF Vok1122$ = "Falsch" THEN
GOTO BL1122
ELSEIF Vok1123$ = "Falsch" THEN
GOTO BL1123
ELSEIF Vok1124$ = "Falsch" THEN
GOTO BL1124
ELSEIF Vok1125$ = "Falsch" THEN
GOTO BL1125
ELSEIF Vok1126$ = "Falsch" THEN
GOTO BL1126
ELSEIF Vok1127$ = "Falsch" THEN
GOTO BL1127
ELSEIF Vok1128$ = "Falsch" THEN
GOTO BL1128
ELSE GOTO Letztende11
END IF

Letztende11:
END SUB

SUB BlauLektion12

BL1201:
Latein$ = "circumdare"
Deutsch$ = "umgeben"
Vok1201$ = Methode2$(Latein$, Deutsch$)
GOTO BL1202

BL1202:
Latein$ = "autem"
Deutsch$ = "aber, jedoch"
Deutsch2$ = "jedoch, aber"
Vok1202$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1203

BL1203:
Latein$ = "tuus, a, um"
Deutsch$ = "dein"
Vok1203$ = Methode2$(Latein$, Deutsch$)
GOTO BL1204

BL1204:
Latein$ = "respondere"
Deutsch$ = "antworten"
Vok1204$ = Methode2$(Latein$, Deutsch$)
GOTO BL1205

BL1205:
Latein$ = "sanctus, a, um"
Deutsch$ = "heilig"
Vok1205$ = Methode2$(Latein$, Deutsch$)
GOTO BL1206

BL1206:
Latein$ = "poena"
Deutsch$ = "Strafe"
Genus$ = "F"
Genitiv$ = "POENAE"
Vok1206$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1207

BL1207:
Latein$ = "solvere"
Deutsch$ = "lîsen, zahlen"
Deutsch2$ = "zahlen, lîsen"
Vok1207$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1208

BL1208:
Latein$ = "schola"
Deutsch$ = "Schule"
Genus$ = "F"
Genitiv$ = "SCHOLAE"
Vok1208$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1209

BL1209:
Latein$ = "tabula"
Deutsch$ = "Tafel, Karte, GemÑlde"
Deutsch2$ = "Tafel, GemÑlde, Karte"
Genus$ = "F"
Genitiv$ = "TABULAE"
Vok1209$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1210

BL1210:
Latein$ = "mundus"
Deutsch$ = "Welt"
Genus$ = "M"
Genitiv$ = "MUNDI"
Vok1210$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1211

BL1211:
Latein$ = "magister"
Deutsch$ = "Lehrer"
Genus$ = "M"
Genitiv$ = "MAGISTRATI"
Vok1211$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1212

BL1212:
Latein$ = "oceanus"
Deutsch$ = "Ozean"
Genus$ = "M"
Genitiv$ = "OCEANI"
Vok1212$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1213

BL1213:
Latein$ = "terra"
Deutsch$ = "Land, Erde"
Deutsch2$ = "Erde, Land"
Genus$ = "F"
Genitiv$ = "TERRAE"
Vok1213$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1214

BL1214:
Latein$ = "recte (Adv.)"
Deutsch$ = "richtig"
Vok1214$ = Methode2$(Latein$, Deutsch$)
GOTO BL1215

BL1215:
Latein$ = "insula"
Deutsch$ = "Insel"
Genus$ = "F"
Genitiv$ = "INSULAE"
Vok1215$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1216

BL1216:
Latein$ = "situs, a, um"
Deutsch$ = "gelegen, liegend"
Deutsch2$ = "liegend, gelegen"
Vok1216$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1217

BL1217:
Latein$ = "monstrare"
Deutsch$ = "zeigen"
Vok1217$ = Methode2$(Latein$, Deutsch$)
GOTO BL1218

BL1218:
Latein$ = "nescire"
Deutsch$ = "nicht wissen"
Vok1218$ = Methode2$(Latein$, Deutsch$)
GOTO BL1219

BL1219:
Latein$ = "paene (Adv.)"
Deutsch$ = "fast, beinahe"
Deutsch2$ = "beinahe, fast"
Vok1219$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1220

BL1220:
Latein$ = "nauta"
Deutsch$ = "Seemann, Matrose"
Deutsch2$ = "Matrose, Seemann"
Genus$ = "M"
Genitiv$ = "NAUTAE"
Vok1220$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1221

BL1221:
Latein$ = "navigare"
Deutsch$ = "segeln (be)fahren"
Deutsch2$ = "(be)fahren, segeln"
Vok1221$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1222

BL1222:
Latein$ = "audere"
Deutsch$ = "wagen"
Vok1222$ = Methode2$(Latein$, Deutsch$)
GOTO BL1223

BL1223:
Latein$ = "meus, a, um"
Deutsch$ = "mein"
Vok1223$ = Methode2$(Latein$, Deutsch$)
GOTO BL1224

BL1224:
Latein$ = "extremus, a, um"
Deutsch$ = "der Ñu·erste"
Vok1224$ = Methode2$(Latein$, Deutsch$)
GOTO BL1225

BL1225:
Latein$ = "docere"
Deutsch$ = "lehren"
Vok1225$ = Methode2$(Latein$, Deutsch$)
GOTO BL1226

BL1226:
Latein$ = "provincia"
Deutsch$ = "Provinz"
Genus$ = "F"
Genitiv$ = "PROVINCIAE"
Vok1226$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO Ende12

Ende12:
IF Vok1201$ = "Falsch" THEN
GOTO BL1201
ELSEIF Vok1202$ = "Falsch" THEN
GOTO BL1202
ELSEIF Vok1203$ = "Falsch" THEN
GOTO BL1203
ELSEIF Vok1204$ = "Falsch" THEN
GOTO BL1204
ELSEIF Vok1205$ = "Falsch" THEN
GOTO BL1205
ELSEIF Vok1206$ = "Falsch" THEN
GOTO BL1206
ELSEIF Vok1207$ = "Falsch" THEN
GOTO BL1207
ELSEIF Vok1208$ = "Falsch" THEN
GOTO BL1208
ELSEIF Vok1209$ = "Falsch" THEN
GOTO BL1209
ELSEIF Vok1210$ = "Falsch" THEN
GOTO BL1210
ELSEIF Vok1211$ = "Falsch" THEN
GOTO BL1211
ELSEIF Vok1212$ = "Falsch" THEN
GOTO BL1212
ELSEIF Vok1213$ = "Falsch" THEN
GOTO BL1213
ELSEIF Vok1214$ = "Falsch" THEN
GOTO BL1214
ELSEIF Vok1215$ = "Falsch" THEN
GOTO BL1215
ELSEIF Vok1216$ = "Falsch" THEN
GOTO BL1216
ELSEIF Vok1217$ = "Falsch" THEN
GOTO BL1217
ELSEIF Vok1218$ = "Falsch" THEN
GOTO BL1218
ELSEIF Vok1219$ = "Falsch" THEN
GOTO BL1219
ELSEIF Vok1220$ = "Falsch" THEN
GOTO BL1220
ELSEIF Vok1221$ = "Falsch" THEN
GOTO BL1221
ELSEIF Vok1222$ = "Falsch" THEN
GOTO BL1222
ELSEIF Vok1223$ = "Falsch" THEN
GOTO BL1223
ELSEIF Vok1224$ = "Falsch" THEN
GOTO BL1224
ELSEIF Vok1225$ = "Falsch" THEN
GOTO BL1225
ELSEIF Vok1226$ = "Falsch" THEN
GOTO BL1226
ELSE GOTO Letztende12
END IF

Letztende12:
END SUB

SUB BlauLektion13
END SUB

SUB BlauLektion14
END SUB

SUB BlauLektion15

BL1501:
Latein$ = "asellus"
Deutsch$ = "Esel"
Genus$ = "M"
Genitiv$ = "ASELLI"
Vok1501$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1502

BL1502:
Latein$ = "leo"
Deutsch$ = "Lîwe"
Genus$ = "M"
Genitiv$ = "LEONIS"
Vok1502$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1503

BL1503:
Latein$ = "fugitivus, a, um"
Deutsch$ = "entflohen"
Vok1503$ = Methode2$(Latein$, Deutsch$)
GOTO BL1504

BL1504:
Latein$ = "se (Akk. u. Abl.)"
Deutsch$ = "sich"
Vok1504$ = Methode2$(Latein$, Deutsch$)
GOTO BL1505

BL1505:
Latein$ = "occultare"
Deutsch$ = "verbergen"
Vok1505$ = Methode2$(Latein$, Deutsch$)
GOTO BL1506

BL1506:
Latein$ = "bestia"
Deutsch$ = "Tier"
Genus$ = "F"
Genitiv$ = "BESTIAE"
Vok1506$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1507

BL1507:
Latein$ = "fuga"
Deutsch$ = "Flucht"
Genus$ = "F"
Genitiv$ = "FUGAE"
Vok1507$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1508

BL1508:
Latein$ = "saevus, a, um"
Deutsch$ = "wild"
Vok1508$ = Methode2$(Latein$, Deutsch$)
GOTO BL1509

BL1509:
Latein$ = "attentus, a, um"
Deutsch$ = "aufmerksam"
Vok1509$ = Methode2$(Latein$, Deutsch$)
GOTO BL1510

BL1510:
Latein$ = "pendere"
Deutsch$ = "(herab)hÑngen"
Vok1510$ = Methode2$(Latein$, Deutsch$)
GOTO BL1511

BL1511:
Latein$ = "inquit"
Deutsch$ = "sagt(e) er, sagt(e) sie"
Deutsch2$ = "sagt(e) sie, sagt(e) er"
Vok1511$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1512

BL1512:
Latein$ = "pellere"
Deutsch$ = "sto·en, schlagen, treiben"
Deutsch2$ = "sto·en, treiben, schlagen"
Vok1512$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1513

BL1513:
Latein$ = "incidere"
Deutsch$ = "hineinfallen, in etw. geraten, sich ereignen"
Deutsch2$ = "hineinfallen, sich ereignen, in etw. geraten"
Vok1513$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1514

BL1514:
Latein$ = "iterum (Adv.)"
Deutsch$ = "wieder, noch einmal"
Deutsch2$ = "noch einmal, wieder"
Vok1514$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1515

BL1515:
Latein$ = "accidere"
Deutsch$ = "sich ereignen"
Vok1515$ = Methode2$(Latein$, Deutsch$)
GOTO BL1516

BL1516:
Latein$ = "nihil"
Deutsch$ = "nichts"
Vok1516$ = Methode2$(Latein$, Deutsch$)
GOTO BL1517

BL1517:
Latein$ = "credere"
Deutsch$ = "glauben, anvertrauen"
Deutsch2$ = "anvertrauen, glauben"
Vok1517$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1518

BL1518:
Latein$ = "concurrere"
Deutsch$ = "zusammenlaufen"
Vok1518$ = Methode2$(Latein$, Deutsch$)
GOTO BL1519

BL1519:
Latein$ = "incautus, a, um"
Deutsch$ = "unvorsichtig"
Vok1519$ = Methode2$(Latein$, Deutsch$)
GOTO BL1520

BL1520:
Latein$ = "reperire"
Deutsch$ = "finden"
Vok1520$ = Methode2$(Latein$, Deutsch$)
GOTO BL1521

BL1521:
Latein$ = "errare"
Deutsch$ = "irren, umherirren"
Deutsch2$ = "umherirren, irren"
Vok1521$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1522

BL1522:
Latein$ = "addere"
Deutsch$ = "hinzufÅgen"
Vok1522$ = Methode2$(Latein$, Deutsch$)
GOTO BL1523

BL1523:
Latein$ = "praeter (m.Akk.)"
Deutsch$ = "au·er, an...vorbei"
Deutsch2$ = "an...vorbei, au·er"
Vok1523$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1524

BL1524:
Latein$ = "corvus"
Deutsch$ = "Rabe"
Genus$ = "M"
Genitiv$ = "CORVI"
Vok1524$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1525

BL1525:
Latein$ = "vulpecula"
Deutsch$ = "Fuchs"
Genus$ = "F"
Genitiv$ = "VULPECULAE"
Vok1525$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1526

BL1526:
Latein$ = "aliquando (Adv.)"
Deutsch$ = "einst, irgendwann"
Deutsch2$ = "irgendwann, einst"
Vok1526$ = Methode4$(Latein$, Deutsch$, Deutsch2$)

GOTO BL1527

BL1527:
Latein$ = "oculus"
Deutsch$ = "Auge"
Genus$ = "M"
Genitiv$ = "OCULI"
Vok1527$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1528

BL1528:
Latein$ = "ponere"
Deutsch$ = "setzen, stellen, legen"
Deutsch2$ = "setzen, legen, stellen"
Vok1528$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1529

BL1529:
Latein$ = "proximus, a, um"
Deutsch$ = "der nÑchste"
Vok1529$ = Methode2$(Latein$, Deutsch$)
GOTO BL1530

BL1530:
Latein$ = "excitare"
Deutsch$ = "aufscheuchen, wecken"
Deutsch2$ = "wecken, aufscheuchen"
Vok1530$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1531

BL1531:
Latein$ = "quam"
Deutsch$ = "wie, wie sehr"
Deutsch2$ = "wie sehr, wie"
Vok1531$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1532

BL1532:
Latein$ = "penna"
Deutsch$ = "Feder"
Genus$ = "F"
Genitiv$ = "PENNAE"
Vok1532$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO Ende15

Ende15:
IF Vok1501$ = "Falsch" THEN
GOTO BL1501
ELSEIF Vok1502$ = "Falsch" THEN
GOTO BL1502
ELSEIF Vok1503$ = "Falsch" THEN
GOTO BL1503
ELSEIF Vok1504$ = "Falsch" THEN
GOTO BL1504
ELSEIF Vok1505$ = "Falsch" THEN
GOTO BL1505
ELSEIF Vok1506$ = "Falsch" THEN
GOTO BL1506
ELSEIF Vok1507$ = "Falsch" THEN
GOTO BL1507
ELSEIF Vok1508$ = "Falsch" THEN
GOTO BL1508
ELSEIF Vok1509$ = "Falsch" THEN
GOTO BL1509
ELSEIF Vok1510$ = "Falsch" THEN
GOTO BL1510
ELSEIF Vok1511$ = "Falsch" THEN
GOTO BL1511
ELSEIF Vok1512$ = "Falsch" THEN
GOTO BL1512
ELSEIF Vok1513$ = "Falsch" THEN
GOTO BL1513
ELSEIF Vok1514$ = "Falsch" THEN
GOTO BL1514
ELSEIF Vok1515$ = "Falsch" THEN
GOTO BL1515
ELSEIF Vok1516$ = "Falsch" THEN
GOTO BL1516
ELSEIF Vok1517$ = "Falsch" THEN
GOTO BL1517
ELSEIF Vok1518$ = "Falsch" THEN
GOTO BL1518
ELSEIF Vok1519$ = "Falsch" THEN
GOTO BL1519
ELSEIF Vok1520$ = "Falsch" THEN
GOTO BL1520
ELSEIF Vok1521$ = "Falsch" THEN
GOTO BL1521
ELSEIF Vok1522$ = "Falsch" THEN
GOTO BL1522
ELSEIF Vok1523$ = "Falsch" THEN
GOTO BL1523
ELSEIF Vok1524$ = "Falsch" THEN
GOTO BL1524
ELSEIF Vok1525$ = "Falsch" THEN
GOTO BL1525
ELSEIF Vok1526$ = "Falsch" THEN
GOTO BL1526
ELSEIF Vok1527$ = "Falsch" THEN
GOTO BL1527
ELSEIF Vok1528$ = "Falsch" THEN
GOTO BL1528
ELSEIF Vok1529$ = "Falsch" THEN
GOTO BL1529
ELSEIF Vok1530$ = "Falsch" THEN
GOTO BL1530
ELSEIF Vok1531$ = "Falsch" THEN
GOTO BL1531
ELSEIF Vok1532$ = "Falsch" THEN
GOTO BL1532
ELSE GOTO Letztende15
END IF

Letztende15:
END SUB

SUB BlauLektion16

BL1601:
Latein$ = "regnare"
Deutsch$ = "regieren"
Vok1601$ = Methode2$(Latein$, Deutsch$)
GOTO BL1602

BL1602:
Latein$ = "honor"
Deutsch$ = "Ehre Ehrenamt"
Deutsch2$ = "Ehrenamt Ehre"
Genus$ = "M"
Genitiv$ = "HONORIS"
Vok1602$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1603

BL1603:
Latein$ = "apud (m.Akk.)"
Deutsch$ = "bei"
Vok1603$ = Methode2$(Latein$, Deutsch$)
GOTO BL1604

BL1604:
Latein$ = "augere"
Deutsch$ = "vermehren vergrî·ern"
Deutsch2$ = "vergrî·ern vermehren"
Vok1604$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1605

BL1605:
Latein$ = "aut; aut...aut"
Deutsch$ = "oder; entweder...oder"
Vok1605$ = Methode2$(Latein$, Deutsch$)
GOTO BL1606

BL1606:
Latein$ = "colere"
Deutsch$ = "(den Acker) bebauen verehren"
Deutsch2$ = "verehren (den Acker) bebauen"
Vok1606$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1607

BL1607:
Latein$ = "recensere"
Deutsch$ = "mustern"
Vok1607$ = Methode2$(Latein$, Deutsch$)
GOTO BL1608

BL1608:
Latein$ = "armatus, a, um"
Deutsch$ = "bewaffnet"
Vok1608$ = Methode2$(Latein$, Deutsch$)
GOTO BL1609

BL1609:
Latein$ = "cum (inversivum)"
Deutsch$ = "als (plîtzlich)"
Vok1609$ = Methode2$(Latein$, Deutsch$)
GOTO BL1610

BL1610:
Latein$ = "nimbus"
Deutsch$ = "Wolke"
Genus$ = "M"
Genitiv$ = "NIMBI"
Vok1610$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1611

BL1611:
Latein$ = "densus, a, um"
Deutsch$ = "dicht"
Vok1611$ = Methode2$(Latein$, Deutsch$)
GOTO BL1612

BL1612:
Latein$ = "vacuus, a, um"
Deutsch$ = "leer"
Vok1612$ = Methode2$(Latein$, Deutsch$)
GOTO BL1613

BL1613:
Latein$ = "dolor"
Deutsch$ = "Schmerz"
Genus$ = "M"
Genitiv$ = "DOLORIS"
Vok1613$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1614

BL1614:
Latein$ = "clarus, a, um"
Deutsch$ = "hell berÅhmt"
Deutsch2$ = "berÅhmt hell"
Vok1614$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1615

BL1615:
Latein$ = "invidere (m.Dat.)"
Deutsch$ = "(jemanden) beneiden"
Vok1615$ = Methode2$(Latein$, Deutsch$)
GOTO BL1616

BL1616:
Latein$ = "postridie (Adv.)"
Deutsch$ = "am folgenden Tag"
Vok1616$ = Methode2$(Latein$, Deutsch$)
GOTO BL1617

BL1617:
Latein$ = "fidus, a, um"
Deutsch$ = "treu zuverlÑssig"
Deutsch2$ = "zuverlÑssig treu"
Vok1617$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1618

BL1618:
Latein$ = "forte (Adv.)"
Deutsch$ = "zufÑllig"
Vok1618$ = Methode2$(Latein$, Deutsch$)
GOTO BL1619

BL1619:
Latein$ = "apparere"
Deutsch$ = "erscheinen offenkundig sein"
Deutsch2$ = "offenkundig sein erscheinen"
Vok1619$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1620

BL1620:
Latein$ = "animus"
Deutsch$ = "Geist Herz Mut"
Deutsch2$ = "Geist Mut Herz"
Genus$ = "M"
Genitiv$ = "ANIMI"
Vok1620$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1621

BL1621:
Latein$ = "tacere"
Deutsch$ = "schweigen verschweigen"
Deutsch2$ = "verschweigen schweigen"
Vok1621$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1622

BL1622:
Latein$ = "benigne (Adv.)"
Deutsch$ = "freundlich wohlwollend"
Deutsch2$ = "wohlwollend freundlich"
Vok1622$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1623

BL1623:
Latein$ = "advocare"
Deutsch$ = "herbeirufen"
Vok1623$ = Methode2$(Latein$, Deutsch$)
GOTO BL1624

BL1624:
Latein$ = "desinere"
Deutsch$ = "aufhîren"
Vok1624$ = Methode2$(Latein$, Deutsch$)
GOTO BL1625

BL1625:
Latein$ = "non modo...sed etiam"
Deutsch$ = "nicht nur...sondern auch"
Vok1625$ = Methode2$(Latein$, Deutsch$)
GOTO BL1626

BL1626:
Latein$ = "inimicus(,a)"
Deutsch$ = "Feind feindlich"
Deutsch2$ = "feindlich Feind"
Genus$ = "M"
Genitiv$ = "INIMICI"
Vok1626$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1627

BL1627:
Latein$ = "dubitare"
Deutsch$ = "zîgern zweifeln"
Deutsch2$ = "zweifeln zîgern"
Vok1627$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1628

BL1628:
Latein$ = "confirmare"
Deutsch$ = "stÑrken versichern"
Deutsch2$ = "versichern stÑrken"
Vok1628$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1629

BL1629:
Latein$ = "habere"
Deutsch$ = "haben halten"
Deutsch2$ = "halten haben"
Vok1629$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1630

BL1630:
Latein$ = "memoriam sacram habere"
Deutsch$ = "das Andenken heilig halten"
Vok1630$ = Methode2$(Latein$, Deutsch$)
GOTO Ende16

Ende16:
IF Vok1601$ = "Falsch" THEN
GOTO BL1601
ELSEIF Vok1602$ = "Falsch" THEN
GOTO BL1602
ELSEIF Vok1603$ = "Falsch" THEN
GOTO BL1603
ELSEIF Vok1604$ = "Falsch" THEN
GOTO BL1604
ELSEIF Vok1605$ = "Falsch" THEN
GOTO BL1605
ELSEIF Vok1606$ = "Falsch" THEN
GOTO BL1606
ELSEIF Vok1607$ = "Falsch" THEN
GOTO BL1607
ELSEIF Vok1608$ = "Falsch" THEN
GOTO BL1608
ELSEIF Vok1609$ = "Falsch" THEN
GOTO BL1609
ELSEIF Vok1610$ = "Falsch" THEN
GOTO BL1610
ELSEIF Vok1611$ = "Falsch" THEN
GOTO BL1611
ELSEIF Vok1612$ = "Falsch" THEN
GOTO BL1612
ELSEIF Vok1613$ = "Falsch" THEN
GOTO BL1613
ELSEIF Vok1614$ = "Falsch" THEN
GOTO BL1614
ELSEIF Vok1615$ = "Falsch" THEN
GOTO BL1615
ELSEIF Vok1616$ = "Falsch" THEN
GOTO BL1616
ELSEIF Vok1617$ = "Falsch" THEN
GOTO BL1617
ELSEIF Vok1618$ = "Falsch" THEN
GOTO BL1618
ELSEIF Vok1619$ = "Falsch" THEN
GOTO BL1619
ELSEIF Vok1620$ = "Falsch" THEN
GOTO BL1620
ELSEIF Vok1621$ = "Falsch" THEN
GOTO BL1621
ELSEIF Vok1622$ = "Falsch" THEN
GOTO BL1622
ELSEIF Vok1623$ = "Falsch" THEN
GOTO BL1623
ELSEIF Vok1624$ = "Falsch" THEN
GOTO BL1624
ELSEIF Vok1625$ = "Falsch" THEN
GOTO BL1625
ELSEIF Vok1626$ = "Falsch" THEN
GOTO BL1626
ELSEIF Vok1627$ = "Falsch" THEN
GOTO BL1627
ELSEIF Vok1628$ = "Falsch" THEN
GOTO BL1628
ELSEIF Vok1629$ = "Falsch" THEN
GOTO BL1629
ELSEIF Vok1630$ = "Falsch" THEN
GOTO BL1630
ELSE GOTO Letztende16
END IF

Letztende16:
END SUB

SUB BlauLektion17

BL1701:
Latein$ = "quod"
Deutsch$ = "da weil"
Deutsch2$ = "weil da"
Vok1701$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1702

BL1702:
Latein$ = "nisi"
Deutsch$ = "wenn nicht au·er"
Deutsch2$ = "au·er wenn nicht"
Vok1702$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1703

BL1703:
Latein$ = "mittere"
Deutsch$ = "schicken (los)lassen"
Deutsch2$ = "(los)lassen schicken"
Vok1703$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1704

BL1704:
Latein$ = "quamquam"
Deutsch$ = "obwohl obgleich"
Deutsch2$ = "obgleich obwohl"
Vok1704$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1705

BL1705:
Latein$ = "longe (Adv.)"
Deutsch$ = "weit"
Vok1705$ = Methode2$(Latein$, Deutsch$)
GOTO BL1706

BL1706:
Latein$ = "abesse"
Deutsch$ = "entfernt sein abwesend sein fehlen"
Deutsch2$ = "entfernt sein fehlen abwesend sein"
Vok1706$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1707

BL1707:
Latein$ = "ubi"
Deutsch$ = "sobald"
Vok1707$ = Methode2$(Latein$, Deutsch$)
GOTO BL1708

BL1708:
Latein$ = "postquam"
Deutsch$ = "nachdem"
Vok1708$ = Methode2$(Latein$, Deutsch$)
GOTO BL1709

BL1709:
Latein$ = "ut"
Deutsch$ = "wie"
Vok1709$ = Methode2$(Latein$, Deutsch$)
GOTO BL1710

BL1710:
Latein$ = "nondum"
Deutsch$ = "noch nicht"
Vok1710$ = Methode2$(Latein$, Deutsch$)
GOTO BL1711

BL1711:
Latein$ = "si"
Deutsch$ = "wenn"
Vok1711$ = Methode2$(Latein$, Deutsch$)
GOTO BL1712

BL1712:
Latein$ = "possunt"
Deutsch$ = "sie kînnen"
Vok1712$ = Methode2$(Latein$, Deutsch$)
GOTO BL1713

BL1713:
Latein$ = "morbus"
Deutsch$ = "Krankheit"
Genus$ = "M"
Genitiv$ = "MORBI"
Vok1713$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1714

BL1714:
Latein$ = "homo"
Deutsch$ = "Mensch"
Genus$ = "M"
Genitiv$ = "HOMINIS"
Vok1714$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1715

BL1715:
Latein$ = "inopia"
Deutsch$ = "Mangel"
Genus$ = "F"
Genitiv$ = "INOPIAE"
Vok1715$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1716

BL1716:
Latein$ = "arcere"
Deutsch$ = "abwehren fernhalten"
Deutsch2$ = "fernhalten abwehren"
Vok1716$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1717

BL1717:
Latein$ = "liber"
Deutsch$ = "frei"
Vok1717$ = Methode2$(Latein$, Deutsch$)
GOTO BL1718

BL1718:
Latein$ = "mercator"
Deutsch$ = "Kaufmann"
Genus$ = "M"
Genitiv$ = "MERCATORIS"
Vok1718$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1719

BL1719:
Latein$ = "licet"
Deutsch$ = "es ist erlaubt man kann"
Deutsch2$ = "man kann es ist erlaubt"
Vok1719$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1720

BL1720:
Latein$ = "cicada"
Deutsch$ = "Grille"
Genus$ = "F"
Genitiv$ = "CICADAE"
Vok1720$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1721

BL1721:
Latein$ = "formica"
Deutsch$ = "Ameise"
Genus$ = "F"
Genitiv$ = "FORMICAE"
Vok1721$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1722

BL1722:
Latein$ = "hiems"
Deutsch$ = "Winter"
Genus$ = "F"
Genitiv$ = "HIEMIS"
Vok1722$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1723

BL1723:
Latein$ = "praebere"
Deutsch$ = "gewÑhren geben zeigen"
Deutsch2$ = "gewÑhren zeigen geben"
Vok1723$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1724

BL1724:
Latein$ = "mollire"
Deutsch$ = "erweichen"
Vok1724$ = Methode2$(Latein$, Deutsch$)
GOTO BL1725

BL1725:
Latein$ = "merito (Adv.)"
Deutsch$ = "mit Recht"
Vok1725$ = Methode2$(Latein$, Deutsch$)
GOTO BL1726

BL1726:
Latein$ = "saltare"
Deutsch$ = "tanzen"
Vok1726$ = Methode2$(Latein$, Deutsch$)
GOTO Ende17

Ende17:
IF Vok1701$ = "Falsch" THEN
GOTO BL1701
ELSEIF Vok1702$ = "Falsch" THEN
GOTO BL1702
ELSEIF Vok1703$ = "Falsch" THEN
GOTO BL1703
ELSEIF Vok1704$ = "Falsch" THEN
GOTO BL1704
ELSEIF Vok1705$ = "Falsch" THEN
GOTO BL1705
ELSEIF Vok1706$ = "Falsch" THEN
GOTO BL1706
ELSEIF Vok1707$ = "Falsch" THEN
GOTO BL1707
ELSEIF Vok1708$ = "Falsch" THEN
GOTO BL1708
ELSEIF Vok1709$ = "Falsch" THEN
GOTO BL1709
ELSEIF Vok1710$ = "Falsch" THEN
GOTO BL1710
ELSEIF Vok1711$ = "Falsch" THEN
GOTO BL1711
ELSEIF Vok1712$ = "Falsch" THEN
GOTO BL1712
ELSEIF Vok1713$ = "Falsch" THEN
GOTO BL1713
ELSEIF Vok1714$ = "Falsch" THEN
GOTO BL1714
ELSEIF Vok1715$ = "Falsch" THEN
GOTO BL1715
ELSEIF Vok1716$ = "Falsch" THEN
GOTO BL1716
ELSEIF Vok1717$ = "Falsch" THEN
GOTO BL1717
ELSEIF Vok1718$ = "Falsch" THEN
GOTO BL1718
ELSEIF Vok1719$ = "Falsch" THEN
GOTO BL1719
ELSEIF Vok1720$ = "Falsch" THEN
GOTO BL1720
ELSEIF Vok1721$ = "Falsch" THEN
GOTO BL1721
ELSEIF Vok1722$ = "Falsch" THEN
GOTO BL1722
ELSEIF Vok1723$ = "Falsch" THEN
GOTO BL1723
ELSEIF Vok1724$ = "Falsch" THEN
GOTO BL1724
ELSEIF Vok1725$ = "Falsch" THEN
GOTO BL1725
ELSEIF Vok1726$ = "Falsch" THEN
GOTO BL1726
ELSE GOTO Letztende17
END IF

Letztende17:
END SUB

SUB BlauLektion18

BL1801:
Latein$ = "forum"
Deutsch$ = "Markt Marktplatz"
Deutsch2$ = "Marktplatz markt"
Genus$ = "N"
Genitiv$ = "FORI"
Vok1801$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1802

BL1802:
Latein$ = "duc!"
Deutsch$ = "fÅhre!"
Vok1802$ = Methode2$(Latein$, Deutsch$)
GOTO BL1803

BL1803:
Latein$ = "aedificium"
Deutsch$ = "GebÑude"
Genus$ = "N"
Genitiv$ = "AEDIFICII"
Vok1803$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1804

BL1804:
Latein$ = "descendere"
Deutsch$ = "hinabsteigen"
Vok1804$ = Methode2$(Latein$, Deutsch$)
GOTO BL1805

BL1805:
Latein$ = "templum"
Deutsch$ = "Tempel"
Genus$ = "N"
Genitiv$ = "TEMPLI"
Vok1805$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1806

BL1806:
Latein$ = "cum (iterativum)"
Deutsch$ = "(jedesmal) wenn"
Vok1806$ = Methode2$(Latein$, Deutsch$)
GOTO BL1807

BL1807:
Latein$ = "bellum"
Deutsch$ = "Krieg"
Genus$ = "N"
Genitiv$ = "BELLI"
Vok1807$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1808

BL1808:
Latein$ = "gerere"
Deutsch$ = "tragen (aus)fÅhren"
Deutsch2$ = "(aus)fÅhren tragen"
Vok1808$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1809

BL1809:
Latein$ = "prope (Adv.)"
Deutsch$ = "in der NÑhe beinahe"
Deutsch2$ = "beinahe in der NÑhe"
Vok1809$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1810

BL1810:
Latein$ = "rostrum"
Deutsch$ = "Schnabel"
Genus$ = "N"
Genitiv$ = "ROSTRI"
Vok1810$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1811

BL1811:
Latein$ = "adesse (m.Dat.)"
Deutsch$ = "beistehen helfen"
Deutsch2$ = "helfen beistehen"
Vok1811$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1812

BL1812:
Latein$ = "relinquere"
Deutsch$ = "zurÅcklassen verlassen"
Deutsch2$ = "verlassen zurÅcklassen"
Vok1812$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1813

BL1813:
Latein$ = "theatrum"
Deutsch$ = "Theater"
Genus$ = "N"
Genitiv$ = "THEATRI"
Vok1813$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1814

BL1814:
Latein$ = "nomen"
Deutsch$ = "Name"
Genus$ = "N"
Genitiv$ = "NOMINIS"
Vok1814$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1815

BL1815:
Latein$ = "dea"
Deutsch$ = "Gîttin"
Genus$ = "F"
Genitiv$ = "DEAE"
Vok1811$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1816

BL1816:
Latein$ = "poeta"
Deutsch$ = "Dichter"
Genus$ = "M"
Genitiv$ = "POETAE"
Vok1816$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1817

BL1817:
Latein$ = "recitare"
Deutsch$ = "vortragen vorlesen"
Deutsch2$ = "vorlesen vortragen"
Vok1817$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1818

BL1818:
Latein$ = "carmen"
Deutsch$ = "Spruch Gedicht Lied"
Deutsch2$ = "Spruch Lied Gedicht"
Genus$ = "N"
Genitiv$ = "CARMINIS"
Vok1818$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1819

BL1819:
Latein$ = "opus"
Deutsch$ = "Arbeit Werk"
Deutsch2$ = "Werk Arbeit"
Genus$ = "M"
Genitiv$ = "OPI"
Vok1819$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1820

BL1820:
Latein$ = "dum (meist m. PrÑs.)"
Deutsch$ = "wÑhrend"
Vok1820$ = Methode2$(Latein$, Deutsch$)
GOTO BL1821

BL1821:
Latein$ = "tempus"
Deutsch$ = "Zeit"
Genus$ = "N"
Genitiv$ = "TEMPORIS"
Vok1821$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1822

BL1822:
Latein$ = "antiquus, a, um"
Deutsch$ = "alt"
Vok1822$ = Methode2$(Latein$, Deutsch$)
GOTO BL1823

BL1823:
Latein$ = "raro (Adv.)"
Deutsch$ = "selten"
Vok1823$ = Methode2$(Latein$, Deutsch$)
GOTO BL1824

BL1824:
Latein$ = "interdum"
Deutsch$ = "manchmal"
Vok1824$ = Methode2$(Latein$, Deutsch$)
GOTO BL1825

BL1825:
Latein$ = "convenire"
Deutsch$ = "zusammenkommen vereinbaren"
Deutsch2$ = "vereinbaren zusammenkommen"
Vok1825$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1826

BL1826:
Latein$ = "maximus, a, um"
Deutsch$ = "der grî·te"
Vok1826$ = Methode2$(Latein$, Deutsch$)
GOTO BL1827

BL1827:
Latein$ = "optimus, a, um"
Deutsch$ = "der beste"
Vok1827$ = Methode2$(Latein$, Deutsch$)
GOTO Ende18

Ende18:
IF Vok1801$ = "Falsch" THEN
GOTO BL1801
ELSEIF Vok1802$ = "Falsch" THEN
GOTO BL1802
ELSEIF Vok1803$ = "Falsch" THEN
GOTO BL1803
ELSEIF Vok1804$ = "Falsch" THEN
GOTO BL1804
ELSEIF Vok1805$ = "Falsch" THEN
GOTO BL1805
ELSEIF Vok1806$ = "Falsch" THEN
GOTO BL1806
ELSEIF Vok1807$ = "Falsch" THEN
GOTO BL1807
ELSEIF Vok1808$ = "Falsch" THEN
GOTO BL1808
ELSEIF Vok1809$ = "Falsch" THEN
GOTO BL1809
ELSEIF Vok1810$ = "Falsch" THEN
GOTO BL1810
ELSEIF Vok1811$ = "Falsch" THEN
GOTO BL1811
ELSEIF Vok1812$ = "Falsch" THEN
GOTO BL1812
ELSEIF Vok1813$ = "Falsch" THEN
GOTO BL1813
ELSEIF Vok1814$ = "Falsch" THEN
GOTO BL1814
ELSEIF Vok1815$ = "Falsch" THEN
GOTO BL1815
ELSEIF Vok1816$ = "Falsch" THEN
GOTO BL1816
ELSEIF Vok1817$ = "Falsch" THEN
GOTO BL1817
ELSEIF Vok1818$ = "Falsch" THEN
GOTO BL1818
ELSEIF Vok1819$ = "Falsch" THEN
GOTO BL1819
ELSEIF Vok1820$ = "Falsch" THEN
GOTO BL1820
ELSEIF Vok1821$ = "Falsch" THEN
GOTO BL1821
ELSEIF Vok1822$ = "Falsch" THEN
GOTO BL1822
ELSEIF Vok1823$ = "Falsch" THEN
GOTO BL1823
ELSEIF Vok1824$ = "Falsch" THEN
GOTO BL1824
ELSEIF Vok1825$ = "Falsch" THEN
GOTO BL1825
ELSE GOTO Letztende18
END IF

Letztende18:
END SUB

SUB BlauLektion19

BL1901:
Latein$ = "rex"
Deutsch$ = "Kînig"
Genus$ = "M"
Genitiv$ = "REGI"
Vok1901$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1902

BL1902:
Latein$ = "ultimus, a, um"
Deutsch$ = "der letzte"
Vok1902$ = Methode2$(Latein$, Deutsch$)
GOTO BL1903

BL1904:
Latein$ = "laus"
Deutsch$ = "lob"
Genus$ = "F"
Genitiv$ = "LAUDIS"
Vok1904$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1905

BL1903:
Latein$ = "lex"
Deutsch$ = "Gesetz"
Genus$ = "F"
Genitiv$ = "LEGIS"
Vok1903$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1904

BL1905:
Latein$ = "sibi (Dat.)"
Deutsch$ = "sich"
Vok1905$ = Methode2$(Latein$, Deutsch$)
GOTO BL1906

BL1906:
Latein$ = "pax"
Deutsch$ = "Friede"
Genus$ = "F"
Genitiv$ = "PACIS"
Vok1906$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1907

BL1907:
Latein$ = "finitimus, a, um"
Deutsch$ = "benachbart, Nachbar"
Deutsch2$ = "Nachbar, benachbart"
Vok1907$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1908

BL1908:
Latein$ = "firmare"
Deutsch$ = "stÑrken, sichern, ermutigen"
Deutsch2$ = "stÑrken, ermutigen, sichern"
Vok1908$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1909

BL1909:
Latein$ = "salus"
Deutsch$ = "Wohlergehen, Rettung"
Deutsch2$ = "Rettung, Wohlergehen"
Genus$ = "F"
Genitiv$ = "SALUTIS"
Vok1909$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1910

BL1910:
Latein$ = "civitas"
Deutsch$ = "Staat, Gemeinde, BÅrgerrecht"
Deutsch2$ = "BÅrgerrecht, Staat, Gemeinde"
Genus$ = "F"
Genitiv$ = "CIVITATIS"
Vok1910$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1911

BL1911:
Latein$ = "aetas"
Deutsch$ = "Zeitalter, Lebensalter"
Deutsch2$ = "Lebensalter, Zeitalter"
Genus$ = "F"
Genitiv$ = "AETATIS"
Vok1911$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1912

BL1912:
Latein$ = "celeritas"
Deutsch$ = "Schnelligkeit"
Genus$ = "F"
Genitiv$ = "CELERITATIS"
Vok1912$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1913

BL1913:
Latein$ = "imperium"
Deutsch$ = "Befehl, Reich, Herrschaft"
Deutsch2$ = "Befehl, Herrschaft, Reich"
Genus$ = "N"
Genitiv$ = "IMPERII"
Vok1913$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1914

BL1914:
Latein$ = "septimus, a, um"
Deutsch$ = "der siebte"
Vok1914$ = Methode2$(Latein$, Deutsch$)
GOTO BL1915

BL1915:
Latein$ = "propter (m. Akk.)"
Deutsch$ = "wegen"
Vok1915$ = Methode2$(Latein$, Deutsch$)
GOTO BL1916

BL1916:
Latein$ = "superbia"
Deutsch$ = "Hochmut, Stolz"
Deutsch2$ = "Stolz, Hochmut"
Genus$ = "F"
Genitiv$ = "SUPERBIAE"
Vok1916$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1917

BL1917:
Latein$ = "servitus"
Deutsch$ = "Knechtschaft, Sklaverei"
Deutsch2$ = "Sklaverei, Knechtschaft"
Genus$ = "F"
Genitiv$ = "SERVITUTIS"
Vok1917$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1918

BL1918:
Latein$ = "libertas"
Deutsch$ = "Freiheit"
Genus$ = "F"
Genitiv$ = "LIBERTATIS"
Vok1918$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1919

BL1919:
Latein$ = "pes"
Deutsch$ = "Fu·"
Genus$ = "M"
Genitiv$ = "PEDIS"
Vok1919$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1920

BL1920:
Latein$ = "dux"
Deutsch$ = "AnfÅhrer"
Genus$ = "M"
Genitiv$ = "DUCIS"
Vok1920$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1921

BL1921:
Latein$ = "deinde (Adv.)"
Deutsch$ = "darauf, dann"
Deutsch2$ = "dann, darauf"
Vok1921$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1922

BL1922:
Latein$ = "vox"
Deutsch$ = "Stimme"
Genus$ = "F"
Genitiv$ = "VOCIS"
Vok1922$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1923

BL1923:
Latein$ = "summus, a, um"
Deutsch$ = "der oberste, der hîchste"
Deutsch2$ = "der hîchste, der oberste"
Vok1923$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1924

BL1924:
Latein$ = "defendere"
Deutsch$ = "verteidigen"
Vok1924$ = Methode2$(Latein$, Deutsch$)
GOTO BL1925

BL1925:
Latein$ = "plebs"
Deutsch$ = "Volk, Volksmenge"
Deutsch2$ = "Volksmenge, Volk"
Genus$ = "F"
Genitiv$ = "PLEBIS"
Vok1925$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL1926

BL1926:
Latein$ = "concordia"
Deutsch$ = "Eintracht"
Genus$ = "F"
Genitiv$ = "CONCORDIAE"
Vok1926$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1927

BL1927:
Latein$ = "opprimere"
Deutsch$ = "unterdrÅcken, Åberfallen"
Deutsch2$ = "Åberfallen, unterdrÅcken"
Vok1927$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1928

BL1928:
Latein$ = "oportet"
Deutsch$ = "es ist nîtig, man mu·"
Deutsch2$ = "man mu·, es ist nîtig"
Vok1928$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1929

BL1929:
Latein$ = "duodecim"
Deutsch$ = "zwîlf"
Vok1929$ = Methode2$(Latein$, Deutsch$)
GOTO BL1930

BL1930:
Latein$ = "collocare"
Deutsch$ = "aufstellen"
Vok1930$ = Methode2$(Latein$, Deutsch$)
GOTO Ende19

Ende19:
IF Vok1901$ = "Falsch" THEN
GOTO BL1901
ELSEIF Vok1902$ = "Falsch" THEN
GOTO BL1902
ELSEIF Vok1903$ = "Falsch" THEN
GOTO BL1903
ELSEIF Vok1904$ = "Falsch" THEN
GOTO BL1904
ELSEIF Vok1905$ = "Falsch" THEN
GOTO BL1905
ELSEIF Vok1906$ = "Falsch" THEN
GOTO BL1906
ELSEIF Vok1907$ = "Falsch" THEN
GOTO BL1907
ELSEIF Vok1908$ = "Falsch" THEN
GOTO BL1908
ELSEIF Vok1909$ = "Falsch" THEN
GOTO BL1909
ELSEIF Vok1910$ = "Falsch" THEN
GOTO BL1910
ELSEIF Vok1911$ = "Falsch" THEN
GOTO BL1911
ELSEIF Vok1912$ = "Falsch" THEN
GOTO BL1912
ELSEIF Vok1913$ = "Falsch" THEN
GOTO BL1913
ELSEIF Vok1914$ = "Falsch" THEN
GOTO BL1914
ELSEIF Vok1915$ = "Falsch" THEN
GOTO BL1915
ELSEIF Vok1916$ = "Falsch" THEN
GOTO BL1916
ELSEIF Vok1917$ = "Falsch" THEN
GOTO BL1917
ELSEIF Vok1918$ = "Falsch" THEN
GOTO BL1918
ELSEIF Vok1919$ = "Falsch" THEN
GOTO BL1919
ELSEIF Vok1920$ = "Falsch" THEN
GOTO BL1920
ELSEIF Vok1921$ = "Falsch" THEN
GOTO BL1921
ELSEIF Vok1922$ = "Falsch" THEN
GOTO BL1922
ELSEIF Vok1923$ = "Falsch" THEN
GOTO BL1923
ELSEIF Vok1924$ = "Falsch" THEN
GOTO BL1924
ELSEIF Vok1925$ = "Falsch" THEN
GOTO BL1925
ELSEIF Vok1926$ = "Falsch" THEN
GOTO BL1926
ELSEIF Vok1927$ = "Falsch" THEN
GOTO BL1927
ELSEIF Vok1928$ = "Falsch" THEN
GOTO BL1928
ELSEIF Vok1929$ = "Falsch" THEN
GOTO BL1929
ELSEIF Vok1930$ = "Falsch" THEN
GOTO BL1930
ELSE GOTO Letztende19
END IF

Letztende19:
END SUB

SUB BlauLektion2

BL0201:
Latein$ = "quem?"
Deutsch$ = "wen?"
Vok0201$ = Methode2$(Latein$, Deutsch$)
GOTO BL0202

BL0202:
Latein$ = "videre"
Deutsch$ = "sehen"
Vok0202$ = Methode2$(Latein$, Deutsch$)
GOTO BL0203

BL0204:
Latein$ = "orator"
Deutsch$ = "Redner"
Genus$ = "M"
Genitiv$ = "ORATOREM"
Vok0204$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0205

BL0205:
Latein$ = "laudare"
Deutsch$ = "loben"
Vok0205$ = Methode2$(Latein$, Deutsch$)
GOTO BL0206

BL0206:
Latein$ = "amittere"
Deutsch$ = "verlieren"
Vok0206$ = Methode2$(Latein$, Deutsch$)
GOTO BL0207

BL0207:
Latein$ = "quo?"
Deutsch$ = "wohin?"
Vok0207$ = Methode2$(Latein$, Deutsch$)
GOTO BL0208

BL0208:
Latein$ = "equus"
Deutsch$ = "Pferd"
Genus$ = "M"
Genitiv$ = "EQUI"
Vok0208$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0209

BL0209:
Latein$ = "ad (m. Akk.)"
Deutsch$ = "zu, nach, bei"
Deutsch2$ = "zu, bei, nach"
Vok0209$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0210

BL0203:
Latein$ = "sed"
Deutsch$ = "aber, sondern"
Deutsch2$ = "sondern, aber"
Vok0203$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0204

BL0210:
Latein$ = "hortus"
Deutsch$ = "Garten"
Genus$ = "M"
Genitiv$ = "HORTI"
Vok0210$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0211

BL0211:
Latein$ = "tum"
Deutsch$ = "dann, darauf"
Deutsch2$ = "darauf, dann"
Vok0211$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0212

BL0212:
Latein$ = "per (m. Akk.)"
Deutsch$ = "durch"
Vok0212$ = Methode2$(Latein$, Deutsch$)
GOTO BL0213

BL0213:
Latein$ = "porta"
Deutsch$ = "Tor"
Genus$ = "F"
Genitiv$ = "PORTAE"
Vok0213$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0214

BL0214:
Latein$ = "intrare"
Deutsch$ = "eintreten, betreten"
Deutsch2$ = "betreten, eintreten"
Vok0214$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0215

BL0215:
Latein$ = "clamare"
Deutsch$ = "rufen, schreien"
Deutsch2$ = "schreien, rufen"
Vok0215$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0216

BL0216:
Latein$ = "timere"
Deutsch$ = "fÅrchten, sich fÅrchten"
Deutsch2$ = "sich fÅrchten, fÅrchten"
Vok0216$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0217

BL0217:
Latein$ = "capere"
Deutsch$ = "fangen, nehmen, fassen"
Deutsch2$ = "fangen, fassen, nehmen"
Vok0217$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0218

BL0218:
Latein$ = "cena"
Deutsch$ = "Essen, Mahlzeit"
Deutsch2$ = "Mahlzeit, Essen"
Genus$ = "F"
Genitiv$ = "CENAE"
Vok0218$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0219

BL0219:
Latein$ = "ianua"
Deutsch$ = "TÅr"
Genus$ = "F"
Genitiv$ = "IANUAE"
Vok0219$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0220

BL0220:
Latein$ = "in (m. Akk.)"
Deutsch$ = "in...(hinein)"
Vok0220$ = Methode2$(Latein$, Deutsch$)
GOTO BL0221

BL0221:
Latein$ = "villa"
Deutsch$ = "Landhaus"
Genus$ = "F"
Genitiv$ = "VILLAE"
Vok0221$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0222

BL0222:
Latein$ = "gallus"
Deutsch$ = "Hahn"
Genus$ = "M"
Genitiv$ = "GALLI"
Vok0222$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0223

BL0223:
Latein$ = "aula"
Deutsch$ = "Hof"
Genus$ = "F"
Genitiv$ = "AULAE"
Vok0223$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0224

BL0224:
Latein$ = "dies"
Deutsch$ = "Tag"
Genus$ = "M"
Genitiv$ = "DIEM"
Vok0224$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0225

BL0225:
Latein$ = "salutare"
Deutsch$ = "grÅ·en, begrÅ·en"
Deutsch2$ = "begrÅ·en, grÅ·en"
Vok0225$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0226

BL0226:
Latein$ = "cibus"
Deutsch$ = "Speise, Essen, Futter"
Deutsch2$ = "Futter, Speise, Essen"
Genus$ = "M"
Genitiv$ = "CIBI"
Vok0226$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0227

BL0227:
Latein$ = "apportare"
Deutsch$ = "herbei tragen, bringen"
Deutsch2$ = "bringen, herbei tragen"
Vok0227$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0228

BL0228:
Latein$ = "volare"
Deutsch$ = "fliegen"
Vok0228$ = Methode2$(Latein$, Deutsch$)
GOTO BL0229

BL0229:
Latein$ = "devorare"
Deutsch$ = "fressen"
Vok0229$ = Methode2$(Latein$, Deutsch$)
GOTO BL0230

BL0230:
Latein$ = "fugere"
Deutsch$ = "fliehen, vermeiden"
Deutsch2$ = "vermeiden, fliehen"
Vok0230$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0232

BL0232:
Latein$ = "terrere"
Deutsch$ = "erschrecken"
Vok0232$ = Methode2$(Latein$, Deutsch$)
GOTO BL0233

BL0233:
Latein$ = "vituperare"
Deutsch$ = "tadeln"
Vok0233$ = Methode2$(Latein$, Deutsch$)
GOTO Ende02

Ende02:
IF Vok0201$ = "Falsch" THEN
GOTO BL0201
ELSEIF Vok0202$ = "Falsch" THEN
GOTO BL0202
ELSEIF Vok0203$ = "Falsch" THEN
GOTO BL0203
ELSEIF Vok0204$ = "Falsch" THEN
GOTO BL0204
ELSEIF Vok0205$ = "Falsch" THEN
GOTO BL0205
ELSEIF Vok0206$ = "Falsch" THEN
GOTO BL0206
ELSEIF Vok0207$ = "Falsch" THEN
GOTO BL0207
ELSEIF Vok0208$ = "Falsch" THEN
GOTO BL0208
ELSEIF Vok0209$ = "Falsch" THEN
GOTO BL0209
ELSEIF Vok0210$ = "Falsch" THEN
GOTO BL0210
ELSEIF Vok0211$ = "Falsch" THEN
GOTO BL0211
ELSEIF Vok0212$ = "Falsch" THEN
GOTO BL0212
ELSEIF Vok0213$ = "Falsch" THEN
GOTO BL0213
ELSEIF Vok0214$ = "Falsch" THEN
GOTO BL0214
ELSEIF Vok0215$ = "Falsch" THEN
GOTO BL0215
ELSEIF Vok0216$ = "Falsch" THEN
GOTO BL0216
ELSEIF Vok0217$ = "Falsch" THEN
GOTO BL0217
ELSEIF Vok0218$ = "Falsch" THEN
GOTO BL0218
ELSEIF Vok0219$ = "Falsch" THEN
GOTO BL0219
ELSEIF Vok0220$ = "Falsch" THEN
GOTO BL0220
ELSEIF Vok0221$ = "Falsch" THEN
GOTO BL0221
ELSEIF Vok0222$ = "Falsch" THEN
GOTO BL0222
ELSEIF Vok0223$ = "Falsch" THEN
GOTO BL0223
ELSEIF Vok0224$ = "Falsch" THEN
GOTO BL0224
ELSEIF Vok0225$ = "Falsch" THEN
GOTO BL0225
ELSEIF Vok0226$ = "Falsch" THEN
GOTO BL0226
ELSEIF Vok0227$ = "Falsch" THEN
GOTO BL0227
ELSEIF Vok0228$ = "Falsch" THEN
GOTO BL0228
ELSEIF Vok0229$ = "Falsch" THEN
GOTO BL0229
ELSEIF Vok0230$ = "Falsch" THEN
GOTO BL0230
ELSEIF Vok0232$ = "Falsch" THEN
GOTO BL0232
ELSEIF Vok0233$ = "Falsch" THEN
GOTO BL0233
ELSE GOTO Letztende02
END IF

Letztende02:
END SUB

SUB BlauLektion20

BL2001:
Latein$ = "oppidum"
Deutsch$ = "Stadt"
Genus$ = "N"
Genitiv$ = "OPPIDA"
Vok2001$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2002

BL2002:
Latein$ = "regere"
Deutsch$ = "regieren, lenken"
Deutsch2$ = "lenken, regieren"
Vok2002$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2003

BL2003:
Latein$ = "oraculum"
Deutsch$ = "Orakel"
Genus$ = "N"
Genitiv$ = "ORACULA"
Vok2003$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2004

BL2004:
Latein$ = "metuere"
Deutsch$ = "fÅrchten"
Vok2004$ = Methode2$(Latein$, Deutsch$)
GOTO BL2005

BL2005:
Latein$ = "secum"
Deutsch$ = "mit sich"
Vok2005$ = Methode2$(Latein$, Deutsch$)
GOTO BL2006

BL2006:
Latein$ = "iudicium"
Deutsch$ = "Gericht, Urteil"
Deutsch2$ = "Urteil, Gericht"
Genus$ = "N"
Genitiv$ = "IUDICIA"
Vok2006$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2007

BL2007:
Latein$ = "adducere"
Deutsch$ = "heranfÅhren"
Vok2007$ = Methode2$(Latein$, Deutsch$)
GOTO BL2008

BL2008:
Latein$ = "malum"
Deutsch$ = "Apfel"
Genus$ = "N"
Genitiv$ = "MALA"
Vok2008$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2009

BL2009:
Latein$ = "iudicare"
Deutsch$ = "urteilen, richten"
Deutsch2$ = "richten, urteilen"
Vok2009$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2010

BL2010:
Latein$ = "alter, altera, alterum"
Deutsch$ = "der eine, der andere, der zweite"
Deutsch2$ = "der eine, der zweite, der andere"
Vok2010$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2011

BL2011:
Latein$ = "tertius, a, um"
Deutsch$ = "der dritte"
Vok2011$ = Methode2$(Latein$, Deutsch$)
GOTO BL2012

BL2012:
Latein$ = "promittere"
Deutsch$ = "versprechen"
Vok2012$ = Methode2$(Latein$, Deutsch$)
GOTO BL2013

BL2013:
Latein$ = "amor"
Deutsch$ = "Liebe"
Genus$ = "M"
Genitiv$ = "ARMORIS"
Vok2013$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2014

BL2014:
Latein$ = "accipere"
Deutsch$ = "annehmen, empfangen"
Deutsch2$ = "empfangen, annehmen"
Vok2014$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2015

BL2015:
Latein$ = "interesse (m. Dat.)"
Deutsch$ = "teilnehmen an, dabeisein"
Deutsch2$ = "dabeisein, teilnehmen an"
Vok2015$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2016

BL2016:
Latein$ = "animadvertere"
Deutsch$ = "bemerken"
Vok2016$ = Methode2$(Latein$, Deutsch$)
GOTO BL2017

BL2017:
Latein$ = "ligneus a, um"
Deutsch$ = "hîlzern"
Vok2017$ = Methode2$(Latein$, Deutsch$)
GOTO BL2018

BL2018:
Latein$ = "oppugnare "
Deutsch$ = "bestÅrmen"
Vok2018$ = Methode2$(Latein$, Deutsch$)
GOTO BL2019

BL2019:
Latein$ = "decem"
Deutsch$ = "zehn"
Vok2019$ = Methode2$(Latein$, Deutsch$)
GOTO BL2020

BL2020:
Latein$ = "virtus"
Deutsch$ = "Tapferkeit, TÅchtigkeit"
Deutsch2$ = "TÅchtigkeit, Tapferkeit"
Genus$ = "F"
Genitiv$ = "VIRTUTIS"
Vok2020$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2021

BL2021:
Latein$ = "callidus, a, um"
Deutsch$ = "schlau"
Vok2021$ = Methode2$(Latein$, Deutsch$)
GOTO BL2022

BL2022:
Latein$ = "litus"
Deutsch$ = "Strand, KÅste"
Deutsch2$ = "KÅste, Strand"
Genus$ = "N"
Genitiv$ = "LITORIS"
Vok2022$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2023

BL2023:
Latein$ = "nox"
Deutsch$ = "Nacht"
Genus$ = "F"
Genitiv$ = "NOCTIS"
Vok2023$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO Ende20

Ende20:
IF Vok2001$ = "Falsch" THEN
GOTO BL2001
ELSEIF Vok2002$ = "Falsch" THEN
GOTO BL2002
ELSEIF Vok2003$ = "Falsch" THEN
GOTO BL2003
ELSEIF Vok2004$ = "Falsch" THEN
GOTO BL2004
ELSEIF Vok2005$ = "Falsch" THEN
GOTO BL2005
ELSEIF Vok2006$ = "Falsch" THEN
GOTO BL2006
ELSEIF Vok2007$ = "Falsch" THEN
GOTO BL2007
ELSEIF V2k2008$ = "Falsch" THEN
GOTO BL2008
ELSEIF Vok2009$ = "Falsch" THEN
GOTO BL2009
ELSEIF Vok2010$ = "Falsch" THEN
GOTO BL2010
ELSEIF Vok2011$ = "Falsch" THEN
GOTO BL2011
ELSEIF Vok2012$ = "Falsch" THEN
GOTO BL2012
ELSEIF Vok2013$ = "Falsch" THEN
GOTO BL2013
ELSEIF Vok2014$ = "Falsch" THEN
GOTO BL2014
ELSEIF Vok2015$ = "Falsch" THEN
GOTO BL2015
ELSEIF Vok2016$ = "Falsch" THEN
GOTO BL2016
ELSEIF Vok2017$ = "Falsch" THEN
GOTO BL2017
ELSEIF Vok2018$ = "Falsch" THEN
GOTO BL2018
ELSEIF Vok2019$ = "Falsch" THEN
GOTO BL2019
ELSEIF Vok2020$ = "Falsch" THEN
GOTO BL2020
ELSEIF Vok2021$ = "Falsch" THEN
GOTO BL2021
ELSEIF Vok2022$ = "Falsch" THEN
GOTO BL2022
ELSEIF Vok2023$ = "Falsch" THEN
GOTO BL2023
ELSE GOTO Letztende20
END IF

Letztende20:
END SUB

SUB BlauLektion21

BL2101:
Latein$ = "sacerdos"
Deutsch$ = "Priester"
Genus$ = "M"
Genitiv$ = "SACERDOTIS"
Vok2101$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2102

BL2102:
Latein$ = "periculum"
Deutsch$ = "Gefahr"
Genus$ = "N"
Genitiv$ = "PERICULI"
Vok2102$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2103

BL2103:
Latein$ = "donum"
Deutsch$ = "Geschenk"
Genus$ = "N"
Genitiv$ = "DONI"
Vok2103$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2104

BL2104:
Latein$ = "primo (Adv.)"
Deutsch$ = "anfangs, zuerst"
Deutsch2$ = "zuerst, anfangs"
Vok2104$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2105

BL2105:
Latein$ = "fides"
Deutsch$ = "Treue, Glauben, Vertrauen"
Deutsch2$ = "Treue, Glauben, Vertrauen"
Genus$ = "F"
Genitiv$ = "FIDEI"
Vok2105$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2106

BL2106:
Latein$ = "sententia"
Deutsch$ = "Meinung"
Genus$ = "F"
Genitiv$ = "SENTENTIAE"
Vok2106$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2107

BL2107:
Latein$ = "verbum"
Deutsch$ = "Wort"
Genus$ = "N"
Genitiv$ = "VERBI"
Vok2107$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2108

BL2108:
Latein$ = "longitudo"
Deutsch$ = "LÑnge"
Genus$ = "F"
Genitiv$ = "LONGITUDINIS"
Vok2108$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2109

BL2109:
Latein$ = "omen"
Deutsch$ = "Vorzeichen"
Genus$ = "N"
Genitiv$ = "OMINIS"
Vok2109$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2110

BL2110:
Latein$ = "expugnare"
Deutsch$ = "erobern"
Vok2110$ = Methode2$(Latein$, Deutsch$)
GOTO BL2111

BL2111:
Latein$ = "atque oder ac"
Deutsch$ = "und"
Vok2111$ = Methode2$(Latein$, Deutsch$)
GOTO BL2112

BL2112:
Latein$ = "dormire"
Deutsch$ = "schlafen"
Vok2112$ = Methode2$(Latein$, Deutsch$)
GOTO BL2113

BL2113:
Latein$ = "claum (Adv.)"
Deutsch$ = "heimlich"
Vok2113$ = Methode2$(Latein$, Deutsch$)
GOTO BL2114

BL2114:
Latein$ = "delere"
Deutsch$ = "zerstîren, vernichten"
Deutsch2$ = "vernichten, zerstîren"
Vok2114$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2115

BL2115:
Latein$ = "ager"
Deutsch$ = "Feld, (Acker)land"
Deutsch2$ = "(Acker)land, Feld"
Genus$ = "M"
Genitiv$ = "AGRI"
Vok2115$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2116

BL2116:
Latein$ = "vastare"
Deutsch$ = "verwÅsten"
Vok2116$ = Methode2$(Latein$, Deutsch$)
GOTO BL2117

BL2117:
Latein$ = "robur"
Deutsch$ = "Kraft"
Genus$ = "N"
Genitiv$ = "ROBORIS"
Vok2117$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2118

BL2118:
Latein$ = "gladius"
Deutsch$ = "Schwert"
Genus$ = "M"
Genitiv$ = "GLADII"
Vok2118$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2119

BL2119:
Latein$ = "sagitta"
Deutsch$ = "Pfeil"
Genus$ = "F"
Genitiv$ = "SAGITTAE"
Vok2119$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2120

BL2120:
Latein$ = "arma (Pl.)"
Deutsch$ = "Waffen"
Genus$ = "N"
Genitiv$ = "ARMORUM"
Vok2120$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2121

BL2121:
Latein$ = "vestigium"
Deutsch$ = "Spur"
Genus$ = "N"
Genitiv$ = "VESTIGII"
Vok2121$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2122

BL2122:
Latein$ = "durus, a, um"
Deutsch$ = "hart"
Vok2122$ = Methode2$(Latein$, Deutsch$)
GOTO BL2123

BL2123:
Latein$ = "frangere"
Deutsch$ = "(etwas) zerbrechen"
Vok2123$ = Methode2$(Latein$, Deutsch$)
GOTO BL2124

BL2124:
Latein$ = "caput"
Deutsch$ = "Kopf, Hauptstadt"
Deutsch2$ = "Hauptstadt, Kopf"
Genus$ = "N"
Genitiv$ = "CAPITIS"
Vok2124$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2125

BL2125:
Latein$ = "consilium"
Deutsch$ = "Rat, Ratsversammlung, Absicht"
Deutsch2$ = "Rat, Ratsversammlung, Absicht"
Genus$ = "N"
Genitiv$ = "CONSILII"
Vok2125$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO Ende21

Ende21:
IF Vok2101$ = "Falsch" THEN
GOTO BL2101
ELSEIF Vok2102$ = "Falsch" THEN
GOTO BL2102
ELSEIF Vok2103$ = "Falsch" THEN
GOTO BL2103
ELSEIF Vok2104$ = "Falsch" THEN
GOTO BL2104
ELSEIF Vok2105$ = "Falsch" THEN
GOTO BL2105
ELSEIF Vok2106$ = "Falsch" THEN
GOTO BL2106
ELSEIF Vok2107$ = "Falsch" THEN
GOTO BL2107
ELSEIF V2k2108$ = "Falsch" THEN
GOTO BL2108
ELSEIF Vok2109$ = "Falsch" THEN
GOTO BL2109
ELSEIF Vok2110$ = "Falsch" THEN
GOTO BL2110
ELSEIF Vok2111$ = "Falsch" THEN
GOTO BL2111
ELSEIF Vok2112$ = "Falsch" THEN
GOTO BL2112
ELSEIF Vok2113$ = "Falsch" THEN
GOTO BL2113
ELSEIF Vok2114$ = "Falsch" THEN
GOTO BL2114
ELSEIF Vok2115$ = "Falsch" THEN
GOTO BL2115
ELSEIF Vok2116$ = "Falsch" THEN
GOTO BL2116
ELSEIF Vok2117$ = "Falsch" THEN
GOTO BL2117
ELSEIF Vok2118$ = "Falsch" THEN
GOTO BL2118
ELSEIF Vok2119$ = "Falsch" THEN
GOTO BL2119
ELSEIF Vok2120$ = "Falsch" THEN
GOTO BL2120
ELSEIF Vok2121$ = "Falsch" THEN
GOTO BL2121
ELSEIF Vok2122$ = "Falsch" THEN
GOTO BL2122
ELSEIF Vok2123$ = "Falsch" THEN
GOTO BL2123
ELSEIF Vok2124$ = "Falsch" THEN
GOTO BL2124
ELSEIF Vok2125$ = "Falsch" THEN
GOTO BL2125
ELSE GOTO Letztende21
END IF

Letztende21:
END SUB

SUB BlauLektion22

BL2201:
Latein$ = "qui quae quod"
Deutsch$ = "der, die, das, welcher, welche, welches, wer, was"
Deutsch2$ = "der, die, das, wer, was, welcher, welche, welches"
Vok2201$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2202

BL2202:
Latein$ = "cottidie (Adv.)"
Deutsch$ = "tÑglich"
Vok2202$ = Methode2$(Latein$, Deutsch$)
GOTO BL2203

BL2203:
Latein$ = "gaudium"
Deutsch$ = "Freude"
Genus$ = "N"
Genitiv$ = "GAUDII"
Vok2203$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2204

BL2204:
Latein$ = "causa"
Deutsch$ = "Grund"
Genus$ = "F"
Genitiv$ = "CAUSAE"
Vok2204$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2205

BL2205:
Latein$ = "virga"
Deutsch$ = "Stab"
Genus$ = "F"
Genitiv$ = "VIRGAE"
Vok2205$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2206

BL2206:
Latein$ = "tenere"
Deutsch$ = "halten"
Vok2206$ = Methode2$(Latein$, Deutsch$)
GOTO BL2207

BL2207:
Latein$ = "demonstrare"
Deutsch$ = "zeigen"
Vok2207$ = Methode2$(Latein$, Deutsch$)
GOTO BL2208

BL2208:
Latein$ = "coniungere"
Deutsch$ = "verbinden"
Vok2208$ = Methode2$(Latein$, Deutsch$)
GOTO BL2209

BL2209:
Latein$ = "corpus"
Deutsch$ = "Kîrper"
Genus$ = "N"
Genitiv$ = "CORPORIS"
Vok2209$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2210

BL2210:
Latein$ = "amicitia"
Deutsch$ = "Freundschaft"
Genus$ = "F"
Genitiv$ = "AMICITIAE"
Vok2210$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2211

BL2211:
Latein$ = "discere"
Deutsch$ = "lernen"
Vok2211$ = Methode2$(Latein$, Deutsch$)
GOTO BL2212

BL2212:
Latein$ = "sapientia"
Deutsch$ = "Klugheit, Weisheit"
Deutsch2$ = "Weisheit, Klugheit"
Genus$ = "F"
Genitiv$ = "SAPIENTIAE"
Vok2212$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2213

BL2213:
Latein$ = "in numero habere"
Deutsch$ = "zÑhlen zu"
Vok2213$ = Methode2$(Latein$, Deutsch$)
GOTO BL2214

BL2214:
Latein$ = "Romae"
Deutsch$ = "in Rom"
Vok2214$ = Methode2$(Latein$, Deutsch$)
GOTO BL2215

BL2215:
Latein$ = "stabulum"
Deutsch$ = "Stall"
Genus$ = "N"
Genitiv$ = "STABULI"
Vok2215$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2216

BL2216:
Latein$ = "imponere"
Deutsch$ = "darauflegen, auferlegen"
Deutsch2$ = "auferlegen, darauflegen"
Vok2216$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2217

BL2217:
Latein$ = "divitiae"
Deutsch$ = "Reichtum"
Genus$ = "F"
Genitiv$ = "DIVITIARUM"
Vok2217$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2218

BL2218:
Latein$ = "possidere"
Deutsch$ = "besitzen"
Vok2218$ = Methode2$(Latein$, Deutsch$)
GOTO BL2219

BL2219:
Latein$ = "immensus, a, um"
Deutsch$ = "unerme·lich"
Vok2219$ = Methode2$(Latein$, Deutsch$)
GOTO BL2220

BL2220:
Latein$ = "nemo (! ! ! DATIV ! ! !) "
Deutsch$ = "niemand"
Genus$ = "M"
Genitiv$ = "NEMINI"
Vok2220$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2221

BL2221:
Latein$ = "movere"
Deutsch$ = "bewegen, veranlassen"
Deutsch2$ = "veranlassen, bewegen"
Vok2221$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2222

BL2222:
Latein$ = "removere"
Deutsch$ = "entfernen"
Vok2222$ = Methode2$(Latein$, Deutsch$)
GOTO BL2223

BL2223:
Latein$ = "regio"
Deutsch$ = "Richtung, Gegend"
Deutsch2$ = "Gegend, Richtung"
Genus$ = "F"
Genitiv$ = "REGIONIS"
Vok2223$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2224

BL2224:
Latein$ = "tantus, a, um"
Deutsch$ = "so gro·"
Vok2224$ = Methode2$(Latein$, Deutsch$)
GOTO BL2225

BL2225:
Latein$ = "modus"
Deutsch$ = "Art und Weise"
Vok2225$ = Methode2$(Latein$, Deutsch$)
GOTO BL2226

BL2226:
Latein$ = "conficere"
Deutsch$ = "verfertigen, beenden"
Deutsch2$ = "beenden, verfertigen"
Vok2226$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2227

BL2227:
Latein$ = "aqua"
Deutsch$ = "Wasser"
Genus$ = "F"
Genitiv$ = "AQUAE"
Vok2227$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2228

BL2228:
Latein$ = "flumen"
Deutsch$ = "Flu·"
Genus$ = "N"
Genitiv$ = "FLUMINIS"
Vok2228$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO Ende22

Ende22:
IF Vok2201$ = "Falsch" THEN
GOTO BL2201
ELSEIF Vok2202$ = "Falsch" THEN
GOTO BL2202
ELSEIF Vok2203$ = "Falsch" THEN
GOTO BL2203
ELSEIF Vok2204$ = "Falsch" THEN
GOTO BL2204
ELSEIF Vok2205$ = "Falsch" THEN
GOTO BL2205
ELSEIF Vok2206$ = "Falsch" THEN
GOTO BL2206
ELSEIF Vok2207$ = "Falsch" THEN
GOTO BL2207
ELSEIF V2k2208$ = "Falsch" THEN
GOTO BL2208
ELSEIF Vok2209$ = "Falsch" THEN
GOTO BL2209
ELSEIF Vok2210$ = "Falsch" THEN
GOTO BL2210
ELSEIF Vok2211$ = "Falsch" THEN
GOTO BL2211
ELSEIF Vok2212$ = "Falsch" THEN
GOTO BL2212
ELSEIF Vok2213$ = "Falsch" THEN
GOTO BL2213
ELSEIF Vok2214$ = "Falsch" THEN
GOTO BL2214
ELSEIF Vok2215$ = "Falsch" THEN
GOTO BL2215
ELSEIF Vok2216$ = "Falsch" THEN
GOTO BL2216
ELSEIF Vok2217$ = "Falsch" THEN
GOTO BL2217
ELSEIF Vok2218$ = "Falsch" THEN
GOTO BL2218
ELSEIF Vok2219$ = "Falsch" THEN
GOTO BL2219
ELSEIF Vok2220$ = "Falsch" THEN
GOTO BL2220
ELSEIF Vok2221$ = "Falsch" THEN
GOTO BL2221
ELSEIF Vok2222$ = "Falsch" THEN
GOTO BL2222
ELSEIF Vok2223$ = "Falsch" THEN
GOTO BL2223
ELSEIF Vok2224$ = "Falsch" THEN
GOTO BL2224
ELSEIF Vok2225$ = "Falsch" THEN
GOTO BL2225
ELSEIF Vok2226$ = "Falsch" THEN
GOTO BL2226
ELSEIF Vok2227$ = "Falsch" THEN
GOTO BL2227
ELSEIF Vok2228$ = "Falsch" THEN
GOTO BL2228
ELSE GOTO Letztende22
END IF

Letztende22:
END SUB

SUB BlauLektion23

BL2301:
Latein$ = "septem"
Deutsch$ = "sieben"
Vok2301$ = Methode2$(Latein$, Deutsch$)
GOTO BL2302

BL2302:
Latein$ = "regina"
Deutsch$ = "Kînigin"
Genus$ = "F"
Genitiv$ = "REGINAE"
Vok2302$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2303

BL2303:
Latein$ = "neglegere"
Deutsch$ = "nicht beachten, vernachlÑssigen"
Deutsch2$ = "vernachlÑssigen, nicht beachten"
Vok2303$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2304

BL2304:
Latein$ = "ne (m. Konj.)"
Deutsch$ = "da· nicht, damit nicht"
Deutsch2$ = "damit nicht, da· nicht"
Vok2304$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2305

BL2305:
Latein$ = "statua"
Deutsch$ = "Statue, Standbild"
Deutsch2$ = "Standbild, Statue"
Genus$ = "F"
Genitiv$ = "STATUAE"
Vok2305$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2306

BL2306:
Latein$ = "piger, pigra, pigrum"
Deutsch$ = "trÑge"
Vok2306$ = Methode2$(Latein$, Deutsch$)
GOTO BL2307

BL2307:
Latein$ = "studium"
Deutsch$ = "Eifer, BeschÑftigung"
Deutsch2$ = "BeschÑftigung, Eifer"
Genus$ = "N"
Genitiv$ = "STUDII"
Vok2307$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2308

BL2308:
Latein$ = "gratus, grata, gratum"
Deutsch$ = "angenehm, dankbar"
Deutsch2$ = "dankbar, angenehm"
Vok2308$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2309

BL2309:
Latein$ = "utinam (m. Konj.)"
Deutsch$ = "hoffentlich, wenn doch"
Deutsch2$ = "wenn doch, hoffentlich"
Vok2309$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2310

BL2310:
Latein$ = "propitius, propitia, propitium"
Deutsch$ = "gnÑdig"
Vok2310$ = Methode2$(Latein$, Deutsch$)
GOTO BL2311

BL2311:
Latein$ = "adversus, adversa, adversum"
Deutsch$ = "zugewandt, feindlich"
Deutsch2$ = "feindlich, zugewandt"
Vok2311$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2312

BL2312:
Latein$ = "advenire"
Deutsch$ = "ankommen"
Vok2312$ = Methode2$(Latein$, Deutsch$)
GOTO BL2313

BL2313:
Latein$ = "comparare"
Deutsch$ = "beschaffen, vergleichen"
Deutsch2$ = "vergleichen, beschaffen"
Vok2313$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2314

BL2314:
Latein$ = "impius, impia, impium"
Deutsch$ = "gottlos, frevelhaft"
Deutsch2$ = "frevelhaft, gottlos"
Vok2314$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2315

BL2315:
Latein$ = "deridere"
Deutsch$ = "verlachen, auslachen"
Deutsch2$ = "auslachen, verlachen"
Vok2315$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2316

BL2316:
Latein$ = "facinus"
Deutsch$ = "Tat, Verbrechen"
Deutsch2$ = "Verbrechen, Tat"
Genus$ = "N"
Genitiv$ = "FACINORIS"
Vok2316$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2317

BL2317:
Latein$ = "manere"
Deutsch$ = "bleiben, erwarten"
Deutsch2$ = "erwarten, bleiben"
Vok2317$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2318

BL2318:
Latein$ = "praeclarus, praeclari, praclarum"
Deutsch$ = "hochberÅhmt"
Vok2318$ = Methode2$(Latein$, Deutsch$)
GOTO BL2319

BL2319:
Latein$ = "exilium"
Deutsch$ = "Exil, Verbannung"
Deutsch2$ = "Verbannung, Exil"
Genus$ = "N"
Genitiv$ = "EXILII"
Vok2319$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2320

BL2320:
Latein$ = "labyrinthus"
Deutsch$ = "Labyrinth, Irrgarten"
Deutsch2$ = "Irrgarten, Labyrinth"
Genus$ = "M"
Genitiv$ = "LABYRINTHI"
Vok2320$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2321

BL2321:
Latein$ = "custodire"
Deutsch$ = "bewachen"
Vok2321$ = Methode2$(Latein$, Deutsch$)
GOTO BL2322

BL2322:
Latein$ = "taurus"
Deutsch$ = "Stier"
Genus$ = "M"
Genitiv$ = "TAURI"
Vok2322$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2323

BL2323:
Latein$ = "servire"
Deutsch$ = "dienen, Sklave sein"
Deutsch2$ = "Sklave sein, dienen"
Vok2323$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2324

BL2324:
Latein$ = "satis (Adv.)"
Deutsch$ = "genug"
Vok2324$ = Methode2$(Latein$, Deutsch$)
GOTO BL2325

BL2325:
Latein$ = "effugere"
Deutsch$ = "entfliehen"
Vok2325$ = Methode2$(Latein$, Deutsch$)
GOTO BL2326

BL2326:
Latein$ = "retinere"
Deutsch$ = "zurÅckhalten"
Vok2326$ = Methode2$(Latein$, Deutsch$)
GOTO Ende23

Ende23:
IF Vok2301$ = "Falsch" THEN
GOTO BL2301
ELSEIF Vok2302$ = "Falsch" THEN
GOTO BL2302
ELSEIF Vok2303$ = "Falsch" THEN
GOTO BL2303
ELSEIF Vok2304$ = "Falsch" THEN
GOTO BL2304
ELSEIF Vok2305$ = "Falsch" THEN
GOTO BL2305
ELSEIF Vok2306$ = "Falsch" THEN
GOTO BL2306
ELSEIF Vok2307$ = "Falsch" THEN
GOTO BL2307
ELSEIF V2k2308$ = "Falsch" THEN
GOTO BL2308
ELSEIF Vok2309$ = "Falsch" THEN
GOTO BL2309
ELSEIF Vok2310$ = "Falsch" THEN
GOTO BL2310
ELSEIF Vok2311$ = "Falsch" THEN
GOTO BL2311
ELSEIF Vok2312$ = "Falsch" THEN
GOTO BL2312
ELSEIF Vok2313$ = "Falsch" THEN
GOTO BL2313
ELSEIF Vok2314$ = "Falsch" THEN
GOTO BL2314
ELSEIF Vok2315$ = "Falsch" THEN
GOTO BL2315
ELSEIF Vok2316$ = "Falsch" THEN
GOTO BL2316
ELSEIF Vok2317$ = "Falsch" THEN
GOTO BL2317
ELSEIF Vok2318$ = "Falsch" THEN
GOTO BL2318
ELSEIF Vok2319$ = "Falsch" THEN
GOTO BL2319
ELSEIF Vok2320$ = "Falsch" THEN
GOTO BL2320
ELSEIF Vok2321$ = "Falsch" THEN
GOTO BL2321
ELSEIF Vok2322$ = "Falsch" THEN
GOTO BL2322
ELSEIF Vok2323$ = "Falsch" THEN
GOTO BL2323
ELSEIF Vok2324$ = "Falsch" THEN
GOTO BL2324
ELSEIF Vok2325$ = "Falsch" THEN
GOTO BL2325
ELSEIF Vok2326$ = "Falsch" THEN
GOTO BL2326
ELSE GOTO Letztende23
END IF

Letztende23:
END SUB

SUB BlauLektion24

BL2401:
Latein$ = "centum"
Deutsch$ = "hundert"
Vok2401$ = Methode2$(Latein$, Deutsch$)
GOTO BL2402

BL2402:
Latein$ = "urbs"
Deutsch$ = "Stadt"
Genus$ = "F"
Genitiv$ = "URBIS"
Vok2402$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2403

BL2403:
Latein$ = "potentia"
Deutsch$ = "Macht"
Genus$ = "F"
Genitiv$ = "POTENTIAE"
Vok2403$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2404

BL2404:
Latein$ = "exercitus"
Deutsch$ = "Heer"
Genus$ = "M"
Genitiv$ = "EXERCITUS"
Vok2404$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2405

BL2405:
Latein$ = "classis"
Deutsch$ = "Flotte"
Genus$ = "F"
Genitiv$ = "CLASSIS"
Vok2405$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2406

BL2406:
Latein$ = "longe lateque"
Deutsch$ = "weit und breit"
Vok2406$ = Methode2$(Latein$, Deutsch$)
GOTO BL2407

BL2407:
Latein$ = "mare"
Deutsch$ = "Meer"
Genus$ = "N"
Genitiv$ = "MARIS"
Vok2407$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2408

BL2408:
Latein$ = "gens"
Deutsch$ = "Volksstamm, Geschlecht"
Deutsch2$ = "Geschlecht, Volksstamm"
Genus$ = "F"
Genitiv$ = "GENTIS"
Vok2408$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2409

BL2409:
Latein$ = "civis"
Deutsch$ = "BÅrger"
Genus$ = "M"
Genitiv$ = "CIVIS"
Vok2409$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2410

BL2410:
Latein$ = "tributum"
Deutsch$ = "Tribut"
Genus$ = "N"
Genitiv$ = "TRIBUTI"
Vok2410$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2411

BL2411:
Latein$ = "domus"
Deutsch$ = "Haus"
Genus$ = "F"
Genitiv$ = "DOMUS"
Vok2411$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2412

BL2412:
Latein$ = "latitudo"
Deutsch$ = "Breite, Weite"
Deutsch2$ = "Weite, Breite"
Genus$ = "F"
Genitiv$ = "LATITUDINIS"
Vok2412$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2413

BL2413:
Latein$ = "altitudo"
Deutsch$ = "Hîhe, Tiefe"
Deutsch2$ = "Tiefe, Hîhe"
Genus$ = "F"
Genitiv$ = "ALTITUDINIS"
Vok2413$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2414

BL2414:
Latein$ = "moenia"
Deutsch$ = "Mauern, Befestigungen"
Deutsch2$ = "Befestigungen, Mauern"
Genus$ = "N"
Genitiv$ = "MOENIARUM"
Vok2414$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2415

BL2415:
Latein$ = "regnum"
Deutsch$ = "Reich, Herrschaft"
Deutsch2$ = "Herrschaft, Reich"
Genus$ = "N"
Genitiv$ = "REGNI"
Vok2415$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2416

BL2416:
Latein$ = "finis"
Deutsch$ = "Ende, Grenze"
Deutsch2$ = "Grenze, Ende"
Genus$ = "F"
Genitiv$ = "FINIS"
Vok2416$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2417

BL2417:
Latein$ = "vis, Akk. Vim, Abl. vi"
Deutsch$ = "Kraft, Gewalt"
Deutsch2$ = "Gewalt, Kraft"
Vok2417$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2418

BL2418:
Latein$ = "fluctus"
Deutsch$ = "Flut, Welle"
Deutsch2$ = "Welle, Flut"
Genus$ = "M"
Genitiv$ = "FLUCTUS"
Vok2410$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2411

BL2419:
Latein$ = "appellere"
Deutsch$ = "herantreiben"
Vok2419$ = Methode2$(Latein$, Deutsch$)
GOTO BL2420

BL2420:
Latein$ = "portus"
Deutsch$ = "Hafen"
Genus$ = "M"
Genitiv$ = "PORTUS"
Vok2420$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2421

BL2421:
Latein$ = "impetus"
Deutsch$ = "Ansturm, Angriff"
Deutsch2$ = "Angriff, Ansturm"
Genus$ = "M"
Genitiv$ = "IMPETUS"
Vok2421$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2422

BL2422:
Latein$ = "manus"
Deutsch$ = "Hand, Schar"
Deutsch2$ = "Schar, Hand"
Genus$ = "F"
Genitiv$ = "MANUS"
Vok2422$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2423

BL2423:
Latein$ = "lavare"
Deutsch$ = "waschen, baden"
Deutsch2$ = "baden, waschen"
Vok2423$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2424

BL2424:
Latein$ = "senatus"
Deutsch$ = "Senat"
Genus$ = "M"
Genitiv$ = "SENATUS"
Vok2424$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2425

BL2425:
Latein$ = "navis"
Deutsch$ = "Schiff"
Genus$ = "F"
Genitiv$ = "NAVIS"
Vok2425$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2426

BL2426:
Latein$ = "claudere"
Deutsch$ = "(ab)schlie·en, einschlie·en"
Deutsch2$ = "einschlie·en, (ab)schlie·en"
Vok2426$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2427

BL2427:
Latein$ = "caelum"
Deutsch$ = "Himmel"
Genus$ = "N"
Genitiv$ = "CAELI"
Vok2427$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2428

BL2428:
Latein$ = "ars"
Deutsch$ = "Geschicklichkeit, Kunst"
Deutsch2$ = "Kunst, Geschicklichkeit"
Genus$ = "F"
Genitiv$ = "ARTIS"
Vok2428$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2429

BL2429:
Latein$ = "medius, a, um"
Deutsch$ = "der mittlere"
Vok2429$ = Methode2$(Latein$, Deutsch$)
GOTO BL2430

BL2430:
Latein$ = "sol"
Deutsch$ = "Sonne"
Genus$ = "M"
Genitiv$ = "SOLIS"
Vok2430$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2431

BL2431:
Latein$ = "evitare"
Deutsch$ = "vermeiden"
Vok2431$ = Methode2$(Latein$, Deutsch$)
GOTO BL2432

BL2432:
Latein$ = "custos"
Deutsch$ = "WÑchter"
Genus$ = "M"
Genitiv$ = "CUSTODIS"
Vok2432$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2433

BL2433:
Latein$ = "mons"
Deutsch$ = "Berg"
Genus$ = "M"
Genitiv$ = "MONTIS"
Vok2433$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO Ende24

Ende24:
IF Vok2401$ = "Falsch" THEN
GOTO BL2401
ELSEIF Vok2402$ = "Falsch" THEN
GOTO BL2402
ELSEIF Vok2403$ = "Falsch" THEN
GOTO BL2403
ELSEIF Vok2404$ = "Falsch" THEN
GOTO BL2404
ELSEIF Vok2405$ = "Falsch" THEN
GOTO BL2405
ELSEIF Vok2406$ = "Falsch" THEN
GOTO BL2406
ELSEIF Vok2407$ = "Falsch" THEN
GOTO BL2407
ELSEIF V2k2408$ = "Falsch" THEN
GOTO BL2408
ELSEIF Vok2409$ = "Falsch" THEN
GOTO BL2409
ELSEIF Vok2410$ = "Falsch" THEN
GOTO BL2410
ELSEIF Vok2411$ = "Falsch" THEN
GOTO BL2411
ELSEIF Vok2412$ = "Falsch" THEN
GOTO BL2412
ELSEIF Vok2413$ = "Falsch" THEN
GOTO BL2413
ELSEIF Vok2414$ = "Falsch" THEN
GOTO BL2414
ELSEIF Vok2415$ = "Falsch" THEN
GOTO BL2415
ELSEIF Vok2416$ = "Falsch" THEN
GOTO BL2416
ELSEIF Vok2417$ = "Falsch" THEN
GOTO BL2417
ELSEIF Vok2418$ = "Falsch" THEN
GOTO BL2418
ELSEIF Vok2419$ = "Falsch" THEN
GOTO BL2419
ELSEIF Vok2420$ = "Falsch" THEN
GOTO BL2420
ELSEIF Vok2421$ = "Falsch" THEN
GOTO BL2421
ELSEIF Vok2422$ = "Falsch" THEN
GOTO BL2422
ELSEIF Vok2423$ = "Falsch" THEN
GOTO BL2423
ELSEIF Vok2424$ = "Falsch" THEN
GOTO BL2424
ELSEIF Vok2425$ = "Falsch" THEN
GOTO BL2425
ELSEIF Vok2426$ = "Falsch" THEN
GOTO BL2426
ELSEIF Vok2427$ = "Falsch" THEN
GOTO BL2427
ELSEIF Vok2428$ = "Falsch" THEN
GOTO BL2428
ELSEIF Vok2429$ = "Falsch" THEN
GOTO BL2429
ELSEIF Vok2430$ = "Falsch" THEN
GOTO BL2430
ELSEIF Vok2431$ = "Falsch" THEN
GOTO BL2431
ELSEIF Vok2432$ = "Falsch" THEN
GOTO BL2432
ELSEIF Vok2433$ = "Falsch" THEN
GOTO BL2433
ELSE GOTO Letztende24
END IF

Letztende24:
END SUB

SUB BlauLektion25

BL2501:
Latein$ = "vinum"
Deutsch$ = "Wein"
Genus$ = "N"
Genitiv$ = "VINI"
Vok2501$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2502

BL2502:
Latein$ = "comes"
Deutsch$ = "Begleiter"
Genus$ = "M"
Genitiv$ = "COMITIS"
Vok2502$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2503

BL2503:
Latein$ = "agmen"
Deutsch$ = "(Heeres)zug, Marsch"
Deutsch2$ = "Marsch, (Heeres)zug"
Genus$ = "N"
Genitiv$ = "AGMINIS"
Vok2503$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2504

BL2504:
Latein$ = "iter"
Deutsch$ = "Weg, Marsch, Reise"
Deutsch2$ = "Weg, Reise, Marsch"
Genus$ = "N"
Genitiv$ = "ITINERIS"
Vok2504$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2505

BL2505:
Latein$ = "cedere"
Deutsch$ = "gehen, weichen"
Deutsch2$ = "weichen, gehen"
Vok2505$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2506

BL2506:
Latein$ = "procedere"
Deutsch$ = "vorrÅcken"
Vok2506$ = Methode2$(Latein$, Deutsch$)
GOTO BL2507

BL2507:
Latein$ = "senex"
Deutsch$ = "Greis, Alt"
Deutsch2$ = "Alt, Greis"
Genus$ = "M"
Genitiv$ = "SENIS"
Vok2507$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2508

BL2508:
Latein$ = "adventus"
Deutsch$ = "Ankunft"
Genus$ = "M"
Genitiv$ = "ADVENTUS"
Vok2508$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2509

BL2509:
Latein$ = "hospes"
Deutsch$ = "Gast, Gastgeber"
Deutsch2$ = "Gastgeber, Gast"
Genus$ = "M"
Genitiv$ = "HOSPITIS"
Vok2509$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2510

BL2510:
Latein$ = "ut (m. Konj.)"
Deutsch$ = "da·, damit, (um) zu"
Deutsch2$ = "da·, damit, (um)zu"
Vok2510$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2511
 
BL2511:
Latein$ = "pauci"
Deutsch$ = "wenige, einige"
Deutsch2$ = "einige, wenige"
Vok2511$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2512

BL2512:
Latein$ = "afficere(m. Abl.)"
Deutsch$ = "versehen mit"
Vok2512$ = Methode2$(Latein$, Deutsch$)
GOTO BL2513

BL2513:
Latein$ = "desiderare"
Deutsch$ = "verlangen, vermissen"
Deutsch2$ = "vermissen, verlangen"
Vok2513$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2514

BL2514:
Latein$ = "praemium"
Deutsch$ = "Belohnung"
Genus$ = "N"
Genitiv$ = "PRAEMII"
Vok2514$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2515

BL2515:
Latein$ = "permittere"
Deutsch$ = "erlauben"
Vok2515$ = Methode2$(Latein$, Deutsch$)
GOTO BL2516

BL2516:
Latein$ = "optare"
Deutsch$ = "wÅnschen"
Vok2516$ = Methode2$(Latein$, Deutsch$)
GOTO BL2517

BL2517:
Latein$ = "ingratus, a, um"
Deutsch$ = "undankbar"
Vok2517$ = Methode2$(Latein$, Deutsch$)
GOTO BL2518

BL2518:
Latein$ = "videri"
Deutsch$ = "(er)scheinen"
Vok2518$ = Methode2$(Latein$, Deutsch$)
GOTO BL2519

BL2519:
Latein$ = "aurum"
Deutsch$ = "Gold"
Genus$ = "N"
Genitiv$ = "AURI"
Vok2519$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2520

BL2520:
Latein$ = "mutare"
Deutsch$ = "verwandeln, verÑndern"
Deutsch2$ = "verÑndern, verwandeln"
Vok2520$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2521

BL2521:
Latein$ = "commovere"
Deutsch$ = "(innerlich) bewegen, veranlassen"
Deutsch2$ = "veranlassen, (innerlich) bewegen"
Vok2521$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2522

BL2522:
Latein$ = "instrumentum"
Deutsch$ = "Werkzeug, GerÑt"
Deutsch2$ = "GerÑt, Werkzeug"
Genus$ = "N"
Genitiv$ = "INSTRUMENTI"
Vok2522$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2523

BL2523:
Latein$ = "consumere"
Deutsch$ = "verbrauchen"
Vok2523$ = Methode2$(Latein$, Deutsch$)
GOTO BL2524

BL2524:
Latein$ = "lapis"
Deutsch$ = "Stein"
Genus$ = "M"
Genitiv$ = "LAPIDIS"
Vok2524$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2525

BL2526:
Latein$ = "fac!"
Deutsch$ = "mach!"
Vok2526$ = Methode2$(Latein$, Deutsch$)
GOTO BL2527

BL2527:
Latein$ = "contendere"
Deutsch$ = "sich anstrengen, eilen, kÑmpfen"
Deutsch2$ = "sich anstrengen, kÑmpfen, eilen"
Vok2527$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2528

BL2528:
Latein$ = "opulentus"
Deutsch$ = "reichlich, Åppig"
Deutsch2$ = "Åppig, reichlich"
Vok2528$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2529

BL2529:
Latein$ = "fames"
Deutsch$ = "Hunger"
Genus$ = "F"
Genitiv$ = "FAMIS"
Vok2529$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2530

BL2530:
Latein$ = "numen"
Deutsch$ = "(gîttliche) Macht, Gottheit"
Deutsch2$ = "Gottheit, (gîttliche) Macht"
Genus$ = "N"
Genitiv$ = "NUMINIS"
Vok2530$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2531

BL2525:
Latein$ = "tangere"
Deutsch$ = "berÅhren"
Vok2525$ = Methode2$(Latein$, Deutsch$)
GOTO BL2526

BL2531:
Latein$ = "efficere"
Deutsch$ = "bewirken"
Vok2531$ = Methode2$(Latein$, Deutsch$)
GOTO Ende25

Ende25:
IF Vok2501$ = "Falsch" THEN
GOTO BL2501
ELSEIF Vok2502$ = "Falsch" THEN
GOTO BL2502
ELSEIF Vok2503$ = "Falsch" THEN
GOTO BL2503
ELSEIF Vok2504$ = "Falsch" THEN
GOTO BL2504
ELSEIF Vok2505$ = "Falsch" THEN
GOTO BL2505
ELSEIF Vok2506$ = "Falsch" THEN
GOTO BL2506
ELSEIF Vok2507$ = "Falsch" THEN
GOTO BL2507
ELSEIF Vok2508$ = "Falsch" THEN
GOTO BL2508
ELSEIF Vok2509$ = "Falsch" THEN
GOTO BL2509
ELSEIF Vok2510$ = "Falsch" THEN
GOTO BL2510
ELSEIF Vok2511$ = "Falsch" THEN
GOTO BL2511
ELSEIF Vok2512$ = "Falsch" THEN
GOTO BL2512
ELSEIF Vok2513$ = "Falsch" THEN
GOTO BL2513
ELSEIF Vok2514$ = "Falsch" THEN
GOTO BL2514
ELSEIF Vok2515$ = "Falsch" THEN
GOTO BL2515
ELSEIF Vok2516$ = "Falsch" THEN
GOTO BL2516
ELSEIF Vok2517$ = "Falsch" THEN
GOTO BL2517
ELSEIF Vok2518$ = "Falsch" THEN
GOTO BL2518
ELSEIF Vok2519$ = "Falsch" THEN
GOTO BL2519
ELSEIF Vok2520$ = "Falsch" THEN
GOTO BL2520
ELSEIF Vok2521$ = "Falsch" THEN
GOTO BL2521
ELSEIF Vok2522$ = "Falsch" THEN
GOTO BL2522
ELSEIF Vok2523$ = "Falsch" THEN
GOTO BL2523
ELSEIF Vok2524$ = "Falsch" THEN
GOTO BL2524
ELSEIF Vok2525$ = "Falsch" THEN
GOTO BL2525
ELSEIF Vok2526$ = "Falsch" THEN
GOTO BL2526
ELSEIF Vok2527$ = "Falsch" THEN
GOTO BL2527
ELSEIF Vok2528$ = "Falsch" THEN
GOTO BL2528
ELSEIF Vok2529$ = "Falsch" THEN
GOTO BL2529
ELSEIF Vok2530$ = "Falsch" THEN
GOTO BL2530
ELSEIF Vok2531$ = "Falsch" THEN
GOTO BL2531
ELSE GOTO Letztende25
END IF

Letztende25:
END SUB

SUB BlauLektion26
END SUB

SUB BlauLektion27

BL2701:
Latein$ = "quartus, a, um"
Deutsch$ = "der vierte"
Vok2701$ = Methode2$(Latein$, Deutsch$)
GOTO BL2702

BL2702:
Latein$ = "natus, a, um"
Deutsch$ = "geboren"
Vok2702$ = Methode2$(Latein$, Deutsch$)
GOTO BL2703

BL2703:
Latein$ = "a. Chr. n."
Deutsch$ = "vor Christi Geburt"
Vok2703$ = Methode2$(Latein$, Deutsch$)
GOTO BL2704

BL2704:
Latein$ = "saeculum"
Deutsch$ = "Jahrhundert Zeitalter"
Deutsch2$ = "Zeitalter Jahrhundert"
Genus$ = "N"
Genitiv$ = "SAECULI"
Vok2704$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2705

BL2705:
Latein$ = "invadere"
Deutsch$ = "eindringen"
Vok2705$ = Methode2$(Latein$, Deutsch$)
GOTO BL2706

BL2706:
Latein$ = "multitudo"
Deutsch$ = "Menge"
Genus$ = "F"
Genitiv$ = "MULTITUDINIS"
Vok2706$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2707

BL2707:
Latein$ = "alere"
Deutsch$ = "ernÑhren"
Vok2707$ = Methode2$(Latein$, Deutsch$)
GOTO BL2708

BL2708:
Latein$ = "legatus"
Deutsch$ = "Gesandter Legat"
Deutsch2$ = "Legat Gesandter"
Genus$ = "M"
Genitiv$ = "LEGATI"
Vok2708$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2709

BL2709:
Latein$ = "oratio"
Deutsch$ = "Rede"
Genus$ = "F"
Genitiv$ = "ORATIONIS"
Vok2709$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2710

BL2710:
Latein$ = "posse"
Deutsch$ = "kînnen"
Vok2710$ = Methode2$(Latein$, Deutsch$)
GOTO BL2711

BL2711:
Latein$ = "pars"
Deutsch$ = "Teil Seite"
Deutsch2$ = "Seite Teil"
Genus$ = "F"
Genitiv$ = "PARTIS"
Vok2711$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2712

BL2712:
Latein$ = "prodesse"
Deutsch$ = "nÅtzen"
Vok2712$ = Methode2$(Latein$, Deutsch$)
GOTO BL2713

BL2713:
Latein$ = "abundare"
Deutsch$ = "öberflu· haben Åberflie·en"
Deutsch2$ = "Åberflie·en öberflu· haben"
Vok2713$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2714

BL2714:
Latein$ = "aequus, a, um"
Deutsch$ = "gleich gerecht gÅnstig"
Deutsch2$ = "gleicht gÅnstig gerecht"
Vok2714$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2715

BL2715:
Latein$ = "iniquus, a, um"
Deutsch$ = "ungleich ungerecht ungÅnstig"
Deutsch2$ = "ungleich ungÅnstig ungerecht"
Vok2715$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2716

BL2716:
Latein$ = "auxilium"
Deutsch$ = "Hilfe"
Genus$ = "N"
Genitiv$ = "AUXILII"
Vok2716$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2717

BL2717:
Latein$ = "castra (Pl.)"
Deutsch$ = "(Feld)lager"
Genus$ = "N"
Genitiv$ = "CASTRORUM"
Vok2717$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2718

BL2718:
Latein$ = "permovere"
Deutsch$ = "(innerlich) bewegen veranlassen"
Deutsch2$ = "veranlassen (innerlich) bewegen"
Vok2718$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2719

BL2719:
Latein$ = "legatio"
Deutsch$ = "Gesandtschaft"
Genus$ = "F"
Genitiv$ = "LEGATIONIS"
Vok2719$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2720

BL2720:
Latein$ = "praesse"
Deutsch$ = "vorstehen anfÅhren"
Deutsch2$ = "anfÅhren vorstehen"
Vok2720$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2721

BL2721:
Latein$ = "barbarus"
Deutsch$ = "Barbar"
Genus$ = "M"
Genitiv$ = "BARBARI"
Vok2721$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2722

BL2722:
Latein$ = "una (Adv.)"
Deutsch$ = "zusammen"
Vok2722$ = Methode2$(Latein$, Deutsch$)
GOTO BL2723

BL2723:
Latein$ = "obesse"
Deutsch$ = "schaden"
Vok2723$ = Methode2$(Latein$, Deutsch$)
GOTO BL2724

BL2724:
Latein$ = "ius"
Deutsch$ = "Recht"
Genus$ = "N"
Genitiv$ = "IURIS"
Vok2724$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2725

BL2725:
Latein$ = "iratus, a, um"
Deutsch$ = "zornig"
Vok2725$ = Methode2$(Latein$, Deutsch$)
GOTO BL2726

BL2726:
Latein$ = "clades"
Deutsch$ = "Niederlage"
Genus$ = "F"
Genitiv$ = "CLADIS"
Vok2726$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2727

BL2727:
Latein$ = "intellegere"
Deutsch$ = "bemerken einsehen verstehen"
Deutsch2$ = "bemerken verstehen einsehen"
Vok2727$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2728

BL2728:
Latein$ = "curare"
Deutsch$ = "sorgen besorgen"
Deutsch2$ = "besorgen sorgen"
Vok2728$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2729

BL2729:
Latein$ = "arx"
Deutsch$ = "Burg"
Genus$ = "F"
Genitiv$ = "ARCIS"
Vok2729$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2730

BL2730:
Latein$ = "quidem (Adv.)"
Deutsch$ = "jedenfalls wenigstens"
Deutsch2$ = "wenigstens jedenfalls"
Vok2730$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2731

BL2731:
Latein$ = "noctu (Adv.)"
Deutsch$ = "bei Nacht"
Vok2731$ = Methode2$(Latein$, Deutsch$)
GOTO BL2732

BL2732:
Latein$ = "canis"
Deutsch$ = "Hund"
Genus$ = "M F"
Genitiv$ = "CANIS"
Vok2732$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2733

BL2733:
Latein$ = "pestilentia"
Deutsch$ = "Seuche Krankheit"
Deutsch2$ = "Krankheit Seuche"
Genus$ = "F"
Genitiv$ = "PESTILENTIAE"
Vok2733$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO Ende27

Ende27:
IF Vok2701$ = "Falsch" THEN
GOTO BL2701
ELSEIF Vok2702$ = "Falsch" THEN
GOTO BL2702
ELSEIF Vok2703$ = "Falsch" THEN
GOTO BL2703
ELSEIF Vok2704$ = "Falsch" THEN
GOTO BL2704
ELSEIF Vok2705$ = "Falsch" THEN
GOTO BL2705
ELSEIF Vok2706$ = "Falsch" THEN
GOTO BL2706
ELSEIF Vok2707$ = "Falsch" THEN
GOTO BL2707
ELSEIF V2k2708$ = "Falsch" THEN
GOTO BL2708
ELSEIF Vok2709$ = "Falsch" THEN
GOTO BL2709
ELSEIF Vok2710$ = "Falsch" THEN
GOTO BL2710
ELSEIF Vok2711$ = "Falsch" THEN
GOTO BL2711
ELSEIF Vok2712$ = "Falsch" THEN
GOTO BL2712
ELSEIF Vok2713$ = "Falsch" THEN
GOTO BL2713
ELSEIF Vok2714$ = "Falsch" THEN
GOTO BL2714
ELSEIF Vok2715$ = "Falsch" THEN
GOTO BL2715
ELSEIF Vok2716$ = "Falsch" THEN
GOTO BL2716
ELSEIF Vok2717$ = "Falsch" THEN
GOTO BL2717
ELSEIF Vok2718$ = "Falsch" THEN
GOTO BL2718
ELSEIF Vok2719$ = "Falsch" THEN
GOTO BL2719
ELSEIF Vok2720$ = "Falsch" THEN
GOTO BL2720
ELSEIF Vok2721$ = "Falsch" THEN
GOTO BL2721
ELSEIF Vok2722$ = "Falsch" THEN
GOTO BL2722
ELSEIF Vok2723$ = "Falsch" THEN
GOTO BL2723
ELSEIF Vok2724$ = "Falsch" THEN
GOTO BL2724
ELSEIF Vok2725$ = "Falsch" THEN
GOTO BL2725
ELSEIF Vok2726$ = "Falsch" THEN
GOTO BL2726
ELSEIF Vok2727$ = "Falsch" THEN
GOTO BL2727
ELSEIF Vok2728$ = "Falsch" THEN
GOTO BL2728
ELSEIF Vok2729$ = "Falsch" THEN
GOTO BL2729
ELSEIF Vok2730$ = "Falsch" THEN
GOTO BL2730
ELSEIF Vok2731$ = "Falsch" THEN
GOTO BL2731
ELSEIF Vok2732$ = "Falsch" THEN
GOTO BL2732
ELSEIF Vok2733$ = "Falsch" THEN
GOTO BL2733
ELSE GOTO Letztende27
END IF

Letztende27:
END SUB

SUB BlauLektion28








END SUB

SUB BlauLektion29

BL2901:
Latein$ = "Alesiae"
Deutsch$ = "in Alesia"
Vok2901$ = Methode2$(Latein$, Deutsch$)
GOTO BL2902

BL2902:
Latein$ = "includere"
Deutsch$ = "einschlie·en"
Vok2902$ = Methode2$(Latein$, Deutsch$)
GOTO BL2903

BL2903:
Latein$ = "sperare"
Deutsch$ = "hoffen"
Vok2903$ = Methode2$(Latein$, Deutsch$)
GOTO BL2904

BL2904:
Latein$ = "desperare"
Deutsch$ = "verzweifeln die Hoffnung aufgeben"
Deutsch2$ = "die Hoffnung aufgeben verzweifeln"
Vok2904$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2905

BL2905:
Latein$ = "sui Pl."
Deutsch$ = "die Seinigen seine Leute"
Deutsch2$ = "seine Leute die Seinigen"
Genus$ = "M"
Genitiv$ = "SUIORUM"
Vok2905$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2906

BL2906:
Latein$ = "munitio"
Deutsch$ = "Befestigung"
Genus$ = "F"
Genitiv$ = "MUNITIONIS"
Vok2906$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2907

BL2907:
Latein$ = "certe (Adv.)"
Deutsch$ = "gewi· sicher"
Deutsch2$ = "sicher gewi·"
Vok2907$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2908

BL2908:
Latein$ = "considerare"
Deutsch$ = "betrachten"
Vok2908$ = Methode2$(Latein$, Deutsch$)
GOTO BL2909

BL2909:
Latein$ = "imminere"
Deutsch$ = "drohen"
Vok2909$ = Methode2$(Latein$, Deutsch$)
GOTO BL2910

BL2910:
Latein$ = "recipere"
Deutsch$ = "zurÅcknehmen aufnehmen"
Deutsch2$ = "aufnehmen zurÅcknehmen"
Vok2910$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2911

BL2911:
Latein$ = "at"
Deutsch$ = "jedoch"
Vok2911$ = Methode2$(Latein$, Deutsch$)
GOTO BL2912

BL2912:
Latein$ = "iniustus"
Deutsch$ = "ungerecht"
Vok2912$ = Methode2$(Latein$, Deutsch$)
GOTO BL2913

BL2913:
Latein$ = "expellere"
Deutsch$ = "vertreiben"
Vok2913$ = Methode2$(Latein$, Deutsch$)
GOTO BL2914

BL2914:
Latein$ = "trucidare"
Deutsch$ = "tîten umbringen"
Deutsch2$ = "umbringen tîten"
Vok2914$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2915

BL2915:
Latein$ = "miseria"
Deutsch$ = "Elend"
Genus$ = "F"
Genitiv$ = "MISERIAE"
Vok2915$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2916

BL2916:
Latein$ = "mors"
Deutsch$ = "Tod"
Genus$ = "F"
Genitiv$ = "MORTIS"
Vok2916$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2917

BL2917:
Latein$ = "mox"
Deutsch$ = "bald"
Vok2917$ = Methode2$(Latein$, Deutsch$)
GOTO BL2918

BL2918:
Latein$ = "reliquiae Pl."
Deutsch$ = "öberreste"
Genus$ = "F"
Genitiv$ = "RELIQUIARUM"
Vok2918$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL2919

BL2919:
Latein$ = "putare"
Deutsch$ = "glauben"
Vok2919$ = Methode2$(Latein$, Deutsch$)
GOTO BL2920

BL2920:
Latein$ = "rumpere"
Deutsch$ = "brechen"
Vok2920$ = Methode2$(Latein$, Deutsch$)
GOTO BL2921

BL2921:
Latein$ = "perrumpere"
Deutsch$ = "durchbrechen"
Vok2921$ = Methode2$(Latein$, Deutsch$)
GOTO BL2922

BL2922:
Deutsch$ = "keiner kein"
Deutsch2$ = "kein keiner"
Vok2922$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2923

BL2923:
Latein$ = "reliquus, a, um"
Deutsch$ = "Åbrig"
Vok2923$ = Methode2$(Latein$, Deutsch$)
GOTO BL2924

BL2924:
Latein$ = "fortuna"
Deutsch$ = "Schicksal GlÅck"
Deutsch2$ = "GlÅck Schicksal"
Genus$ = "F"
Genitiv$ = "FORTUNAE"
Vok2924$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL2925

BL2925:
Latein$ = "suscipere"
Deutsch$ = "unternehmen"
Vok2925$ = Methode2$(Latein$, Deutsch$)
GOTO BL2926

BL2926:
Latein$ = "tradere"
Deutsch$ = "Åbergeben Åberliefern"
Deutsch2$ = "Åberliefern Åbergeben"
Vok2926$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO Ende29

Ende29:
IF Vok2901$ = "Falsch" THEN
GOTO BL2901
ELSEIF Vok2902$ = "Falsch" THEN
GOTO BL2902
ELSEIF Vok2903$ = "Falsch" THEN
GOTO BL2903
ELSEIF Vok2904$ = "Falsch" THEN
GOTO BL2904
ELSEIF Vok2905$ = "Falsch" THEN
GOTO BL2905
ELSEIF Vok2906$ = "Falsch" THEN
GOTO BL2906
ELSEIF Vok2907$ = "Falsch" THEN
GOTO BL2907
ELSEIF V2k2908$ = "Falsch" THEN
GOTO BL2908
ELSEIF Vok2909$ = "Falsch" THEN
GOTO BL2909
ELSEIF Vok2910$ = "Falsch" THEN
GOTO BL2910
ELSEIF Vok2911$ = "Falsch" THEN
GOTO BL2911
ELSEIF Vok2912$ = "Falsch" THEN
GOTO BL2912
ELSEIF Vok2913$ = "Falsch" THEN
GOTO BL2913
ELSEIF Vok2914$ = "Falsch" THEN
GOTO BL2914
ELSEIF Vok2915$ = "Falsch" THEN
GOTO BL2915
ELSEIF Vok2916$ = "Falsch" THEN
GOTO BL2916
ELSEIF Vok2917$ = "Falsch" THEN
GOTO BL2917
ELSEIF Vok2918$ = "Falsch" THEN
GOTO BL2918
ELSEIF Vok2919$ = "Falsch" THEN
GOTO BL2919
ELSEIF Vok2920$ = "Falsch" THEN
GOTO BL2920
ELSEIF Vok2921$ = "Falsch" THEN
GOTO BL2921
ELSEIF Vok2922$ = "Falsch" THEN
GOTO BL2922
ELSEIF Vok2923$ = "Falsch" THEN
GOTO BL2923
ELSEIF Vok2924$ = "Falsch" THEN
GOTO BL2924
ELSEIF Vok2925$ = "Falsch" THEN
GOTO BL2925
ELSEIF Vok2926$ = "Falsch" THEN
GOTO BL2926
ELSE GOTO Letztende29
END IF

Letztende29:
END SUB

SUB BlauLektion3

BL0301:
Latein$ = "festus(, a, um)"
Deutsch$ = "festlich, Fest"
Deutsch2$ = "Fest, festlich"
Genus$ = "M"
Genitiv$ = "FESTI"
Vok0301$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0301

BL0302:
Latein$ = "hodie (Adv.)"
Deutsch$ = "heute"
Vok0302$ = Methode2$(Latein$, Deutsch$)
GOTO BL0303

BL0303:
Latein$ = "ara"
Deutsch$ = "Altar"
Genus$ = "F"
Genitiv$ = "ARAE"
Vok0303$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0304

BL0305:
Latein$ = "adorare"
Deutsch$ = "anbeten"
Vok0305$ = Methode2$(Latein$, Deutsch$)
GOTO BL0306

BL0304:
Latein$ = "deus"
Deutsch$ = "Gott"
Genus$ = "M"
Genitiv$ = "DEI"
Vok0304$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0305

BL0306:
Latein$ = "visitare"
Deutsch$ = "besuchen"
Vok0306$ = Methode2$(Latein$, Deutsch$)
GOTO BL0307

BL0307:
Latein$ = "fabula"
Deutsch$ = "Geschichte"
Genus$ = "F"
Genitiv$ = "FABULAE"
Vok0307$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0308

BL0308:
Latein$ = "narrare"
Deutsch$ = "erzÑhlen"
Vok0308$ = Methode2$(Latein$, Deutsch$)
GOTO BL0309

BL0309:
Latein$ = "parvus, a, um"
Deutsch$ = "klein, unbedeutend"
Deutsch2$ = "unbedeutend, klein"
Vok0309$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0310

BL0310:
Latein$ = "delectare"
Deutsch$ = "erfreuen"
Vok0310$ = Methode2$(Latein$, Deutsch$)
GOTO BL0311

BL0311:
Latein$ = "novus, a, um"
Deutsch$ = "neu"
Vok0311$ = Methode2$(Latein$, Deutsch$)
GOTO BL0312

BL0312:
Latein$ = "libenter (Adv.)"
Deutsch$ = "gern"
Vok0312$ = Methode2$(Latein$, Deutsch$)
GOTO BL0313

BL0313:
Latein$ = "malus, a, um"
Deutsch$ = "schlecht, bîse"
Deutsch2$ = "bîse, schlecht"
Vok0313$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0314

BL0314:
Latein$ = "magnus, a, um"
Deutsch$ = "gro·, bedeutend"
Deutsch2$ = "bedeutend, gro·"
Vok0314$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0315

BL0315:
Latein$ = "campus"
Deutsch$ = "Feld, freier Platz"
Deutsch2$ = "freier Platz, Feld"
Genus$ = "M"
Genitiv$ = "CAMPI"
Vok0315$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0316

BL0316:
Latein$ = "ambulare"
Deutsch$ = "spatzieren gehen"
Vok0316$ = Methode2$(Latein$, Deutsch$)
GOTO BL0317

BL0317:
Latein$ = "quamdiu?"
Deutsch$ = "wie lange?"
Vok0317$ = Methode2$(Latein$, Deutsch$)
GOTO BL0318

BL0318:
Latein$ = "unus, a, um"
Deutsch$ = "ein, ein einziger"
Deutsch2$ = "ein einziger, ein"
Vok0318$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0319

BL0319:
Latein$ = "hora"
Deutsch$ = "Stunde"
Genus$ = "F"
Genitiv$ = "HORAE"
Vok0319$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0320

BL0320:
Latein$ = "ita (Adv.)"
Deutsch$ = "so"
Vok0320$ = Methode2$(Latein$, Deutsch$)
GOTO BL0321

BL0321:
Latein$ = "celebrare"
Deutsch$ = "feiern, preisen"
Deutsch2$ = "preisen, feiern"
Vok0321$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0322

BL0322:
Latein$ = "totus, a, um"
Deutsch$ = "ganz"
Vok0322$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0323

BL0323:
Latein$ = "bonus, a, um"
Deutsch$ = "gut"
Vok0323$ = Methode2$(Latein$, Deutsch$)
GOTO BL0324

BL0324:
Latein$ = "semper (Adv.)"
Deutsch$ = "immer"
Vok0324$ = Methode2$(Latein$, Deutsch$)
GOTO BL0325

BL0325:
Latein$ = "silva"
Deutsch$ = "Wald"
Genus$ = "F"
Genitiv$ = "SILVAE"
Vok0325$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0326

BL0326:
Latein$ = "amica"
Deutsch$ = "Freundin"
Genus$ = "F"
Genitiv$ = "AMICAE"
Vok0326$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0327

BL0327:
Latein$ = "columba"
Deutsch$ = "Taube"
Genus$ = "F"
Genitiv$ = "COLUMBAE"
Vok0327$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0328

BL0328:
Latein$ = "fenestra"
Deutsch$ = "Fenster"
Genus$ = "F"
Genitiv$ = "FENESTRAE"
Vok0328$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0329

BL0329:
Latein$ = "statim (Adv.)"
Deutsch$ = "sofort"
Vok0329$ = Methode2$(Latein$, Deutsch$)
GOTO BL0331

BL0331:
Latein$ = "protare"
Deutsch$ = "tragen"
Vok0331$ = Methode2$(Latein$, Deutsch$)
GOTO BL0332

BL0332:
Latein$ = "spectare"
Deutsch$ = "betrachten, schauen"
Deutsch2$ = "schauen, betrachten"
Vok0333$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0333

BL0333:
Latein$ = "nunc (Adv.)"
Deutsch$ = "jetzt, nun"
Deutsch2$ = "nun, jetzt"
Vok0333$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0334

BL0334:
Latein$ = "valde (Adv.)"
Deutsch$ = "sehr"
Vok0334$ = Methode2$(Latein$, Deutsch$)
GOTO Ende03

Ende03:
IF Vok0301$ = "Falsch" THEN
GOTO BL0301
ELSEIF Vok0302$ = "Falsch" THEN
GOTO BL0302
ELSEIF Vok0303$ = "Falsch" THEN
GOTO BL0303
ELSEIF Vok0304$ = "Falsch" THEN
GOTO BL0304
ELSEIF Vok0305$ = "Falsch" THEN
GOTO BL0305
ELSEIF Vok0306$ = "Falsch" THEN
GOTO BL0306
ELSEIF Vok0307$ = "Falsch" THEN
GOTO BL0307
ELSEIF Vok0308$ = "Falsch" THEN
GOTO BL0308
ELSEIF Vok0309$ = "Falsch" THEN
GOTO BL0309
ELSEIF Vok0310$ = "Falsch" THEN
GOTO BL0310
ELSEIF Vok0311$ = "Falsch" THEN
GOTO BL0311
ELSEIF Vok0312$ = "Falsch" THEN
GOTO BL0312
ELSEIF Vok0313$ = "Falsch" THEN
GOTO BL0313
ELSEIF Vok0314$ = "Falsch" THEN
GOTO BL0314
ELSEIF Vok0315$ = "Falsch" THEN
GOTO BL0315
ELSEIF Vok0316$ = "Falsch" THEN
GOTO BL0316
ELSEIF Vok0317$ = "Falsch" THEN
GOTO BL0317
ELSEIF Vok0318$ = "Falsch" THEN
GOTO BL0318
ELSEIF Vok0319$ = "Falsch" THEN
GOTO BL0319
ELSEIF Vok0320$ = "Falsch" THEN
GOTO BL0320
ELSEIF Vok0321$ = "Falsch" THEN
GOTO BL0321
ELSEIF Vok0322$ = "Falsch" THEN
GOTO BL0322
ELSEIF Vok0323$ = "Falsch" THEN
GOTO BL0323
ELSEIF Vok0324$ = "Falsch" THEN
GOTO BL0324
ELSEIF Vok0325$ = "Falsch" THEN
GOTO BL0325
ELSEIF Vok0326$ = "Falsch" THEN
GOTO BL0326
ELSEIF Vok0327$ = "Falsch" THEN
GOTO BL0327
ELSEIF Vok0328$ = "Falsch" THEN
GOTO BL0328
ELSEIF Vok0329$ = "Falsch" THEN
GOTO BL0329
ELSEIF Vok0330$ = "Falsch" THEN
GOTO BL0331
ELSEIF Vok0332$ = "Falsch" THEN
GOTO BL0332
ELSEIF Vok0333$ = "Falsch" THEN
GOTO BL0333
ELSEIF Vok0334$ = "Falsch" THEN
GOTO BL0334
ELSE GOTO Letztende03
END IF

Letztende03:
END SUB

SUB BlauLektion30

BL3001:
Latein$ = "nobilis"
Deutsch$ = "adlig berÅhmt"
Deutsch2$ = "berÅhmt adlig"
Vok3001$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3002

BL3002:
Latein$ = "acer"
Deutsch$ = "scharf heftig"
Deutsch2$ = "heftig scharf"
Vok3002$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3003

BL3003:
Latein$ = "fortis"
Deutsch$ = "tapfer"
Vok3003$ = Methode2$(Latein$, Deutsch$)
GOTO BL3004

BL3004:
Latein$ = "reditus"
Deutsch$ = "RÅckkehr"
Genus$ = "M"
Genitiv$ = "REDITUS"
Vok3004$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3005

BL3005:
Latein$ = "componere"
Deutsch$ = "zusammenstellen verfassen"
Deutsch2$ = "verfassen zusammenstellen"
Vok3005$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3006

BL3006:
Latein$ = "celer"
Deutsch$ = "schnell"
Vok3006$ = Methode2$(Latein$, Deutsch$)
GOTO BL3007

BL3007:
Latein$ = "ventus"
Deutsch$ = "Wind"
Genus$ = "M"
Genitiv$ = "VENTI"
Vok3007$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3008

BL3008:
Latein$ = "frons"
Deutsch$ = "Stirn"
Genus$ = "F"
Genitiv$ = "FRONTIS"
Vok3008$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3009

BL3009:
Latein$ = "ingens"
Deutsch$ = "riesig"
Vok3008$ = Methode2$(Latein$, Deutsch$)
GOTO BL3010

BL3010:
Latein$ = "monstrum"
Deutsch$ = "Ungeheuer"
Genus$ = "N"
Genitiv$ = "MONSTRI"
Vok3010$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3011

BL3011:
Latein$ = "crudelis"
Deutsch$ = "grausam"
Vok3011$ = Methode2$(Latein$, Deutsch$)
GOTO BL3012

BL3012:
Latein$ = "saxum"
Deutsch$ = "Felsen Stein"
Deutsch2$ = "Stein Felsen"
Genus$ = "N"
Genitiv$ = "SAXI"
Vok3012$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL3013

BL3013:
Latein$ = "gravis"
Deutsch$ = "schwer ernst wichtig"
Deutsch2$ = "schwer wichtig ernst"
'Deutsch3$ = "ernst schwer wichtig"
Vok3013$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3014

BL3014:
Latein$ = "introitus"
Deutsch$ = "Eingang"
Genus$ = "M"
Genitiv$ = "INTROITUS"
Vok3014$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3015

BL3015:
Latein$ = "evadere"
Deutsch$ = "entkommen"
Vok3015$ = Methode2$(Latein$, Deutsch$)
GOTO BL3016

BL3016:
Latein$ = "timidus"
Deutsch$ = "Ñngstlich"
Vok3016$ = Methode2$(Latein$, Deutsch$)
GOTO BL3017

BL3017:
Latein$ = "felix"
Deutsch$ = "glÅcklich"
Vok3017$ = Methode2$(Latein$, Deutsch$)
GOTO BL3018

BL3018:
Latein$ = "infelix"
Deutsch$ = "unglÅcklich"
Vok3018$ = Methode2$(Latein$, Deutsch$)
GOTO BL3020

BL3019:
Latein$ = "ominis, e"
Deutsch$ = "jeder ganz"
Deutsch2$ = "ganz jeder"
Vok3019$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3020

BL3020:
Latein$ = "facilis, e"
Deutsch$ = "leicht"
Vok3029$ = Methode2$(Latein$, Deutsch$)
GOTO BL3021

BL3021:
Latein$ = "facile (Adv.)"
Deutsch$ = "leicht"
Vok3021$ = Methode2$(Latein$, Deutsch$)
GOTO BL3022

BL3022:
Latein$ = "difficilis, e"
Deutsch$ = "schwierig"
Vok3022$ = Methode2$(Latein$, Deutsch$)
GOTO BL3023

BL3023:
Latein$ = "somnus"
Deutsch$ = "Schlaf"
Genus$ = "M"
Genitiv$ = "SOMNI"
Vok3023$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3024

BL3024:
Latein$ = "temptare"
Deutsch$ = "versuchen"
Vok3024$ = Methode2$(Latein$, Deutsch$)
GOTO BL3025

BL3025:
Latein$ = "mens"
Deutsch$ = "Verstand Gesinnung Geist"
Deutsch2$ = "Verstand Geist Gesinnung"
Genus$ = "F"
Genitiv$ = "MENTIS"
Vok3025$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL3026

BL3026:
Latein$ = "sanus, a, um"
Deutsch$ = "gesund"
Vok3026$ = Methode2$(Latein$, Deutsch$)
GOTO BL3027

BL3027:
Latein$ = "iacere"
Deutsch$ = "werfen"
Vok3027$ = Methode2$(Latein$, Deutsch$)
GOTO BL3028

BL3028:
Latein$ = "deicere"
Deutsch$ = "hinabwerfen verschlagen"
Deutsch2$ = "verschlagen hinabwerfen"
Vok3028$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3029

BL3029:
Latein$ = "mortalis, e"
Deutsch$ = "sterblich"
Vok3029$ = Methode2$(Latein$, Deutsch$)
GOTO BL3030

BL3030:
Latein$ = "immortalis, e"
Deutsch$ = "unsterblich"
Vok3030$ = Methode2$(Latein$, Deutsch$)
GOTO BL3031

BL3031:
Latein$ = "constans"
Deutsch$ = "standhaft bestÑndig"
Deutsch2$ = "bestÑndig standhaft"
'Genitiv$ = "CONSTANTIS"
Vok3031$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3032

BL3032:
Latein$ = "constituere"
Deutsch$ = "festsetzen beschlie·en"
Deutsch2$ = "beschlie·en festsetzen"
Vok3032$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3033

BL3033:
Latein$ = "invitus, a, um"
Deutsch$ = "wiederwillig"
Vok3033$ = Methode2$(Latein$, Deutsch$)
GOTO Ende30

Ende30:
IF Vok3001$ = "Falsch" THEN
GOTO BL3001
ELSEIF Vok3002$ = "Falsch" THEN
GOTO BL3002
ELSEIF Vok3003$ = "Falsch" THEN
GOTO BL3003
ELSEIF Vok3004$ = "Falsch" THEN
GOTO BL3004
ELSEIF Vok3005$ = "Falsch" THEN
GOTO BL3005
ELSEIF Vok3006$ = "Falsch" THEN
GOTO BL3006
ELSEIF Vok3007$ = "Falsch" THEN
GOTO BL3007
ELSEIF V2k3008$ = "Falsch" THEN
GOTO BL3008
ELSEIF Vok3009$ = "Falsch" THEN
GOTO BL3009
ELSEIF Vok3010$ = "Falsch" THEN
GOTO BL3010
ELSEIF Vok3011$ = "Falsch" THEN
GOTO BL3011
ELSEIF Vok3012$ = "Falsch" THEN
GOTO BL3012
ELSEIF Vok3013$ = "Falsch" THEN
GOTO BL3013
ELSEIF Vok3014$ = "Falsch" THEN
GOTO BL3014
ELSEIF Vok3015$ = "Falsch" THEN
GOTO BL3015
ELSEIF Vok3016$ = "Falsch" THEN
GOTO BL3016
ELSEIF Vok3017$ = "Falsch" THEN
GOTO BL3017
ELSEIF Vok3018$ = "Falsch" THEN
GOTO BL3018
ELSEIF Vok3019$ = "Falsch" THEN
GOTO BL3019
ELSEIF Vok3020$ = "Falsch" THEN
GOTO BL3020
ELSEIF Vok3021$ = "Falsch" THEN
GOTO BL3021
ELSEIF Vok3022$ = "Falsch" THEN
GOTO BL3022
ELSEIF Vok3023$ = "Falsch" THEN
GOTO BL3023
ELSEIF Vok3024$ = "Falsch" THEN
GOTO BL3024
ELSEIF Vok3025$ = "Falsch" THEN
GOTO BL3025
ELSEIF Vok3026$ = "Falsch" THEN
GOTO BL3026
ELSEIF Vok3027$ = "Falsch" THEN
GOTO BL3027
ELSEIF Vok3028$ = "Falsch" THEN
GOTO BL3028
ELSEIF Vok3029$ = "Falsch" THEN
GOTO BL3029
ELSEIF Vok3030$ = "Falsch" THEN
GOTO BL3030
ELSEIF Vok3031$ = "Falsch" THEN
GOTO BL3031
ELSEIF Vok3032$ = "Falsch" THEN
GOTO BL3032
ELSEIF Vok3033$ = "Falsch" THEN
GOTO BL3033
ELSE GOTO Letztende30
END IF

Letztende30:
END SUB

SUB BlauLektion31

BL3101:
Latein$ = "fatum"
Deutsch$ = "Schicksal"
Genus$ = "N"
Genitiv$ = "FATI"
Vok3101$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3102

BL3102:
Latein$ = "quam (M. Komparativ)"
Deutsch$ = "als"
Vok3102$ = Methode2$(Latein$, Deutsch$)
GOTO BL3103

BL3103:
Latein$ = "age!, agite!"
Deutsch$ = "auf!"
Vok3103$ = Methode2$(Latein$, Deutsch$)
GOTO BL3104

BL3104:
Latein$ = "certamen"
Deutsch$ = "Wettkampf"
Genus$ = "N"
Genitiv$ = "CERTAMINIS"
Vok3104$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3105

BL3105:
Latein$ = "umquam"
Deutsch$ = "jemals"
Vok3105$ = Methode2$(Latein$, Deutsch$)
GOTO BL3106

BL3106:
Latein$ = "tres, tres, tria"
Deutsch$ = "drei"
Vok3106$ = Methode2$(Latein$, Deutsch$)
GOTO BL3107

BL3107:
Latein$ = "blandus, a, um"
Deutsch$ = "freundlich, schmeichelnd"
Deutsch2$ = "schmeichelnd, freundlich"
Vok3107$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3108

BL3108:
Latein$ = "nostrum (Gen. partitivus)"
Deutsch$ = "von uns"
Vok3108$ = Methode2$(Latein$, Deutsch$)
GOTO BL3109

BL3109:
Latein$ = "brevis, e"
Deutsch$ = "kurz"
Vok3109$ = Methode2$(Latein$, Deutsch$)
GOTO BL3110

BL3110:
Latein$ = "spatium"
Deutsch$ = "Strecke, Raum"
Deutsch2$ = "Raum, Strecke"
Genus$ = "N"
Genitiv$ = "SPATII"
Vok3110$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL3111

BL3111:
Latein$ = "fore"
Deutsch$ = "futurum (am, os, as, a) esse"
Deutsch2$ = "futurum esse"
Vok3111$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3112

BL3112:
Latein$ = "certare"
Deutsch$ = "streiten, kÑmpfen"
Deutsch2$ = "kÑmpfen, streiten"
Vok3112$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3113

BL3113:
Latein$ = "multo (m. Komparativ)"
Deutsch$ = "viel"
Vok3113$ = Methode2$(Latein$, Deutsch$)
GOTO BL3114

BL3114:
Latein$ = "periculosus, a, um"
Deutsch$ = "gefÑhrlich"
Vok3114$ = Methode2$(Latein$, Deutsch$)
GOTO BL3115

BL3115:
Latein$ = "carus, a, um"
Deutsch$ = "lieb, teuer"
Deutsch2$ = "teuer, lieb"
Vok3115$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3116

BL3116:
Latein$ = "fidelis, e"
Deutsch$ = "treu"
Vok3116$ = Methode2$(Latein$, Deutsch$)
GOTO BL3117

BL3117:
Latein$ = "reducere"
Deutsch$ = "zurÅckfÅhren"
Vok3117$ = Methode2$(Latein$, Deutsch$)
GOTO BL3118

BL3118:
Latein$ = "alienus, a, um"
Deutsch$ = "fremd"
Vok3118$ = Methode2$(Latein$, Deutsch$)
GOTO BL3119

BL3119:
Latein$ = "exponere"
Deutsch$ = "aussetzen"
Vok3119$ = Methode2$(Latein$, Deutsch$)
GOTO BL3120

BL3120:
Latein$ = "sors"
Deutsch$ = "Los, Schicksal"
Deutsch2$ = "Schicksal, Los"
Genus$ = "F"
Genitiv$ = "SORTIS"
Vok3120$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL3121

BL3121:
Latein$ = "obviam (Adv.)"
Deutsch$ = "entgegen"
Vok3121$ = Methode2$(Latein$, Deutsch$)
GOTO BL3122

BL3122:
Latein$ = "cautus, a, um"
Deutsch$ = "vorsichtig"
Vok3122$ = Methode2$(Latein$, Deutsch$)
GOTO BL3123


BL3123:
Latein$ = "mortuus, a, um"
Deutsch$ = "tot"
Vok3123$ = Methode2$(Latein$, Deutsch$)
GOTO BL3124

BL3124:
Latein$ = "eligere"
Deutsch$ = "auswÑhlen"
Vok3124$ = Methode2$(Latein$, Deutsch$)
GOTO BL3125

BL3125:
Latein$ = "species"
Deutsch$ = "Aussehen, Gestalt"
Deutsch2$ = "Gestalt, Aussehen"
Genus$ = "F"
Genitiv$ = "SPECIEI"
Vok3125$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL3126

BL3126:
Latein$ = "incognitus, a, um"
Deutsch$ = "unerkannt, unbekannt"
Deutsch2$ = "unbekannt, unerkannt"
Vok3126$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3127

BL3127:
Latein$ = "tolerare"
Deutsch$ = "ertragen"
Vok3127$ = Methode2$(Latein$, Deutsch$)
GOTO BL3128

BL3128:
Latein$ = "arcus"
Deutsch$ = "Bogen"
Genus$ = "M"
Genitiv$ = "ARCUS"
Vok3128$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3129

BL3129:
Latein$ = "tendere"
Deutsch$ = "spannen"
Vok3129$ = Methode2$(Latein$, Deutsch$)
GOTO BL3130

BL3130:
Latein$ = "matrimonium"
Deutsch$ = "Ehe"
Genus$ = "N"
Genitiv$ = "MATRIMONII"
Vok3130$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3131

BL3131:
Latein$ = "iustus, a, um"
Deutsch$ = "gerecht, rechtmÑ·ig"
Deutsch2$ = "rechtmÑ·ig, gerecht"
Vok3131$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO Ende31

Ende31:
IF Vok3101$ = "Falsch" THEN
GOTO BL3101
ELSEIF Vok3102$ = "Falsch" THEN
GOTO BL3102
ELSEIF Vok3103$ = "Falsch" THEN
GOTO BL3103
ELSEIF Vok3104$ = "Falsch" THEN
GOTO BL3104
ELSEIF Vok3105$ = "Falsch" THEN
GOTO BL3105
ELSEIF Vok3106$ = "Falsch" THEN
GOTO BL3106
ELSEIF Vok3107$ = "Falsch" THEN
GOTO BL3107
ELSEIF V2k3108$ = "Falsch" THEN
GOTO BL3108
ELSEIF Vok3109$ = "Falsch" THEN
GOTO BL3109
ELSEIF Vok3110$ = "Falsch" THEN
GOTO BL3110
ELSEIF Vok3111$ = "Falsch" THEN
GOTO BL3111
ELSEIF Vok3112$ = "Falsch" THEN
GOTO BL3112
ELSEIF Vok3113$ = "Falsch" THEN
GOTO BL3113
ELSEIF Vok3114$ = "Falsch" THEN
GOTO BL3114
ELSEIF Vok3115$ = "Falsch" THEN
GOTO BL3115
ELSEIF Vok3116$ = "Falsch" THEN
GOTO BL3116
ELSEIF Vok3117$ = "Falsch" THEN
GOTO BL3117
ELSEIF Vok3118$ = "Falsch" THEN
GOTO BL3118
ELSEIF Vok3119$ = "Falsch" THEN
GOTO BL3119
ELSEIF Vok3120$ = "Falsch" THEN
GOTO BL3120
ELSEIF Vok3021$ = "Falsch" THEN
GOTO BL3121
ELSEIF Vok3122$ = "Falsch" THEN
GOTO BL3122
ELSEIF Vok3123$ = "Falsch" THEN
GOTO BL3123
ELSEIF Vok3124$ = "Falsch" THEN
GOTO BL3124
ELSEIF Vok3125$ = "Falsch" THEN
GOTO BL3125
ELSEIF Vok3126$ = "Falsch" THEN
GOTO BL3126
ELSEIF Vok3127$ = "Falsch" THEN
GOTO BL3127
ELSEIF Vok3128$ = "Falsch" THEN
GOTO BL3128
ELSEIF Vok3129$ = "Falsch" THEN
GOTO BL3129
ELSEIF Vok3130$ = "Falsch" THEN
GOTO BL3130
ELSEIF Vok3131$ = "Falsch" THEN
GOTO BL3131
END IF

Letztende31:
END SUB

SUB BlauLektion32

BL3201:
Latein$ = "iussu"
Deutsch$ = "auf Befehl"
Vok3201$ = Methode2$(Latein$, Deutsch$)
GOTO BL3202

BL3202:
Latein$ = "utilis, e"
Deutsch$ = "nÅtzlich, brauchbar"
Deutsch2$ = "brauchbar, nÅtzlich"
Vok3202$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3203

BL3203:
Latein$ = "inutilis, e"
Deutsch$ = "unnÅtz, unbrauchbar"
Deutsch2$ = "unbrauchbar, unnÅtz"
Vok3203$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3204

BL3204:
Latein$ = "domare"
Deutsch$ = "zÑhmen, unterwerfen"
Deutsch2$ = "unterwerfen, zÑhmen"
Vok3204$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3205

BL3205:
Latein$ = "sapiens (Gen.: sapientis)"
Deutsch$ = "klug, weise"
Deutsch2$ = "weise, klug"
Genus$ = "SAPIENTIS"
Genitiv$ = "SAPIENTIS"
Vok3205$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL3206

BL3206:
Latein$ = "vertere"
Deutsch$ = "wenden"
Vok3206$ = Methode2$(Latein$, Deutsch$)
GOTO BL3207

BL3207:
Latein$ = "placare"
Deutsch$ = "beruhigen, besÑnftigen"
Deutsch2$ = "besÑnftigen, beruhigen"
Vok3207$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3208

BL3208:
Latein$ = "equidem"
Deutsch$ = "ich jedenfalls"
Vok3208$ = Methode2$(Latein$, Deutsch$)
GOTO BL3209

BL3209:
Latein$ = "pretium"
Deutsch$ = "Preis"
Genus$ = "N"
Genitiv$ = "PRETII"
Vok3209$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3210

BL3210:
Latein$ = "philosophus"
Deutsch$ = "Philosoph"
Genus$ = "M"
Genitiv$ = "PHILOSOPHI"
Vok3210$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3211

BL3211:
Latein$ = "cum primum"
Deutsch$ = "sobald"
Vok3211$ = Methode2$(Latein$, Deutsch$)
GOTO BL3212

BL3212:
Latein$ = "ubi primum"
Deutsch$ = "sobald"
Vok3212$ = Methode2$(Latein$, Deutsch$)
GOTO BL3213

BL3213:
Latein$ = "dominatio"
Deutsch$ = "Herrschaft"
Genus$ = "F"
Genitiv$ = "DOMINATIONIS"
Vok3213$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3214

BL3214:
Latein$ = "quoque"
Deutsch$ = "auch"
Vok3214$ = Methode2$(Latein$, Deutsch$)
GOTO BL3215

BL3215:
Latein$ = "usque ad (m. Akk.)"
Deutsch$ = "bis zu"
Vok3215$ = Methode2$(Latein$, Deutsch$)
GOTO BL3216

BL3216:
Latein$ = "potens (Gen. Potentis)"
Deutsch$ = "mÑchtig"
Genus$ = "MéCHTIG"
Genitiv$ = "MéCHTIG"
Vok3216$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3217

BL3217:
Latein$ = "impedire"
Deutsch$ = "hindern, verhindern"
Deutsch2$ = "verhindern, hindern"
Vok3217$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3218

BL3218:
Latein$ = "currus"
Deutsch$ = "Wagen"
Genus$ = "M"
Genitiv$ = "CURRUS"
Vok3218$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3219

BL3219:
Latein$ = "universus, a, um"
Deutsch$ = "gesamt, allein"
Deutsch2$ = "allein, gesamt"
Vok3219$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3220

BL3220:
Latein$ = "perficere"
Deutsch$ = "vollenden, vollbringen"
Deutsch2$ = "vollbringen, vollenden"
Vok3220$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3221

BL3221:
Latein$ = "deficere"
Deutsch$ = "abnehmen, schwinden, versagen"
Deutsch2$ = "abnehmen, versagen, schwinden"
Vok3221$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3222

BL3222:
Latein$ = "exclamare"
Deutsch$ = "ausrufen"
Vok3222$ = Methode2$(Latein$, Deutsch$)
GOTO BL3223

BL3223:
Latein$ = "intervest inter (m. Akk.)"
Deutsch$ = "es ist ein Unterschied zwischen"
Vok3223$ = Methode2$(Latein$, Deutsch$)
GOTO BL3224

BL3224:
Latein$ = "divinus, a, um"
Deutsch$ = "gîttlich"
Vok3224$ = Methode2$(Latein$, Deutsch$)
GOTO Ende32

Ende32:
IF Vok3201$ = "Falsch" THEN
GOTO BL3201
ELSEIF Vok3202$ = "Falsch" THEN
GOTO BL3202
ELSEIF Vok3203$ = "Falsch" THEN
GOTO BL3203
ELSEIF Vok3204$ = "Falsch" THEN
GOTO BL3204
ELSEIF Vok3205$ = "Falsch" THEN
GOTO BL3205
ELSEIF Vok3206$ = "Falsch" THEN
GOTO BL3206
ELSEIF Vok3207$ = "Falsch" THEN
GOTO BL3207
ELSEIF V2k3208$ = "Falsch" THEN
GOTO BL3208
ELSEIF Vok3209$ = "Falsch" THEN
GOTO BL3209
ELSEIF Vok3210$ = "Falsch" THEN
GOTO BL3210
ELSEIF Vok3211$ = "Falsch" THEN
GOTO BL3211
ELSEIF Vok3212$ = "Falsch" THEN
GOTO BL3212
ELSEIF Vok3213$ = "Falsch" THEN
GOTO BL3213
ELSEIF Vok3214$ = "Falsch" THEN
GOTO BL3214
ELSEIF Vok3215$ = "Falsch" THEN
GOTO BL3215
ELSEIF Vok3216$ = "Falsch" THEN
GOTO BL3216
ELSEIF Vok3217$ = "Falsch" THEN
GOTO BL3217
ELSEIF Vok3218$ = "Falsch" THEN
GOTO BL3218
ELSEIF Vok3219$ = "Falsch" THEN
GOTO BL3219
ELSEIF Vok3220$ = "Falsch" THEN
GOTO BL3220
ELSEIF Vok3221$ = "Falsch" THEN
GOTO BL3221
ELSEIF Vok3222$ = "Falsch" THEN
GOTO BL3222
ELSEIF Vok3223$ = "Falsch" THEN
GOTO BL3223
ELSEIF Vok3224$ = "Falsch" THEN
GOTO BL3224
ELSE GOTO Letztende32
END IF

Letztende32:
END SUB

SUB BlauLektion33

BL3301:
Latein$ = "discipulus"
Deutsch$ = "SchÅler"
Genus$ = "M"
Genitiv$ = "DISCIPULI"
Vok3301$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3302

BL3302:
Latein$ = "Athenis (Abl.)"
Deutsch$ = "in Athen"
Vok3302$ = Methode2$(Latein$, Deutsch$)
GOTO BL3303

BL3304:
Latein$ = "iustia"
Deutsch$ = "Gerechtigkeit"
Genus$ = "F"
Genitiv$ = "IUSTIA"
Vok3304$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3305

BL3305:
Latein$ = "coram (m. Abl.)"
Deutsch$ = "vor, im Angesicht von"
Deutsch2$ = "im Angesicht von, vor"
Vok3305$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3306

BL3303:
Latein$ = "disputare"
Deutsch$ = "sich unterhalten, diskutieren"
Deutsch2$ = "diskutieren, sich unterhalten"
Vok3303$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3304

BL3307:
Latein$ = "faber"
Deutsch$ = "Handwerker"
Genus$ = "M"
Genitiv$ = "FABRI"
Vok3307$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3308

BL3308:
Latein$ = "fraudare"
Deutsch$ = "betrÅgen"
Vok3308$ = Methode2$(Latein$, Deutsch$)
GOTO BL3309

BL3309:
Latein$ = "bona (Pl.)"
Deutsch$ = "GÅter, Besitz"
Deutsch2$ = "Besitz, GÅter"
Genus$ = "N"
Genitiv$ = "BONORUM"
Vok3309$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL3310

BL3310:
Latein$ = "sin"
Deutsch$ = "wenn aber"
Vok3310$ = Methode2$(Latein$, Deutsch$)
GOTO BL3311

BL3306:
Latein$ = "privatus, a, um"
Deutsch$ = "privat, ohne Staatsamt"
Deutsch2$ = "ohne Staatsamt, privat"
Vok3306$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3307

BL3311:
Latein$ = "fraus"
Deutsch$ = "Betrug"
Genus$ = "F"
Genitiv$ = "FRAUDIS"
Vok3311$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3312

BL3312:
Latein$ = "medicina"
Deutsch$ = "Heilmittel"
Genus$ = "F"
Genitiv$ = "MEDICINAE"
Vok3312$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3313

BL3313:
Latein$ = "repudiare"
Deutsch$ = "zurÅckweisen"
Vok3313$ = Methode2$(Latein$, Deutsch$)
GOTO BL3314

BL3314:
Latein$ = "sanare"
Deutsch$ = "heilen"
Vok3314$ = Methode2$(Latein$, Deutsch$)
GOTO BL3315

BL3315:
Latein$ = "contio"
Deutsch$ = "(Volks)versammlung"
Genus$ = "F"
Genitiv$ = "CONTIONIS"
Vok3315$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3316

BL3316:
Latein$ = "prudens (Gen.: prudentis)"
Deutsch$ = "klug"
Genus$ = "PRUDENTIS"
Genitiv$ = "PRUDENTIS"
Vok3316$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3317

BL3317:
Latein$ = "ignis"
Deutsch$ = "Feuer"
Genus$ = "M"
Genitiv$ = "IGNIS"
Vok3317$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL3318

BL3318:
Latein$ = "adhuc (Adv.)"
Deutsch$ = "bis jetzt"
Vok3318$ = Methode2$(Latein$, Deutsch$)
GOTO BL3319

BL3319:
Latein$ = "principatus"
Deutsch$ = "Vormacht, fÅhrende Stellung"
Deutsch2$ = "fÅhrende Stellung, Vormacht"
Genus$ = "M"
Genitiv$ = "PRINCIPATUS"
Vok3319$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL3320

BL3320:
Latein$ = "honestus, a, um"
Deutsch$ = "ehrenvoll, anstÑndig, angesehen"
Deutsch2$ = "angesehen, ehrenvoll, anstÑndig"
Vok3320$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3321

BL3321:
Latein$ = "ne...quidem"
Deutsch$ = "nicht einmal"
Vok3321$ = Methode2$(Latein$, Deutsch$)
GOTO BL3322

BL3322:
Latein$ = "fortasse (Adv.)"
Deutsch$ = "vielleicht"
Vok3322$ = Methode2$(Latein$, Deutsch$)
GOTO BL3323

BL3323:
Latein$ = "odium"
Deutsch$ = "Ha·"
Genus$ = "N"
Genitiv$ = "ODII"
Vok3323$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO Ende33

Ende33:
IF Vok3301$ = "Falsch" THEN
GOTO BL3301
ELSEIF Vok3302$ = "Falsch" THEN
GOTO BL3302
ELSEIF Vok3303$ = "Falsch" THEN
GOTO BL3303
ELSEIF Vok3304$ = "Falsch" THEN
GOTO BL3304
ELSEIF Vok3305$ = "Falsch" THEN
GOTO BL3305
ELSEIF Vok3306$ = "Falsch" THEN
GOTO BL3306
ELSEIF Vok3307$ = "Falsch" THEN
GOTO BL3307
ELSEIF V2k3308$ = "Falsch" THEN
GOTO BL3308
ELSEIF Vok3309$ = "Falsch" THEN
GOTO BL3309
ELSEIF Vok3310$ = "Falsch" THEN
GOTO BL3310
ELSEIF Vok3311$ = "Falsch" THEN
GOTO BL3311
ELSEIF Vok3312$ = "Falsch" THEN
GOTO BL3312
ELSEIF Vok3313$ = "Falsch" THEN
GOTO BL3313
ELSEIF Vok3314$ = "Falsch" THEN
GOTO BL3314
ELSEIF Vok3315$ = "Falsch" THEN
GOTO BL3315
ELSEIF Vok3316$ = "Falsch" THEN
GOTO BL3316
ELSEIF Vok3317$ = "Falsch" THEN
GOTO BL3317
ELSEIF Vok3318$ = "Falsch" THEN
GOTO BL3318
ELSEIF Vok3319$ = "Falsch" THEN
GOTO BL3319
ELSEIF Vok3320$ = "Falsch" THEN
GOTO BL3320
ELSEIF Vok3321$ = "Falsch" THEN
GOTO BL3321
ELSEIF Vok3322$ = "Falsch" THEN
GOTO BL3322
ELSEIF Vok3323$ = "Falsch" THEN
GOTO BL3323
ELSE GOTO Letztende33
END IF

Letztende33:
END SUB

SUB BlauLektion34
END SUB

SUB BlauLektion35
END SUB

SUB BlauLektion4

BL0401:
Latein$ = "sunt"
Deutsch$ = "sind"
Vok0401$ = Methode2$(Latein$, Deutsch$)
GOTO BL0401

BL0402:
Latein$ = "puella"
Deutsch$ = "MÑdchen"
Genus$ = "F"
Genitiv$ = "PUELLAE"
Vok0402$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0403

BL0403:
Latein$ = "orare"
Deutsch$ = "reden, bitten, beten"
Deutsch2$ = "reden, beten, bitten"
Vok0403$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0404

BL0404:
Latein$ = "liberi (Pl.)"
Deutsch$ = "Kinder"
Genus$ = "M"
Genitiv$ = "LIBERORUM"
Vok0404$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0405

BL0405:
Latein$ = "considere"
Deutsch$ = "sich setzen"
Vok0405$ = Methode2$(Latein$, Deutsch$)
GOTO BL0406

BL0406:
Latein$ = "multi, ae, a"
Deutsch$ = "viele"
Vok0406$ = Methode2$(Latein$, Deutsch$)
GOTO BL0407

BL0407:
Latein$ = "amare"
Deutsch$ = "lieben"
Vok0407$ = Methode2$(Latein$, Deutsch$)
GOTO BL0408

BL0408:
Latein$ = "itaque"
Deutsch$ = "daher, deshalb"
Deutsch2$ = "deshalb, daher"
Vok0408$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0409

BL0409:
Latein$ = "incitare"
Deutsch$ = "antreiben"
Vok0409$ = Methode2$(Latein$, Deutsch$)
GOTO BL0410

BL0410:
Latein$ = "res"
Deutsch$ = "Sache, Ding, Besitz"
Deutsch2$ = "Sache, Besitz, Ding "
Genus$ = "F"
Genitiv$ = "REI"
Vok0410$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0411

BL0411:
Latein$ = "iucundus, a, um"
Deutsch$ = "angenehm"
Vok0411$ = Methode2$(Latein$, Deutsch$)
GOTO BL0412

BL0412:
Latein$ = "subito (Adv.)"
Deutsch$ = "plîtzlich"
Vok0412$ = Methode2$(Latein$, Deutsch$)
GOTO BL0413

BL0413:
Latein$ = "aperire"
Deutsch$ = "îffnen"
Vok0413$ = Methode2$(Latein$, Deutsch$)
GOTO BL0414

BL0414:
Latein$ = "severus, a, um"
Deutsch$ = "streng"
Vok0414$ = Methode2$(Latein$, Deutsch$)
GOTO BL0415

BL0415:
Latein$ = "postea (Adv.)"
Deutsch$ = "danach, spÑter"
Deutsch2$ = "spÑter, danach"
Vok0415$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0416

BL0416:
Latein$ = "arbor"
Deutsch$ = "Baum"
Genus$ = "F"
Genitiv$ = "ORBORIS"
Vok0416$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0417

BL0417:
Latein$ = "altus, a, um"
Deutsch$ = "hoch, tief"
Deutsch2$ = "tief, hoch"
Vok0417$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0418

BL0418:
Latein$ = "caedere"
Deutsch$ = "fallen, tîten"
Deutsch2$ = "tîten, fallen"
Vok0418$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0419

BL0419:
Latein$ = "longus, a, um"
Deutsch$ = "lang"
Vok0419$ = Methode2$(Latein$, Deutsch$)
GOTO BL0420

BL0420:
Latein$ = "trahere"
Deutsch$ = "ziehen, schleppen"
Deutsch2$ = "schleppen, ziehen"
Vok0420$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0421

BL0421:
Latein$ = "agere"
Deutsch$ = "treiben, betreiben, verhandeln"
Deutsch2$ = "treiben, verhandeln, betreiben"
Vok0421$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0422

BL0422:
Latein$ = "feriae (Pl.)"
Deutsch$ = "Ferien"
Genus$ = "F"
Genitiv$ = "FERAIARUM"
Vok0422$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0423

BL0423:
Latein$ = "ludere"
Deutsch$ = "spielen"
Vok0423$ = Methode2$(Latein$, Deutsch$)
GOTO BL0424

BL0424:
Latein$ = "laetus, a, um"
Deutsch$ = "frîhlich, vergnÅgt"
Deutsch2$ = "vergnÅgt, frîhlich"
Vok0424$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0425

BL0425:
Latein$ = "locus"
Deutsch$ = "Ort, Platz, Stelle"
Deutsch2$ = "Stelle, Ort, Platz"
Vok0425$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0426

BL0426:
Latein$ = "apertus, a, um"
Deutsch$ = "offen"
Vok0426$ = Methode2$(Latein$, Deutsch$)
GOTO BL0427

BL0427:
Latein$ = "ibi (Adv.)"
Deutsch$ = "dort"
Vok0427$ = Methode2$(Latein$, Deutsch$)
GOTO BL0428

BL0428:
Latein$ = "ascendere"
Deutsch$ = "besteigen, hinaufsteigen"
Deutsch2$ = "hinaufsteigen, besteigen"
Vok0428$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0429

BL0429:
Latein$ = "ridere"
Deutsch$ = "lachen"
Vok0429$ = Methode2$(Latein$, Deutsch$)
GOTO BL0430

BL0430:
Latein$ = "tandem"
Deutsch$ = "endlich"
Vok0430$ = Methode2$(Latein$, Deutsch$)
GOTO BL0431

BL0431:
Latein$ = "tumulus"
Deutsch$ = "HÅgel"
Genus$ = "M"
Genitiv$ = "TUMULI"
Vok0431$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0432

BL0432:
Latein$ = "vicus"
Deutsch$ = "Dorf"
Deutsch2$ = "auffm' Dorf"
Genus$ = "F"
Genitiv$ = "VICI"
Vok0432$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO Ende04

Ende04:
IF Vok0401$ = "Falsch" THEN
GOTO BL0401
ELSEIF Vok0402$ = "Falsch" THEN
GOTO BL0402
ELSEIF Vok0403$ = "Falsch" THEN
GOTO BL0403
ELSEIF Vok0404$ = "Falsch" THEN
GOTO BL0404
ELSEIF Vok0405$ = "Falsch" THEN
GOTO BL0405
ELSEIF Vok0406$ = "Falsch" THEN
GOTO BL0406
ELSEIF Vok0407$ = "Falsch" THEN
GOTO BL0407
ELSEIF Vok0408$ = "Falsch" THEN
GOTO BL0408
ELSEIF Vok0409$ = "Falsch" THEN
GOTO BL0409
ELSEIF Vok0410$ = "Falsch" THEN
GOTO BL0410
ELSEIF Vok0411$ = "Falsch" THEN
GOTO BL0411
ELSEIF Vok0412$ = "Falsch" THEN
GOTO BL0412
ELSEIF Vok0413$ = "Falsch" THEN
GOTO BL0413
ELSEIF Vok0414$ = "Falsch" THEN
GOTO BL0414
ELSEIF Vok0415$ = "Falsch" THEN
GOTO BL0415
ELSEIF Vok0416$ = "Falsch" THEN
GOTO BL0416
ELSEIF Vok0417$ = "Falsch" THEN
GOTO BL0417
ELSEIF Vok0418$ = "Falsch" THEN
GOTO BL0418
ELSEIF Vok0419$ = "Falsch" THEN
GOTO BL0419
ELSEIF Vok0420$ = "Falsch" THEN
GOTO BL0420
ELSEIF Vok0421$ = "Falsch" THEN
GOTO BL0421
ELSEIF Vok0422$ = "Falsch" THEN
GOTO BL0422
ELSEIF Vok0423$ = "Falsch" THEN
GOTO BL0423
ELSEIF Vok0424$ = "Falsch" THEN
GOTO BL0424
ELSEIF Vok0425$ = "Falsch" THEN
GOTO BL0425
ELSEIF Vok0426$ = "Falsch" THEN
GOTO BL0426
ELSEIF Vok0427$ = "Falsch" THEN
GOTO BL0427
ELSEIF Vok0428$ = "Falsch" THEN
GOTO BL0428
ELSEIF Vok0429$ = "Falsch" THEN
GOTO BL0429
ELSEIF Vok0430$ = "Falsch" THEN
GOTO BL0430
ELSEIF Vok0431$ = "Falsch" THEN
GOTO BL0431
ELSEIF Vok0432$ = "Falsch" THEN
GOTO BL0432
ELSE GOTO Letztende04
END IF

Letztende04:
END SUB

SUB BlauLektion5

BL0501:
Latein$ = "equitare "
Deutsch$ = "reiten"
Vok0501$ = Methode2$(Latein$, Deutsch$)
GOTO BL0502

BL0502:
Latein$ = "cupere"
Deutsch$ = "wÅnschen, wollen, verlangen"
Deutsch2$ = "wÅnschen, verlangen, wollen"
Vok0502$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0503

BL0503:
Latein$ = "potesse"
Deutsch$ = "kînnen"
Vok0503$ = Methode2$(Latein$, Deutsch$)
GOTO BL0504

BL0504:
Latein$ = "sinere"
Deutsch$ = "lassen, zulassen"
Deutsch2$ = "zulassen, lassen"
Vok0504$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0505

BL0505:
Latein$ = "iubere"
Deutsch$ = "befehlen, auffordern"
Deutsch2$ = "auffordern, befehlen"
Vok0505$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0506

BL0506:
Latein$ = "conscendere"
Deutsch$ = "besteigen"
Vok0506$ = Methode2$(Latein$, Deutsch$)
GOTO BL0507

BL0507:
Latein$ = "vetare"
Deutsch$ = "verbieten"
Vok0507$ = Methode2$(Latein$, Deutsch$)
GOTO BL0508

BL0509:
Latein$ = "mater"
Deutsch$ = "Mutter"
Genus$ = "F"
Genitiv$ = "MATRORUM"
Vok0509$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0510

BL0508:
Latein$ = "conspidere"
Deutsch$ = "erblicken"
Vok0509$ = Methode2$(Latein$, Deutsch$)
GOTO BL0510

BL0510:
Latein$ = "pater"
Deutsch$ = "Vater"
Genus$ = "M"
Genitiv$ = "PATRORUM"
Vok0510$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0511

BL0511:
Latein$ = "esse"
Deutsch$ = "sein"
Vok0511$ = Methode2$(Latein$, Deutsch$)
GOTO BL0512

BL0512:
Latein$ = "familia"
Deutsch$ = "Familie, Hausgemeinschaft"
Genus$ = "F"
Genitiv$ = "FAMILIAE"
Vok0512$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0513

BL0513:
Latein$ = "vespertinus, a, um"
Deutsch$ = "abendlich, Abend-"
Deutsch2$ = "Abend-, abendlich"
Vok0513$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0514

BL0514:
Latein$ = "adesse"
Deutsch$ = "dasein, anwesend sein"
Deutsch2$ = "anwesend sein, dasein"
Vok0514$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0515

BL0515:
Latein$ = "conviva"
Deutsch$ = "Gast"
Genus$ = "M"
Genitiv$ = "CONVIVAE"
Vok0515$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0516

BL0516:
Latein$ = "invitare"
Deutsch$ = "einladen"
Vok0516$ = Methode2$(Latein$, Deutsch$)
GOTO BL0517

BL0517:
Latein$ = "ante (m. Akk.), (Adv.)"
Deutsch$ = "vor, vorher"
Deutsch2$ = "vorher, vor"
Vok0517$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0518

BL0518:
Latein$ = "et...et"
Deutsch$ = "sowohl...als auch"
Vok0518$ = Methode2$(Latein$, Deutsch$)
GOTO BL0519

BL0519:
Latein$ = "purgare"
Deutsch$ = "sÑubern, reinigen"
Deutsch2$ = "reinigen, sÑubern"
Vok0519$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0520

BL0520:
Latein$ = "finire"
Deutsch$ = "beenden"
Vok0520$ = Methode2$(Latein$, Deutsch$)
GOTO BL0521

BL0521:
Latein$ = "aegrotus, a, um"
Deutsch$ = "krank"
Vok0521$ = Methode2$(Latein$, Deutsch$)
GOTO BL0522

BL0522:
Latein$ = "diu (Adv.)"
Deutsch$ = "lange"
Vok0522$ = Methode2$(Latein$, Deutsch$)
GOTO BL0523

BL0523:
Latein$ = "exspectare"
Deutsch$ = "erwarten"
Vok0523$ = Methode2$(Latein$, Deutsch$)
GOTO BL0524

BL0524:
Latein$ = "scire"
Deutsch$ = "wissen"
Vok0524$ = Methode2$(Latein$, Deutsch$)
GOTO BL0525

BL0525:
Latein$ = "medicus"
Deutsch$ = "Arzt"
Genus$ = "M"
Genitiv$ = "MEDICI"
Vok0525$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0526

BL0526:
Latein$ = "mensa"
Deutsch$ = "Tisch"
Genus$ = "F"
Genitiv$ = "MENSAE"
Vok0526$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0527

BL0527:
Latein$ = "lectus"
Deutsch$ = "Bett, Liege"
Deutsch2$ = "Liege, Bett"
Genus$ = "M"
Genitiv$ = "LECTI"
Vok0527$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO Ende05

Ende05:
IF Vok0501$ = "Falsch" THEN
GOTO BL0501
ELSEIF Vok0502$ = "Falsch" THEN
GOTO BL0502
ELSEIF Vok0503$ = "Falsch" THEN
GOTO BL0503
ELSEIF Vok0504$ = "Falsch" THEN
GOTO BL0504
ELSEIF Vok0505$ = "Falsch" THEN
GOTO BL0505
ELSEIF Vok0506$ = "Falsch" THEN
GOTO BL0506
ELSEIF Vok0507$ = "Falsch" THEN
GOTO BL0507
ELSEIF Vok0508$ = "Falsch" THEN
GOTO BL0508
ELSEIF Vok0509$ = "Falsch" THEN
GOTO BL0509
ELSEIF Vok0510$ = "Falsch" THEN
GOTO BL0510
ELSEIF Vok0511$ = "Falsch" THEN
GOTO BL0511
ELSEIF Vok0512$ = "Falsch" THEN
GOTO BL0512
ELSEIF Vok0513$ = "Falsch" THEN
GOTO BL0513
ELSEIF Vok0514$ = "Falsch" THEN
GOTO BL0514
ELSEIF Vok0515$ = "Falsch" THEN
GOTO BL0515
ELSEIF Vok0516$ = "Falsch" THEN
GOTO BL0516
ELSEIF Vok0517$ = "Falsch" THEN
GOTO BL0517
ELSEIF Vok0518$ = "Falsch" THEN
GOTO BL0518
ELSEIF Vok0519$ = "Falsch" THEN
GOTO BL0519
ELSEIF Vok0520$ = "Falsch" THEN
GOTO BL0520
ELSEIF Vok0521$ = "Falsch" THEN
GOTO BL0521
ELSEIF Vok0522$ = "Falsch" THEN
GOTO BL0522
ELSEIF Vok0523$ = "Falsch" THEN
GOTO BL0523
ELSEIF Vok0524$ = "Falsch" THEN
GOTO BL0524
ELSEIF Vok0525$ = "Falsch" THEN
GOTO BL0525
ELSEIF Vok0526$ = "Falsch" THEN
GOTO BL0526
ELSEIF Vok0527$ = "Falsch" THEN
GOTO BL0527
ELSE GOTO Letztende05
END IF

Letztende05:
END SUB

SUB BlauLektion6

BL0601:
Latein$ = "cum (m. Abl.)"
Deutsch$ = "mit"
Vok0601$ = Methode2$(Latein$, Deutsch$)
GOTO BL0602

BL0602:
Latein$ = "parare"
Deutsch$ = "(vor)bereiten, verschaffen"
Deutsch2$ = "verschaffen, (vor)bereiten"
Vok0602$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0603

BL0603:
Latein$ = "facere"
Deutsch$ = "machen, tun"
Deutsch2$ = "tun, machen"
Vok0603$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0604

BL0604:
Latein$ = "vesper"
Deutsch$ = "Abend"
Genus$ = "M"
Genitiv$ = "VESPERI"
Vok0604$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0605

BL0605:
Latein$ = "appropinquare"
Deutsch$ = "sich nÑhern"
ok0605$ = Methode2$(Latein$, Deutsch$)
GOTO BL0606

BL0606:
Latein$ = "properare"
Deutsch$ = "eilen"
Vok0606$ = Methode2$(Latein$, Deutsch$)
GOTO BL0607

BL0607:
Latein$ = "celeriter (Adv.)"
Deutsch$ = "schnell"
Vok0607$ = Methode2$(Latein$, Deutsch$)
GOTO BL0608

BL0608:
Latein$ = "puer"
Deutsch$ = "Junge"
Genus$ = "M"
Genitiv$ = "PUERI"
Vok0608$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0609

BL0609:
Latein$ = "laetitia"
Deutsch$ = "Freude"
Genus$ = "F"
Genitiv$ = "LAETITAE"
Vok0609$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0610

BL0610:
Latein$ = "post (m. Akk.), (Adv.)"
Deutsch$ = "nach, hinter, danach"
Vok0610$ = Methode2$(Latein$, Deutsch$)
GOTO BL0611

BL0611:
Latein$ = "cuncti, ae, a"
Deutsch$ = "alle"
Vok0611$ = Methode2$(Latein$, Deutsch$)
GOTO BL0612

BL0612:
Latein$ = "venator"
Deutsch$ = "JÑger"
Genus$ = "M"
Genitiv$ = "VENATORIS"
Vok0612$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0613

BL0613:
Latein$ = "clamor"
Deutsch$ = "Geschrei, LÑrm"
Deutsch2$ = "LÑrm, Geschrei"
Genus$ = "M"
Genitiv$ = "CLAMORIS"
Vok0613$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0614

BL0614:
Latein$ = "lupus"
Deutsch$ = "Wolf"
Genus$ = "M"
Genitiv$ = "LUPI"
Vok0614$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0615

BL0615:
Latein$ = "hasta"
Deutsch$ = "Lanze, Speer"
Deutsch2$ = "Speer, Lanze"
Genus$ = "F"
Genitiv$ = "HASTAE"
Vok0615$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0616

BL0616:
Latein$ = "necare"
Deutsch$ = "tîten"
Vok0616$ = Methode2$(Latein$, Deutsch$)
GOTO BL0617

BL0617:
Latein$ = "-que"
Deutsch$ = "und"
Vok0617$ = Methode2$(Latein$, Deutsch$)
GOTO BL0618

BL0618:
Latein$ = "flos"
Deutsch$ = "Blume"
Genus$ = "M"
Genitiv$ = "FLORIS"
Vok0618$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0619

BL0619:
Latein$ = "colligere"
Deutsch$ = "sammeln"
Vok0619$ = Methode2$(Latein$, Deutsch$)
GOTO BL0620
 
BL0621:
Latein$ = "qua re?"
Deutsch$ = "wodurch, womit?"
Deutsch2$ = "womit?, wodurch?"
Vok0621$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0622

BL0620:
Latein$ = "ornare"
Deutsch$ = "schmÅcken"
Vok0620$ = Methode2$(Latein$, Deutsch$)
GOTO BL0621

BL0622:
Latein$ = "quocum?"
Deutsch$ = "mit wem?"
Vok0622$ = Methode2$(Latein$, Deutsch$)
GOTO BL0623

BL0623:
Latein$ = "sub (m. Akk.)"
Deutsch$ = "unter"
Vok0623$ = Methode2$(Latein$, Deutsch$)
GOTO BL0624

BL0624:
Latein$ = "corona"
Deutsch$ = "Kranz"
Genus$ = "F"
Genitiv$ = "CORONAE"
Vok0624$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0625

BL0625:
Latein$ = "alius, a, um"
Deutsch$ = "ein andere"
Vok0625$ = Methode2$(Latein$, Deutsch$)
GOTO BL0626

BL0626:
Latein$ = "surgere"
Deutsch$ = "aufstehen, sich erheben"
Deutsch2$ = "sich erheben, aufstehen"
Vok0626$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0627

BL0627:
Latein$ = "gratia"
Deutsch$ = "Dank, Ansehen, Beliebtheit"
Genus$ = "F"
Genitiv$ = "GRATIAE"
Vok0627$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO Ende06

Ende06:
IF Vok0601$ = "Falsch" THEN
GOTO BL0601
ELSEIF Vok0602$ = "Falsch" THEN
GOTO BL0602
ELSEIF Vok0603$ = "Falsch" THEN
GOTO BL0603
ELSEIF Vok0604$ = "Falsch" THEN
GOTO BL0604
ELSEIF Vok0605$ = "Falsch" THEN
GOTO BL0605
ELSEIF Vok0606$ = "Falsch" THEN
GOTO BL0606
ELSEIF Vok0607$ = "Falsch" THEN
GOTO BL0607
ELSEIF Vok0608$ = "Falsch" THEN
GOTO BL0608
ELSEIF Vok0609$ = "Falsch" THEN
GOTO BL0609
ELSEIF Vok0610$ = "Falsch" THEN
GOTO BL0610
ELSEIF Vok0611$ = "Falsch" THEN
GOTO BL0611
ELSEIF Vok0612$ = "Falsch" THEN
GOTO BL0612
ELSEIF Vok0613$ = "Falsch" THEN
GOTO BL0613
ELSEIF Vok0614$ = "Falsch" THEN
GOTO BL0614
ELSEIF Vok0615$ = "Falsch" THEN
GOTO BL0615
ELSEIF Vok0616$ = "Falsch" THEN
GOTO BL0616
ELSEIF Vok0617$ = "Falsch" THEN
GOTO BL0617
ELSEIF Vok0618$ = "Falsch" THEN
GOTO BL0618
ELSEIF Vok0619$ = "Falsch" THEN
GOTO BL0619
ELSEIF Vok0620$ = "Falsch" THEN
GOTO BL0620
ELSEIF Vok0621$ = "Falsch" THEN
GOTO BL0621
ELSEIF Vok0622$ = "Falsch" THEN
GOTO BL0622
ELSEIF Vok0623$ = "Falsch" THEN
GOTO BL0623
ELSEIF Vok0624$ = "Falsch" THEN
GOTO BL0624
ELSEIF Vok0625$ = "Falsch" THEN
GOTO BL0625
ELSEIF Vok0626$ = "Falsch" THEN
GOTO BL0626
ELSEIF Vok0627$ = "Falsch" THEN
GOTO BL0627
ELSE GOTO Letztende06
END IF

Letztende06:
END SUB

SUB BlauLektion7

BL0701:
Latein$ = "de (m. Abl.)"
Deutsch$ = "von...herab, Åber"
Deutsch2$ = "Åber, von...herab"
Vok0701$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0702

BL0702:
Latein$ = "circus"
Deutsch$ = "Zirkus, Pferderennbahn"
Deutsch2$ = "Pferderennbahn, Zirkus"
Genus$ = "M"
Genitiv$ = "CIRCI"
Vok0702$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0703

BL0703:
Latein$ = "meridies"
Deutsch$ = "Mittag"
Genus$ = "M"
Genitiv$ = "MERIDIIS"
Vok0703$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0704

BL0704:
Latein$ = "calor"
Deutsch$ = "WÑrme"
Genus$ = "M"
Genitiv$ = "CALORIS"
Vok0704$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0705

BL0705:
Latein$ = "fatigare"
Deutsch$ = "mÅde machen"
Vok0705$ = Methode2$(Latein$, Deutsch$)
GOTO BL0706

BL0706:
Latein$ = "e, ex (m. Abl.)"
Deutsch$ = "aus"
Vok0706$ = Methode2$(Latein$, Deutsch$)
GOTO BL0707

BL0707:
Latein$ = "in (m. Abl.)"
Deutsch$ = "in (wo?)"
Vok0707$ = Methode2$(Latein$, Deutsch$)
GOTO BL0708

BL0708:
Latein$ = "umbra"
Deutsch$ = "Schatten"
Genus$ = "F"
Genitiv$ = "UMBRAE"
Vok0708$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0709

BL0709:
Latein$ = "sub (m. Abl.)"
Deutsch$ = "unter (wo?)"
Vok0709$ = Methode2$(Latein$, Deutsch$)
GOTO BL0710

BL0710:
Latein$ = "iacere"
Deutsch$ = "liegen"
Vok0710$ = Methode2$(Latein$, Deutsch$)
GOTO BL0711

BL0711:
Latein$ = "Roma"
Deutsch$ = "aus Rom"
Vok0710$ = Methode2$(Latein$, Deutsch$)
GOTO BL0712

BL0712:
Latein$ = "ludus"
Deutsch$ = "Spiel"
Genus$ = "M"
Genitiv$ = "LUDI"
Vok0712$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0713

BL0713:
Latein$ = "non iam (Adv.)"
Deutsch$ = "nicht mehr"
Vok0713$ = Methode2$(Latein$, Deutsch$)
GOTO BL0714

BL0714:
Latein$ = "nonus, a, um"
Deutsch$ = "der neunte"
Vok0714$ = Methode2$(Latein$, Deutsch$)
GOTO BL0715

BL0715:
Latein$ = "mecum"
Deutsch$ = "mit mir"
Vok0715$ = Methode2$(Latein$, Deutsch$)
GOTO BL0716

BL0716:
Latein$ = "agricola"
Deutsch$ = "Bauer"
Genus$ = "M"
Genitiv$ = "AGRICOLAE"
Vok0716$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0717

BL0717:
Latein$ = "ubi?"
Deutsch$ = "wo?"
Vok0717$ = Methode2$(Latein$, Deutsch$)
GOTO BL0718

BL0718:
Latein$ = "sedere"
Deutsch$ = "sitzen"
Vok0718$ = Methode2$(Latein$, Deutsch$)
GOTO BL0719

BL0719:
Latein$ = "unde?"
Deutsch$ = "woher?"
Vok0719$ = Methode2$(Latein$, Deutsch$)
GOTO BL0720

BL0720:
Latein$ = "quando?"
Deutsch$ = "wann?"
Vok0720$ = Methode2$(Latein$, Deutsch$)
GOTO BL0721

BL0721:
Latein$ = "decimus, a, um"
Deutsch$ = "der zehnte"
Vok0721$ = Methode2$(Latein$, Deutsch$)
GOTO BL0722

BL0722:
Latein$ = "spectator"
Deutsch$ = "Zuschauer"
Genus$ = "M"
Genitiv$ = "SPECTATORIS"
Vok0722$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0723

BL0723:
Latein$ = "iam (Adv.)"
Deutsch$ = "schon, jetzt gleich"
Deutsch2$ = "jetzt gleich, schnon"
Vok0723$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0724

BL0724:
Latein$ = "arena"
Deutsch$ = "Sand, Kampfbahn"
Deutsch2$ = "Kampfbahn, Sand"
Genus$ = "F"
Genitiv$ = "ARENAE"
Vok0724$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0725

BL0725:
Latein$ = "timor"
Deutsch$ = "Furcht"
Genus$ = "M"
Genitiv$ = "TIMORIS"
Vok0725$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0726

BL0726:
Latein$ = "cadere"
Deutsch$ = "fallen"
Vok0726$ = Methode2$(Latein$, Deutsch$)
GOTO BL0727

BL0727:
Latein$ = "carrus"
Deutsch$ = "Wagen"
Genus$ = "M"
Genitiv$ = "CARRI"
Vok0727$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0728

BL0728:
Latein$ = "tamen"
Deutsch$ = "dennoch"
Vok0728$ = Methode2$(Latein$, Deutsch$)
GOTO Ende07

Ende07:
IF Vok0701$ = "Falsch" THEN
GOTO BL0701
ELSEIF Vok0702$ = "Falsch" THEN
GOTO BL0702
ELSEIF Vok0703$ = "Falsch" THEN
GOTO BL0703
ELSEIF Vok0704$ = "Falsch" THEN
GOTO BL0704
ELSEIF Vok0705$ = "Falsch" THEN
GOTO BL0705
ELSEIF Vok0706$ = "Falsch" THEN
GOTO BL0706
ELSEIF Vok0707$ = "Falsch" THEN
GOTO BL0707
ELSEIF Vok0708$ = "Falsch" THEN
GOTO BL0708
ELSEIF Vok0709$ = "Falsch" THEN
GOTO BL0709
ELSEIF Vok0710$ = "Falsch" THEN
GOTO BL0710
ELSEIF Vok0711$ = "Falsch" THEN
GOTO BL0711
ELSEIF Vok0712$ = "Falsch" THEN
GOTO BL0712
ELSEIF Vok0713$ = "Falsch" THEN
GOTO BL0713
ELSEIF Vok0714$ = "Falsch" THEN
GOTO BL0714
ELSEIF Vok0715$ = "Falsch" THEN
GOTO BL0715
ELSEIF Vok0716$ = "Falsch" THEN
GOTO BL0716
ELSEIF Vok0717$ = "Falsch" THEN
GOTO BL0717
ELSEIF Vok0718$ = "Falsch" THEN
GOTO BL0718
ELSEIF Vok0719$ = "Falsch" THEN
GOTO BL0719
ELSEIF Vok0720$ = "Falsch" THEN
GOTO BL0720
ELSEIF Vok0721$ = "Falsch" THEN
GOTO BL0721
ELSEIF Vok0722$ = "Falsch" THEN
GOTO BL0722
ELSEIF Vok0723$ = "Falsch" THEN
GOTO BL0723
ELSEIF Vok0724$ = "Falsch" THEN
GOTO BL0724
ELSEIF Vok0725$ = "Falsch" THEN
GOTO BL0725
ELSEIF Vok0726$ = "Falsch" THEN
GOTO BL0726
ELSEIF Vok0727$ = "Falsch" THEN
GOTO BL0727
ELSEIF Vok0728$ = "Falsch" THEN
GOTO BL0728
ELSE GOTO Letztende07
END IF

Letztende07:
END SUB

SUB BlauLektion8

BL0801:
Latein$ = "populus"
Deutsch$ = "Volk"
Genus$ = "M"
Genitiv$ = "POPULI"
Vok0801$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0802

BL0802:
Latein$ = "mane (Adv.)"
Deutsch$ = "frÅh, morgens"
Deutsch2$ = "morgens, frÅh"
Vok0802$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0803

BL0803:
Latein$ = "vicinus, a, um"
Deutsch$ = "benachbart, Nachbar"
Deutsch2$ = "Nachbar, benachbart"
Vok0803$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0804

BL0804:
Latein$ = "cur?"
Deutsch$ = "warum?"
Vok0804$ = Methode2$(Latein$, Deutsch$)
GOTO BL0805

BL0805:
Latein$ = "tam (Adv.)"
Deutsch$ = "so"
Vok0805$ = Methode2$(Latein$, Deutsch$)
GOTO BL0806

BL0806:
Latein$ = "via"
Deutsch$ = "Stra·e"
Genus$ = "F"
Genitiv$ = "VIAE"
Vok0806$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0807

BL0807:
Latein$ = "consul"
Deutsch$ = "Konsul"
Genus$ = "M"
Genitiv$ = "CONSULIS"
Vok0807$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0808

BL0808:
Latein$ = "creare"
Deutsch$ = "wÑhlen, erschaffen"
Deutsch2$ = "erschaffen, wÑhlen"
Vok0808$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0809

BL0809:
Latein$ = "interrogare"
Deutsch$ = "fragen"
Vok0809$ = Methode2$(Latein$, Deutsch$)
GOTO BL0810

BL0810:
Latein$ = "domi"
Deutsch$ = "zu Hause"
Vok0810$ = Methode2$(Latein$, Deutsch$)
GOTO BL0811

BL0811:
Latein$ = "publicus, a, um"
Deutsch$ = "staatlich, îffentlich"
Deutsch2$ = "îffentlich, staatlich"
Vok0811$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0812

BL0812:
Latein$ = "res publica"
Deutsch$ = "Staat, Republik"
Deutsch2$ = "Republick, Staat"
Genus$ = "F"
Genitiv$ = "REI PUBLICAE"
Vok0812$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0813

BL0813:
Latein$ = "-ne"
Deutsch$ = "Fragepartikel"
Vok0813$ = Methode2$(Latein$, Deutsch$)
GOTO BL0814

BL0814:
Latein$ = "migrare"
Deutsch$ = "wandern"
Vok0814$ = Methode2$(Latein$, Deutsch$)
GOTO BL0815

BL0815:
Latein$ = "bene (Adv.)"
Deutsch$ = "gut"
Vok0815$ = Methode2$(Latein$, Deutsch$)
GOTO BL0816

BL0816:
Latein$ = "debere"
Deutsch$ = "mÅssen, schulden, verdanken"
Deutsch2$ = "mÅssen, verdanken, schulden"
Vok0816$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0817

BL0817:
Latein$ = "vir"
Deutsch$ = "Mann"
Genus$ = "M"
Genitiv$ = "VIRIS"
Vok0817$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0818

BL0818:
Latein$ = "femina"
Deutsch$ = "Frau"
Genus$ = "F"
Genitiv$ = "FEMINAE"
Vok0818$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0819

BL0819:
Latein$ = "a, ab (m. Abl.)"
Deutsch$ = "von"
Vok0819$ = Methode2$(Latein$, Deutsch$)
GOTO BL0820

BL0820:
Latein$ = "prohibere"
Deutsch$ = "fernhalten, hindern"
Deutsch2$ = "hindern, fernhalten"
Vok0820$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0821

BL0821:
Latein$ = "constare"
Deutsch$ = "feststehen, bestehen aus"
Deutsch2$ = "bestehen aus, feststehen"
Vok0821$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0822

BL0822:
Latein$ = "quotannis (Adv.)"
Deutsch$ = "jedes Jahr"
Vok0822$ = Methode2$(Latein$, Deutsch$)
GOTO BL0823

BL0823:
Latein$ = "collega"
Deutsch$ = "Kollege, Amtsgenosse"
Deutsch2$ = "Amtsgenosse, Kollege"
Genus$ = "M"
Genitiv$ = "COLLEGAE"
Vok0823$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0824

BL0824:
Latein$ = "administrare"
Deutsch$ = "verwalten, besorgen"
Deutsch2$ = "besorgen, verwalten"
Vok0824$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0825

BL0825:
Latein$ = "senator"
Deutsch$ = "Senator"
Genus$ = "M"
Genitiv$ = "SENORIS"
Vok0825$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0826

BL0826:
Latein$ = "curia"
Deutsch$ = "Rathaus, Ratsversammlung"
Deutsch2$ = "Ratsversammlung, Rathaus"
Genus$ = "F"
Genitiv$ = "CURIAE"
Vok0826$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0827

BL0827:
Latein$ = "consulere (m. Akk.)"
Deutsch$ = "un Rat fragen, beraten, beschlie·en"
Deutsch2$ = "um Rat fragen, beschlie·en, beraten"
Vok0827$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0828

BL0828:
Latein$ = "num?"
Deutsch$ = "etwa?"
Vok0828$ = Methode2$(Latein$, Deutsch$)
GOTO BL0829

BL0829:
Latein$ = "ecce!"
Deutsch$ = "da!, sieh!, bitte!"
Deutsch2$ = "da!, bitte!, sieh!"
Vok0829$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO Ende08

Ende08:
IF Vok0801$ = "Falsch" THEN
GOTO BL0801
ELSEIF Vok0802$ = "Falsch" THEN
GOTO BL0802
ELSEIF Vok0803$ = "Falsch" THEN
GOTO BL0803
ELSEIF Vok0804$ = "Falsch" THEN
GOTO BL0804
ELSEIF Vok0805$ = "Falsch" THEN
GOTO BL0805
ELSEIF Vok0806$ = "Falsch" THEN
GOTO BL0806
ELSEIF Vok0807$ = "Falsch" THEN
GOTO BL0807
ELSEIF Vok0808$ = "Falsch" THEN
GOTO BL0808
ELSEIF Vok0809$ = "Falsch" THEN
GOTO BL0809
ELSEIF Vok0810$ = "Falsch" THEN
GOTO BL0810
ELSEIF Vok0811$ = "Falsch" THEN
GOTO BL0811
ELSEIF Vok0812$ = "Falsch" THEN
GOTO BL0812
ELSEIF Vok0813$ = "Falsch" THEN
GOTO BL0813
ELSEIF Vok0814$ = "Falsch" THEN
GOTO BL0814
ELSEIF Vok0815$ = "Falsch" THEN
GOTO BL0815
ELSEIF Vok0816$ = "Falsch" THEN
GOTO BL0816
ELSEIF Vok0817$ = "Falsch" THEN
GOTO BL0817
ELSEIF Vok0818$ = "Falsch" THEN
GOTO BL0818
ELSEIF Vok0819$ = "Falsch" THEN
GOTO BL0819
ELSEIF Vok0820$ = "Falsch" THEN
GOTO BL0820
ELSEIF Vok0821$ = "Falsch" THEN
GOTO BL0821
ELSEIF Vok0822$ = "Falsch" THEN
GOTO BL0822
ELSEIF Vok0823$ = "Falsch" THEN
GOTO BL0823
ELSEIF Vok0824$ = "Falsch" THEN
GOTO BL0824
ELSEIF Vok0825$ = "Falsch" THEN
GOTO BL0825
ELSEIF Vok0826$ = "Falsch" THEN
GOTO BL0826
ELSEIF Vok0827$ = "Falsch" THEN
GOTO BL0827
ELSEIF Vok0828$ = "Falsch" THEN
GOTO BL0828
ELSEIF Vok0829$ = "Falsch" THEN
GOTO BL0829
ELSE GOTO Letztende08
END IF

Letztende08:
END SUB

SUB BlauLektion9

BL0901:
Latein$ = "candidatus"
Deutsch$ = "Kandidat"
Genus$ = "M"
Genitiv$ = "CANTIDATI"
Vok0901$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0902

BL0902:
Latein$ = "iurgare"
Deutsch$ = "streiten"
Vok0902$ = Methode2$(Latein$, Deutsch$)
GOTO BL0903

BL0903:
Latein$ = "vel"
Deutsch$ = "oder, oder auch"
Deutsch2$ = "oder auch, oder"
Vok0903$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0904

BL0904:
Latein$ = "murus"
Deutsch$ = "Mauer"
Genus$ = "M"
Genitiv$ = "MURI"
Vok0904$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0905

BL0905:
Latein$ = "littera"
Deutsch$ = "Buchstabe"
Genus$ = "F"
Genitiv$ = "LITTERAE"
Vok0905$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0906

BL0906:
Latein$ = "legere"
Deutsch$ = "lesen, sammeln"
Deutsch2$ = "sammeln, lesen"
Vok0906$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0907

BL0907:
Latein$ = "proletarius"
Deutsch$ = "Proletarier"
Vok0907$ = Methode2$(Latein$, Deutsch$)
GOTO BL0908

BL0908:
Latein$ = "censere"
Deutsch$ = "meinen, beschlie·en"
Deutsch2$ = "beschlie·en, meinen"
Vok0908$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0909

BL0909:
Latein$ = "tantum (Adv.)"
Deutsch$ = "nur"
Vok0909$ = Methode2$(Latein$, Deutsch$)
GOTO BL0910

BL0910:
Latein$ = "adiuvare"
Deutsch$ = "unterstÅtzen, helfen"
Deutsch2$ = "helfen, unterstÅtzen"
Vok0910$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0911

BL0911:
Latein$ = "noster, nostra, nostrum"
Deutsch$ = "unser"
Vok0911$ = Methode2$(Latein$, Deutsch$)
GOTO BL0912

BL0912:
Latein$ = "contra (Adv.), (m. Akk.)"
Deutsch$ = "dagegen, gegen"
Deutsch2$ = "gegen, dagegen"
Vok0912$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0913

BL0913:
Latein$ = "neque"
Deutsch$ = "und nicht, aber nicht, auch nicht"
Deutsch2$ = "und nicht, auch nicht, aber nicht"
Vok0913$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0914

BL0914:
Latein$ = "vexare"
Deutsch$ = "quÑlen, bedrÑngen"
Deutsch2$ = "bedrÑngen, quÑlen"
Vok0914$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0915

BL0915:
Latein$ = "despoliare"
Deutsch$ = "berauben"
Vok0915$ = Methode2$(Latein$, Deutsch$)
GOTO BL0916

BL0916:
Latein$ = "decipere"
Deutsch$ = "tÑuschen, betrÅgen"
Deutsch2$ = "betrÅgen, tÑuschen"
Vok0916$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0917

BL0917:
Latein$ = "nuntiare"
Deutsch$ = "melden"
Vok0917$ = Methode2$(Latein$, Deutsch$)
GOTO BL0918

BL0918:
Latein$ = "ducere"
Deutsch$ = "fÅhren, meinen"
Deutsch2$ = "meinen, fÅhren"
Vok0918$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0919

BL0919:
Latein$ = "complere"
Deutsch$ = "anfÅllen, fÅllen"
Deutsch2$ = "fÅllen, anfÅllen"
Vok0919$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0920

BL0920:
Latein$ = "tuba"
Deutsch$ = "Trompete"
Genus$ = "F"
Genitiv$ = "TUBAE"
Vok0920$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL0921

BL0921:
Latein$ = "canere"
Deutsch$ = "singen, blasen"
Deutsch2$ = "blasen, singen"
Vok0921$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0922

BL0922:
Latein$ = "nominare"
Deutsch$ = "nennen"
Vok0922$ = Methode2$(Latein$, Deutsch$)
GOTO BL0923

BL0923:
Latein$ = "tabella"
Deutsch$ = "Tafel, Liste"
Deutsch2$ = "Liste, Tafel"
Genus$ = "F"
Genitiv$ = "TABELLAE"
Vok0923$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL0924

BL0924:
Latein$ = "scribere"
Deutsch$ = "schreiben"
Vok0924$ = Methode2$(Latein$, Deutsch$)
GOTO BL0925

BL0925:
Latein$ = "inscribere"
Deutsch$ = "(dar)aufschreiben, eintragen"
Deutsch2$ = "eintragen, (dar)aufschreiben"
Vok0925$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0926

BL0926:
Latein$ = "ceteri, ae, a"
Deutsch$ = "die Åbrigen, die anderen"
Deutsch2$ = "die anderen, die Åbrigen"
Vok0926$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0927

BL0927:
Latein$ = "numerare"
Deutsch$ = "zÑhlen, aufzÑhlen"
Deutsch2$ = "aufzÑhlen, zÑhlen"
Vok0927$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO Ende09

Ende09:
IF Vok0901$ = "Falsch" THEN
GOTO BL0901
ELSEIF Vok0902$ = "Falsch" THEN
GOTO BL0902
ELSEIF Vok0903$ = "Falsch" THEN
GOTO BL0903
ELSEIF Vok0904$ = "Falsch" THEN
GOTO BL0904
ELSEIF Vok0905$ = "Falsch" THEN
GOTO BL0905
ELSEIF Vok0906$ = "Falsch" THEN
GOTO BL0906
ELSEIF Vok0907$ = "Falsch" THEN
GOTO BL0907
ELSEIF Vok0908$ = "Falsch" THEN
GOTO BL0908
ELSEIF Vok0909$ = "Falsch" THEN
GOTO BL0909
ELSEIF Vok0910$ = "Falsch" THEN
GOTO BL0910
ELSEIF Vok0911$ = "Falsch" THEN
GOTO BL0911
ELSEIF Vok0912$ = "Falsch" THEN
GOTO BL0912
ELSEIF Vok0913$ = "Falsch" THEN
GOTO BL0913
ELSEIF Vok0914$ = "Falsch" THEN
GOTO BL0914
ELSEIF Vok0915$ = "Falsch" THEN
GOTO BL0915
ELSEIF Vok0916$ = "Falsch" THEN
GOTO BL0916
ELSEIF Vok0917$ = "Falsch" THEN
GOTO BL0917
ELSEIF Vok0918$ = "Falsch" THEN
GOTO BL0918
ELSEIF Vok0919$ = "Falsch" THEN
GOTO BL0919
ELSEIF Vok0920$ = "Falsch" THEN
GOTO BL0920
ELSEIF Vok0921$ = "Falsch" THEN
GOTO BL0921
ELSEIF Vok0922$ = "Falsch" THEN
GOTO BL0922
ELSEIF Vok0923$ = "Falsch" THEN
GOTO BL0923
ELSEIF Vok0924$ = "Falsch" THEN
GOTO BL0924
ELSEIF Vok0925$ = "Falsch" THEN
GOTO BL0925
ELSEIF Vok0926$ = "Falsch" THEN
GOTO BL0926
ELSEIF Vok0927$ = "Falsch" THEN
GOTO BL0927
ELSE GOTO Letztende09
END IF

Letztende09:
END SUB

FUNCTION Methode1$ (Latein$, Deutsch$, Genus$, Genitiv$)

M1:
  CLS
  LOCATE 10, 10
  PRINT Latein$
  LINE INPUT "Deutsch: "; DA$
  IF DA$ = Deutsch$ THEN
    GOTO WeiterM11
  ELSEIF UCASE$(DA$) = "HELP" THEN
    Help$ = "1": GOTO m1u
  ELSE
    GOTO m1u
  END IF

WeiterM11:
  LINE INPUT "Genus: "; GA$
  IF UCASE$(GA$) = Genus$ THEN
    GOTO WeiterM12
    ELSEIF UCASE$(GA$) = "HELP" THEN
    Help$ = "2": GOTO m1u
    ELSE
    GOTO m1u
  END IF

WeiterM12:
  LINE INPUT "Genitiv: "; WA$
  IF UCASE$(WA$) = Genitiv$ THEN
    GOTO EndeM11
    ELSEIF UCASE$(WA$) = "HELP" THEN
    Help$ = "3": GOTO m1u
    ELSE
    GOTO m1u
  END IF

m1u:
  CLS
  IF Help$ = "1" THEN
    LOCATE 1, 10
    PRINT Deutsch$
    GOTO M1Anfang
    ELSEIF Help$ = "2" THEN
    LOCATE 1, 10
    PRINT Deutsch$
    LOCATE 2, 10
    PRINT Genus$
    GOTO M1Anfang
    ELSEIF Help$ = "3" THEN
    LOCATE 1, 10
    PRINT Deutsch$
    LOCATE 2, 10
    PRINT Genus$
    LOCATE 3, 10
    PRINT Genitiv$
    GOTO M1Anfang
    ELSE
    GOTO M1Anfang
  END IF
M1Anfang:
  LOCATE 10, 10
  PRINT Latein$
  LINE INPUT "Deutsch: "; DA$
  IF DA$ = Deutsch$ THEN
    GOTO WeiterM13
    ELSEIF UCASE$(DA$) = "HELP" THEN
    Help$ = "1": GOTO m1u
    ELSE
    GOTO m1u
  END IF

WeiterM13:
  LINE INPUT "Genus: "; GA$
  IF UCASE$(GA$) = Genus$ THEN
    GOTO WeiterM14
    ELSEIF UCASE$(GA$) = "HELP" THEN
    Help$ = "2": GOTO m1u
    ELSE
    GOTO m1u
  END IF

WeiterM14:
  LINE INPUT "Genitiv: "; WA$
  IF UCASE$(WA$) = Genitiv$ THEN
    GOTO EndeM12
    ELSEIF UCASE$(WA$) = "HELP" THEN
    Help$ = "3": GOTO m1u
    ELSE GOTO m1u
  END IF

EndeM11:
Methode1$ = "Richtig"
GOTO SchlussM1

EndeM12:
Methode1$ = "Falsch"
GOTO SchlussM1

SchlussM1:
CLS
END FUNCTION

FUNCTION Methode2$ (Latein$, Deutsch$)

M2:
CLS
LOCATE 10, 10
PRINT Latein$
LINE INPUT "Deutsch: "; DA$
IF DA$ = Deutsch$ THEN
GOTO EndeM21
ELSEIF UCASE$(DA$) = "HELP" THEN
Help$ = "1": GOTO m2u
ELSE GOTO m2u
END IF

m2u:
CLS
IF Help$ = "1" THEN
LOCATE 1, 10
PRINT Deutsch$: GOTO M2Anfang
ELSE
GOTO M2Anfang
END IF

M2Anfang:
LOCATE 10, 10
PRINT Latein$
LINE INPUT "Deutsch: "; DA$
IF DA$ = Deutsch$ THEN
GOTO EndeM22
ELSEIF UCASE$(DA$) = "HELP" THEN
Help$ = "1": GOTO m2u
ELSE GOTO m2u
END IF

EndeM21:
Methode2$ = "Richtig"
GOTO SchlussM2

EndeM22:
Methode2$ = "Falsch"
GOTO SchlussM2

SchlussM2:
END FUNCTION

FUNCTION Methode3$ (Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)

M3:
CLS
LOCATE 10, 10
PRINT Latein$
LINE INPUT "Deutsch: "; DA$
IF DA$ = Deutsch$ THEN
GOTO WeiterM31
ELSEIF DA$ = Deutsch2$ THEN
GOTO WeiterM31
ELSEIF UCASE$(DA$) = "HELP" THEN
Help$ = "1"
ELSE GOTO m3u
END IF

WeiterM31:
LINE INPUT "Genus: "; GA$
IF UCASE$(GA$) = Genus$ THEN
GOTO WeiterM32
ELSEIF UCASE$(GA$) = "HELP" THEN
Help$ = "2": GOTO m3u
ELSE GOTO m3u
END IF

WeiterM32:
LINE INPUT "Genitiv: "; WA$
IF UCASE$(WA$) = Genitiv$ THEN
GOTO EndeM31
ELSEIF UCASE$(WA$) = "HELP" THEN
Help$ = "3": GOTO m3u
ELSE GOTO m3u
END IF

m3u:
CLS
  IF Help$ = "1" THEN
    LOCATE 1, 10
    PRINT Deutsch$
    GOTO M3Anfang
    ELSEIF Help$ = "2" THEN
    LOCATE 1, 10
    PRINT Deutsch$
    LOCATE 2, 10
    PRINT Genus$
    GOTO M3Anfang
    ELSEIF Help$ = "3" THEN
    LOCATE 1, 10
    PRINT Deutsch$
    LOCATE 2, 10
    PRINT Genus$
    LOCATE 3, 10
    PRINT Genitiv$
    GOTO M3Anfang
    ELSE
    GOTO M3Anfang
  END IF

M3Anfang:
LOCATE 10, 10
PRINT Latein$
LINE INPUT "Deutsch: "; DA$
IF DA$ = Deutsch$ THEN
GOTO WeiterM33
ELSEIF DA$ = Deutsch2$ THEN
GOTO WeiterM33
ELSEIF UCASE$(DA$) = "HELP" THEN
Help$ = "1": GOTO m3u
ELSE GOTO m3u
END IF

WeiterM33:
LINE INPUT "Genus: "; GA$
IF UCASE$(GA$) = Genus$ THEN
GOTO WeiterM34
ELSEIF UCASE$(GA$) = "HELP" THEN
Help$ = "2": GOTO m3u
ELSE GOTO m3u
END IF

WeiterM34:
LINE INPUT "Genitiv: "; WA$
IF UCASE$(WA$) = Genitiv$ THEN
GOTO EndeM32
ELSEIF UCASE$(WA$) = "HELP" THEN
Help$ = "3": GOTO m3u
ELSE GOTO m3u
END IF

EndeM31:
Methode3$ = "Richtig"
GOTO SchlussM3

EndeM32:
Methode3$ = "Falsch"
GOTO SchlussM3

SchlussM3:
END FUNCTION

FUNCTION Methode4$ (Latein$, Deutsch$, Deutsch2$)

M4:
CLS
LOCATE 10, 10
PRINT Latein$
LINE INPUT "Deutsch: "; DA$
IF DA$ = Deutsch$ THEN
GOTO EndeM41
ELSEIF DA$ = Deutsch2$ THEN
GOTO EndeM41
ELSEIF UCASE$(DA$) = "HELP" THEN
Help$ = "1": GOTO m4u
ELSE GOTO m4u
END IF

m4u:
CLS
IF Help$ = "1" THEN
LOCATE 1, 10
PRINT Deutsch$
ELSE GOTO M4Anfang
END IF

M4Anfang:
LOCATE 10, 10
PRINT Latein$
LINE INPUT "Deutsch: "; DA$
IF DA$ = Deutsch$ THEN
GOTO EndeM42
ELSEIF DA$ = Deutsch2$ THEN
GOTO EndeM42
ELSEIF UCASE$(DA$) = "HELP" THEN
Help$ = "1": GOTO m4u
ELSE GOTO m4u
END IF

EndeM41:
Methode4$ = "Richtig"
GOTO SchlussM4

EndeM42:
Methode4$ = "Falsch"
GOTO SchlussM4

SchlussM4:
END FUNCTION

