<%@ Import Namespace="Temiang.Avicenna.Common " %>

<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterBootstrap.Master" AutoEventWireup="true"
    CodeBehind="PrescQueueDisplay.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Kiosk.PrescQueueDisplay" EnableTheming="False" %>

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
        //$ = $telerik.$;

        var dtList1;
        var dtList2;

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

            dtList1 = CreateList('dtList1', false);
            dtList2 = CreateList('dtList2', true);

            AttachFn();
        });

        function GetSuID() {
            var suid = getUrlVars()["suid"];
            if (suid == undefined) suid = "";
            return suid;
        }

        function CreateList(DtTblName, IsComplete) {
            return $('#' + DtTblName).DataTable({
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
                    { title: "No", data: 'PrescriptionNo', name: 'ItemID', className: "myTh" },
                    { title: "Status", data: 'StatusName', name: 'StatusName', className: "myTh" },
                    { title: "Duration", data: 'Duration', name: 'Duration', className: "myTh" }
                ],
                "ajax": {
                    "url": "<%=Helper.UrlRoot() %>/WebService/KioskQueue.asmx/PrescriptionGetQueue",
                    "type": "POST",
                    "datatype": "json",
                    "data": function (d) {
                        d.IpAddress = '<%=Request.UserHostAddress %>';
                        d.ServiceUnitID = GetSuID();
                        d.IsComplete = IsComplete;
                    },
                    "dataSrc": function (response) {
                        if (response.status == false) {
                            //ShowError(response.msg);
                            DisplayToast(response.msg, "error");
                            return [];
                        }
                        if (IsComplete) {
                            _fnGetData2IsRunning = false;
                            //console.log("_fnGetData2IsRunning done");
                        }
                        else {
                            _fnGetData1IsRunning = false;
                            //console.log("_fnGetData1IsRunning done");
                        }
                        return response.data;
                    },
                    "error": function (xhr, error, code) {
                        if (IsComplete) {
                            _fnGetData2IsRunning = false;
                            //console.log("_fnGetData2IsRunning done");
                        }
                        else {
                            _fnGetData1IsRunning = false;
                            //console.log("_fnGetData1IsRunning done");
                        }
                        DisplayToast(xhr.responseText, "error");
                    }
                }
            });
        }

        function AttachFn() {
            var args = [];
            var jSonFnTic = { fn: GetLastData, args: args, tic: 10000 };
            _FNTOEXEC.push(jSonFnTic);

            var jSonFnAjaxCounter = { fn: GetAjaxCounter, args: args, tic: 600000 };
            _FNTOEXEC.push(jSonFnAjaxCounter);
        }

        var _fnGetData1IsRunning = false;
        var _fnGetData2IsRunning = false;
        function GetLastData() {
            if (_fnGetData1IsRunning == true) {
                //console.log("skip");
                return;
            }
            _fnGetData1IsRunning = true;
            //console.log("_fnGetData1IsRunning call");
            dtList1.ajax.reload();

            if (_fnGetData2IsRunning == true) {
                //console.log("skip");
                return;
            }
            _fnGetData2IsRunning = true;
            //console.log("_fnGetData2IsRunning call");
            dtList2.ajax.reload();
        }
    </script>
    <div class="overlay"></div>
    <div class="row">
        <div class="col-6">
            <div class="card">
                <div class="card-body table-responsive p-0">
                    <table class="table table-striped" id="dtList1" width="100%">
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
        <div class="col-6">
            <div class="card">
                <div class="card-body table-responsive p-0">
                    <table class="table table-striped" id="dtList2" width="100%">
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
    </div>
    <form id="Form1" runat="server">
    </form>
</asp:Content>
