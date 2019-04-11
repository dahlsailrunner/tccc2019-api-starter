# TCCC 2019 API Starter

ASP.NET Core WebAPI templates that add Swagger, error handling/logging, and authN/Z to project.

## Installation 
Installation of the templates is simple.  

`dotnet new -i RealPage.ApiTemplates --nuget-source https://realpage.myget.org/F/rp-packages/auth/7847a040-3b78-4f9c-9a4b-3981f6d860db/api/v3/index.json`

If you need to set back your dotnet new list to "factory defaults", use this command:

`dotnet new --debug:reinit`

## Templates

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
RP_Authority | Defines the identity server (green book) url that will issue tokens for the api | https://myldev.corp.realpage.com/identity
RP_Authority2 | Same as above but allows for a second id server instance in non-prd environments | https://demo.identityserver.io
RP_Client | OpenID Connect client id for signing in via Swagger | implicit
RP_Scope | OAuth2 scope that will be required for this api | api
RP_LogApiUrl | URL for logging API to write log entries to ELK stack (only applies to rpapieh template) | http://logapidev.realpage.com/api/


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
