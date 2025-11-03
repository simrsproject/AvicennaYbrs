<%@ Import Namespace="Temiang.Avicenna.Common " %>

<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterBootstrap.Master" AutoEventWireup="true"
    CodeBehind="QueueDisplay.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Kiosk.QueueDisplay" EnableTheming="False" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="<%=Helper.UrlRoot() %>/Bootstrap/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <!-- Page level plugin JavaScript-->
    <script src="<%=Helper.UrlRoot() %>/Bootstrap/plugins/datatables/jquery.dataTables.js"></script>
    <script src="<%=Helper.UrlRoot() %>/Bootstrap/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>

    <style>
        .dataTables_scrollHeadInner {
            width: 100% !important;
            padding: 0 !important;
        }

        .dataTables_scrollHeadInner table {
            width: 100% !important;
            padding: 0 !important;
        }

        .myTh {
            font-size: 24px;
        }

        .ctr {
            font-size:2.5rem;
        }
        .queueno {
            font-size:4rem;
            font-weight:bold;
        }
        .textcenter {
            align-content:center;
            text-align:center;
        }
    </style>

    <script language="javascript">
        var revVersion = "0.dev";
        var iBlinkDurInSecs = 15;
        //$ = $telerik.$;

        $(document).ready(function () {
            $.ajaxSetup({
                beforeSend: function (xhr) {
                    if (xhr.overrideMimeType) {
                        xhr.overrideMimeType("application/json");
                    }
                }
            });

            // set nav collapse
            $('body').removeClass("sidebar-mini");
            $('body').addClass("sidebar-collapse");

            SetHtml("spanPageTitle", "<%= GetBrandName()%> Queuing System");

            // load queue buttons
            GetRefID();
            //LoadQueueDisplay();
            dtTable = $('#dtList').DataTable({
                searching: false,
                paging: false,
                info: false,
                autoWidth: true,
                "columnDefs": [{
                    "targets": '_all',
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).css('padding-top', '0px');
                        $(td).css('padding-bottom', '0px');
                        $(td).css('font-size', '20px');
                    }
                }],
                "columns": [
                    { data: 'ItemID', name: 'ItemID', visible: false },
                    {
                        title: "Description", data: 'ItemName', name: 'ItemName', className: "myTh",
                        "render": function (data, type, row, meta) {
                            return row.ItemName.split("|")[0].replace("<br />", " ");
                        }
                    },
                    {
                        title: "Next", data: 'ItemID', className: "myTh",
                        "render": function (data, type, row, meta) {
                            return "<span id=\"spanLast" + row.ItemID + "\"></span>";
                        }
                    }
                ],
                "ajax": {
                    "url": "<%=Helper.UrlRoot() %>/WebService/KioskQueue.asmx/GetQueueCodeUnionChild",
                    "type": "POST",
                    "datatype": "json",
                    "data": function (d) {
                        d.RefID = GetRefID()
                    }
                }
            });

            AttachFn();
        });

        function GetRefID() {
            var refid = getUrlVars()["refid"];
            if (refid == undefined) refid = "";
            return refid;
        }

        function AttachFn() {
            var args = [];
            var jSonFnTic = { fn: GetLastData, args: args, tic: 2000 };
            _FNTOEXEC.push(jSonFnTic);

            var jSonFnAjaxCounter = { fn: GetAjaxCounter, args: args, tic: 60000 };
            _FNTOEXEC.push(jSonFnAjaxCounter);
        }

        var _fnGetDataIsRunning = false;
        var _lastData = "";

        function GetLastData() {
            if (_fnGetDataIsRunning == true) return;

            _fnGetDataIsRunning = true;

            var refid = GetRefID();
            var url = BaseURL + '/WebService/KioskQueue.asmx/GetLastQueueByRefIDWithCounterUpdate';

            $.ajax({
                type: 'POST',
                url: url,
                data: { RefID: refid, LastData: _lastData },
                success: function (ret) {
                    if (ret.status === 'OK') {
                        _lastData = JsonToStr(ret.data[1]);
                        ParsingQueueLast(ret.data);
                    } else {
                        DisplayToast(ret.data, "error");
                    }

                    _fnGetDataIsRunning = false;
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    _fnGetDataIsRunning = false;
                    DisplayToast(xhr.responseText, "error");
                },
                dataType: 'json'
            });
        }

        var counters = [];
        function ParsingQueueLast(oData) {
            var sortCounters = false;
            var o = oData[0];
            for (var i = 0; i < o.length; i++) {
                var oSpan = $('#spanLast' + o[i].kioskQueueType);
                if (oSpan.length) {
                    oSpan.html(o[i].next);
                }
            }

            /////
            //var table = document.getElementById("dtList");
            //for (var i = 0, row; row = table.rows[i]; i++) {

            //    console.log(row.cells[2]);
            //    //var xxx = $(row.cells[2]);
            //    //console.log(xxx);
            //    //for (var j = 0, col; col = row.cells[j]; j++) {
            //    //    if (col.innerText == "Pending") {
            //    //        row.style = "display:none;";
            //    //        console.log(col.innerText);
            //    //    }
            //    //}
            //}
            //console.log(oData);
            /////

            // refresh counter
            var c = oData[1];
            // set all inactive
            for (var i = 0; i < counters.length; i++) {
                counters[i].iCtr++;
            }

            for (var i = 0; i < c.length; i++) {
                if (ReplaceAll(c[i].ProcessByUserID, '.', '') == '') continue;
                // find by userid
                var aIdx = counters.findIndex((obj => ReplaceAll(obj.ProcessByUserID, '.', '') === ReplaceAll(c[i].ProcessByUserID, '.', '')));
                if (aIdx == -1) {
                    // create new
                    c[i].iCtr = 0;
                    counters.push(c[i]);
                    aIdx = counters.findIndex((obj => ReplaceAll(obj.ProcessByUserID, '.', '') === ReplaceAll(c[i].ProcessByUserID, '.', '')));
                    // create display
                    CreateCounter(counters[aIdx]);
                    sortCounters = true;
                } else {
                    counters[aIdx].iCtr = 0;
                    if (counters[aIdx].KioskQueueNo != c[i].KioskQueueNo ||
                        counters[aIdx].LastCounterName != c[i].LastCounterName ||
                        c[i].DataCounter == 0) {
                        // update
                        counters[aIdx].KioskQueueNo = c[i].KioskQueueNo;
                        counters[aIdx].LastCounterName = c[i].LastCounterName;
                        counters[aIdx].DataCounter = c[i].DataCounter;
                        // update display
                        UpdateCounter(counters[aIdx]);
                    }
                }

                // remove another with the same counter name
                // find by lastcountername in case ganti shift
                aIdx = counters.findIndex(obj => obj.LastCounterName === counters[aIdx].LastCounterName &&
                    ReplaceAll(obj.ProcessByUserID, '.', '') != ReplaceAll(counters[aIdx].ProcessByUserID, '.', ''));
                if (aIdx != -1) {
                    RemoveCounter(counters[aIdx]);
                    counters.splice(aIdx, 1);
                    ReDraw();
                }
            }

            for (var i = 0; i < counters.length; i++) {
                if (counters[i].iCtr > 0) {
                    counters[i].KioskQueueNo = "OFF";
                    UpdateCounter(counters[i]);
                }
            }

            if (sortCounters) SortCounters();
        }

        function SortCounters() {
            var $divs = $('#divCounters .textcenter');
            var alphabeticallyOrderedDivs = $divs.sort(function (a, b) {
                return $(a).attr("data-order") > $(b).attr("data-order");
            });
            $("#divCounters").html(alphabeticallyOrderedDivs);
        }

        function CreateCounter(obj) {
            obj.iBlink = 99;
            if (ReplaceAll(obj.ProcessByUserID, '.', '') == '') return;
            var sNew = $('<div id="box' + ReplaceAll(obj.ProcessByUserID, '.', '') + '" class="col-12 small-box bg-info textcenter" data-order="' + obj.LastCounterName +
                '" style="display:none;"><span id="no' + ReplaceAll(obj.ProcessByUserID, '.', '') +
                '" class="queueno">' + obj.KioskQueueNo +
                '</span><a class="small-box-footer ctr"><i class="fa fa-desktop"></i> <span id="ctr' + ReplaceAll(obj.ProcessByUserID, '.', '') +
                '">' + obj.LastCounterName + '</span></a></div>');
            sNew.appendTo($('#divCounters')).fadeIn('slow');
            //SortCounters();

            //console.log("CreateCounter");
            ReDraw();

            Say(obj);
        }
        function UpdateCounter(obj) {
            obj.iBlink = 99;
            $('#no' + ReplaceAll(obj.ProcessByUserID, '.', '')).html(obj.KioskQueueNo);
            $('#ctr' + ReplaceAll(obj.ProcessByUserID, '.', '')).html(obj.LastCounterName);

            if (obj.KioskQueueNo == "OFF") {
                UpdateCounterBG(obj);
            } else {
                Say(obj);
            }
        }
        function UpdateCounterBG(obj) {
            obj.iBlink++;
            if (obj.KioskQueueNo == "OFF") {
                $('#box' + ReplaceAll(obj.ProcessByUserID, '.', '')).removeClass("bg-info").removeClass("bg-warning").addClass("bg-secondary disabled");
            } else {
                if (obj.iBlink < iBlinkDurInSecs && blinkCounter % 2 == 1) {
                    $('#box' + ReplaceAll(obj.ProcessByUserID, '.', '')).removeClass("bg-secondary disabled").removeClass("bg-info").addClass("bg-warning");
                } else {
                    $('#box' + ReplaceAll(obj.ProcessByUserID, '.', '')).removeClass("bg-secondary disabled").removeClass("bg-warning").addClass("bg-info");
                }
            }
        }
        function RemoveCounter(obj) {
            var oDiv = $('#box' + ReplaceAll(obj.ProcessByUserID, '.', ''));
            oDiv.remove();
        }

        var iCounterCount = 0;
        function ReDraw() {
            if (counters.length != iCounterCount) {
                iCounterCount = counters.length;
                // redraw
                if (iCounterCount <= 4) {
                    $('#divTabel').removeClass().addClass("col-8");
                    $('#divCounter').removeClass().addClass("col-4");
                    var divs = $('#divCounters .textcenter');
                    for (var i = 0; i < divs.length; i++) {
                        $(divs[i]).removeClass("col-6").addClass("col-12");
                    }
                } else {
                    $('#divTabel').removeClass().addClass("col-6");
                    $('#divCounter').removeClass().addClass("col-6");
                    var divs = $('#divCounters .textcenter');
                    for (var i = 0; i < divs.length; i++) {
                        $(divs[i]).removeClass("col-12").addClass("col-6");
                    }
                }
            }
        }

        var blinkCounter = 0;
        var blinkIsRunning = false;
        function BlinkCounters() {
            blinkIsRunning = true;

            var bDone = true;
            for (var i = 0; i < counters.length; i++) {
                if (counters[i].iBlink <= iBlinkDurInSecs) {
                    UpdateCounterBG(counters[i]);
                    bDone = false;
                }
            }

            if (!bDone) {
                setTimeout(function () {
                    BlinkCounters();
                }, 1000);
                blinkCounter++;
            } else {
                blinkCounter = 0;
                blinkIsRunning = false;
            }
        }

        var sQueToSay = [];
        function Say(obj) {
            sQueToSay.push("1st|" + ReplaceAll(obj.ProcessByUserID, '.', ''));
            sQueToSay.push("s_nomorurut");
            myArray = obj.KioskQueueNo.split(/([0-9]+)/);
            for (var i = 0; i < myArray.length; i++) {
                if (isNaN(myArray[i])) {
                    var aChar = myArray[i].toLowerCase().split("");
                    for (var b = 0; b < aChar.length; b++) {
                        if (aChar[b] == "") continue;
                        sQueToSay.push("s_" + aChar[b]);
                    }
                } else {
                    var sNum = SayNumber(parseInt(myArray[i]));
                    var aNum = sNum.split(" ");
                    for (var j = 0; j < aNum.length; j++) {
                        if (aNum[j] == "") continue;
                        sQueToSay.push("s_" + aNum[j]);
                    }
                }
            }

            sQueToSay.push("s_konter");
            var iCounter = 0;
            if (!CoreIsNullOrEmpty(obj.LastCounterName)) {
                iCounter = obj.LastCounterName.replace(/[^0-9\.]/g, '');
            }
            var sCounter = SayNumber(iCounter);
            var aCounter = sCounter.split(" ");
            for (var k = 0; k < aCounter.length; k++) {
                if (aCounter[k] == "") continue;
                sQueToSay.push("s_" + aCounter[k]);
            }

            if (!SetMediaIsRunning) SetMedia(0);
        }
        function GetDiffTime(a, b) {
            return (Math.round(a.getTime() - b.getTime()));
        }

        var SetMediaIsRunning = false;
        function SetMedia(iDelay) {
            SetMediaIsRunning = true;
            if (sQueToSay.length == 0) {
                SetMediaIsRunning = false;
                return;
            }

            setTimeout(function () {
                var jDelay = 0;
                var start_date = new Date();
                var aSkip = ["", " ", "-", "s_", "s_ ", "s_-"];
                if (aSkip.indexOf(sQueToSay[0]) >= 0 || sQueToSay[0].indexOf("1st|") >= 0) {
                    if (sQueToSay[0].indexOf("1st|") >= 0) {
                        var aIdx = counters.findIndex((obj => ReplaceAll(obj.ProcessByUserID, '.', '') === sQueToSay[0].split("|")[1]));
                        try {
                            //console.log(aIdx);
                            //console.log(counters);
                            // kalau bentrok counter number 
                            counters[aIdx].iBlink = 0;
                        } catch (ex) {
                            sQueToSay = [];
                            SetMediaIsRunning = false;
                            return;
                        }
                        if (!blinkIsRunning) BlinkCounters();
                    }
                } else {
                    if (sQueToSay[0] == 's_nomorurut') {
                        DisplayToast("<H1> Attention </H1>", "warning");
                    }

                    var m = document.getElementById(sQueToSay[0]);
                    if (m != null) {
                        m.pause();
                        m.currentTime = 0;
                        jDelay = m.duration * 1000;
                        m.play();
                    }
                }
                sQueToSay.splice(0, 1);
                SetMedia(jDelay);
            }, iDelay);
        }

        function SayNumber(num) {
            num = Math.abs(num);
            var huruf = ["", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan", "sepuluh", "sebelas"];
            var tmp = "";
            if (num < 12) {
                tmp = "" + huruf[num];
            } else if (num < 20) {
                tmp = SayNumber(num - 10) + " belas";
            } else if (num < 100) {
                tmp = SayNumber(parseInt(num / 10)) + " puluh " + SayNumber(num % 10);
            } else if (num < 200) {
                tmp = " seratus " + SayNumber(num - 100);
            } else if (num < 1000) {
                tmp = SayNumber(parseInt(num / 100)) + " ratus " + SayNumber(num % 100);
            } else if (num < 2000) {
                tmp = " seribu " + SayNumber(num - 1000);
            } else if (num < 1000000) {
                tmp = SayNumber(parseInt(num / 1000)) + " ribu " + SayNumber(num % 1000);
            }
            return tmp;
        }
    </script>
    <div class="overlay"></div>
    <div class="row">
        <div id="divTabel" class="col-8">
            <div class="card">
                <div class="card-body table-responsive p-0">
                    <table class="table table-striped" id="dtList" width="100%">
                        <thead>
                            <tr>
                                <th></th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
        <div id="divCounter" class="col-4">
            <!-- small box -->
            <div id="divCounters" class="row">
            </div>
        </div>
        &nbsp;
    </div>
    <div class="audio">
        <audio id="s_nomorurut" src="<%= Helper.UrlRoot() %>/audio/nomor-urut.MP3"></audio> 
        <audio id="s_konter" src="<%= Helper.UrlRoot() %>/audio/konter.MP3"></audio> 
	    <audio id="s_satu" src="<%= Helper.UrlRoot() %>/audio/1.MP3"></audio> 
	    <audio id="s_dua" src="<%= Helper.UrlRoot() %>/audio/2.MP3"></audio> 
	    <audio id="s_tiga" src="<%= Helper.UrlRoot() %>/audio/3.MP3"></audio> 
	    <audio id="s_empat" src="<%= Helper.UrlRoot() %>/audio/4.MP3"></audio> 
	    <audio id="s_lima" src="<%= Helper.UrlRoot() %>/audio/5.MP3"></audio> 
	    <audio id="s_enam" src="<%= Helper.UrlRoot() %>/audio/6.MP3"></audio> 
	    <audio id="s_tujuh" src="<%= Helper.UrlRoot() %>/audio/7.MP3"></audio> 
	    <audio id="s_delapan" src="<%= Helper.UrlRoot() %>/audio/8.MP3"></audio> 
	    <audio id="s_sembilan" src="<%= Helper.UrlRoot() %>/audio/9.MP3"></audio> 
        <audio id="s_sepuluh" src="<%= Helper.UrlRoot() %>/audio/sepuluh.MP3"></audio> 
        <audio id="s_sebelas" src="<%= Helper.UrlRoot() %>/audio/sebelas.MP3"></audio>
        <audio id="s_belas" src="<%= Helper.UrlRoot() %>/audio/belas.MP3"></audio>
        <audio id="s_puluh" src="<%= Helper.UrlRoot() %>/audio/puluh.MP3"></audio>
        <audio id="s_seratus" src="<%= Helper.UrlRoot() %>/audio/seratus.MP3"></audio>
        <audio id="s_ratus" src="<%= Helper.UrlRoot() %>/audio/ratus.MP3"></audio>
        <audio id="s_seribu" src="<%= Helper.UrlRoot() %>/audio/seribu.MP3"></audio>
        <audio id="s_ribu" src="<%= Helper.UrlRoot() %>/audio/ribu.MP3"></audio>

        <audio id="s_a" src="<%= Helper.UrlRoot() %>/audio/a.MP3"></audio>
        <audio id="s_b" src="<%= Helper.UrlRoot() %>/audio/b.MP3"></audio>
        <audio id="s_c" src="<%= Helper.UrlRoot() %>/audio/c.MP3"></audio>
        <audio id="s_d" src="<%= Helper.UrlRoot() %>/audio/d.MP3"></audio>
        <audio id="s_e" src="<%= Helper.UrlRoot() %>/audio/e.MP3"></audio>
        <audio id="s_f" src="<%= Helper.UrlRoot() %>/audio/f.MP3"></audio>
        <audio id="s_g" src="<%= Helper.UrlRoot() %>/audio/g.MP3"></audio>
        <audio id="s_h" src="<%= Helper.UrlRoot() %>/audio/h.MP3"></audio>
        <audio id="s_i" src="<%= Helper.UrlRoot() %>/audio/i.MP3"></audio>
        <audio id="s_j" src="<%= Helper.UrlRoot() %>/audio/j.MP3"></audio>
        <audio id="s_k" src="<%= Helper.UrlRoot() %>/audio/k.MP3"></audio>
        <audio id="s_l" src="<%= Helper.UrlRoot() %>/audio/l.MP3"></audio>
        <audio id="s_m" src="<%= Helper.UrlRoot() %>/audio/m.MP3"></audio>
        <audio id="s_n" src="<%= Helper.UrlRoot() %>/audio/n.MP3"></audio>
        <audio id="s_o" src="<%= Helper.UrlRoot() %>/audio/o.MP3"></audio>
        <audio id="s_p" src="<%= Helper.UrlRoot() %>/audio/p.MP3"></audio>
        <audio id="s_q" src="<%= Helper.UrlRoot() %>/audio/q.MP3"></audio>
        <audio id="s_r" src="<%= Helper.UrlRoot() %>/audio/r.MP3"></audio>
        <audio id="s_s" src="<%= Helper.UrlRoot() %>/audio/s.MP3"></audio>
        <audio id="s_t" src="<%= Helper.UrlRoot() %>/audio/t.MP3"></audio>
        <audio id="s_u" src="<%= Helper.UrlRoot() %>/audio/u.MP3"></audio>
        <audio id="s_v" src="<%= Helper.UrlRoot() %>/audio/v.MP3"></audio>
        <audio id="s_w" src="<%= Helper.UrlRoot() %>/audio/w.MP3"></audio>
        <audio id="s_x" src="<%= Helper.UrlRoot() %>/audio/x.MP3"></audio>
        <audio id="s_y" src="<%= Helper.UrlRoot() %>/audio/y.MP3"></audio>
        <audio id="s_z" src="<%= Helper.UrlRoot() %>/audio/z.MP3"></audio>
    </div>
    <form id="Form1" runat="server">
    </form>
</asp:Content>
