var FORM = (function () {

    function onSubmit() {
        // will first fade out the loading animation
        jQuery(".status").fadeIn();
        // will fade out the whole DIV that covers the website.
        jQuery(".preloader").fadeIn("slow");
    }

    function onComplete() {
        // will first fade out the loading animation
        jQuery(".status").fadeOut();
        // will fade out the whole DIV that covers the website.
        jQuery(".preloader").delay(1000).fadeOut("slow");
    }

    return {
        OnSubmit: onSubmit,
        OnComplete: onComplete
    };
})();