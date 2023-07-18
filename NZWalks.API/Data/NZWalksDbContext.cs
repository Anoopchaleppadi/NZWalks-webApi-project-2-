﻿using Microsoft.EntityFrameworkCore; //for inherit
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext:  DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }


        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> walks { get; set; }
    }
}
