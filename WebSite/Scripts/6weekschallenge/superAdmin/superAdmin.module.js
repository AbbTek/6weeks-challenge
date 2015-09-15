(function () {
	'use strict';

	angular
		.module('6weekschallenge.superAdmin',
		[
		'6weekschallenge.core',
		'uiGmapgoogle-maps'
		])
		.config(function (uiGmapGoogleMapApiProvider) {
			uiGmapGoogleMapApiProvider.configure({
				//    key: 'your api key',
				v: '3.20', //defaults to latest 3.X anyhow
				libraries: 'places'
			});
		});
})();