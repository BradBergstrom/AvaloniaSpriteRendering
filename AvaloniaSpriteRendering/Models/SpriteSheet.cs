using Avalonia.Media.Imaging;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using Avalonia;
using Serilog;
using Avalonia.Animation;
using Avalonia.Controls;
using System.Globalization;

namespace AvaloniaSpriteRendering.Models
{
	public class SpriteSheet
	{
		Bitmap _spriteSheet;
		List<CroppedBitmap> _spriteList;
		int _frameWidth;
		int _frameHeight;


		/// <summary>
		/// Create new sprite
		/// </summary>
		/// <param name="spriteSheet">BitmapImage of spritesheet</param>
		/// <param name="frames">Number of frames in the animation</param>
		/// <param name="frameWidth">Width of each frame in pixels</param>
		/// <param name="frameHeight">Height of each frame in pixels</param>
		public SpriteSheet(Bitmap spriteSheet, int frameWidth, int frameHeight)
		{
			this._spriteSheet = spriteSheet;
			this._frameWidth = frameWidth;
			this._frameHeight = frameHeight;

			this._spriteList = cutSheet();
		}

		public CroppedBitmap GetTile(int index)
		{
			return this._spriteList[index];
		}
		public int GetNumberTiles()
		{
			return _spriteList.Count;
		}

		/// <summary>
		/// Cut spritesheet into bitmap animation frames
		/// </summary>
		/// <returns>Returns a list of animation frames</returns>
		private List<CroppedBitmap> cutSheet()
		{
			int xOffset = 0;
			int yOffset = 0;
			PixelRect cropRect = new PixelRect(0, 0, this._frameWidth, this._frameHeight);
			List<CroppedBitmap> tiles = new List<CroppedBitmap>();

			Bitmap sheet = this._spriteSheet;
			if ((sheet.PixelSize.Height % this._frameHeight) == 0 && (sheet.PixelSize.Width % this._frameWidth) == 0)
			{
				int rows = (int)sheet.PixelSize.Height / (int)this._frameHeight;
				int columns = (int)sheet.PixelSize.Width / (int)this._frameWidth;
				for (int row = 0; row < rows; row++)
				{
					for (int col = 0; col < columns; col++)
					{
						int currentY = yOffset + row * this._frameHeight;
						int currentX = xOffset + col * this._frameWidth;
						cropRect = new PixelRect(currentX, currentY, this._frameWidth, this._frameHeight);

						tiles.Add(new CroppedBitmap(sheet, cropRect));
					}
				}
			}
			return tiles;
		}
	}
}
