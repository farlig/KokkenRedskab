// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.CompilerServices;

Console.OutputEncoding = System.Text.Encoding.UTF8;
var menuItems = new List<string> { "Opskriftsomregner", "Indkøbsudregner", "Lagerstyring", "Slut" }; //liste med alle menupunkter. hvis dette ændres, er det vigtigst at slut stadig ligger til sidst
int valgtIndex = 0;

while (true) //loop kører for evigt
{
    Console.Clear();
    for (int i = 0; i < menuItems.Count; i++) //giver alternativ baggrundsfarve til den valgte mulighed
    {
        if (i == valgtIndex)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        else
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine(menuItems[i]);
    }
    Console.ResetColor();
    var key = Console.ReadKey(true);
    if (key.Key == ConsoleKey.UpArrow) //scroll op og ned i menu med arrow keys og vælg mulighed med enter
    {
        valgtIndex = (valgtIndex - 1 + menuItems.Count) % menuItems.Count;
    }
    else if (key.Key == ConsoleKey.DownArrow)
    {
        valgtIndex = (valgtIndex + 1) % menuItems.Count;
    }
    else if (key.Key == ConsoleKey.Enter)
    {
        if (valgtIndex == menuItems.Count - 1) //tilbage knap
        {
            break;
        }
        else if (valgtIndex == 0) //første menumulighed
        {
            string opskrifterPath = @"C:\Users\Anton\Desktop\skole\programmering\projekt_1\opskrifter"; //definerer placering for folder med opskrifter. SKIFT DENNE FILSTI TIL PLACERINGEN AF JERES CSV/OPSKRIFT FILER
            List<string> opskriftListe = Directory.GetFiles(opskrifterPath).ToList(); //laver en liste med alle filer i den tidligere nævnte folder
            opskriftListe.Add("Tilbage");

            if (opskriftListe.Count == 0) //tjeker om folderen er tom
            {
                Console.WriteLine("Ingen opskrifter fundet");
                Console.ReadKey();
            }

            int opskrifterIndex = 0;
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Vælg en opskrift");

                for (int i = 0; i < opskriftListe.Count; i++) //giver alternativ baggrundsfarve til den valgte mulighed
                {
                    if (i == opskrifterIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    string opskriftNavn = Path.GetFileNameWithoutExtension(opskriftListe[i]); //henter navne på filer i folderen, uden path eller file extension
                    string formatOpskrift = opskriftNavn.Replace("_", " "); //fjerner _ fra filnavne
                    Console.WriteLine(formatOpskrift);
                }
                Console.ResetColor();

                var opskriftKey = Console.ReadKey(true);

                if (opskriftKey.Key == ConsoleKey.UpArrow) //scroll op og ned i menu med arrow keys og vælg mulighed med enter
                {
                    opskrifterIndex = (opskrifterIndex - 1 + opskriftListe.Count) % opskriftListe.Count;
                }
                else if (opskriftKey.Key == ConsoleKey.DownArrow)
                {
                    opskrifterIndex = (opskrifterIndex + 1) % opskriftListe.Count;
                }
                else if (opskriftKey.Key == ConsoleKey.Enter)
                {
                    if (opskrifterIndex == opskriftListe.Count - 1) //tilbage knap
                    {
                        break;
                    }
                    else //opskrifter
                    {
                        bool omdannet = false;
                        int personer = 0;

                        Console.Clear();

                        while (!omdannet) //håndterer input af mængde mennesker og validerer at et gyldigt tal er indtastet. hvis ikke, prøver den igen.
                        {
                            Console.Write("Hvor mange mennesker skal du lave mad til? ");
                            string mennesker = Console.ReadLine();

                            if (int.TryParse(mennesker, out personer))
                            {
                                omdannet = true;
                            }
                            else
                            {
                                Console.WriteLine("Du indtastede ikke et gyldigt tal, prøv igen.");
                                Thread.Sleep(500);
                            }

                        }

                        string[] linjer = File.ReadAllLines(opskriftListe[opskrifterIndex]); //laver et array med hver linje af den valgte opskrift (csv fil)

                        foreach (string linje in linjer) //kører følgende loop en gang per linje i csv fil
                        {
                            string[] dele = linje.Split('.'); //deler linje op ved punktum, så man får 3 array entries per linje

                            if (double.TryParse(dele[1], out double mængde)) //omdanner anden entry i hver linje i csv fil til en double
                            {
                                double nyMængde = mængde * personer; //udregner nye ingrediensmængder
                                Console.WriteLine($"{dele[0]} {nyMængde} {dele[2]}"); //printer nye ingrediensmængder
                            }
                            else
                            {
                                Console.WriteLine("Kunne ikke omdanne mængde"); //giver en fejl, hvis opskriften er indtastet forkert og der ikke er et tal som anden entry pr linje
                            }
                        }
                        Console.Write("Tryk på en knap for at gå tilbage");
                        Console.ReadKey();
                    }
                }
            }
        }
        else if (valgtIndex == 2) // tredje menumulighed
        {
            string lager = @"C:\Users\anton\Desktop\skole\programmering\projekt_1\lager\lager.csv"; //filsti for lagerfilen
            string[] lagerLinjerTemp = File.ReadAllLines(lager);
            List<String> lagerLinjer = lagerLinjerTemp.ToList();
            lagerLinjer.Add("Tilbage");
            int lagerIndex = 0;
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("The Holy Lagerstyring");
                Console.WriteLine();
                for (int i = 0; i < lagerLinjer.Count; i++) //giver alternativ baggrundsfarve til den valgte mulighed
                {
                    if (i == lagerIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    string formatLager = lagerLinjer[i].Replace('.', ' ');
                    Console.WriteLine(formatLager);
                }
                Console.ResetColor();

                var lagerKey = Console.ReadKey(true);

                if (lagerKey.Key == ConsoleKey.UpArrow) //scroll op og ned i menu med arrow keys og vælg mulighed med enter
                {
                    lagerIndex = (lagerIndex - 1 + lagerLinjer.Count) % lagerLinjer.Count;
                }
                else if (lagerKey.Key == ConsoleKey.DownArrow)
                {
                    lagerIndex = (lagerIndex + 1) % lagerLinjer.Count;
                }
                else if (lagerKey.Key == ConsoleKey.Enter)
                {
                    if (lagerIndex == lagerLinjer.Count - 1) //tilbage knap
                    {
                        break;
                    }
                    else
                    {
                        string formatLager = lagerLinjer[lagerIndex].Replace(".", " ");
                        Console.Clear();
                        Console.Write(formatLager);
                        Console.ReadKey();
                    }
                }
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine($"Du valgte {menuItems[valgtIndex]}");
            Console.ReadKey();
        }
    }
}    
