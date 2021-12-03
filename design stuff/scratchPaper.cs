/****************************************************************
                        scratchPaper.cs
This file is normally used to design components before
putting them into the program.
****************************************************************/

        private static void StudyHUD()
        {
            Console.Clear();
            if (TopicsList.ElementAt(globals.TopicID).Top_Studied == true)
                studyVars.topStudBool = "true";
            else
                studyVars.topStudBool = "false";
            Console.WriteLine($"Course Name: {globals.CourseName}");
            if (globals.newLeft > Constants.ZERO_INT)
                Console.WriteLine($"Course Completion Expected: Section = {predictVars.Final_Topic} Date = {predictVars.Prediction_Date}");
            Console.WriteLine($"Section: {TopicsList.ElementAt(globals.TopicID).Top_Name}");
            Console.WriteLine($"Previously Studied: {studyVars.topStudBool}");
            Console.WriteLine($"Number of LATE practice to review: {globals.lateLeft}");
            Console.WriteLine($"Number of ON-TIME practice topics to review: {globals.currentLeft}");
            Console.WriteLine($"Number of NEW topics left: {globals.newLeft}");
            Console.WriteLine($"\n\nNumber of questions/problems: {TopicsList.ElementAt(globals.TopicID).Num_Problems}");
            Console.WriteLine("\nOPTIONS:");
            Console.WriteLine("(m) = Main Menu.");
            if (globals.lateLeft > Constants.ZERO_INT || globals.currentLeft > Constants.ZERO_INT)
                Console.WriteLine("(enter key) = Process topic, and go to next");
            else
                Console.WriteLine("(u) = Update number of questions.");
        }


        /****************MODIFIED VERSION******************************/

        private static void StudyHUD()
        {
            Console.Clear();
            if (TopicsList.ElementAt(globals.TopicID).Top_Studied == true)
                studyVars.topStudBool = "true";
            else
                studyVars.topStudBool = "false";
            Console.WriteLine($"Course Name: {globals.CourseName}");


            if (predictVars.Enough_Studied == true)
            {
		// Display Initial Goal Date IF End_Reached == FALSE AND Unlock_New_Date == FALSE
		if (predictVars.End_Reached == false && predictVars.Unlock_New_Date == false)
		{
			// Display Number of topics left to reach Initial Goal Date
			// Initial_Prediction_Date
		}
		// Display New Goal Date IF End_Reached == FALSE AND Unlock_New_Date == TRUE
		if (predictVars.End_Reached == false && predictVars.Unlock_New_Date == true)
		{
			// Display New Goal Date
			// New_Prediction_Date
		}
		// Display No_Date IF End_Reached == TRUE
		if (predictVars.End_Reached == true)
		{
			// Display "Maintenence study"
		}
            }
            // if (predictVars.Enough_Studied == true && predictVars.End_Reached == false);
            	//if (globals.newLeft > Constants.ZERO_INT)
                	//Console.WriteLine($"Course Completion Expected: Section = {predictVars.Final_Topic} Date = {predictVars.Prediction_Date}");
            Console.WriteLine($"Section: {TopicsList.ElementAt(globals.TopicID).Top_Name}");
            Console.WriteLine($"Previously Studied: {studyVars.topStudBool}");
            Console.WriteLine($"Number of LATE practice to review: {globals.lateLeft}");
            Console.WriteLine($"Number of ON-TIME practice topics to review: {globals.currentLeft}");
            Console.WriteLine($"Number of NEW topics left: {globals.newLeft}");
            Console.WriteLine($"\n\nNumber of questions/problems: {TopicsList.ElementAt(globals.TopicID).Num_Problems}");
            Console.WriteLine("\nOPTIONS:");
            Console.WriteLine("(m) = Main Menu.");
            if (globals.lateLeft > Constants.ZERO_INT || globals.currentLeft > Constants.ZERO_INT)
                Console.WriteLine("(enter key) = Process topic, and go to next");
            else
                Console.WriteLine("(u) = Update number of questions.");
        }


	IF predictVars.Enough_Studied == TRUE

	{

		Display Initial Goal Date IF End_Reached == FALSE AND Unlock_New_Date == FALSE

		Display New Goal Date IF End_Reached == FALSE AND Unlock_New_Date == TRUE

		Display No_Date IF End_Reached == TRUE
        }
