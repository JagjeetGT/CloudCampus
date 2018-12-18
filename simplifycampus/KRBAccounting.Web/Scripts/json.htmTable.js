// This function creates a standard table with column/rows
// Parameter Information
// objArray = Anytype of object array, like JSON results
// theme (optional) = A css class to add to the table (e.g. <table class="<theme>">
// enableHeader (optional) = Controls if you want to hide/show, default is show
function CreateTableView(objArray, theme, enableHeader, changeId, textChangeId) {
    // set optional theme parameter
    if (theme === undefined) {
        theme = 'mediumTable'; //default theme
    }

    if (enableHeader === undefined) {
        enableHeader = true; //default enable headers
    }

    var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

    var str = '<table class="' + theme + '">';

    // table head
    if (enableHeader) {
        str += '<thead><tr>';
        for (var index in array[0]) {
            if (index != "Id") {
                str += '<th scope="col">' + index + '</th>';
            }

        }
        str += '</tr></thead>';
    }

    // table body
    str += '<tbody>';
    for (var i = 0; i < array.length; i++) {
        str += (i % 2 == 0) ? '<tr class="alt">' : '<tr>';
        for (var index in array[i]) {
            if (index != "Id") {
                var val = "Id";
                str += '<td><a href="javascript:void(0);" onClick="changeFunction(' + "'" + changeId + "'" + ',' + array[i][val] + ",'" + array[i][index] + "','" + textChangeId + "'" + ');">' + array[i][index] + '</a></td>';
            }
        }
        str += '</tr>';
    }
    str += '</tbody>';
    str += '</table>';
    return str;
}

// This function creates a details view table with column 1 as the header and column 2 as the details
// Parameter Information
// objArray = Anytype of object array, like JSON results
// theme (optional) = A css class to add to the table (e.g. <table class="<theme>">
// enableHeader (optional) = Controls if you want to hide/show, default is show
function CreateDetailView(objArray, theme, enableHeader) {
    // set optional theme parameter
    if (theme === undefined) {
        theme = 'mediumTable';  //default theme
    }

    if (enableHeader === undefined) {
        enableHeader = true; //default enable headers
    }

    var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

    var str = '<table class="' + theme + '">';
    str += '<tbody>';

    for (var i = 0; i < array.length; i++) {
        var row = 0;
        for (var index in array[i]) {
            str += (row % 2 == 0) ? '<tr class="alt">' : '<tr>';

            if (enableHeader) {
                str += '<th scope="row">' + index + '</th>';
            }

            str += '<td>' + array[i][index] + '</td>';
            str += '</tr>';
            row++;
        }
    }
    str += '</tbody>';
    str += '</table>';
    return str;
}


function CreateTableViewList(objArray, theme, enableHeader) {
    // set optional theme parameter
    if (theme === undefined) {
        theme = 'mediumTable'; //default theme
    }

    if (enableHeader === undefined) {
        enableHeader = true; //default enable headers
    }

    var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

    var str = '<table class="' + theme + ' selectable-table">';
    // table head
    if (enableHeader) {
        str += '<thead><tr>';
        for (var index in array[0]) {
            if (index != "Id") {
                str += '<th>' + index + '</th>';
            }

        }
        str += '</tr></thead>';
    }

    // table body
    str += '<tbody>';
    for (var i = 0; i < array.length; i++) {
        str += (i % 2 == 0) ? '<tr>' : '<tr>';
        for (var index in array[i]) {
            if (index != "Id") {
                var val = "Id";
                str += '<td ledger-id="' + array[i][val] + '">' + array[i][index] + '</td>';
            }
        }
        str += '</tr>';
    }
    str += '</tbody>';
    str += '</table>';
    return str;
}


function CreateTableViewDocNumber(objArray, theme, enableHeader, changeId, textChangeId) {
    // set optional theme parameter
    if (theme === undefined) {
        theme = 'mediumTable'; //default theme
    }

    if (enableHeader === undefined) {
        enableHeader = true; //default enable headers
    }

    var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

    var str = '<table class="' + theme + '">';

    // table head
    if (enableHeader) {
        str += '<thead><tr>';
        for (var index in array[0]) {
            if (index != "Id") {
                str += '<th scope="col">' + index + '</th>';
            }

        }
        str += '</tr></thead>';
    }

    // table body
    str += '<tbody>';
    for (var i = 0; i < array.length; i++) {
        str += (i % 2 == 0) ? '<tr class="alt">' : '<tr>';
        for (var index in array[i]) {
            if (index != "Id") {
                var val = "Id";
                str += '<td><a href="javascript:void(0);" onClick="changeFunctionDocNumber(' + "'" + changeId + "'" + ',' + array[i][val] + ",'" + array[i][index] + "','" + textChangeId + "'" + ');">' + array[i][index] + '</a></td>';
            }
        }
        str += '</tr>';
    }
    str += '</tbody>';
    str += '</table>';
    return str;
}

function CreateBillingTermTableView(objArray, theme, enableHeader) {
    // set optional theme parameter
    if (theme === undefined) {
        theme = 'mediumTable'; //default theme
    }

    if (enableHeader === undefined) {
        enableHeader = true; //default enable headers
    }

    var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

    var str = '<table class="' + theme + '">';

    // table head
    if (enableHeader) {
        str += '<thead><tr>';
        for (var index in array[0]) {
            if (index != "Id") {
                str += '<th scope="col">' + index + '</th>';
            }

        }
        str += '</tr></thead>';
    }

    // table body
    str += '<tbody>';
    for (var i = 0; i < array.length; i++) {
        var desc = "";
        str += (i % 2 == 0) ? '<tr class="alt">' : '<tr>';
        for (var index in array[i]) {
            if (index == "Id") {
                str += '<input type="hidden" value="' + array[i][index] + '"class="billingterm_' + index + '"/>';
            }
            if (index == "Description" || index == "Basis" || index == "Sign") {
                if (index == "Description") {
                    desc = array[i][index].replace(" ", "_");
                }

                str += '<td class="billingterm_' + index + '">' + array[i][index] + '</td>';
            }
            if (index == "Rate") {
                //alert(1);
                if ($("#billing-term-list").find("." + desc).first().length != 0) {
                    var rate = $("#billing-term-list").find("." + desc).first().attr("rate");
                } else {
                    rate = 0;
                }
                //alert(rate);
                str += '<td><input type="text" class="billingterm_rate" style="width:50px;" value="' + rate + '"/></td>';
            }
            if (index == "Amount") {
                var amt = parseInt(0);
                if ($("#billing-term-list").find("." + desc).first().length != 0) {
                    amt = Math.abs($("#billing-term-list").find("." + desc).first().attr("amt"));
                    //alert(amt);
                } else {
                    amt = 0;
                }
                str += '<td><input type="text" class="billingterm_amount" style="width:70px;" value="' + amt + '"/></td>';
            }
        }
        str += '</tr>';
    }
    str += '</tbody>';
    str += '</table>';
    return str;
}



function CreateBillingTermTableViewProductWise(objArray, theme, enableHeader, selectedElementTerm, basicAmt) {
    // set optional theme parameter
    if (theme === undefined) {
        theme = 'mediumTable'; //default theme
    }

    if (enableHeader === undefined) {
        enableHeader = true; //default enable headers
    }

    var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

    var str = '<table class="' + theme + '">';

    // table head
    if (enableHeader) {
        str += '<thead><tr>';
        for (var index in array[0]) {
            if (index != "Id" && index != "DisplayOrder") {
                str += '<th scope="col">' + index + '</th>';
            }

        }
        str += '</tr></thead>';
    }

    // table body
    str += '<tbody>';
    for (var i = 0; i < array.length; i++) {
        var desc = "";
        str += (i % 2 == 0) ? '<tr class="alt">' : '<tr>';
        for (var index in array[i]) {
            if (index == "Id") {
                str += '<input type="hidden" value="' + array[i][index] + '"class="billingterm_' + index + '"/>';
            }
            if (index == "DisplayOrder") {
                str += '<input type="hidden" value="' + array[i][index] + '"class="billingterm_displayorder"/>';
            }
            if (index == "Description" || index == "Basis" || index == "Sign") {
                if (index == "Description") {
                    //alert(array[i][index]);
                    desc = array[i][index].replace(" ", "_");
                    //alert(desc);
                }

                str += '<td class="billingterm_' + index + '">' + array[i][index] + '</td>';
            }
            if (index == "Rate") {
                //alert(1);
                if (selectedElementTerm.find("." + desc).first().length != 0) {
                    var rate = selectedElementTerm.find("." + desc).first().attr("rate");
                }
                else if (typeof array[i][index] !== "undefined") {
                    rate = array[i][index];
                }
                else {

                    rate = 0;
                }
                //alert(rate);
                str += '<td><input type="text" class="billingterm_rate input" style="width:50px;" value="' + rate + '"/></td>';
            }
            if (index == "Amount") {
//                var amt = parseInt(0);
//                if (selectedElementTerm.find("." + desc).first().length != 0) {
//                    amt = Math.abs(selectedElementTerm.find("." + desc).first().attr("amt"));
//                } else {
//                    amt = 0;
//                }
                //                str += '<td><input type="text" class="billingterm_amount" style="width:70px;" value="' + amt + '"/></td>';
                var amt = 0;
                if (typeof array[i][index] !== "undefined") {
                    amt = array[i][index];
                }
                str += '<td><input type="text" class="billingterm_amount" style="width:70px;" value="' + amt+ '"/></td>';
            }
        }
        str += '</tr>';
    }
    str += '</tbody>';
    str += '</table>';
    return str;
}

function CreateTableViewCustom(objArray, theme, enableHeader, changeId, textChangeId, type, displaytext) {
    // set optional theme parameter
    if (theme === undefined) {
        theme = 'mediumTable'; //default theme
    }

    if (enableHeader === undefined) {
        enableHeader = true; //default enable headers
    }

    var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

    var str = '<table class="' + theme + '">';

    // table head
    if (enableHeader) {
        str += '<thead><tr>';
        for (var index in array[0]) {
            if (index != "Id" && index != "Reference") {
                str += '<th scope="col">' + index + '</th>';
            }

        }
        str += '</tr></thead>';
    }

    // table body
    str += '<tbody>';
    var reference = "";
    for (var i = 0; i < array.length; i++) {
        str += (i % 2 == 0) ? '<tr class="alt">' : '<tr>';
        for (var index in array[i]) {
            if (index == "Reference") {
               // var j = i + 1;
                reference = array[i][index];
            }
            if (index == displaytext) {
                var val = "Id";
                str += '<td><a href="javascript:void(0);" class="picklist ' + 'displaytext" data-changeid="' + changeId + '" data-value="' + array[i][val] + '"reference="' + reference + '" data-text="' + array[i][index] + '" data-textchangeid="' + textChangeId + '" value-type="' + type + '">' + array[i][index] + '</a></td>';
            }
            else if (index != "Id" && index != "Reference") {
                var val = "Id";
                str += '<td><a href="javascript:void(0);" class="picklist" data-changeid="' + changeId + '" data-value="' + array[i][val] + '" data-text="' + array[i][index] + '"reference="' + reference + '" data-textchangeid="' + textChangeId + '" value-type="' + type + '">' + array[i][index] + '</a></td>';
            }
        }
        str += '</tr>';
    }
    str += '</tbody>';
    str += '</table>';
    return str;
}