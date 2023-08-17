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
		private static readonly int _frameDuration = 200;
		private static DispatcherTimer _animationTimer;
		public static DispatcherTimer AnimationTimer
		{
			get
			{
				if (_animationTimer == null)
				{
					_animationTimer = new DispatcherTimer();
					_animationTimer.Interval = TimeSpan.FromMilliseconds(_frameDuration);
					_animationTimer.Start();
				}
				return _animationTimer;
			}
		}
		Bitmap _spriteSheet;
		Image _currentFrame;
		List<CroppedBitmap> _spriteFrames;
		int _numFrames;
		int _frameWidth;
		int _frameHeight;

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
			_currentFrame = new Image();
			_currentFrame.Width = frameWidth;
			_currentFrame.Height = frameHeight;

			this._spriteSheet = spriteSheet;
			this._numFrames = frames;
			this._frameWidth = frameWidth;
			this._frameHeight = frameHeight;

			this._spriteFrames = cutSheet();

			//this._currentFrame = this._spriteFrames[0];
			SetFrame(0);
			AnimationTimer.Tick += (sender, e) => nextFrame();
		}

		public Image Brush
		{
			get { return this._currentFrame; }
		}

		public DispatcherTimer Timer
		{
			get { return AnimationTimer; }
		}

		public List<CroppedBitmap> Frames
		{
			get { return this._spriteFrames; }
		}

		public int FrameDuration
		{
			get { return _frameDuration; }
		}

		/// <summary>
		/// Manually set the frame index. This will not stop the running animation nor change the animation index.
		/// </summary>
		/// <param name="index">Frame index</param>
		public void SetFrame(int index)
		{
			this._currentFrame.Source = this._spriteFrames[index];
		}


		/// <summary>
		/// Pause running sprite animation
		/// </summary>
		public void PauseAnimation()
		{
			AnimationTimer.Stop();
		}

		/// <summary>
		/// Increase animation index by one or reset to zero if at end of list
		/// </summary>
		private void nextFrame()
		{
			this._animIndex++;
			this._animIndex = this._animIndex % this._spriteFrames.Count;
			this._currentFrame.Source = this._spriteFrames[this._animIndex];
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
			List<CroppedBitmap> frames = new List<CroppedBitmap>();

			Bitmap sheet = this._spriteSheet;
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
							cropRect = new PixelRect(currentX, currentY, this._frameWidth, this._frameHeight);

							frames.Add(new CroppedBitmap(sheet, cropRect));

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
