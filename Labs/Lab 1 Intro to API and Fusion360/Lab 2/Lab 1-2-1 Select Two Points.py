#Author-     Devin Adams
#For-        Dr. Salmon, ME 578
#Date-       19 Jan 2018
#Description-Lab 1-2-1-Have the user select two points.
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
        ui.messageBox('Select two cylindrical faces')
        
        #Get the location of the center of the first cylinder
        selected_cylinder_face_1 = ui.selectEntity("Select First Cylinder", "CylindricalFaces")
        selected_cylinder_face_1_value = selected_cylinder_face_1.point
        
        #Get the location of the center of the second cyliner
        selected_cylinder_face_2 = ui.selectEntity("Select Second Cylinder", "CylindricalFaces")
        selected_cylinder_face_2_value = selected_cylinder_face_1.point
        
        ui.messageBox('Two cylindrical faces successfully selected')
        

    except:
        if ui:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))
