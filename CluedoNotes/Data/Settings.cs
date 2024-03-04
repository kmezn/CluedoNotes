using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations.Schema;
using ForeignKeyAttribute = SQLiteNetExtensions.Attributes.ForeignKeyAttribute;

namespace CluedoNotes.Data
{
    public enum AppTheme
    {
        Dark = 0,
        Light = 1,
    }

    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [EnumDataType(typeof(TickColour))]
        public TickColour DefaultTickColour { get; set; }
        // tick colour used for confirmation tick or eventId display on game notes page.

        public AppTheme Theme { get; set; }
    }
}
