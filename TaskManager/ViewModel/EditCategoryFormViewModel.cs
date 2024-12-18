namespace TaskManager.ViewModel
{
    public class EditCategoryFormViewModel
    {
        [Required(ErrorMessage = "معرف الفئة مطلوب.")]
        [Display(Name = "معرف الفئة")]
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الفئة مطلوب.")]
        [StringLength(100, ErrorMessage = "اسم الفئة لا يمكن أن يتجاوز 100 حرف.")]
        [Display(Name = "اسم الفئة")]
        public string? Name { get; set; }
    }
}
