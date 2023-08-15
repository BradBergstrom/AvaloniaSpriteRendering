using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;

using AvaloniaSpriteRendering.ViewModels;
using AvaloniaSpriteRendering.Views;
using Serilog;

namespace AvaloniaSpriteRendering;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
		Log.Information("Starting up Avalonia application!");	
		Window mainWindow = new MainWindow();

		// Line below is needed to remove Avalonia data validation.
		// Without this line you will get duplicate validations from both Avalonia and CT
		BindingPlugins.DataValidators.RemoveAt(0);

		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			desktop.MainWindow = mainWindow;
			//desktop.MainWindow = new MainWindow
			//{
			//    DataContext = new MainViewModel()
			//};
		}
		else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
		{
			singleViewPlatform.MainView = mainWindow;
			//singleViewPlatform.MainView = new MainView
			//{
			//    DataContext = new MainViewModel()
			//};
		}

		base.OnFrameworkInitializationCompleted();
    }
}
