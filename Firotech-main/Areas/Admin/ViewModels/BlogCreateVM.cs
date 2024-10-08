using System.ComponentModel.DataAnnotations;

namespace Firotechbd.Areas.Admin.ViewModels
{
    public class BlogCreateVM
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string Thumbnail { get; set; }
    }
}
