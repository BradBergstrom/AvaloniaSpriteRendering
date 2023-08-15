using Avalonia.Media.Imaging;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using Avalonia;
using Serilog;
using Avalonia.Animation;
using Avalonia.Controls;

namespace AvaloniaSpriteRendering.Models
{
	public class Sprite
	{
		Bitmap _spriteSheet;
		public ImageBrush _currentFrame;
		List<RelativeRect> _spriteFrames;
		int _numFrames;
		int _frameWidth;
		int _frameHeight;

		DispatcherTimer _animTimer;
		int _frameDuration = 200;
		int _animIndex = 0;


		/// <summary>
		/// Create new sprite
		/// </summary>
		/// <param name="spriteSheet">BitmapImage of spritesheet</param>
		/// <param name="frames">Number of frames in the animation</param>
		/// <param name="frameWidth">Width of each frame in pixels</param>
		/// <param name="frameHeight">Height of each frame in pixels</param>
		public Sprite(Bitmap spriteSheet, int frames, int frameWidth, int frameHeight)
		{
			this._spriteSheet = spriteSheet;
			this._numFrames = frames;
			this._frameWidth = frameWidth;
			this._frameHeight = frameHeight;

			this._spriteFrames = cutSheet();

			this._currentFrame = new ImageBrush(spriteSheet);
			this._currentFrame.SourceRect = this._spriteFrames[0];

			this._animTimer = new DispatcherTimer();
			this._animTimer.Tick += (sender, e) => nextFrame();
		}

		public ImageBrush Brush
		{
			get { return this._currentFrame; }
		}

		public DispatcherTimer Timer
		{
			get { return this._animTimer; }
		}

		public List<RelativeRect> Frames
		{
			get { return this._spriteFrames; }
		}

		public int FrameDuration
		{
			get { return this._frameDuration; }
			set { this._frameDuration = value; }
		}

		/// <summary>
		/// Manually set the frame index. This will not stop the running animation nor change the animation index.
		/// </summary>
		/// <param name="index">Frame index</param>
		public void SetFrame(int index)
		{
			this._currentFrame.SourceRect = this._spriteFrames[index];
		}

		/// <summary>
		/// Start sprite animation.
		/// </summary>
		public void StartAnimation()
		{
			this._animTimer.Interval = TimeSpan.FromMilliseconds(this._frameDuration);
			this._animTimer.Start();
		}

		/// <summary>
		/// Pause running sprite animation
		/// </summary>
		public void PauseAnimation()
		{
			if (this._animTimer != null)
			{
				this._animTimer.Stop();
			}
		}

		/// <summary>
		/// Increase animation index by one or reset to zero if at end of list
		/// </summary>
		private void nextFrame()
		{
			this._animIndex++;
			this._animIndex = this._animIndex % this._spriteFrames.Count;
			this._currentFrame.SourceRect = this._spriteFrames[this._animIndex];
		}

		/// <summary>
		/// Cut spritesheet into bitmap animation frames
		/// </summary>
		/// <returns>Returns a list of animation frames</returns>
		private List<RelativeRect> cutSheet()
		{
			int xOffset = 0;
			int yOffset = 0;
			List<RelativeRect> frames = new List<RelativeRect>();

			Bitmap sheet = this._spriteSheet;
			//RelativeRect rect = new RelativeRect(0, 0, this._frameWidth, this._frameHeight, RelativeUnit.Relative);
			if ((sheet.PixelSize.Height % this._frameHeight) == 0 && (sheet.PixelSize.Width % this._frameWidth) == 0)
			{
				int rows = (int)sheet.PixelSize.Height / (int)this._frameHeight;
				int columns = (int)sheet.PixelSize.Width / (int)this._frameWidth;
				int frameCount = 0;
				for (int row = 0; row < rows; row++)
				{
					if (frameCount < this._numFrames)
					{
						for (int col = 0; col < columns; col++)
						{
							int currentY = yOffset + row * this._frameHeight;
							int currentX = xOffset + col * this._frameWidth;
							//cropRect.X = currentX;
							//cropRect.Y = currentY;
							//cropRect = new PixelRect(currentX, currentY, this._frameWidth, this._frameHeight);
							
							//derp.Destingation
							//frame = new CroppedBitmap(sheet, cropRect);
							frames.Add(new RelativeRect(currentX, currentY, this._frameWidth, this._frameHeight, RelativeUnit.Absolute));

							frameCount++;
							if (frameCount == this._numFrames) { break; }
						}
					}
					else { break; }
				}
			}
			return frames;
		}
	}
}
