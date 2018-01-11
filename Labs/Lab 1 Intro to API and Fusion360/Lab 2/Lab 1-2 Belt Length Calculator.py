#Author-     Devin Adams
#For-        Dr. Salmon, ME 578
#Date-       19 Jan 2018
#Description-Lab 1-2 Calculate the belt length required to ecncompass two points.
import adsk.core, adsk.fusion, adsk.cam, traceback, math

def run(context):
    ui = None
    try:
        #Setup the Document
        app = adsk.core.Application.get()
        ui  = app.userInterface
        design = app.activeProduct
        
        #Get the root component of the active design
        rootComp = design.rootComponent
        
        #Prompt the user to select two cylindrical faces
        ui.messageBox('Select two points')
        
        #Get the location of the center of the first cylinder
        selected_point1 = ui.selectEntity("Select First Point", "CylindricalFaces,SketchPoints,Vertices,ConstructionPoints")
        selected_point1_value = selected_point1.point
        
        #Get the location of the center of the second cyliner
        selected_point2= ui.selectEntity("Select Second Point", "CylindricalFaces,SketchPoints,Vertices,ConstructionPoints")
        selected_point2_value = selected_point2.point
        
        #Calculate and display the distance between the two points
        distance = selected_point1_value.distanceTo(selected_point2_value)
        unitsMgr = design.unitsManager
        displayDistance = unitsMgr.formatInternalValue(distance, unitsMgr.defaultLengthUnits,True)
        ui.messageBox('Distance between the two points is:'+displayDistance)
        
        
        #Calculate the belt length around pulleys located at the selected points
        
        #Define diameter variables
        D1 = 2
        D2 = 1
        B = 2*math.acos((D2-D1)/(2*distance))
        
        BeltLength = 2*distance*math.sin(B/2)+(math.pi/2)*(D1+D2)+(math.pi/180)*(90-B/2)*(D2-D1)
        
        #Display the length of the belt
        displayBeltLength = unitsMgr.formatInternalValue(BeltLength, unitsMgr.defaultLengthUnits,True) 
        ui.messageBox('Belt length is:'+displayBeltLength)
        

    except:
        if ui:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))
