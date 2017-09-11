namespace Coeus.Infrastructure.SharpPDFLabel {
    using System;
    using System.Collections.Generic;    

    /// <summary>
    /// The base class for a Label, all the actual label definition classes derive from this
    /// Dimensions and margins are defined in inches but converted to Points when read back (for use in iTextSharp)
    /// </summary>
    public abstract class Label {
        public enum PaperSize { A4, LETTER }
        [Flags]
        public enum FontStyle {
            BOLD = 1,
            BOLDITALIC = 3,
            DEFAULTSIZE = 12,
            ITALIC = 2,
            NORMAL = 0,
            STRIKETHRU = 8,
            UNDEFINED = -1,
            UNDERLINE = 4,
        }

        /// <summary>
        /// The height of 1 label
        /// </summary>
        protected double height;
        public float Height {
            get { return inToPoint(height); }
        }

        /// <summary>
        /// The width of 1 label
        /// </summary>
        protected double width;
        public float Width {
            get { return inToPoint(width); }
        }

        /// <summary>
        /// The width of the horizontal gap between labels
        /// </summary>
        protected double gapWidth;
        public float GapWidth {
            get { return inToPoint(gapWidth); }
        }

        /// <summary>
        /// The height of the vertical gap between labels
        /// </summary>
        protected double gapHeight;
        public float GapHeight {
            get { return inToPoint(gapHeight); }
        }

        /// <summary>
        /// The left page margin
        /// </summary>
        protected double pageMarginLeft;
        public float PageMarginLeft {
            get { return inToPoint(pageMarginLeft); }
        }

        /// <summary>
        /// The right page margin
        /// </summary>
        protected double pageMarginRight;
        public float PageMarginRight {
            get { return inToPoint(pageMarginRight); }
        }

        /// <summary>
        /// The top page margin
        /// </summary>
        protected double pageMarginTop;
        public float PageMarginTop {
            get { return inToPoint(pageMarginTop); }
        }

        /// <summary>
        /// The bottom page margin
        /// </summary>
        protected double pageMarginBottom;
        public float PageMarginBottom {
            get { return inToPoint(pageMarginBottom); }
        }
        

        /// <summary>
        /// The paper size
        /// </summary>
        public PaperSize PageSize { get; set; }


        /// <summary>
        /// The number of labels running across the page
        /// </summary>
        public int columns { get; set; }


        /// <summary>
        /// The number of labels running down the page
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// iTextSharp uses points as its units
        /// </summary>
        /// <param name="inch">Inches to convert</param>
        /// <returns>Inches converted to points represented by a float</returns>
        private float inToPoint(double inch) {
            return (float)(inch * 72);
        }

        public float[] TotalWidth {
            get {
                var width = new List<float>();

                for (var i = 0; i < TotalColumns; i++) {
                    width.Add((i % 2 == 0) ? Width : GapWidth);
                }
                return width.ToArray();
            }
        }

        public int TotalColumns {
            get {
                return (columns * 2) - 1;
            }
        }

        public int TotalLabels {
            get {
                return columns * rows;
            }
        }
        /*
        public bool ValidLabel {
            get {
                float sheetWidth = (columns * Width) + ((columns - 1) * GapWidth) + PageMarginLeft + PageMarginRight;
                float sheetHeight = (rows * Height) + ((Height - 1) * GapHeight) + PageMarginTop + PageMarginBottom;

                return sheetWidth == PageSize.A4.Width && sheetHeight == PageSize.A4.Height;
            }
        }*/

    }
}