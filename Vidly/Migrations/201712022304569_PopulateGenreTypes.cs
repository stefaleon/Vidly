namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenreTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Action')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Romance')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Thriller')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Comedy')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'Crime')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (6, 'Horror')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (7, 'Western')");
        }
        
        public override void Down()
        {
        }
    }
}
