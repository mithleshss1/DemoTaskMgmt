# DemoTaskMgmt

#1 Extract the TaskManagement.zip file

#2 Open Project in visual studio by triggering TaskManagement.sln file located in the folder.

#3 Locate appsettings.json and update "TaskDBContext" key pointing to your SQL Server Instance.

#4 In VS navigate to Tools > NuGet Package Manager > Package Manager Console and run the following command to create database at your local sql server Instance defined in step 3:
Update-database -V

#5 Run the project using F5 

#6 In order to execute UnitTest locate TaskManagement.Tests folder in Windows Explorer and run the command prompt (CMD) from there and run the following command:
DOTNET Test

#7 In case if you encounter any error stop the debugging and clean the solution from VS.
