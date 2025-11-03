<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustomBootstrap.Master" AutoEventWireup="true"
    Codebehind="NonPatientCustomerChargesList.aspx.cs" 
    Inherits="Temiang.Avicenna.Module.Charges.Mobile.NonPatientCustomerChargesList" EnableTheming="False"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="<%=GetBasePath() %>/Bootstrap/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <!-- Page level plugin JavaScript-->
    <script src="<%=GetBasePath() %>/Bootstrap/plugins/datatables/jquery.dataTables.js"></script>
    <script src="<%=GetBasePath() %>/Bootstrap/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>

    <style>
        .dataTables_scrollHeadInner{
          width:100% !important;
          padding: 0 !important;
        }
        .dataTables_scrollHeadInner table{
          width:100% !important;
          padding: 0 !important;
        }
    </style>
    

    <script language="javascript">   
        var revVersion = "2.xyz2";
        var dtTable;
        var dtDetail = null;
        var _regNo;
        var curDate = new Date();
        var bPendingOnly = false;
        var RegNos = [];
    
        $(document).ready(function() {
            //alert(BaseURL + "/WebService/jQueryWS.asmx/NPCGetList");
            
            $.ajaxSetup({
                beforeSend: function (xhr) {
                    if (xhr.overrideMimeType) {
                        xhr.overrideMimeType("application/json");
                    }
                }
            });

            dtTable = $('#dtList').DataTable({
                "processing": true,
                "serverSide": true,
                "scrollX": true,
                "columnDefs": [{
                    "targets": '_all',
                    "createdCell": function (td, cellData, rowData, row, col) {
                        $(td).css('padding-top', '0px');
                        $(td).css('padding-bottom', '0px');
                    }
                }],
                "ajax": {
                    "url": "<%=GetBasePath() %>/WebService/jQueryWS.asmx/NPCGetRegList",
                    "type": "POST",
                    "datatype": "json",
                    "data": function(d){
                        d.ServiceUnitID = $("#"+ '<%= hfServiceUnitID.ClientID %>').val(),
                        d.CurDate = curDate.getFullYear() + "-" + (curDate.getMonth() + 1) + "-" + curDate.getDate(),
                        d.PendingOnly = (bPendingOnly == false ? "0":"1")
                    }
                },
                "columns": [
                    { title: "Registration No", data: 'RegistrationNo', name: 'RegistrationNo', "width": "160px" },
                    { data: 'RegistrationDate', name: 'RegistrationDate', "width": "100px", "visible": false },
                    { data: 'TableNo', name: 'TableNo', "width": "50px" },
                    { data: 'CustomerName', name: 'CustomerName', "autoWidth": true , "render": function( data, type, row, meta ){
                        return row.CustomerName + "<span id=\""+RegNoReplace(row.RegistrationNo)+"\" customtag=\"reg\"></span>";
                    }},
                    { data: 'ServiceUnitName', name: 'ServiceUnitName', "visible": false },
                    { data: 'RegistrationNo', "width": "40px", "sortable": false, "render": function ( data, type, row, meta ){
                        return "<button type=\"button\" class=\"btn btn-outline-primary btn-sm\" onclick=\"javascript:ShowDetail('"+data+"')\" style=\"width:40px\"><span class='fa fa-lg fa-pen'></span></button>";
                      } 
                    }
                ],
                "order": [[0, "desc"]],
                "fnDrawCallback": function( oSettings ) {
                    RegNos = [];
                    GetMainNotif();
                }
            });

            //$('#dtList').on('dblclick', 'tr', function (event) {
            //    var RegNo = dtTable.row(this).data()["RegistrationNo"];
            //    ShowDetail(RegNo);
            //});
            
            $("#dtList").on("click", 'tr', function (event) {
                if (IsDoubleClick()) {
                    var RegNo = dtTable.row(this).data()["RegistrationNo"];
                    ShowDetail(RegNo);
                }
            });

            ShowListWithReload(false);

            var args = [];
            var jSonFnTic = {fn: GetMainNotif, args: args, tic: 8000};
            _FNTOEXEC.push(jSonFnTic);
            
            var argsSession = [];
            var jSonFnTicSession = {fn: NotifGetSession, args: argsSession, tic: 30000};
            _FNTOEXEC.push(jSonFnTicSession);
            
            UpdateDate();
        });
        
        var _aElements = ["divList","divDetail","divEntry"];

        function ShowListPending() {
            CoreToggleShowHide(_aElements, 0);
            bPendingOnly = true;
            dtTable.ajax.reload();
        }

        function ShowList() {
            bPendingOnly = false;
            ShowListWithReload(true);
        }
        
        function ShowListWithReload(IsReload) {
            CoreToggleShowHide(_aElements, 0);
            if (IsReload) {
                curDate = new Date();
                dtTable.ajax.reload();
                UpdateDate();
            }
        }

        function ShowPrev() {
            curDate.setDate(curDate.getDate() - 1);
            dtTable.ajax.reload();
            UpdateDate();
        }
        function ShowNext() {
            curDate.setDate(curDate.getDate() + 1);
            dtTable.ajax.reload();
            UpdateDate();
        }
        
        function UpdateDate(){
            $("#spanCurrentDate").html(" [" +curDate.getDate() + "-" + (curDate.getMonth() + 1) + "-" + curDate.getFullYear() + "]");
        }
        
        function BackToDetail(){
            ShowDetail(_regNo);
        }

        function ShowDetail(RegistrationNo) {
            _regNo = RegistrationNo;
            
            var SUID = $("#"+ '<%= hfServiceUnitID.ClientID %>').val();
            if(CoreIsNullOrEmpty(SUID)){
                ShowError("Please select service unit");
                return 0;
            }
        
            CoreToggleShowHideWithFn(_aElements, 1, function(fn){
                //var url = BaseURL + '/Module/Charges/ServiceUnit/NonPatientCustomerCharges/Mobile/Editor.html';
                var url = 'Editor.html?v=' + revVersion;
                // mark datatable to be reinitialize due to editor page is reloaded
                dtDetail = null;
                
                CoreLoadContentToContainerDiv(url, _aElements[1], 'GET', function() {
                    // load data
                    $.ajax({
                        type: 'POST',
                        url: "<%=GetBasePath() %>/WebService/jQueryWS.asmx/NPCGetReg?regNo=" + _regNo,
                        success: function (data) {
                            // by default editor is read only
                            // to avoid showing keyboard on touch device
                            $(':text').prop('readonly', false);
							$(':text').blur();
							
                            if(CoreIsNullOrEmpty(_regNo)){
                                $('#txtTableNo').focus();
                            }
                            
                            // do something
                            if (data.status == _sJsonRequestOkStatus) {
                                $('#txtTableNo').val(data.data.DischargeMedicalNotes);
                                $('#txtCustomerName').val(data.data.DischargeNotes);
                                
                                ShowDetailList();
                                CloseProgress();
                            } else {
                                ShowError(data.data);
                            }
                            
                            if(CoreIsNullOrEmpty(_regNo)){
                                // disable button add new and approve
                                $("#btnShowMenu").prop('disabled', true);
                                $("#btnApprove").prop('disabled', true);
                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            ShowError(xhr.responseText);
                        },
                        dataType: 'json'
                    });
                    
                    if(CoreIsNullOrEmpty($('#txtTableNo').val()))
						$('#txtTableNo').focus();
                    
                    fn();
                });
            });
            
            return 0;
        }
        
        function SaveReg(){
            ShowProgress();
                
            var sSerialized = coreReplaceSerialize($('#formEd').serialize());
                        
            $.ajax({
                type: 'POST',
                url: "<%=GetBasePath() %>/WebService/jQueryWS.asmx/NPCSaveReg?serviceUnitID=" + 
                    $("#"+ '<%= hfServiceUnitID.ClientID %>').val() +
                    "&regNo=" + _regNo + "&" + sSerialized,
                success: function (data) {
                    // do something
                    if (data.status == _sJsonRequestOkStatus) {
                        _regNo = data.data;
                        
                        $("#divDtDetail").show();
                        ShowDetailList();
                        CloseProgress();
                        
                        $("#btnShowMenu").prop('disabled', false);
                        $("#btnApprove").prop('disabled', false);
                    } else {
                        ShowError(data.data);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    ShowError(xhr.responseText);
                },
                dataType: 'json'
            });
        }
        
        function ShowDetailList(){
            // hide detail list if noreg not saved yet
            if(CoreIsNullOrEmpty(_regNo)){
                // hide
                $("#divDtDetail").hide();
            }
        
            //alert(dtDetail);
            if(dtDetail == null){
                dtDetail = $('#dtDetail').DataTable({
                    "processing": true,
                    "serverSide": true,
                    "scrollX": true,
                    "columnDefs": [{
                        "targets": '_all',
                        "createdCell": function (td, cellData, rowData, row, col) {
                            $(td).css('padding-top', '0px');
                            $(td).css('padding-bottom', '0px');
                        }
                    }],
                    "ajax": {
                        "url": BaseURL + "/WebService/jQueryWS.asmx/NPCGetTransList",
                        "type": "POST",
                        "datatype": "json",
                        "data": function(d){
                            d.RegistrationNo = _regNo;
                        }
                    },
                    "columns": [
                        { title: 'Item ID', data: 'ItemID', name: 'ItemID', "width": "80px" },
                        { title: 'Item Name', data: 'ItemName', name: 'ItemName', "autoWidth": true, 
                        "render": function (data, type, row, meta) {
                            var itemName = (row.IsOrderRealization) ? " <a href=\"\" onclick=\"javascript:ConfirmUndoDelivered('" + row.TransactionNo + "', '" + row.SequenceNo + "');return false;\"><span>&#10003;&#10003;</span></a>" :
                                " <a href=\"\" onclick=\"javascript:RemoveItem(this, '" + row.ItemID + "', " + -row.Qty + ");return false;\"><span class='fa fa-lg fa-times text-danger'></span></a>";
                            itemName = (!row.IsOrderRealization && row.IsApprove) ? " <a href=\"\" onclick=\"javascript:SetDelivered('" + row.TransactionNo + "', '" + row.SequenceNo + "', '1');return false;\"><span>&#10003;</span></a>" : itemName;
                            itemName = " " + row.Notes + itemName;
                              return "<a href=\"\" onclick=\"javascript:ShowFormCustomMenu('" + row.TransactionNo + "', '" + row.SequenceNo + "');return false;\">" +
                                  row.ItemName + "</a>" + itemName;
                        }},
                        { title: 'Qty', data: 'Qty', name: 'Qty', "width": "50px", "sortable": false },
                        { title: 'Amount', data: 'ItemID', "width": "80px", "sortable": false, className: "dt-right",
                          "render": function ( data, type, row, meta ){
                            return $.fn.dataTable.render.number(',', '.', 2, '').display(row.Qty * row.Price);
                          } 
                        },
                        { data: 'TransactionNo', name: 'TransactionNo', "visible": false },
                        { data: 'SequenceNo', name: 'SequenceNo', "visible": false }
                    ]
                });

                //$('#dtDetail').on('dblclick', 'tr', function(event) {
                //    var tn = dtDetail.row(this).data()["TransactionNo"];
                //    var sn = dtDetail.row(this).data()["SequenceNo"];
                //    ShowFormCustomMenu(tn, sn);
                //});

                $("#dtDetail").on("click", 'tr', function (event) {
                    if (IsDoubleClick()) {
                        var tn = dtDetail.row(this).data()["TransactionNo"];
                        var sn = dtDetail.row(this).data()["SequenceNo"];
                        ShowFormCustomMenu(tn, sn);
                    }
                });

            }else{
                dtDetail.ajax.reload();
            }

            UpdateMenuPrint();
        }

        function RemoveItem(Sender, ItemID, add) {
            CoreFnConfirm("Are you sure want to remove?", function () {
                DoRemoveItem(Sender, ItemID, add);
            })
            //return false;
        }
        function DoRemoveItem(Sender, ItemID, add) {
            // close confirm dialog
            _divModal.modal('hide');

            AddItem(Sender, ItemID, add, function () {
                dtDetail.ajax.reload();
            });
        }

        function UpdateMenuPrint() {
            $.ajax({
                type: 'POST',
                url: "<%=GetBasePath() %>/WebService/jQueryWS.asmx/NPCGetTransNoList?RegistrationNo=" + _regNo,
                success: function (data) {
                    // do something
                    if (data.status == _sJsonRequestOkStatus) {
                        // update print menu here
                        var ddContainer = $("#divBtnPrint").find(".dropdown-menu");
                        ddContainer.html("");
                        if (data.data.length > 0) {
                            $("#btnPrintDD").prop('disabled', false);
                            for (var i = 0, len = data.data.length; i < len; i++) {
                                ddContainer.append("<a class=\"dropdown-item\" href=\"#\" onclick=\"javascript:PrintOrderMenu('" + data.data[i] + "')\"><span class='fa fa-print'></span> " + data.data[i] + "</a>");
                            }
                        } else {
                            $("#btnPrintDD").prop('disabled', true);
                        }
                        //console.log(data.data);
                    } else {
                        ShowError(data.data);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    ShowError(xhr.responseText);
                },
                dataType: 'json'
            });
        }

        function PrintOrderMenu(TransactionNo) {
            
            $.ajax({
                type: 'POST',
                url: "<%=GetBasePath() %>/WebService/jQueryWS.asmx/NPCPrintMenuOrder",
                data: {
                    TransactionNo: TransactionNo
                },
                success: function (data) {
                    // do something
                    if (data.status == _sJsonRequestOkStatus) {
                        ShowInfo("Print command has been sent");
                    } else {
                        ShowError(data.data);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    ShowError(xhr.responseText);
                },
                dataType: 'json'
            });
        }

        function ShowFormCustomMenu(tn, sn) {
            // show editor form
            var header = "<h3><span class='fa fa-lg fa-edit'></span> Custom Order</h3>";
            var body = "<div class='row'>" +
                "<div class='col-sm-12'>" +
                "<div class='form-group'>" +
                "<textarea class='form-control' rows='3' id='txtCustomOrder' name='txtCustomOrder' placeholder='Enter custom order'></textarea>" +
                "</div>" +
                "</div></div>";
            var footer = "<a href='' id='lnSubmit' class='btn text-success'><span class='fa fa-lg fa-check'></span> Submit</a>";

            // load custom menu
            $.ajax({
                type: 'POST',
                url: "<%=GetBasePath() %>/WebService/jQueryWS.asmx/NPCGetCustomMenu",
                data: {
                    TransactionNo: tn, SequenceNo: sn
                },
                success: function (data) {
                    // do something
                    if (data.status == _sJsonRequestOkStatus) {
                        $("#txtCustomOrder").val(data.data);
                    } else {
                        ShowError(data.data);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    ShowError(xhr.responseText);
                },
                dataType: 'json'
            });

            //CoreCustomDialogWithCallback(header, body, footer, CustomMenu, tn, sn);
            CoreFnCustomDialog(header, body, footer, function () {
                CustomMenu(tn, sn);
            });
        }

        function CustomMenu(TransactionNo, SequenceNo) {
            $("#lnSubmit").click(function () {

                $("#lnSubmit").find('i').addClass("fa-spin");
                $("#lnSubmit").prop('disabled', true);

                var custMsg = $("#txtCustomOrder").val();
                $.ajax({
                    type: 'POST',
                    url: "<%=GetBasePath() %>/WebService/jQueryWS.asmx/NPCSetCustomMenu",
                    data: {
                        TransactionNo: TransactionNo, SequenceNo: SequenceNo, AdditionalMessage: custMsg
                    },
                    success: function (data) {
                        // do something
                        if (data.status == _sJsonRequestOkStatus) {
                            _divModal.modal('hide');
                            ShowDetailList();
                        } else {
                            ShowError(data.data);
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        ShowError(xhr.responseText);
                    },
                    dataType: 'json'
                });
                
                return false;
            });
        }

        function ConfirmUndoDelivered(TransactionNo, SequenceNo) {
            CoreFnConfirm("Are you sure want to undo delivery?", function () {
                DoConfirmUndoDelivered(TransactionNo, SequenceNo);
            });
        }

        function DoConfirmUndoDelivered(TransactionNo, SequenceNo) {
            var oSender = _divModal.find('#btnConfirm');

            _divModal.modal('hide');

            SetDelivered(TransactionNo, SequenceNo, "0");
        }

        function SetDelivered(TransactionNo, SequenceNo, status /*string 0 or 1*/) {
            $.ajax({
                type: 'POST',
                url: "<%=GetBasePath() %>/WebService/jQueryWS.asmx/NPCSetDelivered",
                data: {
                    TransactionNo: TransactionNo, SequenceNo: SequenceNo, status
                },
                success: function (data) {
                    // do something
                    if (data.status == _sJsonRequestOkStatus) {
                        ShowDetailList();
                    } else {
                        ShowError(data.data);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    ShowError(xhr.responseText);
                },
                dataType: 'json'
            });
        }
        
        function ShowListMenu(IsReload) {
            CoreToggleShowHideWithFn(_aElements, 2, function(fn){
                var url = 'ItemList.html?v=' + revVersion;
                CoreLoadContentToContainerDiv(url, _aElements[2], 'GET', function() {
                    
                    //
                    dtItemList = $('#dtItemList').DataTable({
                        "processing": true,
                        "serverSide": true,
                        "scrollX": true,
                        "columnDefs": [{
                            "targets": '_all',
                            "createdCell": function (td, cellData, rowData, row, col) {
                                $(td).css('padding-top', '0px');
                                $(td).css('padding-bottom', '0px');
                            }
                        }],
                        "ajax": {
                            "url": BaseURL + "/WebService/jQueryWS.asmx/NPCGetItemList",
                            "type": "POST",
                            "datatype": "json",
                            "data": function(d){
                                d.RegistrationNo = _regNo;
                                d.serviceUnitID = $("#"+ '<%= hfServiceUnitID.ClientID %>').val();
                            }
                        },
                        "columns": [
                            { title: 'Item ID', data: 'ItemID', name: 'ItemID', "width": "80px" },
                            { title: 'Item Name', data: 'ItemName', name: 'ItemName', "autoWidth": true },
                            { title: 'Balance', data: 'Balance', name: 'Balance', "width": "50px", "sortable": false },
                            { title: 'Unit', data: 'ItemUnit', name: 'ItemUnit', "width": "50px", "sortable": false },
                            { title: 'Qty', data: 'ItemID', "width": "30px", "sortable": false, "render": function ( data, type, row, meta ){
                                // return "<input type=\"text\" class=\"form-control\" id=\"txtQtyOrder"+data+"\" name=\"txtQtyOrder"+data+"\" readonly />";
                                return "<span id=\"lblQtyOrder" + data + "\">" + row.ChargeQuantity + "</span>";
                              } 
                            },
                            { data: 'ItemID', "width": "90px", "sortable": false, "render": function ( data, type, row, meta ){
                                var btnDown = "<button type=\"button\" class=\"btn btn-outline-danger btn-sm\" onclick=\"javascript:AddItem(this, '"+data+"', -1)\" style=\"width:40px\"><span class='fa fa-lg fa-minus'></span></button>";
                                var btnUp = "<button type=\"button\" class=\"btn btn-outline-success btn-sm\" onclick=\"javascript:AddItem(this, '"+data+"', 1)\" style=\"width:40px\"><span class='fa fa-lg fa-plus'></span></button>";
                                return "<div class='row'>" + btnDown + "&nbsp;" + btnUp + "</div>";
                              } 
                            }
                        ]
                    });
                
                    fn();
                });
            });
            
            return 0;
        }
        
        function AddItem(Sender, ItemID, add, callbackfn){
            var oSender = $(Sender);
            oSender.find('i').addClass("fa-spin");
            oSender.prop('disabled', true);

            $.ajax({
                type: 'POST',
                url: BaseURL + "/WebService/jQueryWS.asmx/NPCAddItem",
                data: {
                    RegistrationNo: _regNo,
                    ServiceUnitID: $("#"+ '<%= hfServiceUnitID.ClientID %>').val(),
                    ItemID: ItemID,
                    Add: add
                },
                success: function (data) {
                    // do something
                    if (data.status == _sJsonRequestOkStatus) {
                        //console.log(data);
                        //console.log(ItemID);
                        //$("#txtQtyOrder" + ItemID).val(data.data);
                        $("#lblQtyOrder" + ItemID.replace(".","\\.")).html(data.data);
                        oSender.find('i').removeClass("fa-spin");
                        oSender.prop('disabled', false);

                        if (!CoreIsNull(callbackfn)) {
                            callbackfn();
                        }
                    } else {
                        oSender.find('i').removeClass("fa-spin");
                        oSender.prop('disabled', false);
                        ShowError(data.data);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    oSender.find('i').removeClass("fa-spin");
                    oSender.prop('disabled', false);
                    ShowError(xhr.responseText);
                },
                dataType: 'json'
            });

            return false;
        }
        
        function Approve(Sender){
            CoreFnConfirm("Are you sure want to approve?", function () {
                DoApprove();
            });
        }
        
        function DoApprove(){
            var oSender = _divModal.find("#btnConfirm");
            oSender.find('i').addClass("fa-spin");
            oSender.prop('disabled', true);
            
            $.ajax({
                type: 'POST',
                url: BaseURL + "/WebService/jQueryWS.asmx/NPCApprove",
                data: {
                    RegistrationNo: _regNo
                },
                success: function (data) {
                    // do something
                    if (data.status == _sJsonRequestOkStatus) {
                        oSender.find('i').removeClass("fa-spin");
                        oSender.prop('disabled', false);

                        _divModal.modal('hide');
                        
                        ShowDetailList();
                    } else {
                        oSender.find('i').removeClass("fa-spin");
                        oSender.prop('disabled', false);
                        ShowError(data.data);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    oSender.find('i').removeClass("fa-spin");
                    oSender.prop('disabled', false);
                    ShowError(xhr.responseText);
                },
                dataType: 'json'
            });
        }

        // function untuk update notifikasi
        var _bFnGetNotifIsRunning = false;
        var counter = 0;
        function GetMainNotif(){
            // we have to suspend another request before prev request is completed vrohh..!!
            if (_bFnGetNotifIsRunning)
                return;
            _bFnGetNotifIsRunning = true;

            //console.log("1");

            $.ajax({
                type: 'POST',
                url: BaseURL + "/WebService/jQueryWS.asmx/NPCGetMainNotif",
                data: {
                    ServiceUnitID: $("#" + '<%= hfServiceUnitID.ClientID %>').val(),
                    CurDate : curDate.getFullYear() + "-" + (curDate.getMonth() + 1) + "-" + curDate.getDate()
                },
                success: function (data) {
                    counter++;
                    // do something
                    if (data.status == _sJsonRequestOkStatus) {
                        var isDanger = false;
                        // set all exist false
                        for(var i = 0; i < RegNos.length; i++){
                            RegNos[i].IsExist = false;
                            RegNos[i].IsNewState = false;
                        }
                        
                        //console.log(data.data.length);
                        for (var i = 0; i < data.data.length; i++) {
                            var idx = -1;
                            //console.log(i);
                            for(var j = 0; j < RegNos.length; j++){
                                if(RegNos[j].RegistrationNo == data.data[i].RegistrationNo){
                                    idx = j;
                                    break;
                                }
                            }
                            //console.log(data.data[i].RegistrationNo);
                            if(idx >= 0){
                                RegNos[idx].IsNewState = RegNos[idx].IsDanger != (data.data[i].sTimeLimit == "bigger than");
                                RegNos[idx].IsDanger = (data.data[i].sTimeLimit == "bigger than");
                                RegNos[idx].IsExist = true;
                                //console.log("IsExist " + RegNos[idx].RegistrationNo);
                            }else{
                                RegNos.push({
                                    RegistrationNo: data.data[i].RegistrationNo, 
                                    IsDanger: (data.data[i].sTimeLimit == "bigger than"),
                                    IsExist: true,
                                    IsNewState: true });
                                idx = RegNos.length - 1;
                                //console.log("New " + RegNos[idx].RegistrationNo);
                            }
                            
                            if(data.data[i].sTimeLimit == "bigger than") isDanger = true;
                            
                            if(idx >= 0){
                                var img = "";
                                var x = $("#" + RegNoReplace(RegNos[idx].RegistrationNo));
                                
                                //console.log((RegNos[idx].IsNewState ? "NewState":"State") + " " + RegNos[idx].RegistrationNo);
                                
                                if(RegNos[idx].IsNewState){
                                    var existing = x.find('img');
                                    if(existing.length){
                                        //existing.slideUp();
                                        existing.remove();
                                    }
                                    if(RegNos[idx].IsDanger){
                                        img = "<img src='"+BaseURL+"/Bootstrap/adminlte/img/icons/angry32.png' />";
                                    }else{
                                        img = "<img src='"+BaseURL+"/Bootstrap/adminlte/img/icons/smile32.png' />";
                                    }
                                    
                                    var objImg = $(img);
                                    if(x.length){
                                        //setTimeout(function (y) {
                                        //    console.log(y);
                                        //    $("#" + y).append(objImg).find('img').hide().slideDown();
                                        //}, (200 * i),RegNoReplace(RegNos[idx].RegistrationNo));
                                        x.append(objImg).find('img').hide().slideDown();
                                    } 
                                }
                            }   
                        }
                        
                        var allRegInGrid = $("[customtag='reg']");
                        
                        // remove not exist
                        for(var i = RegNos.length -1; i >= 0 ; i--){
                            if(!RegNos[i].IsExist){
                                allRegInGrid.each(function(){
                                    //console.log($(this).attr('id'));
                                    var isExist = false;
                                    for (var j = 0; j < RegNos.length; j++) {
                                        if(RegNos[j].IsExist) continue;
                                        
                                        if(RegNoReplace(RegNos[i].RegistrationNo) == $(this).attr('id')){
                                            var x = $(this).find("img");
                                            x.slideUp();
                                            break;
                                        }
                                    }
                                });
                            
                                RegNos.splice(i, 1);
                            }
                        }
                        
                        
                        var spanNotif = $("#spanNotif");
                        spanNotif.removeClass("badge-warning");
                        spanNotif.removeClass("badge-danger");
                        if (RegNos.length > 0) {
                            if (isDanger) {
                                spanNotif.addClass("badge-danger");
                            } else {
                                spanNotif.addClass("badge-warning");
                            }
                            spanNotif.html(data.data.length);
                        } else {
                            spanNotif.html('');
                        }
                        _bFnGetNotifIsRunning = false;
                    } else {
                        ShowError(data.data);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    ShowError(xhr.responseText);
                },
                dataType: 'json'
            });
        }
        
        var _bFnNotifGetSession = false;
        function NotifGetSession(){
            // we have to suspend another request before prev request is completed vrohh..!!
            if (_bFnNotifGetSession)
                return;
            _bFnNotifGetSession = true;

            //console.log("1");

            $.ajax({
                type: 'POST',
                url: BaseURL + "/WebService/jQueryWS.asmx/NPCNotifGetSession",
                success: function (data) {
                    // do something
                    if (data.status == _sJsonRequestOkStatus) {
                        _bFnNotifGetSession = false;
                    } else {
                        var dlg = ShowError(data.data);
                        dlg.find('.modal-footer').find('a').click(function(){
                            backToMainApp();
                        });
                        _bFnNotifGetSession = false;
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    ShowError(xhr.responseText);
                    _bFnNotifGetSession = false;
                },
                dataType: 'json'
            });
        }
    </script>
    <div class="col-12">
          <div id="divList" class="card">
            <div class="card-header">
                <div class="row">
                    <div class="col-sm-6">
                        <h3 class="card-title"><span class='fa fa-lg fa-book'></span> Registration List <span id="spanCurrentDate"></span></h3>
                    </div>
                    <div class="col-sm-6">
                        <div class="row float-right">
                            <button type="button" class="btn btn-outline-success btn-lg" 
                            onclick="javascript:ShowDetail('');"><span class='fa fa-plus'></span></button> &nbsp;
                            <button type="button" class="btn btn-outline-primary btn-lg" 
                            onclick="javascript:ShowPrev();"><span class='fa fa-angle-left'></span></button> &nbsp;
                            <button type="button" class="btn btn-outline-primary btn-lg" 
                            onclick="javascript:ShowList();"><span class='fa fa-sync'></span></button> &nbsp;
                            <button type="button" class="btn btn-outline-primary btn-lg" 
                            onclick="javascript:ShowNext();"><span class='fa fa-angle-right'></span></button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="row">
                    <form id="Form1" runat="server">
                        <asp:HiddenField ID="hfServiceUnitID" runat="server" />
                    </form>
                    <table class="table table-bordered" id="dtList" width="100%" cellspacing="0">
                        <thead>
                            <tr>
                                <th>Registration No</th>
                                <th>Registration Date</th>
                                <th>T.No</th>
                                <th>Customer Name</th>
                                <th>Service Unit</th>
                                <th>&nbsp;</th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
          </div>
          <div id="divDetail"></div>
          <div id="divEntry"></div>
    </div>   
</asp:Content>
