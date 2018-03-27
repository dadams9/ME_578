//From:     Devin Adams
//For:      Dr. Salmon
//Date:     3/6/18
//Purpose:  Find the min volume of a beam that will not yield
//NOTE!!: Hard coded paths have been commented out and replaced with more 
//flexible paths based on the current directory of this script's exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
/***********************************************************************/
/*
//     This finds the max volume of a given cylinder
//      It is based off of MIDACO example
//     -------------------------------------
//
//     MIDACO solves Multi-Objective Mixed-Integer Non-Linear Problems:
//
//
//      Minimize     F_1(X),... F_O(X)  where X(1,...N-NI)   is CONTINUOUS
//                                      and   X(N-NI+1,...N) is DISCRETE
//
//      subject to   G_j(X)  =  0   (j=1,...ME)      equality constraints
//                   G_j(X) >=  0   (j=ME+1,...M)  inequality constraints
//
//      and bounds   XL <= X <= XU
//
//
//     The problem statement of this example is given below. You can use 
//     this example as template to run your own problem. To do so: Replace 
//     the objective functions 'F' (and in case the constraints 'G') given 
//     here with your own problem and follow the below instruction steps.
*/
/***********************************************************************/
/*********************   OPTIMIZATION PROBLEM   ************************/
/***********************************************************************/


/***********************************************************************/
/**************** DEFINE THE OPTIMIZATION PROBLEM   ********************/
/***********************************************************************/

//Variables: heightA, heightB, length, thickness

//Objective: Minimize volume

//Constraints: Stress < 2,000,000,000 Pa

//Bounds: thickness, heightA 
// 0.1m <= thickness <= 5m
// 0.1 <= heightA <= 5m
//

//Objective Function:   V = ((heightA+heightB)/2)*L*thickness
//Constraint Function uses output from ANSYS

//Constraint Function:  max_stress < 2,000,000,000 

class Optimizationproblem {
    
    public static void blackbox( double[] f, double[] g, double[] x ) 
    {
        double thickness = x[0];
        double heightA = x[1];
        double heightB = (1.618 * heightA);
        double L = 10.0;
        double volume = ((heightA + heightB) / 2) * L* thickness;
        
        /* Objective functions F(X) */
        f[0] = volume;

        //Get current working directory
        string current_directory = Directory.GetCurrentDirectory();

        //Delete the old files in the working directory
        //Define the directory to delete the files in
        //System.IO.DirectoryInfo di = new DirectoryInfo("J:\\Documents\\ME578\\Midterm\\Working_Directory\\");
        System.IO.DirectoryInfo di = new DirectoryInfo(current_directory+"\\Working_Directory\\");

        //Delete each file in the directory, di
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
        
        ////Define the name for the prt file and the igs file NX will create
        //string part_name = "J:\\Documents\\ME578\\Midterm\\Working_Directory\\Optimized_beam";
        string part_name = current_directory+"\\Working_Directory\\Optimized_beam";

        //Define path to the run_managed executable to open NX10
        string ex1 = "C:\\Program Files\\Siemens\\NX 10.0\\UGII\\run_managed.exe";

        //Define the path to the NX journal executable and the subsequent arguments needed to pass into the journal:
        //heightA of the beam, length, thickness, name of the part/igs file
        //string ex2 = "J:\\Documents\\ME578\\Midterm\\Golden_Cantilever_VS_NX_Journal.exe " + heightA + " " + 10 + " " + thickness + " " + part_name;
        string ex2 = current_directory+"\\Golden_Cantilever_VS_NX_Journal.exe " + heightA + " " + 10 + " " + thickness + " " + part_name;

        //Start the process of running NX, passing it ex2 as an argument, and wait for it to exit
        using (Process process = Process.Start(ex1, ex2))
        {
            process.WaitForExit();

        }

        //Wait for the NX journal to create the igs file before moving on
        //while (!System.IO.File.Exists(@"J:\Documents\ME578\Midterm\Working_Directory\Optimized_beam.igs"))
        while (!System.IO.File.Exists(current_directory+"\\Working_Directory\\Optimized_beam.igs"))
        {
            System.Threading.Thread.Sleep(10);
        }

        //Define path to the run_managed executable to open ANSYS version 18.1
        string ex3 = "C:\\Program Files\\ANSYS Inc\\v181\\ansys\\bin\\winx64\\ANSYS181.exe";

        //Define the path to the ANSYS script and the name of the log file for the ANSYS run:
        //string ex4 = "-b -i J:\\Documents\\ME578\\Midterm\\ANSYS_Golden_Cantilever_Session_Editor.txt -o Ansys_batch_log.out ";
        string ex4 = "-b -i " + current_directory + "\\ANSYS_Golden_Cantilever_Session_Editor.txt -o " + current_directory + "\\Working_Directory\\Ansys_batch_log.out ";

        //Start the process of running ANSYS and wait for it to exit, passing ex4 as an argument
        using (Process process = Process.Start(ex3, ex4))
        {
            process.WaitForExit();

        }

        //Wait for the ANSYS to output the max_stress file before moving on
        //while (!System.IO.File.Exists(@"J:\Documents\ME578\Midterm\Working_Directory\Ansys_Midterm_output_file.txt"))
        while (!System.IO.File.Exists(current_directory + "\\Working_Directory\\Ansys_Midterm_output_file.txt"))
        {
            System.Threading.Thread.Sleep(10);
        }

        //Read in the value of the max stress from the Ansys_Midterm_output_file.txt anc convert it to a double
        //string text = System.IO.File.ReadAllText(@"J:\Documents\ME578\Midterm\Working_Directory\Ansys_Midterm_output_file.txt");
        string text = System.IO.File.ReadAllText(current_directory+"\\Working_Directory\\Ansys_Midterm_output_file.txt");
        double max_stress = System.Convert.ToDouble(text);

        /* Equality constraints G(X) = 0 MUST COME FIRST in g[0:me-1] */
        /* Inequality constraints G(X) >= 0 MUST COME SECOND in g[me:m-1] */
        //Constraint functions
        g[0] = 2000000000-max_stress;
        g[1] = x[0] - 0.1;
        g[2] = -x[0] + 5.0;
        g[3] = x[1] - 0.1;
        g[4] = -x[1] + 5.0;

    }
} 


/***********************************************************************/
/************************   MAIN PROGRAM   *****************************/
/***********************************************************************/
class Example {

  public static void Main() {

        long i,n; double[] x,xl,xu;
        Dictionary<string, long> problem = new Dictionary<string, long>();
        Dictionary<string, long> option  = new Dictionary<string, long>();
        Dictionary<string, double> parameter  = new Dictionary<string, double>();
        Dictionary<string, double[]> solution   = new Dictionary<string, double[]>();
        string key = "MIDACO_LIMITED_VERSION___[CREATIVE_COMMONS_BY-NC-ND_LICENSE]";

        /*****************************************************************/
        /***  Step 1: Problem definition  ********************************/
        /*****************************************************************/

        /* STEP 1.A: Problem dimensions
        ******************************/
        problem["o"]  = 1; /* Number of objectives                          */
        problem["n"]  = 2; /* Number of variables (in total)                */
        problem["ni"] = 0; /* Number of integer variables (0 <= ni <= n)    */
        problem["m"]  = 5; /* Number of constraints (in total)              */
        problem["me"] = 0; /* Number of equality constraints (0 <= me <= m) */

        /* STEP 1.B: Lower and upper bounds 'xl' & 'xu'  
        **********************************************/ 
        n = problem["n"]; 
        xl = new double[n]; 
        xu = new double[n]; 
        for(i=0;i<n;i++)
        { 
            xl[i] = 0.10;
            xu[i] = 5.0; 
        }

        /* STEP 1.C: Starting point 'x'  
        ******************************/          
        x  = new double[n]; 
        for(i=0;i<n;i++)
        { 
            x[i] = 1.0; /* Here for example: starting point = lower bounds */ 
        } 

        /*****************************************************************/
        /***  Step 2: Choose stopping criteria and printing options   ****/
        /*****************************************************************/
                  
        /* STEP 2.A: Stopping criteria 
        *****************************/
        option["maxeval"] = 200;    /* Maximum number of function evaluation (e.g. 1000000)  */
        option["maxtime"] = 60*60*24; /* Maximum time limit in Seconds (e.g. 1 Day = 60*60*24) */

        /* STEP 2.B: Printing options  
        ****************************/
        option["printeval"] = 10; /* Print-Frequency for current best solution (e.g. 1000) */
        option["save2file"] = 1;    /* Save SCREEN and SOLUTION to TXT-files [ 0=NO/ 1=YES]  */          

        /*****************************************************************/
        /***  Step 3: Choose MIDACO parameters (FOR ADVANCED USERS)    ***/
        /*****************************************************************/

        parameter["param1"]  = 0.0; /* ACCURACY  */
        parameter["param2"]  = 0.0; /* SEED      */
        parameter["param3"]  = 0.0; /* FSTOP     */
        parameter["param4"]  = 0.0; /* ALGOSTOP  */
        parameter["param5"]  = 0.0; /* EVALSTOP  */
        parameter["param6"]  = 0.0; /* FOCUS     */
        parameter["param7"]  = 0.0; /* ANTS      */
        parameter["param8"]  = 0.0; /* KERNEL    */
        parameter["param9"]  = 0.0; /* ORACLE    */
        parameter["param10"] = 0.0; /* PARETOMAX */
        parameter["param11"] = 0.0; /* EPSILON   */
        parameter["param12"] = 0.0; /* BALANCE   */
        parameter["param13"] = 0.0; /* CHARACTER */   
 
        /*****************************************************************/
        /***  Step 4: Choose Parallelization Factor    *******************/
        /*****************************************************************/  

        option ["parallel"] = 0; /* Serial: 0 or 1, Parallel: 2,3,4,... */

        /*****************************************************************/
        /***********************  Run MIDACO  ****************************/
        /*****************************************************************/

        solution = Midaco.run ( problem, x,xl,xu, option, parameter, key );

        /* Print solution return arguments from MIDACO to console */    
        double[] f,g;
        Console.WriteLine(" ");
        f = solution["f"];
        g = solution["g"];
        x = solution["x"];
        double vol = ((x[1]*1.618+x[1])/2)*10*x[0];
        double heightA = x[1];
        double thickness = x[0];
        double M = g[0];

        //Get current working directory
        string current_directory = Directory.GetCurrentDirectory();

        //Write output to screeen
        Console.WriteLine("Minimum volume of the beam = " + f[0]);
        Console.WriteLine("Stress in Beam = " + M);
        Console.WriteLine("Dimensions of Optimum Beam: Length = 10  Thickness = " + x[0] + "  A = " + x[1] + "  B = " + (1.618*x[1]));

        //Delete the old files in the working directory
        //Define the directory to delete the files in
        //System.IO.DirectoryInfo di = new DirectoryInfo("J:\\Documents\\ME578\\Midterm\\Working_Directory\\");
        System.IO.DirectoryInfo di = new DirectoryInfo(current_directory+"\\Working_Directory\\");

        //Delete each file in the directory, di
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }


        //Write output file with dimensions of beam in current directory
        //File.WriteAllText("J:\\Documents\\ME578\\Midterm\\Working_Directory\\Optimum_beam_dimensions.txt",
        File.WriteAllText(current_directory+"\\Working_Directory\\Optimum_beam_dimensions.txt",
                            "Volume of Optimal Beam = " + (vol) + "m^3\r\nLength = 10 m" +
                            "\r\nMax Stress = " + (2000000000-g[0]) +" Pa"+
                            "\r\nThickness = " + (x[0] ) + " m\r\nHeightA = " + (x[1] ) + " m"+"\r\nHeightB = " + (1.61*x[1] ) + " m");



        //Create optimized beam in NX and do ANSYS analysis on it
        ////Define the name for the prt file and the igs file NX will create
        //string part_name = "J:\\Documents\\ME578\\Midterm\\Working_Directory\\Optimized_beam";
        string part_name = current_directory+"\\Working_Directory\\Optimized_beam";

        //Define path to the run_managed executable to open NX10
        string ex1 = "C:\\Program Files\\Siemens\\NX 10.0\\UGII\\run_managed.exe";

        //Define the path to the NX journal executable and the subsequent arguments needed to pass into the journal:
        //heightA of the beam, length, thickness, name of the part/igs file
        //string ex2 = "J:\\Documents\\ME578\\Midterm\\Golden_Cantilever_VS_NX_Journal.exe " + heightA + " " + 10 + " " + thickness + " " + part_name;
        string ex2 = current_directory+"\\Golden_Cantilever_VS_NX_Journal.exe " + heightA + " " + 10 + " " + thickness + " " + part_name;

        //Start the process of running NX, passing it ex2 as an argument, and wait for it to exit
        using (Process process = Process.Start(ex1, ex2))
        {
            process.WaitForExit();

        }

        //Wait for the NX journal to create the igs file before moving on
        //while (!System.IO.File.Exists(@"J:\Documents\ME578\Midterm\Working_Directory\Optimized_beam.igs"))
        while (!System.IO.File.Exists(current_directory+"\\Working_Directory\\Optimized_beam.igs"))
        {
            System.Threading.Thread.Sleep(10);
        }

        //Define path to the run_managed executable to open ANSYS version 18.1
        string ex3 = "C:\\Program Files\\ANSYS Inc\\v181\\ansys\\bin\\winx64\\ANSYS181.exe";

        //Define the path to the ANSYS script and the name of the log file for the ANSYS run:
        //string ex4 = "-b -i J:\\Documents\\ME578\\Midterm\\ANSYS_Golden_Cantilever_Session_Editor.txt -o Ansys_batch_log.out ";
        string ex4 = "-b -i "+current_directory+"\\ANSYS_Golden_Cantilever_Session_Editor.txt -o "+current_directory+"\\Working_directory\\Ansys_batch_log.out ";

        //Start the process of running ANSYS and wait for it to exit, passing ex4 as an argument
        using (Process process = Process.Start(ex3, ex4))
        {
            process.WaitForExit();

        }

        //Create the beam with actual dimensions in meters

        //Check to see if the part file already exists. If so, delete it.
        bool path_already_exists = File.Exists((part_name+".prt"));
        if (path_already_exists == true)
        {
            File.Delete((part_name + ".prt"));
        }


        //Define the path to the NX journal executable and the subsequent arguments needed to pass into the journal:
        //heightA of the beam, length, thickness, name of the part/igs file
        //ex2 = "J:\\Documents\\ME578\\Midterm\\Golden_Cantilever_VS_NX_Journal.exe " + (1000*heightA) + " " + (10*1000) + " " + (1000*thickness) + " " + part_name;
        ex2 = current_directory+"\\Golden_Cantilever_VS_NX_Journal.exe " + (1000 * heightA) + " " + (10 * 1000) + " " + (1000 * thickness) + " " + part_name;

        //Start the process of running NX, passing it ex2 as an argument, and wait for it to exit
        using (Process process = Process.Start(ex1, ex2))
        {
            process.WaitForExit();

        }

        //Wait for the NX journal to create the igs file before moving on
        //while (!System.IO.File.Exists(@"J:\Documents\ME578\Midterm\Working_Directory\Optimized_beam.igs"))
        while (!System.IO.File.Exists(current_directory+"\\Working_Directory\\Optimized_beam.igs"))
        {
            System.Threading.Thread.Sleep(10);
        }

        /*****************************************************************/
        /***********************  END OF MAIN  ***************************/
        /*****************************************************************/        
    }
}

