namespace Firotechbd.Areas.Admin.Models
{
    public class BlogPost
    {
        public int Id { get; set; }               // Unique identifier for each blog post
        public string Title { get; set; }          // Title of the blog post
        public string Content { get; set; }        // Main content of the blog
        public string Slug { get; set; }           // URL-friendly version of the title
        public string Summary { get; set; }        // Short summary of the blog post
        public DateTime PublishedAt { get; set; }  // Date the post was published
        public DateTime CreatedAt { get; set; }    // Date the post was created
        public DateTime UpdatedAt { get; set; }    // Date the post was last updated

        // Relationships
        public int AuthorId { get; set; }          // Foreign key to Author
        public BlogAuthor Author { get; set; }         // Navigation property for the author
        //public ICollection<Comment> Comments { get; set; }  // List of comments
        //public ICollection<Category> Categories { get; set; } // Blog categories

        public bool IsPublished { get; set; }      // Whether the post is published or in draft
    }
    public class BlogAuthor
    {
        public int Id { get; set; }               // Unique identifier for each author
        public string Name { get; set; }          // Author's name
        public string Bio { get; set; }           // Short biography of the author
        public string ProfileImageUrl { get; set; }  // Link to the author's profile image

    }
    public class Comment
    {
        public int Id { get; set; }                // Unique identifier for each comment
        public string Content { get; set; }        // Comment content
        public DateTime CreatedAt { get; set; }    // Date the comment was made

        // Relationships
        public int BlogPostId { get; set; }        // Foreign key to the BlogPost
        public BlogPost BlogPost { get; set; }     // Navigation property to the BlogPost
        public string CommenterName { get; set; }  // Name of the person who commented
        public string CommenterEmail { get; set; } // Email of the person who commented (optional)
    }
    public class Category
    {
        public int Id { get; set; }               // Unique identifier for each category
        public string Name { get; set; }          // Name of the category
        public string Description { get; set; }   // Optional description of the category

    }
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

}
