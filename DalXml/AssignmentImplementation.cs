using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using DO;
internal class AssignmentImplementation : IAssignment
{

    public void Create(Assignment item)
    {
        List<Assignment> Assignments = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        int id = Config.NextAssignmentId;
        Assignment copy = item with { AssignmentId = id };
        Assignments!.Add(copy);
        XMLTools.SaveListToXMLSerializer<Assignment>(Assignments, "assignment");
    }

    public void Delete(int id)
    {
        List<Assignment> Courses = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        if (Courses.RemoveAll(it => it.AssignmentId == id) == 0)
            throw new DalDoesNotExistException("Assignment", $"Course with ID={id} does Not exist");
        XMLTools.SaveListToXMLSerializer(Courses, Config.s_assignments_xml);
    }

    public void DeleteAll()
    {
        XMLTools.SaveListToXMLSerializer(new List<Assignment>(), Config.s_assignments_xml);
    }

    public Assignment? Read(int id)
    {
        List<Assignment> Courses = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        if (Courses == null)
            throw new DalDoesNotExistException("Assignment", "The list of assignments is empty or does not exist.");
        return Courses!.FirstOrDefault(obj => obj.AssignmentId == id);
    }

    public Assignment? Read(Func<Assignment, bool> filter)
    {
        List<Assignment> Courses = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        if (Courses == null)
            throw new DalDoesNotExistException("Assignment", "The list of assignments is empty or does not exist.");
        return Courses!.FirstOrDefault(filter);
    }

    public IEnumerable<Assignment> ReadAll(Func<Assignment, bool>? filter = null) 
        => (filter == null) ?XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml)!.Select(item => item) :
       XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml)!.Where(filter);

    public void Update(Assignment item)
    {
        List<Assignment> Courses = XMLTools.LoadListFromXMLSerializer<Assignment>(Config.s_assignments_xml);
        if (Courses.RemoveAll(it => it.AssignmentId == item.AssignmentId) == 0)
            throw new DalDoesNotExistException("Assignment", $"Course with ID={item.AssignmentId} does Not exist");
        Courses.Add(item);
        XMLTools.SaveListToXMLSerializer(Courses, Config.s_assignments_xml);
    }
}
