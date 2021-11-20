namespace GlideCLI.Models
{
    public class PredictModel
    {
        // Indices
        public int Gen_Studied_Index {get; set;}
        public int Gen_Projected_Index {get; set;}
        public int Loop_Index {get; set;}
        public int XrepIndex {get; set;}
        public int Sim_Past_Index {get; set;}

        
        // Bools
        public bool Process_Prediction {get; set;}
        public bool Process_Gen_Sims_Studied {get; set;}
        public bool First_Check {get; set;}
        public bool First_Rep {get; set;}
        public bool Only_ONE {get; set;}

        
        //Ints
        public int Process_Repetitions {get; set;}
        
        //Doubles
        public double Avg_Difficulty { get; set; }

        //Strings
        public string Sim_Nxt_Date {get; set;}
        public string Sim_Date_Use {get; set;} // The date being simulated
        public string Sim_All_Date_Use {get; set;}



        //These need to be a list in the event of multiple same dates, so the correct x value can be determined.
        // Points for line to be plotted
        
        //Max Y-Values: first studied
        /*
        y-point values correspond to first study dates
        x-point values correspond to number of repeats
        performed where y-value exists
        */        
        public double Y_High_Ycount {get; set;}
        public double Y_High_Xcount {get; set;}


        //Max X-Values: repeats studied
        /*
        x-point values correspond to first study dates
        y-point values correspond to number of new-studies
        performed where x-value exists
        */

        //These need to be a list in the event of multiple 
        //dates with equally the highest number of repetitions.
        //so the highest y value can be determined where the 
        // highst value of x exists.
        
        //public string X_High_Date {get; set;} 
        public double X_High_Ycount {get; set;}
        public double X_High_Xcount {get; set;}


        public double Current_X {get; set;} // needs to be type double
        public int Current_Y {get; set;} //needs to be type int


        //YmaxFirsts
        public string Prediction_Date {get; set;}
        //public bool Loop_Entered {get; set;} // To know if a date was stored.
        public int Find_Yhigh_Index {get; set;}

        //XmaxRepeatSort
        public int J {get; set;}
        public int I {get; set;}
        public int Date_Check {get; set;}
        public int Xsort_Count {get; set;}

        //XmaxRepeats
        public int Hold_Stuff {get; set;}



        //Debug vars
        public int debugWhileCount {get; set;}
        public int debugFirstTrueCount {get; set;}
        public int debugElseOneCount {get; set;}
        public int debugElseTwoCount {get; set;}
        public int debugElseThreeCount {get; set;}
        public bool debugFunk {get; set;}

        //Debug simulate genSimsAll
        public int debugTopic {get; set;}
        
    }
}
