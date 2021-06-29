using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntonProgramTwo
{
    class Program
    {
        private const int LIST_SIZE = 3;

        static void showPatient(Patient p)
        {
            // TODO put code to display the patient's name, id and procedure cost here.
            
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Patient Information");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Patient ID: {0}", p.Id);
            Console.WriteLine("Patient Name: {0} {1}", p.FirstName, p.LastName);
            Console.WriteLine("Procedure Cost: ${0}", p.ProcedureCost); 
        }//end showPatient

        //DisplayMainMenu displays the main menu
        static void displayMainMenu()
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Menu Options\n");
            Console.WriteLine("(F)ind");
            Console.WriteLine("(S)tatistics");
            Console.WriteLine("(R)emove");
            Console.WriteLine("(Q)uit");
            Console.WriteLine("Please enter your selection below:");
        }//end DisplayMainMenu

        //DisplayStats displays the statistics for the user found 
        static void displayStats(PatientData data)
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("STATISTICS");
            Console.WriteLine("Number of Array Slots: {0}", LIST_SIZE);
            Console.WriteLine("Percentage of Array Slots Used: {0}%", data.CalculatePercentArrayUsed());
            Console.WriteLine("Max Length of Linked List: {0}", data.FindLongestLength());
            Console.WriteLine("Average Length of Linked Lists: {0}", data.CalculateAvgLength());
        }//end DisplayStats

        //displayPatRemoved displays that the patient has been removed
        static void displayPatRemoved(Patient removedPat)
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Patient {0} {1} with ID {2} has been removed from the system.", removedPat.FirstName, removedPat.LastName, removedPat.Id);

        }//end displayPatRemoved

        //ValidateMenu displays the menu and validates the selection
        static char validateMenu()
        {
            displayMainMenu();
            char input = Console.ReadLine()[0];
            input = Char.ToUpper(input);

            //validate input
            while(input != 'F' && input != 'S' && input != 'R' && input != 'Q')
            {
                Console.WriteLine("\nThis is an invalid input. Please enter another input...");

                displayMainMenu();
                input = Console.ReadLine()[0];
                input = Char.ToUpper(input);
            }//end invalid input           
            return input;
        }//end ValidateMenu   

        static void Main(string[] args)
        {
            //declare menu option
            char menuOption = ' ';
            PatientData data = new PatientData(LIST_SIZE);
            data.LoadPatients("C:\\Users\\SMAUG\\source\\repos\\HuntonProgramTwo\\HuntonProgramTwo\\patientList.csv");

            //display the menu
            menuOption = validateMenu();

            //run while for quit option
            while (menuOption != 'Q')
            {
                //selection structure for Find name
                if (menuOption == 'F')
                {
                    Console.WriteLine("\nEnter first and last name:");
                    string name = Console.ReadLine();

                    String[] names = name.Split();
                    //do not allow any invalid types that can be entered 
                    while (names.Length != 2)
                    {
                        // TODO -- you may need to modify where you put the end brace
                        // You may also want to change the sense of the boolean expression in the 'if' above
                        // There are multiple ways to handle bad input,
                        // but you shouldn't let bad input crash your program

                        //display error
                        Console.WriteLine("\nError. This is not a valid input. Please enter the patient's");
                        Console.WriteLine("first and last name with a space between.");

                        //request input
                        Console.WriteLine("\nEnter first and last name:");
                        name = Console.ReadLine();
                        names = name.Split();
                    }//end of while loop checking for errors
                    Patient.Comparisons = 0;
                    Patient pat = data.FindPatientByName(names[0], names[1]);
                    if (pat != null)
                    {
                        //display the patient
                        showPatient(pat);
                        //display comparisons
                        Console.WriteLine("Comparisons made: {0}", Patient.Comparisons);
                    }
                    else
                    {
                        Console.WriteLine("\nPatient not found");
                    }
                }//end Find menu option selection

                //else if menu option Stats
                else if (menuOption == 'S')
                {
                    displayStats(data);
                }//end Stats menu option

                //else menu option Remove
                else
                {
                    Console.WriteLine("Enter first and last name of the patient to be removed:");
                    string name = Console.ReadLine();

                    String[] names = name.Split();
                    //do not allow any invalid types that can be entered 
                    while (names.Length != 2)
                    {
                        //display error
                        Console.WriteLine("Error. This is not a valid input. Please enter the patient's");
                        Console.WriteLine("first and last name with a space between.");

                        //request input
                        Console.WriteLine("Enter first and last name of the patient to be removed:");
                        name = Console.ReadLine();
                        names = name.Split();
                    }//end of while loop checking for errors
                    Patient removedPatient = data.RemovePatientByName(names[0], names[1]);
                        if (removedPatient != null)
                        {

                            //declare the patient was successfully removed
                            displayPatRemoved(removedPatient);
                        }
                        else
                        {
                            Console.WriteLine("\nPatient not found");
                        }    
                }//end Remove menu option selection

                //re-display the menu
                menuOption = validateMenu();
            }//end quit menu option
        }//end of Main Method
    }//end of Class
}//end of namespace
