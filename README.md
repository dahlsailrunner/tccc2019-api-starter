# TCCC 2019 API Starter
[![Build status](https://knowyourtoolset.visualstudio.com/TCCC-2019/_apis/build/status/TCCC2019-Build%20and%20Publish%20API%20Starter%20Template)](https://knowyourtoolset.visualstudio.com/TCCC-2019/_build/latest?definitionId=15)

This ASP.NET Core API starter template has the following features:
* Swagger doc and UI - and starting the project starts on the swagger UI page
* OAuth2 bearer support from a public demo identity server (but configurable)
* All api routes are secured, and all do performance logging (to SQL Server)
* Errors are shielded from callers and logged to an ELK instance
* CORS support is added and supports a local Angular SPA to start
* Most configuration is in ``appsettings.json``

## Installation 
Installation of the templates is simple.  

Download the template from Azure DevOps (use the download link): https://knowyourtoolset.visualstudio.com/TCCC-2019/_packaging?_a=package&feed=TCCC-2019&package=Tccc2019.ApiStarter&protocolType=NuGet&version=1.0.0

`dotnet new -i C:\users\<yourusername>\Downloads\Tccc2019.ApiStarter`

If you need to set back your dotnet new list to "factory defaults", use this command:

`dotnet new --debug:reinit`

## Prerequisites for Out-of-the-Box Execution
* SQL Server - SQL Express is free and you can just create a ``Logging`` database and leave it empty
* ELK instance - I used the ``sebp/elk`` docker image. 

To get the ELK instance running (assuming you have Docker installed):
``docker pull sebp/elk`` (this gets the image onto your computer)
``docker run -p 5601:5601 -p 9200:9200 -p 5044:5044 -it --name elk sebp/elk``  (this runs the ELK instance)

The ELK docker container is well-documented here: https://elk-docker.readthedocs.io/

## Template Installation

### **dotnet new tccc-api**
This template includes a Swagger user interface (via Swashbuckle.AspNetCore) and can accept bearer tokens from the green book identity server.

Here is the command to create a new project from the rpapi template:

`dotnet new tccc-api -o <projname>`

Here's a concrete example:

`dotnet new tccc-api -o Tccc.ApiSample`

The above command will create a new directory in your current working directory called `Tccc.ApiSample` (from the -o output directory parameter) and create a project in that directory called `Tccc.ApiSample`.


#### Testing an error
Try the ``thingies/{id}`` route with a value >= 2 and you will get a an exception that should be handled and logged.

## ``appSettings.json`` Configuration

Name | Purpose | Default value
--- | --- | ---
``ConnectionStrings.LoggingDb`` | Defines the database that performance logs will be written to (they go to a table called ``PerfLog``) | "Server=.\\sqlexpress;Database=Logging;Trusted_Connection=True;"
``ElasticsearchUri`` | Defines the Elasticsearch cluster that error logs are written to (the index pattern is ``error-*``) | http://localhost:9200
``Authority`` | Defines the OpenId-Connect / OAuth2 server that for authentication (a client for the Swagger UI is in the code) | https://demo.identityserver.io
``ApiErrorUrl`` | Get this from Kibana AFTER posting an initial error to Elasticsearch | http://localhost:5601/goto/37e0af6fc02e0aee006ed95521550d5c
``ApiName`` | OAuth2 "resource name" representing your api | api
``CorsOrigins`` | Domains you want to allow for browser-based api calls | http://localhost:4200


## Todos in the projects
When you create a project there are a few steps you need to follow.  They are documented with the following syntax:

`//TODO: take this action`

So if you open a "Task View" in Visual Studio you should be able to see the short list of things you probably need to follow-up on to really get your project set up correctly.


## Contributing
If you'd like to add more templates or update the existing ones:

- clone the repo
- branch it
- make your changes, update the semantic version of the `.nuspec` file, and update the readme.md and/or releasenotes.md documents
- commit and publish your new branch
- submit a pull request

Upon completion of the pull request, a new version of the package will be published.
