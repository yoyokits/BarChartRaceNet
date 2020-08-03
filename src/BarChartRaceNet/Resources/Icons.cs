namespace BarChartRaceNet.Resources
{
    using System.Windows.Media;

    /// <summary>
    /// Defines the <see cref="Icons" />.
    /// </summary>
    public static class Icons
    {
        #region Properties

        /// <summary>
        /// Gets the About.
        /// </summary>
        public static Geometry About { get; } = Geometry.Parse("M1,10L3,10 3,32 1,32z M2,0C3.1,0 4,0.9 4,2 4,3.1 3.1,4 2,4 0.9,4 0,3.1 0,2 0,0. 0.9,0 2,0z");

        /// <summary>
        /// Gets the Cancel.
        /// </summary>
        public static Geometry Cancel { get; } = Geometry.Parse("M9.9,6L6,9.9 12.1,16 6,22.1 9.9,26 16,19.9 22.1,26 26,22.1 19.9,16 26,9.9 22.1,6 16,12.17z M16,0C24.8,0 32,7.2 32,16 32,24.8 24.8,32 16,32 7.2,32 0,24.8 0,16 0,7.2 7.2,0 16,0z");

        /// <summary>
        /// Gets the ExportChart.
        /// </summary>
        public static Geometry ExportChart { get; } = Geometry.Parse("M15.49,19.7L15.59,19.7C15.9,19.8,16.29,19.9,16.7,19.9L21.48,19.9 21.48,25.48C21.48,27 20.18,28.5 18.5,28.5 16.89,28.5 15.49,27.18 15.5,25.48z M25.6,10.58C25.63,10.6 25.7,10.6 25.68,10.6 25.9,10.6 26,10.7 26.2,10.8L27.58,12C28.28,12.59,29.38,13.49,30,14L31.47,15.29C32.17,15.9,32.2,16.79,31.47,17.29L30,18.4C29.377,19,28.28,19.9,27.6,20.38L26.179,21.485C25.48,22,25,21.6,25,20.48L25,18.3 16.7,18.3C16.1,18.3,15.6,17.8,15.6,17.2L15.6,15C15.6,14.4,16,13.9,16.7,13.9L25,13.9 25,11.7C25,11,25.24,10.6,25.6,10.6z M2.4,9.7C3.7,9.7,4.8,10.8,4.8,12L4.8,19.9C4.8,21.2 3.7,22.28 2.4,22.3 1.1,22.3 0,21.18 0,19.9L0,12C0,10.8,1,9.7,2.4,9.7z M18.5,3.3C20,3.3,21.5,4.6,21.5,6.3L21.5,12.5 16.7,12.5C16.3,12.5,16,12.59,15.6,12.7L15.6,6.3C15.6,4.6,16.9,3.3,18.5,3.3z M9.7,0C11.2,0,12.5,1.3,12.5,2.8L12.5,16.7C12.5,18.19 11.2,19.5 9.7,19.5 8.2,19.5 6.9,18.19 6.9,16.7L6.9,2.8C6.9,1.3,8.2,0,9.7,0z");

        /// <summary>
        /// Gets the Load.
        /// </summary>
        public static Geometry Load { get; } = Geometry.Parse("M0,0.4L10.1,0.4 10.1,3 17.9,3 17.6,3.2C16.1,4,15,5,14.3,6.1L0,6.1 0,3.5 0,3z M26.3,0L32,5.7 26.3,10.7 26.3,8C26.3,8,19.1,6,15.5,8.9L15.5,9 25.2,9 25.2,20.5 0,20.5 0,9 14.4,9 14.4,8.9C15.2,6.5,17.8,2.7,26.3,2.7z");

        /// <summary>
        /// Gets the Pause.
        /// </summary>
        public static Geometry Pause { get; } = Geometry.Parse("M17.9,0L29.1,0 29.1,32 17.9,32z M0,0L11.2,0 11.2,32 0,32z");

        /// <summary>
        /// Gets the Play.
        /// </summary>
        public static Geometry Play { get; } = Geometry.Parse("M0,0L15.8,8 31.65,16 15.8,24 0,32 0,16z");

        #endregion Properties
    }
}