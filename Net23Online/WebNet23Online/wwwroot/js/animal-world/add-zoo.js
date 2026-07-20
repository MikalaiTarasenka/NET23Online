$(document).ready(function () {
    const $input = $('#ZooName');
    const $feedback = $('#zoo-name-feedback');
    const $submit = $('.js-add-zoo-form button[type="submit"]');
    let checkTimeout;

    const messages = {
        checking: $feedback.data('msg-checking'),
        available: $feedback.data('msg-available'),
        taken: $feedback.data('msg-taken'),
        error: $feedback.data('msg-error')
    };

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
        $feedback.text(messages.checking);

        checkTimeout = setTimeout(function () {
            $.getJSON('/api/AnimalWorld/IsZooNameFree', {
                zooName: value
            })
                .done(function (isFree) {
                    if (isFree) {
                        $feedback.removeClass().addClass('zoo-name-feedback zoo-name-feedback--available');
                        $feedback.text(messages.available);
                        $submit.prop('disabled', false);
                    } else {
                        $feedback.removeClass().addClass('zoo-name-feedback zoo-name-feedback--taken');
                        $feedback.text(messages.taken);
                        $submit.prop('disabled', true);
                    }
                })
                .fail(function () {
                    $feedback.removeClass().addClass('zoo-name-feedback zoo-name-feedback--taken');
                    $feedback.text(messages.error);
                    $submit.prop('disabled', true);
                });
        }, 500);
    });
});