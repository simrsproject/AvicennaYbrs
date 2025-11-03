<%@ Import Namespace="Temiang.Avicenna.Common " %>

<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterBootstrap.Master" AutoEventWireup="true"
    CodeBehind="PrescQueueDisplay6Col.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Kiosk.PrescQueueDisplay6Col" EnableTheming="False" %>

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
            font-size: 26px;
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
        var dtList5;
        var dtList6;

        var colsDef = <%=GetPrescriptionDisplayColsDefJS() %>

        $(document).ready(function () {
            $.ajaxSetup({
                beforeSend: function (xhr) {
                    if (xhr.overrideMimeType) {
                        xhr.overrideMimeType("application/json");
                    }
                }
            });

            SetHtml("spanPageTitle", "<%= GetBrandName()%> Queuing System");

            dtList1 = CreateList('dtList1', 1);
            dtList2 = CreateList('dtList2', 2);
            dtList3 = CreateList('dtList3', 3);
            dtList4 = CreateList('dtList4', 4);
            dtList5 = CreateList('dtList5', 5);
            dtList6 = CreateList('dtList6', 6);

            AttachFn();

            $('body').removeClass("sidebar-mini");
            $('[data-widget="pushmenu"]').PushMenu('collapse');
        });

        function GetSuID() {
            var suid = getUrlVars()["suid"];
            if (suid == undefined) suid = "";
            return suid;
        }

        function CreateList(DtTblName, iProgress) {
            return $('#' + DtTblName).DataTable({
                searching: false,
                paging: false,
                info: false,
                autoWidth: true,
                order: [[1, 'desc']],
                "columnDefs": [{
                    "targets": '_all',
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).css('padding-top', '0px');
                        $(td).css('padding-bottom', '0px');
                        $(td).css('font-size', '24px');
                    }
                }],
                "columns": [
                    {
                        data: 'PrescriptionNo', name: 'PrescriptionNo', className: "myTh",
                        "render": function (data, type, row, meta) {
                            strDef = "";
                            //console.log(row);
                            for (var i = 0; i < colsDef.length; i++) {
                                let txt = row[colsDef[i]].toString();
                                //console.log(colsDef[i] + "|" + txt + "|" + txt.length);
                                if (txt.length > 0) {
                                    if (colsDef[i] == "Flag") {
                                        strDef = strDef + " (" + txt + ")";
                                    } else {
                                        strDef = strDef + ((strDef.length == 0) ? "" : " - ");
                                        strDef = strDef + txt;
                                    }
                                }
                            }
                            return "<span>" + strDef + "</span>";
                            //return "<span>" + row.MedicalNo + " - " + row.PrescriptionNo +"</span>";
                        }
                    },
                    { data: 'Duration', name: 'Duration', className: "myTh" }
                ],
                "ajax": {
                    "url": "<%=Helper.UrlRoot() %>/WebService/KioskQueue.asmx/PrescriptionGetQueue6Col",
                    "type": "POST",
                    "datatype": "json",
                    "data": function (d) {
                        d.IpAddress = '<%=Request.UserHostAddress %>'
                        d.ServiceUnitID = GetSuID();
                        d.iProgress = iProgress;
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
            var jSonFnTic = { fn: GetLastData, args: args, tic: 3000 };
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
                }
                case 2: {
                    dtList3.ajax.reload();
                    curIdx = 3;
                    break;
                }
                case 3: {
                    dtList4.ajax.reload();
                    curIdx = 4;
                    break;
                }
                case 4: {
                    dtList5.ajax.reload();
                    curIdx = 5;
                    break;
                }
                case 5: {
                    dtList6.ajax.reload();
                    curIdx = 0;
                    break;
                }
            }
        }
    </script>
    <div class="overlay"></div>
    <div class="row">
        <div class="col-6">
            <div class="card card-outline card-danger">
                <div class="card-header">
                    <h3 class="card-title">RACIKAN</h3>
                </div>
                <div class="card-body table-responsive p-1">
                    <div class="row">
                        <div class="col-4">
                            <table class="table table-striped" id="dtList1" width="100%">
                                <thead>
                                    <tr>
                                        <th>ORDER</th>
                                        <th></th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <div class="col-4">
                           <table class="table table-striped" id="dtList2" width="100%">
                                <thead>
                                    <tr>
                                        <th>PROSES</th>
                                        <th></th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <div class="col-4">
                            <table class="table table-striped" id="dtList3" width="100%">
                                <thead>
                                    <tr>
                                        <th>SELESAI</th>
                                        <th></th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-6">
            <div class="card card-outline card-primary">
                <div class="card-header">
                    <h3 class="card-title">NON RACIKAN</h3>
                </div>
                <div class="card-body table-responsive p-1">
                    <div class="row">
                        <div class="col-4">
                            <table class="table table-striped" id="dtList4" width="100%">
                                <thead>
                                    <tr>
                                        <th>ORDER</th>
                                        <th></th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <div class="col-4">
                           <table class="table table-striped" id="dtList5" width="100%">
                                <thead>
                                    <tr>
                                        <th>PROSES</th>
                                        <th></th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <div class="col-4">
                            <table class="table table-striped" id="dtList6" width="100%">
                                <thead>
                                    <tr>
                                        <th>SELESAI</th>
                                        <th></th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <form id="Form1" runat="server">
    </form>
</asp:Content>
