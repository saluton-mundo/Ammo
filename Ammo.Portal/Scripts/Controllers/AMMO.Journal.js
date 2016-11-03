var JOURNAL = (function () {

    function _onDeleteSuccess(data) {
        $('#js-ammo-modal-content').html(data);
        MODAL.Show();
    }
    
    return {
        OnDeleteSuccess: _onDeleteSuccess
    };
})();