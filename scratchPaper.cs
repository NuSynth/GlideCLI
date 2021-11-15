/****************************************************************
                        scratchPaper.cs
This file is normally used to design components before
putting them into the program.
****************************************************************/
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

    GenerateProjectedS);
    /*****************/

    // Make function to clear lists that need to be cleared.
    // Do not call it here.
    // Call it after each real repetition is performed
    // This will allow results to update in real-time
    simVars.Clear();
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
            newSims.Real_Repetition = topic.Top_Repetition; // Stop simulating studied at current repetition
            newSims.Sim_Repetition = Constants.ZERO_INT;
            newSims.Top_Difficulty = topic.Top_Difficulty;
            newSims.Interval_Length = topic.Interval_Length;
            newSims.Interval_Remaining = topic.Interval_Remaining;
            newSims.Top_Number = topicNumber; 
            simVars.Add(newSims);
            ++topicNumber;
        }
}
private static void YmaxFirsts()
{
    bool firstCheck = true;
    int index = Constants.ZERO_INT;
    int count = Constants.ONE_INT;
    int dateCompare;
    DateTime tempDateOne;
    DateTime tempDateTwo;    
    List<string> fStudyDates = new List<string>();
    List<int> fStudyCounts = new List<int>();
    foreach (var topic in simVars)
    {
        if (firstCheck == true)
        {
            fStudyDates.Add($"{topic.First_Date}");
            fStudyCounts.Add(count);
            firstCheck = false;
        }
        else if(firstCheck == false)
        {
            tempDateOne = DateTime.Parse(fStudyDates.ElementAt(index - Constants.ONE_INT));
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
        
        for (j = 2; j < fStudyCounts.Count; ++j)
        {
            keyOne = fStudyCounts.ElementAt(j);
            keyTwo = fStudyDates.ElementAt(j);

            // Insert fStudyCounts[j] into sorted sequence fStudyCounts[1...j-1]
            // Insert fStudyDates[j] into sorted sequence fStudyDates[1...j-1]
            i = j-Constants.ONE_INT;
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
    Convert.ToDouble(fStudyCounts.ElementAt(index));
    index = fStudyCounts.Count - Constants.ONE_INT;
    predictVars.Y_High_Date = fStudyDates.ElementAt(index);
    predictVars.Y_High_Ycount = Convert.ToDouble(fStudyCounts.ElementAt(index));

    //Test that it is higher
    if (predictVars.Y_High_Ycount > fStudyCounts.ElementAt(ZERO)){
        Console.WriteLine("Y_High_Ycount is higher. Test Passed");
        Console.ReadLine();
    }
    else
    {
        Console.WriteLine("Y_High_Ycount is higher. Test Failed");
        Console.ReadLine();
    }
}
private static void SimulatePastStudies()
{
    SimModel studiedSims = new SimModel();
    bool firstRep = true;
    string repetitionDate = " ";
    int repetitionIndex = Constants.ZERO_INT;
    predictVars.Gen_Studied_Index = Constants.ZERO_INT;

    foreach (var topic in simVars)
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
    // Use date of Y_High_Date.
    // check all non-first repetions in a loop
    // If date == Y_High_Date, Then Add 1 to Y_High_Xcount
    // Do this until there are no more elements
    DateTime topicDate;
    DateTime yMaxDate =  DateTime.Parse(predictVars.Y_High_Date);
    DateTime dateCompare;
    predictVars.Y_High_Xcount = Constants.ZERO_INT;
    if (studyVars.dateCompare == Constants.ZERO_INT)

    foreach (var topic in genSimsStudied)
    {
        topicDate =  DateTime.Parse(topic.Repetition_Date);
        dateCompare =  = DateTime.Compare(topicDate, yMaxDate);
        if (topic.Sim_Repetition > Constants.ONE_INT && dateCompare == Constants.ZERO_INT)
            ++predictVars.Y_High_Xcount;
    }
}
private static void XmaxRepeats()
{
    foreach (var topic in genSimsStudied)
    {
        topicDate =  DateTime.Parse(topic.Repetition_Date);
        dateCompare =  = DateTime.Compare(topicDate, yMaxDate);
        if (topic.Sim_Repetition > Constants.ONE_INT && dateCompare == Constants.ZERO_INT)
            ++predictVars.Y_High_Xcount;
    }
}
private static void XmaxFirsts()
{

}







private static void GenerateProjected()
{
    // Make sure I got (x2 , y2)

    // Use Avg_Difficulty

    // generate genSimsProjected list

    // Maximum amount of new topics that can be simulated being studied, 
    // depends on the number of repetitions that have to be performed for
    // the day.

    // Since we are finding the value of y, by using the value of x, and
    // since we have 2 points of data already, we can use the
    // slope-intercept formula:

    // get slope m
    // m = (y2 - y1)/(x2 - x1)

    // In order to get y for e
    // get y
    // y = mx + b


SimModel projectedDates = new SimModel();
List<int> yCounts = new List<int>();
List<int> xCounts = new List<int>();
    foreach (var topic in genSimsStudied)
    {
        if (topic.Real_Repetition == topic.Sim_Repetition)
        {
            // 
        }
    }



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
        ++genSimsProjected.ElementAt(predictVars.Gen_Projected_Index).Sim_Repetition;
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
        difficulty = genSimsProjected.ElementAt(predictVars.Gen_Projected_Index).Top_Difficulty;
        ithRepetition = genSimsProjected.ElementAt(predictVars.Gen_Projected_Index).Sim_Repetition;
        intervalLength = genSimsProjected.ElementAt(predictVars.Gen_Projected_Index).Interval_Length;
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
        genSimsProjected.ElementAt(predictVars.Gen_Projected_Index).Interval_Length = intervalLength;
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
        intervalLength = genSimsProjected.ElementAt(predictVars.Gen_Projected_Index).Interval_Length;
        days = Convert.ToInt32(intervalLength / SINGLE_DAY);
        fakeToday = DateTime.Parse(genSimsProjected.ElementAt(predictVars.Gen_Projected_Index).Repetition_Date);
        nextDate = fakeToday.AddDays(days);
        nextDateString = nextDate.ToString("d");
    }
    
    if (predictVars.Process_Gen_Sims_Studied == true)
        genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Next_Date = nextDateString;
    else
        genSimsProjected.ElementAt(predictVars.Gen_Projected_Index).Next_Date = nextDateString;
}