using Avalonia;
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
		makeMapCanvas(gridCanvas, gridBorder);

		return;
		for (int i = 0; i < 48; i++)
		{
			StackPanel row = new StackPanel();
			row.Orientation = Avalonia.Layout.Orientation.Horizontal;
			bunchOfDudes.Children.Add(row);
			makeTenSprites(viewModel, row);
		}
	}
	private void makeMapCanvas(Canvas canvas, Border border)
	{
		int rows = 32;
		int cols = 48;
		int widthHeight = 32;
		int xStep = 0;
		int yStep = 0;
		Bitmap tileSheetSource = new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaSpriteRendering/Assets/Overworld.png")));
		//Bitmap tileSheetSourceZoom = tileSheetSource.CreateScaledBitmap(new PixelSize(64, 64), BitmapInterpolationMode.None);
		SpriteSheet tileSheet = new SpriteSheet(tileSheetSource, 32, 32);

		for (int i = 0; i < cols; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				Avalonia.Controls.Image copy = new Avalonia.Controls.Image();
				copy.Width = 32;
				copy.Height = 32;
				copy.Source = tileSheet.GetTile(34);
								
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
		//result= 20mb

	}
	private void makeMapCanvasOld(MainViewModel viewModel, Canvas canvas, Border border)
	{
		//int rows = 32;
		//int cols = 48;
		//int widthHeight = 64;
		//int xStep = 0;
		//int yStep = 0;

		//for (int i = 0; i < cols; i++)
		//{
		//	for (int j = 0; j < rows; j++)
		//	{
		//		Avalonia.Controls.Shapes.Rectangle rectangle = new Avalonia.Controls.Shapes.Rectangle();
		//		Sprite sprite = viewModel.loadSpriteSheet(spriteRectangle, 32, widthHeight / 32);
		//		rectangle.Fill = sprite.Brush;
		//		rectangle.Width = widthHeight;
		//		rectangle.Height = widthHeight;
		//		//rectangle.MouseEnter += new MouseEventHandler(m_PaintManager.GridElementMouseOver);
		//		//rectangle.MouseDown += new MouseButtonEventHandler(m_PaintManager.GridElementMouseOver);

		//		Canvas.SetLeft(rectangle, 0 + (widthHeight * xStep));
		//		Canvas.SetTop(rectangle, 0 + (widthHeight * yStep));
		//		canvas.Children.Add(rectangle);
		//		xStep++;
		//	}
		//	xStep = 0;
		//	yStep++;
		//}
		//canvas.Width = rows * widthHeight;
		//canvas.Height = cols * widthHeight;
		//border.Width = rows * widthHeight;
		//border.Height = cols * widthHeight;
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
