using System;
using System.Collections.Generic;
using System.Text;

namespace GlideCLI.Models
{
    public class TopicModel
    {
        public int Top_ID { get; set; }
        public int Course_ID { get; set; } // If more than one course, then it may be best to not start the TopicID at ZERO for the first topic, so that less of the program needs modifying to allow multiple courses.
        public string Top_Name { get; set; } // This is only used to make things easier for building a course. I can't think of why this would be needed, other than for that purpose.
        public bool Top_Studied { get; set; }

        public string Next_Date { get; set; }
        public string First_Date { get; set; }  // I might have a feature that displays the progress of topics since their first study dates.

        public double Num_Problems { get; set; }
        public double Num_Correct { get; set; }

        public double Top_Difficulty { get; set; }
        public double Top_Repetition { get; set; }
        public double Interval_Remaining { get; set; }

        public double Interval_Length { get; set; }
        public double Engram_Stability { get; set; }
        public double Engram_Retrievability { get; set; }
    }
}
