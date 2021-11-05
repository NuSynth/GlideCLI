namespace GlideCLI.Models
{
    public class Globals
    {
        public int studyTracker {get; set;} //used to check if list needs to be cleared


        /*TODO: Correct how I named osSwitch, to OsSwitch */
        public bool osSwitch { get; set; } // True if Linux, false if Windows. This is needed to store files correctly, so this app can be used in both operating systems.
        public string DirectoryPath { get; set; }
        public string DirectoryExists { get; set; }
        public string FilePath { get; set; }
        public bool FileExists { get; set; }


        /* Course files variables */
        public string CourseName { get; set; }
        public string CourseChapters { get; set; }
        public int CourseCount { get; set; }
        public int TopicCount { get; set; }


        public int TopicIndex { get; set; } // Needed for course study session
        public int TopicID { get; set; }
        public bool ProblemsDone { get; set; }
        
        // Use this date, its from the start of the study session
        public string TheDate { get; set; }
        
        //This is for Ready menu
        public bool madeSelect {get; set;}

        // These three are just used to display
        // topics left to study for the user
        // to see.
        public int newLeft {get; set;}
        public int currentLeft {get; set;}
        public int lateLeft {get; set;}
    }
}
