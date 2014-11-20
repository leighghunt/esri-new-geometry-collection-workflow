Notes for using ArcGIS for Windows Mobile Project Templates
====================================================================

Table of Contents
-----------------
1. Introduction to Project Templates
2. How to Use Project Templates

1. Introduction to Project Templates
------------------------------------
ArcGIS for Windows Mobile ships a task templates, an extension template and a few item templates that will be installed 
and integrated into Visual Studio 2008 IDE. These project and item templates can be used the same 
way as any other Visual Studio built-in templates.

To create either a task or extension project, simply choose the “Visual C# > ArcGIS Mobile” project type 
on the "New Project" dialog, and the template will create three projects for you within a solution.
These projects are:
 - .Net task or extension project to work with ArcGIS for Windows Mobile application running on Windows Mobile devices
 - .Net task or extension project to work with ArcGIS for Windows Mobile application running on Windows devices
 - .Net task or extension project to work with Mobile Project Center (MPC)

Note that the project template creates task or extension projects for both Windows Mobile and Windows platforms. 
If you intend to create task or project extension that works for both platforms, your next step 
would be the implementation of your functions within each of these two projects. If, however, you are only 
interested in creating your task or extension for handheld device, you can remove the project for the Windows platform, 
and vice versa. 

You need to use the MPC project to describe your task or extension that's deployed to the device, 
therefore you should always keep the MPC project in the solution.

2. How to Use Project Templates
-------------------------------
After you implement the functionality for your custom task/extension, you need to make sure the following attributes 
are the same for all the projects in the solution:
 -  Current Namespace
 -  Class name
 -  Assembly name (through Project's Property dialog)
 -  Default Namespace (through Project's Property dialog)

After you build the task/extension solution, the output of the MPC, Windows Mobile and Windows projects are three assemblies, 
and you need to deploy them to the ArcGIS for Windows Mobile "Extensions" folder and its sub-folders as follow:
 - Copy the MPC assembly to:
 -- For Windows 7 machines: C:\ProgramData\ESRI\MobileProjectCenter\Extensions
 -- For Windows XP machines: C:\Documents and Settings\All Users\Application Data\ESRI\MobileProjectCenter\Extensions
 - Copy the Windows Mobile and/or Windows assemblies to:
 -- For Windows 7 machines: C:\ProgramData\ESRI\MobileProjectCenter\Extensions\Win32 (for Windows devices); or C:\ProgramData\ESRI\MobileProjectCenter\Extensions\WinCE (for Windows Mobile devices)
 -- For Windows XP machines: C:\Documents and Settings\All Users\Application Data\ESRI\MobileProjectCenter\Extensions\Win32 (for Windows devices); or C:\Documents and Settings\All Users\Application Data\ESRI\MobileProjectCenter\Extensions\WinCE (for Windows Mobile devices).

Once you put the output assemblies to the above folders, MPC will be able to locate them when it starts up, and you can 
add the custom task and/or extension to your mobile project. Once you finish authoring the project and deploy it 
to the Windows Mobile or Windows device, the assemblies for the device will be deployed with the project automatically.

Please refer to "Using ArcGIS Mobile project templates" topic in the developer help for a walkthrough about 
using the project templates. You can also find a sample Hello World Task task at 
http://www.arcgis.com/home/item.html?id=3c34fa18e5274b26827a2e4ea306b17c.













