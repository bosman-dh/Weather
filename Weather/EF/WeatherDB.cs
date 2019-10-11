namespace Weather.EF
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Weather.Models;

    public class WeatherDB : DbContext
    {
        // Your context has been configured to use a 'WeatherDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Weather.EF.WeatherDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'WeatherDB' 
        // connection string in the application configuration file.
        public WeatherDB()
            : base("name=WeatherDB")
        {
            //Database.SetInitializer<WeatherDB>(new DropCreateDatabaseAlways<WeatherDB>());  //standard initializer
            Database.SetInitializer(new DataInitializer()); //user initializer
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<Data> Datas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Station>()
                .HasMany(wd => wd.Datas)
                .WithRequired(s => s.Station)
                .HasForeignKey(wd => wd.StationId)  //optional (convention)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Data>()
                .Property(wd => wd.DataId)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }
}