using GlideCLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace GlideCLI
{
    public class Program
    {
        static List<int> ToStudy = new List<int>();
        static List<TopicModel> TopicsList = new List<TopicModel>();
        static List<TopicModel> topics = new List<TopicModel>();
        static Globals globals = new Globals();
        static CreationModel creationVars = new CreationModel();
        static StudyModel studyVars = new StudyModel();
        
        /***********************************************/
        // Start of parts to predict date of last topic being studied
        static List<SimModel> studiedSimList = new List<SimModel>();
        static List<SimModel> projectedSimList = new List<SimModel>();
        static List<SimModel> genSimsStudied = new List<SimModel>();
        static List<SimModel> genSimsAll = new List<SimModel>();
        static List<string> fStudyDates = new List<string>();
        static List<int> fStudyCounts = new List<int>();
        // For Ymax END

        // For XmaxRepeats START
        static List<string> xRepTempDates = new List<string>();
        static List<int> xRepDateCounts = new List<int>();
        static List<SimModel> toXmax = new List<SimModel>();
        // For XmaxRepeats END

        //ReduceNew START
        static List<SimModel> reducedProjected = new List<SimModel>();
        //ReduceNew END
        static List<SimModel> xMaxSortList = new List<SimModel>();
        static List<int> studyRepElements = new List<int>();
        static PredictModel predictVars = new PredictModel();
        // End of parts to predict date of last topic being studied
        /***********************************************/

        public static void Main(string[] args)
        {
            //Console.Clear();
            GetTheDate();
            GetPath();
            StartUp();
        }
        // Get the date of the start of the study session
        private static void GetTheDate()
        {
            DateTime today = DateTime.Now;
            globals.TheDate = today.ToString("d");
        }
        private static void GetPath()
        {
            var userName = Environment.UserName;
            string pathString;
            pathString = ($"//home//{userName}//Documents");            
            if (Directory.Exists(pathString))
                Linux();
            else
                Windows();
        }
        private static void Linux()
        {
            var userName = Environment.UserName;
            string pathString = ($"//home//{userName}//Documents//GlideCLI");
            if (!Directory.Exists(pathString))
                Directory.CreateDirectory(pathString);
            globals.DirectoryPath = pathString;
            globals.osSwitch = true;
        }
        private static void Windows()
        {
            string directory = (@"\GlideCLI");
            string pathString = Convert.ToString(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + directory);
            string usablePath = pathString.Replace(@"\", @"\\");
            if (!Directory.Exists(usablePath))
                Directory.CreateDirectory(usablePath);
            globals.DirectoryPath = usablePath;
            globals.osSwitch = false;
        }
        private static void StartUp()
        {
            while (true)
            {
                StudyDecrementer();
                CheckForCountFile();
                MainMenu();
                StudyIncrementer();
                ClearLists();
            }            
        }
        private static void CheckForCountFile()
        {
            string filename;
            string path;
            string courseCount;
            if (globals.osSwitch == true)
                filename = "//CourseCount.txt";
            else
                filename = "\\CourseCount.txt";
            path = $"{globals.DirectoryPath}{filename}";
            try
            {
                courseCount = File.ReadAllText(path);
                globals.CourseCount = Convert.ToInt32(courseCount);
            }
            catch
            {
                File.WriteAllText(path, Constants.ZERO_STRING);
                courseCount = File.ReadAllText(path);
                globals.CourseCount = Convert.ToInt32(courseCount);
            }
        }
        private static void MainMenu()
        {
            //one if there are courses, zero if there are no courses
            if (globals.CourseCount > Constants.ZERO_INT)
                AvailableOptions(Constants.ONE_INT);
            else
                AvailableOptions(Constants.ZERO_INT);
        }
        private static void AvailableOptions(int options)
        {
            int selectionInt;
            string selectionString;
            
            globals.madeSelect = false;
            while (globals.madeSelect == false)
            {
                selectionInt = Constants.ZERO_INT; //To get rid of IDE warning.
                if (options == Constants.ZERO_INT)
                {
                    // Option set for no available courses
                    SelectionDialogs(Constants.ONE_INT);
                    try
                    {
                        selectionString = Console.ReadLine();
                        selectionInt = Convert.ToInt32(selectionString);
                    }
                    catch
                    {
                        Console.WriteLine("Invalid Input. Try again:");
                    }                    
                    if (selectionInt == Constants.ONE_INT || selectionInt == Constants.TWO_INT)
                        MainOptions(selectionInt);
                }
                if (options == Constants.ONE_INT)
                {
                    //Option set if courses are available
                    SelectionDialogs(Constants.TWO_INT);
                    try
                    {
                        selectionString = Console.ReadLine();
                        selectionInt = Convert.ToInt32(selectionString);
                    }
                    catch
                    {
                        Console.WriteLine("Invalid Input. Try again:");
                    }
                    if (selectionInt >= Constants.ONE_INT && selectionInt <= Constants.THREE_INT)
                        MainOptions(selectionInt);
                }                
            }
        }
        private static void MainOptions(int selectionInt)
        {
            const int ZERO = 0;
            switch (selectionInt)
                {
                    case 1:
                        globals.madeSelect = true;
                        Console.Clear();
                        Console.WriteLine("Good Bye");
                        Environment.Exit(ZERO);
                        break;
                    case 2:
                        globals.madeSelect = true;
                        Console.Clear();
                        Console.WriteLine("Create new course selected.\n");
                        CreateCourse();
                        break;
                    case 3:
                        globals.madeSelect = true;
                        Console.Clear();
                        Console.WriteLine("COURSES:\n");                        
                        StudyIncrementer();
                        ClearLists();
                        SelectCourse();
                        StudyCourse();                        
                        break;
                    default:
                        Console.WriteLine("Default case");
                        globals.madeSelect = false;
                        break;
                }
        }
        private static void SelectionDialogs(int dialog)
        {
            switch (dialog)
            {
                case 1:
                    //For AvailableOptions()
                    // Option 1 if no courses available
                    Console.Clear();
                    Console.WriteLine("\n\n1: Exit the program");
                    Console.WriteLine("2: Create a new course\n");
                    Console.WriteLine("\n\n\nEnter an option from the menu: ");
                    break;
                case 2:
                    //For AvailableOptions()
                    // Option 2 if courses are available
                    Console.Clear();
                    Console.WriteLine("\n\n1: Exit the program");
                    Console.WriteLine("2: Create a new course");
                    Console.WriteLine("3: Study a course\n"); 
                    Console.WriteLine("\n\n\nSelect an option from the menu: ");
                    break;
                case 3:
                    //For StudyCourse()
                    Console.Clear();
                    Console.WriteLine("\n\n\nNothing left to study for current topic today.");
                    Console.WriteLine("Enter m to quit back to menu, or any other key to exit.");
                    globals.response = Console.ReadLine();                
                    if (globals.response == "m")
                    {
                        Console.Clear();
                        globals.madeSelect = false;
                        globals.newLeft = Constants.ZERO_INT;
                        globals.currentLeft = Constants.ZERO_INT;
                        globals.lateLeft = Constants.ZERO_INT;
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        Environment.Exit(Constants.ZERO_INT);
                    }
                    break;
                case 4:
                    //For CreateCourse()
                    Console.Clear();
                    Console.WriteLine("\n\n\n\n\nWhat is the name of the course? ");
                    globals.CourseName = Console.ReadLine();
                    Console.WriteLine("\n\n\n\n\nHow many chapters are in the text book? ");
                    globals.CourseChapters = Console.ReadLine();
                    creationVars.chaptersInt = Convert.ToInt32(globals.CourseChapters);
                    break;
                case 5:
                    //For SetupData()
                    Console.WriteLine($"\n\n\n\n\nHow many sub-sections are in chapter {creationVars.currentChapter}: "); // Chapters are used here to make it easier to set the course up.
                    creationVars.subSectionString = Console.ReadLine();
                    creationVars.subSectionCounter = Convert.ToInt32(creationVars.subSectionString);
                    break;
                case 6:
                    //For SetupData()
                    Console.WriteLine($"\n\n\n\n\nHow many topics are in section {creationVars.currentChapter}.{creationVars.currentSubSection}: "); // Chapters are used here to make it easier to set the course up.
                    creationVars.topicCountString = Console.ReadLine();
                    break;
                case 7:
                    //For SetupData()
                    Console.WriteLine($"\n\n\n\n\nEnter the quantity of questions for section {creationVars.newTopName}: ");
                    creationVars.pCountString = Console.ReadLine();
                    break;
                case 8:
                    //For UpdateCounts()
                    Console.Clear();
                    Console.WriteLine("Finished updating CourseCount.txt");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 9:
                    //For SelectCourse()
                    Console.Write("\n\nEnter a Course ID: ");
                    break;
                case 10:
                    //For StudyCourse()
                    StudyHUD();
                    break;
                case 11:
                    //For StudyCourse()
                    Console.Write("\n\n\nQuantity answered correctly, or option choice: ");
                    break;
                case 12:
                    //For StudyCourse()
                    StudyHUD();
                    Console.WriteLine("Invalid Input:");
                    Console.WriteLine("\n\nvalue exceeds number of problems or questions, \nor it is less than zero.");
                    Console.Write("\n\n\nQuantity answered correctly, or option choice: ");
                    break;
            }
        }
        private static void CreateCourse()
        {
            // increment the course count after the course file containing the topic names
            // is created AND/OR updated.
            creationVars.chapterLoop = Constants.ZERO_INT;
            creationVars.currentChapter = Constants.ZERO_INT;
            creationVars.chaptersInt = Constants.ZERO_INT;
            creationVars.topicCounter = Constants.ZERO_INT;
            creationVars.topicCountString = Constants.ZERO_STRING;
            creationVars.topicLoop = Constants.ZERO_INT;
            creationVars.subSectionCounter = Constants.ZERO_INT;
            creationVars.subSectionString = Constants.ZERO_STRING;
            creationVars.subLoop = Constants.ZERO_INT;
            creationVars.topicID = Constants.ZERO_INT;
            globals.TopicCount = Constants.ZERO_INT;
            creationVars.currentSubSection = Constants.ZERO_INT;
            SelectionDialogs(Constants.FOUR_INT);
            SetupData();
            ProduceCourse();
            UpdateCounts();
        }
        private static void SetupData()
        {
            //For CreateCourse()
            while (creationVars.chapterLoop < creationVars.chaptersInt)
            {
                creationVars.currentChapter = creationVars.chapterLoop + Constants.ONE_INT;
                SelectionDialogs(Constants.FIVE_INT);
                while (creationVars.subLoop < creationVars.subSectionCounter)
                {
                    creationVars.currentSubSection = creationVars.subLoop + Constants.ONE_INT;
                    SelectionDialogs(Constants.SIX_INT);
                    creationVars.topicCounter = Convert.ToInt32(creationVars.topicCountString);
                    globals.TopicCount = globals.TopicCount + creationVars.topicCounter;
                    
                    // This loop is where all of the essential data are set up
                    while (creationVars.topicLoop < creationVars.topicCounter)
                    {
                        TopicModel newTopic = new TopicModel();
                        if (creationVars.topicLoop == Constants.ZERO_INT)
                            newTopic.Top_ID = Constants.ZERO_INT;
                        creationVars.problemCount = Constants.ZERO_INT;
                        creationVars.currentTopic = creationVars.topicLoop + Constants.ONE_INT;
                        creationVars.check = Constants.ZERO_INT; // will be used to see if Top_ID should increment.
                        creationVars.topicString = Convert.ToString(creationVars.currentTopic);
                        newTopic.Top_Name = ($"{creationVars.currentChapter}.{creationVars.currentSubSection}.{creationVars.topicString}");
                        creationVars.newTopName = newTopic.Top_Name;
                        SelectionDialogs(Constants.SEVEN_INT);
                        creationVars.problemCount = Convert.ToDouble(creationVars.pCountString);
                        newTopic.Course_ID = globals.CourseCount + Constants.ONE_INT; // int
                        newTopic.Top_Studied = false; // bool
                        newTopic.Next_Date = Constants.NONE; // string
                        newTopic.First_Date = Constants.NONE; // string
                        newTopic.Num_Problems = creationVars.problemCount; // the rest are type double
                        newTopic.Num_Correct = Constants.ZERO_DOUBLE;
                        newTopic.Top_Difficulty = Constants.ZERO_DOUBLE;
                        newTopic.Top_Repetition = Constants.ZERO_INT;
                        newTopic.Interval_Remaining = Constants.ZERO_DOUBLE;
                        newTopic.Interval_Length = Constants.ZERO_DOUBLE;
                        newTopic.Engram_Stability = Constants.ZERO_DOUBLE;
                        newTopic.Engram_Retrievability = Constants.ZERO_DOUBLE;
                        topics.Add(newTopic);
                        ++creationVars.topicLoop;
                        creationVars.check = creationVars.topicLoop;
                        if (creationVars.check <= creationVars.topicCounter)
                        {
                            // Top_ID must be incremented before next iteration of loop, if more topics exist.
                            ++creationVars.topicID;
                            newTopic.Top_ID = creationVars.topicID;
                        }
                    }
                    creationVars.topicCounter = Constants.ZERO_INT;
                    creationVars.topicLoop = Constants.ZERO_INT;
                    ++creationVars.subLoop;
                }
                creationVars.subSectionCounter = Constants.ZERO_INT;
                creationVars.subLoop = Constants.ZERO_INT;                
                ++creationVars.chapterLoop;
            }            
        }
        private static void ProduceCourse()
        {
            List<string> output = new List<string>();
            foreach (var topic in topics)
                output.Add($"{topic.Top_ID},{topic.Course_ID},{topic.Top_Name},{topic.Top_Studied},{topic.Next_Date},{topic.First_Date},{topic.Num_Problems},{topic.Num_Correct},{topic.Top_Difficulty},{topic.Top_Repetition},{topic.Interval_Remaining},{topic.Interval_Length},{topic.Engram_Stability},{topic.Engram_Retrievability}");
            if (globals.osSwitch == true)
            {
                // For Linux
                creationVars.filePath = $"{globals.DirectoryPath}//{globals.CourseName}.txt";
                File.WriteAllLines(creationVars.filePath, output);
                creationVars.listFile = $"{globals.DirectoryPath}//CourseList.txt";
            }
            else
            {
                // For Windows
                creationVars.filePath = $"{globals.DirectoryPath}\\{globals.CourseName}.txt";
                File.WriteAllLines(creationVars.filePath, output);
                creationVars.listFile = $"{globals.DirectoryPath}\\CourseList.txt";
            }
        }
        private static void UpdateCounts()
        {
            int newID = globals.CourseCount;
            string path;
            string courseCount;
            string listContents;
            List<string> contentList = new List<string>();            
            ++newID;
            listContents = $"{newID},{globals.CourseName}.txt,{creationVars.filePath}";
            contentList.Add(listContents);            
            if (File.Exists(creationVars.listFile))
                CourseListPath();            
            else
                File.WriteAllLines(creationVars.listFile, contentList);                   
            if (globals.osSwitch == true)
                path = $"{globals.DirectoryPath}//CourseCount.txt";            
            else
                path = $"{globals.DirectoryPath}\\CourseCount.txt";            
            courseCount = File.ReadAllText(path);
            globals.CourseCount = Convert.ToInt32(courseCount);
            ++globals.CourseCount;
            courseCount = Convert.ToString(globals.CourseCount);
            File.WriteAllText(path, courseCount); 
            SelectionDialogs(Constants.EIGHT_INT);
        }
        private static void CourseListPath()
        {
            string filePath;
            string filePath2;
            string courseFilePath;
            if (globals.osSwitch == true)
            {
                // Linux
                filePath = $"{globals.DirectoryPath}//CourseList.txt";
                filePath2 = $"{globals.DirectoryPath}//CourseList.bak";
                courseFilePath = $"{globals.DirectoryPath}//{globals.CourseName}.txt";
            }
            else
            {
                // Windows
                filePath = $"{globals.DirectoryPath}\\CourseList.txt";
                filePath2 = $"{globals.DirectoryPath}\\CourseList.bak";
                courseFilePath = $"{globals.DirectoryPath}\\{globals.CourseName}.txt";
            }
            AddCourseToList(filePath, filePath2, courseFilePath);
        }
        private static void AddCourseToList(string filePath, string filePath2, string courseFilePath)
        {
            int courseID = globals.CourseCount;
            ++courseID;
            List<string> lines = new List<string>();
            if (File.Exists(filePath))
                lines = File.ReadAllLines(filePath).ToList();          
            lines.Add($"{courseID},{globals.CourseName}.txt,{courseFilePath}");
            File.WriteAllLines(filePath2, lines); // Just in case the computer loses power, or freezes up. CourseList.bak would have to be manually renamed.
            File.WriteAllLines(filePath, lines);
        }
        private static void SelectCourse()
        {
            string filePath;
            string selectionString;
            int selectionInt = Constants.ZERO_INT;
            bool validInput = false;
            if (globals.osSwitch == true)
                filePath = $"{globals.DirectoryPath}//CourseList.txt";
            else
                filePath = $"{globals.DirectoryPath}\\CourseList.txt";
            List<CourseListModel> completeList = new List<CourseListModel>();
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(filePath).ToList();
            foreach (string line in lines)
            {
                string[] entries = line.Split(',');
                CourseListModel newList = new CourseListModel();
                newList.Course_ID = Convert.ToInt32(entries[Constants.ZERO_INT]);
                newList.Course_Name = entries[Constants.ONE_INT];
                newList.File_Path = entries[Constants.TWO_INT];
                completeList.Add(newList);
            }
            foreach (var course in completeList)
                Console.WriteLine($"Course ID: {course.Course_ID} - Course Name: {course.Course_Name}");
            while (validInput == false)
            {
                try
                {
                    SelectionDialogs(Constants.NINE_INT);
                    selectionString = Console.ReadLine();
                    selectionInt = Convert.ToInt32(selectionString);
                    validInput = true;
                }
                catch
                {
                    Console.WriteLine("Invalid selection.");
                    validInput = false;                    
                }
                if (validInput == true)
                {
                    --selectionInt;
                    var testVar = selectionInt + Constants.ONE_INT;
                    try
                    {
                        globals.FilePath = completeList.ElementAt(selectionInt).File_Path;
                        validInput = true;
                        Console.Clear();
                        foreach (var course in completeList)
                        {
                            Console.WriteLine($"Course ID: {course.Course_ID} - Course Name: {course.Course_Name}");
                            if (testVar == course.Course_ID)
                                globals.CourseName = course.Course_Name;                            
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid selection.");
                        validInput = false;
                    }
                }
            }            
        }
        private static void StudyCourse()
        {
            studyVars.index = Constants.ZERO_INT;
            studyVars.today = DateTime.Parse(globals.TheDate);
            studyVars.filePath = globals.FilePath;
            studyVars.lineCount = Constants.ZERO_INT;
            StudyLines();
            if (studyVars.lineCount > Constants.ZERO_INT)
            {
                if (TopicsList.Count > Constants.ZERO_INT)
                {
                    StudyDates();
                    if (ToStudy.Count != Constants.ZERO_INT)
                    {
                        StudyNotZero();
                        while (globals.ProblemsDone != true)
                        {
                            if (globals.newLeft > Constants.ZERO_INT)
                                PredictMain();
                            StudyNotDone();
                            if (studyVars.response == "m")
                                return;
                        }
                    }
                }
                SelectionDialogs(Constants.THREE_INT);
            }
            else
                SelectionDialogs(Constants.THREE_INT);
        }
        private static void CalculateLearning()
        {
            AddRepetition();
            int ithRepetition = TopicsList.ElementAt(globals.TopicID).Top_Repetition;
            if (ithRepetition == Constants.ONE_INT)
                TopicDifficulty();
            IntervalTime();
            EngramStability();
            EngramRetrievability();
            ProcessDate();
        }
        private static void AddRepetition()
        {
            ++TopicsList.ElementAt(globals.TopicID).Top_Repetition;
        }
        private static void TopicDifficulty()
        {
            // Since intervalTime multiplies against difficulty, and difficulty is set only once
            // then a topic could be scheduled every day for a long time if too close to 1.0, and too 
            // far apart if above 2.5
            const double LOW_DIFFICULTY = 2.5;
            const double HIGH_DIFFICULTY = 1.3;
            double rise = LOW_DIFFICULTY - HIGH_DIFFICULTY;
            double totalProblems = TopicsList.ElementAt(globals.TopicID).Num_Problems;
            double correctProblems = TopicsList.ElementAt(globals.TopicID).Num_Correct;
            double run = totalProblems;
            double slope = rise / run;
            double difficulty = (slope * correctProblems) + HIGH_DIFFICULTY; // Slope-Intercept formula y = mx + b
            TopicsList.ElementAt(globals.TopicID).Top_Difficulty = difficulty; // Write difficulty to student record file Difficulty column
        }
        private static void IntervalTime()
        {
            const double SINGLE_DAY = 1440; // 1440 is the quatity in minutes of a day. I'm using minutes, instead of whole days, to be more precise.
            double difficulty = TopicsList.ElementAt(globals.TopicID).Top_Difficulty;
            int ithRepetition = TopicsList.ElementAt(globals.TopicID).Top_Repetition;
            double intervalRemaining = TopicsList.ElementAt(globals.TopicID).Interval_Remaining;
            double intervalLength = TopicsList.ElementAt(globals.TopicID).Interval_Length;

            //     Second repetition will occur the next day. 
            //	   Although, the research document does not precisely
            //	   state a time frame until the second repetition. The 
            //	   values of the two variables may need to be changed, 
            //	   if the spacing is too far apart.
            if (ithRepetition == Constants.ONE_INT)
            {
                // The researech document says that s == r @ 1st repetition
                intervalRemaining = SINGLE_DAY;
                intervalLength = SINGLE_DAY;
            }
            else
                intervalLength = intervalLength * difficulty;
            intervalRemaining = intervalLength;
            TopicsList.ElementAt(globals.TopicID).Interval_Length = intervalLength; // Write intervalLength to student record Interval.
            TopicsList.ElementAt(globals.TopicID).Interval_Remaining = intervalRemaining; // Write remainingTime to student record file RTime column
        }
        private static void EngramStability()
        {
            const double KNOWLEDGE_LINK = -0.0512932943875506;
            const double NEGATIVE_ONE = -1;
            // remainingTime and intervalLength represent the variables r and s, respectively, from the research document.
            double intervalRemaining = TopicsList.ElementAt(globals.TopicID).Interval_Remaining;
            double intervalLength = TopicsList.ElementAt(globals.TopicID).Interval_Length;
            double stabilityOfEngram;
            stabilityOfEngram = (NEGATIVE_ONE * intervalLength) / KNOWLEDGE_LINK; // S = -s/ln(K), where K = 0.95, and the natural logarithm of K equals KNOWLEDGE_LINK.            
            TopicsList.ElementAt(globals.TopicID).Engram_Stability = stabilityOfEngram; // Write Stability to student record file Stability column
        }
        private static void EngramRetrievability()
        {
            const double NEGATIVE_ONE = -1;
            double intervalLength = TopicsList.ElementAt(globals.TopicID).Interval_Length;
            double intervalRemaining = TopicsList.ElementAt(globals.TopicID).Interval_Remaining;
            double stabilityOfEngram = TopicsList.ElementAt(globals.TopicID).Engram_Stability;
            double power = NEGATIVE_ONE * (intervalLength - intervalRemaining) / stabilityOfEngram;
            double retrievability = Math.Exp(power);
            TopicsList.ElementAt(globals.TopicID).Engram_Retrievability = retrievability;
        }
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
        private static void SaveProgress()
        {
            string filePath;
            string filePath2;
            List<string> output = new List<string>();
            foreach (var topic in TopicsList)
                output.Add($"{topic.Top_ID},{topic.Course_ID},{topic.Top_Name},{topic.Top_Studied},{topic.Next_Date},{topic.First_Date},{topic.Num_Problems},{topic.Num_Correct},{topic.Top_Difficulty},{topic.Top_Repetition},{topic.Interval_Remaining},{topic.Interval_Length},{topic.Engram_Stability},{topic.Engram_Retrievability}");
            Console.WriteLine("\n\n\n\n\nSaving Work.");
            if (globals.osSwitch == true)
            {
                // For Linux
                filePath2 = $"{globals.DirectoryPath}//{globals.CourseName}.bak";
                filePath = $"{globals.DirectoryPath}//{globals.CourseName}";
                File.WriteAllLines(filePath2, output);
                File.WriteAllLines(filePath, output);
            }
            else
            {
                // For Windows
                filePath2 = $"{globals.DirectoryPath}\\{globals.CourseName}.bak";
                filePath = $"{globals.DirectoryPath}\\{globals.CourseName}";
                File.WriteAllLines(filePath2, output);
                File.WriteAllLines(filePath, output);
            }

            Console.WriteLine("Work Saved.");
            Console.ReadLine();
        }
        private static void ChangeTopicQuestions()
        {
            string numTotalString;
            double numTotalDouble = Constants.ZERO_DOUBLE;
            bool test = false;
            while (test == false)
            {
                Console.Clear();
                Console.WriteLine("Enter new number of TOTAL questions:");
                try
                {
                    numTotalString = Console.ReadLine();
                    numTotalDouble = Convert.ToDouble(numTotalString);
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("\n\nInvalid Input\n\nPress Enter to Continue");
                    Console.ReadLine();
                    SelectionDialogs(Constants.TEN_INT);                
                }
            }
            
            TopicsList.ElementAt(globals.TopicID).Num_Problems = numTotalDouble;
        }
        private static void StudyIncrementer()
        {
            ++globals.studyTracker;
        }
        private static void StudyDecrementer()
        {
            if (globals.studyTracker > Constants.ZERO_INT)
                --globals.studyTracker;
        }
        private static void ClearLists()
        {
            if ( globals.studyTracker > Constants.ONE_INT)
            {
                TopicsList.Clear();
                topics.Clear();
                ToStudy.Clear();
            }
        }
        private static void StudyLines()
        {
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(studyVars.filePath).ToList();
            studyVars.lineCount = lines.Count;
            foreach (string line in lines)
            {
                string[] entries = line.Split(',');
                TopicModel newList = new TopicModel();
                newList.Top_ID = Convert.ToInt32(entries[Constants.ZERO_INT]);
                newList.Course_ID = Convert.ToInt32(entries[Constants.ONE_INT]);
                newList.Top_Name = entries[Constants.TWO_INT];
                studyVars.topStudString = entries[Constants.THREE_INT];
                if (studyVars.topStudString == Constants.TRUE)
                    newList.Top_Studied = true;
                else
                    newList.Top_Studied = false;
                newList.Next_Date = entries[Constants.FOUR_INT];
                newList.First_Date = entries[Constants.FIVE_INT];
                newList.Num_Problems = Convert.ToDouble(entries[Constants.SIX_INT]);
                newList.Num_Correct = Convert.ToDouble(entries[Constants.SEVEN_INT]);
                newList.Top_Difficulty = Convert.ToDouble(entries[Constants.EIGHT_INT]);
                newList.Top_Repetition = Convert.ToInt32(entries[Constants.NINE_INT]);
                newList.Interval_Remaining = Convert.ToDouble(entries[Constants.TEN_INT]);
                newList.Interval_Length = Convert.ToDouble(entries[Constants.ELLEVEN_INT]);
                newList.Engram_Stability = Convert.ToDouble(entries[Constants.TWELVE_INT]);
                newList.Engram_Retrievability = Convert.ToDouble(entries[Constants.THIRTEEN_INT]);
                TopicsList.Add(newList);
            }
        }

        private static void StudyDates()
        {
            //Called From StudyCourse()
            // Retry Studied TopicID's section (these are study sessions that were missed)
            studyVars.index = Constants.ZERO_INT;
            globals.lateLeft = Constants.ZERO_INT;
            while (studyVars.index < TopicsList.Count)
            {
                if (TopicsList.ElementAt(studyVars.index).Top_Studied == true)
                {
                    studyVars.dateAsString = TopicsList.ElementAt(studyVars.index).Next_Date;
                    studyVars.topicDate = DateTime.Parse(studyVars.dateAsString);
                    studyVars.dateCompare = DateTime.Compare(studyVars.topicDate, studyVars.today);
                    if (studyVars.dateCompare < Constants.ZERO_INT)
                    {                        
                        ToStudy.Add(studyVars.index);
                        // to display number of late topics left to study
                        ++globals.lateLeft;
                    }
                }
                ++studyVars.index;
            }
            // Studied TopicID's scheduled for today section
            studyVars.index = Constants.ZERO_INT;
            globals.currentLeft = Constants.ZERO_INT;
            while (studyVars.index < TopicsList.Count)
            {
                if (TopicsList.ElementAt(studyVars.index).Top_Studied == true)
                {
                    studyVars.dateAsString = TopicsList.ElementAt(studyVars.index).Next_Date;
                    studyVars.topicDate = DateTime.Parse(studyVars.dateAsString);
                    studyVars.dateCompare = DateTime.Compare(studyVars.topicDate, studyVars.today);
                    if (studyVars.dateCompare == Constants.ZERO_INT)
                    {
                        ToStudy.Add(studyVars.index);
                        // to display number of on-time review topics left to study
                        ++globals.currentLeft;
                    }
                }
                ++studyVars.index;
            }
            // New Topic ID's section
            studyVars.index = Constants.ZERO_INT;
            globals.newLeft = Constants.ZERO_INT;
            while (studyVars.index < TopicsList.Count)
            {
                if (TopicsList.ElementAt(studyVars.index).Top_Studied == false)
                {
                    ToStudy.Add(studyVars.index);
                    ++globals.newLeft;
                }
                ++studyVars.index;
            }
        }
        private static void StudyFalse()
        {
            //From StudyNotDone()
            bool test = false;
            studyVars.studied = true;
            while (test == false)
            {
                SelectionDialogs(Constants.ELLEVEN_INT);
                studyVars.response = Console.ReadLine();
                if (studyVars.response == "m" || studyVars.response == "u")
                {
                    while (studyVars.response == "u")
                    {                            
                        ChangeTopicQuestions();
                        Console.WriteLine("\n\n\nEnter the quantity you answered correctly: ");
                        try
                        {
                            studyVars.response = Console.ReadLine();
                            studyVars.numCorrectDouble = Convert.ToDouble(studyVars.response);
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("\n\nInvalid Input\n\nPress Enter to Continue");
                            studyVars.response = "u";
                            Console.ReadLine();
                            SelectionDialogs(Constants.TEN_INT);
                        }
                    }
                    if (studyVars.response == "m")
                    {
                        Console.Clear();
                        globals.madeSelect = false;
                        globals.newLeft = Constants.ZERO_INT;
                        globals.currentLeft = Constants.ZERO_INT;
                        globals.lateLeft = Constants.ZERO_INT;
                        studyVars.studied = false;
                        return;
                    }
                }
                try
                {
                    studyVars.numCorrectString = studyVars.response;
                    studyVars.numCorrectDouble = Convert.ToDouble(studyVars.numCorrectString);
                    test = true;                        
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("\n\nInvalid Input\n\nPress Enter to try again");
                    Console.ReadLine();
                    SelectionDialogs(Constants.TEN_INT);
                    test = false;
                }            
                //Not an else since response expected to change if response == "u"
                if (test == true)
                {
                    studyVars.numCorrectString = studyVars.response;
                    studyVars.numCorrectDouble = Convert.ToDouble(studyVars.numCorrectString);
                    while (studyVars.numCorrectDouble > TopicsList.ElementAt(globals.TopicID).Num_Problems || studyVars.numCorrectDouble < Constants.ZERO_DOUBLE)
                    {
                        SelectionDialogs(Constants.TWELVE_INT);
                        studyVars.response = Console.ReadLine();
                        if (studyVars.response == "u")
                        {
                            ChangeTopicQuestions();
                            Console.Clear();
                            Console.WriteLine("Re-enter number of problems or questions you respoded to correctly");
                        }          
                        if (studyVars.response == "m")
                        {
                            Console.Clear();
                            globals.madeSelect = false;
                            globals.newLeft = Constants.ZERO_INT;
                            globals.currentLeft = Constants.ZERO_INT;
                            globals.lateLeft = Constants.ZERO_INT;
                            studyVars.studied = false;
                            return;
                        }                      
                        try
                        {
                            studyVars.numCorrectString = studyVars.response;
                            studyVars.numCorrectDouble = Convert.ToDouble(studyVars.numCorrectString);
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("\n\nInvalid Input\n\nPress Enter to try again");
                            Console.ReadLine();
                        }
                    }
                    TopicsList.ElementAt(globals.TopicID).Num_Correct = studyVars.numCorrectDouble;
                    TopicsList.ElementAt(globals.TopicID).First_Date = studyVars.todayDateString;
                    --globals.newLeft;
                    Console.Clear();
                }
            }
        }
        private static void StudyTrue()
        {
            Console.Write("\n\n\nOption: ");
            studyVars.studied = true;
            studyVars.response = Console.ReadLine();
            if (studyVars.response == "m")
            {
                globals.madeSelect = false;
                Console.Clear();
                globals.newLeft = Constants.ZERO_INT;
                globals.currentLeft = Constants.ZERO_INT;
                globals.lateLeft = Constants.ZERO_INT;
                studyVars.studied = false;
                return;
            }
            studyVars.dateAsString = TopicsList.ElementAt(globals.TopicID).Next_Date;
            studyVars.topicDate = DateTime.Parse(studyVars.dateAsString);
            studyVars.dateCompare = DateTime.Compare(studyVars.topicDate, studyVars.today);
            if (studyVars.dateCompare < Constants.ZERO_INT)
                --globals.lateLeft;
            else if (studyVars.dateCompare == Constants.ZERO_INT)
                --globals.currentLeft;
            Console.Clear();
        }
        private static void StudyNotDone()
        {
            //From StudyCourse()
            studyVars.studied = false;
            if (globals.TopicIndex < studyVars.toStudyCount)
            {
                SelectionDialogs(Constants.TEN_INT);
                if (TopicsList.ElementAt(globals.TopicID).Top_Studied == false)
                {
                    StudyFalse();
                    if (studyVars.response == "m")
                        return;
                }
                else
                    StudyTrue();
                if (studyVars.studied == true)
                {
                    studyVars.numCorrectDouble = Constants.ZERO_DOUBLE;
                    CalculateLearning();
                    SaveProgress();
                    ++globals.TopicIndex;
                    if (globals.TopicIndex < studyVars.toStudyCount)
                        globals.TopicID = ToStudy.ElementAt(globals.TopicIndex); 
                }
 
            }
            else
                globals.ProblemsDone = true;
        }
        private static void StudyNotZero()
        {
            //From StudyCourse()
            globals.TopicID = ToStudy.ElementAt(Constants.ZERO_INT);
            globals.TopicIndex = Constants.ZERO_INT;
            studyVars.toStudyCount = ToStudy.Count;
            studyVars.todayDateString = studyVars.today.ToString("d");
            if (studyVars.toStudyCount > Constants.ZERO_INT)
                globals.ProblemsDone = false;
            else
                globals.ProblemsDone = true;
            studyVars.response = Constants.ZERO_STRING; //Just added this to give option to go back to Ready Menu
            studyVars.numCorrectDouble = Constants.ZERO_DOUBLE;
        }
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

        /***********************************PREDICTION START*********************************************/
        private static void PredictMain()
        {
            AvgDiff();

            // Point Ymax is (x1 , y1)
            // For point (x1 , y1), where y1 is maximum first studies performed
            // x1 is number of repeat studies performed at max first studies
            CollectFirstStudies();      // Get first study dates of studied topics
            if (studiedSimList.Count > Constants.ZERO_INT)
            {
                YmaxFirsts();            // Get y1: max Y First Studies for point Ymax
                if (predictVars.Only_ONE == true)
                {
                    predictVars.Prediction_Date = "Not enough topics studied yet!";
                    predictVars.Only_ONE = false;

                    //Clear Lists here too
                    ProdictionListsClear();
                    return;
                }
                PreparePastStudies();
                SimulatePastStudies();
                XmaxRepeats();

                CollectNonStudied();
                predictVars.Process_Prediction = true;
                GenerateProjectedStudies();
            }
        }


        private static void AvgDiff()
        {
            // Apply average of difficulty to simulation of 
            // calculating non-studied topics learning dates.
            double nDifficultsDouble = Constants.ZERO_DOUBLE;
            double difficultsAdded = Constants.ZERO_DOUBLE;
            predictVars.Avg_Difficulty = Constants.ZERO_DOUBLE;
            int count = Constants.ZERO_INT;
            foreach (var topic in TopicsList)
            {
                if (topic.Top_Studied == true)
                {
                    difficultsAdded += Convert.ToDouble(topic.Top_Difficulty);
                    ++count;
                }
            }
            nDifficultsDouble = Convert.ToDouble(count);
            predictVars.Avg_Difficulty = difficultsAdded/nDifficultsDouble;
        }
        private static void CollectFirstStudies()
        {
            int topicNumber = Constants.ZERO_INT;
            int tracker = Constants.ZERO_INT;
            
            int index = Constants.ZERO_INT;
            while (index < TopicsList.Count)
            {
                tracker = AddFirstsCollect(topicNumber, index);
                topicNumber = tracker;
                ++index;
            }
            index = Constants.ZERO_INT;
        }
        private static int AddFirstsCollect(int topicNumber, int index)
        {
            SimModel newSims = new SimModel();
            if (TopicsList.ElementAt(index).Top_Studied == true)
            {
                newSims.First_Date = TopicsList.ElementAt(index).First_Date;
                newSims.Real_Repetition = TopicsList.ElementAt(index).Top_Repetition;
                newSims.Sim_Repetition = Constants.ONE_INT;
                newSims.Top_Difficulty = TopicsList.ElementAt(index).Top_Difficulty;
                newSims.Interval_Length = TopicsList.ElementAt(index).Interval_Length;
                newSims.Top_Number = topicNumber; 
                newSims.Next_Date = TopicsList.ElementAt(index).Next_Date;
                studiedSimList.Add(newSims);

                ++topicNumber;
            }
            return topicNumber;
        }
        private static void YmaxFirsts()
        {
            int highestIndex = Constants.ZERO_INT;
            predictVars.Loop_Index = Constants.ZERO_INT;
            predictVars.Find_Yhigh_Index = Constants.ZERO_INT;
            predictVars.First_Check = true;


            while (predictVars.Loop_Index < studiedSimList.Count)
            {

                FindHighY();
                ++predictVars.Loop_Index;
            }

            predictVars.Loop_Index = Constants.ZERO_INT;
            predictVars.Find_Yhigh_Index = Constants.ZERO_INT;
            if (fStudyCounts.Count > Constants.TWO_INT)
            {


                YmaxSortFirsts();
                predictVars.Only_ONE = false;
            }
            else
            {
                predictVars.Only_ONE = true;
                return;
            }

            highestIndex = fStudyCounts.Count - Constants.ONE_INT;
            predictVars.Y_High_Ycount = fStudyCounts[highestIndex];
            
        }
        private static void YmaxSortFirsts()
        {
            int count = Constants.ONE_INT;
            int j, i, keyOne;
            string keyTwo;

            count = Constants.ZERO_INT;
            while (count < Constants.TWO_INT)
            {  
                
                for (j = Constants.TWO_INT; j < fStudyCounts.Count; ++j)
                {
                    keyOne = fStudyCounts[j];
                    keyTwo = fStudyDates[j];

                    // Insert fStudyCounts[j] into sorted sequence fStudyCounts[1...j-1]
                    // Insert fStudyDates[j] into sorted sequence fStudyDates[1...j-1]
                    i = j - Constants.ONE_INT;
                    while (i > Constants.ZERO_INT && fStudyCounts[i] > keyOne)
                    {
                        fStudyCounts[i + Constants.ONE_INT] = fStudyCounts[i];
                        fStudyDates[i + Constants.ONE_INT] = fStudyDates[i];
                        i = i - Constants.ONE_INT;
                    }
                    fStudyCounts[i + Constants.ONE_INT] = keyOne;
                    fStudyDates[i + Constants.ONE_INT] = keyTwo;
                }

                /* 
                this is here to get the first element sorted into 
                the rest of the array on the second run of the loop
                */
                if (count == Constants.ZERO_INT)
                {
                    //key = A[ZERO];
                    keyOne = fStudyCounts[Constants.ZERO_INT];
                    keyTwo = fStudyDates[Constants.ZERO_INT];
                    //A[ZERO] = A[ONE];
            
                    fStudyCounts[Constants.ZERO_INT] = fStudyCounts[Constants.ONE_INT];
                    fStudyDates[Constants.ZERO_INT] = fStudyDates[Constants.ONE_INT];
                    //A[ONE] = key;
                    fStudyCounts[Constants.ONE_INT] = keyOne;
                    fStudyDates[Constants.ONE_INT] = keyTwo;
                }
                ++count;
            }
        }
        private static void FindHighY()
        {
            int dateCompare;
            DateTime tempDateOne;
            DateTime tempDateTwo;    

            if (predictVars.First_Check == true)
            {
                fStudyDates.Add($"{studiedSimList.ElementAt(predictVars.Loop_Index).First_Date}");
                fStudyCounts.Add(Constants.ONE_INT);
                predictVars.First_Check = false;
            }
            else
            {
                tempDateOne = DateTime.Parse(fStudyDates[predictVars.Find_Yhigh_Index]);
                tempDateTwo = DateTime.Parse(studiedSimList.ElementAt(predictVars.Loop_Index).First_Date);
                dateCompare = DateTime.Compare(tempDateOne, tempDateTwo);

                if (dateCompare == Constants.ZERO_INT)
                    ++fStudyCounts[predictVars.Find_Yhigh_Index];
                else
                {
                    ++predictVars.Find_Yhigh_Index;
                    fStudyDates.Add($"{studiedSimList.ElementAt(predictVars.Loop_Index).First_Date}");
                    fStudyCounts.Add(Constants.ONE_INT);
                }
            }
        }
        private static void PreparePastStudies()
        {
            int index = Constants.ZERO_INT;
            int repetition = Constants.ONE_INT;
            while (index < studiedSimList.Count) //Replace Foreach foreach
            {
                repetition = studiedSimList.ElementAt(index).Sim_Repetition;
                InitializeOnePastStudy(index, repetition);
                ++index;                
            }
        }
        private static void InitializeOnePastStudy(int index, int repetition)
        {
            int nextRep = Constants.ZERO_INT;
            int lastRep = studiedSimList.ElementAt(index).Real_Repetition;
            while (repetition <= lastRep)
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
                studiedSims.Simulated_Date = studiedSimList.ElementAt(index).First_Date;
            
            genSimsStudied.Add(studiedSims);
            ++repetition;
            return repetition;
        }
        private static void SimulatePastStudies()
        {
            predictVars.Gen_Studied_Index = Constants.ZERO_INT;
            while (predictVars.Gen_Studied_Index < genSimsStudied.Count)
            {
                predictVars.Process_Gen_Sims_Studied = true;
                SimCalculateLearning();
                predictVars.Process_Gen_Sims_Studied = false;
                ++predictVars.Gen_Studied_Index;
            }
            predictVars.Gen_Studied_Index = Constants.ZERO_INT;
        }

        private static void XmaxRepeats()
        {
            //xMaxList
        
            predictVars.Loop_Index = Constants.ZERO_INT;
            while (predictVars.Loop_Index < genSimsStudied.Count)
            {
                XmaxToSort();
                ++predictVars.Loop_Index;
            }
            XmaxRepeatSort();

            predictVars.Loop_Index = Constants.ZERO_INT;
            while (predictVars.Loop_Index < xMaxSortList.Count)
            {
                AddToXmax();
                ++predictVars.Loop_Index;
            }
            predictVars.Loop_Index = Constants.ZERO_INT;
            xMaxSortList.Clear();


            //Get X
            // int32 something = DateTime.Compare(t1 , t2);
            // if something < zero, then t1 is earlier than t2
            // if something == zero, then same day
            // if something > zero, then t1 is later than t2
  
            predictVars.XrepIndex = Constants.ZERO_INT;
            predictVars.First_Check = true;
            predictVars.Loop_Index = Constants.ZERO_INT;
            while (predictVars.Loop_Index < toXmax.Count)
            {
                xMaxCountReps();
                ++predictVars.Loop_Index;
            }
            predictVars.XrepIndex = Constants.ZERO_INT;

            bool RepsortCorrect, RepSortCheck;
            predictVars.Loop_Index = Constants.ZERO_INT;
            RepsortCorrect = RepSortCheck = true;
            while (RepsortCorrect == true)
            {
                RepSortCheck = SortXrepCounts(RepsortCorrect);
                RepsortCorrect = RepSortCheck;
                ++predictVars.Loop_Index;
            }
            predictVars.Loop_Index = Constants.ZERO_INT;
            int highestElement = xRepDateCounts.Count - Constants.ONE_INT;
            predictVars.X_High_Xcount = xRepDateCounts[highestElement];
        }
        private static bool SortXrepCounts(bool RepsortCorrect)
        {
		int listKey = new int();

		for (predictVars.J = Constants.TWO_INT; predictVars.J < xRepDateCounts.Count; predictVars.J++)
		{
			listKey = xRepDateCounts[predictVars.J];

			// Insert xRepDateCounts[j] into sorted sequence xRepDateCounts[1...j-1]
			predictVars.I = predictVars.J - Constants.ONE_INT;
			while (predictVars.I > Constants.ZERO_INT && xRepDateCounts[predictVars.I] > listKey)
			{
			    xRepDateCounts[predictVars.I + Constants.ONE_INT] = xRepDateCounts[predictVars.I];
			    predictVars.I = predictVars.I - Constants.ONE_INT;
			}
			xRepDateCounts[predictVars.I + Constants.ONE_INT] = listKey;
		}

            /* 
            this is here to get the first element sorted into 
            the rest of the array on the second run of the loop
            */

            if (xRepDateCounts[Constants.ZERO_INT]  > xRepDateCounts[Constants.ONE_INT])
            {
                //key = A[ZERO];
                listKey = xRepDateCounts[Constants.ZERO_INT];
                //A[ZERO] = A[ONE];
                xRepDateCounts[Constants.ZERO_INT] = xRepDateCounts[Constants.ONE_INT];
                //A[ONE] = key;
                xRepDateCounts[Constants.ONE_INT] = listKey;
            }
            else
            {
                RepsortCorrect = false;
            }

            return RepsortCorrect;
        }

        private static void XmaxToSort()
        {
            SimModel sim = new SimModel();
            sim.First_Date = genSimsStudied.ElementAt(predictVars.Loop_Index).First_Date;
            sim.Real_Repetition = genSimsStudied.ElementAt(predictVars.Loop_Index).Real_Repetition;
            sim.Top_Difficulty = genSimsStudied.ElementAt(predictVars.Loop_Index).Top_Difficulty;
            sim.Interval_Length = genSimsStudied.ElementAt(predictVars.Loop_Index).Interval_Length;
            sim.Top_Number = genSimsStudied.ElementAt(predictVars.Loop_Index).Top_Number;
            sim.Next_Date = genSimsStudied.ElementAt(predictVars.Loop_Index).Next_Date;
            sim.Simulated_Date = genSimsStudied.ElementAt(predictVars.Loop_Index).Simulated_Date;
            sim.Sim_Repetition = genSimsStudied.ElementAt(predictVars.Loop_Index).Sim_Repetition;

            xMaxSortList.Add(sim);
        }
        private static void AddToXmax()
        {
            SimModel sim = new SimModel();
            sim.First_Date = xMaxSortList.ElementAt(predictVars.Loop_Index).First_Date;
            sim.Real_Repetition = xMaxSortList.ElementAt(predictVars.Loop_Index).Real_Repetition;
            sim.Top_Difficulty = xMaxSortList.ElementAt(predictVars.Loop_Index).Top_Difficulty;
            sim.Interval_Length = xMaxSortList.ElementAt(predictVars.Loop_Index).Interval_Length;
            sim.Top_Number = xMaxSortList.ElementAt(predictVars.Loop_Index).Top_Number;
            sim.Next_Date = xMaxSortList.ElementAt(predictVars.Loop_Index).Next_Date;
            sim.Simulated_Date = xMaxSortList.ElementAt(predictVars.Loop_Index).Simulated_Date;
            sim.Sim_Repetition = xMaxSortList.ElementAt(predictVars.Loop_Index).Sim_Repetition;

            toXmax.Add(sim);
        }
        private static void xMaxCountReps()
        {
            int firstRep = Constants.ONE_INT;
            if (predictVars.First_Check == true)
            {
                if (firstRep != toXmax.ElementAt(predictVars.Loop_Index).Sim_Repetition)
                {
                    xRepDateCounts.Add(Constants.ZERO_INT);
                    xRepTempDates.Add(toXmax.ElementAt(predictVars.Loop_Index).Simulated_Date);
                    predictVars.First_Check = false;
                }
            }
            if (predictVars.First_Check == false) // Must not be an else for logic to work
            {
                if (firstRep != toXmax.ElementAt(predictVars.Loop_Index).Sim_Repetition)
                {
                    if (xRepTempDates.ElementAt(predictVars.XrepIndex) == toXmax.ElementAt(predictVars.Loop_Index).Simulated_Date)
                    {
                        ++xRepDateCounts[predictVars.XrepIndex];
                    }
                    else
                    {
                        ++predictVars.XrepIndex;
                        xRepTempDates.Add(toXmax.ElementAt(predictVars.Loop_Index).Simulated_Date);
                        xRepDateCounts.Add(Constants.ONE_INT);
                    }
                }
            }
            
        }
        private static void XmaxRepeatSort()
        {
            predictVars.J = Constants.ZERO_INT;
            predictVars.I = Constants.ZERO_INT;
            predictVars.Date_Check = Constants.ZERO_INT;
            
            bool XsortCorrect, XsortCheck;
            XsortCheck = XsortCorrect = true;
            while (XsortCheck == true)
            {  
                XsortCheck = XinsertSort(XsortCorrect);
                XsortCorrect = XsortCheck;
            }
       
            predictVars.J = Constants.ZERO_INT;
            predictVars.I = Constants.ZERO_INT;
            predictVars.Date_Check = Constants.ZERO_INT;
        }

        private static bool XinsertSort(bool XsortCorrect)
        {

            SimModel listKey = new SimModel();

            for (predictVars.J = Constants.TWO_INT; predictVars.J < xMaxSortList.Count; predictVars.J++)
            {
                listKey = xMaxSortList[predictVars.J];

                // Insert xMaxSortList[j] into sorted sequence xMaxSortList[1...j-1]
                predictVars.I = predictVars.J - Constants.ONE_INT;
                predictVars.Date_Check = DateTime.Compare(DateTime.Parse(xMaxSortList.ElementAt(predictVars.I).Simulated_Date), DateTime.Parse(listKey.Simulated_Date));
                while (predictVars.I > Constants.ZERO_INT && predictVars.Date_Check > Constants.ZERO_INT)
                {
                    xMaxSortList[predictVars.I + Constants.ONE_INT] = xMaxSortList[predictVars.I];
                    predictVars.I = predictVars.I - Constants.ONE_INT;
                    predictVars.Date_Check = DateTime.Compare(DateTime.Parse(xMaxSortList.ElementAt(predictVars.I).Simulated_Date), DateTime.Parse(listKey.Simulated_Date));
                }
                xMaxSortList[predictVars.I + Constants.ONE_INT] = listKey;
            }

            /* 
            this is here to get the first element sorted into 
            the rest of the array on the second run of the loop
            */
            int toFixSort = DateTime.Compare(DateTime.Parse(xMaxSortList[Constants.ZERO_INT].Simulated_Date), DateTime.Parse(xMaxSortList[Constants.ONE_INT].Simulated_Date));
            if (toFixSort  > Constants.ZERO_INT)
            {
                //key = A[ZERO];
                listKey = xMaxSortList[Constants.ZERO_INT];
                //A[ZERO] = A[ONE];
                xMaxSortList[Constants.ZERO_INT] = xMaxSortList[Constants.ONE_INT];
                //A[ONE] = key;
                xMaxSortList[Constants.ONE_INT] = listKey;
            }
            else
                XsortCorrect = false;
            return XsortCorrect;
        }
        private static void CollectNonStudied()
        {
            int index = studiedSimList.Count - Constants.ONE_INT;
            int topicNumber = studiedSimList.ElementAt(index).Top_Number + Constants.ONE_INT;
            int tracker = Constants.ZERO_INT;
            
            int loopIndex = Constants.ZERO_INT;
            while (loopIndex < TopicsList.Count)
            {
                tracker = NonStudyGetter(topicNumber, loopIndex);
                topicNumber = tracker;
                ++loopIndex;
            }
            loopIndex = Constants.ZERO_INT;                
        }
        private static int NonStudyGetter(int topicNumber, int loopIndex)
        {
            SimModel newSims = new SimModel();
            if (TopicsList.ElementAt(loopIndex).Top_Studied == false)
            {
                newSims.First_Date = TopicsList.ElementAt(loopIndex).First_Date;
                newSims.Real_Repetition = TopicsList.ElementAt(loopIndex).Top_Repetition;
                newSims.Sim_Repetition = Constants.ZERO_INT;
                newSims.Top_Difficulty = predictVars.Avg_Difficulty;
                newSims.Interval_Length = Constants.ZERO_INT;
                newSims.Top_Number = topicNumber; 
                projectedSimList.Add(newSims);
                ++topicNumber;
            }
            return topicNumber;
        }




        private static void GenerateProjectedStudies()
        {
            DateTime startDate, previousDate, simDate;
            int totalNewTopics, count;
            startDate = DateTime.Now;
            predictVars.Sim_Date_Use = startDate.ToString("d");
            totalNewTopics = projectedSimList.Count; //This reduces in value as new topics are transfered
            count = Constants.ZERO_INT; //This stays ZERO

            while (count < totalNewTopics)
            {
                previousDate = DateTime.Parse(predictVars.Sim_Date_Use);
                simDate = previousDate.AddDays(Constants.ONE_INT);
                PredictStudies();
                predictVars.Sim_Date_Use = simDate.ToString("d");
                totalNewTopics = projectedSimList.Count;
            }
            ProdictionListsClear();
        }
        private static void ProdictionListsClear()
        {
            studiedSimList.Clear();
            genSimsAll.Clear();
            genSimsStudied.Clear();
            projectedSimList.Clear();
            fStudyDates.Clear();
            fStudyCounts.Clear();
            xRepTempDates.Clear();
            xRepDateCounts.Clear();
            toXmax.Clear();
        }
        private static void PredictStudies()
        {
            int totalNewTopics = Constants.ZERO_INT;
            int predictedIndex = Constants.ZERO_INT;
            int index;
            
            index = Constants.ZERO_INT;
            if (predictVars.Process_Prediction == true)
            {
                index = Constants.ZERO_INT;
                while (index < TopicsList.Count)
                {
                    GenSimsAllGetter(index);
                    ++index;
                }
                predictVars.Process_Prediction = false; 
            }

            CollectStudyX();
            FindYatX();
            CollectStudyY();
            ReduceNew();

            index = Constants.ZERO_INT;
           
            while (index < studyRepElements.Count)
            {
                predictVars.Gen_Projected_Index = studyRepElements.ElementAt(index);
                SimCalculateLearning();
                ++index;
            }
            studyRepElements.Clear();
            totalNewTopics = projectedSimList.Count;
            if (totalNewTopics == Constants.ZERO_INT) //FIXME - Make sure this doesn't cause problems once new reps are 0 for reps of old topics.
            {
                predictedIndex = genSimsAll.Count - Constants.ONE_INT;
                predictVars.Final_Topic = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Top_Number + Constants.ONE_INT;
                predictVars.Prediction_Date = genSimsAll[predictVars.Gen_Projected_Index].Next_Date;
            }
        }
        private static void CollectStudyX()
        {
            
            DateTime useDate, nextDate;
            int dateCompare;
            useDate = DateTime.Parse(predictVars.Sim_Date_Use);

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
        private static void GenSimsAllGetter(int index)
        {
            

            SimModel newSims = new SimModel();

            if (TopicsList[index].Top_Studied == true)
            {
                newSims.Top_Number = TopicsList.ElementAt(index).Top_ID - Constants.ONE_INT;
                newSims.First_Date = TopicsList.ElementAt(index).First_Date;
                newSims.Simulated_Date = TopicsList.ElementAt(index).Next_Date;
                newSims.Next_Date = TopicsList.ElementAt(index).Next_Date;
                newSims.Real_Repetition = TopicsList.ElementAt(index).Top_Repetition;
                newSims.Sim_Repetition = TopicsList.ElementAt(index).Top_Repetition;  //Some of these are not matching Real_Repetition. They should increment to that value in simulation of previous study sessions.
                newSims.Top_Difficulty = TopicsList.ElementAt(index).Top_Difficulty;
                newSims.Interval_Length = TopicsList.ElementAt(index).Interval_Length;
                
                genSimsAll.Add(newSims);
            }
        }
        private static void FindYatX()
        {
            double lowY, highY, run;
            double curReps, rise, slope, value_b, yCurrent;

            highY = value_b = predictVars.Y_High_Ycount;
            run = predictVars.X_High_Xcount; 
            lowY = Constants.ZERO_DOUBLE;
            rise = lowY - highY;
            slope = rise / run;
            curReps = predictVars.Current_X;
            yCurrent = (slope * curReps) + value_b;
            predictVars.Current_Y = (int)yCurrent;
        }
        private static void CollectStudyY()
        {
            
            int yToStudied = genSimsAll.Count - Constants.ONE_INT;
            int index = Constants.ZERO_INT;

            while (index < predictVars.Current_Y)
            {
                if (index < projectedSimList.Count)
                {
                    ++yToStudied;
                    studyRepElements.Add(yToStudied);
                }
                ++index;
            }
            index = Constants.ZERO_INT;
            int indexTwo = Constants.ZERO_INT;
            while (index < predictVars.Current_Y)
            {
                if (projectedSimList.Count >= predictVars.Current_Y)
                {
                    YtoGenSimsAll(index);
                }
                else if (projectedSimList.Count > Constants.ZERO_INT)
                {
                    while (indexTwo < projectedSimList.Count)
                    {
                        YtoGenSimsAll(indexTwo);
                        ++indexTwo;
                    }
                }
                ++index;
            }
        }
        private static void YtoGenSimsAll(int index)
        {
            SimModel newSims = new SimModel();

            newSims.Top_Number = projectedSimList.ElementAt(index).Top_Number; 
            newSims.First_Date = predictVars.Sim_Date_Use;
            newSims.Simulated_Date = predictVars.Sim_Date_Use; //Store next date here so that it will be picked up for study on that day
            newSims.Next_Date = predictVars.Sim_Date_Use; // Make sure it gets the next date. 
            newSims.Real_Repetition = Constants.ZERO_INT; // Because it is never really studied before the simulation
            newSims.Sim_Repetition = Constants.ONE_INT;  // Because it is now its first repetition being simulated
            newSims.Top_Difficulty = projectedSimList.ElementAt(index).Top_Difficulty; // It is set to calculated average of real studied topics
            newSims.Interval_Length = projectedSimList.ElementAt(index).Interval_Length; // I think I need to make sure this recieves interval length the same way genSimsStudied does
            genSimsAll.Add(newSims);

        }
        private static void ReduceNew()
        {
            int count = Constants.ZERO_INT;
            if (projectedSimList.Count > Constants.ZERO_INT && count < predictVars.Current_Y)
            {
                while (count < projectedSimList.Count)
                {
                    GetReducedNew(count);
                    ++count;
                }
                projectedSimList.Clear();                
              
                count = Constants.ZERO_INT;
                while (count < reducedProjected.Count)
                {
                    GetReducedProjected(count);
                    ++count;
                }
                reducedProjected.Clear();
            }
        }
        private static void GetReducedNew(int count)
        {
            SimModel newSims = new SimModel();

            if (count >= predictVars.Current_Y)
            {
                newSims = projectedSimList[count];
                reducedProjected.Add(newSims);
            }

        }
        private static void GetReducedProjected(int count)
        {
            SimModel newSims = new SimModel();
            newSims = reducedProjected[count];
            projectedSimList.Add(newSims);
        }


        /**************SimulateDates**************/
        private static void SimCalculateLearning()
        {
            SimIntervalTime();
            if (predictVars.Process_Gen_Sims_Studied == false)
                SimAddRepetition();
            SimProcessDate();
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

                if (ithRepetition == Constants.ONE_INT)
                    intervalLength = SINGLE_DAY;
                else
                    intervalLength = intervalLength * difficulty;
                
                genSimsStudied[predictVars.Gen_Studied_Index].Interval_Length = intervalLength;

                int indexFuture = predictVars.Gen_Studied_Index + Constants.ONE_INT;
                if (indexFuture < genSimsStudied.Count)
                    if (genSimsStudied[predictVars.Gen_Studied_Index].Top_Number == genSimsStudied[indexFuture].Top_Number)
                        genSimsStudied[indexFuture].Interval_Length = intervalLength;
            }
            else
            {
                difficulty = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Top_Difficulty;
                ithRepetition = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Sim_Repetition;
                intervalLength = genSimsAll.ElementAt(predictVars.Gen_Projected_Index).Interval_Length;
            
                if (ithRepetition == Constants.ONE_INT)
                    intervalLength = SINGLE_DAY;
                else
                    intervalLength = intervalLength * difficulty;

                 genSimsAll[predictVars.Gen_Projected_Index].Interval_Length = intervalLength;
            }
        }
        private static void SimAddRepetition()
        {
            ++genSimsAll[predictVars.Gen_Projected_Index].Sim_Repetition;
        }
        private static void SimProcessDate()
        {
            const double SINGLE_DAY = 1440;
            double intervalLength;
            double daysDouble;
            DateTime fakeToday;
            DateTime nextDate;
            string nextDateString;

            if (predictVars.Process_Gen_Sims_Studied == true)
            {
                intervalLength = genSimsStudied.ElementAt(predictVars.Gen_Studied_Index).Interval_Length;
                daysDouble = intervalLength / SINGLE_DAY; //Necessary to cut off fractional portion without rounding, so cant convert to Int32 yet.
                fakeToday = DateTime.Parse(genSimsStudied[predictVars.Gen_Studied_Index].Simulated_Date);
            }
            else
            {
                intervalLength = genSimsAll[predictVars.Gen_Projected_Index].Interval_Length;
                daysDouble = intervalLength / SINGLE_DAY;  //Necessary to cut off fractional portion without rounding, so cant convert to Int32 yet.
                fakeToday = DateTime.Parse(predictVars.Sim_Date_Use);
            }
            nextDate = fakeToday.AddDays(daysDouble);
            nextDateString = nextDate.ToString("d");

            int indexFuture = Constants.ZERO_INT;
            if (predictVars.Process_Gen_Sims_Studied == true)
            {
                indexFuture = predictVars.Gen_Studied_Index + Constants.ONE_INT;
                genSimsStudied[predictVars.Gen_Studied_Index].Next_Date = nextDateString;
                if (indexFuture < genSimsStudied.Count)
                    if (genSimsStudied[predictVars.Gen_Studied_Index].Top_Number == genSimsStudied[indexFuture].Top_Number)
                        genSimsStudied[indexFuture].Simulated_Date = nextDateString;
            }
            else
            {
                genSimsAll[predictVars.Gen_Projected_Index].Simulated_Date = predictVars.Sim_Date_Use;
                genSimsAll[predictVars.Gen_Projected_Index].Next_Date = nextDateString;
            }
        }
        /***********************************PREDICTION END*********************************************/
    }
}
