# CrossPlatformLibrary.Callouts

### Download and Install CrossPlatformLibrary.Callouts
This library is available on NuGet: https://www.nuget.org/packages/CrossPlatformLibrary.Callouts/
Use the following command to install CrossPlatformLibrary.Callouts using NuGet package manager console:

    PM> Install-Package CrossPlatformLibrary.Callouts

You can use this library in any .Net project which is compatible to PCL (e.g. Xamarin Android, iOS, Windows Phone, Windows Store, Universal Apps, etc.)

### API Usage
CrossPlatformLibrary.Callouts provides a simple and platform-agnostic interface, ```IWebBrowser``` which can be used to open an URL in the web browser on the target platform:

```
IWebBrowser webBrowser = new WebBrowser(); // Get instance from IoC container via dependency injection, if possible
webBrowser.OpenUrl("http://www.thomasgalliker.ch");
```

### License
This library is Copyright &copy; 2015 [Thomas Galliker](https://ch.linkedin.com/in/thomasgalliker). Free for non-commercial use. For commercial use please contact the author.

