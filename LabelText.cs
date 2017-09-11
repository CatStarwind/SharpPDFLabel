using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coeus.Infrastructure.SharpPDFLabel {
    public class LabelText {
        public string Text { get; set; }
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public int FontStyle { get; set; }
        public bool EmbedFont { get; set; }
    }
}