RANDOMIZE TIMER
CLS
GOTO weiter

totalanfang:
b$ = INPUT$(1)
IF UCASE$(b$) = "E" GOTO ende ELSE GOTO weiter

weiter:
vok = RND(1)
A = INT(10 * vok) + 1

IF A = 1 THEN
GOTO 1
ELSEIF A = 2 THEN
GOTO 2
ELSEIF A = 3 THEN
GOTO 3
ELSEIF A = 4 THEN
GOTO 4
ELSEIF A = 5 THEN
GOTO 5
ELSEIF A = 6 THEN
GOTO 6
ELSEIF A = 7 THEN
GOTO 7
ELSEIF A = 8 THEN
GOTO 8
ELSEIF A = 9 THEN
GOTO 9
ELSEIF A = 10 THEN
GOTO 10
END IF

1 :
PRINT "filius"
PRINT
LINE INPUT "Deutsch: "; uebersetzUng$
IF UCASE$(uebersetzUng$) = "SOHN" THEN PRINT "R I C H T I G" ELSE PRINT "F A L S C H": GOTO 1
GOTO totalanfang

2 :
PRINT "filia                    "
PRINT
LINE INPUT "Deutsch: "; uebersetzUng$
IF UCASE$(uebersetzUng$) = "TOCHTER" THEN PRINT "R I C H T I G" ELSE PRINT "F A L S C H": GOTO 2
GOTO totalanfang

3 :
PRINT "ancilla              "
PRINT
LINE INPUT "Deutsch: "; uebersetzUng$
IF UCASE$(uebersetzUng$) = "SKLAVIN" THEN PRINT "R I C H T I G" ELSE PRINT "F A L S C H": GOTO 3
GOTO totalanfang
 
4 :
PRINT "dominus              "
PRINT
LINE INPUT "Deutsch: "; uebersetzUng$
IF UCASE$(uebersetzUng$) = "HERR" THEN PRINT "R I C H T I G" ELSE PRINT "F A L S C H": GOTO 4
GOTO totalanfang

5 :
PRINT "domina               "
PRINT
LINE INPUT "Deutsch: "; uebersetzUng$
IF UCASE$(uebersetzUng$) = "HERRIN" THEN PRINT "R I C H T I G" ELSE PRINT "F A L S C H": GOTO 5
GOTO totalanfang

6 :
PRINT "servus               "
PRINT
LINE INPUT "Deutsch: "; uebersetzUng$
IF UCASE$(uebersetzUng$) = "SKLAVE" THEN PRINT "R I C H T I G" ELSE PRINT "F A L S C H": GOTO 6
GOTO totalanfang

7 :
PRINT "currere              "
PRINT
LINE INPUT "Deutsch: "; uebersetzUng$
IF UCASE$(uebersetzUng$) = "LAUFEN" THEN PRINT "R I C H T I G" ELSE PRINT "F A L S C H": GOTO 7
GOTO totalanfang

8 :
PRINT "etiam                "
PRINT
LINE INPUT "Deutsch: "; uebersetzUng$
IF UCASE$(uebersetzUng$) = "AUCH SOGAR" THEN PRINT "R I C H T I G" ELSE PRINT "F A L S C H": GOTO 8
GOTO totalanfang

9 :
PRINT "vincere              "
PRINT
LINE INPUT "Deutsch: "; uebersetzUng$
IF UCASE$(uebersetzUng$) = "SIEGEN BESIEGEN" THEN PRINT "R I C H T I G" ELSE PRINT "F A L S C H": GOTO 9
GOTO totalanfang
 
10 :
PRINT "gaudere              "
PRINT
LINE INPUT "Deutsch: "; uebersetzUng$
IF UCASE$(uebersetzUng$) = "SICH FREUEN" THEN PRINT "R I C H T I G" ELSE PRINT "F A L S C H": GOTO 10
GOTO totalanfang

ende:
CLS
COLOR 7, 0
END

