$(document).ready(function () {


    $('.tt').qtooltip({ style: { name: 'aquarius' },
        position: {
            corner: {
                target: 'topRight',
                tooltip: 'bottomLeft'
            }
        }
    });

    $('.ttRC').qtooltip({ style: { name: 'aquarius' },
        position: {
            corner: {
                target: 'rightMiddle',
                tooltip: 'leftMiddle'
            }
        }
    });

    $('.ttRB').qtooltip({ style: { name: 'aquarius' },
        position: {
            corner: {
                target: 'bottomRight',
                tooltip: 'topLeft'
            }
        }
    });

    $('.ttLT').qtooltip({ style: { name: 'aquarius' },
        position: {
            corner: {
                target: 'topLeft',
                tooltip: 'bottomRight'
            }
            
        }
    });

    $('.ttLC').qtooltip({ style: { name: 'aquarius' },
        position: {
            corner: {
                target: 'leftMiddle',
                tooltip: 'rightMiddle'
            }
        }
    });

    $('.ttLB').qtooltip({ style: { name: 'aquarius' },
        position: {
            corner: {
                target: 'bottomLeft',
                tooltip: 'topRight'
            }
        }
    });


//    // SORTABLE       
//    $("#sort_1").sortable({ placeholder: "placeholder" });
//    $("#sort_1").disableSelection();

//    // SELECTABLE
//    $("#selectable_1").selectable();


//    // WYSIWIG HTML EDITOR
////    if ($("#wysiwyg").length > 0) {
////        editor = $("#wysiwyg").cleditor({ width: "100%", height: "100%" })[0].focus();
////    }
//    
//    // Sortable table
//    if ($("#tSortable").length > 0) {
//        $("#tSortable").dataTable({ "iDisplayLength": 5, "aLengthMenu": [5, 10, 25, 50, 100], "sPaginationType": "full_numbers", "aoColumns": [{ "bSortable": false }, null, null, null, null] });
//        $("#tbl-list").dataTable({ "iDisplayLength": 5, "sPaginationType": "full_numbers", "bLengthChange": false, "bFilter": false, "bInfo": false, "bPaginate": true, "aoColumns": [{ "bSortable": false }, null, null, null, null] });
//        $(".tbl-list").dataTable({ "iDisplayLength": 5, "sPaginationType": "full_numbers", "bLengthChange": false, "bFilter": false, "bInfo": false, "bPaginate": true, "aoColumns": [{ "bSortable": false }, null, null, null, null] });

//        $("#tSortable_2").dataTable({ "iDisplayLength": 5, "sPaginationType": "full_numbers", "bLengthChange": false, "bFilter": false, "bInfo": false, "bPaginate": true, "aoColumns": [{ "bSortable": false }, null, null, null, null] });
//    }

});

$(window).resize(function () {

    //if ($("#wysiwyg").length > 0) editor.refresh();
    if ($(".wysiwyg").length > 0) editor.refresh();


});
