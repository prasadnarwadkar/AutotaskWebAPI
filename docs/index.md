# AutotaskWebAPI
This is an ASP.NET Web API which wraps Autotask (AT) SOAP API. It helps developers use the Web API from any client (HTML5 web app, AngularJS web app, desktop client and so forth). It is RESTful. Using the SOAP API directly is tedious and needs wrapping code in clients like JavaScript/jQuery, web apps and desktop apps. This ASP.NET Web API fulfils this need. 

# Benefits
I believe that the API will serve as an important toolset to create dashboards, both desktop and web-based for your Autotask account whereby your employees and clients can easily work with Autotask entities directly from your dashboard rather than navigating into Autotask portal. 
Your clients can log on to your dashboard using your own login mechanism and then they can use your dashboard functions to work with Autotask entities such as Accounts, Tickets, Contacts, Contracts, Ticket notes, attachments and invoices.

# Usage
This Web API is ready for use in production. However, it will be updated regularly with new features. Of course, at any given point in time, read me will be updated to reflect changes done.

## Namespaces used
Two prominent namespaces used are `AutotaskWebAPI.Autotask.Net.Webservices` and `System.Web.Http`.

Namespace `AutotaskWebAPI.Autotask.Net.Webservices` refers to [AT Web Services](https://www.autotask.net/help/Content/AdminSetup/2ExtensionsIntegrations/APIs/WebServicesAPI.htm). Please check the Visual Studio solutions demonstrating .NET code to query SOAP based AT API.

## Configuration
All you need is two keys in appSettings (web.config) with values as per your Autotask Account. Your Autotask API username must be enabled for API access in order to use this ASP.NET Web API and underlying SOAP API.

```
<add key="APIUsername" value="" />
<add key="APIPassword" value="" />
```

## Request Authorization header

If the web.config keys for API username and password are left blank, you may still invoke the Web API by sending an auth header with every request.


###### Example of setting an auth header from .NET console app

```

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BasicAuthentication.Client
{
    class Program
    {       
        const string ApiOpEndPoint = "http://serverName/api/accounts/test";

        static void Main()
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine("Sending request with basic auth header...");
                Console.WriteLine("Response is: ");
                Console.WriteLine(await TryRequestAsync(client, 
                                  CreateBasicCredentials("myUserName", 
                                                        "myPassword")));
            }
        }

        static async Task<string> TryRequestAsync(HttpClient client, 
                             AuthenticationHeaderValue authorization)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, 
                                                ApiOpEndPoint))
            {
                request.Headers.Authorization = authorization;

                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    Console.WriteLine("{0} {1}", (int)response.StatusCode, 
                                                response.ReasonPhrase);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return "Status code is not equal to HttpStatusCode.OK";
                    }

                    Console.WriteLine();

                    return await response.Content.ReadAsStringAsync();
                    
                }
            }
        }

        static AuthenticationHeaderValue CreateBasicCredentials(string userName, 
                                                                string password)
        {
            string toEncode = userName + ":" + password;
            
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            byte[] toBase64 = encoding.GetBytes(toEncode);
            string parameter = Convert.ToBase64String(toBase64);

            return new AuthenticationHeaderValue("Basic", parameter);
        }
    }
}

```

###### Example of setting an auth header from jQuery

```

function createBasicAuthHeader(user, password) {
    var token = user + ":" + password;

    var hash = btoa(token);

    return "Basic " + hash;
    }
    
function submitAccountSearch()
{
    var userName = $("#apiUsername").val();

    if (userName.length == 0)
    {
        alert("Please enter user name");
        return;
    }

    var passWord = $("#apiPassword").val();

    if (passWord.length == 0) {
        alert("Please enter password");
        return;
    }

    var authHeader = createBasicAuthHeader(userName, passWord);
    var headers = {};
    headers.Authorization = authHeader;

    $("#loadingText").html("Loading...");
    $("#loadingText").show();
    $("#accountTableBody").html('');

    var urlToInvoke = 'api/accounts/' + $("#accountNameTextBox").val();

    $.ajax({
        url: urlToInvoke,
        method: 'get',
        headers: headers,
        success: function (list) {
        var accountTableBodyHtml = "";

        $.each(list, function (index, value) {
        // Each value is an account.
        accountTableBodyHtml += '<tr>';
        accountTableBodyHtml += '<td>' + value.accountNameField + '</td>';
        accountTableBodyHtml += '<td>' + value.accountNumberField + '</td>';
        accountTableBodyHtml += '<td>' + value.idField + '</td>';
        accountTableBodyHtml += '</tr>';
        });

        $("#accountTableBody").html(accountTableBodyHtml);
        $("#loadingText").hide();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var error = JSON.parse(XMLHttpRequest.responseText).Message;
            $("#loadingText").html(error);
            $("#loadingText").show();

            console.log(textStatus + " " + errorThrown);
        }
    });

}
        
```

## Testing the Web API
This Web API is documented with [Swagger.](http://autotaskwebapi.us-west-2.elasticbeanstalk.com/swagger/) It lists all APIs with details of operations, requests, and response types.

## Example: Querying Tickets by account id and status

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

HTTP GET to `{base url}/api/tickets/account/{}/status/{}`.
For example,` {base url}/api/tickets/account/12345678/status/8` returns all tickets with account id 12345678 and status as 8 (In Progress).

## Example: Posting a ticket note for a ticket using Generic endpoint

Request body

```

{"EntityType":"TicketNote","EntityObj":{"TicketId":"123", 
"CreatorResourceId":"123", "Title":"string", 
"Description":"string","NoteType":3, "Publish":2}}

```

-`CreatorResourceId` is creator resource id which is resource id of the note creator.

-`TicketId` is the id of the ticket to which new note is being added.

-`Title` is note title.

-`Description` is note description.

-`NoteType` is note type. e.g for a user note it is 3. Please refer to [AT Web Services](https://www.autotask.net/help/Content/AdminSetup/2ExtensionsIntegrations/APIs/WebServicesAPI.htm).

-`Publish` is 1 to publish to all users or 2 to publish to internal users.

```

POST /api/generics HTTP/1.1
Host: host-base-url
Authorization: Basic xxxxxxxxxxxxx
Content-Type: application/json

{"EntityType":"TicketNote","EntityObj":{"TicketId":"123", 
"CreatorResourceId":"123", "Title":"string", 
"Description":"string","NoteType":3, "Publish":2}}

```

Updating a ticket note is similar. TicketNote `id` is the only additional parameter. Instead of `POST`, use `PUT`.

```

PUT /api/generics HTTP/1.1
Host: {api base url}
Authorization: Basic xxxxxx
Content-Type: application/json

{"EntityType":"TicketNote","EntityObj":{"Id":"123","TicketId":"123", 
"CreatorResourceId":"123", "Title":"test note 3", 
"Description":"test description","NoteType":3, "Publish":2}}

```

## Example: Get Attachments by parent id (such as a ticket id) and attach date
When you query an attachment, you first query an `AttachmentInfo`. Using id of a certain attachment info object, you can query the actual attachment.
This is tricky but that is how it is structured in Autotask. 

So first, you would get attachment info.

###### URL

`{base url}/api/attachments/parent/123/attachdate/2016-12-22`

This will return a list of `AttachmentInfo` objects.

Then for each `AttachmentInfo` object, you would get its id using `AttachmentInfo.id`. From this id, you would get the actual attachment as follows.

###### URL

`{base url}/api/attachments/123`

This will return attachment byte array content which can be consumed at client side.

## About passing dates to the API methods

You might want to get entities by their last activity date or last modified date. When you send a date in such a case, the API methods return entities with last activity date or last modified date which is after the date argument. 

For example, if you send a date argument such as 2016-12-22 to an API endpoint, it returns tickets which have last activity date that is after 22nd Dec 2016. While sending date parameters to the operations of this Web API, please send them in 'yyyy-mm-dd' format.

e.g. `{base url}/api/tickets/lastactivitydate/2016-12-22`

# Error handling
Error handling is quite extensive in this API. I have used error messages from AT SOAP API which are quite user-friendly. For example, while creating a ticket, you must pass both assigned resource id and assigned resource role id together or else there will be an error which the message properly indicates. Please check the error message in the http response in case of an error. If there is no error, the response contains JSON object containing all data you requested. Sometimes you might get an empty list of entities which is alright if the parameters do not match any of the entities in the AT database. If there is no error, your query is deemed to have worked.

# Tools
You may want to use Postman (Chrome extension) to test the API. Any other RESTful API testing tool can also be used, even Fiddler can be used. I have used Postman and found it suitable for my purposes. This Web API uses Swagger spec to document the API endpoints. This makes it easy to try the API and create client-side code to consume them in your dashboards, intranet web apps and so forth.

# Current Status
At present `Account`, `Ticket`, `Contact`, `Contract`, `Resource`, `TicketNote`, `Attachment` and `Picklist`, `Task` entities are supported with their own controllers. All other entities are supported using a Generic controller which allows getting any entity by its name, a field name and field value.

# Roadmap

At present, everything in basic mode of API usage is supported. In the near future, I have planned to support more business rules in Web API rather than facing errors thrown by Autotask SOAP API and propagating them back to Web API.

# More Advanced Examples

## jQuery

###### Post a ticket attachment

Attachment file is taken from input control of file type. The `url` is relative in the following example. However, since the Web API enables CORS from server side, you may deploy the Web API anywhere (e.g. your on-premise web server, cloud hosting providers such as AWS, Azure etc.) and use that URL.

```
function createNewAttachment()
{
    console.log('Posting the attachment...');
    var formData = new FormData();
    var attachedFile = $('#chosenFile')[0];
    
    formData.append("attachedFile", attachedFile.files[0]);
    
    formData.append("ParentID", 123); // attachment parent id.
    formData.append('Title', attachedFile.files[0].name);// file title
    formData.append('FullPath', attachedFile.files[0].name); // file full path
    formData.append('Publish', '2'); // Publish to internal users.
    formData.append('ParentType', '4'); // 4 indicates a ticket parent.
    
    $.ajax({
        url: 'api/attachments',
        type: 'POST',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        complete: function (data) {
            console.log(data.statusCode);
            $("#attachmentCreationResult").html("Attachement posted successfully!!!");
        },
        error: function (response) {
            console.log(response.responseText);
        }
    });
}

```

## Postman

###### Post a ticket attachment

```

POST /api/attachments HTTP/1.1
Host: base-api-url
Authorization: Basic xxxxxxxxxx

Content-Type: multipart/form-data;

Content-Disposition: form-data; name="file1"; filename=""
Content-Type: 

Content-Disposition: form-data; name="ParentID"

123

Content-Disposition: form-data; name="Title"

file1.txt

Content-Disposition: form-data; name="FullPath"

file1.txt

Content-Disposition: form-data; name="Publish"

2

Content-Disposition: form-data; name="ParentType"

4

```

# Complex Queries

Pages 345-347 of [PDF Guide](https://www.autotask.net/help/Content/LinkedDOCUMENTS/WSAPI/T_WebServicesAPIv1_5.pdf) shows complex queries. To realize this type of complex query using this API, here are a few examples. 

The complex query is realized using POST method. However, it returns a list of entities matching the given condition(s).
Model of the POST body is the following.

```

ComplexQuery {
entityName (string, optional),
conditions (Array[Condition], optional)
}
Condition {
conditionType (integer): 
Condition type: 
1 = Field // Array of nested Conditions is null. 
	  // Only a field is present. 
2 = SimpleCondition // Array of nested conditions is null. 
// Array of fields is not null and 
// contains the list of multiple fields used by the condition. 
3= NestedConditions // nested conditions. 
// Array of fields is null.
 = ['1', '2', '3'],
operatorVal (integer): 
Operator. OR, AND
= ['1', '2'],
fields (Array[SimpleField], optional),
childConditions (Array[Condition], optional)
}
SimpleField {
fieldName (string, optional),
op (string), // field operator
valueToUse (string, optional)
}

```

Field operator is one of the following.

```

Equals, NotEqual, GreaterThan, LessThan, GreaterThanorEquals, LessThanOrEquals, 
BeginsWith,EndsWith, Contains, IsNotNull, IsNull, IsThisDay, Like, NotLike, 
SoundsLike 

```

###### Multiple Fields Query

```

<queryxml>
<entity>contact</entity>
<query>
<condition>
<field>firstname
<expression op="equals">Joe</expression>
</field>
</condition>
<condition>
<field>lastname
<expression op="equals">Smith</expression>
</field>
</condition>
</query>
</queryxml>

```

```

POST /api/complexquery HTTP/1.1
Host: {{base url}}
Authorization: Basic xxxx
Content-Type: application/json

{
"EntityName":"Contact", 
"Conditions":
[
{
"ConditionType":2, 
"ChildConditions": [],
"OperatorVal":"AND", 
"Fields":[{"FieldName":"firstname", "op":"Equals", "ValueToUse":"Joe"}]
},
{
"ConditionType":2, 
"ChildConditions": [],
"OperatorVal":"AND", 
"Fields":[{"FieldName":"lastname", "op":"Equals", "ValueToUse":"Smith"}]
}
]
}

```

###### Multiple Fields combined with OR

```

<queryxml>
<entity>contact</entity>
<query>
<condition>
<field>firstname
<expression op="equals">Joe</expression>
</field>
</condition>
<condition operator="OR">
<field>lastname
<expression op="equals">Brown</expression>
</field>
</condition>
</query>
</queryxml>

```

```

POST /api/complexquery HTTP/1.1
Host: {{base url}}
Authorization: Basic xxxx
Content-Type: application/json

{
"EntityName":"Contact", 
"Conditions":
[
{
"ConditionType":2, 
"ChildConditions": [],
"OperatorVal":"AND", 
"Fields":[{"FieldName":"firstname", "op":"Equals", "ValueToUse":"Joe"}]
},
{
"ConditionType":2, 
"ChildConditions": [],
"OperatorVal":"OR", 
"Fields":[{"FieldName":"lastname", "op":"Equals", "ValueToUse":"Brown"}]}
]
}

```

###### Nested Conditions

```

<queryxml>
<entity>contact</entity>
<query>
<field>firstname
<expression op="equals">Joe</expression>
</field>
<condition operator="OR">
<condition>
<field>firstname
<expression op="equals">Larry</expression>
</field>
<field>lastname
<expression op="equals">Brown</expression>
</field>
</condition>
<condition operator="OR">
<field>firstname
<expression op="equals">Mary</expression>
</field>
<field>lastname
<expression op="equals">Smith</expression>
</field>
</condition>
</condition>
</query>
</queryxml>

```

```

POST /api/complexquery HTTP/1.1
Host: {{base url}}
Authorization: Basic xxxx
Content-Type: application/json

{
"entityName": "contact",
"conditions": [
{
"conditionType": 1,
"operatorVal": "AND",
"fields": [
{
"fieldName": "firstname",
"op": "Equals",
"valueToUse": "Joe"
}
],
"childConditions": null
},
{
"conditionType": 3,
"operatorVal": "OR",
"fields": null,
"childConditions": [
{
"conditionType": 2,
"operatorVal": "AND",
"fields": [
{
"fieldName": "firstname",
"op": "Equals",
"valueToUse": "Larry"
},
{
"fieldName": "lastname",
"op": "Equals",
"valueToUse": "Brown"
}
],
"childConditions": null
},
{
"conditionType": 2,
"operatorVal": "OR",
"fields": [
{
"fieldName": "firstname",
"op": "Equals",
"valueToUse": "Mary"
},
{
"fieldName": "lastname",
"op": "Equals",
"valueToUse": "Smith"
}
],
"childConditions": null
}
]
}
]
}

```

# Reference

Please always refer to [AT Web Services](https://www.autotask.net/help/Content/AdminSetup/2ExtensionsIntegrations/APIs/WebServicesAPI.htm). This page has links to download a PDF guide which explains all business rules of querying an entity, creating an entity and udpating an entity. Whenever you receive errors from Web API, they are meaningful errors thrown by Autotask SOAP API and explain what went wrong. In these cases of errors, it makes sense to refer to the PDF guide.
