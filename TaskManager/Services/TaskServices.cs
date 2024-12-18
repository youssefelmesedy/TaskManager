namespace TaskManager.Services
{
    public class TaskServices : ITaskServices
    {
        private readonly  ApplicationDbContext _context;

        public TaskServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddTaskAsync(AddTaskFormViewModel model, DateTime dueDate)
        {
            try
            {
                var task = new Tasks
                {
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = model.DueDate, // دمج التاريخ مع الوقت
                    TimeOnly = model.timeOnly, // دمج التاريخ مع الوقت
                    CategoryId = model.CategoryId,
                    RemainingDays = GetRemainingDays(dueDate) // حساب الأيام المتبقية عند الإضافة
                };

                _context.tasks.Add(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle exception (e.g., log it)
                return false;
            }
        }


        public async Task<bool> EditTaskAsync(EditTaskFormViewModel model, DateTime dueDate)
        {
            try
            {
                var task = await _context.tasks.FindAsync(model.Id);
                if (task == null)
                    return false;

                // تحديث القيم من النموذج
                task.Title = model.Title;
                task.Description = model.Description;
                task.DueDate = model.DueDate; // استخدام dueDate المدموج
                task.TimeOnly = model.timeOnly; 
                task.CategoryId = model.CategoryId;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // يمكن تسجيل الخطأ هنا إذا لزم الأمر
                return false;
            }
        }


        public async Task<List<ListTasksViewModel>> GetAllTasksAsync()
        {
            return await _context.tasks
                .Select(t => new ListTasksViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    CategoryName = t.Category.Name,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    timeOnly = t.TimeOnly,
                    RemainingDays = (t.DueDate.Date - DateTime.Now.Date).Days,
                    IsCompleted = t.IsCompleted
                })
                .ToListAsync() ?? new List<ListTasksViewModel>(); // إعادة قائمة فارغة بدلاً من null
        }

        public async Task<EditTaskFormViewModel> GetTaskByIdAsync(int id)
        {
            try
            {
                var task = await _context.tasks
                    .Where(t => t.Id == id)
                    .Select(t => new EditTaskFormViewModel
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        DueDate = t.DueDate,
                        timeOnly = t.TimeOnly,
                        CategoryId = t.CategoryId
                    })
                    .FirstOrDefaultAsync();

                if (task == null)
                {
                    // هنا يمكننا التعامل مع حالة المهمة غير موجودة، مثل إرجاع null أو رسالة خطأ.
                    return null!;
                }

                return task!;
            }
            catch (Exception ex)
            {
                // يمكنك تسجيل الاستثناء أو التعامل معه بما يناسب تطبيقك.
                // مثلا: logging the exception
                // _logger.LogError(ex, "حدث خطأ أثناء جلب المهمة");

                // يمكن إعادة إلقاء الاستثناء أو إرجاع null حسب الحاجة
                throw new InvalidOperationException("حدث خطأ أثناء جلب المهمة من قاعدة البيانات", ex);
            }
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            try
            {
                var task = await _context.tasks.FindAsync(id);
                if (task == null)
                    return false;

                _context.tasks.Remove(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle the error
                return false;
            }
        }

        public async Task<bool> ToggleTaskStatusAsync(int taskId, bool status)
        {
            var task = await _context.tasks.FindAsync(taskId);
            if (task == null)
            {
                return false; // المهمة غير موجودة
            }

            task.IsCompleted = status;
            _context.tasks.Update(task);
            await _context.SaveChangesAsync();

            return true;
        }
        public int GetRemainingDays(DateTime dueDate)
        {
            var today = DateTime.Today;
            var remainingDays = (dueDate - today).Days;
            return remainingDays;
        }

    }
}
