%FROM: 		Devin Adams
%FOR:  		Dr. Salmon
%DATE:		2 April 2018
%PURPOSE:	README for Midterm. Running the MIDACO script will optimize the 
%dimensions of a 10m long "Golden Cantilever" beam for a minimum volume that 
%does not experience more than 2GPa of stress given a 500,000N load on the end 
%of the beam. The MIDACO script opens and runs NX to create an IGES file of 
%the beam that is subsequently read into ANSYS for a stress analysis. ANSYS
%then outputs a text file with the max stress of the beam and MIDACO then reads 
%that text file to know how to update the dimensions of the beam and the 
%process is repeated 100 times to arrive at the optimum beam.

%**NOTE**
%This program uses the following program versions to run correctly. Paths may
%need to be changed if using other versions of the program.
%NX10
%ANSYS 18.1
%Visual Studio 2012

%Also, it is assumed that you already have the midacocs.dll located 
%in a folder on your C: drive called MIDACO.
%********
%------------------------------------------------------------------------------
%------------------------------------README------------------------------------
%------------------------------------------------------------------------------

%P1_NX 			= Midterm_VS_Journal_NX
%P2_ANSYS 		= ANSYS_Golden_Cantilever_Session_Editor.txt
%P3_MIDACO 		= MIDACO_VS_Golden_Cantilever
%Midterm.sln 	= MIDACO_VS_Golden_Cantilever.sln


%------------------------------------------------------------------------------
%FILEPATHS
%This is a list of all the hardcoded file paths. However, the program has been
%created such that if you run the MIDACO executable from the Midterm folder, you will 
%not need to changeb any of the paths for files below as they are all based off of 
%the current working directory of the MIDACO executable, which is J:\Documents\ME578\Midterm\
%You WILL NEED to change the file paths for NX and ANSYS if using versions other
%than NX10 and ANSYS181
%------------------------------------------------------------------------------
	%----------------
	%-------NX-------
	%----------------
		%-------INPUTS-------
		%NX Journal Visual Studios Soln
		J:\Documents\ME578\Midterm\Midterm_VS_Journal_NX\Golden_Cantilever_VS_NX_Journal\Golden_Cantilever_VS_NX_Journal.sln
		
		%NX Journal script to create prt and igs file
		J:\Documents\ME578\Midterm\Midterm_VS_Journal_NX\Golden_Cantilever_VS_NX_Journal\Golden_Cantilever_VS_NX_Journal\Golden_Cantilever_journal.cs

		%NX Journal executable
		J:\Documents\ME578\Midterm\Golden_Cantilever_VS_NX_Journal.exe
	
		%-------OUTPUTS-------
		%NX created optimized beam part file
		%J:\Documents\ME578\Midterm\Working_Directory\Optimized_beam.prt
		
		%NX created optimized beam IGES file
		%J:\Documents\ME578\Midterm\Working_Directory\Optimized_beam.igs	
	
	%----------------
	%------ANSYS-----
	%----------------
		%-------INPUTS-------
		%ANSYS script for solving stresses on beam
		J:\Documents\ME578\Midterm\ANSYS_Golden_Cantilever_Session_Editor.txt
	
		%-------OUTPUTS-------
		%ANSYS output for max stress
		J:\Documents\ME578\Midterm\Working_Directory\Ansys_Midterm_output_file.txt
		
		%ANSYS output image of vonMises stress
		J:\Documents\ME578\Midterm\Working_Directory\file000.jpeg
	
	%----------------
	%-----MIDACO-----
	%----------------
		%-------INPUTS-------
		%MIDACO Executable
		After building solution
		J:\Documents\ME578\Midterm\Midterm_MIDACO\MIDACO_VS_Golden_Cantilever\MIDACO_VS_Golden_Cantilever\bin\x64\Debug\MIDACO_VS_Golden_Cantilever.exe
		
		%If you don't build solution because you didn't need to change any file paths
		J:\Documents\ME578\Midterm\MIDACO_VS_Golden_Cantilever.exe
		
		
		%MIDACO Visual Studios Soln
		J:\Documents\ME578\Midterm\Midterm_MIDACO\MIDACO_VS_Golden_Cantilever\MIDACO_VS_Golden_Cantilever.sln
		
		%MIDACO Optimization script
		J:\Documents\ME578\Midterm\Midterm_MIDACO\MIDACO_VS_Golden_Cantilever\MIDACO_VS_Golden_Cantilever\Golden_Cantilever_Optimizer.cs
	
		%-------OUTPUTS-------
		%MIDACO output for optimized beam dimensions and max stress
		J:\Documents\ME578\Midterm\Working_Directory\Optimized_beam.txt
	
	
%------------------------------------------------------------------------------
%FILENAMES
%------------------------------------------------------------------------------
	
	%----------------
	%-------NX-------
	%----------------
		%-------INPUTS-------
		%NX Journal Visual Studios Soln
		Golden_Cantilever_VS_NX_Journal.sln
		
		%NX Journal script to create prt and igs file
		Golden_Cantilever_journal.cs

		%NX Journal executable
		Golden_Cantilever_VS_NX_Journal.exe
	
		%-------OUTPUTS-------
		%NX created optimized beam part file
		Optimized_beam.prt
		
		%NX created optimized beam IGES file
		Optimized_beam.igs	
	
	%----------------
	%------ANSYS-----
	%----------------
		%-------INPUTS-------
		%ANSYS script for solving stresses on beam
		ANSYS_Golden_Cantilever_Session_Editor.txt
		
		%-------OUTPUTS-------
		%ANSYS output for max stress
		Ansys_Midterm_output_file.txt
		
		%ANSYS output image of vonMises stress
		file000.jpeg
	
	%----------------
	%-----MIDACO-----
	%----------------
		%-------INPUTS-------
		%MIDACO Executable
		MIDACO_VS_Golden_Cantilever.exe
		
		%MIDACO Visual Studios Soln
		MIDACO_VS_Golden_Cantilever.sln
		
		%MIDACO Optimization script
		Golden_Cantilever_Optimizer.cs
		
		%-------OUTPUTS-------
		%MIDACO output for optimized beam dimensions and max stress
		Optimized_beam.txt
	
	
%------------------------------------------------------------------------------
%LINE NUMBERS W/ PATHS
%------------------------------------------------------------------------------
	All paths are found in the MIDACO "Golden_Cantilever_Optimizer.cs" script
	These are the line numbers for the commented out hardcoded paths
	
	Path to "Working_Directory" that's cleared:								line 79, 263
	Path to NX part/IGES file, "Optimized_beam":							line 89, 285
	Path to NX run_managed.exe:												line 93, 288
	Path to NX journal executable, "Golden_Cantilever_VS_NX_Journal.exe":	line 97, 292, 336
	Path to NX IGES file, "Optimized_beam.igs":								line 109, 304, 347
	Path to ANSYS181.exe:													line 115, 310
	Path to ANSYS script, "ANSYS_Golden_Cantilever_Session_Editor.txt":		line 119, 313
	Path to ANSYS max stress output, "Ansys_Midterm_output_file.txt":		line 129, 136
	Path to MIDACO optimized beam, "Optimum_beam_dimensions.txt":			line 275
	
	
%------------------------------------------------------------------------------
%INSTRUCTIONS TO RUN
%------------------------------------------------------------------------------
1. Open the MIDACO "Golden_Cantilever_Optimizer.sln" and change the paths to 
	the version of NX or ANSYS that you are using (line numbers for those are
	shown above). You shouldn't have to change any of the paths to files since they
	are based off of the current directory.
2. Build the solution
3. Go to J:\Documents\ME578\Midterm\Midterm_MIDACO\MIDACO_VS_Golden_Cantilever\MIDACO_VS_Golden_Cantilever\bin\x64\Debug\MIDACO_VS_Golden_Cantilever.exe
	and copy the executable to the Midterm folder. Overwrite the old executable that was there.
4. Run the executable.
5. Output from the different parts of the program will be found in Working_Directory after the program is complete