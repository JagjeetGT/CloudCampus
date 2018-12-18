/*公共函数*/
;(function($) 
{
//自动适应窗口大小
	$.resizeObjs = function() {
		return{
			arr: new Array(),
			fixSize: new Number(),
			init: function() {
				arr = new Array();
				fixSize = 0;
				startSize = {
					w: $(window).width(),
					h: $(window).height()
				};
			},
			reFixSize: function( h ) {
				fixSize += h;
			},
			getFixSize: function() {
				return fixSize;
			},
			get: function() {
				return arr;
			},
			push: function( objs ) {
				arr.push(objs);
			},
			shift: function( objs ) {
				var newArr = $.grep(arr, function(n, i) {
					return $(n.container).data("orderNum")==$(objs).data("orderNum");
				}, true);
				arr = newArr;
				//alert(arr);
			},
			doResize: function() {
				var dh = $(window).height() - startSize.h;
				var pluginbarPop = $("#pluginbarPop");
				var pluginHide = false;
				if("none"==pluginbarPop.css("display"))
				{
					pluginHide = true;
					pluginbarPop.show();
				}
				$(arr).each(function(i) {
					var container = $(this.container);
					

					if("none"==container.css("display"))
					{
						container.show();
						container.css("height", container.height() + dh);
						if(this.child)
						{
							var ifra = container.find("iframe:first-child");
							ifra.css("height", ifra.outerHeight() + dh);
						}
						container.hide();
					}
					else if(container.attr("active")!="false")
					{
						container.css("height", container.height() + dh);
						if(this.child)
						{
							var ifra = container.find("iframe:first-child");
							ifra.css("height", ifra.outerHeight() + dh);
						}
					}
				});
				if(pluginHide)
				{
					pluginHide = false;
					pluginbarPop.hide();
				}
				startSize.h = $(window).height();

				//计算tab内容区域的宽度
				var dw = $(window).width() - startSize.w;

				var tabpageContent = $("#tabpageContent");
				tabpageContent.width(tabpageContent.width() + dw);
				startSize.w = $(window).width();

				var tabPageOverflow = $("#tabpageOverflow");
				tabPageOverflow.width(tabPageOverflow.width()+ dw);
				var horimenuContainer = $("#horimenuContainer");
				horimenuContainer.width(horimenuContainer.width() + dw);
				
				//在FF下让doResize方法执行两遍，是为了规避多出一个滚动条位置的问题
				if($.browser.mozilla)
				{
					if($.resizeObjs.second)
					{
						$.resizeObjs.second = false;
					}
					else
					{
						$.resizeObjs.second=true;
						setTimeout("$.resizeObjs().doResize()", 1);
					}
				}
			}
		};
		
};

//全选复选框
$.selectAll = {
	initEvent: function( areaId, allId) {
		var allCbox = $("#"+allId);
		var areaObj = $("#"+areaId);
		var cboxs = areaObj.find(":checkbox");
		cboxs.each(function(i) {
			var cbox = $(this);
			cbox.bind("click", function() {
				cbox.selectAllCbox( areaObj, allCbox);
			});
		});
		if(areaObj.find("#"+allId).attr("id")!=allId)
		{
			allCbox.bind("click", function() {
				allCbox.selectAllCbox( areaObj, allCbox);
			});
		}
	}
};
$.fn.selectAllCbox = function( areaObj, allCbox ) {
	var cboxObjs;
	if(this.attr("id")==allCbox.attr("id"))
	{
		cboxObjs = $(":checkbox", areaObj);
		if(this.attr("checked"))
		{
			cboxObjs.each( function(i) {
				this.checked = true;
			});
		}
		else
		{
			cboxObjs.each( function(i) {
				this.checked = false;
			});
		}
		return true;
	}
	else
	{
		cboxObjs = areaObj.find(":checkbox:not(:checked)");
		if(cboxObjs.length==0)
		{
			allCbox.attr("checked", true);
		}
		else
		{
			if(cboxObjs.length==1&&cboxObjs.attr("id")==allCbox.attr("id"))
			{
				allCbox.attr("checked", true);
			}
			else
			{
				allCbox.attr("checked", false);
			}
		}
		return true;
	}
};

//使对象垂直居中
//oy: 向上偏移量, 默认为0
//cid: 容器id，如果为空则默认为body
$.fn.alignMiddle = function( _oy, _cid ) {
	var _elm = this;
	toMiddle( _elm, _oy, _cid );
	function toMiddle( elm, oy, cid ) {
		var container;
		if(cid)
		{
			container = $('#'+cid);
		}
		else
		{
			container = $('body');
		}
		var offsetY = oy || 0;	//向上偏移量
		var ch = container.height();
		var eh = elm.height();
		var y;
		if(ch>eh+offsetY)
		{
			y = (ch-eh)/2 - offsetY/2;
		}
		elm.css('padding-top', y);
	}
	$(window).bind('resize', function() {
		toMiddle( _elm, _oy, _cid );
	});
};
})(jQuery);

//主框架布局组件
(function($) {
	$.fn.mainFrameLayout = function( s ) {
		var container = this;
		var conf = $.extend({}, s);
		var layoutHtml = "";
		for( var i=0; i<conf.columns.length; i++)
		{
			layoutHtml += "<div></div>";
			if(conf.columns[i].height>0)
			{
				var addH = conf.columns[i].height;
				$.resizeObjs().reFixSize(addH);
			}
		}
		var layouts = $(layoutHtml).appendTo(container);
		var wh = $(window).height();
		var fh = $.resizeObjs().getFixSize();
		layouts.each(function(i) {
			var layout = $(this);
			layout.attr("id", conf.columns[i].id)
					.css({width: conf.columns[i].width, height: conf.columns[i].height});
			if(conf.columns[i].autoSize)
			{
				var colH = wh-fh;
				layout.height(colH);
				$.resizeObjs().push({container:layout, child: false});
			}
		});
	};

})(jQuery);

(function($) {
	$.contents = function() {
		return {
			init: function() {
				var topHTML = "<div class='top_logo'></div>";
				if(typeof(sysmenuData)!="undefined"){
					topHTML += "<div id='systemMenu' class='sysmenu'>";
					topHTML += "</div>";
				}
				topHTML += "<div class='top_fun'>";
				topHTML += "<ul>";
				topHTML += "<li class='fun_logout' onclick='location.href=\""+logoutConf.lnk+"\"'>"+logoutConf.name+"</li>";
				topHTML += "<li class='top_fun_split'>&nbsp;|&nbsp;</li>";
				topHTML += "<li class='fun_help' id='"+helpConf.id+"' onclick='window.open(\""+helpConf.lnk+"\");'>"+helpConf.name+"</li>";
				if(!searchConf.isDisabled){
					var searchBox = $("#searchPopBox");
					searchBox.hide();
					topHTML += "<li class='top_fun_split'>&nbsp;|&nbsp;</li>";
					topHTML += "<li class='fun_search' id='"+searchConf.id+"' onmouseover='$.showSearchPop(this);'>"+searchConf.name+"</li>";
				}
				topHTML += "<li class='fun_user'>"+userConf.name+"</li>";
				topHTML += "</ul>";
				topHTML += "</div>";
				topHTML += "<div class='top_menu' id='topMenu'></div>";
				if(!alarmConf.isDisabled){
					topHTML += "<div class='top_alarm' id='"+alarmConf.id+"'>";
					if(alarmConf.children.length>0){
						for(var i=0; i<alarmConf.children.length;i++){
							topHTML +="<ul id='"+alarmConf.children[i].id+"'><li class='alarm_left'></li><li class='alarm_center'>"+alarmConf.children[i].name+"</li><li class='alarm_right'></li></ul>";
						}
					}
					topHTML += "</div>";
				}
				
				function childData(d){
					if((typeof d.children)!="undefined" && d.children.length >0 ){
						var This = d.children;
						$(This).each(function(i){
							if(this.isSelected && (typeof this.url)!="undefined" && "" != this.url){
								tabsData.data [tabsData.data.length] = {name:this.name,id:this.id.toString(),url:this.url,hideCloseIcon:this.hideCloseIcon};
							}
							childData(this);
						});
					}
				}
				
				$("#topFrame").append(topHTML);
				if(typeof(menuData) != "undefined"){
					$("#topMenu").horimenu(menuData);
					$(menuData.data).each(function(i){
						if((typeof this.isSelected)!="undefined" && this.isSelected && (typeof this.url)!="undefined" && "" != this.url){
							tabsData.data[tabsData.data.length] = {name:this.name,id:this.id.toString(),url:this.url,hideCloseIcon:this.hideCloseIcon};
						}
						childData(this);
					});
				}
				$("#horimenuContainer").width($(window).width()-406);
				if(typeof(sysmenuData)!="undefined"){
					$("#systemMenu").sysmenu(sysmenuData);
					$(sysmenuData.systemMenu).each(function(i){
						if((typeof this.isSelected)!="undefined" && this.isSelected && (typeof this.url)!="undefined" && "" != this.url){
							tabsData.data[tabsData.data.length] = {name:this.name,id:this.id.toString(),url:this.url,hideCloseIcon:this.hideCloseIcon};
						}
						childData(this);
					});
				}
				$("#middleFrame").tabPage(tabsData);
				$.plugin = $("#middleFrame").pluginBar(pluginData);
				$("#footerFrame").append("<div id='stateBar'></div>");
				$("#stateBar").stateBar();
			}
		};
	};
})(jQuery);

(function($) {
	$.showSearchPop = function(obj) {
		var actObj = $(obj);
		var searchBox = $("#searchPopBox");
		var offset = actObj.offset();
		var leftValue = offset.left - searchBox.width()+actObj.width()+30;
		
		searchBox.css({"top": offset.top-6,"left": leftValue});
		searchBox.show();
		
		searchBox.mouseover(function(){
			$(this).show();			
		});
		searchBox.mouseout(function(){
			$(this).hide();
		});
	};
})(jQuery);

//插件栏
(function($) {
	$.fn.pluginBar = function( s ) {
		var middleFrame = $(this);
		var pluginbar;
		var pluginbarContainer;
		var pluginbarPop;
		var pluginbarPopContent;
		var pluginbarPopHeader;
		var pluginbarPopTitle;
		var pluginbarPopFixBtn;		//弹出窗口固定按钮
		var pluginbarPopCloseBtn;	//弹出窗口关闭按钮
		var isFix = false;			//弹出窗口是否固定
		this.defaults = {
			align: "left",
			hidden: false
		};
		var conf = $.extend({}, this.defaults, s);
		init(conf.items);

		//addPlugin		添加插件
		//removePlugin	删除插件
		//newMsg		更新插件信息数字
		//rePosition	更改插件栏位置
		//hide			隐藏插件栏
		//show			显示插件栏
		return {
			add: addPlugin,
			remove: removePlugin,
			newMsg: newMsgFun,
			rePosition: rePosFun,
			hide: hideFun, 
			show: showFun
		};
		
		//添加插件
		function addPlugin( pdata ) {
			var showing = false;
			if(pluginbarPop.css("display")=="none")
			{
				pluginbarPop.show();
				showing = true;
			}
			//生成图标
			var itemObj = $("<div class='pluginbar_item'></div>");
			var itemIcon = $("<div class='pluginbar_item_icon'></div>");
			itemObj.append(itemIcon);
			itemObj.attr("id", pdata.id);
			itemIcon.css("background-image", "url("+pdata.icon+")");
			pluginbarContainer.append(itemObj);
			itemObj.bind("mouseover", function() {
				//showPop(itemObj, pdata);
			});
			itemObj.bind("mouseout", function() {
				//hidePop(itemObj);
			});
			itemObj.bind("click.showpop", function() {
				if(pluginbarPop.is(":visible")&&pluginbarPop.data("actObj")==itemObj)
				{
					hidePop(itemObj);
				}
				else
				{
					showPop(itemObj, pdata);
				}
			});
			//生成内容
			var itemContent = $("<div class='pluginbarPopContent_body'></div>");
			itemContent.height(pluginbar.height()-pluginbarPopHeader.height());
			pluginbarPopContent.append(itemContent);
			if(pdata.contentUrl&&!pdata.contentType)
			{
				var itemIfra = $("<iframe src='' frameborder='0' scrolling='auto' name='pluginIFra"+pdata.id+"' width='100%'></iframe>");
				itemIfra.appendTo(itemContent);
				itemIfra.attr("src", pdata.contentUrl)
					.attr("height", "100%");
			}
			if('SHORTCUTMENU'==pdata.contentType)
			{
				
				itemContent.shortcutMenu(shortcutMenuData);
			}
			$.resizeObjs().push({container:itemContent, child: false});
			if(showing)
			{
				pluginbarPop.hide();
			}

			//将插件内容与插件图标关连起来，已供显示内容时能找到对应的项
			itemObj.data("contentObj", itemContent);
			//默认隐藏内容
			//itemContent.hide();
		}
		
		//删除某插件
		function removePlugin( pid ) {
		}
		
		//更新某个插件的最新记录数
		function newMsgFun( pid, msgnumber ) {
		}
		
		//改变插件位置
		function rePosFun( align ) {
			var tabpageContent = $("#tabpageContent");
			if(align=="left")
			{
				pluginbar.css({"left": "0px", "right": "auto"});
				pluginbarPop.css({"left": "36px", "right": "auto"});
				pluginbarContainer.css("margin-left", "0px");
				tabpageContent.css("float", "right");
				pluginbar.removeClass("pluginbar_alignright");
				pluginbarPop.parent().removeClass("pluginbar_pop_alignright");
			}
			else
			{
				pluginbar.css({"left": "auto", "right": "0px"});
				pluginbarPop.css({"left": "auto", "right": "36px"});
				pluginbarContainer.css("margin-left", "-3px");
				tabpageContent.css("float", "left");
				pluginbar.addClass("pluginbar_alignright");
				pluginbarPop.parent().addClass("pluginbar_pop_alignright");
			}
		}
		
		//显示插件栏
		function showFun() {
			pluginbar.show();
			//计算tab内容区域的宽度
			var tabpageContent = $("#tabpageContent");
			if($.browser.msie)
			{
				tabpageContent.width($(window).width()-pluginbar.outerWidth()-1);
			}
			else
			{
				tabpageContent.width($(window).width()-pluginbar.outerWidth());
			}
			
			//主框架tab标签栏向右扩充
			$("#tabpageFix").removeClass("tabpage_for_pluginhidden");

		}
		
		//隐藏插件栏
		function hideFun() {
			pluginbar.hide();
			//如果弹出框为显示状态则隐藏弹出框
			if(isFix)
			{
				popfloat();
				pluginbarPop.hide();
			}
			else
			{
				pluginbarPop.hide();
				isFix = false;
			}
			//重新计算tab内容区域的宽度
			var tabpageContent = $("#tabpageContent");
			tabpageContent.width($(window).width());
			
			//主框架tab标签栏向左填充
			$("#tabpageFix").addClass("tabpage_for_pluginhidden");
		}

		function showPop(actObj, pdata) {
			var preActObj = pluginbarPop.data("actObj");
			if(preActObj)
			{
				preActObj.removeClass("pluginbar_item_on");
			}
			pluginbarPopTitle.html(pdata.name);
			actObj.addClass("pluginbar_item_on");
			pluginbarPop.data("actObj", actObj);
			pluginbarPop.show();
			//显示对象的插件内容
			pluginbarPop.find("div.pluginbarPopContent_body").hide();
			actObj.data("contentObj").show();
		}

		function hidePop(actObj) {
			if(!isFix)
			{
				pluginbarPop.hide();
				actObj.removeClass("pluginbar_item_on");
			}
		}

		function popfloat() {
			isFix = false;
			resizeLayout(-1);
			pluginbarPopFixBtn.attr("class", "float_pop");
			pluginbarPop.removeClass("pluginbarpop_fix");
		}

		function popfix() {
			isFix = true;
			resizeLayout(1);
			pluginbarPopFixBtn.attr("class", "fix_pop");
			pluginbarPop.addClass("pluginbarpop_fix");
		}

		//固定或取消固定插件栏弹出窗口时重置布局大小
		function resizeLayout(co) {
			var fixW = 196;
			var tabpageContent = $("#tabpageContent");
			tabpageContent.width(tabpageContent.width()-fixW*co);
			pluginbar.width(pluginbar.width()+fixW*co);
		}

		function init( itemsArr ) {
			pluginbar = $("<div id='pluginbar'></div>");
			pluginbarContainer = $("<div id='pluginbarContainer'><div class='pluginbar_top'></div><div class='pluginbar_bottom'></div></div>");
			pluginbar.append(pluginbarContainer);
			pluginbarPop = $("<div id='pluginbarPop' class='pluginbar_pop pluginbarpop_float'></div>");
			pluginbarPopContent = $("<div id='pluginbarPopContent'></div>");
			pluginbarPopHeader = $("<div class='pluginbarPopContent_header'></div>").appendTo(pluginbarPopContent);
			pluginbarPopTitle = $("<div class='pluginbarPopContent_header_left'></div>").appendTo(pluginbarPopHeader);
			var pluginbarPopRight = $("<div class='pluginbarPopContent_header_right'></div>").appendTo(pluginbarPopHeader);
			pluginbarPopFixBtn = $("<span class='float_pop'></span>").appendTo(pluginbarPopRight);
			pluginbarPopCloseBtn = $("<span class='close_pop'></span>").appendTo(pluginbarPopRight);
			pluginbarPop.append(pluginbarPopContent);
			var tabpageContent = $("#tabpageContent");
			tabpageContent.before(pluginbar);
			
			//计算弹出窗口位置
			var pluginbarPos = pluginbar.offset();
			var popLeft = (pluginbarPos.left+pluginbar.width())+"px";
			var popTop = (pluginbarPos.top-1) + "px";
			pluginbarPop.css({"position": "absolute", "left": popLeft, "top": popTop});
			pluginbarPop.hide();

			tabpageContent.after("<div class='nofloat'></div>");
			tabpageContent.after(pluginbarPop);
			
			//计算插件栏高度
			var fixH = $.resizeObjs().getFixSize();
			var ph = $(window).height()-fixH;
			pluginbar.height(ph);
			pluginbarPop.height(ph-1);
			$.resizeObjs().push({container:pluginbar, child: false});
			$.resizeObjs().push({container:pluginbarPop, child: false});
			
			//计算tab内容区域的宽度
			if($.browser.msie)
			{
				tabpageContent.width($(window).width()-pluginbar.outerWidth()-1);
			}
			else
			{
				tabpageContent.width($(window).width()-pluginbar.outerWidth());
			}
			
			//添加插件
			if(itemsArr&&itemsArr.length>0)
			{
				for(var i=0; i<itemsArr.length; i++)
				{
					var itemData = itemsArr[i];
					addPlugin(itemData);
				}
			}

			//鼠标在插件栏弹出窗口时保持打开状态
			pluginbarPop.bind("mouseover", function() {
				//var actObj = pluginbarPop.data("actObj");
				//actObj.mouseover();
			});
			//鼠标离开插件栏弹出窗口时隐藏弹出窗口
			pluginbarPop.bind("mouseout", function() {
				//var actObj = pluginbarPop.data("actObj");
				//if(!isFix)
				//{
				//	actObj.mouseout();
				//}
			});
			//固定弹出窗口按钮事件
			pluginbarPopFixBtn.bind("click", function() {
				if(isFix)
				{
					popfloat();
				}
				else
				{
					popfix();
				}
			});

			//关闭弹出窗口按钮事件
			pluginbarPopCloseBtn.bind("click", function() {
				if(isFix)
				{
					popfloat();
					pluginbarPop.hide();
				}
				else
				{
					pluginbarPop.hide();
					isFix = false;
				}

				var actObj = pluginbarPop.data("actObj");
				actObj.removeClass("pluginbar_item_on");
			});
			
			//初始化插件栏位置
			rePosFun(conf.align);
			//初始化插件栏状态
			if(conf.hidden)
			{
				hideFun();
			}
		}
	};
})(jQuery);

//常用菜单
(function($) {
	$.fn.shortcutMenu = function( s ) {
		return new shortcutMenu().init(this, s);
	};

	function shortcutMenu() {
		return {
			init: function(container, s) {
				var menu = $('<ul id="shortcutmenu" class="shortcutmenu"></ul>').appendTo(container);
				menu.data('saveUrl', s.saveUrl);
				menu.data('selectedData', []);
				var selectedList = s.selectedData;
				if(selectedList&&selectedList.length>0)
				{
					$(selectedList).each(function(i) {
						shortcutMenu().addItem(this);
					});
				}

				var bottom = $('<div class="shortcutmenu_bottom"></div>').appendTo(container);
				var setBtn = $('<span class="bc_btn bc_ui_ele"><div><div>'+s.setLabel+'</div></div></span>').appendTo(bottom);
				setBtn.bind('click', shortcutMenu().openCloseAll);
				
				var allContainer = $('<div id="shortcutmenuAll" class="shortcutmenu_all"></div>').prependTo(container);
				var content = $('<div class="shortcutmenu_all_content"></div>').appendTo(allContainer);
				var allList = s.allData.data;
				if(allList&&allList.length>0)
				{
					var group;
					$(allList).each(function(i) {
						var groupAtt = this;
						group = $('<div class="shortcutmenu_all_group"></div>').appendTo(content);
						var fa = $('<div class="shortcutmenu_all_fa">'+groupAtt.name+'</div>').appendTo(group);
						var groupList = $('<ul class="shortcutmenu_all_sub"></ul>').appendTo(group);
						if(groupAtt.children&&groupAtt.children.length>0)
						{
							$(groupAtt.children).each(function() {
								var itemAtt = this;
								var item = $('<li><label for='+itemAtt.id+'>'+itemAtt.name+'</label></li>').appendTo(groupList);
								var input = $('<input id="'+itemAtt.id+'" type="checkbox" value="'+itemAtt.id+'" />').prependTo(item);
								input.bind('click', function() {
									if(input.attr('checked'))
									{
										shortcutMenu().addItem(itemAtt);
									}
									else
									{
										shortcutMenu().removeItem(itemAtt.id);
									}
								});
							});
						}
						if(i>0&&i%3==0)
						{
							$('<div class="nofloat"></div>').appendTo(content);
						}
					});
					
					var cellWidth = group.outerWidth(true);
					if(allList.length>4)
					{
						allContainer.width(cellWidth*4);
					}
					else
					{
						allContainer.width(cellWidth*allList.length);
					}
				}
				else
				{
					setBtn.hide();
				}
				var allBottom = $('<div class="shortcutmenu_all_bottom"></div>').appendTo(allContainer);
				var okBtn = $('<span class="bc_btn bc_ui_ele"><div><div>'+s.okLabel+'</div></div></span>').appendTo(allBottom);
				okBtn.bind('click', shortcutMenu().okHandler);
				var cancelBtn = $('<span class="bc_btn bc_ui_ele"><div><div>'+s.cancelLabel+'</div></div></span>').appendTo(allBottom);
				cancelBtn.bind('click', shortcutMenu().cancelHandler);
				shortcutMenu().resize();
				allContainer.hide();
			},
			addItem: function(itemAtt) {
				var menu = $('#shortcutmenu');
				var item = $('<li id='+itemAtt.id+'>'+itemAtt.name+'</li>').appendTo(menu);
				var upBtn = $('<span class="shortcutmenu_up"></span>').prependTo(item);
				var delBtn = $('<span class="shortcutmenu_del"></span>').prependTo(item);
				item.bind('click', function() {
					var tabitem=$.fn.tabPage.add({
						name:itemAtt.name, 
						url:itemAtt.url, 
						id:itemAtt.id.toString()
					});
				});
				item.hover( 
					function() {
						$(this).addClass('shortcutmenu_over');
					}, 
					function() {
						$(this).removeClass('shortcutmenu_over');
					}
				);
				upBtn.bind('click', function() {
					if(item.prev().is('li'))
					{
						item.insertBefore(item.prev());
						item.mouseout();
					}
					else
					{
						item.appendTo(menu);
						item.mouseout();
					}
					shortcutMenu().saveState();
					return false;
				});
				delBtn.bind('click', function() {
					item.remove();
					shortcutMenu().saveState();
					return false;
				});
				var selectedData = menu.data('selectedData');
				selectedData.push(itemAtt);
				menu.data('selectedData', selectedData);
			},
			removeItem: function(id) {
				var menu = $('#shortcutmenu');
				var item = menu.find('li[id='+id+']');
				item.remove();
				
				var selectedData = menu.data('selectedData');
				var newSelectData = $.grep(selectedData, function(n,i){
					return n.id==id;
				}, true);
				menu.data('selectedData', newSelectData);
			},
			resize: function() {
				var pluginbar = $('#pluginbar');
				var allContainer = $('#shortcutmenuAll');
				var content = allContainer.find('div.shortcutmenu_all_content');
				if(allContainer.height()>pluginbar.height()-10)
				{
				   content.height(pluginbar.height()-50);
				}
			},
			openCloseAll: function() {
				var allContainer = $('#shortcutmenuAll');
				if(allContainer.is(':hidden'))
				{
					shortcutMenu().showAll();
				}
				else
				{
					shortcutMenu().hideAll();
				}
			},
			showAll: function() {
				var allContainer = $('#shortcutmenuAll');
				var poper = allContainer.data('poper');
				if (undefined == poper)
				{
					poper = new jBME.Poper(allContainer);
					poper.defaultWrap();
					poper.mask = true;
					allContainer.data('poper', poper);
				}
				var jqPluginbarPop = $("#pluginbarPop");
				var offset = jqPluginbarPop.offset();
				poper.showIn(offset.left + jqPluginbarPop.width(), offset.top - 2);
				shortcutMenu().resize();
				
				var menu = $('#shortcutmenu');
				allContainer.find('input:checked').attr('checked', false);
				menu.find('li').each(function() {
					var input = allContainer.find('input[id='+$(this).attr('id')+']');
					input.attr('checked', true);
				});
				
				var selectData = [];
				$(menu.data('selectedData')).each(function() {
					selectData.push(this);
				});
				menu.data('lastData', selectData);
			},
			hideAll: function() {
				var allContainer = $('#shortcutmenuAll');
				allContainer.data('poper').hide();
			},
			okHandler: function() {
				shortcutMenu().saveState();
				shortcutMenu().hideAll();
			},
			cancelHandler: function() {
				var menu = $('#shortcutmenu');
				var lastData = menu.data('lastData');
				menu.html('');
				menu.data('selectedData', []);
				$(lastData).each(function() {
					shortcutMenu().addItem(this);
				});
				shortcutMenu().hideAll();
			},
			saveState: function() {
				var ids = [];
				var menu = $('#shortcutmenu');
				var saveUrl = menu.data('saveUrl');
				menu.find('li').each(function() {
					ids.push($(this).attr('id'));
				});
				$.ajax({
					url: saveUrl,
					data: { "ids": ids.toString() }
				});
			}
		};
	}
})(jQuery);
//菜单组件
(function($) {
	$.fn.horimenu = function( s ) {
		return new horimenuComponent().init(this, s);
	};

	//$.fn.shortmenu = function( s ) {
	//	return new horimenuComponent().shortmenu(this, s);
	//};

	function horimenuComponent() {
		return {
			init: function(elem, s) {
				var menuHTML = "<div class='horimenu'>";
				menuHTML += "<div class='horimenu_bg_left'></div>";
				menuHTML += "<div class='horimenu_bg_center' id='horimenuContainer'></div>";
				menuHTML += "<div class='horimenu_bg_right'></div>";
				menuHTML += "<div id='menuTitleContainer'></div>";
				menuHTML += "<div id='submenuContainer'></div>";
				menuHTML += "</div>";
				$(elem).append(menuHTML);
				//$("#horimenuContainer").shortmenu(sysmenuData);
				$(s.data).each(function(i) {
					var titleItem = horimenuComponent().addMenu($("#horimenuContainer"), this, i);
					horimenuComponent().addSubTitle($("#menuTitleContainer"), this , i);
					horimenuComponent().addSubmenu($("#submenuContainer"), this.children, i);
					if(i<s.data.length-1)
					{
						horimenuComponent().addSplit(titleItem);
					}
				});
				//horimenuComponent().switchMenu(0);
			},
			shortmenu: function(elem, s ) {
				var htmlStr = "<ul class='horimenu_title_normal'>";
				htmlStr += "<li class='horimenu_title_left'></li>";
				htmlStr += "<li class='horimenu_title_center shortmenu'></li>";
				htmlStr += "<li class='horimenu_title_right'></li>";
				htmlStr += "</ul>";
				var shortmenuItem = $(htmlStr).prependTo(elem);
				shortmenuItem.click( function () {
					//horimenuComponent().switchMenu(index);
				});
				shortmenuItem.mouseover( function() {
					$(this).removeClass("horimenu_title_normal");
					$(this).addClass("horimenu_title_on");
					horimenuComponent().showSubMenu(0);
				});
				shortmenuItem.mouseout( function() {
					$(this).removeClass("horimenu_title_on");
					$(this).addClass("horimenu_title_normal");
					horimenuComponent().hideSubMenu(0);
				});

				horimenuComponent().addSubmenu($("#submenuContainer"), s, 0);
				
			},
			addMenu: function(ctnr, itemAtt, index) {
				var htmlStr = "<ul class='horimenu_title_normal title_ul' title='"+ itemAtt.name +"'>";
				htmlStr += "<li class='horimenu_title_left'></li>";
				htmlStr += "<li class='horimenu_title_center'><span>" + itemAtt.name + "</span></li>";
				htmlStr += "<li class='horimenu_title_right'></li>";
				htmlStr += "</ul>";
				var titleItem = $(htmlStr).appendTo(ctnr);
				
				titleItem.click( function () {
					if(itemAtt.url)
					{
						if(typeof(sysmenuData)!= "undefined"){
							var itemDataString = "{ id: '"+ itemAtt.id + "', label:'" + itemAtt.name+ "', url: '"+itemAtt.url+ "'}";
							$.fn.historyReport(itemDataString,sysmenuData);
						}
						var tabitem=$.fn.tabPage.add({
							name:itemAtt.name, 
							url:itemAtt.url, 
							id:itemAtt.id.toString(),
							helpid:itemAtt.helpid
						}); //20100831
					}
				});
				
				titleItem.mouseover( function() {  //鼠标经过菜单项时的触发动作
					$(this).removeClass("horimenu_title_normal");
					$(this).addClass("horimenu_title_on");
					if(itemAtt.children.length>0)
					{
						//horimenuComponent().showSubTitle(index);
						horimenuComponent().showSubMenu(index);
					}
				});
				
				titleItem.mouseout( function() {
					$(this).removeClass("horimenu_title_on");
					$(this).addClass("horimenu_title_normal");
					if(itemAtt.children.length>0)
					{
						//horimenuComponent().hideSubTitle(index);
						horimenuComponent().hideSubMenu(index);
					}
				});
				return titleItem;
			},
			addSubTitle: function(ctnr, item, index) {
				var menuItem = $("#horimenuContainer").find("ul.title_ul:eq("+index+")");
				var titleHtml = "<div style='position: absolute;'><ul class='horimenu_title_sub_on'><li class='horimenu_title_left'></li><li class='horimenu_title_center'><span>"+item.name+"</span></li><li class='horimenu_title_right'></li></ul></div>";
				var titleContainer = $(titleHtml).appendTo(ctnr);
				titleContainer.width(menuItem.width()+12);
				
				var offset = menuItem.offset();
				titleContainer.css("left", offset.left-6);
				
				titleContainer.mouseover( function() {
					horimenuComponent().showSubTitle(index);
					horimenuComponent().showSubMenu(index);
				});
				titleContainer.mouseout( function() {
					horimenuComponent().hideSubTitle(index);
					horimenuComponent().hideSubMenu(index);
				});

				titleContainer.hide();
			},
			addSubmenu: function(ctnr, itemAtt, index) {
				var menuItem = $("#horimenuContainer").find("ul.title_ul:eq("+index+")");
				var htmlStr = "<div style='position:absolute;'><div class='menu_masker'><iframe frameborder='0' src=''></iframe></div></div>";
				var subContainer = $(htmlStr).appendTo(ctnr);
				//htmlStr = "<div class='menu_shadow'></div>";
				//var shadow = $(htmlStr).appendTo(subContainer);
				htmlStr = "<div class='horimenu_sub'><div class='horimenuSub_top'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><div class='horimenuSub_top_left'></div></td>	<td class='horimenuSub_top_center'></td><td><div class='horimenuSub_top_right'></div></td></tr></table></div><div class='horimenuSub_center_left'><div class='horimenuSub_center_right'><div class='horimenuSub_center_center'><ul></ul></div><div class='porlet_nofloat'></div></div></div><div class='horimenuSub_bottom'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><div class='horimenuSub_bottom_left'></div></td><td class='horimenuSub_bottom_center'></td><td><div class='horimenuSub_bottom_right'></div></td></tr></table></div></div>";
				var contentItem = $(htmlStr).appendTo(subContainer);
				contentItem.attr("id", "horimenuSub"+index)
					.addClass("horimenu_sub");
				
				$(itemAtt).each(function () {
					horimenuComponent().addSubItem(contentItem.find("ul:eq(0)"), this, index);
				});
				var mwidth = $(contentItem).width();
				var mheight = $(contentItem).height();
				contentItem.find("li").each(function() {
					if((menuItem.width())>=(mwidth+11)){
						$(this).width(menuItem.width()+26);
						contentItem.width(menuItem.width()+37);
					}
					else{
						$(this).width(mwidth-11);
					}
					//$(this).width(mwidth - 11);
					$(this).find("a").css("display", "block");
				});
				var subMasker = subContainer.find("iframe:eq(0)");
				subMasker.width(mwidth+8);
				subMasker.height(mheight+8);
				//shadow.width(mwidth+3);
				//shadow.height(mheight+6);
				var offset = menuItem.offset();
				subContainer.css("left", offset.left-2);

				subContainer.mouseover( function() {
					menuItem.removeClass("horimenu_title_normal");
					menuItem.addClass("horimenu_title_on");
					//horimenuComponent().showSubTitle(index);
					horimenuComponent().showSubMenu(index);
				});
				subContainer.mouseout( function() {
					menuItem.removeClass("horimenu_title_on");
					menuItem.addClass("horimenu_title_normal");
					//horimenuComponent().hideSubTitle(index);
					horimenuComponent().hideSubMenu(index);
				});
				subContainer.hide();
			},
			addSubItem: function(ctnr, itemAtt, index)
			{
				var htmlStr = "<li><a>" + itemAtt.name + "</a></li>";
				var subItem = $(htmlStr).appendTo(ctnr);
				if(itemAtt.disabled)
				{
					subItem.attr('disabled', true);
				}
				subItem.click( function() {
					if(itemAtt.javascript)
					{
						itemAtt.javascript(itemAtt.url);
						return;
					}
					if(typeof(sysmenuData)!= "undefined"){
						var itemDataString = "{ id: '"+ itemAtt.id + "', label:'" + itemAtt.name+ "', url: '"+itemAtt.url+ "'}";
						$.fn.historyReport(itemDataString,sysmenuData);
					}
					var tabitem=$.fn.tabPage.add({
						name:itemAtt.name, 
						url:itemAtt.url, 
						id:itemAtt.id,
						helpid:itemAtt.helpid
					});  //20100831
					horimenuComponent().hideSubMenu(index);
				});
			},
			addSplit: function(titleItem)
			{
				var htmlStr = "<ul class='horimenu_title_split'><li></li></ul>";
				$(htmlStr).insertAfter(titleItem);
			},
			/*switchMenu: function( index ) {
				$("#horimenuContainer > ul").removeClass()
					.addClass("horimenu_title_normal");
				$("#horimenuContainer > ul:eq("+index+")").removeClass()
					.addClass("horimenu_title_on");
				$("#submenuContainer > div").hide();
				$("#submenuContainer > div:eq("+index+")").show();
			},*/
			showSubMenu: function( index ) {
				$("#submenuContainer > div:eq("+index+")").show();
			},
			hideSubMenu: function( index ) {
				$("#submenuContainer > div:eq("+index+")").hide();
			},
			showSubTitle: function(index) {
				$("#menuTitleContainer > div:eq("+index+")").show();
			},
			hideSubTitle: function(index) {
				$("#menuTitleContainer > div:eq("+index+")").hide();
			}
		};
	}
})(jQuery);

//系统菜单
(function($) {
	$.fn.sysmenu = function( s ) {
		var container = this;
		var titleItem = container;
		var menu;
		var menuList;
		var subMenu;
		
		/*var customBar;
		var tempHash = {};
		var listHash = {};		*/
		var delayCloseTimer = this.delayCloseTimer = null;  //菜单延迟关闭计时器
	    var closetime=0; //0毫秒后关闭
	    var singleBoxWidth = 240;
		var itemsHash = {};	
		var showSearch = s.hasSearch;
		var showHistory = s.showHistory;
		if(typeof(s.historyTitle) != "undefined"){
			var historyTitle = s.historyTitle;
		}
		else {
			var historyTitle = "The last history";
		}
		var itemArr = s.subProj;
		var systemMenu = s.systemMenu;
		titleItem.bind("click",function() {
			$(this).removeClass("sysmenu").addClass("sysmenu_hover");
			showmenu();
			var maskDiv = $("<div style='position: absolute; top: 0px; left: 0px;'></div>");
			maskDiv.width($("body").width());
			maskDiv.height($("body").height());
			maskDiv.insertBefore(menu);
			menu.data("maskDiv",maskDiv);
		});
		
		if(!menu)
		{
			createMenu(titleItem);
		}
		
		function showmenu() {
			menu.show();
			menu.find("div.shortsubmenu_sub").hide();
		}
		function hidemenu() {
			titleItem.removeClass("sysmenu_hover").addClass("sysmenu");
			menu.hide();
			menu.find("div.shortsubmenu_sub").hide();
			var maskDiv = menu.data("maskDiv");
			$(maskDiv).remove();
		}
		function changeItem(){
			titleItem.removeClass("sysmenu_hover").addClass("sysmenu");
			var maskDiv = menu.data("maskDiv");
			$(maskDiv).remove();
		}
		
		function createMenu(obj) {
			var menuHtml = "<div id='systemMenuContainer' onmouseover='$.menuEnter();' ><div class='shortmenu_sub'>";
			menuHtml += "<div class='menu_masker'><iframe width='0' height='0' frameborder='0' src=''></iframe></div>";
			menuHtml += "<div class='horimenuSub_top'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><div class='horimenuSub_top_left'></div></td><td class='horimenuSub_top_center'></td><td><div class='horimenuSub_top_right'></div></td></tr></table></div>";
			menuHtml += "<div class='horimenuSub_center_left'><div class='horimenuSub_center_right'><div class='horimenuSub_center_center'>";
			if(showSearch){
				menuHtml += "<div class='sysmenu_header'><div class='sysmenu_search'><div class='search_input'><input type='text' value='' name='systemSearch' id='systemSearch' class='system_text_input' /><input type='submit' value='' class='system_btn' /></div></div>";
				menuHtml += "<div class='sysmenu_map_tool'><div class='system_menu_close'></div><span>Site Map</span></div>";
				menuHtml += "<div class='nofloat'></div></div>";
			}
			else {
				menuHtml += "<div class='sysmenu_header'>";
				menuHtml += "<div class='sysmenu_map_tool'><div class='system_menu_close'></div></div>";
				menuHtml += "<div class='nofloat'></div></div>";
			}
			if(typeof(itemArr) != "undefined"){
				menuHtml += "<div class='shortmenu_proj_list'><ul class='shortmenu_proj_ul'></ul></div>";
			}
			if(showHistory){
				menuHtml += "<div class='shortmenu_his_list'><span class='history_title'>"+ historyTitle +"</span><ul class='shortmenu_his_ul'></ul></div>";
			}
			if(typeof(systemMenu) != "undefined"){
				menuHtml += "<div class='shortmenu_proj_items_list'><ul class='shortmenu_proj_itmes_ul'></ul></div>";
			}
			menuHtml += "<div class='porlet_nofloat'></div></div></div></div>";
			menuHtml += "<div class='horimenuSub_bottom'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><div class='horimenuSub_bottom_left'></div></td><td class='horimenuSub_bottom_center'></td><td><div class='horimenuSub_bottom_right'></div></td></tr></table>";
			menuHtml += "</div></div>";
			var menuEle = $(menuHtml);
			menuEle.appendTo("#topFrame");
			menu = menuEle;
			if(typeof(itemArr) != "undefined"){
				for(var i=0; i<itemArr.length; i++)
				{
					addMenuItem(itemArr[i]);
				}
			}
			if(typeof(systemMenu) != "undefined"){
				for(var i=0; i<systemMenu.length; i++)
				{
					addProjItem(systemMenu[i]);
				}
			}
			if(showHistory && $.cookie("historyItems") != null){
				var ulObj = menu.find("ul.shortmenu_his_ul");
				var historyStr = $.cookie("historyItems");
				var historyObj = eval('('+historyStr+')');
				if(ulObj.html() != ""){
					ulObj.html("");
				};

				for(var j = 0; j<historyObj.length; j++){
					addHistoryList(historyObj[j]);
				};	
			}			
				
			var offset = obj.offset();
			var searchBox = menu.find("div.sysmenu_header");
			var projBox = menu.find("div.shortmenu_proj_list");
			var projSubBox = menu.find("div.shortmenu_proj_items_list");
			var historyBox = menu.find("div.shortmenu_his_list");
			var boxW = 0;
			
			if(projBox.width() != null && projSubBox.width() != null && historyBox.width() == null){
				boxW = projBox.width() + projSubBox.width();
				if(projBox.height() > projSubBox.height()){
					projSubBox.addClass("list_noline");
					projSubBox.height(projBox.height());
				}
				else {
					projBox.addClass("list_noline");
					projBox.height(projSubBox.height());
				}				
			}
			if((projBox.width() == null || projSubBox.width() == null) && historyBox.width() == null){
				boxW = singleBoxWidth;
				if(projBox.width() == null){
					projSubBox.width(singleBoxWidth);
					projSubBox.addClass("list_noline");
				}
				else if(projSubBox.width() == null){
					projBox.width(singleBoxWidth);
					projBox.addClass("list_noline");
				}
			}
			if(historyBox.width() != null && projSubBox.width() != null && projBox.width() == null){
				boxW = historyBox.width() + projSubBox.width();
				if(historyBox.height() > projSubBox.height()){
					projSubBox.addClass("list_noline");
					//projSubBox.height(historyBox.height());
				}
				else {
					historyBox.addClass("list_noline");
					//historyBox.height(projSubBox.height());
				}
			}
			
			$("#systemMenuContainer").css({"top": (offset.top+obj.height()+2), "width": (boxW+18)});
			
			menu.find("div.system_menu_close").click(function(){
				hidemenu();			
			});
			if(typeof(systemMenu) != "undefined"){
				projSubBox.showSubMenu("sub_selected");
			}
		
			menu.hide();
			titleItem.ExternalClick(menu,changeItem);
		
			return menuEle;
		}
		function addHistoryList(itemData){
			var ulObj = menu.find("ul.shortmenu_his_ul");
			var itemHtml = "<li>";
			itemHtml += "<a href = '#none'>"+ itemData.label +"</a>";
			itemHtml += "</li>";
			
			var hisItem = $(itemHtml).prependTo(ulObj);
		
			hisItem.click(function(){
				var tabitem=$.fn.tabPage.add({
					name:itemData.label, 
					url:itemData.url, 
					id:itemData.id,
					helpid:itemData.helpid
				}); 
				hidemenu();
			});
		}
		function addMenuItem( itemAtt ) {
			var ul = menu.find("ul.shortmenu_proj_ul");
			var menuHtml = "<li>";

			menuHtml += "<a href='"+ itemAtt.url +"' target='_blank'><img src='"+$.trim(itemAtt.icon)+"' /></a>";
			menuHtml += "</li>";
			var menuItem = $(menuHtml).appendTo(ul);

			itemsHash[itemAtt.id] = menuItem;

			if(itemAtt.isdisabled)
			{
				menuItem.attr('disabled', true);
			}
			menuItem.click(function(){
				hidemenu();				
			});
			return menuItem;
		}
		function addProjItem( itemsAtt ) {
			var ul = menu.find("ul.shortmenu_proj_itmes_ul");
			
			if(itemsAtt.children.length>0){
				var menuHtml = "<li>";
				menuHtml += "<a href='#none' class='has_sub'>"+ itemsAtt.name +"</a>";
			}
			else {
				var menuHtml = "<li>";
				menuHtml += "<a href='#none'>"+ itemsAtt.name +"</a>";
			}
			menuHtml += "</li>";
			var projMenuItem = $(menuHtml).appendTo(ul);
			var itemw = $(ul).width();
			
			if(typeof(itemArr) != "undefined" || showHistory){
				projMenuItem.width(itemw + 2);	
			}
			else {
				projMenuItem.width(singleBoxWidth+7);
			}

			if(itemsAtt.children.length==0){
				projMenuItem.click( function() {
					hidemenu();
					if(typeof(sysmenuData)!= "undefined"){
						var itemDataString = "{ id: '"+ itemsAtt.id + "', label:'" + itemsAtt.name+ "', url: '"+itemsAtt.url+ "'}";
						$.fn.historyReport(itemDataString,sysmenuData);
					}
					var tabitem=$.fn.tabPage.add({
						name:itemsAtt.name, 
						url:itemsAtt.url, 
						id:itemsAtt.id,
						helpid:itemsAtt.helpid
					}); //20100831
				});
			}
			else {
				createSubMenu(projMenuItem,itemsAtt.children);
			}
			return projMenuItem;
		}
		function showSubMenu(actObj) {
			var subMenuContainer = $(actObj).next();
			subMenuContainer.bind("mouseover",function() {
				$(this).show();
				titleItem.removeClass("sysmenu").addClass("sysmenu_hover");
			});
			subMenuContainer.show();
		}
		function hideSubMenu(actObj) {
			var subMenuContainer = $(actObj).next();
			subMenuContainer.hide();
		}
		function createSubMenu(liObj,subItemsArr) {  
			var menuHtml = "<div class='shortsubmenu_sub'>";
			menuHtml += "<div class='menu_masker'><iframe width='0' height='0' frameborder='0' src=''></iframe></div>";
			menuHtml += "<div class='horimenuSub_top'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><div class='horimenuSub_top_left'></div></td><td class='horimenuSub_top_center'></td><td><div class='horimenuSub_top_right'></div></td></tr></table></div>";
			menuHtml += "<div class='horimenuSub_center_left'><div class='horimenuSub_center_right'><div class='horimenuSub_center_center'>";
			menuHtml += "<ul></ul>";
			menuHtml += "</div><div class='porlet_nofloat'></div></div></div>";
			menuHtml += "<div class='horimenuSub_bottom'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><div class='horimenuSub_bottom_left'></div></td><td class='horimenuSub_bottom_center'></td><td><div class='horimenuSub_bottom_right'></div></td></tr></table>";
			menuHtml += "</div>";
			var menuEle = $(menuHtml);
			menuEle.appendTo(liObj);
			var _subMenu = menuEle;
			for(var i=0; i<subItemsArr.length; i++)
			{
				addSubMenuItem(subItemsArr[i], _subMenu);
			}
			
			var projBox = menu.find("div.shortmenu_proj_list");
			var projSubBox = menu.find("div.shortmenu_proj_items_list");
			if($.browser.msie){
				if($.browser.version=="7.0" || $.browser.version=="6.0"){					
					var topDiff = -6;
				}
				if($.browser.version=="8.0")
					var topDiff = -5;
			}
			else {
				var topDiff = -4;
			}
			_subMenu.css({"left": $(_subMenu).parent().width()-12,"top": topDiff });
			_subMenu.hide();
		}
		function addSubMenuItem( itemAtt, _subMenu ) {
			var ul = _subMenu.find("ul:eq(0)");
			if(itemAtt.children && itemAtt.children.length > 0){
				var menuHtml = "<li>";
				menuHtml += "<a href='#none' class='has_sub'>"+ itemAtt.name +"</a>";
			}
			else {
				var menuHtml = "<li>";
				menuHtml += "<a href='#none'>"+ itemAtt.name +"</a>";
			}
			menuHtml += "</li>";
			var menuItem = $(menuHtml).appendTo(ul);
			menuItem.css({"width": (_subMenu.width()-11)});
			
			if(itemAtt.children && itemAtt.children.length > 0){
				createSubMenu(menuItem,itemAtt.children);
			}
			else {
				menuItem.click( function() {
					_subMenu.hide();
					hidemenu();
					if(typeof(sysmenuData)!= "undefined"){
						var itemDataString = "{ id: '"+ itemAtt.id + "', label:'" + itemAtt.name+ "', url: '"+itemAtt.url+ "'}";
						$.fn.historyReport(itemDataString,sysmenuData);
					}
					var tabitem=$.fn.tabPage.add({
						name:itemAtt.name, 
						url:itemAtt.url, 
						id:itemAtt.id,
						helpid:itemAtt.helpid
					}); //20100831
				});
			}
			
			//return menuItem;
		}
	};
})(jQuery);

(function($) {
	$.menuEnter = function() {
		var menu = $("#systemMenuContainer");
		var titleItem = $("#systemMenu");
		titleItem.removeClass("sysmenu").addClass("sysmenu_hover");
		//menu.show();
	};
})(jQuery);

//状态栏组件
(function($) {
	$.fn.stateBar = function() {
		//var conf = $.extend({}, s);
		var logininfoHtml = "";
		var toplnkHtml = "";
		$(this).append("<div id='loginInfo'></div><div id='alarmInfo'></div>");
		
		logininfoHtml += "<ul>";
		logininfoHtml += "<li class='login_user'>" + loginData.user + "</li>";
		logininfoHtml += "<li class='login_ip'>" + loginData.ipAddress + "</li>";
		//logininfoHtml += "<li>" + conf.loginDate + "</li>";
		logininfoHtml += "</ul>";
		$("#loginInfo").append(logininfoHtml);
		
		//toplnkHtml += "<ul>";
		//toplnkHtml += "<li>Logout</li>";
		//toplnkHtml += "<li>Help</li>";
		//toplnkHtml += "</ul>";
		
		$("#topLnk").append(toplnkHtml);
		
		$.fn.alarm();
		//$("#alarmInfo").everyTime(5000, 
		//				  	"alarmlight", 
		//				  	function(i) {
		//						$.fn.alarm();
		//					}, 
		//					0);
	};
	
	$.fn.alarm = function() {
		/*$.ajax({
		   //type: "POST",
		   url: "alarmlight.txt",
		   dataType: "jason",
		   success: showAlarm
	   })*/
		//$.getJSON(alarmData, showAlarm);
	};
	var showAlarm = function(alarmData) {
		//alert(alarmData.datas);
		var alarminfoHtml = "";
		alarminfoHtml += "<ul>";
		for(var i=0; i<alarmData.length; i++) {
			alarminfoHtml += "<li id='alarm"+ alarmData[i].id +"'><img src='" + alarmData[i].icon + "' align='absmiddle' />" + alarmData[i].times + "</li>";
		}
		alarminfoHtml += "</ul>";
		$("#alarmInfo").html(alarminfoHtml);
	};
})(jQuery);

//tab页组件
(function($) {
	$.fn.tabPage = function( s ) {
		var conf = $.extend({}, s);
		return new tabPageComponent().init(this, conf);
	};
	
	$.fn.tabPage.add = function(name, url, id, hideCloseIcon) {
		return tabPageComponent().add(name, url, id, hideCloseIcon);
	};
	
	var selectTabs =  new Array();
	function tabPageComponent() {
		var maxNum = 10000;
		return {
			spaceWidth: 500,
			init: function(elem, s) {
				var initW = $(window).width()- 76;
				maxNum = s.max||maxNum;
				var tabPageHTML = "<div class='tabpage'>";
				tabPageHTML += "<div class='tabpage_bg_left'></div>";
				tabPageHTML += "<div class='tabpage_bg_right'></div>";
				tabPageHTML += "<div class='tabpage_bg_center'>";
				tabPageHTML += "<div class='tabpage_fix' id='tabpageFix'></div>";
				tabPageHTML += "<div class='tabpage_overflow' id='tabpageOverflow'>";
				tabPageHTML += "<div class='tabpage_inner' id='tabPageInner'>";
				tabPageHTML += "<div id='tabpageTitle'>";
				tabPageHTML += "</div></div></div>";
				tabPageHTML += "<div class='tabpage_tools' id='tabpageTools'></div>";
				tabPageHTML += "</div>";
				tabPageHTML += "</div>";
				tabPageHTML += "<div id='tabpageContent'></div>";
				//var tabPageContainer = "<div class='tabpage'><div class='tabpage_bg_left'></div><div class='tabpage_bg_right'></div><div class='tabpage_bg_center' id='tabpageTitle'></div></div><div id='tabpageContent'></div>";
				var tabPage = $(tabPageHTML);
				tabPage.appendTo(elem);
				var tabPageTitle = $("#tabpageTitle");
				var tabPageOverflow = $("#tabpageOverflow");
				tabPageComponent().initTools($("#tabpageTools"));
				tabPageTitle.data("count", 0)
							.data("current", new Array())
							.width(tabPageComponent().spaceWidth);
				tabPageOverflow.width(initW);
				

				tabPageTitle.data("max", maxNum);
				tabPageTitle.data('openedNum', 0);
				
				$.resizeObjs().reFixSize(tabPage.outerHeight());
				if(s.data)
				{
					$(s.data).each(function(i) {
						tabPageComponent().add({
							name:this.name, 
							url:this.url, 
							id:this.id, 
							hideCloseIcon:this.hideCloseIcon, 
							index:i,
							helpid:this.helpid
					});
					})
				}
				
				//if(s.data.length>0)
				//{
				//	tabPageComponent().switchTo($("#tabpageTitle > ul:eq(0)"));
				//}
			},
			/**
			 * 重要
			 * add(o) o={name, url, id, hideCloseIcon, index, helpid} 以后使用add(o)方法
			 * */
			add: function(name, url, id, hideCloseIcon, index, helpid) {
				if(typeof name=="object"){
					var json=name;
					name=json.name;
					url=json.url;
					id=json.id;
					hideCloseIcon=json.hideCloseIcon;
					index=json.index;
					helpid=json.helpid;
				}
				var tabPageTitle = $("#tabpageTitle");
				var tabPageFix = $("#tabpageFix");
				var tabPageTools = $("#tabpageTools");
								
				//创建新Tab标签				
				//通过ID判断是否已经打开了Tab页，不能重复打开相同Tab页
				if(id)
				{
					if(document.getElementById("tabpageTitle"+id))
					{
						var tempTitleItem = document.getElementById("tabpageTitle"+id);
						
						tabPageComponent().switchTo(tempTitleItem);
						return true;
					}
					tid = "tabpageTitle"+id;
				}
				else
				{
					tid = "tabpageTitleRd"+index;
				}
				//通过名称判断是否已经打开了Tab页，不能重复打开相同Tab页
				var titleItemArr = tabPageTitle.find("li.tabpage_title_center");
				if(titleItemArr.length>0)
				{
					for(var i=0; i<titleItemArr.length; i++)
					{
						var tempTitleCenter = $(titleItemArr[i]);
						if(name==tempTitleCenter.text())
						{
							var tempTitleItem2 = tempTitleCenter.parent();
							tabPageComponent().switchTo(tempTitleItem2);
							return true;
						}
					}
				}
				
				//判断是否到达最大Tab页数
				
				var openedNum = tabPageTitle.data("openedNum");
				var maxNum = tabPageTitle.data("max");

				if(openedNum>=maxNum)
				{
					return true;
				}

				//$("#tabpageContent > div").hide();
				var indx = index?index:tabPageComponent().count();

				var titleHtml = "<ul id='" + tid + "' class='tabpage_title tabpage_title_normal' title='"+ name +"' tabIndx='"+indx+"'>";
				titleHtml += "<li class='tabpage_title_left'></li>";
				titleHtml += "<li class='tabpage_title_center'><span>" + name + "</span></li>";
				titleHtml += "<li class='tabpage_title_icon'><span></span></li>";
				titleHtml += "<li class='tabpage_title_right'></li>";
				titleHtml += "</ul>";
				//tab 的头部对象	
				var titleItem;
				var itemW = 0;
				if(hideCloseIcon)
				{
					titleItem = $(titleHtml).appendTo(tabPageFix);
					itemW = 0;
				}
				else
				{
					titleItem = $(titleHtml).appendTo(tabPageTitle);
					itemW = titleItem.width();
					selectTabs.push(titleItem);
					//alert(titleItem)
				}
				titleItem.helpid=helpid;
				if(name=="内容上传")titleItem.helpid="真空"; 
				//alert(selectTabs.length)
				var tabPageOverflow = $("#tabpageOverflow");
				tabPageOverflow.width($(window).width() - tabPageFix.width() -76 );
				
				//计算新的Tab标签整长度
				var titleW = tabPageTitle.width();
				var newW = titleW + itemW;
				
				//alert("titleW: "+titleW+", newW:"+newW)
				tabPageTitle.width(newW);
				
				//定义鼠标事件
				titleItem.click(function() {
					tabPageComponent().switchTo(titleItem);
				});
				titleItem.mouseover(function() {
					if("tabpage_title_on"==$(this).attr("class"))
					{
						return false;
					}
					$(this).removeClass("tabpage_title_normal");
					$(this).addClass("tabpage_title_over");
				});
				titleItem.mouseout(function() {
					if("tabpage_title_on"==$(this).attr("class"))
					{
						return false;
					}
					$(this).removeClass("tabpage_title_over");
					$(this).addClass("tabpage_title_normal");
				});
				titleItem.find("li:eq(2)").click(function() {
					tabPageComponent().del(titleItem);
				});
				//关闭按钮鼠标效果
				var closeIcon = titleItem.find("span:eq(1)");
				closeIcon.mouseover(function() {
					$(this).addClass("tabicon_over");
				});
				closeIcon.mouseout(function() {
					$(this).removeClass("tabicon_over");
				});
				if(hideCloseIcon)
				{
					titleItem.addClass('tabpage_title_fix');
					titleItem.find('li.tabpage_title_icon:eq(0)').hide();
				}
				//创建tab页内容
				//var fixH = $.resizeObjs().getFixSize() + $("div.tabpage").outerHeight();
				var fixH = $.resizeObjs().getFixSize() + 4;
				var contentHtml = "<div></div>";
				var contentDiv = $(contentHtml).appendTo("#tabpageContent");
				var toHeight = $(window).height()-fixH;
				$(contentDiv).height(toHeight);
				//保存tab页序号
				titleItem.data("orderNum", indx);
				contentDiv.data("orderNum", indx);
				contentDiv.data("toHeight", toHeight);
				
				contentHtml = "<iframe name='tabPageIfra"+indx+"' width='100%' scrolling='auto' frameborder='0' border='0' src=''></iframe>";
				var contentIframe = $(contentHtml).appendTo($(contentDiv));
				if (url.indexOf("?") > 0) {
					url += "&BMETimestamp="+(new Date().getTime());
				} else {
					url += "?BMETimestamp="+(new Date().getTime());
				}
				$(contentIframe).height(toHeight)
					.attr('src', url);
				//将tab内容容器加入窗口重设大小对象队列
				$.resizeObjs().push({container:$(contentDiv), child: true});
				
				tabPageComponent().switchTo(titleItem);
				tabPageComponent().count(1);
				
				//如果tab数量大于1则显示滚动条
				if((tabPageTitle.width()-tabPageComponent().spaceWidth) > tabPageOverflow.width())
				{
					tabPageComponent().scrollShow();
				}
				//重设滚动条大小
				tabPageComponent().scrollResize("add");
				//重设滚动按钮位置
				tabPageComponent().scrollRepos("add");

				openedNum++;
				tabPageTitle.data("openedNum", openedNum);
				titleItem.setHelpId=function(id){
					titleItem.data("helpid",id||"真空");
				};				
				
				return titleItem;
			},
			del: function(menuItem) {
				var tabPageTitle = $("#tabpageTitle");
				var tabPageOverflow = $("#tabpageOverflow");
				
				$.resizeObjs().shift($("#tabpageContent > div:eq("+$(menuItem).data("orderNum")+")"));
				tabPageComponent().resetCurrent(menuItem, "del");
				//计算新的Tab标签整长度
				var tabPageTitle = $("#tabpageTitle");
				var titleW = tabPageTitle.width();
				var newW = titleW - menuItem.width();
				tabPageTitle.width(newW);
				
				// 删除指定tab页
                selectTabs = $.grep(selectTabs, function(n, i)
                {
                    return n != menuItem;
                });
                // As a common rule, before removing an iframe from document, reset its url to "about:blank."
                // Google "IE iframe unload":
                // http://www.bigresource.com/JAVASCRIPTS-iframe-onunload-3pEK8orz.html
                tabPageComponent().getCurrentIframe(menuItem).each(function(){this.src = "about:blank";});
                $("#tabpageContent > div:eq(" + $(menuItem).data("orderNum") + ")").remove();
			
				$(menuItem).remove();
				tabPageComponent().count(-1);
				
							
				$("div.tabpage_bg_center ul.tabpage_title").each(function(i) {
					$(this).show();
					$(this).data("orderNum", i);
				});
				$("#tabpageContent > div").each(function(i) {
					$(this).data("orderNum", i);
				});
				//alert($("#tabPage").data('current'));
				tabPageComponent().switchTo();
				
					
				//重设滚动条大小
				tabPageComponent().scrollResize("del");
				//重设滚动按钮位置
				tabPageComponent().scrollRepos("del");
				//如果tab数量小于2则隐藏滚动条
				if((tabPageTitle.width()-tabPageComponent().spaceWidth) <= tabPageOverflow.width())
				{
					tabPageComponent().scrollHide();
				}
				
				var openedNum = tabPageTitle.data("openedNum");
				openedNum--;
				tabPageTitle.data("openedNum", openedNum);
			},
			switchTo: function(menuItem) {
				var mitem;
				if(menuItem)
				{
					mitem = menuItem;
				}
				else
				{
					if(tabPageComponent().getCurrent(0))
					{
						mitem =  tabPageComponent().getCurrent(0);
					}
					else if($("#tabpageTitle > ul:eq(0)"))
					{
						mitem = $("#tabpageTitle > ul:eq(0)");
					}
					else
					{
						return false;
					}
				}
				tabPageComponent().resetCurrent(mitem);
				$("#tabpageTitle").data('currentItem', $(mitem));
				$("#tabpageTitle > ul").removeClass("tabpage_title_on")
					.addClass("tabpage_title_normal");
				$("#tabpageFix > ul").removeClass("tabpage_title_on")
				.addClass("tabpage_title_normal");
				$(mitem).removeClass("tabpage_title_normal")
					.addClass("tabpage_title_on");	
				
				var tabPageInner = $("#tabPageInner");
				var tabPageTitle = $("#tabpageTitle");
				var tabPageOverflow = $("#tabpageOverflow");
				var uls = document.getElementById("tabpageTitle").getElementsByTagName("ul");
				for(var i = 0 ;i<uls.length;i++){
					uls[i].style.display = "block";
				}
				
				//if(repeat){
					var mitemID = $(mitem).attr("id");					
					var currentNum = 0;
					for(var i = 0 ;i<uls.length;i++){
						if(uls[i].id){
							if(uls[i].id == mitemID){
								currentNum = i;
							}
						}
					}
					var mw = $(mitem).width()+1;
					var minNum = Math.ceil(Math.abs(parseInt(tabPageInner.css("left")))/mw);
					var maxNum = Math.floor((Math.abs(parseInt(tabPageInner.css("left")))+tabPageOverflow.width())/mw);
					
					//alert("mitemID: "+ mitemID+", mw: "+mw+ ", currentNum3: "+currentNum + ", minNum: "+minNum+ ", maxNun: "+maxNum);
					
					if(currentNum < minNum){
						//alert(1)
						if(parseInt(tabPageInner.css("left"))<0){
							//alert(11)
							var leftValue = parseInt(tabPageInner.css("left")) +((minNum - currentNum) * mw);
							if(leftValue > 0){
								leftValue = 0;
							}
						}
						else{
							//alert(12)
							var leftValue = 0;
						}
						tabPageInner.css("left",leftValue);
						for(var i = (maxNum-minNum+currentNum) ;i<uls.length;i++){
							uls[i].style.display = "none";
						}
					}
					
					if(currentNum >= maxNum){
						//alert(2)
						var leftValue = parseInt(tabPageInner.css("left")) -((currentNum - maxNum + 1 ) * mw);
						if(leftValue > 0){
							leftValue = 0;
						}
						tabPageInner.css("left",leftValue);
						for(var i = currentNum+1 ;i<uls.length;i++){
							uls[i].style.display = "none";
						}
					}
					if(minNum <= currentNum && currentNum < maxNum){
						//alert(3)
						for(var i = maxNum ;i<uls.length;i++){
							uls[i].style.display = "none";
						}
					}					
				//}
				
				//解决Firefox下切换Tab页时引起Flash刷新的问题
				if($.browser.mozilla)
				{
					$("#tabpageContent > div").css("visibility", "hidden").css("height", "0px").attr("active", false);
					var activeContent = $("#tabpageContent > div:eq("+$(mitem).data("orderNum")+")");
					activeContent.css("visibility", "visible").css("height", activeContent.data("toHeight")+"px").attr("active", true);
				}
				else
				{
					$("#tabpageContent > div").hide();
					$("#tabpageContent > div:eq("+$(mitem).data("orderNum")+")").show();
				}
			},
			initTools: function(ctnr) {
				var toolsHTML = "<ul>";
				toolsHTML += "<li class='tabpage_scroll'>";
				toolsHTML += "	<div style='position:absolute'>";
				toolsHTML += "	<ul id='tabpageScrollBar'>";
				toolsHTML += "		<li class='tabpage_scroll_left'></li>";
				toolsHTML += "		<li class='tabpage_scroll_center'></li>";
				toolsHTML += "		<li class='tabpage_scroll_right'></li>";
				toolsHTML += "	</ul>";
				toolsHTML += "	</div>";
				toolsHTML += "</li>";
				toolsHTML += "<li class='tabpage_icon_list'></li>";
				toolsHTML += "<li class='tabpage_icon_close'></li>";
				toolsHTML += "</ul>";
				var tools = $(toolsHTML).appendTo(ctnr);
				tabPageComponent().scrollTabpage($("#tabpageScrollBar"));
				document.getElementById("tabpageScrollBar").onselectstart = function() { return false; };
				tabPageComponent().scrollHide();
				var openListIcon = tools.find("li.tabpage_icon_list");
				var closeTabIcon = tools.find("li.tabpage_icon_close");
				openListIcon.bind("mouseover", tabPageComponent().showOpenedList);
				openListIcon.bind("mouseout", tabPageComponent().closeOpenedList);
				closeTabIcon.bind("click", tabPageComponent().closeCurrent);
			},
			showOpenedList: function() {  
				tabPageComponent().showTabPop();
			},
			closeOpenedList: function() {
				tabPageComponent().hideTabPop();
			},
			showTabPop: function(){
				tabPageComponent().createTabPop();
				var tabPopContainer = $("#tabPopContainer");
				tabPopContainer.mouseover(function(){
					$(this).show();			
				});
				tabPopContainer.mouseout(function(){
					$(this).hide();			
				});
				tabPopContainer.show();
			},
			hideTabPop: function(){
				var tabPopContainer = $("#tabPopContainer");
				tabPopContainer.hide();
			},
			createTabPop: function(){
				if(document.getElementById("tabPopContainer")){
					$("#tabPopContainer").remove();
				}
				var tabPopHTML = "<div class='tab_pop_menu' id='tabPopContainer'>";
				tabPopHTML += "<div class='menu_masker'><iframe width='0' height='0' frameborder='0' src=''></iframe></div>";
				tabPopHTML += "<div class='tab_pop_tool' id='tabPopTool'></div><div class='nofloat'></div>";
				tabPopHTML += "<div class='horimenuSub_top'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><div class='horimenuSub_top_left'></div></td><td class='horimenuSub_top_center'></td><td><div class='horimenuSub_top_right'></div></td></tr></table></div>";
				tabPopHTML += "<div class='horimenuSub_center_left'><div class='horimenuSub_center_right'><div class='horimenuSub_center_center'>";
				tabPopHTML += "<ul></ul>";
				tabPopHTML += "</div><div class='porlet_nofloat'></div></div></div>";
				tabPopHTML += "<div class='horimenuSub_bottom'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><div class='horimenuSub_bottom_left'></div></td><td class='horimenuSub_bottom_center'></td><td><div class='horimenuSub_bottom_right'></div></td></tr></table>";
				tabPopHTML += "</div>";
				var tabPop = $(tabPopHTML).appendTo("#topFrame");
				var tabPopTool = $("#tabpageTools").find("li.tabpage_icon_list");
				var offset = tabPopTool.offset();
				var leftX = offset.left - tabPop.width()+ tabPopTool.width()+ 5;	
			
				tabPop.css({"left": leftX , "top": offset.top-6});
				
				for(var i=0; i<selectTabs.length; i++)
				{
					tabPageComponent().addTabMenuItem(selectTabs[i]);
				}
				
				return tabPop;
			},	
			addTabMenuItem: function( itemAtt ) {
				var tabPopContainer = $("#tabPopContainer");
				var ul = $("#tabPopContainer").find("ul");
				var tabmenuHtml = "<li>";
				tabmenuHtml += "<a href='#none'>"+ itemAtt.text() +"</a>";
				tabmenuHtml += "</li>";
				var tabmenuItem = $(tabmenuHtml).appendTo(ul);
				tabmenuItem.css({"width": (tabPopContainer.width()-11)});
				
				tabmenuItem.click( function() {
					tabPopContainer.hide();
					tabPageComponent().switchTo(itemAtt);
				});
				
				return tabmenuItem;
			},
			closeCurrent: function() {
				var currentItem = $("#tabpageTitle").data('currentItem');
				tabPageComponent().del(currentItem);
			},
			scrollTabpage: function(scrollBar) {
				scrollBar.focus(function() {
					scrollBar.blur();
				});
				scrollBar.bind("mousedown", tabPageComponent().scrollDown);
			},
			scrollDown: function(event) {
				$(document).bind("mousemove.tabPageScroll", tabPageComponent().scrollMove);
				$(document).bind("mouseup.tabPageScroll", tabPageComponent().scrollUp);
				//$(document).bind("mouseout.tabPageScroll", tabPageComponent().scrollUp);
				event = tabPageComponent().fixE(event);
				if(event.which!=1)
				{
					return true;
				}
				$(this).data("lastMouseX", event.clientX);
				this.isScrolling = true;
			},
			scrollUp: function(event) {
				$(document).unbind("mousemove.tabPageScroll");
				$(document).unbind("mouseup.tabPageScroll");
				this.isScrolling = false;
			},
			scrollMove: function(event) {
				var scrollBar = $("#tabpageScrollBar");
				var marginLeft = parseInt(scrollBar.css("left"));
				var _clientX = event.clientX;
				var _lastMouseX = scrollBar.data("lastMouseX");
				var newX = marginLeft + ( _clientX - _lastMouseX );
				if( newX < 1 )
				{
					newX = 1;
				}
				if( newX >= (59 - scrollBar.width()) )
				{
					newX = 59 - scrollBar.width();
				}
				scrollBar.css("left", newX);
				scrollBar.data("lastMouseX", event.clientX);
				tabPageComponent().scrollTo(newX, scrollBar);
			},
			scrollHide: function() {
				var scrollBar = $("#tabpageTools");
				scrollBar.hide();
			},
			scrollShow: function() {
				var scrollBar = $("#tabpageTools");
				scrollBar.show();
			},
			scrollResize: function(act) {
				var scrollBar = $("#tabpageScrollBar");
				var scrollBarCenter = scrollBar.find("li:eq(1)");
				var scrollW = scrollBar.width();
				var scrollCW = scrollBarCenter.width();
				
				if(scrollBar.css("display")=="none")
				{
					return true;
				}
				
				var len;
				if("add"==act)
				{
					if(scrollBar.width()<=26)
					{
						return true;
					}
					len = -2;
				}
				else
				{
					if(scrollBar.width()>=44)
					{
						return true;
					}
					len = 2;
				}
				
				scrollBar.width(scrollW + len);
				scrollBarCenter.width(scrollCW + len);
			},
			scrollRepos: function(act) {
				var tabPage = $("#tabpageTitle");
				var tabPageInner = $("#tabPageInner");
				var tabpageOverflow = $("#tabpageOverflow");
				var scrollBar = $("#tabpageScrollBar");
				var tabPageLast = tabPage.find("ul:last");
				var mitem;
				if("add"==act)
				{
					if(tabPage.width()-tabPageComponent().spaceWidth > tabpageOverflow.width())
					{
						toX = tabpageOverflow.width() - ( tabPage.width()-tabPageComponent().spaceWidth );
						var xDiff = - Math.ceil(Math.abs(toX)/(tabPageLast.width()+1)) * (tabPageLast.width()+1);
												
						tabPageInner.css("left", xDiff + "px");
						
						var tabX = parseInt(tabPageInner.css("left"));
						var tabPage = $("#tabpageTitle");
						var tabPageInner = $("#tabPageInner");
						var tabPageLast = tabPage.find("ul:last");
						var tpDistance = tabPage.width() - tabPageLast.width() - tabPageComponent().spaceWidth;
						var barDistance = 58 - scrollBar.width();
						var barX = - (barDistance*tabX/tpDistance + 1);
						scrollBar.css("left", barX + "px");
					}
				}
				else 
				{
					toX = parseInt(tabPageInner.css("left"));
					if(toX < 0){
						var xDiff =  toX + (tabPageLast.width()+1);
											
						tabPageInner.css("left", xDiff + "px");
						//var toLeft = parseInt(scrollBar.css("left")) - 2;
						//scrollBar.css("left", toLeft + "px");
					}
				}
			},
			scrollTo: function(newX, scrollBar) {
				var tabPage = $("#tabpageTitle");
				var tabPageInner = $("#tabPageInner");
				var tabPageLast = tabPage.find("ul:last");
				var tpDistance = tabPage.width() - tabPageLast.width() - tabPageComponent().spaceWidth;
				var barDistance = 58 - scrollBar.width();
				var toX = -(newX-1)/barDistance*tpDistance;
				tabPageInner.css("left", toX + "px");
			},
			count: function(num) {
				var cout = $("#tabpageTitle").data('count');
				if(num&&isNaN!=num)
				{
					cout += num;
				}
				$("#tabpageTitle").data('count', cout);
				return cout;
			},
			resetCurrent: function(menuItem, todo) {
				var currt = $("#tabpageTitle").data('current');
				var newCurrt = $.grep(currt, function(n, i) {
					return n.data('orderNum')==$(menuItem).data('orderNum');
				}, true);
				if("del"!=todo) {
					newCurrt.unshift($(menuItem));
				}
				$("#tabpageTitle").data('current', newCurrt);
			},
			isOpen: function() {
			},
			getCurrent: function(index) {
				return $("#tabpageTitle").data('current')[index];
			},
			getCurrentIframe: function(menuItem) {
				var tabpageContent = $("#tabpageContent > div:eq("+$(menuItem).data("orderNum")+")");
				var tabpageIfra = tabpageContent.find("iframe:first-child");
				return tabpageIfra;
			},

			// 解决不同浏览器的 event 模型不同的问题
			fixE: function(ig_) {
				if( typeof ig_ == "undefined" ) {
					ig_ = window.event;
				}
				if( typeof ig_.layerX == "undefined" ) {
					ig_.layerX = ig_.offsetX;
				}
				if( typeof ig_.layerY == "undefined" ) {
					ig_.layerY = ig_.offsetY;
				}
				if( typeof ig_.which == "undefined" ) {
					ig_.which = ig_.button;
				}
				return ig_;
			}
		};
	}
	
})(jQuery);


//获取某个区域中表单控件的值，并组织成一个json对象
(function($){
	$.fn.getFormData = function(toType) {
		var textObj = this.find(":text");
		var checkedObj = this.find(":checked");
		var selectedObj = this.find(":selected");
		var hideObj = this.find("input:hidden");
		
		//拼装json数据
		var jsonstr = "";
		jsonstr += "({";
		if(textObj.length>0)
		{
			textObj.each(function(i) {
				if(this.value)
				{
					jsonstr += "'"+this.name+"': '"+this.value+"',";
				}
			});
		}
		
		if(checkedObj.length>0)
		{
			checkedObj.each(function(i) {
				if(this.value)
				{
					jsonstr += "'"+this.name+"': '"+this.value+"',";
				}
			});
		}
		
		if(hideObj.length>0)
		{
			hideObj.each(function(i) {
				if(this.value)
				{
					jsonstr += "'"+this.name+"': '"+this.value+"',";
				}
			});
		}
		
		if(selectedObj.length>0)
		{
			selectedObj.each(function(i) {
				if(this.value)
				{
					jsonstr += "'"+$(this).parent().attr("name")+"': '"+this.value+"',";
				}
			});
		}
		
		jsonstr += "})";
		jsonstr = jsonstr.replace(",}", "}");
		return eval(jsonstr);
	};
})(jQuery);

(function($){
	$.fn.fullPageHeight = function () {
		var bodyH = document.body.offsetHeight - 22;
		var boxH = this.find("div.page_container").height();
		if(boxH < bodyH)
		{
			$(this).css("height", bodyH);
		}
		else
		{
			this.css("height", "auto");
		}
		$(window).bind("resize.fullPageHeight", { targetEle: this }, resizeFullPageHeight);
		function resizeFullPageHeight(event) {
			var ele = event.data.targetEle;
			
			ele.css("height", "auto");
			if ( $.browser.msie && "6.0"==$.browser.version )
			{
				$("body").css("height", "auto");
			}
			var bodyH = document.body.offsetHeight - 22;
			var boxH = ele.find("div.page_container").height();
			if(boxH <= bodyH)
			{
				ele.css("height", bodyH);
			}
			if ( $.browser.msie && "6.0"==$.browser.version )
			{
				$("body").css("height", "100%");
			}
		}
	};
})(jQuery);

(function($){
	$.fn.containerMiddle = function() {
		var targetObj = this;
		doContainerMiddle(targetObj);
		$(window).bind("resize.fullPageHeight", { targetEle: targetObj }, resetContainerMiddle);
		function resetContainerMiddle(event) {
			var ele = event.data.targetEle;
			doContainerMiddle(ele);
		}
		function doContainerMiddle(ele) {
			var bodyH = $("body").height();
			var targetH = ele.height();
			var posY = (bodyH - targetH)/2-100;
			
			if(posY>0)
			{
				ele.css("top", posY+"px");
			}
		}
	};
})(jQuery);

//页面加载
(function($){
	function PageLoad() {
		var pl;
		this.show = function() {
			init();
			pl.show();
		};
		this.cancel = function() {
			if(pl)
			{
				pl.remove();
			}
		};
		
		function init() {
			if(document.getElementById("pageload"))
			{
				$("#pageload").remove();
			}
			pl = createPageLoad();
		}

		function createPageLoad() {
			var pageloadStr = "<div id='pageload'>";
			pageloadStr += "<div class='pageload_masker'><iframe width='100%' height='100%' frameborder='0'></iframe></div>";
			pageloadStr += "<div class='pageload_body'></div>";
			pageloadStr += "</div>";
			var plObj = $(pageloadStr);
			plObj.appendTo("body");
			plObj.hide();
			return plObj;
		}
	}
	$.pageload = new PageLoad();
})(jQuery);


//多级联动菜单
(function($) {
	function getObjById(id) {
		return "string" == typeof id ? document.getElementById(id) : id;
	};

	function addEventHandler(oTarget, sEventType, fnHandler) {
		if (oTarget.addEventListener) {
			oTarget.addEventListener(sEventType, fnHandler, false);
		} else if (oTarget.attachEvent) {
			oTarget.attachEvent("on" + sEventType, fnHandler);
		} else {
			oTarget["on" + sEventType] = fnHandler;
		}
	};

	function Each(arrList, fun){
		for (var i = 0, len = arrList.length; i < len; i++) { fun(arrList[i], i); }
	};

	function GetOption(val, txt) {
		var op = document.createElement("OPTION");
		op.value = val; op.innerHTML = txt;
		return op;
	};

	var Class = {
		create: function() {
			return function() {
				this.initialize.apply(this, arguments);
			};
		}
	};

	Object.extend = function(destination, source) {
		for (var property in source) {
			destination[property] = source[property];
		}
		return destination;
	};


	//var CascadeSelect = Class.create();
	var CascadeSelect = function() {};
	CascadeSelect.prototype = {
		//select集合，菜单对象
		create: function(arrSelects, arrMenu, options) {
			if(arrSelects.length <= 0 || arrMenu.lenght <= 0) return;//菜单对象
			
			var oThis = this;
			
			this.Selects = [];//select集合
			this.Menu = arrMenu;//菜单对象
			
			this.SetOptions(options);
			
			this.Default = this.options.Default || [];
			
			this.ShowEmpty = !!this.options.ShowEmpty;
			this.EmptyText = this.options.EmptyText.toString();
			
			//设置Selects集合和change事件
			Each(arrSelects, function(o, i){
				addEventHandler((oThis.Selects[i] = getObjById(o)), "change", function(){ oThis.Set(i); });
			});
			
			this.ReSet();
		},
		//设置默认属性
		SetOptions: function(options) {
			this.options = {//默认值
				Default:    [],//默认值(按顺序)
				ShowEmpty:    false,//是否显示空值(位于第一个)
				EmptyText:    "Please select"//空值显示文本(ShowEmpty为true时有效)
			};
			Object.extend(this.options, options || {});
		},
		//初始化select
		ReSet: function() {

			this.SetSelect(this.Selects[0], this.Menu, this.Default.shift());
			this.Set(0);
		},
		//全部select设置
		Set: function(index) {
			var menu = this.Menu;
			
			//第一个select不需要处理所以从1开始
			for(var i=1, len = this.Selects.length; i < len; i++){
				if(menu.length > 0){
					//获取菜单
					var value = this.Selects[i-1].value;
					if(value!=""){
						Each(menu, function(o){ if(o.val == value){ menu = o.menu || []; } });
					} else { menu = []; }
					
					//设置菜单
					if(i > index){ this.SetSelect(this.Selects[i], menu, this.Default.shift()); }
				} else {
					//没有数据
					this.SetSelect(this.Selects[i], [], "");
				}
			}
			//清空默认值
			this.Default.length = 0;
		},
		//select设置
		SetSelect: function(oSel, menu, value) {
			oSel.options.length = 0; oSel.disabled = false;
			if(this.ShowEmpty){ oSel.appendChild(GetOption("", this.EmptyText)); }
			if(menu.length <= 0){ oSel.disabled = true; return; }
			
			Each(menu, function(o){
				var op = GetOption(o.val, o.txt ? o.txt : o.val); op.selected = (value == op.value);
				oSel.appendChild(op);
			});    
		},
		//添加菜单
		Add: function(menu) {
			this.Menu[this.Menu.length] = menu;
			this.ReSet();
		},
		//删除菜单
		Delete: function(index) {
			if(index < 0 || index >= this.Menu.length) return;
			for(var i = index, len = this.Menu.length - 1; i < len; i++){ this.Menu[i] = this.Menu[i + 1]; }
			this.Menu.pop();
			this.ReSet();
		}
	};

	$.cascadeselect = new CascadeSelect();
})(jQuery);

// h00142237: I DONOT know who and why added $.fn.treeTable here and for what...
//
////树形表格
//(function($) {
//	$.fn.treeTable = function() {
//		var tbody = this.find("tbody");
//		var rows = tbody.find("tr");
//		rows.each(function(i) {
//			var row = $(this);
//			this.onmouseover = function() {
//				this.className = this.className.replace("_normal", "_over");
//			};
//			this.onmouseout = function() {
//				this.className = this.className.replace("_over", "_normal");
//			};
//			var idStr = this.id;
//			var idAtom = idStr.split("-");
//			var firstCell = row.find("td:eq(0)");
//			firstCell.html("<span style='margin-left: "+((idAtom.length-2)*24)+"px; padding: 24px;'>"+firstCell.html()+"</span>");
//			//firstCell.css("margin-left", "24px");
//			var childRows = tbody.find("tr.childof-"+idStr);
//			if(childRows.length>0)
//			{
//				firstCell.addClass("treetable_parent_opened");
//				row.bind("click.openCloseChild", openCloseChild);
//			}
//		});
//
//		function openCloseChild() {
//			var row = $(this);
//			var firstCell = row.find("td:eq(0)");
//			var idStr = row.attr("id");
//			var childRows = tbody.find("tr.childof-"+idStr);
//			if(-1==firstCell.attr("class").indexOf("treetable_parent_opened"))
//			{
//				firstCell.removeClass("treetable_parent_closed");
//				firstCell.addClass("treetable_parent_opened");
//				childRows.show();
//				openChild(childRows);
//			}
//			else
//			{
//				firstCell.removeClass("treetable_parent_opened");
//				firstCell.addClass("treetable_parent_closed");
//				childRows.hide();
//				closeChild(childRows);
//			}
//		}
//		function openChild(childRows)
//		{
//			childRows.each(function(i) {
//				var row = $(this);
//				var idStr = row.attr("id");
//				var firstCell = row.find("td:eq(0)");
//				var childRows = tbody.find("tr.childof-"+idStr);
//				if(childRows.length>0)
//				{
//					if(-1!=firstCell.attr("class").indexOf("treetable_parent_opened"))
//					{
//						childRows.show();
//					}
//					openChild(childRows);
//				}
//			});
//		}
//		function closeChild(childRows)
//		{
//			childRows.each(function(i) {
//				var row = $(this);
//				var idStr = row.attr("id");
//				var childRows = tbody.find("tr.childof-"+idStr);
//				if(childRows.length>0)
//				{
//					childRows.hide();
//					closeChild(childRows);
//				}
//			});
//		}
//	};
//})(jQuery);

//tab组件
(function($) {
	$.fn.tab = function( s ) {
		var TAB = this;
		var conf = $.extend({}, s);
		var currentNo = 0;
		var tabTitle = TAB.children(".tab_title");
		var tabTitles = tabTitle.find(".tab_title_normal");
		var tabContents = TAB.children(".tab_content");
		var eventArr = [];
		//if(conf.defaultNo)
		//{
		//	currentNo = conf.defaultNo - 1;
		//}
		if(conf.normalStyle)
		{
			TAB.addClass(conf.normalStyle);
		}
		showTab(currentNo);
		tabTitles.each(function(i) {
			var tabTitle = $(this);
			tabTitle.bind("click", function() {
				showTab(i);
			});
			tabTitle.data("no", i);
			if(i==0)
			{
				tabTitle.addClass("tab_title_first");
			}
		});

		function showTab(no) {
			currentNo = no;
			var currentTitle = $(tabTitles[no]);
			var currentContent = $(tabContents[no]);
			
			if(tabTitles[no].className.indexOf("button_disable") != -1)
			{
				return;
			}
			tabTitles.removeClass("tab_title_normal");
			tabTitles.removeClass("tab_title_on");
			tabTitles.addClass("tab_title_normal");
			currentTitle.removeClass("tab_title_normal");
			currentTitle.removeClass("tab_title_on");
			currentTitle.addClass("tab_title_on");
			tabContents.hide();
			currentContent.show();

			dispatchEvent(no);
		}

		function dispatchEvent(no) {
			if(eventArr.length>0)
			{
				for(var i=0, j=eventArr.length;i<j;i++ ) {
					var obj = eventArr[i];
					if(typeof(obj.backfun)=="function")
					{
						obj.backfun.apply(this, [{type:obj.type, data: no}]);
					}
				}
			}
		}

		return {
			addEventListener: function(m_type, m_backfun) {
				var obj = { type:m_type, backfun: m_backfun };
				var index = $.inArray(obj, eventArr);
				if(-1==index)
				{
					eventArr.push(obj);
				}
			}, 
			removeEventListener: function(m_type, m_backfun) {
				var obj = { type:m_type, backfun: m_backfun };
				$.grep(eventArr, function(n,i){
					return n==obj;
				}, true);
			},
			switchTo: function(no) {
				showTab(no-1);
			},
			getCurrent: function() {
				return currentNo+1;
			}
		};
	};
})(jQuery);

function setBmeInputElementValue(domThis, target, value, validate)
{
	var jqThis = $(domThis);
	var jqObj = jqThis.find(target);
	jqObj.val(value);
	if (!validate)
	{
		return;
	}
	jqObj.valid();
}
// 设置input的值，this组件的外框
(function($) {
	$.fn.setBmeInputValue = function(value, validate) 
	{
		setBmeInputElementValue(this, "input", value, validate);
	};
})(jQuery);
//设置textarea的值，this组件的外框
(function($) {
	$.fn.setBmeTextareaValue = function(value) 
	{
		setBmeInputElementValue(this, "textarea", value, validate);
	};
})(jQuery);
//设置select的值，this是组件的外框
(function($) {
	$.fn.setBmeSelectValue = function(value) 
	{
		setBmeInputElementValue(this, "select", value, validate);
	};
})(jQuery);

//清除<input type="file" />的值，因为有的浏览器下file控件是只读的 
//所以只好clone一份 
//safari、ff、chrome下value可赋值为空串，不可改为其他值，没有问题 
//ie下只读，但是clone时，是不带值clone的，也没问题 
(function($) {
	$.fn.clearFileValue = function() 
	{
		var fileTextObj = jQuery('#' + this.attr("id") + '_text'); 
		var fileObj = jQuery('#' + this.attr("id") + '_file'); 
		fileTextObj.val('');
		fileTextObj.valid(); 
		fileObj.val(''); 	
		if ($.browser.msie) 
		{ 
			var newObj = fileObj.clone(true); 
			fileObj.replaceWith(newObj); 
		} 
	};
})(jQuery);

// bme init函数统一入口
(function($) {
	function createTitleTipsPoper()
	{
		if (undefined != jBME.titleTipsPoper)
		{
			return;
		}
		jBME.titleTipsPoper = new jBME.Poper();
		jBME.titleTipsPoper.offsetInfo.left = 15;
		jBME.titleTipsPoper.offsetInfo.right = -15;
		jBME.titleTipsPoper.offsetInfo.top = 5;
	}

	$.fn.onLoadHelp = function() {
        this.tipsHelp();      // from jquery.oms.js, someone added....
        this.focusBorderInit();	// attach onfocus/onblur
        this.focusFieldInit();	// attach field focus effect
        this.autoHeightInit();
		createTitleTipsPoper();
	};
})(jQuery);

(function($) {
    /**
     *  元素自动适应页面高度
     *  使用场景举例：iframe之于以下doctype场景下占满全页面:
     *  eg: <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
     *  
     * @param  selector   [number or string] 指定现有控件已用高度(数字或jquery选择器)
     * @param  autoResize [boolean]          是否在窗口大小改变时重新调整大小: true自动调整，false不自动调整，默认为true
     */
	$.fn.autoHeight = function(selector, autoResize) 
    {
		var remain = this;
		var parent = $(document.documentElement);
        //Calculate total control height or the given height.
        var height = 0;
        if(undefined == selector)
        {
        	this.addClass("auto_height_self");
        	parent = remain.parent();
        	var objs = parent.children(":not(.auto_height_self)");
        	this.removeClass("auto_height_self");
        	objs.each(function(i){
        		height += $(this).outerHeight(true);
        	});
        }
        else if("number" === typeof(selector))
        {
            height = selector;
        }
        else
        {
            $(selector || []).each(function()
            {
                height += $(this).outerHeight(true);
            });
        }
        
        //Get the remain height in page and set to the autoHeight element.
        
        remain.height( getPageRemainHeight(height) );
        if(autoResize !== false)
        {
        	$(window).bind("resize", function() 
            {
                remain.height( getPageRemainHeight(height) );  
            });
        }
        
        function getPageRemainHeight(givenHeight)
        {
            var remainHeight = parent.innerHeight() - (givenHeight || 0);
            // 需要再减去自己的margin/padding/border值
            remainHeight -= remain.outerHeight(true) - remain.height();
            if(parseFloat($.browser.version) >=8)
            {
                remainHeight -= 4;  //IE8 scrollHeight... :(
            }
            return Math.max(remainHeight, 0);
        }
    };
	$.fn.autoHeightInit = function() {
		var container = this;
		var jqTargets = container.find(".bc_auto_height");
		jqTargets.each(function(i) {
			var jqTarget = $(this);
			jqTarget.autoHeight(undefined, true);
		});
	};
})(jQuery);

//表单提示信息组件
(function($) {
	$.fn.tipsHelp = function() {
		// 整个页面一个tip对象
		if (!jBME.tipsObj)
		{
			// 创建tips对象
			var html = '<div class="bf_tips">';
			html += '<div hl="true"></div>';
			html += '<div hr="true"></div>';
			html += '<div ml="true"><div mr="true"></div></div>';
			html += '<div fl="true"></div>';
			html += '<div fr="true"></div>';
			html += '</div>';
			
			jBME.tipsObj = $(html);
			jBME.tipsObj.content = jBME.tipsObj.find("[mr]");
			jBME.tipsObj.appendTo("body");
			
			jBME.tipsObj.poper = new jBME.Poper(jBME.tipsObj);
		}
		jBME.tipsObj.getTipsTarget = function(jqTarget)
		{
			// 没有bmeTipsId，则在jqTarget上显示tips（这是为了支持checkboxlist等等这样的场景）
			var jqTipsTarget = jqTarget.attr("bmeTipsId");
			if (undefined == jqTipsTarget)
			{
				jqTipsTarget = jqTarget;
			}
			else
			{
				jqTipsTarget = $("#" + jqTipsTarget);
			}
			return jqTipsTarget;
		};
		jBME.tipsObj.hideTips = function()
		{
			jBME.tipsObj.jqTipsTarget = undefined;
			jBME.tipsObj.hide();
		};
		// 数据由jqTarget确定，位置由于jqTipsTarget确定
		jBME.tipsObj.refresh = function(jqTarget, jqTipsTarget)
		{
			if (jBME.tipsObj.jqTipsTarget && (jBME.tipsObj.jqTipsTarget.get(0) != jqTipsTarget.get(0)))
			{
				return;
			}

			reContent(jqTarget, jqTipsTarget);
		};
		var container = this;
		var jqTargets = container.find("[bmeTips],[validateRules]");
		jqTargets.each(function(i) {
			var jqTarget = $(this);
			// 没有bmeTipsId，则在jqTarget上显示tips（这是为了支持checkboxlist等等这样的场景）
			var jqTipsTarget = jBME.tipsObj.getTipsTarget(jqTarget);
			// 在safari下radio/checkbox在鼠标点击过去时，不会触发focusin、focusout
			jqTarget.unbind("focusin.tips").bind("focusin.tips", function() {
				jBME.tipsObj.jqTipsTarget = jqTipsTarget;
				//checkbox,radio不希望响应focusin事件,因为本身会响应checkbox事件,
				//响应focusin事件会造成上次一次校验的结果在重新点击checkbox,radio时一闪而过,体验不好
//				if(jBME.tipsObj.jqTipsTarget.is(".bc_checkbox")||jBME.tipsObj.jqTipsTarget.is(".bc_radio"))
//				{
//					return;
//				}
				jBME.tipsObj.refresh(jqTarget, jqTipsTarget);
			});
			jqTarget.unbind("blur.tips").bind("blur.tips", function(event) {
				var jqBlurTipsTarget = jBME.tipsObj.getTipsTarget($(event.target));
				if (jBME.tipsObj.jqTipsTarget && (jBME.tipsObj.jqTipsTarget.get(0) != jqBlurTipsTarget.get(0)))
				{
					return;
				}
				jBME.tipsObj.hideTips();
			});
			// 在safafi、chrome、ie下，如果是radio、checkbox，则在click时显示tips
			// 当有其他tips显示、隐藏处理时才取消这里tips显示，这样处理，是因为似乎找不到时机隐藏,IE下,解决时间控件tips展示的问题
			if (($.browser.safari || $.browser.msie) && jqTarget.is("input[type='checkbox'],input[type='radio'],input[type='text']")) 
			{
				
				jqTarget.unbind("click.tips").bind("click.tips", function() {
					jBME.tipsObj.jqTipsTarget = jqTipsTarget;
					jBME.tipsObj.refresh(jqTarget, jqTipsTarget);
				});
			}
		});
		
		function reContent(jqTarget, jqTipsTarget) {
			var vMsg = jqTarget.attr("vMsg");
			var text = "";
			if (vMsg)
			{
				text += "<p class='tips_error'>" + vMsg + "</p>";
			}
			var tips = jqTarget.attr("bmeTips");
			if (tips)
			{
				text += tips;
			}
			
			// 只有自己当前聚焦，才显示
			//对于checkbox,radio这种组件,校验规则绑定在第一个checkradio元素上,且每个checkradio的name相等.
			//我们这里的jqTarget取的一直是第一个元素,document.activeElement是当前聚焦元素 .所以改成判断name不等才return
			if (("" == text) || ($(document.activeElement).filter(":input").attr("name") != jqTarget.attr("name")))
			{
					jBME.tipsObj.hideTips();
					return;
			}

			jBME.tipsObj.poper.setPopFromInfoByDom(jqTipsTarget);
			jBME.tipsObj.poper.popFromInfo.left += 20;
			jBME.tipsObj.poper.popFromInfo.width = 0;
			jBME.tipsObj.poper.bottomFirst = false;
			
			// 有时tips所在窗口较小，需要将tips本身的内容清空，让tips小一点，找到正确的显示位置
			// 再将tips内容放入，tips会在正确位置上自动适应宽度
			jBME.tipsObj.content.html("&nbsp;");
			jBME.tipsObj.width("auto");
			// 先show一下，找位置
			jBME.tipsObj.poper.show();
			
			// 放入内容
			jBME.tipsObj.content.html(text);
			jBME.tipsObj.jqTipsTarget = jqTipsTarget;// 应对tips是因为校验出现而独立弹出的场景
			
			// ie7下，无法自适应显示，必须有确定的width才ok
			jBME.tipsObj.width(jBME.tipsObj.width());

			// 重新show出来			
			jBME.tipsObj.poper.show();

			jBME.tipsObj.attr("arrowTop", jBME.tipsObj.poper.resultBottom);
			jBME.tipsObj.fixSelectorBug();
			jBME.tipsObj.css("z-index", 9999);

			return;
		};
		
	};
})(jQuery);

// 给拥有focusBorder属性的dom节点，加上该属性指定的聚焦效果
(function($) {
	$.fn.focusBorderInit = function() {
		var container = this;
		var infoObjs = container.find("[focusBorder]");
		infoObjs.each(
			function(i) 
			{
				var jqObj = $(this);
				var focusBorder = jqObj.attr("focusBorder");
				jqObj.unbind("focus.border").bind("focus.border", 
						function()
						{
							// 场景：iframe嵌套iframe，嵌套多层后，可能出现通过一次无法聚集
							// 目前不知道如何重现，先保留这段代码吧
					
							// 打开此功能，由要选中一段文字，必须先聚焦，再选中，而默认是可以直接选中的，所以先屏蔽
//							if($.browser.msie)
//							{
//								var r =this.createTextRange();
//								var txt = this.value;
//								r.collapse(true); 
//								r.moveStart('character',txt.length);  
//								r.select();
//							}
						
							jqObj.addClass(focusBorder);
						});
				jqObj.unbind("blur.border").bind("blur.border", 
						function()
						{
							jqObj.removeClass(focusBorder);
						});
			});
	};
})(jQuery);

//给field加上聚焦效果
(function($) {
	$.fn.focusFieldInit = function() {
		var container = this;
		// 有focusField属性，并且没有field_no_bg样式
		var infoObjs = container.find("[focusField]").not(".field_no_bg");
		infoObjs.each(
			function()
			{
				var jqField = $(this);
				var focusField = jqField.attr("focusField");
				jqField.find(":input").each(
					function(i) 
					{
						var jqObj = $(this);
						jqObj.unbind("focus.field").bind("focus.field", 
								function()
								{
									jqField.addClass(focusField);
								});
						jqObj.unbind("blur.field").bind("blur.field", 
								function()
								{
									jqField.removeClass(focusField);
								});
					});
			});
	};
})(jQuery);

//条件选择单选框
(function($) {
	$.fn.conditionCheck = function(initNo) {
		var container = this;
		var titleObj = container.find(".vlist_condition_title");
		var actObjs = titleObj.find(":radio");
		var contentObjs = container.find("div.vlist_condition_content");
		actObjs.each(function(i) {
			var actObj = $(this);
			actObj.bind("click", function() {
				switchContent(i);
			});
		});

		function switchContent(index) 
		{
			contentObjs.hide();
			$(contentObjs[index]).show();
		}
	};
})(jQuery);

//将区域内的attrName转为自定义tips
//@para maxWidth: tip最大宽度
//@para attrName: 需要转换为tips的属性名，默认为title,并且如果是title，则将DOM上的title属性删除
(function ($) {
$.fn.tips = function(s) {
    s = s || {};
    var maxWidth = s.maxWidth || 800;
    var attrName = s.attrName || "title";
    var container = this;
    actObjs = container.find("["+attrName+"]");    
    actObjs.each(function() {
        var actObj = $(this);
        var titleStr = actObj.attr(attrName);
        //alert(titleStr);
        actObj.data("tips", $("<div>" + titleStr + "</div>"));
        if(attrName=="title")
        {
        	actObj.removeAttr("title");
        }

        actObj.unbind("mouseover").bind("mouseover", showTips);
        actObj.unbind("mouseout").bind("mouseout", hideTips);
		    actObj.unbind("click").bind("click", hideTips);
		   
    });

	function initPoper()
	{
		jBME.titleTipsPoper.wrapperFunc = function()
		{
			var jqWrapper = jBME.titleTipsPoper.createShadowWrapper();
			jqWrapper.bind("mouseout", hideTips);
			jqWrapper.removeClass("bf_shadow").addClass("bf_title_tips");
            jqWrapper.css("max-width", maxWidth + "px");   
			return jqWrapper;
		};
	}
    function showTips(event) 
	{
		initPoper();
		jBME.titleTipsPoper.setPop($(this).data("tips"));
		jBME.titleTipsPoper.setPopFromInfoByEvent(event);

		jBME.titleTipsPoper.show();
    }

    function hideTips(event) {
        jBME.titleTipsPoper.hide();
    }
};
})(jQuery);

//点击document区域隐藏指定内容
(function($) {
    $.fn.ExternalClick = function(obj, callback) {
        var actObj = $(this);
        var targetObj = $(obj);
        var randm = Math.random().toString();
		if(callback){
        	var _callback = callback;
		}
        
        actObj.attr("ExternalClickID", randm);
        targetObj.attr("ExternalClickID", randm);
        function mousedownHandler(event) {
            var $target = $(event.target);
            var parentObjs = $target.parents("[ExternalClickID='"+randm+"']");
            var isOver = false;
            if ($target.data("ExternalClickID")==randm) {
                isOver = true;
            }
            if(parentObjs.size()>0)
            {
                isOver = true;
            }
            if(!isOver && targetObj.is(":visible"))
            {
                targetObj.hide();
				if(callback){
                	_callback.call(this, actObj, targetObj);
				}
            }
        }
        $(document).mousedown(mousedownHandler);
    };
})(jQuery);

//操作历史记录方法
(function($) {
	$.fn.historyReport = function( dataString , s ) {
		var maxLength = 10;
		var ulObj = $("#systemMenuContainer").find("ul.shortmenu_his_ul");
		var hisBox = $("#systemMenuContainer").find("div.shortmenu_his_list");
		var submenuBox = $("#systemMenuContainer").find("div.shortmenu_proj_items_list");
		
		if(typeof(s.showHistory) != "undefined" && s.showHistory){
			var historyStr = $.cookie("historyItems");

			if(historyStr != null){
				var historyObj = eval('('+historyStr+')');
				var newReport = eval('('+dataString+')');
				
				for(var i=0; i<historyObj.length;i++){
					if(historyObj[i].id == newReport.id){
						historyObj.splice(i,1);	
					};
				};
				historyObj.push(newReport);
				if(historyObj.length > maxLength){
					historyObj = historyObj.slice((historyObj.length - maxLength));
				};
				
				$.cookie("historyItems",null);
				$.cookie("historyItems",getJsonString(historyObj));
			}
			else {
				var historyStr = "["+dataString+"]";
				$.cookie("historyItems",historyStr);
			};
			
			var historyStr = $.cookie("historyItems");
			var historyObj = eval('('+historyStr+')');
			if(ulObj.html() != ""){
				ulObj.html("");
			};

			for(var j = 0; j<historyObj.length; j++){
				addHistoryList(historyObj[j]);
			};	
		}
		
		//将json数据转为字符串
		function getJsonString(jsonObj){
			var sA = [];
			(function(o){
			   var isObj=true;
			   if(o instanceof Array)
				  isObj=false;
			   else if(typeof o!='object'){
				  if(typeof o=='string')
					  sA.push('"'+o+'"');
				  else
					  sA.push(o);
				  return;
			   }
			   sA.push(isObj?"{":"[");
			   for(var i in o){
				if(o.hasOwnProperty(i) && i!='prototype'){
				 if(isObj)
				  sA.push(i+':');
				   arguments.callee(o[i]);
				   sA.push(',');   
				}
			   }
			   sA.push(isObj?"}":"]");
			})(jsonObj);
			return sA.slice(0).join('').replace(/,\}/g,'}').replace(/,\]/g,']');
		}
		
		//生成history列表
		function addHistoryList(itemData){
			var itemHtml = "<li>";
			itemHtml += "<a href = '#none'>"+ itemData.label +"</a>";
			itemHtml += "</li>";
			
			var hisItem = $(itemHtml).prependTo(ulObj);
			
			if($("#systemMenuContainer").css("display") == "none"){
				$("#systemMenuContainer").show();
				if(hisBox.height() > submenuBox.height()){
					submenuBox.addClass("list_noline");
					hisBox.removeClass("list_noline");
				}
				else {
					hisBox.addClass("list_noline");
					submenuBox.removeClass("list_noline");
				};
				$("#systemMenuContainer").hide();
			};
		
			hisItem.click(function(){
				var tabitem=$.fn.tabPage.add(itemData.label, itemData.url, itemData.id); 
				tabitem.setHelpId(itemsData.helpid);
				$("#systemMenu").removeClass("sysmenu_hover").addClass("sysmenu");
				$("#systemMenuContainer").hide();
				var maskDiv = $("#systemMenuContainer").data("maskDiv");
				$(maskDiv).remove();
			});
		}
	};
})(jQuery);

//多级菜单触发显示
(function($) {
    $.fn.showSubMenu = function(c) {
		var t = 15,
			z = 50,
			s = 6,
			a;	
		var actObj = $(this);
		var _h = []; // 存取有子菜单的项目
		var _c = []; // 存取子菜单
		
		init(c);
		
		function init(c)
		{
				a = c;   // a是鼠标触发时的样式名
			var w = actObj,  // w是菜单容器
				s = $(actObj).find('div.shortsubmenu_sub'), // s是子菜单数组
				l = s.size(), //二级菜单的个数
				i = 0;
				
			for(i; i < l; i++)
			{	var h = $(s[i]).parent("li"); 
				_h[i] = h; 
				_c[i] = s[i];	
				$(_h[i]).data("subNum",i);
				
				_h[i].mouseover(function(){ st($(this).data("subNum"),true); }); 
				_h[i].mouseout(function() { st($(this).data("subNum")); });
			}
		};
        
        function st(x,f)
		{
			var c = _c[x], 
				h = _h[x], 
				p = $(h).find('a:eq(0)');	

			if(c.t){
				clearInterval(c.t); 
			}

			if( f )
			{
				$(p).addClass(a);
				$(c).show();
				$(h).css("overflow","visible");
				c.t = setInterval(function(){sl(c,1);}, t );
			}else
			{
				$(p).removeClass(a); 
				c.t = setInterval(function(){ sl(c,-1);}, t );
			}
		};
		
		function sl(c,f)
		{
			if(f==1){
				$(c).show();
			}
			else {
				$(c).hide();
				$(c).parent("li").css("overflow","hidden");
			}
			clearInterval(c.t);
		};
    };
})(jQuery);

/**
 * Client Server: jQuery-Extend Method. (Plugins)
 */
(function($) 
{
    /**
     * Client Service: toggle disabled state.
     * 
     * @param [disabled]: Optional.  A Boolean indicating whether to disable or enable the components.
     *                    Default is change the status.
     * @see http://api.jquery.com/toggle/
     * @see http://api.jquery.com/toggleClass/
     */
	$.fn.toggleDisabled = function(disabled) 
	{
		if (this.toggleClass("cbme_disabled", disabled).hasClass("cbme_disabled"))
		{//Change to disabled.
			this.attr("disabled", "disabled");
			this.find(":input,.bc,.bc_toggleable").attr("disabled", "disabled").addClass("cbme_disabled");
		}else
		{//Change to enabled.
	        this.removeAttr("disabled");
	        this.find(":input,.bc,.bc_toggleable").removeAttr("disabled").removeClass("cbme_disabled");
		}
		return this;
	};
	
	/**
	 * Force Browser to render CSS Attribute Selector after an attribute was change on DOM.
	 * Note: Cause not all the browsers support CSS Attribute Selector so efficient, 
     *         so we need CSS Class Selector to tell browser to update facade)
	 * Eg: IE/Chrom.
	 */
	$.fn.fixSelectorBug = function()
	{
	    return this.toggleClass("bmeFixSelectorBug");
	};
})(jQuery);
