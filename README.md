# Update Nov 20th 2021: Completion prediction - a new feature coming soon!
I decided to add another feature before converting the C# code into Vala for the simple GUI version using gtk, and then C with tcl/tk for FreeClear. It is going to predict the date that the last topic will be studied. I need this feature because I need to be able to know when I will finish studying each course. This will allow me to know this.

It will be ready soon.


# Previous update
I didn't plan to update this, but this morning I thought some specific features would be nice to have now before they are implemented in FreeClear.

Updated to:
* display number of topics left
* allows user to switch topics
* exit from menu
* Change number of total questions on first study of a topic
   
There may be about 2 more major updates to GlideCLI before FreeClear is finished:
1) I'm probably going to port this code over to Vala. This will make the program more accessible to Linux users. Right now people have to install .Net, and then if I don't update the csproject file when microsoft updates .Net, then it won't compile on machines only running the newest .Net version. So, at least it will be accessible for some people. Plus, it doesn't look like I would have to change much to the actual source code to port it over to Vala.

2) If I port it to Vala, then I'm going to give it a GUI. The GUI for this Vala version will give me a good idea of how I want the FreeClear GUI to be organized at first. FreeClear is going to use a different widget kit though. So this will still be a prototype for FreeClear.


# Updates 10-17-2021
I was going to re-write GlideCLI in the C language. I still am, but it is getting a different name, and is going to be much more than just a port of GlideCLI from C# to the C language. GlideCLI is now just a prototype to part of a new software application coming soon, which is designed to help people learn.

# FreeClear:
 https://github.com/Dartomic/FreeClear - It is a replacement for this program - Not ready, but coming soon!







# About
Glide allows people who use it to study more efficiently, and learn at a very fast rate. 

I took instructions down for how to compile or run this app, because .Net updates cause me to have to update the project's dependencies that .Net produces in the project. Just wait for FreeClear if you want to try it out. FreeClear v1.0 will be just like this one, except without the initial hassle of getting it to run.

GlideCLI will recieve no further updates, except to this readme to just point you towards FreeClear once v1.0 is finished.






# Difference between this software, and the research
The current research for the calculation of difficulty, which is used to calculate the interval length between study sessions, it is also used in the calculation of engram stability, and it is also used in the calculation of the forgetting curve, is inaccurate.The research for the currently accepted calculation of difficulty can be found here: https://github.com/Dartomic/GlideCLI/blob/master/easinessFactor.pdf

The calculation is inaccurate, because it can produce a result below the value of 1.3, which would cause the interval length between study sessions to be far too short. The way the researchers would go around that, is by setting the veriable to 1.3 if it was calculated as being below 1.3... The problem with this, is that their formula in the Easiness Factor research paper that I linked to, is calculating an innacurate value for everything. Information that was calculated as being a difficulty of 1.3, and was easier to learn than the hardest material, which is calculated as less than 1.3, does not all of a sudden become equally as difficult to maintain as the hardest material, when the hardest material is raised to a value equal to 1.3. 

I had to invent a way to calculate difficulty, because I could not find that research document on the Easiness Factor, until after I had written this program. I knew that the calculation of difficulty had to be constrained between these values inclusively, (1.3, and 2.5), and the image of a sloped line on a graph, intersecting y at 1.3, and x at 0, just popped into my head. So I applied the point slope formula to calculate difficulty, and it works perfect. So perfect, that difficulty does not need to be re-calculated for every repetition. It only needs to be calculated on the first repetition. If you look at the pdf file that I linked in the about section (https://github.com/Dartomic/GlideCLI/blob/master/Manual.pdf), I go into great detail to explain how difficulty is calculated there. Although, it is very simple. I just wanted to be as detailed as I could be so that it is understood how it is more accurate than the research article's version.
