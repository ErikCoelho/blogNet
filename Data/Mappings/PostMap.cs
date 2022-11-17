using Blog.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BlogNet.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.ImageUrl)
                .HasColumnName("ImageUrl")
                .HasColumnType("VARCHAR")
                .HasMaxLength(2083);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);

            builder.Property(x => x.LastUpdateDate)
                .IsRequired()
                .HasColumnName("LastUpdateDate")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60)
                .HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder
                .HasIndex(x => x.Slug, "IX_Post_Slug")
                .IsUnique();

        }
    }
}
