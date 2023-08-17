using Avalonia.Controls;
using AvaloniaSpriteRendering.Models;
using AvaloniaSpriteRendering.ViewModels;
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

		for (int i = 0; i < 48; i++)
		{
			StackPanel row = new StackPanel();
			row.Orientation = Avalonia.Layout.Orientation.Horizontal;
			bunchOfDudes.Children.Add(row);
			makeTenSprites(viewModel, row);
		}
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
