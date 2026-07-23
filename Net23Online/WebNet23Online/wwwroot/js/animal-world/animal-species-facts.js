$(document).ready(function () {
    const $select = $('#animal-type-select');
    const $text = $('#factText');
    const $container = $('#facts-container');
    const $feedback = $('#fact-form-feedback');
    const $addBtn = $('#addFactBtn');

    const messages = {
        errorEmpty: $feedback.data('msg-error-empty'),
        success: $feedback.data('msg-success'),
        errorSubmit: $feedback.data('msg-error-submit'),
        emptyState: $container.data('empty-text'),
        errorLoad: $container.data('error-text')
    };

    const factsApiUrl = $('.fact-form-card').data('url');

    // Загрузка видов животных
    $.getJSON('/api/AnimalWorld/GetAnimalSpeciesNames')
        .done(function (list) {
            $select.empty();
            list.forEach(function (name) {
                $select.append(new Option(name, name));
            });
            loadFacts();
        })
        .fail(function () {
            $select.empty().append(new Option('—', ''));
            $('#facts-loading-status').remove();
            renderState(messages.errorLoad, '⚠️');
        });

    // Добавление факта
    $addBtn.on('click', function () {
        const animal = $select.val();
        const val = $text.val().trim();

        if (!val) {
            showFeedback('error', messages.errorEmpty);
            return;
        }

        if (!animal) {
            showFeedback('error', messages.errorSubmit);
            return;
        }

        $addBtn.prop('disabled', true);

        $.ajax({
            url: `${factsApiUrl}/AddFact`,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                animalSpeciesName: animal,
                text: val
            }),
            success: function (createdFact) {
                $text.val('');
                showFeedback('success', messages.success);

                // Если список был пуст — убираем заглушку
                $container.find('.empty-container').remove();

                // Если индикатор загрузки ещё висит — убираем
                $('#facts-loading-status').remove();

                // Используем данные от сервера, если пришли, иначе локальные
                const factData = createdFact && createdFact.text
                    ? createdFact
                    : { animalSpeciesName: animal, text: val };

                // Безопасное создание DOM (защита от XSS)
                const $newFact = $('<article>', { class: 'fact-item fact-item--new' });
                $('<span>', { class: 'fact-animal-type', text: factData.animalSpeciesName }).appendTo($newFact);
                $('<p>', { class: 'fact-text', text: factData.text }).appendTo($newFact);

                $container.prepend($newFact);

                setTimeout(() => $newFact.removeClass('fact-item--new'), 500);
                setTimeout(() => $feedback.text('').hide(), 3000);
            },
            error: function () {
                showFeedback('error', messages.errorSubmit);
            },
            complete: function () {
                $addBtn.prop('disabled', false);
            }
        });
    });

    // Загрузка списка фактов
    function loadFacts() {
        $.getJSON(`${factsApiUrl}/GetFacts`)
            .done(function (facts) {
                $('#facts-loading-status').remove();

                if (!facts || facts.length === 0) {
                    renderState(messages.emptyState, '💭');
                    return;
                }

                // Очищаем только элементы фактов, не трогая индикатор загрузки
                $container.find('.fact-item, .empty-container').remove();

                facts.forEach(function (fact) {
                    const $fact = $('<article>', { class: 'fact-item' });
                    $('<span>', { class: 'fact-animal-type', text: fact.animalSpeciesName }).appendTo($fact);
                    $('<p>', { class: 'fact-text', text: fact.text }).appendTo($fact);
                    $container.append($fact);
                });
            })
            .fail(function () {
                $('#facts-loading-status').remove();
                renderState(messages.errorLoad, '⚠️');
            });
    }

    // Рендер состояния "пусто" или "ошибка"
    function renderState(text, icon) {
        const $state = $('<div>', { class: 'empty-container' });
        $('<div>', { class: 'empty-icon', text: icon }).appendTo($state);
        $('<p>', { class: 'empty-text', text: text }).appendTo($state);
        $container.html($state);
    }

    // Показ сообщения-фидбека
    function showFeedback(type, message) {
        $feedback
            .removeClass('fact-feedback--success fact-feedback--error')
            .addClass(`fact-feedback--${type}`)
            .text(message)
            .show();
    }
});