$(document).ready(function () {
    const $select = $('#animal-type-select');
    const $text = $('#factText');
    const $container = $('#facts-container');
    const $feedback = $('#fact-form-feedback');

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
            showFeedback('error', 'Введите текст факта');
            return;
        }

        $.ajax({
            url: 'https://localhost:7264/AddFact',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                animalSpeciesName: animal,
                text: val
            }),
            success: function () {
                $text.val('');
                showFeedback('success', 'Факт успешно добавлен!');

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
                showFeedback('error', 'Ошибка при добавлении факта');
            }
        });
    });

    function loadFacts() {
        $.getJSON('https://localhost:7264/GetFacts')
            .done(function (facts) {
                $('#facts-loading-status').remove();

                if (facts.length === 0) {
                    $container.html(`
                        <div class="facts-empty">
                            <span class="facts-empty-icon">💭</span>
                            <p class="facts-empty-text">Пока нет ни одного факта. Будьте первым, кто поделится интересной информацией!</p>
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
                $('#facts-loading-status').text('Ошибка загрузки фактов').addClass('facts-empty');
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