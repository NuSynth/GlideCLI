/****************************************************************
                        scratchPaper.cs
This file is normally used to design components before
putting them into the program.
****************************************************************/

        private static void GoalSetter()
        {
        	int currentX = (int)predictVars.Current_X;
        	int currentY = predictVars.Current_Y;
        	int countDown = currentX + currentY;

        	if (currentY > globals.newLeft)
			    currentY = globals.newLeft;

            if (predictVars.Lock_Initial == false && predictVars.Unlock_New_Date == false)
                    predictVars.Until_New = countDown;

            if (predictVars.Until_New == Constants.ZERO_INT)
                predictVars.Unlock_New_Date = true;
            else
                predictVars.Unlock_New_Date = false;

            if (predictVars.Unlock_New_Date == false)
            {
                if (predictVars.Lock_Initial == false)
                {
                    predictVars.Lock_Initial = true;
                    predictVars.Prediction_Date = genSimsAll[predictVars.Gen_Projected_Index].Next_Date;
                    predictVars.Final_Topic = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Top_Number;
                }
            }
            if (predictVars.Unlock_New_Date == true)
            {
                predictVars.Until_New = countDown;
                predictVars.Prediction_Date = genSimsAll[predictVars.Gen_Projected_Index].Next_Date;
                predictVars.Final_Topic = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Top_Number;
            }

        }


        /****************MODIFIED VERSION******************************/


        private static void GoalSetter()
        {
        	int currentX = (int)predictVars.Current_X;
        	int currentY = predictVars.Current_Y;
        	int countDown = currentX + currentY;

            ++predictVars.debugCount;
        	if (currentY > globals.newLeft)
			    currentY = globals.newLeft;

            //DELETEME START
            if (currentY > globals.newLeft)
                predictVars.debugFunk = true;
            Console.WriteLine($"\ndebugCount = {predictVars.debugCount}");
            Console.WriteLine($"genSimsAll.Count = {genSimsAll.Count}");
            Console.WriteLine($"debugFunk = {predictVars.debugFunk}");
            Console.WriteLine($"currentX = {currentX}");
            Console.WriteLine($"currentY = {currentY}");
            Console.WriteLine($"countDown = {countDown}\n");
            //DELETEME END

            if (predictVars.Until_New == Constants.ZERO_INT)
            {
                predictVars.Until_New = countDown;
                predictVars.Prediction_Date = genSimsAll[predictVars.Gen_Projected_Index].Next_Date;
                predictVars.Final_Topic = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Top_Number;
            }


            // if (predictVars.Until_New == Constants.ZERO_INT)
            //     predictVars.Unlock_New_Date = true;
            // else
            //     predictVars.Unlock_New_Date = false;

            // if (predictVars.Unlock_New_Date == false)
            // {
            //     if (predictVars.Lock_Initial == false)
            //     {
            //         predictVars.Lock_Initial = true;
            //         predictVars.Prediction_Date = genSimsAll[predictVars.Gen_Projected_Index].Next_Date;
            //         predictVars.Final_Topic = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Top_Number;
            //     }
            // }
            // if (predictVars.Unlock_New_Date == true)
            // {
            //     predictVars.Until_New = countDown;
            //     predictVars.Prediction_Date = genSimsAll[predictVars.Gen_Projected_Index].Next_Date;
            //     predictVars.Final_Topic = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Top_Number;
            // }

        }





        /**************************************PROBLEM SEPARATOR******************************************************************/


        // Right before GoalSetter is called:

        // predictVars.CurrentX needs to be set == to lateLeft + currentLeft
        // Then FindYatX needs to be called.
        // Then GoalSetter

predictVars.Sim_Date_Use