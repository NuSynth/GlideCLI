/****************************************************************
                        scratchPaper.cs
This file is normally used to design components before
putting them into the program.
****************************************************************/


// I was pretty tired when I wrote a lot of this.
// Go through and correct some of the bad logic.

private static void PredictLast()
{
    predictVars.Process_Prediction = true;
    // Use actual difficulties for studied topics
    // But get average of real difficulties to use 
    // for non-studied topics for them to be simulated
    AvgDiff();
    
    /*****************/
    // These will be used to plot line of points (x1 , y1) and (x2 , y2)
    // XY correspond to new topics, and repeat topics
    
    

    // Point Ymax is (x1 , y1)
    // For point (x1 , y1), where y1 is maximum first studies performed
    // x1 is number of repeat studies performed at max first studies
    CollectFirstStudies();      // Get first study dates of studied topics
    YmaxFirsts();               // Get y1: max Y First Studies for point Ymax
    SimulatePastStudies();      // Produce real repeats
    YmaxRepeats();              // Get x1: max X Repeat Studies for point Ymax

    // Since all the past study sessions are simulated and collected 
    // already, we just need to get the XmaxRepeats and XmaxFirsts.

    // Point Xmax (x2 , y2)
    // For point (x2 , y2), where x2 is maximum  repeat studies performed
    // y2 is number of first studies performed at max repeat studies
    XmaxRepeats();
    XmaxFirsts();

    GenerateProjectedStudies();
    /*****************/

    // Make function to clear lists that need to be cleared.
    // Do not call it here.
    // Call it after each real repetition is performed
    // This will allow results to update in real-time
    studiedSimVars.Clear();
    genSimsStudied.Clear();
    predictVars.Process_Prediction = false;
}
private static void avgDiff()
{
    // Apply average of difficulty to simulation of 
    // calculating non-studied topics learning dates.
    double nDifficultsDouble = Constants.ZERO_DOUBLE;
    double difficultsAdded = Constants.ZERO_DOUBLE;
    predictVars.Avg_Difficulty = Constants.ZERO_DOUBLE;
    int count = Constants.ZERO_INT;
    foreach (vat topic in TopicsList)
    {
        if (topic.Top_Studied == true)
        {
            difficultsAdded += Convert.ToDouble(topic.Top_Difficulty);
            ++counts;
        }
    }
    nDifficultsDouble = Convert.ToDouble(counts);
    predictVars.Avg_Difficulty = difficultsAdded/nDifficultsDouble;
}
private static void CollectFirstStudies()
{
    SimModel newSims = new SimModel();
    int topicNumber = Constants.ZERO_INT;
    foreach (var topic in TopicsList)
        if (topic.Top_Studied == true)
        {
            newSims.First_Date = topic.First_Date;
            newSims.Real_Repetition = topic.Top_Repetition;
            newSims.Sim_Repetition = Constants.ZERO_INT;
            newSims.Top_Difficulty = topic.Top_Difficulty;
            newSims.Interval_Length = topic.Interval_Length;
            newSims.Top_Number = topicNumber; 
            studiedSimVars.Add(newSims);
            ++topicNumber;
        }
}
private static void YmaxFirsts()
{
    List<string> fStudyDates = new List<string>();
    List<int> fStudyCounts = new List<int>();
    bool firstCheck = true;
    int index = Constants.ZERO_INT;
    int count = Constants.ONE_INT;
    int dateCompare;
    DateTime tempDateOne;
    DateTime tempDateTwo;    


    foreach (var topic in studiedSimVars)
    {
        if (firstCheck == true)
        {
            fStudyDates.Add($"{topic.First_Date}");
            fStudyCounts.Add(count);
            firstCheck = false;
        }
        else
        {
            tempDateOne = DateTime.Parse(fStudyDates.ElementAt(index));
            tempDateTwo = DateTime.Parse(topic.First_Date);
            dateCompare = DateTime.Compare(tempDateOne, tempDateTwo);

            if (dateCompare == Constants.ZERO_INT)
                ++fStudyCounts.ElementAt(index);
            else
            {
                ++index;
                fStudyDates.Add($"{topic.First_Date}");
                fStudyCounts.Add(count);
            }            
        }        
    }
    
    /******************
    Sorting Section Start
    ******************/
    int j, i, keyOne;
    string keyTwo;

    i = Constants.ZERO_INT;
    j = Constants.ONE_INT;
    keyOne = Constants.ZERO_INT;
    count = Constants.ZERO_INT;
    while (count < Constants.TWO_INT)
    {  
        
        for (j = Constants.TWO_INT; j < fStudyCounts.Count; ++j)
        {
            keyOne = fStudyCounts.ElementAt(j);
            keyTwo = fStudyDates.ElementAt(j);

            // Insert fStudyCounts[j] into sorted sequence fStudyCounts[1...j-1]
            // Insert fStudyDates[j] into sorted sequence fStudyDates[1...j-1]
            i = j - Constants.ONE_INT;
            while (i > Constants.ONE_INT && fStudyCounts.ElementAt(i) > keyOne)
            {
                fStudyCounts.ElementAt(i + Constants.ONE_INT) = fStudyCounts.ElementAt(i);
                fStudyDates.ElementAt(i + Constants.ONE_INT) = fStudyDates.ElementAt(i);
                i = i - Constants.ONE_INT;
            }
            fStudyCounts.ElementAt(i + Constants.ONE_INT) = keyOne;
            fStudyDates.ElementAt(i + Constants.ONE_INT) = keyTwo;
        }

        /* 
           this is here to get the first element sorted into 
           the rest of the array on the second run of the loop
        */
        if (count == ZERO)
        {
            //key = A[ZERO];
            keyOne = fStudyCounts.ElementAt(Constants.ZERO_INT);
            keyTwo = fStudyDates.ElementAt(Constants.ZERO_INT);
            //A[ZERO] = A[ONE];
            fStudyCounts.ElementAt(Constants.ZERO_INT) = fStudyCounts.ElementAt(Constants.ONE_INT);
            fStudyDates.ElementAt(Constants.ZERO_INT) = fStudyDates.ElementAt(Constants.ONE_INT);
            //A[ONE] = key;
            fStudyCounts.ElementAt(Constants.ONE_INT) = keyOne;
            fStudyDates.ElementAt(Constants.ONE_INT) = keyTwo;
        }
        ++count;
    }
    /******************
    Sorting Section End
    ******************/


    // Each of the dates with the equally highest number of 
    // first topics studied needs to be used, so that the 
    // one with the highest number of repetitions also performed 
    // can be used for Y_High_Xcount Since this method does not 
    // count previously studied repetitions, then the dates just 
    // need to be passed to a list instead of the first single 
    // date with the highest Y count being passed to a variable.
    
    List<int> elementList = new List<int>();
    int check = Constants.ZERO_INT;
    index = fStudyCounts.Count - Constants.ONE_INT;
    firstCheck = true;
    while (index > Constants.ZERO_INT)
    {
        if (firstCheck == true)
        {
            firstCheck = false;
            elementList.Add(index);
            check = fStudyCounts.ElementAt(index);
        }
        else if ((index - Constants.ONE_INT) >= Constants.ZERO_INT)
            if (check == fStudyCounts.ElementAt(index - Constants.ONE_INT))
                elementList.Add(index - Constants.ONE_INT);
        --index;
    }

    // Add all highest dates to list. Date with highest X-value
    // will be selected from the yMaxList later.
    List<PointLimits> tempElements = new List<PointLimits>();
    index = Constants.ZERO_INT;
    while (index < elementList.Count)
    {
        tempElements.High_Date = fStudyDates.ElementAt(elementList.ElementAt(index));
        tempElements.Y_Count = fStudyCounts.ElementAt(elementList(index));
        
        yMaxList.Add(tempElements);
        ++index;
    }
}
private static void SimulatePastStudies()
{
    SimModel studiedSims = new SimModel();
    bool firstRep = true;
    string repetitionDate = " ";
    int repetitionIndex = Constants.ZERO_INT;
    predictVars.Gen_Studied_Index = Constants.ZERO_INT;

    foreach (var topic in studiedSimVars)
    {
        while (repetitionIndex < topic.Real_Repetition)
        {
            studiedSims.First_Date = topic.First_Date;
            studiedSims.Real_Repetition = topic.Real_Repetition;
            if (firstRep == true)
            {
                studiedSims.Repetition_Date = topic.First_Date;
                studiedSims.Sim_Repetition = Constants.ZERO_INT;
                firstRep = false;
            }
            else
            {
                studiedSims.Repetition_Date = genSimsStudied.ElementAt(predictVars.Gen_Studied_Index - Constants.ONE).Next_Date;
            }
            studiedSims.Top_Difficulty = topic.Top_Difficulty;
            studiedSims.Top_Number = topic.Top_Number; 
            genSimsStudied.Add(studiedSims);

            predictVars.Process_Gen_Sims_Studied = true;
            SimCalculateLearning();
            predictVars.Process_Gen_Sims_Studied = false;

            ++predictVars.Gen_Studied_Index;
            repetitionIndex = studiedSims.Sim_Repetition;
        }
        repetitionIndex = Constants.ZERO_INT;
        firstRep = true;
    }
    predictVars.Gen_Studied_Index = Constants.ZERO_INT;
}
private static void YmaxRepeats()
{
    // Use the date of highest first studies, 
    // with the highest number of repetitions occuring on that first date
    // check all non-first repetions in a loop
    // If date == Y_High_Date, Then Add 1 to Y_High_Xcount
    // Do this until there are no more elements
    DateTime topicDate;
    DateTime yMaxDate;      // There can exist more than one date with same Y-value
    DateTime dateCompare;
    int index = Constants.ZERO_INT;

/*Maybe use modification of this to get xMaxList values*/
    while (index < yMaxList.Count)
    {
        yMaxDate = yMaxList.ElementAt(index).High_Date;
        foreach (var topic in genSimsStudied)
        {
            topicDate =  DateTime.Parse(topic.Repetition_Date);
            dateCompare = DateTime.Compare(topicDate, yMaxDate);
            if (topic.Sim_Repetition > Constants.ONE_INT && dateCompare == Constants.ZERO_INT)
                ++yMaxList.ElementAt(index).X_Count;
        }
        ++index;
    }

    double check = Constants.ZERO_INT;
    int useable = Canstants.ZERO_INT;
    index = Constants.ZERO_INT;
    check = yMaxList.ElementAt(index).X_Count;
    while (index < yMaxList.Count)
    {
        if ((index + Constants.ONE_INT) < yMaxList.Count)
            if (check < yMaxList.ElementAt(index + Constants.ONE_INT).X_Count)
            {
                check = yMaxList.ElementAt(index + Constants.ONE_INT).X_Count;
                useable = index + Constants.ONE_INT;
            }
        ++index;
    }
    predictVars.Y_High_Ycount = yMaxList.ElementAt(useable).Y_Count;
    predictVars.Y_High_Xcount = yMaxList.ElementAt(useable).X_Count;
}
private static void XmaxRepeats()
{
    // I have to make a copy of genSimsStudied
    List<SimModel> studiedSims = new List<SimModel>();

    studiedSims.Add(genSimsStudied);
    xMaxSortList.Add(studiedSims);
    XmaxRepeatSort();
    studiedSims.Clear();
    studiedSims.Add(xMaxSortList);
    xMaxSortList.Clear();

    //Get X
    List<string> tempDates = new List<string>();
    List<int> dateCounts = new List<int>();
    int index = Constants.ZERO_INT;
    int firstRep = Constants.ONE_INT;
    bool firstCheck = true;

    // int32 something = DateTime.Compare(t1 , t2);
    // if something < zero, then t1 is earlier than t2
    // if something == zero, then same day
    // if something > zero, then t1 is later than t2
    foreach (var section in studiedSims)
    {
        if (firstCheck == true)
        {
            if (firstRep != section.Sim_Repetition)
            {
                dateCounts.Add(Constants.ZERO_INT);
                tempDates.Add(section.Repetition_Date);
                firstCheck = false;
            }
        }
        if (firstCheck == false) // Must not be an else for logic to work
        {
            if (firstRep != section.Sim_Repetition)
            {
                if (tempDates.ElementAt(index) == section.Repetition_Date)
                    ++dateCounts.ElementAt(index);
                else
                {
                    ++index;
                    tempDates.Add(section.Repetition_Date);
                    dateCounts.Add(Constants.ONE_INT);
                }
            }
        }
    }

    // Each of the dates with the equally highest number of 
    // repeat topics studied needs to be used, so that the 
    // one with the highest number of first studies also performed 
    // can be used for X_High_Ycount Since this method does not 
    // count first study repetitions, then the dates just 
    // need to be passed to a list instead of the first single 
    // date with the highest X count being passed to a variable.
    
    List<int> elementList = new List<int>();
    int check = Constants.ZERO_INT;
    index = dateCounts.Count - Constants.ONE_INT;
    firstCheck = true;
    while (index > Constants.ZERO_INT)
    {
        if (firstCheck == true)
        {
            firstCheck = false;
            elementList.Add(index);
            check = dateCounts.ElementAt(index);
        }
        else if ((index - Constants.ONE_INT) >= Constants.ZERO_INT)
            if (check == dateCounts.ElementAt(index - Constants.ONE_INT))
                elementList.Add(index - Constants.ONE_INT);
        --index;
    }

    // Add all highest dates to list. Date with highest Y-intercept
    // will be selected from the xMaxList later.
    List<PointLimits> tempElements = new List<PointLimits>();
    index = Constants.ZERO_INT;
    while (index < elementList.Count)
    {
        tempElements.High_Date = tempDates.ElementAt(elementList.ElementAt(index));
        tempElements.X_Count = dateCounts.ElementAt(elementList(index));
        
        xMaxList.Add(tempElements);
        ++index;
    }
    
}
private static void XmaxRepeatSort()
{
    SimModel listKey = new SimModel();
    int j, i, dateCheck, count;

    i = Constants.ZERO_INT;
    j = Constants.ONE_INT;
    count = Constants.ZERO_INT;
    while (count < Constants.TWO_INT)
    {  
        
        for (j = Constants.TWO_INT; j < xMaxSortList.Count; ++j)
        {
            listKey = xMaxSortList.ElementAt(j);

            // Insert xMaxSortList[j] into sorted sequence xMaxSortList[1...j-1]
            i = j - Constants.ONE_INT;
            dateCheck = DateTime.Compare(xMaxSortList.ElementAt(i).Repetition_Date, listKey.Repetition_Date);
            while (i > Constants.ONE_INT && dateCheck > Constants.ZERO_INT)
            {
                xMaxSortList.ElementAt(i + Constants.ONE_INT) = xMaxSortList.ElementAt(i);
                i = i - Constants.ONE_INT;
            }
            xMaxSortList.ElementAt(i + Constants.ONE_INT) = listKey;
        }

        /* 
           this is here to get the first element sorted into 
           the rest of the array on the second run of the loop
        */
        if (count == ZERO)
        {
            //key = A[ZERO];
            listKey = xMaxSortList.ElementAt(Constants.ZERO_INT);
            //A[ZERO] = A[ONE];
            xMaxSortList.ElementAt(Constants.ZERO_INT) = xMaxSortList.ElementAt(Constants.ONE_INT);
            //A[ONE] = key;
            xMaxSortList.ElementAt(Constants.ONE_INT) = listKey;
        }
        ++count;
    }
}
private static void XmaxFirsts()
{
    // Use the date of highest repetition studies, 
    // with the highest number of first studies occuring on that repetition date
    // check all repetions in a loop
    // If date == X_High_Date, && Sim_Repetition == 1
    // Then Add 1 to X_High_Ycount
    // Do this until there are no more elements
    DateTime topicDate;
    DateTime xMaxDate;      // There can exist more than one date with same X-value
    DateTime dateCompare;
    int index = Constants.ZERO_INT;

    while (index < xMaxList.Count)
    {
        xMaxDate = xMaxList.ElementAt(index).High_Date;
        foreach (var topic in genSimsStudied)
        {
            topicDate =  DateTime.Parse(topic.Repetition_Date);
            dateCompare = DateTime.Compare(topicDate, xMaxDate);
            if (topic.Sim_Repetition == Constants.ONE_INT && dateCompare == Constants.ZERO_INT)
                ++xMaxList.ElementAt(index).Y_Count;
        }
        ++index;
    }

    double check = Constants.ZERO_INT;
    int useable = Canstants.ZERO_INT;
    index = Constants.ZERO_INT;
    check = xMaxList.ElementAt(index).Y_Count;
    while (index < xMaxList.Count)
    {
        if ((index + Constants.ONE_INT) < xMaxList.Count)
            if (check < xMaxList.ElementAt(index + Constants.ONE_INT).Y_Count)
            {
                check = xMaxList.ElementAt(index + Constants.ONE_INT).Y_Count;
                useable = index + Constants.ONE_INT;
            }
        ++index;
    }
    predictVars.X_High_Ycount = xMaxList.ElementAt(useable).Y_Count;
    predictVars.X_High_Xcount = xMaxList.ElementAt(useable).X_Count;
}

private static void CollectNonStudied()
{
    SimModel newSims = new SimModel();
    int index = studiedSimVars.Count - Constants.ONE_INT;
    int topicNumber = studiedSimVars.ElementAt(index).Top_Number + Constants.ONE_INT;
    foreach (var topic in TopicsList)
        if (topic.Top_Studied == false)
        {
            newSims.First_Date = topic.First_Date;
            newSims.Real_Repetition = topic.Top_Repetition;
            newSims.Sim_Repetition = Constants.ZERO_INT;
            newSims.Top_Difficulty = predictVars.Avg_Difficulty;
            newSims.Interval_Length = Constants.ZERO_INT;
            newSims.Top_Number = topicNumber; 
            projectedSimVars.Add(newSims);
            ++topicNumber;
        }
}




private static void GenerateProjectedStudies()
{
    DateTime startDate, previousDate, simDate;
    int totalTopics, count;
    startDate = DateTime.Now;
    predictVars.Sim_Date_Use = startDate.ToString("d");
    totalTopics = projectedSimVars.Count;
    count = Constants.ZERO_INT;

    predictVars.Process_Gen_Sims_Studied = false;
    while (count < totalTopics)
    {
        predictStudies();
        previousDate = DateTime.Parse(predictVars.Sim_Date_Use);
        simDate = previousDate.AddDays(Constants.ONE_INT);
        predictVars.Sim_Date_Use = simDate.ToString("d");
        totalTopics = projectedSimVars.Count;
    }
    predictVars.Process_Gen_Sims_Studied = true;
}
private static void predictStudies()
{
    
}

/**************SimulateDates**************/
private static void SimCalculateLearning()
{
    SimAddRepetition();
    SimIntervalTime();
    SimProcessDate();
}
private static void SimAddRepetition()
{
    if (predictVars.Process_Gen_Sims_Studied == true)
        ++genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Sim_Repetition;
    else
        ++genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Sim_Repetition;
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


    //     Second repetition will occur the next day. 
    //	   Although, the research document does not state
    //	   a time frame until the second repetition. The 
    //	   values of the two variables may need to be changed, 
    //	   if the spacing is too far apart. So far they seem fine.
    if (ithRepetition == Constants.ONE_INT)
    {
        intervalLength = SINGLE_DAY;
    }
    else
        intervalLength = intervalLength * difficulty;
    if (predictVars.Process_Gen_Sims_Studied == true)
        genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Interval_Length = intervalLength;
    else
        genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Interval_Length = intervalLength;
}
private static void SimProcessDate()
{
    const double SINGLE_DAY = 1440;
    double intervalLength;
    double days;
    DateTime fakeToday;
    DateTime nextDate;
    string nextDateString;

    if (predictVars.Process_Gen_Sims_Studied == true)
    {
        intervalLength = genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Interval_Length;
        days = Convert.ToInt32(intervalLength / SINGLE_DAY);
        fakeToday = DateTime.Parse(genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Repetition_Date);
        nextDate = fakeToday.AddDays(days);
        nextDateString = nextDate.ToString("d");
    }
    else
    {
        intervalLength = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Interval_Length;
        days = Convert.ToInt32(intervalLength / SINGLE_DAY);
        fakeToday = DateTime.Parse(genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Repetition_Date);
        nextDate = fakeToday.AddDays(days);
        nextDateString = nextDate.ToString("d");
    }
    
    if (predictVars.Process_Gen_Sims_Studied == true)
        genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Next_Date = nextDateString;
    else
        genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Next_Date = nextDateString;
}