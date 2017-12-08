using OfficeOpenXml;
using OfficeOpenXml.Style;
using SecuritiesData.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecuritiesData.Service
{
   public class ExcelService
    {
       static string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static void GererateExcel(Security model)
        {

            using (ExcelPackage ep = new ExcelPackage())
            {
                ep.Workbook.Worksheets.Add("Security");
                ExcelWorksheet sheet = ep.Workbook.Worksheets["Security"];
                //標題
                sheet.Cells[1, 1, 1, 7].Merge = true;
                sheet.Cells[1, 1].Value = model.title;
                //欄位
                for (int i = 0; i < model.fields.Count(); i++)
                {
                    sheet.Cells[2, i + 1].Value = model.fields[i];

                }
                //樣式
                using (ExcelRange rng = sheet.Cells["A2:G2"])
                {
                    //rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//文字置中
                    //rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;          //設定背景實線            
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
                    rng.Style.Font.Color.SetColor(Color.White);
                    rng.Style.Font.SetFromFont(new Font("Consolas", 10, FontStyle.Bold));//粗體 斜體
                }
                //塞資料
                sheet.Cells["A3"].LoadFromArrays(model.data);
                sheet.Cells.AutoFitColumns();

                //存檔
                string folder = filepath + "/File";
                string savePath = folder + "/Security.xls";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }

                FileStream fs = new FileStream(savePath, FileMode.Create);
                ep.SaveAs(fs);
                fs.Close();
            }

        }
    }
}
