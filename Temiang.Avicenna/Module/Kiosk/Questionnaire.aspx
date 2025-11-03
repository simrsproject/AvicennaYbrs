<%@ Import Namespace="Temiang.Avicenna.Common " %>
<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterBootstrap.Master" AutoEventWireup="true"
    Codebehind="Questionnaire.aspx.cs" 
    Inherits="Temiang.Avicenna.Module.Kiosk.Questionnaire" EnableTheming="False"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="<%=Helper.UrlRoot() %>/Bootstrap/plugins/flags/css/flags.css" rel="stylesheet" />
    <link href="<%=Helper.UrlRoot() %>/Bootstrap/plugins/jQuery-Validation-Engine/validationEngine.jquery.css" rel="stylesheet" />
    <link href="<%=Helper.UrlRoot() %>/Bootstrap/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="<%=Helper.UrlRoot() %>/Bootstrap/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <script src="<%=Helper.UrlRoot() %>/Bootstrap/plugins/jQuery-Validation-Engine/jquery.validationEngine.min.js"></script>
    <script src="<%=Helper.UrlRoot() %>/Bootstrap/plugins/jQuery-Validation-Engine/languages/jquery.validationEngine-id.js"></script>
    <script src="<%=Helper.UrlRoot() %>/Bootstrap/plugins/daterangepicker/moment.min.js"></script>
    <script src="<%=Helper.UrlRoot() %>/Bootstrap/plugins/daterangepicker/daterangepicker.js"></script>
    <script src="<%=Helper.UrlRoot() %>/Bootstrap/plugins/datatables/jquery.dataTables.js"></script>
    <script src="<%=Helper.UrlRoot() %>/Bootstrap/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>
    <script src="<%=Helper.UrlRoot() %>/Bootstrap/plugins/inputmask/min/jquery.inputmask.bundle.min.js"></script>
    <style>
        .emo-selector input{
            margin:0;padding:0;
            -webkit-appearance:none;
               -moz-appearance:none;
                    appearance:none;
        }
        
        .emo-selector input:active +.emo{opacity: .9;}
        .emo-selector input:checked +.emo{
            -webkit-filter: none;
               -moz-filter: none;
                    filter: none;
        }
        .emo{
            cursor:pointer;
            background-size:contain;
            background-repeat:no-repeat;
            display:inline-block;
            width:100px;height:70px;
            -webkit-transition: all 100ms ease-in;
               -moz-transition: all 100ms ease-in;
                    transition: all 100ms ease-in;
            -webkit-filter: brightness(1.4) grayscale(.7) opacity(.8);
               -moz-filter: brightness(1.4) grayscale(.7) opacity(.8);
                    filter: brightness(1.4) grayscale(.7) opacity(.8);
        }
        .emo:hover{
            -webkit-filter: brightness(1.2) grayscale(.5) opacity(.9);
               -moz-filter: brightness(1.2) grayscale(.5) opacity(.9);
                    filter: brightness(1.2) grayscale(.5) opacity(.9);
        }

        /* Extras */
        a:visited{color:#888}
        a{color:#444;text-decoration:none;}
        p{margin-bottom:.3em;}

        .emo1{background-image:url(<%=Helper.UrlRoot() %>/Images/Emo/S1.png);}
        .emo2{background-image:url(<%=Helper.UrlRoot() %>/Images/Emo/S2.png);}
        .emo3{background-image:url(<%=Helper.UrlRoot() %>/Images/Emo/S3.png);}
        .emo4{background-image:url(<%=Helper.UrlRoot() %>/Images/Emo/S4.png);}
        .emo5{background-image:url(<%=Helper.UrlRoot() %>/Images/Emo/S5.png);}
    </style>
    <script language="javascript">   
        var revVersion = "0.dev";
        var QuestionFormID = "";
        var TransactionNo = "";
        var startDate;
        var endDate;
        var dtTable;

        function EnableButton(btnId, enable) {
            //$('#' + btnId).prop('disabled', !enable);
            if (enable) {
                $('#' + btnId).show();
            } else {
                $('#' + btnId).hide();
            }
        }
        function EnableInput(formId, enable) {
            $('#'+formId).prop("disabled", !enable);
        }

        $(document).ready(function() {
            $.ajaxSetup({
                beforeSend: function (xhr) {
                    if (xhr.overrideMimeType) {
                        xhr.overrideMimeType("application/json");
                    }
                }
            });

            // set nav collapse
            $('body').addClass("layout-navbar-fixed");

            SetHtml('spanPageTitle', 'Kuisioner');
            SetLeftMenu("List", "LoadPageList()", "fa fa-list");
            <%= GetScriptQuesionnaireList() %>
            SetLeftMenu("Home", "backToMainApp()", "fa fa-home");

            $("#Form1").validationEngine({ promptPosition: "bottomLeft", scroll: true });
            EnableButton('btnEdit', false);
            EnableButton('btnSave', false);

            $('#dateRange').daterangepicker({
                "startDate": moment(),
                "endDate": moment(),
                "autoApply": true,
                "buttonClasses": "btn-info",
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')]
                }
            }, function (start, end) {
                ////console.log("Callback has been called!");
                ////$('#dateRange span').html(start.format('D MMMM YYYY') + ' - ' + end.format('D MMMM YYYY'));
                //startDate = start;
                //endDate = end;

                //console.log(startDate);
                RefreshGrid();
            });

            //console.log(startDate);

            SetupGridList();

            LoadPageList();
        });

        function LoadPageQuestionnaire(QfID, QfName, TransNo) {
            CoreToggleShowHide(_aElements, 1);

            QuestionFormID = QfID;
            TransactionNo = TransNo;
            SetHtml('spanSub1Title', QfName);
            // load page quis
            var url = BaseURL + '/Module/Kiosk/QuestionnaireForm.aspx?qfid=' + QfID + '&transno=' + TransactionNo;
            CoreLoadContentToContainerDiv(url, 'divQuisForm', 'GET');

            if (TransactionNo == '') {
                EditQuestionnaire(true);
            } else {
                EditQuestionnaire(false);
            }
        }
        
        function LoadPageList() {
            CoreToggleShowHide(_aElements, 0);
            EnableButton('btnSave', false);
            EnableButton('btnEdit', false);
            //DisplayToast('Under construction', 'info');
        }

        function EditQuestionnaire(bEdit) {
            EnableButton('btnSave', bEdit);
            EnableButton('btnEdit', !bEdit);
            EnableInput("fsForm1", bEdit);
        }

        var _fnSaveQuestionnaireIsRunning = false;
        function SaveQuestionnaire() {
            if (_fnSaveQuestionnaireIsRunning == true) {
                return;
            }

            // validate 
            //alert($("#Form1").validationEngine('validate'));
            if (!$("#Form1").validationEngine('validate')) {
                return;
            }

            _fnSaveQuestionnaireIsRunning = true;
            EnableButton('btnSave', false);

            ShowProgress();

            //alert(coreReplaceSerialize($('#Form1').serialize()));

            var url = BaseURL + '/WebService/KioskQueue.asmx/PhrSave?qfid=' + QuestionFormID + '&transno=' + TransactionNo + '&' +
                coreReplaceSerialize($('#Form1').serialize());
                        
            $.ajax({
                type: 'POST',
                url: url,
                success: function (ret) {
                    // do something

                    //console.log(ret.data.ErrorType);
                    //CloseProgress();
                    if (ret.status === 'OK') {
                        console.log(ret.data);
                        TransactionNo = ret.data;
                        EditQuestionnaire(false);
                        ShowSuccess("Data have been saved");
                    } else {
                        var isErrValidation = false;
                        if (typeof (ret.data.ErrorType) === 'undefined') {
                            // The property DOESN'T exists
                        } else {
                            if (ret.data.ErrorType === "FieldValidation") {
                                isErrValidation = true;
                                MarkAsInvalid(ret.data.Keys)
                            } else {
                                
                            }
                        }
                        if (!isErrValidation) ShowError(ret.data);
                        EditQuestionnaire(true);
                        ShowError("Data can not be saved");
                    }
                    _fnSaveQuestionnaireIsRunning = false;
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    _fnSaveQuestionnaireIsRunning = false;
                    EditQuestionnaire(true);
                    ShowError(xhr.responseText);
                },
                dataType: 'json'
            });
        }

        function MarkAsInvalid(Keys) {
            ShowError("Some fields are required, please check your data");
        }

        function SetupGridList() {
            dtTable = $('#dtList').DataTable({
                "searchDelay": 1000,
                "processing": true,
                "serverSide": true,
                "scrollX": true,
                "searching": true,
                "paging": true,
                "info": true,
                "autoWidth": true,
                "columns": [
                    {
                        title: "Transaction No", data: 'TransactionNo', name: 'TransactionNo',
                        "render": function (data, type, row, meta) {
                            return "<a href=\"javascript:void(0);\" onclick=\"javascript:LoadPageQuestionnaire('" + row.QuestionFormID + "', '" + row.QuestionFormName + "', '" + row.TransactionNo + "')\">" + row.TransactionNo + "</a>";
                        }
                    },
                    //{ data: 'QuestionFormID', name: 'QuestionFormID', visible: false },
                    { title: "Form Name", data: 'QuestionFormName', name: 'QuestionFormName' },
                    { title: "Responden", data: 'Responden', name: 'Responden' }
                ],
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "<%=Helper.UrlRoot() %>/WebService/KioskQueue.asmx/PhrGetList",
                    "type": "POST",
                    "datatype": "json",
                    "data": function (d) {
                        //d.datefrom = getDateValue();
                        //d.dateto = getDateValue();
                        d.datefrom = moment($('#dateRange').data('daterangepicker').startDate).format('YYYY-MM-DD');
                        d.dateto = moment($('#dateRange').data('daterangepicker').endDate).format('YYYY-MM-DD');
                        //console.log(moment($('#dateRange').data('daterangepicker').startDate).format('YYYY-MM-DD'));
                    }
                }
            });
        }
        function RefreshGrid() {
            dtTable.ajax.reload();
        }
        
        // ============================
        var _aElements = ["divQuisList", "divQuisContent"];
    </script>
    <div class="row">
        <div class="col-sm-12 px-0 py-0">
            <div id="divQuisList">
                <div class="card card-secondary">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-sm-5">
                                <h3 class="card-title"><span class='fa fa-lg fa-book'></span> Questionnaire List</span></h3>
                            </div>
                            <div class="col-sm-7">
                                <div class="row float-right">
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="fa fa-calendar"></i>
                                                </span>
                                            </div>
                                            <input type="text" class="form-control float-right" id="dateRange">
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <button type="button" class="btn btn-outline-warning btn-lg" id="btnRefreshGrid"
                                            onclick="javascript:RefreshGrid();"><span class='fa fa-sync'> Refresh</span></button>&nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body table-responsive p-1">
                        <!-- /.form group -->
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
            <div id="divQuisContent">
                <div class="card card-secondary">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-sm-6">
                                <h3 class="card-title"><span class='fa fa-lg fa-book'></span> <span id="spanSub1Title"></span></h3>
                            </div>
                            <div class="col-sm-6">
                                <div class="row float-right">
                                    <button type="button" class="btn btn-outline-warning btn-lg" id="btnSave"
                                    onclick="javascript:SaveQuestionnaire();"><span class='fa fa-save'> Save</span></button>&nbsp;
                                    <button type="button" class="btn btn-outline-warning btn-lg" id="btnEdit"
                                    onclick="javascript:EditQuestionnaire(true);"><span class='fa fa-edit'> Edit</span></button>&nbsp;
                                </div>
                            </div>
                        </div>
                    </div>
                    <form role="form" id="Form1" class="form-horizontal">
                        <fieldset id="fsForm1">
                            <div class="card-body" id="divQuisForm">
                                Questions go here
                            </div>
                        </fieldset>
                    </form> 
                </div>
            </div>
        </div>
    </div>
</asp:Content>
