using BlApi;
using Helpers;
using System.Collections;
namespace BlImplementation
{
    internal class VolunteerImplementation : IVolunteer
    {
        private readonly DalApi.IDal _dal = DalApi.Factory.Get;
        private readonly CallImplementation callImplementation = new CallImplementation();
        public void AddVolunteer(BO.Volunteer volunteer)
        {
            if (volunteer == null)
                throw new ArgumentNullException(nameof(volunteer), "The volunteer cannot be null.");
            Tools.ValidateVolunteerFormat(volunteer);
            Tools.ValidateVolunteerLogic(volunteer);
            DO.Volunteer newVolunteer = new DO.Volunteer
            {
                VolunteerId = volunteer.VolunteerId,
                FullName = volunteer.FullName,
                PhoneNum = volunteer.PhoneNum,
                Email = volunteer.Email,
                Password = volunteer.Password,
                Latitude = volunteer.Latitude,
                Longitude = volunteer.Longitude,
                role = (DO.Role)volunteer.role,
                IsActive = volunteer.IsActive,
                MaxDistance = volunteer.MaxDistance,
                distanceType = (DO.DistanceType)volunteer.distanceType
            };

            try
            {
                _dal.Volunteer.Create(newVolunteer);
                VolunteerManager.Observers.NotifyListUpdated(); //stage 5
            }
            catch (DO.DalAlreadyExistsException ex)
            {
                // תפיסת החריגה במקרה שכבר קיים מתנדב עם תעודת זהות זהה,
                // וזריקת חריגה מתאימה לשכבת התצוגה
                throw new BO.BlAlreadyExistsException($"A volunteer with ID {volunteer.VolunteerId} already exists.", ex);
            }
        }
        /// <summary>
        /// Deletes a volunteer from the system, ensuring that only inactive volunteers can be removed.
        /// </summary>
        /// <param name="userId">The ID of the volunteer to be deleted.</param>
        public void DeleteVolunteer(int userId)
        {
            try
            {
                DO.Volunteer? currentUser = _dal.Volunteer.Read(userId);
                DO.Assignment? userAssignments = _dal.Assignment.ReadAll().FirstOrDefault(a => a.VolunteerId == userId);
                if (currentUser!.IsActive != false && userAssignments == null)
                {
                    throw new Exception("Cannot delete an active volunteer.");
                }
                _dal.Volunteer.Delete(userId);
                VolunteerManager.Observers.NotifyListUpdated();//stage 5  	
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"A volunteer with ID {userId} dosen't exists.", ex);
            }
        }
        public BO.Role GetRole(string userName, string password)
        {
            var volunteer = _dal.Volunteer.ReadAll().FirstOrDefault(
                v => v.FullName == userName && v.Password == password);

            if (volunteer == null)
            {
                throw new ArgumentException("User not found or incorrect password.");
            }

            return (BO.Role)volunteer.role;
        }
        public BO.Volunteer GetVolunteerDetails(int userId)
        {
            try
            {
                DO.Volunteer? currentUser = _dal.Volunteer.Read(userId);
                return new BO.Volunteer
                {
                    VolunteerId = currentUser!.VolunteerId,
                    FullName = currentUser.FullName,
                    PhoneNum = currentUser.PhoneNum,
                    Email = currentUser.Email,
                    Password = currentUser.Password,
                    Adress = currentUser.Adress,
                    Latitude = currentUser.Latitude,
                    Longitude = currentUser.Longitude,
                    role = (BO.Role)currentUser.role,
                    IsActive = currentUser.IsActive,
                    MaxDistance = currentUser.MaxDistance,
                    distanceType = (BO.DistanceType)currentUser.distanceType
                };
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"A volunteer with ID {userId} dosen't exists.", ex);
            }
        }
        public IEnumerable<BO.VolunteerInList> GetVolunteersList(bool? isActive, BO.VolunteerSortField? sortBy)
        {
            var volunteers = _dal.Volunteer.ReadAll();
            var assignments = _dal.Assignment.ReadAll();            
            IEnumerable<BO.VolunteerInList> volunteerInList = volunteers.Select(v => new BO.VolunteerInList
            {
                VolunteerId = v.VolunteerId,
                FullName = v.FullName,
                IsActive = v.IsActive,
                HandledCallsCount = assignments.Count(a => a.VolunteerId == v.VolunteerId && a.typeTreatEnd.ToString() == BO.TypeTreatEnd.Treated.ToString()),
                CanceledCallsCount = assignments.Count(a => a.VolunteerId == v.VolunteerId && a.typeTreatEnd.ToString() == BO.TypeTreatEnd.SelfCancle.ToString()),
                ExpiredCallsCount = assignments.Count(a => a.VolunteerId == v.VolunteerId && a.typeTreatEnd.ToString() == BO.TypeTreatEnd.Expired.ToString()),
                CurrentCallId = callImplementation.RequestACallList(BO.CallSortField.Id, v.VolunteerId, BO.CallSortField.Id)
                                .Select(c => callImplementation.ReadingDetailsRequest(c.Id))
                                .FirstOrDefault(call => call.CallAssignments.Any(a => a.ActualFinishTime == null))?.CallId,
                CallType = callImplementation.RequestACallList(BO.CallSortField.Id, v.VolunteerId, BO.CallSortField.Id)
                            .Select(c => callImplementation.ReadingDetailsRequest(c.Id))
                            .FirstOrDefault(call => call.CallAssignments.Any(a => a.ActualFinishTime == null))?.callType
                            ?? BO.CallType.None

            }).ToList();
            // אם הועבר ערך בוליאני, מסננים לפי סטטוס המתנדבים (פעיל/לא פעיל)
            if (isActive.HasValue)
            {
                volunteers = volunteers.Where(v => v.IsActive == isActive.Value).ToList();
            }
            // אם הועבר שדה למיון, ממיינים את הרשימה
            if (sortBy.HasValue)
            {
                switch (sortBy.Value)
                {
                    case BO.VolunteerSortField.Id:
                        volunteers = volunteers.OrderBy(v => v.VolunteerId).ToList();
                        break;
                    case BO.VolunteerSortField.Name:
                        volunteers = volunteers.OrderBy(v => v.FullName).ToList();
                        break;
                    case BO.VolunteerSortField.PhoneNum:
                        volunteers = volunteers.OrderBy(v => v.PhoneNum).ToList();
                        break;
                    case BO.VolunteerSortField.Email:
                        volunteers = volunteers.OrderBy(v => v.Email).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // אם לא הועבר שדה למיון, ממיינים לפי ת.ז (VolunteerId)
                volunteers = volunteers.OrderBy(v => v.VolunteerId).ToList();
            }
            return volunteerInList;
        }
        /// <summary>
        /// Updates the details of a volunteer, ensuring proper validation and permission checks.
        /// </summary>
        /// <param name="userId">The ID of the user requesting the update.</param>
        /// <param name="volunteer">The updated volunteer object containing the new details.</param>
        public void UpdateVolunteerDetails(int userId, BO.Volunteer volunteer)
        {
            try
            {
                if (GetRole(volunteer.FullName!, volunteer.Password!) != BO.Role.Manager ||
                userId != volunteer.VolunteerId)
                {
                    throw new UnauthorizedAccessException(
                        "Only a manager or the volunteer you want to update can update a volunteer's details.");
                }
                DO.Volunteer? currentUser = _dal.Volunteer.Read(userId);
                var tmp = currentUser;
                Tools.ValidateVolunteerFormat(volunteer);
                Tools.ValidateVolunteerLogic(volunteer);

                var changedFields = typeof(BO.Volunteer)
                    .GetProperties()
                    .Where(prop => prop.CanRead && prop.CanWrite && prop.Name != nameof(BO.Volunteer.VolunteerId)
                     && !Equals(prop.GetValue(volunteer), prop.GetValue(currentUser)))
                    .Select(prop => prop.Name)
                    .ToList();
                if (changedFields.Contains(nameof(BO.Volunteer.role)) && GetRole(volunteer.FullName!, volunteer.Password!) != BO.Role.Manager)
                {
                    throw new UnauthorizedAccessException("Only a manager can update the volunteer's role.");
                }
                foreach (var field in changedFields)
                {
                    var property = typeof(DO.Volunteer).GetProperty(field);
                    if (property != null && property.CanWrite)
                    {
                        property.SetValue(currentUser, typeof(BO.Volunteer).GetProperty(field)?.GetValue(volunteer));
                    }
                }
                if (changedFields.Contains(nameof(BO.Volunteer.Adress)) && !string.IsNullOrEmpty(volunteer.Adress))
                {
                    var coordinates = VolunteerManager.AddressCoordinates(volunteer.Adress!);
                    tmp = currentUser with { Latitude = coordinates.latitude, Longitude = coordinates.longitude };
                }
                _dal.Volunteer.Update(tmp!);
                VolunteerManager.Observers.NotifyItemUpdated(tmp.VolunteerId);  //stage 5
                VolunteerManager.Observers.NotifyListUpdated();  //stage 5

            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"A volunteer with ID {volunteer.VolunteerId} dosen't exists.", ex);
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error updating volunteer {userId}: {ex.Message}");
                throw;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public IEnumerator<BO.VolunteerInList> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        // #region Stage 5
        public void AddObserver(Action listObserver) =>
        VolunteerManager.Observers.AddListObserver(listObserver); //stage 5
        public void AddObserver(int id, Action observer) =>
        VolunteerManager.Observers.AddObserver(id, observer); //stage 5
        public void RemoveObserver(Action listObserver) =>
        VolunteerManager.Observers.RemoveListObserver(listObserver); //stage 5
        public void RemoveObserver(int id, Action observer) =>
        VolunteerManager.Observers.RemoveObserver(id, observer); //stage 5
        //#endregion Stage 5
    }
}
