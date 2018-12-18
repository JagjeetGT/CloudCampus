$(".ui-dialog-titlebar-close").live('click', function () {
    var i = 1;
});
var modalshow = true;
var changeId = "";
var elementId = "";
var element = "";
var rowIdForBillingTerm = "";
var d = 0;
var selectedElement = "";
var picklistelement = "";
var isProductWise = false;
var changeRate = false;
//BillingTerm Variables
var arrBillingTerms = new Array();
var arrBillWiseBillingTerms = new Array();
var breakloop = 0;


//$(document).ready(function () {
//    $("li.dropdown").live("hover", function () {
//        $(this).toggleClass('open');
//    });
//});
function modalShow() {
    modalshow = true;
}

function removeIdVal() {
    changeId = "";
    elementId = "";
}
$(".modallink").live('keydown', function (e) {
    if (!$(this).attr("disabled") && !$(this).attr("readonly")) {
        if (modalshow == true) {

            var element = $(this);
            elementId = $(this).attr("id");
            changeId = $(this).attr("valueId");
            if (!changeId) {
                var changeId = element.closest("td").find(".value").attr("id");
            }
            var ev = e || event;
            var keycode = ev.keyCode;
            if (keycode == "9") {
                if (element.val() != "") {
                    return true;
                }
            }
            if (keycode == "13") {
                return true;
            }
            if (keycode == 46 || keycode == 8) {
                element.val("");
                var valueId = $(this).attr("valueid");
                $("#" + valueId).val("0");
                return false;
            }
            if (keycode == 27) {
                setTimeout(function () {
                    element.closest("tr").find('td :input').each(function () {
                        $(this).val('');
                    });
                    if (typeof updateFunction == 'function') {
                        updateFunction();
                    }
                    if (element.closest("tr").hasClass('deletable')) {
                        element.closest("tr").remove();
                    }
                }, 200);

            }
            if ((keycode >= 65 && keycode <= 90) || keycode >= 48 && keycode <= 57) {
                $.blockUI();
                e.preventDefault();
                modalshow = false;
                var closesid = $(this).closest('form').attr("id");
                var link = $(this).attr("href");
                if (!link) {
                    return false;
                }
                var parent = $(this).attr('parent');
                if (parent) {

                    var id = $('#' + closesid + " #" + parent).val();

                    link = link + "?id=" + id;
                    var parentnext = $(this).attr('parentnext');
                    if (parentnext) {
                        var idnew = $('#' + closesid + " #" + parentnext).val();
                        link += "&&nextid=" + idnew;
                    }
                    var parentnextnext = $(this).attr('parentnextnext');
                    if (parentnextnext) {
                        var idnewnew = $('#' + closesid + " #" + parentnextnext).val();
                        link += "&&nextidnestid=" + idnewnew;
                    }
                    var children = $(this).attr('children');
                    if (children) {
                        var list = document.querySelectorAll("." + children);
                        var childId = "";
                       // alert(list.length);
                        //var ii = 0;
                        $.each(list, function (index) {
                            if (index > 0 && $(this).val() != 0) {
                                childId = childId + ",";
                            }

                            if ($(this).val() != 0) {
                                childId += $(this).val();
                            }

                        });

                        link += "&&childrenlist=" + childId;
                    }
                }
                var child = $(this).attr('child');
                if (child) {
                    var array = child.split(' ');
                    $("#" + array[0]).val("");
                    $("#" + array[1]).val("");
                }
                //changeId = $(this).attr("valueId");
                $.ajax({
                    url: link,
                    dataType: "json",
                    type: "GET",
                    error: function () {
                        alert("An error occurred.");
                        modalshow = true;
                    },
                    success: function (data) {

                        var tableData = CreateTableView(data, "listpopup", true, changeId, elementId);
                        $('#modalbox .msg-body').html(tableData);
                        var title = "";
                        if (element.attr("title")) {
                            title = element.attr("title");
                        }
                        $("#modalbox").dialog({
                            resizable: false,
                            width: 'auto',
                            modal: true,
                            title: title
                        });
                        $(".search-box").val("").focus();
                        modalshow = true;
                        $.unblockUI();
                    }
                });
            } else {
                e.preventDefault();
            }


        } else {
            return false;
        }
    }
});

$(".modallink_custom").live('keydown', function (e) {
    //    var isdisabled = $(this).attr("disabled");
    //    alert(isdisabled);
    if (!$(this).attr("disabled") && !$(this).attr("readonly")) {
        if (modalshow == true) {
            element = $(this);
            elementId = $(this).attr("id");
            changeId = $(this).attr("valueId");
            if (!changeId) {
                var changeId = element.closest("td").find(".value").attr("id");
            }
            var valuetype = $(this).attr("valuetype");
            var ev = e || event;
            var keycode = ev.keyCode;
            if (keycode == "9") {
                if (element.val() != "") {
                    return true;
                }
            }
            if (keycode == "13") {
                return true;
            }
            if (keycode == 46 || keycode == 8) {
                element.val("");
                var valueId = $(this).attr("valueid");
                $("#" + valueId).val("");
                return false;
            }
            if (keycode == 27) {
                setTimeout(function () {
                    element.closest("tr").find('td :input').each(function () {
                        $(this).val('');
                    });
                    if (typeof updateFunction == 'function') {
                        updateFunction();
                    }
                    if (element.closest("tr").hasClass('deletable')) {
                        element.closest("tr").remove();
                    }
                }, 200);
            }
            if ((keycode >= 65 && keycode <= 90) || keycode >= 48 && keycode <= 57) {
                $.blockUI();
                e.preventDefault();
                modalshow = false;
                var link = $(this).attr("href");
                if (!link) {
                    return false;
                }
                var parent = $(this).attr('parent');
                if (parent) {
                    var id = $("#" + parent).val();
                    link = link + "?id=" + id;
                }
                var child = $(this).attr('child');
                if (child) {
                    var array = child.split(' ');
                    $("#" + array[0]).val("");
                    $("#" + array[1]).val("");
                }
                //changeId = $(this).attr("valueId");
                $.ajax({
                    url: link,
                    dataType: "json",
                    type: "GET",
                    error: function () {
                        alert("An error occurred.");
                        modalshow = true;
                    },
                    success: function (data) {
                        var displayText = element.attr("displaytext");
                        var tableData = CreateTableViewCustom(data, "listpopup", true, changeId, elementId, valuetype, displayText);
                        $('#modalbox .msg-body').html(tableData);
                        var title = "";
                        if (element.attr("title")) {
                            title = element.attr("title");
                        }
                        $("#modalbox").dialog({
                            resizable: false,
                            width: 'auto',
                            modal: true,
                            title: title
                        });
                        $(".search-box").val("").focus();
                        modalshow = true;
                        $.unblockUI();
                    }
                });
            } else {
                e.preventDefault();
            }
        } else {
            return false;
        }
    }
});
function changeFunction(id, value, text, textchangeId) {
    $("#" + id).val(value);
    $("#" + textchangeId).val(text);
    $("#" + textchangeId).removeClass("error");
    //$(".close" + d).click();
    $("#modalbox").dialog("close");
    changeId = "";
    elementId = "";
    $("#" + textchangeId).focus();
    active = 0;
    modalShow();
}

function _generateGuid() {
    // Source: http://stackoverflow.com/questions/105034/how-to-create-a-guid-uuid-in-javascript/105074#105074
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

function changeFunctionDocNumber(id, value, text, textchangeId) {
    $("#" + id).val(value);
    $.getJSON("/Entry/GetDocNumber/" + value, function (data) {
        $("#" + textchangeId).val(data);
    });

    $("#" + textchangeId).removeClass("error");
    //$(".close" + d).click();
    $("#modalbox").dialog("close");
    changeId = "";
    elementId = "";
    modalShow();
    var detail = $("#" + textchangeId).attr("IsDetail");
    if (detail == 'True')
        if (typeof getDetail == 'function') {
            getDetail(value);
        }

}

jQuery.fn.getDetailCurrentBalance = function () {
    this.live("focus", function () {
        var ledgerId = $(this).closest("tr").find(".hdn_ledger_Id").val();
        if (ledgerId && ledgerId != '0') {
            $.getJSON("/Entry/GetGLCode?id=" + ledgerId, null, function (data) {
                $("#GlCode").val(data);

                $.getJSON("/Entry/GetCurrentBalance?glCode=" + ledgerId, null, function (bal) {
                    //                    bal = Math.round(bal * 100) / 100;
                    $("#GlDetailCurrBal").val(bal);
                });
            });
        }
    });
};

jQuery.fn.getCurrentBal = function () {
    var glCode = this.val();
    if (glCode != "") {
        $.getJSON("/Entry/GetCurrentBalance?glCode=" + glCode, null, function (data) {
            $("#CurrentBalance").val(data);
        });
    }
};

$(".input").live('keypress', function (e) {
    var ev = e || event;
    var keycode = ev.keyCode;
    if (keycode == 13) {
        e.preventDefault();
        if (($(this).val() == "" && $(this).attr("required") && $(this).hasClass("modallink")) || $(this).hasClass("hasDatepicker")) {
            return false;
        }
        var index = $(".input").index($(this));
        var elements = document.querySelectorAll(".input");
        if ((elements.length - 1) == index) {
            return false;
        }
        else {
            index = index + 1;
            CheckInputAttr(".input", index, elements.length);
        }
    }
});

function CheckInputAttr(attr, index, elementCount) {
    if ($(attr).get(index).hasAttribute("disabled") || $(".input").get(index).hasAttribute("readonly")) {
        index = index + 1;
        if (index != elementCount && index < elementCount) {
            CheckInputAttr(attr, index, elementCount);
        } else {
            return false;
        }
    } else {
        $(attr).get(index).focus();
        return false;
    }
}

function ajaxFormSubmit(url) {
    $.ajax({
        url: url,
        type: "GET",
        error: function () {
            alert("An error occurred.");
        },
        success: function (data) {
            return data;
        }
    });
}

function ajaxFormSubmit(formId) {
    var form = $("#" + formId);
    $.ajax({
        type: "POST",
        url: form.attr('action'),
        data: form.serialize(),
        success: function (data) {
            $("#" + changeId).val(data.id);
            $("#" + elementId).val(data.title);
            changeId = "";
            elementId = "";
            $(".close" + d).click();
        }
    });
};

var selectedRows = function () {
    var instance = this;
    if (!this.list) {
        this.list = [];
    }
    this.add = function (row) {
        list.push(row);
    };
    this.remove = function (row) {
        for (var i = 0; i < list.length; i++) {
            if (list[i] == row) {
                this.list.splice(i, 1);
            }
        }
    };
    this.deleteall = function () {
        this.list = [];
    };
    return (instance);
} ();

function selectable(id) {
    $(id).tableSelect({
        onClick: function (row) {
        },
        onDoubleClick: function (rows) {
            $.each(rows, function (i, row) {
                var name = $(row).attr("id");
                //alert(name);
                //var name = $(row).children('td').eq(0).html() + ' ' + $(row).children('td').eq(1).html();
                if ($(row).hasClass('disabled')) {
                    $(row).removeClass('disabled');
                    //selectedRows.remove(name);
                } else {
                    $(row).addClass('disabled');
                    selectedRows.add(name);
                }
            });
            updateList();
        },
        onChange: function (row) {
            //alert(row);
        }
    });
}

function DisplayReportAsOn(url) {
    if (url == null) {
        url = "/ReportLedger/LedgerReport/?ledgers";
    }
    var elements = document.getElementsByName("ledgers");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    if (arr == "") {
        alert("No any ledgers are selected");
    } else {
        var asOnDate = $("#AsOnDate").val();
        $.ajax({
            type: "POST",
            url: url + "=" + arr + "&&AsOnDate=" + asOnDate,
            success: function (data) {
                $("#report").html(data);
                $.unblockUI();
                // $("#msgClose").click();
                $('#modalReportForm').dialog("close");
            }
        });
    }
}


function DisplayReport(url) {
    if (url == null) {
        url = "/ReportLedger/LedgerReport/?ledgers";
    }
    var elements = document.getElementsByName("ledgers");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    if (arr == "") {
        alert("No any ledgers are selected");
    } else {
        var startDate = $("#StartDate").val();
        var endDate = $("#EndDate").val();
        var withvalue = $('#WithValue').attr('checked') ? true : false;
        var dateshow = $('#DateShow').attr('checked') ? true : false;
        var zerobalance = $('#ZeroBalance').attr('checked') ? true : false;
        $.ajax({
            type: "POST",
            url: url + "=" + arr + "&&StartDate=" + startDate + "&&EndDate=" + endDate,
            success: function (data) {
                $("#report").html(data);
                $.unblockUI();
                //   $("#msgClose").click();
                $('#modalReportForm').dialog("close");
            }
        });
    }
}


function DisplayReportLedgerandSubledger(url) {
    if (url == null) {
        url = "/ReportLedger/LedgerReport/?ledgers";
    }
    var elements = document.getElementsByName("ledgers");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    if (arr == "") {
        alert("No any ledgers are selected");
    } else {
        url = url + "=" + arr;
        ShowSubLedgerPickList(url);
        $("#modalbox-report").dialog("close");
        //DisplayReportSubLedger(url);
    }
}

function DisplayReportBOMRegister(url) {
    if (url == null) {
        url = "/Inventory/BOMRegister/?costcenters";
    }
    var elements = document.getElementsByName("costcenters");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    if (arr == "") {
        alert("No any Cost Centers are selected");
    } else {
        url = url + "=" + arr;
        ShowFinishedGoodsPickList(url);
        $("#modalbox-report").dialog("close");
        //DisplayReportSubLedger(url);
    }
}

function DisplayReportFinishedGoods(url) {
    if (url == null) {
        url = "/Inventory/BOMRegister/?costcenters=";
    }
    //"/ReportLedger/LedgerReport/?ledgers=&&StartDate=" + startDate + "&&EndDate=" + endDate + "&&subLedgers=&&includeSubLedger=true";
    var elements = document.getElementsByName("finishedgoods");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    $.ajax({
        type: "POST",
        url: url + "&&finishedgoods=" + arr,
        success: function (data) {
            $("#report").html(data);
            $.unblockUI();
            $('#modalReportForm').dialog("close");
        }
    });
}
//function DisplayReportSubLedger(url) {
//    if (url == null) {
//        url = "/ReportLedger/LedgerReport/?ledgers=";
//    }
//    //"/ReportLedger/LedgerReport/?ledgers=&&StartDate=" + startDate + "&&EndDate=" + endDate + "&&subLedgers=&&includeSubLedger=true";
//    var elements = document.getElementsByName("subledgers");
//    var len = elements.length;
//    var arr = [];
//    for (var i = 0; i < len; i++) {
//        arr.push(elements[i].value);
//    }
//    var startDate = $("#StartDate").val();
//    var endDate = $("#EndDate").val();
//    $.ajax({
//        type: "POST",
//        url: url + "&&StartDate=" + startDate + "&&EndDate=" + endDate + "&&subLedgers=" + arr + "&&includeSubLedger=true",
//        success: function (data) {
//            $("#report").html(data);
//            $('#modalReportForm').dialog("close");
//        }
//    });
//}


function DisplayReportSubLedger(url) {
    if (url == null) {
        url = "/ReportLedger/LedgerReport/?ledgers=";
    }
    //"/ReportLedger/LedgerReport/?ledgers=&&StartDate=" + startDate + "&&EndDate=" + endDate + "&&subLedgers=&&includeSubLedger=true";
    var elements = document.getElementsByName("subledgers");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    var startDate = $("#StartDate").val();
    var endDate = $("#EndDate").val();
    $.ajax({
        type: "POST",
        url: url + "&&StartDate=" + startDate + "&&EndDate=" + endDate + "&&subLedgers=" + arr + "&&includeSubLedger=true",
        success: function (data) {
            $("#report").html(data);
            $.unblockUI();
            $('#modalReportForm').dialog("close");
        }
    });
}

function DisplayReportPartyLedger(url) {

    var category = $('input:radio[name=Category]:checked').val();
    var elements = document.getElementsByName("ledgers");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    if (arr == "") {
        alert("No any ledgers are selected");
    } else {
        var startDate = $("#StartDate").val();
        var endDate = $("#EndDate").val();

        if (!url) {
            url = "/ARAP/PartyLedger/?ledgers=" + arr + "&&StartDate=" + startDate + "&&EndDate=" + endDate + "&&Category=" + category;
        }
        else {
            url+="="+arr;
        }
        $.ajax({
            type: "POST",
            url: url,
            success: function (data) {
                $("#report").html(data);
                $.unblockUI();
                //$("#msgClose").click();
                $('#modalReportForm').dialog("close");
            }
        });
    }
}
function DisplayReportPartyLedgerWithProdetails(url) {
    var category = $('input:radio[name=Category]:checked').val();
    var elements = document.getElementsByName("ledgers");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    if (arr == "") {
        alert("No any ledgers are selected");
    } else {
        var startDate = $("#StartDate").val();
        var endDate = $("#EndDate").val();
        $.ajax({
            type: "POST",
            url: "/ARAP/PartyLedger/?ledgers=" + arr + "&&StartDate=" + startDate + "&&EndDate=" + endDate + "&&Category=" + category + " &&includeProductDetails=true",
            success: function (data) {
                $("#report").html(data);
                $.unblockUI();
                //  $("#msgClose").click();
                $('#modalReportForm').dialog("close");
            }
        });
    }
}
function DisplaySummaryReportPartyLedger(url) {
    var category = $('input:radio[name=Category]:checked').val();
    var elements = document.getElementsByName("ledgers");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    if (arr == "") {
        alert("No any ledgers are selected");
    } else {
        var startDate = $("#StartDate").val();
        var endDate = $("#EndDate").val();
        $.ajax({
            type: "POST",
            url: "/ARAP/PartyLedger/?ledgers=" + arr + "&&StartDate=" + startDate + "&&EndDate=" + endDate + "&&Category=" + category + " &&includeSummary=true",
            success: function (data) {
                $("#report").html(data);
                $.unblockUI();
                //$("#msgClose").click();
                $('#modalReportForm').dialog("close");
            }
        });
    }
}

function DisplayReportPartyLedgerWithRemarks(url) {
    var category = $('input:radio[name=Category]:checked').val();
    var elements = document.getElementsByName("ledgers");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    if (arr == "") {
        alert("No any ledgers are selected");
    } else {
        var startDate = $("#StartDate").val();
        var endDate = $("#EndDate").val();

        $.ajax({
            type: "POST",
            url: "/ARAP/PartyLedger/?ledgers=" + arr + "&&StartDate=" + startDate + "&&EndDate=" + endDate + "&&Category=" + category + " &&includeRemarks=true",
            success: function (data) {
                $("#report").html(data);
                $.unblockUI();
                //  $("#msgClose").click();
                $('#modalReportForm').dialog("close");
            }
        });
    }
}
function DisplayReportPartyLedgerandSubledger(url) {
    if (url == null) {
        url = "/ReportLedger/LedgerReport/?ledgers";
    }
    var category = $('input:radio[name=Category]:checked').val();
    var elements = document.getElementsByName("ledgers");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    if (arr == "") {
        alert("No any ledgers are selected");
    } else {
        url = url + "=" + arr;
        ShowSubLedgerPickList(url);
        $("#modalbox-report").dialog("close");
        //DisplayReportSubLedger(url);
    }
}
function DisplayReportPartySubLedger(url) {
    if (url == null) {
        url = "/ReportLedger/LedgerReport/?ledgers=";
    }
    var category = $('input:radio[name=Category]:checked').val();
    //"/ReportLedger/LedgerReport/?ledgers=&&StartDate=" + startDate + "&&EndDate=" + endDate + "&&subLedgers=&&includeSubLedger=true";
    var elements = document.getElementsByName("subledgers");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    var startDate = $("#StartDate").val();
    var endDate = $("#EndDate").val();
    $.ajax({
        type: "POST",
        url: url + "&&StartDate=" + startDate + "&&EndDate=" + endDate + "&&Category=" + category + "&&subLedgers=" + arr + "&&includeSubLedger=true",
        success: function (data) {
            $("#report").html(data);
            $.unblockUI();
            $('#modalReportForm').dialog("close");
        }
    });
}

function DisplayCustomerLedger() {

    var elements = document.getElementsByName("ledgers");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    if (arr == "") {
        alert("No any ledgers are selected");
    } else {
        var startDate = $("#AsOnDate").val();


        $.ajax({
            type: "POST",
            url: "/ARAP/OutStandingCustomer/?ledgers=" + arr + "&&AsOnDate=" + startDate,
            success: function (data) {
                $("#report").html(data);
                $.unblockUI();
                //$("#msgClose").click();
                $('#modalReportForm').dialog("close");
            }
        });
    }
}

function DisplayLedgerList() {

    var elements = document.getElementsByName("ledgers");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    if (arr == "") {
        alert("No any ledgers are selected");
    } else {
        var groupBy = $("#GroupBy").val();
        var reportType = $('#ReportType').val();
        var inclideOpening = $('#InclideOpening').attr('checked') ? true : false;

        $.ajax({
            type: "POST",
            url: "/ReportLedger/LedgerListReport/?ledgersList=" + arr + "&&groupby=" + groupBy + "&&reportType=" + reportType + "&&inclideOpening=" + inclideOpening,
            success: function (data) {
                $("#report").html(data);
                $.unblockUI();
                //$("#msgClose").click();
                $('#modalReportForm').dialog("close");
            }
        });
    }
    }


function DisplaySupplierLedger() {

    var elements = document.getElementsByName("ledgers");
    var len = elements.length;
    var arr = [];
    for (var i = 0; i < len; i++) {
        arr.push(elements[i].value);
    }
    if (arr == "") {
        alert("No any ledgers are selected");
    } else {
        var startDate = $("#AsOnDate").val();


        $.ajax({
            type: "POST",
            url: "/ARAP/OutStandingSupplier/?ledgers=" + arr + "&&AsOnDate=" + startDate,
            success: function (data) {
                $("#report").html(data);
                $.unblockUI();
                //    $("#msgClose").click();
                $('#modalReportForm').dialog("close");
            }
        });
    }


}


$(".modaldoclink").live('keydown', function (e) {
    if (modalshow == true) {
        var element = $(this);
        elementId = $(this).attr("id");
        changeId = $(this).attr("valueId");
        var ev = e || event;
        var keycode = ev.keyCode;

        if (keycode == "9") {
            if (element.val() != "") {
                return true;
            }
        }
        if (keycode == "13") {
            return true;
        }
        if (keycode == 46) {
            element.val("");
            var valueId = $(this).attr("valueid");
            $("#" + valueId).val("");
            return false;
        }
        if (keycode == 32) {
            if ($(this).val().trim() == "") {
                e.preventDefault();
                modalshow = false;
                var link = "/entry/GetDocumentNumber";
                var module = $(this).attr('module');

                //changeId = $(this).attr("valueId");
                $.ajax({
                    url: link,
                    data: { module: module },
                    dataType: "json",
                    type: "GET",
                    error: function () {
                        alert("An error occurred.");
                    },
                    success: function (data) {
                        var tableData = CreateTableViewDocNumber(data, "listpopup", true, changeId, elementId);
                        $('#modalbox .msg-body').html(tableData);
                        $("#modalbox").dialog({
                            resizable: false,
                            width: 'auto',
                            modal: true,
                            zIndex: 3999
                        });
                        modalshow = true;
                    }
                });
            }
        }
    } else {
        return false;
    }
});

//------------------------------------------------
$(".modalNarrationlink").live('keydown', function (e) {
    if (modalshow == true) {
        var element = $(this);
        elementId = $(this).attr("id");
        changeId = $(this).attr("valueId");
        var ev = e || event;
        var keycode = ev.keyCode;

        if (keycode == "9") {
            if (element.val() != "") {
                return true;
            }
        }
        if (keycode == "13") {
            return true;
        }
        if (keycode == 46) {
            element.val("");
            var valueId = $(this).attr("valueid");
            $("#" + valueId).val("");
            return false;
        }
        if (keycode == 79) {
            if (e.altKey) {
                modalshow = false;
                var link = "/master/GetNarrationList";
                var module = $(this).attr('module');
                //changeId = $(this).attr("valueId");
                $.ajax({
                    url: link,
                    data: { module: module },
                    dataType: "json",
                    type: "GET",
                    error: function () {
                        alert("An error occurred.");
                    },
                    success: function (data) {
                        var tableData = CreateTableViewDocNumber(data, "listpopup", true, changeId, elementId);
                        $('#modalbox .msg-body').html(tableData);
                        $("#modalbox").dialog({
                            resizable: false,
                            width: 'auto',
                            modal: true,
                            zIndex: 3999
                        });
                        modalshow = true;
                    }
                });
            }
        }
    } else {
        return false;
    }


});



//------------------------------------------------


$(".billingterm_rate").live('keydown', function (e) {
    var ev = e || event;
    var keycode = ev.keyCode;
    //e.preventDefault();
    var totalAmt = parseFloat(0);
    var basicAmt = parseFloat(0);
    var amt = parseFloat(0);

    if (isProductWise) {
        basicAmt = RoundTwoDecimalPlace(parseFloat(selectedElement.find(".basicAmt").val()));

        //basicAmt = Math.round(basicAmt * 100) / 100;
    } else {
        basicAmt = RoundTwoDecimalPlace(parseFloat($(".MasterBasicAmt").val()));
    }
    var masterBasicAmt = basicAmt;
    // var currRate = $(this).val();
    // if (currRate) {

    // var currAmount = parseFloat(currRate * basicAmt);
    // $(this).closest("tr").find(".billingterm_amount").val(currAmount);
    // } else {
    // $(this).closest("tr").find(".billingterm_amount").val(0);
    // }
    var termId = $(this).closest("tr").find(".billingterm_Id").val();

    //alert(keycode);
    if (keycode == 13 || keycode == 9) {

        var parent = $(this).closest("tbody");
        (parent.find("tr")).each(function () {
            if (($(this).find(".billingterm_rate").val() != '')) {
                var id = $(this).closest("tr").find(".billingterm_Id").val();
                var amt = $(this).find(".billingterm_amount").val();
                if (termId == $(this).closest("tr").find(".billingterm_Id").val() || ($(this).find(".billingterm_amount").val() != 0 && $(this).find(".billingterm_amount").val() != '')) {
                    var rate = parseFloat($(this).find(".billingterm_rate").val());
                    if (isNaN(rate)) {
                        rate = 0;
                    }
                    var termbasis = $(this).find(".billingterm_termbasis").val();
                    if (termbasis) {
                        var arrFormula = termbasis.split(',');
                        var tempBasicAmt = parseFloat(0);
                        arrFormula.forEach(function (item, index) {
                            if (item == 'b') {
                                tempBasicAmt += masterBasicAmt;
                            }
                            else {
                                (parent.find("tr")).each(function () {
                                    if (($(this).find(".billingterm_rate").val() != '')) {
                                        if (termId == $(this).closest("tr").find(".billingterm_Id").val() || ($(this).find(".billingterm_amount").val() != 0 && $(this).find(".billingterm_amount").val() != '')) {
                                            var code = $(this).find(".billingterm_code").val().trim();
                                            if (code == item) {
                                                var termAmt = parseFloat($(this).find(".billingterm_amount").val());
                                                if (isNaN(termAmt)) {
                                                    termAmt = 0;
                                                }
                                                //billingterm_amount
                                                var sign = $(this).find(".billingterm_Sign").text().trim();
                                                if (sign == "+") {
                                                    tempBasicAmt += termAmt;
                                                } else {
                                                    tempBasicAmt -= termAmt;
                                                }

                                            }
                                        }
                                    }
                                });
                                /do something/
                            }
                        });
                        amt = RoundTwoDecimalPlace(rate / 100.0 * tempBasicAmt);
                    }
                    else {
                        amt = RoundTwoDecimalPlace(rate / 100.0 * basicAmt);
                    }
                    //amt = Math.round(amt * 100) / 100;
                    if ($(this).find(".billingterm_Sign").html() == "-") {
                        basicAmt = parseFloat(basicAmt) - parseFloat(amt);
                    }
                    if ($(this).find(".billingterm_Sign").html() == "+") {
                        basicAmt = parseFloat(basicAmt) + parseFloat(amt);
                    }
                    //alert("ater calc-"+basicAmt);
                    basicAmt = Math.abs(basicAmt);
                    var amtElement = $(this).find(".billingterm_amount");
                    amtElement.val(amt);

                    //alert(rate);
                    //alert(basicAmt);


                }
            }
        });
        $(this).closest("tr").find(".billingterm_amount").focus();
    }

});
$(".billingterm_rate").live("focus", function () {
    $(this).select();
});

$(".detail-entry").live("mouseover", function () {
    if ($(this).hasClass('deletable')) {
        $(this).find('img').css('visibility', 'visible');
    }
});

$(".detail-entry").live("mouseout", function () {
    $(this).find('img').css('visibility', 'hidden');
});

$.expr[':'].containsIgnoreCase = function (n, i, m) {
    return jQuery(n).text().toUpperCase().indexOf(m[3].toUpperCase()) >= 0;
};


$(".search-box").live('keyup', function (e) {
    $(this).closest(".ui-dialog").find(".msg-body tbody").find("tr").hide();
    var data = this.value.split(" ");
    var jo = $(this).closest(".ui-dialog").find(".msg-body tbody").find("tr");
    $.each(data, function (i, v) {
        //Use the new containsIgnoreCase function instead
        jo = jo.filter("*:containsIgnoreCase('" + v + "')");
    });

    jo.show();

}).focus(function () {
    this.value = "";
    $(this).css({ "color": "black" });
    $(this).unbind('focus');
}).css({ "color": "#C0C0C0" });

$('.onlineSave').live('click', function () {
    var form = $(this).closest("form");
    var element = $(this);
    $.ajax({
        type: "POST",
        url: form.attr('action'),
        data: form.serialize(),
        success: function (data) {
            if (data.msg != "false") {
                var textId = element.attr("textid");
                var valueId = element.attr("valueid");
                $("#" + valueId).val(data.id);
                $("#" + textId).val(data.title);
                form.dialog("close");
            }
        }
    });
});


function getFormRequest(url, element, title) {
    $.ajax({
        url: url,
        context: document.body,
        success: function (data) {
            $(element + ' .block').html(data);
            $(this).addClass("done");
            $(element).dialog({
                modal: true,
                width: 'auto',
                title: title,
                buttons: {
                    "Save": function () {
                        SaveForm();
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });

        },
        error: function (err) {
            alert("error");
        }
    });
}

function getEditFormRequest(url, element, title) {
    $.ajax({
        url: url,
        context: document.body,
        success: function (data) {
            $(element + ' .block').html(data);
            $(this).addClass("done");
            $(element).dialog({
                modal: true,
                width: 'auto',
                title: title,
                buttons: {
                    "Save": function () {
                        SaveEditForm();
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });

        },
        error: function (err) {
            alert("error");
        }
    });
}

$(".last-col").live('keydown', function (e) {
    var tableClass = $(this).attr("tableid");
    var element = $(this);
    var ev = e || event;
    var keycode = ev.keyCode;
    var disable = 0;
    if (keycode == 13) {
        var closestTr = element.closest("tr");
        closestTr.find("input[type='text']").each(function () {
            if ($(this).val() == "" && $(this).attr("isrequired")) {
                disable = 1;
            }
        });
        if (disable == 0) {
            var link = $(this).attr("href");
            $.ajax({
                url: link,
                type: "GET",
                error: function () {
                    alert("An error occurred.");
                },
                success: function (data) {
                    if (closestTr.is(":last-child")) {
                        $('#' + tableClass + ' tr:last').addClass("deletable");
                        var id = $('#' + tableClass + ' tr:last').attr("id");
                        $('#' + tableClass + ' tr:last').after(data);
                        var arrString = id.split("_");
                        var index = parseInt(arrString[1]) + 1;
                        $(".first-col:last").closest("tr").attr("id", "tr_" + index);
                    }
                    closestTr.next().find('.first-col').focus();

                    $('input[type="text"]').focus(function () {
                        if ($(this).attr("entermsg")) {
                            $(".enter-msg").html($(this).attr("entermsg"));
                        } else {
                            $(".enter-msg").html("");
                        }
                    });
                }
            });
            closestTr.next().find('.first-col').focus();
        }
        else {
            closestTr.find("input[type='text']").each(function () {
                if ($(this).val() == "" && $(this).attr("isrequired")) {
                    $(this).addClass("error");
                    $(this).focus();
                }
            });
        }
        return false;
    }
    return true;
});


$(".last-col-custom").live('keydown', function (e) {
    var tableClass = $(this).attr("tableid");
    var element = $(this);
    var ev = e || event;
    var keycode = ev.keyCode;
    var disable = 0;
    if (keycode == 13) {
        var closestTr = element.closest("tr");
        closestTr.find("input[type='text']").each(function () {
            if ($(this).val() == "" && $(this).attr("isrequired")) {
                disable = 1;
            }
        });
        if (disable == 0) {
            var link = $(this).attr("href");
            $.ajax({
                url: link,
                type: "GET",
                error: function () {
                    alert("An error occurred.");
                },
                success: function (data) {
                    if (closestTr.is(":last-child")) {
                        $('#' + tableClass + ' tr:last').addClass("deletable");
                        var id = $('#' + tableClass + ' tr:last').attr("id");
                        $('#' + tableClass + ' tr:last').after(data);
                        var arrString = id.split("_");
                        var index = parseInt(arrString[1]) + 1;
                        $(".first-col:last").closest("tr").attr("id", "tr_" + index);
                        if (typeof NewRowDetailList == 'function') {
                            NewRowDetailList(element, index);
                        }
                    }
                    closestTr.next().find('.first-col').focus();
                    $('input[type="text"]').focus(function () {
                        if ($(this).attr("entermsg")) {
                            $(".enter-msg").html($(this).attr("entermsg"));
                        } else {
                            $(".enter-msg").html("");
                        }
                    });
                }
            });
            closestTr.next().find('.first-col').focus();
        }
        else {
            closestTr.find("input[type='text']").each(function () {
                if ($(this).val() == "" && $(this).attr("isrequired")) {
                    $(this).addClass("error");
                    $(this).focus();
                }
            });
        }
        return false;
    }
    return true;
});


$(".last-col-modified").live('keydown', function (e) {
    var tableClass = $(this).attr("tableid");
    var element = $(this);

    var ev = e || event;
    var keycode = ev.keyCode;
    var disable = 0;
    if (keycode == 13) {
        var closestTr = element.closest("tr");
        closestTr.find("input[type='text']").each(function () {
            if (element.val() == "" && element.attr("isrequired")) {
                disable = 1;
            }
        });
        if (disable == 0) {
            var link = element.attr("href");

            //alert(valueid);
            var valueId = '';
            if (element.attr("valueId")) {
                valueId = closestTr.find(".first-col").attr("valueid");
            }
            else {
                valueId = closestTr.find(".first-col").closest("td").find(".value").attr("id");
            }

            var value = $("#" + valueId).val();
            if (link) {
                $.ajax({
                    url: link,
                    type: "GET",
                    error: function () {
                        alert("An error occurred.");
                    },
                    success: function (data) {
                        var trIndex = parseInt(closestTr[0].sectionRowIndex) + 1;
                        //alert(trIndex);
                        var trCount = closestTr.closest("table").find("tr").length;

                        //closestTr.is(":last-child") && 
                        if (trIndex == trCount) {
                            //$('.ui-dialog #' + tableClass + ' tr:last').addClass("deletable");
                            var id = $('.ui-dialog #' + tableClass + ' tr:last').attr("id");
                            element.closest("form").find('#' + tableClass + ' tr:last').after(data);
                            //var index = parseInt(trIndex + 1);
                            trIndex = parseInt(trIndex) - 1;
                            $(".first-col:last").closest("tr").attr({ id: "tr_" + trIndex });

                            if (typeof NewRowDetailList == 'function') {
                                NewRowDetailList(element, trIndex);
                            }
                        }
                        if (value) {
                            closestTr.next().find('.first-col').focus();
                        } else {

                            closestTr.find('.first-col').focus();
                            closestTr.find('.first-col').addClass('error');
                        }


                        $('input[type="text"]').focus(function () {
                            if (element.attr("entermsg")) {
                                $(".enter-msg").html($(this).attr("entermsg"));
                            } else {
                                $(".enter-msg").html("");
                            }
                        });
                        trCount = closestTr.closest("table").find("tr").length;
                        if (trCount > 2) {
                            //$('.ui-dialog #' + tableClass + ' tr:last').addClass("deletable");
                            element.closest("tr").addClass("deletable");
                        }
                    }
                });
            } else {
                // alert(element.attr("id"));
                var templateId = element.attr("templateid");
                if (templateId) {
                    var trIndex = parseInt(closestTr[0].sectionRowIndex) + 1;
                    //alert(trIndex);
                    var trCount = closestTr.closest("tbody").find("tr").length;
                    //closestTr.is(":last-child") && 
                    //alert(trIndex+","+ trCount);

                    if (trIndex == trCount) {
                        //$('.ui-dialog #' + tableClass + ' tr:last').addClass("deletable");
                        var guid = _generateGuid();
                        element.closest("form").find('#' + tableClass + ' tr:last').after($("#" + templateId).tmpl({ index: guid }));
                        trIndex = parseInt(trIndex) - 1;
                        $(".first-col:last").closest("tr").attr({ id: "tr_" + trIndex });
                        if (typeof NewRowDetailList == 'function') {
                            NewRowDetailList(element, trIndex);
                        }

                    }
                    if (value != 0 && value != '') {
                        closestTr.next().find('.first-col').focus();
                    } else {

                        closestTr.find('.first-col').focus();
                        closestTr.find('.first-col').addClass('error');
                    }

                    $('input[type="text"]').focus(function () {
                        if (element.attr("entermsg")) {
                            $(".enter-msg").html(element.attr("entermsg"));
                        } else {
                            $(".enter-msg").html("");
                        }
                    });
                    trCount = closestTr.closest("table").find("tr").length;
                    if (trCount > 2) {
                        //$('.ui-dialog #' + tableClass + ' tr:last').addClass("deletable");
                        element.closest("tr").addClass("deletable");
                    }
                }

            }
            //var formId = element.closest("form").attr("id");

            //$.validator.unobtrusive.parse("#bom-form");

            closestTr.next().find('.first-col').focus();
        }
        else {
            closestTr.find("input[type='text']").each(function () {
                if ($(this).val() == "" && $(this).attr("isrequired")) {
                    $(this).addClass("error");
                    $(this).focus();
                }
            });
        }


        return false;
    }
    return true;
});


function RoundTwoDecimalPlace(value) {
    value = Math.round(value * 100) / 100;
    return value;
}

function checkValidationError() {
    var test = document.querySelectorAll('.error');
    if (test.length > 0) {
        $.unblockUI();
    }
}


function GrowlMsg(msg) {
    $.blockUI({
        message: '<h4 style="color: white; padding: 5px 5px 5px 75px; text-align: left">' + msg + '</h4>',
        fadeIn: 700,
        fadeOut: 700,
        timeout: 2000,
        showOverlay: false,
        centerY: false,
        css: {
            width: '350px',
            top: '10px',
            left: '',
            right: '10px',
            border: 'none',
            padding: '5px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .6,
            color: '#fff'
        }
    });
}

function BeginRequest() {
    $.blockUI({
        message: 'Please Wait..',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#fff',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            color: '#000'
        }
    });
}

$(".picklist").live("click", function () {
    //id, value, text, textchangeId
    picklistelement = $(this);
    var changeId = $(this).attr("data-changeid");
    var value = $(this).attr("data-value");
    var text = "";
    if ($(this).closest("tr").find(".displaytext").length > 0) {
        text = $(this).closest("tr").find(".displaytext").text();
    } else {
        text = $(this).text();
    }
    var textchangeid = $(this).attr("data-textchangeid");
    var type = $(this).attr("value-type");
    changeFunction(changeId, value, text, textchangeid);
    if (typeof PicklistCustomFunction == 'function') {
        PicklistCustomFunction(value, type);
    }
});


$(".picklist_basic").live("click", function () {
    var changeId = $(this).attr("data-changeid");
    var value = $(this).attr("data-value");
    var text = $(this).attr("data-text");
    var textchangeid = $(this).attr("data-textchangeid");
    changeFunction(changeId, value, text, textchangeid);
});


function resetValidation() {
    //Removes validation from input-fields
    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    //Removes validation summary 
    $('.validation-summary-errors').addClass('validation-summary-valid');
    $('.validation-summary-errors').removeClass('validation-summary-errors');

}
function IsNumberKey(e) {
    var ev = e || event;
    var keycode = ev.keyCode;
    //alert(keycode);
    //48-57=0-9 , 96-105=09 , 8=backspace,46=delete,16=delete
    if ((keycode > 47 && keycode < 58) || (keycode > 95 && keycode < 106) || keycode === 46 || keycode === 16 || keycode == 8 || keycode === 9 || keycode === 13) {
        //alert(1);
        return true;
    }
    else {
        return false;
    }
}


$(".numberonly").live("keydown", function (e) {
    var ev = e || event;
    var keycode = ev.keyCode;
    //alert(keycode);
    //48-57=0-9 , 96-105=09 , 8=backspace,46=delete,16=delete
    if ((keycode > 47 && keycode < 58) || (keycode > 95 && keycode < 106) || keycode === 110 || keycode === 190 || keycode === 46 || keycode === 16 || keycode == 8 || keycode === 9 || keycode === 13) {
        //alert(1);
        return true;
    }
    else {
        e.preventDefault();
        return false;
    }
});

$(".numberonly").live("focus", function (e) {
    $(this).select();
});

function isDate(value, sepVal, dayIdx, monthIdx, yearIdx) {
    try {
        //Change the below values to determine which format of date you wish to check. It is set to dd/mm/yyyy by default.
        var DayIndex = dayIdx !== undefined ? dayIdx : 0;
        var MonthIndex = monthIdx !== undefined ? monthIdx : 0;
        var YearIndex = yearIdx !== undefined ? yearIdx : 0;

        value = value.replace(/-/g, "/").replace(/\./g, "/");
        var SplitValue = value.split(sepVal || "/");
        var OK = true;
        if (!(SplitValue[DayIndex].length == 1 || SplitValue[DayIndex].length == 2)) {
            OK = false;
        }
        if (OK && !(SplitValue[MonthIndex].length == 1 || SplitValue[MonthIndex].length == 2)) {
            OK = false;
        }
        if (OK && SplitValue[YearIndex].length != 4) {
            OK = false;
        }
        if (OK) {
            var Day = parseInt(SplitValue[DayIndex], 10);
            var Month = parseInt(SplitValue[MonthIndex], 10);
            var Year = parseInt(SplitValue[YearIndex], 10);

            //            if (OK = ((Year > 1900) && (Year < new Date().getFullYear()))) {
            if (OK = ((Year > 1900))) {
                if (OK = (Month <= 12 && Month > 0)) {

                    var LeapYear = (((Year % 4) == 0) && ((Year % 100) != 0) || ((Year % 400) == 0));

                    if (OK = Day > 0) {
                        if (Month == 2) {
                            OK = LeapYear ? Day <= 29 : Day <= 28;
                        }
                        else {
                            if ((Month == 4) || (Month == 6) || (Month == 9) || (Month == 11)) {
                                OK = Day <= 30;
                            }
                            else {
                                OK = Day <= 31;
                            }
                        }
                    }
                }
            }
        }
        return OK;
    }
    catch (e) {
        return false;
    }
}


function Begin() {
    $.blockUI({
        message: 'Please Wait..',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#fff',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            color: '#000',
            baseZ: '1051'

        }
    });
}


$(".qty").live("keyup", function () {
    if ($(this).attr("enablestockaction")) {
        var stockqty = $(this).closest("tr").find(".StockQty").val();
        if (stockqty) {
            stockqty = parseFloat(stockqty);
        }
        var qty = $(this).val();
        if (qty) {
            qty = parseFloat(qty);
        }
        if (typeof negativeStockAction !== 'undefined') {
            if (negativeStockAction == '1') {
                if (stockqty && qty) {
                    if (stockqty < qty) {
                        qty = qty.toString().slice(0, -1);
                        $(this).val(qty);
                        alert('Quantity cannot be greater than stock Quantity');
                    }
                }
            }
            else if (negativeStockAction == '2') {
                if (stockqty && qty) {
                    if (stockqty < qty) {
                        alert('The Quantity you entered is greater than stock Quantity');
                    }
                }
            } else if (negativeStockAction == '3') {
                //Ignore
            }
        }

    }

});

Array.prototype.remove = function (from, to) {
    var rest = this.slice((to || from) + 1 || this.length);
    this.length = from < 0 ? this.length + from : from;
    return this.push.apply(this, rest);
};


Date.prototype.addDays = function(num) {
    var value = this.valueOf();
    value += 86400000 * num;
    return new Date(value);
};

Date.prototype.addSeconds = function(num) {
    var value = this.valueOf();
    value += 1000 * num;
    return new Date(value);
};

Date.prototype.addMinutes = function(num) {
    var value = this.valueOf();
    value += 60000 * num;
    return new Date(value);
};

Date.prototype.addHours = function(num) {
    var value = this.valueOf();
    value += 3600000 * num;
    return new Date(value);
};

Date.prototype.addMonths = function(num) {
    var value = new Date(this.valueOf());

    var mo = this.getMonth();
    var yr = this.getYear();

    mo = (mo + num) % 12;
    if (0 > mo) {
        yr += (this.getMonth() + num - mo - 12) / 12;
        mo += 12;
    } else
        yr += ((this.getMonth() + num - mo) / 12);

    value.setMonth(mo);
    value.setYear(yr);
    return value;
};

function DisplayBillTerm(e,element) {
    var ev = e || event;
    var keycode = ev.keyCode;
    var closestTr = element.closest("tr");
    if (keycode == 13 || keycode == 9) {
        if (closestTr.find(".productId").val() != "" && closestTr.find(".productId").val() != 0 && closestTr.find(".qty").val() != "" && closestTr.find(".rate").val() != "") {
            var guid = closestTr.find(".detailGuid").val();
            var qty = parseFloat(closestTr.find('.qty').val());
            if (isNaN(qty)) {
                qty = 0;
            }
            var rate = parseFloat(closestTr.find('.rate').val());
            if (isNaN(rate)) {
                rate = 0;
            }
            var basicAmt = qty * rate;
            $((closestTr.find(".basicAmt").val(basicAmt)));
            var link = element.attr("href");
            var id = $('#table-detail tr:last').attr("id");
            var arrString = id.split("_");
            var index = parseInt(arrString[1]) + 1;
            $.ajax({
                url: link,
                type: "GET",
                data: {
                    index: index
                },
                error: function () {
                    alert("An error occurred.");
                },
                success: function (data) {
                    if (closestTr.is(":last-child")) {
                        $('#table-detail tr:last').addClass("deletable");
                        $('#table-detail tr:last').after(data);
                        $(".text_product:last").attr("id", "product_" + index);
                        $(".text_product:last").closest("tr").attr("id", "tr_" + index);
                        $(".indexId:last").val(index);
                        $(".text_product").attr("valueId", $(".productId:last").attr("id"));
                    }
                    selectedElement = closestTr;
                    if ($(element).hasClass("rate-bill-term")) {
                        isProductWise = true;
                        var arrNewBatchList = [];
                        if (element.closest("td").find(".product_wise_billterms").find(".single-list").length > 0) {
                            element.closest("td").find(".product_wise_billterms").find(".single-list").each(function (item, index3) {
                                var Id = $(this).find(".billing_term_value").val();
                                var Amount = $(this).find(".amount").val();
                                if (typeof Amount === "undefined") {
                                    Amount = "0";
                                }
                                var Basis = $(this).find(".Basis").val();
                                var Description = $(this).find(".Description").val();
                                var Rate = $(this).find(".Rate").val();
                                if (typeof Rate === "undefined") {
                                    Rate = "0";
                                }
                                var Sign = $(this).find(".Sign").val();
                                var BillingTermIndex = $(this).find(".BillingTermIndex").val();
                                var IsProductWise = $(this).find(".IsProductWise").val();
                                var ParentGuid = $(this).find(".ParentGuid").val();
                                var DisplayOrder = $(this).find(".DisplayOrder").val();
                                if (typeof DisplayOrder === "undefined") {
                                    DisplayOrder = "0";
                                }
                                arrNewBatchList.push({
                                    Id: Id,
                                    Rate: Rate,
                                    Amount: Amount,
                                    DisplayOrder: DisplayOrder,
                                    IsProductWise: IsProductWise,
                                    Description: Description,
                                    Sign: Sign,
                                    BillingTermIndex: BillingTermIndex,
                                    Basis: Basis,
                                    ParentGuid: ParentGuid
                                });
                            });
                        } else {
                            arrNewBatchList = productWiseBatchList;
                        }
                        if (productWiseBatchList.length != 0) {
                            $('#modalbox-billingterm-productwise .msg-body').find('tbody').html('');
                            arrNewBatchList.forEach(function (item, index4) {
                                if ($('#modalbox-billingterm-productwise .msg-body').find('tbody').html() == '') {
                                    $('#modalbox-billingterm-productwise .msg-body tbody').html($("#BillTermPopupTemplate").tmpl({
                                        Id: item.Id,
                                        Amount: item.Amount,
                                        Basis: item.Basis,
                                        Description: item.Description,
                                        Rate: item.Rate,
                                        Sign: item.Sign
                                    }));
                                } else {
                                    $('#modalbox-billingterm-productwise .msg-body tbody tr:last').after($("#BillTermPopupTemplate").tmpl({
                                        Id: item.Id,
                                        Amount: item.Amount,
                                        Basis: item.Basis,
                                        Description: item.Description,
                                        Rate: item.Rate,
                                        Sign: item.Sign
                                    }));
                                }
                            });
                            $("#modalbox-billingterm-productwise").dialog({
                                modal: true,
                                width: 'auto',
                                buttons: {
                                    "Ok": function () {
                                        selectedElement.find(".billing_term").remove();
                                        var termAmt = parseFloat(0);
                                        closestTr.find(".product_wise_billterms").html("");
                                        $(this).closest("#modalbox-billingterm-productwise").find(".billingterm_amount").each(function () {
                                            var value = parseFloat(0);
                                            var basis = $(this).closest("tr").find(".billingterm_Basis").html();
                                            var sign = $(this).closest("tr").find(".billingterm_Sign").html();
                                            var rate = $(this).closest("tr").find(".billingterm_rate").val();
                                            if (typeof rate == "undefined") {
                                                rate = "0";
                                            }
                                            var desc = $(this).closest("tr").find(".billingterm_Description").html();
                                            var id = $(this).closest("tr").find(".billingterm_Id").val();
                                            var displayOrder = $(this).closest("tr").find(".billingterm_displayorder").val();
                                            if (typeof displayOrder == "undefined" || displayOrder=='') {
                                                displayOrder = "0";
                                            }

                                            var amount = $(this).closest("tr").find(".billingterm_amount").val();
                                            if (typeof amount == "undefined") {
                                                amount = "0";
                                            }
                                            value = parseFloat($(this).val());
                                            if (sign == "-") {
                                                termAmt = RoundTwoDecimalPlace(termAmt - value);
                                            } else {
                                                termAmt = RoundTwoDecimalPlace(termAmt + value);
                                            }
                                            var basicAmt = selectedElement.find(".basicAmt").val();
                                            selectedElement.find(".termAmt").val(termAmt);
                                            var netAmt = parseFloat(basicAmt) + parseFloat(termAmt);
                                            selectedElement.find(".netAmt").val(netAmt);
                                            var index = selectedElement.find(".indexId").val();
                                            var batch = new BillingTermViewModel(id, rate, amount, displayOrder, 'true', desc, sign, basis, guid);
                                            var result = $.queryable(arrBillingTerms).where('a=>a.Guid=="' + guid + '"');
                                            result = result.where('a=>a.Id=="' + id + '"');
                                            if (result.length == 0) {
                                                arrBillingTerms.push(batch);
                                            } else {
                                                var arrLength = arrBillingTerms.length;
                                                if (arrLength > 0) {
                                                    breakloop = 0;
                                                    arrBillingTerms.forEach(function (item, indexa) {
                                                        var result = $.queryable(arrBillingTerms).where('a=>a.Guid=="' + guid + '"');
                                                        result = result.where('a=>a.Id=="' + id + '"');
                                                        if (result.length != 0 && breakloop == 0) {
                                                            breakloop = 1;
                                                            arrBillingTerms.splice(indexa, 1);
                                                            arrBillingTerms.push(batch);
                                                        }
                                                    });
                                                }
                                            }
                                            if (element.closest("td").find('.product_wise_billterms').html() != '') {
                                                var rowGuid = _generateGuid();
                                                element.closest("td").find('.product_wise_billterms .single-list:last').after($("#ProductBillTermTemplate").tmpl({
                                                    index: rowGuid,
                                                    Id: id,
                                                    Amount: amount,
                                                    Basis: basis,
                                                    Description: desc,
                                                    Rate: rate,
                                                    Sign: sign,
                                                    BillingTermIndex: index,
                                                    IsProductWise: 'true',
                                                    ParentGuid: guid,
                                                    DisplayOrder: displayOrder
                                                }));
                                            } else {
                                                var rowGuid = _generateGuid();
                                                element.closest("td").find('.product_wise_billterms').html($("#ProductBillTermTemplate").tmpl({
                                                    index: rowGuid,
                                                    Id: id,
                                                    Amount: amount,
                                                    Basis: basis,
                                                    Description: desc,
                                                    Rate: rate,
                                                    Sign: sign,
                                                    BillingTermIndex: index,
                                                    IsProductWise: 'true',
                                                    ParentGuid: guid,
                                                    DisplayOrder: displayOrder
                                                }));
                                            }
                                        });
                                        getTotalQty();
                                        getTotalAmt();
                                        $('input[type="text"]').focus(function () {
                                            if ($(this).attr("entermsg")) {
                                                $(".enter-msg").html($(this).attr("entermsg"));
                                            } else {
                                                $(".enter-msg").html("");
                                            }
                                            $(".receipt-amt").blur(function () {
                                                if ($(this).val() != "") {
                                                    selectedElement.find(".payment-amt").attr("disabled", "disabled");
                                                } else {
                                                    selectedElement.find(".payment-amt").removeAttr("disabled");
                                                }
                                            });

                                            $(".payment-amt").blur(function () {
                                                if ($(this).val() != "") {
                                                    selectedElement.find(".receipt-amt").attr("disabled", "disabled");
                                                } else {
                                                    selectedElement.find(".receipt-amt").removeAttr("disabled");
                                                }
                                            });
                                        });
                                        $('#modalbox-billingterm-productwise .msg-body').find('tbody').html('');
                                        $(this).dialog("close");
                                    },
                                    Cancel: function () {
                                        $(this).dialog("close");
                                    }
                                }
                            });
                            $('#modalbox-billingterm-productwise').find(".billingterm_rate").first().focus().select();
                            return false;
                        }
                        return false;
                    } else {
                        var basicAmt = selectedElement.find(".basicAmt").val();
                        selectedElement.find(".termAmt").val(0);
                        selectedElement.find(".netAmt").val(basicAmt);
                        getTotalQty();
                        getTotalAmt();
                    }
                    closestTr.next().find('.text_product').focus();
                    isProductWise = false;
                    getTotalQty();
                    getTotalAmt();
                    $('input[type="text"]').focus(function () {
                        if ($(this).attr("entermsg")) {
                            $(".enter-msg").html($(this).attr("entermsg"));
                        } else {
                            $(".enter-msg").html("");
                        }
                        $(".receipt-amt").blur(function () {
                            if ($(this).val() != "") {
                                closestTr.find(".payment-amt").attr("disabled", "disabled");
                            } else {
                                closestTr.find(".payment-amt").removeAttr("disabled");
                            }
                        });
                        $(".payment-amt").blur(function () {
                            if ($(this).val() != "") {
                                closestTr.find(".receipt-amt").attr("disabled", "disabled");
                            } else {
                                closestTr.find(".receipt-amt").removeAttr("disabled");
                            }
                        });
                    });
                    $("#billing-term-list").html("");
                }
            });
        }
        closestTr.find(".text_ledger").addClass("error");
        closestTr.find(".text_ledger").focus();
        return false;
    }
    if ((keycode > 95 && keycode < 106) || (keycode > 45 && keycode < 58) || keycode == 8 || (keycode > 36 && keycode < 40)) {

        return true;
    }
    return false;
}


function CalculateTermAmt(e, element) {
    if (IsNumberKey(e)) {
        var billtermelement = element.closest("tr").find(".product_wise_billterms").find(".single-list");
        var closestTr = element.closest("tr");
        var quantity = parseFloat(closestTr.find(".qty").val());
        if (isNaN(quantity)) {
            quantity = 0;
        }
        var rate = parseFloat(closestTr.find(".rate").val());
        if (isNaN(rate)) {
            rate = 0;
        }

        element.removeClass("error");
        element.closest("tr").find(".rate").removeClass("error");
        var basic = RoundTwoDecimalPlace(parseFloat(quantity) * parseFloat(rate));
        closestTr.find(".basicAmt").val(basic);
        var termAmt = 0;
        var netAmt = basic;
        if (billtermelement.length > 0) {
            billtermelement.each(function (item, index3) {
                var amount = $(this).find(".amount").val();
                if (typeof amount === "undefined") {
                    amount = "0";
                }
                var rate = $(this).find(".Rate").val();
                if (typeof rate === "undefined") {
                    rate = "0";
                }
                if (amount != 0 && rate != 0) {
                    var sign = $(this).find(".Sign").val();
                    var value = RoundTwoDecimalPlace(rate / 100 * netAmt);
                    if (sign == "-") {
                        termAmt = RoundTwoDecimalPlace(termAmt - value);
                    } else {
                        termAmt = RoundTwoDecimalPlace(termAmt + value);
                    }
                    netAmt += parseFloat(termAmt);
                    $(this).find(".amount").val(value);
                }
            });
            element.closest('tr').find('.termAmt').val(termAmt);
            element.closest('tr').find('.netAmt').val(netAmt);
        }
        if (!termAmt) {
            termAmt = 0;
            closestTr.find(".termAmt").val('0');
        }
        closestTr.find(".termAmt").val(termAmt);
        closestTr.find(".netAmt").val(netAmt);
        updateFunction();
    } else {
        e.preventDefault();
        return false;
    }
}

function updateFunction() {
    getTotalQty();
    getTotalAmt();
}

function getTotalAmt() {
    var totalAmt = parseFloat(0);
    var basicAmtElements = document.querySelectorAll(".netAmt");
    var j;
    for (j = 0; j < basicAmtElements.length; j++) {
        var amt = Math.abs(basicAmtElements[j].value);
        if (amt != "") {
            totalAmt = totalAmt + parseFloat(amt);
        }
    }
    var basicAmt = totalAmt;
    var netAmt = 0;
    var termAmt = 0;
    var totalCurrAmt = 0;
    $("#billing-term-list .billing_term_value").each(function () {
        if ($(this).closest(".single-list").find(".amount").val() != '' && $(this).closest(".single-list").find(".amount").val() != 0) {
            var sign = $(this).closest(".single-list").find(".Sign").val();
            var rate = parseFloat($(this).closest(".single-list").find(".Rate").val());
            if (rate == '') {
                rate = 0;
            }
            var value = RoundTwoDecimalPlace(rate / 100 * totalAmt);
            totalAmt += value;
            if (sign == "-") {
                termAmt = RoundTwoDecimalPlace(termAmt - value);
            } else {
                termAmt = RoundTwoDecimalPlace(termAmt + value);
            }
            netAmt = parseFloat(totalAmt) + parseFloat(termAmt);

            var newTermAmt = Math.abs(termAmt);
            $(this).closest(".single-list").find(".amount").val(newTermAmt);
        }
    });
    netAmt = basicAmt + termAmt;
    var currAmt = $(".CurrRate").val();
    if (currAmt != null && currAmt != 0) {
        totalCurrAmt = RoundTwoDecimalPlace(netAmt * currAmt);
    } else {
        totalCurrAmt = netAmt;
    }
    $(".MasterBasicAmt").val(basicAmt);
    $(".MasterNetAmt").val(netAmt);
    $(".MasterTermAmt").val(termAmt);
    $("#TotalAmtRs").val(totalCurrAmt);
}

function getTotalQty() {
   var totalQty = parseFloat(0);
    var qtyElements = document.querySelectorAll(".qty");
    var i;
    for (i = 0; i < qtyElements.length; i++) {
        var amt = qtyElements[i].value;
        if (amt != "") {
            totalQty = totalQty + parseFloat(amt);
        }
    }
    $("#TotalQty").val(totalQty);
}

function DisplayBillWiseTerm(form,element,e) {
    var arrCount = 0;
    if (element.closest("tr").is(":first-child") || element.val() != "") {
        return true;
    } else {
        var ev = e || event;
        var keycode = ev.keyCode;
        if (keycode == "13") {
            if (element.val() == "") {
                if (billWiseBatchList.length != 0) {
                    isProductWise = false;
                    var arrNewBatchList = [];
                    if (element.closest(form).find("#billing-term-list").find(".single-list").length != 0) {
                        element.closest(form).find("#billing-term-list").find(".single-list").each(function (item, index3) {
                            var Id = $(this).find(".billing_term_value").val();
                            var Amount = $(this).find(".amount").val();
                            if (typeof Amount === "undefined") {
                                Amount = "0";
                            }
                            var Basis = $(this).find(".Basis").val();
                            var Description = $(this).find(".Description").val();
                            var Rate = $(this).find(".Rate").val();
                            if (typeof Rate === "undefined") {
                                Rate = "0";
                            }
                            var Sign = $(this).find(".Sign").val();
                            var BillingTermIndex = $(this).find(".BillingTermIndex").val();
                            var IsProductWise = 'false';
                            var ParentGuid = $(this).find(".ParentGuid").val();
                            var DisplayOrder = $(this).find(".DisplayOrder").val();
                            if (typeof DisplayOrder === "undefined") {
                                DisplayOrder = "0";
                            }
                            arrNewBatchList.push({
                                Id: Id,
                                Rate: Rate,
                                Amount: Amount,
                                DisplayOrder: DisplayOrder,
                                IsProductWise: IsProductWise,
                                Description: Description,
                                Sign: Sign,
                                BillingTermIndex: BillingTermIndex,
                                Basis: Basis,
                                ParentGuid: ParentGuid
                            });
                        });
                    } else {
                        arrNewBatchList = billWiseBatchList;
                    }
                    $('#modalbox-billingterm .msg-body').find('tbody').html('');
                    arrNewBatchList.forEach(function (item, index4) {
                        if ($('#modalbox-billingterm .msg-body').find('tbody').html() == '') {
                            $('#modalbox-billingterm .msg-body tbody').html($("#BillTermPopupTemplate").tmpl({
                                Id: item.Id,
                                Amount: item.Amount,
                                Basis: item.Basis,
                                Description: item.Description,
                                Rate: item.Rate,
                                Sign: item.Sign
                            }));
                        } else {
                            $('#modalbox-billingterm .msg-body tbody tr:last').after($("#BillTermPopupTemplate").tmpl({
                                Id: item.Id,
                                Amount: item.Amount,
                                Basis: item.Basis,
                                Description: item.Description,
                                Rate: item.Rate,
                                Sign: item.Sign
                            }));
                        }
                    });
                    $("#modalbox-billingterm").dialog({
                        modal: true,
                        width: 'auto',
                        buttons: {
                            "Ok": function () {
                                $("#billing-term-list").html("");
                                var termAmt = parseFloat(0);
                                var totalCurrAmt = 0;
                                $(this).closest("#modalbox-billingterm").find(".billingterm_amount").each(function () {
                                    var value = parseFloat(0);
                                    var basis = $(this).closest("tr").find(".billingterm_Basis").html();
                                    var sign = $(this).closest("tr").find(".billingterm_Sign").html();
                                    var rate = $(this).closest("tr").find(".billingterm_rate").val();
                                    if (typeof rate == "undefined") {
                                        rate = "0";
                                    }
                                    var desc = $(this).closest("tr").find(".billingterm_Description").html();
                                    var id = $(this).closest("tr").find(".billingterm_Id").val();
                                    var displayOrder = $(this).closest("tr").find(".billingterm_displayorder").val();
                                    if (typeof displayOrder == "undefined") {
                                        displayOrder = "0";
                                    }
                                    var amount = $(this).closest("tr").find(".billingterm_amount").val();
                                    if (typeof amount == "undefined") {
                                        amount = "0";
                                    }
                                    value = parseFloat($(this).val());
                                    if (sign == "-") {
                                        termAmt = RoundTwoDecimalPlace(termAmt - value);
                                    } else {
                                        termAmt = RoundTwoDecimalPlace(termAmt + value);
                                    }
                                    var basicAmt = RoundTwoDecimalPlace($(".MasterBasicAmt").val());
                                    var netAmt = parseFloat(basicAmt) + parseFloat(termAmt);
                                    var currAmt = $(".CurrRate").val();
                                  if (currAmt != null && currAmt != 0) {
                                        totalCurrAmt = RoundTwoDecimalPlace(netAmt * currAmt);
                                    } else {
                                        totalCurrAmt = netAmt;
                                    }

                                    $(".MasterNetAmt").val(netAmt);
                                    $("#TotalAmtRs").val(totalCurrAmt);
                                    $(".MasterTermAmt").val(termAmt);
                                    var batch = new BillingTermViewModel(id, rate, amount, displayOrder, 'false', desc, sign, basis, '');
                                    var result = $.queryable(arrBillWiseBillingTerms).where('a=>a.Id=="' + id + '"');
                                    if (result.length == 0) {
                                        arrCount++;
                                        arrBillWiseBillingTerms.push(batch);
                                    } else {
                                        var arrLength = arrBillWiseBillingTerms.length;
                                        if (arrLength > 0) {
                                            breakloop = 0;
                                            arrBillingTerms.forEach(function (item, indexa) {
                                                var result = $.queryable(arrBillWiseBillingTerms).where('a=>a.Id=="' + id + '"');
                                                if (result.length != 0 && breakloop == 0) {
                                                    breakloop = 1;
                                                    arrBillWiseBillingTerms.splice(indexa, 1);
                                                    arrBillWiseBillingTerms.push(batch);
                                                }
                                            });
                                        }
                                    }
                                    if (element.closest(form).find('#billing-term-list').html() != '') {
                                        var rowGuid = _generateGuid();
                                        element.closest(form).find('#billing-term-list .single-list:last').after($("#ProductBillTermTemplate").tmpl({
                                            index: rowGuid,
                                            Id: id,
                                            Amount: amount,
                                            Basis: basis,
                                            Description: desc,
                                            Rate: rate,
                                            Sign: sign,
                                            BillingTermIndex: '0',
                                            IsProductWise: 'false',
                                            ParentGuid: ''
                                        }));
                                    } else {
                                        var rowGuid = _generateGuid();
                                        element.closest(form).find('#billing-term-list').html($("#ProductBillTermTemplate").tmpl({
                                            index: rowGuid,
                                            Id: id,
                                            Amount: amount,
                                            Basis: basis,
                                            Description: desc,
                                            Rate: rate,
                                            Sign: sign,
                                            BillingTermIndex: '0',
                                            IsProductWise: 'false',
                                            ParentGuid: ''
                                        }));
                                    }
                                });
                                $(this).dialog("close");
                            },
                            Cancel: function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                    $('#modalbox-billingterm').find(".billingterm_rate").first().focus();
                }
                return false;
            }
            return true;
        }
    }
}

$(".InvoiceDate,.DueDay").live('blur', function () {
    var dueDay = $(".DueDay").val();
    if (dueDay != 0 && dueDay != null) {
        var invoiceDate = $(".InvoiceDate").val();
        if (dateType == "1") {
            var data = invoiceDate.split('/');
            var month = data[0];
            var day = data[1];
            var year = data[2];
            var datestr = year + '-' + month + '-' + day;
            var date = new Date(datestr);
            var age = date.addDays(dueDay);
            var e = new Date(age);
            var curr_engdate = e.getDate();
            if (curr_engdate.toString().length == 1) {
                curr_engdate = "0" + curr_engdate;
            }
            var curr_engmonth = e.getMonth() + 1;
            if (curr_engmonth.toString().length == 1) {
                curr_engmonth = "0" + curr_engmonth;
            }
            var curr_engyear = e.getFullYear();
            var engdateString = curr_engmonth + "/" + curr_engdate + "/" + curr_engyear;
            //    var engDateString = convertToEnglishNew(engdateString);
            $("#DueDate").val(engdateString);
        }
        else {
            var engDate = convertToEnglishNew(invoiceDate);
            var nepdata = engDate.split('/');
            var nepmonth = nepdata[0];
            var nepday = nepdata[1];
            var nepyear = nepdata[2];
            var nepdatestr = nepyear + '-' + nepmonth + '-' + nepday;
            var nepdate = new Date(nepdatestr);
            var nepage = nepdate.addDays(dueDay);
            var d = new Date(nepage);
            var curr_date = d.getDate();
            if (curr_date.toString().length == 1) {
                curr_date = "0" + curr_date;
            }
            var curr_month = d.getMonth() + 1;
            if (curr_month.toString().length == 1) {
                curr_month = "0" + curr_month;
            }
            var curr_year = d.getFullYear();
            var dateString = curr_month + "/" + curr_date + "/" + curr_year;
            var nepDateString = convertToNepaliNew(dateString);
            $("#DueDate").val(nepDateString);

        }
    }
    else {
        $("#DueDate").val(' ');
    }
});

function BillingTermViewModel(id, rate, amount, displayOrder, isProductWise, description, sign, basis, guid) {
    this.Id = id;
    this.Rate = rate;
    this.Amount = amount;
    this.DisplayOrder = displayOrder;
    this.IsProductWise = isProductWise;
    this.Description = description;
    this.Sign = sign;
    this.BillingTermIndex = basis;
    this.Guid = guid;
}

$(".CurrRate").live("keyup", function (e) {
    var totalCurrAmt = 0;
    var currAmt = $(".CurrRate").val();
    var netAmt = $(".MasterNetAmt ").val();

    if (currAmt != null && currAmt != 0) {
        totalCurrAmt = RoundTwoDecimalPlace(netAmt * currAmt);
    } else {
        totalCurrAmt = netAmt;
    }
    $("#TotalAmtRs").val(totalCurrAmt);
});

//function validateDecimal(value)    {
//    var RE = /^\d*\.?\d{0,2}$/;
//    if(RE.test(value)){
//       return true;
//    }else{
//       return false;
//    }
//}

//$(".billingterm_amount").live("keypress", (function (event) {
//    // Backspace, tab, enter, end, home, left, right
//    // We don't support the del key in Opera because del == . == 46.
//    var controlKeys = [8, 9, 13, 35, 36, 37, 39, 46];
//    // IE doesn't support indexOf
//    var isControlKey = controlKeys.join(",").match(new RegExp(event.which));
//    // Some browsers just don't raise events for control keys. Easy.
//    // e.g. Safari backspace.
//    if (!event.which || // Control keys in most browsers. e.g. Firefox tab is 0
//      (49 <= event.which && event.which <= 57) || // Always 1 through 9
//      (48 == event.which && $(this).attr("value")) || // No 0 first digit
//      isControlKey) { // Opera assigns values for control keys.
//        return;
//    } else {
//        event.preventDefault();
//    }
//})
//    );



/*
$(".numeric").keypress(function(event) {
// Backspace, tab, enter, end, home, left, right,decimal(.)in number part, decimal(.) in alphabet
// We don't support the del key in Opera because del == . == 46.
var controlKeys = [8, 9, 13, 35, 36, 37, 39,110,190];
// IE doesn't support indexOf
var isControlKey = controlKeys.join(",").match(new RegExp(event.which));
// Some browsers just don't raise events for control keys. Easy.
// e.g. Safari backspace.
if (!event.which || // Control keys in most browsers. e.g. Firefox tab is 0
(49 <= event.which && event.which <= 57) || // Always 1 through 9
(96 <= event.which && event.which <= 106) || // Always 1 through 9 from number section 
(48 == event.which && $(this).attr("value")) || // No 0 first digit
(96 == event.which && $(this).attr("value")) || // No 0 first digit from number section
isControlKey) { // Opera assigns values for control keys.
return;
} else {
event.preventDefault();
}
});
*/

//$(".billingterm_amount").live("keyup", function (e) {
//    if (validateDecimal($(this).val())) {
//        alert(2);
//    } else {
//        e.preventDefault();
//    }
//})


//String.prototype.count = function (s1) {
//    return (this.length - this.replace(new RegExp(s1, "g"), '').length) ;/// s1.length;
//}

$(".basicAmt").live("keyup", function(e) {
    var element = $(this);
    if (IsNumberKey(e)) {
        var billtermelement = element.closest("tr").find(".product_wise_billterms").find(".single-list");
        var closestTr = element.closest("tr");
        var quantity = closestTr.find(".qty").val();
        if (!quantity) {
            quantity = 0;
        }
        var amount = parseFloat(closestTr.find(".basicAmt").val());
        if (isNaN(amount)) {
            amount = 0;
        }

        if (changeRate) {
            var rate = amount / quantity;
            closestTr.find(".rate").val(rate);
        }


        //        if (!rate) {
        //            rate = 0;
        //        }

        element.removeClass("error");
        element.closest("tr").find(".rate").removeClass("error");
        var basic = amount; //RoundTwoDecimalPlace(parseFloat(quantity) * parseFloat(rate));
        closestTr.find(".basicAmt").val(basic);
        var termAmt = 0;
        var netAmt = basic;
        if (billtermelement.length > 0) {
            billtermelement.each(function(item, index3) {
                var amount = $(this).find(".amount").val();
                if (typeof amount === "undefined" || amount == '') {
                    amount = "0";
                }
                var rate = $(this).find(".Rate").val();
                if (typeof rate === "undefined" || amount == '') {
                    rate = "0";
                }
                if (amount != 0 && rate != 0) {
                    var sign = $(this).find(".Sign").val();
                    var value = RoundTwoDecimalPlace(rate / 100 * netAmt);
                    if (sign == "-") {
                        termAmt = RoundTwoDecimalPlace(termAmt - value);
                    } else {
                        termAmt = RoundTwoDecimalPlace(termAmt + value);
                    }
                    netAmt += parseFloat(termAmt);
                    $(this).find(".amount").val(value);
                }
            });
            element.closest('tr').find('.termAmt').val(termAmt);
            element.closest('tr').find('.netAmt').val(netAmt);
        }
        if (!termAmt) {
            termAmt = 0;
            closestTr.find(".termAmt").val('0');
        }
        closestTr.find(".termAmt").val(termAmt);
        closestTr.find(".netAmt").val(netAmt);
        updateFunction();
    } else {
        e.preventDefault();
        return false;
    }
});


$(".qty").live("keyup", function (e) {
    if (IsNumberKey(e)) {
        $(this).removeClass("error");
        var element = $(this);
        var closestTr = element.closest("tr");
        var quantity = parseFloat(closestTr.find(".qty").val());
        if (isNaN(quantity)) {
            quantity = 0;
        }
        var rate = parseFloat(closestTr.find(".rate").val());
        if (isNaN(rate)) {
            rate = 0;
        }
        if (quantity) {
            if (!rate) {
                rate = 0;
            }
            var basic = parseFloat(quantity) * parseFloat(rate);
            closestTr.find(".basicAmt").val(basic);
            var termAmt = parseFloat(closestTr.find(".termAmt").val());
            if (isNaN(termAmt)) {
                termAmt = 0;
            }
            var netAmt = basic + termAmt;
            closestTr.find(".netAmt").val(netAmt);
            getTotalQty();
            getTotalAmt();
        }
    } else {
        e.preventDefault();
        return false;
    }
});

function GrowlMsg(msg) {
    $.blockUI({
        message: '<h4 style="color: white; padding: 5px 5px 5px 75px; text-align: left">' + msg + '</h4>',
        fadeIn: 700,
        fadeOut: 700,
        timeout: 2000,
        showOverlay: false,
        centerY: false,
        css: {
            width: '350px',
            top: '30px',
            left: '',
            right: '10px',
            border: 'none',
            padding: '5px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .6,
            color: '#fff'
        }
    });
}