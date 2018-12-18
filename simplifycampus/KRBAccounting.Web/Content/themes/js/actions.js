$(document).ready(function() {


    $(".header_menu .list_icon").click(function() {

        var menu = $("body > .menu");

        if (menu.is(":visible"))
            menu.fadeOut(200);
        else
            menu.fadeIn(300);

        return false;
    });

    if ($(".adminControl").hasClass('active')) {
        $('.admin').fadeIn(300);
    }


    $(".adminControl").click(function() {

        if ($(this).hasClass('active')) {

            // $.cookies.set('b_Admin_visibility', 'hidden');

            $('.admin').fadeOut(200);

            $(this).removeClass('active');

        } else {

            // $.cookies.set('b_Admin_visibility', 'visible');

            $('.admin').fadeIn(300);

            $(this).addClass('active');
        }

    });

    $(".buttons li > a").click(function() {

        var parent = $(this).parent();

        if (parent.find(".dd-list").length > 0) {

            var dropdown = parent.find(".dd-list");

            if (dropdown.is(":visible")) {
                dropdown.hide();
                parent.removeClass('active');
            } else {
                dropdown.show();
                parent.addClass('active');
            }

            return false;

        }

    });


    $(document).resize(function() {

        if ($("body > .content").css('margin-left') == '220px') {
            if ($("body > .menu").is(':hidden'))
                $("body > .menu").show();
        }

        
        thumbs();
        headInfo();
    });

    function headInfo() {
        var block = $(".headInfo .input-append");
        var input = block.find("input[type=text]");
        var button = block.find("button");

        input.width(block.width() - button.width() - 44);

    }

    function thumbs() {

        var w_block = $(".block.thumbs").width() - 20;
        var w_item = $(".block.thumbs .thumbnail").width() + 10;

        var c_items = Math.floor(w_block / w_item);

        var m_items = Math.floor((w_block - w_item * c_items) / (c_items * 2));

        $(".block.thumbs .thumbnail").css('margin', m_items);

    }

});