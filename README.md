# GlideCLI

Instructions on how to use GlideCLI are here: https://github.com/Dartomic/GlideCLI/blob/master/How%20to%20use.md

# For Linux: 
You need to install .Net core, and go into the GlideCLI directory within the project folder, open a terminal from there, and type: dotnet run

You can probably also compile the program from monodevelop, but I haven't tried.

# For Windows: 
You need to have .Net core. You can use Visual Studio. I compiled GlideCLI from Visual Studio in Windows, but I have only tested that everything works on Linux. (I like Visual Studio, but I don't like using Windows LOL). If there is an issue with it saving and loading work on Windows, just open an issue here, and I'll fix it.

# About
Glide implements calculations from scientific research on the forgetting curve, to present study material at the right time.


After I made Glide-UWP, I started working on porting the application over to Linux, and was going to build the Linux version of Glide with the ability to build a course, and keep a library of courses that have been built. Then, I was going to add that functionality into the UWP version of Glide, so that a user would not have to touch a single line of code. But I have been wanting to actually use the program for a while, and decided to use this version on Linux, before editing the UWP version any futher. I realized that making and editing images from a text book of all of the math lessons, problems, and answers, could take weeks. Hardcoding the UWP version of Glide to use the images wouldn't take as long as making them, but it would still take a while, since I hadn't yet built the parts for the users to add a course from the GUI.

I decided that I would rather use flash cards, or just hide the answer of a math problem that is given in a text book, for me to check when I finish working the problem, instead of spending weeks doing the work it would have taken to build images of a course for me to study in the UWP version of this program.


This version of Glide does not do what B.F. Skinner's "GLIDER" machine did, because you have to use a book with questions and answers, and reveal the answer yourself, or use flashcards, and reveal the answer yourself. B.F. Skinner's "GLIDER" machine presents a question to the respondent, and the correct answer to the respondent, once the respondent is finished producing his or her own answer. "GLIDER" implements a continuous reinforcement schedule, just like using flash cards would. It also scheduled the spacing of repetitions, but that schedule had no way of being precise. The production of that schedule was managed by a human, and the human that managed the production of schedules, did nothing more than to guess when the material should be restudied.


GlideCLI implements calculations from research on the forgetting curve, to schedule the spacing of repetitions for the user. GlideCLI also allows the user to add courses into the program very easily. Just prepare your flash cards if you need them, for every section of every chapter, make sure you know which section of which chapter they belong, then start GlideCLI. This program is a lot better than the machine called "GLIDER", as long as you check your answer after every problem, or question.

Research that all calculations are based on, except for two, can be found at https://github.com/Dartomic/GlideCLI/blob/master/5535.pdf

I was unaware of the existence of this paper, for the formula that calculates difficulty, until someone helped me find it: https://github.com/Dartomic/GlideCLI/blob/master/easinessFactor.pdf I was going to implement it too, if it produced more accuracy than the formula that I implement in this software, but it does not. Before I did a single calculation, I was unable to think of how my program could produce any more accuracy in this calculation than it currently does, because it is not possible. To my surprise, the formula that calculates difficulty, in the peer-reviewed research article titled "Optimization of repetition spacing in the practice of learning," is less accurate. 

Here is how GlideCLI is more accurate than similar software applications: https://github.com/Dartomic/GlideCLI/blob/master/Differences.md


The calculation that is not available in either paper, is the amount of time that should exist between the first and second repetitions. Most studies use 24 hours, so that's the amount of time that is used in this software. I also use the more accurate Slope-Intercept formula to measure difficulty.


This works on Windows, and Linux. It may also work for macOS if you select Linux as your operating system, but I don't have macOS, so I don't know. 
