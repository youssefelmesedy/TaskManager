function setDeleteModalData(taskId, taskTitle) {
    const form = document.getElementById("deleteForm");
    const modalTitle = document.getElementById("modalTaskTitle");

    // التأكد من أن الفورم موجود
    if (form) {
        form.action = `/Tasks/Delete/${taskId}`; // تحديث الرابط إلى رابط الحذف المناسب
    }

    // التأكد من أن عنوان المودال موجود
    if (modalTitle) {
        modalTitle.textContent = `هل تريد حذف المهمة "${taskTitle}"؟`; // تحديث عنوان المودال
    }
}
console.log("Form:");
console.log("Modal Title:", modalTitle);
console.log(`Task ID: ${taskId}, Task Title: ${taskTitle}`);
