using System;
using System.IO;
using System.Text;
using iText.Kernel.Pdf;

namespace POCPDFPwd
{
    class Program
    {
        public static readonly String DEST = "/Users/paulus/Documents/SSC/decrypt_pdf.pdf";
        public static readonly String SRC = "/Users/paulus/Documents/SSC/[ENCRYPTED] Citibank_Debit_Advice_00480613032021_ref3311027007.pdf";

        public static readonly String OWNER_PASSWORD = "Sscpayment#2017";

        static void Main(string[] args)
        {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();

            try
            {
                var reader = new PdfReader(SRC);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            new Program().ManipulatePdf(DEST);
        }

        protected void ManipulatePdf(String dest)
        {
            using (PdfDocument document = new PdfDocument(
                new PdfReader(SRC, new ReaderProperties().SetPassword(Encoding.UTF8.GetBytes(OWNER_PASSWORD))),
                new PdfWriter(dest)
            ))
            {
                byte[] computeUserPassword = document.GetReader().ComputeUserPassword();

                // The result of user password computation logic can be null in case of
                // AES256 password encryption or non password encryption algorithm
                String userPassword = computeUserPassword == null ? null : Encoding.UTF8.GetString(computeUserPassword);
                Console.Out.WriteLine(userPassword);
            }
        }
    }
}
