using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using CookieCounterApp;

namespace UWPCookies.Migrations
{
    [DbContext(typeof(BingeContext))]
    [Migration("20151026005510_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964");

            modelBuilder.Entity("CookieCounterApp.CookieBinge", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HowMany");

                    b.Property<DateTime>("TimeOccurred");

                    b.Property<bool>("WorthIt");

                    b.HasKey("Id");
                });
        }
    }
}
