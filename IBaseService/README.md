# IBaseService
MVC Controller interface to add new functionality for services classes including AD Group validation, AD Name retrieval, sending emails, and more.

## Requirements
- Visual Studio or Visual Studio Code
- For any Active Directory functions, you must have AD Authentication enabled. 

## Usage
These files are provided as coding snippets. It is up to you to modify and implement them as needed. Generally, you can copy the folders into your project and begin using them. Feel free to remove unneeded functions.

This interface currently provides these different benefits:

### Validate User is in AD Group
The following function definition in the `IBaseService.cs` interface class will add AD Group validation: 
```c#
internal bool IsInSecurityGroup(string group)
```

This function will check if the current user is a member of the given AD Group and return true if so. `group` should be formatted as @"DOMAIN\GROUP". This requires AD Authentication to be enabled.

### Get User Name & Email
The following function definitions will return the current user's full name and email from AD. 

```c#
internal string GetUser(){}
internal string GetUserEmail(){}
```

Again, this requires AD Authentication to be enabled.

### Send Emails
The following function definition allow you to send an email through your service.

```c#
internal bool SendEmail(string[] toAddresses, string fromAddress, string[] ccAddresses, string subject, string body, IEnumerable<HttpPostedFileBase> attachments)
```

This function operators under the precondition that all array parameters are non-empty arrays. You must configure the `server` and `port` options in this function to match your implementation. Anonymous sending must be enabled on the mail server. I recommend whitelisting your web server's IP address for anonymous sending on the mail server and not enabling it globally.


## Liability
This repository is provided as is. I make no representations or warranties of any kind concerning the legality, safety, suitability, lack of viruses, inaccuracies, typographical errors, or other harmful components of this repository. There are inherent dangers in the use of any software, and you are solely responsible for determining whether this software is compatible with your equipment and other software installed on your equipment. You are also solely responsible for the protection of your equipment and backup of your data, and I will not be liable for any damages you may suffer in connection with using, modifying, or distributing this software. This software was written for educational purposes only.
