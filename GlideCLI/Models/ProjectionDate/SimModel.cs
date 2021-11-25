namespace GlideCLI.Models
{
    public class SimModel
    {
        // For all lists
        public string First_Date { get; set; }
        public int Real_Repetition { get; set; }
        public double Top_Difficulty { get; set; }
        public double Interval_Length {get; set;}
        public int Top_Number {get;set;} // The line number here is used instead of topic id


        //Exclusively for Generate Studied topics
        public string Next_Date { get; set; }
        public string Simulated_Date { get; set; } // Simulated date of the simulateed repetition.
        public int Sim_Repetition { get; set; }        
    }
}

// Maybe clear all of these at the end of StudyCourse during real study session
// That could produce a live update that may be reinforcing