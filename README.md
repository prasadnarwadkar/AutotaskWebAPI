# AutotaskWebAPI
ASP.NET Web API which wraps Autotask (AT) SOAP API. It helps developers use the Web API from any client (HTML5 web app, AngularJS web app, desktop client and so forth). It is RESTful.

#Usage
This Web API is in development and will be updated regularly. However, at any given point in time, read me will be updated to reflect changes done.
##Namespaces used
Two prominent namespaces used are
```
AutotaskWebAPI.Autotask.Net.Webservices;
System.Web.Http;
```
Namespace ```AutotaskWebAPI.Autotask.Net.Webservices``` refers to [AT Web Services](https://www.autotask.net/help/Content/AdminSetup/2ExtensionsIntegrations/APIs/WebServicesAPI.htm). Please check the VS solutions demonstrating .NET code to query SOAP based AT API.
##Configuration
  All you need is two keys in appSettings (web.config) with values as per your Autotask Account.
  ```
  <add key="APIUsername" value="" />
  <add key="APIPassword" value="" />
  ```
##Querying Ticket notes in Autotask for example
Base url: http://localhost:{port number} or if you deploy it to IIS, http://localhost/{appname}

HTTP GET {base url}/api/note/GetById?id={note id in AT}
#Tools
You may want to use Postman (Chrome extension) to test the API. Any other RESTful API testing tool can also be used, even Fiddler can be used.
