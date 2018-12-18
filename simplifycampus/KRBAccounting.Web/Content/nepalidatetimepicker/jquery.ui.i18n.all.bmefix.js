/* Arabic Translation for jQuery UI date picker plugin. */
/* Khaled Al Horani -- koko.dw@gmail.com */
/* خالد الحوراني -- koko.dw@gmail.com */
/* NOTE: monthNames are the original months names and they are the Arabic names, not the new months name فبراير - يناير and there isn't any Arabic roots for these months */
;jQuery(function($){
    /*
     * Get Reginal by local(en-US) or country(en)
     * @param lang: Take as local(en-US) first, if can't find one, then pick the country part(en) and find again.
     */
	$.datetimepicker.getRegional = function(lang){
		return ($.datetimepicker.regional[lang] || $.datetimepicker.regional[lang.split("-")[0]]);
	};
});

/* Nepal initialisation for the jQuery UI date picker plugin. */
/* Written by shuosuo (shuosuo@163.com). */
jQuery(function($){
	$.datetimepicker.regional['ne-NP'] = {
		displayLocal:true,
		calendric: 3,
		closeText: 'Close',
		prevText: '&#x3c;Prev Month',
		nextText: 'Next Month&#x3e;',
		prevYearText: '&#x3c;Prev Year',
		nextYearText: 'Next Year&#x3e;',
		currentText: 'आज',
		//monthNames: ['Baishākh', 'Jeṭha', 'Asār', 'Sāun', 'Bhadau', 'Asoj', 'Kartik', 'Mangsir', 'Push', 'Magh', 'Falgun', 'Chaitra'],
		monthNames: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
		monthNamesShort: ['बैशाख','जेष्ठ','आषाढ़','श्रावण','भाद्र','आश्विन',
		'कार्तिक','मार्ग','पौष','माघ','फाल्गुन','चैत्र'],
		dayNames: ['आइतबार्','सोम्बार्','मङल्बार्','बुधबार्','बिहिबार्','शुक्रबार्','शनिबार्'],
		dayNamesShort: ['आइतबार्','सोम्बार्','मङल्बार्','बुधबार्','बिहिबार्','शुक्रबार्','शुक्रबार्'],
		dayNamesMin: ['Su','Mo','Tu','We','Th','Fr','Sa'],
		okLabel: 'OK',
		cancelLabel: 'Cancel',
		dateFormat: 'yy-mm-dd', dateFormatHeader: 'yy-mm', firstDay: 0,
		isRTL: false,
		pickertitle:'नेपाली पात्रो',
		buttonText: 'Open'};
});
/* Polish initialisation for the jQuery UI date picker plugin. */
/* Written by Jacek Wysocki (jacek.wysocki@gmail.com). */

/* english initialisation for the jQuery UI date picker plugin. */
/* Written by Ressol (ressol@gmail.com). */
jQuery(function($){
	$.datetimepicker.regional['en'] = {
		closeText: 'Close',
		prevText: '&#x3c;Prev Month',
		nextText: 'Next Month&#x3e;',
		prevYearText: '&#x3c;Prev Year',
		nextYearText: 'Next Year&#x3e;',
		currentText: 'Today',
		monthNames: ['January','February','March','April','May','June',
		 			'July','August','September','October','November','December'],
		monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
		dayNames: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
		dayNamesShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
		dayNamesMin: ['Su','Mo','Tu','We','Th','Fr','Sa'],
		dateFormat: 'mm/dd/yy', dateFormatHeader: 'mm/yy', firstDay: 0,
		isRTL: false,
		buttonText: 'Select Date'};
	//$.datetimepicker.setDefaults($.datetimepicker.regional['en-US']);
});