namespace BarChartRaceNet.Helpers
{
    using FFMpegCore;
    using FFMpegCore.Arguments;
    using FFMpegCore.Enums;
    using FFMpegCore.Extend;
    using FFMpegCore.Pipes;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="FFMpegHelper" />.
    /// </summary>
    public static class FFMpegHelper
    {
        #region Methods

        /// <summary>
        /// The Record.
        /// </summary>
        /// <param name="outputFilePath">The outputFilePath<see cref="string"/>.</param>
        /// <param name="element">The element<see cref="FrameworkElement"/>.</param>
        /// <param name="drawChartAction">The drawChartAction<see cref="Action{int}"/>.</param>
        /// <param name="frameCount">The frameCount<see cref="int"/>.</param>
        public static void Record(string outputFilePath, FrameworkElement element, Action<int> drawChartAction, int frameCount)
        {
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }

            var codecArgs = new VideoCodecArgument(VideoCodec.LibX264);
            var videoType = VideoType.Mp4;

            var videoFramesSource = new RawVideoPipeSource(CreateChartBitmaps(element, drawChartAction, frameCount));
            var arguments = FFMpegArguments.FromPipe(videoFramesSource);
            arguments.WithArgument(codecArgs);
            var processor = arguments.OutputToFile(outputFilePath);
            processor.ProcessSynchronously();
        }

        /// <summary>
        /// The BitmapFromSource.
        /// </summary>
        /// <param name="bitmapsource">The bitmapsource<see cref="BitmapSource"/>.</param>
        /// <returns>The <see cref="System.Drawing.Bitmap"/>.</returns>
        private static System.Drawing.Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            System.Drawing.Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
            }
            return bitmap;
        }

        /// <summary>
        /// The CreateChartBitmaps.
        /// </summary>
        /// <param name="element">The element<see cref="FrameworkElement"/>.</param>
        /// <param name="drawChartAction">The drawChartAction<see cref="Action{int}"/>.</param>
        /// <param name="frameCount">The frameCount<see cref="int"/>.</param>
        /// <returns>The <see cref="IEnumerable{IVideoFrame}"/>.</returns>
        private static IEnumerable<IVideoFrame> CreateChartBitmaps(FrameworkElement element, Action<int> drawChartAction, int frameCount)
        {
            var width = (int)element.ActualWidth;
            var height = (int)element.ActualHeight;
            RenderTargetBitmap renderBmp = null;
            Bitmap bitmap = null;
            element.Invoke(() =>
            {
                renderBmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            });

            for (var frame = 0; frame < frameCount; frame++)
            {
                var stream = new MemoryStream();
                element.Invoke(() =>
                {
                    drawChartAction(frame);
                });

                Thread.Sleep(40);
                element.Invoke(() =>
                {
                    renderBmp.Render(element);
                    bitmap = new Bitmap(renderBmp.PixelWidth, renderBmp.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    var bitmapData = bitmap.LockBits(new Rectangle(System.Drawing.Point.Empty, bitmap.Size),
                        ImageLockMode.WriteOnly, bitmap.PixelFormat);

                    renderBmp.CopyPixels(Int32Rect.Empty, bitmapData.Scan0,
                        bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

                    bitmap.UnlockBits(bitmapData);
                });

                using var wrapper = new BitmapVideoFrameWrapper(bitmap);
                yield return wrapper;
            }
        }

        #endregion Methods
    }
}