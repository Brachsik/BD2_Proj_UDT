using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace projectUDT_app
{
    public class Program
    {

        public static void Menu()
        {
            Console.WriteLine(@"
############################
#                          #  
#            MENU          #   
#                          #  
# 1. Wprowadz nowa figure. #
# 2. Wyswietl dane figur.  #
# 3. Usun wybrane figury.  #
# 4. Zakoncz.              #
#                          #
############################
            ");
            ShowMenu();
        }
        public static void ListFig()
        {
            Console.WriteLine(@"
#############################
#   1. Punkt.               #
#   2. Prosta.              #
#   3. Trojkat.             #
#   4. Kwadrat.             #
#   5. Prostokat.           #
#   6. Kolo.                #
#   7. Powrot do menu.      #
#############################");
        }

        public static string Figrecog(int option)
        {

            string figura = "";

            switch (option)
            {
                case 1:
                    figura = Figury.Shape.Punkt.ToString();
                    break;
                case 2:
                    figura = Figury.Shape.Prosta.ToString();
                    break;
                case 3:
                    figura = Figury.Shape.Trojkat.ToString();
                    break;
                case 4:
                    figura = Figury.Shape.Kwadrat.ToString();
                    break;
                case 5:
                    figura = Figury.Shape.Prostokat.ToString();
                    break;
                case 6:
                    figura = Figury.Shape.Kolo.ToString();
                    break;
                case 7:
                    throw new ArgumentException("Powrot do menu...");
                default:
                    throw new ArgumentException("Wybrano niewlasciwa opcje!");
            }
            return figura;
        }

        public static void ShowMenu()
        {
            try
            {
                int option = int.Parse(Console.ReadLine());

                        switch (option)
                        {
                            case 1:
                                MenuInsert();
                                break;
                            case 2:
                                MenuSelect();
                                break;
                            case 3:
                                MenuDelete();
                                break;
                            case 4:
                                throw new StopAppExcep("Exit...");
                            default:
                                throw new ArgumentException("Podano nieprawidlowy numer opcji!");
                        }
            }
            catch (Exception e)
            {
                string err_mssg;
                if (!e.Message.Equals(""))
                {
                    err_mssg = e.Message;
                }
                else
                {
                    err_mssg = "Wprowadzono nieprawidlowe dane!";
                }

                if (e.Message.Equals("Exit...")) throw new StopAppExcep();
                throw new ArgumentException(err_mssg);
            }
        }

        public static void MenuInsert()
        {
            Console.WriteLine(@"
#############################
#                           #
#    Creator figur          #
#    Wybierz figure.        #");     
            ListFig();

            int option;
            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch
            {
                throw new ArgumentException("Wprowadzono niepoprawne dane!");
            }

            string figura = Figrecog(option);
            InsertData(figura);
        }

        public static void InsertData(string figura)
        {
            string query = "INSERT INTO dbo." + figura + " VALUES('";

            Console.WriteLine(@"Wprowadz wspolrzedne figury!");

            switch (figura)
            {
                case "Punkt":
                    Console.WriteLine(@"Example: 1/1");
                    break;
                case "Prosta":
                    Console.WriteLine(@"Example: 1/1/2/2");
                    break;
                case "Trojkat":
                    Console.WriteLine(@"Example: 1/1/2/2/3/3");
                    break;
                case "Kwadrat":
                    Console.WriteLine(@"Example: 1/1/2/2/3/3/4/4");
                    break;
                case "Prostokat":
                    Console.WriteLine(@"Example: 1/1/2/2/3/3/4/4");
                    break;
                case "Kolo":
                    Console.WriteLine(@"Example: 1/1/2/2");
                    break;

            }

            string wspolrzedne = Console.ReadLine();

            query += wspolrzedne;
            query += "');";

            DBQueryConnection database = new DBQueryConnection();
            database.InsertQuery(query);

        }

        public static void MenuSelect()
        {
            Console.WriteLine(@"
#############################
#                           #
#    Wyswietl dane figur.   #
#    Wybierz figure.        #");
            ListFig();

            int option;
            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch
            {
                throw new ArgumentException("Wprowadzono niepoprawne dane!");
            }

            string figura = Figrecog(option);
            ChooseSelectOption(figura);
        }

        public static void ChooseSelectOption(string figura)
        {
            Console.WriteLine(@"
#####################################################################
#                                                                   #
#   Wyswietl dane figur.                                            #
#   Wybierz opcje.                                                  #
#                                                                   #
#   1. Wyswietl wspolrzedne zarejestrowanych figur.                 #
#   2. Wyswietl wspolrzedne zarejestrowanych figur wraz z obwodem.  #
#   3. Wyswietl wspolrzedne zarejestrowanych figur wraz z polem.    #
#   4. 2 + Sortowanie.                                              #
#   5. 3 + Sortowanie.                                              #
#   6. Wyswietl figury o obwodzie mniejszym od zadanej wartosci.    #
#   7. Wyswietl figury o obwodzie wiekszym od zadanej wartosci.     #
#   8. Wyswietl figury o polu mniejszym od zadanej wartosci.        #
#   9. Wyswietl figury o polu wiekszym od zadanej wartosci.         #
#   10. Wyswietl figury o obwodzie rownym zadanej wartosci.         #
#   11. Wyswietl figury o polu rownym zadanej wartosci.             #
#   12. Wyswietl wszystkie rekordy dla zadanej figury.              #
#   13. Powrot do menu.                                             #
#                                                                   #
#####################################################################

");

            int option;
            List<string> atrybuty = new List<string>();
            string query = "SELECT " + figura + ".ToString() AS Info ";
            string query_if_sort = "";
            string query_where_condition = "";

            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch
            {
                throw new ArgumentException("Wprowadzono niepoprawne dane!");
            }

            atrybuty.Add("Info");

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    atrybuty.Add("Obwod");
                    query += ", " + figura + ".Obwod() AS Obwod ";
                    break;
                case 3:
                    atrybuty.Add("Pole");
                    query += ", " + figura + ".Pole() AS Pole ";
                    break;
                case 4:
                    atrybuty.Add("Obwod");
                    query += ", " + figura + ".Obwod() AS Obwod ";
                    query_if_sort = SortChoice("Obwod");
                    break;
                case 5:
                    atrybuty.Add("Pole");
                    query += ", " + figura + ".Pole() AS Pole ";
                    query_if_sort = SortChoice("Pole");
                    break;
                case 6:
                    atrybuty.Add("Obwod");
                    query += ", " + figura + ".Obwod() AS Obwod ";
                    query_where_condition = AdvancedFigOptionSel(figura, "Obwod", "<");
                    break;
                case 7:
                    atrybuty.Add("Obwod");
                    query += ", " + figura + ".Obwod() AS Obwod ";
                    query_where_condition = AdvancedFigOptionSel(figura, "Obwod", ">");
                    break;
                case 8:
                    atrybuty.Add("Pole");
                    query += ", " + figura + ".Pole() AS Pole ";
                    query_where_condition = AdvancedFigOptionSel(figura, "Pole", "<");
                    break;
                case 9:
                    atrybuty.Add("Pole");
                    query += ", " + figura + ".Pole() AS Pole ";
                    query_where_condition = AdvancedFigOptionSel(figura, "Pole", ">");
                    break;
                case 10:
                    atrybuty.Add("Obwod");
                    query += ", " + figura + ".Obwod() AS Obwod ";
                    query_where_condition = AdvancedFigOptionSel(figura, "Obwod", "=");
                    break;
                case 11:
                    atrybuty.Add("Pole");
                    query += ", " + figura + ".Pole() AS Pole ";
                    query_where_condition = AdvancedFigOptionSel(figura, "Pole", "=");
                    break;
                case 12:
                    atrybuty.Add("Obwod");
                    atrybuty.Add("Pole");
                    query += ", " + figura + ".Obwod() AS Obwod";
                    query += ", " + figura + ".Pole() AS Pole ";
                    break;
                case 13:
                    throw new ArgumentException("Powrot do menu..");
                default:
                    throw new ArgumentException("Wybrano niewlasciwa opcje!");
            }
            query += "FROM dbo." + figura;

            if (!query_where_condition.Equals(""))
            {
                query += query_where_condition;
            }

            if (!query_if_sort.Equals(""))
            {
                query += query_if_sort;
            }

            query += ";";

            DBQueryConnection database = new DBQueryConnection();
            database.SelectQuery(query, atrybuty);
        }

        public static string SortChoice(string thing)
        {
            string query = " ORDER BY " + thing;
            Console.WriteLine(@"
#############################
#                           #
#   Wyswietl dane figur.    #
#   Sortuj:                 #
#   1. Rosnaco.             #
#   2. Malejaco.            #
#                           #
#############################
");

            int option;

            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch
            {
                throw new ArgumentException("Wprowadzono niepoprawne dane!");
            }

            switch (option)
            {
                case 1:
                    return query;
                case 2:
                    return query + " DESC";
                default:
                    throw new ArgumentException("Wybrano niewlasciwa opcje!");
            }
        }

        public static string AdvancedFigOptionSel(string figura, string property, string delimiter)
        {
            string value;
            Console.WriteLine(@"
#############################+                                               
#                           #                
#   Wyswietl dane figur.    #
#   Prosze podac wartosc:   #
#                           #
#############################
");

            try
            {
                value = Console.ReadLine();
            }
            catch
            {
                throw new ArgumentException("Wprowadzono niepoprawne dane!");
            }

            return " WHERE " + figura + ".Wyznacz" + property + "() " + delimiter + " CONVERT(NVARCHAR, " + value + ") ";
        }



        //----------------------------------------------------- DELETE ---------------------------------------------------------------

        public static string CondiditionDelete(string figura, string property)
        {
            string value;
            Console.WriteLine(@"
#############################+                                               
#                           #                
#  Wpisz wspolrzedne figury #
#  ktora chcesz usunac      #
#                           #
#############################

Example: 1/1 itd w zaleznosci od wybranej figury!

");

            try
            {
                value = Console.ReadLine();
            }
            catch
            {
                throw new ArgumentException("Wprowadzono niepoprawne dane!");
            }
            return " WHERE " + figura + ".Wyznacz" + property + "() = CAST(\'(" + value + ")\' AS nvarchar )";
        }

        public static void MenuDelete()
        {
            Console.WriteLine(@"
##################################################
#                                                #
#  Wybierz zbior danych z ktorego chcesz usuwac! #
#                                                #
##################################################   
");
            ListFig();

            int option;
            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch
            {
                throw new ArgumentException("Wprowadzono niepoprawne dane!");
            }

            string figura = Figrecog(option);
            DeleteOptions(figura);
        }

        public static void DeleteOptions(string figura)
        {
            Console.WriteLine(@"
#########################################
#  Usun dane wybranej figury.           #
#                                       #
#  Wybierz opcje:                       #
#  1. Usun figure poprzez wspolrzedne.  #
#  2. Usun wszystkie rekordy.           #
#  3. Powrot do menu.                   #
#                                       #
#########################################
  
Wybierz opcje:
");
            int option;
            string query = "DELETE dbo." + figura;

            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch
            {
                throw new ArgumentException("Wprowadzono niepoprawne dane!");
            }

            switch (option)
            {
                case 1:
                    query += CondiditionDelete(figura, "Wspolrzedne");
                    break;
                case 2:
                    break;
                case 3:
                    throw new ArgumentException("Powrot do menu...");

                default:
                    throw new ArgumentException("Wybrano niewlasciwa opcje!");
            }

            query += ";";
            DBQueryConnection database = new DBQueryConnection();
            database.DeleteQuery(query);
        }


        // ----------------------------------------------------------------------- MAIN -----------------------------------------------------------

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Menu();
                }
                catch (StopAppExcep)
                {
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }
    }
}