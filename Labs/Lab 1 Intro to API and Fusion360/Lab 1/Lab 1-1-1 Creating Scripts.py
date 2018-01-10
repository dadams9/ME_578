#Author-     Devin Adams
#For-        Dr. Salmon, ME 578
#Date-       19 Jan 2018
#Description-Lab 1 Creating scripts example following the tutorial videos for
#            Lab 1. It also creates the sketch of a circle.

#import the necessary libraries
import adsk.core, adsk.fusion, adsk.cam, traceback

def run(context):
    ui = None
    try:
        
        #Document Setup
        app = adsk.core.Application.get()
        ui  = app.userInterface
        design = app.activeProduct
        rootComp = design.rootComponent
        
        #Display a message box with text
        ui.messageBox('ME 578 API Hello World')

    except:
        if ui:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))
