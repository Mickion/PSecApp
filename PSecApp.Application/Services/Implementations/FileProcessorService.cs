using Excel = Microsoft.Office.Interop.Excel;
using PSecApp.Application.Services.Abstractions;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data;

namespace PSecApp.Application.Services.Implementations
{
    public class FileProcessorService : IFileProcessorService
    {
        public void ProcessDownloadedFile(string fileLocation)
        {
            Console.WriteLine("PROCESS FILE : Length {0} Name {1}", new FileInfo(fileLocation).Length, fileLocation);

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileLocation); //, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[1];
            //Excel.Range xlRange = xlWorksheet.UsedRange;

            // var tst = xlRange.Value2;
            int xlColums = xlWorksheet.UsedRange.Columns.Count;
            for (int col = 1; col < length; col++)
            {

            }
        
            //var tst = READExcel(fileLocation);
            //int rowCount = xlRange.Rows.Count;
            //int colCount = xlRange.Columns.Count;

            //for (int i = 1; i <= rowCount; i++)
            //{
            //    for (int j = 1; j <= colCount; j++)
            //    {
            //        //string cellValue = (string)(xlRange.Cells[i, j] as Excel.Range).Value2;
            //        Console.WriteLine(xlRange.Cells[i, j].Value2);
            //    }
            //}

            //xlWorkbook.Close();
            //xlApp.Quit();
        }

        public System.Data.DataTable READExcel(string path)
        {
            Microsoft.Office.Interop.Excel.Application objXL = null;
            Microsoft.Office.Interop.Excel.Workbook objWB = null;
            objXL = new Microsoft.Office.Interop.Excel.Application();
            objWB = objXL.Workbooks.Open(path);
            Microsoft.Office.Interop.Excel.Worksheet objSHT = objWB.Worksheets[1];

            int rows = objSHT.UsedRange.Rows.Count;
            int cols = objSHT.UsedRange.Columns.Count;
            System.Data.DataTable dt = new System.Data.DataTable();
            int noofrow = 1;

            for (int c = 1; c <= cols; c++)
            {
                string colname = objSHT.Cells[1, c].Text;
                dt.Columns.Add(colname);
                noofrow = 2;
            }

            for (int r = noofrow; r <= rows; r++)
            {
                DataRow dr = dt.NewRow();
                for (int c = 1; c <= cols; c++)
                {
                    dr[c - 1] = objSHT.Cells[r, c].Text;
                }

                dt.Rows.Add(dr);
            }

            objWB.Close();
            objXL.Quit();
            return dt;
        }

        //private System.Data.DataTable ConvertWorkbookToDataTable(Excel.Range xlRange)
        //{
        //    var dataTable = new System.Data.DataTable();

        //    object[,] rowArray = (object[,])xlRange.Value2;

        //    for (int rowNum = 1; rowNum <= rowArray.GetUpperBound(0); rowNum++)
        //    {
        //        for (int columnNum = 1; columnNum <= rowArray.GetUpperBound(1); columnNum++)
        //        {
        //            var tst = rowArray[rowNum, columnNum].ToString()
        //        }
                    
        //    }

        //        //    //Gets the entire used range as a 2D array
        //        //    object[,] rangeAsArray = (object[,])xlWorksheet?.UsedRange.Value2;

        //     return dataTable;
        //}

        //private static DataTable WorkbookToDataTable(string filePath, int sheet = 1, int skipfirstRows = 0)
        //{
        //    Excel.Application xlApp = new Excel.Application();
        //    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileLocation); //, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
        //    Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[1];
        //    Excel.Range xlRange = xlWorksheet.UsedRange;

        //    //Gets the entire used range as a 2D array
        //    object[,] rangeAsArray = (object[,])xlWorksheet?.UsedRange.Value2;

        //    List<string> newRow = new List<string>();
        //    var dataTable = new DataTable();

        //    for (int rowNum = 1; rowNum <= rowArray.GetUpperBound(0); rowNum++)
        //    {
        //        for (int columnNum = 1; columnNum <= rowArray.GetUpperBound(1); columnNum++)
        //        {
        //            // In my solution, the first row of the table is assumed to be header rows.
        //            // So the first row's items will be the name of each column
        //            if (rowNum == 1)
        //            {
        //                dataTable.Columns.Add(new System.Data.DataColumn(rowArray[rowNum, columnNum].ToString(), typeof(object)));
        //            }
        //            else if (rowArray[rowNum, columnNum] != null)
        //            {
        //                newRow.Add(rowArray[rowNum, columnNum].ToString());
        //            }
        //        }
        //    }

        //    if (rowNum != 1)
        //    {
        //        dataTable.Rows.Add(newRow);
        //        newRow = new List<string>();
        //    }

        //    workbook.Close();
        //    app.Quit();
        //    return dataTable;
        //}

        //private async Task ProcessDownloadedFileAsync(string fileLocation)
    }
}
