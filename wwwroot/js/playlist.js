$(document).ready(function () {
    var playlistItems = $(".playlist-item");
    var expandButton = $("#expand-button");

    // Ocultar playlists a partir de la segunda
    playlistItems.slice(1).hide();

    if (playlistItems.length > 1) {
        expandButton.show();

        expandButton.click(function () {
            playlistItems.slice(1).toggle(); // Mostrar u ocultar a partir de la segunda playlist

            if (playlistItems.slice(1).is(":visible")) {
                expandButton.text("Mostrar Menos-");
            } else {
                expandButton.text("Mostrar Todo+");
            }
        });
    }
});
