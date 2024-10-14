using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models
{
    public class Comment : Entity
    {

        public string Content { get;  set; }
        
        public Author Author { get;  set; }
        
        public Post Post { get; private set; }

        public Comment()
        {
                
        }


        public Comment(string content, Author author, Post post)
        {
            Content = content;
            Author = author;
            Post = post;
        }
    }
}

