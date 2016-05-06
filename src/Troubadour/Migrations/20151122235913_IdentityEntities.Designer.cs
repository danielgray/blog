using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Troubadour.Models;

namespace Troubadour.Migrations
{
    [DbContext(typeof(TroubadourContext))]
    [Migration("20151122235913_IdentityEntities")]
    partial class IdentityEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Troubadour.Models.Response", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AuthorId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("PublishedAt");

                    b.Property<long?>("StoryId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Troubadour.Models.Story", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AuthorId");

                    b.Property<string>("Content");

                    b.Property<int>("PublishStatus");

                    b.Property<DateTime>("PublishedAt");

                    b.Property<string>("Title");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Troubadour.Models.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<long?>("StoryId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Troubadour.Models.Response", b =>
                {
                    b.HasOne("Troubadour.Models.Story")
                        .WithMany()
                        .ForeignKey("StoryId");
                });

            modelBuilder.Entity("Troubadour.Models.Tag", b =>
                {
                    b.HasOne("Troubadour.Models.Story")
                        .WithMany()
                        .ForeignKey("StoryId");
                });
        }
    }
}
