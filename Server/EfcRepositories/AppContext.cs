﻿using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class AppContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
<<<<<<< HEAD
        optionsBuilder.UseSqlite(@"Data Source =  C:\Users\shankar\RiderProjects\DnpAssignment\DnpAssignment\Server\EfcRepositories\app.db");
=======
        optionsBuilder.UseSqlite("Data Source = C:\\Users\\Bibek\\OneDrive\\Desktop\\ThirdSemester\\DNP\\DNP_ASSIGNMENT_\\DnpAssignment\\Server\\EfcRepositories\\app.db");
>>>>>>> 9b41c88e1eac0d347d5f99f743020e678ac6355c
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)  // Assuming a Post has many Comments
            .HasForeignKey(c => c.PostId);  // Explicitly sets the FK
    }
    
}