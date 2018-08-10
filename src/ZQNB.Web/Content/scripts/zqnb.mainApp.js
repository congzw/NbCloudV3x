var zqnb = zqnb || {};

(function () {
    'use strict';

    zqnb.mainApp = angular.module('mainApp', []);

    var mainApp = zqnb.mainApp;

    mainApp.factory("serviceFactory", function ($http) {
        var createService = function (area, name) {
            var serviceName = area;
            if (name) {
                serviceName += "/" + name;
            }
            var service = {
                edit: function (item) {
                    var editUri = zqnb.route.makeUrl('api', serviceName, 'put') + '/' + item.Id;
                    return $http.post(editUri, item);
                },
                add: function (item) {
                    var addUri = zqnb.route.makeUrl('api', serviceName, 'post');
                    return $http.post(addUri, item);
                },
                remove: function (id) {
                    var removeUri = zqnb.route.makeUrl('api', serviceName, 'delete') + '/' + id;
                    return $http.post(removeUri);
                },
                get: function (query) {
                    var getUri = zqnb.route.makeUrl('api', serviceName, 'get');
                    return $http.get(getUri, { params: query });
                },
                getxxx: function () {
                    var getUri = zqnb.route.makeUrl('api', serviceName, 'get') + '/';
                    return $http.get(getUri, { params: query });
                }
            }; 

            service.addFunction = function (method, httpVerb, notWrapQueryParams) {
                service[method] = function (params) {
                    httpVerb = httpVerb.toLowerCase();
                    if (httpVerb == "get") {
                        var getUri = zqnb.route.makeUrl('api', serviceName, method);
                        if (notWrapQueryParams) {
                            return $http.get(getUri, params);
                        }
                        return $http.get(getUri, { params: params });
                    }
                    else if (httpVerb == "post") {
                        var addUri = zqnb.route.makeUrl('api', serviceName, method);
                        return $http.post(addUri, params);
                    } else {
                        alert("不支持的谓词");
                    }
                    return null;
                };
            };
            return service;
        };

        return {
            createService: createService
        };
    });
}());
