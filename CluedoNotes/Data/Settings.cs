﻿using SQLite;
using System.ComponentModel.DataAnnotations;

namespace CluedoNotes.Data
{
    public enum AppTheme
    {
        Dark = 0,
        Light = 1,
    }
    public enum GameVersion
    {
        Classic = 0,
        EnglishHerritige = 1,
        Simpsons = 2
    }

    
    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [EnumDataType(typeof(TickColour))]
        public TickColour CardShownColour { get; set; }

        [EnumDataType(typeof(TickColour))]
        public TickColour ConfirmedCardSeenColour { get; set; }
        [EnumDataType(typeof(TickStyle))]
        public TickStyle ConfirmedCardSeenStyle { get; set; }

        [EnumDataType(typeof(TickColour))]
        public TickColour ConfirmedNoCardColour { get; set; }
        [EnumDataType(typeof(TickStyle))]
        public TickStyle ConfirmedNoCardStyle { get; set; }

        [EnumDataType(typeof(TickColour))]
        public TickColour MyCardColour { get; set; }
        [EnumDataType(typeof(TickStyle))]
        public TickStyle MyCardStyle { get; set; }

        public AppTheme Theme { get; set; }

        public GameVersion GameVersion { get; set; }
    }
}
