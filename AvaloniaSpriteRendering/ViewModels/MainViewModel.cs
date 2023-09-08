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

	public void loadSingleSprites()
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
	public Sprite loadSpriteAnimation(Rectangle rect, int scale = 1)
	{
		try
		{
			Bitmap spriteSheet = new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaSpriteRendering/Assets/blue_unit.png")));
			Bitmap scaledSheet = spriteSheet.CreateScaledBitmap(new PixelSize(spriteSheet.PixelSize.Width * scale, spriteSheet.PixelSize.Height * scale), BitmapInterpolationMode.None);

			Sprite unitSprite = new Sprite(scaledSheet, 4, 32 * scale, 32 * scale);
			rect.Fill = unitSprite.Brush;
			return unitSprite;
		}
		catch (Exception e)
		{
			Log.Fatal(e, "Exception caught in MainViewModel.loadSpriteSheet");
		}
		return null;
	}
	public Sprite loadSpriteSheet(Rectangle rect, int frameSize, int scale = 1)
	{
		try
		{
			Bitmap spriteSheet = new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaSpriteRendering/Assets/blue_unit.png")));
			Sprite unitSprite = new Sprite(spriteSheet, 4, frameSize * scale, frameSize * scale);

			Bitmap scaledSheet = spriteSheet.CreateScaledBitmap(new PixelSize(spriteSheet.PixelSize.Width * scale, spriteSheet.PixelSize.Height * scale), BitmapInterpolationMode.None);

			//rect.Fill = unitSprite.Brush;
			//unitSprite.StartAnimation();
			return unitSprite;
		}
		catch (Exception e)
		{
			Log.Fatal(e, "Exception caught in MainViewModel.loadSpriteSheet");
		}
		return null;
	}
}
