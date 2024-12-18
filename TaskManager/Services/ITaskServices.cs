namespace TaskManager.Services
{
    public interface ITaskServices
    {
        /// <summary>
        /// إضافة مهمة جديدة.
        /// </summary>
        /// <param name="model">نموذج بيانات المهمة.</param>
        /// <param name="duedate">تاريخ التسليم.</param>
        /// <returns>True إذا نجحت العملية، False إذا فشلت.</returns>
        Task<bool> AddTaskAsync(AddTaskFormViewModel model, DateTime duedate);

        /// <summary>
        /// تعديل مهمة قائمة.
        /// </summary>
        /// <param name="model">نموذج تعديل المهمة.</param>
        /// <returns>True إذا نجحت العملية، False إذا فشلت.</returns>
        Task<bool> EditTaskAsync(EditTaskFormViewModel model , DateTime duedate);

        /// <summary>
        /// استرجاع قائمة المهام.
        /// </summary>
        /// <returns>قائمة المهام في شكل ViewModel.</returns>
        Task<List<ListTasksViewModel>> GetAllTasksAsync();

        /// <summary>
        /// استرجاع مهمة لتعديلها.
        /// </summary>
        /// <param name="id">معرف المهمة.</param>
        /// <returns>نموذج تعديل المهمة.</returns>
        Task<EditTaskFormViewModel> GetTaskByIdAsync(int id);

        /// <summary>
        /// حذف مهمة.
        /// </summary>
        /// <param name="id">معرف المهمة.</param>
        /// <returns>True إذا نجحت العملية، False إذا فشلت.</returns>
        Task<bool> DeleteTaskAsync(int id);

        /// <summary>
        /// تبديل حالة المهمة بين مكتملة وغير مكتملة.
        /// </summary>
        /// <param name="taskId">معرف المهمة.</param>
        /// <param name="status">الحالة الجديدة.</param>
        /// <returns>True إذا نجحت العملية، False إذا فشلت.</returns>
        Task<bool> ToggleTaskStatusAsync(int taskId, bool status);

        /// <summary>
        /// حساب الأيام المتبقية على تسليم المهمة.
        /// </summary>
        /// <param name="dueDate">تاريخ التسليم.</param>
        /// <returns>عدد الأيام المتبقية.</returns>
        int GetRemainingDays(DateTime dueDate);
    }
}
