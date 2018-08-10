var zqnb = zqnb || {};

(function () {
    'use strict';

    if (zqnb.inited) {
        //防止页面重复加载
        return;
    }
    //------ route 路由相关-------
    var makeUrl = function (area, controller, action, queryString) {
        if (!queryString) {

            return "/" + area + "/" + controller + "/" + action;
        }
        var fdStart = queryString.indexOf("?");
        if (fdStart == -1) {

            return "/" + area + "/" + controller + "/" + action + "?" + queryString;
        }
        return "/" + area + "/" + controller + "/" + action + queryString;
    };
    var makeSiteUrl = function (site, area, controller, action, queryString) {
        if (!queryString) {
            return "/" + site + "/" + area + "/" + controller + "/" + action;
        }
        var fdStart = queryString.indexOf("?");
        if (fdStart == -1) {
            return "/" + site + "/" + area + "/" + controller + "/" + action + "?" + queryString;
        }
        return "/" + site + "/" + area + "/" + controller + "/" + action + queryString;
    };
    var makeSpaceUrl = function (owner, area, controller, action, queryString) {
        if (!queryString) {
            return "/" + space + "/" + owner + "/" + controller + "/" + action;
        }
        var fdStart = queryString.indexOf("?");
        if (fdStart == -1) {
            return "/" + space + "/" + owner + "/" + controller + "/" + action + "?" + queryString;
        }
        return "/" + space + "/" + owner + "/" + controller + "/" + action + queryString;
    };
    var makeApiUrl = function (area, controller, action, queryString) {

        if (!queryString) {
            if (!area) {
                return "/" + api + "/" + controller + "/" + action;
            }
            return "/" + "api" + "/" + area + "/" + controller + "/" + action;
        }

        var fdStart = queryString.indexOf("?");
        if (fdStart == -1) {
            if (!area) {
                return "/" + api + "/" + controller + "/" + action + "?" + queryString;
            }
            return "/" + "api" + "/" + area + "/" + controller + "/" + action + "?" + queryString;
        }

        if (!area) {
            return "/" + api + "/" + controller + "/" + action + "?" + queryString;
        }
        return "/" + "api" + "/" + area + "/" + controller + "/" + action + "?" + queryString;
    };
    var appendQueryString = function (url, queryString) {
        if (!queryString) {
            return url;
        }
        var urlHasQMark = queryString.indexOf("?") > -1;
        var fdSqueryStringHasQMark = queryString.indexOf("?") > -1;
        //console.log(queryString + fdSqueryStringHasQMark);
        if (!urlHasQMark && fdSqueryStringHasQMark) {
            return url + "?" + queryString;
        }
        return url + queryString;
    };

    zqnb.route = {
        makeUrl: makeUrl,
        makeSiteUrl: makeSiteUrl,
        makeSpaceUrl: makeSpaceUrl,
        makeApiUrl: makeApiUrl,
        appendQueryString: appendQueryString
    };

    var makeTime = function (date) {
        return date.replace("T", " ");
    };
    zqnb.timeFormat = {
        makeTime: makeTime
    };

    var nbSubstring = function (str, start, len, suffix) {
        if (!str) {
            return "";
        }
        var needSuffix = false;
        var l = 0;
        var a = str.split("");
        for (var i = 0; i < a.length; i++) {
            if (a[i].charCodeAt(0) < 299) {
                l++;
            } else {
                l += 2;
            }
            if (l > len) {
                str = str.substring(0, i);
                needSuffix = true;
                break;
            }
        }
        if (needSuffix && !!suffix) {
            str += suffix;
        }
        return str + "";

    };

    /* Creates a name namespace.
    *  Example:
    *  var subjectService = zqnb.utils.createNamespace(zqnb, 'api.subject');
    *  ISubjectAppService will be equal to zqnb.api.subject
    *  first argument (root) must be defined first
    ************************************************************/
    var createNamespace = function (root, ns) {
        var parts = ns.split('.');
        for (var i = 0; i < parts.length; i++) {
            if (typeof root[parts[i]] === 'undefined') {
                root[parts[i]] = {};
            }
            root = root[parts[i]];
        }
        return root;
    };
    zqnb.utils = {
        nbSubstring: nbSubstring,
        createNamespace: createNamespace
    };

    //------ ace 初始化AceUI-------
    var saveAceSetting = function (aceSetting) {
        $(document).ready(function () {
            ace.settings.navbar_fixed(null, aceSetting.FixedNavbar);
            ace.settings.sidebar_fixed(null, aceSetting.FixedSidebar);
            ace.settings.breadcrumbs_fixed(null, aceSetting.FixedBreadcrumbs);
            ace.settings.main_container_fixed(null, aceSetting.InsideContainer);
            //ace.settings.sidebar_collapsed(null, true, false);
            //上面的事件不需要触发

            //改写了ace.extra.js，不显示settingBox的事件也不会被触发
            //如果修改Ace UI，保存到服务器上
            $(document).on('settings.ace', function (event, name, status) {
                if (name == "sidebar_collapsed") {
                    //此项不保存
                    //用户自己的设置为准
                    return;
                }

                console.log(name + " : " + status);
                var uri = zqnb.route.makeUrl("Api", "Ace", "SaveAceSettingDto");
                $.post(uri, { name: name, status: status })
                    .done(function () {
                        console.log("服务器保存" + name + " : " + status);
                    });
            });
        });
    };
    var initAceSetting = function (aceSetting) {
        ace.settings.navbar_fixed(null, aceSetting.FixedNavbar);
        ace.settings.sidebar_fixed(null, aceSetting.FixedSidebar);
        ace.settings.breadcrumbs_fixed(null, aceSetting.FixedBreadcrumbs);
    };
    zqnb.ace = {
        initAceSetting: initAceSetting
    };

    //------ prototype extension for zqnb-------
    //对string的一些扩展
    String.prototype.nbSubstring = function (start, len, suffix) {
        return zqnb.utils.nbSubstring(this, start, len, suffix);
    };

    zqnb.inited = true;
}());