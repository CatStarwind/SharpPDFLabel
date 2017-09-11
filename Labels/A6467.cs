namespace Coeus.Infrastructure.SharpPDFLabel.Labels {    
    /// <summary>
	/// Dimensions: 0.5in x 1.75in
	/// Per Sheet: 80 per sheet 
	/// Inkjet code: ?
	/// </summary>
    public class A6467 : Label {
        public A6467() {
            height = 0.5;
            width = 1.75;            
            gapWidth = 0.3;
            gapHeight = 0;

            pageMarginTop = 0.5;
            pageMarginBottom = 0.5;
            pageMarginLeft = 0.3;
            pageMarginRight = 0.3;

            PageSize = PaperSize.LETTER;
            columns = 4;
            rows = 20;
        }
    }
}