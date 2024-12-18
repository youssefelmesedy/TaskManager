namespace TaskManager.ViewModel
{
    public class AddCategoryFormViewModel
    {
        [Required(ErrorMessage = "اسم الفئة مطلوب.")]
        [StringLength(100, ErrorMessage = "اسم الفئة لا يمكن أن يتجاوز 100 حرف.")]
        [Display(Name = "اسم الفئة")]
        public string? Name { get; set; }
    }
}
