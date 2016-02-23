using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using EF7Samurai.Model;

namespace EF7Samurai.Model.Migrations
{
    [DbContext(typeof(SamuraiContext))]
    [Migration("20160223025804_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

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

            modelBuilder.Entity("EF7Samurai.Domain.Maker", b =>
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

            modelBuilder.Entity("EF7Samurai.Domain.SamuraiBattle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BattleId");

                    b.Property<DateTime>("DateJoined");

                    b.Property<int>("SamuraiId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("EF7Samurai.Domain.Sword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MakerId");

                    b.Property<int>("SamuraId");

                    b.Property<int?>("SamuraiId");

                    b.Property<int>("WeightGrams");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("EF7Samurai.Domain.Battle", b =>
                {
                    b.HasOne("EF7Samurai.Domain.Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("EF7Samurai.Domain.Quote", b =>
                {
                    b.HasOne("EF7Samurai.Domain.Samurai")
                        .WithMany()
                        .HasForeignKey("SamuraiId");
                });

            modelBuilder.Entity("EF7Samurai.Domain.SamuraiBattle", b =>
                {
                    b.HasOne("EF7Samurai.Domain.Battle")
                        .WithMany()
                        .HasForeignKey("BattleId");

                    b.HasOne("EF7Samurai.Domain.Samurai")
                        .WithMany()
                        .HasForeignKey("SamuraiId");
                });

            modelBuilder.Entity("EF7Samurai.Domain.Sword", b =>
                {
                    b.HasOne("EF7Samurai.Domain.Maker")
                        .WithMany()
                        .HasForeignKey("MakerId");

                    b.HasOne("EF7Samurai.Domain.Samurai")
                        .WithMany()
                        .HasForeignKey("SamuraiId");
                });
        }
    }
}
