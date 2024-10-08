using Firotechbd.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Firotechbd.Areas.Admin.ViewModels
{
    public class BlogPostVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        [DataType(DataType.Date)]
        public DateTime PostedDate { get; set; }
        public string Tags { get; set; }
    }
}
