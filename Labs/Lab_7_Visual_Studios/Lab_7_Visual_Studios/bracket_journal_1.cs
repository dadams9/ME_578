// NX 10.0.0.24
// Journal created by dadams26 on Fri Mar 09 10:41:08 2018 Mountain Standard Time
//
using System;
using NXOpen;

public class NXJournal
{
    //private double _radius;
    //public NXJournal(double radius)
    //{
    //    _radius = radius;
    //}
    double mBase_thickness = 10;
    double mBase_length = 70;
    double mBack_thickness = 10;
    double mBack_height = 40;
    double mFillet_radius = 6;
 
    public void SetBracketDimensions(double bt, double bl, double bkt, double bkh, double fr)
    {
        mBase_thickness = bt;
        mBase_length = bl;
        mBack_thickness = bkt;
        mBack_height = bkh;
        mFillet_radius = fr;
    }
  public void CreateBracket()
  {
    
    NXOpen.Session theSession = NXOpen.Session.GetSession();
    NXOpen.Part workPart = theSession.Parts.Work;
    NXOpen.Part displayPart = theSession.Parts.Display;

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
    
    theSession.SetUndoMarkName(markId6, "Create Sketch Dialog");
    
    NXOpen.DatumPlane datumPlane1 = (NXOpen.DatumPlane)workPart.Datums.FindObject("DATUM_CSYS(0) YZ plane");
    NXOpen.Point3d point1 = new NXOpen.Point3d(8.50466575404331, 8.5046657540433, 2.77555756156289e-016);
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
    
    theSession.SetUndoMarkVisibility(markId11, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint1 = new NXOpen.Point3d(0.0, 0.0, 0.0);
    NXOpen.Point3d endPoint1 = new NXOpen.Point3d(32.0, 0.0, 0.0);
    NXOpen.Line line1;
    line1 = workPart.Curves.CreateLine(startPoint1, endPoint1);
    
    theSession.ActiveSketch.AddGeometry(line1, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_1 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_1.Geometry = line1;
    geom1_1.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_1.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_1 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_1.Geometry = point2;
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
    NXOpen.Point3d dimOrigin1 = new NXOpen.Point3d(16.0, -6.8879182709912, 0.0);
    NXOpen.Expression nullNXOpen_Expression = null;
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint1;
    sketchDimensionalConstraint1 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_1, dimObject2_1, dimOrigin1, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint1 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint1;
    NXOpen.Annotations.Dimension dimension1;
    dimension1 = sketchHelpedDimensionalConstraint1.AssociatedDimension;
    
    NXOpen.Expression expression3;
    expression3 = sketchHelpedDimensionalConstraint1.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId12;
    markId12 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.SetUndoMarkVisibility(markId12, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint2 = new NXOpen.Point3d(0.0, 0.0, 0.0);
    NXOpen.Point3d endPoint2 = new NXOpen.Point3d(6.09156789582913e-014, 52.0, 0.0);
    NXOpen.Line line2;
    line2 = workPart.Curves.CreateLine(startPoint2, endPoint2);
    
    theSession.ActiveSketch.AddGeometry(line2, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_2 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_2.Geometry = line2;
    geom1_2.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_2.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_2 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_2.Geometry = line1;
    geom2_2.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_2.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint3;
    sketchGeometricConstraint3 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_2, geom2_2);
    
    NXOpen.Sketch.ConstraintGeometry geom2 = new NXOpen.Sketch.ConstraintGeometry();
    geom2.Geometry = line2;
    geom2.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom2.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint4;
    sketchGeometricConstraint4 = theSession.ActiveSketch.CreateVerticalConstraint(geom2);
    
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
    NXOpen.Point3d dimOrigin2 = new NXOpen.Point3d(6.88791827099123, 26.0, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint2;
    sketchDimensionalConstraint2 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_2, dimObject2_2, dimOrigin2, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint2 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint2;
    NXOpen.Annotations.Dimension dimension2;
    dimension2 = sketchHelpedDimensionalConstraint2.AssociatedDimension;
    
    NXOpen.Expression expression4;
    expression4 = sketchHelpedDimensionalConstraint2.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId13;
    markId13 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.SetUndoMarkVisibility(markId13, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint3 = new NXOpen.Point3d(0.0, 52.0, 0.0);
    NXOpen.Point3d endPoint3 = new NXOpen.Point3d(32.0, 52.0, 0.0);
    NXOpen.Line line3;
    line3 = workPart.Curves.CreateLine(startPoint3, endPoint3);
    
    theSession.ActiveSketch.AddGeometry(line3, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_3 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_3.Geometry = line3;
    geom1_3.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_3.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_3 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_3.Geometry = line2;
    geom2_3.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_3.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint5;
    sketchGeometricConstraint5 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_3, geom2_3);
    
    NXOpen.Sketch.ConstraintGeometry geom3 = new NXOpen.Sketch.ConstraintGeometry();
    geom3.Geometry = line3;
    geom3.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom3.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint6;
    sketchGeometricConstraint6 = theSession.ActiveSketch.CreateHorizontalConstraint(geom3);
    
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
    NXOpen.Point3d dimOrigin3 = new NXOpen.Point3d(16.0, 45.1120817290088, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint3;
    sketchDimensionalConstraint3 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_3, dimObject2_3, dimOrigin3, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint3 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint3;
    NXOpen.Annotations.Dimension dimension3;
    dimension3 = sketchHelpedDimensionalConstraint3.AssociatedDimension;
    
    NXOpen.Expression expression5;
    expression5 = sketchHelpedDimensionalConstraint3.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Arc...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId14;
    markId14 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId15;
    markId15 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression6;
    expression6 = workPart.Expressions.CreateSystemExpression("12");
    
    theSession.DeleteUndoMark(markId15, "Curve");
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Arc...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId16;
    markId16 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId17;
    markId17 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression7;
    expression7 = workPart.Expressions.CreateSystemExpression("12");
    
    theSession.DeleteUndoMark(markId17, "Curve");
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Line...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId18;
    markId18 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId19;
    markId19 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression8;
    expression8 = workPart.Expressions.CreateSystemExpression("12");
    
    theSession.SetUndoMarkVisibility(markId19, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint4 = new NXOpen.Point3d(32.0, 52.0, 0.0);
    NXOpen.Point3d endPoint4 = new NXOpen.Point3d(32.0, 40.0, 0.0);
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
    
    NXOpen.Sketch.ConstraintGeometry geom4 = new NXOpen.Sketch.ConstraintGeometry();
    geom4.Geometry = line4;
    geom4.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom4.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint8;
    sketchGeometricConstraint8 = theSession.ActiveSketch.CreateVerticalConstraint(geom4);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_4 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_4.Geometry = line4;
    dimObject1_4.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_4.AssocValue = 0;
    dimObject1_4.HelpPoint.X = 0.0;
    dimObject1_4.HelpPoint.Y = 0.0;
    dimObject1_4.HelpPoint.Z = 0.0;
    dimObject1_4.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_4 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_4.Geometry = line4;
    dimObject2_4.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_4.AssocValue = 0;
    dimObject2_4.HelpPoint.X = 0.0;
    dimObject2_4.HelpPoint.Y = 0.0;
    dimObject2_4.HelpPoint.Z = 0.0;
    dimObject2_4.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin4 = new NXOpen.Point3d(25.1120817290088, 46.0, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint4;
    sketchDimensionalConstraint4 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_4, dimObject2_4, dimOrigin4, expression8, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint4 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint4;
    NXOpen.Annotations.Dimension dimension4;
    dimension4 = sketchHelpedDimensionalConstraint4.AssociatedDimension;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId20;
    markId20 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.SetUndoMarkVisibility(markId20, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint5 = new NXOpen.Point3d(32.0, 40.0, 0.0);
    NXOpen.Point3d endPoint5 = new NXOpen.Point3d(88.0, 40.0, 0.0);
    NXOpen.Line line5;
    line5 = workPart.Curves.CreateLine(startPoint5, endPoint5);
    
    theSession.ActiveSketch.AddGeometry(line5, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_5 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_5.Geometry = line5;
    geom1_5.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_5.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_5 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_5.Geometry = line4;
    geom2_5.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_5.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint9;
    sketchGeometricConstraint9 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_5, geom2_5);
    
    NXOpen.Sketch.ConstraintGeometry geom5 = new NXOpen.Sketch.ConstraintGeometry();
    geom5.Geometry = line5;
    geom5.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom5.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint10;
    sketchGeometricConstraint10 = theSession.ActiveSketch.CreateHorizontalConstraint(geom5);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_5 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_5.Geometry = line5;
    dimObject1_5.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_5.AssocValue = 0;
    dimObject1_5.HelpPoint.X = 0.0;
    dimObject1_5.HelpPoint.Y = 0.0;
    dimObject1_5.HelpPoint.Z = 0.0;
    dimObject1_5.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_5 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_5.Geometry = line5;
    dimObject2_5.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_5.AssocValue = 0;
    dimObject2_5.HelpPoint.X = 0.0;
    dimObject2_5.HelpPoint.Y = 0.0;
    dimObject2_5.HelpPoint.Z = 0.0;
    dimObject2_5.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin5 = new NXOpen.Point3d(60.0, 33.1120817290088, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint5;
    sketchDimensionalConstraint5 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_5, dimObject2_5, dimOrigin5, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint5 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint5;
    NXOpen.Annotations.Dimension dimension5;
    dimension5 = sketchHelpedDimensionalConstraint5.AssociatedDimension;
    
    NXOpen.Expression expression9;
    expression9 = sketchHelpedDimensionalConstraint5.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId21;
    markId21 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.SetUndoMarkVisibility(markId21, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint6 = new NXOpen.Point3d(32.0, 0.0, 0.0);
    NXOpen.Point3d endPoint6 = new NXOpen.Point3d(32.0, 12.0, 0.0);
    NXOpen.Line line6;
    line6 = workPart.Curves.CreateLine(startPoint6, endPoint6);
    
    theSession.ActiveSketch.AddGeometry(line6, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_6 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_6.Geometry = line6;
    geom1_6.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_6.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_6 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_6.Geometry = line1;
    geom2_6.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_6.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint11;
    sketchGeometricConstraint11 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_6, geom2_6);
    
    NXOpen.Sketch.ConstraintGeometry geom6 = new NXOpen.Sketch.ConstraintGeometry();
    geom6.Geometry = line6;
    geom6.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom6.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint12;
    sketchGeometricConstraint12 = theSession.ActiveSketch.CreateVerticalConstraint(geom6);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_6 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_6.Geometry = line6;
    dimObject1_6.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_6.AssocValue = 0;
    dimObject1_6.HelpPoint.X = 0.0;
    dimObject1_6.HelpPoint.Y = 0.0;
    dimObject1_6.HelpPoint.Z = 0.0;
    dimObject1_6.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_6 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_6.Geometry = line6;
    dimObject2_6.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_6.AssocValue = 0;
    dimObject2_6.HelpPoint.X = 0.0;
    dimObject2_6.HelpPoint.Y = 0.0;
    dimObject2_6.HelpPoint.Z = 0.0;
    dimObject2_6.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin6 = new NXOpen.Point3d(38.8879182709912, 5.99999999999999, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint6;
    sketchDimensionalConstraint6 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_6, dimObject2_6, dimOrigin6, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint6 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint6;
    NXOpen.Annotations.Dimension dimension6;
    dimension6 = sketchHelpedDimensionalConstraint6.AssociatedDimension;
    
    NXOpen.Expression expression10;
    expression10 = sketchHelpedDimensionalConstraint6.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId22;
    markId22 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.SetUndoMarkVisibility(markId22, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint7 = new NXOpen.Point3d(32.0, 12.0, 0.0);
    NXOpen.Point3d endPoint7 = new NXOpen.Point3d(88.0, 12.0, 0.0);
    NXOpen.Line line7;
    line7 = workPart.Curves.CreateLine(startPoint7, endPoint7);
    
    theSession.ActiveSketch.AddGeometry(line7, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_7 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_7.Geometry = line7;
    geom1_7.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_7.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_7 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_7.Geometry = line6;
    geom2_7.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_7.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint13;
    sketchGeometricConstraint13 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_7, geom2_7);
    
    NXOpen.Sketch.ConstraintGeometry geom7 = new NXOpen.Sketch.ConstraintGeometry();
    geom7.Geometry = line7;
    geom7.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom7.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint14;
    sketchGeometricConstraint14 = theSession.ActiveSketch.CreateHorizontalConstraint(geom7);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_7 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_7.Geometry = line7;
    dimObject1_7.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_7.AssocValue = 0;
    dimObject1_7.HelpPoint.X = 0.0;
    dimObject1_7.HelpPoint.Y = 0.0;
    dimObject1_7.HelpPoint.Z = 0.0;
    dimObject1_7.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_7 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_7.Geometry = line7;
    dimObject2_7.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_7.AssocValue = 0;
    dimObject2_7.HelpPoint.X = 0.0;
    dimObject2_7.HelpPoint.Y = 0.0;
    dimObject2_7.HelpPoint.Z = 0.0;
    dimObject2_7.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin7 = new NXOpen.Point3d(60.0, 5.1120817290088, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint7;
    sketchDimensionalConstraint7 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_7, dimObject2_7, dimOrigin7, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint7 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint7;
    NXOpen.Annotations.Dimension dimension7;
    dimension7 = sketchHelpedDimensionalConstraint7.AssociatedDimension;
    
    NXOpen.Expression expression11;
    expression11 = sketchHelpedDimensionalConstraint7.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Arc...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId23;
    markId23 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId24;
    markId24 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression12;
    expression12 = workPart.Expressions.CreateSystemExpression("14");
    
    workPart.Expressions.Edit(expression12, "14");
    
    theSession.SetUndoMarkVisibility(markId24, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.NXMatrix nXMatrix1;
    nXMatrix1 = theSession.ActiveSketch.Orientation;
    
    NXOpen.Point3d center1 = new NXOpen.Point3d(88.0, 26.0, 0.0);
    NXOpen.Arc arc1;
    arc1 = workPart.Curves.CreateArc(center1, nXMatrix1, 14.0, ( 270.0 * Math.PI/180.0 ), ( 450.0 * Math.PI/180.0 ));
    
    theSession.ActiveSketch.AddGeometry(arc1, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_8 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_8.Geometry = arc1;
    geom1_8.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_8.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_8 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_8.Geometry = line5;
    geom2_8.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_8.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint15;
    sketchGeometricConstraint15 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_8, geom2_8);
    
    NXOpen.Sketch.ConstraintGeometry geom1_9 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_9.Geometry = arc1;
    geom1_9.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_9.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_9 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_9.Geometry = line7;
    geom2_9.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_9.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint16;
    sketchGeometricConstraint16 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_9, geom2_9);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_8 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_8.Geometry = arc1;
    dimObject1_8.AssocType = NXOpen.Sketch.AssocType.Midpoint;
    dimObject1_8.AssocValue = 0;
    dimObject1_8.HelpPoint.X = 0.0;
    dimObject1_8.HelpPoint.Y = 0.0;
    dimObject1_8.HelpPoint.Z = 0.0;
    dimObject1_8.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin8 = new NXOpen.Point3d(94.5546491141781, 29.3627467776917, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint8;
    sketchDimensionalConstraint8 = theSession.ActiveSketch.CreateRadialDimension(dimObject1_8, dimOrigin8, expression12, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.Annotations.Dimension dimension8;
    dimension8 = sketchDimensionalConstraint8.AssociatedDimension;
    
    theSession.ActiveSketch.Update();
    
    // ----------------------------------------------
    //   Dialog Begin Arc
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId25;
    markId25 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression13;
    expression13 = workPart.Expressions.CreateSystemExpression("12");
    
    theSession.DeleteUndoMark(markId25, "Curve");
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Arc...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId26;
    markId26 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId27;
    markId27 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression14;
    expression14 = workPart.Expressions.CreateSystemExpression("12");
    
    theSession.SetUndoMarkVisibility(markId27, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.NXMatrix nXMatrix2;
    nXMatrix2 = theSession.ActiveSketch.Orientation;
    
    NXOpen.Point3d center2 = new NXOpen.Point3d(44.0, 52.0, 0.0);
    NXOpen.Arc arc2;
    arc2 = workPart.Curves.CreateArc(center2, nXMatrix2, 12.0, ( 180.0 * Math.PI/180.0 ), ( 270.0 * Math.PI/180.0 ));
    
    theSession.ActiveSketch.AddGeometry(arc2, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_10 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_10.Geometry = arc2;
    geom1_10.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_10.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_10 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_10.Geometry = line3;
    geom2_10.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_10.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint17;
    sketchGeometricConstraint17 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_10, geom2_10);
    
    NXOpen.Sketch.ConstraintGeometry geom1_11 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_11.Geometry = arc2;
    geom1_11.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom1_11.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometryHelp geom1Help1 = new NXOpen.Sketch.ConstraintGeometryHelp();
    geom1Help1.Type = NXOpen.Sketch.ConstraintGeometryHelpType.Point;
    geom1Help1.Point.X = 43.9983696696565;
    geom1Help1.Point.Y = 40.0;
    geom1Help1.Point.Z = 0.0;
    geom1Help1.Parameter = 0.0;
    NXOpen.Sketch.ConstraintGeometry geom2_11 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_11.Geometry = line5;
    geom2_11.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom2_11.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometryHelp geom2Help1 = new NXOpen.Sketch.ConstraintGeometryHelp();
    geom2Help1.Type = NXOpen.Sketch.ConstraintGeometryHelpType.Point;
    geom2Help1.Point.X = 43.9983696696565;
    geom2Help1.Point.Y = 40.0;
    geom2Help1.Point.Z = 0.0;
    geom2Help1.Parameter = 0.0;
    NXOpen.SketchTangentConstraint sketchTangentConstraint1;
    sketchTangentConstraint1 = theSession.ActiveSketch.CreateTangentConstraint(geom1_11, geom1Help1, geom2_11, geom2Help1);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_9 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_9.Geometry = arc2;
    dimObject1_9.AssocType = NXOpen.Sketch.AssocType.Midpoint;
    dimObject1_9.AssocValue = 0;
    dimObject1_9.HelpPoint.X = 0.0;
    dimObject1_9.HelpPoint.Y = 0.0;
    dimObject1_9.HelpPoint.Z = 0.0;
    dimObject1_9.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin9 = new NXOpen.Point3d(41.8491830912946, 45.9464491803488, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint9;
    sketchDimensionalConstraint9 = theSession.ActiveSketch.CreateRadialDimension(dimObject1_9, dimOrigin9, expression14, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.Annotations.Dimension dimension9;
    dimension9 = sketchDimensionalConstraint9.AssociatedDimension;
    
    theSession.ActiveSketch.Update();
    
    // ----------------------------------------------
    //   Dialog Begin Arc
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: Edit->Undo List->1 Curve
    // ----------------------------------------------
    bool marksRecycled1;
    bool undoUnavailable1;
    theSession.UndoLastNVisibleMarks(1, out marksRecycled1, out undoUnavailable1);
    
    theSession.DeleteUndoMark(markId26, "Curve");
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Fillet...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId28;
    markId28 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId29;
    markId29 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "##40Fillet");
    
    NXOpen.Point3d helpPoint1_1 = new NXOpen.Point3d(32.0, 52.0, 0.0);
    NXOpen.Point3d helpPoint2_1 = new NXOpen.Point3d(44.0, 40.0, 0.0);
    NXOpen.Arc[] fillets1;
    NXOpen.SketchConstraint[] constraints1;
    fillets1 = theSession.ActiveSketch.Fillet(line4, line5, helpPoint1_1, helpPoint2_1, 12.0, NXOpen.Sketch.TrimInputOption.True, NXOpen.Sketch.CreateDimensionOption.True, NXOpen.Sketch.AlternateSolutionOption.False, out constraints1);
    
    theSession.ActiveSketch.Update();
    
    NXOpen.Session.UndoMarkId markId30;
    markId30 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "##40Fillet");
    
    NXOpen.Point3d helpPoint1_2 = new NXOpen.Point3d(44.0, 12.0, 0.0);
    NXOpen.Point3d helpPoint2_2 = new NXOpen.Point3d(32.0, 1.21277171953783e-014, 0.0);
    NXOpen.Arc[] fillets2;
    NXOpen.SketchConstraint[] constraints2;
    fillets2 = theSession.ActiveSketch.Fillet(line7, line6, helpPoint1_2, helpPoint2_2, 12.0, NXOpen.Sketch.TrimInputOption.True, NXOpen.Sketch.CreateDimensionOption.True, NXOpen.Sketch.AlternateSolutionOption.False, out constraints2);
    
    theSession.ActiveSketch.Update();
    
    // ----------------------------------------------
    //   Menu: Edit->Delete...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId31;
    markId31 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Delete");
    
    bool notifyOnDelete1;
    notifyOnDelete1 = theSession.Preferences.Modeling.NotifyOnDelete;
    
    theSession.UpdateManager.ClearErrorList();
    
    NXOpen.Session.UndoMarkId markId32;
    markId32 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Delete");
    
    NXOpen.NXObject[] objects1 = new NXOpen.NXObject[2];
    objects1[0] = line4;
    objects1[1] = sketchGeometricConstraint8;
    int nErrs2;
    nErrs2 = theSession.UpdateManager.AddToDeleteList(objects1);
    
    bool notifyOnDelete2;
    notifyOnDelete2 = theSession.Preferences.Modeling.NotifyOnDelete;
    
    int nErrs3;
    nErrs3 = theSession.UpdateManager.DoUpdate(markId32);
    
    theSession.DeleteUndoMark(markId31, null);
    
    // ----------------------------------------------
    //   Menu: Edit->Delete...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId33;
    markId33 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Delete");
    
    bool notifyOnDelete3;
    notifyOnDelete3 = theSession.Preferences.Modeling.NotifyOnDelete;
    
    theSession.UpdateManager.ClearErrorList();
    
    NXOpen.Session.UndoMarkId markId34;
    markId34 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Delete");
    
    NXOpen.NXObject[] objects2 = new NXOpen.NXObject[1];
    objects2[0] = line6;
    int nErrs4;
    nErrs4 = theSession.UpdateManager.AddToDeleteList(objects2);
    
    bool notifyOnDelete4;
    notifyOnDelete4 = theSession.Preferences.Modeling.NotifyOnDelete;
    
    int nErrs5;
    nErrs5 = theSession.UpdateManager.DoUpdate(markId34);
    
    theSession.DeleteUndoMark(markId33, null);
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Circle...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId35;
    markId35 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId36;
    markId36 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression15;
    expression15 = workPart.Expressions.CreateSystemExpression("12");
    
    theSession.SetUndoMarkVisibility(markId36, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.NXMatrix nXMatrix3;
    nXMatrix3 = theSession.ActiveSketch.Orientation;
    
    NXOpen.Point3d center3 = new NXOpen.Point3d(19.8442200927677, 11.5420463804873, 0.0);
    NXOpen.Arc arc3;
    arc3 = workPart.Curves.CreateArc(center3, nXMatrix3, 6.0, 0.0, ( 360.0 * Math.PI/180.0 ));
    
    theSession.ActiveSketch.AddGeometry(arc3, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_10 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_10.Geometry = arc3;
    dimObject1_10.AssocType = NXOpen.Sketch.AssocType.Midpoint;
    dimObject1_10.AssocValue = 0;
    dimObject1_10.HelpPoint.X = 0.0;
    dimObject1_10.HelpPoint.Y = 0.0;
    dimObject1_10.HelpPoint.Z = 0.0;
    dimObject1_10.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin10 = new NXOpen.Point3d(19.8442200927677, 13.8380191374844, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint10;
    sketchDimensionalConstraint10 = theSession.ActiveSketch.CreateDiameterDimension(dimObject1_10, dimOrigin10, expression15, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.Annotations.Dimension dimension10;
    dimension10 = sketchDimensionalConstraint10.AssociatedDimension;
    
    theSession.ActiveSketch.Update();
    
    NXOpen.Session.UndoMarkId markId37;
    markId37 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.DeleteUndoMark(markId37, "Curve");
    
    // ----------------------------------------------
    //   Menu: Edit->Snap Point->Arc Center
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: Edit->Snap Point->Arc Center
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Circle...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId38;
    markId38 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId39;
    markId39 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression16;
    expression16 = workPart.Expressions.CreateSystemExpression("12");
    
    theSession.SetUndoMarkVisibility(markId39, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.NXMatrix nXMatrix4;
    nXMatrix4 = theSession.ActiveSketch.Orientation;
    
    NXOpen.Point3d center4 = new NXOpen.Point3d(19.6417280510048, 40.0934242690613, 0.0);
    NXOpen.Arc arc4;
    arc4 = workPart.Curves.CreateArc(center4, nXMatrix4, 6.0, 0.0, ( 360.0 * Math.PI/180.0 ));
    
    theSession.ActiveSketch.AddGeometry(arc4, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_11 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_11.Geometry = arc4;
    dimObject1_11.AssocType = NXOpen.Sketch.AssocType.Midpoint;
    dimObject1_11.AssocValue = 0;
    dimObject1_11.HelpPoint.X = 0.0;
    dimObject1_11.HelpPoint.Y = 0.0;
    dimObject1_11.HelpPoint.Z = 0.0;
    dimObject1_11.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin11 = new NXOpen.Point3d(19.6417280510048, 42.3893970260584, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint11;
    sketchDimensionalConstraint11 = theSession.ActiveSketch.CreateDiameterDimension(dimObject1_11, dimOrigin11, expression16, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.Annotations.Dimension dimension11;
    dimension11 = sketchDimensionalConstraint11.AssociatedDimension;
    
    theSession.ActiveSketch.Update();
    
    NXOpen.Session.UndoMarkId markId40;
    markId40 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Line...
    // ----------------------------------------------
    theSession.DeleteUndoMark(markId40, "Curve");
    
    NXOpen.Session.UndoMarkId markId41;
    markId41 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId42;
    markId42 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.SetUndoMarkVisibility(markId42, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint8 = new NXOpen.Point3d(88.0, 20.0, 0.0);
    NXOpen.Point3d endPoint8 = new NXOpen.Point3d(44.0, 20.0, 0.0);
    NXOpen.Line line8;
    line8 = workPart.Curves.CreateLine(startPoint8, endPoint8);
    
    theSession.ActiveSketch.AddGeometry(line8, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom8 = new NXOpen.Sketch.ConstraintGeometry();
    geom8.Geometry = line8;
    geom8.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom8.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint18;
    sketchGeometricConstraint18 = theSession.ActiveSketch.CreateHorizontalConstraint(geom8);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_12 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_12.Geometry = line8;
    dimObject1_12.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_12.AssocValue = 0;
    dimObject1_12.HelpPoint.X = 0.0;
    dimObject1_12.HelpPoint.Y = 0.0;
    dimObject1_12.HelpPoint.Z = 0.0;
    dimObject1_12.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_8 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_8.Geometry = line8;
    dimObject2_8.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_8.AssocValue = 0;
    dimObject2_8.HelpPoint.X = 0.0;
    dimObject2_8.HelpPoint.Y = 0.0;
    dimObject2_8.HelpPoint.Z = 0.0;
    dimObject2_8.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin12 = new NXOpen.Point3d(66.0, 26.8879182709912, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint12;
    sketchDimensionalConstraint12 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_12, dimObject2_8, dimOrigin12, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint8 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint12;
    NXOpen.Annotations.Dimension dimension12;
    dimension12 = sketchHelpedDimensionalConstraint8.AssociatedDimension;
    
    NXOpen.Expression expression17;
    expression17 = sketchHelpedDimensionalConstraint8.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId43;
    markId43 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.SetUndoMarkVisibility(markId43, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint9 = new NXOpen.Point3d(88.0, 32.0, 0.0);
    NXOpen.Point3d endPoint9 = new NXOpen.Point3d(44.0, 32.0, 0.0);
    NXOpen.Line line9;
    line9 = workPart.Curves.CreateLine(startPoint9, endPoint9);
    
    theSession.ActiveSketch.AddGeometry(line9, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom9 = new NXOpen.Sketch.ConstraintGeometry();
    geom9.Geometry = line9;
    geom9.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom9.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint19;
    sketchGeometricConstraint19 = theSession.ActiveSketch.CreateHorizontalConstraint(geom9);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_13 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_13.Geometry = line9;
    dimObject1_13.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_13.AssocValue = 0;
    dimObject1_13.HelpPoint.X = 0.0;
    dimObject1_13.HelpPoint.Y = 0.0;
    dimObject1_13.HelpPoint.Z = 0.0;
    dimObject1_13.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_9 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_9.Geometry = line9;
    dimObject2_9.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_9.AssocValue = 0;
    dimObject2_9.HelpPoint.X = 0.0;
    dimObject2_9.HelpPoint.Y = 0.0;
    dimObject2_9.HelpPoint.Z = 0.0;
    dimObject2_9.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin13 = new NXOpen.Point3d(66.0, 38.8879182709912, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint13;
    sketchDimensionalConstraint13 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_13, dimObject2_9, dimOrigin13, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint9 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint13;
    NXOpen.Annotations.Dimension dimension13;
    dimension13 = sketchHelpedDimensionalConstraint9.AssociatedDimension;
    
    NXOpen.Expression expression18;
    expression18 = sketchHelpedDimensionalConstraint9.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Arc...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId44;
    markId44 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId45;
    markId45 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression19;
    expression19 = workPart.Expressions.CreateSystemExpression("6");
    
    theSession.SetUndoMarkVisibility(markId45, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.NXMatrix nXMatrix5;
    nXMatrix5 = theSession.ActiveSketch.Orientation;
    
    NXOpen.Point3d center5 = new NXOpen.Point3d(44.0, 26.0, 0.0);
    NXOpen.Arc arc5;
    arc5 = workPart.Curves.CreateArc(center5, nXMatrix5, 6.0, ( 90.0 * Math.PI/180.0 ), ( 270.0 * Math.PI/180.0 ));
    
    theSession.ActiveSketch.AddGeometry(arc5, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_12 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_12.Geometry = arc5;
    geom1_12.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_12.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_12 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_12.Geometry = line9;
    geom2_12.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_12.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint20;
    sketchGeometricConstraint20 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_12, geom2_12);
    
    NXOpen.Sketch.ConstraintGeometry geom1_13 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_13.Geometry = arc5;
    geom1_13.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_13.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_13 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_13.Geometry = line8;
    geom2_13.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_13.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint21;
    sketchGeometricConstraint21 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_13, geom2_13);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_14 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_14.Geometry = arc5;
    dimObject1_14.AssocType = NXOpen.Sketch.AssocType.Midpoint;
    dimObject1_14.AssocValue = 0;
    dimObject1_14.HelpPoint.X = 0.0;
    dimObject1_14.HelpPoint.Y = 0.0;
    dimObject1_14.HelpPoint.Z = 0.0;
    dimObject1_14.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin14 = new NXOpen.Point3d(41.3961042482024, 23.2629910824692, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint14;
    sketchDimensionalConstraint14 = theSession.ActiveSketch.CreateRadialDimension(dimObject1_14, dimOrigin14, expression19, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.Annotations.Dimension dimension14;
    dimension14 = sketchDimensionalConstraint14.AssociatedDimension;
    
    theSession.ActiveSketch.Update();
    
    // ----------------------------------------------
    //   Dialog Begin Arc
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId46;
    markId46 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression20;
    expression20 = workPart.Expressions.CreateSystemExpression("6");
    
    theSession.SetUndoMarkVisibility(markId46, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.NXMatrix nXMatrix6;
    nXMatrix6 = theSession.ActiveSketch.Orientation;
    
    NXOpen.Point3d center6 = new NXOpen.Point3d(88.0, 26.0, 0.0);
    NXOpen.Arc arc6;
    arc6 = workPart.Curves.CreateArc(center6, nXMatrix6, 6.0, ( 270.0 * Math.PI/180.0 ), ( 450.0 * Math.PI/180.0 ));
    
    theSession.ActiveSketch.AddGeometry(arc6, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_14 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_14.Geometry = arc6;
    geom1_14.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_14.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_14 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_14.Geometry = line9;
    geom2_14.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_14.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint22;
    sketchGeometricConstraint22 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_14, geom2_14);
    
    NXOpen.Sketch.ConstraintGeometry geom1_15 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_15.Geometry = arc6;
    geom1_15.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_15.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_15 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_15.Geometry = line8;
    geom2_15.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_15.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint23;
    sketchGeometricConstraint23 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_15, geom2_15);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_15 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_15.Geometry = arc6;
    dimObject1_15.AssocType = NXOpen.Sketch.AssocType.Midpoint;
    dimObject1_15.AssocValue = 0;
    dimObject1_15.HelpPoint.X = 0.0;
    dimObject1_15.HelpPoint.Y = 0.0;
    dimObject1_15.HelpPoint.Z = 0.0;
    dimObject1_15.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin15 = new NXOpen.Point3d(90.6038957517976, 28.7370089175308, 0.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint15;
    sketchDimensionalConstraint15 = theSession.ActiveSketch.CreateRadialDimension(dimObject1_15, dimOrigin15, expression20, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.Annotations.Dimension dimension15;
    dimension15 = sketchDimensionalConstraint15.AssociatedDimension;
    
    theSession.ActiveSketch.Update();
    
    // ----------------------------------------------
    //   Dialog Begin Arc
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: File->Finish Sketch
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId47;
    markId47 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Deactivate Sketch");
    
    theSession.ActiveSketch.Deactivate(NXOpen.Sketch.ViewReorient.False, NXOpen.Sketch.UpdateLevel.Model);
    
    // ----------------------------------------------
    //   Menu: Insert->Design Feature->Extrude...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId48;
    markId48 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
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
    
    NXOpen.Expression expression21;
    expression21 = workPart.Expressions.CreateSystemExpressionWithUnits("2.00", unit2);
    
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
    
    NXOpen.GeometricUtilities.SmartVolumeProfileBuilder smartVolumeProfileBuilder1;
    smartVolumeProfileBuilder1 = extrudeBuilder1.SmartVolumeProfile;
    
    smartVolumeProfileBuilder1.OpenProfileSmartVolumeOption = false;
    
    smartVolumeProfileBuilder1.CloseProfileRule = NXOpen.GeometricUtilities.SmartVolumeProfileBuilder.CloseProfileRuleType.Fci;
    
    theSession.SetUndoMarkName(markId48, "Extrude Dialog");
    
    extrudeBuilder1.Limits.StartExtend.Value.RightHandSide = "0";
    
    extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
    
    NXOpen.Body[] targetBodies2 = new NXOpen.Body[1];
    targetBodies2[0] = nullNXOpen_Body;
    extrudeBuilder1.BooleanOperation.SetTargetBodies(targetBodies2);
    
    extrudeBuilder1.Limits.EndExtend.Value.RightHandSide = "25";
    
    extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
    
    NXOpen.Body[] targetBodies3 = new NXOpen.Body[1];
    targetBodies3[0] = nullNXOpen_Body;
    extrudeBuilder1.BooleanOperation.SetTargetBodies(targetBodies3);
    
    extrudeBuilder1.Draft.FrontDraftAngle.RightHandSide = "2";
    
    NXOpen.Expression expression22;
    expression22 = extrudeBuilder1.Draft.FrontDraftAngle;
    
    expression22.RightHandSide = "2";
    
    extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
    
    NXOpen.Body[] targetBodies4 = new NXOpen.Body[1];
    targetBodies4[0] = nullNXOpen_Body;
    extrudeBuilder1.BooleanOperation.SetTargetBodies(targetBodies4);
    
    extrudeBuilder1.Draft.BackDraftAngle.RightHandSide = "2";
    
    NXOpen.Expression expression23;
    expression23 = extrudeBuilder1.Draft.BackDraftAngle;
    
    expression23.RightHandSide = "2";
    
    extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
    
    NXOpen.Body[] targetBodies5 = new NXOpen.Body[1];
    targetBodies5[0] = nullNXOpen_Body;
    extrudeBuilder1.BooleanOperation.SetTargetBodies(targetBodies5);
    
    extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
    
    NXOpen.Body[] targetBodies6 = new NXOpen.Body[1];
    targetBodies6[0] = nullNXOpen_Body;
    extrudeBuilder1.BooleanOperation.SetTargetBodies(targetBodies6);
    
    extrudeBuilder1.Offset.StartOffset.RightHandSide = "0";
    
    NXOpen.Expression expression24;
    expression24 = extrudeBuilder1.Offset.StartOffset;
    
    expression24.RightHandSide = "0";
    
    extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
    
    NXOpen.Body[] targetBodies7 = new NXOpen.Body[1];
    targetBodies7[0] = nullNXOpen_Body;
    extrudeBuilder1.BooleanOperation.SetTargetBodies(targetBodies7);
    
    extrudeBuilder1.Offset.EndOffset.RightHandSide = "5";
    
    NXOpen.Expression expression25;
    expression25 = extrudeBuilder1.Offset.EndOffset;
    
    expression25.RightHandSide = "5";
    
    extrudeBuilder1.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
    
    NXOpen.Body[] targetBodies8 = new NXOpen.Body[1];
    targetBodies8[0] = nullNXOpen_Body;
    extrudeBuilder1.BooleanOperation.SetTargetBodies(targetBodies8);
    
    section1.DistanceTolerance = 0.01;
    
    section1.ChainingTolerance = 0.0095;
    
    section1.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.OnlyCurves);
    
    NXOpen.Session.UndoMarkId markId49;
    markId49 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "section mark");
    
    NXOpen.Session.UndoMarkId markId50;
    markId50 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, null);
    
    NXOpen.Features.Feature[] features1 = new NXOpen.Features.Feature[1];
    NXOpen.Features.SketchFeature sketchFeature1 = (NXOpen.Features.SketchFeature)feature1;
    features1[0] = sketchFeature1;
    NXOpen.CurveFeatureRule curveFeatureRule1;
    curveFeatureRule1 = workPart.ScRuleFactory.CreateRuleCurveFeature(features1);
    
    section1.AllowSelfIntersection(true);
    
    NXOpen.SelectionIntentRule[] rules1 = new NXOpen.SelectionIntentRule[1];
    rules1[0] = curveFeatureRule1;
    NXOpen.Point3d helpPoint1 = new NXOpen.Point3d(29.5638380973886, 52.0, 0.0);
    section1.AddToSection(rules1, line3, nullNXOpen_NXObject, nullNXOpen_NXObject, helpPoint1, NXOpen.Section.Mode.Create, false);
    
    theSession.DeleteUndoMark(markId50, null);
    
    NXOpen.Direction direction1;
    direction1 = workPart.Directions.CreateDirection(sketch1, NXOpen.Sense.Forward, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    extrudeBuilder1.Direction = direction1;
    
    theSession.DeleteUndoMark(markId49, null);
    
    extrudeBuilder1.Limits.EndExtend.Value.RightHandSide = "40";
    
    NXOpen.Session.UndoMarkId markId51;
    markId51 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    theSession.DeleteUndoMark(markId51, null);
    
    NXOpen.Session.UndoMarkId markId52;
    markId52 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    extrudeBuilder1.ParentFeatureInternal = false;
    
    NXOpen.Features.Feature feature2;
    feature2 = extrudeBuilder1.CommitFeature();
    
    theSession.DeleteUndoMark(markId52, null);
    
    theSession.SetUndoMarkName(markId48, "Extrude");
    
    NXOpen.Expression expression26 = extrudeBuilder1.Limits.StartExtend.Value;
    NXOpen.Expression expression27 = extrudeBuilder1.Limits.EndExtend.Value;
    extrudeBuilder1.Destroy();
    
    workPart.Expressions.Delete(expression21);
    
    // ----------------------------------------------
    //   Menu: Orient View->Front
    // ----------------------------------------------
    workPart.ModelingViews.WorkView.Orient(NXOpen.View.Canned.Front, NXOpen.View.ScaleAdjustment.Fit);
    
    NXOpen.Point3d scaleAboutPoint1 = new NXOpen.Point3d(-1.81704545454545, 2.58579545454545, 0.0);
    NXOpen.Point3d viewCenter1 = new NXOpen.Point3d(1.81704545454545, -2.58579545454545, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.8, scaleAboutPoint1, viewCenter1);
    
    NXOpen.Point3d scaleAboutPoint2 = new NXOpen.Point3d(-2.27130681818182, 3.23224431818182, 0.0);
    NXOpen.Point3d viewCenter2 = new NXOpen.Point3d(2.27130681818182, -3.23224431818182, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.8, scaleAboutPoint2, viewCenter2);
    
    NXOpen.Point3d scaleAboutPoint3 = new NXOpen.Point3d(-2.83913352272726, 4.04030539772728, 0.0);
    NXOpen.Point3d viewCenter3 = new NXOpen.Point3d(2.83913352272727, -4.04030539772726, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.8, scaleAboutPoint3, viewCenter3);
    
    NXOpen.Point3d scaleAboutPoint4 = new NXOpen.Point3d(-3.54891690340909, 5.05038174715909, 0.0);
    NXOpen.Point3d viewCenter4 = new NXOpen.Point3d(3.54891690340909, -5.05038174715909, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.8, scaleAboutPoint4, viewCenter4);
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId53;
    markId53 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.SketchInPlaceBuilder sketchInPlaceBuilder2;
    sketchInPlaceBuilder2 = workPart.Sketches.CreateNewSketchInPlaceBuilder(nullNXOpen_Sketch);
    
    NXOpen.Expression expression28;
    expression28 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression29;
    expression29 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.SketchAlongPathBuilder sketchAlongPathBuilder2;
    sketchAlongPathBuilder2 = workPart.Sketches.CreateSketchAlongPathBuilder(nullNXOpen_Sketch);
    
    sketchAlongPathBuilder2.PlaneLocation.Expression.RightHandSide = "0";
    
    theSession.SetUndoMarkName(markId53, "Create Sketch Dialog");
    
    NXOpen.DatumPlane datumPlane2 = (NXOpen.DatumPlane)workPart.Datums.FindObject("DATUM_CSYS(0) XY plane");
    NXOpen.Point3d point3 = new NXOpen.Point3d(6.69832430752837, 0.0, 7.16608220880681);
    sketchInPlaceBuilder2.PlaneOrFace.SetValue(datumPlane2, workPart.ModelingViews.WorkView, point3);
    
    sketchInPlaceBuilder2.SketchOrigin = point2;
    
    sketchInPlaceBuilder2.PlaneOrFace.Value = null;
    
    sketchInPlaceBuilder2.PlaneOrFace.Value = datumPlane2;
    
    sketchInPlaceBuilder2.ReversePlaneNormal = true;
    
    sketchInPlaceBuilder2.Axis.Value = datumAxis1;
    
    NXOpen.Session.UndoMarkId markId54;
    markId54 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Sketch");
    
    theSession.DeleteUndoMark(markId54, null);
    
    NXOpen.Session.UndoMarkId markId55;
    markId55 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Sketch");
    
    theSession.Preferences.Sketch.CreateInferredConstraints = true;
    
    theSession.Preferences.Sketch.ContinuousAutoDimensioning = true;
    
    theSession.Preferences.Sketch.DimensionLabel = NXOpen.Preferences.SketchPreferences.DimensionLabelType.Expression;
    
    theSession.Preferences.Sketch.TextSizeFixed = true;
    
    theSession.Preferences.Sketch.FixedTextSize = 3.0;
    
    theSession.Preferences.Sketch.ConstraintSymbolSize = 3.0;
    
    theSession.Preferences.Sketch.DisplayObjectColor = false;
    
    theSession.Preferences.Sketch.DisplayObjectName = true;
    
    NXOpen.NXObject nXObject3;
    nXObject3 = sketchInPlaceBuilder2.Commit();
    
    NXOpen.Sketch sketch2 = (NXOpen.Sketch)nXObject3;
    NXOpen.Features.Feature feature3;
    feature3 = sketch2.Feature;
    
    NXOpen.Session.UndoMarkId markId56;
    markId56 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "update");
    
    int nErrs6;
    nErrs6 = theSession.UpdateManager.DoUpdate(markId56);
    
    sketch2.Activate(NXOpen.Sketch.ViewReorient.True);
    
    theSession.DeleteUndoMark(markId55, null);
    
    theSession.SetUndoMarkName(markId53, "Create Sketch");
    
    sketchInPlaceBuilder2.Destroy();
    
    sketchAlongPathBuilder2.Destroy();
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression29);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression28);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Rectangle...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId57;
    markId57 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId58;
    markId58 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Rectangle");
    
    theSession.SetUndoMarkVisibility(markId58, "Create Rectangle", NXOpen.Session.MarkVisibility.Visible);
    
    // ----------------------------------------------
    // Creating rectangle using By 2 Points method 
    // ----------------------------------------------
    NXOpen.Point3d startPoint10 = new NXOpen.Point3d(10.0, 0.0, 10.0);
    NXOpen.Point3d endPoint10 = new NXOpen.Point3d(102.0, 0.0, 10.0);
    NXOpen.Line line10;
    line10 = workPart.Curves.CreateLine(startPoint10, endPoint10);
    
    NXOpen.Point3d startPoint11 = new NXOpen.Point3d(102.0, 0.0, 10.0);
    NXOpen.Point3d endPoint11 = new NXOpen.Point3d(102.0, 0.0, 40.0000000000002);
    NXOpen.Line line11;
    line11 = workPart.Curves.CreateLine(startPoint11, endPoint11);
    
    NXOpen.Point3d startPoint12 = new NXOpen.Point3d(102.0, 0.0, 40.0000000000002);
    NXOpen.Point3d endPoint12 = new NXOpen.Point3d(10.0, 0.0, 40.0000000000002);
    NXOpen.Line line12;
    line12 = workPart.Curves.CreateLine(startPoint12, endPoint12);
    
    NXOpen.Point3d startPoint13 = new NXOpen.Point3d(10.0, 0.0, 40.0000000000002);
    NXOpen.Point3d endPoint13 = new NXOpen.Point3d(10.0, 0.0, 10.0);
    NXOpen.Line line13;
    line13 = workPart.Curves.CreateLine(startPoint13, endPoint13);
    
    theSession.ActiveSketch.AddGeometry(line10, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    theSession.ActiveSketch.AddGeometry(line11, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    theSession.ActiveSketch.AddGeometry(line12, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    theSession.ActiveSketch.AddGeometry(line13, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_16 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_16.Geometry = line10;
    geom1_16.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_16.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_16 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_16.Geometry = line11;
    geom2_16.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_16.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint24;
    sketchGeometricConstraint24 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_16, geom2_16);
    
    NXOpen.Sketch.ConstraintGeometry geom1_17 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_17.Geometry = line11;
    geom1_17.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_17.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_17 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_17.Geometry = line12;
    geom2_17.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_17.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint25;
    sketchGeometricConstraint25 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_17, geom2_17);
    
    NXOpen.Sketch.ConstraintGeometry geom1_18 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_18.Geometry = line12;
    geom1_18.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_18.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_18 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_18.Geometry = line13;
    geom2_18.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_18.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint26;
    sketchGeometricConstraint26 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_18, geom2_18);
    
    NXOpen.Sketch.ConstraintGeometry geom1_19 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_19.Geometry = line13;
    geom1_19.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_19.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_19 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_19.Geometry = line10;
    geom2_19.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_19.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint27;
    sketchGeometricConstraint27 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_19, geom2_19);
    
    NXOpen.Sketch.ConstraintGeometry geom10 = new NXOpen.Sketch.ConstraintGeometry();
    geom10.Geometry = line10;
    geom10.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom10.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint28;
    sketchGeometricConstraint28 = theSession.ActiveSketch.CreateHorizontalConstraint(geom10);
    
    NXOpen.Sketch.ConstraintGeometry conGeom1_1 = new NXOpen.Sketch.ConstraintGeometry();
    conGeom1_1.Geometry = line10;
    conGeom1_1.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom1_1.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry conGeom2_1 = new NXOpen.Sketch.ConstraintGeometry();
    conGeom2_1.Geometry = line11;
    conGeom2_1.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom2_1.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint29;
    sketchGeometricConstraint29 = theSession.ActiveSketch.CreatePerpendicularConstraint(conGeom1_1, conGeom2_1);
    
    NXOpen.Sketch.ConstraintGeometry conGeom1_2 = new NXOpen.Sketch.ConstraintGeometry();
    conGeom1_2.Geometry = line11;
    conGeom1_2.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom1_2.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry conGeom2_2 = new NXOpen.Sketch.ConstraintGeometry();
    conGeom2_2.Geometry = line12;
    conGeom2_2.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom2_2.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint30;
    sketchGeometricConstraint30 = theSession.ActiveSketch.CreatePerpendicularConstraint(conGeom1_2, conGeom2_2);
    
    NXOpen.Sketch.ConstraintGeometry conGeom1_3 = new NXOpen.Sketch.ConstraintGeometry();
    conGeom1_3.Geometry = line12;
    conGeom1_3.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom1_3.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry conGeom2_3 = new NXOpen.Sketch.ConstraintGeometry();
    conGeom2_3.Geometry = line13;
    conGeom2_3.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom2_3.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint31;
    sketchGeometricConstraint31 = theSession.ActiveSketch.CreatePerpendicularConstraint(conGeom1_3, conGeom2_3);
    
    NXOpen.Sketch.ConstraintGeometry conGeom1_4 = new NXOpen.Sketch.ConstraintGeometry();
    conGeom1_4.Geometry = line13;
    conGeom1_4.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom1_4.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry conGeom2_4 = new NXOpen.Sketch.ConstraintGeometry();
    conGeom2_4.Geometry = line10;
    conGeom2_4.PointType = NXOpen.Sketch.ConstraintPointType.None;
    conGeom2_4.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint32;
    sketchGeometricConstraint32 = theSession.ActiveSketch.CreatePerpendicularConstraint(conGeom1_4, conGeom2_4);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_16 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_16.Geometry = line12;
    dimObject1_16.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_16.AssocValue = 0;
    dimObject1_16.HelpPoint.X = 0.0;
    dimObject1_16.HelpPoint.Y = 0.0;
    dimObject1_16.HelpPoint.Z = 0.0;
    dimObject1_16.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_10 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_10.Geometry = line12;
    dimObject2_10.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_10.AssocValue = 0;
    dimObject2_10.HelpPoint.X = 0.0;
    dimObject2_10.HelpPoint.Y = 0.0;
    dimObject2_10.HelpPoint.Z = 0.0;
    dimObject2_10.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin16 = new NXOpen.Point3d(56.0, 0.0, 45.8038011139944);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint16;
    sketchDimensionalConstraint16 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_16, dimObject2_10, dimOrigin16, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint10 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint16;
    NXOpen.Annotations.Dimension dimension16;
    dimension16 = sketchHelpedDimensionalConstraint10.AssociatedDimension;
    
    NXOpen.Expression expression30;
    expression30 = sketchHelpedDimensionalConstraint10.AssociatedExpression;
    
    NXOpen.Sketch.DimensionGeometry dimObject1_17 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_17.Geometry = line11;
    dimObject1_17.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_17.AssocValue = 0;
    dimObject1_17.HelpPoint.X = 0.0;
    dimObject1_17.HelpPoint.Y = 0.0;
    dimObject1_17.HelpPoint.Z = 0.0;
    dimObject1_17.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_11 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_11.Geometry = line11;
    dimObject2_11.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_11.AssocValue = 0;
    dimObject2_11.HelpPoint.X = 0.0;
    dimObject2_11.HelpPoint.Y = 0.0;
    dimObject2_11.HelpPoint.Z = 0.0;
    dimObject2_11.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin17 = new NXOpen.Point3d(107.803801113994, 0.0, 25.0000000000001);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint17;
    sketchDimensionalConstraint17 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_17, dimObject2_11, dimOrigin17, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint11 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint17;
    NXOpen.Annotations.Dimension dimension17;
    dimension17 = sketchHelpedDimensionalConstraint11.AssociatedDimension;
    
    NXOpen.Expression expression31;
    expression31 = sketchHelpedDimensionalConstraint11.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    NXOpen.SmartObject[] geoms1 = new NXOpen.SmartObject[4];
    geoms1[0] = line10;
    geoms1[1] = line11;
    geoms1[2] = line12;
    geoms1[3] = line13;
    theSession.ActiveSketch.UpdateConstraintDisplay(geoms1);
    
    NXOpen.SmartObject[] geoms2 = new NXOpen.SmartObject[4];
    geoms2[0] = line10;
    geoms2[1] = line11;
    geoms2[2] = line12;
    geoms2[3] = line13;
    theSession.ActiveSketch.UpdateDimensionDisplay(geoms2);
    
    // ----------------------------------------------
    //   Menu: File->Finish Sketch
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId59;
    markId59 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Deactivate Sketch");
    
    theSession.ActiveSketch.Deactivate(NXOpen.Sketch.ViewReorient.False, NXOpen.Sketch.UpdateLevel.Model);
    
    // ----------------------------------------------
    //   Menu: Insert->Design Feature->Extrude...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId60;
    markId60 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.Features.ExtrudeBuilder extrudeBuilder2;
    extrudeBuilder2 = workPart.Features.CreateExtrudeBuilder(nullNXOpen_Features_Feature);
    
    NXOpen.Section section2;
    section2 = workPart.Sections.CreateSection(0.0095, 0.01, 0.5);
    
    extrudeBuilder2.Section = section2;
    
    extrudeBuilder2.AllowSelfIntersectingSection(true);
    
    NXOpen.Expression expression32;
    expression32 = workPart.Expressions.CreateSystemExpressionWithUnits("2.00", unit2);
    
    extrudeBuilder2.DistanceTolerance = 0.01;
    
    extrudeBuilder2.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
    
    NXOpen.Body[] targetBodies9 = new NXOpen.Body[1];
    targetBodies9[0] = nullNXOpen_Body;
    extrudeBuilder2.BooleanOperation.SetTargetBodies(targetBodies9);
    
    extrudeBuilder2.Limits.StartExtend.Value.RightHandSide = "0";
    
    extrudeBuilder2.Limits.EndExtend.Value.RightHandSide = "40";
    
    extrudeBuilder2.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
    
    NXOpen.Body[] targetBodies10 = new NXOpen.Body[1];
    targetBodies10[0] = nullNXOpen_Body;
    extrudeBuilder2.BooleanOperation.SetTargetBodies(targetBodies10);
    
    extrudeBuilder2.Draft.FrontDraftAngle.RightHandSide = "2";
    
    extrudeBuilder2.Draft.BackDraftAngle.RightHandSide = "2";
    
    extrudeBuilder2.Offset.StartOffset.RightHandSide = "0";
    
    extrudeBuilder2.Offset.EndOffset.RightHandSide = "5";
    
    NXOpen.GeometricUtilities.SmartVolumeProfileBuilder smartVolumeProfileBuilder2;
    smartVolumeProfileBuilder2 = extrudeBuilder2.SmartVolumeProfile;
    
    smartVolumeProfileBuilder2.OpenProfileSmartVolumeOption = false;
    
    smartVolumeProfileBuilder2.CloseProfileRule = NXOpen.GeometricUtilities.SmartVolumeProfileBuilder.CloseProfileRuleType.Fci;
    
    theSession.SetUndoMarkName(markId60, "Extrude Dialog");
    
    section2.DistanceTolerance = 0.01;
    
    section2.ChainingTolerance = 0.0095;
    
    section2.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.OnlyCurves);
    
    NXOpen.Session.UndoMarkId markId61;
    markId61 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "section mark");
    
    NXOpen.Session.UndoMarkId markId62;
    markId62 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, null);
    
    NXOpen.Features.Feature[] features2 = new NXOpen.Features.Feature[1];
    NXOpen.Features.SketchFeature sketchFeature2 = (NXOpen.Features.SketchFeature)feature3;
    features2[0] = sketchFeature2;
    NXOpen.CurveFeatureRule curveFeatureRule2;
    curveFeatureRule2 = workPart.ScRuleFactory.CreateRuleCurveFeature(features2);
    
    section2.AllowSelfIntersection(true);
    
    NXOpen.SelectionIntentRule[] rules2 = new NXOpen.SelectionIntentRule[1];
    rules2[0] = curveFeatureRule2;
    NXOpen.Point3d helpPoint2 = new NXOpen.Point3d(21.3717307350852, 0.0, 40.0000000000002);
    section2.AddToSection(rules2, line12, nullNXOpen_NXObject, nullNXOpen_NXObject, helpPoint2, NXOpen.Section.Mode.Create, false);
    
    theSession.DeleteUndoMark(markId62, null);
    
    NXOpen.Direction direction2;
    direction2 = workPart.Directions.CreateDirection(sketch2, NXOpen.Sense.Forward, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    extrudeBuilder2.Direction = direction2;
    
    NXOpen.Body[] targetBodies11 = new NXOpen.Body[1];
    NXOpen.Body body1 = (NXOpen.Body)workPart.Bodies.FindObject("EXTRUDE(2)");
    targetBodies11[0] = body1;
    extrudeBuilder2.BooleanOperation.SetTargetBodies(targetBodies11);
    
    extrudeBuilder2.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Unite;
    
    NXOpen.Body[] targetBodies12 = new NXOpen.Body[1];
    targetBodies12[0] = body1;
    extrudeBuilder2.BooleanOperation.SetTargetBodies(targetBodies12);
    
    theSession.DeleteUndoMark(markId61, null);
    
    extrudeBuilder2.Limits.EndExtend.Value.RightHandSide = "52";
    
    extrudeBuilder2.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Unite;
    
    NXOpen.Body[] targetBodies13 = new NXOpen.Body[1];
    targetBodies13[0] = body1;
    extrudeBuilder2.BooleanOperation.SetTargetBodies(targetBodies13);
    
    extrudeBuilder2.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Subtract;
    
    NXOpen.Body[] targetBodies14 = new NXOpen.Body[1];
    targetBodies14[0] = body1;
    extrudeBuilder2.BooleanOperation.SetTargetBodies(targetBodies14);
    
    NXOpen.Session.UndoMarkId markId63;
    markId63 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    theSession.DeleteUndoMark(markId63, null);
    
    NXOpen.Session.UndoMarkId markId64;
    markId64 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    extrudeBuilder2.ParentFeatureInternal = false;
    
    NXOpen.Features.Feature feature4;
    try
    {
      // Tool body completely outside target body.
      feature4 = extrudeBuilder2.CommitFeature();
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(670030);
    }
    
    theSession.UndoToMarkWithStatus(markId64, null);
    
    theSession.DeleteUndoMark(markId64, null);
    
    extrudeBuilder2.Limits.EndExtend.Value.RightHandSide = "-52";
    
    NXOpen.Session.UndoMarkId markId65;
    markId65 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    theSession.DeleteUndoMark(markId65, null);
    
    NXOpen.Session.UndoMarkId markId66;
    markId66 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    extrudeBuilder2.ParentFeatureInternal = false;
    
    NXOpen.Features.Feature feature5;
    feature5 = extrudeBuilder2.CommitFeature();
    
    theSession.DeleteUndoMark(markId66, null);
    
    theSession.SetUndoMarkName(markId60, "Extrude");
    
    NXOpen.Expression expression33 = extrudeBuilder2.Limits.StartExtend.Value;
    NXOpen.Expression expression34 = extrudeBuilder2.Limits.EndExtend.Value;
    extrudeBuilder2.Destroy();
    
    workPart.Expressions.Delete(expression32);
    
    // ----------------------------------------------
    //   Menu: Orient View->Left
    // ----------------------------------------------
    workPart.ModelingViews.WorkView.Orient(NXOpen.View.Canned.Left, NXOpen.View.ScaleAdjustment.Fit);
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId67;
    markId67 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.SketchInPlaceBuilder sketchInPlaceBuilder3;
    sketchInPlaceBuilder3 = workPart.Sketches.CreateNewSketchInPlaceBuilder(nullNXOpen_Sketch);
    
    NXOpen.Expression expression35;
    expression35 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression36;
    expression36 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.SketchAlongPathBuilder sketchAlongPathBuilder3;
    sketchAlongPathBuilder3 = workPart.Sketches.CreateSketchAlongPathBuilder(nullNXOpen_Sketch);
    
    sketchAlongPathBuilder3.PlaneLocation.Expression.RightHandSide = "0";
    
    theSession.SetUndoMarkName(markId67, "Create Sketch Dialog");
    
    NXOpen.Scalar scalar1;
    scalar1 = workPart.Scalars.CreateScalar(0.0, NXOpen.Scalar.DimensionalityType.None, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    NXOpen.Features.Extrude extrude1 = (NXOpen.Features.Extrude)feature2;
    NXOpen.Edge edge1 = (NXOpen.Edge)extrude1.FindObject("EDGE * 130 * 150 {(0.0000000000001,52,40)(0,26,40)(-0,0,40) EXTRUDE(2)}");
    NXOpen.Point point4;
    point4 = workPart.Points.CreatePoint(edge1, scalar1, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    NXOpen.Face face1 = (NXOpen.Face)extrude1.FindObject("FACE 150 {(0,26,20) EXTRUDE(2)}");
    NXOpen.Point3d point5 = new NXOpen.Point3d(4.09841789802572e-014, 34.9856940514868, 32.9592923003856);
    sketchInPlaceBuilder3.PlaneOrFace.SetValue(face1, workPart.ModelingViews.WorkView, point5);
    
    sketchInPlaceBuilder3.SketchOrigin = point4;
    
    NXOpen.Scalar scalar2;
    scalar2 = workPart.Scalars.CreateScalar(0.0, NXOpen.Scalar.DimensionalityType.None, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    NXOpen.Point point6;
    point6 = workPart.Points.CreatePoint(edge1, scalar2, NXOpen.PointCollection.PointOnCurveLocationOption.PercentParameter, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    sketchInPlaceBuilder3.PlaneOrFace.Value = null;
    
    sketchInPlaceBuilder3.PlaneOrFace.Value = face1;
    
    NXOpen.Edge edge2 = (NXOpen.Edge)extrude1.FindObject("EDGE * 120 * 150 {(0.0000000000001,52,0)(0,26,0)(-0,0,0) EXTRUDE(2)}");
    sketchInPlaceBuilder3.Axis.Value = edge2;
    
    int nErrs7;
    nErrs7 = theSession.UpdateManager.AddToDeleteList(point4);
    
    NXOpen.Session.UndoMarkId markId68;
    markId68 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Sketch");
    
    theSession.DeleteUndoMark(markId68, null);
    
    NXOpen.Session.UndoMarkId markId69;
    markId69 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Create Sketch");
    
    theSession.Preferences.Sketch.CreateInferredConstraints = true;
    
    theSession.Preferences.Sketch.ContinuousAutoDimensioning = true;
    
    theSession.Preferences.Sketch.DimensionLabel = NXOpen.Preferences.SketchPreferences.DimensionLabelType.Expression;
    
    theSession.Preferences.Sketch.TextSizeFixed = true;
    
    theSession.Preferences.Sketch.FixedTextSize = 3.0;
    
    theSession.Preferences.Sketch.ConstraintSymbolSize = 3.0;
    
    theSession.Preferences.Sketch.DisplayObjectColor = false;
    
    theSession.Preferences.Sketch.DisplayObjectName = true;
    
    NXOpen.NXObject nXObject4;
    nXObject4 = sketchInPlaceBuilder3.Commit();
    
    NXOpen.Sketch sketch3 = (NXOpen.Sketch)nXObject4;
    NXOpen.Features.Feature feature6;
    feature6 = sketch3.Feature;
    
    NXOpen.Session.UndoMarkId markId70;
    markId70 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "update");
    
    int nErrs8;
    nErrs8 = theSession.UpdateManager.DoUpdate(markId70);
    
    sketch3.Activate(NXOpen.Sketch.ViewReorient.True);
    
    theSession.DeleteUndoMark(markId69, null);
    
    theSession.SetUndoMarkName(markId67, "Create Sketch");
    
    sketchInPlaceBuilder3.Destroy();
    
    sketchAlongPathBuilder3.Destroy();
    
    workPart.Points.DeletePoint(point6);
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression36);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression35);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    NXOpen.Point3d scaleAboutPoint5 = new NXOpen.Point3d(-5.41850897577095, 0.180616965859032, 0.0);
    NXOpen.Point3d viewCenter5 = new NXOpen.Point3d(5.41850897577095, -0.180616965859032, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.8, scaleAboutPoint5, viewCenter5);
    
    NXOpen.Point3d origin1 = new NXOpen.Point3d(0.0, 25.1332263284693, 18.3372526572826);
    workPart.ModelingViews.WorkView.SetOrigin(origin1);
    
    NXOpen.Point3d origin2 = new NXOpen.Point3d(0.0, 25.1332263284693, 18.3372526572826);
    workPart.ModelingViews.WorkView.SetOrigin(origin2);
    
    NXOpen.Matrix3x3 rotMatrix1 = new NXOpen.Matrix3x3();
    rotMatrix1.Xx = 0.0;
    rotMatrix1.Xy = -1.0;
    rotMatrix1.Xz = 0.0;
    rotMatrix1.Yx = 0.0;
    rotMatrix1.Yy = 0.0;
    rotMatrix1.Yz = 1.0;
    rotMatrix1.Zx = -1.0;
    rotMatrix1.Zy = 0.0;
    rotMatrix1.Zz = 0.0;
    NXOpen.Point3d translation1 = new NXOpen.Point3d(25.1332263284693, -18.3372526572826, 0.0);
    workPart.ModelingViews.WorkView.SetRotationTranslationScale(rotMatrix1, translation1, 4.68763641687722);
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Arc...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId71;
    markId71 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Point3d scaleAboutPoint6 = new NXOpen.Point3d(-18.5132390005507, 21.2789362902671, 0.0);
    NXOpen.Point3d viewCenter6 = new NXOpen.Point3d(18.5132390005507, -21.2789362902672, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint6, viewCenter6);
    
    NXOpen.Session.UndoMarkId markId72;
    markId72 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.DeleteUndoMark(markId72, "Curve");
    
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Line...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId73;
    markId73 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId74;
    markId74 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.SetUndoMarkVisibility(markId74, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint14 = new NXOpen.Point3d(0.0, 47.0, 32.0);
    NXOpen.Point3d endPoint14 = new NXOpen.Point3d(0.0, 47.0, 24.0);
    NXOpen.Line line14;
    line14 = workPart.Curves.CreateLine(startPoint14, endPoint14);
    
    theSession.ActiveSketch.AddGeometry(line14, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom11 = new NXOpen.Sketch.ConstraintGeometry();
    geom11.Geometry = line14;
    geom11.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom11.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint33;
    sketchGeometricConstraint33 = theSession.ActiveSketch.CreateVerticalConstraint(geom11);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_18 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_18.Geometry = line14;
    dimObject1_18.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_18.AssocValue = 0;
    dimObject1_18.HelpPoint.X = 0.0;
    dimObject1_18.HelpPoint.Y = 0.0;
    dimObject1_18.HelpPoint.Z = 0.0;
    dimObject1_18.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_12 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_12.Geometry = line14;
    dimObject2_12.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_12.AssocValue = 0;
    dimObject2_12.HelpPoint.X = 0.0;
    dimObject2_12.HelpPoint.Y = 0.0;
    dimObject2_12.HelpPoint.Z = 0.0;
    dimObject2_12.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin18 = new NXOpen.Point3d(0.0, 48.5359553002186, 28.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint18;
    sketchDimensionalConstraint18 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_18, dimObject2_12, dimOrigin18, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint12 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint18;
    NXOpen.Annotations.Dimension dimension18;
    dimension18 = sketchHelpedDimensionalConstraint12.AssociatedDimension;
    
    NXOpen.Expression expression37;
    expression37 = sketchHelpedDimensionalConstraint12.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId75;
    markId75 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.SetUndoMarkVisibility(markId75, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint15 = new NXOpen.Point3d(0.0, 41.0, 32.0);
    NXOpen.Point3d endPoint15 = new NXOpen.Point3d(0.0, 41.0, 24.0);
    NXOpen.Line line15;
    line15 = workPart.Curves.CreateLine(startPoint15, endPoint15);
    
    theSession.ActiveSketch.AddGeometry(line15, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom12 = new NXOpen.Sketch.ConstraintGeometry();
    geom12.Geometry = line15;
    geom12.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom12.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint34;
    sketchGeometricConstraint34 = theSession.ActiveSketch.CreateVerticalConstraint(geom12);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_19 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_19.Geometry = line15;
    dimObject1_19.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_19.AssocValue = 0;
    dimObject1_19.HelpPoint.X = 0.0;
    dimObject1_19.HelpPoint.Y = 0.0;
    dimObject1_19.HelpPoint.Z = 0.0;
    dimObject1_19.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_13 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_13.Geometry = line15;
    dimObject2_13.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_13.AssocValue = 0;
    dimObject2_13.HelpPoint.X = 0.0;
    dimObject2_13.HelpPoint.Y = 0.0;
    dimObject2_13.HelpPoint.Z = 0.0;
    dimObject2_13.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin19 = new NXOpen.Point3d(0.0, 42.5359553002185, 28.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint19;
    sketchDimensionalConstraint19 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_19, dimObject2_13, dimOrigin19, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint13 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint19;
    NXOpen.Annotations.Dimension dimension19;
    dimension19 = sketchHelpedDimensionalConstraint13.AssociatedDimension;
    
    NXOpen.Expression expression38;
    expression38 = sketchHelpedDimensionalConstraint13.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Arc...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId76;
    markId76 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId77;
    markId77 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression39;
    expression39 = workPart.Expressions.CreateSystemExpression("3");
    
    theSession.SetUndoMarkVisibility(markId77, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.NXMatrix nXMatrix7;
    nXMatrix7 = theSession.ActiveSketch.Orientation;
    
    NXOpen.Point3d center7 = new NXOpen.Point3d(0.0, 44.0, 32.0);
    NXOpen.Arc arc7;
    arc7 = workPart.Curves.CreateArc(center7, nXMatrix7, 3.0, 0.0, ( 180.0 * Math.PI/180.0 ));
    
    theSession.ActiveSketch.AddGeometry(arc7, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_20 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_20.Geometry = arc7;
    geom1_20.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_20.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_20 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_20.Geometry = line14;
    geom2_20.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_20.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint35;
    sketchGeometricConstraint35 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_20, geom2_20);
    
    NXOpen.Sketch.ConstraintGeometry geom1_21 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_21.Geometry = arc7;
    geom1_21.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_21.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_21 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_21.Geometry = line15;
    geom2_21.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_21.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint36;
    sketchGeometricConstraint36 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_21, geom2_21);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_20 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_20.Geometry = arc7;
    dimObject1_20.AssocType = NXOpen.Sketch.AssocType.Midpoint;
    dimObject1_20.AssocValue = 0;
    dimObject1_20.HelpPoint.X = 0.0;
    dimObject1_20.HelpPoint.Y = 0.0;
    dimObject1_20.HelpPoint.Z = 0.0;
    dimObject1_20.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin20 = new NXOpen.Point3d(0.0, 44.7403334114607, 33.4014403956542);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint20;
    sketchDimensionalConstraint20 = theSession.ActiveSketch.CreateRadialDimension(dimObject1_20, dimOrigin20, expression39, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.Annotations.Dimension dimension20;
    dimension20 = sketchDimensionalConstraint20.AssociatedDimension;
    
    theSession.ActiveSketch.Update();
    
    // ----------------------------------------------
    //   Dialog Begin Arc
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId78;
    markId78 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression40;
    expression40 = workPart.Expressions.CreateSystemExpression("3");
    
    theSession.SetUndoMarkVisibility(markId78, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.NXMatrix nXMatrix8;
    nXMatrix8 = theSession.ActiveSketch.Orientation;
    
    NXOpen.Point3d center8 = new NXOpen.Point3d(0.0, 44.0, 24.0);
    NXOpen.Arc arc8;
    arc8 = workPart.Curves.CreateArc(center8, nXMatrix8, 3.0, ( 180.0 * Math.PI/180.0 ), ( 360.0 * Math.PI/180.0 ));
    
    theSession.ActiveSketch.AddGeometry(arc8, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_22 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_22.Geometry = arc8;
    geom1_22.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_22.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_22 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_22.Geometry = line14;
    geom2_22.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_22.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint37;
    sketchGeometricConstraint37 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_22, geom2_22);
    
    NXOpen.Sketch.ConstraintGeometry geom1_23 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_23.Geometry = arc8;
    geom1_23.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_23.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_23 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_23.Geometry = line15;
    geom2_23.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_23.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint38;
    sketchGeometricConstraint38 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_23, geom2_23);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_21 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_21.Geometry = arc8;
    dimObject1_21.AssocType = NXOpen.Sketch.AssocType.Midpoint;
    dimObject1_21.AssocValue = 0;
    dimObject1_21.HelpPoint.X = 0.0;
    dimObject1_21.HelpPoint.Y = 0.0;
    dimObject1_21.HelpPoint.Z = 0.0;
    dimObject1_21.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin21 = new NXOpen.Point3d(0.0, 43.2596665885393, 22.5985596043458);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint21;
    sketchDimensionalConstraint21 = theSession.ActiveSketch.CreateRadialDimension(dimObject1_21, dimOrigin21, expression40, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.Annotations.Dimension dimension21;
    dimension21 = sketchDimensionalConstraint21.AssociatedDimension;
    
    theSession.ActiveSketch.Update();
    
    // ----------------------------------------------
    //   Dialog Begin Arc
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Line...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId79;
    markId79 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId80;
    markId80 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.SetUndoMarkVisibility(markId80, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint16 = new NXOpen.Point3d(0.0, 5.00000000000001, 32.0);
    NXOpen.Point3d endPoint16 = new NXOpen.Point3d(0.0, 5.00000000000001, 24.0);
    NXOpen.Line line16;
    line16 = workPart.Curves.CreateLine(startPoint16, endPoint16);
    
    theSession.ActiveSketch.AddGeometry(line16, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom13 = new NXOpen.Sketch.ConstraintGeometry();
    geom13.Geometry = line16;
    geom13.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom13.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint39;
    sketchGeometricConstraint39 = theSession.ActiveSketch.CreateVerticalConstraint(geom13);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_22 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_22.Geometry = line16;
    dimObject1_22.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_22.AssocValue = 0;
    dimObject1_22.HelpPoint.X = 0.0;
    dimObject1_22.HelpPoint.Y = 0.0;
    dimObject1_22.HelpPoint.Z = 0.0;
    dimObject1_22.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_14 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_14.Geometry = line16;
    dimObject2_14.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_14.AssocValue = 0;
    dimObject2_14.HelpPoint.X = 0.0;
    dimObject2_14.HelpPoint.Y = 0.0;
    dimObject2_14.HelpPoint.Z = 0.0;
    dimObject2_14.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin22 = new NXOpen.Point3d(0.0, 6.53595530021854, 28.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint22;
    sketchDimensionalConstraint22 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_22, dimObject2_14, dimOrigin22, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint14 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint22;
    NXOpen.Annotations.Dimension dimension22;
    dimension22 = sketchHelpedDimensionalConstraint14.AssociatedDimension;
    
    NXOpen.Expression expression41;
    expression41 = sketchHelpedDimensionalConstraint14.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId81;
    markId81 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    theSession.SetUndoMarkVisibility(markId81, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.Point3d startPoint17 = new NXOpen.Point3d(0.0, 11.0, 32.0);
    NXOpen.Point3d endPoint17 = new NXOpen.Point3d(0.0, 11.0, 24.0);
    NXOpen.Line line17;
    line17 = workPart.Curves.CreateLine(startPoint17, endPoint17);
    
    theSession.ActiveSketch.AddGeometry(line17, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom14 = new NXOpen.Sketch.ConstraintGeometry();
    geom14.Geometry = line17;
    geom14.PointType = NXOpen.Sketch.ConstraintPointType.None;
    geom14.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint40;
    sketchGeometricConstraint40 = theSession.ActiveSketch.CreateVerticalConstraint(geom14);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_23 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_23.Geometry = line17;
    dimObject1_23.AssocType = NXOpen.Sketch.AssocType.StartPoint;
    dimObject1_23.AssocValue = 0;
    dimObject1_23.HelpPoint.X = 0.0;
    dimObject1_23.HelpPoint.Y = 0.0;
    dimObject1_23.HelpPoint.Z = 0.0;
    dimObject1_23.View = nullNXOpen_NXObject;
    NXOpen.Sketch.DimensionGeometry dimObject2_15 = new NXOpen.Sketch.DimensionGeometry();
    dimObject2_15.Geometry = line17;
    dimObject2_15.AssocType = NXOpen.Sketch.AssocType.EndPoint;
    dimObject2_15.AssocValue = 0;
    dimObject2_15.HelpPoint.X = 0.0;
    dimObject2_15.HelpPoint.Y = 0.0;
    dimObject2_15.HelpPoint.Z = 0.0;
    dimObject2_15.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin23 = new NXOpen.Point3d(0.0, 12.5359553002185, 28.0);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint23;
    sketchDimensionalConstraint23 = theSession.ActiveSketch.CreateDimension(NXOpen.Sketch.ConstraintType.ParallelDim, dimObject1_23, dimObject2_15, dimOrigin23, nullNXOpen_Expression, NXOpen.Sketch.DimensionOption.CreateAsAutomatic);
    
    NXOpen.SketchHelpedDimensionalConstraint sketchHelpedDimensionalConstraint15 = (NXOpen.SketchHelpedDimensionalConstraint)sketchDimensionalConstraint23;
    NXOpen.Annotations.Dimension dimension23;
    dimension23 = sketchHelpedDimensionalConstraint15.AssociatedDimension;
    
    NXOpen.Expression expression42;
    expression42 = sketchHelpedDimensionalConstraint15.AssociatedExpression;
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = false;
    
    theSession.ActiveSketch.Update();
    
    theSession.Preferences.Sketch.AutoDimensionsToArcCenter = true;
    
    // ----------------------------------------------
    //   Dialog Begin Line
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: Insert->Sketch Curve->Arc...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId82;
    markId82 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Profile short list");
    
    NXOpen.Session.UndoMarkId markId83;
    markId83 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression43;
    expression43 = workPart.Expressions.CreateSystemExpression("3");
    
    theSession.SetUndoMarkVisibility(markId83, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.NXMatrix nXMatrix9;
    nXMatrix9 = theSession.ActiveSketch.Orientation;
    
    NXOpen.Point3d center9 = new NXOpen.Point3d(0.0, 8.00000000000001, 32.0);
    NXOpen.Arc arc9;
    arc9 = workPart.Curves.CreateArc(center9, nXMatrix9, 3.0, 0.0, ( 180.0 * Math.PI/180.0 ));
    
    theSession.ActiveSketch.AddGeometry(arc9, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_24 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_24.Geometry = arc9;
    geom1_24.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_24.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_24 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_24.Geometry = line17;
    geom2_24.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_24.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint41;
    sketchGeometricConstraint41 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_24, geom2_24);
    
    NXOpen.Sketch.ConstraintGeometry geom1_25 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_25.Geometry = arc9;
    geom1_25.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_25.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_25 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_25.Geometry = line16;
    geom2_25.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom2_25.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint42;
    sketchGeometricConstraint42 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_25, geom2_25);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_24 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_24.Geometry = arc9;
    dimObject1_24.AssocType = NXOpen.Sketch.AssocType.Midpoint;
    dimObject1_24.AssocValue = 0;
    dimObject1_24.HelpPoint.X = 0.0;
    dimObject1_24.HelpPoint.Y = 0.0;
    dimObject1_24.HelpPoint.Z = 0.0;
    dimObject1_24.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin24 = new NXOpen.Point3d(0.0, 8.74033341146074, 33.4014403956542);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint24;
    sketchDimensionalConstraint24 = theSession.ActiveSketch.CreateRadialDimension(dimObject1_24, dimOrigin24, expression43, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.Annotations.Dimension dimension24;
    dimension24 = sketchDimensionalConstraint24.AssociatedDimension;
    
    theSession.ActiveSketch.Update();
    
    // ----------------------------------------------
    //   Dialog Begin Arc
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId84;
    markId84 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Curve");
    
    NXOpen.Expression expression44;
    expression44 = workPart.Expressions.CreateSystemExpression("3");
    
    theSession.SetUndoMarkVisibility(markId84, "Curve", NXOpen.Session.MarkVisibility.Visible);
    
    NXOpen.NXMatrix nXMatrix10;
    nXMatrix10 = theSession.ActiveSketch.Orientation;
    
    NXOpen.Point3d center10 = new NXOpen.Point3d(0.0, 8.00000000000001, 24.0);
    NXOpen.Arc arc10;
    arc10 = workPart.Curves.CreateArc(center10, nXMatrix10, 3.0, ( 180.0 * Math.PI/180.0 ), ( 360.0 * Math.PI/180.0 ));
    
    theSession.ActiveSketch.AddGeometry(arc10, NXOpen.Sketch.InferConstraintsOption.InferNoConstraints);
    
    NXOpen.Sketch.ConstraintGeometry geom1_26 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_26.Geometry = arc10;
    geom1_26.PointType = NXOpen.Sketch.ConstraintPointType.StartVertex;
    geom1_26.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_26 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_26.Geometry = line17;
    geom2_26.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_26.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint43;
    sketchGeometricConstraint43 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_26, geom2_26);
    
    NXOpen.Sketch.ConstraintGeometry geom1_27 = new NXOpen.Sketch.ConstraintGeometry();
    geom1_27.Geometry = arc10;
    geom1_27.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom1_27.SplineDefiningPointIndex = 0;
    NXOpen.Sketch.ConstraintGeometry geom2_27 = new NXOpen.Sketch.ConstraintGeometry();
    geom2_27.Geometry = line16;
    geom2_27.PointType = NXOpen.Sketch.ConstraintPointType.EndVertex;
    geom2_27.SplineDefiningPointIndex = 0;
    NXOpen.SketchGeometricConstraint sketchGeometricConstraint44;
    sketchGeometricConstraint44 = theSession.ActiveSketch.CreateCoincidentConstraint(geom1_27, geom2_27);
    
    NXOpen.Sketch.DimensionGeometry dimObject1_25 = new NXOpen.Sketch.DimensionGeometry();
    dimObject1_25.Geometry = arc10;
    dimObject1_25.AssocType = NXOpen.Sketch.AssocType.Midpoint;
    dimObject1_25.AssocValue = 0;
    dimObject1_25.HelpPoint.X = 0.0;
    dimObject1_25.HelpPoint.Y = 0.0;
    dimObject1_25.HelpPoint.Z = 0.0;
    dimObject1_25.View = nullNXOpen_NXObject;
    NXOpen.Point3d dimOrigin25 = new NXOpen.Point3d(0.0, 7.25966658853928, 22.5985596043458);
    NXOpen.SketchDimensionalConstraint sketchDimensionalConstraint25;
    sketchDimensionalConstraint25 = theSession.ActiveSketch.CreateRadialDimension(dimObject1_25, dimOrigin25, expression44, NXOpen.Sketch.DimensionOption.CreateAsDriving);
    
    NXOpen.Annotations.Dimension dimension25;
    dimension25 = sketchDimensionalConstraint25.AssociatedDimension;
    
    theSession.ActiveSketch.Update();
    
    // ----------------------------------------------
    //   Dialog Begin Arc
    // ----------------------------------------------
    // ----------------------------------------------
    //   Menu: File->Finish Sketch
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId85;
    markId85 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Deactivate Sketch");
    
    theSession.ActiveSketch.Deactivate(NXOpen.Sketch.ViewReorient.False, NXOpen.Sketch.UpdateLevel.Model);
    
    // ----------------------------------------------
    //   Menu: Insert->Design Feature->Extrude...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId86;
    markId86 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.Features.ExtrudeBuilder extrudeBuilder3;
    extrudeBuilder3 = workPart.Features.CreateExtrudeBuilder(nullNXOpen_Features_Feature);
    
    NXOpen.Section section3;
    section3 = workPart.Sections.CreateSection(0.0095, 0.01, 0.5);
    
    extrudeBuilder3.Section = section3;
    
    extrudeBuilder3.AllowSelfIntersectingSection(true);
    
    NXOpen.Expression expression45;
    expression45 = workPart.Expressions.CreateSystemExpressionWithUnits("2.00", unit2);
    
    extrudeBuilder3.DistanceTolerance = 0.01;
    
    extrudeBuilder3.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;
    
    NXOpen.Body[] targetBodies15 = new NXOpen.Body[1];
    targetBodies15[0] = nullNXOpen_Body;
    extrudeBuilder3.BooleanOperation.SetTargetBodies(targetBodies15);
    
    extrudeBuilder3.Limits.StartExtend.Value.RightHandSide = "0";
    
    extrudeBuilder3.Limits.EndExtend.Value.RightHandSide = "-52";
    
    extrudeBuilder3.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Subtract;
    
    NXOpen.Body[] targetBodies16 = new NXOpen.Body[1];
    targetBodies16[0] = body1;
    extrudeBuilder3.BooleanOperation.SetTargetBodies(targetBodies16);
    
    extrudeBuilder3.Draft.FrontDraftAngle.RightHandSide = "2";
    
    extrudeBuilder3.Draft.BackDraftAngle.RightHandSide = "2";
    
    extrudeBuilder3.Offset.StartOffset.RightHandSide = "0";
    
    extrudeBuilder3.Offset.EndOffset.RightHandSide = "5";
    
    NXOpen.GeometricUtilities.SmartVolumeProfileBuilder smartVolumeProfileBuilder3;
    smartVolumeProfileBuilder3 = extrudeBuilder3.SmartVolumeProfile;
    
    smartVolumeProfileBuilder3.OpenProfileSmartVolumeOption = false;
    
    smartVolumeProfileBuilder3.CloseProfileRule = NXOpen.GeometricUtilities.SmartVolumeProfileBuilder.CloseProfileRuleType.Fci;
    
    theSession.SetUndoMarkName(markId86, "Extrude Dialog");
    
    section3.DistanceTolerance = 0.01;
    
    section3.ChainingTolerance = 0.0095;
    
    section3.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.OnlyCurves);
    
    NXOpen.Session.UndoMarkId markId87;
    markId87 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "section mark");
    
    NXOpen.Session.UndoMarkId markId88;
    markId88 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, null);
    
    NXOpen.Features.Feature[] features3 = new NXOpen.Features.Feature[1];
    NXOpen.Features.SketchFeature sketchFeature3 = (NXOpen.Features.SketchFeature)feature6;
    features3[0] = sketchFeature3;
    NXOpen.CurveFeatureRule curveFeatureRule3;
    curveFeatureRule3 = workPart.ScRuleFactory.CreateRuleCurveFeature(features3);
    
    section3.AllowSelfIntersection(true);
    
    NXOpen.SelectionIntentRule[] rules3 = new NXOpen.SelectionIntentRule[1];
    rules3[0] = curveFeatureRule3;
    NXOpen.Point3d helpPoint3 = new NXOpen.Point3d(0.0, 43.0271419958963, 34.8351248710814);
    section3.AddToSection(rules3, arc7, nullNXOpen_NXObject, nullNXOpen_NXObject, helpPoint3, NXOpen.Section.Mode.Create, false);
    
    theSession.DeleteUndoMark(markId88, null);
    
    NXOpen.Direction direction3;
    direction3 = workPart.Directions.CreateDirection(sketch3, NXOpen.Sense.Forward, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    extrudeBuilder3.Direction = direction3;
    
    theSession.DeleteUndoMark(markId87, null);
    
    extrudeBuilder3.Limits.EndExtend.Value.RightHandSide = "10";
    
    NXOpen.Session.UndoMarkId markId89;
    markId89 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    theSession.DeleteUndoMark(markId89, null);
    
    NXOpen.Session.UndoMarkId markId90;
    markId90 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    extrudeBuilder3.ParentFeatureInternal = false;
    
    NXOpen.Features.Feature feature7;
    try
    {
      // Tool body completely outside target body.
      feature7 = extrudeBuilder3.CommitFeature();
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(670030);
    }
    
    theSession.UndoToMarkWithStatus(markId90, null);
    
    theSession.DeleteUndoMark(markId90, null);
    
    extrudeBuilder3.Limits.EndExtend.Value.RightHandSide = "-10";
    
    NXOpen.Session.UndoMarkId markId91;
    markId91 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    theSession.DeleteUndoMark(markId91, null);
    
    NXOpen.Session.UndoMarkId markId92;
    markId92 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Extrude");
    
    extrudeBuilder3.ParentFeatureInternal = false;
    
    NXOpen.Features.Feature feature8;
    feature8 = extrudeBuilder3.CommitFeature();
    
    theSession.DeleteUndoMark(markId92, null);
    
    theSession.SetUndoMarkName(markId86, "Extrude");
    
    NXOpen.Expression expression46 = extrudeBuilder3.Limits.StartExtend.Value;
    NXOpen.Expression expression47 = extrudeBuilder3.Limits.EndExtend.Value;
    extrudeBuilder3.Destroy();
    
    workPart.Expressions.Delete(expression45);
    
    // ----------------------------------------------
    //   Menu: Insert->Detail Feature->Edge Blend...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId93;
    markId93 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.Features.EdgeBlendBuilder edgeBlendBuilder1;
    edgeBlendBuilder1 = workPart.Features.CreateEdgeBlendBuilder(nullNXOpen_Features_Feature);
    
    NXOpen.Expression expression48;
    expression48 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression49;
    expression49 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.GeometricUtilities.BlendLimitsData blendLimitsData1;
    blendLimitsData1 = edgeBlendBuilder1.LimitsListData;
    
    NXOpen.Point3d origin3 = new NXOpen.Point3d(0.0, 0.0, 0.0);
    NXOpen.Vector3d normal1 = new NXOpen.Vector3d(0.0, 0.0, 1.0);
    NXOpen.Plane plane1;
    plane1 = workPart.Planes.CreatePlane(origin3, normal1, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    NXOpen.GeometricUtilities.FacePlaneSelectionBuilder facePlaneSelectionBuilder1;
    facePlaneSelectionBuilder1 = workPart.FacePlaneSelectionBuilderData.Create();
    
    NXOpen.Expression expression50;
    expression50 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression51;
    expression51 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    theSession.SetUndoMarkName(markId93, "Edge Blend Dialog");
    
    workPart.FacePlaneSelectionBuilderData.Destroy(facePlaneSelectionBuilder1);
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression51);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression50);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    edgeBlendBuilder1.Destroy();
    
    workPart.Expressions.Delete(expression48);
    
    workPart.Expressions.Delete(expression49);
    
    theSession.UndoToMark(markId93, null);
    
    theSession.DeleteUndoMark(markId93, null);
    
    // ----------------------------------------------
    //   Menu: Insert->Detail Feature->Edge Blend...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId94;
    markId94 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.Features.EdgeBlendBuilder edgeBlendBuilder2;
    edgeBlendBuilder2 = workPart.Features.CreateEdgeBlendBuilder(nullNXOpen_Features_Feature);
    
    NXOpen.Expression expression52;
    expression52 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression53;
    expression53 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.GeometricUtilities.BlendLimitsData blendLimitsData2;
    blendLimitsData2 = edgeBlendBuilder2.LimitsListData;
    
    NXOpen.Point3d origin4 = new NXOpen.Point3d(0.0, 0.0, 0.0);
    NXOpen.Vector3d normal2 = new NXOpen.Vector3d(0.0, 0.0, 1.0);
    NXOpen.Plane plane2;
    plane2 = workPart.Planes.CreatePlane(origin4, normal2, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    NXOpen.GeometricUtilities.FacePlaneSelectionBuilder facePlaneSelectionBuilder2;
    facePlaneSelectionBuilder2 = workPart.FacePlaneSelectionBuilderData.Create();
    
    NXOpen.Expression expression54;
    expression54 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression55;
    expression55 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    theSession.SetUndoMarkName(markId94, "Edge Blend Dialog");
    
    // ----------------------------------------------
    //   Menu: Orient View->Left
    // ----------------------------------------------
    workPart.ModelingViews.WorkView.Orient(NXOpen.View.Canned.Left, NXOpen.View.ScaleAdjustment.Fit);
    
    // ----------------------------------------------
    //   Menu: Orient View->Top
    // ----------------------------------------------
    workPart.ModelingViews.WorkView.Orient(NXOpen.View.Canned.Top, NXOpen.View.ScaleAdjustment.Fit);
    
    NXOpen.ScCollector scCollector1;
    scCollector1 = workPart.ScCollectors.CreateCollector();
    
    NXOpen.Edge[] seedEdges1 = new NXOpen.Edge[1];
    NXOpen.Edge edge3 = (NXOpen.Edge)extrude1.FindObject("EDGE * 130 * 140 {(10,52,40)(5,52,40)(0,52,40) EXTRUDE(2)}");
    seedEdges1[0] = edge3;
    NXOpen.EdgeMultipleSeedTangentRule edgeMultipleSeedTangentRule1;
    edgeMultipleSeedTangentRule1 = workPart.ScRuleFactory.CreateRuleEdgeMultipleSeedTangent(seedEdges1, 0.5, true);
    
    NXOpen.SelectionIntentRule[] rules4 = new NXOpen.SelectionIntentRule[1];
    rules4[0] = edgeMultipleSeedTangentRule1;
    scCollector1.ReplaceRules(rules4, false);
    
    NXOpen.Session.UndoMarkId markId95;
    markId95 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Edge Blend");
    
    theSession.DeleteUndoMark(markId95, null);
    
    NXOpen.Session.UndoMarkId markId96;
    markId96 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Edge Blend");
    
    edgeBlendBuilder2.Tolerance = 0.01;
    
    edgeBlendBuilder2.AllInstancesOption = false;
    
    edgeBlendBuilder2.RemoveSelfIntersection = true;
    
    edgeBlendBuilder2.PatchComplexGeometryAreas = true;
    
    edgeBlendBuilder2.LimitFailingAreas = true;
    
    edgeBlendBuilder2.ConvexConcaveY = false;
    
    edgeBlendBuilder2.RollOverSmoothEdge = true;
    
    edgeBlendBuilder2.RollOntoEdge = true;
    
    edgeBlendBuilder2.MoveSharpEdge = true;
    
    edgeBlendBuilder2.TrimmingOption = false;
    
    edgeBlendBuilder2.OverlapOption = NXOpen.Features.EdgeBlendBuilder.Overlap.AnyConvexityRollOver;
    
    edgeBlendBuilder2.BlendOrder = NXOpen.Features.EdgeBlendBuilder.OrderOfBlending.ConvexFirst;
    
    edgeBlendBuilder2.SetbackOption = NXOpen.Features.EdgeBlendBuilder.Setback.SeparateFromCorner;
    
    edgeBlendBuilder2.BlendFaceContinuity = NXOpen.Features.EdgeBlendBuilder.FaceContinuity.Tangent;
    
    int csIndex1;
    csIndex1 = edgeBlendBuilder2.AddChainset(scCollector1, "9");
    
    NXOpen.Features.Feature feature9;
    feature9 = edgeBlendBuilder2.CommitFeature();
    
    theSession.DeleteUndoMark(markId96, null);
    
    theSession.SetUndoMarkName(markId94, "Edge Blend");
    
    workPart.FacePlaneSelectionBuilderData.Destroy(facePlaneSelectionBuilder2);
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression55);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression54);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    edgeBlendBuilder2.Destroy();
    
    workPart.Expressions.Delete(expression52);
    
    workPart.Expressions.Delete(expression53);
    
    // ----------------------------------------------
    //   Menu: Insert->Detail Feature->Edge Blend...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId97;
    markId97 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
    
    NXOpen.Features.EdgeBlendBuilder edgeBlendBuilder3;
    edgeBlendBuilder3 = workPart.Features.CreateEdgeBlendBuilder(nullNXOpen_Features_Feature);
    
    NXOpen.Expression expression56;
    expression56 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression57;
    expression57 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.GeometricUtilities.BlendLimitsData blendLimitsData3;
    blendLimitsData3 = edgeBlendBuilder3.LimitsListData;
    
    NXOpen.Point3d origin5 = new NXOpen.Point3d(0.0, 0.0, 0.0);
    NXOpen.Vector3d normal3 = new NXOpen.Vector3d(0.0, 0.0, 1.0);
    NXOpen.Plane plane3;
    plane3 = workPart.Planes.CreatePlane(origin5, normal3, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    NXOpen.GeometricUtilities.FacePlaneSelectionBuilder facePlaneSelectionBuilder3;
    facePlaneSelectionBuilder3 = workPart.FacePlaneSelectionBuilderData.Create();
    
    NXOpen.Expression expression58;
    expression58 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression59;
    expression59 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    theSession.SetUndoMarkName(markId97, "Edge Blend Dialog");
    
    NXOpen.ScCollector scCollector2;
    scCollector2 = workPart.ScCollectors.CreateCollector();
    
    NXOpen.Edge[] seedEdges2 = new NXOpen.Edge[1];
    NXOpen.Edge edge4 = (NXOpen.Edge)extrude1.FindObject("EDGE * 130 * 160 {(0,0,40)(5,0,40)(10,0,40) EXTRUDE(2)}");
    seedEdges2[0] = edge4;
    NXOpen.EdgeMultipleSeedTangentRule edgeMultipleSeedTangentRule2;
    edgeMultipleSeedTangentRule2 = workPart.ScRuleFactory.CreateRuleEdgeMultipleSeedTangent(seedEdges2, 0.5, true);
    
    NXOpen.SelectionIntentRule[] rules5 = new NXOpen.SelectionIntentRule[1];
    rules5[0] = edgeMultipleSeedTangentRule2;
    scCollector2.ReplaceRules(rules5, false);
    
    NXOpen.Session.UndoMarkId markId98;
    markId98 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Edge Blend");
    
    theSession.DeleteUndoMark(markId98, null);
    
    NXOpen.Session.UndoMarkId markId99;
    markId99 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Edge Blend");
    
    edgeBlendBuilder3.Tolerance = 0.01;
    
    edgeBlendBuilder3.AllInstancesOption = false;
    
    edgeBlendBuilder3.RemoveSelfIntersection = true;
    
    edgeBlendBuilder3.PatchComplexGeometryAreas = true;
    
    edgeBlendBuilder3.LimitFailingAreas = true;
    
    edgeBlendBuilder3.ConvexConcaveY = false;
    
    edgeBlendBuilder3.RollOverSmoothEdge = true;
    
    edgeBlendBuilder3.RollOntoEdge = true;
    
    edgeBlendBuilder3.MoveSharpEdge = true;
    
    edgeBlendBuilder3.TrimmingOption = false;
    
    edgeBlendBuilder3.OverlapOption = NXOpen.Features.EdgeBlendBuilder.Overlap.AnyConvexityRollOver;
    
    edgeBlendBuilder3.BlendOrder = NXOpen.Features.EdgeBlendBuilder.OrderOfBlending.ConvexFirst;
    
    edgeBlendBuilder3.SetbackOption = NXOpen.Features.EdgeBlendBuilder.Setback.SeparateFromCorner;
    
    edgeBlendBuilder3.BlendFaceContinuity = NXOpen.Features.EdgeBlendBuilder.FaceContinuity.Tangent;
    
    int csIndex2;
    csIndex2 = edgeBlendBuilder3.AddChainset(scCollector2, "9");
    
    NXOpen.Features.Feature feature10;
    feature10 = edgeBlendBuilder3.CommitFeature();
    
    theSession.DeleteUndoMark(markId99, null);
    
    theSession.SetUndoMarkName(markId97, "Edge Blend");
    
    workPart.FacePlaneSelectionBuilderData.Destroy(facePlaneSelectionBuilder3);
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression59);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression58);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    edgeBlendBuilder3.Destroy();
    
    workPart.Expressions.Delete(expression56);
    
    workPart.Expressions.Delete(expression57);
    
    NXOpen.Session.UndoMarkId markId100;
    markId100 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Redefine Feature");
    
    NXOpen.Session.UndoMarkId markId101;
    markId101 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Redefine Feature");
    
    NXOpen.Features.EdgeBlend edgeBlend1 = (NXOpen.Features.EdgeBlend)feature9;
    workPart.Features.SetEditWithRollbackFeature(edgeBlend1);
    
    theSession.UpdateManager.InterpartDelay = true;
    
    NXOpen.Features.Extrude extrude2 = (NXOpen.Features.Extrude)feature8;
    extrude2.MakeCurrentFeature();
    
    NXOpen.Session.UndoMarkId markId102;
    markId102 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");
    
    NXOpen.Features.EdgeBlendBuilder edgeBlendBuilder4;
    edgeBlendBuilder4 = workPart.Features.CreateEdgeBlendBuilder(edgeBlend1);
    
    NXOpen.ScCollector scCollector3;
    NXOpen.Expression expression60;
    bool isvalid1;
    edgeBlendBuilder4.GetChainsetAndStatus(0, out scCollector3, out expression60, out isvalid1);
    
    NXOpen.Expression expression61;
    expression61 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression62;
    expression62 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.GeometricUtilities.BlendLimitsData blendLimitsData4;
    blendLimitsData4 = edgeBlendBuilder4.LimitsListData;
    
    NXOpen.Point3d origin6 = new NXOpen.Point3d(0.0, 0.0, 0.0);
    NXOpen.Vector3d normal4 = new NXOpen.Vector3d(0.0, 0.0, 1.0);
    NXOpen.Plane plane4;
    plane4 = workPart.Planes.CreatePlane(origin6, normal4, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    NXOpen.GeometricUtilities.FacePlaneSelectionBuilder facePlaneSelectionBuilder4;
    facePlaneSelectionBuilder4 = workPart.FacePlaneSelectionBuilderData.Create();
    
    NXOpen.Expression expression63;
    expression63 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression64;
    expression64 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    theSession.SetUndoMarkName(markId102, "Edge Blend Dialog");
    
    // ----------------------------------------------
    //   Dialog Begin Edge Blend
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId103;
    markId103 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Edge Blend");
    
    theSession.DeleteUndoMark(markId103, null);
    
    NXOpen.Session.UndoMarkId markId104;
    markId104 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Edge Blend");
    
    edgeBlendBuilder4.Tolerance = 0.01;
    
    edgeBlendBuilder4.AllInstancesOption = false;
    
    edgeBlendBuilder4.RemoveSelfIntersection = true;
    
    edgeBlendBuilder4.PatchComplexGeometryAreas = true;
    
    edgeBlendBuilder4.LimitFailingAreas = true;
    
    edgeBlendBuilder4.ConvexConcaveY = false;
    
    edgeBlendBuilder4.RollOverSmoothEdge = true;
    
    edgeBlendBuilder4.RollOntoEdge = true;
    
    edgeBlendBuilder4.MoveSharpEdge = true;
    
    edgeBlendBuilder4.TrimmingOption = false;
    
    edgeBlendBuilder4.OverlapOption = NXOpen.Features.EdgeBlendBuilder.Overlap.AnyConvexityRollOver;
    
    edgeBlendBuilder4.BlendOrder = NXOpen.Features.EdgeBlendBuilder.OrderOfBlending.ConvexFirst;
    
    edgeBlendBuilder4.SetbackOption = NXOpen.Features.EdgeBlendBuilder.Setback.SeparateFromCorner;
    
    edgeBlendBuilder4.BlendFaceContinuity = NXOpen.Features.EdgeBlendBuilder.FaceContinuity.Tangent;
    
    int csIndex3;
    csIndex3 = edgeBlendBuilder4.AddChainset(scCollector3, "8");
    
    NXOpen.Features.Feature feature11;
    feature11 = edgeBlendBuilder4.CommitFeature();
    
    theSession.DeleteUndoMark(markId104, null);
    
    theSession.SetUndoMarkName(markId102, "Edge Blend");
    
    workPart.FacePlaneSelectionBuilderData.Destroy(facePlaneSelectionBuilder4);
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression64);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression63);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    edgeBlendBuilder4.Destroy();
    
    workPart.Expressions.Delete(expression61);
    
    workPart.Expressions.Delete(expression62);
    
    theSession.DeleteUndoMark(markId102, null);
    
    theSession.UpdateManager.InterpartDelay = false;
    
    NXOpen.Features.EdgeBlend edgeBlend2 = (NXOpen.Features.EdgeBlend)feature10;
    edgeBlend2.MakeCurrentFeature();
    
    theSession.UpdateManager.InterpartDelay = false;
    
    workPart.Features.SetEditWithRollbackFeature(nullNXOpen_Features_Feature);
    
    theSession.DeleteUndoMark(markId101, null);
    
    NXOpen.Session.UndoMarkId markId105;
    markId105 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Redefine Feature");
    
    NXOpen.Session.UndoMarkId markId106;
    markId106 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Redefine Feature");
    
    workPart.Features.SetEditWithRollbackFeature(edgeBlend2);
    
    theSession.UpdateManager.InterpartDelay = true;
    
    NXOpen.Features.EdgeBlend edgeBlend3 = (NXOpen.Features.EdgeBlend)feature11;
    edgeBlend3.MakeCurrentFeature();
    
    NXOpen.Session.UndoMarkId markId107;
    markId107 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");
    
    NXOpen.Features.EdgeBlendBuilder edgeBlendBuilder5;
    edgeBlendBuilder5 = workPart.Features.CreateEdgeBlendBuilder(edgeBlend2);
    
    NXOpen.ScCollector scCollector4;
    NXOpen.Expression expression65;
    bool isvalid2;
    edgeBlendBuilder5.GetChainsetAndStatus(0, out scCollector4, out expression65, out isvalid2);
    
    NXOpen.Expression expression66;
    expression66 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression67;
    expression67 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.GeometricUtilities.BlendLimitsData blendLimitsData5;
    blendLimitsData5 = edgeBlendBuilder5.LimitsListData;
    
    NXOpen.Point3d origin7 = new NXOpen.Point3d(0.0, 0.0, 0.0);
    NXOpen.Vector3d normal5 = new NXOpen.Vector3d(0.0, 0.0, 1.0);
    NXOpen.Plane plane5;
    plane5 = workPart.Planes.CreatePlane(origin7, normal5, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    NXOpen.GeometricUtilities.FacePlaneSelectionBuilder facePlaneSelectionBuilder5;
    facePlaneSelectionBuilder5 = workPart.FacePlaneSelectionBuilderData.Create();
    
    NXOpen.Expression expression68;
    expression68 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    NXOpen.Expression expression69;
    expression69 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    theSession.SetUndoMarkName(markId107, "Edge Blend Dialog");
    
    // ----------------------------------------------
    //   Dialog Begin Edge Blend
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId108;
    markId108 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Edge Blend");
    
    theSession.DeleteUndoMark(markId108, null);
    
    NXOpen.Session.UndoMarkId markId109;
    markId109 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Edge Blend");
    
    edgeBlendBuilder5.Tolerance = 0.01;
    
    edgeBlendBuilder5.AllInstancesOption = false;
    
    edgeBlendBuilder5.RemoveSelfIntersection = true;
    
    edgeBlendBuilder5.PatchComplexGeometryAreas = true;
    
    edgeBlendBuilder5.LimitFailingAreas = true;
    
    edgeBlendBuilder5.ConvexConcaveY = false;
    
    edgeBlendBuilder5.RollOverSmoothEdge = true;
    
    edgeBlendBuilder5.RollOntoEdge = true;
    
    edgeBlendBuilder5.MoveSharpEdge = true;
    
    edgeBlendBuilder5.TrimmingOption = false;
    
    edgeBlendBuilder5.OverlapOption = NXOpen.Features.EdgeBlendBuilder.Overlap.AnyConvexityRollOver;
    
    edgeBlendBuilder5.BlendOrder = NXOpen.Features.EdgeBlendBuilder.OrderOfBlending.ConvexFirst;
    
    edgeBlendBuilder5.SetbackOption = NXOpen.Features.EdgeBlendBuilder.Setback.SeparateFromCorner;
    
    edgeBlendBuilder5.BlendFaceContinuity = NXOpen.Features.EdgeBlendBuilder.FaceContinuity.Tangent;
    
    int csIndex4;
    csIndex4 = edgeBlendBuilder5.AddChainset(scCollector4, "8");
    
    NXOpen.Features.Feature feature12;
    feature12 = edgeBlendBuilder5.CommitFeature();
    
    theSession.DeleteUndoMark(markId109, null);
    
    theSession.SetUndoMarkName(markId107, "Edge Blend");
    
    workPart.FacePlaneSelectionBuilderData.Destroy(facePlaneSelectionBuilder5);
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression69);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    try
    {
      // Expression is still in use.
      workPart.Expressions.Delete(expression68);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    edgeBlendBuilder5.Destroy();
    
    workPart.Expressions.Delete(expression66);
    
    workPart.Expressions.Delete(expression67);
    
    theSession.DeleteUndoMark(markId107, null);
    
    theSession.UpdateManager.InterpartDelay = false;
    
    NXOpen.Features.EdgeBlend edgeBlend4 = (NXOpen.Features.EdgeBlend)feature12;
    edgeBlend4.MakeCurrentFeature();
    
    theSession.UpdateManager.InterpartDelay = false;
    
    workPart.Features.SetEditWithRollbackFeature(nullNXOpen_Features_Feature);
    
    theSession.DeleteUndoMark(markId106, null);
    
    // ----------------------------------------------
    //   Menu: File->Save
    // ----------------------------------------------
    NXOpen.PartSaveStatus partSaveStatus1;
    partSaveStatus1 = workPart.Save(NXOpen.BasePart.SaveComponents.True, NXOpen.BasePart.CloseAfterSave.False);
    
    partSaveStatus1.Dispose();
    // ----------------------------------------------
    //   Menu: Tools->Journal->Stop Recording
    // ----------------------------------------------
    
  }
  public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}
