using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.entities;

namespace Application.helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options): base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Join>().HasKey(j => new { j.UserId, j.ConservationId });
        }
        
        public DbSet<User> Users {get;set;}
        public DbSet<Join> Joins {get;set;}
        public DbSet<Conversation> Conversations {get;set;}
        public DbSet<Message> Messages {get;set;}


    }
}