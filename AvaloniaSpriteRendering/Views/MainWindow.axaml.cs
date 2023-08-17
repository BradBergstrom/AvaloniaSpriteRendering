using Avalonia.Controls;
using AvaloniaSpriteRendering.Models;
using AvaloniaSpriteRendering.ViewModels;
using System.Collections.Generic;

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
		viewModel.loadSpriteSheet(spriteRectangle128, 32, 4);

		for(int i = 0; i < 48; i++)
		{
			StackPanel row = new StackPanel();
			row.Orientation = Avalonia.Layout.Orientation.Horizontal;
			bunchOfDudes.Children.Add(row);
			makeTenSprites(viewModel, row);
		}
	}
	private void makeTenSprites(MainViewModel viewModel, StackPanel stack)
	{
		Sprite sprite0 = viewModel.loadSpriteSheet(spriteRectangle, 32, 1);
		Sprite sprite1 = viewModel.loadSpriteSheet(spriteRectangle, 32, 1);
		Sprite sprite2 = viewModel.loadSpriteSheet(spriteRectangle, 32, 1);
		Sprite sprite3 = viewModel.loadSpriteSheet(spriteRectangle, 32, 1);
		Sprite sprite4 = viewModel.loadSpriteSheet(spriteRectangle, 32, 1);
		Sprite sprite5 = viewModel.loadSpriteSheet(spriteRectangle, 32, 1);
		Sprite sprite6 = viewModel.loadSpriteSheet(spriteRectangle, 32, 1);
		Sprite sprite7 = viewModel.loadSpriteSheet(spriteRectangle, 32, 1);
		Sprite sprite8 = viewModel.loadSpriteSheet(spriteRectangle, 32, 1);

		List<Sprite> sprites = new List<Sprite>() { sprite0, sprite1, sprite2, sprite3, sprite4, sprite5, sprite6, sprite7, sprite8};
		for(int l =  0; l < sprites.Count; l++)
		{
			for (int i = 0; i < 4; i++)
			{
				Border copy = new Border();
				copy.Width = 32;
				copy.Height = 32;

				Avalonia.Controls.Shapes.Rectangle rectangle = new Avalonia.Controls.Shapes.Rectangle();
				rectangle.Fill = sprites[l].Brush;
				copy.Child = rectangle;
				stack.Children.Add(copy);
			}
		}
	}
}
