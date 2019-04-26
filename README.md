# GlideCLI

Instructions on how to use GlideCLI are here: https://github.com/Dartomic/GlideCLI/blob/master/How%20to%20use.md

# For Linux: 
You need to install .Net core, and go into the GlideCLI directory within the project folder, open a terminal from there, and type: dotnet run

You can probably also compile the program from monodevelop, but I haven't tried.

# For Windows: 
You need to have .Net core. You can use Visual Studio. I compiled GlideCLI from Visual Studio in Windows, but I have only tested that everything works on Linux. (I like Visual Studio, but I don't like using Windows LOL). If there is an issue with it saving and loading work on Windows, just open an issue here, and I'll fix it.

# About
Glide allows people who use it to study more efficiently, and learn at a very fast rate. 

To run the software, install .Net Core, made by Microsoft. Navigate to the directory that has the file labeled as 'Program.cs", open a command prompt, or a terminal there, then enter: dotnet run

For instructions on how to use the software, and to read about this software, look at this power point: https://github.com/Dartomic/GlideCLI/blob/master/Manual.pptx


# Difference between this software, and the research
The current research for the calculation of difficulty, which is used to calculate the interval length between study sessions, it is also used in the calculation of engram stability, and it is also used in the calculation of the forgetting curve, is inaccurate.The research for the currently accepted calculation of difficulty can be found here: https://github.com/Dartomic/GlideCLI/blob/master/easinessFactor.pdf

The calculation is inaccurate, because it can produce a result below the value of 1.3, which would cause the interval length between study sessions to be far too short. The way the researchers would go around that, is by setting the veriable to 1.3 if it was calculated as being below 1.3... The problem with this, is that their formula in the Easiness Factor research paper that I linked to, is calculating an innacurate value for everything. Information that was calculated as being a difficulty of 1.3, and was easier to learn than the hardest material, which is calculated as less than 1.3, does not all of a sudden become equally as difficult to maintain as the hardest material, when the hardest material is raised to a value equal to 1.3. 

I had to invent a way to calculate difficulty, because I could not find that research document on the Easiness Factor, until after I had written this program. I knew that the calculation of difficulty had to be constrained between these values inclusively, (1.3, and 2.5), and the image of a sloped line on a graph, intersecting y at 1.3, and x at 0, just popped into my head. So I applied the point slope formula to calculate difficulty, and it works perfect. So perfect, that difficulty does not need to be re-calculated for every repetition. It only needs to be calculated on the first repetition. If you look at the power point that I linked in the about section, I go into great detail to explain how difficulty is calculated there. It is very simple.
