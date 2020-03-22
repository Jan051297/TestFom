using System;

namespace Trinkspiel
{
    class FragenTxt //Fragen werden aus txt Datei gezogen
    {
        string text = System.IO.File.ReadAllText(@"C:\Users\kaise\Source\Repos\TestFom\TestFom\trinkspiel_fragen.txt");
        public string[] lines = System.IO.File.ReadAllLines(@"C:\Users\kaise\Source\Repos\TestFom\TestFom\trinkspiel_fragen.txt");
    }
    class Moderator
    {
        public void Spielstart(Setup md, Spieler[] pe, FragenTxt fragen)
        {
            int Qiterate = 1;
            for (int i = 0; i < md.anzRunden; i++)
            {
                for (int a = 0; a < md.anzSpieler; a++)
                {
                    Console.WriteLine("_______________________________________________________________");
                    Console.WriteLine("Hallo " + pe[a].name + " Deine Frage:\nIch hab: " + fragen.lines[Qiterate++]);
                    string antwort = Console.ReadLine();
                    if (antwort == "ja" || antwort == "Ja")
                    {
                        Console.WriteLine("Trinke " + md.anzSchlücke + " Schlücke");

                        pe[a].yesCount++;
                    }
                    else
                    {
                        Console.WriteLine("Nagut der Nächste ist dran!");

                        pe[a].noCount++;
                    }
                }
            }
        }
        public void Ergebnistabelle(Spieler[] pe, Setup md) //Dient der Ausgabe der Spielergebnisse nach Ende der letzten Runde
        {
            Console.WriteLine("Ergebnis nach " + md.anzRunden + " Runden:");
            for (int i = 0; i < md.anzSpieler; i++)
            {
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine(pe[i].name + " musste " + pe[i].yesCount + " mal " + md.anzSchlücke +
                    " Schlücke trinken.\nDas sind insgesamt " + (pe[i].yesCount * md.anzSchlücke) + " Schlücke");
                Console.WriteLine("Es wurden dem nach " + pe[i].noCount + " Fragen mit nein und " + pe[i].yesCount
                     + " Fragen mit ja beantwortet");

            }
        }
    }
    class Spieler //Spielerklasse
    {

        public string name;
        public int yesCount;
        public int noCount;
        public Spieler(string _name)
        {
            name = _name;
        }
    }
    class Setup //Setupklasse für die Spieleinstellungen
    {

        public int anzRunden;
        public int anzSchlücke;
        public int anzSpieler;
        public void Spielsetup()
        {
            Console.WriteLine("|------------------------------------|");
            Console.WriteLine(" Willkommen bei: Ich hab noch nie ...");
            Console.WriteLine("|------------------------------------|\n");
            Console.WriteLine("Wie viele Runden werden gespielt?");
            anzRunden = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Wie viele Schlücke sollen bei JA vergeben werden?");
            anzSchlücke = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Anzahl der Spieler eingeben:");
            anzSpieler = Convert.ToInt32(Console.ReadLine());

        }
        public void Eingabekontrolle(Spieler[] sp)
        {
            Console.WriteLine("Eingabekontrolle:");
            for (int i = 0; i < anzSpieler; i++)
            {
                Console.WriteLine("Spieler " + (i + 1) + " wurde der Name " + sp[i].name + " gegeben.");
            }
            Console.WriteLine("Rundenanzahl: " + anzRunden);
            Console.WriteLine("Schlücke bei JA: " + anzSchlücke);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            // neue Setup instanz
            Setup newSetup = new Setup();
            // neue Moderator instanz
            Moderator newModerator = new Moderator();
            // neue Fragen instanz
            FragenTxt newFragen = new FragenTxt();
            // Spielsetup sammelt die Runden- Spieler und Schluckzahl 
            newSetup.Spielsetup();
            // Es werden jeweils so viele Spieler Instanzen (Array von Objekten) erstellt wie im Spielsetup angegeben wurde
            // Dabei werden die Namen über den Konstruktor vergeben
            Spieler[] sp = new Spieler[newSetup.anzSpieler];
            for (int i = 0; i < newSetup.anzSpieler; i++)
            {
                Console.WriteLine("Name von Spieler " + (i + 1) + ":");
                string nameEingabe = Convert.ToString(Console.ReadLine());
                sp[i] = new Spieler(nameEingabe);
                Console.WriteLine("");
            }
            // Methode aus Setup Klasse für die Ausgabe der Einstellungen und Spieler
            newSetup.Eingabekontrolle(sp);
            // Methode aus Moderator Klasse mit der Spiellogik und Abfrage
            newModerator.Spielstart(newSetup, sp, newFragen);
            // Methode aus der Moderator Klasse zur Ausgabe der Spielergebnisse
            newModerator.Ergebnistabelle(sp, newSetup);

        }
    }
}
