using System;
using System.Globalization;
using Nancy;
using Nancy.ModelBinding;
using System.Data.SQLite;
using System.Collections.Generic;

namespace WhiteBoard
{
   public class GradeBookModule : NancyModule
   {
      public GradeBookModule(PetaPoco.Database db, List<Grade> grades, GradeBook gradebook)
         : base()
      {
         Get["/"] = _ =>
         {
            ViewBag["error"] = Session["error"];
            Session["error"] = null;

            return View["Index", grades];
         };

         Get["/About"] = _ =>
         {
            return View["About"];
         };


         Post["/AddGrade"] = _ =>
            {
               int gradeValue = 0;
               //Read the grade
               try
               {
                  gradeValue = Convert.ToInt32(Request.Form.NumberGrade.ToString());

                  if (gradeValue < 0 || gradeValue > 100)
                  {
                     throw new ArgumentOutOfRangeException();
                  }
               }
               catch
               {
                  Session["error"] = "Invalid grade (0-100)";
                  return Response.AsRedirect("/");
               }

               //Read the name
               TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
               string firstName = myTI.ToTitleCase(Request.Form.FirstName);
               string lastName = myTI.ToTitleCase(Request.Form.LastName);

               Student student = new Student(db, firstName, lastName);
               Grade grade = new Grade(db, student, gradeValue);

               using (var scope = db.GetTransaction())
               {
                  if (student.Add())
                  {
                     grade.Student.StudentId = student.StudentId;

                     if (grade.Add())
                     {
                        scope.Complete();
                        return Response.AsRedirect("/");
                     }
                     else
                     {
                        Session["error"] = "Problem adding grade";
                        return Response.AsRedirect("/");
                     }
                  }
                  else
                  {
                     Session["error"] = "Problem adding student (please check for a duplicate)";
                     return Response.AsRedirect("/");
                  }
               }
            };

         Get["Summary"] = _ =>
            {
               return View["Summary", gradebook];
            };
      }
   }
}


