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

        // Step 2: Convert XML to JSON
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlContent);
        string jsonText = JsonConvert.SerializeXmlNode(xmlDoc);

        // Deserialize the JSON text to a dictionary
        Dictionary<string, object> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonText);

        // Step 3: Flatten the JSON dictionary
        List<KeyValuePair<string, string>> flattenedList = FlattenDictionary(jsonDict);

        // Output the flattened list
        foreach (var kvp in flattenedList)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }

    // Recursive function to flatten the dictionary
    static List<KeyValuePair<string, string>> FlattenDictionary(Dictionary<string, object> dict, string parentKey = "")
    {
        List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

        foreach (var kvp in dict)
        {
            string newKey = string.IsNullOrEmpty(parentKey) ? kvp.Key : $"{parentKey}.{kvp.Key}";

            // Check if the value is a nested dictionary
            if (kvp.Value is Dictionary<string, object> nestedDict)
            {
                // Recursively flatten the nested dictionary
                result.AddRange(FlattenDictionary(nestedDict, newKey));
            }
            else
            {
                // Convert the value to a string and add the key-value pair
                string value = kvp.Value?.ToString() ?? "";
                result.Add(new KeyValuePair<string, string>(newKey, value));
            }
        }

        return result;
    }
}
