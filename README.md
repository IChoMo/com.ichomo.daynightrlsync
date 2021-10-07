Created by David McKnight 2021

Thanks for downloading!

This project works in Unity and will:

 - Create a 12 hour clock + AM or PM
 - Show the name of the location of said timezone 
 - set the suns rotation to match the time of day based of of the set sunrise & sunset times

====================================================

SETUP:

 - Create a text object in your unity scene to hold the current time and current location
 - Add the DayNightRLSync to any in game object
 - Drag and drop your directional light and text objects to populate the DayNightRLSync script
 - Fill in the "Region Location Lable" & "Region Time Zones" arrays with as many diffrent locations as you would like 

example of time zones you can use:


Location name:          Time reletive to utc/gmt time:

London                  1
Amsterdam               2
Montreal                -4
Tokyo                   9
San Jose                -7
