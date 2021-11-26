/****************************************************************
                        scratchPaper.cs
This file is normally used to design components before
putting them into the program.
****************************************************************/


        private static void SimCalculateLearning()
        {
            if (predictVars.Process_Gen_Sims_Studied == false)
                SimAddRepetition();
            SimIntervalTime();
            SimProcessDate();
        }
        private static void SimAddRepetition()
        {
                ++genSimsAll[predictVars.Gen_Projected_Index].Sim_Repetition;
        }
        private static void SimIntervalTime()
        {
            const double SINGLE_DAY = 1440; // 1440 is the quatity in minutes of a day. I'm using minutes, instead of whole days, to be more precise.
            double difficulty;
            int ithRepetition;
            double intervalLength;

            if (predictVars.Process_Gen_Sims_Studied == true)
            {
                difficulty = genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Top_Difficulty;
                ithRepetition = genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Sim_Repetition;
                intervalLength = genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Interval_Length;
            }
            else
            {
                difficulty = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Top_Difficulty;
                ithRepetition = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Sim_Repetition;
                intervalLength = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Interval_Length;
            }
            if (ithRepetition == Constants.ONE_INT)
                intervalLength = SINGLE_DAY;
            else
                intervalLength = intervalLength * difficulty;
            if (predictVars.Process_Gen_Sims_Studied == true)
                genSimsStudied[predictVars.Gen_Studied_Index].Interval_Length = intervalLength;
            else
                genSimsAll[predictVars.Gen_Projected_Index].Interval_Length = intervalLength;
        }
        private static void SimProcessDate()
        {
            const double SINGLE_DAY = 1440;
            double intervalLength;
            double daysDouble;
            //int daysInt;
            DateTime fakeToday;
            DateTime nextDate;
            string nextDateString;
            //DateTime fakeNewDay;



            if (predictVars.Process_Gen_Sims_Studied == true)
            {
                intervalLength = genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Interval_Length;
                daysDouble = intervalLength / SINGLE_DAY; //Necessary to cut off fractional portion without rounding, so cant convert to Int32 yet.
                fakeToday = DateTime.Parse(predictVars.Gen_Sims_Studied_Date);
                genSimsStudied[predictVars.Gen_Studied_Index].Simulated_Date = fakeToday.ToString("d");
            }
            else
            {
                intervalLength = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Interval_Length;
                daysDouble = intervalLength / SINGLE_DAY;  //Necessary to cut off fractional portion without rounding, so cant convert to Int32 yet.
                fakeToday = DateTime.Parse(predictVars.Sim_Date_Use);
            }
            //fakeNewDay = fakeToday; //Take the current fake day to increment the fake day later
            //daysInt = Convert.ToInt32(daysDouble); //Necessary for AddDays function
            //daysDouble = Convert.ToInt32(daysInt); //Necessary for AddDays function
            nextDate = fakeToday.AddDays(daysDouble);
            nextDateString = nextDate.ToString("d");
            
            if (predictVars.Process_Gen_Sims_Studied == true)
                predictVars.Gen_Sims_Studied_Date = nextDateString;
            else
            {
                // This bracket is for debugging. Only keep genSimsAll here after debugging.
                genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Next_Date = nextDateString;
                predictVars.debugTopic = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Top_Number;
                predictVars.Prediction_Date = nextDateString;
                // Console.WriteLine($"DELETEME - inside last function\nPrediction Date = {predictVars.Prediction_Date}");
                // Console.ReadLine();
                // debugBool = true;
            }
            // fakeNewDay = fakeNewDay.AddDays(Constants.ONE_INT); // Increment the fake day now
            // predictVars.Sim_Date_Use = fakeNewDay.ToString("d");


            // //DELETEME - Debug stuff under here
            // if (debugBool == true)
            // {
            //     Console.WriteLine("Did Prediction Date display?");
            //     Console.ReadLine();
            // }
            
        }

        /****************MODIFIED VERSION FOR JUST INITIALIZATION******************************/


        private static void SimCalculateLearning()
        {
            if (predictVars.Process_Gen_Sims_Studied == false)
                SimAddRepetition();
            SimIntervalTime();
            SimProcessDate();
        }
        private static void SimAddRepetition()
        {
            Console.WriteLine($"DELETEME - is repetition increasing? genSimsAll[{predictVars.Gen_Projected_Index}].Sim_Repetition = {genSimsAll[predictVars.Gen_Projected_Index].Sim_Repetition}");
            ++genSimsAll[predictVars.Gen_Projected_Index].Sim_Repetition;
        }
        private static void SimIntervalTime()
        {
            const double SINGLE_DAY = 1440; // 1440 is the quatity in minutes of a day. I'm using minutes, instead of whole days, to be more precise.
            double difficulty;
            int ithRepetition;
            double intervalLength;

            if (predictVars.Process_Gen_Sims_Studied == true)
            {
                difficulty = genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Top_Difficulty;
                ithRepetition = genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Sim_Repetition;
                intervalLength = genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Interval_Length;
            }
            else
            {
                difficulty = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Top_Difficulty;
                ithRepetition = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Sim_Repetition;
                intervalLength = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Interval_Length;
            }
            if (ithRepetition == Constants.ONE_INT)
                intervalLength = SINGLE_DAY;
            else
                intervalLength = intervalLength * difficulty;
            if (predictVars.Process_Gen_Sims_Studied == true)
                genSimsStudied[predictVars.Gen_Studied_Index].Interval_Length = intervalLength;
            else
                genSimsAll[predictVars.Gen_Projected_Index].Interval_Length = intervalLength;
        }
        private static void SimProcessDate()
        {
            const double SINGLE_DAY = 1440;
            double intervalLength;
            double daysDouble;
            //int daysInt;
            DateTime fakeToday;
            DateTime nextDate;
            string nextDateString;
            //DateTime fakeNewDay;



            if (predictVars.Process_Gen_Sims_Studied == true)
            {
                intervalLength = genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Interval_Length;
                daysDouble = intervalLength / SINGLE_DAY; //Necessary to cut off fractional portion without rounding, so cant convert to Int32 yet.
                fakeToday = DateTime.Parse(genSimsStudied[predictVars.Gen_Studied_Index].Simulated_Date);
                // genSimsStudied now gets simulated date from previous study calculation, or from
                // initialization if it is the first repetition.
                // genSimsStudied[predictVars.Gen_Studied_Index].Simulated_Date = fakeToday.ToString("d");
            }
            else
            {
                intervalLength = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Interval_Length;
                daysDouble = intervalLength / SINGLE_DAY;  //Necessary to cut off fractional portion without rounding, so cant convert to Int32 yet.
                fakeToday = DateTime.Parse(predictVars.Sim_Date_Use);
            }
            nextDate = fakeToday.AddDays(daysDouble);
            nextDateString = nextDate.ToString("d");

            int indexFuture = predictVars.Gen_Studied_Index + Constants.ONE_INT;
            if (predictVars.Process_Gen_Sims_Studied == true)
            {
                genSimsStudied[predictVars.Gen_Studied_Index].Next_Date = nextDateString;
                if (indexFuture < genSimsStudied.Count)
                    if (genSimsStudied[predictVars.Gen_Studied_Index].Top_Number == genSimsStudied[indexFuture].Top_Number)
                        genSimsStudied[indexFuture].Simulated_Date = nextDateString;
            }
            else
            {
                // This bracket is for debugging. Only keep genSimsAll here after debugging.
                genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Next_Date = nextDateString;
                predictVars.debugTopic = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Top_Number;
                predictVars.Prediction_Date = nextDateString;
                // Console.WriteLine($"DELETEME - inside last function\nPrediction Date = {predictVars.Prediction_Date}");
                // Console.ReadLine();
                // debugBool = true;
            }
            // fakeNewDay = fakeNewDay.AddDays(Constants.ONE_INT); // Increment the fake day now
            // predictVars.Sim_Date_Use = fakeNewDay.ToString("d");


            // //DELETEME - Debug stuff under here
            // if (debugBool == true)
            // {
            //     Console.WriteLine("Did Prediction Date display?");
            //     Console.ReadLine();
            // }
            
        }