$(document).ready(function () {
    const MAX_LENGTH = 1000;

    const $form = $('#commentForm');
    const $textarea = $('#commentText');
    const $submitBtn = $('#submitBtn');
    const $submitText = $('#submitText');
    const $charCount = $('#charCount');
    const $error = $('#commentError');
    const $commentsList = $('#commentsList');
    const $commentsCount = $('#commentsCount');
    const $commentsEmpty = $('#commentsEmpty');

    // Изначальное состояние кнопки
    updateButtonState();

    // Обновление счётчика и состояния кнопки при вводе
    $textarea.on('input', function () {
        const remaining = MAX_LENGTH - $textarea.val().length;
        $charCount.text(remaining);
        updateButtonState();
        hideError();
    });

    function updateButtonState() {
        const text = $textarea.val().trim();
        if (text.length === 0) {
            $submitBtn.prop('disabled', true);
        } else {
            $submitBtn.prop('disabled', false);
        }
    }

    function hideError() {
        $error.hide();
    }

    function showError() {
        $error.show();
    }

    function setLoading(isLoading) {
        if (isLoading) {
            $submitBtn.prop('disabled', true);
            $submitText.text('Отправка...');
        } else {
            $submitText.text('Отправить');
            updateButtonState();
        }
    }

    // Отправка комментария через AJAX
    $form.on('submit', function (e) {
        e.preventDefault();

        const text = $textarea.val().trim();
        if (text.length === 0) {
            return;
        }

        hideError();
        setLoading(true);

        // Получаем anti-forgery token
        const token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: '/api/CommentApi/AddComment',
            type: 'POST',
            data: {
                ZooId: $form.find('input[name="ZooId"]').val(),
                CommentText: text
            },
            headers: {
                'RequestVerificationToken': token
            },
            success: function (response) {
                // Очищаем поле и сбрасываем счётчик
                $textarea.val('');
                $charCount.text(MAX_LENGTH);

                // Скрываем пустое состояние, если оно было
                if ($commentsEmpty.length > 0) {
                    $commentsEmpty.remove();
                }

                // Добавляем новый комментарий в начало списка
                const newCommentHtml = `
                    <div class="comment-card comment-card-new">
                        <div class="comment-header">
                            <span class="comment-author"></span>
                            <span class="comment-date"></span>
                        </div>
                        <p class="comment-text"></p>
                    </div>
                `;
                const $newComment = $(newCommentHtml);
                $newComment.find('.comment-author').text(response.authorName);
                $newComment.find('.comment-date').text(response.createdAt);
                $newComment.find('.comment-text').text(response.text);

                $commentsList.prepend($newComment);

                // Обновляем счётчик комментариев
                const currentCount = parseInt($commentsCount.text(), 10);
                $commentsCount.text(currentCount + 1);

                setLoading(false);
            },
            error: function () {
                showError();
                setLoading(false);
            }
        });
    });
});