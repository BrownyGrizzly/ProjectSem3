using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineRailroad.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrainPassengerDetailRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrainNo",
                table: "PassengerDetails",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerDetails_TrainNo",
                table: "PassengerDetails",
                column: "TrainNo");

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerDetails_Trains_TrainNo",
                table: "PassengerDetails",
                column: "TrainNo",
                principalTable: "Trains",
                principalColumn: "TrainNo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassengerDetails_Trains_TrainNo",
                table: "PassengerDetails");

            migrationBuilder.DropIndex(
                name: "IX_PassengerDetails_TrainNo",
                table: "PassengerDetails");

            migrationBuilder.AlterColumn<string>(
                name: "TrainNo",
                table: "PassengerDetails",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
