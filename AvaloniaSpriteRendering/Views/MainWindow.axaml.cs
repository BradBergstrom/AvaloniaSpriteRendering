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
		viewModel.populateSpriteCanvas(gridCanvas, gridBorder);
		return;
	}
	
}
