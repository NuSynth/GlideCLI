namespace GlideCLI.Models
{
    public class PredictModel
    {
        // Indices
        public int Gen_Studied_Index {get; set;}
        public int Gen_Projected_Index {get; set;}
        
        // Bools
        public bool Process_Prediction {get; set;}
        public bool Process_Gen_Sims_Studied {get; set;}
        
        //Ints
        public int Process_Repetitions {get; set;}
        
        //Doubles
        public double Avg_Difficulty { get; set; }

        //Strings
        public string Sim_Nxt_Date {get; set;}



        //These need to be a list in the event of multiple same dates, so the correct x value can be determined.
        // Points for line to be plotted
        
        //Max Y-Values: first studied
        /*
        y-point values correspond to first study dates
        x-point values correspond to number of repeats
        performed where y-value exists
        */
        
        //public string Y_High_Date {get; set;} 
        public double Y_High_Ycount {get; set;}
        public double Y_High_Xcount {get; set;} // Need the average of x if multiple equal first studies at Ymax


        //Max X-Values: repeats studied
        /*
        x-point values correspond to first study dates
        y-point values correspond to number of new-studies
        performed where x-value exists
        */

        //These to be a list in the event of multiple same dates, so the correct y value can be determined.
        //public string X_High_Date {get; set;} 
        public double X_High_Ycount {get; set;} // Need the average of y if multiple equal first studies at Xmax
        public double X_High_Xcount {get; set;}
    }
}
