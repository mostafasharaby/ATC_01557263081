using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImageUrl", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Token", "TokenExpiryTime", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "admin1", 0, "f12c3de6-9ac1-4e3e-bedb-198b2d225a45", "admin1@example.com", false, null, false, null, "ADMIN1@EXAMPLE.COM", "ADMIN1", null, null, false, null, null, "34df1383-4441-4aa5-b31b-a3d583d248d0", null, null, false, "admin1" },
                    { "user1", 0, "0d789ce0-76a8-4e19-a921-27319e06ff0e", "Shady@example.com", false, null, false, null, "SHADY@EXAMPLE.COM", "SHADY", null, null, false, null, null, "faf53b25-a73c-48e0-9df6-b3eb9118d226", null, null, false, "Shady" },
                    { "user2", 0, "b1e8cebd-edb2-4189-93b8-fbdfce226bdb", "Mohamed@example.com", false, null, false, null, "MOHAMED@EXAMPLE.COM", "MOHAMED", null, null, false, null, null, "0a145f1e-c7aa-48b7-861d-fd9a8e5259fc", null, null, false, "Mohamed" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AvailableSeats", "Category", "CreatedAt", "CreatedBy", "Description", "EventDate", "ImageUrl", "IsDeleted", "Name", "Price", "UpdatedAt", "UpdatedBy", "Venue" },
                values: new object[,]
                {
                    { 8, 50000, "Football", new DateTime(2025, 5, 15, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5840), "user1", "The biggest football showdown of the year.", new DateTime(2025, 5, 30, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5831), "/images/events/football.webp", null, "Champions League Final", 99.99m, null, null, "National Stadium" },
                    { 9, 20000, "Basketball", new DateTime(2025, 5, 15, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5844), "user2", "Experience the thrill of top NBA talent in one spectacular game.", new DateTime(2025, 6, 14, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5842), "/images/events/basketball.jpeg", null, "NBA All-Star Game", 149.99m, null, null, "Madison Square Garden" },
                    { 10, 1000, "Food", new DateTime(2025, 5, 15, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5846), "user1", "A celebration of world cuisine and culinary innovation.", new DateTime(2025, 6, 4, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5845), "/images/events/food.jpg", null, "Gourmet Food Expo", 25.00m, null, null, "City Exhibition Hall" },
                    { 11, 10000, "Concert", new DateTime(2025, 5, 15, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5850), "user2", "Join the electrifying concert experience with top artists live.", new DateTime(2025, 6, 29, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5848), "/images/events/concert.jpeg", null, "Live Concert: The Soundwave Tour", 79.99m, null, null, "Open Air Arena" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 6, new DateTime(2025, 5, 15, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5804), null, null, "Music", null, null },
                    { 7, new DateTime(2025, 5, 15, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5807), null, null, "Conference", null, null },
                    { 8, new DateTime(2025, 5, 15, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5808), null, null, "Workshop", null, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ac58ca5b-0100-4200-8aa4-f43c458923b4", "admin1" },
                    { "a1ff9d3d-b691-4726-b3cd-92d0b0687001", "user1" },
                    { "a1ff9d3d-b691-4726-b3cd-92d0b0687001", "user2" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "CreatedAt", "CreatedBy", "EventId", "IsDeleted", "TicketCount", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[,]
                {
                    { 8, new DateTime(2025, 5, 15, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5943), new DateTime(2025, 5, 15, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5943), null, 10, null, 2, null, null, "user1" },
                    { 9, new DateTime(2025, 5, 15, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5945), new DateTime(2025, 5, 15, 17, 19, 16, 357, DateTimeKind.Utc).AddTicks(5945), null, 11, null, 1, null, null, "user2" }
                });

            migrationBuilder.InsertData(
                table: "EventTags",
                columns: new[] { "EventId", "TagId" },
                values: new object[,]
                {
                    { 8, 6 },
                    { 9, 7 },
                    { 10, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ac58ca5b-0100-4200-8aa4-f43c458923b4", "admin1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a1ff9d3d-b691-4726-b3cd-92d0b0687001", "user1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a1ff9d3d-b691-4726-b3cd-92d0b0687001", "user2" });

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EventTags",
                keyColumns: new[] { "EventId", "TagId" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                table: "EventTags",
                keyColumns: new[] { "EventId", "TagId" },
                keyValues: new object[] { 9, 7 });

            migrationBuilder.DeleteData(
                table: "EventTags",
                keyColumns: new[] { "EventId", "TagId" },
                keyValues: new object[] { 10, 8 });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user2");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
