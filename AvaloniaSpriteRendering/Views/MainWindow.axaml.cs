using Avalonia.Controls;
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
		viewModel.loadSprites();
		viewModel.loadSpriteSheet(spriteRectangle, 32);
		viewModel.loadSpriteSheet(spriteRectangle64, 32, 2);
		//viewModel.loadSpriteSheet(spriteRectangle128, 32, 4);


		StackPanel rowTest = new StackPanel();
		rowTest.Orientation = Avalonia.Layout.Orientation.Horizontal;
		bunchOfDudes.Children.Add(rowTest);
		loadSpriteSheet(rowTest);

		return;
		for (int i = 0; i < 48; i++)
		{
			StackPanel row = new StackPanel();
			row.Orientation = Avalonia.Layout.Orientation.Horizontal;
			bunchOfDudes.Children.Add(row);
			makeTenSprites(viewModel, row);
		}
	}
	private void loadSpriteSheet(StackPanel stack)
	{
		Bitmap tileSheetSource = new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaSpriteRendering/Assets/Overworld.png")));
		SpriteSheet tileSheet = new SpriteSheet(tileSheetSource, 32, 32);
		for (int i = 0; i < 32; i++)
		{
			Avalonia.Controls.Image copy = new Avalonia.Controls.Image();
			copy.Width = 32;
			copy.Height = 32;
			copy.Source = tileSheet.GetTile(34);
			stack.Children.Add(copy);
		}
		//result= 20mb

	}
	private void makeTenSprites(MainViewModel viewModel, StackPanel stack)
	{
		for (int i = 0; i < 32; i++)
		{
			Sprite sprite = viewModel.loadSpriteSheet(spriteRectangle, 32, 1);
			Border copy = new Border();
			copy.Width = 32;
			copy.Height = 32;
			copy.Child = sprite.Brush;
			stack.Children.Add(copy);
		}
	}
}
