using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace victors.Migrations
{
    /// <inheritdoc />
    public partial class webjoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15c5ecae-8cff-4e42-9919-82e49201c2dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1dc752e7-e3a9-496d-ba37-86d00aae1be7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e454ea44-e169-45e6-9b66-6a861a274752");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9895f3b-095e-45c5-9a19-631583e6e0db");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Students",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Students",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "StudentFromWebsiteId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "24a2a1e9-7976-4b60-a552-dfe2a194a168", null, "Parent", "PARENT" },
                    { "9813c4c4-4d0d-4802-ad00-feee77c4e674", null, "Visitor", "VISITOR" },
                    { "c9fcde5e-797d-4d3f-9e43-b5877fa10772", null, "Burser", "BURSER" },
                    { "dfac0cb8-f28a-403f-bbf4-021490d735f2", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51009a63-58e3-45f0-b9b3-8442c0c3d847",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a391edb3-9655-4dc7-9570-a6c8f29b22a6", "AQAAAAIAAYagAAAAEHDx221JTXMvJJMyu65stNnzFSBpx08gJleA2vogpnnWvM3pAt7fnUDEDyEVP5hOxA==", "8a588c34-624b-4d5f-90fa-3e8362b84863" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24a2a1e9-7976-4b60-a552-dfe2a194a168");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9813c4c4-4d0d-4802-ad00-feee77c4e674");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9fcde5e-797d-4d3f-9e43-b5877fa10772");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfac0cb8-f28a-403f-bbf4-021490d735f2");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentFromWebsiteId",
                table: "Students");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15c5ecae-8cff-4e42-9919-82e49201c2dd", null, "Parent", "PARENT" },
                    { "1dc752e7-e3a9-496d-ba37-86d00aae1be7", null, "Administrator", "ADMINISTRATOR" },
                    { "e454ea44-e169-45e6-9b66-6a861a274752", null, "Visitor", "VISITOR" },
                    { "f9895f3b-095e-45c5-9a19-631583e6e0db", null, "Burser", "BURSER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51009a63-58e3-45f0-b9b3-8442c0c3d847",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d9c3a06-50fd-4806-b8e2-6170d995c46d", "AQAAAAIAAYagAAAAECRIPRmyUsmKseXC//Ytu7ijCYSEbKbZqMXgX158z0pnxdUwXHdCfJuFxsvpgX7m0w==", "dafe9159-37d7-4433-9642-2b09bebdf45c" });
        }
    }
}
