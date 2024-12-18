function setDeleteModalDataCategorey(categoryId, categoryName) {
    document.getElementById('deleteModalTitle').textContent = `حذف التصنيف: ${categoryName}`;
    const deleteForm = document.getElementById('deleteForm');
    deleteForm.action = `/Categories/Delete/${categoryId}`; // تحقق من صحة المسار
}

console.log("Form:");
console.log("Modal Title:", modalTitle);
console.log(`Task ID: ${categoryId}, Task Title: ${categoryName}`);