using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace EVALab.Util.IO
{
    public class ExcelExporter
    {
        public static void DataTableToExcel(DataTable dt, string sheetName, string filePath)
        {
            Excel.Application oXL;
            Excel.Workbook oWB;
            Excel.Worksheet oSheet;
            Excel.Range oRange;

            // Start Excel and get Application object.
            oXL = new Excel.Application();

            // Set some properties
            oXL.Visible = false;
            oXL.DisplayAlerts = false;

            // Get a new workbook.
            oWB = oXL.Workbooks.Add(Missing.Value);

            // Get the active sheet
            oSheet = (Excel.Worksheet)oWB.ActiveSheet;
            oSheet.Name = sheetName;

            // Process the DataTable
            
            int rowCount = 1;
            foreach (DataRow dr in dt.Rows)
            {
                rowCount += 1;
                for (int i = 1; i < dt.Columns.Count + 1; i++)
                {
                    // Add the header the first time through
                    if (rowCount == 2)
                    {
                        oSheet.Cells[1, i] = dt.Columns[i - 1].ColumnName;
                    }
                    oSheet.Cells[rowCount, i] = dr[i - 1].ToString();
                }
            }

            // Resize the columns
            oRange = oSheet.get_Range(oSheet.Cells[1, 1],
                          oSheet.Cells[rowCount, dt.Columns.Count]);
            oRange.EntireColumn.AutoFit();

            // Save the sheet and close
            oSheet = null;
            oRange = null;
            oWB.SaveAs(filePath, Excel.XlFileFormat.xlWorkbookNormal,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Excel.XlSaveAsAccessMode.xlExclusive,
                Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value);
            oWB.Close(Missing.Value, Missing.Value, Missing.Value);
            oWB = null;
            oXL.Quit();

            // Clean up
            // NOTE: When in release mode, this does the trick
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect(); 
        }
    }
}
