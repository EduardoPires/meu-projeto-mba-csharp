using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models
{
    public class Post : Entity
    {
        public string Title { get;  set; }

        public string Content { get;  set; }

        public Author Author { get;  set; }

        public List<Comment> Comments { get; private set; }

        public Post()
        {
                
        }

        public Post(string title, string content, Author author)
        {
            Title = title;
            Content = content;
            Author = author;
            Comments = new List<Comment>();
        }

        internal void UpdatePost(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}

