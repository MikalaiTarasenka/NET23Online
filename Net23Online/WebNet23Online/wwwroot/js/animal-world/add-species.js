$(document).ready(function () {
    const $previewImage = $('#image-preview');
    const $previewPlaceholder = $('.preview-placeholder');

    $previewImage.hide();
    $previewPlaceholder.show();

    $('#image-input').on('change', function (event) {
        const input = event.target;

        if (input.files && input.files[0]) {
            const reader = new FileReader();

            reader.onload = function (e) {
                $previewImage.attr('src', e.target.result);
                $previewImage.fadeIn(200);
                $previewPlaceholder.fadeOut(150);
            };

            reader.readAsDataURL(input.files[0]);
        } else {
            $previewImage.fadeOut(150, function () {
                $previewImage.attr('src', '#');
            });
            $previewPlaceholder.fadeIn(200);
        }
    });
});