namespace TaskManager.Controllers
{

    public class CategoriesController : Controller
    {
        private readonly ICategoriesServices _categoriesServices;

        public CategoriesController(ICategoriesServices categoriesServices)
        {
            _categoriesServices = categoriesServices;
        }

        // عرض قائمة الفئات
        public async Task<IActionResult> Index()
        {
            var categories = await _categoriesServices.GetAllCategoriesAsync();
            return View(categories);
        }

        // عرض نموذج إضافة فئة جديدة
        public IActionResult Create()
        {
            return View(new AddCategoryFormViewModel());
        }

        // إضافة فئة جديدة
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddCategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "الرجاء تصحيح الأخطاء في النموذج.";
                return View(model);
            }

            var success = await _categoriesServices.AddCategoryAsync(model);
            if (success)
            {
                TempData["SuccessMessage"] = "تمت إضافة الفئة بنجاح.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "حدث خطأ أثناء إضافة الفئة.";
                return View(model);
            }
        }

        // عرض نموذج تعديل الفئة
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoriesServices.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // تعديل الفئة
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _categoriesServices.EditCategoryAsync(model);
            if (!success)
            {
                TempData["ErrorMessage"] = "حدث خطأ أثناء تعديل الفئه.";
                return View(model);
            }

            TempData["SuccessMessage"] = "تم تعديل الفئه بنجاح.";
            return RedirectToAction(nameof(Index));
        }

        // حذف فئة
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoriesServices.DeleteCategoryAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "تم حذف المهمة بنجاح.";
            }
            else
            {
                TempData["ErrorMessage"] = "حدث خطأ أثناء الحذف.";
            }

            return RedirectToAction(nameof(Index));
        }
    }

}

