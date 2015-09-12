(function () {
    'use strict';

    angular
        .module('6weekschallenge.core')
        .factory('uploadService', uploadService);

    function uploadService(Upload, utilsService) {
        return {
            getNormalizedParamsImage: function (file) {
                return utilsService.getNormalizeFileName(file)
                    .then(function (fileResult) {
                        return {
                            url: 'https://6weekschallenge-dev.s3.amazonaws.com/',
                            fields: {
                                key: 'images/' + fileResult.normalizedName,
                                AWSAccessKeyId: 'AKIAJDM4MVKTULA4W6ZQ',
                                acl: 'public-read',
                                policy: 'ew0KICAiZXhwaXJhdGlvbiI6ICIyMDE2LTA5LTEyVDAwOjU1OjAxWiIsDQogICJjb25kaXRpb25zIjogWw0KICAgIHsiYnVja2V0IjogIjZ3ZWVrc2NoYWxsZW5nZS1kZXYifSwNCiAgICBbInN0YXJ0cy13aXRoIiwgIiRrZXkiLCAiaW1hZ2VzLyJdLA0KICAgIHsiYWNsIjogInB1YmxpYy1yZWFkIn0sDQogICAgWyJzdGFydHMtd2l0aCIsICIkQ29udGVudC1UeXBlIiwgImltYWdlLyJdLA0KICAgIFsic3RhcnRzLXdpdGgiLCAiJGZpbGVuYW1lIiwgIiJdLA0KICAgIFsiY29udGVudC1sZW5ndGgtcmFuZ2UiLCAwLCAxMDQ4NTc2MF0NCiAgXQ0KfQ==',
                                signature: 'GtFinPFteLfx+x8ICS2Ef7N3L68=',
                                "Content-Type": fileResult.file.type != '' ? fileResult.file.type : 'application/octet-stream',
                                filename: fileResult.file.name // this is needed for Flash polyfill IE8-9
                            },
                            file: fileResult.file,
                            urlImage: 'https://6weekschallenge-dev.s3.amazonaws.com/' + 'images/' + fileResult.normalizedName
                        };
                    });
            },
            imageUpload: function (param) {
                return Upload.upload(param);
            }
        };
    };


})();