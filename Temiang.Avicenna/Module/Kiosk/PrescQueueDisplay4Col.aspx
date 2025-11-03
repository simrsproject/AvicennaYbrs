<%@ Import Namespace="Temiang.Avicenna.Common " %>

<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterBootstrap.Master" AutoEventWireup="true"
    CodeBehind="PrescQueueDisplay4Col.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Kiosk.PrescQueueDisplay4Col" EnableTheming="False" %>

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
            font-size: 18px;
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
        //$ = $telerik.$;

        var dtList1;
        var dtList2;
        var dtList3;
        var dtList4;

        $(document).ready(function () {
            $.ajaxSetup({
                beforeSend: function (xhr) {
                    if (xhr.overrideMimeType) {
                        xhr.overrideMimeType("application/json");
                    }
                }
            });

            SetHtml("spanPageTitle", "<%= GetBrandName()%> Queuing System");

            var qcodes = getUrlVars()["code"];
            if (CoreIsNullOrEmpty(qcodes)) {
                dtList1 = CreateList('dtList1', "DA");
                dtList2 = CreateList('dtList2', "DB");
                dtList3 = CreateList('dtList3', "DC");
                dtList4 = CreateList('dtList4', "DD");
            } else {
                var qcode = qcodes.split(",");
                console.log((CoreIsNullOrEmpty(qcode[0]) ? "xx" : qcode[0]));
                dtList1 = CreateList('dtList1', (CoreIsNullOrEmpty(qcode[0]) ? "xx" : qcode[0]));
                dtList2 = CreateList('dtList2', (CoreIsNullOrEmpty(qcode[1]) ? "xx" : qcode[1]));
                dtList3 = CreateList('dtList3', (CoreIsNullOrEmpty(qcode[2]) ? "xx" : qcode[2]));
                dtList4 = CreateList('dtList4', (CoreIsNullOrEmpty(qcode[3]) ? "xx" : qcode[3]));
            }

            

            AttachFn();

            $('body').removeClass("sidebar-mini");
            $('[data-widget="pushmenu"]').PushMenu('collapse');
        });

        function GetSuID() {
            var suid = getUrlVars()["suid"];
            if (suid == undefined) suid = "";
            return suid;
        }

        function CreateList(DtTblName, QueueCode) {
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
                        $(td).css('font-size', '18px');
                    }
                }],
                "columns": [
                    { title: "No", data: 'PrescriptionNo', name: 'ItemID', className: "myTh" },
                    { title: "Status", data: 'StatusName', name: 'StatusName', className: "myTh" },
                    { title: "Dur", data: 'Duration', name: 'Duration', className: "myTh" }
                ],
                "ajax": {
                    "url": "<%=Helper.UrlRoot() %>/WebService/KioskQueue.asmx/PrescriptionGetQueueByCode",
                    "type": "POST",
                    "datatype": "json",
                    "data": function (d) {
                        d.IpAddress = '<%=Request.UserHostAddress %>'
                        d.ServiceUnitID = GetSuID();
                        d.QueueCode = QueueCode;
                    },
                    "dataSrc": function (response) {
                        if (response.status == false) {
                            //ShowError(response.msg);
                            DisplayToast(response.msg, "error");
                            return [];
                        }
                        _fnGetDataIsRunning = false;
                        return response.data;
                    },
                    "error": function (xhr, error, code) {
                        _fnGetDataIsRunning = false;
                        DisplayToast(xhr.responseText, "error");
                    }
                }
            });
        }

        function AttachFn() {
            var args = [];
            var jSonFnTic = { fn: GetLastData, args: args, tic: 4000 };
            _FNTOEXEC.push(jSonFnTic);

            var jSonFnAjaxCounter = { fn: GetAjaxCounter, args: args, tic: 600000 };
            _FNTOEXEC.push(jSonFnAjaxCounter);
        }

        var _fnGetDataIsRunning = false;
        var curIdx = 0;
        function GetLastData() {
            //console.log(curIdx);
            if (_fnGetDataIsRunning == true) {
                //console.log("skip");
                return;
            }
            _fnGetDataIsRunning = true;
            //console.log("_fnGetData1IsRunning call");
            switch (curIdx) {
                case 0: {
                    dtList1.ajax.reload();
                    curIdx = 1;
                    break;
                }
                case 1: {
                    dtList2.ajax.reload();
                    curIdx = 2;
                    break;
                } case 2: {
                    dtList3.ajax.reload();
                    curIdx = 3;
                    break;
                } case 3: {
                    dtList4.ajax.reload();
                    curIdx = 0;
                    break;
                }
            }
        }
    </script>
    <div class="overlay"></div>
    <div class="row">
        <div class="col-3">
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
        <div class="col-3">
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
        <div class="col-3">
            <div class="card">
                <div class="card-body table-responsive p-0">
                    <table class="table table-striped" id="dtList3" width="100%">
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
        <div class="col-3">
            <div class="card">
                <div class="card-body table-responsive p-0">
                    <table class="table table-striped" id="dtList4" width="100%">
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
