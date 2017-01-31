using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.Web.Models
{
    public class EmotionPlatziWebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        //Recrear estrategia de Base de Datos --
        public EmotionPlatziWebContext() : base("name=EmotionPlatziAzure")
        {
            //Tipo del DataContext (podría ser una clase aparte
            Database.SetInitializer<EmotionPlatziWebContext>(
                new DropCreateDatabaseIfModelChanges<EmotionPlatziWebContext>());

                //new MigrateDatabaseToLatestVersion
        }

        
        public DbSet<EmotionPlatzi.Web.Models.EmoPicture> EmoPictures { get; set; }

        //Se crean las cadenas adicionales 
        public DbSet<EmotionPlatzi.Web.Models.EmoFace> EmoFaces { get; set; }

        public DbSet<EmotionPlatzi.Web.Models.EmoEmotion> EmoEmotions { get; set; }

        public System.Data.Entity.DbSet<EmotionPlatzi.Web.Models.Home> Homes { get; set; }
    }
    /*
    //Otras estrategias de inicialización 
    public class SchoolDBContext: DbContext
    {
        public SchoolDBContext() : base("SchoolDBConnectionString")
        {
            Database.SetInitializer<SchoolDBContext>(
                new CreateDatabaseIfNotExists<SchoolDBContext>());
            Database.SetInitializer<SchoolDBContext>(
                new DropCreateDatabaseIfModelChanges<SchoolDBContext>());
            Database.SetInitializer<SchoolDBContext>(
                new DropCreateDatabaseAlways<SchoolDBContext>());
        }
    }*/

}
