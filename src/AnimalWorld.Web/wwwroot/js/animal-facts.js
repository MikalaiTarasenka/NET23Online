$(document).ready(function () {
    const MAX_LENGTH = 1000;
    
    // Получаем URL из data-атрибута
    const $formCard = $('.facts-form-card');
    const factsApiUrl = $formCard.data('api-url');
    const getUrl = factsApiUrl + 'GetFacts';
    const postUrl = factsApiUrl + 'AddFact';

    const $select = $('#animalSpeciesSelect');
    const $textarea = $('#factText');
    const $btn = $('#submitFactBtn');
    const $charCount = $('#charCount');
    const $addError = $('#addError');
    const $container = $('#factsContainer');

    // Проверка валидности для активации кнопки
    function updateButtonState() {
        const isTextValid = $textarea.val().trim().length > 0;
        const isSelectValid = $select.val() !== "";
        
        if (isTextValid && isSelectValid) {
            $btn.prop('disabled', false);
        } else {
            $btn.prop('disabled', true);
        }
    }

    // Обработка ввода текста
    $textarea.on('input', function () {
        const remaining = MAX_LENGTH - $textarea.val().length;
        $charCount.text(remaining >= 0 ? remaining : 0);
        updateButtonState();
        $addError.hide();
    });

    // Обработка выбора в dropdown
    $select.on('change', function () {
        updateButtonState();
        $addError.hide();
    });

    // Рендер пустого состояния
    function renderEmptyState() {
        $container.html('<div class="facts-empty">Фактов пока нет</div>');
    }

    // Рендер ошибки API
    function renderErrorState() {
        $container.html('<div class="facts-api-error">Технические проблемы при загрузке фактов. Попробуйте позже.</div>');
    }

    // Рендер загрузки
    function renderLoadingState() {
        $container.html('<div class="facts-loading">Загрузка фактов...</div>');
    }

    // Загрузка фактов при старте
    function loadFacts() {
        renderLoadingState();
        
        $.get(getUrl)
            .done(function (data) {
                if (!data || data.length === 0) {
                    renderEmptyState();
                } else {
                    $container.empty();
                    // Переворачиваем массив, чтобы самые новые (последние в ответе API) оказались сверху
                    const reversedData = data.slice().reverse();
                    reversedData.forEach(function (fact) {
                        appendFactToContainer(fact.animalSpeciesName, fact.text, false);
                    });
                }
            })
            .fail(function () {
                renderErrorState();
            });
    }

    // Добавление факта в DOM
    function appendFactToContainer(speciesName, text, animate) {
        // Если сейчас показано пустое состояние, ошибка или загрузка — очищаем контейнер
        if ($container.find('.facts-empty, .facts-api-error, .facts-loading').length > 0) {
            $container.empty();
        }

        const factHtml = `
            <div class="fact-card">
                <span class="fact-species"></span>
                <p class="fact-text"></p>
            </div>
        `;
        
        const $fact = $(factHtml);
        // Используем .text() для защиты от XSS
        $fact.find('.fact-species').text(speciesName);
        $fact.find('.fact-text').text(text);

        // Добавляем в начало списка (новые сверху)
        $container.prepend($fact);
    }

    // Отправка нового факта
    $btn.on('click', function () {
        const speciesName = $select.val();
        const text = $textarea.val().trim();

        if (!speciesName || !text) return;

        // Блокируем кнопку на время запроса
        $btn.prop('disabled', true).text('Добавление...');
        $addError.hide();

        const payload = {
            animalSpeciesName: speciesName,
            text: text
        };

        $.ajax({
            url: postUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(payload)
        })
        .done(function (response) {
            // Очищаем поле ввода и сбрасываем счетчик
            $textarea.val('');
            $charCount.text(MAX_LENGTH);
            
            // Берем данные для отображения из ответа сервера (или из формы, если сервер не возвращает объект)
            const displaySpecies = response.animalSpeciesName || speciesName;
            const displayText = response.text || text;

            // Добавляем новый факт в начало списка с анимацией
            appendFactToContainer(displaySpecies, displayText, true);
        })
        .fail(function () {
            $addError.show();
        })
        .always(function () {
            // Восстанавливаем состояние кнопки (она снова станет disabled, т.к. textarea теперь пустая)
            updateButtonState();
            $btn.text('Добавить факт');
        });
    });

    // Инициализация при загрузке страницы
    updateButtonState();
    loadFacts();
});