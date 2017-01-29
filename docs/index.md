# AutotaskWebAPI
ASP.NET Web API which wraps Autotask (AT) SOAP API. It helps developers use the Web API from any client (HTML5 web app, AngularJS web app, desktop client and so forth). It is RESTful.

# Benefits
I believe that the API will serve as an important toolset to create dashboards, both desktop and web-based for your Autotask account whereby your employees and clients can easily work with Autotask entities directly from your dashboard rather than navigating into Autotask portal. 
Your clients can log on to your dashboard using your own login mechanism and then they can use your dashboard functions to work with Autotask entities such as Accounts, Tickets, Contacts, Contracts, Ticket notes, attachments and invoices.

# Usage
This Web API is in development and will be updated regularly. However, at any given point in time, read me will be updated to reflect changes done. At present, it supports Tickets, Accounts, and Ticket Notes.

## Namespaces used
Two prominent namespaces used are `AutotaskWebAPI.Autotask.Net.Webservices` and `System.Web.Http`.

Namespace `AutotaskWebAPI.Autotask.Net.Webservices` refers to [AT Web Services](https://www.autotask.net/help/Content/AdminSetup/2ExtensionsIntegrations/APIs/WebServicesAPI.htm). Please check the VS solutions demonstrating .NET code to query SOAP based AT API.

## Configuration
All you need is two keys in appSettings (web.config) with values as per your Autotask Account.

```
<add key="APIUsername" value="" />
<add key="APIPassword" value="" />
```


## Querying Tickets by account id and status
A ticket is always associated with an account. It makes sense to query tickets by account id and to narrow down search, it helps to filter them by status (e.g. Complete, In Progress, Waiting Approval etc.).

Following is a lookup table for ticket status.

- In Progress	8
- New	1
- Complete	5
- Waiting Customer	7
- Waiting Materials	9
- Dispatched	10
- Escalated	11
- Waiting Vendor	12
- Waiting Approval	13
- Action required	14
- Information	15
- Assigned to GNOC	16

###### URL
HTTP GET to {base url}/api/ticket/GetByAccountIdAndStatus?accountId={}&status={}.
For example, {base url}/api/ticket/GetByAccountIdAndStatus?accountId=12345678&status=8 returns all tickets with account id 12345678 and status as 8 (In Progress).

## Querying Ticket notes in Autotask

Base url: http://localhost:{port number} or if you deploy it to IIS, it is http://localhost/{appname}
You can query ticket notes by note id. This query returns a note matching the given id.

HTTP GET {base url}/api/note/GetById?id={note id in AT}

You can also query ticket notes by ticket id. This query returns all notes added to the given ticket.

HTTP GET {base url}/api/note/GetByTicketId?ticketId={ticket_id}

## Posting a ticket note for a ticket is simpler

HTTP POST to {base url}/api/note/post with the following body. Of course, the content type is application/json.

```
{"TicketId":"", "CreatorResourceId":"", "Title":"test note", "Description":"Description" ,"NoteType":3, "Publish":1}
```

-`CreatorResourceId` is creator resource id which is resource id of the note creator.

-`TicketId` is the id of the ticket to which new note is being added.

-`Title` is note title.

-`Description` is note description.

-`NoteType` is note type. e.g for a user note it is 3. Please refer to [AT Web Services](https://www.autotask.net/help/Content/AdminSetup/2ExtensionsIntegrations/APIs/WebServicesAPI.htm).

-`Publish` is 1 to publish.

## Get Attachments by parent id (such as a ticket id) and attach date
When you query an attachment, you first query an `AttachmentInfo`. Using id of a certain attachment info object, you can query the actual attachment.
This is tricky but that is how it is structured in Autotask. 

So first, you would get attachment info:

`{base url}/api/attachment/GetInfoByParentIdAndAttachDate?parentId=123&attachDate=2016-12-22T00:00:00`

This will return a list of `AttachmentInfo` objects.

Then for each `AttachmentInfo` object, you would get its id using `AttachmentInfo.id`. From this id, you would get the actual attachment as follows.

`{base url}/api/attachment/GetById?id=123`

This will return attachment byte array content which can be consumed at client side.

More examples of usage will appear here soon.

# Error handling
Error handling is quite extensive in this API. I have used error messages from AT SOAP API which are quite user-friendly. For example, while creating a ticket, you must pass both assigned resource id and assigned resource role id together or else there will be an error which the message properly indicates. Please check the error message in the http response in case of an error. If there is no error, the response contains JSON object containing all data you requested. Sometimes you might get an empty list of entities which is alright if the parameters do not match any of the entities in the AT database. If there is no error, your query is deemed to have worked.

# Tools
You may want to use Postman (Chrome extension) to test the API. Any other RESTful API testing tool can also be used, even Fiddler can be used. I have used Postman and found it suitable for my purposes.
