# Softwareprojekt SoSe 2024

## Tutorial-Scene für Unity Spiel

Wie das Spiel vor dem Softwaresprint aussieht: [YouTube Short](https://youtube.com/shorts/3tWdn8CPG1E?feature=share)

Dieses Repository entsteht im Rahmen der Veranstaltung "Softwareentwicklung" im Sommersemester 2024 an der TU Bergakademie Freiberg und wird bearbeitet von Fabian Zänker (ROB) und Noah Steiner (ROB).

Dokumentation:
Zu Beginn der Projektarbeit haben wir uns getroffen um unser weiteres Vorgehen zu besprechen und die Aufgaben aufgeteilt.
Daraufhin wurde ein erster Entwurf für das Script entworfen, welcher jedoch noch einmal überarbeitet werden musste, da er nicht ausführlich genug war. Nach erneute übersicht durch beide Personen haben wir das Script als fertig angesehen und sind zu den nächsten Schritten übergegangen. Es erfolgte eine grobe Aufteilung in die Planung und das Entwickeln des Codes, mit reger gegenseitiger Absprache bei Fragen, Ideen und Anregungen.

Das Tutorial für unser Handyspiel "Swipe" ist darauf ausgelegt, den Spieler interaktiv und Schritt-für-Schritt in die Spielmechanik einzuführen. Es werden dem Spieler konkrete Anweisungen gegeben, welche von einem Script überwacht und auf Vollständigkeit überprüft werden. Falls eine Aufgabe absolviert wird, schaltet das Script in die nächste Stufe. Bei fehlerhafter Ausführung wird das Tutorial zurückgesetzt und der Spieler muss das Tutorial erneut beginnen. Es ist uns noch nicht gelungen, nur die aktuelle Stufe zurückzusetzen, da jede Stufe ein gewisses Setup der Scene vorraussetzt, welches individuell für jede Stufe wieder hergestellt werden müsste. Die Entwicklung der Datei TutorialManager.cs ist in diesem Repository dokumentiert. Sie steuert und koordiniert die gesamte Scene. Der TutorialManager steht mit vielen anderen Scripts der Scene in Verbindung, wie zum Beispiel dem PlayerManager.

Ablauf des Tutorials:

Das Tutorial ist als Zustandsmaschine (State Machine) aufgebaut und durchläuft verschiedene Zustände. Diese Zustände sind:

start: Der Beginn des Tutorials, in dem eine Einführung stattfindet.
Der Startzustand initialisiert das Tutorial und zeigt eine Einführungsnachricht an. Der Spieler wird auf das bevorstehende Tutorial vorbereitet.


waitForAim_0: Der Spieler muss eine Zielaktion ausführen.
In diesem Zustand wird darauf gewartet, dass der Spieler eine Zielaktion ausführt. Dies ist der erste Schritt, um die Steuerung kennenzulernen.

waitForTouchEnd_0: Der Spieler soll die Berührung beenden.
Hier wartet das Tutorial darauf, dass der Spieler die Berührung des Bildschirms beendet, um die grundlegende Steuerung zu verstehen.

waitForMoreJumps: Der Spieler muss mehrere Sprünge hintereinander ausführen.
Das Tutorial wartet darauf, dass der Spieler mehrere Sprünge hintereinander ausführt, um die Sprungmechanik zu erlernen.


waitForPassEnemy: Der Spieler muss einen Gegner passieren.
Der Spieler muss einen Gegner passieren, was die Ausweichmechanik vermittelt.


waitForPassFly: Der Spieler muss eine fliegende Herausforderung meistern.
Hier wird gewartet, bis der Spieler eine fliegende Herausforderung meistert, um komplexere Bewegungsmuster zu verstehen.

waitForCollectBoost: Der Spieler soll einen Boost einsammeln.
Der Spieler soll einen Boost einsammeln, was die Nutzung von Power-Ups erklärt.

boost: Der Boost wird aktiviert und der Spieler lernt, wie er diesen nutzt.
In diesem Zustand wird der Boost aktiviert und dem Spieler gezeigt, wie er diesen effektiv nutzt.


idle: Ein Ruhezustand, in dem auf die nächste Aktion des Spielers gewartet wird.
Ein Ruhezustand, in dem auf die nächste Aktion des Spielers gewartet wird, bevor die nächste Anweisung erteilt wird.

text_1: Eine Textnachricht wird angezeigt, um den Spieler weiter zu instruieren.
Eine Textnachricht wird angezeigt, um dem Spieler weitere Anweisungen oder Lob zu geben.

touch_1: Der Spieler muss den Bildschirm berühren, um fortzufahren.
Das Tutorial wartet auf eine Berührung des Bildschirms durch den Spieler, um zum nächsten Schritt fortzufahren.


Das Tutorial-System ist so konzipiert, dass es den Spieler Schritt für Schritt an die Spielmechaniken heranführt. Durch die Struktur als Zustandsmaschine wird ein klarer und geordneter Ablauf gewährleistet. Dies ermöglicht es dem Spieler, die notwendigen Fertigkeiten zu erlernen und im Spiel erfolgreich zu sein. Jeder Zustand ist gezielt darauf ausgerichtet, eine bestimmte Mechanik zu vermitteln und sicherzustellen, dass der Spieler diese korrekt versteht und anwendet.

Verwendete Programmierkonzepte:

Der TutorialManager ist grundlegend eine State-Maschine, welche die verschiedenen Schritte des Tutorials durchläuft. Alle Schritte sind in einer Enumeration-Variable aufgeführt. Eine Switch-Anweisung in der Main Loop (in Unity "Update()" genannt) führt den Codeblock für die aktuelle Stufe aus. Wird eine Stufe aus irgendeinem Grund für erledigt markiert, schaltet die Switch in den nächsten Anweisungsblock. Ein Grund kann beispielsweise das Auslösen eines Events in einem anderen Script sein oder die Abarbeitung der aktuellen Stufe.
Ein oft verwendetes Programmierkonzept ist die Nutzung von Delegates, welche es einfach gesagt Funktionen erlaubt, Methoden als Parameter anzunehmen. Somit kann beispielsweise unterschiedlicher Code in den Abschnitten TouchBegin, TouchMoved oder TouchEnd ausgeführt werden.

Wie bereitserwähnt, hört der TutorialManager auf einige Events von anderen Objekten. Beispielsweise gibt es eine HandleOnPlayerDeath-Methode, welche definiert, was passieren soll, wenn der Spieler stirbt.

Wenn das Tutorial erfolgreich absolviert worden ist, wird im Speicher des Handys mittels PlayerPrefs eine Variable gesetzt, welche den erneuten Start des Tutorials verhindert. Schließlich soll das Tutorial nur bei erstmaligem Spielstart druchlaufen werden müssen. Trotzdem lässt es sich aus dem Main-Menu des Spiels erneut öffnen.
