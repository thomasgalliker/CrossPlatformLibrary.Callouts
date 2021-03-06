# CrossPlatformLibrary.Callouts

### Download and Install CrossPlatformLibrary.Callouts
This library is available on NuGet: https://www.nuget.org/packages/CrossPlatformLibrary.Callouts/
Use the following command to install CrossPlatformLibrary.Callouts using NuGet package manager console:

    PM> Install-Package CrossPlatformLibrary.Callouts

You can use this library in any .Net project which is compatible to PCL (e.g. Xamarin Android, iOS, Windows Phone, Windows Store, Universal Apps, etc.)

### API Usage
CrossPlatformLibrary.Callouts provides a simple and platform-agnostic interface, ```ICallout``` which can be used to show message boxes on the target platform.

#### Show message box with plain text
The most simple way of showing a message box is shown in the follwing example:
```csharp
ICallout callout = SimpleIoc.Default.GetInstance<ICallout>(); // Use dependency injection if possible
callout.Show("Simple Callout", "Message with some text.");
```

#### Show message box with custom content
If you want to show more sophisticated content in the message box, you can pass UI elements to the content parameter.
```csharp
ICallout callout = SimpleIoc.Default.GetInstance<ICallout>(); // Use dependency injection if possible

// Create custom message box content
var panel = new StackPanel();
var textblock = new TextBlock { Text = "This is a short message.", TextWrapping = TextWrapping.Wrap };
panel.Children.Add(textblock);

var checkBox = new CheckBox { Content = "Agree" };
checkBox.Checked += (o, args) => { /* Do something when checked */ };
panel.Children.Add(checkBox);

// Specify message box buttons
var buttonConfigs = new[] 
{ 
    new ButtonConfig("I Agree", () => { /* Do something when button is pressed */  }),
    new ButtonConfig("I Decline")
};

callout.Show("Content Callout", panel, buttonConfigs);
```
However, if you use `ICallout` in an MVVM scenario you may want to resolve and inject the visual content using a navigation service. (You don't want the ViewModel to know about visual elements).
```csharp
ICallout callout = SimpleIoc.Default.GetInstance<ICallout>(); // Use dependency injection if possible

// Resolve and inject custom message box content
var resolvedContent = this.NavigationService.Resolve(new Uri("/Views/CustomMessageBox"));

// Specify message box buttons
var buttonConfigs = new[] 
{ 
    new ButtonConfig("I Agree", () => { /* Do something when button is pressed */  }),
    new ButtonConfig("I Decline")
};

callout.Show("Content Callout", resolvedContent, buttonConfigs);
```

### License
This library is Copyright &copy; 2016 [Thomas Galliker](https://ch.linkedin.com/in/thomasgalliker). Free for non-commercial use. For commercial use please contact the author.

