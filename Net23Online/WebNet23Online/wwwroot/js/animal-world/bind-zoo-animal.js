(function () {
    'use strict';

    const zooSelect = document.getElementById('ZooId');
    if (!zooSelect) return;

    zooSelect.addEventListener('change', function () {
        const checkboxes = document.querySelectorAll('.checkbox-input');
        checkboxes.forEach(function (cb) {
            cb.checked = false;
        });
    });
})();