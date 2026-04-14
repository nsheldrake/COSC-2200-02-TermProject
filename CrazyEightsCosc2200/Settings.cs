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
        // Private properties
        private string backgroundColor;
        private string menuColor;

        // Public getters and setters
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

        // Constructor 
        public Settings()
        {
            backgroundColor = "#0E2B06";
            menuColor = "#06193B";
            // Load saved settings upon start up
            // LoadSettings(); 
        }

        // Load settings from JSON file
        public void LoadSettings()
        {
            try
            {
                string settingsFile = File.ReadAllText("./settings.json");
                Settings? loaded = JsonSerializer.Deserialize<Settings>(settingsFile);

                if (loaded != null)
                {
                    backgroundColor = loaded.BackgroundColor;
                    menuColor = loaded.MenuColor;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to load settings: {e.Message}");
            }

        }

        // Save current settings to JSON file
        public void SaveSettings()
        {
            try
            {
                string saved = JsonSerializer.Serialize(this, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText("./settings.json", saved);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to save settings: {e.Message}");
            }
        }
    }
}
