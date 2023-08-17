using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaSpriteRendering.Models
{
	public class GridElement
	{
		public int m_GridID;
		public int m_GridLayer;
		public bool m_Visible;

		public double m_PositionX;
		public double m_PositionY;

		public int m_ID;
		public Image m_Image;

		public GridElement()
		{
			m_Visible = true;
		}
	}
	public class GridLayer
	{
		public string m_Name;
		public bool m_Visible;
		public List<GridElement> m_GridElements = new List<GridElement>();

		public GridLayer(string name)
		{
			m_Name = name;
			m_Visible = true;
		}
	}
	public class TileGrid
	{
		private Canvas m_GridCanvas;
		///Grid width and height
		public int m_GridWidth;
		public int m_GridHeight;
		public void GenerateGrid(Canvas GridCanvas, int width, int height)
		{
			m_GridCanvas = GridCanvas;
			m_GridWidth = width;
			m_GridHeight = height;

			//When needed resetting grid
			if (m_GridCanvas.Children.Count > 0)
			{
				m_GridCanvas.Children.Clear();
			}

			double widthHeight = m_GridCanvas.Width / m_GridWidth;

			//Loop indexes
			int xStep = -1;
			int yStep = 0;

			int size = m_GridWidth * m_GridHeight;

			//Bitmap image for base layer
			Bitmap noTileBitmap = new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaSpriteRendering/Assets/grass_tile.png")));
			//CroppedBitmap croppedBitmap = new CroppedBitmap()
			for (int i = 0; i < size; i++)
			{
				//Creating and defining grid image
				Image img = new Image();
				img.Source = noTileBitmap;
				img.Stretch = Stretch.Fill;
				img.Width = widthHeight;
				img.Height = widthHeight;
				//img.MouseEnter += new MouseEventHandler(m_PaintManager.GridElementMouseOver);
				//img.MouseDown += new MouseButtonEventHandler(m_PaintManager.GridElementMouseOver);

				xStep++;

				//Handling of correct spacing of loop indexes
				if (xStep > m_GridWidth - 1)
				{
					xStep = 0;
					yStep++;
				}

				//Setting location of image
				Canvas.SetLeft(img, 0 + (widthHeight * xStep));
				Canvas.SetTop(img, 0 + (widthHeight * yStep));

				m_GridCanvas.Children.Add(img);
			}

		}
	}
}
