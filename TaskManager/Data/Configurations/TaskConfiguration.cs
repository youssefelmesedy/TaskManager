namespace TaskManager
{
    public class TaskConfiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.HasKey(t => t.Id); // المفتاح الأساسي
            builder.Property(t => t.Title)
                  .IsRequired() // الحقل مطلوب
                  .HasMaxLength(200); // الحد الأقصى للطول

            builder.Property(t => t.Description)
                  .HasMaxLength(500); // الحد الأقصى للوصف

            builder.Property(t => t.DueDate)
                  .IsRequired(); // تاريخ التسليم مطلوب

            // إعداد العلاقة بين Task و Category
            builder.HasOne(t => t.Category)
                  .WithMany(c => c.Tasks)
                  .HasForeignKey(t => t.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade); // حذف المهام المرتبطة عند حذف الفئة
        }
    }
    public class CategoreyConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id); // المفتاح الأساسي

            builder.Property(c => c.Name)
                  .IsRequired() // الحقل مطلوب
                  .HasMaxLength(100); // الحد الأقصى للطول
        }
    }
}

