using Dal;
using DalApi;
using DO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Data.Common;
using System.Xml.Linq;
using System.Data;
using System.Reflection.Metadata.Ecma335;
namespace DalTest
{
    internal class Program
    {
        static readonly IDal? s_dal = Factory.Get;
        //static readonly IDal? s_dal = new DalList();
        //static readonly IDal? s_dal = new DalXml();
        /// <summary>
        /// A method that prompts the user for all the details of a volunteer and returns a new Volunteer object.
        /// </summary>
        /// <returns>
        /// A <see cref="Volunteer"/> object containing the details of the volunteer entered by the user.
        /// </returns>
        private static Volunteer GetVolunteerDetails()
        {
            Console.WriteLine("Enter all detailes: \n" + "Volunteer's id:");
            int VolunteerId = int.Parse(Console.ReadLine());
            Console.WriteLine("Volunteer full name:");
            string FullName = Console.ReadLine();
            Console.WriteLine("Volunteer's fhone number:");
            string PhoneNum = Console.ReadLine();
            Console.WriteLine("Volunteer's Email:");
            string Email = Console.ReadLine();
            Console.WriteLine("password:");
            string? Password = Console.ReadLine();
            Console.WriteLine("Volunteer's Adress:");
            string? Adress = Console.ReadLine();
            Console.WriteLine("Adress's latitude:");
            double? Latitude = double.Parse(Console.ReadLine());
            Console.WriteLine("Adress's longitude:");
            double? Longitude = double.Parse(Console.ReadLine());
            Console.WriteLine("Volunteer's role (manager? press 0. Volunteer? press 1.) :");
            int tmpInput = int.Parse(Console.ReadLine());
            Role role = (Role)tmpInput;
            Console.WriteLine("Active? press 1. not active - press 0. :");
            tmpInput = int.Parse(Console.ReadLine());
            System.Boolean IsActive = (tmpInput == 1) ? true : false;
            Console.WriteLine("Max distance you can arrive:");
            double? MaxDistance = double.Parse(Console.ReadLine());
            Console.WriteLine("Distance's type (Aerial-press 0, Walking-press 1, Driving-press 2) :");
            tmpInput = int.Parse(Console.ReadLine());
            DistanceType distanceType = (DistanceType)tmpInput;

            Volunteer newValunteer = new Volunteer(VolunteerId, FullName, PhoneNum,
               Email, Password, Adress, Latitude, Longitude, role, IsActive, MaxDistance);

            return newValunteer;
        }
        /// <summary>
        /// A method that prompts the user for all the details of a call and returns a new Call object.
        /// </summary>
        /// <returns>
        /// A <see cref="Call"/> object containing the details of the call entered by the user.
        /// </returns>
        private static Call GetCallDetails()
        {
            Console.WriteLine("Enter all detailes: \n" + "Call's id:");
            int CallId = int.Parse(Console.ReadLine());
            Console.WriteLine("Call's type (NurseHelp-press 1, SongCheer-press 2, KidsActivity-press 3, CoockFood-press 4, Delivery-press 5) :");
            int tmpInput = int.Parse(Console.ReadLine());
            DO.CallType callType = (DO.CallType)tmpInput;
            Console.WriteLine("Verbal description:");
            string? VerbalDescription = Console.ReadLine();
            Console.WriteLine("Call's adress:");
            string Adress = Console.ReadLine();
            Console.WriteLine("Adress's latitude:");
            double Latitude = double.Parse(Console.ReadLine());
            Console.WriteLine("Adress's longitude:");
            double Longitude = double.Parse(Console.ReadLine());
            Console.WriteLine("At what time should the call be applied? (write in format (yy, mm, dd, hh, mm, ss))");
            DateTime CallOpenTime = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("At what time will the call end? (write in format (yy, mm, dd, hh, mm, ss))");
            DateTime MaxEndTime = DateTime.Parse(Console.ReadLine());

            Call newCall = new Call(0, callType, VerbalDescription, Adress, Latitude, Longitude, CallOpenTime, MaxEndTime);
            s_dal!.Call.Create(newCall);

            return newCall;
        }
        /// <summary>
        /// A method that prompts the user for all the details of an assignment and returns a new Assignment object.
        /// </summary>
        /// <returns>
        /// A <see cref="Assignment"/> object containing the details of the assignment entered by the user.
        /// </returns>
        private static Assignment GetAssignmentDetails()
        {
            Console.WriteLine("Enter all detailes: \n" + "Assignment's id:");
            int AssignmentId = int.Parse(Console.ReadLine());
            Console.WriteLine("Call's id:");
            int CallId = int.Parse(Console.ReadLine());
            Console.WriteLine("Volunteer's id:");
            int VolunteerId = int.Parse(Console.ReadLine());
            Console.WriteLine("What time should the treatment be applied? (write in format (yy, mm, dd, hh, mm, ss))");
            DateTime TreatStart = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("At what time will the treatment end? (write in format (yy, mm, dd, hh, mm, ss))");
            DateTime TreatEnd = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("How did the treatment end? Treated-press 1, SelfCancle-press 2, ManagerCancle-press 3, Expired-press 4");
            int tmpInput = int.Parse(Console.ReadLine());
            TypeTreatEnd typeTreatEnd = (TypeTreatEnd)tmpInput;

            Assignment newAssignment = new Assignment(0, 0, VolunteerId, TreatStart, TreatEnd, typeTreatEnd);
            return newAssignment;
        }
        /// <summary>
        /// A generic method that adds a new entity (Volunteer, Call, or Assignment) to the data store based on the type parameter.
        /// </summary>
        /// <typeparam name="T">The type of entity to be added (either <see cref="Volunteer"/>, <see cref="Call"/>, or <see cref="Assignment"/>).</typeparam>
        /// <param name="input">An integer input value used to determine the subsequent action, such as displaying the newly added entity.</param>
        private static void AddEntity<T>(int input) where T : class
        {
            if (typeof(T)?.Equals(typeof(Volunteer)) ?? false)
            {
                s_dal!.Volunteer.Create(GetVolunteerDetails());
                ViewEntity<Volunteer>(input);
            }
            else if (typeof(T)?.Equals(typeof(Call)) ?? false)
            {
                s_dal!.Call.Create(GetCallDetails());
                ViewEntity<Call>(input);
            }
            else
            {
                s_dal!.Assignment.Create(GetAssignmentDetails());
                ViewEntity<Assignment>(input);
            }

        }
        /// <summary>
        /// A generic method that retrieves and displays the details of a single entity (Volunteer, Call, or Assignment) from the data store based on the type parameter.
        /// </summary>
        /// <typeparam name="T">The type of entity to be viewed (either <see cref="Volunteer"/>, <see cref="Call"/>, or <see cref="Assignment"/>).</typeparam>
        /// <param name="input">An integer input value that represents the ID of the entity to be viewed.</param>
        private static void ViewEntity<T>(int input) where T : class
        {
            if (typeof(T)?.Equals(typeof(Volunteer)) ?? false)
            {
                Console.WriteLine(s_dal!.Volunteer.Read(input));
            }
            else if (typeof(T)?.Equals(typeof(Call)) ?? false)
            {
                Console.WriteLine(s_dal!.Call.Read(input));
            }
            else
            {
                Console.WriteLine(s_dal!.Volunteer.Read(input));
            }
        }
        /// <summary>
        /// A generic method that retrieves and displays all entities of a specified type (Volunteer, Call, or Assignment) from the data store.
        /// </summary>
        /// <typeparam name="T">The type of entity to be viewed (either <see cref="Volunteer"/>, <see cref="Call"/>, or <see cref="Assignment"/>).</typeparam>
        private static void ViewEntitys<T>() where T : class
        {
            if (typeof(T)?.Equals(typeof(Volunteer)) ?? false)
            {
                foreach (Volunteer volunteer in s_dal!.Volunteer.ReadAll())
                {
                    Console.WriteLine(volunteer);
                }
            }
            else if (typeof(T)?.Equals(typeof(Call)) ?? false)
            {
                foreach (Call call in s_dal!.Call.ReadAll())
                {
                    Console.WriteLine(call);
                }
            }
            else
            {
                foreach (Assignment assignment in s_dal!.Assignment.ReadAll())
                {
                    Console.WriteLine(assignment);
                }
            }

        }
        /// <summary>
        /// A generic method that allows updating an existing entity (Volunteer, Call, or Assignment) based on the type parameter.
        /// It displays the current details of the entity, allows updates, and then shows the updated entity.
        /// </summary>
        /// <typeparam name="T">The type of entity to be updated (either <see cref="Volunteer"/>, <see cref="Call"/>, or <see cref="Assignment"/>).</typeparam>
        /// <param name="input">An integer input value used to identify the entity to be updated and later displayed.</param>
        private static void UpdateEntity<T>(int input) where T : class
        {
            if (typeof(T)?.Equals(typeof(Volunteer)) ?? false)
            {
                ViewEntity<Volunteer>(input);
                s_dal!.Volunteer.Update(GetVolunteerDetails());
                ViewEntity<Volunteer>(input);
            }
            else if (typeof(T)?.Equals(typeof(Call)) ?? false)
            {
                ViewEntity<Call>(input);
                s_dal!.Call.Update(GetCallDetails());
                ViewEntity<Call>(input);
            }
            else
            {
                ViewEntity<Assignment>(input);
                s_dal!.Assignment.Update(GetAssignmentDetails());
                ViewEntity<Assignment>(input);
            }

            Console.WriteLine(s_dal!.Volunteer.Read(input));
        }
        /// <summary>
        /// A method that retrieves and displays the details of a single <see cref="Call"/> entity from the data store.
        /// </summary>
        /// <param name="input">An integer input value representing the ID of the <see cref="Call"/> entity to be viewed.</param>
        private static void ViewCall(int input)
        {
            Console.WriteLine(s_dal!.Call.Read(input));
        }
        /// <summary>
        /// A method that retrieves and displays the details of a single <see cref="Assignment"/> entity from the data store.
        /// </summary>
        /// <param name="input">An integer input value representing the ID of the <see cref="Assignment"/> entity to be viewed.</param>
        private static void ViewAssignment(int input)
        {
            Console.WriteLine(s_dal!.Assignment.Read(input));
        }
        /// <summary>
        /// A generic method that deletes an entity (Volunteer, Call, or Assignment) from the data store based on the type parameter.
        /// </summary>
        /// <typeparam name="T">The type of entity to be deleted (either <see cref="Volunteer"/>, <see cref="Call"/>, or <see cref="Assignment"/>).</typeparam>
        /// <param name="input">The ID of the entity to be deleted.</param>
        private static void DeleteEntity<T>(int input) where T : class
        {
            if (typeof(T)?.Equals(typeof(Volunteer)) ?? false)
            {
                s_dal?.Volunteer.Delete(input);
            }
            else if (typeof(T)?.Equals(typeof(Call)) ?? false)
            {
                s_dal?.Call.Delete(input);
            }
            else
            {
                s_dal?.Volunteer.Delete(input);
            }
        }
        /// <summary>
        /// A generic method that deletes all entities of a specified type (Volunteer, Call, or Assignment) from the data store.
        /// </summary>
        /// <typeparam name="T">The type of entities to be deleted (either <see cref="Volunteer"/>, <see cref="Call"/>, or <see cref="Assignment"/>).</typeparam>
        private static void DeleteEntitys<T>() where T : class
        {
            if (typeof(T)?.Equals(typeof(Volunteer)) ?? false)
            {
                s_dal?.Volunteer.DeleteAll();
            }
            else if (typeof(T)?.Equals(typeof(Call)) ?? false)
            {
                s_dal?.Volunteer.DeleteAll();
            }
            else
            {
                s_dal?.Assignment.DeleteAll();
            }
        }
        /// <summary>
        /// Prompts the user to enter an ID value and returns the input as an integer.
        /// </summary>
        /// <returns>
        /// An integer representing the ID entered by the user.
        /// </returns>
        private static int GetInput()
        {
            Console.WriteLine("Please enter id:");
            int input = int.Parse(Console.ReadLine());
            return input;
        }
        /// <summary>
        /// Displays the options available in the sub-menu to the user, including actions like adding, viewing, updating, and deleting entities.
        /// </summary>
        private static void PrintForSubMenues()
        {
            Console.WriteLine(
                "Press 0 to exit the sub-menu." +
                "\nPress 1 to add a new entity object to the list." +
                "\nPress 2 to view an entity object by its ID." +
                "\nPress 3 to view all entity objects." +
                "\nPress 4 to update an existing entity object." +
                "\nPress 5 to delete an entity object." +
                "\nPress 6 to delete all entity objects." +
                "\nPress 7 to catch and handle any exceptions thrown during these operations."
            );

        }
        /// <summary>
        /// Displays the sub-menu for managing Volunteer entities and handles user input to perform different actions (add, view, update, delete, etc.).
        /// </summary>
        private static void SubMenuVolunteer()
        {
            PrintForSubMenues();
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {

                case 0: Environment.Exit(0); break;
                case 1: AddEntity<Volunteer>(GetInput()); break;
                case 2: ViewEntity<Volunteer>(GetInput()); break;
                case 3: ViewEntitys<Volunteer>(); break;
                case 4: UpdateEntity<Volunteer>(GetInput()); break;
                case 5: DeleteEntity<Volunteer>(GetInput()); break;
                case 6: DeleteEntitys<Volunteer>(); break;
            }
        }
        /// <summary>
        /// Displays the sub-menu for managing Call entities and handles user input to perform different actions (add, view, update, delete, etc.).
        /// </summary>
        private static void SubMenuCall()
        {
            PrintForSubMenues();
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {

                case 0: Environment.Exit(0); break;
                case 1: AddEntity<Call>(GetInput()); break;
                case 2: ViewEntity<Call>(GetInput()); break;
                case 3: ViewEntitys<Call>(); break;
                case 4: UpdateEntity<Call>(GetInput()); break;
                case 5: DeleteEntity<Call>(GetInput()); break;
                case 6: DeleteEntitys<Call>(); break;
            }
        }
        /// <summary>
        /// Displays the sub-menu for managing Assignment entities and handles user input to perform different actions (add, view, update, delete, etc.).
        /// </summary>
        private static void SubMenuAssignment()
        {
            PrintForSubMenues();
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {

                case 0: Environment.Exit(0); break;
                case 1: AddEntity<Assignment>(GetInput()); break;
                case 2: ViewEntity<Assignment>(GetInput()); break;
                case 3: ViewEntitys<Assignment>(); break;
                case 4: UpdateEntity<Assignment>(GetInput()); break;
                case 5: DeleteEntity<Assignment>(GetInput()); break;
                case 6: DeleteEntitys<Assignment>(); break;
            }
        }
        /// <summary>
        /// Displays the configuration sub-menu and handles user input to perform configuration-related tasks like adjusting the system clock, displaying current settings, and resetting configuration variables.
        /// </summary>
        private static void SubMenuConfig()
        {
            Console.WriteLine(
                "Press 0 to exit the sub-menu." +
                "\nPress 1 to advance the system clock by one minute." +
                "\nPress 2 to advance the system clock by one hour." +
                "\nPress 3 to advance the system clock by a custom value (e.g., minutes or hours)." +
                "\nPress 4 to display the current system clock value." +
                "\nPress 6 to display the current risk range" +
                "\nPress 7 to reset all configuration variables to their default values."
            );

            int input = int.Parse(Console.ReadLine());
            try
            {
                switch (input)
                {

                    case 0: Environment.Exit(0); break;
                    case 1: s_dal!.Config.Clock.AddMinutes(1); break;
                    case 2: s_dal!.Config.Clock.AddHours(1); break;
                    case 3:
                        Console.WriteLine("Enter the number of minutes to advance the system clock:");
                        int minutes = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the number of hours to advance the system clock:");
                        int hours = int.Parse(Console.ReadLine());
                        s_dal!.Config.Clock = s_dal!.Config.Clock.AddMinutes(minutes).AddHours(hours); break;
                    case 4: Console.WriteLine(s_dal!.Config.Clock); break;
                    case 5: Console.WriteLine(s_dal!.Config.RiskRange); break;
                    case 6: s_dal!.Config.Reset(); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// Prints all the lists from DataSourse.
        /// </summary>
        private static void PrintAllLists()
        {
            Console.WriteLine("Volunteers:");
            foreach (var volunteer in s_dal!.Volunteer.ReadAll())
            {
                Console.WriteLine(volunteer);
            }
            Console.WriteLine("\nCalls:");
            foreach (var call in s_dal!.Call.ReadAll())
            {
                Console.WriteLine(call);
            }
            Console.WriteLine("\nAssignments:");
            foreach (var assignment in s_dal!.Assignment.ReadAll())
            {
                Console.WriteLine(assignment);
            }
        }
        /// <summary>
        /// Displays the main menu to the user, allowing them to navigate to sub-menus for managing different entities or perform actions like initializing data, displaying data, or resetting the database.
        /// </summary>
        private static void MainMenu()
        {
            int input = 1;
            while (input != 0)
            {
                Console.WriteLine("Press 0 to exit the main menu." +
                 "\nPress 1 to show the sub - menu of Entity 1." +
                 "\nPress 2 to show the sub - menu of Entity 2." +
                 "\nPress 3 to show the sub - menu of Entity 3." +
                 "\nPress 4 to initialize the data(call Initialization.Do method)." +
                 "\nPress 5 to display all data in the database(print data lists for all entities)." +
                 "\nPress 6 to show the sub-menu for the Configuration Entity." +
                 "\nPress 7 to reset the database and configuration data."
                 );

                string inputString = Console.ReadLine();
                input = int.Parse(inputString);

                switch (input)
                {
                    case 0: Environment.Exit(0); break;
                    case 1: SubMenuVolunteer(); break;
                    case 2: SubMenuCall(); break;
                    case 3: SubMenuAssignment(); break;
                    case 4: Initialization.Do(); break;
                    case 5: PrintAllLists(); break;
                    case 6: SubMenuConfig(); break;
                    case 7: s_dal!.Volunteer.DeleteAll(); s_dal!.Call.DeleteAll(); s_dal!.Assignment.DeleteAll(); break;
                }
            }
        }
        /// <summary>
        /// The main entry point for the program. Initializes the data store and displays the main menu for user interaction.
        /// </summary>
        /// <param name="args">The command-line arguments passed to the program (not used in this case).</param>
        static void Main(string[] args)
        {
            try
            {
                Initialization.Do();
                MainMenu();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex);
                
            }
        }
    }
}
