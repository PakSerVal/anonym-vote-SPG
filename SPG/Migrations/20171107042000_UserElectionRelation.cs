using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SPG.Migrations
{
    public partial class UserElectionRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVoter_Elections_ElectionId",
                table: "ElectionVoter");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVoter_Users_VoterId",
                table: "ElectionVoter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectionVoter",
                table: "ElectionVoter");

            migrationBuilder.RenameTable(
                name: "ElectionVoter",
                newName: "ElectionVoters");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionVoter_VoterId",
                table: "ElectionVoters",
                newName: "IX_ElectionVoters_VoterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectionVoters",
                table: "ElectionVoters",
                columns: new[] { "ElectionId", "VoterId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVoters_Elections_ElectionId",
                table: "ElectionVoters",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVoters_Users_VoterId",
                table: "ElectionVoters",
                column: "VoterId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVoters_Elections_ElectionId",
                table: "ElectionVoters");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectionVoters_Users_VoterId",
                table: "ElectionVoters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectionVoters",
                table: "ElectionVoters");

            migrationBuilder.RenameTable(
                name: "ElectionVoters",
                newName: "ElectionVoter");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionVoters_VoterId",
                table: "ElectionVoter",
                newName: "IX_ElectionVoter_VoterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectionVoter",
                table: "ElectionVoter",
                columns: new[] { "ElectionId", "VoterId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVoter_Elections_ElectionId",
                table: "ElectionVoter",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionVoter_Users_VoterId",
                table: "ElectionVoter",
                column: "VoterId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
