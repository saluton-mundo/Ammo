/* Menu Specific JS*/
var MENU = (function() {

	function _initMenu() {	
		// Create a new instance of Slidebars (@see js/slidebars.min.js)
	    var controller = new slidebars();

    	// Initialize Slidebars
    	controller.init();

    	// Wire up open/close event to the menu icon
	    $('.js-toggle-right-slidebar').on('click', function(event) {
	        event.stopPropagation();
	        //$(".js-toggle-right-slidebar").toggleClass("js-toggle-right-slidebar-close");
	        controller.toggle('slidebar-2');
	    });

	    // Wire up the open/close event to the toolbox icon
	    $('.js-toggle-left-slidebar').on('click', function(event) {
	    	 event.stopPropagation();
	        //$(".js-toggle-left-slidebar").toggleClass("js-toggle-left-slidebar-close");
	        controller.toggle('slidebar-1');
	    });
	  
	    // Bottom Slidebar controls
	    $('.js-toggle-bottom-slidebar').on('click', function (event) {

	        event.stopPropagation();

        	$("#layouts-menu").hide();
        	$("#signifiers-menu").hide();
        	$("#bullets-menu").hide();

	        if($(this).attr('class').includes("bullets-menu-trigger")) {
	        	$("#bullets-menu").show();
	        } else if ($(this).attr('class').includes("layouts-menu-trigger")) {
	        	$("#layouts-menu").show();
	        } else if ($(this).attr('class').includes("signifiers-menu-trigger")) {
	        	$("#signifiers-menu").show();
	        }

	        controller.toggle('slidebar-3');
	    });

	   // Ensure that the slide out menu is close my touch,clicks in the main document. 
	    $(controller.events).on('opened', function () {
	        $('[canvas="container"]').addClass('js-close-any-slidebar');
	    });
	 
	    $(controller.events).on('closed', function () {
	        $('[canvas="container"]').removeClass('js-close-any-slidebar');
	    });

	    $('body').on('click', '.js-close-any-slidebar', function(event) {
	        event.stopPropagation();
	        controller.close();
	    });
	}

	return {
		Init: _initMenu
	};
})();

// Wire up menu interaction after parsing the document - more efficient!
document.addEventListener("DOMContentLoaded", function (event) {
	MENU.Init();    
});
