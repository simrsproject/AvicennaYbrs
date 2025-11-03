<%@ Import Namespace="Temiang.Avicenna.Common " %>

<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterBootstrap.Master" AutoEventWireup="true"
    CodeBehind="BedMonitoringDisplayV2.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Kiosk.BedMonitoringDisplayV2" EnableTheming="False" %>

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

        .myThNum {
            width:80px !important;
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
        .rowRed {
            background-color: darkred;
            color: white;
        }
        .rowOrange {
            background-color: darkorange;
            color: black;
        }
        .rowWhite {
            background-color: floralwhite;
            color: black;
        }
    </style>

    <script language="javascript">
        var revVersion = "0.dev";
        //$ = $telerik.$;

        var dtList1;

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

            SetHtml("spanPageTitle", "<%= GetBrandName()%> Bed Information");

            dtList1 = CreateListRoom('dtList1');

            AttachFn();
        });

        function GetSuID() {
            var suid = getUrlVars()["suid"];
            if (suid == undefined) suid = "";
            return suid;
        }

        function CreateListRoom(DtTblName) {
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
                "createdRow": function (row, data, dataIndex) {
                    if (data.Kosong == 0) {
                        // full
                        $(row).addClass('rowRed');
                    } else if (data.Kosong <=5) {
                        $(row).addClass('rowOrange');
                    } else {
                        $(row).addClass('rowWhite');
                    }
                    //console.log(data);
                },
                "columns": [
                    { title: "Group", data: 'GroupPandemi', name: 'GroupPandemi', className: "myTh" },
                    { title: "Class", data: 'ClassName', name: 'ClassName', className: "myTh" },
                    { title: "Service Unit", data: 'ServiceUnitName', name: 'ServiceUnitName', className: "myTh" },
                    { title: "Capacity", data: 'Jumlah', name: 'Jumlah', className: "myTh myThNum" },
                    { title: "Occupied", data: 'Isi', name: 'Isi', className: "myTh myThNum" },
                    { title: "Male", data: 'M', name: 'M', className: "myTh myThNum" },
                    { title: "Female", data: 'F', name: 'F', className: "myTh myThNum" },
                    { title: "Ready", data: 'Kosong', name: 'Kosong', className: "myTh myThNum" }
                ],
                "ajax": {
                    "url": "<%=Helper.UrlRoot() %>/WebService/KioskQueue.asmx/BedMonitoringGetListV2",
                    "type": "POST",
                    "datatype": "json",
                    "data": function (d) {
                        d.IpAddress = '<%=Request.UserHostAddress %>';
                    },
                    "dataSrc": function (response) {
                        if (response.status == false) {
                            //ShowError(response.msg);
                            DisplayToast(response.msg, "error");
                            return [];
                        }
                        _fnGetData1IsRunning = false;
                            //console.log("_fnGetData1IsRunning done");
                        return response.data;
                    },
                    "error": function (xhr, error, code) {
                        _fnGetData1IsRunning = false;
                        //console.log("_fnGetData1IsRunning done");
                        DisplayToast(xhr.responseText, "error");
                    }
                }
            });
        }

        function AttachFn() {
            var args = [];
            var jSonFnTic = { fn: GetLastDataBed, args: args, tic: 10000 };
            _FNTOEXEC.push(jSonFnTic);

            var jSonFnAjaxCounter = { fn: GetAjaxCounter, args: args, tic: 600000 };
            _FNTOEXEC.push(jSonFnAjaxCounter);
        }

        var _fnGetData1IsRunning = false;
        function GetLastDataBed() {
            if (_fnGetData1IsRunning == true) {
                //console.log("skip");
                return;
            }
            _fnGetData1IsRunning = true;
            //console.log("_fnGetData1IsRunning call");
            dtList1.ajax.reload();
        }
    </script>
    <div class="overlay"></div>
    <div class="row">
        <div class="col-12">
            <div class="card card-primary card-outline">
                <div class="card-header">
                    <h3 class="card-title"><span class='fas fa-2x fa-bed text-primary'></span> Bed Information</h3>
                </div>
                <div class="card-body table-responsive p-0">
                    <table class="table" id="dtList1" width="100%">
                        <thead>
                            <tr>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
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
