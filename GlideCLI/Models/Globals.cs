using System;
using System.Collections.Generic;
using System.Text;

namespace GlideCLI.Models
{
    public class Globals
    {
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
    }
}
