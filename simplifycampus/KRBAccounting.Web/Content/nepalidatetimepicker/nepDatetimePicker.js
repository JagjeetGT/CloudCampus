$(function () {
    $.datetimepicker.setDefaults($.datetimepicker.getRegional('ne-NP'));
});
var dateId = "";
var displayId = "";
var mitiId = "";
var showElement;
var miti = "";
var element;
var nepDate = "";
var engDate = "";
var nepaliMonth = ['Baishākh', 'Jesṭha', 'Asadh', 'Shrawan', 'Bhadra', 'Asoj', 'Kartik', 'Mangsir', 'Paush', 'Magh', 'Falgun', 'Chaitra'];
function getBottomDate() {
    var currentDay = $(".datetimepicker_currentDay span");
    //    alert(parseInt(currentDay.attr("month")));
    //alert(currentDay.attr("month") + "," + parseInt(currentDay.attr("month")));
    var day = currentDay.attr("day");


    var month = currentDay.attr("month");
    var monthPrefix = month.charAt(0);
    if (monthPrefix == "0") {
        month = month.substring(1);
    }

    var npMonth = nepaliMonth[parseInt(month) - 1];
    var year = currentDay.attr("year");
    var dateBottom = day + " " + npMonth + ", " + year;
    return dateBottom;
}
(function ($) {
    //    jQuery.fn.nepDateTimePicker = function(dateId,displayId,mitiId) {
    jQuery.fn.nepDateTimePicker = function () {
        return this.each(function () {
            $(this).datetimepicker({
                displaytime: false,
                showOn: 'button',
                buttonImage: '/Content/nepalidatetimepicker/icon_calendar.gif',
                buttonImageOnly: true,
                yearRange: 'null',
                dateFormat: 'MM/dd/yyyy',
                enableDsl: true
            });
            if ($(this).attr("displayId")) {
                displayId = $(this).attr("displayId");
                showElement = $("#" + displayId);
            } else {
                showElement = $(this);
                displayId = "";
            }
            

            showElement.focus(function () {
                element = $(this);
                if (element.attr("dateId")) {
                    dateId = element.attr("dateId");
                } else {
                    dateId = "";
                }
                if (element.attr("displayId")) {
                    displayId = element.attr("displayId");
                } else {
                    displayId = "";
                }
                
                if(!$('#'+displayId).hasClass("has_datepicker")) {
                    $('#'+displayId).addClass("has_datepicker");
                }

                if (element.attr("mitiId")) {

                    mitiId = element.attr("mitiId");
                    //alert(1);
                } else {

                    mitiId = "";
                    //alert(mitiId);
                }
                $(this).closest("span").find(".datetimepicker_trigger").click();
                var dateBottom = getBottomDate();
                var date = "<div class='date_bottom'>" + dateBottom + "</div>";
                var html = "";
                if ($(this).attr("miti")) {
                    //alert(1);
                    miti = $(this).attr("miti");
                } else {
                    //alert(2);
                    //alert("miti =3");
                    miti = "3";
                }

                //if(mitiId!=null) {
                html = "<div class='datetimepicker_div'>" + $("#datetimepicker_div").html() + date + "</div>";
                //} else {
                //html = "<div class='datetimepicker_div'>" + $("#datetimepicker_div").html() + "</div>";
                //}

                $(this).removeData('qtip')
                    .qtip({
                        content: {
                            text: html,
                            title: {
                                text: "",
                                button: false
                            }
                        },
                        position: {
                            my: "top center", // Use the corner...
                            at: "bottom center" // ...and opposite corner
                        },
                        show: {
                            event: false, // Don't specify a show event...
                            ready: true, // ... but show the tooltip when ready
                            solo: true
                        },
                        hide: 'unfocus', // Don't specify a hide event either!
                        style: {
                            classes: 'ui-tooltip-shadow ui-tooltip-' + 'bootstrap'
                        }
                    });
                $(this).focusout();
            });
        });
    };
})(jQuery);

//function convertToNepali(date) {
//    var engDate = date.split("/");
//    var day = engDate[0];
//    var mth = engDate[1];
//    var year = engDate[2];
//    var nepDate = gregorianToLocal(3, year, mth, day);
//    var convertedDate = nepDate.month + "/" + nepDate.day + "/" + nepDate.year;
//    return convertedDate;
//}

//function convertToEnglish(date) {
//    var nepDate = date.split("/");
//    var day = nepDate[0];
//    var mth = nepDate[1];
//    var year = nepDate[2];
//    var engDate = localToGregorian(3, year, mth, day);
//    var convertedDate = engDate.month + "/" + engDate.day + "/" + engDate.year;
//    return convertedDate;
//}

function convertToNepali(date) {
    var engDate = date.split("/");
    var day = parseInt(engDate[0].replace(/^0+/, ''));
    var mth = parseInt(engDate[1].replace(/^0+/, ''));
    var year = parseInt(engDate[2]);
    var nepDate = gregorianToLocal(3, year, mth, day);
    var convertedDate = nepDate.year + "/" + nepDate.month + "/" + nepDate.day;
    return convertedDate;
}

function convertToEnglish(date) {
    var nepDate = date.split("/");
    var year = nepDate[0];
    var mth = nepDate[1];
    var day = nepDate[2];
    var engDate = localToGregorian(3, year, mth, day);
    var convertedDate = engDate.month + "/" + engDate.day + "/" + engDate.year;
    return convertedDate;
}

function convertToNepaliNew(date) {

    var engDate = date.split("/");
    var mth = parseInt(engDate[0].replace(/^0+/, ''));
    var day = parseInt(engDate[1].replace(/^0+/, ''));
    var year = parseInt(engDate[2]);
    var nepDate = gregorianToLocal(3, year, mth, day);
    var convertedDate = nepDate.year + "/" + nepDate.month + "/" + nepDate.day;
    return convertedDate;
}

function convertToEnglishNew(date) {
    var nepDate = date.split("/");
    var year = nepDate[0];
    var mth = nepDate[1];
    var day = nepDate[2];
    var engDate = localToGregorian(3, year, mth, day);
    var convertedDate = engDate.month + "/" + engDate.day + "/" + engDate.year;
    return convertedDate;
}

function isNepaliDate(date) {
    var engDate = convertToEnglish(date);
    if (engDate == "00/00/0000") {
        return false;
    } else {
        return true;
    }
}

$(".setDate").live("click", function () {
    engDate = $(this).find("span").attr("engDate");
    if (mitiId != "") {
        var day = $(this).find("span").attr("day");
        var month = $(this).find("span").attr("month");
        var year = $(this).find("span").attr("year");
        //nepDate = month + "/" + day + "/" + year;
        nepDate = year + "/" + month + "/" + day;

        $("#" + mitiId).val(nepDate);
    }
    if (dateId != "") {
        $("#" + dateId).val(engDate);
    }
    if (miti == "2") {
        element.val(nepDate);
    } else if (miti == "1") {

        element.val(engDate);
    }
    $(".qtip").remove();
});

$(".setDate").live("mouseover", function () {
    var day = $(this).find("span").attr("day");
    var month = $(this).find("span").attr("month");
    var monthPrefix = month.charAt(0);
    if (monthPrefix == "0") {
        month = month.substring(1);
    }
    var npMonth = nepaliMonth[parseInt(month) - 1];
    var year = $(this).find("span").attr("year");
    var date = day + " " + npMonth + ", " + year;
    $(".date_bottom").text(date);
});
$(".setDate").live("mouseout", function () {
    $(".date_bottom").text(getBottomDate());
});

//$(".has_datepicker").live("focusout", function() {
//    $(".qtip").remove();
//});




(function ($) {
    //    jQuery.fn.nepDateTimePicker = function(dateId,displayId,mitiId) {
    jQuery.fn.nepNoBackDatePicker = function () {

        return this.each(function () {
            $(this).datetimepicker({
                displaytime: false,
                showOn: 'button',
                buttonImage: '/Content/nepalidatetimepicker/icon_calendar.gif',
                buttonImageOnly: true,
                yearRange: 'null',
                dateFormat: 'MM/dd/yyyy',
                enableDsl: true,
                minDate: '\/Date(<%= DateTime.Now.Ticks %>)\/'
            });
            if ($(this).attr("displayId")) {
                displayId = $(this).attr("displayId");
                showElement = $("#" + displayId);
            } else {
                showElement = $(this);
                displayId = "";
            }
            showElement.focus(function () {
                element = $(this);
                if (element.attr("dateId")) {
                    dateId = element.attr("dateId");
                } else {
                    dateId = "";
                }
                if (element.attr("displayId")) {
                    displayId = element.attr("displayId");
                } else {
                    displayId = "";
                }
                if (element.attr("mitiId")) {

                    mitiId = element.attr("mitiId");
                    //alert(1);
                } else {

                    mitiId = "";
                    //alert(mitiId);
                }
                $(this).closest("span").find(".datetimepicker_trigger").click();
                var dateBottom = getBottomDate();
                var date = "<div class='date_bottom'>" + dateBottom + "</div>";
                var html = "";
                if ($(this).attr("miti")) {
                    //alert(1);
                    miti = $(this).attr("miti");
                } else {
                    //alert(2);
                    //alert("miti =3");
                    miti = "3";
                }

                //if(mitiId!=null) {
                html = "<div class='datetimepicker_div'>" + $("#datetimepicker_div").html() + date + "</div>";
                //} else {
                //html = "<div class='datetimepicker_div'>" + $("#datetimepicker_div").html() + "</div>";
                //}

                $(this).removeData('qtip')
                    .qtip({
                        content: {
                            text: html,
                            title: {
                                text: "",
                                button: false
                            }
                        },
                        position: {
                            my: "top center", // Use the corner...
                            at: "bottom center" // ...and opposite corner
                        },
                        show: {
                            event: false, // Don't specify a show event...
                            ready: true, // ... but show the tooltip when ready
                            solo: true
                        },
                        hide: 'unfocus', // Don't specify a hide event either!
                        style: {
                            classes: 'ui-tooltip-shadow ui-tooltip-' + 'bootstrap'
                        }
                    });
                $(this).focusout();
            });
        });
    };
})(jQuery);