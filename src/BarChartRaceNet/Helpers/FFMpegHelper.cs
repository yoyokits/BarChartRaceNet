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
        /// <param name="token">The token<see cref="CancellationToken"/>.</param>
        public static void Record(string outputFilePath, FrameworkElement element, Action<int> drawChartAction, int frameCount, CancellationToken token)
        {
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }

            var codecArgs = new VideoCodecArgument(VideoCodec.LibX264);
            var videoType = VideoType.Mp4;

            var videoFramesSource = new RawVideoPipeSource(CreateChartBitmaps(element, drawChartAction, frameCount, token));
            var arguments = FFMpegArguments.FromPipe(videoFramesSource);
            arguments.WithArgument(codecArgs);
            var processor = arguments.OutputToFile(outputFilePath);
            processor.ProcessSynchronously();
        }

        /// <summary>
        /// The CreateChartBitmaps.
        /// </summary>
        /// <param name="element">The element<see cref="FrameworkElement"/>.</param>
        /// <param name="drawChartAction">The drawChartAction<see cref="Action{int}"/>.</param>
        /// <param name="frameCount">The frameCount<see cref="int"/>.</param>
        /// <param name="token">The token<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="IEnumerable{IVideoFrame}"/>.</returns>
        private static IEnumerable<IVideoFrame> CreateChartBitmaps(FrameworkElement element, Action<int> drawChartAction, int frameCount, CancellationToken token)
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
                if (token.IsCancellationRequested)
                {
                    yield break;
                }

                var stream = new MemoryStream();
                element.Invoke(() =>
                {
                    drawChartAction(frame);
                });

                Thread.Sleep(1);
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

                Thread.Sleep(1);
                using var wrapper = new BitmapVideoFrameWrapper(bitmap);
                yield return wrapper;
            }
        }

        #endregion Methods
    }
}