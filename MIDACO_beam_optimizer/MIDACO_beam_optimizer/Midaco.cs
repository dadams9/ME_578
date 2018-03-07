/*////////////////////////// GATEWAY HEADER ////////////////////////////
//                           
//     _|      _|  _|_|_|  _|_|_|      _|_|      _|_|_|    _|_|    
//     _|_|  _|_|    _|    _|    _|  _|    _|  _|        _|    _|  
//     _|  _|  _|    _|    _|    _|  _|_|_|_|  _|        _|    _|  
//     _|      _|    _|    _|    _|  _|    _|  _|        _|    _|  
//     _|      _|  _|_|_|  _|_|_|    _|    _|    _|_|_|    _|_|  
//
//                                                   Version 6.0
//
////////////////////////////////////////////////////////////////////////
//
//                  ========= SERIAL VERSION =========
//                             
////////////////////////////////////////////////////////////////////////
//
//           See the MIDACO user manual for detailed information
//
////////////////////////////////////////////////////////////////////////
//
//    Author (C) :   Dr. Martin Schlueter
//                   Information Initiative Center,
//                   Division of Large Scale Computing Systems,
//                   Hokkaido University, JAPAN.
//
//    Email :        info@midaco-solver.com
//
//    URL :          http://www.midaco-solver.com
//       
//////////////////////////////////////////////////////////////////////*/

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/*//////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////*/

class Midaco {

    public static Dictionary<string, double[]> run( 
                  Dictionary<string, long> problem,
                  double[] x_, double[] xl_, double[] xu_,
                  Dictionary<string, long> option,
                  Dictionary<string, double> parameter,
                  string key ) 
     {
      long o  = problem["o"];
      long n  = problem["n"];
      long ni = problem["ni"];
      long m  = problem["m"];
      long me = problem["me"];

      long maxeval   = option["maxeval"];
      long maxtime   = option["maxtime"]; 
      long printeval = option["printeval"]; 
      long save2file = option["save2file"]; 

      double[] param = new double[13];
      param[ 0] = parameter["param1"];  /* ACCURACY  */
      param[ 1] = parameter["param2"];  /* SEED      */
      param[ 2] = parameter["param3"];  /* FSTOP     */
      param[ 3] = parameter["param4"];  /* ALGOSTOP  */
      param[ 4] = parameter["param5"];  /* EVALSTOP  */
      param[ 5] = parameter["param6"];  /* FOCUS     */
      param[ 6] = parameter["param7"];  /* ANTS      */
      param[ 7] = parameter["param8"];  /* KERNEL    */
      param[ 8] = parameter["param9"];  /* ORACLE    */
      param[ 9] = parameter["param10"]; /* PARETOMAX */
      param[10] = parameter["param11"]; /* EPSILON   */
      param[11] = parameter["param12"]; /* BALANCE   */
      param[12] = parameter["param13"]; /* CHARACTER */      

      long p = 1;

      if (option ["parallel"] >= 2) {
            Console.WriteLine(" ############################################### ");         
            Console.WriteLine(" ### This is the serial version of Midaco.cs ### ");
            Console.WriteLine(" ###                                         ### ");
            Console.WriteLine(" ###       To enable parallelization:        ### ");
            Console.WriteLine(" ###                                         ### ");
            Console.WriteLine(" ###  Use the parallel version of Midaco.cs  ### ");
            Console.WriteLine(" ############################################### "); 
      }
/*//////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////*/

      long lrw = 120*n+20*m+20*o+20*p+p*(m+2*o)+o*o+5000;
      long liw = 3*n+p+1000; 
      long lpf = 1;

      if ( o >= 2 ) 
      { 
        lpf = 1000 * (o+m+n) + 1;
        if ( param[9] >= 1.0 ) { lpf =  (long)param[9] * (o+m+n) + 1; }
        if ( param[9] <=-1.0 ) { lpf = -(long)param[9] * (o+m+n) + 1; }        
      } 

      double[] x  = new double[n];
      double[] f  = new double[o];
      double[] g  = new double[m];
      double[] xl = new double[n];
      double[] xu = new double[n];
      double[] rw = new double[lrw];
      double[] pf = new double[lpf];

      long[] iw = new long[liw];
      long iflag = 0;
      long istop = 0;

      for(long i=0; i < n; i++){ x[i]  = x_[i];  }
      for(long i=0; i < n; i++){ xl[i] = xl_[i]; }
      for(long i=0; i < n; i++){ xu[i] = xu_[i]; } 

/*//////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////*/
              
      double[] xtra = new double[10];

      xtra[0] = (double)iflag;
      xtra[1] = (double)istop;

/*//////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////*/

      Native_cs.Midaco_print ( 1, printeval, save2file, xtra, 
                                  f, g, x, xl, xu, o, n, ni, m, me, rw, pf, 
                                  maxeval, maxtime, param, p, key );           

      while ( istop <= 0 )
      {   

          Optimizationproblem.blackbox (f, g, x);

          midacocs ( p, o, n, ni, m, me, 
                     x, f, g, xl, xu, 
                     ref iflag, ref istop, param, 
                     rw, lrw, iw, liw, pf, lpf, key,
                     xtra );

          Native_cs.Midaco_print ( 2, printeval, save2file, xtra, 
                                      f, g, x, xl, xu, o, n, ni, m, me, rw, pf, 
                                      maxeval, maxtime, param, p, key );      

          iflag = (long)xtra[0];
          istop = (long)xtra[1];
      }

/*//////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////*/

      Dictionary<string, double[]> solution  = new Dictionary<string, double[]>();

      solution["f"] = f;
      solution["g"] = g;
      solution["x"] = x;

      return solution;
    }

/*//////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////*/

    /* Set path to "midacocs.dll" library [=== FEEL FREE TO CHANGE THE PATH ===] */  

    [DllImport("C:\\MIDACO\\midacocs.dll")] /* Example of Windows Path */

    // [DllImport("/home/midacocs.dll")] /* Example of Mac/Linux Path */  
    
    private static extern void midacocs( long p, long o, long n, long ni, long m, long me,     
                                         [MarshalAs(UnmanagedType.LPArray)] double[] x, 
                                         [MarshalAs(UnmanagedType.LPArray)] double[] f,
                                         [MarshalAs(UnmanagedType.LPArray)] double[] g,
                                         [MarshalAs(UnmanagedType.LPArray)] double[] xl, 
                                         [MarshalAs(UnmanagedType.LPArray)] double[] xu, 
                                         ref long iflag, ref long istop,
                                         [MarshalAs(UnmanagedType.LPArray)] double[] param,
                                         [MarshalAs(UnmanagedType.LPArray)] double[] rw, long lrw,
                                         [MarshalAs(UnmanagedType.LPArray)] long[] iw, long liw,
                                         [MarshalAs(UnmanagedType.LPArray)] double[] pf, long lpf,
                                         String key,
                                         [MarshalAs(UnmanagedType.LPArray)] double[] xtra);    
}
















/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/  
/*CCCCCCCCCCCCCCC MIDACO C# native printing commands CCCCCCCCCCCCCCCCC*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/  
class Native_cs {   
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*Increase below size of bestg and bestx for problems with N+O > 10000*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/  
static double[] bestg; 
static double[] bestx;
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
static string abc;
static TextWriter iout1; /* MIDADCO_SCREEN.TXT */
static TextWriter iout2; /* MIDADCO_SOLUTION.TXT */
static TextWriter iout3; /* MIDADCO_PARETOFRONT.tmp */
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
static double tstart, tnow, tmax;
static long eval;
static double acc;
static long tic;
static long extraoffset; 
static long q; 
static long kx;  
static long kf;
static long kg; 
static long kres;  
static long wx;   
static long wf; 
static long wg;
static long wres;    
static long kbest;
static long wbest;
static double[] bestf;
static double[] bestr;
static long update,i;
static double dummy_f, dummy_vio;
static long pfmax;
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/ 
public static void Midaco_print( long c, long printeval, long save2file, double[] xtra, 
                                 double[] f , double[] g, double[] x, double[] xl, double[] xu, 
                                 long o, long n, long ni, long m, long me, double[] rw, double[] pf, 
                                 long maxeval, long maxtime, double[] param, long p, string key )
      {
        long iflag = (long)xtra[0];
        long istop = (long)xtra[1];

        if( c == 2 )
        {
            tnow = gettime()-tstart;   
            eval = eval + p;           
            if( iflag >= 10)
            {
              warnings_and_erros( iflag, 0 );
              if(save2file > 0)
              {
                warnings_and_erros( iflag, 1 );
                warnings_and_erros( iflag, 2 );
              }
            } 

            if(printeval > 0)
            {            
                tic = tic + p;
                if((tic >= printeval)||(eval == p)||(iflag >= 1))
                {
                   if(eval < 0)
                   { tic = 0; 
                     eval = - eval - 2*printeval; }
                   if(eval > p){ tic = 0; } 
                   if(rw[kres] == rw[wres]){
                     kbest = kf;
                     wbest = wf;
                   }else{
                     kbest = kres;
                     wbest = wres;
                   }                
                   if((rw[wbest] < rw[kbest]) ||
                      (iflag >= 1)||(iflag == -300)||(iflag == -500)){
                      
                         bestf[0] = rw[wf];
                         bestr[0] = rw[wres];
                         for(i=0;i<m+2*o;i++){  bestg[i] = rw[wg+i]; }
                         for(i=0;i<n;i++){  bestx[i] = rw[wx+i]; }  
                   }
                   else
                   {        
                         bestf[0] = rw[kf];
                         bestr[0] = rw[kres];
                         for(i=0;i<m+2*o;i++){  bestg[i] = rw[kg+i]; }
                         for(i=0;i<n;i++){  bestx[i] = rw[kx+i]; }          
                   }
                   if( iflag < 100){      
                   print_line( o, eval, tnow, bestf[0], bestr[0], pf, 0 );  
                   if( save2file > 0)
                   {
                    print_line( o, eval, tnow, bestf[0], bestr[0], pf, 1 );            
                   }
                   if(save2file > 0)
                   {           
                       update = 0;   
                       if( (bestr[0]  < dummy_vio)||
                          ((bestr[0] == dummy_vio)&&(bestf[0] < dummy_f)) )
                          {
                            dummy_f = bestf[0];
                            dummy_vio = bestr[0];
                            update = 1;
                          }
                         if( update > 0 )
                         {                              
                           abc = String.Format("\n\n            CURRENT BEST SOLUTION");
                           using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}
                           print_solution( o, n, m, me, bestx, bestg, 
                                           bestf, bestr, xl, xu, acc, 
                                           eval, tnow, iflag, pf, 2 );
                         }   
                         if( o > 1 )
                         {
                           if(( save2file >= 1 )&&( printeval >= 1))
                           {
                             print_paretofront(o,m,n,pf,bestx,bestg,pfmax);
                           }
                         }          
                   }
                   }          
                }
            }  

/*////////////////////////////////////////////////////////////////////*/
            if( istop == 0)
            {
                if(tnow >= tmax)     { iflag = -999;}
                if(eval >= maxeval-1)
            {
             if(maxeval < 99999999){ iflag = -999;}
            }          
                /* special case maxeval = 1 */
                if( maxeval<=1 ){
            {
                if(tnow >= tmax)     { iflag = -999;}
                if(eval >= maxeval-1)
            {
             if(maxeval < 99999999){ iflag = -999;}
            }   
                  /* special case maxeval = 1 */
                  if((tnow<=0)||(maxeval<=1)){ 
                  istop = 1; 
                  iflag = 0;
                  for(i=0;i<n;i++){ x[i] = bestx[i]; } 
                }        
            } 
                  istop = 1; 
                  iflag = 0;
                  for(i=0;i<n;i++){ x[i] = bestx[i]; } 
                }   
            }
/*////////////////////////////////////////////////////////////////////*/
  
         if( istop >= 1 )
         {
           if( o <= 1)
           {
            bestf[0] = f[0];
           }
           else
           {
            bestf[0] = rw[wf];
           }

           for(i=0;i<m;i++){  bestg[i] = g[i]; }
              if( o >= 2 ){ for(i=0;i<o*2;i++){  bestg[m+i] = rw[wg+m+i]; }}
           if(printeval > 0)
           {
            print_final(o,iflag,tnow,tmax,eval,maxeval,n,m,me,x,bestg,bestf, 
            xl,xu,rw,pf,acc,wres,param, 0 ); 
            if(save2file > 0)
            {
               print_final(o,iflag,tnow,tmax,eval,maxeval,n,m,me,x,bestg,bestf, 
               xl,xu,rw,pf,acc,wres,param, 1 );
               print_final(o,iflag,tnow,tmax,eval,maxeval,n,m,me,x,bestg,bestf, 
               xl,xu,rw,pf,acc,wres,param, 2 );            
            }
           }
         }

        } /* end clause 2 */



        if( c == 1 )
        {

/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*  Increase size of bestg and bestx for problems with N+O > 10000    */
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/              
          bestg  = new double[10000];
          bestx  = new double[10000];
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
/*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/          
          bestf  = new double[1];
          bestr  = new double[1];                    

          iflag = 0;
          istop = 0;
          tmax = Convert.ToDouble(maxtime);
          tstart = gettime();
          eval = 0;
          if( param[0] <= 0.0 )
          {
            acc = 0.001;
          }else{
            acc = param[0];
          }
          extraoffset = 5*(p+o+n+m)+100;
          q    = 102*n+(m+2*o)+516 + extraoffset;     
          kx   = 9;
          kf   = 9+n;
          kg   = 9+n+1;
          kres = 9+n+1+m;  
          if( o > 1 ){ kres = 9+n+1+ (m+2*o); }
          wx   = q;
          wf   = q+n;
          wg   = q+n+1;
          wres = q+n+1+m; 
          if( o > 1 ){ wres = q+n+1+ (m+2*o); }    
          if((save2file > 0)&&( printeval > 0))
          {
            iout1 = new StreamWriter("MIDACO_SCREEN.TXT");
            iout2 = new StreamWriter("MIDACO_SOLUTION.TXT");
            iout1.Close();
            iout2.Close();   
          }  
          bestf[0] = 1.0e+99;
          bestr[0] = 1.0e+99;
          dummy_f   = 1.0e+99;
          dummy_vio = 1.0e+99;    
          tic = 0;    
          pfmax = 100;
          if( param[9] >= 1.0 ){ pfmax =     Convert.ToInt32(param[9]); }
          if( param[9] <=-1.0 ){ pfmax = -1*(Convert.ToInt32(param[9])); }  
          if(printeval >= 1)
          {
              print_head(p,o,n,ni,m,me,param,maxeval,maxtime,printeval,save2file,key,0);

              if(save2file > 0)
              { 
                print_head(p,o,n,ni,m,me,param,maxeval,maxtime,printeval,save2file,key,1);
              }
           } 
           if((save2file > 0)&&(printeval > 0))
           {
             abc = String.Format(" MIDACO - SOLUTION\n -----------------\n\n This file saves the current best solution X found by MIDACO.\n This file is updated after every PRINTEVAL function evaluation,\n if X has been improved.\n\n");
             using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}
           }           

        } /* end clause 1 */
   
        xtra[0] = (double)iflag;
        xtra[1] = (double)istop;
      }
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/ 
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/ 
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/             
      public static double gettime()
      {
        TimeSpan timeOfUtcDay = DateTime.UtcNow.TimeOfDay;
        double second = timeOfUtcDay.TotalSeconds;
        return second;
      }  
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/             
      public static void warnings_and_erros( long iflag, long iout)
      {
          if( iflag < 100 )
          {
            abc = String.Format("\n *** WARNING ***   ( IFLAG = {0} )\n\n",iflag);
            if( iout == 0 ){ Console.WriteLine(abc); }
            if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }  
            if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);} }                        
          }
          else
          {
            abc = String.Format("\n *** MIDACO INPUT ERROR ***   ( IFLAG = {0} )\n\n",iflag);
            if( iout == 0 ){ Console.WriteLine(abc); }
            if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }  
            if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);} }                        
          }
      }         
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/ 
      public static void print_head(long p, long o, long n, long ni, long m, 
                                    long me, double[] param, long maxeval, long maxtime, 
                                    long printeval, long save2file, string key, long iout)
      {
        long i, dummy;

        abc = String.Format("\n MIDACO 6.0    (www.midaco-solver.com)\n -------------------------------------\n\n LICENSE-KEY:  {0}\n\n ----------------------------------------",key);
        if( iout == 0 ){ Console.WriteLine(abc); }
        if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} } 
        abc = String.Format(" | OBJECTIVES{0,5} | PARALLEL{1,10} |\n |--------------------------------------|",o,p);
        if( iout == 0 ){ Console.WriteLine(abc); }
        if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   
        abc = String.Format(" | N{0,14} | MAXEVAL{1,11} |\n | NI{2,13} | MAXTIME{3,11} |\n | M{4,14} | PRINTEVAL{5,9} |\n | ME{6,13} | SAVE2FILE{7,9} |\n |--------------------------------------|",n,maxeval,ni,maxtime,m,printeval,me,save2file);   
        if( iout == 0 ){ Console.WriteLine(abc); }
        if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   

        dummy = 0; for(i=0;i<12;i++){ if(param[i] != 0.0){ dummy=1;}}   
        if(dummy == 0)
        {
          abc = String.Format(" | PARAMETER:    All by default (0)     |");
          if( iout == 0 ){ Console.WriteLine(abc); }
          if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }           
        }          
        else
        {
          if( iout == 0 ){
          if(param[ 0]!=0.0){ abc = String.Format(" | PARAM[ 0] {0,14} ACCURACY    |",param[ 0]); Console.WriteLine(abc); }
          if(param[ 1]!=0.0){ abc = String.Format(" | PARAM[ 1] {0,14} RANDOM-SEED |",param[ 1]); Console.WriteLine(abc); }
          if(param[ 2]!=0.0){ abc = String.Format(" | PARAM[ 2] {0,14} FSTOP       |",param[ 2]); Console.WriteLine(abc); }
          if(param[ 3]!=0.0){ abc = String.Format(" | PARAM[ 3] {0,14} ALGOSTOP    |",param[ 3]); Console.WriteLine(abc); }
          if(param[ 4]!=0.0){ abc = String.Format(" | PARAM[ 4] {0,14} EVALSTOP    |",param[ 4]); Console.WriteLine(abc); }
          if(param[ 5]!=0.0){ abc = String.Format(" | PARAM[ 5] {0,14} FOCUS       |",param[ 5]); Console.WriteLine(abc); }
          if(param[ 6]!=0.0){ abc = String.Format(" | PARAM[ 6] {0,14} ANTS        |",param[ 6]); Console.WriteLine(abc); }
          if(param[ 7]!=0.0){ abc = String.Format(" | PARAM[ 7] {0,14} KERNEL      |",param[ 7]); Console.WriteLine(abc); }
          if(param[ 8]!=0.0){ abc = String.Format(" | PARAM[ 8] {0,14} ORACLE      |",param[ 8]); Console.WriteLine(abc); }
          if(param[ 9]!=0.0){ abc = String.Format(" | PARAM[ 9] {0,14} PARETOMAX   |",param[ 9]); Console.WriteLine(abc); }
          if(param[10]!=0.0){ abc = String.Format(" | PARAM[10] {0,14} EPSILON     |",param[10]); Console.WriteLine(abc); }
          if(param[11]!=0.0){ abc = String.Format(" | PARAM[11] {0,14} BALANCE     |",param[11]); Console.WriteLine(abc); }
          if(param[12]!=0.0){ abc = String.Format(" | PARAM[12] {0,14} CHARACTER   |",param[12]); Console.WriteLine(abc); } }          
          if( iout == 1 ){
          if(param[ 0]!=0.0){ abc = String.Format(" | PARAM[ 0] {0,14} ACCURACY    |",param[ 0]); using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }
          if(param[ 1]!=0.0){ abc = String.Format(" | PARAM[ 1] {0,14} RANDOM-SEED |",param[ 1]); using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }
          if(param[ 2]!=0.0){ abc = String.Format(" | PARAM[ 2] {0,14} FSTOP       |",param[ 2]); using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }
          if(param[ 3]!=0.0){ abc = String.Format(" | PARAM[ 3] {0,14} ALGOSTOP    |",param[ 3]); using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }
          if(param[ 4]!=0.0){ abc = String.Format(" | PARAM[ 4] {0,14} EVALSTOP    |",param[ 4]); using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }
          if(param[ 5]!=0.0){ abc = String.Format(" | PARAM[ 5] {0,14} FOCUS       |",param[ 5]); using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }
          if(param[ 6]!=0.0){ abc = String.Format(" | PARAM[ 6] {0,14} ANTS        |",param[ 6]); using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }
          if(param[ 7]!=0.0){ abc = String.Format(" | PARAM[ 7] {0,14} KERNEL      |",param[ 7]); using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }
          if(param[ 8]!=0.0){ abc = String.Format(" | PARAM[ 8] {0,14} ORACLE      |",param[ 8]); using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }
          if(param[ 9]!=0.0){ abc = String.Format(" | PARAM[ 9] {0,14} PARETOMAX   |",param[ 9]); using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }
          if(param[10]!=0.0){ abc = String.Format(" | PARAM[10] {0,14} EPSILON     |",param[10]); using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }
          if(param[11]!=0.0){ abc = String.Format(" | PARAM[11] {0,14} BALANCE     |",param[11]); using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }        
          if(param[12]!=0.0){ abc = String.Format(" | PARAM[12] {0,14} CHARACTER   |",param[12]); using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} } }                    
        }                
        abc = String.Format(" ----------------------------------------\n");
        if( iout == 0 ){ Console.WriteLine(abc); }
        if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} } 

        if( o <= 1 ){ abc = String.Format(" [     EVAL,    TIME]        OBJECTIVE FUNCTION VALUE         VIOLATION OF G(X)\n ------------------------------------------------------------------------------"); }
        if( o >= 2 ){ abc = String.Format(" [     EVAL,    TIME]   MULTI-OBJECTIVE PROGRESS   VIOLATION OF G(X)\n -------------------------------------------------------------------   [PARETO]"); }

        if( iout == 0 ){ Console.WriteLine(abc); }
        if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }         
      }  
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/ 
      public static void print_line(long o, long eval, double tnow, double f, double vio, double[] pf, long iout)
      {
        long psize=0; 

        if( o >= 2 ){ psize = Convert.ToInt32(pf[0]); }

        if( o <= 1 ){ abc = String.Format(" [{0,9},{1,8:0.0}]        F(X):{2,19:0.00000000}         VIO:{3,13:0.000000}",eval,tnow,f,vio); }
        if( o >= 2 ){ abc = String.Format(" [{0,9},{1,8:0.0}]   PRO:{2,20:0.00000000}   VIO:{3,13:0.000000}   [{4,6}]",eval,tnow,f,vio,psize); }

        if( iout == 0 ){ Console.WriteLine(abc); }
        if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }         
      }
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/ 
      public static void print_solution(long o, long n, long m, long me, double[] x, double[] g, 
                                        double[] f, double[] vio, double[] xl, double[] xu, double acc, 
                                        long eval, double tnow, long iflag, double[] pf, long iout )
      {
        long j,on;
        long profil=0;
        long psize=0;

        abc = String.Format(" --------------------------------------------\n EVAL:{0,10},  TIME:{1,8:0.00},  IFLAG:{2,4}\n --------------------------------------------",eval,tnow,iflag);
        if( iout == 0 ){ Console.WriteLine(abc); }
        if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   
        if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}} 
         
        if( o <= 1 )
        {  
          abc = String.Format(" f[0] ={0,38:0.000000000000000}",f[0]);
          if( iout == 0 ){ Console.WriteLine(abc); }
          if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   
          if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}} 
        }
        else
        {   

            abc = String.Format(" PROGRESS{0,36:0.000000000000000}",f[0]);
            if( iout == 0 ){ Console.WriteLine(abc); }
            if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   
            if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}} 

            psize = Convert.ToInt32(pf[0]);
            abc = String.Format(" --------------------------------------------\n NUMBER OF PARETO POINTS{0,21} \n --------------------------------------------",psize);
            if( iout == 0 ){ Console.WriteLine(abc); }
            if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   
            if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}} 

            for( i=0; i<o; i++)
            {   
                if(g[m+o+i]<=0.0)
                {
                  abc = String.Format(" f[{0,4}] = {1,34:0.000000000000000}",i,g[m+i]);
                }
                else
                {
                  abc = String.Format(" f[{0,4}] = {1,34:0.000000000000000}",i,-g[m+i]);
                }
                if( iout == 0 ){ Console.WriteLine(abc); }
                if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   
                if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}}                  
            }
        }

        if( m > 0 )
        {
            abc = String.Format(" --------------------------------------------");
            if( iout == 0 ){ Console.WriteLine(abc); }
            if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   
            if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}} 
             }

        if(m > 0)
        {
            if(iflag < 100)
            {
               abc = String.Format(" VIOLATION OF G(X) {0,26:0.000000000000}\n --------------------------------------------",vio[0]);
               if( iout == 0 ){ Console.WriteLine(abc); }
               if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   
               if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}} 
               }
            for( i=0; i<m; i++)
            {                     
                if(i < me)
                {
                  if(( g[i] <= acc )&&( g[i] >= -acc ))
                  {
                    abc = String.Format(" g[{0,4}] ={1,16:0.00000000}  (EQUALITY CONSTR)",i,g[i]);
                  }else{
                    abc = String.Format(" g[{0,4}] ={1,16:0.00000000}  (EQUALITY CONSTR)  <---  INFEASIBLE  ( G NOT = 0 )",i,g[i]);                    
                  }
                }else{
                  if( g[i] >= -acc )
                  {
                    abc = String.Format(" g[{0,4}] ={1,16:0.00000000}  (IN-EQUAL CONSTR)",i,g[i]);
                  }else{
                    abc = String.Format(" g[{0,4}] ={1,16:0.00000000}  (IN-EQUAL CONSTR)  <---  INFEASIBLE  ( G < 0 )",i,g[i]);                    
                  }        
                }                
                if( iout == 0 ){ Console.WriteLine(abc); }
                if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   
                if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}} 
                     }
        }

        abc = String.Format(" --------------------------------------------         BOUNDS-PROFIL    ");
        if( iout == 0 ){ Console.WriteLine(abc); }
        if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   
        if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}} 
         
        for( i=0; i<n; i++)
        {
            profil = -1; on = 1; 
            if((on==1)&&( x[i] > xu[i]+1.0e-6 )){ profil = 91; on = 0; }
            if((on==1)&&( x[i] < xl[i]-1.0e-6 )){ profil = 92; on = 0; }        
            if((on==1)&&( xl[i] > xu[i]       )){ profil = 93; on = 0; }         
            if((on==1)&&( xl[i] == xu[i]      )){ profil = 90; on = 0; }
            if((on==1)&&( (x[i]-xl[i]) < (xu[i]-xl[i])/1000.0 )){ profil =  0; on = 0; }                
            if((on==1)&&( (xu[i]-x[i]) < (xu[i]-xl[i])/1000.0 )){ profil = 22; on = 0; }     
            for( j=1; j<=21; j++)
            {
              if((on==1)&&( x[i] <= xl[i] + Convert.ToDouble(j) * (xu[i]-xl[i])/21.0 )){ profil = j; on = 0; }        
            } 

            if(profil == 0){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* XL___________________ */",i,x[i]);}
            if(profil == 1){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* x____________________ */",i,x[i]);}
            if(profil == 2){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* _x___________________ */",i,x[i]);}
            if(profil == 3){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* __x__________________ */",i,x[i]);}
            if(profil == 4){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* ___x_________________ */",i,x[i]);}
            if(profil == 5){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* ____x________________ */",i,x[i]);}
            if(profil == 6){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* _____x_______________ */",i,x[i]);}
            if(profil == 7){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* ______x______________ */",i,x[i]);}
            if(profil == 8){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* _______x_____________ */",i,x[i]);}
            if(profil == 9){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* ________x____________ */",i,x[i]);}
            if(profil ==10){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* _________x___________ */",i,x[i]);}
            if(profil ==11){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* __________x__________ */",i,x[i]);}
            if(profil ==12){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* ___________x_________ */",i,x[i]);}
            if(profil ==13){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* ____________x________ */",i,x[i]);}
            if(profil ==14){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* _____________x_______ */",i,x[i]);}
            if(profil ==15){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* ______________x______ */",i,x[i]);}
            if(profil ==16){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* _______________x_____ */",i,x[i]);}
            if(profil ==17){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* ________________x____ */",i,x[i]);}
            if(profil ==18){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* _________________x___ */",i,x[i]);}
            if(profil ==19){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* __________________x__ */",i,x[i]);}
            if(profil ==20){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* ___________________x_ */",i,x[i]);}
            if(profil ==21){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* ____________________x */",i,x[i]);}
            if(profil ==22){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* ___________________XU */",i,x[i]);}
            if(profil ==90){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000};  /* WARNING: XL = XU      */",i,x[i]);}
            if(profil ==91){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000}; ***ERROR*** (X > XU)        ",i,x[i]);}
            if(profil ==92){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000}; ***ERROR*** (X < XL)        ",i,x[i]);}
            if(profil ==93){ abc = String.Format(" x[{0,4}] ={1,34:0.000000000000000}; ***ERROR*** (XL > XU)       ",i,x[i]);}
            if(profil < 0 ){ abc = String.Format(" PROFIL-ERROR");} 

            if( iout == 0 ){ Console.WriteLine(abc); }
            if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   
            if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}} 
         }        
      }
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/ 
      public static void print_final(long o, long iflag, double tnow, double tmax, long eval, 
                                     long maxeval, long n, long m, long me, double[] x, double[] g, double[] f,
                                     double[] xl, double[] xu, double[] rw, double[] pf, double acc, long wres, double[] param, long iout)
      {
         double[] vio;
         vio = new double[1];
         vio[0] = rw[wres];
         long dummy;
         if((iflag == 1)||(iflag == 2))
         {
         if(tnow >=    tmax){ abc = String.Format("\n OPTIMIZATION FINISHED  --->  MAXTIME REACHED");}
         if(eval >= maxeval){ abc = String.Format("\n OPTIMIZATION FINISHED  --->  MAXEVAL REACHED");}
         }
         if((iflag == 3)||(iflag == 4))
         {      
             dummy = Convert.ToInt32(param[3]);
             abc = String.Format("\n OPTIMIZATION FINISHED  --->  ALGOSTOP (={0,3})",dummy);
         }
         if((iflag == 5)||(iflag == 6))
         {  
             dummy = Convert.ToInt32(param[4]);             
             abc = String.Format("\n OPTIMIZATION FINISHED  --->  EVALSTOP (={0,9})",dummy);
         }    
         if(iflag == 7)
         {      
             abc = String.Format("\n OPTIMIZATION FINISHED  --->  FSTOP REACHED");
         }           
    
         if( iout == 0 ){ Console.WriteLine(abc); }
         if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   
         if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}}
         
         abc = String.Format("\n\n         BEST SOLUTION FOUND BY MIDACO"); 

         if( iout == 0 ){ Console.WriteLine(abc); }
         if( iout == 1 ){ using (StreamWriter w = File.AppendText("MIDACO_SCREEN.TXT")){ w.WriteLine(abc);} }   
         if( iout == 2 ){ using (StreamWriter w = File.AppendText("MIDACO_SOLUTION.TXT")){ w.WriteLine(abc);}} 
                           
         print_solution( o, n, m, me, x, g, f, vio, xl, xu, acc, eval, tnow, iflag, pf, iout );            
      }
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/ 
      public static void print_paretofront(long o, long m, long n, double[] pf, 
                                           double[] x, double[] g, long pfmax)
      {
        long psize;
        psize = Convert.ToInt32(pf[0]);
        long i;
        long k; 
        double dummy;

        iout3 = new StreamWriter("MIDACO_PARETOFRONT.tmp");
        iout3.Close();        

        abc = String.Format("#########################################################\n### This file contains the pareto front approximation ###\n#########################################################\n### Solution format:     F(1:O)    G(1:M)    X(1:N)   ###\n#########################################################\n#\n#        O         M         N     PSIZE\n#\n {0,9} {1,9} {2,9} {3,9} \n#\n#        MIDACO solution\n#", o, m, n, psize);
        using (StreamWriter w = File.AppendText("MIDACO_PARETOFRONT.tmp")){ w.WriteLine(abc);}

        abc = String.Format(" "); 

        for( i=0; i<o; i++)
        { 
          dummy = g[m+i];
          if( g[m+o+i] > 0.0 ){ dummy = -dummy; }
          
          abc = abc + String.Format(" {0,19:0.00000000}",dummy); 
        }
        for( i=0; i<m; i++)
        {           
          abc = abc + String.Format(" {0,19:0.00000000}",g[i]); 
        }    
        for( i=0; i<n; i++)
        {           
          abc = abc + String.Format(" {0,19:0.00000000}",x[i]);           
        }       
        using (StreamWriter w = File.AppendText("MIDACO_PARETOFRONT.tmp")){ w.WriteLine(abc);}         

        abc = String.Format("#\n#        All non-dominated solutions found by MIDACO\n#");
        using (StreamWriter w = File.AppendText("MIDACO_PARETOFRONT.tmp")){ w.WriteLine(abc);}

        for( k=0; k<psize; k++) 
        {  
            abc = String.Format(" ");
            
            for( i=0; i<o; i++)
            { 
              dummy = pf[ 1 + o*k + i ];          
              abc = abc + String.Format(" {0,19:0.00000000}",dummy); 
            }
            for( i=0; i<m; i++)
            { 
              dummy = pf[ 1 + o*pfmax+m*k + i ];          
              abc = abc + String.Format(" {0,19:0.00000000}",dummy); 
            }
            for( i=0; i<n; i++)
            { 
              dummy = pf[ 1 + o*pfmax+m*pfmax+n*k + i ];         
              abc = abc + String.Format(" {0,19:0.00000000}",dummy); 
            }   
            using (StreamWriter w = File.AppendText("MIDACO_PARETOFRONT.tmp")){ w.WriteLine(abc);}             
        }
 
        try{    System.IO.File.Delete("MIDACO_PARETOFRONT.TXT"); 
        }catch{ /* continue */ }
        
        System.IO.File.Copy("MIDACO_PARETOFRONT.tmp", "MIDACO_PARETOFRONT.TXT");

        try{    System.IO.File.Delete("MIDACO_PARETOFRONT.tmp"); 
        }catch{ /* continue */ }

      }
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/
      /*CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC*/ 
} /* END OF MIDACO_PRINTING */
