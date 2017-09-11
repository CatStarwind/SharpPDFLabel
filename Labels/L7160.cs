namespace Coeus.Infrastructure.SharpPDFLabel.Labels {
    /// <summary>
    /// Dimensions: 63.5mm x 38.1mm 
    /// Per Sheet: 21 per sheet 
    /// Inkjet code: J8160
    /// </summary>
    public class L7160 : Label {
        public L7160() {
            width = 63.5 / 25.4;
            height = 38.1 / 25.4;
            gapWidth = 2.5 / 25.4;
            gapHeight = 0;

            pageMarginTop = 15.1 / 25.4;
            pageMarginBottom = 15.1 / 25.4;
            pageMarginLeft = 7.2 / 25.4;
            pageMarginRight = 7.2 / 25.4;

            PageSize = PaperSize.A4;
            columns = 3;
            rows = 7;
        }
    }
}