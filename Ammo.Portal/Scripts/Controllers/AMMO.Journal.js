var JOURNAL = (function () {

    function _onDeleteSuccess(data) {
        $('#js-ammo-modal-content').empty();
        $('#js-ammo-modal-content').html(data);
        MODAL.Show();
    }
    
    function _onBulletCollectionSuccess(data) {
        MODAL.Hide();
        window.Location.reload();
    }

    function _onShowBulletCollectionSuccess(data) {
        $('#js-ammo-modal-content').empty();
        $('#js-ammo-modal-content').html(data);
        MODAL.Show();
    }

    return {
        OnBulletCollectionSuccess: _onBulletCollectionSuccess,
        OnDeleteSuccess: _onDeleteSuccess,
        OnShowBulletCollectionSuccess: _onShowBulletCollectionSuccess
    };
})();