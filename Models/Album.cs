﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MusicAppWPF.Models;


public class Album
{
    [Key]
    public Guid AlbumId { get; set; }
    public string Title { get; set; }
    [ForeignKey("ArtistId")]
    public Artist Artist { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string CoverImageUrl { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}