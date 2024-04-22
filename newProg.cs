using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        // Step 1: Load the XML file
        string xmlFilePath = "path_to_xml_file.xml";
        string xmlContent = File.ReadAllText(xmlFilePath);

        // Step 2: Convert XML to JSON dictionary
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlContent);

        // Convert XML to JSON using Newtonsoft.Json
        string jsonText = JsonConvert.SerializeXmlNode(xmlDoc);
        
        // Deserialize the JSON text to a dictionary
        Dictionary<string, object> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonText);

        // Step 3: Iterate through the dictionary and generate a list of key-value pairs
        List<KeyValuePair<string, object>> keyValuePairs = new List<KeyValuePair<string, object>>();

        // Function to iterate through the dictionary recursively
        void IterateDictionary(Dictionary<string, object> dict)
        {
            foreach (var kvp in dict)
            {
                keyValuePairs.Add(new KeyValuePair<string, object>(kvp.Key, kvp.Value));
                
                // If the value is a nested dictionary, recursively iterate through it
                if (kvp.Value is Dictionary<string, object> nestedDict)
                {
                    IterateDictionary(nestedDict);
                }
            }
        }

        // Start iterating from the root of the JSON dictionary
        IterateDictionary(jsonDict);

        // Output the key-value pairs
        foreach (var kvp in keyValuePairs)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }
}
