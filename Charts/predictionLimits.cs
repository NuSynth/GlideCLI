        private static void ProcessDate()
        {
            const double SINGLE_DAY = 1440;
            double intervalLength = TopicsList.ElementAt(globals.TopicID).Interval_Length;
            double days = Convert.ToInt32(intervalLength / SINGLE_DAY);
            DateTime today = DateTime.Parse(globals.TheDate);
            DateTime nextDate = today.AddDays(days);
            string nextDateString = nextDate.ToString("d");
            TopicsList.ElementAt(globals.TopicID).Next_Date = nextDateString;
            if (TopicsList.ElementAt(globals.TopicID).Top_Studied == false)
                TopicsList.ElementAt(globals.TopicID).Top_Studied = true;
        }

        private static void SimProcessDate()
        {
            const double SINGLE_DAY = 1440;
            double intervalLength;
            double days;
            DateTime fakeToday = predictVars.Sim_Date_Use;
            DateTime nextDate = fakeToday.AddDays(days);
            string nextDateString;


            if (predictVars.Process_Gen_Sims_Studied == true)
            {
                intervalLength = genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Interval_Length;
                days = (int)(intervalLength / SINGLE_DAY);
                fakeToday = DateTime.Parse(genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Repetition_Date);
                nextDate = fakeToday.AddDays(days);
                nextDateString = nextDate.ToString("d");
            }
            else
            {
                Console.WriteLine($"DELETEME genSimsAll.Count = {genSimsAll.Count}");
                Console.ReadLine();

                intervalLength = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Interval_Length;
                days = (int)(intervalLength / SINGLE_DAY);
                nextDateString = nextDate.ToString("d");
            }
            
            if (predictVars.Process_Gen_Sims_Studied == true)
                genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Next_Date = nextDateString;
            else
            {
                // This bracket is for debugging. Only keep genSimsAll here after debugging.
                genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Next_Date = nextDateString;
                predictVars.debugTopic = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Top_Number;
                predictVars.Prediction_Date = nextDateString;
                Console.WriteLine($"DELETEME - inside last function\nPrediction Date = {predictVars.Prediction_Date}");
                Console.ReadLine();
            }
            
        }