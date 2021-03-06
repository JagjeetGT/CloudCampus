/********************************************
**** UI-Pro jQuery Plugin
**** Created by: wintercounter
**** Version: 1.2
**** Available at CodeCanyon
********************************************/
(function (e) {
    var t, n, r; e.uiPro = function (r) {
        params = null; params = e.extend(
            { init: "both",
                leftMenu: false,
                rightMenu: false,
                threshold: 40,
                onMouse: true,
                onSwipe: true
            }, r);
            t = e(document).width();
            n = e(document).height();
            e.uiPro.init()
        };
        e.uiPro.init = function () {
            var n = false; var r = false;
            var i = right = false;
            if (params.init == "left" || params.init == "both") {
                if (e.uiPro.createMenu("left"))
                { i = e("#uipro_left") } 
            }
            if (params.init == "right" || params.init == "both")
            { if (e.uiPro.createMenu("right")) { right = e("#uipro_right") } }
            if (i || right) {
                var s = 0; var o = params.onMouse ? "mousemove " : "";
                o = params.onSwipe ? o + "touchstart " : o + ""; o = o + "uiRightOpen uiLeftOpen uiRightClose uiLeftClose"; 
                var u = false; e(document).bind(o, function (e) { if (u && e.type != "uiRightOpen" && e.type != "uiLeftOpen" && e.type != "uiRightClose" && e.type != "uiLeftClose") { return } if (typeof e.originalEvent != "undefined" && typeof e.originalEvent.touches != "undefined") { s = e.originalEvent.touches[0].pageX } if (i) { if (e.pageX < params.threshold && !i.hasClass("active") || !i.hasClass("active") && e.type == "touchstart" && s < params.threshold + 100 || e.type == "uiLeftOpen") { i.addClass("active"); u = true; setTimeout(function () { u = false }, 500) } else if (i.hasClass("active") && e.pageX > i.width() || i.hasClass("active") && e.type == "touchstart" && s < i.width() + 100 || e.type == "uiLeftClose") { i.removeClass("active"); u = true; setTimeout(function () { u = false }, 500) } } if (right) { if (e.pageX > t - params.threshold && !right.hasClass("active") || !right.hasClass("active") && e.type == "touchstart" && s > t - params.threshold - 100 || e.type == "uiRightOpen") { right.addClass("active"); u = true; setTimeout(function () { u = false }, 500) } else if (right.hasClass("active") && e.pageX < t - right.width() || right.hasClass("active") && e.type == "touchstart" && s > t - right.width() - 100 || e.type == "uiRightClose") { right.removeClass("active"); u = true; setTimeout(function () { u = false }, 500) } } }) } }; e.uiPro.open = function (t) { var n = t == "left" ? "uiLeftOpen" : "uiRightOpen"; e(document).trigger(n) }; e.uiPro.close = function (t) { var n = t == "left" ? "uiLeftClose" : "uiRightClose"; e(document).trigger(n) }; e.uiPro.toggle = function (t) { var n = false; if (e("#uipro_" + t).hasClass("active")) { n = t == "left" ? "uiLeftClose" : "uiRightClose" } else { n = t == "left" ? "uiLeftOpen" : "uiRightOpen" } e(document).trigger(n) }; e.uiPro.createMenu = function (t) { var n = e.uiPro.parseMenuItems(t); if (n) { e("body").append('<div id="uipro_' + t + '" class="uipro">' + n + "</div>"); return true } else { return false } }; e.uiPro.parseMenuItems = function (t) { setTimeout(function () { var n = e("#uipro_" + t + " ul li").length; e("#uipro_" + t + " ul").css({ height: n * 100 + "px", marginTop: n * 100 / 2 * -1 + "px" }) }, 100); if (typeof params[t + "Menu"] == "object") { var n = "<ul>"; var r = params[t + "Menu"]; for (var i in r) { i = r[i]; i.target = typeof i.target == "undefined" ? "_self" : i.target; n += '<li><a class="a_' + i.klass + '" href="' + i.link + '" target="' + i.target + '"><i class="' + i.klass + '"></i><span>' + i.label + "</span></a></li>\n" } n += "\n</ul>"; return n } else if (typeof params[t + "Menu"] == "string") { return e("<div />").append(e(params[t + "Menu"]).clone()).html() } else { return false } } })(jQuery)