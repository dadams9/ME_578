// NX 9.0.3.4
// Journal created by dadams26 on Mon Mar 05 14:00:19 2018 Mountain Standard Time
//Creates a beam that has been optimized to minimize volume and not exceed a stress of 
//30000 pascals when treated as a cantilever
using System;
using NXOpen;
using System.IO;

public class NXJournal
{
  public static void Main(string[] args)
  {
      
      Console.WriteLine("Starting NX Journal");

    //Declare Part Variables
    //Convert Strings to doubles from the args
    double length = System.Convert.ToDouble(args[0]);
    double width = System.Convert.ToDouble(args[1]);
    double height = System.Convert.ToDouble(args[2]);
    string part_name = args[3];

    //Measurements need to be converted from m to mm
    length = 1000 * length;
    width = 1000 * width;
    height = 1000 * height;


    //Convert necessary values to string.
    string length_string = System.Convert.ToString(length);
    string width_string = System.Convert.ToString(width);
    string height_string = System.Convert.ToString(height);


        Session theSession = Session.GetSession();
    // ----------------------------------------------
    //   Menu: File->New...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId1;
    markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    FileNew fileNew1;
    fileNew1 = theSession.Parts.FileNew();
    
    theSession.SetUndoMarkName(markId1, "New Dialog");
    
    NXOpen.Session.UndoMarkId markId2;
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "New");
    
    theSession.DeleteUndoMark(markId2, null);
    
    NXOpen.Session.UndoMarkId markId3;
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "New");
    
    fileNew1.TemplateFileName = "model-plain-1-mm-template.prt";
    
    fileNew1.ApplicationName = "ModelTemplate";
    
    fileNew1.Units = NXOpen.Part.Units.Millimeters;
    
    fileNew1.RelationType = "";
    
    fileNew1.UsesMasterModel = "No";
    
    fileNew1.TemplateType = FileNewTemplateType.Item;

    fileNew1.NewFileName = part_name;
    
    fileNew1.MasterFileName = "";
    
    fileNew1.UseBlankTemplate = false;
    
    fileNew1.MakeDisplayedPart = true;
    
    NXObject nXObject1;
    nXObject1 = fileNew1.Commit();
    
    Part workPart = theSession.Parts.Work;
    Part displayPart = theSession.Parts.Display;
    theSession.DeleteUndoMark(markId3, null);
    
    fileNew1.Destroy();
    
    NXOpen.Session.UndoMarkId markId4;
    markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Enter Modeling");
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId5;
    markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    Sketch nullSketch = null;
    SketchInPlaceBuilder sketchInPlaceBuilder1;
    sketchInPlaceBuilder1 = workPart.Sketches.CreateNewSketchInPlaceBuilder(nullSketch);
    
    Unit unit1 = (Unit)workPart.UnitCollection.FindObject("MilliMeter");
    Expression expression1;
    expression1 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    Expression expression2;
    expression2 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    SketchAlongPathBuilder sketchAlongPathBuilder1;
    sketchAlongPathBuilder1 = workPart.Sketches.CreateSketchAlongPathBuilder(nullSketch);
    
    sketchAlongPathBuilder1.PlaneLocation.Expression.RightHandSide = "0";
    
    theSession.SetUndoMarkName(markId5, "Create Sketch Dialog");
    
    DatumPlane datumPlane1 = (DatumPlane)workPart.Datums.FindObject("DATUM_CSYS(0) XY plane");
    Point3d point1 = new Point3d(26.3626531592029, 1.06581410364015e-014, 30.438226401797);
    sketchInPlaceBuilder1.PlaneOrFace.SetValue(datumPlane1, workPart.ModelingViews.WorkView, point1);
    
    NXOpen.Features.DatumCsys datumCsys1 = (NXOpen.Features.DatumCsys)workPart.Features.FindObject("DATUM_CSYS(0)");
    Point point2 = (Point)datumCsys1.FindObject("POINT 1");
    sketchInPlaceBuilder1.SketchOrigin = point2;
    
    sketchInPlaceBuilder1.PlaneOrFace.Value = null;
    
    sketchInPlaceBuilder1.PlaneOrFace.Value = datumPlane1;
    
    sketchInPlaceBuilder1.ReversePlaneNormal = true;
    
    DatumAxis datumAxis1 = (DatumAxis)workPart.Datums.FindObject("DATUM_CSYS(0) X axis");
    sketchInPlaceBuilder1.Axis.Value = datumAxis1;
    
    NXOpen.Session.UndoMarkId markId6;
    markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Sketch");
    
    theSession.DeleteUndoMark(markId6, null);
    
    NXOpen.Session.UndoMarkId markId7;
    markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Sketch");
    
    theSession.Preferences.Sketch.CreateInferredConstraints = true;
    
    theSession.Preferences.Sketch.ContinuousAutoDimensioning = true;
    
    theSession.Preferences.Sketch.DimensionLabel = NXOpen.Preferences.SketchPreferences.DimensionLabelType.Expression;
    
    theSession.Preferences.Sketch.TextSizeFixed = true;
    
    theSession.Preferences.Sketch.FixedTextSize = 3.0;
    
    theSession.Preferences.Sketch.ConstraintSymbolSize = 3.0;
    
    theSession.Preferences.Sketch.DisplayObjectColor = false;
    
    theSession.Preferences.Sketch.DisplayObjectName = true;
    
    NXObject nXObject2;
    nXObject2 = sketchInPlaceBuilder1.Commit();
    
    Sketch sketch1 = (Sketch)nXObject2;
    NXOpen.Features.Feature feature1;
    feature1 = sketch1.Feature;
    
    NXOpen.Session.UndoMarkId markId8;
    markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "update");
    
    int nErrs1;
    nErrs1 = theSession.UpdateManager.DoUpdate(markId8);
    
    sketch1.Activate(NXOpen.Sketch.ViewReorient.True);
    
    theSession.DeleteUndoMark(markId7, null);
    
    theSession.SetUndoMarkName(markId5, "Create Sketch");
    
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
    //   Menu: Insert->Sketch Curve->Rectangle...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId9;
    markId9 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId10;
    markId10 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Rectangle");
    
    Expression expression3;
    expression3 = workPart.Expressions.CreateSystemExpression(width_string);
    
    theSession.SetUndoMarkVisibility(markId10, "Create Rectangle", NXOpen.Session.MarkVisibility.Visible);
    
    // ----------------------------------------------
    // Creating rectangle using By 2 Points method 
    // ----------------------------------------------
    Point3d startPoint1 = new Point3d(0.0, 0.0, 0.0);
    Point3d endPoint1 = new Point3d(width, 0.0, 0.0);
    Line line1;
    line1 = workPart.Curves.CreateLine(startPoint1, endPoint1);
    
    Point3d startPoint2 = new Point3d(width, 0.0, 0.0);
    Point3d endPoint2 = new Point3d(width, 0.0, height);
    Line line2;
    line2 = workPart.Curves.CreateLine(startPoint2, endPoint2);
    
    Point3d startPoint3 = new Point3d(width, 0.0, height);
    Point3d endPoint3 = new Point3d(0.0, 0.0, height);
    Line line3;
    line3 = workPart.Curves.CreateLine(startPoint3, endPoint3);
    
    Point3d startPoint4 = new Point3d(0.0, 0.0, height);
    Point3d endPoint4 = new Point3d(0.0, 0.0, 0.0);
    Line line4;
    line4 = workPart.Curves.CreateLine(startPoint4, endPoint4);
    
    theSession.ActiveSketch.AddGeometry(line1, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    theSession.ActiveSketch.AddGeometry(line2, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    theSession.ActiveSketch.AddGeometry(line3, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    theSession.ActiveSketch.AddGeometry(line4, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_1;
    geom1_1.Geometry = line1;
    geom1_1.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_1.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_1;
    geom2_1.Geometry = line2;
    geom2_1.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_1.SplineDefiningPointIndex = 0;
    SketchGeometricConstraint sketchGeometricConstraint1;
    sketchGeometricConstraint1 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_1, geom2_1);
    
    NXOpen.Sketch.ConstraintGeometry geom1_2;
    geom1_2.Geometry = line2;
    geom1_2.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_2.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_2;
    geom2_2.Geometry = line3;
    geom2_2.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_2.SplineDefiningPointIndex = 0;
    SketchGeometricConstraint sketchGeometricConstraint2;
    sketchGeometricConstraint2 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_2, geom2_2);
    
    NXOpen.Sketch.ConstraintGeometry geom1_3;
    geom1_3.Geometry = line3;
    geom1_3.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_3.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_3;
    geom2_3.Geometry = line4;
    geom2_3.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_3.SplineDefiningPointIndex = 0;
    SketchGeometricConstraint sketchGeometricConstraint3;
    sketchGeometricConstraint3 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_3, geom2_3);
    
    NXOpen.Sketch.ConstraintGeometry geom1_4;
    geom1_4.Geometry = line4;
    geom1_4.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_4.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_4;
    geom2_4.Geometry = line1;
    geom2_4.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_4.SplineDefiningPointIndex = 0;
    SketchGeometricConstraint sketchGeometricConstraint4;
    sketchGeometricConstraint4 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_4, geom2_4);
    
    NXOpen.Sketch.ConstraintGeometry geom1;
    geom1.Geometry = line1;
    geom1.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom1.SplineDefiningPointIndex = 0;
    SketchGeometricConstraint sketchGeometricConstraint5;
    sketchGeometricConstraint5 = theSession.ActiveSketch.CreateHorizontalConstraint(geom1);
    
    NXOpen.Sketch.ConstraintGeometry conGeom1_1;
    conGeom1_1.Geometry = line1;
    conGeom1_1.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom1_1.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry conGeom2_1;
    conGeom2_1.Geometry = line2;
    conGeom2_1.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom2_1.SplineDefiningPointIndex = 0;
    SketchGeometricConstraint sketchGeometricConstraint6;
    sketchGeometricConstraint6 = theSession.ActiveSketch.CreatePerpendicularConstraint(conGeom1_1, conGeom2_1);
    
    NXOpen.Sketch.ConstraintGeometry conGeom1_2;
    conGeom1_2.Geometry = line2;
    conGeom1_2.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom1_2.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry conGeom2_2;
    conGeom2_2.Geometry = line3;
    conGeom2_2.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom2_2.SplineDefiningPointIndex = 0;
    SketchGeometricConstraint sketchGeometricConstraint7;
    sketchGeometricConstraint7 = theSession.ActiveSketch.CreatePerpendicularConstraint(conGeom1_2, conGeom2_2);
    
    NXOpen.Sketch.ConstraintGeometry conGeom1_3;
    conGeom1_3.Geometry = line3;
    conGeom1_3.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom1_3.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry conGeom2_3;
    conGeom2_3.Geometry = line4;
    conGeom2_3.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom2_3.SplineDefiningPointIndex = 0;
    SketchGeometricConstraint sketchGeometricConstraint8;
    sketchGeometricConstraint8 = theSession.ActiveSketch.CreatePerpendicularConstraint(conGeom1_3, conGeom2_3);
    
    NXOpen.Sketch.ConstraintGeometry conGeom1_4;
    conGeom1_4.Geometry = line4;
    conGeom1_4.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom1_4.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry conGeom2_4;
    conGeom2_4.Geometry = line1;
    conGeom2_4.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom2_4.SplineDefiningPointIndex = 0;
    SketchGeometricConstraint sketchGeometricConstraint9;
    sketchGeometricConstraint9 = theSession.ActiveSketch.CreatePerpendicularConstraint(conGeom1_4, conGeom2_4);
    
    NXOpen.Sketch.ConstraintGeometry geom1_5;
    geom1_5.Geometry = line1;
    geom1_5.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_5.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_5;
    geom2_5.Geometry = point2;
    geom2_5.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom2_5.SplineDefiningPointIndex = 0;
    SketchGeometricConstraint sketchGeometricConstraint10;
    sketchGeometricConstraint10 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_5, geom2_5);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_1;
    dimObject1_1.Geometry = line3;
    dimObject1_1.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_1.AssocValue = 0;
    dimObject1_1.HelpPoint.X = 0.0;
    dimObject1_1.HelpPoint.Y = 0.0;
    dimObject1_1.HelpPoint.Z = 0.0;
    NXObject nullNXObject = null;
    dimObject1_1.View = nullNXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_1;
    dimObject2_1.Geometry = line3;
    dimObject2_1.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_1.AssocValue = 0;
    dimObject2_1.HelpPoint.X = 0.0;
    dimObject2_1.HelpPoint.Y = 0.0;
    dimObject2_1.HelpPoint.Z = 0.0;
    dimObject2_1.View = nullNXObject;
    Point3d dimOrigin1 = new Point3d(25.0, 0.0, 149.651881900218);
    SketchDimensionalConstraint sketchDimensionalConstraint1;
    sketchDimensionalConstraint1 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_1, dimObject2_1, dimOrigin1, expression3, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint1 = (SketchHelpedDimensionalConstraint)sketchDimensionalConstraint1;
    NXOpen.Annotations.Dimension dimension1;
    dimension1 = sketchHelpedDimensionalConstraint1.AssociatedDimension;
    
    NXOpen.Sketch.DimensionGeometry dimObject1_2;
    dimObject1_2.Geometry = line2;
    dimObject1_2.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_2.AssocValue = 0;
    dimObject1_2.HelpPoint.X = 0.0;
    dimObject1_2.HelpPoint.Y = 0.0;
    dimObject1_2.HelpPoint.Z = 0.0;
    dimObject1_2.View = nullNXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_2;
    dimObject2_2.Geometry = line2;
    dimObject2_2.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_2.AssocValue = 0;
    dimObject2_2.HelpPoint.X = 0.0;
    dimObject2_2.HelpPoint.Y = 0.0;
    dimObject2_2.HelpPoint.Z = 0.0;
    dimObject2_2.View = nullNXObject;
    Point3d dimOrigin2 = new Point3d(74.651881900218, 0.0, 62.5);
    Expression nullExpression = null;
    SketchDimensionalConstraint sketchDimensionalConstraint2;
    sketchDimensionalConstraint2 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_2, dimObject2_2, dimOrigin2, nullExpression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint2 = (SketchHelpedDimensionalConstraint)sketchDimensionalConstraint2;
    NXOpen.Annotations.Dimension dimension2;
    dimension2 = sketchHelpedDimensionalConstraint2.AssociatedDimension;
    
    Expression expression4;
    expression4 = sketchHelpedDimensionalConstraint2.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    SmartObject[] geoms1 = new SmartObject[4];
    geoms1[0] = line1;
    geoms1[1] = line2;
    geoms1[2] = line3;
    geoms1[3] = line4;
    theSession.ActiveSketch.UpdateConstraintDisplay(geoms1);
    
    SmartObject[] geoms2 = new SmartObject[4];
    geoms2[0] = line1;
    geoms2[1] = line2;
    geoms2[2] = line3;
    geoms2[3] = line4;
    theSession.ActiveSketch.UpdateDimensionDisplay(geoms2);
    
    // ----------------------------------------------
    //   Menu: File->Finish Sketch
    // ----------------------------------------------
    theSession.ActiveSketch.Deactivate(NXOpen.Sketch.ViewReorient.False, NXOpen.Sketch.UpdateLevel.Model);
    
    // ----------------------------------------------
    //   Menu: Insert->Design Feature->Extrude...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId11;
    markId11 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.Features.Feature nullFeatures_Feature = null;
    
    if ( !workPart.Preferences.Modeling.GetHistoryMode() )
    {
        throw new Exception("Create or edit of a Feature was recorded in History Mode but playback is in History-Free Mode.");
    }
    
    NXOpen.Features.ExtrudeBuilder extrudeBuilder1;
    extrudeBuilder1 = workPart.Features.CreateExtrudeBuilder(nullFeatures_Feature);
    
    Section section1;
    section1 = workPart.Sections.CreateSection(0.0095, 0.01, 0.5);
    
    extrudeBuilder1.Section = section1;
    
    extrudeBuilder1.AllowSelfIntersectingSection(true);
    
    Unit unit2;
    unit2 = extrudeBuilder1.Draft.FrontDraftAngle.Units;
    
    Expression expression5;
    expression5 = workPart.Expressions.CreateSystemExpressionWithUnits("2.00", unit2);
    
    extrudeBuilder1.DistanceTolerance = 0.01;
    
    extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
    
    Body[] targetBodies1 = new Body[1];
    Body nullBody = null;
    targetBodies1[0] = nullBody;
    extrudeBuilder1.BooleanOperation.SetTargetBodies(targetBodies1);
    
    extrudeBuilder1.Limits.StartExtend.Value.RightHandSide = "0";
    
    extrudeBuilder1.Limits.EndExtend.Value.RightHandSide = "25";
    
    extrudeBuilder1.Offset.StartOffset.RightHandSide = "0";
    
    extrudeBuilder1.Offset.EndOffset.RightHandSide = "5";
    
    extrudeBuilder1.Limits.StartExtend.Value.RightHandSide = "0";
    
    extrudeBuilder1.Limits.EndExtend.Value.RightHandSide = length_string;
    
    extrudeBuilder1.Draft.FrontDraftAngle.RightHandSide = "2";
    
    extrudeBuilder1.Draft.BackDraftAngle.RightHandSide = "2";
    
    extrudeBuilder1.Offset.StartOffset.RightHandSide = "0";
    
    extrudeBuilder1.Offset.EndOffset.RightHandSide = "5";
    
    NXOpen.GeometricUtilities.SmartVolumeProfileBuilder smartVolumeProfileBuilder1;
    smartVolumeProfileBuilder1 = extrudeBuilder1.SmartVolumeProfile;
    
    smartVolumeProfileBuilder1.OpenProfileSmartVolumeOption = false;
    
    smartVolumeProfileBuilder1.CloseProfileRule = NXOpen.GeometricUtilities.SmartVolumeProfileBuilder.CloseProfileRuleType.Fci;
    
    theSession.SetUndoMarkName(markId11, "Extrude Dialog");
    
    section1.DistanceTolerance = 0.01;
    
    section1.ChainingTolerance = 0.0095;
    
    section1.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.OnlyCurves);
    
    NXOpen.Session.UndoMarkId markId12;
    markId12 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "section mark");
    
    NXOpen.Session.UndoMarkId markId13;
    markId13 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, null);
    
    NXOpen.Features.Feature[] features1 = new NXOpen.Features.Feature[1];
    NXOpen.Features.SketchFeature sketchFeature1 = (NXOpen.Features.SketchFeature)feature1;
    features1[0] = sketchFeature1;
    CurveFeatureRule curveFeatureRule1;
    curveFeatureRule1 = workPart.ScRuleFactory.CreateRuleCurveFeature(features1);
    
    section1.AllowSelfIntersection(true);
    
    SelectionIntentRule[] rules1 = new SelectionIntentRule[1];
    rules1[0] = curveFeatureRule1;
    Point3d helpPoint1 = new Point3d(23.553389477581, 0.0, height);
    section1.AddToSection(rules1, line3, nullNXObject, nullNXObject, helpPoint1, NXOpen.Section.Mode.Create, false);
    
    theSession.DeleteUndoMark(markId13, null);
    
    Direction direction1;
    direction1 = workPart.Directions.CreateDirection(sketch1, Sense.Forward, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    extrudeBuilder1.Direction = direction1;
    
    theSession.DeleteUndoMark(markId12, null);
    
    NXOpen.Session.UndoMarkId markId14;
    markId14 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    theSession.DeleteUndoMark(markId14, null);
    
    NXOpen.Session.UndoMarkId markId15;
    markId15 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    extrudeBuilder1.ParentFeatureInternal = false;
    
    NXOpen.Features.Feature feature2;
    feature2 = extrudeBuilder1.CommitFeature();
    
    theSession.DeleteUndoMark(markId15, null);
    
    theSession.SetUndoMarkName(markId11, "Extrude");
    
    Expression expression6 = extrudeBuilder1.Limits.StartExtend.Value;
    Expression expression7 = extrudeBuilder1.Limits.EndExtend.Value;
    extrudeBuilder1.Destroy();
    
    workPart.Expressions.Delete(expression5);
    
    // ----------------------------------------------
    //   Menu: Analysis->Measure Bodies...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId16;
    markId16 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    MeasureBodyBuilder measureBodyBuilder1;
    measureBodyBuilder1 = workPart.MeasureManager.CreateMeasureBodyBuilder(nullNXObject);
    
    theSession.SetUndoMarkName(markId16, "Measure Bodies Dialog");
    
    Body[] bodies1 = new Body[1];
    Body body1 = (Body)workPart.Bodies.FindObject("EXTRUDE(2)");
    bodies1[0] = body1;
    BodyDumbRule bodyDumbRule1;
    bodyDumbRule1 = workPart.ScRuleFactory.CreateRuleBodyDumb(bodies1, true);
    
    SelectionIntentRule[] rules2 = new SelectionIntentRule[1];
    rules2[0] = bodyDumbRule1;
    measureBodyBuilder1.BodyCollector.ReplaceRules(rules2, false);
    
    NXOpen.Session.UndoMarkId markId17;
    markId17 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Measure Bodies");
    
    theSession.DeleteUndoMark(markId17, null);
    
    NXOpen.Session.UndoMarkId markId18;
    markId18 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Measure Bodies");
    
    theSession.DeleteUndoMark(markId18, null);
    
    theSession.SetUndoMarkName(markId16, "Measure Bodies");
    
    measureBodyBuilder1.Destroy();


    MeasureBodies measureBodies1 = workPart.MeasureManager.NewMassProperties(workPart.UnitCollection.ToArray(), 1.0, bodies1);
    //Console.WriteLine(measureBodies1.Volume);

    // ----------------------------------------------
    //   Menu: File->Save
    // ----------------------------------------------
    PartSaveStatus partSaveStatus1;
    partSaveStatus1 = workPart.Save(NXOpen.BasePart.SaveComponents.True, NXOpen.BasePart.CloseAfterSave.False);
    
    partSaveStatus1.Dispose();
    // ----------------------------------------------
    //   Menu: Tools->Journal->Stop Recording
    // ----------------------------------------------


    Console.WriteLine("Finishing NX Journal. Part Saved.");

    //Write output file with dimensions of beam in current directory
    File.WriteAllText("Optimum_beam_dimensions.txt",
                       "Volume of Optimal Beam = " + ((measureBodies1.Volume)/1000000000) + "m^3\r\nLength = 2m" +
                       "\r\nWidth = " + (width/1000) +"m\r\nHeight = " +(height/1000)+"m");
    Console.WriteLine("Beam dimensions written to file.");

  }
  public static int GetUnloadOption(string dummy) { return (int)Session.LibraryUnloadOption.Immediately; }
}

