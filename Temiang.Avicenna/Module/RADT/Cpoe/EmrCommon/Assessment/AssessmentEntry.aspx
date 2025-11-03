<%@ Page Title="Assessment" Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="AssessmentEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentEntry" MaintainScrollPositionOnPostback="true" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <style>
            .container {
                height: 100%;
                position: relative;
            }

            #wrapper {
                height: 100%;
            }

            div.RadToolBar .rtbUL {
                width: 100%;
                white-space: normal;
            }

            div.RadToolBar .rightButton {
                /* float: left;  pindah di kiri saja supaya terlihat user*/
            }
        </style>
        <style>
            /*Pain Scale Image*/
            .ps00 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: 0px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps01 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-repeat: no-repeat;
                background-position: -49px 0px;
                height: 44px;
                width: 44px;
                display: block;
                cursor: pointer;
            }

            .ps02 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -98px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps03 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -147px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps04 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -194px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps05 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -243px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps06 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -292px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps07 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -340px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps08 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -388px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps09 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -437px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps10 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -485px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }
            /* End Pain Scale Image*/
        </style>

        <%--Special Input Char--%>
        <link href="<%= Helper.UrlRoot() %>/javascript/SpecialInput/jquery.specialinput.css" rel="stylesheet" />
        <script src="<%= Helper.UrlRoot() %>/javascript/SpecialInput/jquery-3.4.1.min.js"></script>
        <script type="text/javascript" src="<%= Helper.UrlRoot() %>/javascript/SpecialInput/jquery.specialinput.js"></script>

        <%-- Contoh Penerapan:
        $(document).ready(function () {
        $("#<%=txtNeurologis.ClientID%>").specialinput();
        }); --%>

        <%--End Special Input Char--%>


        <script type="text/javascript" language="javascript">
            function onWinWebCamClose(sender, eventArgs) {
                // Get retval
                var arg = eventArgs.get_argument();
                if (arg) {
                    var img = document.getElementById("<%=imgPatientPhoto.ClientID%>");
                    img.setAttribute('src', arg);
                    var hdnImgData = document.getElementById("<%=hdnImgData.ClientID%>");
                    hdnImgData.value = arg;
                }
                ContinueAutoSave();
            }

            function openWinWebCam() {
                PauseAutoSave();
                var oWnd = $find("<%= winWebCam.ClientID %>");
                oWnd.setUrl("<%= Helper.UrlRoot() %>/Module/RADT/Registration/PatientPhoto2/WebCam.aspx");
                oWnd.setSize(240 + 50, 320 + 50);
                oWnd.center();
                oWnd.show();
            }

            function openWinComparePhoto() {
                var url = "<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/Assessment/ComparePhoto.aspx?rimid=<%=Request.QueryString["rimid"] %>&astp=<%= Request.QueryString["astp"] %>&regno=<%= RegistrationNo %>";
                openWinEntryMaximize(url);
            }

            function ZoomViewImage(trno, seqno, no) {
                var url = "<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/ExamOrderResult/ExamOrderImageZoomView.aspx?trno=" + trno + "&seqno=" + seqno + "&imgno=" + no;
                openWinEntryMaximize(url);
            }

        </script>


        <script type="text/javascript" language="javascript">
            var _wcWidth =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamWidth) %>;
            var _wcHeight =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamHeight) %>;


            function onWinWebCamAssessmentImageClose(sender, eventArgs) {
                // Get retval
                var arg = eventArgs.get_argument();
                if (arg) {
                    var img = document.getElementById("imgAssessmentImage");
                    img.setAttribute('src', arg);
                    var hdnImgData = document.getElementById("hdnAssessmentImage");
                    hdnImgData.value = arg;
                }
                ContinueAutoSave();
            }

            function openWinWebCamAssessmentImage() {
                PauseAutoSave();
                var oWnd = $find("<%= winWebCamAssessmentImage.ClientID %>");
                oWnd.setUrl("<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/PatientDocument/WebCamJCrop.aspx");
                oWnd.setSize(_wcWidth + 40, _wcHeight + 80);
                oWnd.center();
                oWnd.show();
            }
            function AssessmentImageGallery(rimid, imgNo) {
                var url = "<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/ExamOrderResult/ExamOrderImageZoomView.aspx?trno=" + rimid + "&imgno=" + no;
                openWinEntryMaximize(url);
            }

            function radWindowManager_ClientClose(oWnd, args) {
                //get the transferred arguments from MasterDialogEntry
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.callbackMethod == 'submit') {
                        __doPostBack(arg.eventTarget, arg.eventArgument);
                    } else {
                        if (arg.callbackMethod == 'rebind') {
                            var ctl = $find(arg.eventTarget);
                            if (typeof ctl.rebind == 'function') {
                                ctl.rebind();
                            } else {
                                var masterTable = $find(arg.eventTarget).get_masterTableView();
                                masterTable.rebind();
                            }

                        }
                    }
                }
                ContinueAutoSave();
            }

            function openWinEntryMaxWindow(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                if (!(url.includes("&rt=") || url.includes("?rt=")))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';

                openWindow(url, width - 40, height - 40);
            }

            function openWindow(url, width, height) {
                PauseAutoSave();
                var oWnd = $find('<%=winDialog.ClientID %>');
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.show();
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
            }

            function openWinEntry(url, width, height) {
                url = url + '&rt=<%= Request.QueryString["rt"] %>';
                openWindow(url, width, height);
            }
            function openWinEntryMaximize(url) {
                PauseAutoSave();
                var oWnd;
                url = url + '&rt=<%= Request.QueryString["rt"] %>';
                oWnd = radopen(url, 'winDialog');
                oWnd.maximize();
            }
            function openWinNoTitlebar(url) {
                url = url + '&rt=<%= Request.QueryString["rt"] %>';
                var radwindow = $find('<%=winNoTitlebar.ClientID %>');
                radwindow.setUrl(url);
                radwindow.show();
                radwindow.maximize();
            }
            function OpenHistoryQuestion(questionID) {
                PauseAutoSave();
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl('../Common/QuestionHistoryDialog.aspx?pid=<%= PatientID %>&qid=' + questionID);
                oWnd.set_title('Allergy List');
                oWnd.show();
            }

            function openVitalSignChart(vitalSignID) {

                var url = '../../Common/VitalSignChart.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&fregno=<%= FromRegistrationNo %>&vid=' + vitalSignID;
                openWindow(url, 1000, 600);
            }

            // Other Notes Entry //
            (function (global, undefined) {
                var EditorCommandList;
                var _editor;

                var registerCustomCommands = function () {
                    EditorCommandList["RichEditor"] = function (commandName, editor, args) {
                        _editor = editor;
                        editorContent = _editor.get_html(true); //get RadEditor content
                        var advancedEditorDialog = global.findDialogWindow();
                        advancedEditorDialog.show(); //open RadWindow
                    };
                };

                global.Avicenna = {
                    editor_onClientLoad: function (editor, args) {
                        editor.get_contentArea().setAttribute("spellcheck", "false");
                        EditorCommandList = Telerik.Web.UI.Editor.CommandList;
                        registerCustomCommands();
                    },

                    SetEditorContent: function (content) {
                        //set content to RadEditor on the mane page from RadWindow
                        _editor.set_html(content);
                    },

                    SetDialogContent: function (oWnd) {
                        var contentWindow = oWnd.get_contentFrame().contentWindow;
                        if (contentWindow && contentWindow.setContent) {
                            window.setTimeout(function () {
                                //pass and set the content from the mane page to RadEditor in RadWindow
                                contentWindow.setContent(editorContent);
                            }, 500);
                        }
                    }
                };
            })(window);

            function applyDivEntryHeightMax() {
                var height =
                    ((window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight) - 90) + "px";
                document.getElementById("<%= divEntry.ClientID %>").style.maxHeight = height;
            }
            window.onload = function () {
                applyDivEntryHeightMax();
            }
            window.onresize = function () {
                applyDivEntryHeightMax();
            }

            // After postback
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (s, e) {
                applyDivEntryHeightMax();
            });


            function ShowSignEntry(mod, signFor) {
                window.PauseAutoSave();

                var imgId = "";
                var txtId = "";

                switch (signFor) {
                    case "PAR":
                        imgId = "<%=rbiParSign.ClientID %>";
                        txtId = "<%=hdnParSign.ClientID %>";
                        break;
                    case "PAT":
                        imgId = "<%=rbiPatSign.ClientID %>";
                        txtId = "<%=hdnPatSign.ClientID %>";
                        break;

                }

                // Tampilkan tandatangan
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=' + mod + '&imgId=' + imgId + '&txtId=' + txtId;

                var oWnd = $find('<%=winSign.ClientID %>');
                oWnd.setUrl(url);
                oWnd.show();

            }
            function winSign_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    document.getElementById(arg.txtId).value = arg.image;
                    var img = document.getElementById(arg.imgId);

                    img.setAttribute('src', "data:image/Png;base64," + arg.image);
                }
                window.ContinueAutoSave();
            }
        </script>

        <script type="text/javascript">
            // divEntry set scroll
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(BeginRequestHandler);
            prm.add_endRequest(EndRequestHandler);

            function BeginRequestHandler(sender, args) {
                xPos = document.getElementById("<%=divEntry.ClientID %>").scrollLeft;
                yPos = document.getElementById("<%=divEntry.ClientID %>").scrollTop;
                var h = document.getElementById("<%=hdnScrollValue.ClientID %>");
                h.value = xPos.toString() + "_" + yPos.toString();
            }
            function EndRequestHandler(sender, args) {
                var val = document.getElementById("<%=hdnScrollValue.ClientID %>").value.split('_');
                xPos = parseFloat(val[0]);
                yPos = parseFloat(val[1]);
                document.getElementById("<%=divEntry.ClientID %>").scrollLeft = xPos;
                document.getElementById("<%=divEntry.ClientID %>").scrollTop = yPos;
            }
        </script>
        <script type="text/javascript">
            function autoCheckOnBlur(sender, args, objClientID) {
                //console.log(sender.get_value());
                var rbl = document.getElementById(objClientID); //$find(objClientID);
                //var oRbl = $find(objClientID); <-- this crap is not working
                //console.log(rbl);
                //console.log(oRbl);
                //oRbl.set_selectedIndex(1);

                // lets do this the hard way
                $(rbl).find("input[type=radio]").each(function (i) {
                    if (i == 1) {
                        $(this).prop("checked", sender.get_value());
                    } else {
                        $(this).prop("checked", !sender.get_value());
                    }
                    //console.log($(this).prop("value"));
                    //console.log(sender.get_value());
                });
            }

            function checkedLastOptOnBlur(sender, args, objClientID) {
                // Checked last option if note not empty
                var rbl = document.getElementById(objClientID);

                // lets do this the hard way
                $(rbl).find("input[type=radio]").each(function (i) {
                    if (i == 1) {
                        $(this).prop("checked", sender.get_value());
                    }
                });
            }

            function fixDangText(sender, eventArgs) {
                var s = sender.get_value();

                //s = s.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot');
                s = s.replace(/</g, '< ').replace(/>/g, '> ').replace(/  /g, ' ');
                sender.set_value(s);
            }
        </script>
    </telerik:RadCodeBlock>
    <asp:HiddenField ID="hdnScrollValue" runat="server" />
    <asp:HiddenField ID="hdnIsContinuedAssessment" runat="server" />
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        VisibleStatusbar="false" DestroyOnClose="False" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="700px" Height="300px" runat="server" Modal="True" Behaviors="Close,Move" CssClass="label">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winNoTitlebar" Width="700px" Height="300px" runat="server" Behaviors="None" VisibleTitlebar="False">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winPrintPreview" Animation="None" Width="680px" Height="200px" runat="server"
                ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
                Modal="true">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winWebCam" Width="680px" Height="620px" runat="server" Modal="true"
                ShowContentDuringLoad="false" Behaviors="None" VisibleStatusbar="False"
                OnClientClose="onWinWebCamClose">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winWebCamAssessmentImage" Width="680px" Height="620px" runat="server" Modal="true"
                ShowContentDuringLoad="false" Behaviors="None" VisibleStatusbar="False"
                OnClientClose="onWinWebCamAssessmentImageClose">
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Move, Close,Maximize,Resize"
                ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winSign_ClientClose"
                ID="winSign" />
        </Windows>
    </telerik:RadWindowManager>
    <div id="divEntry" runat="server" style="width: 100%; overflow-y: scroll;">
        <table width="100%">
            <tr>
                <td style="vertical-align: top; width: 125px">
                    <fieldset style="min-height: 125px;">
                        <legend>Photo</legend>
                        <table style="width: 100%">
                            <tr>
                                <td colspan="2">
                                    <asp:Image runat="server" ID="imgPatientPhoto" Width="90px" Height="90px" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" Text="Cap" ID="btnCaptureImage" Width="45px"
                                        OnClientClick="openWinWebCam();return false;" /></td>
                                <td>
                                    <asp:Button runat="server" Text="Comp" ID="btnCompare" Width="45px"
                                        OnClientClick="openWinComparePhoto();return false;" /></td>
                            </tr>
                        </table>


                        <asp:HiddenField runat="server" ID="hdnImgData" />
                    </fieldset>
                </td>
                <td style="width: 450px">
                    <table width="100%">
                        <tr>
                            <td width="50px">Name
                            </td>
                            <td width="4px">:
                            </td>
                            <td>
                                <div style="font-weight: bold; font-size: large; text-align: left;">
                                    <asp:Label runat="server" ID="lblPatientName" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="50px">MRN
                            </td>
                            <td width="4px">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblMedicalNo" Font-Bold="true" />
                            </td>
                        </tr>                        
                        <tr>
                            <td>Gender
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblGender" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">DOB
                            </td>
                            <td style="vertical-align: top;">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblDateOfBirth" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>Education
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblSREducation" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>Job Title
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblEmployeeJobTitleName" Font-Bold="true" />
                            </td>
                        </tr>                         
                    </table>

                </td>
                <td style="width: 400px">
                    <table width="100%">
                        <tr>
                            <td>Reg. No
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblRegistrationNo" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>Reg. Date
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblRegistrationDate" Font-Bold="true" />
                            </td>
                        </tr>                        
                        <tr>
                            <td style="vertical-align: top;">Guarantor
                            </td>
                            <td style="vertical-align: top;">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblGuarantor" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">Unit
                            </td>
                            <td style="vertical-align: top;">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblServiceUnit" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">Room
                            </td>
                            <td style="vertical-align: top;">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblRoom" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">Handle
                            </td>
                            <td style="vertical-align: top;">:
                            </td>
                            <td>
                                <asp:HiddenField runat="server" ID="hdnAssessmentDateTime" />
                                <asp:Label runat="server" ID="lblHandleTime" Font-Bold="true" />
                            </td>
                        </tr>                       
                    </table>
                </td>
                <td style="width: 300px">
                    <fieldset runat="server" id="divPhysicianSign" style="width: 300px">
                        <legend>Doctor Sign</legend>
                        <telerik:RadBinaryImage ID="rbiParSign" runat="server"
                            Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                        <asp:Button runat="server" ID="btnParSign" Text="Sign" Width="300px" OnClientClick="javascript:ShowSignEntry('edit','PAR');return false;" />
                        <asp:HiddenField runat="server" ID="hdnParSign" />
                    </fieldset>
                </td>
                <td style="width: 300px">
                    <fieldset runat="server" id="divPatientSign" style="width: 300px">
                        <legend>Patient Sign</legend>
                        <telerik:RadBinaryImage ID="rbiPatSign" runat="server"
                            Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                        <asp:Button runat="server" ID="btnPatSign" Text="Sign" Width="300px" OnClientClick="javascript:ShowSignEntry('edit','PAT');return false;" />
                        <asp:HiddenField runat="server" ID="hdnPatSign" />
                    </fieldset>
                </td>
                <td></td>
            </tr>

        </table>

        <asp:HiddenField runat="server" ID="hdnRegistrationInfoMedicID" Value="" />
        <asp:HiddenField runat="server" ID="hdnIsEdited" Value="false" />
        <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
            Orientation="HorizontalTop">
            <Tabs>
                <telerik:RadTab runat="server" Text="Assessment" PageViewID="pvAssessment" Selected="True">
                </telerik:RadTab>
                <telerik:RadTab runat="server" Text="Other Notes" PageViewID="pvOtherNotes">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <telerik:RadMultiPage ID="multiPage" runat="server">
            <telerik:RadPageView ID="pvAssessment" runat="server" Selected="True">
                <div style="height: 4px;"></div>
                <div id="wrapper">
                    <div class="container" style="float: left; width: 100%;">
                        <fieldset>
                            <legend><b>ANAMNESIS / SUBJECTIVE</b></legend>
                            <div runat="server" id="anamnesisEntry"></div>
                        </fieldset>
                        <fieldset runat="server" id="peEntry">
                            <legend><b>PHYSICAL EXAMINATION</b></legend>
                            <table width="100%">
                                <tr>
                                    <td style="width: 50%">
                                        <div runat="server" id="peLeft"></div>
                                    </td>
                                    <td style="width: 50%">
                                        <div runat="server" id="peRight"></div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <div runat="server" id="contentEntry"></div>
                    </div>
                </div>

            </telerik:RadPageView>
            <telerik:RadPageView ID="pvOtherNotes" runat="server">
                <telerik:RadEditor RenderMode="Lightweight" ID="edtOtherNotes" runat="server" Width="100%"
                    OnClientLoad="Avicenna.editor_onClientLoad" AutoResizeHeight="true" ToolsFile="~/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ToolBarEditorAdv.xml" NewLineMode="Br">
                </telerik:RadEditor>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>
