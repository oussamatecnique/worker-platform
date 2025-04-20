using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace worker.platform.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_task_type_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequest_JobCategory_JobCategoryId",
                table: "JobRequest");

            migrationBuilder.DropTable(
                name: "JobCategoryParamsDefinition");

            migrationBuilder.RenameColumn(
                name: "JobCategoryId",
                table: "JobRequest",
                newName: "TaskTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_JobRequest_JobCategoryId",
                table: "JobRequest",
                newName: "IX_JobRequest_TaskTypeId");

            migrationBuilder.CreateTable(
                name: "TaskType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JobCategoryId = table.Column<int>(type: "int", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskType_JobCategory_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypeParamsDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttributeType = table.Column<int>(type: "int", nullable: false),
                    TaskTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypeParamsDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskTypeParamsDefinition_TaskType_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TaskType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskType_JobCategoryId",
                table: "TaskType",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTypeParamsDefinition_TaskTypeId",
                table: "TaskTypeParamsDefinition",
                column: "TaskTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequest_TaskType_TaskTypeId",
                table: "JobRequest",
                column: "TaskTypeId",
                principalTable: "TaskType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequest_TaskType_TaskTypeId",
                table: "JobRequest");

            migrationBuilder.DropTable(
                name: "TaskTypeParamsDefinition");

            migrationBuilder.DropTable(
                name: "TaskType");

            migrationBuilder.RenameColumn(
                name: "TaskTypeId",
                table: "JobRequest",
                newName: "JobCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_JobRequest_TaskTypeId",
                table: "JobRequest",
                newName: "IX_JobRequest_JobCategoryId");

            migrationBuilder.CreateTable(
                name: "JobCategoryParamsDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCategoryId = table.Column<int>(type: "int", nullable: false),
                    AttributeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttributeType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategoryParamsDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobCategoryParamsDefinition_JobCategory_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobCategoryParamsDefinition_JobCategoryId",
                table: "JobCategoryParamsDefinition",
                column: "JobCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequest_JobCategory_JobCategoryId",
                table: "JobRequest",
                column: "JobCategoryId",
                principalTable: "JobCategory",
                principalColumn: "Id");
        }
    }
}
