namespace TaskManager.Services
{
    public interface ICategoriesServices
    {
        Task<bool> AddCategoryAsync(AddCategoryFormViewModel model);
        Task<bool> EditCategoryAsync(EditCategoryFormViewModel model);
        Task<List<ListCategoriesViewModel>> GetAllCategoriesAsync();
        Task<EditCategoryFormViewModel> GetCategoryByIdAsync(int id);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
