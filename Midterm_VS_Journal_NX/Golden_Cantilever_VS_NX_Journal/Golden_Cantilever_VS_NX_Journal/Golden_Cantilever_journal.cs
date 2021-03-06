//From:     Devin Adams
//For:      Dr. Salmon
//Date:     2 April 2018
//Purpose:  Create a prt file and igs file for the golden cantilever beam that
//          minimizes the volume for a given load. This accepts
//          the height, length, thickness, and name of file to be created as 
//          arguments.

//NX 10.0.0.24
// Journal created by dadams26 on Mon Mar 26 20:16:09 2018 Mountain Daylight Time
//
using System;
using NXOpen;

public class NXJournal
{
  public static void Main(string[] args)
  {

      Console.WriteLine("Starting NX Golden Cantilever Journal");

      //Declare Part Variables
      //Convert Strings to doubles from the args
      double heightA = System.Convert.ToDouble(args[0]);
      double length = System.Convert.ToDouble(args[1]);
      double thickness = System.Convert.ToDouble(args[2]);
      

      //Declare part name for the part file and the IGES file
      string part_name = args[3];
      string part_name_prt = part_name + ".prt";
      string part_name_igs = part_name + ".igs";

      //Measurements need to be converted from m to mm
      //length = 1000 * length;
      //thickness = 1000 * thickness;
      //heightA = 1000 * heightA;
      double heightB = 1.618 * heightA;


      //Convert necessary values back to strings because I already 
      //converted them to doubles and I'm too lazy to go back and change
      //this since it works.
      string length_string = System.Convert.ToString(length);
      string thickness_string = System.Convert.ToString(thickness);
      string heightA_string = System.Convert.ToString(heightA);
      string heightB_string = System.Convert.ToString(heightB);

    NXOpen.Session theSession = NXOpen.Session.GetSession();
    // ----------------------------------------------
    //   Menu: File->New...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId1;
    markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.FileNew fileNew1;
    fileNew1 = theSession.Parts.FileNew();
    
    theSession.SetUndoMarkName(markId1, "New Dialog");
    
    NXOpen.Session.UndoMarkId markId2;
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "New");
    
    theSession.DeleteUndoMark(markId2, null);
    
    NXOpen.Session.UndoMarkId markId3;
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "New");
    
    fileNew1.TemplateFileName = "model-plain-1-mm-template.prt";
    
    fileNew1.UseBlankTemplate = false;
    
    fileNew1.ApplicationName = "ModelTemplate";
    
    fileNew1.Units = NXOpen.Part.Units.Millimeters;
    
    fileNew1.RelationType = "";
    
    fileNew1.UsesMasterModel = "No";
    
    fileNew1.TemplateType = NXOpen.FileNewTemplateType.Item;
    
    fileNew1.TemplatePresentationName = "Model";
    
    fileNew1.ItemType = "";
    
    fileNew1.Specialization = "";
    
    fileNew1.SetCanCreateAltrep(false);
    
    fileNew1.NewFileName = part_name_prt;
    
    fileNew1.MasterFileName = "";
    
    fileNew1.MakeDisplayedPart = true;
    
    NXOpen.NXObject nXObject1;
    nXObject1 = fileNew1.Commit();
    
    NXOpen.Part workPart = theSession.Parts.Work;
    NXOpen.Part displayPart = theSession.Parts.Display;
    theSession.DeleteUndoMark(markId3, null);
    
    fileNew1.Destroy();
    
    theSession.ApplicationSwitchImmediate("UG_APP_MODELING");
    
    NXOpen.Session.UndoMarkId markId4;
    markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Enter Gateway");
    
    NXOpen.Session.UndoMarkId markId5;
    markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Enter Modeling");
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId6;
    markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.Sketch nullNXOpen_Sketch = null;
    NXOpen.SketchInPlaceBuilder sketchInPlaceBuilder1;
    sketchInPlaceBuilder1 = workPart.Sketches.CreateNewSketchInPlaceBuilder(nullNXOpen_Sketch);
    
    NXOpen.Unit unit1 = (NXOpen.Unit)workPart.UnitCollection.FindObject("MilliMeter");
    NXOpen.Expression expression1;
    expression1 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression2;
    expression2 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.SketchAlongPathBuilder sketchAlongPathBuilder1;
    sketchAlongPathBuilder1 = workPart.Sketches.CreateSketchAlongPathBuilder(nullNXOpen_Sketch);
    
    sketchAlongPathBuilder1.PlaneLocation.Expression.RightHandSide = "0";
    
    theSession.SetUndoMarkName(markId6, "Create Sketch Dialog");
    
    NXOpen.DatumPlane datumPlane1 = (NXOpen.DatumPlane)workPart.Datums.FindObject("DATUM_CSYS(0) YZ plane");
    NXOpen.Point3d point1 = new NXOpen.Point3d(11.1863449593062, 6.4875984180345, 0.0);
    sketchInPlaceBuilder1.PlaneOrFace.SetValue(datumPlane1, workPart.ModelingViews.WorkView, point1);
    
    NXOpen.Features.DatumCsys datumCsys1 = (NXOpen.Features.DatumCsys)workPart.Features.FindObject("DATUM_CSYS(0)");
    NXOpen.Point point2 = (NXOpen.Point)datumCsys1.FindObject("POINT 1");
    sketchInPlaceBuilder1.SketchOrigin = point2;
    
    sketchInPlaceBuilder1.PlaneOrFace.Value = null;
    
    sketchInPlaceBuilder1.PlaneOrFace.Value = datumPlane1;
    
    NXOpen.DatumAxis datumAxis1 = (NXOpen.DatumAxis)workPart.Datums.FindObject("DATUM_CSYS(0) X axis");
    sketchInPlaceBuilder1.Axis.Value = datumAxis1;
    
    NXOpen.Session.UndoMarkId markId7;
    markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Sketch");
    
    theSession.DeleteUndoMark(markId7, null);
    
    NXOpen.Session.UndoMarkId markId8;
    markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Sketch");
    
    theSession.Preferences.Sketch.CreateInferredConstraints = true;
    
    theSession.Preferences.Sketch.ContinuousAutoDimensioning = true;
    
    theSession.Preferences.Sketch.DimensionLabel = NXOpen.Preferences.SketchPreferences.DimensionLabelType.Expression;
    
    theSession.Preferences.Sketch.TextSizeFixed = true;
    
    theSession.Preferences.Sketch.FixedTextSize = 3.0;
    
    theSession.Preferences.Sketch.ConstraintSymbolSize = 3.0;
    
    theSession.Preferences.Sketch.DisplayObjectColor = false;
    
    theSession.Preferences.Sketch.DisplayObjectName = true;
    
    NXOpen.NXObject nXObject2;
    nXObject2 = sketchInPlaceBuilder1.Commit();
    
    NXOpen.Sketch sketch1 = (NXOpen.Sketch)nXObject2;
    NXOpen.Features.Feature feature1;
    feature1 = sketch1.Feature;
    
    NXOpen.Session.UndoMarkId markId9;
    markId9 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "update");
    
    int nErrs1;
    nErrs1 = theSession.UpdateManager.DoUpdate(markId9);
    
    sketch1.Activate(NXOpen.Sketch.ViewReorient.True);
    
    theSession.DeleteUndoMark(markId8, null);
    
    theSession.SetUndoMarkName(markId6, "Create Sketch");
    
    sketchInPlaceBuilder1.Destroy();
    
    sketchAlongPathBuilder1.Destroy();
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression2);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression1);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Line...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId10;
    markId10 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId11;
    markId11 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression3;
    expression3 = workPart.Expressions.CreateSystemExpression(length_string);
    
    theSession.SetUndoMarkVisibility(markId11, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint1 = new NXOpen.Point3d(0.0, 0.0, 0.0);
    NXOpen.Point3d endPoint1 = new NXOpen.Point3d(length, 0.0, 0.0);
    NXOpen.Line line1;
    line1 = workPart.Curves.CreateLine(startPoint1, endPoint1);
    
    theSession.ActiveSketch.AddGeometry(line1, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_1 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_1.Geometry = line1;
    geom1_1.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_1.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_1 = new NXOpen.Sketch.ConstraintGeometry();
    NXOpen.Features.DatumCsys datumCsys2 = (NXOpen.Features.DatumCsys)workPart.Features.FindObject("SKETCH(1:1B)");
    NXOpen.Point point3 = (NXOpen.Point)datumCsys2.FindObject("POINT 1");
    geom2_1.Geometry = point3;
    geom2_1.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom2_1.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint1;
    sketchGeometricConstraint1 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_1, geom2_1);
    
    NXOpen.Sketch.ConstraintGeometry geom1 = new NXOpen.Sketch.ConstraintGeometry();
    geom1.Geometry = line1;
    geom1.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom1.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint2;
    sketchGeometricConstraint2 = theSession.ActiveSketch.CreateHorizontalConstraint(geom1);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_1 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_1.Geometry = line1;
    dimObject1_1.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_1.AssocValue = 0;
    dimObject1_1.HelpPoint.X = 0.0;
    dimObject1_1.HelpPoint.Y = 0.0;
    dimObject1_1.HelpPoint.Z = 0.0;
    NXOpen.NXObject nullNXOpen_NXObject = null;
    dimObject1_1.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_1 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_1.Geometry = line1;
    dimObject2_1.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_1.AssocValue = 0;
    dimObject2_1.HelpPoint.X = 0.0;
    dimObject2_1.HelpPoint.Y = 0.0;
    dimObject2_1.HelpPoint.Z = 0.0;
    dimObject2_1.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin1 = new NXOpen.Point3d(45.0, -9.0598069411704, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint1;
    sketchDimensionalConstraint1 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_1, dimObject2_1, dimOrigin1, expression3, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint1 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint1;
    NXOpen.Annotations.Dimension dimension1;
    dimension1 = sketchHelpedDimensionalConstraint1.AssociatedDimension;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId12;
    markId12 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression4;
    expression4 = workPart.Expressions.CreateSystemExpression(heightA_string);
    
    NXOpen.Expression expression5;
    expression5 = workPart.Expressions.CreateSystemExpression(length_string);
    
    workPart.Expressions.Edit(expression4, heightA_string);
    
    theSession.SetUndoMarkVisibility(markId12, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint2 = new NXOpen.Point3d(length, 0.0, 0.0);
    NXOpen.Point3d endPoint2 = new NXOpen.Point3d(length, heightA, 0.0);
    NXOpen.Line line2;
    line2 = workPart.Curves.CreateLine(startPoint2, endPoint2);
    
    theSession.ActiveSketch.AddGeometry(line2, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_2 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_2.Geometry = line2;
    geom1_2.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_2.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_2 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_2.Geometry = line1;
    geom2_2.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_2.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint3;
    sketchGeometricConstraint3 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_2, geom2_2);
    
    NXOpen.Sketch.ConstraintGeometry geom2 = new NXOpen.Sketch.ConstraintGeometry();
    geom2.Geometry = line2;
    geom2.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom2.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint4;
    sketchGeometricConstraint4 = theSession.ActiveSketch.CreateVerticalConstraint(geom2);
    
    workPart.Expressions.Delete(expression5);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_2 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_2.Geometry = line2;
    dimObject1_2.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_2.AssocValue = 0;
    dimObject1_2.HelpPoint.X = 0.0;
    dimObject1_2.HelpPoint.Y = 0.0;
    dimObject1_2.HelpPoint.Z = 0.0;
    dimObject1_2.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_2 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_2.Geometry = line2;
    dimObject2_2.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_2.AssocValue = 0;
    dimObject2_2.HelpPoint.X = 0.0;
    dimObject2_2.HelpPoint.Y = 0.0;
    dimObject2_2.HelpPoint.Z = 0.0;
    dimObject2_2.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin2 = new NXOpen.Point3d(99.0598069411704, 15.0, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint2;
    sketchDimensionalConstraint2 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_2, dimObject2_2, dimOrigin2, expression4, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint2 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint2;
    NXOpen.Annotations.Dimension dimension2;
    dimension2 = sketchHelpedDimensionalConstraint2.AssociatedDimension;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId13;
    markId13 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression6;
    expression6 = workPart.Expressions.CreateSystemExpression(heightB_string);
    
    NXOpen.Expression expression7;
    expression7 = workPart.Expressions.CreateSystemExpression(length_string);
    
    theSession.SetUndoMarkVisibility(markId13, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint3 = new NXOpen.Point3d(0.0, 0.0, 0.0);
    NXOpen.Point3d endPoint3 = new NXOpen.Point3d(7.02873218749514e-014, heightB, 0.0);
    NXOpen.Line line3;
    line3 = workPart.Curves.CreateLine(startPoint3, endPoint3);
    
    theSession.ActiveSketch.AddGeometry(line3, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_3 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_3.Geometry = line3;
    geom1_3.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_3.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_3 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_3.Geometry = line1;
    geom2_3.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_3.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint5;
    sketchGeometricConstraint5 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_3, geom2_3);
    
    NXOpen.Sketch.ConstraintGeometry geom3 = new NXOpen.Sketch.ConstraintGeometry();
    geom3.Geometry = line3;
    geom3.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom3.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint6;
    sketchGeometricConstraint6 = theSession.ActiveSketch.CreateVerticalConstraint(geom3);
    
    workPart.Expressions.Delete(expression7);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_3 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_3.Geometry = line3;
    dimObject1_3.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_3.AssocValue = 0;
    dimObject1_3.HelpPoint.X = 0.0;
    dimObject1_3.HelpPoint.Y = 0.0;
    dimObject1_3.HelpPoint.Z = 0.0;
    dimObject1_3.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_3 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_3.Geometry = line3;
    dimObject2_3.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_3.AssocValue = 0;
    dimObject2_3.HelpPoint.X = 0.0;
    dimObject2_3.HelpPoint.Y = 0.0;
    dimObject2_3.HelpPoint.Z = 0.0;
    dimObject2_3.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin3 = new NXOpen.Point3d(9.05980694117043, heightA, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint3;
    sketchDimensionalConstraint3 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_3, dimObject2_3, dimOrigin3, expression6, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint3 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint3;
    NXOpen.Annotations.Dimension dimension3;
    dimension3 = sketchHelpedDimensionalConstraint3.AssociatedDimension;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId14;
    markId14 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.SetUndoMarkVisibility(markId14, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint4 = new NXOpen.Point3d(0.0, heightB, 0.0);
    NXOpen.Point3d endPoint4 = new NXOpen.Point3d(length, heightA, 0.0);
    //NXOpen.Point3d endPoint4 = new NXOpen.Point3d(90.0, 29.9999999999998, 0.0);
    NXOpen.Line line4;
    line4 = workPart.Curves.CreateLine(startPoint4, endPoint4);
    
    theSession.ActiveSketch.AddGeometry(line4, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_4 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_4.Geometry = line4;
    geom1_4.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_4.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_4 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_4.Geometry = line3;
    geom2_4.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_4.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint7;
    sketchGeometricConstraint7 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_4, geom2_4);
    
    NXOpen.Sketch.ConstraintGeometry geom1_5 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_5.Geometry = line4;
    geom1_5.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_5.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_5 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_5.Geometry = line2;
    geom2_5.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_5.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint8;
    sketchGeometricConstraint8 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_5, geom2_5);
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: File->Finish Sketch
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId15;
    markId15 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Deactivate Sketch");
    
    theSession.ActiveSketch.Deactivate(NXOpen.Sketch.ViewReorient.False, NXOpen.Sketch.UpdateLevel.Model);
    
    // ----------------------------------------------
    //   Menu: Insert->Design Feature->Extrude...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId16;
    markId16 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.Features.Feature nullNXOpen_Features_Feature = null;
    
    if ( !workPart.Preferences.Modeling.GetHistoryMode() )
    {
        throw new Exception("Create or edit of a Feature was recorded in History Mode but playback is in History-Free Mode.");
    }
    
    NXOpen.Features.ExtrudeBuilder extrudeBuilder1;
    extrudeBuilder1 = workPart.Features.CreateExtrudeBuilder(nullNXOpen_Features_Feature);
    
    NXOpen.Section section1;
    section1 = workPart.Sections.CreateSection(0.0095, 0.01, 0.5);
    
    extrudeBuilder1.Section = section1;
    
    extrudeBuilder1.AllowSelfIntersectingSection(true);
    
    NXOpen.Unit unit2;
    unit2 = extrudeBuilder1.Draft.FrontDraftAngle.Units;
    
    NXOpen.Expression expression8;
    expression8 = workPart.Expressions.CreateSystemExpressionWithUnits("2.00", unit2);
    
    extrudeBuilder1.DistanceTolerance = 0.01;
    
    extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
    
    NXOpen.Body[] targetBodies1 = new NXOpen.Body[1];
    NXOpen.Body nullNXOpen_Body = null;
    targetBodies1[0] = nullNXOpen_Body;
    extrudeBuilder1.BooleanOperation.SetTargetBodies(targetBodies1);
    
    extrudeBuilder1.Limits.StartExtend.Value.RightHandSide = "0";
    
    extrudeBuilder1.Limits.EndExtend.Value.RightHandSide = "25";
    
    extrudeBuilder1.Offset.StartOffset.RightHandSide = "0";
    
    extrudeBuilder1.Offset.EndOffset.RightHandSide = "5";
    
    extrudeBuilder1.Limits.StartExtend.Value.RightHandSide = "0";
    
    extrudeBuilder1.Limits.EndExtend.Value.RightHandSide = thickness_string;
    
    extrudeBuilder1.Draft.FrontDraftAngle.RightHandSide = "2";
    
    extrudeBuilder1.Draft.BackDraftAngle.RightHandSide = "2";
    
    extrudeBuilder1.Offset.StartOffset.RightHandSide = "0";
    
    extrudeBuilder1.Offset.EndOffset.RightHandSide = "5";
    
    NXOpen.GeometricUtilities.SmartVolumeProfileBuilder smartVolumeProfileBuilder1;
    smartVolumeProfileBuilder1 = extrudeBuilder1.SmartVolumeProfile;
    
    smartVolumeProfileBuilder1.OpenProfileSmartVolumeOption = false;
    
    smartVolumeProfileBuilder1.CloseProfileRule = NXOpen.GeometricUtilities.SmartVolumeProfileBuilder.CloseProfileRuleType.Fci;
    
    theSession.SetUndoMarkName(markId16, "Extrude Dialog");
    
    section1.DistanceTolerance = 0.01;
    
    section1.ChainingTolerance = 0.0095;
    
    section1.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.OnlyCurves);
    
    NXOpen.Session.UndoMarkId markId17;
    markId17 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "section mark");
    
    NXOpen.Session.UndoMarkId markId18;
    markId18 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, null);
    
    NXOpen.Features.Feature[] features1 = new NXOpen.Features.Feature[1];
    NXOpen.Features.SketchFeature sketchFeature1 = (NXOpen.Features.SketchFeature)feature1;
    features1[0] = sketchFeature1;
    NXOpen.CurveFeatureRule curveFeatureRule1;
    curveFeatureRule1 = workPart.ScRuleFactory.CreateRuleCurveFeature(features1);
    
    section1.AllowSelfIntersection(true);
    
    NXOpen.SelectionIntentRule[] rules1 = new NXOpen.SelectionIntentRule[1];
    rules1[0] = curveFeatureRule1;
    NXOpen.Point3d helpPoint1 = new NXOpen.Point3d(53.8762063337749, 42.0412645554082, 0.0);
    section1.AddToSection(rules1, line4, nullNXOpen_NXObject, nullNXOpen_NXObject, helpPoint1, NXOpen.Section.Mode.Create, false);
    
    theSession.DeleteUndoMark(markId18, null);
    
    NXOpen.Direction direction1;
    direction1 = workPart.Directions.CreateDirection(sketch1, NXOpen.Sense.Forward, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    extrudeBuilder1.Direction = direction1;
    
    theSession.DeleteUndoMark(markId17, null);
    
    NXOpen.Session.UndoMarkId markId19;
    markId19 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    theSession.DeleteUndoMark(markId19, null);
    
    NXOpen.Session.UndoMarkId markId20;
    markId20 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    extrudeBuilder1.ParentFeatureInternal = false;
    
    NXOpen.Features.Feature feature2;
    feature2 = extrudeBuilder1.CommitFeature();
    
    theSession.DeleteUndoMark(markId20, null);
    
    theSession.SetUndoMarkName(markId16, "Extrude");
    
    NXOpen.Expression expression9 = extrudeBuilder1.Limits.StartExtend.Value;
    NXOpen.Expression expression10 = extrudeBuilder1.Limits.EndExtend.Value;
    extrudeBuilder1.Destroy();
    
    workPart.Expressions.Delete(expression8);
    
    // ----------------------------------------------
    //   Menu: File->Save
    // ----------------------------------------------
    NXOpen.PartSaveStatus partSaveStatus1;
    partSaveStatus1 = workPart.Save(NXOpen.BasePart.SaveComponents.True, NXOpen.BasePart.CloseAfterSave.False);
    
    partSaveStatus1.Dispose();
    // ----------------------------------------------
    //   Menu: File->Export->IGES...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId21;
    markId21 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.IgesCreator igesCreator1;
    igesCreator1 = theSession.DexManager.CreateIgesCreator();
    
    igesCreator1.ExportModelData = true;
    
    igesCreator1.ExportDrawings = true;
    
    igesCreator1.MapTabCylToBSurf = true;
    
    igesCreator1.BcurveTol = 0.0508;
    
    igesCreator1.IdenticalPointResolution = 0.001;
    
    igesCreator1.MaxThreeDMdlSpace = 10000.0;
    
    igesCreator1.ObjectTypes.Curves = true;
    
    igesCreator1.ObjectTypes.Surfaces = true;
    
    igesCreator1.ObjectTypes.Annotations = true;
    
    igesCreator1.ObjectTypes.Structures = true;
    
    igesCreator1.ObjectTypes.Solids = true;
    
    igesCreator1.OutputFile = part_name_igs;
    
    igesCreator1.SettingsFile = "C:\\Program Files\\Siemens\\NX 10.0\\iges\\igesexport.def";
    
    igesCreator1.MaxLineThickness = 2.0;
    
    igesCreator1.SysDefmaxThreeDMdlSpace = true;
    
    igesCreator1.SysDefidenticalPointResolution = true;
    
    igesCreator1.InputFile = part_name_prt;
    
    igesCreator1.OutputFile = part_name_igs;
    
    theSession.SetUndoMarkName(markId21, "Export to IGES Options Dialog");
    
    igesCreator1.OutputFile = part_name_igs;
    
    NXOpen.Session.UndoMarkId markId22;
    markId22 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Export to IGES Options");
    
    theSession.DeleteUndoMark(markId22, null);
    
    NXOpen.Session.UndoMarkId markId23;
    markId23 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Export to IGES Options");
    
    igesCreator1.FileSaveFlag = false;
    
    igesCreator1.LayerMask = "1-256";
    
    igesCreator1.DrawingList = "";
    
    igesCreator1.ViewList = "Top,Front,Right,Back,Bottom,Left,Isometric,Trimetric,User Defined";
    
    NXOpen.NXObject nXObject3;
    nXObject3 = igesCreator1.Commit();
    
    theSession.DeleteUndoMark(markId23, null);
    
    theSession.SetUndoMarkName(markId21, "Export to IGES Options");
    
    igesCreator1.Destroy();
    
    // ----------------------------------------------
    //   Menu: Tools->Journal->Stop Recording
    // ----------------------------------------------

    Console.WriteLine("Finishing NX Golden Cantilever Journal");
  }
  public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}
