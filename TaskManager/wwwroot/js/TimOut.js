setTimeout(() => {
    const notification = document.getElementById('notification');
    if (notification) {
        notification.style.transition = 'opacity 0.5s';
        notification.style.opacity = '0';
        setTimeout(() => notification.remove(), 500); // احذف العنصر بعد انتهاء التحول
    }
}, 5000);
