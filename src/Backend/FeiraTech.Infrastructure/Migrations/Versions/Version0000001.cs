using FluentMigrator;

namespace FeiraTech.Infrastructure.Migrations.Versions
{
    //Criando a primeira versão do banco de dados usando o FluentMigrator

    [Migration(DatabaseVersions.TABLE_USERS, "Created table the user save information")]
    public class Version0000001 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Users")
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Email").AsString(100).NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable()
                .WithColumn("CPF").AsString(11).NotNullable()
                .WithColumn("Phone").AsString(11).NotNullable();



        }
    }
}
