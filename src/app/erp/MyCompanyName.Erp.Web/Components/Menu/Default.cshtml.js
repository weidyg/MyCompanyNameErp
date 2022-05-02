$(function () {
    $('.a-left-menu-panel-main .close').on('click', function (e) {
        $(this).parents('.a-left-menu-panel').fadeOut();
        $('.a-left-menu .link-a.action').removeClass("action");
        return false;
    });
    $('.a-left-menu-panel-mask').on('click', function (e) {
        $(this).parents('.a-left-menu-panel').fadeOut();
        $('.a-left-menu .link-a.action').removeClass("action");
        return false;
    });
    $('.a-left-menu-panel-main .filter-search-text input').focus(function () {
        $(".a-left-menu-panel-main  .filter-search-icon .anticon-search").addClass("action");
    });
    $('.a-left-menu-panel-main .filter-search-text input').blur(function () {
        $(".a-left-menu-panel-main  .filter-search-icon .anticon-search").removeClass("action");
    });
    $('.a-left-menu-panel-main .filter-search-text input').bind('input propertychange', function () {
        //$('.a-left-menu-panel-main .filter-search-text input').change(function () {
        let val = $(this).val();
        let hasMatch = false;
        let grouped = $(`.a-left-menu-panel-main .main-left .all .grouped:visible`);
        grouped.find(".group").each(function (i, n) {
            let group = $(n); group.hide();
            let hasTitleVal = group.find(".title").text().indexOf(val) > -1;
            group.find(".item-a").each(function (i, m) {
                let item = $(m);
                let hasVal = item.text().indexOf(val) > -1;
                if (hasTitleVal || hasVal) {
                    item.show();
                    group.show();
                    hasMatch = true;
                } else {
                    item.hide();
                }
            });
        });
        let tip = $(`.a-left-menu-panel-main .main-left .tip`);
        if (val) {
            tip.show();
            tip.html(`${hasMatch ? "Find" : "NoFind"}{0}`.format(`<code>${val}</code >`));
        }
        else {
            tip.hide();
        };
    });
    $('.a-left-menu .menu').on('click', function (e) {
        let menuPanel = $('.a-left-menu-panel');
        menuPanel.fadeIn();
        LeftMenuToggle();
    });
    $('.a-left-menu .menu').hover(function () {
        runLeftMenuToggle = true;
        setTimeout(function () { LeftMenuToggle() }, 300);
    }, function () {
        runLeftMenuToggle = false;
    });
    $('.a-left-menu .link-a').on('click', function (e) {
        let _that = $(this);
        let menuPanel = $('.a-left-menu-panel');
        menuPanel.fadeIn();
        LeftMenuToggle(_that);
        return false;
    });
    $('.a-left-menu .link-a').hover(function () {
        let _that = $(this);
        runLeftMenuToggle = true;
        setTimeout(function () { LeftMenuToggle(_that) }, 300);
    }, function () {
        runLeftMenuToggle = false;
    });
    var runLeftMenuToggle = true;
    function LeftMenuToggle(_that) {
        if (runLeftMenuToggle) {
            let menuPanel = $('.a-left-menu-panel');
            if (menuPanel.css("display") != 'none') {
                menuPanel.fadeIn();
                $('.a-left-menu .link-a.action').removeClass("action");
                if (_that) {
                    _that.addClass('action');
                    $(`.a-left-menu-panel-main .main-left .all [menu-module]`).hide();
                    $(`[id="${_that.attr('id')}_Grouped"]`).show();
                } else {
                    $(`.a-left-menu-panel-main .main-left .all [menu-module]`).show();
                }
                var grouped = $(`.a-left-menu-panel-main .main-left .all .grouped`);
                var column = 0;
                grouped.find("[menu-module]:visible").each(function (i, n) {
                    column += $(n).find(".group ").length;
                });
                column = column > 3 ? 3 : column < 1 ? 1 : column;
                grouped.css("columns", "180px " + column);
            }
            return false;
        }
    }
});

//var menuTab = document.getElementById("menuTab");
//function openPageTab(_that) {
//    let _url = _that.dataset.url;
//    let _target = _that.dataset.target;
//    if (_target == '_blank') {
//        window.open(_url); 
//    } else {
//        const iframe = document.createElement("iframe");
//        iframe.frameBorder = "0";
//        iframe.allowFullscreen = "true";
//        iframe.src = _url;
//        menuTab.openTab({
//            key: _that.key,
//            title: _that.title,
//            content: iframe,
//        });
//        $('.a-left-menu-panel-main .close').click();
//    }
//}