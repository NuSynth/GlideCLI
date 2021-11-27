namespace GlideCLI.Models
{
    public class CreationModel
    {
        public int chapterLoop { get; set; }
        public int currentChapter { get; set; }
        public int chaptersInt { get; set; }
        public int topicCounter { get; set; }
        public string topicCountString { get; set; }
        public int topicLoop { get; set; }
        public int subSectionCounter { get; set; }
        public string subSectionString { get; set; }
        public int subLoop { get; set; }
        public string filePath { get; set; }
        public int topicID { get; set; }
        public int currentSubSection {get; set;}

        //These are for the data loops
        public double problemCount {get; set;}
        public string pCountString {get; set;}
        public int currentTopic {get; set;}
        public int check {get; set;}
        public string topicString{get; set;}
        public string newTopName {get; set;}
        public string listFile {get; set;}
    }
}