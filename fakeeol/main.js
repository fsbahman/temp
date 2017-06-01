(function(){
	var _panel = null;
	
	var injectCss = function() {
		var link = document.createElement( "link" );
		link.href = "style.css";
		link.type = "text/css";
		link.rel = "stylesheet";
		
		document.getElementsByTagName( "head" )[0].appendChild( link );
	};
	
	injectCss();
	
	var main = document.createElement('div');
	var menu = '<a href="#" onclick="onTabClicked(1);">about</a><a href="#" onclick="onTabClicked(2);">financial</a><a href="#" onclick="onTabClicked(3);">orders</a>';
	main.innerHTML = menu;
	main.classList.add('panel');
	main.classList.add('hidden');
	document.body.appendChild(main);
	_panel = main;
	
	document.addEventListener('keyup', onHotKeyClicked, false);
	
	var EolSrv = new EolService();
})();