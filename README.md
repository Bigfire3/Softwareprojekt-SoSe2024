# Softwareprojekt SoSe 2024

## Tutorial-Scene für Unity Spiel

Wie das Spiel vor dem Softwaresprint aussieht: [YouTube Short](https://youtube.com/shorts/3tWdn8CPG1E?feature=share)

Dieses Repository entsteht im Rahmen der Veranstaltung "Softwareentwicklung" im Sommersemester 2024 an der TU Bergakademie Freiberg und wird bearbeitet von Fabian Zänker (ROB) und Noah Steiner (ROB).

Dokumentation:

Das Tutorial für unser Handyspiel ist darauf ausgelegt, den Spieler interaktiv und schrittweise in die verschiedenen Spielmechaniken einzuführen. Ziel ist es, den Spieler durch gezielte Anweisungen und Überprüfungen optimal auf das Spiel vorzubereiten. Jeder Schritt im Tutorial stellt eine neue Mechanik vor, die der Spieler lernen soll. Dabei wird überprüft, ob der Spieler die Anweisungen korrekt befolgt. Erfüllt der Spieler die Anweisung, geht es zum nächsten Schritt. Bei fehlerhafter Ausführung wird der Prozess durch eine Textnachricht zurückgesetzt und der Spieler muss den Schritt erneut ausführen.

Ablauf des Tutorials
Das Tutorial ist als Zustandsmaschine (State Machine) aufgebaut und durchläuft verschiedene Zustände. Diese Zustände sind:

start: Der Beginn des Tutorials, in dem eine Einführung stattfindet.
waitForAim_0: Der Spieler muss eine Zielaktion ausführen.
waitForTouchEnd_0: Der Spieler soll die Berührung beenden.
waitForMoreJumps: Der Spieler muss mehrere Sprünge hintereinander ausführen.
waitForPassEnemy: Der Spieler muss einen Gegner passieren.
waitForPassFly: Der Spieler muss eine fliegende Herausforderung meistern.
waitForCollectBoost: Der Spieler soll einen Boost einsammeln.
boost: Der Boost wird aktiviert und der Spieler lernt, wie er diesen nutzt.
idle: Ein Ruhezustand, in dem auf die nächste Aktion des Spielers gewartet wird.
text_1: Eine Textnachricht wird angezeigt, um den Spieler weiter zu instruieren.
touch_1: Der Spieler muss den Bildschirm berühren, um fortzufahren.

Implementierung in einer Schleife
Das Tutorial ist in einer Schleife implementiert, in der ein Switch-Konstrukt den aktuellen Zustand überprüft und den entsprechenden Codeblock ausführt. 

Zustandsbeschreibung
start: Der Startzustand initialisiert das Tutorial und zeigt eine Einführungsnachricht an. Der Spieler wird auf das bevorstehende Training vorbereitet.
waitForAim_0: In diesem Zustand wird darauf gewartet, dass der Spieler eine Zielaktion ausführt. Dies ist der erste Schritt, um die Steuerung kennenzulernen.
waitForTouchEnd_0: Hier wartet das Tutorial darauf, dass der Spieler die Berührung des Bildschirms beendet, um die grundlegende Steuerung zu verstehen.
waitForMoreJumps: Das Tutorial wartet darauf, dass der Spieler mehrere Sprünge hintereinander ausführt, um die Sprungmechanik zu erlernen.
waitForPassEnemy: Der Spieler muss einen Gegner passieren, was die Ausweichmechanik vermittelt.
waitForPassFly: Hier wird gewartet, bis der Spieler eine fliegende Herausforderung meistert, um komplexere Bewegungsmuster zu verstehen.
waitForCollectBoost: Der Spieler soll einen Boost einsammeln, was die Nutzung von Power-Ups erklärt.
boost: In diesem Zustand wird der Boost aktiviert und dem Spieler gezeigt, wie er diesen effektiv nutzt.
idle: Ein Ruhezustand, in dem auf die nächste Aktion des Spielers gewartet wird, bevor die nächste Anweisung erteilt wird.
text_1: Eine Textnachricht wird angezeigt, um dem Spieler weitere Anweisungen oder Lob zu geben.
touch_1: Das Tutorial wartet auf eine Berührung des Bildschirms durch den Spieler, um zum nächsten Schritt fortzufahren.

Das Tutorial-System ist so konzipiert, dass es den Spieler Schritt für Schritt an die Spielmechaniken heranführt. Durch die Struktur als Zustandsmaschine wird ein klarer und geordneter Ablauf gewährleistet. Dies ermöglicht es dem Spieler, die notwendigen Fertigkeiten zu erlernen und im Spiel erfolgreich zu sein. Jeder Zustand ist gezielt darauf ausgerichtet, eine bestimmte Mechanik zu vermitteln und sicherzustellen, dass der Spieler diese korrekt versteht und anwendet.
