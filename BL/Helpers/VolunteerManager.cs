using DalApi;
using Helpers;
using System;
using System.Net.Http;
using System.Text.Json;

internal class VolunteerManager 
{
    private static IDal s_dal = Factory.Get; //stage 4
    internal static ObserverManager Observers = new(); //stage 5 

    //נעזרתי בצ'אט GPT כדי לכתוב את המתודה הזו
    private const string API_KEY = "pk.6de6ba63ae20a164294411929f67adeb";
    /// <summary>
    /// Retrieves the geographical coordinates (latitude and longitude) of a given address  
    /// using the LocationIQ reverse geocoding service.  
    /// Throws an exception if the address is invalid or if the request fails.  
    /// </summary>
    internal static (double latitude, double longitude) AddressCoordinates(string? address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Address cannot be null or empty.", nameof(address));
        }
        string url = $"https://us1.locationiq.com/v1/search.php?key={API_KEY}&q={Uri.EscapeDataString(address)}&format=json";
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = client.GetAsync(url).Result; // קריאה סינכרונית
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error fetching coordinates. HTTP Status: {response.StatusCode}");
            }
            string jsonResponse = response.Content.ReadAsStringAsync().Result; // קריאה סינכרונית
            var results = JsonSerializer.Deserialize<LocationIqResponse[]>(jsonResponse);
            if (results == null || results.Length == 0)
            {
                throw new Exception("No coordinates found for the given address.");
            }
            return (results[0].Lat, results[0].Lon);
        }
    }

    private class LocationIqResponse
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

}
