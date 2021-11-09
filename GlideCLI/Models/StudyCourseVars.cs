using System;

namespace GlideCLI.Models
{
    public class StudyModel
    {
        //DateTimes
        public DateTime today {get; set;}
        public DateTime topicDate {get; set;}

        //Ints
        public int dateCompare {get; set;}
        public int lineCount {get; set;}
        public int index {get; set;}
        public int toStudyCount {get; set;}

        //Doubles
        public double numCorrectDouble {get; set;}

        //Strings
        public string dateAsString {get; set;}
        public string filePath {get; set;}
        public string topStudString {get; set;}
        public string topStudBool {get; set;}
        public string numCorrectString {get; set;}
        public string todayDateString {get; set;}
        public string response {get; set;}

        //Bools
        public bool studied {get; set;}
    }
}

//Maybe clear all of these at the end of StudyCourse