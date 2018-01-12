#Author-     Devin Adams
#For-        Dr. Salmon, ME 578
#Date-       19 Jan 2018
#Description-Lab 1-2 - Deliverable. Create 2 pulleys

#import the necessary libraries
import adsk.core, adsk.fusion, adsk.cam, traceback, math

def run(context):
    ui = None
    try:
        
        #Create variables and initialize them (length in cm)
        num_teeth = 25
        distance_between_teeth = 0.5
        
        radius_1 = (num_teeth*distance_between_teeth)/(2*math.pi)
        radius_2 = 0.156
        radius_3 = 0.5
        radius_4 = radius_1+0.1*radius_1
        
        #Setup the Document
        app = adsk.core.Application.get()
        ui  = app.userInterface
        design = app.activeProduct
        
        #Get the root component of the active design
        rootComp = design.rootComponent
        
        #Create sketches in the XY plane
        sketches = rootComp.sketches
        xyPlane = rootComp.xYConstructionPlane
        
        #Create the first, larger circle in sketch 1
        large_circle_sketch = sketches.add(xyPlane) 
        
        #Draw the circle with radius of 2cm centered at the origin
        circles1 = large_circle_sketch.sketchCurves.sketchCircles
        large_circle = circles1.addByCenterRadius(adsk.core.Point3D.create(0,0,0),radius_1)
        
        #Get the profile of the large circle
        large_circle_profile = large_circle_sketch.profiles.item(0)
        
        #Create the second, smaller circle in sketch 2
        small_circle_sketch = sketches.add(xyPlane)
        
        #Draw the circle with radius of 0.156cm centered on the edge of circle 1
        circles2 = small_circle_sketch.sketchCurves.sketchCircles
        small_circle = circles2.addByCenterRadius(adsk.core.Point3D.create(radius_1,0,0),radius_2)
        
        #Get the profile of the small circle
        small_circle_profile = small_circle_sketch.profiles.item(0)
        
        #Create the third, center circle in sketch 3
        center_circle_sketch = sketches.add(xyPlane)
        
        #Draw the circle with radius of 0.5cm centered in the middle of circle 1
        circles3 = center_circle_sketch.sketchCurves.sketchCircles
        center_circle = circles3.addByCenterRadius(adsk.core.Point3D.create(0,0,-0.2),radius_3)
        
        #Get the profile of the small circle
        center_circle_profile = center_circle_sketch.profiles.item(0)
        
        #Create the fourth, base circle in sketch 4
        base_circle_sketch = sketches.add(xyPlane)
        
        #Draw the circle with radius of 0.5cm centered in the middle of circle 1
        circles4 = base_circle_sketch.sketchCurves.sketchCircles
        base_circle = circles4.addByCenterRadius(adsk.core.Point3D.create(0,0,-0.2),radius_4)
        
        #Get the profile of the small circle
        base_circle_profile = base_circle_sketch.profiles.item(0)
        
        
        #Display a message box with text
        #ui.messageBox('Circles with a 2cm and a 0.156cm radius created.')
        
        #Extrude the four circles
        extrudes = rootComp.features.extrudeFeatures
        large_extrude_input = extrudes.createInput(large_circle_profile, adsk.fusion.FeatureOperations.NewBodyFeatureOperation)
        small_extrude_input = extrudes.createInput(small_circle_profile, adsk.fusion.FeatureOperations.CutFeatureOperation)
        center_extrude_input = extrudes.createInput(center_circle_profile, adsk.fusion.FeatureOperations.CutFeatureOperation)
        base_extrude_input = extrudes.createInput(base_circle_profile, adsk.fusion.FeatureOperations.NewBodyFeatureOperation)
        
        #Assign a distance for the extrude
        distanceLargeExtrude = adsk.core.ValueInput.createByReal(1.5)
        distanceSmallExtrude = adsk.core.ValueInput.createByReal(1.5)
        distanceCenterExtrude = adsk.core.ValueInput.createByReal(1.7)
        distanceBaseExtrude = adsk.core.ValueInput.createByReal(0.2)
        #distanceCenterExtrude = adsk.core.ValueInput.createByReal(-0.2)
        
        large_extrude_input.setDistanceExtent(False, distanceLargeExtrude)
        small_extrude_input.setDistanceExtent(False, distanceSmallExtrude)
        center_extrude_input.setDistanceExtent(False, distanceCenterExtrude)
        base_extrude_input.setDistanceExtent(False, distanceBaseExtrude)

        #Create the extrusion with one small circle cut out
        large_extrude = extrudes.add(large_extrude_input)
        small_extrude = extrudes.add(small_extrude_input)
        base_extrude = extrudes.add(base_extrude_input) 
        center_extrude = extrudes.add(center_extrude_input)
        
        #Create a Pattern Feature to cut out the teeth for the pulley
        #Get the circular patterns collection
        circularPatterns = rootComp.features.circularPatternFeatures
        
        #Create the entity collection to pass into the input
        inputEntitiesCollection = adsk.core.ObjectCollection.create()
        inputEntitiesCollection.add(small_extrude)
        inputAxis = rootComp.zConstructionAxis
        
        #Create the circular pattern
        circularPatternInput = circularPatterns.createInput(inputEntitiesCollection, inputAxis)
        circularPatternInput.quantity = adsk.core.ValueInput.createByReal(num_teeth)
        circularPatternInput.totalAngle = adsk.core.ValueInput.createByString('360 deg')
        circularPatternInput.isSymmetric = False
        
        #Creating the circular pattern
        circularPattern = circularPatterns.add(circularPatternInput)
        
        
        
    except:
        if ui:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))