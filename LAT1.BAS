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
'Datei zum lernen lateinischer Vokabeln


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

BL0111
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
Vok0119$ = Methode2$(Latein$, Deutsch$, Genus$, Genitiv$)
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
Vok0120$ = Methode2$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
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
GOTO BL0523
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
Vok1123$ = Methode3$(Latein$, Deutsch$, Genus$, Genitiv$)
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
Vok1216$ = Methode4$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
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

BL1301:
Latein$ = "controversia"
Deutsch$ = "Streit"
Genus$ = "F"
Genitiv$ = "CONTROVERSIAE"
Vok1301$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1302

BL1302:
Latein$ = "liber"
Deutsch$ = "Buch"
Genus$ = "M"
Genitiv$ = "LIBRI"
Vok1302$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1303

BL1303:
Latein$ = "quaerere"
Deutsch$ = "suchen, finden"
Deutsch2$ = "finden, suchen"
Vok1303$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1304

BL1304:
Latein$ = "pulcher, pulchra, pulchrum"
Deutsch$ = "schîn"
Vok1304$ = Methode2$(Latein$, Deutsch$)
GOTO BL1305

BL1305:
Latein$ = "ignorare"
Deutsch$ = "nicht kennen, nicht wissen"
Deutsch2$ = "nicht wissen, nicht kennen"
Vok1305$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1306

BL1306:
Latein$ = "non ignorare"
Deutsch$ = "genau kennen"
Vok1306$ = Methode2$(Latein$, Deutsch$)
GOTO BL1307

BL1307:
Latein$ = "numquam (Adv.)"
Deutsch$ = "niemals"
Vok1307$ = Methode2$(Latein$, Deutsch$)
GOTO BL1308

BL1308:
Latein$ = "stultus, a, um"
Deutsch$ = "dumm"
Vok1308$ = Methode2$(Latein$, Deutsch$)
GOTO BL1309

BL1309:
Latein$ = "miser, misera, miserum"
Deutsch$ = "arm, unglÅcklich"
Deutsch2$ = "unglÅcklich, arm"
Vok1309$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1310

BL1310:
Latein$ = "domum"
Deutsch$ = "nach Hause"
Vok1310$ = Methode2$(Latein$, Deutsch$)
GOTO BL1311

BL1311:
Latein$ = "triumphus"
Deutsch$ = "Triumph"
Genus$ = "M"
Genitiv$ = "TRIUMPHI"
Vok1311$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1312

BL1312:
Latein$ = "minime (Adv.)"
Deutsch$ = "keineswegs, am wenigsten"
Deutsch2$ = "am wenigsten, keineswegs"
Vok1312$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1313

BL1313:
Latein$ = "petere"
Deutsch$ = "streben nach, logehen auf, erbitten"
Deutsch2$ = "streben nach, erbitten, losgehen auf"
Vok1313$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1314

BL1314:
Latein$ = "stare"
Deutsch$ = "stehen"
Vok1314$ = Methode2$(Latein$, Deutsch$)
GOTO BL1315

BL1315:
Latein$ = "interea (Adv.)"
Deutsch$ = "inzwischen"
Vok1315$ = Methode2$(Latein$, Deutsch$)
GOTO BL1316

BL1316:
Latein$ = "cogere"
Deutsch$ = "zwingen, versammeln"
Deutsch2$ = "versammeln, zwingen"
Vok1316$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1317

BL1317:
Latein$ = "praeda"
Deutsch$ = "Beute"
Genus$ = "F"
Genitiv$ = "PRAEDAE"
Vok1317$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1318

BL1318:
Latein$ = "superare"
Deutsch$ = "Åberwinden, besiegen"
Deutsch2$ = "besiegen, Åberwinden"
Vok1318$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1319

BL1319:
Latein$ = "aureus, a, um"
Deutsch$ = "golden"
Vok1319$ = Methode2$(Latein$, Deutsch$)
GOTO BL1320

BL1320:
Latein$ = "onerare"
Deutsch$ = "beladen, belasten"
Deutsch2$ = "belasten, beladen"
Vok1320$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1321

BL1321:
Latein$ = "quattuor"
Deutsch$ = "vier"
Vok1321$ = Methode2$(Latein$, Deutsch$)
GOTO BL1322

BL1322:
Latein$ = "toga"
Deutsch$ = "Toga"
Genus$ = "F"
Genitiv$ = "TOGAE"
Vok1322$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1323

BL1323:
Latein$ = "sacer, sacra, sacrum"
Deutsch$ = "heilig, verflucht"
Deutsch2$ = "verflucht, heilig"
Vok1323$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1324

BL1324:
Latein$ = "pro (m. Abl.)"
Deutsch$ = "vor, fÅr, anstatt"
Vok1324$ = Methode2$(Latein$, Deutsch$)
GOTO BL1325

BL1325:
Latein$ = "taberna"
Deutsch$ = "Gasthaus, Schenke"
Deutsch2$ = "Schenke, Gasthaus"
Genus$ = "F"
Genitiv$ = "TABERNA"
Vok1325$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO Ende13

Ende13:
IF Vok1301$ = "Falsch" THEN
GOTO BL1301
ELSEIF Vok1302$ = "Falsch" THEN
GOTO BL1302
ELSEIF Vok1303$ = "Falsch" THEN
GOTO BL1303
ELSEIF Vok1304$ = "Falsch" THEN
GOTO BL1304
ELSEIF Vok1305$ = "Falsch" THEN
GOTO BL1305
ELSEIF Vok1306$ = "Falsch" THEN
GOTO BL1306
ELSEIF Vok1307$ = "Falsch" THEN
GOTO BL1307
ELSEIF Vok1308$ = "Falsch" THEN
GOTO BL1308
ELSEIF Vok1309$ = "Falsch" THEN
GOTO BL1309
ELSEIF Vok1310$ = "Falsch" THEN
GOTO BL1310
ELSEIF Vok1311$ = "Falsch" THEN
GOTO BL1311
ELSEIF Vok1312$ = "Falsch" THEN
GOTO BL1312
ELSEIF Vok1313$ = "Falsch" THEN
GOTO BL1313
ELSEIF Vok1314$ = "Falsch" THEN
GOTO BL1314
ELSEIF Vok1315$ = "Falsch" THEN
GOTO BL1315
ELSEIF Vok1316$ = "Falsch" THEN
GOTO BL1316
ELSEIF Vok1317$ = "Falsch" THEN
GOTO BL1317
ELSEIF Vok1318$ = "Falsch" THEN
GOTO BL1318
ELSEIF Vok1319$ = "Falsch" THEN
GOTO BL1319
ELSEIF Vok1320$ = "Falsch" THEN
GOTO BL1320
ELSEIF Vok1321$ = "Falsch" THEN
GOTO BL1321
ELSEIF Vok1322$ = "Falsch" THEN
GOTO BL1322
ELSEIF Vok1323$ = "Falsch" THEN
GOTO BL1323
ELSEIF Vok1324$ = "Falsch" THEN
GOTO BL1324
ELSEIF Vok1325$ = "Falsch" THEN
GOTO BL1325
ELSE GOTO Letztende13
END IF

Letztende13:
END SUB

SUB BlauLektion14

BL1401:
Latein$ = "accurere"
Deutsch$ = "herbeilaufen"
Vok1401$ = Methode2$(Latein$, Deutsch$)
GOTO BL1402

BL1402:
Latein$ = "existimare"
Deutsch$ = "glauben, meinen"
Deutsch2$ = "meinen, glauben"
Vok1402$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1403

BL1403:
Latein$ = "heri (Adv.)"
Deutsch$ = "gestern"
Vok1403$ = Methode2$(Latein$, Deutsch$)
GOTO BL1404

BL1404:
Latein$ = "monere"
Deutsch$ = "ermahnen, warnen"
Deutsch2$ = "warnen, ermahnen"
Vok1404$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1405

BL1405:
Latein$ = "rapere"
Deutsch$ = "rauben"
Vok1405$ = Methode2$(Latein$, Deutsch$)
GOTO BL1406

BL1406:
Latein$ = "incola"
Deutsch$ = "Einwohner"
Genus$ = "M"
Genitiv$ = "INCOLAE"
Vok1406$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1407

BL1407:
Latein$ = "aedificare"
Deutsch$ = "bauen"
Vok1407$ = Methode2$(Latein$, Deutsch$)
GOTO BL1407

BL1408:
Latein$ = "mulier"
Deutsch$ = "Frau"
Genus$ = "F"
Genitiv$ = "MULIERIS"
Vok1408$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1409

BL1409:
Latein$ = "deesse"
Deutsch$ = "fehlen"
Vok1409$ = Methode2$(Latein$, Deutsch$)
GOTO BL1410

BL1410:
Latein$ = "sine (m. Abl.)"
Deutsch$ = "ohne"
Vok1410$ = Methode2$(Latein$, Deutsch$)
GOTO BL1411

BL1411:
Latein$ = "salvus, a, um"
Deutsch$ = "gesund, wohlbehalten"
Deutsch2$ = "wohlbehalten, gesund"
Vok1411$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1412

BL1412:
Latein$ = "deliberare"
Deutsch$ = "Åberlegen"
Vok1412$ = Methode2$(Latein$, Deutsch$)
GOTO BL1413

BL1413:
Latein$ = "antea (Adv.)"
Deutsch$ = "vorher"
Vok1413$ = Methode2$(Latein$, Deutsch$)
GOTO BL1414

BL1414:
Latein$ = "et (am anfang eines Hauptsatzes)"
Deutsch$ = "und tatsÑchlich"
Vok1414$ = Methode2$(Latein$, Deutsch$)
GOTO BL1415

BL1415:
Latein$ = "frustra (Adv.)"
Deutsch$ = "vergeblich"
Vok1415$ = Methode2$(Latein$, Deutsch$)
GOTO BL1416

BL1416:
Latein$ = "postulare"
Deutsch$ = "fordern"
Vok1416$ = Methode2$(Latein$, Deutsch$)
GOTO BL1417

BL1417:
Latein$ = "vae!"
Deutsch$ = "wehe"
Vok1417$ = Methode2$(Latein$, Deutsch$)
GOTO BL1418

BL1418:
Latein$ = "inter (m. Akk.)"
Deutsch$ = "zwischen, unter"
Deutsch2$ = "unter, zwischen"
Vok1418$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1419

BL1419:
Latein$ = "socius"
Deutsch$ = "Bundesgenosse"
Genus$ = "M"
Genitiv$ = "SOCII"
Vok1419$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1420

BL1420:
Latein$ = "convocare"
Deutsch$ = "zusammenrufen"
Vok1420$ = Methode2$(Latein$, Deutsch$)
GOTO BL1421

BL1421:
Latein$ = "liberare"
Deutsch$ = "befreien"
Vok1421$ = Methode2$(Latein$, Deutsch$)
GOTO BL1422

BL1422:
Latein$ = "paulo (Adv.)"
Deutsch$ = "ein wenig, etwas"
Deutsch2$ = "etwas, ein wenig"
Vok1422$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1423

BL1423:
Latein$ = "rursus (Adv.)"
Deutsch$ = "wieder, rÅckwÑrts"
Deutsch2$ = "rÅckwÑrts, wieder"
Vok1423$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL1424

BL1424:
Latein$ = "vulnerare"
Deutsch$ = "verwunden"
Vok1424$ = Methode2$(Latein$, Deutsch$)
GOTO BL1425

BL1425:
Latein$ = "maritus"
Deutsch$ = "Ehemann"
Genus$ = "M"
Genitiv$ = "MARITI"
Vok1425$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO Ende14

Ende14:
IF Vok1401$ = "Falsch" THEN
GOTO BL1401
ELSEIF Vok1402$ = "Falsch" THEN
GOTO BL1402
ELSEIF Vok1403$ = "Falsch" THEN
GOTO BL1403
ELSEIF Vok1404$ = "Falsch" THEN
GOTO BL1404
ELSEIF Vok1405$ = "Falsch" THEN
GOTO BL1405
ELSEIF Vok1406$ = "Falsch" THEN
GOTO BL1406
ELSEIF Vok1407$ = "Falsch" THEN
GOTO BL1407
ELSEIF Vok1408$ = "Falsch" THEN
GOTO BL1408
ELSEIF Vok1409$ = "Falsch" THEN
GOTO BL1409
ELSEIF Vok1410$ = "Falsch" THEN
GOTO BL1410
ELSEIF Vok1411$ = "Falsch" THEN
GOTO BL1411
ELSEIF Vok1412$ = "Falsch" THEN
GOTO BL1412
ELSEIF Vok1413$ = "Falsch" THEN
GOTO BL1413
ELSEIF Vok1414$ = "Falsch" THEN
GOTO BL1414
ELSEIF Vok1415$ = "Falsch" THEN
GOTO BL1415
ELSEIF Vok1416$ = "Falsch" THEN
GOTO BL1416
ELSEIF Vok1417$ = "Falsch" THEN
GOTO BL1417
ELSEIF Vok1418$ = "Falsch" THEN
GOTO BL1418
ELSEIF Vok1419$ = "Falsch" THEN
GOTO BL1419
ELSEIF Vok1420$ = "Falsch" THEN
GOTO BL1420
ELSEIF Vok1421$ = "Falsch" THEN
GOTO BL1421
ELSEIF Vok1422$ = "Falsch" THEN
GOTO BL1422
ELSEIF Vok1423$ = "Falsch" THEN
GOTO BL1423
ELSEIF Vok1424$ = "Falsch" THEN
GOTO BL1424
ELSEIF Vok1425$ = "Falsch" THEN
GOTO BL1425
ELSE GOTO Letztende14
END IF

Letztende14:
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
Latein$ = salutare
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
GOTO BL0231

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
ELSEIF Vok0231$ = "Falsch" THEN
GOTO BL0231
ELSEIF Vok0232$ = "Falsch" THEN
GOTO BL0232
ELSEIF Vok0233$ = "Falsch" THEN
GOTO BL0233
ELSE GOTO Letztende02
END IF

Letztende02:
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
Vok0314$ = Methode2$(Latein$, Deutsch$, Deutsch2$)
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
Vok0321$ = Methode2$(Latein$, Deutsch$, Deutsch2$)
GOTO BL0322

BL0322:
Latein$ = "totus, a, um"
Deutsch$ = "ganz"
Vok0322$ = Methode2$(Latein$, Deutsch$, Genus$, Genitiv$)
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
GOTO BL0330

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
GOTO BL0330
ELSEIF Vok0331$ = "Falsch" THEN
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
Vok0409$ = Methode2$(Latein$, Deutsch$, Genus$, Genitiv$)
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
Vok0428$ = Methode4$(Latein$, Deutsch$)
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
Vok0513$ = Methode3$(Latein$, Deutsch$, Deutsch2$)
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
GOTO B10712

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
Vok0713$ = Methode2$(Latein$, Deutsch$, Genus$, Genitiv$)
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
Vok0811$ = Methode4$(Latein$, Deutsch$)
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

BL0916
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
Vok0922$ = Methode3$(Latein$, Deutsch$)
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
Vok0925$ = Methode3$(Latein$, Deutsch$, Deutsch2$)
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

