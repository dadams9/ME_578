//From:     Devin Adams
//For:      Dr. Salmon
//Date:     3/6/18
//Purpose:  Find the min volume of a beam that will not yield

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
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

//Variables: height, width

//Objective: Minimize

//Constraints: Stress < 30,000 Pa

//Bounds: height, width, 
// 20m >= width >= 1m
// 20m >= height >= 1m
//

//Objective Function:   V = height, length, width

//Constraint Function:  12*2000*(0.5*height)/(width*height^3) < 30000

class Optimizationproblem {
    
    public static void blackbox( double[] f, double[] g, double[] x ) 
    {
        double width = x[0];
        double height = x[1];
        double volume = width * height * 2;
        
        /* Objective functions F(X) */
        f[0] = volume;

        /* Equality constraints G(X) = 0 MUST COME FIRST in g[0:me-1] */
        /* Inequality constraints G(X) >= 0 MUST COME SECOND in g[me:m-1] */
        g[0] = 30000 - (12000 / (width * height*height));
        g[1] = x[0] - 1;
        g[2] = -x[1] + 20;

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
         xl[i] = 0;
         xu[i] = 500; 
      }

      /* STEP 1.C: Starting point 'x'  
      ******************************/          
      x  = new double[n]; 
      for(i=0;i<n;i++)
      { 
         x[i] = xl[i]; /* Here for example: starting point = lower bounds */ 
      } 

      /*****************************************************************/
      /***  Step 2: Choose stopping criteria and printing options   ****/
      /*****************************************************************/
                  
      /* STEP 2.A: Stopping criteria 
      *****************************/
      option["maxeval"] = 10000;    /* Maximum number of function evaluation (e.g. 1000000)  */
      option["maxtime"] = 60*60*24; /* Maximum time limit in Seconds (e.g. 1 Day = 60*60*24) */

      /* STEP 2.B: Printing options  
      ****************************/
      option["printeval"] = 1000; /* Print-Frequency for current best solution (e.g. 1000) */
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

        //Calculare the final surface area
        double pi = Math.PI;
        


      double[] f,g;
      Console.WriteLine(" ");
      f = solution["f"];
      g = solution["g"];
      x = solution["x"];
      Console.WriteLine("Minimum volume of the beam = " + f[0]);
      double vol = (2 * x[0] * x[1]);
        double M = 12000 / (x[0] * x[1] * x[1]);
      Console.WriteLine("Stress in Beam = " + M);
      Console.WriteLine("Dimensions of Optimal Cylinder: Length = 2  Width = " + x[0] + "  Height = " + x[1]);


      //File.WriteAllText("C:\\Users\\Devin\\Documents\\School\\ME 578\\lab-3-dadams9\\Optimum Cylinder Output.txt", 
      //                  "Max Volume of Optimal Cylinder = " + -f[0] + "\r\nSurface Area of Optimal Cylinder = " + vol +
      //                  "\r\nDimensions of Optimal Cylinder: \r\nRadius = " + x[0] + "  \r\nHeight = " + x[1]);





      Console.ReadKey(); /* pause */

      /*****************************************************************/
      /***********************  END OF MAIN  ***************************/
      /*****************************************************************/        
  }
}

