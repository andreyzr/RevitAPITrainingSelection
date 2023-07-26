using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingSelectionActive_view
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements) //1
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> selectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, new WallFilter(), "Выберете стены");
            var wallList = new List<Wall>();

            string info = string.Empty;
            foreach (var selectedElement in selectedElementRefList)
            {
                Wall oWall = doc.GetElement(selectedElement) as Wall;
                wallList.Add(oWall);
                var width = UnitUtils.ConvertFromInternalUnits(oWall.Width, UnitTypeId.Millimeters);
                info += $"Name: {oWall.Name}, width: {width}{Environment.NewLine}";
            }

            info += $"Количество: {wallList.Count}";

            TaskDialog.Show("Selection", info);

            return Result.Succeeded;
        }
    }
}
