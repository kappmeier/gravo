DECLARE SUB Lulli ()
DECLARE FUNCTION Methode4$ (Latein$, Deutsch$, Deutsch2$)
DECLARE FUNCTION Methode1$ (Latein$, Deutsch$, Genus$, Genitiv$)
DECLARE FUNCTION Methode2$ (Latein$, Deutsch$)
DECLARE FUNCTION Methode3$ (Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
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


Blau:
CLS
LOCATE 2, 6
COLOR 12, 0
PRINT "A u s w a h l"
LOCATE 4, 4
COLOR 7, 0
PRINT "Lektion WÑhlen:"
LOCATE 5, 4
PRINT "(19-35)"
INPUT "Wat' is Junge, wÑhl ma'"; Brsch
IF Brsch = -100 THEN
GOTO HauptEnde
ELSEIF Brsch < 19 THEN
GOTO Blau
ELSEIF Brsch > 35 THEN
GOTO Blau
END IF
SELECT CASE Brsch
CASE 19
CALL BlauLektion19
CASE 20
CALL BlauLektion20
CASE 21
CALL BlauLektion21
CASE 22
CALL BlauLektion22
CASE 23
CALL BlauLektion23
CASE 24
CALL BlauLektion24
CASE 25
CALL BlauLektion25
CASE 26
CALL BlauLektion26
CASE 27
CALL BlauLektion27
CASE 28
CALL BlauLektion28
CASE 29
CALL BlauLektion29
CASE 30
CALL BlauLektion30
CASE 31
CALL BlauLektion31
CASE 32
CALL BlauLektion32
CASE 33
CALL BlauLektion33
CASE 34
CALL BlauLektion34
CASE 35
CALL BlauLektion35
END SELECT

HauptEnde:
CLS
LOCATE 2, 6
COLOR 12, 0
PRINT "A u s w a h l"
LOCATE 4, 4
COLOR 7, 0
PRINT "Willst du beenden?"
LOCATE 5, 4
LINE INPUT ""; Weiter$
IF UCASE$(Weiter$) = "NEIN" THEN
GOTO Blau
ELSEIF UCASE$(Weiter$) = "JA" THEN
GOTO GanzAmEnde
ELSE GOTO HauptEnde
END IF

GanzAmEnde:
CLS
END

SUB BlauLektion19

BL1901:
Latein$ = "rex"
Deutsch$ = "Kînig"
Genus$ = "M"
Genitiv$ = "REGIS"
Vok1901$ = Methode1$(Latein$, Deutsch$, Genus$, Genitiv$)
GOTO BL1902

BL1902:
Latein$ = "ultimus, a, um"
Deutsch$ = "der letzte"
Vok1902$ = Methode2$(Latein$, Deutsch$)
GOTO BL1903

BL1904:
Latein$ = "laus"
Deutsch$ = "Lob"
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

SUB BlauLektion20

BL2001:
Latein$ = "oppidum"
Deutsch$ = "Stadt"
Genus$ = "N"
Genitiv$ = "OPPIDI"
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
Genitiv$ = "ORACULI"
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
Genitiv$ = "IUDICII"
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
Genitiv$ = "MALI"
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
Genitiv$ = "AMORIS"
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
Latein$ = "clam (Adv.)"
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
Deutsch$ = "Jahrhundert, Zeitalter"
Deutsch2$ = "Zeitalter, Jahrhundert"
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
Deutsch$ = "Gesandter, Legat"
Deutsch2$ = "Legat, Gesandter"
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
Deutsch$ = "Teil, Seite"
Deutsch2$ = "Seite, Teil"
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
Deutsch$ = "öberflu· haben, Åberflie·en"
Deutsch2$ = "Åberflie·en, öberflu· haben"
Vok2713$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2714

BL2714:
Latein$ = "aequus, a, um"
Deutsch$ = "gleich, gerecht, gÅnstig"
Deutsch2$ = "gleicht, gÅnstig, gerecht"
Vok2714$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2715

BL2715:
Latein$ = "iniquus, a, um"
Deutsch$ = "ungleich, ungerecht, ungÅnstig"
Deutsch2$ = "ungleich, ungÅnstig, ungerecht"
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
Deutsch$ = "(innerlich) bewegen, veranlassen"
Deutsch2$ = "veranlassen, (innerlich) bewegen"
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
Deutsch$ = "vorstehen, anfÅhren"
Deutsch2$ = "anfÅhren, vorstehen"
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
Deutsch$ = "bemerken, einsehen, verstehen"
Deutsch2$ = "bemerken, verstehen, einsehen"
Vok2727$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2728

BL2728:
Latein$ = "curare"
Deutsch$ = "sorgen, besorgen"
Deutsch2$ = "besorgen, sorgen"
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
Deutsch$ = "jedenfalls, wenigstens"
Deutsch2$ = "wenigstens, jedenfalls"
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
Deutsch$ = "Seuche, Krankheit"
Deutsch2$ = "Krankheit, Seuche"
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
Deutsch$ = "verzweifeln, die Hoffnung aufgeben"
Deutsch2$ = "die Hoffnung aufgeben, verzweifeln"
Vok2904$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2905

BL2905:
Latein$ = "sui Pl."
Deutsch$ = "die Seinigen, seine Leute"
Deutsch2$ = "seine Leute, die Seinigen"
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
Deutsch$ = "gewi·, sicher"
Deutsch2$ = "sicher, gewi·"
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
Deutsch$ = "zurÅcknehmen, aufnehmen"
Deutsch2$ = "aufnehmen, zurÅcknehmen"
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
Deutsch$ = "tîten, umbringen"
Deutsch2$ = "umbringen, tîten"
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
Deutsch$ = "keiner, kein"
Deutsch2$ = "kein, keiner"
Vok2922$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL2923

BL2923:
Latein$ = "reliquus, a, um"
Deutsch$ = "Åbrig"
Vok2923$ = Methode2$(Latein$, Deutsch$)
GOTO BL2924

BL2924:
Latein$ = "fortuna"
Deutsch$ = "Schicksal, GlÅck"
Deutsch2$ = "GlÅck, Schicksal"
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
Deutsch$ = "Åbergeben, Åberliefern"
Deutsch2$ = "Åberliefern, Åbergeben"
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

SUB BlauLektion30

BL3001:
Latein$ = "nobilis"
Deutsch$ = "adlig, berÅhmt"
Deutsch2$ = "berÅhmt, adlig"
Vok3001$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3002

BL3002:
Latein$ = "acer"
Deutsch$ = "scharf, heftig"
Deutsch2$ = "heftig, scharf"
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
Deutsch$ = "zusammenstellen, verfassen"
Deutsch2$ = "verfassen, zusammenstellen"
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
Deutsch$ = "Felsen, Stein"
Deutsch2$ = "Stein, Felsen"
Genus$ = "N"
Genitiv$ = "SAXI"
Vok3012$ = Methode3$(Latein$, Deutsch$, Deutsch2$, Genus$, Genitiv$)
GOTO BL3013

BL3013:
Latein$ = "gravis"
Deutsch$ = "schwer, ernst, wichtig"
Deutsch2$ = "schwer, wichtig, ernst"
'Deutsch3$ = "ernst, schwer, wichtig"
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
Deutsch$ = "jeder, ganz"
Deutsch2$ = "ganz, jeder"
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
Deutsch$ = "Verstand, Gesinnung, Geist"
Deutsch2$ = "Verstand, Geist, Gesinnung"
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
Deutsch$ = "hinabwerfen, verschlagen"
Deutsch2$ = "verschlagen, hinabwerfen"
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
Deutsch$ = "standhaft, bestÑndig"
Deutsch2$ = "bestÑndig, standhaft"
'Genitiv$ = "CONSTANTIS"
Vok3031$ = Methode4$(Latein$, Deutsch$, Deutsch2$)
GOTO BL3032

BL3032:
Latein$ = "constituere"
Deutsch$ = "festsetzen, beschlie·en"
Deutsch2$ = "beschlie·en, festsetzen"
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
Latein$ = "potens (Gen. potentis)"
Deutsch$ = "mÑchtig"
Genus$ = "POTENTIS"
Genitiv$ = "POTENTIS"
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
Latein$ = "interest inter (m. Akk.)"
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
Genitiv$ = "IUSTIAE"
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
Deutsch2$ = "ehrenvoll, angesehen, anstÑndig"
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

SUB Lulli
Hier:
CLS
LOCATE 2, 6
COLOR 12, 0
PRINT "A u s w a h l"
LOCATE 4, 4
COLOR 7, 0
PRINT "Willst du beenden?"
LOCATE 5, 4
LINE INPUT ""; Weiter$
IF UCASE$(Weiter$) = "JA" THEN
END
ELSEIF UCASE$(Weiter$) = "NEIN" THEN
GOTO Irgendwo
ELSE GOTO Hier
END IF

Irgendwo:
CLS
LOCATE 2, 6
COLOR 12, 0
PRINT "A u s w a h l"
LOCATE 4, 4
COLOR 7, 0
PRINT "Lektion WÑhlen:"
LOCATE 5, 4
PRINT "(19-35)"
INPUT "Wat' is Junge, wÑhl ma'"; Arsch
IF Arsch = -100 THEN
GOTO Hier
ELSEIF Arsch < 19 THEN
GOTO Irgendwo
ELSEIF Arsch > 35 THEN
GOTO Irgendwo
ELSE GOTO Juhu
END IF

Juhu:
SELECT CASE Arsch
CASE 19
CALL BlauLektion19
CASE 20
CALL BlauLektion20
CASE 21
CALL BlauLektion21
CASE 22
CALL BlauLektion22
CASE 23
CALL BlauLektion23
CASE 24
CALL BlauLektion24
CASE 25
CALL BlauLektion25
CASE 26
CALL BlauLektion26
CASE 27
CALL BlauLektion27
CASE 28
CALL BlauLektion28
CASE 29
CALL BlauLektion29
CASE 30
CALL BlauLektion30
CASE 31
CALL BlauLektion31
CASE 32
CALL BlauLektion32
CASE 33
CALL BlauLektion33
CASE 34
CALL BlauLektion34
CASE 35
CALL BlauLektion35
END SELECT
END SUB

FUNCTION Methode1$ (Latein$, Deutsch$, Genus$, Genitiv$)

M1:
  CLS
  LOCATE 10, 10
  PRINT Latein$
  LINE INPUT "Deutsch: "; DA$
  IF DA$ = Deutsch$ THEN
    GOTO WeiterM11
  ELSEIF UCASE$(DA$) = "ENDE" THEN
    CALL Lulli
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
  ELSEIF UCASE$(DA$) = "ENDE" THEN
   CALL Lulli
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
ELSEIF UCASE$(DA$) = "ENDE" THEN
CALL Lulli
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
ELSEIF UCASE$(DA$) = "ENDE" THEN
CALL Lulli
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
ELSEIF UCASE$(DA$) = "ENDE" THEN
CALL Lulli
ELSEIF DA$ = Deutsch2$ THEN
GOTO WeiterM31
ELSEIF UCASE$(DA$) = "HELP" THEN
Help$ = "1": GOTO m3u
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
ELSEIF UCASE$(DA$) = "ENDE" THEN
CALL Lulli
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
ELSEIF UCASE$(DA$) = "ENDE" THEN
CALL Lulli
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
ELSEIF UCASE$(DA$) = "ENDE" THEN
CALL Lulli
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

