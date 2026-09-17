$(document).ready(function () {
    $('.ticket-qr-container').each(function () {
        var $container = $(this);
        var qrValue = $container.data('qr-value');

        if (qrValue && typeof QRCode !== 'undefined') {
            new QRCode(this, {
                text: qrValue,
                width: 150,
                height: 150,
                colorDark: '#2c3333',
                colorLight: '#ffffff',
                correctLevel: QRCode.CorrectLevel.M
            });
        }
    });
});