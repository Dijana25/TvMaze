Assignment:

We want you to create an application that:
1. scrapes the TVMaze API for show and cast information;
2. persists the data in storage;
3. provides the scraped data using a REST API.
We want the REST API to satisfy the following business requirements.
1. It should provide a paginated list of all tv shows containing the id of the TV show and a list of 
all the cast that are playing in that TV show.
2. The list of the cast must be ordered by birthday descending.
The REST API should provide a JSON response when a call to a HTTP endpoint is made (it's up to you 
what URI).

*********************************************

Environment:
  .Net Core 2.2
  Sql Server Database - (new database created 'TVMazeDatabase')
  
Connection string:
  Connection string 'TvMazeDbConnection' is retrieved from the environment variables:
  Server=localhost\SQLEXPRESS;Database=TvMazeDatabase;Trusted_Connection=True;  
  
Projects:
  1. TvMaze.DataAccess     -  Responsible for managing the data, contains migrations, first the database should be updated
  2. TvMaze.Scraper.Runner -  Console application that runs Scraper in order to scrape the data from TVMazeAPI and persist them in database, 
                              should be launched manually
  3. TvMaze.ShowCastRestApi - Rest API that exposes the required cast information per show
  
  Note: The projects TvMaze.Scraper, TvMaze.ApiClient are libraries that contain logic for connecting to TvMaze API and scraping the data      

API Url:
https://localhost:44323/api/shows

Swagger Url:
https://localhost:44323/swagger/index.html

  
  
