using BlApi;
using System.Collections;
using Helpers;
using BO;
using System.Globalization;
using DalApi;
using BO;
using DO;
using System.Threading;

namespace BlImplementation
{
    internal class CallImplementation : BlApi.ICall
    {         
        private readonly DalApi.IDal _dal  = DalApi.Factory.Get;
        private readonly AdminImplementation adminImplementation = new AdminImplementation();
       
        // מתודת בקשת כמויות קריאה
        public IEnumerable<BO.CallInList> GetCallCounts()
        {
           
            var groupedCalls = RequestACallList(null, null, null).GroupBy(call => call.Status)
                        .Select(group => new BO.CallInList { Status = group.Key, AssignmentsCount = group.Count() })
                        .ToArray();
            return groupedCalls;
        }
        //מתודת בקשת רשימת קריאות
        public IEnumerable<BO.CallInList> RequestACallList(BO.CallSortField? filterBy, object? value, BO.CallSortField? sortBy)
        {
            // קבלת כל ההקצאות וכל הקריאות
            var assignments = _dal.Assignment.ReadAll();
            var calls = _dal.Call.ReadAll();
            var volunteer = _dal.Volunteer.ReadAll();

            var tempAssigntments = assignments.GroupBy(a => a.CallId)
                .Select(a => new
                {
                    AssId = a.OrderBy(d => d.TreatStart).Last().AssignmentId,
                    CallId = a.Key,
                    CountAssi = a.Count(),
                    VolId = a.OrderBy(d => d.TreatStart).Last().VolunteerId,
                });
            var callInList = tempAssigntments.Select(a => new BO.CallInList
            {
                Id = a.AssId,
                CallId = a.CallId,
                callType = (BO.CallType)calls.FirstOrDefault(b => b.CallId == a.CallId).callType,
                OpenTime = calls.FirstOrDefault(b => b.CallId == a.CallId).CallOpenTime,
                TimeLeft = calls.FirstOrDefault(b => b.CallId == a.CallId).MaxEndTime - AdminManager.Now,
                LastVolunteerName = volunteer.FirstOrDefault(b => b.VolunteerId == a.VolId).FullName,
                TimeToComplete = assignments.FirstOrDefault(b => b.CallId == a.CallId).TreatEnd - calls.FirstOrDefault(b => b.CallId == a.CallId).CallOpenTime,
                Status = (BO.Status)Tools.GetCallInListStatus(calls.FirstOrDefault(b => b.CallId == a.CallId), assignments.FirstOrDefault(b => b.AssignmentId == a.AssId), adminImplementation.GetSystemClock(), adminImplementation.GetRiskRange()),
                AssignmentsCount = a.CountAssi
            });

            // סינון אם יש פרמטר של סינון
            if (filterBy != null && value != null)
            {
                callInList = callInList.Where(c => c.GetType().GetProperty(filterBy.ToString())?.GetValue(c) == value);
            }

            // מיון אם יש פרמטר של מיון
            if (sortBy != null)
            {
                callInList = callInList.OrderBy(c => c.GetType().GetProperty(sortBy.ToString())?.GetValue(c));
            }

            return callInList;
        }
        //מתודת בקשת פרטי קריאה
        public BO.Call ReadingDetailsRequest(int id)
        {

            try
            {
                // קבלת הקריאה מה-DAL
                var volunteer = _dal.Volunteer.ReadAll();
                var call = _dal.Call.Read(id);
                var assignments = _dal.Assignment.ReadAll().Where(a => a.CallId == id);

                // יצירת אובייקט BO.Call והוספת רשימת ההקצאות
                return new BO.Call
                {

                    CallId = call.CallId,
                    callType = (BO.CallType)call.callType,
                    VerbalDescription = call.VerbalDescription,
                    Adress = call.Adress,
                    Latitude = call.Latitude,
                    Longitude = call.Longitude,
                    CallOpenTime = call.CallOpenTime,
                    MaxEndTime = call.MaxEndTime,
                    CallAssignments = assignments.Select(a => new BO.CallAssignInList
                    {
                        VolunteerId = a.VolunteerId,
                        VolunteerName = volunteer.FirstOrDefault(b => b.VolunteerId == a.VolunteerId).FullName,
                        AssignmentTime = a.TreatStart,
                        ActualFinishTime = a.TreatEnd,
                        TypeTreatEnd = (BO.TypeTreatEnd)a.typeTreatEnd
                    }).ToList()
                };
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"No call found with ID{id}", ex);
            }
        }
        // מתודת עדכון פרטי קריאה
        public void UpdateDetailsRequest(BO.Call call)
        {
            try
            {
                Tools.ValidateCallLogic(call);
                Tools.ValidatCallFormat(call);

                // בדיקה האם הקריאה קיימת
                var existingCall = _dal.Call.Read(call.CallId);

                // עדכון קווי האורך והרוחב בהתאם לכתובת (במידת הצורך)
                var coordinates = VolunteerManager.AddressCoordinates(call.Adress!);
                call.Latitude = coordinates.latitude;
                call.Longitude = coordinates.longitude;
             

                // יצירת אובייקט נתונים (DO)
                var updatedCall = new DO.Call
                {
                    CallId = call.CallId,
                    callType = (DO.CallType)call.callType,
                    VerbalDescription = call.VerbalDescription,
                    Adress = call.Adress,
                    Latitude = call.Latitude,
                    Longitude = call.Longitude,
                    CallOpenTime = call.CallOpenTime,
                    MaxEndTime = call.MaxEndTime,
                };


            

                // עדכון הקריאה בשכבת הנתונים
                _dal.Call.Update(updatedCall);
                CallManager.Observers.NotifyItemUpdated(updatedCall.CallId);  //stage 5
                CallManager.Observers.NotifyListUpdated();
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"No call found with ID {call.CallId}", ex);
            }
            
        }
        //מחיקת פרטי קריאה
        public void DeleteCallRequest(int id)
        {
            try
            {
                // שלב 1: פנייה לשכבת הנתונים כדי לבדוק אם קיימת קריאה
                var callRequest = _dal.Call.Read(id); // הנחתה שה-Read מקבל את מזהה הקריאה

                // TODO: שלב 2: בדיקה אם הקריאה בסטטוס "פתוחה"

                // שלב 3: בדיקה אם הקריאה הוקצתה למתנדב (הקצאה)
                var assignments = _dal.Assignment.ReadAll(); // הנחתה ש-ReadAll מחזיר את כל ההקצאות
                bool isAssigned = assignments.Any(a => a.CallId == id); // אם יש הקצאה לקריאה
                if (isAssigned)
                {
                    throw new Exception("Cannot delete a call that has been assigned to a volunteer.");
                }

                // שלב 4: ניסיון למחוק את הקריאה משכבת הנתונים
                _dal.Call.Delete(id); // הנחתה ש-Delete מבצע את המחיקה
                CallManager.Observers.NotifyListUpdated();  //stage 5  	

            }
            catch (DO.DalDoesNotExistException ex)
            {
                // שלב 5: אם מתרחשת חריגה, תיזרק חריגה לכיוון שכבת התצוגה
                throw new BO.BlDoesNotExistException($"A Volunteer with Id: {id} does not exist." + ex);
            }
            catch (Exception ex)
            {
                // שלב 5: אם מתרחשת חריגה, תיזרק חריגה לכיוון שכבת התצוגה
                throw new Exception("Unable to delete the call: " + ex.Message);
            }
        }
        //מתודת הוספת קריאה
        public void AddCall(BO.Call call)
        {
            try
            {
                Tools.ValidateCallLogic(call);
                Tools.ValidatCallFormat(call);
                // יצירת אובייקט נתונים (DO)
                var Call = new DO.Call
                {
                    CallId = call.CallId,
                    callType = (DO.CallType)call.callType,
                    VerbalDescription = call.VerbalDescription,
                    Adress = call.Adress,
                    Latitude = call.Latitude,
                    Longitude = call.Longitude,
                    CallOpenTime = call.CallOpenTime,
                    MaxEndTime = call.MaxEndTime
                };
                _dal.Call.Create(Call);
                CallManager.Observers.NotifyListUpdated();
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BlDoesNotExistException("Error adding the call to the system.", ex);
            }
        }
        //מתודת בקשת רשימת קריאות סגורות שטופלו ע"י מתנדב
        public IEnumerable<BO.ClosedCallInList> ListCloseCallsByVolunteer(int id, BO.CallSortField? sortBy, BO.CallFilterField? filterBy)
        {
            var assignments = _dal.Assignment.ReadAll();
            var calls = _dal.Call.ReadAll();

            // חיבור בין קריאות להקצאות לפי מזהה קריאה וסינון רק קריאות סגורות
            var closedCallInList = from call in calls
                                   join assignment in assignments
                                   on call.CallId equals assignment.CallId into callAssignments
                                   from assignment in callAssignments.DefaultIfEmpty()
                                   where Tools.GetCallInListStatus(call, assignment, adminImplementation.GetSystemClock(), adminImplementation.GetRiskRange()) == 3 // סטטוס 'קריאה סגורה'
                                   && assignment?.VolunteerId == id // סינון לקריאות שטופלו ע"י המתנדב
                                   select new BO.ClosedCallInList
                                   {
                                       Id = call.CallId,
                                       CallType = (BO.CallType)call.callType,
                                       Address = call.Adress,
                                       OpenTime = call.CallOpenTime,
                                       AssignmentTime = assignment?.TreatStart ?? default,
                                       ActualFinishTime = assignment?.TreatEnd,
                                       TypeTreatEnd = (BO.TypeTreatEnd)assignment?.typeTreatEnd
                                   };

            // סינון אם יש פרמטר סינון
            if (filterBy != null && id != null)
            {
                closedCallInList = closedCallInList.Where(c =>
                    c.GetType().GetProperty(filterBy.ToString())?.GetValue(c)?.Equals(id) == true);
            }

            // מיון אם יש פרמטר מיון
            if (sortBy != null)
            {
                closedCallInList = closedCallInList.OrderBy(c =>
                    c.GetType().GetProperty(sortBy.ToString())?.GetValue(c));
            }

            return closedCallInList.ToList();
        }
        //מתודת בקשת רשימת קריאות פתוחות לבחירה ע"י מתנדב
        public IEnumerable<BO.OpenCallInList> ListOpenCallsByVolunteer(int id, BO.CallSortField? sortBy, BO.CallFilterField? filterBy)
        {
            var assignments = _dal.Assignment.ReadAll();
            var calls = _dal.Call.ReadAll();
            var volunteer = _dal.Volunteer.Read(id); // קבלת נתוני המתנדב לצורך חישוב מרחק

            // חיבור בין קריאות להקצאות וסינון לקריאות פתוחות או פתוחות בסיכון
            var openCallInList = from call in calls
                                 join assignment in assignments
                                 on call.CallId equals assignment.CallId into callAssignments
                                 from assignment in callAssignments.DefaultIfEmpty()
                                 let status = (BO.Status)Tools.GetCallInListStatus(call, assignment, adminImplementation.GetSystemClock(), adminImplementation.GetRiskRange())
                                 where status == (BO.Status)1 || status == (BO.Status)5 // סטטוס 'פתוחה' או 'פתוחה בסיכון'
                                 select new BO.OpenCallInList
                                 {
                                     Id = call.CallId,
                                     CallType = (BO.CallType)call.callType,
                                     Description = call.VerbalDescription,
                                     Address = call.Adress,
                                     OpenTime = call.CallOpenTime,
                                     MaxFinishTime = call.MaxEndTime,
                                     DistanceFromVolunteer = Tools.CalculateDistance( volunteer!, call )
                                 };

            // סינון אם יש פרמטר סינון
            if (filterBy != null)
            {
                openCallInList = openCallInList.Where(c =>
                    c.GetType().GetProperty(filterBy.ToString())?.GetValue(c)?.Equals(id) == true);
            }

            // מיון אם יש פרמטר מיון
            if (sortBy != null)
            {
                openCallInList = openCallInList.OrderBy(c =>
                    c.GetType().GetProperty(sortBy.ToString())?.GetValue(c));
            }
            else
            {
                openCallInList = openCallInList.OrderBy(c => c.Id); // ברירת מחדל לפי מספר קריאה
            }

            return openCallInList.ToList();
        }
        //מתודת עדכון סיום טיפול
        public void UpdateEndCall(int idVolunteer, int idCall)
        {
            var assignment = _dal.Assignment.Read(idCall)
               ?? throw new InvalidOperationException("The assignment does not exist.");

            var requester = _dal.Volunteer.Read(idVolunteer)
               ?? throw new InvalidOperationException("The volunteer does not exist.");

            if (assignment.VolunteerId != idVolunteer)
            {
                throw new UnauthorizedAccessException("Only the volunteer themselves is authorized to cancel the call.");
            }

            if (assignment.TreatEnd != null)
            {
                throw new InvalidOperationException("The call is not open or has already ended.");
            }
            DO. Assignment newA= assignment with {TreatEnd= adminImplementation.GetSystemClock(), typeTreatEnd = DO.TypeTreatEnd.Treated };
         

            try
            {
                _dal.Assignment.Update(assignment);
                CallManager.Observers.NotifyItemUpdated(assignment.AssignmentId);  //stage 5
                CallManager.Observers.NotifyListUpdated();  //stage 5

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error updating the assignment.", ex);
            }
        }
        //מתודת עדכון ביטול טיפול
        public void UpdateCancelCall(int idRequester, int idCall)
        {
            var assignment = _dal.Assignment.Read(idCall);
            var requester = _dal.Volunteer.Read(idRequester);
           

             // בדיקת הרשאות - מבקש הבקשה חייב להיות או מנהל או המתנדב שההקצאה רשומה עליו
            bool isRequesterVolunteer = assignment.VolunteerId == idRequester;
            bool isRequesterAdmin = requester.role.ToString() == DO.Role.Manager.ToString();

            if (!isRequesterVolunteer && !isRequesterAdmin)
            {
                throw new UnauthorizedAccessException("Only an administrator or the volunteer themselves are authorized to cancel the call.");
            }

            // בדיקה שההקצאה עדיין פתוחה
            if (assignment.TreatEnd != null)
            {
                throw new InvalidOperationException("The call is either closed or has already ended.");

            }


            // עדכון זמן הסיום בפועל
            var UpdateAssignment = assignment with
            {
                TreatEnd = adminImplementation.GetSystemClock(),
                typeTreatEnd = isRequesterVolunteer
                ? DO.TypeTreatEnd.SelfCancle  // ביטול עצמי
                : DO.TypeTreatEnd.ManagerCancle
            }; // ביטול מנהל };
            _dal.Assignment.Update(UpdateAssignment);
            CallManager.Observers.NotifyListUpdated();
            CallManager.Observers.NotifyItemUpdated(UpdateAssignment.AssignmentId);



        }
        //מתודת בחירה קריאה לטיפול
        public void ChooseCallForTreatment(int volunteerId, int callId)
        {
            // שליפת הקריאה מה-DAL
            var call = _dal.Call.Read(callId);
            var volunteer = _dal.Volunteer.Read(callId);

            // שליפת הקצאות קיימות על הקריאה
            var existingAssignments = _dal.Assignment.ReadAll()
                .Where(a => a.CallId == callId)
                .ToList();

            // בדיקת תקינות הבקשה
            // 1. לבדוק אם הקריאה כבר טופלה
            if (call.MaxEndTime < DateTime.Now)
            {
                throw new InvalidOperationException("The call has expired.");

            }

            // 2. לבדוק אם קיימת הקצאה פתוחה
            bool isCallUnderTreatment = existingAssignments.Any(a => a.TreatEnd == null);
            if (isCallUnderTreatment)
            {
                throw new InvalidOperationException("The call is already being handled.");

            }

            // 3. אם הקריאה לא טופלה עדיין, ניתן להקצות אותה למתנדב
            // יצירת ישות חדשה של הקצאה
            var newAssignment = new DO.Assignment
            {
                CallId = callId,
                VolunteerId = volunteerId,
                TreatStart = DateTime.Now, // זמן כניסה לטיפול
                TreatEnd = null, // עדיין לא הסתיים
                typeTreatEnd = null // עדיין לא ידוע
            };
             // ניסיון הוספה ל-DAL
            try
            {
                _dal.Assignment.Create(newAssignment);
                CallManager.Observers.NotifyListUpdated();
                CallManager.Observers.NotifyItemUpdated(newAssignment.AssignmentId);


            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"A user with Id: {volunteerId} dose not exist", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error creating assignment for volunteer.", ex);

            }
        }

        public IEnumerator<CallInList> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CallInList> RequestingReadingQuantities()
        {
            throw new NotImplementedException();
        }

        public void UpdateExpiredOpenCalls()
        {
            throw new NotImplementedException();
        }

        public void AddObserver(Action listObserver) =>
        CallManager.Observers.AddListObserver(listObserver); //stage 5
        public void AddObserver(int id, Action observer) =>
        CallManager.Observers.AddObserver(id, observer); //stage 5
        public void RemoveObserver(Action listObserver) =>
        CallManager.Observers.RemoveListObserver(listObserver); //stage 5
        public void RemoveObserver(int id, Action observer) =>
        CallManager.Observers.RemoveObserver(id, observer); //stage 5

        // מתודת עדכון כל הקריאות הפתוחות שפג תוקפן
        //public void UpdateExpiredOpenCalls()
        //{
        //    var calls = _dal.Call.ReadAll();
        //    var assignments = _dal.Assignment.ReadAll();

        //    // חיבור בין קריאות להקצאות לפי מזהה קריאה
        //    foreach (var call in calls)
        //    {
        //        // אם זמן הסיום של הקריאה עבר והקריאה לא טופלה עדיין
        //        if (call.MaxEndTime < DateTime.Now && !assignments.Any(a => a.CallId == call.CallId && a.TreatEnd == null))
        //        {
        //            // אם אין הקצאה לקריאה, יצירת הקצאה חדשה עם סוג סיום "ביטול פג תוקף"
        //            var newAssignment = new DO.Assignment
        //            {
        //                CallId = call.CallId,
        //                VolunteerId = null, // אין מתנדב
        //                TreatEnd = DateTime.Now, // זמן סיום לפי שעון המערכת
        //                typeTreatEnd = DO.TypeTreatEnd.Expired, // סוג סיום "ביטול פג תוקף"
        //            };
        //            _dal.Assignment.Create(newAssignment);
        //        }
        //        else
        //        {
        //            // אם יש הקצאה והזמן סיום טיפול שלה הוא null, עדכון ההקצאה
        //            var assignment = assignments.FirstOrDefault(a => a.CallId == call.CallId && a.TreatEnd == null);
        //            if (assignment != null)
        //            {
        //                assignment = assignment with { TreatEnd = adminImplementation.GetSystemClock(), typeTreatEnd = DO.TypeTreatEnd.Treated };
        //                _dal.Assignment.Update(assignment); // עדכון ההקצאה
        //            }
        //        }
        //    }
        //}






    }
}

