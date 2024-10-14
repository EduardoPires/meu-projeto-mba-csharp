using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models
{

    public class Author : Entity
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        // Relação com os posts (um autor pode ter vários posts)
        public List<Post> Posts { get; private set; }

        public List<Comment> Comments { get; private set; }

        public Author()
        {

        }


        public Author(string name, string email)
        {
            Name = name;
            Email = email;
            Posts = new List<Post>();
            Comments = new List<Comment>();
        }

        public void UpdateAuthor(string name, string email)
        {
            Name = name;
            Email = email;
        }

    }

}
