(function () {
    var $sidebarAndWrapper = $("#sidebar,#mainwrapper");
    var $icon = $("#toggleSidebar i.fa");

	$("#toggleSidebar").on("click",function () {
		$sidebarAndWrapper.toggleClass("hidesidebar");
		if ($sidebarAndWrapper.hasClass("hidesidebar")) {
		    $icon.removeClass("fa-angle-double-left");
		    $icon.addClass("fa-angle-double-right");
		} else {
		    $icon.removeClass("fa-angle-double-right");
		    $icon.addClass("fa-angle-double-left");
		};
	});
})();
