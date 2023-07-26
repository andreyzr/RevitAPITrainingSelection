using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingSelectionPoint
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var pickedPoint = uidoc.Selection.PickPoint(ObjectSnapTypes.Endpoints, "Выберете точку");
            TaskDialog.Show("Point info", $"X: {pickedPoint.X}, Y: {pickedPoint.Y}, Z:{pickedPoint.Z}") ;

            return Result.Succeeded;
        }
    }
}
