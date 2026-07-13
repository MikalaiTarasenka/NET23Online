$(document).ready(function () {
    $('#comment-form').on('submit', function (e) {
        e.preventDefault();

        $.post('/api/Comments/AddComment', $(this).serialize())
            .done(function (result) {
                const $newComment = $(`
                    <article class="comment-item comment-item-new" style="display: none;">
                        <div class="comment-item-header">
                            <strong class="comment-author">${result.author}</strong>
                            <span class="comment-date">${result.createdAt}</span>
                        </div>
                        <p class="comment-text"></p>
                    </article>
                `);

                $newComment
                    .find('.comment-text')
                    .text(result.text);

                $newComment
                    .prependTo('#comments-container')
                    .slideDown(200);

                $('#empty-note')
                    .hide();

                $('#form-comment-text')
                    .val('');
            });
    });
});