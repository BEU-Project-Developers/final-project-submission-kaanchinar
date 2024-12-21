﻿using System.Configuration;
using Microsoft.EntityFrameworkCore;
using MusicAppWPF.Models;

namespace MusicAppWPF.Data;

public class MusicAppDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistSong> PlaylistSongs { get; set; }
    public DbSet<UserSongActivity> UserSongActivities { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["MusicApp"].ConnectionString;
        optionsBuilder.UseNpgsql(connectionString);
    }
}