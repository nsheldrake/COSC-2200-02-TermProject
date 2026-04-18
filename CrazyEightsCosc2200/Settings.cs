using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CrazyEightsCosc2200
{
    public class Settings
    {
        // Private properties.
        private string backgroundColor;
        private string menuColor;

        // Public getters and setters.
        public string BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        public string MenuColor
        {
            get { return menuColor; }
            set { menuColor = value; }
        }

        // Constructor.
        public Settings()
        {
            backgroundColor = "#0E2B06";
            menuColor = "#06193B";
            // Load saved settings upon start up
            // LoadSettings(); 
        }

        // Load settings from JSON file.
        public void LoadSettings()
        {
            try
            {
                // References: 1- https://stackoverflow.com/questions/13297563/read-and-parse-a-json-file-in-c-sharp
                //             2- https://www.educative.io/answers/how-to-read-a-json-file-in-c-sharp

                // Read json file and set the contents to a variable (settingsFile)
                string settingsFile = File.ReadAllText("./settings.json");
                // Deserialize the contents into a settings object
                Settings? loaded = JsonSerializer.Deserialize<Settings>(settingsFile);

                // If theres content in the file (file is not null)
                if (loaded != null)
                {
                    // Set the background and menu colors
                    backgroundColor = loaded.BackgroundColor;
                    menuColor = loaded.MenuColor;
                }
            }
            catch (Exception e)
            {
                // If an exception occurs then give a message telling the user
                Console.WriteLine($"Failed to load settings: {e.Message}");
            }

        }

        // Save current settings to JSON file.
        public void SaveSettings()
        {
            try
            {
                // Reference: https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to

                // Create a formatted string, this is used to write to the json file
                string saved = JsonSerializer.Serialize(this, new JsonSerializerOptions
                {
                    // Format = indented
                    WriteIndented = true
                });

                // Write saved to the json file
                File.WriteAllText("./settings.json", saved);
            }
            catch (Exception e)
            {
                // If an exception occurs then give a message telling the user
                Console.WriteLine($"Failed to save settings: {e.Message}");
            }
        }
    }
}
