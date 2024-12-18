using TaskManager.Attributes;

namespace TaskManager.ViewModel
{
    public class EditTaskFormViewModel
    {
        [Required(ErrorMessage = "معرف المهمة مطلوب.")]
        [Display(Name = "معرف المهمة")]
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان المهمة مطلوب.")]
        [StringLength(200, ErrorMessage = "العنوان لا يمكن أن يتجاوز 200 حرف.")]
        [Display(Name = "عنوان المهمة")]
        public string? Title { get; set; }

        [StringLength(500, ErrorMessage = "الوصف لا يمكن أن يتجاوز 500 حرف.")]
        [Display(Name = "وصف المهمة")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "يرجى تحديد تاريخ التسليم.")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "يجب أن يكون تاريخ التسليم في المستقبل.")]
        [Display(Name = "تاريخ التسليم")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "يرجى تحديد تاريخ التسليم.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "وقت التسليم")]
        public TimeOnly timeOnly { get; set; }

        [Required(ErrorMessage = "يرجى اختيار الفئة.")]
        [Display(Name = "الفئة المرتبطة")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "يرجى تحديد وقت التسليم.")]
        [DataType(DataType.DateTime)]
        public TimeSpan DueTime { get; set; } // إضافي لتحديد الوقت

    }
}
