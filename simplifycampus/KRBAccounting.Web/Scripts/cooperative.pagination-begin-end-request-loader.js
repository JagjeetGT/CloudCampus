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

function EndRequest() {
    $.unblockUI();
}