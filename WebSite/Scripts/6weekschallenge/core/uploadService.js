(function () {
    'use strict';

    angular
        .module('6weekschallenge.core')
        .factory('uploadService', uploadService);

    function uploadService(Upload, utilsService, settings) {
        return {
            getNormalizedParamsImage: function (file) {
                return utilsService.getNormalizeFileName(file)
                    .then(function (fileResult) {
                       
                        return {
                            url: settings.UrlS3,
                            fields: {
                                key: 'images/' + fileResult.normalizedName,
                                AWSAccessKeyId: settings.AWSAccessKeyId,
                                acl: 'public-read',
                                policy: settings.UploadPolicy,
                                signature: settings.UploadSignature,
                                "Content-Type": fileResult.file.type != '' ? fileResult.file.type : 'application/octet-stream',
                                filename: fileResult.file.name // this is needed for Flash polyfill IE8-9
                            },
                            file: fileResult.file,
                            urlImage: settings.UrlS3 + 'images/' + fileResult.normalizedName
                        };
                    });
            },
            imageUpload: function (param) {
                return Upload.upload(param);
            }
        };
    };


})();