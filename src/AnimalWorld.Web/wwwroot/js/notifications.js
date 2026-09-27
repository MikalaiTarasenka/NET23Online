(function () {
    const MAX_NOTIFICATIONS = 3;
    const AUTO_CLOSE_DELAY = 5000;
    const EXIT_ANIMATION_DURATION = 250;

    function getContainer() {
        return document.querySelector('.notifications-container');
    }

    function removeNotification(element) {
        if (!element || element.dataset.removing === 'true') return;
        element.dataset.removing = 'true';
        element.classList.add('notification-exit');
        setTimeout(function () {
            if (element.parentNode) {
                element.parentNode.removeChild(element);
            }
        }, EXIT_ANIMATION_DURATION);
    }

    window.showNotification = function (title, message) {
        const container = getContainer();
        if (!container) return;

        const notification = document.createElement('div');
        notification.className = 'notification';

        const titleEl = document.createElement('div');
        titleEl.className = 'notification-title';
        titleEl.textContent = title;

        const messageEl = document.createElement('div');
        messageEl.className = 'notification-message';
        messageEl.textContent = message;

        notification.appendChild(titleEl);
        notification.appendChild(messageEl);

        notification.addEventListener('click', function () {
            removeNotification(notification);
        });

        container.appendChild(notification);

        // Если уведомлений больше максимума — убираем самое старое
        const all = container.querySelectorAll('.notification:not(.notification-exit)');
        if (all.length > MAX_NOTIFICATIONS) {
            removeNotification(all[0]);
        }

        // Автозакрытие через 5 секунд
        setTimeout(function () {
            removeNotification(notification);
        }, AUTO_CLOSE_DELAY);
    };
})();