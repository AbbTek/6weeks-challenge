var challengeApp = angular.module('challenge.superAdmin', 
['ngAnimate', 'ui.bootstrap', 'uiGmapgoogle-maps', 'ngFileUpload']);
challengeApp.config(function (uiGmapGoogleMapApiProvider) {
    uiGmapGoogleMapApiProvider.configure({
        //    key: 'your api key',
        v: '3.20', //defaults to latest 3.X anyhow
        libraries: 'places'
    });
});