using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;
using Bonsai.Vision;
using Bonsai.IO;
using System.IO;

namespace Bonsai.FFmpeg
{
    /// <summary>
    /// Represents an operator that writes a sequence of images to a video file using an FFmpeg process.
    /// </summary>
    [DefaultProperty("FileName")]
    [Description("Writes a sequence of images to a video file using an FFmpeg process.")]
    public class VideoWriter : Sink<IplImage>
    {
        /// <summary>
        /// Gets or sets the name of the output file.
        /// </summary>
        [Description("The name of the output file.")]
        [Editor("Bonsai.Design.SaveFileNameEditor, Bonsai.Design", DesignTypes.UITypeEditor)]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the optional suffix used to generate file names.
        /// </summary>
        [Description("The optional suffix used to generate file names.")]
        public PathSuffix Suffix { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the output file should be overwritten if it already exists.
        /// </summary>
        [Description("Indicates whether the output file should be overwritten if it already exists.")]
        public bool Overwrite { get; set; }

        /// <summary>
        /// Gets or sets the playback frame rate of the image sequence.
        /// </summary>
        [Description("The playback frame rate of the image sequence.")]
        public int FrameRate { get; set; }

        /// <summary>
        /// Gets or sets the optional set of command-line arguments to use for configuring the video codec.
        /// </summary>
        [Description("The optional set of command-line arguments to use for configuring the video codec.")]
        public string OutputArguments { get; set; }

        /// <summary>
        /// Writes an observable sequence of images to a video file using an FFmpeg process.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="IplImage"/> objects representing the individual video
        /// frames to include in the output file.
        /// </param>
        /// <returns>
        /// An observable sequence that is identical to the <paramref name="source"/>
        /// sequence but where there is an additional side effect of writing the
        /// sequence of images to a video file using an FFmpeg process.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// The file already exists.
        /// </exception>
        public override IObservable<IplImage> Process(IObservable<IplImage> source)
        {
            return source.Publish(ps =>
            {
                var fileName = PathHelper.AppendSuffix(FileName, Suffix);
                var overwrite = Overwrite;
                if (File.Exists(fileName) && !overwrite)
                {
                    throw new InvalidOperationException(string.Format("The file '{0}' already exists.", fileName));
                }

                PathHelper.EnsureDirectory(fileName);
                var pipe = @"\\.\pipe\" + Path.GetFileNameWithoutExtension(fileName);
                var writer = new ImageWriter { Path = pipe };
                return writer.Process(ps).Merge(ps.Take(1).Delay(TimeSpan.FromSeconds(1)).SelectMany(image =>
                {
                    var args = string.Format("-f rawvideo -vcodec rawvideo {0}-s {1}x{2} -r {3} -pix_fmt {4} -i {5} {6} {7}",
                        overwrite ? "-y " : string.Empty,
                        image.Width,
                        image.Height,
                        FrameRate,
                        image.Channels == 1 ? "gray" : "bgr24",
                        pipe,
                        OutputArguments,
                        fileName);
                    var ffmpeg = new StartProcess();
                    ffmpeg.Arguments = args;
                    ffmpeg.FileName = "ffmpeg.exe";
                    return ffmpeg.Generate().IgnoreElements().Select(x => default(IplImage));
                }));
            });
        }
    }
}
