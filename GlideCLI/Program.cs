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

        

        public static void Main(string[] args)
        {
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
            const int ZERO = 0;
            const int ONE = 1;
            bool ask = true;
            string operatingSystem;
            int choice = ZERO;
            
            Console.Clear();
            while (ask == true)
            {
                Console.WriteLine("\n\n1. Linux\n2. Microsoft");
                Console.WriteLine("\n\nEnter the option number for which operating system you are using: ");
                try
                {
                    operatingSystem = Console.ReadLine();
                    choice = Convert.ToInt32(operatingSystem);
                    ask = false;
                }
                catch
                {
                    Console.WriteLine("\n\n\nInvalid input. ");
                    ask = true;
                }
            }
            if (choice == ONE)
            {
                Console.Clear();
                Console.WriteLine("\n\nOption 1 selected.");
                Linux();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n\nOption 2 selected.");
                Windows();
            }
        }
        private static void Linux()
        {
            var userName = Environment.UserName;
            string pathString = ($"//home//{userName}//Documents//GlideCLI");
            if (!Directory.Exists(pathString))
            {
                Directory.CreateDirectory(pathString);
            }
            globals.DirectoryPath = pathString;
            globals.osSwitch = true;
        }
        private static void Windows()
        {
            string directory = (@"\GlideCLI");
            string pathString = Convert.ToString(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + directory);
            string usablePath = pathString.Replace(@"\", @"\\");
            
            if (!Directory.Exists(usablePath))
            {
                Directory.CreateDirectory(usablePath);
            }

            globals.DirectoryPath = usablePath;
            globals.osSwitch = false;
        }
        private static void StartUp()
        {
            while (true)
            {
                //Remove stuff @332
                CheckForCountFile(); // Check for file that holds number of courses
                MainMenu();
                StudyIncrementer();
                ClearLists();
            }            
        }
        private static void CheckForCountFile()
        {
            const string ZERO = "0";
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
                File.WriteAllText(path, ZERO);
                courseCount = File.ReadAllText(path);
                globals.CourseCount = Convert.ToInt32(courseCount);
            }
        }
        private static void MainMenu()
        {
            // Go to option set set one if there are courses
            // Go to option set zero if there are no courses
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
                if (options == Constants.ZERO_INT)
                {
                    // Option set for no available courses
                    SelectionDialogs(Constants.ONE_INT);
                    selectionString = Console.ReadLine();
                    selectionInt = Convert.ToInt32(selectionString);
                    if (selectionInt == Constants.ONE_INT || selectionInt == Constants.TWO_INT)
                        MainOptions(selectionInt);
                }
                if (options == Constants.ONE_INT)
                {
                    //Option set if courses are available
                    SelectionDialogs(Constants.TWO_INT);
                    selectionString = Console.ReadLine();
                    selectionInt = Convert.ToInt32(selectionString);
                    if (selectionInt >= Constants.ONE_INT && selectionInt <= Constants.THREE_INT)
                        MainOptions(selectionInt);
                }                
            }
            //SelectionDialogs(Constants.THREE_INT);
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
                        Console.WriteLine("Study a course selected.\n");                        
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
                    Console.WriteLine("\n\n1: Exit the program");
                    Console.WriteLine("2: Create a new course\n");
                    Console.WriteLine("\n\n\nEnter an option from the menu: ");
                    break;
                case 2:
                    //For AvailableOptions()
                    // Option 2 if courses are available
                    Console.WriteLine("\n\n1: Exit the program");
                    Console.WriteLine("2: Create a new course");
                    Console.WriteLine("3: Study a course\n"); 
                    Console.WriteLine("\n\n\nSelect an option from the menu: ");
                    break;
                case 3:
                    //For StudyCourse()
                    Console.WriteLine("\n\n\nNothing left to study for current topic today.");
                    Console.WriteLine("Enter q to quit back to menu, or any other key to exit.");
                    globals.response = Console.ReadLine();                
                    if (globals.response == "q")
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
                        Console.ReadLine();
                        Environment.Exit(Constants.ZERO_INT);
                    }
                    break;
                case 4:
                    //For CreateCourse()
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
                    Console.WriteLine("Finished updating CourseCount.txt");
                    Console.ReadLine();
                    Console.Clear();
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
                        {
                            newTopic.Top_ID = Constants.ZERO_INT;
                        }
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
                        newTopic.Top_Repetition = Constants.ZERO_DOUBLE;
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
            {
                output.Add($"{topic.Top_ID},{topic.Course_ID},{topic.Top_Name},{topic.Top_Studied},{topic.Next_Date},{topic.First_Date},{topic.Num_Problems},{topic.Num_Correct},{topic.Top_Difficulty},{topic.Top_Repetition},{topic.Interval_Remaining},{topic.Interval_Length},{topic.Engram_Stability},{topic.Engram_Retrievability}");
            }
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
            const int ZERO = 0;
            const int ONE = 1;
            const int TWO = 2;
            string filePath;
            string selectionString;
            int selectionInt = Constants.ZERO_INT;
            bool validInput = false;
            if (globals.osSwitch == true)
            {
                // Linux
                filePath = $"{globals.DirectoryPath}//CourseList.txt";
            }
            else
            {
                // Windows
                filePath = $"{globals.DirectoryPath}\\CourseList.txt";
            }
            List<CourseListModel> completeList = new List<CourseListModel>();
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(filePath).ToList();
            Console.WriteLine("\n\n\n\n\nReading data.");
            foreach (string line in lines)
            {
                string[] entries = line.Split(',');
                CourseListModel newList = new CourseListModel();
                newList.Course_ID = Convert.ToInt32(entries[ZERO]);
                newList.Course_Name = entries[ONE];
                newList.File_Path = entries[TWO];
                completeList.Add(newList);
            }
            foreach (var course in completeList)
            {
                Console.WriteLine($"Course ID: {course.Course_ID} - Course Name: {course.Course_Name}");
            }
            while (validInput == false)
            {
                try
                {
                    Console.WriteLine("\n\nEnter the Course's ID number that you wish to study: ");
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
                    selectionInt = selectionInt - ONE;
                    var testVar = selectionInt + ONE;
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
            /*Start of section from LoadTopicIDs*/
            const string TRUE = "True";
            int index;
            DateTime today = DateTime.Parse(globals.TheDate);
            DateTime topicDate;            
            int dateCompare;
            string dateAsString;
            string filePath = globals.FilePath;
            string topStudString;
            List<string> lines = new List<string>();

            lines = File.ReadAllLines(filePath).ToList();
            Console.WriteLine("\n\n\n\n\nReading data.");
            Console.ReadLine();
            if (lines.Count > 0)
            {
                foreach (string line in lines)
                {
                    string[] entries = line.Split(',');
                    TopicModel newList = new TopicModel();

                    newList.Top_ID = Convert.ToInt32(entries[0]);
                    newList.Course_ID = Convert.ToInt32(entries[1]);
                    newList.Top_Name = entries[2];
                    topStudString = entries[3];
                    if (topStudString == TRUE)
                    {
                        newList.Top_Studied = true;
                    }
                    else
                    {
                        newList.Top_Studied = false;
                    }
                    

                    newList.Next_Date = entries[4];
                    newList.First_Date = entries[5];

                    newList.Num_Problems = Convert.ToDouble(entries[6]);
                    newList.Num_Correct = Convert.ToDouble(entries[7]);

                    newList.Top_Difficulty = Convert.ToDouble(entries[8]);
                    newList.Top_Repetition = Convert.ToDouble(entries[9]);
                    newList.Interval_Remaining = Convert.ToDouble(entries[10]);

                    newList.Interval_Length = Convert.ToDouble(entries[11]);
                    newList.Engram_Stability = Convert.ToDouble(entries[12]);
                    newList.Engram_Retrievability = Convert.ToDouble(entries[13]);

                    TopicsList.Add(newList);
                }
                // Retry Studied TopicID's section (these are study sessions that were missed)
                index = Constants.ZERO_INT;
                globals.lateLeft = Constants.ZERO_INT;
                Console.WriteLine($"globals.currentLeft = {globals.lateLeft}");
                while (index < TopicsList.Count)
                {
                    if (TopicsList.ElementAt(index).Top_Studied == true)
                    {
                        dateAsString = TopicsList.ElementAt(index).Next_Date;
                        topicDate = DateTime.Parse(dateAsString);
                        dateCompare = DateTime.Compare(topicDate, today);

                        if (dateCompare < Constants.ZERO_INT)
                        {                        
                            ToStudy.Add(index);
                            // to display number of late topics left to study
                            ++globals.lateLeft;
                        }
                    }
                    ++index;
                }
                

                // Studied TopicID's scheduled for today section
                index = Constants.ZERO_INT;
                globals.currentLeft = Constants.ZERO_INT;
                while (index < TopicsList.Count)
                {
                    if (TopicsList.ElementAt(index).Top_Studied == true)
                    {
                        dateAsString = TopicsList.ElementAt(index).Next_Date;
                        topicDate = DateTime.Parse(dateAsString);
                        dateCompare = DateTime.Compare(topicDate, today);

                        if (dateCompare == Constants.ZERO_INT)
                        {
                            ToStudy.Add(index);
                            
                            // to display number of on-time review topics left to study
                            ++globals.currentLeft;
                        }
                    }
                    ++index;
                }
                

                // New Topic ID's section
                index = Constants.ZERO_INT;
                globals.newLeft = Constants.ZERO_INT;
                while (index < TopicsList.Count)
                {
                    if (TopicsList.ElementAt(index).Top_Studied == false)
                    {
                        ToStudy.Add(index);

                        // to display number of topics left to study for the first time
                        ++globals.newLeft;
                    }

                    ++index;
                }
                

                globals.TopicID = ToStudy.ElementAt(Constants.ZERO_INT);
                globals.TopicIndex = Constants.ZERO_INT;       
                /*End of secton from LoadTopicIDs*/

                int toStudyCount = ToStudy.Count;
                string numCorrectString;
                string topStudBool;
                string todayDateString = today.ToString("d");
                if (toStudyCount > Constants.ZERO_INT)
                {
                    globals.ProblemsDone = false;
                }
                else
                {
                    globals.ProblemsDone = true;
                }

                string response = "0"; //Just added this to give option to go back to Ready Menu
                double numCorrectDouble = Constants.ZERO_DOUBLE;



                while (globals.ProblemsDone != true)
                {
                    
                    if (globals.TopicIndex < toStudyCount)
                    {
                        //Loop through this code, assigning values to it until the list of indexes runs out.
                        //TopicsList.ElementAt(globals.TopicIndex).Num_Correct;
                        Console.Clear();
                        Console.WriteLine($"Course Name: {globals.CourseName}");
                        Console.WriteLine($"Number of LATE practice to review: {globals.lateLeft}");
                        Console.WriteLine($"Number of ON-TIME practice topics to review: {globals.currentLeft}");
                        Console.WriteLine($"Number of NEW topics left: {globals.newLeft}");
                        Console.WriteLine($"Section: {TopicsList.ElementAt(globals.TopicID).Top_Name}");
                        Console.WriteLine($"Number of questions/problems: {TopicsList.ElementAt(globals.TopicID).Num_Problems}");                  
                        if (TopicsList.ElementAt(globals.TopicID).Top_Studied == true)
                        {
                            topStudBool = "true";
                        }
                        else
                        {
                            topStudBool = "false";
                        }


                        Console.WriteLine($"Previously Studied: {topStudBool}");
                        if (TopicsList.ElementAt(globals.TopicID).Top_Studied == false)
                        {
                            Console.WriteLine("\n\n\nEnter the quantity you answered correctly ");
                            Console.WriteLine("\n*OR*\n");
                            //The following option is added because I have found 
                            //that sometimes it is easier to set the questions up right before I
                            //study them, instead of setting them all up at once. And also incase
                            //I need to correct the number of questions that there are.
                            Console.WriteLine("\nEnter q to go back to menu or enter\"change\"(without quotes)to change number of TOTAL problems or questions, if you need to:");
                            response = Console.ReadLine();
                            if (response == "q" || response == "change")
                            {
                                if (response == "change")
                                {                            
                                    ChangeTopicQuestions();
                                    Console.WriteLine("\n\n\nEnter the quantity you answered correctly: ");
                                    response = Console.ReadLine();
                                }
                                else
                                {
                                    Console.Clear();
                                    globals.madeSelect = false;
                                    globals.newLeft = Constants.ZERO_INT;
                                    globals.currentLeft = Constants.ZERO_INT;
                                    globals.lateLeft = Constants.ZERO_INT;
                                    return;
                                }
                            }
                            
                            //Not an else since response expected to change if != "q"
                            if (response != "q" && response != "change")
                            {
                                numCorrectString = response;
                                numCorrectDouble = Convert.ToDouble(numCorrectString);
                                while (numCorrectDouble > TopicsList.ElementAt(globals.TopicID).Num_Problems)
                                {
                                    Console.WriteLine("value exceeds number of problems or questions.");
                                    Console.WriteLine("Input a value less than or equal to number or problems or questions.");
                                    Console.WriteLine("\nOR you can enter the word \"change\" without the quotes to change the number of total problems or questions:");
                                    response = Console.ReadLine();
                                    if (response == "change")
                                    {
                                        ChangeTopicQuestions();
                                        Console.Clear();
                                        Console.WriteLine("Re-enter number of problems or questions you respoded to correctly");
                                    }                                
                                    numCorrectString = Console.ReadLine();
                                    numCorrectDouble = Convert.ToDouble(numCorrectString);
                                }
                                TopicsList.ElementAt(globals.TopicID).Num_Correct = numCorrectDouble;
                                TopicsList.ElementAt(globals.TopicID).First_Date = todayDateString;
                                --globals.newLeft;
                                Console.Clear();
                            }

                        }
                        else
                        {
                            Console.WriteLine("\n\n\nPress any key to study next section.");
                            Console.WriteLine("Type q to go back to menu");
                            response = Console.ReadLine();
                            if (response == "q")
                            {
                                globals.madeSelect = false;
                                Console.Clear();
                                globals.newLeft = Constants.ZERO_INT;
                                globals.currentLeft = Constants.ZERO_INT;
                                globals.lateLeft = Constants.ZERO_INT;
                                return;
                            }
                            

                            dateAsString = TopicsList.ElementAt(globals.TopicID).Next_Date;
                            topicDate = DateTime.Parse(dateAsString);
                            dateCompare = DateTime.Compare(topicDate, today);
                            if (dateCompare < Constants.ZERO_INT)
                                --globals.lateLeft;
                            else if (dateCompare == Constants.ZERO_INT)
                                --globals.currentLeft;


                            Console.Clear();
                        }
                        numCorrectDouble = Constants.ZERO_DOUBLE;

                        // Call funtion to calculate topics from here.
                        CalculateLearning();
                        SaveProgress();

                        ++globals.TopicIndex;
                        if (globals.TopicIndex < toStudyCount)
                        {
                            globals.TopicID = ToStudy.ElementAt(globals.TopicIndex);
                        }                    
                    }
                    else
                    {
                        globals.ProblemsDone = true;
                    }
                }
                SelectionDialogs(Constants.THREE_INT);
            }
            else
                SelectionDialogs(Constants.THREE_INT);
        }
        private static void CalculateLearning()
        {
            const double ONE = 1;
            AddRepetition();

            double ithRepetition = TopicsList.ElementAt(globals.TopicID).Top_Repetition;
            if (ithRepetition == ONE)
            {
                TopicDifficulty();
            }

            IntervalTime();
            EngramStability();
            EngramRetrievability();
            ProcessDate();
        }
        private static void AddRepetition()
        {
            const double ONE = 1;
            double ithRepetition = TopicsList.ElementAt(globals.TopicID).Top_Repetition;

            ithRepetition = ithRepetition + ONE;
            TopicsList.ElementAt(globals.TopicID).Top_Repetition = ithRepetition;
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
            const double ONE = 1;
            const double SINGLE_DAY = 1440; // 1440 is the quatity in minutes of a day. I'm using minutes, instead of whole days, to be more precise.
            double difficulty = TopicsList.ElementAt(globals.TopicID).Top_Difficulty;
            double ithRepetition = TopicsList.ElementAt(globals.TopicID).Top_Repetition;
            double intervalRemaining = TopicsList.ElementAt(globals.TopicID).Interval_Remaining;
            double intervalLength = TopicsList.ElementAt(globals.TopicID).Interval_Length;

            //     Second repetition will occur the next day. 
            //	   Although, the research document does not precisely
            //	   state a time frame until the second repetition. The 
            //	   values of the two variables may need to be changed, 
            //	   if the spacing is too far apart.

            if (ithRepetition == ONE)
            {
                // The researech document says that s == r @ 1st repetition
                intervalRemaining = SINGLE_DAY;
                intervalLength = SINGLE_DAY;
            }
            else
            {
                intervalLength = intervalLength * difficulty;
            }

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
            {
                TopicsList.ElementAt(globals.TopicID).Top_Studied = true;
            }
        }
        private static void SaveProgress()
        {
            string filePath;
            string filePath2;
            List<string> output = new List<string>();
            foreach (var topic in TopicsList)
            {
                output.Add($"{topic.Top_ID},{topic.Course_ID},{topic.Top_Name},{topic.Top_Studied},{topic.Next_Date},{topic.First_Date},{topic.Num_Problems},{topic.Num_Correct},{topic.Top_Difficulty},{topic.Top_Repetition},{topic.Interval_Remaining},{topic.Interval_Length},{topic.Engram_Stability},{topic.Engram_Retrievability}");
            }
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
            double numTotalDouble;
            Console.Clear();
            Console.WriteLine("Enter new number of TOTAL questions:");
            numTotalString = Console.ReadLine();
            numTotalDouble = Convert.ToDouble(numTotalString);
            TopicsList.ElementAt(globals.TopicID).Num_Problems = numTotalDouble;
        }
        private static void StudyIncrementer()
        {
            ++globals.studyTracker;
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
    }
}
