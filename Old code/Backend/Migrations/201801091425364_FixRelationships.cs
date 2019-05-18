namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixRelationships : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ingredients", new[] { "Meal_Id" });
            DropColumn("dbo.Ingredients", "MealId");
            RenameColumn(table: "dbo.Ingredients", name: "Meal_Id", newName: "MealId");
            AlterColumn("dbo.Ingredients", "MealId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ingredients", "MealId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ingredients", new[] { "MealId" });
            AlterColumn("dbo.Ingredients", "MealId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Ingredients", name: "MealId", newName: "Meal_Id");
            AddColumn("dbo.Ingredients", "MealId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ingredients", "Meal_Id");
        }
    }
}
