namespace GlideCLI.Models
{
    public class Globals
    {
        //used to check if list needs to be cleared
        public int studyTracker {get; set;}

        /* Schedule variables */
        public string SchedulePath { get; set; }
        public int ScheduleCount { get; set; }
        public bool ScheduleExists { get; set; }
        public bool DaysOffBool { get; set; }

        /*TODO: Correct how I named osSwitch, to OsSwitch */
        public bool osSwitch { get; set; } // True if Linux, false if Windows. This is needed to store files correctly, so this app can be used in both operating systems.
        public string DirectoryPath { get; set; }
        public string FilePath { get; set; }


        /* Course files variables */
        public string CourseName { get; set; }
        public string CourseChapters { get; set; }
        public int CourseCount { get; set; }
        public int TopicCount { get; set; }
        public int TopicIndex { get; set; } // Needed for course study session
        public int TopicID { get; set; }
        public bool ProblemsDone { get; set; }
        
        // Date from time program initializes
        public string TheDate { get; set; }
        
        //This is for AvailableOptions() and MainOptions()
        public bool madeSelect {get; set;}

        // These three are just used to display
        // topics left to study for the user
        // to see.
        public int newLeft {get; set;}
        public int currentLeft {get; set;}
        public int lateLeft {get; set;}

        //For SelectionDialogs(option three)
        public string response {get; set;}
    }
}
