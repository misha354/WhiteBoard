using System;
using Nancy;
using Nancy.ModelBinding;
using System.Data.SQLite;
using System.Collections.Generic;

namespace WhiteBoard
{
   public class GradeBookModule : NancyModule
   {
      public GradeBookModule(PetaPoco.Database db, List<Grade> grades)
         : base()
      {
         Get["/"] = _ =>
         {
            ViewBag["error"] = Session["error"];
            Session["error"] = null;

            return View["Index", grades];
         };

         Post["/AddGrade"] = _ =>
            {
               int gradeValue = 0;
               //Read the grade
               try
               {
                  gradeValue = Convert.ToInt32(Request.Form.NumberGrade.ToString());
               }
               catch
               {
                  ViewBag["error"] = "Invalid grade";
                  return View["Index", grades];
               }

               Student student = new Student(db, Request.Form.FirstName, Request.Form.LastName);
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
                        ViewBag["error"] = "Problem adding grade";
                        return View["Index", grades];
                     }
                  }
                  else
                  {
                     ViewBag["error"] = "Problem adding student (please check for a duplicate)";
                     return View["Index", new List<Grade>()];
                  }
               }
            };
      }
   }
}


