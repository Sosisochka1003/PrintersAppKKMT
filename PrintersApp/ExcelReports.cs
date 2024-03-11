using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetLight;
using Microsoft.Win32;
using System.Windows;
using System.Collections.Immutable;

namespace PrintersApp
{
    
    public class ExcelReports
    {
        public static void ShipmentReport(ContextDataBase ctx)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.FileName = $"Отчет по расходу картриджей";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (SLDocument Doc = new SLDocument())
                {
                    Doc.SetCellValue(1,1,"Номер записи");
                    Doc.SetCellValue(1, 2, "Принтер");
                    Doc.SetCellValue(1, 3, "Аудитория");
                    Doc.SetCellValue(1, 4, "Картридж");
                    Doc.SetCellValue(1, 5, "Дата выдачи");
                    Doc.SetCellValue(1, 6, $"Дата генерации файла:{DateTime.Now}");
                    var shipment = ctx.Shipments.ToList();
                    for (int i = 0; i < shipment.Count; i++)
                    {
                        Doc.SetCellValue(i + 2, 1, $"{shipment[i].Id}");
                        Doc.SetCellValue(i + 2, 2, $"{ctx.Printers.FirstOrDefault(p => p.Id == shipment[i].PrinterId).Name}");
                        Doc.SetCellValue(i + 2, 3, $"{shipment[i].Room}");
                        Doc.SetCellValue(i + 2, 4, $"{ctx.Cartridges.FirstOrDefault(p => p.Id == shipment[i].CartridgeId).Name}");
                        Doc.SetCellValue(i + 2, 5, $"{shipment[i].ShipmentDate}");
                    }
                    Doc.SaveAs(saveFileDialog.FileName);
                }
            }
        }

        public static void CartridgeCount(ContextDataBase ctx)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.FileName = $"Отчет по остатку картриджей";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (SLDocument Doc = new SLDocument())
                {
                    Doc.SetCellValue(1, 1, "Номер записи");
                    Doc.SetCellValue(1, 2, "Картридж");
                    Doc.SetCellValue(1, 3, "Остаток");
                    Doc.SetCellValue(1, 4, $"Дата генерации файла:{DateTime.Now}");
                    var cartridge = ctx.Cartridges.ToList();
                    for (int i = 0; i < cartridge.Count; i++)
                    {
                        Doc.SetCellValue(i + 2, 1, $"{cartridge[i].Id}");
                        Doc.SetCellValue(i + 2, 2, $"{cartridge[i].Name}");
                        Doc.SetCellValue(i + 2, 3, $"{cartridge[i].StockCount}");
                    }
                    Doc.SaveAs(saveFileDialog.FileName);
                }
            }
        }

        public static void CartridgeComming(ContextDataBase ctx)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.FileName = $"Отчет по приходу картриджей";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (SLDocument Doc = new SLDocument())
                {
                    Doc.SetCellValue(1, 1, "Номер записи");
                    Doc.SetCellValue(1, 2, "Картридж");
                    Doc.SetCellValue(1, 3, "Пришло");
                    Doc.SetCellValue(1, 4, "Дата прихода");
                    Doc.SetCellValue(1, 5, $"Дата генерации файла:{DateTime.Now}");
                    var commings = ctx.Commings.ToList();
                    for (int i = 0; i < commings.Count; i++)
                    {
                        Doc.SetCellValue(i + 2, 1, $"{commings[i].Id}");
                        Doc.SetCellValue(i + 2, 2, $"{ctx.Cartridges.FirstOrDefault(p => p.Id == commings[i].CartridgeId).Name}");
                        Doc.SetCellValue(i + 2, 3, $"{commings[i].Count}");
                        Doc.SetCellValue(i + 2, 4, $"{commings[i].CommingDate}");
                    }
                    Doc.SaveAs(saveFileDialog.FileName);
                }
            }
        }
    }
}
