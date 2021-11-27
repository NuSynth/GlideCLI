/****************************************************************
                        scratchPaper.cs
This file is normally used to design components before
putting them into the program.
****************************************************************/

        private static void CollectStudyX()
        {
            
            DateTime useDate, nextDate;
            int dateCompare;
            useDate = DateTime.Parse(predictVars.Sim_Date_Use);

            // Console.WriteLine("Inside CollectStudyX");
            // Console.WriteLine($"DELETEME studiedSimList.Count = {studiedSimList.Count}");
            // Console.ReadLine();

            
            // Get 2nd rep studies
            int index, repCheck;
            index = repCheck = Constants.ZERO_INT;
            foreach (var topic in studiedSimList)
            {
                nextDate = DateTime.Parse(topic.Next_Date);
                dateCompare = DateTime.Compare(nextDate, useDate);

                if (dateCompare <= Constants.ZERO_INT && topic.Real_Repetition == Constants.TWO_INT)
                    if (repCheck < predictVars.X_High_Xcount)
                    {
                        studyRepElements.Add(index);
                        ++repCheck;
                    }
                ++index;
            }

            // Get Late
            index = Constants.ZERO_INT;
            foreach (var topic in studiedSimList)
            {
                nextDate = DateTime.Parse(topic.Next_Date);
                dateCompare = DateTime.Compare(nextDate, useDate);

                if (dateCompare < Constants.ZERO_INT && topic.Real_Repetition != Constants.TWO_INT)
                    if (repCheck < predictVars.X_High_Xcount)
                    {
                        studyRepElements.Add(index);
                        ++repCheck;
                    }
                ++index;
            }
            
            //Get On-Time
            index = Constants.ZERO_INT;
            foreach (var topic in studiedSimList)
            {
                nextDate = DateTime.Parse(topic.Next_Date);
                dateCompare = DateTime.Compare(nextDate, useDate);

                if (dateCompare == Constants.ZERO_INT && topic.Real_Repetition != Constants.TWO_INT)
                    if (repCheck < predictVars.X_High_Xcount)
                    {
                        studyRepElements.Add(index);
                        ++repCheck;
                    }
                ++index;
            }
            predictVars.Current_X = repCheck;
        }

        /****************MODIFIED VERSION******************************/


        private static void CollectStudyX()
        {
            
            DateTime useDate, nextDate;
            int dateCompare;
            useDate = DateTime.Parse(predictVars.Sim_Date_Use);

            // Console.WriteLine("Inside CollectStudyX");
            // Console.WriteLine($"DELETEME studiedSimList.Count = {studiedSimList.Count}");
            // Console.ReadLine();

            
            // Get 2nd rep studies
            int index, repCheck;
            index = repCheck = Constants.ZERO_INT;
            foreach (var topic in genSimsAll)
            {
                nextDate = DateTime.Parse(topic.Next_Date);
                dateCompare = DateTime.Compare(nextDate, useDate);

                if (dateCompare <= Constants.ZERO_INT && topic.Real_Repetition == Constants.TWO_INT)
                    if (repCheck < predictVars.X_High_Xcount)
                    {
                        studyRepElements.Add(index);
                        ++repCheck;
                    }
                ++index;
            }

            // Get Late
            index = Constants.ZERO_INT;
            foreach (var topic in genSimsAll)
            {
                nextDate = DateTime.Parse(topic.Next_Date);
                dateCompare = DateTime.Compare(nextDate, useDate);

                if (dateCompare < Constants.ZERO_INT && topic.Real_Repetition != Constants.TWO_INT)
                    if (repCheck < predictVars.X_High_Xcount)
                    {
                        studyRepElements.Add(index);
                        ++repCheck;
                    }
                ++index;
            }
            
            //Get On-Time
            index = Constants.ZERO_INT;
            foreach (var topic in genSimsAll)
            {
                nextDate = DateTime.Parse(topic.Next_Date);
                dateCompare = DateTime.Compare(nextDate, useDate);

                if (dateCompare == Constants.ZERO_INT && topic.Real_Repetition != Constants.TWO_INT)
                    if (repCheck < predictVars.X_High_Xcount)
                    {
                        studyRepElements.Add(index);
                        ++repCheck;
                    }
                ++index;
            }
            predictVars.Current_X = repCheck;
        }