#Author-     Devin Adams
#For-        Dr. Salmon, ME 578
#Date-       19 Jan 2018
#Description-Lab 1-1 - Deliverable. Create 2 circles: One with radius 2cm and 
#            the other with radius 0.156cm. The smaller circle should be 
#            centered on the edge of the first circle.

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
        sketch1 = sketches.add(xyPlane) 
        
        #Draw the circle with radius of 2cm centered at the origin
        circles1 = sketch1.sketchCurves.sketchCircles
        circle1 = circles1.addByCenterRadius(adsk.core.Point3D.create(0,0,0),radius_1)
        
        
        #Create the first, larger circle in sketch 1
        sketch2 = sketches.add(xyPlane)
        
        #Draw the circle with radius of 0.156cm centered on the edge of circle 1
        circles2 = sketch2.sketchCurves.sketchCircles
        circle2 = circles2.addByCenterRadius(adsk.core.Point3D.create(radius_1,0,0),radius_2)
        

        
        #Display a message box with text
        ui.messageBox('Circles with a 2cm and a 0.156cm radius created.')

    except:
        if ui:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))