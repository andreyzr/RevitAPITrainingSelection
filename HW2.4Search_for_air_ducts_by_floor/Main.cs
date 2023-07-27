using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2._4Search_for_air_ducts_by_floor
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements) 
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var ducts = new FilteredElementCollector(doc)
                .OfClass(typeof(Duct))
                .Cast<Duct>()
                .ToList();

            string info = string.Empty;
            string [] levels = new string[0];
            int [] counter = new int[0];
            int i = 0;
            foreach (var duct in ducts)
            {
                Array.Resize(ref levels, i + 1);
                string level = duct.ReferenceLevel.Name;
                if (levels.Contains(level))
                {
                    continue;
                }
                levels[i]= level;
                foreach(var duct2 in ducts)
                {
                    if(duct2.ReferenceLevel.Name== level)
                    {
                        Array.Resize(ref counter, i + 1);
                        counter[i]++;
                    }
                }
                info += $"Имя уровня: {levels[i]}, колличество труб: {counter[i]}{Environment.NewLine}";
                counter[i] = 0;
            }

            info += $"Общее количество воздуховодов: {ducts.Count}";

            TaskDialog.Show("Информация о трубе", info) ;


            return Result.Succeeded;
        }
    }
}
