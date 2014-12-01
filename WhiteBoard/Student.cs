using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteBoard
{
   public class Student : DataEntity
   {
      //Constructors
      public Student() { }

      public Student(PetaPoco.Database db) : base(db)
      {
         this.StudentId = 0;
         this.FirstName = "";
         this.LastName = "";
      }

      public Student(PetaPoco.Database db, string firstName, string lastName) : base(db)
      {
         this.FirstName = firstName;
         this.LastName = lastName;
         this.StudentId = 0;
      }

      public Student(PetaPoco.Database db, int studentId, string firstName, string lastName) : base(db)
      {
         this.FirstName = firstName;
         this.LastName = lastName;
         this.StudentId = studentId;
      }

      //public properties
      public int StudentId { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }

      public override bool Add()
      {
         try
         { 
            PetaPoco.Sql query = PetaPoco.Sql.Builder
                                 .Append("SELECT * FROM Students")
                                 .Append("WHERE FirstName=@0 AND LastName=@1", FirstName, LastName);

            var a = db.Fetch<Student>(query);
         
            if (a.Count > 0)
            {             
               //If so, return false and state the error
           //    this.Error = "Duplicates not allowed.";
               return false;
            }

            //Try to insert
            using (var scope = db.GetTransaction())
            {
               db.Insert("Students", "StudentId", this);
               scope.Complete();
            }
         }
         catch
         {
//            this.Error = "Opened database, but couldn't save.";
            return false;
         }

         return true;
      }
   }
}