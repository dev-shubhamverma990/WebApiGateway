using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiGateway.Migrations
{
    public partial class Princetonhive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCityMaster",
                columns: table => new
                {
                    Cityid = table.Column<int>(nullable: false),
                    CityName = table.Column<string>(type: "character varying(1000)", nullable: false),
                    Stateid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCityMaster", x => x.Cityid);
                });

            migrationBuilder.CreateTable(
                name: "tblClassMaster",
                columns: table => new
                {
                    Classid = table.Column<int>(nullable: false),
                    ClassName = table.Column<string>(type: "character varying(1000)", nullable: false),
                    Status = table.Column<string>(type: "character varying(1000)", nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblClassMaster", x => x.Classid);
                });

            migrationBuilder.CreateTable(
                name: "tblCountryMaster",
                columns: table => new
                {
                    Countryid = table.Column<int>(nullable: false),
                    CountryCode = table.Column<string>(type: "character varying(1000)", nullable: false),
                    CountryName = table.Column<string>(type: "character varying(1000)", nullable: false),
                    Phonecode = table.Column<string>(type: "character varying(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCountryMaster", x => x.Countryid);
                });

            migrationBuilder.CreateTable(
                name: "tblEnquiry",
                columns: table => new
                {
                    EnquiryId = table.Column<int>(nullable: false),
                    SchoolName = table.Column<string>(type: "character varying(1000)", nullable: false),
                    Location = table.Column<string>(type: "character varying(1000)", nullable: false),
                    Name = table.Column<string>(type: "character varying(500)", nullable: false),
                    Designation = table.Column<string>(type: "character varying(100)", nullable: false),
                    MobileNo = table.Column<string>(type: "character varying(50)", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", nullable: false),
                    Description = table.Column<string>(type: "character varying(50000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEnquiry", x => x.EnquiryId);
                });

            migrationBuilder.CreateTable(
                name: "tblException_Log",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    ErrorCode = table.Column<string>(type: "character varying(100)", nullable: false),
                    ErrorDescription = table.Column<string>(type: "character varying(50000)", nullable: false),
                    LogDateTime = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblException_Log", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblRegistration",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(type: "character varying(50)", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(500)", nullable: false),
                    LastName = table.Column<string>(type: "character varying(500)", nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(1000)", nullable: false),
                    Gender = table.Column<string>(type: "character varying(500)", nullable: false),
                    DOB = table.Column<DateTime>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "character varying(5000)", nullable: false),
                    City = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    Country = table.Column<int>(nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(100)", nullable: false),
                    SchoolCode = table.Column<string>(type: "character varying(100)", nullable: false),
                    Class = table.Column<string>(type: "character varying(100)", nullable: false),
                    Section = table.Column<string>(type: "character varying(100)", nullable: false),
                    Session = table.Column<string>(type: "character varying(100)", nullable: false),
                    FatherName = table.Column<string>(type: "character varying(100)", nullable: false),
                    FatherMobile = table.Column<string>(type: "character varying(100)", nullable: false),
                    FatherEmail = table.Column<string>(type: "character varying(100)", nullable: false),
                    FatherEducation = table.Column<string>(type: "character varying(100)", nullable: false),
                    FatherOccupation = table.Column<string>(type: "character varying(500)", nullable: false),
                    MotherName = table.Column<string>(type: "character varying(500)", nullable: false),
                    MotherMobile = table.Column<string>(type: "character varying(500)", nullable: false),
                    MotherEmail = table.Column<string>(type: "character varying(500)", nullable: false),
                    MotherEducation = table.Column<string>(type: "character varying(500)", nullable: false),
                    MotherOccupation = table.Column<string>(type: "character varying(500)", nullable: false),
                    Photo = table.Column<string>(type: "character varying(500)", nullable: false),
                    Status = table.Column<string>(type: "character varying(500)", nullable: false),
                    Username = table.Column<string>(type: "character varying(500)", nullable: false),
                    Email = table.Column<string>(type: "character varying(500)", nullable: false),
                    Mobileno = table.Column<string>(type: "character varying(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRegistration", x => x.RegistrationId);
                });

            migrationBuilder.CreateTable(
                name: "tblRoleMaster",
                columns: table => new
                {
                    Roleid = table.Column<int>(nullable: false),
                    RoleName = table.Column<string>(type: "character varying(100)", nullable: false),
                    RoleType = table.Column<string>(type: "character varying(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoleMaster", x => x.Roleid);
                });

            migrationBuilder.CreateTable(
                name: "tblSchool",
                columns: table => new
                {
                    Schoolid = table.Column<int>(nullable: false),
                    SchoolCode = table.Column<string>(type: "character varying(100)", nullable: false),
                    SchoolName = table.Column<string>(type: "character varying(1000)", nullable: false),
                    SchoolAddress = table.Column<string>(type: "character varying(2000)", nullable: false),
                    SchoolCity = table.Column<int>(nullable: false),
                    SchoolState = table.Column<int>(nullable: false),
                    SchoolCountry = table.Column<int>(nullable: false),
                    SchoolPostalCode = table.Column<string>(type: "character varying(100)", nullable: false),
                    SchoolLogo = table.Column<string>(type: "character varying(500)", nullable: false),
                    SchoolPhone = table.Column<string>(type: "character varying(100)", nullable: false),
                    SchoolEmail = table.Column<string>(type: "character varying(100)", nullable: false),
                    SchoolWebsite = table.Column<string>(type: "character varying(100)", nullable: false),
                    SchoolDescription = table.Column<string>(type: "character varying(1000)", nullable: false),
                    ContactPersonName = table.Column<string>(type: "character varying(1000)", nullable: false),
                    ContactPersonMobile = table.Column<string>(type: "character varying(100)", nullable: false),
                    ContactEmail = table.Column<string>(type: "character varying(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSchool", x => x.Schoolid);
                });

            migrationBuilder.CreateTable(
                name: "tblSectionMaster",
                columns: table => new
                {
                    Sectionid = table.Column<int>(nullable: false),
                    SectionName = table.Column<string>(type: "character varying(100)", nullable: false),
                    Status = table.Column<string>(type: "character varying(100)", nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSectionMaster", x => x.Sectionid);
                });

            migrationBuilder.CreateTable(
                name: "tblSessionMaster",
                columns: table => new
                {
                    Sessionid = table.Column<int>(nullable: false),
                    SessionName = table.Column<string>(type: "character varying(100)", nullable: false),
                    Status = table.Column<string>(type: "character varying(100)", nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSessionMaster", x => x.Sessionid);
                });

            migrationBuilder.CreateTable(
                name: "tblStateMaster",
                columns: table => new
                {
                    Stateid = table.Column<int>(nullable: false),
                    StateName = table.Column<string>(type: "character varying(500)", nullable: false),
                    Countryid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStateMaster", x => x.Stateid);
                });

            migrationBuilder.CreateTable(
                name: "tblUserAuth",
                columns: table => new
                {
                    UserAuthId = table.Column<int>(nullable: false),
                    UserRole = table.Column<string>(type: "character varying(500)", nullable: false),
                    UserAuthorities = table.Column<string>(type: "character varying(1000)", nullable: false),
                    UserStatus = table.Column<string>(type: "character varying(500)", nullable: false),
                    ModifyBy = table.Column<int>(nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserAuth", x => x.UserAuthId);
                });

            migrationBuilder.CreateTable(
                name: "tblUserLogin_HST",
                columns: table => new
                {
                    UserLoginHSTId = table.Column<int>(nullable: false),
                    LoginDateTime = table.Column<DateTime>(type: "date", nullable: false),
                    LogoutDateTime = table.Column<DateTime>(type: "date", nullable: false),
                    UserName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserLogin_HST", x => x.UserLoginHSTId);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    UserLoginId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(type: "character varying(500)", nullable: false),
                    Password = table.Column<string>(type: "character varying(500)", nullable: false),
                    Mobile = table.Column<string>(type: "character varying(500)", nullable: true),
                    Email = table.Column<string>(type: "character varying(500)", nullable: true),
                    Name = table.Column<string>(type: "character varying(500)", nullable: true),
                    City = table.Column<string>(type: "character varying(500)", nullable: true),
                    Captcha = table.Column<string>(type: "character varying(500)", nullable: true),
                    UserRole = table.Column<string>(type: "character varying(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.UserLoginId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCityMaster");

            migrationBuilder.DropTable(
                name: "tblClassMaster");

            migrationBuilder.DropTable(
                name: "tblCountryMaster");

            migrationBuilder.DropTable(
                name: "tblEnquiry");

            migrationBuilder.DropTable(
                name: "tblException_Log");

            migrationBuilder.DropTable(
                name: "tblRegistration");

            migrationBuilder.DropTable(
                name: "tblRoleMaster");

            migrationBuilder.DropTable(
                name: "tblSchool");

            migrationBuilder.DropTable(
                name: "tblSectionMaster");

            migrationBuilder.DropTable(
                name: "tblSessionMaster");

            migrationBuilder.DropTable(
                name: "tblStateMaster");

            migrationBuilder.DropTable(
                name: "tblUserAuth");

            migrationBuilder.DropTable(
                name: "tblUserLogin_HST");

            migrationBuilder.DropTable(
                name: "UserLogin");
        }
    }
}
