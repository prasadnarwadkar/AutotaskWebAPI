# AutotaskWebAPI
ASP.NET Web API which wraps Autotask (AT) SOAP API. It helps developers use the Web API from any client (HTML5 web app, AngularJS web app, desktop client and so forth). It is RESTful.

# Usage

Examples of Usage and other info is available [here](https://prasadnarwadkar.github.io/AutotaskWebAPI/).

## Namespaces used
Two prominent namespaces used are
```
AutotaskWebAPI.Autotask.Net.Webservices;
System.Web.Http;
```
Namespace ```AutotaskWebAPI.Autotask.Net.Webservices``` refers to [AT Web Services](https://www.autotask.net/help/Content/AdminSetup/2ExtensionsIntegrations/APIs/WebServicesAPI.htm). Please check the VS solutions demonstrating .NET code to query SOAP based AT API.

## Configuration
  All you need is two keys in appSettings (web.config) with values as per your Autotask Account.
  ```
  <add key="APIUsername" value="" />
  <add key="APIPassword" value="" />
  ```

## Request Authorization Header

If the configuration is not set correctly, you can still use the API by sending a basic scheme authorization header with every request.

