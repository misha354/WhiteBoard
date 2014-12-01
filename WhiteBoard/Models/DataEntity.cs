using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteBoard
{
   public abstract class DataEntity
   {
      //private and protected fields
      protected const string CONNECTION_STRING = "GradeBook";
      protected PetaPoco.Database db { get; set; }

      //public properties
//      public string Error { get; set; }

      //constructor
      public DataEntity()
      { }
      
      public DataEntity(PetaPoco.Database db)
      {
         this.db = db;
      }

      //open a connection to the dabase or null if can't open
      //protected static PetaPoco.Database GetDbConnection()
      //{
      //   PetaPoco.Database db;
      //   try
      //   {
      //      db = new PetaPoco.Database(CONNECTION_STRING);
      //   }
      //   catch
      //   {
      //      System.Diagnostics.Debug.WriteLine("Could not open the database");
      //      db = null;
      //   }

      //   return db;

      //Add the entity to the database
      public abstract bool Add();
   }
}
