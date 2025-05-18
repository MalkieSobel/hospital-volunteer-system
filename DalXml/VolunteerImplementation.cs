using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;
internal class VolunteerImplementation : IVolunteer
{
    static Volunteer getVolunteer(XElement s)
    {
        return new DO.Volunteer()
        {
            VolunteerId = s.ToIntNullable("VolunteerId") ?? throw new FormatException("can't convert id"),
            FullName = (string?)s.Element("FullName") ?? "",
            PhoneNum = (string?)s.Element("PhoneNum") ?? "",
            Email = (string?)s.Element("PhoneNum") ?? "",
            Password = (string?)s.Element("Password") ?? "",
            Adress = (string?)s.Element("Adress") ?? "",
            Latitude = (double?)s.Element("Latitude") ?? throw new FormatException("can't convert Latitude"),
            IsActive = (bool?)s.Element("IsActive") ?? false,
            MaxDistance = (double?)s.Element("MaxDistance") ?? throw new FormatException("can't convert MaxDistance"),
            distanceType = s.ToEnumNullable<DistanceType>("distanceType") ?? DistanceType.Aerial
        };
    }
    public void Create(Volunteer item)
    {       
        XElement? volunteerElem = XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml);
        if (Read(item.VolunteerId) is not null)
            throw new DalAlreadyExistsException("Volunteer", $"Volunteer with ID {item.VolunteerId} already exists.");
        volunteerElem!.Add(CreateVolunteerElement(item));
        XMLTools.SaveListToXMLElement(volunteerElem, Config.s_volunteers_xml);
    }
    public void Delete(int id)
    {
        XElement? volunteerElem = XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml);
        if (volunteerElem == null)
            throw new DalDoesNotExistException("Volunteer", "The list of volunteers is empty or does not exist.");
        XElement? find = volunteerElem.Elements().FirstOrDefault(obj => (int?)obj.Element("VolunteerId") == id);

        if (find is null)
            throw new DalDoesNotExistException("Volunteer", $"An object from type Volunteer with id: {id} does not exist.");
        find.Remove();
        XMLTools.SaveListToXMLElement(volunteerElem, Config.s_volunteers_xml);
    }
    public void DeleteAll()
    {
        XElement? volunteerElem = XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml);
        volunteerElem?.RemoveNodes();
    }
    public Volunteer? Read(int id)
    {
        XElement? studentElem =
        XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml).Elements().FirstOrDefault(st => (int?)st.Element("VolunteerId") == id);
        return studentElem is null ? null : getVolunteer(studentElem);
    }
    public Volunteer? Read(Func<Volunteer, bool> filter)
    {
        return XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml).Elements()
            .Select(s => getVolunteer(s)).FirstOrDefault(filter);
    }
    public IEnumerable<Volunteer> ReadAll(Func<Volunteer, bool>? filter = null)
    {
        XElement volunteerElem = XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml);
        var volunteers = volunteerElem.Elements("Volunteer").Select(x => getVolunteer(x));                                  
        return filter == null ? volunteers : volunteers.Where(filter);
    }
    public void Update(Volunteer item)
    {
        XElement volunteersRootElem = XMLTools.LoadListFromXMLElement(Config.s_volunteers_xml);

        (volunteersRootElem.Elements().FirstOrDefault(st => (int?)st.Element("VolunteerId") == item.VolunteerId)
        ?? throw new DO.DalDoesNotExistException("Volunteer", $"Volunteer with ID={item.VolunteerId} does Not exist")).Remove();
        
        volunteersRootElem.Add(CreateVolunteerElement(item));
        XMLTools.SaveListToXMLElement(volunteersRootElem, Config.s_volunteers_xml);
    }
    private XElement CreateVolunteerElement(Volunteer item)
    {
        return new XElement("Volunteer",
        new XElement("VolunteerId", item.VolunteerId),
        new XElement("FullName", item.FullName),
        new XElement("Email", item.Email),
        new XElement("Password", item.Password),
        new XElement("Adress", item.Adress),
        new XElement("Latitude", item.Latitude),
        new XElement("Longitude", item.Longitude),
        new XElement("role", item.role),
        new XElement("IsActive", item.IsActive),
        new XElement("MaxDistance", item.MaxDistance),
        new XElement("distanceType", item.distanceType));
    }
}
