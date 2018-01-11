#Author-     Devin Adams
#For-        Dr. Salmon, ME 578
#Date-       19 Jan 2018
#Description-Lab 1-2-2 - Deliverable. Create 2 circles: One with radius 2cm and 
#            the other with radius 0.156cm. The smaller circle should be 
#            centered on the edge of the first circle. Query their profiles

#import the necessary libraries
import adsk.core, adsk.fusion, adsk.cam, traceback, math

def run(context):
    ui = None
    try:
        
        #Create variables and initialize them (length in cm)
        radius_1 = 2
        radius_2 = 0.156
        
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
        
        #Display a message box with text
        ui.messageBox('Circles with a 2cm and a 0.156cm radius created.')
        
        #Extrude the large circle
        extrudes = rootComp.features.extrudeFeatures
        large_extrude_input = extrudes.createInput(large_circle_profile, adsk.fusion.FeatureOperations.NewBodyFeatureOperation)

        #Assign a distance for the extrude
        distanceLargeExtrude = adsk.core.ValueInput.createByReal(20)
        #Put the distance value into the input. False is for symmetric
        large_extrude_input.setDistanceExtent(False, distanceLargeExtrude)

        #Create the extrusion
        large_extrude = extrudes.add(large_extrude_input)
        
    except:
        if ui:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))