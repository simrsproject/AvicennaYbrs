<%@ Import Namespace="Temiang.Avicenna.Common " %>
<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterBootstrap.Master" AutoEventWireup="true"
    Codebehind="QueueEntry.aspx.cs" 
    Inherits="Temiang.Avicenna.Module.Kiosk.QueueEntry" EnableTheming="False"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<link href="<%=Helper.UrlRoot() %>/Bootstrap/plugins/flags/css/flags.css" rel="stylesheet" />--%>
    <link href="<%=Helper.UrlRoot() %>/Bootstrap/plugins/flag-icon-css/css/flag-icon.min.css" rel="stylesheet" />
    
    <style>
        .cButton {
            display: inline-block;
            height: 90px;
            width: 200px;
            font-size: 22px;
        }
        .pButton {
            display: inline-block;
            height: 100px;
            width: 270px;
            font-size: 28px;
        }
        .myInfoBtn {
            font-size:18px;
        }
    </style>
    <script language="javascript">   
        var revVersion = "0.dev";
        //$ = $telerik.$;

        var lang = {
            "title": { "ind": "Sistem Antrian", "en": "Queuing System" },
            "sub1Title": { "ind": "Silahkan pilih antrian berikut", "en": "Please choose one of the following queue" }
        };
        var databtn = {};

        $(document).ready(function() {
            //alert(BaseURL + "/WebService/jQueryWS.asmx/NPCGetList");    

            $.ajaxSetup({
                beforeSend: function (xhr) {
                    if (xhr.overrideMimeType) {
                        xhr.overrideMimeType("application/json");
                    }
                }
            });

            SetLang("Ind");
            //SetLeftMenu("Bahasa", "SetLang('Ind')", "flag flag-id");
            //SetLeftMenu("English", "SetLang('En')", "flag flag-us");
            SetLeftMenu("Bahasa", "SetLang('Ind')", "flag-icon flag-icon-id");
            SetLeftMenu("English", "SetLang('En')", "flag-icon flag-icon-us");

            // load queue buttons
            GetRefID();
            LoadQueueButtons();
        });

        function SetLang(l) {
            SetHtml('spanPageTitle', l == "Ind" ? lang.title.ind : lang.title.en);
            SetHtml('spanSub1Title', l == "Ind" ? lang.sub1Title.ind : lang.sub1Title.en);
            // set button lang
            var spans = $("[lang=id]");
            //console.log(spans);
            for (var i = 0; i < spans.length; i++) {
                if(l == "Ind")
                    $(spans[i]).fadeIn('slow');
                else
                    $(spans[i]).hide();
            }

            var spans = $("[lang=en]");
            //console.log(spans);
            for (var i = 0; i < spans.length; i++) {
                if (l == "En")
                    $(spans[i]).fadeIn('slow');
                else
                    $(spans[i]).hide();
            }
        }
        
        function LoadQueueButtons() {
            var divMsg = StartProgressByDivName("divBtnEntry", _htmlLoading)

            var refid = GetRefID();
            var url = BaseURL + '/WebService/KioskQueue.asmx/GetQueueCode';
            //alert(url);

            $.ajax({
                type: 'POST',
                url: url,
                data: { RefID: refid },
                success: function (ret) {
                    StopProgressByDivName("divBtnEntry", "");
                    if (ret.status === 'OK') {
                        databtn = ret.data;
                        for (var i = 0; i < ret.data.length; i++) {
                            $('#divBtnEntry').append(GenerateButton(ret.data[i], false));
                            //$('#divBtnEntry').append("<br /><br />");
                        }

                        AttachNotifQueue();
                    } else {
                        ShowError(ret.data);
                    }

                    _fnNextIsRunning = false;
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    _fnNextIsRunning = false;
                    StopProgressByDivName("divBtnEntry", "");
                    ShowError(xhr.responseText);
                },
                dataType: 'json'
            });
        }

        function GenerateButton(iData, isChild) {
            //var btn = $('<a id="btn' + iData.ItemID +
            //    '" class="btn btn-outline-primary"><i class="fa fa-list fa-1x"></i> ' +
            //    '<span lang="id">' + iData.ItemName + '</span><span lang="en" style="display:none">' + iData.ItemNameEn + '</span>' +
            //    '<span id="btn' + iData.ItemID + 'info" class="badge bg-success navbar-badge-left"></span></a>');

            var sty = CoreIsNullOrEmpty(iData.CustomField) ? "" : iData.CustomField;
            var btn = $('<a id="btn' + iData.ItemID +
                '" class="btn btn-app ' + (isChild ? 'cButton' : 'pButton') + '" ' + (CoreIsNullOrEmpty(sty) ? '' : sty) + '>' +
                '<span lang="id">' + iData.ItemName + '</span><span lang="en" style="display:none">' + iData.ItemNameEn + '</span>' +
                '<span id="btn' + iData.ItemID + 'info" class="badge bg-success myInfoBtn"></span></a>');

            if (iData.ChildCount > 0) {
                btn.click({
                    param1: iData.ItemID,
                    param2: iData.ItemName
                }, function (params) {
                    return ShowButtonChild(params.data.param1, params.data.param2);
                });
                //btn.prop("onclick", "javascript:ShowButtonChild('" + ret.data[i].ItemID + "')");
            } else {
                btn.click({ param1: iData.ItemID }, function (params) {
                    return QueueAdd(params.data.param1);
                });
                //btn.prop("onclick", "javascript:QueueAdd('" + ret.data[i].ItemID + "')");
            }
            return btn;
        }

        function GetRefID() {
            var refid = getUrlVars()["refid"];
            if (refid == undefined) refid = "";
            return refid;
        }

        function AttachNotifQueue() {
            var args = [];
            var jSonFnTic = {fn: GetLastData, args: args, tic: 5000};
            _FNTOEXEC.push(jSonFnTic);

            var jSonFnAjaxCounter = { fn: GetAjaxCounter, args: args, tic: 600000 };
            _FNTOEXEC.push(jSonFnAjaxCounter);
        }

        var _fnGetDataIsRunning = false;
        function GetLastData() {
            if(_fnGetDataIsRunning == true) return;
            
            _fnGetDataIsRunning = true;

            var refid = GetRefID();
            var url = BaseURL + '/WebService/KioskQueue.asmx/GetLastQueueByRefID';

            $.ajax({
                type: 'POST',
                url: url,
                data: { RefID: refid, },
                success: function(ret) {
                    if (ret.status === 'OK') {
                        ParsingQueueLast(ret.data);
                    } else {
                        DisplayToast(ret.data, "error");
                    }

                    _fnGetDataIsRunning = false;
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    _fnGetDataIsRunning = false;
                    DisplayToast(xhr.responseText, "error");
                },
                dataType: 'json'
            });
        }
        function ParsingQueueLast(oData){
            var o = oData[0];
            for (var i = 0; i < o.length; i++) {
                //alert(o[i].next);
                var oSpan = $('#btn' + o[i].kioskQueueType + 'info');
                if (oSpan.length) {
                    //oSpan.html((o[i].next == "") ? "" : o[i].next + " / " + o[i].last);
                    oSpan.html(o[i].count == 0 ? "" : o[i].count);
                }
            }
        }

        var _fnQueueAddIsRunning = false;
        function QueueAdd(code) {
            if (_fnQueueAddIsRunning == true) {
                ShowInfo("Please try again in a few seconds");
                return;
            } 

            _fnQueueAddIsRunning = true;

            var refid = GetRefID();
            var url = BaseURL + '/WebService/KioskQueue.asmx/QueueAdd';

            ShowProgress();

            $.ajax({
                type: 'POST',
                url: url,
                data: { code: code },
                success: function (ret) {
                    if (ret.status === 'OK') {
                        //PrintEpsonTM(ret.data);
                        dlgInfo = ShowInfo("Terima kasih untuk mengantri, nomor antrian Anda adalah: <br />Thank you for queuing, your queue number is:<hr /><H1>" + ret.data + "</H1>");
                        setTimeout(function () {
                            var isShown = dlgInfo.hasClass('show');
                            if (isShown) {
                                dlgInfo.modal('hide');
                            }
                        }, 1000);
                    } else {
                        ShowError(ret.data);
                    }

                    _fnQueueAddIsRunning = false;
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    _fnQueueAddIsRunning = false;
                    ShowError(xhr.responseText);
                },
                dataType: 'json'
            });
        }

        // ============= child queue
        function ShowButtonChild(RefID, HeaderName) {
            // show editor form
            var header = "<h3><span class='fa fa-lg fa-edit'></span> " + HeaderName.replace("<br />","") + "</h3>";
            var body = "<div class='row'>" +
                "<div class='col-sm-12'>" +
                "<div class='form-group' id='divDlgContent'>" +
                "</div>" +
                "</div></div>";
            var footer = "";//"<a id='lnSubmit' class='btn text-success'><span class='fa fa-lg fa-check'></span> Submit</a>";

            CoreFnCustomDialog(header, body, footer, function () {
                LoadButtonChild(RefID);
            }, "modal-xl");
        }
        function LoadButtonChild(RefID) {
            var divMsg = StartProgressByDivName("divDlgContent", _htmlLoading)

            var url = BaseURL + '/WebService/KioskQueue.asmx/GetQueueCodeChild';

            $.ajax({
                type: 'POST',
                url: url,
                data: { RefID: RefID },
                success: function (ret) {
                    StopProgressByDivName("divDlgContent", "");
                    if (ret.status === 'OK') {
                        for (var i = 0; i < ret.data.length; i++) {
                            //var queueBtn = $('<a id="btn' + ret.data[i].ItemID +
                            //    '" class="btn btn-outline-primary"><i class="fa fa-list fa-1x"></i> ' +
                            //    ret.data[i].ItemName
                            //    + '<span id="btn' + ret.data[i].ItemID + 'info" class="badge bg-success navbar-badge-left"></span></a>');
                            //if (ret.data[i].ChildCount > 0) {
                            //    queueBtn.click({
                            //        param1: ret.data[i].ItemID,
                            //        param2: ret.data[i].ItemName
                            //    }, function (params) {
                            //        return ShowButtonChild(params.data.param1, params.data.param2);
                            //    });
                            //    //queueBtn.prop("onclick", "javascript:ShowButtonChild('" + ret.data[i].ItemID + "')");
                            //} else {
                            //    queueBtn.click({ param1: ret.data[i].ItemID }, function (params) {
                            //        return QueueAdd(params.data.param1);
                            //    });
                            //    //queueBtn.prop("onclick", "javascript:QueueAdd('" + ret.data[i].ItemID + "')");
                            //}

                            $('#divDlgContent').append(GenerateButton(ret.data[i], true));
                            //$('#divDlgContent').append("<br /><br />");
                        }

                        //AttachNotifQueue();
                    } else {
                        ShowError(ret.data);
                    }

                    _fnNextIsRunning = false;
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    _fnNextIsRunning = false;
                    StopProgressByDivName("divDlgContent", "");
                    ShowError(xhr.responseText);
                },
                dataType: 'json'
            });
        }
        // ============================
        var _aElements = ["divList","divDetail","divEntry"];
    </script>
    <div class="overlay"></div>
    <div class="row">
        <div class="col-12">
          <div id="divList" class="card" style="width:100%; border:none;">
            <div class="card-header">
                <h3 class="card-title"><span class='fa fa-lg fa-book'></span> <span id="spanSub1Title"></span> </h3>
            </div>
            <div class="card-body">
                <div id="divBtnEntry">
                    <a class="btn btn-outline-primary">
                        <i class="fa fa-list fa-1x"></i> Queue Button
                        <span class="badge bg-success navbar-badge-left">0 / 0</span>
                    </a>
                </div>
            </div>
          </div>
          <div id="divDetail"></div>
          <div id="divEntry"></div>
        </div>  
    </div> 
    <form id="Form1" runat="server">
    </form>
</asp:Content>
