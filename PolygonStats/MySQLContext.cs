﻿using System;
using Microsoft.EntityFrameworkCore;
using PolygonStats.Configuration;
using PolygonStats.Models;

namespace PolygonStats
{
    class MySQLContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<LogEntry> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbConnectionString = ConfigurationManager.shared.config.mysqlSettings.dbConnectionString;
            optionsBuilder.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Default values for LogEntry
            modelBuilder.Entity<LogEntry>()
                .Property(l => l.PokedexId)
                .HasDefaultValue(0);
            modelBuilder.Entity<LogEntry>()
                .Property(l => l.XpReward)
                .HasDefaultValue(0);
            modelBuilder.Entity<LogEntry>()
                .Property(l => l.StardustReward)
                .HasDefaultValue(0);
            modelBuilder.Entity<LogEntry>()
                .Property(l => l.Shiny)
                .HasDefaultValue(false);
        }
    }
}