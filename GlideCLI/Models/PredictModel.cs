namespace GlideCLI.Models
{
    public class PredictModel
    {
        // Date for StudyHud()
        public string Initial_Prediction_Date {get; set;}
        public string New_Prediction_Date {get; set;}
        public int Final_Topic {get; set;}
        public int Until_New {get; set;}                              // Counts down number of topics studied, if topics were scheduled to study
        public bool Lock_Initial {get; set;}                          // Required so that Initial_Prediction_Date is set only once.
        public bool Lock_New {get; set;}                              // Used for new dates to display
        public bool Unlock_New_Date {get; set;}                       // Display a newer date if Until_New reaches ZERO
        public bool End_Reached {get; set;}                           // No expected date shown if == false
        
        // Indices
        public int Gen_Studied_Index {get; set;}
        public int Gen_Projected_Index {get; set;}
        public int Loop_Index {get; set;}
        public int XrepIndex {get; set;}
        
        // Bools
        public bool Process_Prediction {get; set;}                   // Use to get actual info from topics one time
        public bool Process_Gen_Sims_Studied {get; set;}
        public bool First_Check {get; set;}
        public bool Only_ONE {get; set;}
        public bool Enough_Studied {get; set;}                       // For StudyHUD decision

        //Doubles
        public double Avg_Difficulty { get; set; }

        //Strings
        public string Sim_Date_Use {get; set;}                       // The date being simulated
        public double Y_High_Ycount {get; set;}
        public double X_High_Xcount {get; set;}

        //Limits
        public double Current_X {get; set;}                          // needs to be type double
        public int Current_Y {get; set;}                             // needs to be type int


        //YmaxFirsts
        //public bool Loop_Entered {get; set;}                       // To know if a date was stored.
        public int Find_Yhigh_Index {get; set;}

        //XmaxRepeatSort
        public int J {get; set;}
        public int I {get; set;}
        public int Date_Check {get; set;}

        //Debug vars
        // public int debugWhileCount {get; set;}
        // public int debugFirstTrueCount {get; set;}
        // public int debugElseOneCount {get; set;}
        // public int debugElseTwoCount {get; set;}
        // public int debugElseThreeCount {get; set;}
        // public bool debugFunk {get; set;}
        
    }
}
