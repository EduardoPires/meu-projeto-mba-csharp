using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Data.Configurations
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Created)
                .IsRequired();

            builder.Property(x => x.Updated)
                .IsRequired();
            
            builder.Property(x => x.Content)
                .HasMaxLength(500)
                .IsRequired();


            builder.HasOne(x => x.Post)
                .WithMany(x => x.Comments)
                .OnDelete(DeleteBehavior.Restrict); // Um Post pode ter muitos comentários

            
        }
    }
}
