$(document).ready(function () {
    const $deleteBtn = $('#delete-selected-btn');
    const $selectionActions = $('.selection-actions');

    function toggleDeleteButton() {
        const selectedCount = $('.js-family-card.selected').length;
        if (selectedCount > 0) {
            $selectionActions.fadeIn(200);
        } else {
            $selectionActions.fadeOut(200);
        }
    }

    $('.js-family-card').on('click', function () {
        $(this).toggleClass('selected');
        toggleDeleteButton();
    });

    $deleteBtn.on('click', function () {
        const $selectedCards = $('.js-family-card.selected');
        const selectedCount = $selectedCards.length;

        if (confirm(`Вы уверены, что хотите удалить ${selectedCount} семейств?`)) {
            $selectedCards.fadeOut(300, function () {
                $(this).remove();
                toggleDeleteButton();

                if ($('.js-family-card').length === 0) {
                    $('.grid--families').replaceWith(`<p class="empty-state">Нет семейств животных</p>`);
                }
            });
        }
    });
});