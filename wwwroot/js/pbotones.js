document.addEventListener('DOMContentLoaded', function () {
    var addToPlaylistButtons = document.querySelectorAll('.view-button');

    function redirectToAction(action, id) {
        window.location.href = `/Playlists/${action}/${id}`;
    }

    addToPlaylistButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            var action = button.getAttribute('data-action');
            var id = button.getAttribute('data-id');
            redirectToAction(action, id);
        });
    });
});
