#! /bin/bash

csc Program.cs /addmodule:Models/Constants.netmodule /t:module
csc Program.cs /addmodule:Models/CourseListModel.netmodule /t:module
csc Program.cs /addmodule:Models/CreateCourseVars.netmodule /t:module
csc Program.cs /addmodule:Models/Globals.netmodule /t:module
csc Program.cs /addmodule:Models/list.sh /t:module
csc Program.cs /addmodule:Models/PointLimits.netmodule /t:module
csc Program.cs /addmodule:Models/PredictModel.netmodule /t:module
csc Program.cs /addmodule:Models/SimModel.netmodule /t:module
csc Program.cs /addmodule:Models/StudyCourseVars.netmodule /t:module
csc Program.cs /addmodule:Models/TopicModel.netmodule /t:module 