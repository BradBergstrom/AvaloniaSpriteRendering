using Avalonia.Controls;
using AvaloniaSpriteRendering.Models;
using AvaloniaSpriteRendering.ViewModels;

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
		viewModel.loadSpriteSheetScaled(spriteRectangle64, 32, 2);
		viewModel.loadSpriteSheetScaled(spriteRectangle128, 32, 4);

		TileGrid tileGrid = new TileGrid();
		tileGrid.GenerateGrid(gridCanvas, 10, 10);
	}
}
