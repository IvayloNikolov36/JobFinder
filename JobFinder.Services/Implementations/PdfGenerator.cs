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

            HtmlWorker htmlparser = new HtmlWorker(pdfDoc);

            using (MemoryStream memoryStream = new())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                using (StringReader sr = new StringReader(htmlString))
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
