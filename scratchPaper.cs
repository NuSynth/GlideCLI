/****************************************************************
                        scratchPaper.cs
This file is normally used to design components before
putting them into the program.
****************************************************************/


        private static void SimulatePastStudies()
        {
            int index = predictVars.Gen_Studied_Index = predictVars.Sim_Past_Index = Constants.ZERO_INT;
            while (index < studiedSimList.Count) //Replace Foreach foreach
            {
                predictVars.First_Rep = true;
                SimulateOnePastStudy(index);
                ++index;                
            }
            index = Constants.ZERO_INT;
            predictVars.Gen_Studied_Index = Constants.ZERO_INT;
        }
        private static void SimulateOnePastStudy(int index)
        {
            while (predictVars.Sim_Past_Index < studiedSimList.ElementAt(index).Real_Repetition)
            {
                InitializePastStudy(index);
                ++predictVars.Gen_Studied_Index;
            }
            predictVars.Sim_Past_Index = Constants.ZERO_INT;
        }
        private static void InitializePastStudy(int index)
        {
            SimModel studiedSims = new SimModel();

            studiedSims.First_Date = studiedSimList.ElementAt(index).First_Date;
            studiedSims.Real_Repetition = studiedSimList.ElementAt(index).Real_Repetition;
            if (predictVars.First_Rep == true)
            {
                predictVars.First_Rep = false;

                studiedSims.Sim_Repetition = studiedSimList.ElementAt(index).Sim_Repetition;
                studiedSims.Repetition_Date = studiedSimList.ElementAt(index).First_Date;
                predictVars.Gen_Sims_Studied_Date = studiedSims.First_Date;
            }
            else
            {
                studiedSims.Repetition_Date = predictVars.Gen_Sims_Studied_Date;
                studiedSims.Sim_Repetition = genSimsStudied.ElementAt(predictVars.Gen_Studied_Index - Constants.ONE_INT).Sim_Repetition;
            }
            studiedSims.Top_Difficulty = studiedSimList.ElementAt(index).Top_Difficulty;
            studiedSims.Top_Number = studiedSimList.ElementAt(index).Top_Number; 
            

            if (studiedSims.Sim_Repetition < studiedSims.Real_Repetition)
            {
                genSimsStudied.Add(studiedSims);
                predictVars.Process_Gen_Sims_Studied = true;
                SimCalculateLearning();
                predictVars.Process_Gen_Sims_Studied = false;
            }
            predictVars.Sim_Past_Index = genSimsStudied[predictVars.Gen_Studied_Index].Sim_Repetition;

        }


        /****************MODIFIED VERSION FOR JUST INITIALIZATION******************************/

        private static void PreparePastStudies()
        {
            int index = predictVars.Gen_Studied_Index = predictVars.Sim_Past_Index = Constants.ZERO_INT;
            int repetition = Constants.ONE_INT;
            while (index < studiedSimList.Count) //Replace Foreach foreach
            {
                repetition = studiedSimList.ElementAt(index).Sim_Repetition;
                SimulateOnePastStudy(index, repetition);
                ++index;                
            }
        }
        private static void InitializeOnePastStudy(int index, int repetition)
        {
            int nextRep = Constants.ZERO_INT;
            int lastRep = studiedSimList.ElementAt(index).Real_Repetition;
            while (repetition < lastRep)
            {
                nextRep = PastStudyInitialization(index, repetition);
                repetition = nextRep;
            }
        }
        private static int PastStudyInitialization(int index, int repetition)
        {
            SimModel studiedSims = new SimModel();

            studiedSims.First_Date = studiedSimList.ElementAt(index).First_Date;
            studiedSims.Real_Repetition = studiedSimList.ElementAt(index).Real_Repetition;
            studiedSims.Sim_Repetition = repetition;
            studiedSims.Top_Difficulty = studiedSimList.ElementAt(index).Top_Difficulty;
            studiedSims.Interval_Length = Constants.ZERO_INT; // Be sure that this is updated for each rep!
            studiedSims.Top_Number = studiedSimList.ElementAt(index).Top_Number;
            if (repetition == Constants.ONE_INT)
            {
                studiedSims.Repetition_Date = studiedSimList.ElementAt(index).First_Date;
            }
            genSimsStudied.Add(studiedSims);

            ++repetition;
            return repetition;
        }