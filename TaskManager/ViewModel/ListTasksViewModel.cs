using TaskManager.Attributes;

namespace TaskManager.ViewModel
{
    public class ListTasksViewModel
    {
        [Display(Name = "معرف المهمة")]
        public int Id { get; set; }

        [Display(Name = "عنوان المهمة")]
        public string? Title { get; set; }

        [Display(Name = "الفئة المرتبطة")]
        public string? CategoryName { get; set; } = null;
        [Display(Name = "الوصف")]
        public string? Description { get; set; }

        [Display(Name = "تاريخ التسليم")]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "يرجى تحديد تاريخ التسليم.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "وقت التسليم")]
        public TimeOnly timeOnly { get; set; }

        [Display(Name = "هل اكتملت المهمة؟")]
        public bool IsCompleted { get; set; }
        public TimeSpan DueTime { get; set; } // إضافي لتحديد الوقت
        public int RemainingDays { get; set; } // لعرض الأيام المتبقية
    }
}
