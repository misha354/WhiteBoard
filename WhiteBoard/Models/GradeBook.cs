using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteBoard
{
   public class GradeBook
   {
      //private fields
      private List<Grade> grades;

      //Properties
      //The list of grade objects
      public List<Grade> Grades
      { 
         get
         {
            return grades;
         }
      }

      //returns the average of the grades in the gradebook
      public double Average
      {
         get
         {
            return grades.Average(s => s.NumberGrade);
         }
      }

      //returns the maximum grade
      public int Max
      {
         get
         {
            return grades.Max(s => s.NumberGrade);
         }
      }

      //returns the minimum grade
      public int Min
      {
         get
         {
            return grades.Min(s => s.NumberGrade);
         }
      }
   
      //Constructor
      public GradeBook(List<Grade> grades)
      {
         this.grades = grades;
      }

      //Methods

      // output bar chart displaying grade distribution
      public string OutputBarChart()
      {
         const int NUM_BINS = 11; //Number of bins in the histogram
         string[] BIN_LABELS =  { "00-09", "10-19", "20-29", "30-39", "40-49",
                                   "50-59", "60-69", "70-79", "80-89", "90-99",
                                   "   100" }; 

         int[] bins = new int[NUM_BINS]; //the histogram bins
         
         //the output string
         string histogram = "Grade Distribution:\n";

         //Build the bin counts
         foreach (Grade grade in grades)
         {
            bins[grade.NumberGrade / 10]++;
         }

         //Print the histogram
         for (int i = 0; i < NUM_BINS; i++)
         {
            histogram += BIN_LABELS[i] + ": ";
            for (int j = 0; j < bins[i]; j++)
            {
               histogram += "*";
            }
            histogram += "\n";
         }
         return histogram;
      }


   }
}