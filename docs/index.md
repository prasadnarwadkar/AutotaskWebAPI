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
##Posting a ticket note for a ticket is simpler.
HTTP POST to {base url}/api/ticketnote/post with the following body. Of course, the content type is application/json.
```
{"TicketId":"", "CreatorResourceId":"", "Title":"test note", "Description":"Description" ,"NoteType":3, "Publish":1}
```
```CreatorResourceId```is creator resource id which is resource id of the note creator.

```TicketId``` is the id of the ticket to which new note is being added.

```Title``` is note title.

```Description``` is note description.

```NoteType``` is note type. e.g for a user note it is 3. Please refer to [AT Web Services](https://www.autotask.net/help/Content/AdminSetup/2ExtensionsIntegrations/APIs/WebServicesAPI.htm).

```Publish``` is 1 to publish.
#Tools
You may want to use Postman (Chrome extension) to test the API. Any other RESTful API testing tool can also be used, even Fiddler can be used.
