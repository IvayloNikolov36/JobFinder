namespace JobFinder.Services.Implementations
{
    using iTextSharp.text;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;
    using System.IO;

    public class PdfGenerator : IPdfGenerator
    {
        public byte[] Generate(string htmlString)
        {
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var htmlparser = new HtmlWorker(pdfDoc);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                using (var sr = new StringReader(htmlString))
                {
                    htmlparser.Parse(sr);
                }
                
                pdfDoc.Close();

                byte[] bytes = memoryStream.ToArray();
                return bytes;
            }
        } 
    }
}
