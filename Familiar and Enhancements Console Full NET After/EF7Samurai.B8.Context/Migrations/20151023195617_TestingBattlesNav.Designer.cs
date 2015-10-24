using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using EF7Samurai.Context;

namespace EF7Samurai.B8.Context.Migrations
{
    [DbContext(typeof(SamuraiContext))]
    [Migration("20151023195617_TestingBattlesNav")]
    partial class TestingBattlesNav
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EF7Samurai.Domain.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<int?>("LocationId");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("EF7Samurai.Domain.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("EF7Samurai.Domain.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SamuraiId");

                    b.Property<string>("Text");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("EF7Samurai.Domain.Samurai", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("EF7Samurai.Domain.Battle", b =>
                {
                    b.HasOne("EF7Samurai.Domain.Location")
                        .WithMany()
                        .ForeignKey("LocationId");
                });

            modelBuilder.Entity("EF7Samurai.Domain.Quote", b =>
                {
                    b.HasOne("EF7Samurai.Domain.Samurai")
                        .WithMany()
                        .ForeignKey("SamuraiId");
                });
        }
    }
}
