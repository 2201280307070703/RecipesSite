using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipesSite.Data.Migrations
{
    public partial class SeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalLikesCount",
                table: "Dishes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pastry" },
                    { 2, "Desserts" },
                    { 3, "Starters" },
                    { 4, "Soups" },
                    { 5, "Main Dishes" }
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "CategoryId", "CookingTime", "Description", "ImageUrl", "Ingredients", "Name", "PreparationSteps" },
                values: new object[] { 1, 4, 60, "This is very tasty and healthy soup!", "https://media.istockphoto.com/id/1173599844/photo/mulligatawny-soup-with-naan.jpg?s=612x612&w=0&k=20&c=5eE4TJT_AG6CL-eoCohFMmTcGwOd_dH3tkTdbGX4nl0=", "Chicken, Potatoes, Fresh Herbs", "Chicken soup", "Place a large dutch oven or pot over medium high heat and add in oil. Once oil is hot, add in garlic, onion, carrots and celery and so on..." });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "CategoryId", "CookingTime", "Description", "ImageUrl", "Ingredients", "Name", "PreparationSteps" },
                values: new object[] { 2, 2, 120, "This is very tasty and juicy cake. You will love it!", "https://images.unsplash.com/photo-1588195538326-c5b1e9f80a1b?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxleHBsb3JlLWZlZWR8Mnx8fGVufDB8fHx8fA%3D%3D&w=1000&q=80", "Sugar, Butter, Eggs, Baking powder,Milk", "Cake", "Place white sugar and butter into a mixing bowl. Beat with an electric mixer on medium speed until light and fluffy and so on..." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<int>(
                name: "TotalLikesCount",
                table: "Dishes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}
