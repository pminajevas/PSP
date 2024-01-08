using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoS.Infrastructure.Migrations
{
    public partial class KeyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2bf479a2-056a-4d85-a4d1-a17a664f7922"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("423e7d8b-9453-41c7-b85c-83887c09b048"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7b44b021-5480-4dc4-a1c4-9239bfcf2912"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e5d204e8-3114-4691-a1e2-55563eb820c1"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "RoleName" },
                values: new object[,]
                {
                    { new Guid("37e76957-c0e9-41e1-b7fc-26abe102a6e0"), "Customer has access to operations related to customer self-service", "Customer" },
                    { new Guid("745c6724-f255-4c2e-9976-be210824b534"), "Manager has access to all operations in the business", "Manager" },
                    { new Guid("92e68ff5-37fe-4598-91b0-448f4c3d44c3"), "Staff has access to most common operations in the business", "Staff" },
                    { new Guid("e039f4e6-d3bf-4318-82fd-60e95d88f40f"), "Administrator has access to all operations", "Admin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("37e76957-c0e9-41e1-b7fc-26abe102a6e0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("745c6724-f255-4c2e-9976-be210824b534"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("92e68ff5-37fe-4598-91b0-448f4c3d44c3"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e039f4e6-d3bf-4318-82fd-60e95d88f40f"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                columns: new[] { "Id", "BusinessId" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "RoleName" },
                values: new object[,]
                {
                    { new Guid("2bf479a2-056a-4d85-a4d1-a17a664f7922"), "Staff has access to most common operations in the business", "Staff" },
                    { new Guid("423e7d8b-9453-41c7-b85c-83887c09b048"), "Manager has access to all operations in the business", "Manager" },
                    { new Guid("7b44b021-5480-4dc4-a1c4-9239bfcf2912"), "Customer has access to operations related to customer self-service", "Customer" },
                    { new Guid("e5d204e8-3114-4691-a1e2-55563eb820c1"), "Administrator has access to all operations", "Admin" }
                });
        }
    }
}
