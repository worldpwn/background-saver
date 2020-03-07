# BackgroundSaver
![badge](https://github.com/worldpwn/small-analytics/workflows/ci/badge.svg)

Net Standart 2.0 library for creating/adding `IHostedService` (Net Core 2+) that will save data from time to time.

## How to use
To implements it in your net core app please check out the example project **BackgroundSaver.WebAPI**. In the example, it uses EF Core (MsSQL) as a database, but the library itself relies on abstraction `BackgroundSaver.Core.DataStorage.IRepository<TModel>`, so you can use it with any variant for saving the data.  
