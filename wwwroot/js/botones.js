document.addEventListener('DOMContentLoaded', function () {
    var updateButtons = document.querySelectorAll('.update-button');
    var deleteButtons = document.querySelectorAll('.delete-button');
    var agregarButton = document.getElementById('agregarButton');

    function redirectToAction(action, id) {
        window.location.href = `/Cancions/${action}/${id}`;
    }

    function redirectToAgregarAction() {
        var action = agregarButton.getAttribute('data-action');
        redirectToAction(action, '');
    }

    agregarButton.addEventListener('click', redirectToAgregarAction);

    updateButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            var action = button.getAttribute('data-action');
            var id = button.getAttribute('data-id');
            redirectToAction(action, id);
        });
    });

    deleteButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            var action = button.getAttribute('data-action');
            var id = button.getAttribute('data-id');
            redirectToAction(action, id);
        });
    });
});
