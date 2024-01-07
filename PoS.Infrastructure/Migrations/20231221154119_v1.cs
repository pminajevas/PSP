using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoS.Infrastructure.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("02348180-7730-485e-bd6b-2dcd6db45ebd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("208fd140-7967-443a-9e7d-2b443a54f006"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4cbb02d6-e041-4cb2-964e-b66723721ab2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9c316c8f-24f2-49a9-8093-d7c2f9e0a023"));

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "StaffMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "RoleName" },
                values: new object[,]
                {
                    { new Guid("07ce4f6b-22a9-422a-803b-2dfe4562b677"), "Administrator has access to all operations", "Admin" },
                    { new Guid("497bc919-5151-40e0-bb12-392f170ea584"), "Customer has access to operations related to customer self-service", "Customer" },
                    { new Guid("5f5aebb8-7fa7-43fc-a9f4-3bf272648c72"), "Manager has access to all operations in the business", "Manager" },
                    { new Guid("790d8e8e-a1fb-4181-a797-dd3d3dff2a29"), "Staff has access to most common operations in the business", "Staff" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("07ce4f6b-22a9-422a-803b-2dfe4562b677"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("497bc919-5151-40e0-bb12-392f170ea584"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5f5aebb8-7fa7-43fc-a9f4-3bf272648c72"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("790d8e8e-a1fb-4181-a797-dd3d3dff2a29"));

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "StaffMembers");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "RoleName" },
                values: new object[,]
                {
                    { new Guid("02348180-7730-485e-bd6b-2dcd6db45ebd"), "Manager has access to all operations in the business", "Manager" },
                    { new Guid("208fd140-7967-443a-9e7d-2b443a54f006"), "Administrator has access to all operations", "Admin" },
                    { new Guid("4cbb02d6-e041-4cb2-964e-b66723721ab2"), "Customer has access to operations related to customer self-service", "Customer" },
                    { new Guid("9c316c8f-24f2-49a9-8093-d7c2f9e0a023"), "Staff has access to most common operations in the business", "Staff" }
                });
        }
    }
}
