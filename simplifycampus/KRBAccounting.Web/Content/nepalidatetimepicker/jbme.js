/*!
 * BME JavaScript Object: jBME
 * @created by h00142237 20100531 Huawei Technologies Co., Ltd
 */
;
(function()
{
    //Define our global object(class), global equals to window object. 
    var global = this; 
    var jBME = global.jBME = function()
    {
        this.global = global;
        return this;
    };
    
    //Extend jBME Class function.(Every new jBME() Class instance will have these attribute for functions.)
    jBME.prototype = 
    {
        sample : undefined,
        getSample : function(){ }
    };
    
    /**
     * Use jBME.ready(callback) to add function.
     * Use jBME.ready() to run all  
     * @param func
     * @return
     */
	 
    jBME.readyFunc = [];
    jBME.ready = function(func)
    {
    	// 移动dom节点时，也会触发其中的ready调用，此时并不希望操作此列表
    	if (jBME.lockReady)
    	{
    		return;
    	}
        if(func == undefined)
        {//run 
            while(jBME.readyFunc.length > 0)
            {
                var f = jBME.readyFunc.shift();
                f();
            }
        }else
        {//save function to run when ready.
            jBME.readyFunc.push(func);
        }
    };

    /**
     * Package: jBME.meta.
     */
    jBME.meta = 
    {
        cbme_clear : "cbme_clear",
        cbme_clear_item : "cbme_clear_item",
        cbme_disabled : "cbme_disabled",
        cbme_region_ignore : "cbme_region_ignore",
        cbme_selector : "cbme_selector",
        cbme_validator : "cbme_validator"
    };
    
    /**
     * BME Defined DOM events. NOTE these are NOT html standard events, but perform as.
     */
    jBME.EVENT = 
    {
    };
    
    /**
     * Context.
     */
    jBME.context = 
    {
        path : undefined,
        cid : undefined,   //Conversation ID
        isclosed : false,  //Boolean: Indicate whether the conversation ID has already been closed in server.
        model : undefined, //Business Model(DO)
        view : undefined,  //View(USL)
        token : undefined, //Web Security
        runtime : true     //Boolean: Runtime(min)/Debug.
    };
    
    //Define package: jBME.util .
    jBME.util =
    {
        description : "Define common JavaScript functions in this package.",
        
        /**
         * Get BME URL.
         */
        getURL : function()
        {
            return (jBME.context.cid ? "business.action?BMECID="+jBME.context.cid +"&" : "query.action?") 
                + "BMETimestamp="+(new Date().getTime())
                + (jBME.context.token ? "&BMEWebToken=" + jBME.context.token : "");
        },
        
        /**
         * Return JSON or other Javascript object length.
         */
        objectLength : function(obj)
        {
            var count = 0;
            for ( var i in obj)
            {
                ++count;
            }
            return count;
        },
        
        /**
         * Return a string describe the object attribute.
         */
        toString : function(object, name, includeFunc)
        {
            //default arguments.
            name = (name || object);
            includeFunc = (includeFunc || false);
            
            //ok, get string.
            var str = name + "{ ";
            for(var attr in object)
            {
                if(typeof object[attr] == "function" && !includeFunc)
                {
                    continue;
                }
                str += attr;
                str += ":";
                str += object[attr];
                str += ", ";
            }
            str += "}";
            return str;
        },
        
        /**
         * Get a valid id that can use as jQuery selector. Using \\ before some characters.
         */
        encodeId : function(id)
        {
            return id.replace("[","\\[").replace("]","\\]").replace(".","\\.");
        },
        
        /**
         * Get valid jQuery selector from id(s) expression.
         * You can use the return string directly as jQuery selector to get object(s).
         * You will get an objects set union all selector results.
         * 
         * @param idExp : "id1, id2,  ,id3"  or  "id1"  or  "" or undefined.
         * @return jQuery selector: "#id1,#id2,noneTagSelector,#id3,"  or  "#id1," or "noneTagSelector"
         */
        getIdSelector : function(idExp)
        {
            //Case1: Selector nothing.
            idExp = jQuery.trim(idExp);
            if(idExp == "")
            {
                return "noneTagSelector";
            }
            
            //Case2: 
            var selector = "";
            var ids = idExp.split(",");
            for(var i = 0; i < ids.length; ++i)
            {
                var id = jQuery.trim(ids[i]);
                selector += (id == "" ? "noneTagSelector" : "#" + this.encodeId(id));
                selector += (",");
            }
            return selector;
        },
        
        /**
         * Get elements order by "selector1,selector2,selectorN"
         * Note: After jQuery 1.3.2 version, the elements returned by jQuery(expression, [context])
         * are order in DOM. So this function provides a way to get elements in order by selector.
         */
        getElementsOrderBySelector : function(selector, context)
        {
            var elements = [];
            var selectors = selector.split(",");
            for ( var j = 0; j < selectors.length; ++j)
            {
                var selectorj = jQuery.trim(selectors[j]);
                if(selectorj != "")
                {
                    jQuery(selectorj, context).each(function()
                    {
                        elements.push(this);
                    });
                }
            }
            return jQuery(elements);
        },
        
        /**
         * Replace element(s) according to the given selector in a context
         *   with the element(s) in another context that have the same id.
         * 
         * In other words, the function will replace ALL the element(s) in jqToContext selected by the selector
         *   by the element(s) with the same id in jqFromContext.
         *
         * Note: replacing order is according to elements order in selector(not in DOM) 
         * @param selector   jQuery selector
         * @param jqFromContext  Resources holder context, 
         *          you may need to wrap your resources within an dummy element(eg:<div>), 
         *          cause the jQuery.find() method will not return the element set itself like jQuery.filter() does. 
         * @param jqToContext Default is current document. 
         * @param callbackoneach 
         */
        replaceElementsById : function(selector, jqFromContext, jqToContext, callbackOneach)
        {
            var elementsNew = [];
            this.getElementsOrderBySelector(selector, (jqToContext || document)).each(function(i)
            {
                var jqElement = $(this);
                var jqElementNew = jqFromContext.find(jBME.util.getIdSelector(this.id));
                if (this != jqElementNew[0] && (false!==(callbackOneach ? callbackOneach(this):true)))
                {
                    $(".bc_view").trigger("beforerender", this.id);
			

                    jqElement.replaceWith(jqElementNew);
                    elementsNew.push(jqElementNew[0]);
                }
            });
            return $(elementsNew);
        },

        /**
         * Return function name from standard JS function call specification written in string.
         * @param functionExpresion (String)
         *        eg: "a.b.c.func( 123, 'str, 1234')" 
         * @return String
         *        eg: a.b.c.func
         *        
         */
        getNameByFunExp : function(functionExpresion)
        {
            functionExpresion = (functionExpresion || "");
            var iarg = functionExpresion.indexOf("(");
            if(iarg <= 0)
            {
                return jQuery.trim(functionExpresion);
            }
            return jQuery.trim(functionExpresion.substring(0, iarg));
        },
        
        /**
         * Return Arguments using standard JS function call specification written in string.
         * @param functionExpresion
         *        (String), eg: "a.b.c.func( 123, 'str, 1234')" 
         * @return Array
         *        [123]
         *        ['str, 1234']
         */
        getArgsByFunExp : function(functionExpresion)
        {
            functionExpresion = (functionExpresion || "");
            
            // Replace the function name. eg: getArguments(123, 'str1, 234') 
            var iarg = functionExpresion.indexOf("(");
            if(iarg < 0)
            {
                return [];
            }
            
            function getArguments(){return arguments;}; 
            var callExpression = "getArguments" + functionExpresion.substring(iarg);
            
            // Get arguments array from JavaScript Compiler. 
            var argumentArrayObject = [];
            try { argumentArrayObject = (eval(callExpression) || []); } catch(e){} 
            
            var argumentArray = new Array(argumentArrayObject.length);
            for(var i = 0; i < argumentArrayObject.length; ++i)
            {
                argumentArray[i] = argumentArrayObject[i];
            }
            return argumentArray;
        },
        
        /**
         * Check/set whether a DOM element is disabled within BME context.
         *  (has attribute "disabled" or has css class "cbme_disabled". 
         * @param element DOM element.
         */
        isDisabled : function(element)
        {
        	var jq = jQuery(element);
        	return element && (jq.hasClass(jBME.meta.cbme_disabled) || jq.is("[disabled]"));
        },
        setDisabled : function(element, disabled)
        {
            if(element)
            {
                jQuery(element).toggleClass(jBME.meta.cbme_disabled, disabled);
            }
        }
    };//jBME.util package define.
    
    // Define package: jBME.var
    jBME.vars =
    {
        description : "Save var in this package."
    };
    
    // Define package: jBME.Fire
    jBME.Fire = 
    {
        description : "Define Fire(client-service) support JavaScript functions in this package."
    };
    
    // Define package: 
    jBME.theme = 
    {   
        description : "Define theme support JavaScript functions in this package."    
    };
    
    // Define package: jBME.validate
    jBME.validate =
    {
        description : "Define validate support JavaScript functions in this package.",
        /**
         * Set validate result
         * @param id:     DOM input id or contains input with validator.
         * @param status: success/fail/dependency-mismatch/undefined
         * @param tips
         */ 
        setResult : function(id, status, tips)
        {
            var jq = jQuery("#" + id);
            if(!jq.hasClass(jBME.meta.cbme_validator))
            {
                jq = jq.find(".cbme_validator");
            }
            
            if (status == undefined)
            {
            	jq.removeAttr("vStatus");
            }else
            {
                jq.attr("vStatus", status).attr("vMsg", tips);
            }
        }
    };//validate
    
    // Define package: jBME.web 
    jBME.web = 
    {
        response :
        {
            head : "{BMEResult:",
            tail : "HEAD-END",
            example : "{BMEResult:{validation:'fail',exception:true}}HEAD-END\n" 
                + "function(){//javascript codes;}  or HTML text."
        },
        /**
         * Parse server web response.
         * @param responseText
         * @return
         */
        parseResponse : function(responseText)
        {
            //left trim.
            responseText = responseText.replace(/(^\s*)/g, "");
            
            // Response object.
            var response =
            {
                head : jBME.web.response.head,      //string
                body : responseText,                //string
                result : {},                        //BMEResult
                hasStrutsException : function(){return this.result.strutsException;},
                hasException : function(){return this.result.exception;},
                hasError : function(){return this.result.error;},
                passValidate : function(){return this.result.validation != 'fail';}
            };
            
            // parser.
            var iTail = responseText.indexOf(jBME.web.response.tail);
            if ((response.head == responseText.substr(0, response.head.length)) && iTail > 0)
            {
                // get first line(json string) as head.
                response.head = responseText.substring(0, iTail);
                var jsonObj = eval('(' + response.head + ')');
                response.result = (jsonObj.BMEResult || {});

                // get body.
                response.body = ((iTail < responseText.length) ? responseText.substring(iTail
                        + jBME.web.response.tail.length, responseText.length) : "");
            }
            return response;
        },
        
        /**
         * If BME is navigating to another page, we should not close the conversation(etc..)
         */
        isnavigating : false,
        navigate : function(url)
        {
            this.isnavigating = true;
            window.location.replace(url);
        }
    };//jBME.web 
    
    /**
     * BME Preset Initialization.
     * 1)Run all the saved functions you have registered using jBME.read( functionxxx(){} )
     * 2)Other initialization. 
     */
    jQuery(document).ready(function($)
    {
        jBME.ready();
        
        // Initialization for page (body), trigger load event (again) for binders after page loaded.
        var jqView = jQuery(".bc_view");
        jqView.trigger("load");
        jQuery(document.body).onLoadHelp();
        
        // BME Cleanup
        var jqWin = jQuery(window);
        jqWin.bind("beforeunload.bme unload.bme", function(event)
        {
            //'unload' is OK while 'beforeunload' is better, however not all the browsers support 'beforeunload'(Not W3C).
            if(event.type == "beforeunload")
            {
                jqWin.unbind("unload.bme");
            }
            
            // Notify the view before unload, in which can prevent default action by user.
            event.type = "unload";
            jqView.trigger(event);
            if(event.isDefaultPrevented())
            {
                return false;
            }
            
            // Cleanup when close the page who starting the conversation(cid).
            var ignore = jBME.web.isnavigating;
            ignore |= (parent != self && parent.jBME && parent.jBME.context.cid == jBME.context.cid);
            ignore |= (window.frameElement && frameElement.popWindow && frameElement.popWindow.jBME 
                        && frameElement.popWindow.jBME.context.cid == jBME.context.cid);
            if(!ignore)
            {   
                jqWin.closeConversation();
            }
        });
    });
})();


/**
 * Define Class
 */
(function()
{
    /**
     * Define Class and its constructor: BME Event FireTag.
     * @param id
     */
    jBME.FireTag = function(id, parentid, event, mode)
    {   
        this.init();
        
        this.setId(id || ("FireTag_" + (new Date().getTime())) );
        this.setParentid(parentid || "");
        this.setEvent(event || "click");
        this.setMode(mode || "");
        
        this.setModel(jBME.context.model);
        this.setView(jBME.context.view);
        return this;
    };

    /**
     * Define Class members
     */
    jBME.FireTag.prototype = 
    {
        /**
         * The Fire Tag id, which equals to its container DOM id
         */
        id : "",
        jqFire : undefined,
        setId : function(id)
        {
            this.id = id;
            this.jqFire = jQuery("#"+id);
            if(this.jqFire.length == 0)
            {
                this.jqFire = jQuery('<div id="'+this.id+'" class="bc_fire"></div>');
            }
            this.jqFire.data("FireTag", this);
        },

        /**
         * Parent Tag DOM id.
         */
        parentid : "",
        setParentid : function(parentid){ this.parentid = parentid; },
        
        /**
         * Binding event(s).  eg: "click,blur"
         */
        event : "",
        setEvent : function(event){ this.event = (event || "").replace(",", " "); },
        
        /**
         * Fire Mode, eg: validate, render, navigate, popup, popin ...
         */
        mode : "",
        setMode : function(mode)
        {
            this.mode = mode;
            if(this.mode != undefined)
            {
                this.options.data["bmeEvent.mode"] = mode;
            }
        },
        
        /**
         * explicitly set the element id(s) to which the event will bind.
         * Default, the event will bind to parentid or its agents.
         * 
         * Multi id supported!  eg: "id1,id2,id3,id4"
         */
        bindid : undefined,
        setBindid : function(bindid){ this.bindid = bindid; },
        
        /**
         * Return real DOM element (id)s to which the event will really bind.
         * Note, for "really" means:
         * 1) When no explicitly set bindid, event will bind to parentid or its agents. 
         * 2) When explicitly set the bindid, the event will directly bind to them.
         * @return
         */
        getRealBindids : function()
        {
            return jQuery(this.bindid ? this.bindid.split(",") : jBME.Fire.util.getFireAgents(this.parentid));
        },
        getBinders : function()
        {
            var binders = [];
            var bindids = this.getRealBindids();
            for ( var i = 0; i < bindids.length; ++i)
            {   
                var idselector = jBME.util.getIdSelector(bindids[i]);
                var jq = ( (idselector.indexOf("#popwin_") >= 0) 
                        ? jQuery(window.frameElement, parent.document).closest(".popwin").find(idselector) 
                        : jQuery(idselector));
                if(jq.length > 0)
                {
                    binders.push(jq[0]);
                }
            }   
            return jQuery(binders);
        },
        
        /**
         * Set Region Id and its selector.
         */
        regionid : undefined,
        regionSelector : "#body",
        setRegionid : function(regionid)
        {
            this.regionid = regionid;
            if(regionid != undefined)
            {
                this.regionSelector = jBME.util.getIdSelector(regionid);
                this.options.data["bmeEvent.regionid"] = regionid;
            }else
            {
                this.regionSelector = document;
                delete this.options.data["bmeEvent.regionid"];
            }
        },
        
        /**
         * Set Target Id and its selector.
         * Support idExp. eg: "id1, id2, id3, , id4", result is union set of valid id.
         */
        targetid : undefined,
        targetSelector : "dummytag",
        setTargetid : function(targetid)
        {
            this.targetid = targetid;
            if(targetid != undefined)
            {
                this.targetSelector = jBME.util.getIdSelector(targetid);
                this.options.data["bmeEvent.targetid"] = targetid;
            }else
            {
                this.targetSelector= "dummytag";
                delete this.options.data["bmeEvent.targetid"];
            }
        },
        
        /**
         * Validation
         */
        validation : true,
        setValidation : function(validation)
        {
            this.validation = validation;
            if(validation != undefined)
            {
                this.options.data["bmeEvent.validation"] = ("" + validation);
            }
        },
        
        /**
         * Service.
         * 1) Client Service: jQuery Plugin Method.
         * 2) Server Service: Java ServiceBean Method.
         *    Note: To support eval service arguments from DOM, we deny the arguments eval to execute period.
         */
        service : undefined,
        setService : function(service) { this.service = service; },
        syncServiceArgs : function()
        {
            delete this.options.data["bmeEvent.service"];
            delete this.options.data["BMEParam"];
            if(this.service != undefined)
            {
                this.options.data["bmeEvent.service"] = jBME.util.getNameByFunExp(this.service);
                var argumentsArray = jBME.util.getArgsByFunExp(this.service);
                if(argumentsArray && argumentsArray.length > 0)
                {
                    this.options.data["BMEParam"] = argumentsArray;
                } 
            }
            return this;
        },
        
        /**
         * targetStep
         */
        targetstep : undefined,
        setTargetstep : function(targetstep)
        {
            this.targetstep = targetstep;
            if(targetstep != undefined)
            {
                this.options.data["bmeEvent.targetstep"] = targetstep;
            }
        },
        
        /**
         * Render source id.
         */
        sourceid : undefined,
        setSourceid : function(sourceid)
        {
            this.sourceid = sourceid;
            if(sourceid != undefined)
            {
                this.options.data['bmeEvent.sourceid'] = sourceid;
            }
        },
        
        /**
         * Render source View(USL)
         */
        view : undefined, 
        setView : function(view)
        {
            this.view = view;
            if(view != undefined)
            {
                this.options.data['bmeEvent.view'] = view;
            }
        },
        
        /**
         * Model(Service Bean)
         */
        model : undefined,
        setModel : function(model)
        {
            this.model = model;
            if(model != undefined)
            {
                this.options.data['bmeEvent.model'] = model;
            }
        },
        
        /**
         * Export.
         * @param export_
         * @return
         */
        setExport : function(export_)
        {
        	this.dynattr["export"] = export_;
            if(export_ != undefined)
            {
                this.options.data['bmeEvent.export'] = export_;
            }
        },

        /**
         * Progress monitor. Default is true, using monitor defined in theme.
         */
        monitor : true,
        setMonitor : function(monitor){ this.monitor = monitor; },
        
        /**
         * popup
         */
        popup : undefined,
        setPopup : function(title, width, height)
        {
            this.popup = {title:title, width:width, height:height};
        },
        
        /**
         * Custom callback function before Fire ajax send.
         * Fire will continue to action only if custom callback return true.
         * eg: onbefore : "onbeforeExample(this, args0, argn...){ return true; }",
         */
        onbefore : undefined,
        setOnbefore : function(onbefore){ this.onbefore = onbefore; },
        callOnbefore : function()
        {
            var result = (this.onbefore instanceof Function ? this.onbefore.apply(this) : eval(this.onbefore));
            if(this.jqFire && result !== false)
            {
                this.jqFire.triggerHandler("before");
            }
            return result;
        },

        /**
         * Client Script.
         * @return false will stop fire execute flow.
         */
        script : undefined,
        setScript : function(script){ this.script = script; },
        callScript : function()
        { 
            return (this.script instanceof Function ? this.script.call(this) : eval(this.script));
        },
        
        /**
         * Custom callback function when get response from server.
         * @param response, object return by jBME.web.parseResponse.response
         */
        onresponse : undefined,
        setOnresponse : function(onresponse){ this.onresponse = onresponse; },
        callOnresponse : function(response)
        {
            var result = (this.onresponse instanceof Function ? this.onresponse.call(this, response) : eval(this.onresponse)); 
            if(this.jqFire && result !== false)
            {
                this.jqFire.triggerHandler("response");
            }
            return result;
        },
        
        /**
         * Custom callback function after each target element has been replaced and ready.
         * @return true/false,   true to continue render next target; false to stop.
         */
        oneach : undefined,
        setOneach : function(oneach){ this.oneach = oneach; },
        callOneach : function(target)
        {
            var result = (this.oneach instanceof Function ? this.oneach.call(this, target) : eval(this.oneach));
            if(this.jqFire && result !== false)
            {
                this.jqFire.triggerHandler("each");
            }
            return result;
        },
        
        /**
         * onerror 这部分代码特别的丑陋，需要重构！
         * @return 
         */
        onerror : undefined,
        setOnerror : function(onerror)
        {
        	this.onerror = onerror; 
        	if(onerror != undefined)
        	{
        		this.options.data["bmeEvent.errorMode"] = onerror;
        	}
        },
        callOnerror : function(jFireTag,response)
        {
        	var errorStr = jFireTag.onerror;
        	
        	if (errorStr == undefined)
        	{
        		var modeStr = jFireTag.mode;
        		if (modeStr=="render"
        			|| modeStr=="export"
        			|| modeStr=="validate"
        			|| modeStr=="webutil")
        		{
        			errorStr="popup";
        		}
        	}
        	
        	if (errorStr instanceof Function)
        	{
        		errorStr.call(this);
        	}
        	else
        	{
	        	if (errorStr=="popup")
	        	{
		            // popup
	                var jFireTagRender = new jBME.FireTag();
	                jFireTagRender.setMode("popup");
	                jFireTagRender.setRegionid("");
	                jFireTagRender.setTargetid("");
	                jFireTagRender.popup = (this.popup || {title:response.result.poptitle , width:500, height:350});
	                jFireTagRender.setValidation(false);
	                jBME.Fire.execute(jFireTagRender);
	        	}
	        	else
	        	{
	        		var url = jFireTag.options.url;
	        		jBME.web.navigate(url);
	        	}
        	}
        },
        
        /**
         * Custom callback function after Fire ajxa send and after Fire handled server response.
         * eg: onafter "onbeforeExample(this, args0, argn...){  }",
         */
        onafter : undefined,
        setOnafter : function(onafter){ this.onafter = onafter; },
        callOnafter : function()
        {
            var result = (this.onafter instanceof Function ? this.onafter.call(this) : eval(this.onafter));
            if(this.jqFire && result !== false)
            {
                this.jqFire.triggerHandler("after");
            }
        },
        
        /**
         * Dynamic attribute array.
         * Access dynamic attributes this way: jFireTag.dynatrr["att1"] or jFireTag.dynattr.attr1
         */
        dynattr : undefined,
        
        /**
         * jQuery.Event object. Holding event information in fire processing.
         * Only accessible when fire is processing.
         * NOTE: global window.jEvent will be same to this until another fire fired within this fire processing.
         */
        jEvent : undefined,
        
        /**
         * Holding POST data we will send to server.
         * By providing this attribute, we give you a chance to add/modify/remove POST data every time when event fires.
         * Actually, we use this attribute object instance as the real parameter of jQuery.ajax() 'options.data'.  
         * NOTE: Only accessible when fire is processing. And changes every time we fire.
         */
        postdata : undefined,
        
        /**
         * String describe this object.
         */
        toString : function(){ return jBME.util.toString(this, "jBME.FireTag"); },
        
        /**
         * Data initialize.
         * Note: Data members should be initialized here,
         *   to prevent sharing one member instance between all object instances.
         * 
         * For 'options' details: 
         * @see http://api.jquery.com/jQuery.ajax
         */
        options : undefined,
        init : function()
        {
            this.options =
            {
                url : jBME.util.getURL(),
                type : "post",
                cache : false,
                data : {"BMEClear" : [], "BMEClearItem": []},
                dataType : "text",
                beforeSubmit : function(data, jqRegion, options){return true;},
                success : function(responseText, statusText, xhr){},
                error : function(xhr, textStatus, errorThrown){}
            };
            this.dynattr = [];
        }
    };
})();

/**
 * BME Fire(Event) Support.
 */
jBME.Fire.util = 
{
    /**
     * Fire will bind to element(id) or its agents(input) element.
     * This is important, for <bme:field> Fire will bind to its inner <input>,
     *    which is called the Fire Agent of field.
     * Note: Agent can still has its Fire Agent... 
     * eg: <bme:field> ---agent---> <input> ---agent---> checkbox1, checkbox2, checkbox3
     */
    agent : "jBME.Fire.util.agent",
    addFireAgent : function(id, agentId)
    {
        var jq = jQuery("#"+id);
        if (jq.length == 0)
        {
            return;
        }
        
        if(jq.data(jBME.Fire.util.agent) == undefined)
        {
            jq.data(jBME.Fire.util.agent, []);
        }
        jq.data(jBME.Fire.util.agent).push(agentId);
        
        //agent cbme_disabled
        if(jq.hasClass(jBME.meta.cbme_disabled))
        {
            jQuery("#"+agentId).addClass(jBME.meta.cbme_disabled);
        }
    },
    /**
     * Append Preset-Fire Agents.
     */
    appendPresetFireAgents : function(jq, agents)
    {
        //append the inner ':inputs' as the given <bme:field>'s fire agents.
        if(jq.hasClass("bc_field"))
        {
            jQuery(":input", jq).each(function()
            {
                if(!agents[this.id])
                {
                    agents.push(this.id);
                    if(jBME.util.isDisabled(jq[0]))
                    {
                        jBME.util.setDisabled(this, true);
                    }
                }
            });
        }
    },
    /**
     * Get a element(id)'s fire agents.
     * @param resultArray : optional, can be undefined.
     */
    getFireAgents : function(id, resultArray)
    {
        resultArray = (resultArray || []);
        
        // Get the direct agents, and append the preset ones; 
        var jq = jQuery("#"+id);
        var agents = (jq.data(jBME.Fire.util.agent) || []);
        this.appendPresetFireAgents(jq, agents);
        
        // If element(id) has not any fire agents, take itself in
        if(agents.length == 0 && !resultArray[id])
        {
            resultArray.push(id); 
        }
        
        // Return all the element(id)'s fire agents (recursively) 
        for(var i = 0; i < agents.length; ++i)
        {
            if(id == agents[i])
            {//id 's agent can't be id itself!
                resultArray.push(id);
            }else
            {//deep first recursive: find this agent's agents.
                this.getFireAgents(agents[i], resultArray);
            }
        }
        return resultArray;
    },
    
    /**
     * Validate. 
     * Normally, server will NOT send validation success information, so the client should
     * set all the submit region whose has validate rules to success status.
     */
    setRegionValidateSuccess : function (jFireTag)
    {
        jQuery(jFireTag.regionSelector).getInputs().each(function()
        {
            if( jBME.util.objectLength( $(this).rules() ) > 0)
            {
                jBME.validate.setResult(this.id, "success", "");
            }
        });
    }
};

/**
 * Define Class --- jBME.Fire.Mode
 */
(function()
{
    /**
     * Define Class and its constructor
     */
    jBME.Fire.Mode = function(mode)
    {   
        this.mode = mode;
        this.init();
        
        this.SUPER = jBME.Fire.Mode.prototype;
        return this;
    };
    
    /**
     * Fire Mode Register
     */
    jBME.Fire.Mode.register = function(func)
    {
        if(func instanceof Function)
        {
            func.apply();
        }
    };
    
    /**
     * Define Class members
     */
    jBME.Fire.Mode.prototype = 
    {
        /**
         * Initialize: mode name and instances.
         */ 
        mode : "anonymous",
        init : function()
        {
            jBME.Fire.Mode[this.mode] = this;
        },
    
        /**
         * Execute Fire
         * @param jFireTag [jBME.FireTag] object, required!
         * @param [jEvent] [jQuery.Event] object, optional parameter.  
         * 
         * More details and properties about 'jEvent', see documents below:
         * @see [jQuery.Event object] http://api.jquery.com/category/events/event-object/
         * @see <<JavaScript Bible 6th>> "Chapter 25: Event Objects".
         */
        execute : function(jFireTag, jEvent)
        {     
            // Initialize
            this.onInit(jFireTag, jEvent);
            
            // Detect disabled?
            if(jEvent && jBME.util.isDisabled(jEvent.currentTarget))
            {
                return false;
            }
            
            // Can execute?
            if(this.canExecute(jFireTag) === false)
            {
                return false;
            }
            
            // Before i do ...
            var jqRegion = jQuery(jFireTag.regionSelector);
            var jqInputs = jqRegion.getInputs();
            if(this.onBefore(jFireTag, jqInputs) === false)
            {
                return false;
            }
            
            // Pre-handing data.
            var options = jQuery.extend(true, {}, jFireTag.syncServiceArgs().options);
            options.data = jQuery.extend(jFireTag.postdata, options.data);
            this.onVisit(jFireTag, jqInputs, options.data);
            
            // AJAX region submit,  and start the asynchronous executing-flow ...
            jFireTag.callScript();
            this.onLock(jFireTag);
            jqRegion.ajaxRegionSubmit(options);
        },
        
        /**
         * Initializing before execute.
         */
        onInit : function(jFireTag, jEvent)
        {
            var jMode = this;
            jFireTag.options.success = function(responseText, statusText, xhr)
            {
                jMode.onResponse(jFireTag, responseText, statusText, xhr);
            };
            jFireTag.options.error = function(xhr, statusText, thrownError)
            {
                jMode.onError(jFireTag, xhr, statusText, thrownError);
            };
            
            // Clean data we have sent. 
            jFireTag.postdata = {};
            
            //Update global jQuery.Event variable, the object just like window.event in IE,
            //   and onclick="clickHander(event)" in NN,Firefox,W3C.
            //@see [jQuery.Event object] http://api.jquery.com/category/events/event-object/
            //@see <<JavaScript Bible 6th>> "Chapter 25: Event Objects".
            if(jEvent != undefined)
            {
                // 'jFireTag' and 'jEvent' share one 'postdata' instance.
                // Giving you a chance to change post data through jQuery.Event object in event flow. 
                if(jEvent.postdata)
                {
                    jFireTag.postdata = jEvent.postdata;
                }else
                {
                    jEvent.postdata = jFireTag.postdata;
                }
                    
                // 1.Save the instance in this fire execute process flow.
                // 2.Global instance. May be changed if another Fire executed.
                jFireTag.jEvent = jEvent;
                window.jEvent = jEvent;
            }
        },
        
        /**
         * Fire Lock/Unlock.
         */
        lockTarget : undefined,
        onLock : function(jFireTag)
        {
            if(jFireTag.jEvent)
            {
                this.lockTarget = jFireTag.jEvent.currentTarget;
                jBME.util.setDisabled(this.lockTarget, true);
            } 
            if(jFireTag.monitor)
            {
                jBME.theme.block.on(jFireTag);
            }
        },
        onUnlock : function(jFireTag)
        {
            if(jFireTag.monitor)
            {
                jBME.theme.block.off(jFireTag);
            }
            if(jFireTag.jEvent && this.lockTarget)
            {
                jBME.util.setDisabled(this.lockTarget, false);
                this.lockTarget = undefined;
            }
        },
        
        /**
         * Return false to prevent execute.
         * @param jFireTag
         * @return
         */
        canExecute : function(jFireTag)
        {
            return true;
        },

        /**
         * Execute when: before sending AJAX request.
         * @return false to prevent executing...
         */
        onBefore : function(jFireTag, jqInputs)
        {
            // Validate, 
            if (jFireTag.validation && !jqInputs.valid(true))
            {
                return false;
            }
            
            // Customer event: onbefore
            if(jFireTag.callOnbefore() === false)
            {
                return false;
            }
        },
        
        /**
         * Execute when: data pre-handling before sending AJAX request.
         */
        onVisit : function(jFireTag, jqInputs, data)
        {
            for ( var i = 0, max = jqInputs.length; i < max; ++i)
            {
                var element = jqInputs[i];  
                this.onVisitElement(jFireTag, element, data);
            }
        },
        
        /**
         * Execute when: Data pre-handling for each element to submit.
         * @param jFireTag
         * @param element  DOM element.
         * @param data: jFireTag.jEvent.data, which will post to server. You can add/modify data here.
         */
        onVisitElement : function(jFireTag, element, data)
        {
            var jqElement = jQuery(element);
            jqElement.trigger({type:"beforesubmit", postdata:data});
            if (jqElement.hasClass(jBME.meta.cbme_clear) && !data["BMEClear"][element.name])
            {
                data["BMEClear"][element.name] = true;
                data["BMEClear"].push(element.name);
            }
            
            if (jqElement.hasClass(jBME.meta.cbme_clear_item))
            {
                var clearItem = jqElement.attr("orgName") + ":" + jqElement.val();
                if(!data["BMEClearItem"][clearItem])
                {
                    data["BMEClearItem"][clearItem] = true;
                    data["BMEClearItem"].push(clearItem);
                }
            }
        },
        /**
         * Error Handling: execute when single AJAX request error.
         * @return
         */
        onError : function(jFireTag, xhr, statusText, thrownError)
        {
            this.onUnlock(jFireTag);
        },
        
        /**
         * Execute when: server response received.
         * @param responseText
         * @param statusText
         * @param xhr
         */
        onResponse : function(jFireTag, responseText, statusText, xhr)
        {
            var response = jBME.web.parseResponse(responseText);
            var branchResult = undefined;
            if(jFireTag.callOnresponse(response) !== false)
            {
                // Update validate result.
                this.onResponseValidateResult(jFireTag, response);
                
                // Execute different branch.
                if(response.hasStrutsException())
                {
                    branchResult = this.onResponseStrutsException(jFireTag, response);
                }
                else if(response.hasException())
                {
                    branchResult = this.onResponseException(jFireTag, response);
                }
                else if(response.hasError())
                {
                    branchResult = this.onResponseError(jFireTag, response);
                }
                else if(response.passValidate())
                {
                    branchResult = this.onResponseSuccess(jFireTag, response);
                }
                else
                {
                    branchResult = this.onResponseFailed(jFireTag, response); 
                }
            };
            
            // Unlock.
            this.onUnlock(jFireTag);
            
            // Execute next.
            if(branchResult !== false)
            {
                this.onAfter(jFireTag);
            }
        },
        
        /**
         * Execute when: server response validate success.
         */
        onResponseValidateResult : function(jFireTag, response)
        {
            if(jFireTag.validation)
            {
                if(response.passValidate())
                {
                    jBME.Fire.util.setRegionValidateSuccess(jFireTag); 
                }else if(!response.hasException())
                {
                    try{ eval(response.body); } catch(e){}; 
                }
                
                jBME.theme.validate.doAfter(jQuery(jFireTag.regionSelector).getInputs());
            }
        },
        
        /**
         * Execute when: server response and to execute success branch.
         * Overwrite this function to offer different handers within different mode.
         * 
         * @return false will prevent executing next step.
         */
        onResponseSuccess : function(jFireTag, response)
        {
            // do nothing when default......
        },
        
        /**
         * Execute when: server response and to execute failed branch.
         * @return false will prevent executing next step.
         */
        onResponseFailed : function(jFireTag, response)
        {
            return false;
        },

        /**
         * Execute when: server response and to execute Error branch.
         * @return false will prevent executing next step.
         */
        onResponseError : function(jFireTag, response)
        {
        	jFireTag.callOnerror(jFireTag, response);
            return false;
        },
        
        /**
         * Execute when: server response and to execute exception branch.
         * @return false will prevent executing next step.
         */
        onResponseException : function(jFireTag, response)
        {
            //jBME.web.navigate(jFireTag.options.url);
        	jFireTag.callOnerror(jFireTag, response);
            return false;
        },
        
        /**
         * Execute when: struts exception.
         */
        onResponseStrutsException : function(jFireTag, response)
        {
        	if (response.result.errorMode=="popup")
        	{
        		jQuery.popwin.model(response.result.poptitle, "exceptionProcessor.action", "600px", "350px");
        	}
        	else
        	{
        		jBME.web.navigate("exceptionProcessor.action");
        	}
            return false;
        },
        
        /**
         * Execute when: last step of client handling.
         */
        onAfter : function(jFireTag)
        {
            jFireTag.callOnafter();

            // Clear jQuery.Event object and 'postdata' after event handler finished.
            window.jEvent = undefined;
            jFireTag.jEvent = undefined;
            jFireTag.postdata = undefined;
        }
    };//jBME.Fire.Mode.prototype
})();

/**
 * Active FireTag.
 */
(function()
{
    /**
     * Get a mode and fire! 
     * @param jFireTag
     *            [jBME.FireTag] object, required!
     * @param [jEvent]
     *            [jQuery.Event] object, optional parameter.
     */
    jBME.Fire.execute = function(jFireTag, jEvent)
    {
        var jMode = jBME.Fire.Mode[jFireTag.mode];
        if(jMode instanceof jBME.Fire.Mode)
        {
            jMode.execute(jFireTag, jEvent);
        }
    };
    
    /**
     * bind a jBME.Fire function to hander the tag.
     * 1) When No explicitly set bindid, event will bind to parent or its agents.
     * 2) When explicitly set the bindid, the event will bind to them.
     */
    jBME.Fire.bind = function(jFireTag)
    {
        jFireTag.getBinders().bind(jFireTag.event, function(jEvent)
        { 
            jBME.Fire.execute(jFireTag, jEvent);
        });
    };
})();

/**
 * Define and register BME Fire Mode.
 */
(function()
{
    /**
     * Fire Mode: validate
     */
    jBME.Fire.Mode.register(function()
    {
        var jMode = new jBME.Fire.Mode("validate");
        jMode.canExecute = function(jFireTag)
        {
            jFireTag.setTargetid("");  //Ignore the targetid set previous in tag
        };
    });
    
    /**
     * Fire Mode: render
     */
    jBME.Fire.Mode.register(function()
    {
        var jMode = new jBME.Fire.Mode("render");
        jMode.onResponseSuccess = function(jFireTag, response)
        {
            var jqResource = jQuery("<div>"+response.body+"</div>");
            var jqElements = jBME.util.replaceElementsById(
                    jFireTag.targetSelector, jqResource, document, function(element)
            {
                return jFireTag.callOneach(element); 
            });
            jBME.ready();
            jqElements.onLoadHelp();
        };
    });
    
    /**
     * Fire Mode: render
     */
    jBME.Fire.Mode.register(function()
    {
        var jMode = new jBME.Fire.Mode("export");
        jMode.canExecute = function(jFireTag)
        {
            jFireTag.setTargetid("");  // Ignore the targetid set previous in tag
        };
        jMode.onResponseSuccess = function(jFireTag, response)
        {
        	var $iframe = $("#bmefile");
        	if ($iframe.size() > 0)
        	{
				$iframe.remove();
        	}
        	
        	var downloadFileUrl = "download.action?bmeEvent.filename=" + response.body
        	+"&BMETimestamp="+(new Date().getTime()) + "&BMEWebToken=" + jBME.context.token;
        	$iframe = $('<iframe id="bmefile" src="'+ downloadFileUrl +'"/>');
        	$iframe.css({ position: 'absolute', top: '-1000px', left: '-1000px' });
        	$iframe.appendTo("body");
        };
    });
    
    /**
     * Fire Mode: render
     */
    jBME.Fire.Mode.register(function()
    {
        var jMode = new jBME.Fire.Mode("widget");
        jMode.onError = function(jFireTag, xhr, statusText, thrownError)
        {
            this.SUPER.onError.apply(this, arguments);
            var jqWidgetPad = jFireTag.dynattr["jqWidgetPad"];
            if(jqWidgetPad)
            {
                var jqMsg = jQuery("label", jqWidgetPad);
                jqMsg.text(xhr.status + " " + xhr.statusText);
                jqMsg.toggle();
                jqWidgetPad.attr("title", jFireTag.view);
                jqWidgetPad.html(jqMsg);
            }
        };
        jMode.onResponseSuccess = function(jFireTag, response)
        {
            if(jFireTag.targetid)
            {//Part of a view.
                jBME.Fire.Mode["render"].onResponseSuccess.apply(this, arguments);
            }else if(jFireTag.dynattr["jqWidgetPad"])
            {//The whole view.
                var jqWidgetPad = jFireTag.dynattr["jqWidgetPad"];
                var jqView = jQuery(response.body).filter(".page"); 
                //var jqNew = jqView.find(".layout_center");  //TEMPCODE:-Bug for view(layout.ftl layout())
                //jqNew.removeClass("layout_center");         //TEMPCODE:-Bug for view(layout.ftl layout())
                jqWidgetPad.replaceWith(jqView);
                jBME.ready();
                jqView.onLoadHelp();
            }
        };
    }); 
    
    /**
     * Fire Mode: navigate
     */
    jBME.Fire.Mode.register(function()
    {
        var jMode = new jBME.Fire.Mode("navigate");
        jMode.onResponseSuccess = function(jFireTag, response)
        {
            if(jFireTag.targetid != undefined)
            {
                //ajax: request url page into target.
                jQuery.post(jFireTag.options.url, {}, function(data)
                {
                    jQuery(jFireTag.targetSelector).replaceWith(data);
                    jBME.ready();
                    
                    jMode.onAfter(jFireTag);
                }, "text");
                
                // DONOT process default next step, and execute self.
                return false;
            }else
            {
                jBME.web.navigate(jFireTag.options.url);
            }
        };
    });
    
    /**
     * Fire Mode: popup
     */
    jBME.Fire.Mode.register(function()
    {
        var jMode = new jBME.Fire.Mode("popup");
        jMode.onResponseSuccess = function(jFireTag, response)
        {
            // popup
            jQuery.popwin.firePopup(jFireTag.popup.title, jFireTag.options.url, 
                    popin_handler, jFireTag.popup.width, jFireTag.popup.height);
            
            // Handler when popup-window popin. @see Fire mode popin.
            function popin_handler(popinArgument)
            {
                // Handler the delayed popin exception.
                popinArgument = (popinArgument || []);
                var jFireTagPopin = popinArgument[0];
                var exceptionResponse = popinArgument[1];
                if(exceptionResponse)
                {
                    jMode.onResponseStrutsException(jFireTagPopin, exceptionResponse);
                    return false;
                }
                
                // Normally...
                if(jFireTagPopin && (jFireTagPopin.targetid || jFireTag.targetid))
                {
                    var jFireTagRender = new jBME.FireTag();
                    jFireTagRender.setMode("render");
                    jFireTagRender.setRegionid("");
                    jFireTagRender.setValidation(false);
                    jFireTagRender.setTargetid(jFireTagPopin.targetid || jFireTag.targetid);
                    jFireTagRender.setOneach(jFireTagPopin.oneach);
                    jFireTagRender.setOnafter(function()
                    {
                        jMode.onAfter(jFireTag);
                    });
                    jBME.Fire.execute(jFireTagRender);
                }else
                {
                    jMode.onAfter(jFireTag);
                }
            }
            
            // DONOT process default next step, and execute within this mode.
            return false;
        };
    });
    
    /**
     * Fire Mode: popin
     */
    jBME.Fire.Mode.register(function()
    {
        var jMode = new jBME.Fire.Mode("popin");
        jMode.onResponseStrutsException = function(jFireTag, response)
        {//Close popup-window, and delay the default exception handler to parent page(which popup-ed this window).
            jQuery.popwin.closeSelect(arguments);
            return false;
        };
        jMode.onAfter = function(jFireTag)
        {//Close popup-window, normally.
            this.SUPER.onAfter.apply(this, arguments);
            jQuery.popwin.closeSelect(arguments);
        };
    });
    
    /**
     * Fire Mode: webutil
     */
    jBME.Fire.Mode.register(function()
    {
        var jMode = new jBME.Fire.Mode("webutil");
        jMode.canExecute = function(jFireTag){ jFireTag.setTargetid(""); };
    });
    
    /**
     * Fire Mode: client
     */
    jBME.Fire.Mode.register(function()
    {
        var jMode = new jBME.Fire.Mode("client");
        /**
         * In 'client' mode, 'service' means jQuery(include plugins) methods.
         * eg: jFireTag.service="toggle().toggleRegionIgnore()"
         * --->Actually, call: jQuery(targets).toggle().toggleRegionIgnore();
         */
        jMode.callService = function(jFireTag)
        {
            if(jFireTag.service)
            {
                var serviceFunc = new Function("var f=this." + jFireTag.service + "\n; if(f instanceof Function){f.call(this);}");
                var $targets = $(jFireTag.targetSelector);
                if($targets.size() > 0)
                {
                    $targets.each(function()
                    {
                        if(jFireTag.callOneach(this) !== false)
                        {
                            serviceFunc.call( $(this) );
                        }
                    });
                }else
                {
                    serviceFunc.call($target);
                }
            }
        };
        jMode.execute = function(jFireTag, jEvent)
        {
            // Initialize & Detect disabled
            this.onInit(jFireTag, jEvent);  
            if(jEvent && jBME.util.isDisabled(jEvent.currentTarget))
            {
                return false;
            }
            
            // Run only client script! No AJAX!     
            if(jFireTag.callOnbefore() !== false)
            {
                jFireTag.callScript();
                this.callService(jFireTag);
                jFireTag.callOnafter();
            }
        };
    });//end.Fire Mode: client.
})();

/**
 * Extension of bme:fire
 * 
 * This is the example for custom define mode of bme:fire.
 * 1. Create a new jBME.Fire.Mode object, and give it a new mode name.
 * 2. Overwrite the method of jBME.Fire.Mode as you want.
 *
 * You can place your function define in anywhere(*.js or between <script></script>) as you want.
 */
jBME.Fire.Mode.register(function()
{
    var jMode = new jBME.Fire.Mode("sampleCustomMode");
    {
        // now you can overwrite functions in jMode to satisfy your logical service
        // for your new defined bme:fire mode. 
        // ...
    }
});

/**
 * Client Service Define - jQuery (plugin) methods.
 */
(function($)
{
    /**
     * Client Service Define Example
     * Directly define a method.
     * @return
     */
    jQuery.fn.bmeClientServiceSample = function()
    {
        alert('This is the sample of BME Client Service Definition.\n'
        + '1) "jQuery.fn" means jQuery.prototype\n'
        + '2) "bmeClientServiceSample" is Client Service name\n'
        + '3) "this" object point to jQuery("...") object.\n');
        return this;
    };
    
    /**
     * BME Client Service - close conversation.
     */
    jQuery.fn.closeConversation = function()  
    {
        if(jBME.context.cid && !jBME.context.isclosed)
        {
             var jFireTag = new jBME.FireTag();
             jFireTag.setMode("webutil");
             jFireTag.setService("closeConversation()");
             jFireTag.setValidation(false);
             jFireTag.setMonitor(false);
             jFireTag.setRegionid("");
             jFireTag.setTargetid("");
             jFireTag.options.url += "&BMECIDClosing=1";
             jBME.context.isclosed = true;
             jBME.Fire.execute(jFireTag);
        }
        return this;
    };
})(jQuery);


/**
 * Define Class
 * jBME.ComboBox 
 */
(function()
{
    /**
     * Define Class and its constructor
     * @param id
     */
    jBME.ComboBox = function(id)
    {   
        this.attach(id);
        return this;
    };

    /**
     * static constants.
     */
    jBME.ComboBox.DROPDOWN_MIN_HEIGHT = 50;
    jBME.ComboBox.DROPDOWN_MAX_HEIGHT = undefined;
    
    /**
     * Define Class members
     */
    jBME.ComboBox.prototype = 
    {
        /**
         * DOM element for combobox.
         */
        jqCombo : undefined,
        jqSelect : undefined,
        jqDrop : undefined,
        dropPoper : undefined,
        jqFirstCheck: undefined,
        /**
         * @param id
         */
        attach : function(id)
        {
            this.jqCombo = jQuery(jBME.util.getIdSelector(id)).filter(".bc_combobox");
            this.jqSelect = jQuery(".bc_combobox_select", this.jqCombo);
            this.jqDrop = jQuery(".bc_combobox_dropdown", this.jqCombo);
            this.dropPoper = new jBME.Poper(this.jqDrop);
            this.jqFirstCheck = this.jqDrop.find(":checkbox:first");
            
            this.updateSelectText(true);
            this.updatePosition();
            this.activate();
        },
        
        /**
         * Activate the combobox. 
         */
        activate : function()
        {
            var This = this;
            
            //Event: Click over the "select input", will trigger the drop-down list.
            this.jqSelect.bind("click", function(event)
            {
                if(jBME.util.isDisabled(This.jqCombo))
                {
                    return;
                }
                if (This.jqDrop.is(":hidden"))
                {
                    This.updatePosition();
                } 
                This.dropPoper.toggle(This.jqSelect);
            });
            
            //Event: Click over element outside will close the drop-down list. 
            jQuery(document).bind("click", function(event)
            {
                if (This.jqDrop.is(":visible") && $(event.target).closest(".bc_combobox_select")[0] != This.jqSelect[0])
                {
                    // Click inside the drop-down list, do nothing.
                    if(event.pageX >= This.jqDrop.offset().left
                        && event.pageX <= (This.jqDrop.offset().left + This.jqDrop.outerWidth())
                        && event.pageY >= This.jqDrop.offset().top 
                        && event.pageY <= (This.jqDrop.offset().top + This.jqDrop.outerHeight()))
                    {
                        return;
                    }
                    
                    // "close" the drop-down list
                    This.dropPoper.hide();
                }
            });
            
            //Event: Click over item-checkbox will update the select-text.
            jQuery(":checkbox", this.jqDrop).bind("click", function(event)
            {
            	$(this).valid();
                This.updateSelectText();
                event.stopPropagation();
            });
            
            //Event: Click over item-text will trigger the checkbox value. 
            jQuery(".bc_comboboxitem", this.jqDrop).bind("click", function(event)
            {
            	var jqCheckbox = jQuery(this).find(":checkbox");
                jqCheckbox.attr("checked", !jqCheckbox.attr("checked"));
                jqCheckbox.valid();
                This.updateSelectText();
            });
        },
        
        /**
         * Update drop-down list Position.
         */
        updatePosition : function()
        {
            this.jqDrop.width(this.jqSelect.outerWidth() - 3);  //FireFox V3.6.8 may add more (-1) while 3.6.5 is OK.
        	
            //Minimum height.
            if(jQuery(":checkbox", this.jqDrop).length == 0)
            {
                this.jqDrop.height(jBME.ComboBox.DROPDOWN_MIN_HEIGHT);
            }
        },
        
        /**
         * Update select text.
         */
        updateSelectText : function(updateDefaultValue)
        {
            var This = this;
            var text = "";
            jQuery(":checked", This.jqDrop).each(function(i)
            {
                var jqItemText = jQuery("#"+this.id+"_text", This.jqDrop);
                if(text != "")
                {
                    text += ",";
                }
                text += jqItemText.text();
            });
            
            this.jqSelect.find(":input").val(text);
            if(updateDefaultValue)
            {
                this.jqSelect.find(":input").attr("defaultValue", text);
            }
        }
    };//end prototype.
})();


/**
 * Define Class
 * Pagenation
 */
(function() 
{
    /**
     * Define Class and its constructor
     * @param loopSupportID
     */
    jBME.Pagenation = function(loopSupportID, service)
    {   
        this.loopSupportID      = loopSupportID;
        this.pageID             = this.getPageID();

        this.pagenationObj      = $("#" + this.pageID);
        this.curPageObj         = $("#" + this.pageID + "_curPage");
        this.totalPageObj       = $("#" + this.pageID + "_totalPage");
        this.gotoObj            = $("#" + this.pageID + "_goto");
        //this.recordPerPage        = $("#" + pageID + "_recordPerPage");

        this.service = service;
        this.init();
        return this;
    };
   
    jBME.Pagenation.prototype = 
    {
        pageID : "",
        loopSupportID : "",

        pagenationObj : "",
        curPageObj :"",
        totalPageObj : "",
        gotoObj : "",
        service : "",
        //recordPerPage : "10",
        
        getPageID : function()
        {
    		return $("#" + this.loopSupportID).find(".bc_pagenation").attr("id");
        },
        // Bind event.
        bindPageEvent : function(bindID, event, prepareFunc)
        {
            var jFireTag = new jBME.FireTag("", bindID, event, "loadPageData");
            jFireTag.setRegionid(this.loopSupportID);
            jFireTag.setTargetid(this.loopSupportID);
            jFireTag.setService(this.service);
            
            jFireTag.pageID = this.pageID;
            jFireTag.pagenation = this;
            jFireTag.bindObj = $("#" + bindID);
            jFireTag.prepareFunc = prepareFunc;
            
            jBME.Fire.bind(jFireTag);
        },
        
        setFirstPage : function(pagenation)
        {
            pagenation.curPageObj.val("1");
            return true;
        },

        setPreviousPage : function(pagenation)
        {
            pagenation.curPageObj.val(Number(pagenation.curPageObj.val()) - 1);
            return true;
        },

        setNextPage : function(pagenation)
        {
            pagenation.curPageObj.val(Number(pagenation.curPageObj.val()) + 1);
            return true;
        },

        setLastPage : function(pagenation)
        {
            pagenation.curPageObj.val(pagenation.totalPageObj.val());
            return true;
        },

        setGotoPage : function(pagenation)
        {            
            
            if (0 == Number(pagenation.totalPageObj.val()))
            {
            	return false;
            }
            
            var curPage = Number(pagenation.gotoObj.val());
            
            if (curPage < 1) 
            {
            	pagenation.curPageObj.val("1");
                return true;
            }
            
            if (curPage > Number(pagenation.totalPageObj.val()))
            {
            	pagenation.curPageObj.val(pagenation.totalPageObj.val());
                return true;
            }
            
            pagenation.curPageObj.val(curPage);
            return true;
        },
        
        
        // Bind event to all the buttons within pagenation.
        init : function()
        {
            this.bindPageEvent(this.pageID + "_first", "click", this.setFirstPage);
            this.bindPageEvent(this.pageID + "_previous", "click", this.setPreviousPage);
            this.bindPageEvent(this.pageID + "_next", "click", this.setNextPage);
            this.bindPageEvent(this.pageID + "_last", "click", this.setLastPage);
            this.bindPageEvent(this.pageID + "_goto", "change", this.setGotoPage);
            this.bindPageEvent(this.pageID + "_recordPerPage", "change");
            
            $("#"+this.pageID+"_goto").bind("keypress", function(jEvent)
            {
            	var keyCode = jEvent.which;
            	if (!((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || keyCode == 13))
            	{
            		jEvent.preventDefault();
                	return false;
            	}            	
            });
            
        }
    };
})();

/**
 * Fire Extension Mode.
 * For pagenation only.
 */
jBME.Fire.Mode.register(function()
{
    var jMode = new jBME.Fire.Mode("loadPageData");
    jMode.canExecute = function(jFireTag)
    {
        if (jFireTag.bindObj.hasClass("paginate_disabled_first")
                || jFireTag.bindObj.hasClass("paginate_disabled_previous")
                || jFireTag.bindObj.hasClass("paginate_disabled_next")
                || jFireTag.bindObj.hasClass("paginate_disabled_last"))
        {
            return false;
        }

        if (jFireTag.prepareFunc)
        {
        	return jFireTag.prepareFunc(jFireTag.pagenation);
        }
    };
    jMode.onResponseSuccess = function(jFireTag, response)
    {
        var ret = jBME.Fire.Mode["render"].onResponseSuccess.apply(this, arguments);
        
        // 触发一下事件，告知用户刷新已经完成，用户可能需要调整页面大小
		var jqAfterEvent = new jQuery.Event("after");
		jqAfterEvent.stopPropagation();
		// 已经经过局部刷新，必须获取新的dom节点，才能触发
		$("#" + jFireTag.pagenation.getPageID()).trigger(jqAfterEvent);
        
        return ret;
    };
});

/**
 * Fire Extension Mode.
 * For bigTable only.
 */
jBME.Fire.Mode.register(function()
{
    var jMode = new jBME.Fire.Mode("updateBigTable");
    jMode.onResponseSuccess = function(jFireTag, response)
    {
        // get new tbody save to cache
        var newBigTableObj = $(response.body);
        var newBodyObj = $(jFireTag.bigTable.tableBodySelector, newBigTableObj);
        var totalNoObj = $("#" + jFireTag.bigTable.tableID + "_totalNo", newBigTableObj);
        var totalNo = Number(totalNoObj.val());
        jFireTag.bigTable.saveToCache(totalNo, newBodyObj);
        
        jFireTag.bigTable.from = jFireTag.from;
        jFireTag.bigTable.to = jFireTag.to;
        
        // show cache
        jFireTag.bigTable.showCache();
        jFireTag.bigTable.loading = false;
        
        // 确认是否需要再次获取数据，因为用户可能已经拉动滚动条到其他地方去了
        jFireTag.bigTable.judgeAndGetNewData();

        jBME.ready();
    };
});

/**
 * Define Class
 * bigTable
 * @author someone.
 */
(function()
{
    jBME.BigTable = function( tableID, service, curIDArr )
    {
        this.service    = service;
        this.tableID    = tableID;
        this.tableBodySelector = "#" + tableID + "_tbody";

        this.container  = $("#" + tableID);
        this.tableObj   = $("#" + tableID + "_table");
        this.tableOuter = this.tableObj.parent();
        // scroll bar Div
        this.scrollObj = this.container.find('div.bigtable_scrollbar:eq(0)');
        // height of scroll bar div
        this.scrollInnerObj = this.scrollObj.find('div:eq(0)');
        this.tableHeadObj   = $("#" + tableID + "_head");
        this.tableBodyObj= $(this.tableBodySelector, this.container);
        
        this.cols       = this.tableHeadObj.find("th").length;
        // row height
        this.rowH       = this.tableBodyObj.find('tr:eq(0)').outerHeight();

        // if mouse is over the table
        this.scrollFocus = false;
        
        this.perNo  = 10;

        // 除了中间显示的一屏，两边各留三屏的缓存
        // 要修改缓存量时，直接改cacheScreenNo即可
        this.cacheScreenNo = 3;
        // 缓存数据条数，此数据是根据perNo换算出来的，不允许修改
        this.cacheNo = this.perNo * (this.cacheScreenNo * 2 + 1);
        // 获取记录的范围，保持与java中的逻辑一致：[from, to)
        this.from   = 0;
        this.to     = this.from + this.cacheNo + 1;
        
        // 最上面一条数据
        this.topNo  = 0;
        // totalNo表示的是有多少条数据，而不是有多少个tr
        // 因为没有数据时，也有一个tr用于显示无数据
        this.totalNo    = Number($("#" + tableID + "_totalNo").val());
        
        // 是否正在向服务端请求数据
        this.loading    = false;
        
        // 当前所有记录的id数组
        this.curIDArr = curIDArr;
        // 所有的td内容（不包括td本身）
        this.tdValueArr = [];
        // 所有显示当前数据的td，是td本身
        this.curTdArr = [];
       
        this.init();
        
        return  this;
    };
    jBME.BigTable.prototype = 
    {
        // 将所有显示当前数据的td保存起来
        saveCurTd : function()
        {
            var rowArr = this.tableBodyObj.children("tr");
            for(var i=0; i<this.perNo; i++)
            {
                var tempArr = $(rowArr[i]).children("td");
                this.curTdArr.push(tempArr);
            }
            
            // todo:td不足一屏，则需要补足一屏
        },
        
        // 将数据只在到缓存中去
        saveToCache : function(totalNo, bodyObj)
        {
            this.totalNo = totalNo;
            this.tdValueArr.length = 0;
            if (0 != totalNo)
            {
                var rowArr = bodyObj.children("tr");
                // 实际数据数也许根本不足默认的to，需要根据实际情况获取
                this.to = this.from + rowArr.length;
                for(var i=0; i<rowArr.length; i++)
                {
                    var tempArr = $(rowArr[i]).children("td");
                    var htmlArr = new Array();
                    tempArr.each(
                            function() {
                                htmlArr.push($(this).html());
                            }
                    );
                    this.tdValueArr.push(htmlArr);
                }
                // 也许初始数据不足一屏，但是后台数据是可变的，需要保证前台有空间显示perNo的数据
                for (var idx = 0; idx < (this.perNo - rowArr.length); idx++)
                {
                	var row = $(rowArr[0]).clone();
                	var tdIdx = 0;
                	row.children("td").each(function()
                		{
                			var txt = "&nbsp;";
                			if (0 == tdIdx)
                			{
                				txt = "<label class='bc bc_label'>&nbsp;</label>";
                			}
                			tdIdx++;
                			$(this).html(txt);
                		});
                	row.appendTo(bodyObj);
                }
            }
            //alert("copy result:" + this.tdValueArr.length);
        },
        
        // 将缓存中的数据显示到界面上
        showCache : function()
        {
            // 1.缓存显示到界面
            for(var i=0; i<this.tableBodyRowArr.length; i++)
            {
                if(this.topNo+i>=this.to)
                {
                 // 没有数据了
                    break;
                }
                var rowData = this.tdValueArr[this.topNo + i - this.from];
                if(!rowData) 
                {
                    //alert(this.topNo + i - this.from);
                    // 数据未获取成功，显示正在加载
                    this.curTdArr[i].each(
                            function() {
                                this.innerHTML = "&nbsp;";
                            });
                    this.curTdArr[i][0].innerHTML = "<label class='bc bc_label'>loading</label>";
                    return;
                }
                
                // 显示数据
                for(var j=0; j<this.cols; j++)
                {
                    this.curTdArr[i][j].innerHTML =rowData[j];
                }
            }
        },

        init : function()
        {
            this.saveToCache(this.totalNo, this.tableBodyObj);
            this.saveCurTd();
            // 界面上只保留指定行数的数据
            if (this.totalNo > this.perNo)
            {
                $("tr", this.tableBodyObj).slice(this.perNo).remove();
            }
            // all tr in table body
            this.tableBodyRowArr = this.tableBodyObj.children("tr");

            this.scrollBar();
            this.scrollInnerObj.height(this.totalNo * this.rowH);
        },

        // create scrollbar
        scrollBar: function()
        {
            var bigTable = this;
            this.scrollObj.bind('scroll', function(){bigTable.scrollHandler(bigTable);});
            var headHeight = this.tableHeadObj.height();
            var tableHeight = this.tableObj.outerHeight();
            this.scrollObj.css({'height': (tableHeight - headHeight)+'px', 'margin-top': (headHeight + 2)+'px'});
            this.tableOuter.css({'margin-top': -tableHeight+'px'});
            this.tableOuter.bind('mouseover', function() {
                bigTable.scrollFocus = true;
            });
            this.tableOuter.bind('mouseout', function() {
                bigTable.scrollFocus = false;
            });
            
            if(document.addEventListener){
                this.tableObj.context.addEventListener('DOMMouseScroll', function(e){
                    return bigTable.scrollFunc(e, bigTable);
                }, false); 
            }
            else
            {
                window.onmousewheel=document.onmousewheel=function(e) {
                    bigTable.scrollFunc(e, bigTable);
                }; //IE/Opera/Chrome/Safari
            }
            
            // fix bug in ie: can not select content in table
            if($.browser.msie)
            {
                this.tableOuter.css({'width': (this.scrollObj.width()-16)+'px'});
                this.scrollObj.bind('resize', function() {
                    this.tableOuter.width(this.scrollObj.width()-16);
                });
            }
        },
        
        scrollHandler : function(bigTable)
        {
            // this is scrollObj
            // get the first data index in current page
            var scrollToNo = parseInt(bigTable.scrollObj.scrollTop()/bigTable.scrollInnerObj.height()*(bigTable.totalNo));
            bigTable.topNo = scrollToNo;
            
            //alert(scrollToNo +":" + bigTable.to);
            if (bigTable.judgeAndGetNewData())
            {
            	return;
            }
            bigTable.showCache();
        },
        
        // 判断并加载新数据，如果需要加载则返回true
        judgeAndGetNewData : function()
        {
            if (!this.needNewData())
            {
            	return false;
            }
            
            var newFrom = this.topNo - this.perNo * 3 - 1;
            if (newFrom < 0)
            {
                newFrom = 0;
            }
            var newTo = newFrom + this.cacheNo;
            if (newTo > this.totalNo)
            {
            	newTo = this.totalNo;
            }
            this.getNewData(newFrom, newTo);
            return true;
        },
        
        scrollFunc : function(e, bigTable)
        {
            //alert(bigTable);
            if(!bigTable.scrollFocus)
            {
                return ;
            }
            
            e = e || window.event;
            
            // forbid org mouse scroll event in Firefox
            if($.browser.mozilla && e.stopPropagation)
            {   //(e.preventDefault(), e.stopPropagation()) || (e.cancelBubble=true, e.returnValue=false);
                e.preventDefault();
                if(!e.stopPropagation())
                {
                    e.cancelBubble = true;
                    e.returnValue = false;
                }
            }
            
            var deta = 0;
            //IE/Opera/Chrome
            if(e.wheelDelta)
            {
                deta = e.wheelDelta*-0.25;
            }
            //Firefox 
            else if(e.detail)
            {
                deta = e.detail*10;
            }
            
            var scrollT = bigTable.scrollObj.scrollTop() + deta;
            bigTable.scrollObj.scrollTop(scrollT);
            return false;
        },

        setLockScreen : function()
        {
        },
        
        /**
         * TODO:如果有数据刷新
         *  未锁屏，则取第一屏数据
         *  已锁屏，则取当前from到to的数据
         *  如果没有数据刷新 则取当前from到to的数据  刷新当前from到to的数据
         */
        refreshData : function()
        {
            this.getNewData(this.from, this.to);
        },

        /**
         * 1.当前缓存无法满足界面显示时需要获取新数据
         * 2.当前已经在向服务端获取数据，则禁止发起新请求
         */
        needNewData : function()
        {
            if (this.loading)
            {
                return false;
            }
            if (this.topNo < this.from)
            {
                return true;
            }
            if ((this.topNo + this.perNo) > this.to)
            {
                return true;
            }
            
            return false;
        },
        
        // get [from, to] record from server
        getNewData : function(from, to)
        {
            var jFireTag = new jBME.FireTag("", this.tableID, "click", "updateBigTable");
            jFireTag.setRegionid(this.tableID + "_foot");
            jFireTag.setTargetid(this.tableID);
            jFireTag.setService(this.service + "('" + from + "', '" + to + "')");
            
            jFireTag.bigTable = this;
            jFireTag.from = from;
            jFireTag.to = to;
            
            jBME.Fire.execute(jFireTag);
            this.loading = true;
        },
        
        /**
         *  get all id
         *  todo:save all id 
         */
        getCurIDArr : function()
        {
            return this.curIDArr;
        }
    };
})();

/**
 * Define Class
 * jBME.DblSelectbox
 */
(function()
{
    /**
     * Define Class and its constructor
     * @param id
     */
    jBME.DblSelectbox = function(id)
    {   
        this.attach(id);
        return this;
    };

    /**
     * Define Class members
     */
    jBME.DblSelectbox.prototype = 
    {
        /**
         * DOM element for DblSelectbox.
         */
		jqSubmit : undefined,
		jqLeft : undefined,
		jqRight : undefined,
        
        /**
         * @param id
         */
        attach : function(id)
        {
            this.jqSubmit = jQuery(jBME.util.getIdSelector(id + "_submit"));
            this.jqLeft   = jQuery(jBME.util.getIdSelector(id + "_left"));
            this.jqRight  = jQuery(jBME.util.getIdSelector(id + "_right"));
            
            var dbl = this;
            this.jqLeft.bind("dblclick", function(){dbl.moveToRight();});
            this.jqRight.bind("dblclick", function(){dbl.moveToLeft();});
            
            this.copyToSubmit();
        },
        copyToSubmit : function()
        {
            // copy option in jqRight to jqSubmit
            var dbl = this;
            dbl.jqSubmit.empty();
            jQuery("option", this.jqRight).each(function()
            {
            	$(this).clone().appendTo(dbl.jqSubmit).attr("selected", "selected");
            });
          
            this.adjustSize();
        },
        adjustSize : function()
        {
            // in ie, after move, width of select will dec, so fix it 
            this.jqLeft.css("width", "auto");
            this.jqRight.css("width", "auto");
            this.jqLeft.css("width", "100%");
            this.jqRight.css("width", "100%");
        },
        moveUp : function()
        {
        	var jqSelects = jQuery("option:selected", this.jqRight);
            if (0 == jqSelects.length)
            {
            	return;
            }
            if (0 == jqSelects.first().prev().length)
            {
            	return;
            }
			jqSelects.each(function()
				{
					var jqSelect = $(this);
					jqSelect.insertBefore(jqSelect.prev());
				});
            this.adjustSize();
        },
        moveDown : function()
        {
        	var jqSelects = jQuery("option:selected", this.jqRight);
            if (0 == jqSelects.length)
            {
            	return;
            }
            if (0 == jqSelects.last().next().length)
            {
            	return;
            }
			for (var i=jqSelects.length - 1; i>=0; i--) 
			{
				var jqSelect = $(jqSelects.get(i));
				jqSelect.insertAfter(jqSelect.next());
			};
            this.adjustSize();
        },
        moveToRight : function()
        {
        	this.moveSelected(this.jqLeft, this.jqRight);
        },
        moveToLeft : function()
        {
        	this.moveSelected(this.jqRight, this.jqLeft);
        },
        moveAllToRight : function()
        {
        	this.moveAll(this.jqLeft, this.jqRight);
        },
        moveAllToLeft : function()
        {
        	this.moveAll(this.jqRight, this.jqLeft);
        },
        moveSelected: function (jqFrom, jqTo)
    	{
            // move selected option in jqFrom to jqTo, and copy them to jqSubmit
            var dbl = this;
            var selected = jQuery("option:selected", jqFrom);
            if (0 == selected.length)
            {
            	return;
            }
            selected.each(function()
            {
            	$(this).appendTo(jqTo);
            });
        	
        	this.copyToSubmit();
    	},
        moveAll: function (jqFrom, jqTo)
    	{
            // move all option in jqFrom to jqTo, and copy them to jqSubmit
            var dbl = this;
            var all = jQuery("option", jqFrom);
            if (0 == all.length)
            {
            	return;
            }
            all.each(function()
            {
            	$(this).appendTo(jqTo);
            });
        	
        	this.copyToSubmit();
    	}
    };//end prototype.
})();

/**
 * Define Class
 * jBME.Poper
 */
(function()
{
	// 私有函数
     // 返回jqMask
	 function setMaskPos(jqMask)
	 {
    	 jqMask.css("left", document.documentElement.scrollLeft);
    	 jqMask.css("top", document.documentElement.scrollTop);
    	 jqMask.width(document.documentElement.offsetWidth);
    	 jqMask.height(document.documentElement.offsetHeight);	 	
	 }
     function getMask()
     {
    	 var jqBody = $("body");
		 jqMask = $("<div class='bf_mask'></div>");
		 jqBody.append(jqMask);
		 $(window).bind('resize.mask', function()
		 	{
			 setMaskPos(jqMask);
		 	});
		 jqMask.css("z-index", 999);
    	 return jqMask;
     }	
	function showMask(poper)
	{
		if (!poper.mask)
		{
			return;
		}
		if (undefined == poper.jqMask)
		{
			// 获取一个新的mask对象
			poper.jqMask = getMask();
		}
		// 显示出来
		setMaskPos(poper.jqMask);
		poper.jqMask.show();
	}
	function hideMask(poper)
	{
		// 每次都删除mask
		if (undefined == poper.jqMask)
		{
			return;
		}
		poper.jqMask.remove();
		poper.jqMask = undefined;
	}
	function prepareWrapper(poper)
	{
		if (undefined != poper.jqAfterWrap)
		{
			// 当前已经show出来了，不必重新处理
			// 如果需要重新处理，由调用者先hide再show
			return;
		}
		poper.jqAfterWrap = poper.jqPop;
		if (undefined == poper.wrapperFunc)
		{
			poper.jqAfterWrap.appendTo("body");
			poper.jqAfterWrap.css("z-index", 999);
			return;
		}
		
		if (undefined == poper.jqWrapper)
		{
			poper.jqWrapper = poper.wrapperFunc.apply();
		}
		// 将pop移到wrapper中去
		poper.jqPop.css("position", "static");
		poper.jqPop.show();
		jBME.lockReady = true;
		poper.jqPop.appendTo(poper.jqWrapper.jqContent);
		jBME.lockReady = false;

		poper.jqAfterWrap = poper.jqWrapper;
	}
	function calcFromInfo(poper)
	{
		if (undefined == poper.jqPopFrom)
		{
			// 说明用户手动注入from信息，不必计算
			return;
		}
		poper.popFromInfo = getDomInfo(poper.jqPopFrom);
	}
	function showWrapper(poper)
	{
		// 1.准备弹出对象，可能直接使用jqPop,也可能将jqPopWrapper一下，最终的弹出对象为jqAfterWrap
		prepareWrapper(poper);
   
		// 2.计算from信息
		calcFromInfo(poper);

		// 3.取得弹出窗口的大小
		getPopInfo(poper);
		
		var winInfo = getWinInfo();
		var popInfo = $.extend(true, {}, poper.popFromInfo);
		// 4.确定垂直方向的弹出
		setVerticalPopPos(poper, popInfo, winInfo);
		// 5.确定水平方向的弹出
		setHorizontalPopPos(poper, popInfo, winInfo);
		// 6. 修改位置并显示
		poper.jqAfterWrap.css({"left": popInfo.left + "px", 
	        "top": popInfo.top + "px"});
		poper.jqAfterWrap.show();
		poper.jqAfterWrap.fixSelectorBug();
	}
	function hideWrapper(poper)
	{
		if (undefined == poper.jqAfterWrap)
		{
			return;
		}
		poper.jqAfterWrap.hide();
		poper.jqPop.hide();
	        poper.jqPop.css("position", "absolute");
		if (undefined != poper.jqPopParent)
		{
			jBME.lockReady = true;
			poper.jqPop.appendTo(poper.jqPopParent);
			jBME.lockReady = false;
		}
		if (undefined != poper.jqWrapper)
		{
			poper.jqWrapper.jqContent.empty();
			poper.jqWrapper.remove();
			poper.jqWrapper = undefined;
		}
		poper.jqAfterWrap = undefined;
	}
	function getEmptyOffset()
	{
		return {"left":0, "top":0, "right":0, "bottom":0};
	}
	// 获取弹出窗口的坐标计算信息
	function getEmptyPopInfo()
	{
		return {"left":0, "top":0, "width":0, "height":0};
	}
	function getWinInfo()
	{
		var ele = document.documentElement;
		var body = document.body;
		// chrome识别documentElement的scrollTop/scrollLeft有问题，使用body则没有问题
		// 这两个值浏览器总是只返回一个有效的，所以直接相加，避免判断浏览器
		return {"scrollTop":ele.scrollTop + body.scrollTop, "scrollHeight":ele.scrollHeight,
			"scrollLeft":ele.scrollLeft + body.scrollLeft, "scrollWidth":ele.scrollWidth,
			"offsetHeight":ele.offsetHeight, "offsetWidth":ele.offsetWidth};
	}
	// 返回dom的left/right/width/height信息,dom可以是dom，也可以是jq对象
	function getDomInfo(dom)
	{
		jqObj = $(dom);
		var info = getEmptyPopInfo();
		info.left = jqObj.offset().left;
		info.top = jqObj.offset().top;
		info.width = jqObj.outerWidth();
		info.height = jqObj.outerHeight();
		return info;
	}
	function getPopInfo(poper)
	{
		// ie7下，取一下宽度、高度会导致出现滚动条，必须show再hide，无语...
		var orgHidden = poper.jqAfterWrap.is(":hidden");
		poper.jqAfterWrap.css("left", "-1000px");
		poper.jqAfterWrap.show();
		// 不手动设置width，则ie7下无法正确显示
		// 这个设置一定要在show之后进行
		if (undefined != poper.jqWrapper)
		{
			var jqContent = poper.jqWrapper.jqContent;
			var fix = jqContent.outerWidth(true) - jqContent.width();
		
			poper.jqAfterWrap.width(jqContent.width() + fix);			
		}
		poper.popWidth = poper.jqAfterWrap.outerWidth();
		poper.popHeight = poper.jqAfterWrap.outerHeight();
		if (orgHidden)
		{
			poper.jqAfterWrap.hide();
		}
	}
	// 垂直
	function setVerticalPopPos(poper, popInfo, winInfo)
	{
		if (poper.bottomFirst)
		{
			// 向下弹出
			popInfo.top = poper.popFromInfo.top + poper.popFromInfo.height + poper.offsetInfo.top;
			poper.resultBottom = true;
			if (popInfo.top - winInfo.scrollTop + poper.popHeight - poper.offsetInfo.bottom > winInfo.offsetHeight)
			{
				// 下方空间不足，向上弹出
				poper.resultBottom = false;
				popInfo.top = poper.popFromInfo.top - poper.popHeight + poper.offsetInfo.bottom;
			}
		}
		else
		{
			// 先向上弹出
			popInfo.top = poper.popFromInfo.top - poper.popHeight + poper.offsetInfo.bottom;
			poper.resultBottom = false;
			if (popInfo.top  - winInfo.scrollTop - poper.offsetInfo.top < 0)
			{
				// 上方空间不足，向下弹出
				poper.resultBottom = true;
				popInfo.top = poper.popFromInfo.top + poper.popFromInfo.height + poper.offsetInfo.top;
			}
		}
		// 垂直方向仍然无法显示，则将top置为当前窗口滚动top位置
		if ((popInfo.top  - winInfo.scrollTop - poper.offsetInfo.top < 0) || 
			(popInfo.top - winInfo.scrollTop + poper.popHeight - poper.offsetInfo.bottom > winInfo.offsetHeight))
			{
				popInfo.top = winInfo.scrollTop;
			}
	}
	// 水平
	function setHorizontalPopPos(poper, popInfo, winInfo)
	{
		// 向右弹出
		poper.resultRight = true;
		popInfo.left = poper.popFromInfo.left + poper.offsetInfo.left;
		if (popInfo.left  - winInfo.scrollLeft + poper.popWidth - poper.offsetInfo.right > winInfo.offsetWidth)
		{
			// 空间不足，则向左弹出
			poper.resultRight = false;
			popInfo.left = poper.popFromInfo.left + poper.popFromInfo.width - poper.popWidth + poper.offsetInfo.right;
		}
		
		// 水平方向仍然无法显示，则将left置为当前窗口滚动left位置
		if (popInfo.left < winInfo.scrollLeft + poper.offsetInfo.left)
		{
			popInfo.left = winInfo.scrollLeft;
		}
	}
    /**
     * Define Class and its constructor
     * 1.默认优先向下弹出
     * -----
     * |from|
     * -----
     * -----------
     * |   pop   |
     * -----------
     * 2.下方空间不足，则向上弹出
     * -----------
     * |   pop   |
     * -----------
     * -----
     * |from|
     * -----
     * 3.右方空间不足，则向左弹出
     * -----------
     * |   pop   |
     * -----------
     *       -----
     *       |from|
     *       -----
     * 4.向上或向下校正后，空间仍然不足，将top置为0，向左或向右校正后，空间仍然不足，将left置为0
     * 5.from缩小为一个点时，可以满足环绕鼠标弹出的效果
     * 6.可以设置是否需要wrap，内置阴影wrap，工具栏场景则是使用自定义wrap
     * 7.可以设置是否需要mask(默认mask整个body，将来有需求再改)
     */
    jBME.Poper = function(jqPop)
    {   
        this.setPop(jqPop);
		this.offsetInfo = getEmptyOffset();
        return this;
    };

    /**
     * Define Class members
     */
    jBME.Poper.prototype = 
    {
        $view : $(".bc_view"),
        jqPopFrom: undefined,
        jqPop: undefined,
		// 根据jqPop计算出来的父对象
		jqPopParent: undefined,

		jqMask: undefined,
		mask : false,
		
		wrapperFunc : undefined, // 用于获取wapper的函数，由调用者设置，本类内置createShadowWrapper
		jqWrapper : undefined,
		
		bottomFirst : true, // 默认优先向下弹出
		
		// 这里最终体现的是实际弹出方向
		resultBottom : true, 
		resultRight : true,

		// 根据jqPopFrom计算出来的信息，也可以是用户直接注入的信息
		popFromInfo : undefined,
		// jqPop的弹出偏移信息
		offsetInfo : undefined,

		// 下面是内部变量
		// 最终使用这个对象进行弹出显示
		jqAfterWrap : undefined,
		popWidth : undefined,
		popHeight : undefined,
		eventName : undefined,
		
		// 内置shadow wrapper
		createShadowWrapper : function()
		{
			// 创建shadow对象
			var html = '<div class="bf_shadow">';
			html += '<div head="true">';
			html += '<div l="true"></div>';
			html += '<div r="true"></div>';
			html += '<div c="true"></div>';
			html += '</div>';
			html += '<div ml="true"><div mr="true" content="true"></div></div>';
			//modify by hujia start
			html += '<div foot="true" id="foot">';
		  //modify by hujia end
			html += '<div l="true"></div>';
			html += '<div r="true"></div>';
			html += '<div c="true"></div>';
			html += '</div>';
			html += '</div>';
			
			var jqShadow = $(html);
			jqShadow.jqContent = jqShadow.find("[content]");
			jqShadow.appendTo("body");
			jqShadow.css("z-index", 999);
			return jqShadow;
		},
		setPop : function(jqPop)
		{
			if (undefined == jqPop)
			{
				return;
			}
			this.jqPop = jqPop;
			this.jqPopParent = jqPop.parent();
		},
		defaultWrap : function()
		{
			this.wrapperFunc = this.createShadowWrapper;
		},
		setPopFromInfoByDom : function(dom)
		{
			this.popFromInfo = getDomInfo(dom);
		},
		setPopFromInfoByEvent : function(event)
		{
			this.popFromInfo = getEmptyPopInfo();
			this.popFromInfo.left = mouseX(event);
			this.popFromInfo.top = mouseY(event);
	
		},
		toggle : function(jqPopFrom)
		{
			if (this.isShow())
			{
				this.hide();
				return;
			}
			this.show(jqPopFrom);
		},
		isShow : function()
		{
			if (undefined == this.jqAfterWrap)
			{
				return false;
			}
			return this.jqAfterWrap.is(":visible");
		},
		// 指定坐标显示，不服从修正原则，指哪打哪
		showIn : function(x, y)
		{
			this.popFromInfo = getEmptyPopInfo();
			this.show();
			// 修改位置			
			this.jqAfterWrap.css({"left": x + "px", 
		        "top": y + "px"});
		},
		// jqPopFrom可选，如果不指定，则使用调用show之前的jqPopFrom值
		show : function(jqPopFrom)
		{
			if (undefined != jqPopFrom)
			{
				this.jqPopFrom = jqPopFrom;
			}
			// 1.显示mask
			showMask(this);
			// 2.更新wrapper位置并显示
			showWrapper(this);
			// 3.监听局部刷新事件，在自己被干掉之前先hide掉，但是如果没有wrapper，则不需要这个处理
			if (undefined == this.jqWrapper)
			{
				return;
			}
			this.eventName = "beforerender." + this.jqPop.attr("id");
			var This = this;
			$(".bc_view").unbind(this.eventName).bind(this.eventName, function(event, id)
				{
					if (This.jqPop.closest("[id=" + id + "]"))
					{
						This.hide();
					}
				});
		},
		hide : function()
		{
			// 1.隐藏wrapper
			hideWrapper(this);
			// 2.隐藏mask
			hideMask(this);
			// 3.取消事件监听
			this.$view.unbind(this.eventName);
		}
    };
})();

/**
 * Define Class
 * jBME.menu
 */
(function()
{
    /**
     * Define Class and its constructor
     * @param cfg: 
     * 		cfg.id
     * 		cfg.bindid
     * 		cfg.event 当jqBind上触发此事件时，弹出菜单
     */
    jBME.menu = function(cfg)
    {
        this.attach(cfg);
        return this;
    };

    /**
     * Define Class members
     */
    jBME.menu.prototype = 
    {
        /**
         * DOM element for menu.
         */
    	// menu对象
		jqPopMenu: undefined,
		// menu监听哪个对象的事件
		jqBind: undefined,
		// menu在哪个对象旁边弹出，一般来说等于jqbind，在树上使用时，会是各个节点
		jqPopFrom: undefined,
        
		cfg: undefined,
		
		poper : undefined,
		
        /**
         * @param bindid/id
         */
        attach : function(cfg)
        {
    		this.cfg = cfg;
    		this.jqPopMenu = $("#" + cfg.id);
        	this.jqBind = $("#" + cfg.bindid);
        	this.jqPopFrom = this.jqBind;
			// toolbar需要根据dom找到menu对象
        	this.jqPopMenu.data("menu", this);
        	
			this.poper = new jBME.Poper(this.jqPopMenu);
			this.poper.defaultWrap();
			this.poper.mask = true;
			
        	this.initImg();

    		this.bindEvent();
        },
        initImg : function()
        {
        	// 没有menu的节点，创建出空的img来使得文字可以对齐
        	this.jqPopMenu.find("> .bc_link").each(function()
        		{
        			var jqA = $(this);
        			var jqChildren = jqA.children();
        			if ($.isEmptyObject(jqChildren) || !($(jqChildren[0]).is("img")))
        			{
        				jqA.prepend("<img align='absmiddle' src='" + jBME.theme.defaultPath + "/images/empty.gif'/>");
        			}
        		});
        },
        // 获取所有的tems，不包括menuItem、link以外的东东
        getItems : function()
        {
        	return this.jqPopMenu.find(">.bc_link");
        },
        bindEvent : function()
        {
        	var menuObj = this;
        	if ("" != this.cfg.event)
        	{
        		this.jqBind.bind(this.cfg.event, function()
        			{
        				menuObj.showMenu();
        			});
        	}
        	this.getItems().each(function()
        		{
        			var jqItem = $(this);
        			jqItem.unbind("click.menuitem").bind("click.menuitem", function(event)
        				{
	            			//event.preventDefault(); 
	            			event.stopPropagation();
	            			if (jBME.util.isDisabled(jqItem))
	            			{
	            				event.preventDefault();
	            				return;
	            			}
	            			menuObj.hideMenu();
	            			
	        				var jqEvent = new jQuery.Event("menuItemClick");
	        				jqEvent.stopPropagation();
	        				jqEvent.jqItem = jqItem;
	        				jqEvent.menuObj = menuObj;
	        				menuObj.jqPopMenu.trigger(jqEvent);
        				});
        		});
        },
	showMenuFrom : function(jqPopFrom)
	{
		this.jqPopFrom = jqPopFrom;
		this.showMenu();
	},
        showMenu : function()
        {
			// ie下该判定出错的bug又来了，先使用hide规避
//        	if (this.jqPopMenu.is(":visible"))
//			{
//				return;
//			}
			this.hideMenu();
			var menuObj = this;
			if (jBME.util.isDisabled(menuObj.jqBind))
			{
				return;
			}
        	function trigger(eventName)
        	{
    			var jqEvent = new jQuery.Event(eventName);
    			jqEvent.menuObj = menuObj;
    			jqEvent.stopPropagation();
    			menuObj.jqPopMenu.trigger(jqEvent);
        	}
        	// 触发一个beforeshow事件，让外界有机会对菜单进行调整
        	trigger("beforeShow");
			
			this.poper.show(this.jqPopFrom);

        	// 触发一个aftershow事件，让外界有机会做菜单外观外挂
        	trigger("afterShow");
			
        	$(document).bind("click." + this.cfg.id, 
        		function(event)
				{
        			var jqTarget = $(event.target);
					var test= menuObj;
        			if (0 != jqTarget.closest("[id=" + menuObj.cfg.id + "]").length)
        			{
        				return;
        			}
        			if (0 != jqTarget.closest("[id=" + menuObj.cfg.bindid + "]").length)
        			{
        				return;
        			}
        			if (event.target == menuObj.jqPopFrom.get(0))
        			{
        				return;
        			}
					if ($.contains(menuObj.jqPopFrom.get(0), event.target))
					{
						return;
					}
					
					menuObj.hideMenu();
				});
        },
        hideMenu : function()
        {
        	if (this.jqPopMenu.is(":hidden"))
        	{
        		return;
        	}
        	// 触发一个beforehide事件
			var jqEvent = new jQuery.Event("beforeHide");
			jqEvent.menuObj = this;
			jqEvent.stopPropagation();
			this.jqPopMenu.trigger(jqEvent);

        	this.poper.hide();

			$(document).unbind("click." + this.cfg.id);
        }
    };//end prototype.
})();

/**
 * Define Class
 * jBME.DataGrid
 */
(function()
{
    /**
     * Define Class and its constructor
     * @param id: 
     */
    jBME.DataGrid = function(id)
    {   
        this.attach(id);
        return this;
    };

    /**
     * Define Class members
     */
    jBME.DataGrid.prototype = 
    {
        /**
         * DOM element for DataGrid.
         */
		jqDataGrid: undefined,
		
		// 先只支持一列有check状态
		// 初始展示本页时，除去本页已经check处于状态的数据条数
		otherPageCheckCount: 0,
		// checkbox要提交到后台的对象名称
		checkName: undefined,

		/**
         * @param bindid/id
         */
        attach : function(id)
        {
    		this.jqDataGrid = $("#" + id);
    		
    		this.init();
        },
        init: function()
        {
    		var domCheckboxs = $(".cbme_col_check", this.jqDataGrid);
    		if (0 == domCheckboxs.length)
    		{
    			return 0;
    		}
    		
    		var jqFirst = $(domCheckboxs[0]);
			this.checkName = jqFirst.attr("name");
			// column的property对应数据的条数
    		var dg = this;
    		if(jqFirst.hasClass("cbme_crosspages"))
    		{
    			jqFirst.bind("beforesubmit", function()
    			{
    				dg.updateCheckName();
    			});
    		}

			var allCheckCount = jqFirst.attr("allCheckCount");
    		var checkCount = 0;
    		domCheckboxs.each(
    			function()
    			{
    				$(this).attr("orgName", dg.checkName);
    				if (this.checked)
    				{
    					checkCount++;
    				}
    			});
    		
    		this.otherPageCheckCount = allCheckCount - checkCount;
        },
        // 提交数据之前，修改各个checkbox的name，以避免不带下标提交数据，导致后台数据的全覆盖
        // 这样才能支持跨页check
        updateCheckName : function()
        {
    		var domCheckboxs = $(".cbme_col_check", this.jqDataGrid);
    		
    		var dg = this;
    		var count = 0;
    		domCheckboxs.each(
    			function()
    			{
    				var jqCheck = $(this);
    				if (this.checked)
    				{
    					jqCheck.attr("name", dg.checkName + "[" + (dg.otherPageCheckCount + count) + "]");
    					count++;
    				}
    				else
    				{
        				jqCheck.removeAttr("name");
    				}
    			});
        }
        
    };//end prototype.
})();

/**
 * Define Class
 * jBME.SearchBox
 */
(function()
{
    /**
     * Define Class and its constructor
     * @param id: 
     */
    jBME.SearchBox = function(id)
    {   
        this.attach(id);
        return this;
    };

    /**
     * Define Class members
     */
    jBME.SearchBox.prototype = 
    {
        /**
         * DOM element for SearchBox.
         */
    	jqSearchBox: undefined,
		jqInput: undefined,
		jqSearchBtn: undefined,
		
		// keypress/click事件触发时的事件数据
		event: undefined,
		
		// 搜索输入框的值
		value: undefined,
		
		/**
         * @param bindid/id
         */
        attach : function(id)
        {
    		this.jqSearchBox = $("#" + id);
    		this.jqInput = $("#" + id + "_input");
    		this.jqSearchBtn = $("#" + id + "_search");
    		
        	var This = this;
        	this.jqInput.bind("keypress", function(event){
        			// ie下面ctrl+回车时，值为10
        			if ((13 == event.which) || ((10 == event.which) && (event.ctrlKey)))
        			{
            			This.event = event;
        				This.doTrigger();
        			}
        		});
        	this.jqSearchBtn.bind("click", function(event){
        			This.event = event;
        			This.doTrigger();
        		});
        },
        doTrigger : function()
        {
			this.value = this.jqInput.val();
			var jSearchEvent = new jQuery.Event("search");
			jSearchEvent.value = this.value;
			// 是否是搜索下一个
			// 回车、或点击搜索图标是下一个
			// 如果是按住了ctrl键，则表示是上一个
			jSearchEvent.isNext = !this.event.ctrlKey;
			jSearchEvent.stopPropagation();
			this.jqSearchBox.trigger(jSearchEvent);
			this.event = undefined;
        }
    };//end prototype.
})();

/**
 * Define Class
 * jBME.SelectTree
 */
(function()
{
    /**
     * Define Class and its constructor
     * @param id: 
     */
    jBME.SelectTree = function(objTree)
    {   
        this.attach(objTree);
        this.adjustHeight($("div#" + objTree.settings.id).height());
        return this;
    };

    /**
     * Define Class members
     */
    jBME.SelectTree.prototype = 
    {
    	objTree: undefined,
    	jqTree: undefined,
    	jqSelectedBox: undefined,
		
        attach : function(objTree)
        {
    		this.objTree = objTree;
    		this.jqTree = $("#" + objTree.settings.id);
    		this.jqSelectedBox = $("#" + objTree.settings.id + "_selected");
    		
        	var This = this;

        	// 将默认选中的数据增加到右边框框中去
			var checkeds = $.tree.plugins.checkbox.get_checked(this.objTree);
			checkeds.each(function(i) {
				var jqA = $(this).children("a[bmeCheck]");
				var jqInput = jqA.children("input.checkbox-input");
				if (0 == jqInput.length)
				{
					return;
				}
				This.addNodePath(jqA);
			});
        	
        	// 有选中时，将数据加到右边框框中
        	// 取消选中时，将数据从右边框框中删除
        	this.jqTree.bind("check", function(event){
        			if (null == event.jqInput)
        			{
        				// 无数据关联，不处理
        				return;
        			}
        			if (event.checked)
        			{
        				This.addNodePath(event.node);
        				return;
        			}
        			
        			This.removeNodePath(event.node);
        		});
        },
		addNodePath : function (jqA) {
        	var jqInput = jqA.children("input.checkbox-input");
        	if (!jqInput.attr("checked"))
			{
				return;
			}
			var rowObj = $("<div class='treepath_row'></div>").appendTo(this.jqSelectedBox);
			jqA.data("row", rowObj);
			var jqNode = jqA.parent();
//			if(!t.settings.plugins.checkbox.fullpath)
			{
				var iconPath = jqA.attr("icon");
				if(typeof iconPath=="undefined"||iconPath==""||iconPath==null)
				{
					iconPath = jBME.theme.customPath + "/images/tree/icon_shiftleft.png";
				}
				var pathObj = $("<div class='treepath_text'><div class='treepath_text_inner'>"+
						"<img src='" + iconPath + "' width='16' height='16' border='0' style='padding:0 4px;' align='absmiddle' />"  +
						jqA.attr("nodeface") +"</div></div>").appendTo(rowObj);
			}
			rowObj.hover(function() {
				$(this).addClass("treepath_row_over");
			}, function() {
				$(this).removeClass("treepath_row_over");
			});
			rowObj.bind("click", function(){
				var t = $.tree.reference(jqNode);
				t.find_node(jqNode);
			});
			var delBtn = $("<div class='treepath_delbtn'>&nbsp;</div>").appendTo(rowObj);
			delBtn.bind("click", function() {
				$.tree.plugins.checkbox.uncheck(jqNode);
				rowObj.remove();
			});
		},
		removeNodePath : function (jqA) {
			var rowObj = jqA.data("row");
			rowObj.remove();
		},
		
		adjustHeight : function(height){
			var _jqTree = $("#" + this.objTree.settings.id + "_div");
			var tree_h = 271;
			var tree_h_r = tree_h +29;
			_jqTree.css("height",tree_h);
			this.jqSelectedBox.css("height",tree_h_r);
			
			if (height)
			{
				var _h = parseInt(height);
				var tree_tbl = $("#selectTreeTbl_" + this.objTree.settings.id);
				var tbl_h = tree_tbl.outerHeight(true);				
				tree_h = _h - (tbl_h - tree_h);
				if(tree_h<0)tree_h=0;
				_jqTree.height(tree_h);				
				tree_h_r = _h - (tbl_h - tree_h_r);
				this.jqSelectedBox.height(tree_h_r);
			}
		}
    };//end prototype.
})();

/**
 * Define Class
 * jBME.Panel
 */
(function()
{
    /**
     * Define Class and its constructor
     * @param id: 
     */
    jBME.Panel = function(id)
    {   
        this.attach(id);
        return this;
    };

    /**
     * Define Class members
     */
    jBME.Panel.prototype = 
    {
        jqPanel: undefined,
        jqHeader : undefined,
        attach : function(id)
        {
            this.jqPanel = $("#" + id);
            this.jqHeader = $("#" + id + "_header");
            
            this.bindEvent();
        },
        open : function()
        {
            this.jqPanel.removeClass("tpanel_closed");
        },
        close : function()
        {
            this.jqPanel.addClass("tpanel_closed");
        },
        toggleClose : function()
        {
            this.jqPanel.toggleClass("tpanel_closed");
        },
        isOpen : function()
        {
			return !this.jqPanel.hasClass("tpanel_closed");
        },
        bindEvent : function()
        {
            var This = this;
            bindTitle();
            bindCloseBtn();
            bindAfterValid();
            
            function bindTitle()
            {
                var jqTitle = This.jqPanel.find('> .panel_title');
        		jqTitle.bind('click', function(event) {
        			if(jBME.util.isDisabled(This.jqPanel))
        			{
        				return;
        			}
        			var actObj = $(event.target);
        			if(actObj.closest('.panel_header').size()>0)
        			{
        				return;
        			}
                    This.toggleClose();
        		});
            }
            
            function bindCloseBtn()
            {
        		var jqBtn = This.jqHeader.find('.panel_closebtn');
        		jqBtn.bind('click', function(event) {
        			if(jBME.util.isDisabled(This.jqPanel))
        			{
        				return;
        			}
        			This.jqPanel.remove();
        		});
            }
            
            function bindAfterValid()
            {
        		var validEvent = "afterValid";
        		This.jqPanel.unbind(validEvent).bind(validEvent, function(event)
        			{
        				// 如果已经展开，则直接退出
        				if (This.isOpen())
        				{
        					return;
        				}
        				// 查找本panel内部有没有校验出错的组件，有则打开自己
        				var jqErrors = This.jqPanel.find("[vStatus='fail']");
        				if (0 == jqErrors.length)
        				{
        					return;
        				}
                        This.open();
        			});
            }
        }
    };
})();

