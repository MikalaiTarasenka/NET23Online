$(document).ready(function () {
    const $imageInput = $('#imageInput');
    const $imagePreview = $('#imagePreview');
    const $previewImg = $imagePreview.find('img');

    if ($imageInput.length && $imagePreview.length && $previewImg.length) {
        $imageInput.on('change', function (e) {
            const file = e.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (event) {
                    $previewImg.attr('src', event.target.result);
                    $imagePreview.show();
                };
                reader.readAsDataURL(file);
            }
        });
    }
});