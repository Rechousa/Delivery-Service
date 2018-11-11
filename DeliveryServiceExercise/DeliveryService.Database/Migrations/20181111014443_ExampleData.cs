using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryService.Database.Migrations
{
    public partial class ExampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO dbo.Locations VALUES ('Amarante');
INSERT INTO dbo.Locations VALUES ('Felgueiras');
INSERT INTO dbo.Locations VALUES ('Gondomar');
INSERT INTO dbo.Locations VALUES ('Maia');
INSERT INTO dbo.Locations VALUES ('Marco de Canavezes');
INSERT INTO dbo.Locations VALUES ('Matosinhos');
INSERT INTO dbo.Locations VALUES ('Paços de Ferreira');
INSERT INTO dbo.Locations VALUES ('Paredes');
INSERT INTO dbo.Locations VALUES ('Penafiel');
INSERT INTO dbo.Locations VALUES ('Porto');
INSERT INTO dbo.Locations VALUES ('Póvoa de Varzim');
INSERT INTO dbo.Locations VALUES ('Santo Tirso');
INSERT INTO dbo.Locations VALUES ('Valongo');
INSERT INTO dbo.Locations VALUES ('Vila do Conde');
INSERT INTO dbo.Locations VALUES ('Vila Nova de Gaia');

INSERT INTO dbo.Routes VALUES(1, 2, 33, 3);
INSERT INTO dbo.Routes VALUES(1, 3, 48, 10);
INSERT INTO dbo.Routes VALUES(1, 4, 45, 10);
INSERT INTO dbo.Routes VALUES(1, 5, 26, 3);
INSERT INTO dbo.Routes VALUES(1, 6, 44, 11);
INSERT INTO dbo.Routes VALUES(1, 7, 33, 7);
INSERT INTO dbo.Routes VALUES(1, 8, 23, 5);
INSERT INTO dbo.Routes VALUES(1, 9, 21, 5);
INSERT INTO dbo.Routes VALUES(1, 10, 48, 11);
INSERT INTO dbo.Routes VALUES(1, 11, 61, 14);
INSERT INTO dbo.Routes VALUES(1, 12, 50, 9);
INSERT INTO dbo.Routes VALUES(1, 13, 34, 9);
INSERT INTO dbo.Routes VALUES(1, 14, 60, 14);
INSERT INTO dbo.Routes VALUES(1, 15, 49, 11);
INSERT INTO dbo.Routes VALUES(2, 3, 45, 10);
INSERT INTO dbo.Routes VALUES(2, 4, 38, 9);
INSERT INTO dbo.Routes VALUES(2, 5, 31, 5);
INSERT INTO dbo.Routes VALUES(2, 6, 44, 10);
INSERT INTO dbo.Routes VALUES(2, 7, 25, 6);
INSERT INTO dbo.Routes VALUES(2, 8, 28, 6);
INSERT INTO dbo.Routes VALUES(2, 9, 26, 5);
INSERT INTO dbo.Routes VALUES(2, 10, 47, 10);
INSERT INTO dbo.Routes VALUES(2, 11, 60, 10);
INSERT INTO dbo.Routes VALUES(2, 12, 51, 4);
INSERT INTO dbo.Routes VALUES(2, 13, 37, 9);
INSERT INTO dbo.Routes VALUES(2, 14, 57, 10);
INSERT INTO dbo.Routes VALUES(2, 15, 47, 11);
INSERT INTO dbo.Routes VALUES(3, 4, 19, 2);
INSERT INTO dbo.Routes VALUES(3, 5, 44, 8);
INSERT INTO dbo.Routes VALUES(3, 6, 18, 2);
INSERT INTO dbo.Routes VALUES(3, 7, 31, 5);
INSERT INTO dbo.Routes VALUES(3, 8, 26, 5);
INSERT INTO dbo.Routes VALUES(3, 9, 31, 6);
INSERT INTO dbo.Routes VALUES(3, 10, 16, 2);
INSERT INTO dbo.Routes VALUES(3, 11, 36, 6);
INSERT INTO dbo.Routes VALUES(3, 12, 27, 5);
INSERT INTO dbo.Routes VALUES(3, 13, 18, 1);
INSERT INTO dbo.Routes VALUES(3, 14, 35, 5);
INSERT INTO dbo.Routes VALUES(3, 15, 17, 2);
INSERT INTO dbo.Routes VALUES(4, 5, 45, 9);
INSERT INTO dbo.Routes VALUES(4, 6, 13, 1);
INSERT INTO dbo.Routes VALUES(4, 7, 24, 5);
INSERT INTO dbo.Routes VALUES(4, 8, 26, 5);
INSERT INTO dbo.Routes VALUES(4, 9, 31, 6);
INSERT INTO dbo.Routes VALUES(4, 10, 18, 2);
INSERT INTO dbo.Routes VALUES(4, 11, 24, 5);
INSERT INTO dbo.Routes VALUES(4, 12, 21, 4);
INSERT INTO dbo.Routes VALUES(4, 13, 16, 2);
INSERT INTO dbo.Routes VALUES(4, 14, 26, 3);
INSERT INTO dbo.Routes VALUES(4, 15, 17, 2);
INSERT INTO dbo.Routes VALUES(5, 6, 44, 9);
INSERT INTO dbo.Routes VALUES(5, 7, 31, 5);
INSERT INTO dbo.Routes VALUES(5, 8, 23, 4);
INSERT INTO dbo.Routes VALUES(5, 9, 21, 3);
INSERT INTO dbo.Routes VALUES(5, 10, 48, 9);
INSERT INTO dbo.Routes VALUES(5, 11, 61, 13);
INSERT INTO dbo.Routes VALUES(5, 12, 48, 7);
INSERT INTO dbo.Routes VALUES(5, 13, 34, 7);
INSERT INTO dbo.Routes VALUES(5, 14, 59, 12);
INSERT INTO dbo.Routes VALUES(5, 15, 48, 10);
INSERT INTO dbo.Routes VALUES(6, 7, 30, 5);
INSERT INTO dbo.Routes VALUES(6, 8, 26, 5);
INSERT INTO dbo.Routes VALUES(6, 9, 31, 6);
INSERT INTO dbo.Routes VALUES(6, 10, 16, 1);
INSERT INTO dbo.Routes VALUES(6, 11, 24, 4);
INSERT INTO dbo.Routes VALUES(6, 12, 25, 4);
INSERT INTO dbo.Routes VALUES(6, 13, 16, 2);
INSERT INTO dbo.Routes VALUES(6, 14, 22, 3);
INSERT INTO dbo.Routes VALUES(6, 15, 13, 1);
INSERT INTO dbo.Routes VALUES(7, 8, 17, 2);
INSERT INTO dbo.Routes VALUES(7, 9, 22, 3);
INSERT INTO dbo.Routes VALUES(7, 10, 33, 5);
INSERT INTO dbo.Routes VALUES(7, 11, 44, 9);
INSERT INTO dbo.Routes VALUES(7, 12, 26, 2);
INSERT INTO dbo.Routes VALUES(7, 13, 23, 4);
INSERT INTO dbo.Routes VALUES(7, 14, 44, 7);
INSERT INTO dbo.Routes VALUES(7, 15, 34, 6);
INSERT INTO dbo.Routes VALUES(8, 9, 9, 1);
INSERT INTO dbo.Routes VALUES(8, 10, 30, 5);
INSERT INTO dbo.Routes VALUES(8, 11, 42, 9);
INSERT INTO dbo.Routes VALUES(8, 12, 36, 4);
INSERT INTO dbo.Routes VALUES(8, 13, 15, 3);
INSERT INTO dbo.Routes VALUES(8, 14, 40, 8);
INSERT INTO dbo.Routes VALUES(8, 15, 29, 6);
INSERT INTO dbo.Routes VALUES(9, 10, 33, 6);
INSERT INTO dbo.Routes VALUES(9, 11, 46, 10);
INSERT INTO dbo.Routes VALUES(9, 12, 40, 5);
INSERT INTO dbo.Routes VALUES(9, 13, 19, 4);
INSERT INTO dbo.Routes VALUES(9, 14, 45, 9);
INSERT INTO dbo.Routes VALUES(9, 15, 34, 7);
INSERT INTO dbo.Routes VALUES(10, 11, 34, 5);
INSERT INTO dbo.Routes VALUES(10, 12, 26, 4);
INSERT INTO dbo.Routes VALUES(10, 13, 17, 2);
INSERT INTO dbo.Routes VALUES(10, 14, 32, 4);
INSERT INTO dbo.Routes VALUES(10, 15, 13, 1);
INSERT INTO dbo.Routes VALUES(11, 12, 30, 7);
INSERT INTO dbo.Routes VALUES(11, 13, 34, 6);
INSERT INTO dbo.Routes VALUES(11, 14, 7, 1);
INSERT INTO dbo.Routes VALUES(11, 15, 32, 5);
INSERT INTO dbo.Routes VALUES(12, 13, 25, 6);
INSERT INTO dbo.Routes VALUES(12, 14, 28, 7);
INSERT INTO dbo.Routes VALUES(12, 15, 29, 5);
INSERT INTO dbo.Routes VALUES(13, 14, 32, 5);
INSERT INTO dbo.Routes VALUES(13, 15, 21, 3);
INSERT INTO dbo.Routes VALUES(14, 15, 30, 4);
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
