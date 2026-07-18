$(document).ready(function () {
    const $select = $('#animal-type-select');
    const $text = $('#factText');
    const $container = $('#facts-container');
    const $feedback = $('#fact-form-feedback');

    const messages = {
        errorEmpty: $feedback.data('msg-error-empty'),
        success: $feedback.data('msg-success'),
        errorSubmit: $feedback.data('msg-error-submit'),
        emptyState: $container.data('empty-text'),
        errorLoad: $container.data('error-text')
    };

    const factsApiUrl = $('.fact-form-card').data('url');

    $.getJSON('/api/AnimalWorld/GetAnimalSpeciesNames')
        .done(function (list) {
            $select.html(list.map(function (name) {
                return new Option(name, name);
            }));
            loadFacts();
        });

    $('#addFactBtn').on('click', function () {
        const animal = $select.val();
        const val = $text.val().trim();

        if (!val) {
            showFeedback('error', messages.errorEmpty);
            return;
        }

        $.ajax({
            url: `${factsApiUrl}/AddFact`,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                animalSpeciesName: animal,
                text: val
            }),
            success: function () {
                $text.val('');
                showFeedback('success', messages.success);

                const $newFact = $(`
                    <article class="fact-item fact-item--new">
                        <span class="fact-animal-type">${animal}</span>
                        <p class="fact-text">${val}</p>
                    </article>
                `);

                $container.prepend($newFact);

                setTimeout(() => {
                    $newFact.removeClass('fact-item--new');
                }, 500);

                setTimeout(() => {
                    $feedback.text('').hide();
                }, 3000);
            },
            error: function () {
                showFeedback('error', messages.errorSubmit);
            }
        });
    });

    function loadFacts() {
        $.getJSON(`${factsApiUrl}/GetFacts`)
            .done(function (facts) {
                $('#facts-loading-status').remove();

                if (facts.length === 0) {
                    $container.html(`
                        <div class="empty-container">
                            <div class="empty-icon">💭</div>
                            <p class="empty-text">${messages.emptyState}</p>
                        </div>
                    `);
                    return;
                }

                $container.find('.fact-item').remove();

                facts.forEach(function (fact) {
                    $(`<article class="fact-item">
                        <span class="fact-animal-type">${fact.animalSpeciesName}</span>
                        <p class="fact-text">${fact.text}</p>
                    </article>`).appendTo($container);
                });
            })
            .fail(function () {
                $('#facts-loading-status').remove();
                $container.html(`
                    <div class="empty-container">
                        <div class="empty-icon">⚠️</div>
                        <p class="empty-text">${messages.errorLoad}</p>
                    </div>
                `);
            });
    }

    function showFeedback(type, message) {
        $feedback
            .removeClass('fact-feedback--success fact-feedback--error')
            .addClass(`fact-feedback--${type}`)
            .text(message)
            .show();
    }
});