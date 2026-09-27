(function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl('/my-hub/promotions')
        .configureLogging(signalR.LogLevel.Warning)
        .build();

    connection.on('ZooPromotion', function (message) {
        if (typeof showNotification === 'function') {
            showNotification('Акция', message);
        }
    });

    connection.start().catch(function () {
        // ошибки соединения игнорируем
    });
})();