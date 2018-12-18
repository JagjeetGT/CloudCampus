$(".ui-dialog-titlebar-close").live('click', function () {
    var i = 1;
});
var modalshow = true;
var changeId = "";
var elementId = "";
var rowIdForBillingTerm = "";
var d = 0;
var selectedElement = "";
var isProductWise = false;
$(document).ready(function () {
    $("li.dropdown").live("hover", function () {
        $(this).toggleClass('open');
    });
});
function modalShow() {
    modalshow = true;
}

function removeIdVal() {
    changeId = "";
    elementId = "";
}
$(".modallink").live('keydown', function (e) {
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
            $("#" + valueId).val(0);
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
            if (keycode == 78) {
                if (e.altKey) {

                    if ($(this).attr("createhref")) {
                        var createLink = $(this).attr("createhref");

                        $.ajax({
                            url: createLink,
                            type: "GET",
                            error: function () {
                                alert("An error occurred.");
                                modalshow = true;
                            },
                            success: function (data) {

                                $("#online-modalbox").append(data);

                                var modalId = $("#online-modalbox .online-form:last").attr('id');
                                $("#" + modalId + " .onlineSave").attr("valueId", changeId);
                                $("#" + modalId + " .onlineSave").attr("textId", elementId);

                                $("#" + modalId).dialog({
                                    resizable: false,
                                    width: 'auto',
                                    modal: true,
                                    zIndex: 3999
                                });
                                //$("#modalboxCreate .msg-body").html("");
                                modalshow = true;
                                $.unblockUI();
                            },
                            complet: function () {
                                $.unblockUI();
                            }
                        });

                    } $.unblockUI();
                    modalshow = true;

                    return false;
                }
            }

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
                    $("#search-box").val("");
                    modalshow = true;
                    $.unblockUI();
                },
                complete: function () {
                    $.unblockUI();
                }

            });
        } else {
            e.preventDefault();
        }


    } else {
        return false;
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
    modalShow();
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
        if (ledgerId != "" && ledgerId != 0) {
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

$(".input").live('keydown', function (e) {
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
        $(attr).get(index).select();
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




function DisplayReport() {
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
            url: "/ReportLedger/LedgerReport/?ledgers=" + arr + "&&StartDate=" + startDate + "&&EndDate=" + endDate,
            success: function (data) {

                $("#report").html(data);
                $("#msgClose").click();
                $('#modalReportForm').dialog("close");
            }
        });
    }

}

function DisplayReportPartyLedger() {
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
            url: "/ARAP/PartyLedger/?ledgers=" + arr + "&&StartDate=" + startDate + "&&EndDate=" + endDate + "&&Category=" + category,
            success: function (data) {
                $("#report").html(data);
                $("#msgClose").click();
                $('#modalReportForm').dialog("close");
            }
        });
    }


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
                $("#msgClose").click();
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
                $("#msgClose").click();
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
        if (keycode == 79) {
            if (e.altKey) {
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


$(".billingterm_rate").live('keydown', function (e) {
    var ev = e || event;
    var keycode = ev.keyCode;
    //e.preventDefault();
    var totalAmt = parseFloat(0);
    var basicAmt = parseFloat(0);
    if (isProductWise) {
        basicAmt = parseFloat(selectedElement.find(".basicAmt").val());
    } else {
        basicAmt = parseFloat($(".MasterBasicAmt").val());
    }
    if (keycode == 13) {
        var parent = $(this).closest("tbody");
        (parent.find("tr")).each(function () {
            if ($(this).find(".billingterm_rate").val() != 0 || $(this).find(".billingterm_amount").val() != 0) {
                var rate = parseFloat($(this).find(".billingterm_rate").val());
                var amt = (rate / 100 * basicAmt).toFixed(2);
                if ($(this).find(".billingterm_Sign").html() == "-") {
                    basicAmt = basicAmt - amt;
                }
                if ($(this).find(".billingterm_Sign").html() == "+") {
                    basicAmt = basicAmt + amt;
                }
                basicAmt = Math.abs(basicAmt);
                var amtElement = $(this).find(".billingterm_amount");
                amtElement.val(amt);
                amtElement.focus();
            }
        });
    }
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
                        $('#' + tableClass + ' tr:last').addClass("deletable"); $('#' + tableClass + ' tr:last').addClass("detail-entry"); 
                        var id = $('#' + tableClass + ' tr:last').attr("id");
                        $('#' + tableClass + ' tr:last').after(data);
                        var arrString = id.split("_");
                        var index = parseInt(arrString[1]) + 1;
                        $(".first-col:last").closest("tr").attr("id", "tr_" + index);
                        NewRowDetailList(closestTr, index);

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
            if ($(this).val() == "" && $(this).attr("isrequired")) {
                disable = 1;
            }
        });
        if (disable == 0) {
            var link = $(this).attr("href");
            var valueid = closestTr.find(".first-col").attr("valueid");
            //alert(valueid);
            var value = $("#" + valueid).val();
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
                            $('#' + tableClass + ' tr:last').addClass("deletable");
                            var id = $('#' + tableClass + ' tr:last').attr("id");
                            $('#' + tableClass + ' tr:last').after(data);
                            //var index = parseInt(trIndex + 1);
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
                            if ($(this).attr("entermsg")) {
                                $(".enter-msg").html($(this).attr("entermsg"));
                            } else {
                                $(".enter-msg").html("");
                            }
                        });
                    }
                });
            } else {
                // alert(element.attr("id"));
                var templateId = $(this).attr("templateid");
                if (templateId) {
                    var trIndex = parseInt(closestTr[0].sectionRowIndex) + 1;
                    //alert(trIndex);
                    var trCount = closestTr.closest("tbody").find("tr").length;
                    //closestTr.is(":last-child") &&
                    //alert(trIndex+","+ trCount);
                    if (trIndex == trCount) {
                        $('#' + tableClass + ' tr:last').addClass("deletable");
                        var guid = _generateGuid();
                        $('#' + tableClass + ' tr:last').after($("#" + templateId).tmpl({ index: guid }));
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
                        if ($(this).attr("entermsg")) {
                            $(".enter-msg").html($(this).attr("entermsg"));
                        } else {
                            $(".enter-msg").html("");
                        }
                    });
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

function Begin() {
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

function checkValidationError() {
    var test = document.querySelectorAll('.error');
    if (test.length > 0) {
        $.unblockUI();
    }
}

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

            // if (OK = ((Year > 1900) && (Year < new Date().getFullYear()))) {
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


