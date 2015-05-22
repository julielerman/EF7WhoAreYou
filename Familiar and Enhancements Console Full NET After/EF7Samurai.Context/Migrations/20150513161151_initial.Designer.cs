using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using EF7Samurai.Context;

namespace EF7Samurai.Context.Migrations
{
    [ContextType(typeof(SamuraiContext))]
    partial class initial
    {
        public override string Id
        {
            get { return "20150513161151_initial"; }
        }
        
        public override string ProductVersion
        {
            get { return "7.0.0-beta4-12943"; }
        }
        
        public override IModel Target
        {
            get
            {
                var builder = new BasicModelBuilder()
                    .Annotation("SqlServer:ValueGeneration", "Sequence");
                
                builder.Entity("EF7Samurai.Domain.Battle", b =>
                    {
                        b.Property<DateTime>("EndDate")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<DateTime>("StartDate")
                            .Annotation("OriginalValueIndex", 3);
                        b.Key("Id");
                    });
                
                builder.Entity("EF7Samurai.Domain.Quote", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<int>("SamuraiId")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("Text")
                            .Annotation("OriginalValueIndex", 2);
                        b.Key("Id");
                    });
                
                builder.Entity("EF7Samurai.Domain.Samurai", b =>
                    {
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 0)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<string>("Name")
                            .Annotation("OriginalValueIndex", 1);
                        b.Key("Id");
                    });
                
                builder.Entity("EF7Samurai.Domain.Quote", b =>
                    {
                        b.ForeignKey("EF7Samurai.Domain.Samurai", "SamuraiId");
                    });
                
                return builder.Model;
            }
        }
    }
}
