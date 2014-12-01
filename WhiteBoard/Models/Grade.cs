using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteBoard
{
   public class Grade : DataEntity
   {
      public int GradeId { get; set; }
      public int NumberGrade { get; set; }
      public Student Student { get; set; }

      //constructors
      public Grade() { }

      public Grade(PetaPoco.Database db)
         : base(db)
      {
         this.GradeId = 0;
         this.Student = null;
         this.NumberGrade = 0;
      }

      public Grade(PetaPoco.Database db, Student student, int numberGrade)
         : base(db)
      {
         this.GradeId = 0;
         this.Student = student;
         this.NumberGrade = numberGrade;
      }

      public Grade(PetaPoco.Database db, Student student, int gradeId, int numberGrade)
         : base(db)
      {
         this.GradeId = gradeId;
         this.Student = student;
         this.NumberGrade = numberGrade;
      }

      //public methods
      //Tries to add the grade to the database
      public override bool Add()
      {
         try
         {
            using (var scope = db.GetTransaction())
            {
               db.Insert("Grades", "GradeId", true, new { StudentId = this.Student.StudentId,
                                                          NumberGrade = this.NumberGrade});
               scope.Complete();
            }
            return true;
         }
         catch
         {
            return false;
         }
      }
   }
}