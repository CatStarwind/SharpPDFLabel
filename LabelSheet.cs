namespace Coeus.Infrastructure.SharpPDFLabel {
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class LabelSheet {
        private Label labelSheet;        
        private Queue<LabelText> labelCells;
        public bool borders;
        public bool repeatText;

        public LabelSheet(Label label) {
            //if (!label.ValidLabel) throw new System.Exception("Invalid Label");

            labelSheet = label;
            labelCells = new Queue<LabelText>();
            borders = false;
            repeatText = false;
        }
        public void AddText(string text, string fontName, int fontSize, bool embedFont = false, params Label.FontStyle[] fontStyles) {
            int fontStyle = 0;
            if (fontStyles != null) {
                foreach (var item in fontStyles) {
                    fontStyle += (int)item;
                }
            }

            labelCells.Enqueue(new LabelText() { Text = text, FontName = fontName, FontSize = fontSize, FontStyle = fontStyle, EmbedFont = embedFont });
        }

        public Stream CreatePDF() {

            Rectangle pageSize;
            switch (labelSheet.PageSize) {
                case Label.PaperSize.A4:
                    pageSize = PageSize.A4;
                    break;
                case Label.PaperSize.LETTER:
                    pageSize = PageSize.LETTER;
                    break;
                default:
                    pageSize = PageSize.LETTER;
                    break;
            }
            
            Document sheet = new Document(pageSize, labelSheet.PageMarginLeft, labelSheet.PageMarginRight, labelSheet.PageMarginTop, labelSheet.PageMarginBottom);
            MemoryStream output = new MemoryStream();
            PdfWriter.GetInstance(sheet, output).CloseStream = false;                        

            PdfPTable labels = new PdfPTable(labelSheet.TotalColumns);
            labels.WidthPercentage = 100;
            labels.SetWidths(labelSheet.TotalWidth);

            if(labelCells.Count < labelSheet.TotalLabels) {
                LabelText copyText = repeatText ? labelCells.Peek() : new LabelText() { Text = "" };
                for (var i = labelCells.Count; i < labelSheet.TotalLabels; i++)
                    labelCells.Enqueue(copyText);
            }

            for (var i = 0; i< labelSheet.rows ; i++) {
                for (var c = 0; c < labelSheet.TotalColumns; c++) {
                    var cellContent = new Phrase();

                    if (c % 2 == 0) {
                        var labelCell = labelCells.Dequeue();
                        var font = FontFactory.GetFont(labelCell.FontName, BaseFont.CP1250, labelCell.EmbedFont, labelCell.FontSize, labelCell.FontStyle);
                        cellContent.Add(new Chunk(labelCell.Text, font));
                    }
  
                    PdfPCell cell = new PdfPCell(cellContent);
                    cell.FixedHeight = labelSheet.Height;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;                    
                    cell.Border = borders ? Rectangle.BOX : Rectangle.NO_BORDER;
                    labels.AddCell(cell);
                }
            }

            sheet.Open();
            sheet.Add(labels);
            sheet.Close();

            output.Position = 0;
            return output;
        }        
    }
}