using System;
using System.IO;

class Program
{
    static void Main()
    {
        // File paths
        string inputFilePath = "file_A.txt"; // Path to input file A
        string outputFilePath = "file_B.txt"; // Path to output file B

        // Use StreamReader to read from file A and StreamWriter to write to file B
        using (StreamReader reader = new StreamReader(inputFilePath))
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            string line;
            // Read each line from file A
            while ((line = reader.ReadLine()) != null)
            {
                // Remove the last character from the line
                if (line.Length > 0) // Check if the line is not empty
                {
                    line = line.Substring(0, line.Length - 1);
                }

                // Write the modified line to file B
                writer.WriteLine(line);
            }
        }

        Console.WriteLine("Processing complete. Modified lines written to file B.");
    }
}
