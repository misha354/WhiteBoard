namespace WhiteBoard
{
   using System.Collections.Generic;
   using Nancy;
   using Nancy.Bootstrapper;
   using Nancy.TinyIoc;
   using Nancy.Session;


   public class Bootstrapper : DefaultNancyBootstrapper
   {
      // The bootstrapper enables you to reconfigure the composition of the framework,
      // by overriding the various methods and properties.
      // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

      protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
      {
         CookieBasedSessions.Enable(pipelines);
      }

      protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
      {
         base.ConfigureRequestContainer(container, context);

         var db = new PetaPoco.Database("GradeBook");
         var sql = PetaPoco.Sql.Builder.Select("Grades.GradeId, Grades.NumberGrade, Students.StudentId, Students.FirstName, Students.LastName").From("Grades").InnerJoin("Students").On("Students.StudentId = Grades.StudentId");
         List<Grade> grades = db.Fetch<Grade, Student>(sql);
         container.Register(db);
         container.Register(grades);
      }
   }
}