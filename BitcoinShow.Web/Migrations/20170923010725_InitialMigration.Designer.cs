﻿// <auto-generated />
using BitcoinShow.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BitcoinShow.Web.Migrations
{
    [DbContext(typeof(BitcoinShowDBContext))]
    [Migration("20170923010725_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ChangeDetector.SkipDetectChanges", "true")
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");
#pragma warning restore 612, 618
        }
    }
}
