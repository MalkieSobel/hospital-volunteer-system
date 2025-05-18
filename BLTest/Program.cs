namespace BLTest
{
    internal class Program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        /// <summary>
        /// A method that prompts the user for all the details of a volunteer and returns a new Volunteer object.
        /// </summary>
        /// <returns>
        /// A<see cref = "Volunteer" /> object containing the details of the volunteer entered by the user.
        /// </returns>
        private static BO.Volunteer GetDetailsOfVolunteer()
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
            string Password = Console.ReadLine();
            Console.WriteLine("Volunteer's Adress:");
            string Adress = Console.ReadLine();
            Console.WriteLine("Adress's latitude:");
            double Latitude = double.Parse(Console.ReadLine());
            Console.WriteLine("Adress's longitude:");
            double Longitude = double.Parse(Console.ReadLine());
            Console.WriteLine("Volunteer's role (manager? press 0. Volunteer? press 1.) :");
            int tmpInput = int.Parse(Console.ReadLine());
            BO.Role role = (BO.Role)tmpInput;
            Console.WriteLine("Active? press 1. not active - press 0. :");
            tmpInput = int.Parse(Console.ReadLine());
            System.Boolean IsActive = (tmpInput == 1) ? true : false;
            Console.WriteLine("Max distance you can arrive:");
            double MaxDistance = double.Parse(Console.ReadLine());
            Console.WriteLine("Distance's type (Aerial-press 0, Walking-press 1, Driving-press 2) :");
            tmpInput = int.Parse(Console.ReadLine());
            BO.DistanceType distanceType = (BO.DistanceType)tmpInput;

            BO.CallInProgress tmp = new BO.CallInProgress();
            BO.Volunteer newValunteer = new BO.Volunteer(VolunteerId, FullName, PhoneNum, Email, Password, Adress, Latitude, Longitude, role, IsActive, MaxDistance, null, null, null, null, tmp);
            return newValunteer;
        }
        /// <summary>
        /// A method that prompts the user for all the details of a call and returns a new Call object.
        /// </summary>
        /// <returns>
        /// A<see cref = "Call" /> object containing the details of the call entered by the user.
        /// </returns>
        private static BO.Call GetDetailsOfCall()
        {
            Console.WriteLine("Enter all detailes: \n" + "Call's id:");
            int CallId = int.Parse(Console.ReadLine());
            Console.WriteLine("Call's type (NurseHelp-press 1, SongCheer-press 2, KidsActivity-press 3, CoockFood-press 4, Delivery-press 5) :");
            int tmpInput = int.Parse(Console.ReadLine());
            BO.CallType callType = (BO.CallType)tmpInput;
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

            BO.Call newCall = new BO.Call(0, callType, VerbalDescription, Adress, Latitude, Longitude, CallOpenTime, MaxEndTime, 0, null);
            return newCall;
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

        //Sub Menu Admin
        private static void SubMenuAdmin()
        {
            Console.WriteLine(
                "Press 0 to exit the sub-menu." +
                "\nPress 1 to get the system clock." +
                "\nPress 2 to promote the system clock." +
                "\nPress 3 to get the risk range." +
                "\nPress 4 to set the risk range." +
                "\nPress 5 to reset the database." +
                "\nPress 6 to initialize the database."
            );

            int input = int.Parse(Console.ReadLine());
            try
            {
                switch (input)
                {
                    case 0: MainMenu(); break;
                    case 1: GetSystemClock(); break;
                    case 2: PromoteSystemClock(); break;
                    case 3: GetRiskRange(); break;
                    case 4: SetRiskRange(); break;
                    case 5: ResetDB(); break;
                    case 6: InitDB(); break;
                    default: Console.WriteLine("Invalid choice. Please try again."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void GetSystemClock()
        {
            Console.WriteLine(s_bl!.Admin.GetSystemClock());
        }
        private static void PromoteSystemClock()
        {
            Console.WriteLine("Enter time unit (e.g., Hours, Days, etc.):");
            BO.TimeUnit timeUnit = (BO.TimeUnit)Enum.Parse(typeof(BO.TimeUnit), Console.ReadLine() ?? "");
            s_bl!.Admin.ClockPromotion(timeUnit);
        }
        private static void GetRiskRange()
        {
            Console.WriteLine(s_bl!.Admin.GetRiskRange());
        }
        private static void SetRiskRange()
        {
            Console.WriteLine("Enter risk range in hours:");
            TimeSpan riskRange = TimeSpan.FromHours(double.Parse(Console.ReadLine()));
            s_bl!.Admin.SetRiskRange(riskRange);
        }
        private static void ResetDB()
        {
            s_bl!.Admin.ResetDB();
            Console.WriteLine("Database has been reset.");
        }
        private static void InitDB()
        {
            s_bl!.Admin.InitDB();
            Console.WriteLine("Database has been initialized.");
        }

        //Sub Menu Volunteer
        private static void SubMenuVolunteer()
        {
            Console.WriteLine(
                "Press 0 to exit the sub-menu." +
                "\nPress 1 to get the role of a volunteer." +
                "\nPress 2 to get a list of volunteers." +
                "\nPress 3 to get details of a specific volunteer." +
                "\nPress 4 to update volunteer details." +
                "\nPress 5 to delete a volunteer." +
                "\nPress 6 to add a new volunteer."
            );

            int input = int.Parse(Console.ReadLine());
            try
            {
                switch (input)
                {
                    case 0: MainMenu(); break;
                    case 1: GetVolunteerRole(); break;
                    case 2: GetVolunteersList(); break;
                    case 3: GetVolunteerDetails(); break;
                    case 4: UpdateVolunteerDetails(); break;
                    case 5: DeleteVolunteer(); break;
                    case 6: AddVolunteer(); break;
                    default: Console.WriteLine("Invalid choice. Please try again."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void GetVolunteerRole()
        {
            Console.WriteLine("Enter username:");
            string userName = Console.ReadLine()!;
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine()!;
            Console.WriteLine(s_bl!.Volunteer.GetRole(userName, password));
        }
        private static void GetVolunteersList()
        {
            Console.WriteLine("Filter by active status (true/false/null):");
            bool? isActive = bool.TryParse(Console.ReadLine(), out var active) ? active : (bool?)null;
            Console.WriteLine("Enter sort field (or null):");
            var sortBy = (BO.VolunteerSortField?)Enum.Parse(typeof(BO.VolunteerSortField), Console.ReadLine() ?? "");
            Console.WriteLine(string.Join("\n", s_bl!.Volunteer.GetVolunteersList(isActive, sortBy)));
        }
        private static void GetVolunteerDetails()
        {
            Console.WriteLine("Enter volunteer ID:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(s_bl!.Volunteer.GetVolunteerDetails(id));
        }
        private static void UpdateVolunteerDetails()
        {
            Console.WriteLine("Enter volunteer ID:");
            int userId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter volunteer details to update:");
            BO.Volunteer volunteer = new BO.Volunteer(); // Add volunteer details input logic
            s_bl!.Volunteer.UpdateVolunteerDetails(userId, volunteer);
        }
        private static void DeleteVolunteer()
        {
            Console.WriteLine("Enter volunteer ID to delete:");
            int deleteId = int.Parse(Console.ReadLine());
            s_bl!.Volunteer.DeleteVolunteer(deleteId);
        }
        private static void AddVolunteer()
        {
            Console.WriteLine("Enter details for new volunteer:");
            BO.Volunteer newVolunteer = new BO.Volunteer(GetDetailsOfVolunteer());
            s_bl!.Volunteer.AddVolunteer(newVolunteer);
        }

        //Sub Menu Call
        private static void SubMenuCall()
        {
            Console.WriteLine(
                "Press 0 to exit the sub-menu." +
                "\nPress 1 to request quantities of calls." +
                "\nPress 2 to update expired open calls." +
                "\nPress 3 to request a list of calls." +
                "\nPress 4 to request details of a specific call." +
                "\nPress 5 to update the details of a call." +
                "\nPress 6 to delete a call." +
                "\nPress 7 to add a new call." +
                "\nPress 8 to list closed calls by a volunteer." +
                "\nPress 9 to list open calls by a volunteer." +
                "\nPress 10 to update the status of a call as 'treatment completed'." +
                "\nPress 11 to update the status of a call as 'treatment canceled'." +
                "\nPress 12 to assign a call to a volunteer."
            );

            int input = int.Parse(Console.ReadLine());
            try
            {
                switch (input)
                {
                    case 0: MainMenu(); break;
                    case 1: RequestQuantities(); break;
                     case 2: RequestCallList(); break;
                    case 3: RequestCallDetails(); break;
                    case 4: UpdateCallDetails(); break;
                    case 5: DeleteCall(); break;
                    case 6: AddNewCall(); break;
                    case 7: ListClosedCallsByVolunteer(); break;
                    case 8: ListOpenCallsByVolunteer(); break;
                    case 9: UpdateCallStatusCompleted(); break;
                    case 10: UpdateCallStatusCanceled(); break;
                    case 11: AssignCallToVolunteer(); break;
                    default: Console.WriteLine("Invalid choice. Please try again."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void RequestQuantities() => Console.WriteLine(string.Join(", ", s_bl!.Call.GetCallCounts()));        
        private static void RequestCallList()
        {
            Console.WriteLine("Enter filter field (or null):");
            var filterBy = (BO.CallSortField?)Enum.Parse(typeof(BO.CallSortField), Console.ReadLine() ?? "");
            Console.WriteLine("Enter value to filter by (or null):");
            object? value = Console.ReadLine();
            Console.WriteLine("Enter sort field (or null):");
            var sortBy = (BO.CallSortField?)Enum.Parse(typeof(BO.CallSortField), Console.ReadLine() ?? "");
            Console.WriteLine(string.Join("\n", s_bl!.Call.RequestACallList(filterBy, value, sortBy)));
        }
        private static void RequestCallDetails()
        {
            Console.WriteLine("Enter call ID:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(s_bl!.Call.ReadingDetailsRequest(id));
        }
        private static void UpdateCallDetails()
        {
            Console.WriteLine("Enter call details to update:");
            BO.Call callToUpdate = new BO.Call(); // Add call details input logic
            s_bl!.Call.UpdateDetailsRequest(callToUpdate);
        }
        private static void DeleteCall()
        {
            Console.WriteLine("Enter call ID to delete:");
            int deleteId = int.Parse(Console.ReadLine());
            s_bl!.Call.DeleteCallRequest(deleteId);
        }
        private static void AddNewCall()
        {
            Console.WriteLine("Enter details for new call:");
            BO.Call newCall = new BO.Call(GetDetailsOfCall());
            s_bl!.Call.AddCall(newCall);
        }
        private static void ListClosedCallsByVolunteer()
        {
            Console.WriteLine("Enter volunteer ID:");
            int volIdClosed = int.Parse(Console.ReadLine());
            Console.WriteLine(string.Join("\n", s_bl!.Call.ListCloseCallsByVolunteer(volIdClosed, null, null)));
        }
        private static void ListOpenCallsByVolunteer()
        {
            Console.WriteLine("Enter volunteer ID:");
            int volIdOpen = int.Parse(Console.ReadLine());
            Console.WriteLine(string.Join("\n", s_bl!.Call.ListOpenCallsByVolunteer(volIdOpen, null, null)));
        }
        private static void UpdateCallStatusCompleted()
        {
            Console.WriteLine("Enter volunteer ID:");
            int volIdComplete = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter call ID:");
            int callIdComplete = int.Parse(Console.ReadLine());
            s_bl!.Call.UpdateEndCall(volIdComplete, callIdComplete);
        }
        private static void UpdateCallStatusCanceled()
        {
            Console.WriteLine("Enter volunteer ID:");
            int volIdCancel = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter call ID:");
            int callIdCancel = int.Parse(Console.ReadLine());
            s_bl!.Call.UpdateCancelCall(volIdCancel, callIdCancel);
        }
        private static void AssignCallToVolunteer()
        {
            Console.WriteLine("Enter volunteer ID:");
            int volIdAssign = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter call ID:");
            int callIdAssign = int.Parse(Console.ReadLine());
            s_bl!.Call.ChooseCallForTreatment(volIdAssign, callIdAssign);
        }

        /// <summary>
        /// Displays the main menu to the user, allowing them to navigate to sub-menus for managing different entities or perform actions like initializing data,
        /// displaying data, or resetting the database.
        /// </summary>
        private static void MainMenu()
        {
            int input = 1;
            while (input != 0)
            {
                Console.WriteLine("Press 0 to exit the main menu." +
                 "\nPress 1 to show the sub - menu of Volunteer" +
                 "\nPress 2 to show the sub - menu of Call " +
                 "\nPress 3 to show the sub - menu of Admin "
                );

                string inputString = Console.ReadLine();
                input = int.Parse(inputString);

                switch (input)
                {
                    case 0: Environment.Exit(0); break;
                    case 1: SubMenuVolunteer(); break;
                    case 2: SubMenuCall(); break;
                    case 3: SubMenuAdmin(); break;
                }
            }
        }
        static void Main(string[] args)
        {           
            try
            {    
                s_bl.Admin.InitDB();
                MainMenu();
            }
            catch (BO.BlDoesNotExistException ex)
            {
                Console.WriteLine(ex);
            }            
        }
    }
}
