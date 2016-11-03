var MODAL = (function () {

    function _show() {
        // Get the modal
        $('#js-ammo-modal').addClass("md-show");
    }

    function _fill(html) {
        $('#js-ammo-modal').html(html);
    }
  
    function _hide() {
        $('#js-ammo-modal').removeClass("md-show");
        $('#js-ammo-modal').empty();
    }

    return {
        Fill: _fill,
        Hide: _hide,
        Show: _show
    };
})();