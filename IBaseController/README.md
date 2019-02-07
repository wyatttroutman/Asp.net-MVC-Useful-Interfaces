# IBaseController
MVC Controller interface to add new functionality for handling errors and rendering views, partial views, and client-side modals.

## Requirements
- Visual Studio or Visual Studio Code
- Bootstrap V4 is needed for Client-Side Modal functionality.
- A Controller dedicated to Error Handling is needed for extended error handling functionality.

## Usage
These files are provided as coding snippets. It is up to you to modify and implement them as needed. Generally, you can copy the folders into your project and begin using them. Feel free to remove unneeded functions.

This interface currently provides these different benefits:

### Error Handling
The following function definition in the `IBaseController.cs` interface class will add extended error handling: 
`protected override void OnException(ExceptionContext filterContext)`

The implementation will override the default `OnException` function from `Controller` and allow you to redirect the user to another controller. In my implementation, I have a controller named `ErrorHandler` that will parse the information and respond based on the error type. 


### Rendering Views & Partial Views into an HTML String
The following function definitions will add the ability to render Views and Partial Views to a string:

```c#
internal string PartialViewToString(string partialViewName, object model = null) {}
protected string ViewToString(string viewName, object model = null) {}
protected string ViewToString(string viewName, string controllerName, string areaName, object model = null) {}
private string ViewToString(ControllerContext controllerContext, ViewEngineResult viewEngineResult, object model) {}
```

Sometimes, you need the ability to refresh a partial view on a webpage. By creating a Controller Action and using the `PartialViewToString` function, you can easily return a string and inject it into your web page. This is especially useful on JQuery AJAX heavy implementations.

### Rendering Client-Side Modals from Server
The following function definitions will add the ability to render a Modal popup directly from the server code. 

```c#
public void Error(string errorHeader, string errorMessage) {}
public void Error(string errorHeader, string errorMessage, ref Exception exception) {}
public void Info(string infoHeader, string infoMessage) {}
```

Calling one of these functions from your Controller Action will inject JavaScript into your web page on pageload and cause the Modal to render. This is useful for rendering user-friendly Modals that contain errors or general information. You must include the `_ErrorModal.cshtml` and `_InfoModal.cshtml` partial views on your web page or layout page, and the JavaScript injection string from `ExampleView.cshtml` directly on your web page.

Similarly, you can render these same Modals via JavaScript using the functions in `ModalFunctions.js`. To do this, include the JavaScript file in your web page and call the functions from your JavaScript code.

This implementation lets you have a fluid UI instead of having disjointed alerts and popups.

## Liability
This repository is provided as is. I make no representations or warranties of any kind concerning the legality, safety, suitability, lack of viruses, inaccuracies, typographical errors, or other harmful components of this repository. There are inherent dangers in the use of any software, and you are solely responsible for determining whether this software is compatible with your equipment and other software installed on your equipment. You are also solely responsible for the protection of your equipment and backup of your data, and I will not be liable for any damages you may suffer in connection with using, modifying, or distributing this software. This software was written for educational purposes only.