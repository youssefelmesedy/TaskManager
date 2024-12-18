namespace TaskManager.ViewModel
{
    public class ListCategoriesViewModel
    {
        [Display(Name = "معرف الفئة")]
        public int Id { get; set; }

        [Display(Name = "اسم الفئة")]
        public string? Name { get; set; }

        [Display(Name = "عدد المهام المرتبطة")]
        public int TasksCount { get; set; }
    }
}
