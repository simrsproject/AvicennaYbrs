
var _divMsg;
var _divModal;
var _modalFooterDefault = '<button type="button" class="btn btn-outline-danger" data-dismiss="modal"><i class="fas fa-times"></i> Close</button>';

$(document).ready(function() {
    // create default div for message;
    _divMsg = $('<div id="divMsg">');
    _divMsg.prependTo($("body"));
    //  data-backdrop="static" data-keyboard="false"
    _divModal = $('<div class="modal fade" tabindex="-1" role="dialog" data-backdrop="static"  data-keyboard="false">' +
			    ' <div class="modal-dialog" role="document">' +
				'    <div class="modal-content">'+
				'	    <div class="modal-header">'+
				'	    <h3>Header</h3>'+
				'	    <a class="close" data-dismiss="modal">×</a>'+
				'	    </div>'+
				'	    <div class="modal-body">Content</div>'+
                '	    <div class="modal-footer">' + _modalFooterDefault + '</div>'+
				'    </div>'+
			    ' </div>'+
                '</div> ');
    _divModal.prependTo($("body"));
});

function ModalSetSize(modalSize) {
    var dlgContainer = _divModal.find('.modal-dialog');
    dlgContainer.removeClass();
    dlgContainer.addClass("modal-dialog " + (CoreIsNullOrEmpty(modalSize) ? "" : modalSize));
}

///
function ShowError(msg, modalSize) {
    if (CoreIsNullOrEmpty(msg)) {
        var isShown = _divModal.hasClass('show');
        if (isShown) {
            _divModal.modal('hide');
        }
        return _divModal;
    }
    ModalSetSize(modalSize);
    // header
    _divModal.find('.modal-header')
        .html(_htmlDlgHeaderErr);
    // body
    _divModal.find('.modal-body').html("<p>"+msg+"</p>");
    // footer
    _divModal.find('.modal-footer').html(_modalFooterDefault);
    _divModal.modal('handleUpdate');
    _divModal.modal('show');
    return _divModal;
}
///
function ShowInfo(msg, modalSize) {
    ModalSetSize(modalSize);
    // header
    _divModal.find('.modal-header')
        .html(_htmlDlgHeaderInfo);
    // body
    _divModal.find('.modal-body').html("<p>"+msg+"</p>");
    // footer
    _divModal.find('.modal-footer').html(_modalFooterDefault);
    _divModal.modal('handleUpdate');
    _divModal.modal('show');
    return _divModal;
}
///
function ShowSuccess(msg, modalSize) {
    ModalSetSize(modalSize);
    // header
    _divModal.find('.modal-header')
        .html(_htmlDlgHeaderSuccess);
    // body
    _divModal.find('.modal-body').html("<p>" + msg + "</p>");
    // footer
    _divModal.find('.modal-footer').html(_modalFooterDefault);
    _divModal.modal('handleUpdate');
    _divModal.modal('show');
    return _divModal;
}
///
function ShowProgress(modalSize) {
    ModalSetSize(modalSize);
    // header
    _divModal.find('.modal-header')
        .html(_htmlDlgHeaderProgress);
    // body
    _divModal.find('.modal-body').html("<p>"+_htmlProgress+"</p>");
    // footer
    _divModal.find('.modal-footer').html("");
    _divModal.modal('handleUpdate');
    _divModal.modal('show');
    return _divModal;
}
///
function UpdateProgress(msg, modalSize) {
    ModalSetSize(modalSize);
    // body
    _divModal.find('.modal-body').html("<p>"+msg+"</p>");
}
///
function CloseProgress() {
    UpdateProgress("Complete");
    _divModal.find('.modal-header h3 span').removeClass("fa-spin");
    
    // why dont fire?
    //console.log(_divModal);
    _divModal.modal('hide');
    //$('.modal.in').modal('hide');
    //_divModal.hide();
}

function ModalHeaderUpdate(sHeader, cssHeader, cssIco) {
    var newH = '<h3 class="' + cssHeader + '"><span class="' + cssIco + ' fa-lg"></span> ' + sHeader + '</h3>';
    _divModal.find('.modal-header').html(newH);
}
function ModalFooterAppend(footer) {
    _divModal.find('.modal-footer').html(_modalFooterDefault + footer);
}
function ModalFooterReplace(footer) {
    _divModal.find('.modal-footer').html(footer);
}

function StartProgressByDivObj(divObj, sMsg){
    var sHtml = divObj.html();
    divObj.html(sMsg);
    return sHtml;
}
function StopProgressByDivObj(divObj, sMsg){
    var sHtml = divObj.html();
    divObj.html(sMsg);
}
function StartProgressByDivName(divName, sMsg){
    var sHtml = StartProgressByDivObj($('#' + divName), sMsg);
    return sHtml;
}
function StopProgressByDivName(divName, sMsg){
    StopProgressByDivObj($('#' + divName), sMsg);
}

// load page ke objek div
function CoreLoadContentToContainerObj(url, divObj, method, fnCallback) {
    var m = (method == undefined || method == null || method == NaN) ? 'POST' : method;
    divObj.html(ReplaceLoadingIcon(_htmlLoading));
    $.ajax({
        type: m,
        url: url,
        success: function (data) {
            divObj.html(data);
            
            if(fnCallback == undefined || fnCallback == null || fnCallback == NaN){
                
            }else{
                var doExe = partial(fnCallback);
                doExe();
                //alert(fnCallback);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            divObj.html(xhr.responseText);
        },
        dataType: 'html'
    });
}

// load page ke div container
function CoreLoadContentToContainerDiv(url, divContainer, method, fnCallback) {
    CoreLoadContentToContainerObj(url, $('#' + divContainer), method, fnCallback);
}

function CoreFnConfirm(bodyMessage, FnCallBack, modalSize) {
    ModalSetSize(modalSize);

    _divModal.find('.modal-header')
        .html(_htmlDlgHeaderConfirm);
    // body
    _divModal.find('.modal-body').html("<p>" + bodyMessage + "</p>");
    // footer
    _divModal.find('.modal-footer').html(
        '<button type="button" class="btn btn-outline-danger" data-dismiss="modal"><i class="fas fa-times"></i> Cancel</button>' +
        '<button type="button" id="btnConfirm" class="btn btn-outline-warning"><i class="fas fa-check"></i> Confirm</button>'
    );
    _divModal.modal('handleUpdate');
    _divModal.modal('show');

    $("#btnConfirm").click(function () {
        FnCallBack();
    });
    return _divModal;
}

function CoreFnCustomDialog(header, body, footer, FnCallBack, modalSize) {
    ModalSetSize(modalSize);

    _divModal.find('.modal-header')
        .html(header + '<div class="card-tools"><button type="button" class="btn btn-tool btn-outline-danger" data-dismiss="modal"><i class="fas fa-times"></i></button></div>');
    // body
    _divModal.find('.modal-body').html(body);
    // footer
    _divModal.find('.modal-footer').html(_modalFooterDefault + footer);
    _divModal.modal('handleUpdate');
    _divModal.modal('show');

    if (FnCallBack != null) {
        FnCallBack();
    }
}

function partial(func /*, 0..n args */) {
    var args = Array.prototype.slice.call(arguments, 1);
    return function () {
        var allArguments = args.concat(Array.prototype.slice.call(arguments));
        return func.apply(this, allArguments);
    };
}

/// utility

// touch devices (iPhone, iPad...) doesn't have dblclick event handler
var touchtime = 0;
function IsDoubleClick() {
    if (((new Date().getTime()) - touchtime) < 500) {
        return true;
    }
    touchtime = new Date().getTime();
    return false;
};


function CoreIsNull(oVar){
    return (oVar == undefined || oVar == null || oVar == NaN);
}

function CoreIsNullOrEmpty(oVar){
    return (CoreIsNull(oVar) || oVar == '');
}

function CoreIsExistById(oId){
    return ($("#" + oId).length > 0);
}
function CoreIsExist(obj){
    return (obj.length > 0);
}

function CoreToggleShowHide(aElements, iToShow){
    $(aElements).each(function(index){
        if(iToShow != index){
            // hide others
            $('#' + aElements[index]).hide();
        }
    });
    // show based on flag
    $('#' + aElements[iToShow]).show(_ctrlShowEffect, _ctrlShowEffectDuration);
}

function CoreToggleShowHideWithFn(aElements, iToShow, callbackFn){
    $(aElements).each(function(index){
        if(iToShow != index){
            // hide others
            $('#' + aElements[index]).hide();
        }
    });

    // show based on flag
    $('#' + aElements[iToShow]).html(ReplaceLoadingIcon(_htmlLoading));
    $('#' + aElements[iToShow]).show();
    //return;
    callbackFn(function(){
        $('#' + aElements[iToShow]).hide();
        $('#' + aElements[iToShow]).show(_ctrlShowEffect, _ctrlShowEffectDuration);
    });
}

function CoreDivScrollToBottom(oDiv){
    oDiv.animate({"scrollTop": oDiv[0].scrollHeight}, "slow");
}
function CoreDivScrollTo(oContainer, oElement) {
    oContainer.animate({ scrollTop: oElement.offset().top }, "slow");
}

function CoreIsVisible(oDiv) {
    //console.log(oDiv.is(':visible'));
    return oDiv.is(':visible');
}

function CoreShowApprove(oDiv){
    //oDiv.
}

function CheckBoxEnableAll(formId) {
    $("#" + formId + " input[type='checkbox']").removeAttr('disabled');
}
function CheckBoxDisableAll(formId) {
    $("#" + formId + " input[type='checkbox']").attr('disabled', 'disabled');
}

function DisableButtonDialog(sBName) {
    $(".ui-dialog-buttonpane button:contains('" + sBName + "')").button("disable");
}
function EnableButtonDialog(sBName) {
    $(".ui-dialog-buttonpane button:contains('" + sBName + "')").button("enable");
}
function CheckBoxVal(chkId) {
    //return $('#' + chkId).is(':checked')? 1 : 0 ;
    return $('#' + chkId).prop("checked");
}
function CheckBoxSetCheck(chkId) {
    //$('#' + chkId).attr('checked', 'checked');
    $('#' + chkId).prop('checked', true);
}
function CheckBoxSetUncheck(chkId) {
    //$('#' + chkId).removeAttr('checked');
    $('#' + chkId).prop('checked', false);
}

// COMBOBOX
function CoreCBReset(cbID){
    var options = $("#" + cbID);
    options.empty();
    return options;
}
function CoreCBFillSample(cbID){
    CoreCBReset(cbID);
    options.append(new Option("-- Select --", 0));
    $.each(ajx_obj, function () {
        options.append(new Option(this.text, this.value));
    });
}
// end of COMBOBOX

function CoreMysqlDateToJsDate(sMysqlDate){
    var t = sMysqlDate.split(/[- :]/);

    // Apply each element to the Date function
    return new Date(t[0], t[1]-1, t[2], t[3], t[4], t[5]);
}

// for datatable not yet used
function fnSetKey( aoData, sKey, mValue )
{
    for ( var i=0, iLen=aoData.length ; i<iLen ; i++ )
    {
        if ( aoData[i].name == sKey )
        {
            aoData[i].value = mValue;
        }
    }
}
 
function fnGetKey( aoData, sKey )
{
    for ( var i=0, iLen=aoData.length ; i<iLen ; i++ )
    {
        if ( aoData[i].name == sKey )
        {
            return aoData[i].value;
        }
    }
    return null;
}
// end for datatable

// post utility
function coreReplaceSerialize(str){
    return str.replace(/=on/g,"=1");
}
function coreReplaceSerializeIncArray(str, arrObjName){
    return coreReplaceSerialize(str).replace(new RegExp(arrObjName, 'g'), arrObjName + '[]');
}
// end of post utility

// JSON
function StrToJson(str) {
    // deserialize string to json object :P
    if (str == "") str = "[]";

    var jSonArray;
    try {
        jSonArray = (new Function("return( " + str + " );"))();
    } catch (e) {
        jSonArray = (new Function("return( [] );"))();
    }
    return jSonArray;
}

function JsonToStr(jSonArray) {
    return JSON.stringify(jSonArray);
}
// END OF JSON


// COMMON
function ReplaceAll(str, tobereplace, replacewith) {
    return str.split(tobereplace).join(replacewith);
}

function RegNoReplace(RegNo){
    return RegNo.replace("/","").replace("/","").replace("-","").replace(" ","").toLowerCase();
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function random_rgba() {
    var o = Math.round, r = Math.random, s = 255;
    return 'rgba(' + o(r() * s) + ',' + o(r() * s) + ',' + o(r() * s) + ',' + r().toFixed(1) + ')';
}

// array
function ArraySortNumericDistinct(arr) {
    ArraySortNumeric(arr);
    var last_i;
    for (var i = 0; i < arr.length; i++)
        if ((last_i = arr.lastIndexOf(arr[i])) !== i)
            arr.splice(i + 1, last_i - i);
    return arr;
}
function ArraySortNumeric(arr) {
    arr.sort(function (a, b) { return a - b });
}

// print
function PrintRAW(PrintCode) {
    var printWindow = window.open();
    printWindow.document.open('text/plain')
    printWindow.document.write(PrintCode);
    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
    printWindow.close();
}
function PrintEpsonTM(str) {
    // init
    var strp = String.fromCharCode(27, 32, 64, 32, 10);// "ESC @\n";
    //justification: Centering
    strp += String.fromCharCode(27, 32, 97, 32, 49, 10); //"ESC a 1\n";
    //character size: (horizontal (times 2) x vertical (times 2))
    strp += "GS ! 0x11\n";
    strp += str;
    //alert(strp);
    //PrintRAW(strp);
}

/* View in fullscreen */
function OpenFullScreen() {
    var elem = document.documentElement;
    if (elem.requestFullscreen) {
        elem.requestFullscreen();
    } else if (elem.mozRequestFullScreen) { /* Firefox */
        elem.mozRequestFullScreen();
    } else if (elem.webkitRequestFullscreen) { /* Chrome, Safari and Opera */
        elem.webkitRequestFullscreen();
    } else if (elem.msRequestFullscreen) { /* IE/Edge */
        elem.msRequestFullscreen();
    }
}

// iseng
function ReplaceLoadingIcon(source) {
    return source.replace(_icoLoadingDefault, _icoLoading[Math.floor(Math.random() * _icoLoading.length)]);
}

function CoreLoadingOn(o) {
    if (o.is("button")) {
        CoreButtonLoadingOn(o);
    } else if (o.is("a")) {
        CoreAHrefLoadingOn(o);
    } else {
        //alert("Loading on is not supported");
    }
}
function CoreLoadingOff(o) {
    if (o.is("button")) {
        CoreButtonLoadingOff(o);
    } else if (o.is("a")) {
        CoreAHrefLoadingOff(o);
    } else {
        //alert("Loading off is not supported");
    }
}
function CoreButtonLoadingOn(oBtn) {
    var iTag = oBtn.find('i');
    var sClass = iTag.attr("class");
    iTag.data('ClassOri', sClass);
    iTag.removeClass();
    iTag.addClass(ReplaceLoadingIcon(_icoLoadingDefault) + " fa-spin");
    oBtn.prop('disabled', true);
}
function CoreButtonLoadingOff(oBtn) {
    setTimeout(function () {
        var iTag = oBtn.find('i');
        iTag.removeClass();
        iTag.addClass(iTag.data('ClassOri'));
        oBtn.prop('disabled', false);
    }, 500); // prevent double clicked
    
}
function CoreAHrefLoadingOn(oAHref) {
    var iTag = oAHref.find('i');
    var sClass = iTag.attr("class");
    iTag.data('ClassOri', sClass);
    iTag.removeClass();
    iTag.addClass(ReplaceLoadingIcon(_icoLoadingDefault) + " fa-spin");
}
function CoreAHrefLoadingOff(oAHref) {
    setTimeout(function () {
        var iTag = oAHref.find('i');
        iTag.removeClass();
        iTag.addClass(iTag.data('ClassOri'));
    }, 500); // prevent double clicked
    
}

function SetHtml(controlName, title) {
    $('#' + controlName).hide();
    $('#' + controlName).html(title).fadeIn('slow');
}
function NavLeftClear() {
    $('#navLeftCustom').html('');
}
function NavLeftSetSelected(Sender) {
    $('#navLeftCustom .nav-link').each(function () {
        $(this).removeClass("active");
    });
    $(Sender).addClass("active");
}
function NavLeftAddItem(Text, iconClass, JsFN, GroupID, marginTopClass) {
    if (CoreIsNullOrEmpty(GroupID)) {
        $('#navLeftCustom').append(
            '<nav class="' + (CoreIsNullOrEmpty(marginTopClass) ? "mt-2" : marginTopClass) + '">' +
            ' <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">' +
            '  <li class="nav-item">' +
            (CoreIsNullOrEmpty(JsFN) ? '':'   <a href="javascript:void(0);" onclick="javascript:' + JsFN + ';" class="nav-link">') +
            (CoreIsNullOrEmpty(iconClass) ? '' :'    <i class="nav-icon ' + iconClass + '">' + '</i>') +
            '    <p>&nbsp;' + Text + '</p>' +
            (CoreIsNullOrEmpty(JsFN) ? '' :'   </a>') +
            '  </li>' +
            ' </ul>' +
            '</nav>');
    } else {
        //console.log('#' + GroupID + 'Item');
        $('#' + GroupID + 'Item').append(
            '<li class="nav-item">' +
            (CoreIsNullOrEmpty(JsFN) ? '':' <a href="javascript:void(0);" onclick="javascript:' + JsFN + ';" class="nav-link">') +
            (CoreIsNullOrEmpty(iconClass) ? '':'  <i class="nav-icon ' + iconClass + '"></i>') + '<p>' + Text + '</p>' +
            (CoreIsNullOrEmpty(JsFN) ? '' :' </a>') +
            '</li>');
    }
}
function NavLeftAddItemNonIcon(Text, iconClass, JsFN, GroupID, marginTopClass) {
    if (CoreIsNullOrEmpty(GroupID)) {
        $('#navLeftCustom').append(
            '<nav class="' + (CoreIsNullOrEmpty(marginTopClass) ? "mt-2" : marginTopClass) + '">' +
            ' <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">' +
            '  <li class="nav-item">' +
            (CoreIsNullOrEmpty(JsFN) ? '' : '   <a href="javascript:void(0);" onclick="javascript:' + JsFN + ';" class="nav-link">') +
            (CoreIsNullOrEmpty(iconClass) ? '' : '    <img src="' + iconClass + '" alt="Paris">') +
            '    <p>&nbsp;' + Text + '</p>' +
            (CoreIsNullOrEmpty(JsFN) ? '' : '   </a>') +
            '  </li>' +
            ' </ul>' +
            '</nav>');
    } else {
        //console.log('#' + GroupID + 'Item');
        $('#' + GroupID + 'Item').append(
            '<li class="nav-item">' +
            (CoreIsNullOrEmpty(JsFN) ? '' : ' <a href="javascript:void(0);" onclick="javascript:' + JsFN + ';" class="nav-link">') +
            (CoreIsNullOrEmpty(iconClass) ? '' : '    <img src="' + iconClass + '" alt="Paris">') + '<p>' + Text + '</p>' +
            (CoreIsNullOrEmpty(JsFN) ? '' : ' </a>') +
            '</li>');
    }
}
function NavLeftAddGroup(GroupID, GroupText, iconClass) {
    $('#navLeftCustom').append(
        '<nav class="mt-2">' +
        ' <ul id="' + GroupID + '" class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">' +
        '  <li class="nav-item has-treeview menu-open">' +
        '   <a href="#" class="nav-link">' +
        '    <i class="nav-icon ' + iconClass + '"></i>' +
        '    <p>' + GroupText + '<i class="right fas fa-angle-left"></i></p>' +
        '   </a>' +
        '   <ul id="' + GroupID + 'Item" class="nav nav-treeview">' +
        '   </ul>' +
        '  </li>' +
        ' </ul>' +
        '</nav>'
    );
}
