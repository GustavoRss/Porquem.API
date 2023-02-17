﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PQ.Data.Context;

namespace PQ.Data.Migrations
{
    [DbContext(typeof(PQContext))]
    partial class PQContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("CampaignHelpItem", b =>
                {
                    b.Property<int>("HelpItemsId")
                        .HasColumnType("int");

                    b.Property<int>("CampaignsPhilanthropicEntityId")
                        .HasColumnType("int");

                    b.Property<Guid>("CampaignsId")
                        .HasColumnType("char(36)");

                    b.HasKey("HelpItemsId", "CampaignsPhilanthropicEntityId", "CampaignsId");

                    b.HasIndex("CampaignsPhilanthropicEntityId", "CampaignsId");

                    b.ToTable("CampaignHelpItem");
                });

            modelBuilder.Entity("PQ.Core.Domain.Address", b =>
                {
                    b.Property<int>("PhilanthropicEntityId")
                        .HasColumnType("int");

                    b.Property<string>("CEP")
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Complement")
                        .HasColumnType("longtext");

                    b.Property<string>("District")
                        .HasColumnType("longtext");

                    b.Property<string>("Number")
                        .HasColumnType("longtext");

                    b.Property<string>("PublicPlace")
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .HasColumnType("longtext");

                    b.HasKey("PhilanthropicEntityId");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("PQ.Core.Domain.Campaign", b =>
                {
                    b.Property<int>("PhilanthropicEntityId")
                        .HasColumnType("int");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FeedBack")
                        .HasColumnType("longtext");

                    b.Property<string>("HowHelp")
                        .HasColumnType("longtext");

                    b.Property<string>("Logo")
                        .HasColumnType("longtext");

                    b.Property<string>("Objective")
                        .HasColumnType("longtext");

                    b.Property<string>("Slogan")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Wallpaper")
                        .HasColumnType("longtext");

                    b.HasKey("PhilanthropicEntityId", "Id");

                    b.ToTable("Campaign");
                });

            modelBuilder.Entity("PQ.Core.Domain.Document", b =>
                {
                    b.Property<int>("PhilanthropicEntityId")
                        .HasColumnType("int");

                    b.Property<string>("DocumentPath")
                        .HasColumnType("varchar(255)");

                    b.Property<byte[]>("DocumentData")
                        .HasColumnType("longblob");

                    b.HasKey("PhilanthropicEntityId", "DocumentPath");

                    b.HasIndex("PhilanthropicEntityId")
                        .IsUnique();

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("PQ.Core.Domain.HelpItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("HelpType")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("HelpItems");
                });

            modelBuilder.Entity("PQ.Core.Domain.PhilanthropicEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cause")
                        .HasColumnType("longtext");

                    b.Property<string>("Cnpj")
                        .HasColumnType("longtext");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("CorporateName")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DtOpening")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FantasyName")
                        .HasColumnType("longtext");

                    b.Property<string>("History")
                        .HasColumnType("longtext");

                    b.Property<string>("Logo")
                        .HasColumnType("longtext");

                    b.Property<string>("StateRegistration")
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("Telephone")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("PhilanthropicEntities");
                });

            modelBuilder.Entity("PQ.Core.Domain.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PQ.Core.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("ResetToken")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("CampaignHelpItem", b =>
                {
                    b.HasOne("PQ.Core.Domain.HelpItem", null)
                        .WithMany()
                        .HasForeignKey("HelpItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PQ.Core.Domain.Campaign", null)
                        .WithMany()
                        .HasForeignKey("CampaignsPhilanthropicEntityId", "CampaignsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PQ.Core.Domain.Address", b =>
                {
                    b.HasOne("PQ.Core.Domain.PhilanthropicEntity", "PhilanthropicEntity")
                        .WithOne("Address")
                        .HasForeignKey("PQ.Core.Domain.Address", "PhilanthropicEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhilanthropicEntity");
                });

            modelBuilder.Entity("PQ.Core.Domain.Campaign", b =>
                {
                    b.HasOne("PQ.Core.Domain.PhilanthropicEntity", "PhilanthropicEntity")
                        .WithMany("Campaigns")
                        .HasForeignKey("PhilanthropicEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhilanthropicEntity");
                });

            modelBuilder.Entity("PQ.Core.Domain.Document", b =>
                {
                    b.HasOne("PQ.Core.Domain.PhilanthropicEntity", "PhilanthropicEntity")
                        .WithOne("Documents")
                        .HasForeignKey("PQ.Core.Domain.Document", "PhilanthropicEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhilanthropicEntity");
                });

            modelBuilder.Entity("PQ.Core.Domain.PhilanthropicEntity", b =>
                {
                    b.HasOne("PQ.Core.Domain.User", "User")
                        .WithOne("PhilanthropicEntity")
                        .HasForeignKey("PQ.Core.Domain.PhilanthropicEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("PQ.Core.Domain.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PQ.Core.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PQ.Core.Domain.PhilanthropicEntity", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Campaigns");

                    b.Navigation("Documents");
                });

            modelBuilder.Entity("PQ.Core.Domain.User", b =>
                {
                    b.Navigation("PhilanthropicEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
