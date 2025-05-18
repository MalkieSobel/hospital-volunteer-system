using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using DO;
internal class CallImplementation : ICall
{
    public void Create(Call item)
    {
        List<Call> calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        int id = Config.NextCallId;
        Call copy = item with { CallId = id };
        calls!.Add(copy);
        string filePath = Path.GetFullPath(Config.s_calls_xml);
        Console.WriteLine($"Saving to file: {filePath}");
        XMLTools.SaveListToXMLSerializer(calls, Config.s_calls_xml);
    }

    public void Delete(int id)
    {
        List<Call> calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        if (calls == null)
            throw new DalDoesNotExistException("Call", "The list of calls is empty or does not exist.");
        Call? find = calls!.Find(obj => obj.CallId == id);
        if (find is null)
            throw new DalDoesNotExistException("Call", $"An object from type Call with id: {id} does not exist.");
        calls.Remove(find);
        XMLTools.SaveListToXMLSerializer(calls, Config.s_calls_xml);
    }

    public void DeleteAll()
    {
        XMLTools.SaveListToXMLSerializer(new List<Call>(), Config.s_calls_xml);
    }

    public Call? Read(int id)
    {
        List<Call> calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        if (calls == null)
            throw new DalDoesNotExistException("Call", "The list of calls is empty or does not exist.");
        return calls!.FirstOrDefault(obj => obj.CallId == id);
       
    }

    public Call? Read(Func<Call, bool> filter)
    {
        List<Call> calls = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        if (calls == null)
            throw new DalDoesNotExistException("Call", "The list of calls is empty or does not exist.");
        return calls!.FirstOrDefault(filter);
    }

    public IEnumerable<Call> ReadAll(Func<Call, bool>? filter = null)
     => 
        (filter == null) ? XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml)!.Select(item => item) :
        XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml)!.Where(filter);

    public void Update(Call item)
    {

        List<Call> Courses = XMLTools.LoadListFromXMLSerializer<Call>(Config.s_calls_xml);
        if (Courses.RemoveAll(obj => obj.CallId == item.CallId) == 0)
            throw new DalDoesNotExistException("Call", $"An object from type Call with id: {item.CallId} does not exist.");
        Courses.Add(item);
        XMLTools.SaveListToXMLSerializer(Courses, Config.s_calls_xml);

    }
}
