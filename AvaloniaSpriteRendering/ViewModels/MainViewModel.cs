using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using Serilog;
using AvaloniaSpriteRendering.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AvaloniaSpriteRendering.ViewModels;

public partial class MainViewModel : ObservableObject
{
	[ObservableProperty]
	public string spriteStatus = "Sprites are not loaded yet";

	[ObservableProperty]
	public Bitmap itemSprite16x16;

	[ObservableProperty]
	public Bitmap itemSprite32x32;

	[ObservableProperty]
	public Bitmap itemSprite64x64;

	public void loadSprites()
	{
		try
		{
			ItemSprite16x16 = new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaSpriteRendering/Assets/sword_small.png")));
			ItemSprite32x32 = ItemSprite16x16.CreateScaledBitmap(new PixelSize(32, 32), BitmapInterpolationMode.None);
			ItemSprite64x64 = ItemSprite16x16.CreateScaledBitmap(new PixelSize(64, 64), BitmapInterpolationMode.None);

			SpriteStatus = "Sprites should be loaded now";
		}
		catch (Exception e)
		{
			// here we can work with the exception, for example add it to our log file
			Log.Fatal(e, "Exception caught in MainViewModel.loadSprites");
		}

	}
	public void loadSpriteSheet(Rectangle rect, int frameSize)
	{
		try
		{
			Bitmap spriteSheet = new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaSpriteRendering/Assets/blue_unit.png")));
			//Code to fill rectangle by hand:
			//ImageBrush brush = new ImageBrush(spriteSheet);
			//brush.SourceRect = new RelativeRect(0, 0, 32, 32, RelativeUnit.Absolute);
			//brush.DestinationRect = new RelativeRect(0, 0, 32, 32, RelativeUnit.Relative);
			//rect.Fill = brush;

			Sprite unitSprite = new Sprite(spriteSheet, 4, frameSize, frameSize);
			rect.Fill = unitSprite.Brush;
			unitSprite.StartAnimation();
		}
		catch (Exception e)
		{
			Log.Fatal(e, "Exception caught in MainViewModel.loadSpriteSheet");
		}
	}
	public void loadSpriteSheetScaled(Rectangle rect, int frameSize, int scale)
	{
		try
		{
			Bitmap spriteSheet = new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaSpriteRendering/Assets/blue_unit.png")));
			Bitmap scaledSheet = spriteSheet.CreateScaledBitmap(new PixelSize(spriteSheet.PixelSize.Width * scale, spriteSheet.PixelSize.Height * scale), BitmapInterpolationMode.None);

			Sprite unitSprite = new Sprite(scaledSheet, 4, frameSize * scale, frameSize * scale);
			rect.Fill = unitSprite.Brush;
			unitSprite.StartAnimation();
		}
		catch (Exception e)
		{
			Log.Fatal(e, "Exception caught in MainViewModel.loadSpriteSheet");
		}
	}
	public void loadSpriteButtonClicked()
	{
		Log.Information("loadSpriteButtonClicked");
		loadSprites();
	}
}
