using Avalonia.Controls;
using Avalonia.Media;
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

		makeSpriteCanvas(viewModel, gridCanvas, gridBorder);
		//makeTonsOfSprites(viewModel, bunchOfDudes);
		return;
		for (int i = 0; i < 48; i++)
		{
			StackPanel row = new StackPanel();
			row.Orientation = Avalonia.Layout.Orientation.Horizontal;
			bunchOfDudes.Children.Add(row);
			makeTenSprites(viewModel, row);
		}
	}
	private void makeSpriteCanvas(MainViewModel viewModel, Canvas canvas, Border border) 
	{
		int rows = 32;
		int cols = 48;
		int widthHeight = 32;
		int xStep = 0;
		int yStep = 0;
		Sprite sprite = viewModel.loadSpriteSheet(spriteRectangle, 32, widthHeight / 32); //62mb

		for (int i = 0; i < cols; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				Avalonia.Controls.Shapes.Rectangle rectangle = new Avalonia.Controls.Shapes.Rectangle();
				rectangle.Fill = sprite.Brush;
				rectangle.Width = widthHeight;
				rectangle.Height = widthHeight;
				//rectangle.MouseEnter += new MouseEventHandler(m_PaintManager.GridElementMouseOver);
				//rectangle.MouseDown += new MouseButtonEventHandler(m_PaintManager.GridElementMouseOver);

				Canvas.SetLeft(rectangle, 0 + (widthHeight * xStep));
				Canvas.SetTop(rectangle, 0 + (widthHeight * yStep));
				canvas.Children.Add(rectangle);
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
	private void makeTonsOfSprites(MainViewModel viewModel, StackPanel stack)
	{
		int rows = 32;
		int cols = 48;
		int widthHeight = 32;
		int xStep = 0;
		int yStep = 0;

		for (int i = 0; i < cols; i++)
		{
			StackPanel row = new StackPanel();
			row.Orientation = Avalonia.Layout.Orientation.Horizontal;
			stack.Children.Add(row);

			for (int j = 0; j < rows; j++)
			{
				Border copy = new Border();
				copy.Width = widthHeight;
				copy.Height = widthHeight;

				Avalonia.Controls.Shapes.Rectangle rectangle = new Avalonia.Controls.Shapes.Rectangle();
				Sprite sprite = viewModel.loadSpriteSheet(spriteRectangle, 32, 1);
				rectangle.Fill = sprite.Brush;
				rectangle.Width = 32;
				rectangle.Height = 32;
				copy.Child = rectangle;

				row.Children.Add(copy);
				xStep++;
			}
			xStep = 0;
			yStep++;
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
