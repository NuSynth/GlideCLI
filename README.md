# Updates 10-17-2021
I was going to re-write GlideCLI in the C language. I still am, but it is getting a different name, and is going to be much more than just a port of GlideCLI from C# to the C language.

# New Program:
GlideCLI is now considered a prototype to part of a program designed to both help people become educated, and to end 
the current education system. From what I hear; GlideCLI is too much of a tedious process for people to learn to use, 
so I don't blame the lack of interest in it. But the upcoming program that uses a lot of GlideCLI's design is going to 
change the world. So stay tuned!


FreeClear: https://github.com/Dartomic/FreeClear - It is a replacement for this program - Not ready, but coming soon!



# About
Glide allows people who use it to study more efficiently, and learn at a very fast rate. 

It runs on Linux. Follow the below instructions.

Potential Windows problem:
I know that it ran on Windows when I first made GlideCLI. I built it on Windows when I was a Windows user, and the binary did run on other peoples Windows computers at the time too. Last time I tried running the binary on someone's Windows machine, it didn't run. If you really want to try the program, then you can follow the steps below. It might work for your Windows system, but I have no idea if it will or not. I think it had something to do with a change Microsoft made in the implementation of .Net in a Windows update.



# To run the software:

I am almost done writing FreeClear. FreeClear will work on Linux and Windows without any of this initial hassle that didn't exist for GlideCLI when I first wrote it. So I recommend waiting for FreeClear to release.

If you still want to get GlideCLI up and running after I said all of that, and after that warning I gave about it probably not working on Windows, (which is weird since it runs on Linux but it's using Microsoft's dot Net system), then follow the instructions below.

1) Install .Net from Microsoft: https://dotnet.microsoft.com/download
2) If you just want to use the program:

For Windows users:
The binary used to run on other people's Windows PC's, but last time I tried to run this on someone else's Windows PC, it wouldn't run. I don't have Windows anymore, so I wasn't able to figure out what the deal is. Unless you can figure out how to make the binary run, then you can try compiling it from it from the source code on your own machine. But I really don't know if that will make a difference. You can follow the instructions that I give for Linux users. Again; I recommend waiting for FreeClear.

If you just want to run the binary, then you can download the zip file of the program, and unzip it. https://github.com/Dartomic/GlideCLI/raw/master/glideCLI.zip



For Linux Users:
You can drag and drop the file into your open terminal, and press the Enter key.

3) To compile it from source code: 
   1. Download the source code.

   2. If you are on Windows, then I think you can use Visual Studio. You can also build by 
      following the rest of the directions.

   3. Open a terminal, or console, and type dotnet --version.

   4. Copy the number before the decimal, and only one of the numbers after the decimal place.

   5. Extract the source code wherever you want. Open it. Go into the GlideCLI directory inside of it.

   6. Open the file GlideCLI.csproj with a text editor.

   7. Got to the line that says  <TargetFramework>netcoreapp3.1</TargetFramework>

   8. Change the part that says "netcoreapp3.1" to "net5.0"

   9. Change the two numbers from 5.0 to the first two numbers before and after the decimal that you copied from the console.

   10. Save and close this file.
   
   11. Open a terminal or console in the directory that has this file.

   12. Type this in: dotnet run

   13. press the 'Enter' or 'Return' key

   14. Now it should have compiled.

   15. You can run it by going into the directory that just got created called "bin"

   16. Go into the directory called "Debug"

   17. Go into the next directory called "netcoreapp3.1"

   18. The binary is the file that just says "GlideCLI". 
   
   On Linux you can drag and drop the file into an open terminal emulator,at least for all of the ones I have tried that are newer than xterminal. 

   On Windows, I think you can just double click the program. You might be able to drag and drop the program into the console and and pressing the enter key like you can do on Linux. But if it doesn't run for you on Windows, even though it's a program you just compiled yourself, then I'm really sorry. Maybe try right clicking and running as administrator. I don't have Windows right now to know what needs to be changed in the program from when I first wrote it

Navigate to the directory that has the file labeled as 'Program.cs", open a command prompt, or a terminal there, then enter: dotnet run. You can run the program that way every time, or run it from the directory labeled bin.

For instructions on how to use the software, and to read about this software, read this pdf: https://github.com/Dartomic/GlideCLI/blob/master/Manual.pdf


# Difference between this software, and the research
The current research for the calculation of difficulty, which is used to calculate the interval length between study sessions, it is also used in the calculation of engram stability, and it is also used in the calculation of the forgetting curve, is inaccurate.The research for the currently accepted calculation of difficulty can be found here: https://github.com/Dartomic/GlideCLI/blob/master/easinessFactor.pdf

The calculation is inaccurate, because it can produce a result below the value of 1.3, which would cause the interval length between study sessions to be far too short. The way the researchers would go around that, is by setting the veriable to 1.3 if it was calculated as being below 1.3... The problem with this, is that their formula in the Easiness Factor research paper that I linked to, is calculating an innacurate value for everything. Information that was calculated as being a difficulty of 1.3, and was easier to learn than the hardest material, which is calculated as less than 1.3, does not all of a sudden become equally as difficult to maintain as the hardest material, when the hardest material is raised to a value equal to 1.3. 

I had to invent a way to calculate difficulty, because I could not find that research document on the Easiness Factor, until after I had written this program. I knew that the calculation of difficulty had to be constrained between these values inclusively, (1.3, and 2.5), and the image of a sloped line on a graph, intersecting y at 1.3, and x at 0, just popped into my head. So I applied the point slope formula to calculate difficulty, and it works perfect. So perfect, that difficulty does not need to be re-calculated for every repetition. It only needs to be calculated on the first repetition. If you look at the pdf file that I linked in the about section (https://github.com/Dartomic/GlideCLI/blob/master/Manual.pdf), I go into great detail to explain how difficulty is calculated there. Although, it is very simple. I just wanted to be as detailed as I could be so that it is understood how it is more accurate than the research article's version.
