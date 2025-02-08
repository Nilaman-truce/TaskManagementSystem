o	Explain setup instructions, dependencies, and key design decisions.

Clone or extract the file use master branch

Dependencies Install Dapper, Dapper.Contrib, Microsoft.Data.SqlCline(5.2.2) please do not install the 6.0.1 version, QuestPDF, ClosedXML.

I used .NET 7.0, ASP.NET CORE MVC, Dapper, SQL SERVER, JavaScript(jQuery), Bootstrap, HTML/CSS. Used dapper to connect the database and used Repository with raw SQL query and linked them with controller for CRUD operation. 
Created three Tables Tasks, Status and Priority. Status and Priority table are created inorder to show the dropdown and they are linked with Tasks table by their specific id. Wasn't able to use most of the jQuery 
methods due to is not a function issue so instead of using jquery to download excel and pdf document used ClosedXML for downloading excel and QuestPDF for downloading pdf. Same thing with pagination and search 
wasn't able to use jQuery methods so directly through controller, view and SQL query in dapper showed pagination and search. 

Change the defaultconnection in the appsetting.json. Their will be sample data in the SQL script(.sql file) 
