namespace TaskManager.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly ApplicationDbContext _context;

        public CategoriesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCategoryAsync(AddCategoryFormViewModel model)
        {
            try
            {
                var category = new Category
                {
                    Name = model.Name
                };

                _context.categories.Add(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle exception (e.g., log it)
                return false;
            }
        }

        public async Task<bool> EditCategoryAsync(EditCategoryFormViewModel model)
        {
            try
            {
                var category = await _context.categories.FindAsync(model.Id);
                if (category == null)
                    return false;

                category.Name = model.Name;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle exception (e.g., log it)
                return false;
            }
        }

        public async Task<List<ListCategoriesViewModel>> GetAllCategoriesAsync()
        {
            return await _context.categories
                .Select(c => new ListCategoriesViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<EditCategoryFormViewModel> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _context.categories
                    .Where(t => t.Id == id)
                    .Select(t => new EditCategoryFormViewModel
                    {
                        Id = t.Id,
                        Name = t.Name,
                    })
                    .FirstOrDefaultAsync();

                if (category == null)
                {
                    // هنا يمكننا التعامل مع حالة المهمة غير موجودة، مثل إرجاع null أو رسالة خطأ.
                    return null!;
                }

                return category!;
            }
            catch (Exception ex)
            {
                // يمكنك تسجيل الاستثناء أو التعامل معه بما يناسب تطبيقك.
                // مثلا: logging the exception
                // _logger.LogError(ex, "حدث خطأ أثناء جلب المهمة");

                // يمكن إعادة إلقاء الاستثناء أو إرجاع null حسب الحاجة
                throw new InvalidOperationException("حدث خطأ أثناء جلب التصنيف من قاعدة البيانات", ex);
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _context.categories.FindAsync(id);
                if (category == null)
                    return false;

                _context.categories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle exception (e.g., log it)
                return false;
            }
        }
    }
}

