using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using AvaloniaSpriteRendering.Models;
using AvaloniaSpriteRendering.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace AvaloniaSpriteRendering.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
		MainViewModel viewModel = new MainViewModel();
		DataContext = viewModel;
		
		//load item sprites
		viewModel.loadSingleSprites();

		//load unit sprites
		viewModel.loadSpriteAnimation(spriteRectangle32);
		viewModel.loadSpriteAnimation(spriteRectangle64, 2);
		viewModel.loadSpriteAnimation(spriteRectangle128, 4);

		//load the sprite sheet
		populateSpriteCanvas_Efficent(gridCanvas, gridBorder);
		return;
	}
	//Populate the grid with random tiles taken from the sprite sheet
	private void populateSpriteCanvas_Efficent(Canvas canvas, Border border)
	{
		int rows = 10;
		int cols = 10;
		int widthHeight = 32;
		int xStep = 0;
		int yStep = 0;
		Bitmap tileSheetSource = new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaSpriteRendering/Assets/Overworld.png")));
		//Bitmap tileSheetSourceZoom = tileSheetSource.CreateScaledBitmap(new PixelSize(64, 64), BitmapInterpolationMode.None);
		SpriteSheet tileSheet = new SpriteSheet(tileSheetSource, 32, 32);
		Random rnd = new Random();

		for (int i = 0; i < cols; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				Avalonia.Controls.Image copy = new Avalonia.Controls.Image();
				//SpriteSheet tileSheet = new SpriteSheet(tileSheetSource, 32, 32); 

				copy.Width = 32;
				copy.Height = 32;
				copy.Source = tileSheet.GetTile(rnd.Next(0, tileSheet.GetNumberTiles()));
								
				Canvas.SetLeft(copy, 0 + (widthHeight * xStep));
				Canvas.SetTop(copy, 0 + (widthHeight * yStep));
				canvas.Children.Add(copy);
				xStep++;
			}
			xStep = 0;
			yStep++;
		}
		canvas.Width = rows * widthHeight;
		canvas.Height = cols * widthHeight;
		border.Width = rows * widthHeight;
		border.Height = cols * widthHeight;
	}
	private void populateSpriteCanvas_InEfficent(Canvas canvas, Border border)
	{
		int rows = 10;
		int cols = 10;
		int widthHeight = 32;
		int xStep = 0;
		int yStep = 0;
		Bitmap tileSheetSource = new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaSpriteRendering/Assets/Overworld.png")));
		//Bitmap tileSheetSourceZoom = tileSheetSource.CreateScaledBitmap(new PixelSize(64, 64), BitmapInterpolationMode.None);
		Random rnd = new Random();

		for (int i = 0; i < cols; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				Avalonia.Controls.Image copy = new Avalonia.Controls.Image();
				SpriteSheet tileSheet = new SpriteSheet(tileSheetSource, 32, 32); 

				copy.Width = 32;
				copy.Height = 32;
				copy.Source = tileSheet.GetTile(rnd.Next(0, tileSheet.GetNumberTiles()));

				Canvas.SetLeft(copy, 0 + (widthHeight * xStep));
				Canvas.SetTop(copy, 0 + (widthHeight * yStep));
				canvas.Children.Add(copy);
				xStep++;
			}
			xStep = 0;
			yStep++;
		}
		canvas.Width = rows * widthHeight;
		canvas.Height = cols * widthHeight;
		border.Width = rows * widthHeight;
		border.Height = cols * widthHeight;
	}
}
