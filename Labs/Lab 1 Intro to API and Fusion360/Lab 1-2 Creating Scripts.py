#Author-     Devin Adams
#For-        Dr. Salmon, ME 578
#Date-       19 Jan 2018
#Description-Lab 1-2 Creating script to use sketches to create a circle

#import the necessary libraries
import adsk.core, adsk.fusion, adsk.cam, traceback, math

def run(context):
    ui = None
    try:
        
        #Create variables and initialize them
        radius_1 = 2
        radius_2 = 0.156
        
        #Setup the Document
        app = adsk.core.Application.get()
        ui  = app.userInterface
        design = app.activeProduct
        
        #Get the root component of the active design
        rootComp = design.rootComponent
        
        #Create sketch in the XY plane
        sketches = rootComp.sketches
        xyPlane = rootComp.xYConstructionPlane
        sketch = sketches.add(xyPlane)    
        
        #Draw the circle with radius of 2cm centered at the origin
        circles = sketch.sketchCurves.sketchCircles
        circle1 = circles.addByCenterRadius(adsk.core.Point3D.create(0,0,0),radius_1)
        
        #circle2 = circles.addByCenterRadius(adsk.core.Point3D.create(radius_1,0,0),radius_2)
        
        #Display a message box with text
        ui.messageBox('Circle with a 2cm radius created.')

    except:
        if ui:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))
