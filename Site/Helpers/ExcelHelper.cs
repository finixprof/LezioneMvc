using OfficeOpenXml;
using Org.BouncyCastle.Bcpg;
using Site.Models.Entities;

namespace Site.Helpers
{
    public static class ExcelHelper
    {
        public static byte[] CreaFileExcelListaPersonale(List<Personale> lista)
        {
            MemoryStream stream = new MemoryStream();
            //using (var package = new ExcelPackage(new FileInfo("e:\\Personale.xlsx")))
            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Personale");
                ws.Cells.LoadFromCollection(lista);
                //package.Save();

                return package.GetAsByteArray();
            }
        }
    }
}
