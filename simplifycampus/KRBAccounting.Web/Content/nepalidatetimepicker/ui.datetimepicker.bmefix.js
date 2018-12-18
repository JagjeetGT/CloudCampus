/* jQuery UI Date Picker v3.4.3 (previously jQuery Calendar)
   Written by Marc Grabanski (m@marcgrabanski.com) and Keith Wood (kbwood@virginbroadband.com.au).

   Copyright (c) 2007 Marc Grabanski (http://marcgrabanski.com/code/ui-datetimepicker)
   Dual licensed under the MIT (MIT-LICENSE.txt)
   and GPL (GPL-LICENSE.txt) licenses.
   Date: 09-03-2007  */
/* 
 * Time functionality added by Stanislav Dobry (stanislav.dobry@datesoft.cz)
 * Date: 2008-06-04
 */
   
;(function($) { // hide the namespace

function matchDateFormat(dateformat) {
	dateformat = dateformat.replace(":mm", ":ii");
	dateformat = dateformat.replace("mm:", "ii:");
	dateformat = dateformat.replace("m:", "i:");
	if(-1!=dateformat.indexOf("MMMMM"))
	{
		dateformat = dateformat.replace("MMMMM", "MM");
	}
	else
	{
		if(-1!=dateformat.indexOf("MMM"))
		{
			dateformat = dateformat.replace("MMM", "M");
		}
		else
		{
			dateformat = dateformat.replace("MM", "mm");
			dateformat = dateformat.replace("M", "m");
		}
	}
	if(-1!=dateformat.indexOf("yyyy"))
	{
		dateformat = dateformat.replace("yyyy", "yy");
	}
	else
	{   
		//modify by huqiang start
		//dateformat = dateformat.replace("yy", "y");
		//modify by huqiang end
	}
	return dateformat;
}

/* Date picker manager.
   Use the singleton instance of this class, $.datetimepicker, to interact with the date picker.
   Settings for (groups of) date pickers are maintained in an instance object
   (DatepickerInstance), allowing multiple different settings on the same page. */

function DateTimepicker() {
	this.debug = false; // Change this to true to start debugging
	this._nextId = 0; // Next ID for a date picker instance
	this._inst = []; // List of instances indexed by ID
	this._curInst = null; // The current instance in use
	this._disabledInputs = []; // List of date picker inputs that have been disabled
	this._datetimepickerShowing = false; // True if the popup picker is showing , false if not
	this._inDialog = false; // True if showing within a "dialog", false if not
	this.regional = []; // Available regional settings, indexed by language code
	this.regional[''] = { // Default regional settings
		clearText: 'Clear', // Display text for clear link
		clearStatus: 'Erase the current date', // Status text for clear link
		closeText: 'Close', // Display text for close link
		closeStatus: 'Close without change', // Status text for close link
		prevText: '&#x3c;Prev Month', // Display text for previous month link
		prevYearText: '&#x3c;Prev Year', // Display text for previous month link
		okLabel: 'OK', //ok button label
		cancelLabel: 'Cancel', //ok button label
		prevStatus: 'Show the previous month', // Status text for previous month link
		nextText: 'Next Month&#x3e;', // Display text for next month link
		nextYearText: 'Next Year&#x3e;', // Display text for next month link
		nextStatus: 'Show the next month', // Status text for next month link
		currentText: 'Today', // Display text for current month link
		currentStatus: 'Show the current month', // Status text for current month link
		monthNames: ['January','February','March','April','May','June',
			'July','August','September','October','November','December'], // Names of months for drop-down and formatting
		monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'], // For formatting
		defaultMonthNames: ['January','February','March','April','May','June',
		 			'July','August','September','October','November','December'], // Names of months for drop-down and formatting
		defaultMonthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'], // For formatting
		monthStatus: 'Show a different month', // Status text for selecting a month
		yearStatus: 'Show a different year', // Status text for selecting a year
		weekHeader: 'Wk', // Header for the week of the year column
		weekStatus: 'Week of the year', // Status text for the week of the year column
		dayNames: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'], // For formatting
		dayNamesShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'], // For formatting
		dayNamesMin: ['Su','Mo','Tu','We','Th','Fr','Sa'], // Column headings for days starting at Sunday
		dayStatus: 'Set DD as first week day', // Status text for the day of the week selection
		dateStatus: 'Select DD, M d', // Status text for the date selection
		dateFormat: 'mm/dd/yy', // See format options on parseDate
		dateFormatHeader: 'mm/yy',
		displaytime: true,
		//timeFormat: 'HH:ii:ss',
		firstDay: 0, // The first day of the week, Sun = 0, Mon = 1, ...
		initStatus: 'Select a date', // Initial Status text on opening
		isRTL: false, // True if right-to-left language, false if left-to-right
		pickertitle:'local',
		enableDsl:true,
		dslChecked:true
	};
	this._defaults = { // Global defaults for all the date picker instances
		showOn: 'focus', // 'focus' for popup on focus,
			// 'button' for trigger button, or 'both' for either
		showAnim: 'show', // Name of jQuery animation for popup
		defaultDate: null, // Used when field is blank: actual date,
			// +/-number for offset from today, null for today
		appendText: '', // Display text following the input box, e.g. showing the format
		buttonText: '...', // Text for trigger button
		buttonImage: '', // URL for trigger button image
		buttonImageOnly: false, // True if the image appears alone, false if it appears on a button
		closeAtTop: false, // True to have the clear/close at the top,
							// false to have them at the bottom
		closeHide: true,	//True to hide clear/close bar , false to show clear/close bar
		mandatory: false, // True to hide the Clear link, false to include it
		hideIfNoPrevNext: false, // True to hide next/previous month links
			// if not applicable, false to just disable them
		changeMonth: true, // True if month can be selected directly, false if only prev/next
		changeYear: true, // True if year can be selected directly, false if only prev/next
		yearRange: '-10:+10', // Range of years to display in drop-down,
			// either relative to current year (-nn:+nn) or absolute (nnnn:nnnn)
		changeFirstDay: false, // True to click on day name to change, false to remain as set
		showOtherMonths: true, // True to show dates in other months, false to leave blank
		showWeeks: false, // True to show week of the year, false to omit
		calculateWeek: this.iso8601Week, // How to calculate the week of the year,
			// takes a Date and returns the number of the week for it
		shortYearCutoff: '+10', // Short year values < this are in the current century,
			// > this are in the previous century, 
			// string value starting with '+' for current year + value
		showStatus: false, // True to show status bar at bottom, false to not show it
		statusForDate: this.dateStatus, // Function to provide status text for a date -
			// takes date and instance as parameters, returns display text
		minDate: null, // The earliest selectable date, or null for no limit
		maxDate: null, // The latest selectable date, or null for no limit
		speed: 'normal', // Speed of display/closure
		beforeShowDay: null, // Function that takes a date and returns an array with
			// [0] = true if selectable, false if not,
			// [1] = custom CSS class name(s) or '', e.g. $.datetimepicker.noWeekends
		beforeShow: null, // Function that takes an input field and
			// returns a set of custom settings for the date picker
		onSelect: null, // Define a callback function when a date is selected
		onClose: null, // Define a callback function when the datetimepicker is closed
		numberOfMonths: 1, // Number of months to show at a time
		stepMonths: 1, // Number of months to step back/forward
		rangeSelect: false, // Allows for selecting a date range on one date picker
		rangeSeparator: ' - ' // Text between two dates in a range
	};
	$.extend(this._defaults, this.regional['']);
	this._datetimepickerDiv = $('<div id="datetimepicker_div"></div>');
}

$.extend(DateTimepicker.prototype, {
	/* Class name added to elements to indicate already configured with a date picker. */
	markerClassName: 'hasDatepicker',

	/* Debug logging (if enabled). */
	log: function () {
		if (this.debug)
			console.log.apply('', arguments);
	},
	
	/* Register a new date picker instance - with custom settings. */
	_register: function(inst) {
		var id = this._nextId++;
		this._inst[id] = inst;
		return id;
	},

	/* Retrieve a particular date picker instance based on its ID. */
	_getInst: function(id) {
		return this._inst[id] || id;
	},

	/* Override the default settings for all instances of the date picker. 
	   @param  settings  object - the new settings to use as defaults (anonymous object)
	   @return the manager object */
	setDefaults: function(settings) {
		var temp = settings.dateFormat;
		if(temp){
			settings.dateFormat = matchDateFormat(temp);
		}
		temp = settings.localDateFormat;
		if(temp){
			settings.localDateFormat = matchDateFormat(temp);
		}
		extendRemove(this._defaults, settings || {});
		return this;
	},

	/* Attach the date picker to a jQuery selection.
	   @param  target    element - the target input field or division or span
	   @param  settings  object - the new settings to use for this date picker instance (anonymous) */
	_attachDatepicker: function(target, settings) {
		settings = settings || {};
		// check for settings on the control itself - in namespace 'date:'
		var inlineSettings = null;
		for (attrName in this._defaults) {
			var attrValue = target.getAttribute('date:' + attrName);
			if (attrValue) {
				inlineSettings = inlineSettings || {};
				try {
					inlineSettings[attrName] = eval(attrValue);
				} catch (err) {
					inlineSettings[attrName] = attrValue;
				}
			}
		}
		var nodeName = target.nodeName.toLowerCase();
		var instSettings = inlineSettings ?  $.extend(settings, inlineSettings ) : settings;
		if (nodeName == 'input') {
			var inst = (inst && !inlineSettings ? inst :
				new DateTimepickerInstance(instSettings, false));
			this._connectDatepicker(target, inst);
			$(target).bind("paste",function(){return false;});
			var provide_chars = " 0123456789";
			if(inst._settings.dateFormat)
			{
				provide_chars += inst._settings.dateFormat;
			}
			if(inst._settings.timeFormat)
			{
				provide_chars += inst._settings.timeFormat;
			}
			var re = /[mydhis]/gim;
			provide_chars = provide_chars.replace(re,"");
			provide_chars = provide_chars.replace(/\-/gim, "\\-");
			provide_chars = provide_chars.replace(/\//gim, "\\/");
			provide_chars = provide_chars.replace(/\./gim, "\\.");
		} else if (nodeName == 'div' || nodeName == 'span') {
			var inst = new DateTimepickerInstance(instSettings, true);
			this._inlineDatepicker(target, inst);
		}
	},

	/* Detach a datetimepicker from its control.
	   @param  target    element - the target input field or division or span */
	_destroyDatepicker: function(target) {
		var nodeName = target.nodeName.toLowerCase();
		var calId = target._calId;
		target._calId = null;
		var $target = $(target);
		if (nodeName == 'input') {
			$target.siblings('.datetimepicker_append').replaceWith('').end()
				.siblings('.datetimepicker_trigger').replaceWith('').end()
				.removeClass(this.markerClassName)
				.unbind('focus', this._showDatepicker)
				.unbind('keydown', this._doKeyDown)
				.unbind('keypress', this._doKeyPress);
			var wrapper = $target.parents('.datetimepicker_wrap');
			if (wrapper)
			   
				wrapper.replaceWith(wrapper.html());
		} else if (nodeName == 'div' || nodeName == 'span')
			$target.removeClass(this.markerClassName).empty();
		if ($('input[_calId=' + calId + ']').length == 0)
			// clean up if last for this ID
			this._inst[calId] = null;
	},

	/* Enable the date picker to a jQuery selection.
	   @param  target    element - the target input field or division or span */
	_enableDatepicker: function(target) {
		target.disabled = false;
		$(target).siblings('button.datetimepicker_trigger').each(function() { this.disabled = false; }).end()
			.siblings('img.datetimepicker_trigger').css({opacity: '1.0', cursor: ''});
		this._disabledInputs = $.map(this._disabledInputs,
			function(value) { return (value == target ? null : value); }); // delete entry
	},

	/* Disable the date picker to a jQuery selection.
	   @param  target    element - the target input field or division or span */
	_disableDatepicker: function(target) {
		target.disabled = true;
		$(target).siblings('button.datetimepicker_trigger').each(function() { this.disabled = true; }).end()
			.siblings('img.datetimepicker_trigger').css({opacity: '0.5', cursor: 'default'});
		this._disabledInputs = $.map($.datetimepicker._disabledInputs,
			function(value) { return (value == target ? null : value); }); // delete entry
		this._disabledInputs[$.datetimepicker._disabledInputs.length] = target;
	},

	/* Is the first field in a jQuery collection disabled as a datetimepicker?
	   @param  target    element - the target input field or division or span
	   @return boolean - true if disabled, false if enabled */
	_isDisabledDatepicker: function(target) {
		if (!target)
			return false;
		for (var i = 0; i < this._disabledInputs.length; i++) {
			if (this._disabledInputs[i] == target)
				return true;
		}
		return false;
	},

	/* Update the settings for a date picker attached to an input field or division.
	   @param  target  element - the target input field or division or span
	   @param  name    string - the name of the setting to change or
	                   object - the new settings to update
	   @param  value   any - the new value for the setting (omit if above is an object) */
	_changeDatepicker: function(target, name, value) {
		var settings = name || {};
		if (typeof name == 'string') {
			settings = {};
			settings[name] = value;
		}
		if (inst = this._getInst(target._calId)) {
			extendRemove(inst._settings, settings);
			this._updateDatepicker(inst);
		}
	},

	/* Set the dates for a jQuery selection.
	   @param  target   element - the target input field or division or span
	   @param  date     Date - the new date
	   @param  endDate  Date - the new end date for a range (optional) */
	_setDateDatepicker: function(target, date, endDate) {
		if (inst = this._getInst(target._calId)) {
			inst._setDate(date, endDate);
			this._updateDatepicker(inst);
		}
	},

	/* Get the date(s) for the first entry in a jQuery selection.
	   @param  target  element - the target input field or division or span
	   @return Date - the current date or
	           Date[2] - the current dates for a range */
	_getDateDatepicker: function(target) {
		var inst = this._getInst(target._calId);
		return (inst ? inst._getDate() : null);
	},

	/* Handle keystrokes. */
	_doKeyDown: function(e) {
		var inst = $.datetimepicker._getInst(this._calId);
		if ($.datetimepicker._datetimepickerShowing)
			switch (e.keyCode) {
				case 9: 
						$.datetimepicker._hideDatepicker(null, '');
						break; // hide on tab out
				case 13: $.datetimepicker._selectDay(inst, inst._selectedMonth, inst._selectedYear, inst._selectedHour, inst._selectedMinute, inst._selectedSecond,
							$('td.datetimepicker_daysCellOver', inst._datetimepickerDiv)[0]);
						return false; // don't submit the form
						break; // select the value on enter
				case 27: $.datetimepicker._hideDatepicker(null, inst._get('speed'));
						break; // hide on escape
				case 33: $.datetimepicker._adjustDate(inst,
							(e.ctrlKey ? -1 : -inst._get('stepMonths')), (e.ctrlKey ? 'Y' : 'M'));
						break; // previous month/year on page up/+ ctrl
				case 34: $.datetimepicker._adjustDate(inst,
							(e.ctrlKey ? +1 : +inst._get('stepMonths')), (e.ctrlKey ? 'Y' : 'M'));
						break; // next month/year on page down/+ ctrl
				case 35: if (e.ctrlKey) $.datetimepicker._clearDate(inst);
						break; // clear on ctrl+end
				case 36: if (e.ctrlKey) $.datetimepicker._gotoToday(inst);
						break; // current on ctrl+home
				case 37: if (e.ctrlKey) $.datetimepicker._adjustDate(inst, -1, 'D');
						break; // -1 day on ctrl+left
				case 38: if (e.ctrlKey) $.datetimepicker._adjustDate(inst, -7, 'D');
						break; // -1 week on ctrl+up
				case 39: if (e.ctrlKey) $.datetimepicker._adjustDate(inst, +1, 'D');
						break; // +1 day on ctrl+right
				case 40: if (e.ctrlKey) $.datetimepicker._adjustDate(inst, +7, 'D');
						break; // +1 week on ctrl+down
		}
		else if (e.keyCode == 36 && e.ctrlKey) // display the date picker on ctrl+home
			$.datetimepicker._showDatepicker(this);
	},

	/* Filter entered characters - based on date format. */
	_doKeyPress: function(e) {
		var inst = $.datetimepicker._getInst(this._calId);
		var chars = $.datetimepicker._possibleChars(inst._get('dateFormat'));
		var chr = String.fromCharCode(e.charCode == undefined ? e.keyCode : e.charCode);
		return e.ctrlKey || (chr < ' ' || !chars || chars.indexOf(chr) > -1);
	},

	/* Attach the date picker to an input field. */
	_connectDatepicker: function(target, inst) {
		var input = $(target);
	    if (input.is('.' + this.markerClassName))
			return;
		var appendText = inst._get('appendText');
		var isRTL = inst._get('isRTL');
		if (appendText) {
			if (isRTL)
				input.before('<span class="datetimepicker_append">' + appendText);
			else
				input.after('<span class="datetimepicker_append">' + appendText);
		}
		var showOn = inst._get('showOn');
		if (showOn == 'focus' || showOn == 'both') // pop-up date picker when in the marked field
			input.focus(this._showDatepicker);
		if (showOn == 'button' || showOn == 'both') { // pop-up date picker when button clicked
			if(input.disabled)
			{
				return;
			}
			var buttonText = inst._get('buttonText');
			var dv = input.closest(".bc_border");
			var trigger = dv.next();
			trigger.attr({title: buttonText});

			//modify by h00152901 begin 
			var buttonImage = inst._get('buttonImage');
			var trigger = $(inst._get('buttonImageOnly') ? 
				$('<img>').addClass('datetimepicker_trigger').attr({ src: buttonImage, alt: buttonText, title: buttonText }) :
				$('<button>').addClass('datetimepicker_trigger').attr({ type: 'button' }).html(buttonImage != '' ? 
						$('<img>').attr({ src:buttonImage, alt:buttonText, title:buttonText }) : buttonText));
			
		  if (isRTL)
			{
				input.before(trigger);
			}
			else
			{
				input.after(trigger);
			}
			//modify by h00152901 end
			
			trigger.click(function() {
				if ($.datetimepicker._datetimepickerShowing && $.datetimepicker._lastInput == target)
					$.datetimepicker._hideDatepicker();
				else
					$.datetimepicker._showDatepicker(target);
			});
        }
		input.addClass(this.markerClassName).keydown(this._doKeyDown).keypress(this._doKeyPress)
			.bind("setData.datetimepicker", function(event, key, value) {
			   inst._settings[key] = value;
			}).bind("getData.datetimepicker", function(event, key) {
				return inst._get(key);
			});
		input[0]._calId = inst._id;
	},

	/* Attach an inline date picker to a div. */
	_inlineDatepicker: function(target, inst) {
		var input = $(target);
		if (input.is('.' + this.markerClassName))
			return;
		input.addClass(this.markerClassName).append(inst._datetimepickerDiv)
			.bind("setData.datetimepicker", function(event, key, value){
			    
				inst._settings[key] = value;
			}).bind("getData.datetimepicker", function(event, key){
				return inst._get(key);
			});
		input[0]._calId = inst._id;
		this._updateDatepicker(inst);
	},

	/* Tidy up after displaying the date picker. */
	_inlineShow: function(inst) {
		var numMonths = inst._getNumberOfMonths(); // fix width for dynamic number of date pickers
		inst._datetimepickerDiv.width(numMonths[1] * $('.datetimepicker', inst._datetimepickerDiv[0]).width());
	}, 

	/* Pop-up the date picker in a "dialog" box.
	   @param  input     element - ignored
	   @param  dateText  string - the initial date to display (in the current format)
	   @param  onSelect  function - the function(dateText) to call when a date is selected
	   @param  settings  object - update the dialog date picker instance's settings (anonymous object)
	   @param  pos       int[2] - coordinates for the dialog's position within the screen or
	                     event - with x/y coordinates or
	                     leave empty for default (screen centre)
	   @return the manager object */
	_dialogDatepicker: function(input, dateText, onSelect, settings, pos) {
		var inst = this._dialogInst; // internal instance
		if (!inst) {
			inst = this._dialogInst = new DateTimepickerInstance({}, false);
			this._dialogInput = $('<input type="text" size="1" style="position: absolute; top: -100px;"/>');
			this._dialogInput.keydown(this._doKeyDown);
			$('body').append(this._dialogInput);
			this._dialogInput[0]._calId = inst._id;
		}
		extendRemove(inst._settings, settings || {});
		this._dialogInput.val(dateText);

		this._pos = (pos ? (pos.length ? pos : [pos.pageX, pos.pageY]) : null);
		if (!this._pos) {
			var browserWidth = window.innerWidth || document.documentElement.clientWidth ||	document.body.clientWidth;
			var browserHeight = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;
			var scrollX = document.documentElement.scrollLeft || document.body.scrollLeft;
			var scrollY = document.documentElement.scrollTop || document.body.scrollTop;
			this._pos = // should use actual width/height below
				[(browserWidth / 2) - 100 + scrollX, (browserHeight / 2) - 150 + scrollY];
		}

		// move input on screen for focus, but hidden behind dialog
		this._dialogInput.css('left', this._pos[0] + 'px').css('top', this._pos[1] + 'px');
		inst._settings.onSelect = onSelect;
		this._inDialog = true;
		this._datetimepickerDiv.addClass('datetimepicker_dialog');
		this._showDatepicker(this._dialogInput[0]);
		if ($.blockUI)
			$.blockUI(this._datetimepickerDiv);
		return this;
	},

	/* Pop-up the date picker for a given input field.
	   @param  input  element - the input field attached to the date picker or
	                  event - if triggered by focus */
	_showDatepicker: function(input) {
		// Doesn't response the event if the input is disabled.
		if(input.disabled)
		{
			return;
		}
		$(input).keydown(this._doKeyDown);
		input = input.target || input;
		if (input.nodeName.toLowerCase() != 'input') // find from button/image trigger
			input = $('input', input.parentNode)[0];
		if ($.datetimepicker._isDisabledDatepicker(input) || $.datetimepicker._lastInput == input) // already here
			return;
		var inst = $.datetimepicker._getInst(input._calId);
		var beforeShow = inst._get('beforeShow');
		extendRemove(inst._settings, (beforeShow ? beforeShow.apply(input, [input, inst]) : {}));
		$.datetimepicker._hideDatepicker(null, '');
		$.datetimepicker._lastInput = input;
		inst._setDateFromField(input);
		if ($.datetimepicker._inDialog) // hide cursor
			input.value = '';
//		if (!$.datetimepicker._pos) { // position below input
//			$.datetimepicker._pos = $.datetimepicker._findPos(input);
//			$.datetimepicker._pos[1] += input.offsetHeight; // add the height
//		}
//		var isFixed = false;
//		$(input).parents().each(function() {
//			isFixed |= $(this).css('position') == 'fixed';
//		});
//		if (isFixed && $.browser.opera) { // correction for Opera when fixed and scrolled
//			$.datetimepicker._pos[0] -= document.documentElement.scrollLeft;
//			$.datetimepicker._pos[1] -= document.documentElement.scrollTop;
//		}
//		inst._datetimepickerDiv.css('position', ($.datetimepicker._inDialog && $.blockUI ?
//			'static' : (isFixed ? 'fixed' : 'absolute')))
//			.css({ left: $.datetimepicker._pos[0] + 'px', top: $.datetimepicker._pos[1] + 'px' });
//		$.datetimepicker._pos = null;
		inst._rangeStart = null;
		$.datetimepicker._updateDatepicker(inst);
		
		if (!inst._inline) {
			var speed = inst._get('speed');
			
			//inst.poper.show($(input));
			$.datetimepicker._afterShow(inst);
			$.datetimepicker._datetimepickerShowing = true;	

			if (speed == '')
				postProcess();
//			if (inst._input[0].type != 'hidden')
//				inst._input[0].focus();
			$.datetimepicker._curInst = inst;
		}
	},
	/* Generate the date picker content. */
	_updateDatepicker: function(inst) {
		//jBME.titleTipsPoper.hide();
		inst._datetimepickerDiv.empty().append(inst._generateDatepicker());
        //alert($("table.datetimepicker").html());
		//$("table.datetimepicker").tips();


//        $('.table.datetimepicker')
//			/*
//			 * Lets delete the qTip data from our target element so we can apply multiple tooltips.
//			 * Since the data is also stored on the tooltip element itself this isn't a problem!
//			 * 
//			 * Check here for more details on this: http://craigsworks.com/projects/qtip2/tutorials/advanced/#multi
//			 */
//			.removeData('qtip') 
//			.qtip({
//				content: {
//					//text: 'At its ' + at[i], 
//					text: 'At its <h1>aa</h1>', 
//					title: {
//						text: $("table.datetimepicker").html(),
//						button: true
//					}
//				},
//				position: {
//					my: "top center", // Use the corner...
//					at: "top center" // ...and opposite corner
//				},
//				show: {
//					event: false, // Don't specify a show event...
//					ready: true // ... but show the tooltip when ready
//				},
//				hide: false, // Don't specify a hide event either!
//				style: {
//					classes: 'ui-tooltip-shadow ui-tooltip-' + 'bootstrap'
//				}
//			});




        //alert($(".datetimepicker_newYear").val());
        //alert($("table.datetimepicker").html());
		var df = inst._get('dateFormat');
		var tf = df.split(" ")[1];
		if(!tf||tf==""||tf=="undefined")
		{
			tf = "HH:mm:ss";
		}
		if(inst._get('displaytime'))
		{
			$("div.datetimepicker_timepicker").find("input:eq(0)").timePicker({"onchange": function(val) {
				$.datetimepicker._selectTimeFull(inst, val);
			}, "containerStyle": "absolute", timeFormat: tf});
		}
		var numMonths = inst._getNumberOfMonths();
		if (numMonths[0] != 1 || numMonths[1] != 1)
			inst._datetimepickerDiv.addClass('datetimepicker_multi');
		else
			inst._datetimepickerDiv.removeClass('datetimepicker_multi');

		if (inst._get('isRTL'))
			inst._datetimepickerDiv.addClass('datetimepicker_rtl');
		else
			inst._datetimepickerDiv.removeClass('datetimepicker_rtl');

//		if (inst._input && inst._input[0].type != 'hidden')
//			$(inst._input[0]).focus();
		
		var input=$(".datetimepicker_dsl input",inst._datetimepickerDiv);
		var i=this;
//		input.click(function(){
//			//i.setDsl(i._get("dslChecked"));
//		});
		//this.setDsl(this._get("dslChecked"));
	},

	/* Tidy up after displaying the date picker. */
	_afterShow: function(inst) {
		var numMonths = inst._getNumberOfMonths(); // fix width for dynamic number of date pickers
		inst._datetimepickerDiv.width(numMonths[1] * $('.datetimepicker', inst._datetimepickerDiv[0])[0].offsetWidth);
//		if ($.browser.msie && parseInt($.browser.version) < 7) { // fix IE < 7 select problems
//			$('iframe.datetimepicker_cover').css({width: inst._datetimepickerDiv.width() + 4,
//				height: inst._datetimepickerDiv.height() + 4});
//		}
		// re-position on screen if necessary
//		var isFixed = inst._datetimepickerDiv.css('position') == 'fixed';
//		var pos = inst._input ? $.datetimepicker._findPos(inst._input[0]) : null;
//		var browserWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
//		var browserHeight = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;
//		var scrollX = (isFixed ? 0 : document.documentElement.scrollLeft || document.body.scrollLeft);
//		var scrollY = (isFixed ? 0 : document.documentElement.scrollTop || document.body.scrollTop);
//		// reposition date picker horizontally if outside the browser window
//		if ((inst._datetimepickerDiv.offset().left + inst._datetimepickerDiv.width() -
//				(isFixed && $.browser.msie ? document.documentElement.scrollLeft : 0)) >
//				(browserWidth + scrollX)) {
//			inst._datetimepickerDiv.css('left', Math.max(scrollX,
//				pos[0] + (inst._input ? $(inst._input[0]).width() : null) - inst._datetimepickerDiv.width() -
//				(isFixed && $.browser.opera ? document.documentElement.scrollLeft : 0)) + 'px');
//		}
//		// reposition date picker vertically if outside the browser window
//		if ((inst._datetimepickerDiv.offset().top + Math.max(inst._datetimepickerDiv.height(), inst._datetimepickerShadow.jqShadow.outerHeight()) -
//				(isFixed && $.browser.msie ? document.documentElement.scrollTop : 0)) >
//				(browserHeight + scrollY) ) {
//			inst._datetimepickerDiv.css('top', Math.max(scrollY,
//				pos[1] - (this._inDialog ? 0 : inst._datetimepickerDiv.height()) -
//				(isFixed && $.browser.opera ? document.documentElement.scrollTop : 0) - 6) + 'px');
//		}
	},
	
	
	setDsl:function(b){
		var input=$(".datetimepicker_dsl input",inst._datetimepickerDiv);
		input.attr("checked",true);
	},
	
	/* Find an object's position on the screen. */
	_findPos: function(obj) {
        while (obj && (obj.type == 'hidden' || obj.nodeType != 1)) {
            obj = obj.nextSibling;
        }
        var position = $(obj).offset();
	    return [position.left, position.top];
	},

	/* Hide the date picker from view.
	   @param  input  element - the input field attached to the date picker
	   @param  speed  string - the speed at which to close the date picker */
	_hideDatepicker: function(input, speed) {
		var inst = this._curInst;
		if (!inst)
			return;
		var rangeSelect = inst._get('rangeSelect');
		if (rangeSelect && this._stayOpen) {
			this._selectDate(inst, inst._formatDateTime(
				inst._currentDay, inst._currentMonth, inst._currentYear, inst._currentHour, inst.currentMinute, inst.currentSecond));
		}
		this._stayOpen = false;
		if (this._datetimepickerShowing) {
			//inst.poper.hide();
			speed = (speed != null ? speed : inst._get('speed'));
			var showAnim = inst._get('showAnim');
			inst._datetimepickerDiv[(showAnim == 'slideDown' ? 'slideUp' :
				(showAnim == 'fadeIn' ? 'fadeOut' : 'hide'))](speed, function() {
				$.datetimepicker._tidyDialog(inst);
			});
			if (speed == '')
				this._tidyDialog(inst);
			var onClose = inst._get('onClose');
			if (onClose) {
				onClose.apply((inst._input ? inst._input[0] : null),
					[inst._getDate(), inst]);  // trigger custom callback
			}
			this._datetimepickerShowing = false;
			this._lastInput = null;
			inst._settings.prompt = null;
			if (this._inDialog) {
				this._dialogInput.css({ position: 'absolute', left: '0', top: '-100px' });
				if ($.blockUI) {
					$.unblockUI();
					$('body').append(this._datetimepickerDiv);
				}
			}
			this._inDialog = false;
		}
		this._curInst = null;
	},

	/* Tidy up after a dialog display. */
	_tidyDialog: function(inst) {
		inst._datetimepickerDiv.removeClass('datetimepicker_dialog').unbind('.datetimepicker');
		$('.datetimepicker_prompt', inst._datetimepickerDiv).remove();
	},

	/* Close date picker if clicked elsewhere. */
	_checkExternalClick: function(event) {
		if (!$.datetimepicker._curInst)
			return;
		var $target = $(event.target);
		if (($target.parents("#datetimepicker_div").length == 0) &&
				($target.attr('class') != 'datetimepicker_trigger') &&
				$.datetimepicker._datetimepickerShowing && !($.datetimepicker._inDialog && $.blockUI)) {
			$.datetimepicker._hideDatepicker(null, '');
		}
	},

	/* Adjust one of the date sub-fields. */
	_adjustDate: function(id, offset, period) {
		var inst = this._getInst(id);
		inst._adjustDate(offset, period);
		this._updateDatepicker(inst);
	},

	/* Action for current link. */
	_gotoToday: function(id) {
		var date = new Date();
		var inst = this._getInst(id);
		inst._selectedDay = date.getDate();
		inst._drawMonth = inst._selectedMonth = date.getMonth();
		inst._drawYear = inst._selectedYear = date.getFullYear();
		inst._drawHour = inst._selectedHour = date.getHours();
		inst._drawMinute = inst._selectedMinute = date.getMinutes();
		inst._drawSecond = inst._selectedSecond = date.getSeconds();
		this._adjustDate(inst);
	},

	/* Action for selecting a new month/year. */
	_selectMonthYear: function(id, select, period) {
		var selectclass = $(select).attr("class");
		var inst = this._getInst(id);
		inst._selectingMonthYear = false;
		inst[period == 'M' ? '_drawMonth' : '_drawYear'] =
			select.options[select.selectedIndex].value - 0;
		this._adjustDate(inst);
		$("."+selectclass).focus();
        $(".datetimepicker_div").html($("#datetimepicker_div").html());
	    var dateBottom = getBottomDate();
	    var date = "<div class='date_bottom'>" + dateBottom + "</div>";
	    $(".datetimepicker_div").append(date);
	},
	_selectTime: function(id, select, period) {
		var inst = this._getInst(id);
		inst._selectingMonthYear = false;
		var periodStr;
		if(period == 'S')
		{
			periodStr = '_drawSecond';
		}
		else if(period == 'M')
		{
			periodStr = '_drawMinute';
		}
		else
		{
			periodStr = '_drawHour';
		}
		inst[periodStr] =
			select.options[select.selectedIndex].value - 0;
		this._adjustDate(inst);
		
		this._doNotHide = true;
		$('td.datetimepicker_currentDay').each(function(){
			$.datetimepicker._selectDay(inst, inst._selectedMonth, inst._selectedYear, inst._selectedHour, inst._selectedMinute, inst._selectedSecond, $(this));
		});
		this._doNotHide = false;
	},
	_selectTimeFull: function(inst, timeStr) {
		var timeArr = timeStr.split(':');
		inst['_drawHour'] = inst['_selectedHour'] = timeArr[0]-0;
		inst['_drawMinute'] = inst['_selectedMinute'] = timeArr[1]-0;
		inst['_drawSecond'] = inst['_selectedSecond'] = timeArr[2]-0;
	},

	/* Restore input focus after not changing month/year. */
	_clickMonthYear: function(id) {
		var inst = this._getInst(id);
//		if (inst._input && inst._selectingMonthYear && !$.browser.msie)
//			inst._input[0].focus();
		inst._selectingMonthYear = !inst._selectingMonthYear;
	},

	_clickTime: function(id) {
		var inst = this._getInst(id);
		if (inst._input && inst._selectingTime && !$.browser.msie)
			inst._input[0].focus();
		inst._selectingTime = !inst._selectingTime;
	},

	/* Action for changing the first week day. */
	_changeFirstDay: function(id, day) {
		var inst = this._getInst(id);
		inst._settings.firstDay = day;
		this._updateDatepicker(inst);
	},

	/* Action for selecting a day. */
	_selectDay: function(id, month, year, hour, minute, second, td) {
		if ($(td).is('.datetimepicker_unselectable'))
			return;
		var inst = this._getInst(id);
		var rangeSelect = inst._get('rangeSelect');
		if (rangeSelect) {
			if (!this._stayOpen) {
				$('.datetimepicker td').removeClass('datetimepicker_currentDay');
				$(td).addClass('datetimepicker_currentDay');
			} 
			this._stayOpen = !this._stayOpen;
		}
			inst._selectedDay = inst._currentDay = $('a', td).attr("value");
		inst._selectedMonth = inst._currentMonth = month;
		inst._selectedYear = inst._currentYear = year;
		var selectedDay = inst._currentDay;
		var selectedMonth = inst._currentMonth;
		var selectedYear = inst._currentYear;
		if(inst._selectedYear>1893 && inst._selectedYear<2044 && inst._get("calendric"))
		{
			var calendric = parseInt(inst._get("calendric"));
//			inst._selectedMonth = inst._drawMonth = inst._currentMonth = month;
//			inst._selectedYear = inst._drawYear = inst._currentYear = year;
			var lDate = gregorianToLocal(calendric, year, (month+1), inst._currentDay);
            /*selectedDay = lDate.day;
			selectedMonth = lDate.month-1;
			selectedYear = lDate.year;*/
		}
		if (inst._get('displaytime'))
		{
			inst._selectedHour = inst._currentHour = hour;
			inst._selectedMinute = inst._currentMinute = minute;
			inst._selectedSecond = inst._currentSecond = second;
			this._updateDatepicker(inst);
			
			if(inst._selectedYear > inst._drawYear)
			{
				$.datetimepicker._adjustDate(id,1,"M");
			}
			else if(inst._selectedYear < inst._drawYear)
			{
				$.datetimepicker._adjustDate(id,-1,"M");
			}
			else
			{
				if(inst._selectedMonth > inst._drawMonth)
				{
					$.datetimepicker._adjustDate(id,1,"M");
				}
				else if(inst._selectedMonth < inst._drawMonth)
				{
					$.datetimepicker._adjustDate(id,-1,"M");
				}
			}
		
			if($(td).is(":not('.datetimepicker_today')") && $(td).is(":not('.datetimepicker_currentDay')"))
				{
			    	return;
			   }
		}
		else
		{
			inst._selectedHour = inst._currentHour = 0;
			inst._selectedMinute = inst._currentMinute = 0;
			inst._selectedSecond = inst._currentSecond = 0;
		}
		this._selectDate(id, inst._formatDateTime(
				selectedDay, selectedMonth, selectedYear, inst._currentHour, inst._currentMinute, inst._currentSecond));
		if (this._stayOpen) {
			inst._endDay = inst._endMonth = inst._endYear = null;
			inst._rangeStart = new Date(inst._currentYear, inst._currentMonth, inst._currentDay);
			this._updateDatepicker(inst);
		}
		else if (rangeSelect) {
			inst._endDay = inst._currentDay;
			inst._endMonth = inst._currentMonth;
			inst._endYear = inst._currentYear;
			inst._selectedDay = inst._currentDay = inst._rangeStart.getDate();
			inst._selectedMonth = inst._currentMonth = inst._rangeStart.getMonth();
			inst._selectedYear = inst._currentYear = inst._rangeStart.getFullYear();
			inst._rangeStart = null;
			if (inst._inline)
				this._updateDatepicker(inst);
		}
	},

	/* Erase the input field and hide the date picker. */
	_clearDate: function(id) {
		var inst = this._getInst(id);
		if (inst._get('mandatory'))
			return;
		this._stayOpen = false;
		inst._endDay = inst._endMonth = inst._endYear = inst._rangeStart = null;
		this._selectDate(inst, '');
	},

	/* Update the input field with the selected date. */
	_selectDate: function(id, dateStr) {
		var inst = this._getInst(id);
		dateStr = (dateStr != null ? dateStr : inst._formatDateTime());
		if (inst._rangeStart)
			dateStr = inst._formatDateTime(inst._rangeStart) + inst._get('rangeSeparator') + dateStr;
		if (inst._input)
			inst._input.val(dateStr);
		var onSelect = inst._get('onSelect');
		if (onSelect)
			onSelect.apply((inst._input ? inst._input[0] : null), [dateStr, inst]);  // trigger custom callback
		else if (inst._input)
			inst._input.trigger('change'); // fire the change event
		if (inst._inline)
			this._updateDatepicker(inst);
		else if (!this._stayOpen) {
			if (! this._doNotHide) {
				this._hideDatepicker(null, inst._get('speed'));
				this._lastInput = inst._input[0];
				if (typeof(inst._input[0]) != 'object')
					inst._input[0].focus(); // restore focus
				this._lastInput = null;
			}
		}
		jBME.titleTipsPoper.hide();
	},
	
	/* Click event for Ok button */
	_selectDateByButton: function(id, day, year, month) {
		var inst = this._getInst(id);
		this._selectDate(id, inst._formatDateTime(
				day, year, month, inst._selectedHour, inst._selectedMinute, inst._selectedSecond));
	},

	/* Set as beforeShowDay function to prevent selection of weekends.
	   @param  date  Date - the date to customise
	   @return [boolean, string] - is this date selectable?, what is its CSS class? */
	noWeekends: function(date) {
		var day = date.getDay();
		return [(day > 0 && day < 6), ''];
	},
	
	/* Set as calculateWeek to determine the week of the year based on the ISO 8601 definition.
	   @param  date  Date - the date to get the week for
	   @return  number - the number of the week within the year that contains this date */
	iso8601Week: function(date) {
		var checkDate = new Date(date.getFullYear(), date.getMonth(), date.getDate(), (date.getTimezoneOffset() / -60));
		var firstMon = new Date(checkDate.getFullYear(), 1 - 1, 4); // First week always contains 4 Jan
		var firstDay = firstMon.getDay() || 7; // Day of week: Mon = 1, ..., Sun = 7
		firstMon.setDate(firstMon.getDate() + 1 - firstDay); // Preceding Monday
		if (firstDay < 4 && checkDate < firstMon) { // Adjust first three days in year if necessary
			checkDate.setDate(checkDate.getDate() - 3); // Generate for previous year
			return $.datetimepicker.iso8601Week(checkDate);
		} else if (checkDate > new Date(checkDate.getFullYear(), 12 - 1, 28)) { // Check last three days in year
			firstDay = new Date(checkDate.getFullYear() + 1, 1 - 1, 4).getDay() || 7;
			if (firstDay > 4 && (checkDate.getDay() || 7) < firstDay - 3) { // Adjust if necessary
				checkDate.setDate(checkDate.getDate() + 3); // Generate for next year
				return $.datetimepicker.iso8601Week(checkDate);
			}
		}
		return Math.floor(((checkDate - firstMon) / 86400000) / 7) + 1; // Weeks to given date
	},
	
	/* Provide status text for a particular date.
	   @param  date  the date to get the status for
	   @param  inst  the current datetimepicker instance
	   @return  the status display text for this date */
	dateStatus: function(date, inst) {
		return $.datetimepicker.formatDate(inst._get('dateStatus'), date, inst._getFormatConfig());
	},

	/* Parse a string value into a date object.
	   The format can be combinations of the following:
	   d  - day of month (no leading zero)
	   dd - day of month (two digit)
	   D  - day name short
	   DD - day name long
	   m  - month of year (no leading zero)
	   mm - month of year (two digit)
	   M  - month name short
	   MM - month name long
	   y  - year (two digit)
	   yy - year (four digit)
	   '...' - literal text
	   '' - single quote

	   @param  format           String - the expected format of the date
	   @param  value            String - the date in the above format
	   @param  settings  Object - attributes include:
	                     shortYearCutoff  Number - the cutoff year for determining the century (optional)
	                     dayNamesShort    String[7] - abbreviated names of the days from Sunday (optional)
	                     dayNames         String[7] - names of the days from Sunday (optional)
	                     monthNamesShort  String[12] - abbreviated names of the months (optional)
	                     monthNames       String[12] - names of the months (optional)
	   @return  Date - the extracted date value or null if value is blank */
	parseDate: function (format, value, settings) {
		if (format == null || value == null)
			throw 'Invalid arguments';
		value = (typeof value == 'object' ? value.toString() : value + '');
		if (value == '')
			return null;
		var shortYearCutoff = (settings ? settings.shortYearCutoff : null) || this._defaults.shortYearCutoff;
		var dayNamesShort = (settings ? settings.dayNamesShort : null) || this._defaults.dayNamesShort;
		var dayNames = (settings ? settings.dayNames : null) || this._defaults.dayNames;
		var monthNamesShort = (settings ? settings.monthNamesShort : null) || this._defaults.monthNamesShort;
		var monthNames = (settings ? settings.monthNames : null) || this._defaults.monthNames;
        var year = -1;
		var month = -1;
		var day = -1;
		var hour = -1;
		var minute = -1;
		var second = -1;
		var literal = false;
		// Check whether a format character is doubled
		var lookAhead = function(match) {
			var matches = (iFormat + 1 < format.length && format.charAt(iFormat + 1) == match);
			if (matches)
				iFormat++;
			return matches;	
		};
		// Extract a number from the string value
		var getNumber = function(match) {
			lookAhead(match);
			var size = (match == 'y' ? 4 : 2);
			var num = 0;
			while (size > 0 && iValue < value.length &&
					value.charAt(iValue) >= '0' && value.charAt(iValue) <= '9') {
				num = num * 10 + (value.charAt(iValue++) - 0);
				size--;
			}
			if (size == (match == 'y' ? 4 : 2))
				throw 'Missing number at position ' + iValue;
			return num;
		};
		// Extract a name from the string value and convert to an index
		var getName = function(match, shortNames, longNames) {
			var names = (lookAhead(match) ? longNames : shortNames);
			var size = 0;
			for (var j = 0; j < names.length; j++)
				size = Math.max(size, names[j].length);
			var name = '';
			var iInit = iValue;
			while (size > 0 && iValue < value.length) {
				name += value.charAt(iValue++);
				for (var i = 0; i < names.length; i++)
					if (name == names[i])
						return i + 1;
				size--;
			}
			throw 'Unknown name at position ' + iInit;
		};
		// Confirm that a literal character matches the string value
		var checkLiteral = function() {
			if (value.charAt(iValue) != format.charAt(iFormat))
				throw 'Unexpected literal at position ' + iValue;
			iValue++;
		};
		var iValue = 0;
		for (var iFormat = 0; iFormat < format.length; iFormat++) {
			if (literal)
				if (format.charAt(iFormat) == "'" && !lookAhead("'"))
					literal = false;
				else
					checkLiteral();
			else
				switch (format.charAt(iFormat)) {
					case 'h':
						hour = getNumber('h');
						break;
					case 'H':
						hour = getNumber('H');
						break;
					case 'i':
						minute = getNumber('i');
						break;
					case 's':
						second = getNumber('s');
						break;
					case 'd':
						day = getNumber('d');
						break;
					case 'D': 
						getName('D', dayNamesShort, dayNames);
						break;
					case 'm': 
						month = getNumber('m');
						break;
					case 'M':
						month = getName('M', monthNamesShort, monthNames); 
						break;
					case 'y':
						year = getNumber('y');
						break;
					case "'":
						if (lookAhead("'"))
							checkLiteral();
						else
							literal = true;
						break;
					default:
						checkLiteral();
				}
		}
		if (year < 100) {
			year += new Date().getFullYear() - new Date().getFullYear() % 100 +
				(year <= shortYearCutoff ? 0 : -100);
		}
		var date;
		if(second!=-1&&hour!=-1&&minute!=-1)
		{
			date = new Date(year, month - 1, day,hour,minute,second);
		}
		else
		{
			date = new Date(year, month - 1, day);
		}
		if (date.getFullYear() != year || date.getMonth() + 1 != month || date.getDate() != day) {
			throw 'Invalid date'; // E.g. 31/02/*
		}
		return date;
	},
	parseTime : function (format, value)
	{
		var thisArray = value.split(":");
		
		var hour = thisArray[0] == undefined ? "00" : thisArray[0];
		var minute = thisArray[1] == undefined ? "00" : thisArray[1];
		var second = thisArray[2] == undefined ? "00" : thisArray[2];
		
		return new Date(1970, 1, 1, hour, minute, second);
	}, 
	/* Format a date object into a string value.
	   The format can be combinations of the following:
	   d  - day of month (no leading zero)
	   dd - day of month (two digit)
	   D  - day name short
	   DD - day name long
	   m  - month of year (no leading zero)
	   mm - month of year (two digit)
	   M  - month name short
	   MM - month name long
	   y  - year (two digit)
	   yy - year (four digit)
	   '...' - literal text
	   '' - single quote

	   @param  format    String - the desired format of the date
	   @param  date      Date - the date value to format
	   @param  settings  Object - attributes include:
	                     dayNamesShort    String[7] - abbreviated names of the days from Sunday (optional)
	                     dayNames         String[7] - names of the days from Sunday (optional)
	                     monthNamesShort  String[12] - abbreviated names of the months (optional)
	                     monthNames       String[12] - names of the months (optional)
	   @isLocal  display as local, use local month string
	   @return  String - the date in the above format */
	formatDate: function (format, date, settings, isLocal) {
		if (!date)
			return '';
		var dayNamesShort = (settings ? settings.dayNamesShort : null) || this._defaults.dayNamesShort;
		var dayNames = (settings ? settings.dayNames : null) || this._defaults.dayNames;
		var monthNamesShort = (settings ? settings.monthNamesShort : null) || this._defaults.monthNamesShort;
		var monthNames = (settings ? settings.monthNames : null) || this._defaults.monthNames;
		// Check whether a format character is doubled
		var lookAhead = function(match) {
			var matches = (iFormat + 1 < format.length && format.charAt(iFormat + 1) == match);
			if (matches)
				iFormat++;
			return matches;	
		};
		// Format a number, with leading zero if necessary
		var formatNumber = function(match, value) {
			return (lookAhead(match) && value < 10 ? '0' : '') + value;
		};
		// Format a name, short or long as requested
		var formatName = function(match, value, shortNames, longNames) {
			return (lookAhead(match) ? longNames[value] : shortNames[value]);
		};
		
		if(isLocal){
			format=format.replace(/m/g,"M");
		}
		
		var output = '';
		var literal = false;
		if (date) {
			for (var iFormat = 0; iFormat < format.length; iFormat++) {
				if (literal)
					if (format.charAt(iFormat) == "'" && !lookAhead("'"))
						literal = false;
					else
						output += format.charAt(iFormat);
				else
					switch (format.charAt(iFormat)) {
						case 'h':
							output += formatNumber('h', date.getHours());
							break;
						case 'H':
							output += formatNumber('H', date.getHours());
							break;
						case 'i':
							output += formatNumber('i', date.getMinutes());
							break;
						case 's':
							output += formatNumber('s', date.getSeconds());
							break;
						case 'd':
							output += formatNumber('d', date.getDate()); 
							break;
						case 'D':                         
							output += formatName('D', date.getDay(), dayNamesShort, dayNames);
							break;
						case 'm': 
							output += formatNumber('m', date.getMonth() + 1); 
                            //alert(output);
							break;
						case 'M':
                        
							output += formatName('M', date.getMonth(), monthNamesShort, monthNames); 
                            //output += date.getMonth();
                            
							break;
						case 'y':
							output += (lookAhead('y') ? date.getFullYear() : 
								(date.getYear() % 100 < 10 ? '0' : '') + date.getYear() % 100);
							break;
						case "'":
							if (lookAhead("'"))
								output += "'";
							else
								literal = true;
							break;
						default:
							output += format.charAt(iFormat);                           
                            
					}
			}
		}
		return output;
	},

	formatYearMonth: function(format, dateStr) {

	},

	/* Extract all possible characters from the date format. */
	_possibleChars: function (format) {
		var chars = '';
		var literal = false;
		for (var iFormat = 0; iFormat < format.length; iFormat++)
			if (literal)
				if (format.charAt(iFormat) == "'" && !lookAhead("'"))
					literal = false;
				else
					chars += format.charAt(iFormat);
			else
				switch (format.charAt(iFormat)) {
					case 'd' || 'm' || 'y':
						//case 'd': case 'm': case 'y': case 'H': case 'i': case 'H':
						chars += '0123456789'; 
						break;
					case 'D' || 'M':
						return null; // Accept anything
					case "'":
						if (lookAhead("'"))
							chars += "'";
						else
							literal = true;
						break;
					default:
						chars += format.charAt(iFormat);
				}
		return chars;
	}
});

/* Individualised settings for date picker functionality applied to one or more related inputs.
   Instances are managed and manipulated through the Datepicker manager. 
   *
   * @ settings={ dateFormat:as yyyy-mm-dd hh:mm:ss -  highest }
   */
function DateTimepickerInstance(settings, inline) {
	if(settings.dateFormat){
		settings.dateFormat = matchDateFormat(settings.dateFormat);
	}
	if(settings.localFormat){
		settings.localFormat = matchDateFormat(settings.localFormat);
	}
	//alert(settings.dateFormat);
	this._id = $.datetimepicker._register(this);
	this._selectedDay = 0; // Current date for selection
	this._selectedMonth = 0; // 0-11
	this._selectedYear = 0; // 4-digit year
	this._selectedHour = 0;
	this._selectedMinute = 0;
	this._selectedSecond = 0;
	this._drawMonth = 0; // Current month at start of datetimepicker
	this._drawYear = 0;
	this._drawHour = 0;
	this._drawMinute = 0;
	this._drawSecond = 0;
	this._input = null; // The attached input field
	this._inline = inline; // True if showing inline, false if used in a popup
	this._datetimepickerDiv = (!inline ? $.datetimepicker._datetimepickerDiv :
		$('<div id="datetimepicker_div_' + this._id + '" class="datetimepicker_inline">'));
	//alert(this._datetimepickerDiv.parent());
	if(!this._datetimepickerDiv.parent().is("body"))
	{
		$(document.body).append($.datetimepicker._datetimepickerDiv);
		$(document).mousedown($.datetimepicker._checkExternalClick);
	}
    
	//this.poper = new jBME.Poper($(this._datetimepickerDiv));
    //this.poper.mask = true;
	//var poper = this.poper; 
//	this.poper.wrapperFunc = function()
//	{
//		var jqWrapper = poper.createShadowWrapper();
//		jqWrapper.addClass("bf_shadow_datetime");
//		return jqWrapper;
//	};
	// customise the date picker object - uses manager defaults if not overridden
	this._settings = extendRemove(settings || {}); // clone
	if (inline)
		this._setDate(this._getDefaultDate());
	return;
}

$.extend(DateTimepickerInstance.prototype, {
	/* Get a setting value, defaulting if necessary. */
	_get: function(name) {
		return this._settings[name] !== undefined ? this._settings[name] : $.datetimepicker._defaults[name];
	},

	/* Parse existing date and initialise date picker. */
	_setDateFromField: function(input) {
		this._input = $(input);
		var dateFormat;
			dateFormat = this._get('dateFormat');
		var dates = this._input ? this._input.val().split(this._get('rangeSeparator')) : null; 
		this._endDay = this._endMonth = this._endYear = null;
		var date = defaultDate = this._getDefaultDate();
		if (dates.length > 0) {
			var settings = this._getFormatConfig();
			if (dates.length > 1) {
				date = $.datetimepicker.parseDate(dateFormat, dates[1], settings) || defaultDate;
				this._endDay = date.getDate();
				this._endMonth = date.getMonth();
				this._endYear = date.getFullYear();
			}
			try {
				date = $.datetimepicker.parseDate(dateFormat, dates[0], settings) || defaultDate;
			} catch (e) {
				$.datetimepicker.log(e);
				date = defaultDate;
			}
		}
		
		this._selectedDay = date.getDate();
		this._drawMonth = this._selectedMonth = date.getMonth();
		this._drawYear = this._selectedYear = date.getFullYear();
		this._drawHour = this._selectedHour = date.getHours();
		this._drawMinute = this._selectedMinute = date.getMinutes();
		this._drawSecond = this._selectedSecond = date.getSeconds();
		this._currentDay = (dates[0] ? date.getDate() : 0);
		this._currentMonth = (dates[0] ? date.getMonth() : 0);
		this._currentYear = (dates[0] ? date.getFullYear() : 0);
		this._adjustDate();
	},
	
	/* Retrieve the default date shown on opening. */
	_getDefaultDate: function() {
		var date = this._determineDate('defaultDate', new Date());
		var minDate = this._getMinMaxDate('min', true);
		var maxDate = this._getMinMaxDate('max');
		date = (minDate && date < minDate ? minDate : date);
		date = (maxDate && date > maxDate ? maxDate : date);
		return date;
	},

	/* A date may be specified as an exact value or a relative one. */
	_determineDate: function(name, defaultDate) {
		var offsetNumeric = function(offset) {
			var date = new Date();
			date.setDate(date.getDate() + offset);
			return date;
		};
		var offsetString = function(offset, getDaysInMonth) {
			var date = new Date();
			var matches = /^([+-]?[0-9]+)\s*(d|D|w|W|m|M|y|Y)?$/.exec(offset);
			if (matches) {
				var year = date.getFullYear();
				var month = date.getMonth();
				var day = date.getDate();
				switch (matches[2] || 'd') {
					case 'd' : case 'D' :
						day += (matches[1] - 0); break;
					case 'w' : case 'W' :
						day += (matches[1] * 7); break;
					case 'm' : case 'M' :
						month += (matches[1] - 0); 
						day = Math.min(day, getDaysInMonth(year, month));
						break;
					case 'y': case 'Y' :
						year += (matches[1] - 0);
						day = Math.min(day, getDaysInMonth(year, month));
						break;
				}
				date = new Date(year, month, day);
			}
			return date;
		};
		var date = this._get(name);
		return (date == null ? defaultDate :
			(typeof date == 'string' ? offsetString(date, this._getDaysInMonth) :
			(typeof date == 'number' ? offsetNumeric(date) : date)));
	},

	/* Set the date(s) directly. */
	_setDate: function(date, endDate) {
		this._selectedDay = this._currentDay = date.getDate();
		this._drawMonth = this._selectedMonth = this._currentMonth = date.getMonth();
		this._drawYear = this._selectedYear = this._currentYear = date.getFullYear();
		this._drawHour = this._selectedHour = this._currentHour = date.getHours();
		this._drawMinute = this._selectedMinute = this._currentMinute = date.getMinutes();
		this._drawSecond = this._selectedSecond = this._currentSecond = date.getSeconds();
		if (this._get('rangeSelect')) {
			if (endDate) {
				this._endDay = endDate.getDate();
				this._endMonth = endDate.getMonth();
				this._endYear = endDate.getFullYear();
			} else {
				this._endDay = this._currentDay;
				this._endMonth = this._currentMonth;
				this._endYear = this._currentYear;
			}
		}
		this._adjustDate();
	},

	/* Retrieve the date(s) directly. */
	_getDate: function() {
		var startDate = (!this._currentYear || (this._input && this._input.val() == '') ? null :
			new Date(this._currentYear, this._currentMonth, this._currentDay));
		if (this._get('rangeSelect')) {
			return [startDate, (!this._endYear ? null :
				new Date(this._endYear, this._endMonth, this._endDay))];
		} else
			return startDate;
	},
	
	/* Generate the HTML for the current state of the date picker. */
	_generateDatepicker: function() {
		var today = new Date();
		today = new Date(today.getFullYear(), today.getMonth(), today.getDate()); // clear time
		var showStatus = this._get('showStatus');
		var isRTL = this._get('isRTL');
		// build the date picker HTML
		var clear = (this._get('mandatory') ? '' :
			'<div class="datetimepicker_clear"><a onclick="jQuery.datetimepicker._clearDate(' + this._id + ');"' + 
			(showStatus ? this._addStatus(this._get('clearStatus') || '&#xa0;') : '') + '>' +
			this._get('clearText') + '</a></div>');
		var controls = '<div class="datetimepicker_control">' + (isRTL ? '' : clear) +
			'<div class="datetimepicker_close"><a onclick="jQuery.datetimepicker._hideDatepicker();"' +
			(showStatus ? this._addStatus(this._get('closeStatus') || '&#xa0;') : '') + '>' +
			this._get('closeText') + '</a></div>' + (isRTL ? clear : '')  + '</div>';
		var prompt = this._get('prompt');
		var closeAtTop = this._get('closeAtTop');
		var closeHide = this._get('closeHide');
		var hideIfNoPrevNext = this._get('hideIfNoPrevNext');
		var numMonths = this._getNumberOfMonths();
		var stepMonths = this._get('stepMonths');
		var isMultiMonth = (numMonths[0] != 1 || numMonths[1] != 1);
		var minDate = this._getMinMaxDate('min', true);
		var maxDate = this._getMinMaxDate('max');
		var drawDay = this._selectedDay;
		var drawMonth = this._drawMonth;  
		var drawYear = this._drawYear;
		var drawHour = this._drawHour;
		var drawMinute = this._drawMinute;
		var drawSecond = this._drawSecond;
		var showYear = drawYear;
		var showMonth = drawMonth;
		var showDay = drawDay;
		if (maxDate) {
			var maxDraw = new Date(maxDate.getFullYear(),
				maxDate.getMonth() - numMonths[1] + 1, maxDate.getDate());
			maxDraw = (minDate && maxDraw < minDate ? minDate : maxDraw);
			while (new Date(drawYear, drawMonth, 1) > maxDraw) {
				drawMonth--;
				if (drawMonth < 0) {
					drawMonth = 11;
					drawYear--;
				}
			}
		}
		var prev = '<div class="datetimepicker_prev">' + 
			//prev year
			(this._canAdjustMonth(-12, drawYear, drawMonth) ? 
			'<span class="datetimepicker_prev_year"><a onclick="jQuery.datetimepicker._adjustDate(' + this._id + ', -' + 12 + ', \'M\');"' +
			(showStatus ? this._addStatus(this._get('prevStatus') || '&#xa0;') : '') + 
			'title="' + this._get('prevYearText') + '"' + 
			'>' +
			'</a></span>' :
			(hideIfNoPrevNext ? '' : '<span class="datetimepicker_prev_year"><a class="prev_disabled"></a></span>')) + 
			//prev month
			(this._canAdjustMonth(-1, drawYear, drawMonth) ? 
			'<a onclick="jQuery.datetimepicker._adjustDate(' + this._id + ', -' + stepMonths + ', \'M\');"' +
			(showStatus ? this._addStatus(this._get('prevStatus') || '&#xa0;') : '') + 
			'title="' + this._get('prevText') + '"' + 
			'>' +
			'</a>' :
			(hideIfNoPrevNext ? '' : '<a class="prev_disabled"></a>')) + '</div>';
		var next = '<div class="datetimepicker_next">' + 
			//next month
			(this._canAdjustMonth(+1, drawYear, drawMonth) ? 
			'<a onclick="jQuery.datetimepicker._adjustDate(' + this._id + ', +' + stepMonths + ', \'M\');"' +
			(showStatus ? this._addStatus(this._get('nextStatus') || '&#xa0;') : '') + 
			'title="' + this._get('nextText') + '"' + 
			'>' +
			'</a>' :
			(hideIfNoPrevNext ? '' : '<a class="next_disabled"></a>')) +
			//next year
			(this._canAdjustMonth(+12, drawYear, drawMonth) ? 
			'<span class="datetimepicker_next_year"><a onclick="jQuery.datetimepicker._adjustDate(' + this._id + ', +' + 12 + ', \'M\');"' +
			(showStatus ? this._addStatus(this._get('nextStatus') || '&#xa0;') : '') + 
			'title="' + this._get('nextYearText') + '"' + 
			'>' +
			'</a></span>' :
			(hideIfNoPrevNext ? '' : '<span class="datetimepicker_next_year"><a class="next_disabled"></a></span>')) + 
			'</div>';
		var html = (prompt ? '<div class="datetimepicker_prompt">' + prompt + '</div>' : '') +
			(closeAtTop && !this._inline && !closeHide ? controls : '') +
			'<div class="datetimepicker_links">' + (isRTL ? next : prev) +
			'<div class="datetimepicker_current">' + 
			this._generateMonthYearHeader(drawSecond,drawMinute,drawHour,drawMonth, drawYear, minDate, maxDate, selectedDate, row > 0 || col > 0) + // draw month headers
			'</div>' +
			(isRTL ? prev : next) + '</div>';
		var showWeeks = this._get('showWeeks');
		for (var row = 0; row < numMonths[0]; row++)
			for (var col = 0; col < numMonths[1]; col++) {
				var selectedDate = new Date(drawYear, drawMonth, this._selectedDay, drawHour, drawMinute, drawSecond);
				var selectedDateLocalCache={};
				html += '<div class="datetimepicker_oneMonth' + (col == 0 ? ' datetimepicker_newRow' : '') + '">' +
					'<table class="datetimepicker" cellpadding="0" cellspacing="0"><thead>' + 
					'<tr class="datetimepicker_titleRow">' +
					(showWeeks ? '<td>' + this._get('weekHeader') + '</td>' : '');
				var firstDay = this._get('firstDay');
				var changeFirstDay = this._get('changeFirstDay');
				var dayNames = this._get('dayNames');
				var dayNamesShort = this._get('dayNamesShort');
				var dayNamesMin = this._get('dayNamesMin');
				for (var dow = 0; dow < 7; dow++) { // days of the week
					var day = (dow + firstDay) % 7;
					var status = this._get('dayStatus') || '&#xa0;';
					status = (status.indexOf('DD') > -1 ? status.replace(/DD/, dayNames[day]) :
						status.replace(/D/, dayNamesShort[day]));
					html += '<td' + ((dow + firstDay + 6) % 7 >= 5 ? ' class="datetimepicker_weekEndCell"' : '') + '>' +
						(!changeFirstDay ? '<span' :
						'<a onclick="jQuery.datetimepicker._changeFirstDay(' + this._id + ', ' + day + ');"') + 
						(showStatus ? this._addStatus(status) : '') + ' title="' + dayNames[day] + '">' +
						dayNamesMin[day] + (changeFirstDay ? '</a>' : '</span>') + '</td>';
				}
				html += '</tr></thead><tbody>';
				var daysInMonth = this._getDaysInMonth(drawYear, drawMonth);
				if (drawYear == this._selectedYear && drawMonth == this._selectedMonth) {
					drawDay = this._selectedDay = Math.min(this._selectedDay, daysInMonth);
				}
				var leadDays = (this._getFirstDayOfMonth(drawYear, drawMonth) - firstDay + 7) % 7;
				var currentDate = (!this._currentDay ? new Date(9999, 9, 9) :
					new Date(this._currentYear, this._currentMonth, this._currentDay));
				var endDate = this._endDay ? new Date(this._endYear, this._endMonth, this._endDay) : currentDate;
				var printDate = new Date(drawYear, drawMonth, 1 - leadDays);
				var numRows = (isMultiMonth ? 6 : Math.ceil((leadDays + daysInMonth) / 7)); // calculate the number of rows to generate
				//GCU LocalCalendard
				var isLocal = false;
				var local_printDate; 
				var localDateCache;
				if(drawYear>1893 && drawYear<2044 && this._get("calendric"))
				{
					isLocal = true;
					var calendric = parseInt(this._get("calendric"));
					var localDate = gregorianToLocal(calendric, this._selectedYear || drawYear, (this._selectedYear?this._selectedMonth:drawMonth)+1, drawDay); //10-31
					selectedDateLocalCache={
							year:localDate.year,
							month:localDate.month,
							day:localDate.day
					};
					var gDateOfFirst = localToGregorian(calendric, localDate.year, localDate.month, 1);
					var gYearOfFirst = gDateOfFirst.year;
					var gMonthOfFirst = gDateOfFirst.month;
					var gDayOfFirst = gDateOfFirst.day;
					var local_daysInMonth = getMonthDays(calendric, localDate.year, localDate.month);
					var local_leadDays = (new Date(gYearOfFirst, gMonthOfFirst-1, gDayOfFirst).getDay() - firstDay + 7) % 7;
					local_printDate = new Date(gYearOfFirst, gMonthOfFirst-1, gDayOfFirst - leadDays);
					
				}
				var beforeShowDay = this._get('beforeShowDay');
				var showOtherMonths = this._get('showOtherMonths');
				var calculateWeek = this._get('calculateWeek') || $.datetimepicker.iso8601Week;
				var dateStatus = this._get('statusForDate') || $.datetimepicker.dateStatus;
				for (var dRow = 0; dRow < numRows; dRow++) { // create date picker rows
					html += '<tr class="datetimepicker_daysRow">' +
						(showWeeks ? '<td class="datetimepicker_weekCol">' + calculateWeek(printDate) + '</td>' : '');
					for (var dow = 0; dow < 7; dow++) { // create date picker days
						var daySettings = (beforeShowDay ?
							beforeShowDay.apply((this._input ? this._input[0] : null), [printDate]) : [true, '']);
						var otherMonth = (printDate.getMonth() != drawMonth);
						var unselectable = !daySettings[0] ||
							(minDate && printDate < minDate) || (maxDate && printDate > maxDate);
						var localDay,printDay,localMonth,printMonth,localYear,printYear; 
						
						localDay=printDay = printDate.getDate();
						localMonth=printMonth = drawMonth;
						localYear=printYear = drawYear;
						if(isLocal)
						{
							var printLocalDate = gregorianToLocal(calendric, printDate.getFullYear(),(printDate.getMonth()+1),printDate.getDate());
							localDay = printLocalDate.day;
							localMonth = printLocalDate.month;
							localYear = printLocalDate.year;
						}
						
						//var titleStr=$.datetimepicker.formatDate(this._get('dateFormat').split(" ")[0], new Date(printDate.getFullYear(), printDate.getMonth(), printDay), this._getFormatConfig());
                        var titleStr="";
						var localdatestr="";
						if(this._get("displayLocal") && isLocal){
							localdatestr = $.datetimepicker.formatDate((this._get('localFormat') || this._get('dateFormat')).split(" ")[0], new Date(localYear,localMonth-1,localDay), this._getFormatConfig(), true)
							//localdatestr = '<br>'+this._get('pickertitle')+': '+ localdatestr;
						}
						
						titleStr += localdatestr;
					    var engDate =(printDate.getMonth()+1)+"/" +printDay+"/"+printDate.getFullYear();
						html += '<td class="datetimepicker_daysCell setDate' +
							((dow + firstDay + 6) % 7 >= 5 ? ' datetimepicker_weekEndCell' : '') + // highlight weekends
							(otherMonth ? ' datetimepicker_otherMonth' : '') + // highlight days from other months
							(printDate.getYear() == selectedDate.getYear() &&  printDate.getMonth() == selectedDate.getMonth() &&  printDate.getDate() == selectedDate.getDate()&& drawMonth == this._selectedMonth ?
							' datetimepicker_currentDay' : '') + // highlight selected day
							(unselectable ? ' datetimepicker_unselectable' : '') +  // highlight unselectable days
							(otherMonth && !showOtherMonths ? '' : ' ' + daySettings[1] + // highlight custom dates
							(new Date(9999, 9, 9).getTime()==currentDate.getTime() && printDate.getTime() == today.getTime() ? ' datetimepicker_today' : '')) + '"' + // highlight today (if different)
							(unselectable ? '' : ' onmouseover="jQuery(this).addClass(\'datetimepicker_daysCellOver\');' +
							(!showStatus || (otherMonth && !showOtherMonths) ? '' : 'jQuery(\'#datetimepicker_status_' +
							this._id + '\').html(\'' + (dateStatus.apply((this._input ? this._input[0] : null),
							[printDate, this]) || '&#xa0;') +'\');') + '"' +
							' onmouseout="jQuery(this).removeClass(\'datetimepicker_daysCellOver\');' +
							(!showStatus || (otherMonth && !showOtherMonths) ? '' : 'jQuery(\'#datetimepicker_status_' +
							this._id + '\').html(\'&#xa0;\');') + '"' +
							' onclick="jQuery.datetimepicker._selectDay(' +
							this._id + ',' + (printDate.getMonth()) + ',' + printDate.getFullYear() + ', ' + drawHour + ', ' + drawMinute + ', ' + drawSecond + ', this);"') + 
							'>' + // actions
							'<span day= "'+localDay+'" month="'+localMonth+'" year="'+localYear+'"engDate="'+engDate+'"><a value="'+printDate.getDate()+'">' + 
							printDay + ((isLocal && this._get("displayLocal"))?'<div class="secondary">'+localDay+'</div>':'') + 
							'</a>'; 
						html+='</span></td>'; // display for this month
						printDate.setDate(printDate.getDate() + 1);
					}
					html += '</tr>';
				}
				drawMonth++;
				if (drawMonth > 11) {
					drawMonth = 0;
					drawYear++;
				}
				html += '</tbody></table>';
				if (this._get('displaytime'))
				{
					var drawHourStr = drawHour<10?'0'+drawHour:drawHour;
					var drawMinuteStr = drawMinute<10?'0'+drawMinute:drawMinute;
					var drawSecondStr = drawSecond<10?'0'+drawSecond:drawSecond;
					html += '<div class="nofloat"></div>';
					html += '<div class="datetimepicker_time">';
					html += '<div class="datetimepicker_selecteddate">'+$.datetimepicker.formatDate(this._get('dateFormat').split(" ")[0], new Date(this._currentYear==0?showYear:this._currentYear,this._currentYear==0?showMonth:this._currentMonth, this._currentDay==0?showDay:this._currentDay), this._getFormatConfig());
					if(this._get("displayLocal") && isLocal){
						var calendric = parseInt(this._get("calendric"));
						var localDate = gregorianToLocal(calendric, this._selectedYear || drawYear, (this._selectedYear?this._selectedMonth:drawMonth)+1, drawDay); //10-31
						localDate={
								year:localDate.year,
								month:localDate.month,
								day:localDate.day
						};
						var tempstr=$.datetimepicker.formatDate((this._get('localFormat') || this._get('dateFormat')).split(" ")[0], new Date(selectedDateLocalCache.year,selectedDateLocalCache.month-1,selectedDateLocalCache.day), this._getFormatConfig(),true);
						html += '<div>'+tempstr+'</div>';
					}
					
					html +='</div>';
					if(this._get("enableDsl")){
						//html+='<div class="datetimepicker_dsl"><label><input type="checkbox"/>Dsl</label></div>';
					}
					html += '<div class="datetimepicker_timepicker"><input type="hidden" value="'+drawHourStr+':'+drawMinuteStr+':'+drawSecondStr+'" /></div>';
					//add by hujia start
					html += '<div class="nofloat"></div>';
					//add by hujia end
					html += '</div>';
					
					html += '<div class="nofloat"></div>';
					html += '<div class="datetimepicker_button">';
					html += '<span class="bc_btn bc" onclick="jQuery.datetimepicker._selectDateByButton('+this._id+', '+
							this._selectedDay+', '+this._selectedMonth+', '+this._selectedYear+');">'+this._get('okLabel')+'</span>';
					html += '<span class="bc_btn bc" onclick="$.datetimepicker._hideDatepicker();"><div><div>'+this._get('cancelLabel')+'</div></div></span>';
					html += '</div>';
					html += '<div class="nofloat"></div>';
				}
//				html += '<div class="footerspace">&nbsp;</div></div>';
			    html += '</div>';
			}
		html += (showStatus ? '<div style="clear: both;"></div><div id="datetimepicker_status_' + this._id + 
			'" class="datetimepicker_status">' + (this._get('initStatus') || '&#xa0;') + '</div>' : '') +
			(!closeAtTop && !this._inline && !closeHide ? controls : '') +
			'<div style="clear: both;"></div>' + 
			($.browser.msie && parseInt($.browser.version) < 7 && !this._inline ? 
			'<iframe src="javascript:false;" class="datetimepicker_cover"></iframe>' : '');
		return html;
	},

	/* Generate the month and year header. */
	_generateMonthYearHeader: function(drawSecond,drawMinute,drawHour,drawMonth, drawYear, minDate, maxDate, selectedDate, secondary) {
		minDate = (this._rangeStart && minDate && selectedDate < minDate ? selectedDate : minDate);
		var showStatus = this._get('showStatus');
		var html = '<div class="datetimepicker_header">';
		// month selection
		var monthNames = this._get('monthNames');
		
		var printYear = drawYear;
		var printMonth = drawMonth;
		var localYear=printYear;
		var localMonth=printMonth;
		if(drawYear>1893 && drawYear<2044 && this._get("calendric")) {
			var calendric = parseInt(this._get("calendric"));
			var localDate = gregorianToLocal(calendric, drawYear, drawMonth, this._selectedDay); //公历转换本地历法
			localMonth = localDate.month-1;
			localYear = localDate.year;
		}
		if (secondary || !this._get('changeMonth'))
			html += monthNames[printMonth] + '&#xa0;';
			
		else {
			var inMinYear = (minDate && minDate.getFullYear() == drawYear);
			var inMaxYear = (maxDate && maxDate.getFullYear() == drawYear);
			html += '<select class="datetimepicker_newMonth" ' +
				'onchange="jQuery.datetimepicker._selectMonthYear(' + this._id + ', this, \'M\');" ' +
				'onclick="jQuery.datetimepicker._clickMonthYear(' + this._id + ');"' +
				(showStatus ? this._addStatus(this._get('monthStatus') || '&#xa0;') : '') + '>';
			for (var month = 0; month < 12; month++) {
				if ((!inMinYear || month >= minDate.getMonth()) &&
						(!inMaxYear || month <= maxDate.getMonth())) {
					html += '<option  value="' + month + '"' +
						(month == printMonth ? ' selected="selected"' : '') +
						'>' + (month+1) + '</option>';
				}
			}
			html += '</select>&nbsp;';
		}
		// year selection
		if (secondary || !this._get('changeYear'))
			html += printYear;
		else {
			// determine range of years to display
			var years = this._get('yearRange').split(':');
			var year = 0;
			var endYear = 0;
			if (years.length != 2) {
				year = printYear - 10;
				endYear = printYear + 10;
			} else if (years[0].charAt(0) == '+' || years[0].charAt(0) == '-') {
				year = printYear + parseInt(years[0], 10);
				endYear = printYear + parseInt(years[1], 10);
			} else {
				year = parseInt(years[0], 10);
				endYear = parseInt(years[1], 10);
			}
			year = (minDate ? Math.max(year, minDate.getFullYear()) : year);
			endYear = (maxDate ? Math.min(endYear, maxDate.getFullYear()) : endYear);
			html += '<select class="datetimepicker_newYear" ' +
				'onchange="jQuery.datetimepicker._selectMonthYear(' + this._id + ', this, \'Y\');" ' +
				'onclick="jQuery.datetimepicker._clickMonthYear(' + this._id + ');"' +
				(showStatus ? this._addStatus(this._get('yearStatus') || '&#xa0;') : '') + '>';
			for (; year <= endYear; year++) {
				html += '<option value="' + year + '"' +
					(year == printYear ? ' selected="selected"' : '') +
					'>' + year + '</option>';
			}
			html += '</select>';
		}
		
		html += '</div>'; // Close datetimepicker_header
		return html;
	},

	/* Provide code to set and clear the status panel. */
	_addStatus: function(text) {
		return ' onmouseover="jQuery(\'#datetimepicker_status_' + this._id + '\').html(\'' + text + '\');" ' +
			'onmouseout="jQuery(\'#datetimepicker_status_' + this._id + '\').html(\'&#xa0;\');"';
	},

	_adjustDate: function(offset, period) {
		var year = this._drawYear + (period == 'Y' ? offset : 0);
		var month = this._drawMonth + (period == 'M' ? offset : 0);
		var day = Math.min(this._selectedDay, this._getDaysInMonth(year, month)) +
			(period == 'D' ? offset : 0);
		var hour = this._drawHour + (period == 'H' ? offset : 0);
		var minute = this._drawMinute + (period == 'I' ? offset : 0);
		var second = this._drawSecond + (period == 'S' ? offset : 0);
		var date = new Date(year, month, day, hour, minute, second);
		// ensure it is within the bounds set
		var minDate = this._getMinMaxDate('min', true);
		var maxDate = this._getMinMaxDate('max');
		date = (minDate && date < minDate ? minDate : date);
		date = (maxDate && date > maxDate ? maxDate : date);
		this._selectedDay = date.getDate();
		this._drawMonth = this._selectedMonth = date.getMonth();
		this._drawYear = this._selectedYear = date.getFullYear();
		this._drawHour = this._selectedHour = date.getHours();
		this._drawMinute = this._selectedMinute = date.getMinutes();
		this._drawSecond = this._selectedSecond = date.getSeconds();
	},
	
	/* Determine the number of months to show. */
	_getNumberOfMonths: function() {
		var numMonths = this._get('numberOfMonths');
		return (numMonths == null ? [1, 1] : (typeof numMonths == 'number' ? [1, numMonths] : numMonths));
	},

	/* Determine the current maximum date - ensure no time components are set - may be overridden for a range. */
	_getMinMaxDate: function(minMax, checkRange) {
		var date = this._determineDate(minMax + 'Date', null);
		if (date) {
			date.setHours(0);
			date.setMinutes(0);
			date.setSeconds(0);
			date.setMilliseconds(0);
		}
		return date || (checkRange ? this._rangeStart : null);
	},

	/* Find the number of days in a given month. */
	_getDaysInMonth: function(year, month) {
		return 32 - new Date(year, month, 32).getDate();
	},

	/* Find the day of the week of the first of a month. */
	_getFirstDayOfMonth: function(year, month) {
		return new Date(year, month, 1).getDay();
	},

	/* Determines if we should allow a "next/prev" month display change. */
	_canAdjustMonth: function(offset, curYear, curMonth) {
		var numMonths = this._getNumberOfMonths();
		//var date = new Date(curYear, curMonth + (offset < 0 ? offset : numMonths[1]), 1);
		var date = new Date(curYear, curMonth + offset, 1);
		if (offset < 0)
			date.setDate(this._getDaysInMonth(date.getFullYear(), date.getMonth()));
		return this._isInRange(date);
	},

	/* Is the given date in the accepted range? */
	_isInRange: function(date) {
		// during range selection, use minimum of selected date and range start
		var newMinDate = (!this._rangeStart ? null :
			new Date(this._selectedYear, this._selectedMonth, this._selectedDay));
		newMinDate = (newMinDate && this._rangeStart < newMinDate ? this._rangeStart : newMinDate);
		var minDate = newMinDate || this._getMinMaxDate('min');
		var maxDate = this._getMinMaxDate('max');
		return ((!minDate || date >= minDate) && (!maxDate || date <= maxDate));
	},
	
	/* Provide the configuration settings for formatting/parsing. */
	_getFormatConfig: function() {
		var shortYearCutoff = this._get('shortYearCutoff');
		shortYearCutoff = (typeof shortYearCutoff != 'string' ? shortYearCutoff :
			new Date().getFullYear() % 100 + parseInt(shortYearCutoff, 10));
		return {shortYearCutoff: shortYearCutoff,
			dayNamesShort: this._get('dayNamesShort'), dayNames: this._get('dayNames'),
			monthNamesShort: this._get('monthNamesShort'), monthNames: this._get('monthNames')};
	},

	/* Format the given date for display. */
	_formatDateTime: function(day, month, year, hour, minute, second) {
		hour=hour||0; 
		minute=minute||0;
		second=second||0;
		if (!day) {
			this._currentDay = this._selectedDay;
			this._currentMonth = this._selectedMonth;
			this._currentYear = this._selectedYear;
			this._currentHour = this._selectedHour; 
			this._currentMinute = this._selectedMinute;
			this._currentSecond = this._selectedSecond;
		}
		var date = (day ? (typeof day == 'object' ? day : new Date(year, month, day, hour, minute, second)) :
			new Date(this._currentYear, this._currentMonth, this._currentDay, this._currentHour, this._currentMinute, this._currentSecond));
	
		return $.datetimepicker.formatDate(this._get('dateFormat'), date, this._getFormatConfig());
	}
});

/* jQuery extend now ignores nulls! */
function extendRemove(target, props) {
	$.extend(target, props);
	for (var name in props)
		if (props[name] == null)
			target[name] = null;
	return target;
};

/* Invoke the datetimepicker functionality.
   @param  options  String - a command, optionally followed by additional parameters or
                    Object - settings for attaching new datetimepicker functionality
   @return  jQuery object */
$.fn.datetimepicker = function(options){
	var otherArgs = Array.prototype.slice.call(arguments, 1);
	if (typeof options == 'string' && (options == 'isDisabled' || options == 'getDate')) {
		return $.datetimepicker['_' + options + 'Datepicker'].apply($.datetimepicker, [this[0]].concat(otherArgs));
	}
	return this.each(function() {
		typeof options == 'string' ?
			$.datetimepicker['_' + options + 'Datepicker'].apply($.datetimepicker, [this].concat(otherArgs)) :
			$.datetimepicker._attachDatepicker(this, options);
	});
};

$.datetimepicker = new DateTimepicker(); // singleton instance

})(jQuery);


/** Time picker **/
(function($) {
$.timePicker = function( s ) {
	var _defaults = {
		timeFormat: "hh:ii:ss",
		label: "hh:mm:ss",
		hourOptions: ["00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"],
		minuteOptions: ["00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"],
		secondOptions: ["00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"]
	};

	var container;
	var isOver = null;
	var hourObj;
	var hourInp;
	var hourBar;
	var hourSel;
	
	var minuteObj;
	var minuteInp;
	var minuteBar;
	var minuteSel;
	
	var secondObj;
	var secondInp;
	var secondBar;
	var secondSel;
	
	if(s.timeFormat)
	{
		s.timeFormat = s.timeFormat.replace("mm", "ii");
	}
	var conf = $.extend({}, _defaults, s);
	var targetInp = conf.target;

	init();

	return {
		setDefaults: _setDefaults
	};

	function _setDefaults( opts ) {
		conf = $.extend({}, _defaults, opts);
	}

	function init() {
		//targetInp.attr("type", "hidden");
		var labelArr = conf.label.split(":");
		var defaultV = targetInp.val()==""?conf.label:targetInp.val();
		var defaultVArr = defaultV.split(":");
		var timeFormatArr = conf.timeFormat.split(":");
		var labelObj = {};
		var defaultVObj = {};
		for(var i=0; i<timeFormatArr.length; i++)
		{
			labelObj[timeFormatArr[i]] = labelArr[i];
			defaultVObj[timeFormatArr[i]] = defaultVArr[i];
		}
		var newLabelArr = new Array();
		var newDefaultVArr = new Array();
		if(labelObj["hh"])
		{
			newLabelArr.push(labelObj["hh"]);
			newDefaultVArr.push(defaultVObj["hh"]);
		}
		else if(labelObj["HH"])
		{
			newLabelArr.push(labelObj["HH"]);
			newDefaultVArr.push(defaultVObj["HH"]);
		}
		else
		{
			newLabelArr.push("hh");
			newDefaultVArr.push("hh");
		}
		if(labelObj["ii"])
		{
			newLabelArr.push(labelObj["ii"]);
			newDefaultVArr.push(defaultVObj["ii"]);
		}
		else
		{
			newLabelArr.push("ii");
			newDefaultVArr.push("ii");
		}
		if(labelObj["ss"])
		{
			newLabelArr.push(labelObj["ss"]);
			newDefaultVArr.push(defaultVObj["ss"]);
		}
		else
		{
			newLabelArr.push("ss");
			newDefaultVArr.push("ss");
		}
		conf.label = newLabelArr.join(":");
		labelArr = newLabelArr;
		defaultVArr = newDefaultVArr;
		
		var targetId = targetInp.attr("id");
		var hourOptions = conf.hourOptions;
		var minuteOptions = conf.minuteOptions;
		var secondOptions = conf.secondOptions;
		
		var isDisabled = false;
		var disabledClass = "";
		if(targetInp.is(":disabled"))
		{
			isDisabled = true;
		}

		var html = '<ul class="timepicker" id="' + targetId + '_ul"';if(isDisabled){html+=' disabled="disabled" ';} html+= ' >';
		html += '	<li class="timepicker_hour">';
		html += '		<div id="hourSel" class="timepicker_selectlist">';
		var is12Hour = false;
		if(conf.timeFormat.indexOf('hh')!=-1)
		{
			is12Hour = true;
		}
		for(var i=0; i<hourOptions.length; i++)
		{
			if(is12Hour&&parseInt(hourOptions[i])>11)
			{
				break;
			}
			else
			{
				html += '			<a>'+hourOptions[i]+'</a>';
			}
		}
		var defaultHour = defaultVArr[0];
		if(is12Hour)
		{
			if(!isNaN(defaultHour))
			{
				if(parseInt(defaultHour)>11)
				{
					defaultHour -= 12;
					if(defaultHour<10)
					{
						defaultHour = '0' + defaultHour;
					}
					targetInp.val(defaultHour+":"+defaultVArr[1]+":"+defaultVArr[2]);
					conf.onchange.call(this, targetInp.val());
				}
			}
		}
 
		html += '		</div>';
		html += '		<input id="'+targetId+'HourInp" type="text" value="'+defaultHour+'" maxlength="2" '; if(isDisabled){html+=' disabled="disabled" ';} html+= ' />';
		html += '		<ins></ins>';
		html += '	</li>';
		html += '	<li class="timepicker_split">:</li>';
		html += '	<li class="timepicker_minute">';
		html += '		<div id="minuteSel" class="timepicker_selectlist">';
		for(var i=0; i<minuteOptions.length; i++)
		{
			html += '			<a>'+minuteOptions[i]+'</a>';
		}
		html += '		</div>';
		html += '		<span><input id="'+targetId+'MinuteInp" type="text" value="'+defaultVArr[1]+'" maxlength="2" '; if(isDisabled){html+=' disabled="disabled" ';} html+= ' /></span>';
		html += '		<ins></ins>';
		html += '	</li>';
		html += '	<li class="timepicker_split">:</li>';
		html += '	<li class="timepicker_second">';
		html += '		<div id="secondSel" class="timepicker_selectlist">';
		for(var i=0; i<secondOptions.length; i++)
		{
			html += '			<a>'+secondOptions[i]+'</a>';
		}
		html += '		</div>';
		html += '		<input id="'+targetId+'SecondInp" type="text" value="'+defaultVArr[2]+'" maxlength="2" '; if(isDisabled){html+=' disabled="disabled" ';} html+= ' />';
		html += '		<ins></ins>';
		html += '	</li>';
		//html += '	<li class="nofloat"></li>';
		html += '</ul>';
		//html += '<div class="nofloat"></div>';


		container = $(html).insertAfter(targetInp);
		hourObj = container.find("li.timepicker_hour");
		hourInp = hourObj.find("input:eq(0)");
		hourBar = hourObj.find("ins:eq(0)");
		hourSel = hourObj.find("div.timepicker_selectlist:eq(0)");
		var hourPoper = new jBME.Poper(hourSel);

		minuteObj = container.find("li.timepicker_minute");
		minuteInp = minuteObj.find("input:eq(0)");
		minuteBar = minuteObj.find("ins:eq(0)");
		minuteSel = minuteObj.find("div.timepicker_selectlist:eq(0)");
		var minutePoper = new jBME.Poper(minuteSel);
		
		secondObj = container.find("li.timepicker_second");
		secondInp = secondObj.find("input:eq(0)");
		secondBar = secondObj.find("ins:eq(0)");
		secondSel = secondObj.find("div.timepicker_selectlist:eq(0)");
		var secondPoper = new jBME.Poper(secondSel);

		hourInp.bind("paste",function(){return false;});
		minuteInp.bind("paste",function(){return false;});
		secondInp.bind("paste",function(){return false;});

		if(-1==conf.timeFormat.indexOf("hh")&&-1==conf.timeFormat.indexOf("HH"))
		{
			hourObj.next("li.timepicker_split").hide();
			hourObj.hide();
		}
		if(-1==conf.timeFormat.indexOf("ii"))
		{
			if(-1==conf.timeFormat.indexOf("ss"))
			{
				minuteObj.prev("li.timepicker_split").hide();
			}
			minuteObj.next("li.timepicker_split").hide();
			minuteObj.hide();
		}
		if(-1==conf.timeFormat.indexOf("ss"))
		{
			secondObj.prev("li.timepicker_split").hide();
			secondObj.hide();
		}

		if(labelArr[0]==hourInp.val())
		{
			hourInp.removeClass("_fill");
		}
		else
		{
			hourInp.addClass("_fill");
		}
		if(labelArr[1]==minuteInp.val())
		{
			minuteInp.removeClass("_fill");
		}
		else
		{
			minuteInp.addClass("_fill");
		}
		if(labelArr[2]==secondInp.val())
		{
			secondInp.removeClass("_fill");
		}
		else
		{
			secondInp.addClass("_fill");
		}

		$(document).bind("click", function(event) {
			if(!isOver)
			{
				hourPoper.hide();
				minutePoper.hide();
				secondPoper.hide();
			}
		});

		container.bind("mouseover", function() {
			isOver = true;
		});
		container.bind("mouseout", function() {
			isOver = false;
		});
		hourSel.bind("mouseover", function() {
			isOver = true;
		});
		hourSel.bind("mouseout", function() {
			isOver = false;
		});
		minuteSel.bind("mouseover", function() {
			isOver = true;
		});
		minuteSel.bind("mouseout", function() {
			isOver = false;
		});
		secondSel.bind("mouseover", function() {
			isOver = true;
		});
		secondSel.bind("mouseout", function() {
			isOver = false;
		});

		if(!isDisabled)
		{
//			hourObj.bind("mouseover", function() {
//				hourInp.focus();
//				if(labelArr[0]==hourInp.val())
//				{
//					hourInp.select();
//				}
//				hourBar.addClass("_normal");
//			});
//			hourObj.bind("mouseout", function() {
				//hourInp.blur();
//				hourBar.removeClass("_normal");
//				hourBar.removeClass("_over");
//			});
			hourObj.bind("click", function() {
				//hourInp.select();
				if(labelArr[0]==hourInp.val())
				{
					hourInp.val('');
				}
			});
			hourInp.bind("blur", function() {	
				timeCom(hourInp);
				if(""==hourInp.val())
				{
					hourInp.val(labelArr[0]);
				}
				if(labelArr[0]==hourInp.val()||""==hourInp.val())
				{
					hourInp.removeClass("_fill");
				}
				else
				{
					hourInp.addClass("_fill");
				}
			});
			hourInp.bind("focus", function() {
				minutePoper.hide();
				secondPoper.hide();
				hourPoper.hide();
				hourInp.select();
				if(labelArr[0]==hourInp.val())
				{
					hourInp.removeClass("_fill");
				}
				else
				{
					hourInp.addClass("_fill");
				}
			});
			hourBar.bind("mouseover", function() {
				$(this).addClass("_over");
			});
			hourBar.bind("mouseout", function() {
				$(this).removeClass("_over");
			});
			hourBar.bind("click", function(event) {
				hourPoper.show(hourObj);
				minutePoper.hide();
				secondPoper.hide();
//				event.stopPropagation();
			});
			hourSel.find("a").each(function() {
				var item = $(this);
				item.click(function() {
					hourInp.val(item.text());
					hourInp.select();
					hourPoper.hide();
					hourInp.addClass("_fill");
					updateTime();
				});
			});

//			minuteObj.bind("mouseover", function() {
//				minuteInp.focus();
//				if(labelArr[1]==minuteInp.val())
//				{
//					minuteInp.select();
//				}
//				minuteBar.addClass("_normal");
//			});
//			minuteObj.bind("mouseout", function() {
//				//minuteInp.blur();
//				minuteBar.removeClass("_normal");
//				minuteBar.removeClass("_over");
//			});
			minuteObj.bind("click", function() {
				//minuteInp.select();
				if(labelArr[1]==minuteInp.val())
				{
					minuteInp.val('');
				}
			});
			minuteInp.bind("blur", function() {
				timeCom(minuteInp);
				if(""==minuteInp.val())
				{
					minuteInp.val(labelArr[1]);
				}
				if(labelArr[1]==minuteInp.val()||""==minuteInp.val())
				{
					minuteInp.removeClass("_fill");
				}
				else
				{
					minuteInp.addClass("_fill");
				}
			});
			minuteInp.bind("focus", function() {
				hourPoper.hide();
				minutePoper.hide();
				secondPoper.hide();
				minuteInp.select();
				if(labelArr[1]==minuteInp.val())
				{
					minuteInp.removeClass("_fill");
				}
				else
				{
					minuteInp.addClass("_fill");
				}
			});
			minuteBar.bind("mouseover", function() {
				$(this).addClass("_over");
			});
			minuteBar.bind("mouseout", function() {
				$(this).removeClass("_over");
			});
			minuteBar.bind("click", function(event) {
				hourPoper.hide();
				minutePoper.show(minuteObj);
				secondPoper.hide();
//				event.stopPropagation();

			});
			minuteSel.find("a").each(function() {
				var item = $(this);
				item.click(function() {
					minuteInp.val(item.text());
					minuteInp.select();
					minutePoper.hide();
					minuteInp.addClass("_fill");
					updateTime();
				});
			});

//			secondObj.bind("mouseover", function() {
//				secondInp.focus();
//				if(labelArr[2]==secondInp.val())
//				{
//					secondInp.select();
//				}
//				secondBar.addClass("_normal");
//			});
//			secondObj.bind("mouseout", function() {
				//secondInp.blur();
//				secondBar.removeClass("_normal");
//				secondBar.removeClass("_over");
//			});
			secondObj.bind("click", function() {
				//secondInp.select();
				if(labelArr[2]==secondInp.val())
				{
					secondInp.val('');
				}
			});
			secondInp.bind("blur", function() {	
				timeCom(secondInp);
				if(""==secondInp.val())
				{
					secondInp.val(labelArr[2]);
				}
				if(labelArr[2]==secondInp.val()||""==secondInp.val())
				{
					secondInp.removeClass("_fill");
				}
				else
				{
					secondInp.addClass("_fill");
				}
			});
			secondInp.bind("focus", function() {
				hourPoper.hide();
				minutePoper.hide();
				secondPoper.hide();
				secondInp.select();
				if(labelArr[2]==secondInp.val())
				{
					secondInp.removeClass("_fill");
				}
				else
				{
					secondInp.addClass("_fill");
				}
			});
			secondBar.bind("mouseover", function() {
				$(this).addClass("_over");
			});
			secondBar.bind("mouseout", function() {
				$(this).removeClass("_over");
			});
			secondBar.bind("click", function(event) {
				hourPoper.hide();
				minutePoper.hide();
				secondPoper.show(secondObj);
//				event.stopPropagation();
			});
			secondSel.find("a").each(function() {
				var item = $(this);
				item.click(function() {
					secondInp.val(item.text());
					secondInp.select();
					secondPoper.hide();
					secondInp.addClass("_fill");
					updateTime();
				});
			});

			hourInp.keypress(function(event) {
				var keycode = $.browser.mozilla?event.which:event.keyCode;
				
				if((keycode<48||keycode>57)&&keycode!=8&&keycode!=0)
				{
					return false;
				}
				if(hourInp.val()>23)
				{
					return false;
				}
			});
						
			hourInp.keyup(function(event) {
				var v = hourInp.val();
				var keycode = $.browser.mozilla?event.which:event.keyCode;
				
				if(v>23)
				{
					hourInp.val(parseInt(v/10));
					return false;
				}
				
				if(((keycode>=48&&keycode<=57)||(keycode>=96&&keycode<=105))&&hourInp.val().length>=2&&hourInp.val()!=labelArr[0])
				{
					hourInp.blur();
					minuteInp.focus();
					minuteInp.select();
					//return;
				}
				if(keycode==9)
				{
					//alert(event.shiftKey);
					hourInp.focus();
					hourInp.select();
					updateTime();
					return;
				}
				var newVal = hourInp.val().replace(/[^\d]/gim,"");
				hourInp.val(newVal);
				updateTime();
			});
			minuteInp.keypress(function(event) {
				var keycode = $.browser.mozilla?event.which:event.keyCode;
				if((keycode<48||keycode>57)&&keycode!=8&&keycode!=0)
				{
					return false;
				}
				if(minuteInp.val()>59)
				{
					return false;
				}
			});
			minuteInp.keyup(function(event) {
				var v = minuteInp.val();
				var keycode = $.browser.mozilla?event.which:event.keyCode;
				
				if(v>59)
				{
					minuteInp.val(parseInt(v/10));
					return false;
				}
				if(conf.timeFormat.indexOf('ss')==-1)
				{
					return;
				}
				if(((keycode>=48&&keycode<=57)||(keycode>=96&&keycode<=105))&&minuteInp.val().length>=2&&minuteInp.val()!=labelArr[1])
				{
					minuteInp.blur();
					secondInp.focus();
					secondInp.select();
					//return;
				}
				if(keycode==9)
				{
					//alert(event.shiftKey);
					minuteInp.focus();
					minuteInp.select();
					updateTime();
					return;
				}
				var newVal = minuteInp.val().replace(/[^\d]/gim,"");
				minuteInp.val(newVal);
				updateTime();
			});
			secondInp.keypress(function(event) {
				var keycode = $.browser.mozilla?event.which:event.keyCode;
				if((keycode<48||keycode>57)&&keycode!=8&&keycode!=0)
				{
					return false;
				}
				if(secondInp.val()>59)
				{
					return false;
				}
			});
			secondInp.keyup(function(event) {
				var v = secondInp.val();
				if(v>59)
				{
					secondInp.val(parseInt(v/10));
					return false;
				}
				var keycode = $.browser.mozilla?event.which:event.keyCode;
				if(keycode==9)
				{
					secondInp.focus();
					secondInp.select();
					updateTime();
					return;
				}
				var newVal = secondInp.val().replace(/[^\d]/gim,"");
				secondInp.val(newVal);
				updateTime();
			});
		}
	}

	function updateTime() {
		var val = new Array();
		var labelArr = conf.label.split(":");
		var hourVal = hourInp.val();
		var minuteVal = minuteInp.val();
		var secondVal = secondInp.val();
		hourVal = hourVal.length != 2 ? "0" + hourVal : hourVal;
		minuteVal = minuteVal.length != 2 ? "0" + minuteVal : minuteVal;
		secondVal = secondVal.length != 2 ? "0" + secondVal : secondVal;
		var valStr = "";
		
		if((""==hourVal||hourVal==labelArr[0])&&""==(minuteVal||minuteVal==labelArr[1])&&(""==secondVal||secondVal==labelArr[2]))
		{
			valStr = "";
		}
		else
		{
			hourVal = (hourVal==""||hourVal==labelArr[0])?"00":hourVal;
			minuteVal = (minuteVal==""||minuteVal==labelArr[1])?"00":minuteVal;
			secondVal = (secondVal==""||secondVal==labelArr[2])?"00":secondVal;
			if(-1!=conf.timeFormat.indexOf("hh")||-1!=conf.timeFormat.indexOf("HH"))
			{
				val.push(hourVal);
			}
			if(-1!=conf.timeFormat.indexOf("ii"))
			{
				val.push(minuteVal);
			}
			if(-1!=conf.timeFormat.indexOf("ss"))
			{
				val.push(secondVal);
			}
			valStr = val.join(":");
		}
		targetInp.val(valStr);
		if(conf.onchange)
		{
			conf.onchange.call(this, targetInp.val());
		}
	}
	function timeCom(timeobj)
	{
		if (timeobj.val().length!=2)
		{
			timeobj.val('0' + timeobj.val());
		}
		updateTime();		
	}
};


$.fn.timePicker = function( s ) {
	var $this = this.parent("div");
	var $modelinput = $this.find(":input");
	var conf = $.extend(
    {
		"target": $modelinput,
		/**
		 * @require validate plugin.
		 */
	     onchange : function()
	     {
    		$modelinput.valid();
	     }
	}, s);
	
	var object = $.timePicker( conf );
	
	// From BME: for tips and validate of time.
	var $viewinputs = $this.find(":input").not($modelinput[0]);
	$viewinputs.attr("bmeTipsId", $this.attr("id")).addClass($modelinput.attr("errorBorder"))
			   .attr("validaterules",$modelinput.attr("validaterules"));
	$viewinputs.attr("bmeTips", $modelinput.attr("bmeTips"));
	return object;
};
})(jQuery);