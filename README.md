# EF7WhoAreYou
Slides and Demos from 2015 Presentation

Demos and PowerPoints for Julie Lerman's "EF7: Who are you and what have you done to my ORM?" presentation.  
This talk was given at Boston Code Camp (March 21, 2015), Techorama (May 2015) and DevSum15 (May 2015).  
The ASPNET5 app is just a simple setup so you can see how EF7 plugs in.  
The Familiar And Enhancements app is full .NET with lots of InMemory and SqlServer tests to test out behavior, exploring what works about the same and what's new or changed. One of the things I spent a lot of time on is looking at the behavior of attaching disconnected entities. This is not exhaustive, just some things of interest that I can show in teh scope of a short conference talk. If you want exhaustive tests of every feature and behavior, you'll want to check out the http://github.com/aspnet/entityframework source.

****************************
December 2015: Updated "Familiar & Enhancemenets" solution (this uses full .net) to RC1-Final. Updated the ASPNET5WebAPI to RC1-Final and also refactored it for a bit more like patterns I would use. For example, I put the dbcontext calls into a class that is designed to ensure that only notracking queries are executed since the dbcontext goes out of scope immediately and creating the tracking classes is a waste of time. 
****************************
October 2015: Updated the Familiar & Enhancements solution to Beta 8. Removed Console App. Replaced with tests and added a few new ones. That solution uses .NET 4.6, not ASP.NET 5.

Beta 8 represents a "feature complete" version of what will be the EF7 pre-release along with ASPNET5. It is still not for production.
****************************

See Readme.txt files in individual demo folders for details about each demo.

May 2015->Please note! EF7 is really considered an Alpha at this point. EVERYTHING is subject to change. Keeping these demos working even from one "stable" beta (from nuget not from nightly builds) to another has been challenging so please keep that in mind. The goal of this session was to give you an idea of how things will be working not to teach you how to use EF7.

Demos were created in VS2015RC.

If you want to see these demos in action, check out my "Looking Ahead to Entity Framework 7" course on Pluralsight at bit.ly/PS-EF7Alpha.

Julie Lerman
thedatafarm.com
