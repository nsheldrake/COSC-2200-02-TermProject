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
        private string language;

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

        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        // Constructor 
        public Settings()
        {
            backgroundColor = "#FFFFFF";
            menuColor = "#1E3A5F";
            language = "English";
            // Load saved settings upon start up
            LoadSettings(); 
        }

        // Load settings from JSON file
        public void LoadSettings()
        {
            try
            {
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
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to save settings: {e.Message}");
            }
        }
    }
}
