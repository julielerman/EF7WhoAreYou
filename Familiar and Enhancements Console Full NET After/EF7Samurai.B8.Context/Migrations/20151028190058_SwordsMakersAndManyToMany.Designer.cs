using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using EF7Samurai.Context;

namespace EF7Samurai.B8.Context.Migrations
{
    [DbContext(typeof(SamuraiContext))]
    [Migration("20151028190058_SwordsMakersAndManyToMany")]
    partial class SwordsMakersAndManyToMany
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

                    b.Property<Guid>("AlternateKey");

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
                        .ForeignKey("LocationId");
                });

            modelBuilder.Entity("EF7Samurai.Domain.Quote", b =>
                {
                    b.HasOne("EF7Samurai.Domain.Samurai")
                        .WithMany()
                        .ForeignKey("SamuraiId");
                });

            modelBuilder.Entity("EF7Samurai.Domain.SamuraiBattle", b =>
                {
                    b.HasOne("EF7Samurai.Domain.Battle")
                        .WithMany()
                        .ForeignKey("BattleId");

                    b.HasOne("EF7Samurai.Domain.Samurai")
                        .WithMany()
                        .ForeignKey("SamuraiId");
                });

            modelBuilder.Entity("EF7Samurai.Domain.Sword", b =>
                {
                    b.HasOne("EF7Samurai.Domain.Maker")
                        .WithMany()
                        .ForeignKey("MakerId");

                    b.HasOne("EF7Samurai.Domain.Samurai")
                        .WithMany()
                        .ForeignKey("SamuraiId");
                });
        }
    }
}
