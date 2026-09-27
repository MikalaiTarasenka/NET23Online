(function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl('/my-hub/zoos')
        .configureLogging(signalR.LogLevel.Warning)
        .build();

    connection.on('AnimalAppearedMessage', function (message) {
        if (typeof showNotification === 'function') {
            showNotification('Обновление в зоопарке', message);
        }
    });

    connection.start().catch(function () {
        // ошибки соединения игнорируем
    });
})();