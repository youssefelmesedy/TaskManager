namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskServices _taskServices;
        private readonly ICategoriesServices _categoriesServices;

        public TasksController(ITaskServices taskServices, ICategoriesServices categoriesServices)
        {
            _taskServices = taskServices;
            _categoriesServices = categoriesServices;
        }

        // عرض قائمة المهام
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskServices.GetAllTasksAsync();
            return View(tasks);
        }

        // عرض نموذج إضافة مهمة جديدة
        public async Task<IActionResult> Create()
        {
            var categories = await _categoriesServices.GetAllCategoriesAsync();
            ViewBag.Categories = categories; // إرسال قائمة التصنيفات إلى الـ View
            return View(new AddTaskFormViewModel());
        }

        // إضافة مهمة جديدة
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddTaskFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoriesServices.GetAllCategoriesAsync();
                ViewBag.Categories = categories;
                TempData["ErrorMessage"] = "الرجاء تصحيح الأخطاء في النموذج.";
                return View(model);
            }

            // دمج التاريخ مع الوقت
            var dueDate = model.DueDate.Date.Add(model.DueTime);

            var success = await _taskServices.AddTaskAsync(model, dueDate);
            if (!success)
            {
                TempData["ErrorMessage"] = "حدث خطأ أثناء إضافة المهمة.";
                var categories = await _categoriesServices.GetAllCategoriesAsync();
                ViewBag.Categories = categories;
                return View(model);
            }

            TempData["SuccessMessage"] = "تم إضافة المهمة بنجاح.";
            return RedirectToAction(nameof(Index));
        }


        // عرض نموذج تعديل مهمة
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskServices.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            var categories = await _categoriesServices.GetAllCategoriesAsync();
            ViewBag.Categories = categories;
            return View(task);
        }

        // تعديل مهمة
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTaskFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoriesServices.GetAllCategoriesAsync();
                ViewBag.Categories = categories;
                TempData["ErrorMessage"] = "الرجاء تصحيح الأخطاء في النموذج.";
                return View(model);
            }

            // دمج التاريخ مع الوقت
            var dueDate = model.DueDate.Date.Add(model.DueTime);

            var success = await _taskServices.EditTaskAsync(model, dueDate);
            if (!success)
            {
                TempData["ErrorMessage"] = "حدث خطأ أثناء تعديل المهمة.";
                var categories = await _categoriesServices.GetAllCategoriesAsync();
                ViewBag.Categories = categories;
                return View(model);
            }

            TempData["SuccessMessage"] = "تم تعديل المهمة بنجاح.";
            return RedirectToAction(nameof(Index));
        }


        // حذف مهمة
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskServices.GetTaskByIdAsync(id);
            if (task == null)
            {
                TempData["ErrorMessage"] = "المهمة غير موجودة.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _taskServices.DeleteTaskAsync(id);
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

        // تغيير حالة المهمة (مكتملة / غير مكتملة)
        [HttpPost]
        public async Task<IActionResult> ToggleTaskStatus(int id, bool status)
        {
            try
            {
                var result = await _taskServices.ToggleTaskStatusAsync(id, status);
                if (result)
                {
                    TempData["SuccessMessage"] = $"تم تحديث حالة المهمة إلى {(status ? "مكتملة" : "غير مكتملة")} بنجاح.";
                }
                else
                {
                    TempData["ErrorMessage"] = "لم يتم العثور على المهمة أو حدث خطأ أثناء العملية.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "حدث خطأ أثناء تنفيذ العملية: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
