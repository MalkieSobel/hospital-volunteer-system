namespace DalTest
{
    using Dal;
    using DalApi;
    using DO;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// A static class responsible for initializing the system data, such as volunteers, calls, and assignments.
    /// </summary>
    public static class Initialization
    {
        private static IDal? s_dal;  
        private static List<Volunteer>? volunteersVec; 
        private static readonly Random s_rand = new(); 
        /// <summary>
        /// Creates volunteer data and adds them to the DAL (Data Access Layer).
        /// </summary>
        private static void createVolunteers()
        {
            string[] volunteerNames =
        {
            "Moshe Cohen",
            "Yitzhak Levy",
            "Aryeh Fried",
            "Shimon Baruch",
            "Chaim Goldstein",
            "Sarah Levi",
            "Miriam Weiss",
            "Rachel Schwartz",
            "Esther Friedman",
            "Rivka Shapiro",
            "David Rosen",
            "Yaakov Katz",
            "Menachem Stern",
            "Binyamin Segal",
            "Aharon Gold"
        };
            string[] volunteerEmails =
            {
            "moshe.cohen@mail.com",
            "yitzhak.levy@mail.com",
            "aryeh.fried@mail.com",
            "shimon.baruch@mail.com",
            "chaim.goldstein@mail.com",
            "sarah.levi@mail.com",
            "miriam.weiss@mail.com",
            "rachel.schwartz@mail.com",
            "esther.friedman@mail.com",
            "rivka.shapiro@mail.com",
            "david.rosen@mail.com",
            "yaakov.katz@mail.com",
            "menachem.stern@mail.com",
            "binyamin.segal@mail.com",
            "aharon.gold@mail.com"
        };
            string[] volunteerPhoneNumbers =
                   {
            "052-6543210",
            "053-8123456",
            "054-9712365",
            "050-2365478",
            "052-3498765",
            "053-4980321",
            "054-5678901",
            "050-6457893",
            "052-8765432",
            "053-1239876",
            "054-2301798",
            "050-3456789",
            "052-5312478",
            "053-9823401",
            "054-4678123"
        };
            string[] volunteerAddresses =
            {
            "Rehov HaRav Kook 12, Jerusalem",
            "Rehov Sha'arei Chessed 5, Jerusalem",
            "Rehov Zichron Moshe 8, Jerusalem",
            "Rehov Rabbi Akiva 19, Jerusalem",
            "Rehov Rav Herzog 25, Jerusalem",
            "Rehov HaRav Shmuel 3, Jerusalem",
            "Rehov Nechama 14, Jerusalem",
            "Rehov Yosef 21, Jerusalem",
            "Rehov HaRav Shmuel 8, Jerusalem",
            "Rehov Betzalel 4, Jerusalem",
            "Rehov HaRav Shlomo 11, Jerusalem",
            "Rehov Dolev 22, Jerusalem",
            "Rehov Rav Avraham 9, Jerusalem",
            "Rehov Avraham Avinu 16, Jerusalem",
            "Rehov Rav Yitzhak 7, Jerusalem"
        };
            string[] volunteerPasswords =
            {
            "Moshe@2024Cohen",
            "Yitzhak!LevY78",
            "Aryeh_Fried123$",
            "Shimon@Baruch456",
            "Chaim_Goldstein2024#",
            "Sarah!Levi89",
            "Miriam_Weiss!2023",
            "Rachel$Schwartz11",
            "Esther@Friedman99",
            "RivkaSh@piro21",
            "David_Rosen@2024",
            "Yaakov_Katz789#",
            "MenachemStern@22",
            "Binyamin@Segal2023",
            "AharonGold!2024"
        };
            double[] latitudeCoordinates =
            {
            31.7721,
            31.7867,
            31.7739,
            31.7760,
            31.7633,
            31.7852,
            31.7582,
            31.7634,
            31.7911,
            31.7778,
            31.7735,
            31.7504,
            31.7627,
            31.7799,
            31.7724
        };
            double[] longitudeCoordinates =
                    {
            35.2152,
            35.2131,
            35.2113,
            35.2137,
            35.2028,
            35.2006,
            35.2215,
            35.2060,
            35.2153,
            35.2110,
            35.2086,
            35.2068,
            35.2130,
            35.2116,
            35.2150
        };

            int i = 0;
            foreach (var name in volunteerNames)
            {
                int VolunteerId;
                do
                    VolunteerId = s_rand.Next(200000000, 800000000);
                //בודק אם קיים כבר VOLUNTEER עם הID שנשלח
                while (s_dal!.Volunteer.Read(VolunteerId) != null); 

                Volunteer newValunteer = new Volunteer(VolunteerId, volunteerNames[i], volunteerPhoneNumbers[i],
                    volunteerEmails[i], volunteerPasswords[i], volunteerAddresses[i], latitudeCoordinates[i], longitudeCoordinates[i],
                    Role.Volunteer, s_rand.Next(2) == 0, s_rand.Next(4000, 12500));

                s_dal!.Volunteer.Create(newValunteer);
                i++;
            }
        }
        /// <summary>
        /// Creates call requests for help and adds them to the DAL.
        /// </summary>
        private static void createCalls()
        {
            string[] NurseHelp = {
          "Assist in transferring a patient to another department.",
          "Help in preparing medical equipment for a patient before treatment.",
          "Assist the nurse in preparing the room for a new patient.",
          "Help in transferring medications to a patient.",
          "Support in transferring a patient after surgery.",
          "Assist in preparing the room for surgery.",
          "Help in preparing a patient for a medical scan.",
          "Assist in transferring medical equipment between rooms.",
          "Support in transferring equipment to a patient in the ICU.",
          "Assist in maintaining a sterile environment in the room.",
          "Support the nurse in preparing a patient for chemotherapy.",
          "Help in preparing a room for a new patient.",
          "Assist in updating medical records.",
          "Support the nurse in preparing for a meeting with the doctor.",
          "Help in transferring a patient from bed to wheelchair.",
          "Support the nurse in managing equipment in the ward.",
          "Assist in preparing a patient for an upcoming surgery.",
          "Support in transferring medical information between doctors' teams.",
          "Help in preparing a patient before medical treatments.",
          "Assist in organizing the room after a medical procedure."
      };
            string[] SongCheer = {
          "Sing to a patient in isolation to lift their spirits.",
          "Encourage and sing for children in the pediatric ward.",
          "Sing and encourage a patient undergoing surgery.",
          "Sing calming songs for patients who have trouble sleeping.",
          "Group singing for patients in the ICU.",
          "Sing to children in isolation to cheer them up.",
          "Singing and offering emotional support for hospitalized children.",
          "Sing softly to a patient after chemotherapy.",
          "Encourage and sing for patients recovering from surgery.",
          "Group singing with patients to improve their mood.",
          "Sing for patients waiting for medical treatment.",
          "Sing for sick children to help ease their time in the ward.",
          "Encourage and sing for a group of patients after chemotherapy.",
          "Sing with children while playing board games.",
          "Sing for sick children who have undergone painful medical procedures.",
          "Sing and engage in calming activities with sick children.",
          "Encourage and sing for a group of patients during a tough day.",
          "Sing energetic songs in waiting rooms to create a lively atmosphere.",
          "Encourage with songs and kind words for children.",
          "Sing for children with special needs in the ward."
      };
            string[] KidsActivity = {
          "Conduct creative workshops for hospitalized children.",
          "Organize educational activities for children in the pediatric ward.",
          "Set up simple sports activities for children in bed.",
          "Host recycling-based craft workshops for children.",
          "Organize coloring and drawing activities with sick children.",
          "Educational activities and board games for children.",
          "Conduct creative workshops for children with emotional challenges.",
          "Organize group games and races in the children's area.",
          "Create a shared art exhibit by the children.",
          "Host storytelling and craft workshops with sick children.",
          "Dynamic activities for children during extended hospital stays.",
          "Organize memory games and thinking exercises for children.",
          "Create a puppet theater with children in the pediatric ward.",
          "Organize activities with children based on songs.",
          "Hold simple craft workshops for children in isolation.",
          "Conduct quizzes and educational activities with children.",
          "Help children with schoolwork during long-term hospitalization.",
          "Organize group creative workshops with children.",
          "Host sports activities with children.",
          "Create engaging activities for sick children in sensitive situations."
      };
            string[] CoockFood = {
          "Prepare meals for children in the pediatric ward.",
          "Cook healthy meals for patients after surgery.",
          "Prepare light meals for patients who have trouble eating.",
          "Cook snacks for sick children in the ward.",
          "Prepare fresh salads for patients on a healthy diet.",
          "Arrange snacks and drinks for patients in the ICU.",
          "Cook meals for children with food allergies.",
          "Prepare light meals for patients with stomach issues.",
          "Cook food for patients with specific dietary needs.",
          "Prepare meals for patients who need a low-calorie diet.",
          "Prepare meals for patients in isolation.",
          "Cook doughnuts for patients during Shabbat.",
          "Prepare desserts for patients in the wards.",
          "Cook meals for hospital staff and families of patients.",
          "Prepare light meals for patients at risk of digestive problems.",
          "Arrange breakfast for patients in the clinics.",
          "Prepare stews for patients who have trouble eating hot foods.",
          "Prepare a daily menu for hospital staff.",
          "Cook hot meals for patients in long-term hospitalization.",
          "Prepare food for children after painful medical treatment."
      };
            string[] Delivery = {
          "Distribute medications between patient rooms.",
          "Deliver medical equipment to the healthcare staff.",
          "Transfer reading materials for patients waiting for treatment.",
          "Distribute creative workshops for sick children.",
          "Deliver personal items to patients in the pediatric ward.",
          "Distribute snacks to patients waiting for their turn.",
          "Transfer personal mail to patients in various departments.",
          "Deliver medical equipment to patients who need it.",
          "Transfer support equipment to the physiotherapy department.",
          "Distribute books and newspapers to patients in various departments.",
          "Deliver medical supplies to the hospital.",
          "Distribute new medical devices to patients.",
          "Distribute hot meals to patients in their rooms.",
          "Transfer physiotherapy equipment to patients.",
          "Distribute medications to patients in the ICU.",
          "Transfer medical equipment to the medical staff in the ward.",
          "Deliver personal mail and ceramics to patients.",
          "Distribute support supplies to infants and sick children.",
          "Transfer medical packages between department rooms.",
          "Deliver cleaning equipment to hospital departments."
      };
            string[] hospitalsInJerusalem =
            {
         "Hadassah Medical Center - Ein Kerem",
         "Shaare Zedek Medical Center",
         "Hadassah Medical Center - Mount Scopus",
        };
            double[] latitudeCoordinates = { 31.7490, 31.7428, 31.7764 };
            double[] longitudeCoordinates = { 35.1395, 35.2130, 35.2324 };
            DateTime[] callStart =
           {
            new DateTime(2024, 5, 13, 8, 22, 35),
            new DateTime(2024, 3, 25, 17, 46, 59),
            new DateTime(2023, 12, 5, 11, 34, 22),
            new DateTime(2023, 10, 18, 19, 59, 10),
            new DateTime(2024, 2, 7, 14, 8, 30),
            new DateTime(2024, 1, 12, 9, 14, 49),
            new DateTime(2024, 4, 22, 21, 35, 11),
            new DateTime(2023, 11, 20, 3, 40, 58),
            new DateTime(2024, 6, 2, 6, 29, 16),
            new DateTime(2024, 7, 28, 16, 50, 7),
            new DateTime(2023, 9, 3, 23, 3, 2),
            new DateTime(2024, 5, 19, 12, 11, 45),
            new DateTime(2023, 12, 30, 8, 2, 17),
            new DateTime(2023, 11, 11, 20, 19, 26),
            new DateTime(2024, 4, 17, 5, 55, 11),
            new DateTime(2023, 10, 28, 15, 45, 39),
            new DateTime(2024, 3, 3, 0, 5, 13),
            new DateTime(2023, 12, 25, 18, 32, 40),
            new DateTime(2024, 2, 15, 9, 40, 2),
            new DateTime(2024, 1, 30, 14, 52, 36),
            new DateTime(2024, 5, 6, 19, 19, 28),
            new DateTime(2023, 10, 11, 8, 11, 3),
            new DateTime(2024, 6, 23, 2, 44, 15),
            new DateTime(2024, 7, 14, 7, 12, 39),
            new DateTime(2023, 9, 8, 21, 50, 55),
            new DateTime(2023, 12, 14, 4, 27, 18),
            new DateTime(2024, 3, 1, 11, 59, 9),
            new DateTime(2024, 6, 19, 16, 5, 57),
            new DateTime(2024, 1, 10, 12, 1, 4),
            new DateTime(2024, 4, 5, 22, 32, 50),
            new DateTime(2024, 2, 3, 5, 10, 33),
            new DateTime(2023, 11, 17, 13, 21, 7),
            new DateTime(2023, 10, 25, 20, 8, 40),
            new DateTime(2023, 8, 13, 7, 59, 45),
            new DateTime(2024, 5, 23, 0, 13, 2),
            new DateTime(2023, 9, 22, 18, 30, 29),
            new DateTime(2024, 1, 15, 4, 10, 52),
            new DateTime(2024, 2, 28, 22, 4, 9),
            new DateTime(2023, 12, 22, 9, 28, 11),
            new DateTime(2024, 3, 17, 0, 37, 48),
            new DateTime(2023, 11, 9, 12, 25, 5),
            new DateTime(2024, 6, 11, 13, 7, 19),
            new DateTime(2024, 7, 2, 21, 45, 55),
            new DateTime(2023, 8, 20, 5, 38, 27),
            new DateTime(2024, 2, 9, 17, 10, 41),
            new DateTime(2023, 12, 18, 2, 5, 4),
            new DateTime(2024, 1, 22, 16, 24, 10),
            new DateTime(2024, 4, 12, 14, 18, 26),
            new DateTime(2023, 9, 14, 23, 56, 33),
            new DateTime(2024, 7, 8, 10, 43, 50),
            new DateTime(2023, 12, 2, 0, 13, 18),
            new DateTime(2024, 5, 2, 9, 5, 1)
        };
            DateTime[] callEnd =
           {
            new DateTime(2024, 5, 13, 10, 22, 35),
            new DateTime(2024, 3, 25, 22, 46, 59),
            new DateTime(2023, 12, 5, 16, 34, 22),
            new DateTime(2023, 10, 18, 23, 59, 10),
            new DateTime(2024, 2, 7, 16, 8, 30),
            new DateTime(2024, 1, 12, 14, 14, 49),
            new DateTime(2024, 4, 22, 23, 35, 11),
            new DateTime(2023, 11, 20, 5, 40, 58),
            new DateTime(2024, 6, 2, 8, 29, 16),
            new DateTime(2024, 7, 28, 20, 50, 7),
            new DateTime(2023, 9, 3, 2, 3, 2),
            new DateTime(2024, 5, 19, 14, 11, 45),
            new DateTime(2023, 12, 30, 11, 2, 17),
            new DateTime(2023, 11, 11, 22, 19, 26),
            new DateTime(2024, 4, 17, 7, 55, 11),
            new DateTime(2023, 10, 28, 20, 45, 39),
            new DateTime(2024, 3, 3, 5, 5, 13),
            new DateTime(2023, 12, 25, 21, 32, 40),
            new DateTime(2024, 2, 15, 13, 40, 2),
            new DateTime(2024, 1, 30, 18, 52, 36),
            new DateTime(2024, 5, 6, 22, 19, 28),
            new DateTime(2023, 10, 11, 10, 11, 3),
            new DateTime(2024, 6, 23, 4, 44, 15),
            new DateTime(2024, 7, 14, 12, 12, 39),
            new DateTime(2023, 9, 8, 3, 50, 55),
            new DateTime(2023, 12, 14, 7, 27, 18),
            new DateTime(2024, 3, 1, 13, 59, 9),
            new DateTime(2024, 6, 19, 20, 5, 57),
            new DateTime(2024, 1, 10, 18, 1, 4),
            new DateTime(2024, 4, 5, 3, 32, 50),
            new DateTime(2024, 2, 3, 9, 10, 33),
            new DateTime(2023, 11, 17, 16, 21, 7),
            new DateTime(2023, 10, 25, 22, 8, 40),
            new DateTime(2023, 8, 13, 9, 59, 45),
            new DateTime(2024, 5, 23, 2, 13, 2),
            new DateTime(2023, 9, 22, 20, 30, 29),
            new DateTime(2024, 1, 15, 6, 10, 52),
            new DateTime(2024, 2, 28, 3, 4, 9),
            new DateTime(2023, 12, 22, 11, 28, 11),
            new DateTime(2024, 3, 17, 5, 37, 48),
            new DateTime(2023, 11, 9, 14, 25, 5),
            new DateTime(2024, 6, 11, 16, 7, 19),
            new DateTime(2024, 7, 2, 23, 45, 55),
            new DateTime(2023, 8, 20, 7, 38, 27),
            new DateTime(2024, 2, 9, 21, 10, 41),
            new DateTime(2023, 12, 18, 6, 5, 4),
            new DateTime(2024, 1, 22, 18, 24, 10),
            new DateTime(2024, 4, 12, 20, 18, 26),
            new DateTime(2023, 9, 14, 1, 56, 33),
            new DateTime(2024, 7, 8, 16, 43, 50),
            new DateTime(2023, 12, 2, 6, 13, 18),
            new DateTime(2024, 5, 2, 12, 5, 1)
        };

            int i = 0;
            foreach (DateTime call in callStart)
            {
                CallType callType = (CallType)s_rand.Next(1, 5);
                string VerbalDescription = "";
                switch (callType)
                {
                    case (CallType)1: VerbalDescription = NurseHelp[s_rand.Next(0, 20)]; break;
                    case (CallType)2: VerbalDescription = SongCheer[s_rand.Next(0, 20)]; break;
                    case (CallType)3: VerbalDescription = KidsActivity[s_rand.Next(0, 20)]; break;
                    case (CallType)4: VerbalDescription = CoockFood[s_rand.Next(0, 20)]; break;
                    case (CallType)5: VerbalDescription = Delivery[s_rand.Next(0, 20)]; break;
                }

                int temp = s_rand.Next(0, 2);
                Call newCall = new Call(0, callType, VerbalDescription, hospitalsInJerusalem[temp], latitudeCoordinates[temp], longitudeCoordinates[temp], callStart[i], callEnd[i]);
                s_dal!.Call.Create(newCall);
                i++;
            }
        }
        /// <summary>
        /// Creates volunteer assignments based on calls and random treatment data.
        /// </summary>
        private static void createAssignments()
        {
            DateTime[] treatmentStart =
            {
        new DateTime(2024, 5, 13, 9, 05, 00),
        new DateTime(2024, 3, 25, 18, 10, 00),
        new DateTime(2023, 12, 5, 14, 10, 00),
        new DateTime(2023, 10, 18, 21, 15, 00),
        new DateTime(2024, 2, 7, 14, 45, 00),
        new DateTime(2024, 1, 12, 11, 40, 00),
        new DateTime(2024, 4, 22, 22, 05, 00),
        new DateTime(2023, 11, 20, 4, 10, 00),
        new DateTime(2024, 6, 2, 7, 00, 00),
        new DateTime(2024, 7, 28, 17, 00, 00),
        new DateTime(2023, 9, 3, 23, 00, 00),
        new DateTime(2024, 5, 19, 13, 00, 00),
        new DateTime(2023, 12, 30, 9, 00, 00),
        new DateTime(2023, 11, 11, 20, 30, 00),
        new DateTime(2024, 4, 17, 6, 30, 00),
        new DateTime(2023, 10, 28, 16, 30, 00),
        new DateTime(2024, 3, 3, 1, 00, 00),
        new DateTime(2023, 12, 25, 19, 00, 00),
        new DateTime(2024, 2, 15, 9, 45, 00),
        new DateTime(2024, 1, 30, 15, 10, 00),
        new DateTime(2024, 5, 6, 19, 40, 00),
        new DateTime(2023, 10, 11, 8, 30, 00),
        new DateTime(2024, 6, 23, 3, 00, 00),
        new DateTime(2024, 7, 14, 7, 40, 00),
        new DateTime(2023, 9, 8, 21, 10, 00),
        new DateTime(2023, 12, 14, 5, 00, 00),
        new DateTime(2024, 3, 1, 12, 00, 00),
        new DateTime(2024, 6, 19, 17, 00, 00),
        new DateTime(2024, 1, 10, 12, 30, 00),
        new DateTime(2024, 4, 5, 22, 00, 00),
        new DateTime(2024, 2, 3, 7, 30, 00),
        new DateTime(2023, 11, 17, 14, 00, 00),
        new DateTime(2023, 10, 25, 21, 00, 00),
        new DateTime(2023, 8, 13, 8, 00, 00),
        new DateTime(2024, 5, 23, 0, 30, 00),
        new DateTime(2023, 9, 22, 19, 00, 00),
        new DateTime(2024, 1, 15, 5, 30, 00)
    };
            DateTime[] treatmentEnd =
            {
        new DateTime(2024, 5, 13, 9, 45, 00),
        new DateTime(2024, 3, 25, 19, 30, 00),
        new DateTime(2023, 12, 5, 15, 00, 00),
        new DateTime(2023, 10, 18, 22, 00, 00),
        new DateTime(2024, 2, 7, 15, 30, 00),
        new DateTime(2024, 1, 12, 13, 30, 00),
        new DateTime(2024, 4, 22, 22, 45, 00),
        new DateTime(2023, 11, 20, 5, 00, 00),
        new DateTime(2024, 6, 2, 8, 00, 00),
        new DateTime(2024, 7, 28, 18, 30, 00),
        new DateTime(2023, 9, 3, 23, 30, 00),
        new DateTime(2024, 5, 19, 14, 30, 00),
        new DateTime(2023, 12, 30, 11, 30, 00),
        new DateTime(2023, 11, 11, 22, 30, 00),
        new DateTime(2024, 4, 17, 7, 30, 00),
        new DateTime(2023, 10, 28, 20, 00, 00),
        new DateTime(2024, 3, 3, 4, 00, 00),
        new DateTime(2023, 12, 25, 20, 00, 00),
        new DateTime(2024, 2, 15, 13, 00, 00),
        new DateTime(2024, 1, 30, 16, 30, 00),
        new DateTime(2024, 5, 6, 21, 00, 00),
        new DateTime(2023, 10, 11, 9, 30, 00),
        new DateTime(2024, 6, 23, 4, 30, 00),
        new DateTime(2024, 7, 14, 12, 00, 00),
        new DateTime(2023, 9, 8, 3, 30, 00),
        new DateTime(2023, 12, 14, 7, 00, 00),
        new DateTime(2024, 3, 1, 13, 30, 00),
        new DateTime(2024, 6, 19, 20, 00, 00),
        new DateTime(2024, 1, 10, 18, 00, 00),
        new DateTime(2024, 4, 5, 23, 00, 00),
        new DateTime(2024, 2, 3, 8, 00, 00),
        new DateTime(2023, 11, 17, 16, 30, 00),
        new DateTime(2023, 10, 25, 22, 30, 00),
        new DateTime(2023, 8, 13, 9, 30, 00),
        new DateTime(2024, 5, 23, 2, 00, 00),
        new DateTime(2023, 9, 22, 20, 30, 00),
        new DateTime(2024, 1, 15, 6, 00, 00)
    };

            IEnumerable<Call> callService = s_dal!.Call.ReadAll();
            IEnumerable<Volunteer> volunteerService = s_dal!.Volunteer.ReadAll();

            int volunteerId = 0;
            DateTime TreatStart = new();
            DateTime TreatEnd = new();
            TypeTreatEnd typeTreatEnd = (TypeTreatEnd)1;

            // Calculate the loop limit as the smallest size among all data
            int loopCount = Math.Min(Math.Min(callService.Count(), volunteerService.Count()), Math.Min(treatmentStart.Length, treatmentEnd.Length));

            // Loop to create assignments for volunteers
            for (int i = 0; i < loopCount; i++)
            {
                int callId = callService.ElementAt(i).CallId;
                typeTreatEnd = (TypeTreatEnd)((i <= 30) ? 1 : (i < 45) ? s_rand.Next(2, 3) : 4);
                if (typeTreatEnd == (TypeTreatEnd)1)
                {
                    volunteerId = volunteerService.ElementAt(i).VolunteerId;
                    TreatStart = treatmentStart[i];
                    TreatEnd = treatmentEnd[i];
                }
            }

            
            Assignment newAssignment = new Assignment(0, 0, volunteerId, TreatStart, TreatEnd, typeTreatEnd);
            s_dal!.Assignment.Create(newAssignment);
        }
        /// <summary>
        /// Initializes the system by resetting the database and creating volunteers, calls, and assignments.
        /// </summary>
        /// <param name="dal">The DAL object for interacting with the database.</param>
        /// <exception cref="NullReferenceException">Thrown if the DAL object is null.</exception>
        public static void Do()
        {
            s_dal = DalApi.Factory.Get;
            Console.WriteLine("Reset Configuration values and List values...");
            //s_dal.ResetDB();  
            Console.WriteLine("Initializing Volunteers list ...");

            createVolunteers();
            createCalls();
            createAssignments();
        }
    }
}


