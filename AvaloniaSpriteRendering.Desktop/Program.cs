using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Logging;
using Serilog;

namespace AvaloniaSpriteRendering.Desktop;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
	public static void Main(string[] args)
	{
		try
		{
			// prepare and run your App here
			BuildAvaloniaApp().With(new Win32PlatformOptions
			{
				RenderingMode = new List<Win32RenderingMode> { Win32RenderingMode.Software }
			}).StartWithClassicDesktopLifetime(args);
		}
		catch (Exception e)
		{
			// here we can work with the exception, for example add it to our log file
			Log.Fatal(e, "Unhandled Global Exeption");
		}
		finally
		{
			// This block is optional. 
			// Use the finally-block if you need to clean things up or similar
			Log.CloseAndFlush();
		}
	}

	// Avalonia configuration, don't remove; also used by visual designer.
	public static AppBuilder BuildAvaloniaApp()
	{
		Log.Logger = new LoggerConfiguration()
		   .MinimumLevel.Information()
		   .WriteTo.Console()
		   .WriteTo.File("serilog.txt",
			   rollingInterval: RollingInterval.Month,
			   rollOnFileSizeLimit: true)
		   .CreateLogger();

		return AppBuilder.Configure<App>()
				.UsePlatformDetect()
				.WithInterFont()
				.LogToTrace(LogEventLevel.Information, LogArea.Control);
	}

}
