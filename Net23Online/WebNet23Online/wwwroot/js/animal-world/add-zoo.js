$(document).ready(function () {
    const $input = $('#ZooName');
    const $feedback = $('#zoo-name-feedback');
    const $submit = $('.js-add-zoo-form button[type="submit"]');
    let checkTimeout;

    $input.on('input', function () {
        const value = $.trim($input.val());

        clearTimeout(checkTimeout);

        if (value === '') {
            $feedback.removeClass().addClass('zoo-name-feedback');
            $feedback.text('');
            $submit.prop('disabled', false);
            return;
        }

        $submit.prop('disabled', true);
        $feedback.removeClass().addClass('zoo-name-feedback zoo-name-feedback--checking');
        $feedback.text('Проверяем доступность...');

        checkTimeout = setTimeout(function () {
            $.getJSON('/api/AnimalWorld/IsZooNameFree', {
                zooName: value
            })
                .done(function (isFree) {
                    if (isFree) {
                        $feedback.removeClass().addClass('zoo-name-feedback zoo-name-feedback--available');
                        $feedback.text('Имя свободно');
                        $submit.prop('disabled', false);
                    } else {
                        $feedback.removeClass().addClass('zoo-name-feedback zoo-name-feedback--taken');
                        $feedback.text('Это имя уже занято');
                        $submit.prop('disabled', true);
                    }
                })
                .fail(function () {
                    $feedback.removeClass().addClass('zoo-name-feedback zoo-name-feedback--taken');
                    $feedback.text('Ошибка проверки');
                    $submit.prop('disabled', true);
                });
        }, 500);
    });
});