# Softwareprojekt SoSe 2024

## Tutorial-Scene für Unity Spiel

Wie das Spiel vor dem Softwaresprint aussieht: [YouTube Short](https://youtube.com/shorts/3tWdn8CPG1E?feature=share)

Dieses Repository entsteht im Rahmen der Veranstaltung "Softwareentwicklung" im Sommersemester 2024 an der TU Bergakademie Freiberg und wird bearbeitet von Fabian Zänker (ROB) und Noah Steiner (ROB).

## Dokumentation
Zu Beginn der Projektarbeit haben wir uns getroffen um unser Vorgehen zu besprechen und die Aufgaben aufzuteilen. Daraufhin wurde ein erster Entwurf für das Script skizziert, welcher mehrmals überarbeitet werden musste, da er nicht präzise genug war. Als das Script funktionstauglich schien, begannen wir mit der Umsetzung. Mit reger gegenseitiger Absprache bei Fragen, Ideen und Anregungen entstand Schritt-für-Schritt das Tutorial.

Das Tutorial für unser Handyspiel "Swipe" ist darauf ausgelegt, den Spieler interaktiv und Schritt-für-Schritt in die Spielmechanik einzuführen. Es werden dem Spieler konkrete Anweisungen gegeben, welche von einem Script überwacht und auf Vollständigkeit überprüft werden. Falls eine Aufgabe absolviert wird, schaltet das Script in die nächste Stufe. Bei fehlerhafter Ausführung wird das Tutorial zurückgesetzt und der Spieler muss das Tutorial erneut beginnen. Die Entwicklung der Datei TutorialManager.cs ist in diesem Repository dokumentiert. Sie steuert und koordiniert die gesamte Scene. Der TutorialManager steht mit vielen anderen Scripts der Scene in Verbindung, wie zum Beispiel dem PlayerManager. Welche Objekte die Scene zusätzlich umfasst ist in folgender Grafik dargestellt: Grafik

## Ablauf des Tutorials

- start,
- waitForAim,
- waitForTouchEnd_0,
- waitForMoreJumps,
- waitForPassEnemy,
- waitForPassFly,
- waitForCollectBoost,
- waitForBoostEnd,
- doInBoostEnd,
- waitForZoneText,
- startScoreText,
- waitForScoreText,
- speedUpZone,
- doOnZoneFullScreen,
- waitForChanceText,
- startEndText,
- waitForEndText,
- end,
- playerDeath

## Verwendete Programmierkonzepte

Der TutorialManager ist grundlegend eine State-Maschine, welche die verschiedenen Schritte des Tutorials durchläuft. Alle Schritte sind in einer Enumeration-Variable aufgeführt. Eine Switch-Anweisung in der Main Loop (in Unity "Update()" genannt) führt den Codeblock für die aktuelle Stufe aus. Wird eine Stufe aus irgendeinem Grund für erledigt markiert, schaltet die Switch in den nächsten Anweisungsblock. Ein Grund kann beispielsweise das Auslösen eines Events in einem anderen Script sein oder die Abarbeitung der aktuellen Stufe.
Ein oft verwendetes Programmierkonzept ist die Nutzung von Delegates, welche es einfach gesagt Funktionen erlaubt, Methoden als Parameter anzunehmen. Somit kann beispielsweise unterschiedlicher Code in den Abschnitten TouchBegin, TouchMoved oder TouchEnd ausgeführt werden.

Wie bereitserwähnt, hört der TutorialManager auf einige Events von anderen Objekten. Beispielsweise gibt es eine HandleOnPlayerDeath-Methode, welche definiert, was passieren soll, wenn der Spieler stirbt.

Wenn das Tutorial erfolgreich absolviert worden ist, wird im Speicher des Handys mittels PlayerPrefs eine Variable gesetzt, welche den erneuten Start des Tutorials verhindert. Schließlich soll das Tutorial nur bei erstmaligem Spielstart druchlaufen werden müssen. Trotzdem lässt es sich aus dem Main-Menu des Spiels erneut öffnen.

Wie das Spiel nach dem Softwaresprint aussieht: [YouTube Short](https://www.youtube.com/shorts/Km4xLfjPZ-I)
(wurde von "DragBall" in "Swipe" umbenannt)

## Was noch nicht funktioniert

Einige Sonderfälle haben wir bis zum heutigen Stand noch nicht berücksichtigt. Beispielsweise wenn der Spieler den Boost nicht einsammelt oder die App unterbrochen wird. Außerdem funktioniert das zurücksetzen auf eine bestimmte State noch nicht, falls der Spieler stirbt. Dadurch, dass jede Stufe ein gewisses Setup der Scene voraussetzt, welches individuell für jede Stufe wieder hergestellt werden muss, wird aktuell die Scene nur angehalten.

In naher Zukunft soll das Update Swipe v3.0 in App- und PlayStore veröffentlicht werden. Bis das geschehen kann, muss das Tutorial vollständig sein sowie einige Bugs im Spiel behoben werden. Außerdem möchten wir IAP (In-App-Purchases) anbieten, da im letzten Monat ein Skinsystem inklusive Freischaltungssystem implementiert wurde.
