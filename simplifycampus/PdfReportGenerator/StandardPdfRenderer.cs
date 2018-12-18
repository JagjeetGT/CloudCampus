// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardPdfRenderer.cs" company="SemanticArchitecture">
//   http://www.SemanticArchitecture.net
// </copyright>
// <summary>
//   This class is responsible for rendering a html text string to a PDF document
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace ReportManagement
{
    using System.IO;

    using iTextSharp.text;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;

    /// <summary>
    /// This class is responsible for rendering a html text string to a PDF document using the html renderer of iTextSharp.
    /// </summary>
    public class StandardPdfRenderer
    {
        private const int HorizontalMargin = 40;
        private const int VerticalMargin = 40;

        public byte[] Render(string htmlText, string pageTitle, Rectangle pageSize, List<FloatWidth> floatWidth)
        {
            byte[] renderedBuffer;

            using (var outputMemoryStream = new MemoryStream())
            {
                if (pageSize == null)
                    pageSize = PageSize.A4;
                using (var pdfDocument = new Document(pageSize, HorizontalMargin, HorizontalMargin, VerticalMargin, VerticalMargin))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, outputMemoryStream);
                    pdfWriter.CloseStream = false;
                    pdfWriter.PageEvent = new PrintHeaderFooter { Title = pageTitle };
                    pdfDocument.Open();

                    //using (var htmlViewReader = new StringReader(htmlText))
                    //{
                    //    using (var htmlWorker = new HTMLWorker(pdfDocument))
                    //    {
                    //        htmlWorker.Parse(htmlViewReader);
                    //    }
                    //}
                    var i = 0;
                    using (var htmlViewReader = new StringReader(htmlText))
                    {
                        var ie = HTMLWorker.ParseToList(htmlViewReader,null);
                        float pageWidth = pageSize.Width;
                        foreach (IElement element in ie)
                        {
                            PdfPTable table = element as PdfPTable;
                            /*
                             * set the column widths
                             */
                            if (floatWidth != null)
                            {
                                if (table != null)
                                {

                                    float[] width = floatWidth[i].Width.Select(ft => ft * pageWidth).ToArray();
                                    table.SetWidthPercentage(width, pageSize);
                                    i++;
                                    pdfDocument.Add(table);


                                }
                                else
                                {
                                    pdfDocument.Add(element);
                                }
                            }
                            else
                            {
                                pdfDocument.Add(element);
                            }

                        }
                    }
                }
                renderedBuffer = new byte[outputMemoryStream.Position];
                outputMemoryStream.Position = 0;
                outputMemoryStream.Read(renderedBuffer, 0, renderedBuffer.Length);
            }

            return renderedBuffer;
        }
        public byte[] Render(string htmlText, string pageTitle, Rectangle pageSize, List<FloatWidth> floatWidth, int HorizontalMargin, int VerticalMargin)
        {
            byte[] renderedBuffer;

            using (var outputMemoryStream = new MemoryStream())
            {
                if (pageSize == null)
                    pageSize = PageSize.A4;
                using (var pdfDocument = new Document(pageSize, HorizontalMargin, HorizontalMargin, VerticalMargin, VerticalMargin))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, outputMemoryStream);
                    pdfWriter.CloseStream = false;
                    pdfWriter.PageEvent = new PrintHeaderFooter { Title = pageTitle };
                    pdfDocument.Open();

                    //using (var htmlViewReader = new StringReader(htmlText))
                    //{
                    //    using (var htmlWorker = new HTMLWorker(pdfDocument))
                    //    {
                    //        htmlWorker.Parse(htmlViewReader);
                    //    }
                    //}
                    var i = 0;
                    using (var htmlViewReader = new StringReader(htmlText))
                    {
                        var ie = HTMLWorker.ParseToList(htmlViewReader, null);
                        float pageWidth = pageSize.Width;
                        foreach (IElement element in ie)
                        {
                            
                        
                            PdfPTable table = element as PdfPTable;
                            /*
                             * set the column widths
                             */
                            if (floatWidth != null)
                            {
                                if (table != null)
                                {

                                    float[] width = floatWidth[i].Width.Select(ft => ft * pageWidth).ToArray();
                                    table.SetWidthPercentage(width, pageSize);
                                    i++;
                                    pdfDocument.Add(table);


                                }
                                else
                                {
                                    pdfDocument.Add(element);
                                }
                            }
                            else
                            { 

                                                              pdfDocument.Add(element);
                                                             
                            }

                        }
                        pdfDocument.NewPage(); 
                    }
                }
                renderedBuffer = new byte[outputMemoryStream.Position];
                outputMemoryStream.Position = 0;
                outputMemoryStream.Read(renderedBuffer, 0, renderedBuffer.Length);
            }

            return renderedBuffer;
        }
    }
}