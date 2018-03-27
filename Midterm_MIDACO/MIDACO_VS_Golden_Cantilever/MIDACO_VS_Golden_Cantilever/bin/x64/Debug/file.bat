/NOPR   ! Suppress printing of UNDO process 
/PMACRO ! Echo following commands to log
FINISH  ! Make sure we are at BEGIN level   
/CLEAR,NOSTART  ! Clear model since no SAVE found   
! WE SUGGEST YOU REMOVE THIS LINE AND THE FOLLOWING STARTUP LINES   
!*  
/AUX15  
!*  
/CWD,'J:\Documents\ME578\Midterm\Working_Directory\'
IOPTN,IGES,SMOOTH   
IOPTN,MERGE,YES 
IOPTN,SOLID,YES 
IOPTN,SMALL,YES 
IOPTN,GTOLER, DEFA  
IGESIN,'Optimized_beam','igs','J:\Documents\ME578\Midterm\Working_Directory'
VPLOT   
!*  
FINISH  
/PREP7  
!*  
ET,1,SOLID186   
!*  
!*  
MPTEMP,,,,,,,,  
MPTEMP,1,0  
MPDATA,EX,1,,300e9  
MPDATA,PRXY,1,,0.45 
CM,_Y,VOLU  
VSEL, , , ,       1 
CM,_Y1,VOLU 
CHKMSH,'VOLU'   
CMSEL,S,_Y  
!*  
VSWEEP,_Y1  
!*  
CMDELE,_Y   
CMDELE,_Y1  
CMDELE,_Y2  
!*  
NSEL,ALL
    
    
    
    
    
!* Select nodes on base of cantilever to set displacement to 0  
*GET,Min_xloc,NODE,0,MNLOC,X,,  
NSEL,S,LOC,X,(Min_xloc-1),(Min_xloc+1),,0   
/GO 
D,ALL, ,0, , , ,ALL, , , , ,
NSEL,ALL
    
!* Select nodes on end of cantilever to apply load to   
*GET,Max_xloc,NODE,0,MXLOC,X,,  
NSEL,S,LOC,X,(Max_xloc-1),(Max_xloc+1),,0   
*GET,numnodes1,NODE,0,COUNT 
/GO 
F,ALL,FY,(-500000)/numnodes1
NSEL,ALL
    
    
!* Select nodes on base of cantilever to set displacement to 0  
*GET,Min_xloc,NODE,0,MNLOC,X,,  
NSEL,S,LOC,X,(Min_xloc-1),(Min_xloc+1),,0   
/GO 
D,ALL, ,0, , , ,ALL, , , , ,
NSEL,ALL
    
    
FINISH  
/SOL
/STATUS,SOLU
SOLVE   
/SHOW   
FINISH  
/POST1  
!*  
/EFACET,1   
PLNSOL, S,EQV, 0,1.0
    
*GET,Max_stress,PLNSOL,0,MAX
!*CFOPEN, 'J:\Documents\ME578\Midterm\Ansys_Midterm_output_file', 'txt',,   
*CFOPEN, 'Ansys_Midterm_output_file', 'txt',,   
*VWRITE,Max_stress  
(F20.4) 
*CFCLOS 
    
!Save the von mises stress contour plot 
/SHOW,JPEG,,0   
JPEG,QUAL,75,   
JPEG,ORIENT,HORIZ   
JPEG,COLOR,2
JPEG,TMOD,1 
/GFILE,800, 
/REPLOT 
    
)/GOP    ! Resume printing after UNDO process   
)! We suggest a save at this point  
    
