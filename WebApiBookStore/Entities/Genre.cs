using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiBookStore.Entities
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreID { get; set; }
        public string GenreName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
