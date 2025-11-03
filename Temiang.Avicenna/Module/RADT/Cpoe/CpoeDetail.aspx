<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="CpoeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.CpoeDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <style type="text/css">
            .reToolCell, .reLeftVerticalSide, .reRightVerticalSide
            {
                display: none !important;
            }

            .childMenu
            {
                padding: 5px 5px;
                height: 29px;
                color: yellow;
            }

            #wrapper
            {
                margin: 0 auto;
                border: 1px solid blue;
                overflow: hidden;
            }

                #wrapper > div
                {
                    display: inline-block;
                    vertical-align: middle;
                }

            .l
            {
                float: left;
            }

            .r
            {
                float: right;
                color: white;
            }

            .nav
            {
                position: fixed;
                top: 73px;
                width: 100%;
                z-index: 98;
            }

            .content
            {
                top: 32px;
                position: relative;
                z-index: 97;
            }
        </style>
        <script type="text/javascript" language="javascript">
            function OnClientButtonClicked(sender, args) {
                switch (args.get_item().get_value()) {
                    case "save":
                        if (!confirm('Are you sure to save?')) args.set_cancel(true);
                        break;
                    case "cancel":
                        if (!confirm('Are you sure to cancel?')) args.set_cancel(true);
                        break;
                }
            }

            function tbMenu_ClientClicked(sender, args) {
                switch (args.get_item().get_value()) {
                    case "list":
                        location.replace('CpoeList.aspx?rt=<%= Request.QueryString["rt"] %>');
                        break;
                }
            }

            function radWindowManager_ClientClose(oWnd, args) {
                //get the transferred arguments from MasterDialogEntry
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.callbackMethod == 'submit') {
                        __doPostBack(arg.eventTarget, arg.eventArgument);
                    } else {
                        if (arg.callbackMethod == 'rebind') {
                            var grd = $find(arg.eventTarget).get_masterTableView();
                            grd.rebind();

                            // Activate tab
                            var tabStrip = $find("<%= radTabStrip.ClientID %>");
                            var ids = arg.split('_');
                            var tabValue = ids[ids.length - 1];
                            var tab = tabStrip.findTabByValue(tabValue);
                            //Get tab object  
                            if (tab != null) {
                                tab.select(); //Select tab 
                            }
                        }
                    }
                }
                else {
                    if (oWnd.argument && oWnd.argument.rebind != null)
                        __doPostBack("<%= grdTransChargesItem.UniqueID %>", "rebind");
                }

            }

            function openVitalSignChart(regno) {
                var url = 'Common/VitalSignChart.aspx?patid=<%= lblPatientID.Text %>&regno=<%= lblRegistrationNo.Text %>';
                openWindow(url, 1000, 600);
            }

            function openWindow(url, width, height) {
                var oWnd;
                oWnd = radopen(url, 'winDialog');
                oWnd.setSize(width, height);
                oWnd.center();
            }
            function openWinEntry(url, width, height) {
                url = url + '&rt=<%= Request.QueryString["rt"] %>';
                openWindow(url, width, height);
            }
            function openWinEntryMaximize(url) {
                var oWnd;
                url = url + '&rt=<%= Request.QueryString["rt"] %>';
                oWnd = radopen(url, 'winDialog');
                oWnd.maximize();
            }
            function entryPatientAllergy() {
                var url = 'Common/PatientAllergyEntry.aspx?md=edit&patid=<%= lblPatientID.Text %>&ccm=submit&cea=allergy&cet=<%=grdRegistrationInfoMedic.UniqueID %>';
                openWinEntry(url, 700, 400);
            }
            function entryNotes(mod, recid) {
                var url = 'Common/RegistrationInfoMedicEntry.aspx?mod=' + mod + '&patid=<%= lblPatientID.Text %>&regno=<%= lblRegistrationNo.Text %>&recid=' + recid + "&ccm=rebind&cet=<%=grdRegistrationInfoMedic.ClientID %>";
                openWinEntry(url, 730, 500);
            }
            function entryPrescription(mod, presno, parid) {
                var url = 'Common/Medication/PrescriptionEntry.aspx?mod=' + mod + '&patid=<%= lblPatientID.Text %>&regno=<%= lblRegistrationNo.Text %>&presno=' + presno + '&parid=' + parid + "&cem=rebind&cet=<%=grdPrescription.ClientID %>";
                openWinEntry(url, 1000, 600);
            }
            function entryDocument(mod, pdid) {
                var url = 'Common/Document/PatientDocumentEntry.aspx?mod=' + mod + '&patid=<%= lblPatientID.Text %>&regno=<%= lblRegistrationNo.Text %>&pdid=' + pdid + "&ccm=rebind&cet=<%=grdDocument.ClientID %>";
                openWinEntry(url, 800, 420);
            }

            function entryEpisodeBodyDiagram(mod, regno, seqno) {
                var url = 'Common/EpisodeBodyDiagram/EpisodeBodyDiagramEntry.aspx?md=' + mod + '&parid=<%=ParamedicID%>&medno=<%= lblPatientID.Text %>&regno=' + regno + '&seqno=' + seqno + '&unit=<%=Request.QueryString["unit"]%>&ccm=rebind&cet=<%=grdBodyDiagram.ClientID %>';
                openWinEntry(url, 1000, 620);
            }

            function entryCharting(mod, regno, seqno) {
                var url = 'Common/ChartingImage/ChartingEntry.aspx?md=' + mod + '&patid=<%=lblPatientID.Text %>&medno=<%= lblPatientID.Text %>&regno=' + regno + '&seqno=' + seqno + '&ccm=rebind&cet=<%=grdCharting.ClientID %>';
                openWinEntry(url, 600, 400);
            }
            function editChartingImage(regno, seqno) {
                var url = 'Common/ChartingImage/ChartingImageEdit.aspx?md=edit&patid=<%=lblPatientID.Text %>&medno=<%= lblPatientID.Text %>&regno=' + regno + '&seqno=' + seqno + '&ccm=rebind&cet=<%=grdCharting.ClientID %>';
                openWinEntry(url, 1000, 620);
            }

            function entryEpisodeDiagnose(mod, regno, seqno) {
                var url = 'Common/Diagnose/EpisodeDiagnoseEntry.aspx?md=' + mod + '&parid=<%=ParamedicID%>&patid=<%= lblPatientID.Text %>&regno=' + regno + '&seqno=' + seqno + '&unit=<%=Request.QueryString["unit"]%>&ccm=submit&cea=diagnose&cet=<%=grdRegistrationInfoMedic.UniqueID %>';
                openWinEntry(url, 800, 400);
            }
            function voidEpisodeDiagnose(regno, seqno) {
                alert('Maaf, fitur sedang dalam development');
            }
            function entryPhr(md, id, regno, fid) {
                var url = 'Common/Phr/PatientHealthRecordDetail.aspx?md=' + md + '&id=' + id + '&regno=' + regno + '&fid=' + fid + '&menu=su';
                openWinEntryMaximize(url);
            }

            $.download = function (url, data, method) {
                //url and data options required
                if (url && data) {
                    //data can be string of parameters or array/object
                    data = typeof data == 'string' ? data : $.param(data);
                    //split params into form inputs
                    var inputs = '';
                    $.each(data.split('&'), function () {
                        var pair = this.split('=');
                        inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />';
                    });
                    //send request
                    $('<form action="' + url + '" method="' + (method || 'post') + '">' + inputs + '</form>').appendTo('body').submit().remove();
                };
            };

            function openWinPickList() {
                var cboJO = $find("<%= cboServiceUnitIDJO.ClientID %>");
                if (cboJO != null) {
                    if (cboJO.get_visible()) {
                        if (cboJO.get_value() == '') {
                            alert('Service Unit Order is required.');
                            return;
                        }
                    }
                }

                if (cboJO.get_value() != '') {
                    var oWnd = $find("<%= winDialog.ClientID %>");

                    if ((cboJO.get_value() == '<%=Temiang.Avicenna.Common.AppSession.Parameter.ServiceUnitLaboratoryID%>') ||
                        (cboJO.get_value() == '<%=Temiang.Avicenna.Common.AppSession.Parameter.ServiceUnitRadiologyID%>') ||
                        (cboJO.get_value() == '<%=Temiang.Avicenna.Common.AppSession.Parameter.ServiceUnitRadiologyID2%>'))
                        oWnd.setUrl('../../Charges/ServiceUnit/ServiceUnitTransaction/ItemPickerList.aspx?unit=' + cboJO.get_value() + '&reg=<%= Request.QueryString["regno"] %>&type=');
                    else
                        oWnd.setUrl('../../Charges/ServiceUnit/ServiceUnitTransaction/ItemPickerDetail.aspx?unit=' + cboJO.get_value() + '&reg=<%= Request.QueryString["regno"] %>&type=');

                    oWnd.set_title('Item List');
                    oWnd.show();
                    oWnd.maximize();
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdBodyDiagram">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdBodyDiagram" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRegistrationInfoMedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationInfoMedic" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="litPatientAllergy" />
                    <telerik:AjaxUpdatedControl ControlID="litDiagnosis" />
                    <telerik:AjaxUpdatedControl ControlID="litLastVitalSign" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPrescription">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPatientDocument">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatientDocument" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCharting">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCharting" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRefreshOrder">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdLabHist" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdImagingResult">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtImagingResult" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdJobOrder">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboServiceUnitIDJO" />
                    <telerik:AjaxUpdatedControl ControlID="txtTransactionDateJO" />
                    <telerik:AjaxUpdatedControl ControlID="txtNotesJO" />
                    <telerik:AjaxUpdatedControl ControlID="grdJobOrder" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTransChargesItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboServiceUnitIDJO" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="tbJobOrder">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdJobOrder" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="cboServiceUnitIDJO" />
                    <telerik:AjaxUpdatedControl ControlID="txtTransactionDateJO" />
                    <telerik:AjaxUpdatedControl ControlID="chkIsCitoJO" />
                    <telerik:AjaxUpdatedControl ControlID="txtNotesJO" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="700px" Height="300px" runat="server" Behaviors="Close,Move" Modal="True">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winPrintPreview" Animation="None" Width="680px" Height="200px" runat="server"
                ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
                Modal="true">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <div id="nav" class="nav">
        <telerik:RadToolBar runat="server" ID="tbMenu" Width="100%" OnClientButtonClicked="tbMenu_ClientClicked">
            <Items>
                <telerik:RadToolBarButton runat="server" Text="List" Value="list" ImageUrl="~/Images/Toolbar/details16.png"
                    HoveredImageUrl="~/Images/Toolbar/details16_h.png" DisabledImageUrl="~/Images/Toolbar/details16_d.png" />
            </Items>
        </telerik:RadToolBar>
    </div>
    <div id="content" class="content">
        <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="500px" Width="100%" HeightOffset="30" EnableEmbeddedScripts="True">
            <telerik:RadPane ID="navigationPane" runat="server" Width="300px">

                <cc:CollapsePanel ID="cpnRegInfo" runat="server" Width="100%">
                    <table width="100%">
                        <tr>
                            <td style="font-style: italic;" width="100px">Medical No
                            </td>
                            <td width="10px">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblMedicalNo" Font-Bold="true" />
                                <div style="display: none">
                                    <asp:Label runat="server" ID="lblPatientID" />
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td style="font-style: italic" width="100px">Registration No
                            </td>
                            <td width="10px">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblRegistrationNo" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td style="font-style: italic" width="100px">Registration Date
                            </td>
                            <td width="10px">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblRegistrationDate" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td style="font-style: italic" width="100px">Gender
                            </td>
                            <td width="10px">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblGender" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td style="font-style: italic" width="100px">Date of Birth
                            </td>
                            <td width="10px">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblDateOfBirth" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td style="font-style: italic" width="100px">Physician
                            </td>
                            <td width="10px">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblPhysician" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td style="font-style: italic" width="100px">Guarantor
                            </td>
                            <td width="10px">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblGuarantor" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td style="font-style: italic" width="100px">Service Unit
                            </td>
                            <td width="10px">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblServiceUnit" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td style="font-style: italic" width="100px">L O S
                            </td>
                            <td width="10px">:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblLos" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td width="10px"></td>
                            <td>
                                <asp:Label runat="server" ID="lblLastRegistrationNo" Visible="False" />
                            </td>
                        </tr>
                    </table>

                    <div style="height: 10px;">
                    </div>
                </cc:CollapsePanel>
                <cc:CollapsePanel ID="cpnDiagnosis" runat="server" Width="100%" Title="">
                    <asp:Literal runat="server" ID="litDiagnosis"></asp:Literal>

                    <div style="height: 10px">
                    </div>
                </cc:CollapsePanel>
                <cc:CollapsePanel ID="cpnAllergies" runat="server" Width="100%" Title="Allergies<div style='float: right;padding:4px'><a  href='#' onclick='entryPatientAllergy(); event.cancelBubble = true;if(event.stopPropagation) event.stopPropagation();'> <img src='../../../Images/Toolbar/edit16.png'  alt='edit' /></a></div>">
                    <asp:Literal runat="server" ID="litPatientAllergy"></asp:Literal>

                    <div style="height: 10px">
                    </div>
                </cc:CollapsePanel>
                <cc:CollapsePanel ID="CollapsePanel4" runat="server" Width="100%" Title="Last Vital Sign">
                    <asp:Literal runat="server" ID="litLastVitalSign"></asp:Literal>

                    <div style="height: 10px">
                    </div>
                </cc:CollapsePanel>
            </telerik:RadPane>
            <telerik:RadSplitBar ID="RadSplitbar1" runat="server" CollapseMode="Forward">
            </telerik:RadSplitBar>
            <telerik:RadPane ID="contentPane" runat="server" Scrolling="None" Height="100%">

                <telerik:RadTabStrip ID="radTabStrip" runat="server" MultiPageID="radMultiPage" ShowBaseLine="true"
                    SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Value="grdRegistrationInfoMedic" Text="Integrated Notes" PageViewID="pgRegistrationInfoMedic"
                            Selected="True" />
                        <telerik:RadTab runat="server" Value="grdPrescription" Text="Medication" PageViewID="pgPrescription" />
                        <telerik:RadTab runat="server" Text="Exam Order" PageViewID="pgRadiologyHist" />

                        <telerik:RadTab runat="server" Value="grdLaboratory" Text="Lab Result" PageViewID="pgLaboratory" />
                        <telerik:RadTab runat="server" Text="Imaging Result" PageViewID="pgDiagnosticHist" />

                        <telerik:RadTab runat="server" Value="grdBodyDiagram" Text="Body Diagram" PageViewID="pgBodyDiagram" />
                        <telerik:RadTab runat="server" Value="grdPhr" Text="Health Record" PageViewID="pgPhr" />
                        <telerik:RadTab runat="server" Value="grdDocument" Text="Document" PageViewID="pgDocument" />
                        <telerik:RadTab runat="server" Value="grdCharting" Text="Charting" PageViewID="pgCharting" />
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="radMultiPage" runat="server" SelectedIndex="0" ScrollBars="Auto">
                    <telerik:RadPageView ID="pgRegistrationInfoMedic" runat="server">
                        <telerik:RadGrid ID="grdRegistrationInfoMedic" runat="server" EnableViewState="False" Height="470px"
                            OnNeedDataSource="grdRegistrationInfoMedic_NeedDataSource"
                            OnDeleteCommand="grdRegistrationInfoMedic_DeleteCommand"
                            AutoGenerateColumns="False" GridLines="None">
                            <MasterTableView DataKeyNames="RegistrationInfoMedicID" ShowHeader="True" CommandItemDisplay="Top">
                                <CommandItemTemplate>
                                    <div>
                                        <div class="l">
                                            <%# IsUserCanNotAdd() ? "<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"../../../Images/Toolbar/new16_d.png\" />&nbsp;New</a>" : 
                                            string.Format("<a href=\"#\" onclick=\"javascript:entryNotes('new', '{0}', 0); return false;\"><img src=\"../../../Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;New</a>",lblRegistrationNo.Text)%>
                                        &nbsp;&nbsp;<%# string.Format("<a href=\"#\" onclick=\"javascript:openVitalSignChart('{0}'); return false;\"><img src=\"../../../Images/Toolbar/barchart.bmp\"  alt=\"Chart\" />&nbsp;Vital Sign Chart</a>",lblRegistrationNo.Text)%>
                                        </div>
                                        <div class="r" style="display: none">
                                            <asp:CheckBox runat="server" ID="chkShowAllRegistrationInfoMedic" Text="Show All" AutoPostBack="true" OnLoad="chkShowAll_OnLoad" OnCheckedChanged="chkShowAll_OnCheckedChanged" />&nbsp;&nbsp;
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Rebind" ImageUrl="~/Images/Toolbar/refresh16.png">
                                            <img src="../../../Images/Toolbar/refresh16.png" alt=""/>
                                        </asp:LinkButton>
                                        </div>
                                    </div>
                                </CommandItemTemplate>
                                <CommandItemStyle Height="29px" />
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <%# (this.IsUserEditAble.Equals(false) || true.Equals(DataBinder.Eval(Container.DataItem, "IsDeleted")) || !AppSession.UserLogin.UserID.Equals(DataBinder.Eval(Container.DataItem, "LastUpdateByUserID"))  ? 
                                            "<img src=\"../../../Images/Toolbar/edit16_d.png\" />" : 
                                            string.Format("<a href=\"#\" onclick=\"javascript:entryNotes('edit', '{0}'); return false;\"><img src=\"../../../Images/Toolbar/edit16.png\"  /></a>",DataBinder.Eval(Container.DataItem, "RegistrationInfoMedicID")))%>
                                            <hr />
                                            <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                                                Visible='<%#(false.Equals(DataBinder.Eval(Container.DataItem, "IsDeleted")) &&  this.IsUserEditAble.Equals(true) && (DataBinder.Eval(Container.DataItem, "LastUpdateByUserID")).Equals(AppSession.UserLogin.UserID)) %>'
                                                OnClientClick="javascript: if (!confirm('Are you sure delete this ?')) return false;">
                                                    <img style="border: 0px; vertical-align: middle;" src="../../../Images/Toolbar/row_delete16.png" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Notes">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Notes")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Vital Sign">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "VitalSigns")%>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Selecting AllowRowSelect="False" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgPrescription" runat="server">
                        <telerik:RadGrid ID="grdPrescription" runat="server" EnableViewState="False" OnNeedDataSource="grdPrescription_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdPrescription_DeleteCommand" Height="470px"
                            OnItemDataBound="grdPrescription_ItemDataBound">
                            <MasterTableView DataKeyNames="PrescriptionNo" ShowHeader="True" CommandItemDisplay="Top">
                                <CommandItemTemplate>
                                    <div>
                                        <div class="l">
                                            <%# (string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID) || IsUserCanNotAdd() ? "<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"../../../Images/Toolbar/new16_d.png\" />&nbsp;New</a>" : string.Format("<a href=\"#\" onclick=\"javascript:entryPrescription('new', '{0}', 0); return false;\"><img src=\"../../../Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;New</a>",lblRegistrationNo.Text))%>
                                        </div>
                                        <div class="r" style="color: yellow; display: none">
                                            <asp:CheckBox runat="server" ID="chkShowAll" Text="Show All" AutoPostBack="true" ForeColor="Yellow" />&nbsp;&nbsp;
                                        <asp:LinkButton runat="server" CommandName="Rebind" ImageUrl="~/Images/Toolbar/refresh16.png"><img src="../../../Images/Toolbar/refresh16.png" /></asp:LinkButton>
                                        </div>
                                    </div>
                                </CommandItemTemplate>
                                <CommandItemStyle Height="29px" />
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <%# (this.IsUserEditAble.Equals(false) || !(DataBinder.Eval(Container.DataItem, "CreatedByUserID")).Equals(AppSession.UserLogin.UserID) || string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID) || true.Equals(DataBinder.Eval(Container.DataItem, "IsApproval"))  || true.Equals(DataBinder.Eval(Container.DataItem, "IsVoid"))? 
                                            "<img src=\"../../../Images/Toolbar/edit16_d.png\" />" : 
                                            string.Format("<a href=\"#\" onclick=\"javascript:entryPrescription('edit', '{0}', '{1}'); return false;\"><img src=\"../../../Images/Toolbar/edit16.png\"  /></a>",DataBinder.Eval(Container.DataItem, "PrescriptionNo"),DataBinder.Eval(Container.DataItem, "ParamedicID")))%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Prescription History">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "PrescriptionSummary")%>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="250px" />
                                        <HeaderStyle Width="250px" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderText="Item">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "PrescriptionItem")%>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="PrescriptionDate" UniqueName="PrescriptionDate"
                                        Visible="false" />
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Selecting AllowRowSelect="False" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="pgRadiologyHist" runat="server">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td valign="top" width="25%">
                                    <telerik:RadGrid ID="grdJobOrder" runat="server" OnNeedDataSource="grdJobOrder_NeedDataSource"
                                        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdJobOrder_DeleteCommand" Height="470px"
                                        OnItemDataBound="grdJobOrder_ItemDataBound" OnItemCommand="grdJobOrder_ItemCommand">
                                        <MasterTableView DataKeyNames="TransactionNo">
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Exam Order History">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "JobOrderSummary")%>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblEdit" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "TransactionNo")%>'
                                                            CommandName="Update" ToolTip="Edit">
                                                    <img style="border: 0px; text-align:center; vertical-align: middle;" src="../../../Images/Toolbar/edit16.png" />
                                                        </asp:LinkButton>
                                                        <hr />
                                                        <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                                                            OnClientClick="javascript: if (!confirm('Are you sure?')) return false;">
                                                    <img style="border: 0px; vertical-align: middle;" src="../../../Images/Toolbar/row_delete16.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="TransactionDate" UniqueName="TransactionDate"
                                                    Visible="false" />
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </td>
                                <td valign="top" width="75%">
                                    <table width="100%">
                                        <tr>
                                            <td class="label" style="font-style: italic">Service Unit Order
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboServiceUnitIDJO" runat="server" Width="304px" AutoPostBack="True" />
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtTransactionDateJO" runat="server" Width="100px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="font-style: italic">Notes
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtNotesJO" runat="server" TextMode="MultiLine" Width="99.5%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <telerik:RadGrid ID="grdTransChargesItem" runat="server" OnNeedDataSource="grdTransChargesItem_NeedDataSource"
                                                    AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdTransChargesItem_DeleteCommand"
                                                    OnItemCommand="grdTransChargesItem_ItemCommand" ShowHeader="false">
                                                    <MasterTableView DataKeyNames="SequenceNo" CommandItemDisplay="Top">
                                                        <CommandItemTemplate>
                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="left">
                                                                        <table cellpadding="0" cellspacing="0" style="display: none">
                                                                            <tr>
                                                                                <td>&nbsp;&nbsp;&nbsp; <i>Item Name</i> &nbsp;
                                                                            <telerik:RadComboBox ID="cboItemIDJO" runat="server" Width="304px" EnableLoadOnDemand="true"
                                                                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboItemIDJO_ItemDataBound"
                                                                                OnItemsRequested="cboItemIDJO_ItemsRequested">
                                                                                <FooterTemplate>
                                                                                    Note : Show max 15 items
                                                                                </FooterTemplate>
                                                                            </telerik:RadComboBox>
                                                                                </td>
                                                                                <td>&nbsp;
                                                                            <asp:LinkButton ID="lbInsert" runat="server" CommandName="Insert">
                                                                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../Images/Toolbar/insert16.png" /> Add new record
                                                                            </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdTransChargesItem.MasterTableView.IsItemInserted %>'
                                                                            OnClientClick="javascript:openWinPickList();return false;">
                                                                <img style="border: 0px; vertical-align: middle;" alt="" src="../../../Images/Toolbar/views16.png" /> Item picker
                                                                        </asp:LinkButton>
                                                                        &nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </CommandItemTemplate>
                                                        <CommandItemStyle Height="29px" />
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText="Item Name" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ChargeQuantity" HeaderText="Qty"
                                                                UniqueName="ChargeQuantity" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SRItemUnit" HeaderText="Unit"
                                                                UniqueName="SRItemUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                                <HeaderStyle Width="30px" />
                                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                            </telerik:GridButtonColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="true">
                                                        <Resizing AllowColumnResize="True" />
                                                        <Selecting AllowRowSelect="True" />
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <telerik:RadToolBar runat="server" ID="tbJobOrder" Width="100%" OnButtonClick="tbJobOrder_Click"
                                                    OnClientButtonClicked="OnClientButtonClicked">
                                                    <Items>
                                                        <telerik:RadToolBarButton runat="server" ToolTip="New" Text="New" Value="new" ImageUrl="~/Images/Toolbar/new16.png"
                                                            HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
                                                        <telerik:RadToolBarButton runat="server" ToolTip="Save" Text="Save" Value="save"
                                                            ImageUrl="~/Images/Toolbar/save16.png" HoveredImageUrl="~/Images/Toolbar/save16_h.png"
                                                            DisabledImageUrl="~/Images/Toolbar/save16_d.png" />
                                                    </Items>
                                                </telerik:RadToolBar>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="pgLaboratory" runat="server">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="50%">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">Exam Order date
                                            </td>
                                            <td class="entry">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtExamDate1" runat="server" Width="100px" />
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtExamDate2" runat="server" Width="100px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="20px">
                                                <asp:ImageButton ID="btnFilterOrderDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                    OnClick="btnFilterOrder_Click" ToolTip="Search" />
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="btnRefreshOrder" runat="server" ImageUrl="~/Images/Toolbar/refresh16.png"
                                                    OnClick="btnFilterOrder_Click" ToolTip="Refresh" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="50%">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">Exam Order Name
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboExamOrderName" Width="304px" AllowCustomText="true"
                                                    Filter="Contains" />
                                            </td>
                                            <td width="20px">
                                                <asp:ImageButton ID="btnFilterOrderName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                    OnClick="btnFilterOrder_Click" ToolTip="Search" />
                                            </td>
                                            <td />
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <telerik:RadGrid ID="grdLabHist" runat="server" AutoGenerateColumns="False" GridLines="None" Height="440px">
                            <MasterTableView DataKeyNames="OrderLabNo" GroupLoadMode="Client">
                                <GroupByExpressions>
                                    <telerik:GridGroupByExpression>
                                        <SelectFields>
                                            <telerik:GridGroupByField FieldName="OrderLabTglOrder" HeaderText="Order Date" />
                                            <telerik:GridGroupByField FieldName="OrderLabNo" HeaderText="Order No" />
                                        </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="OrderLabTglOrder" SortOrder="Descending" />
                                            <telerik:GridGroupByField FieldName="OrderLabNo" SortOrder="Descending" />
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                    <telerik:GridGroupByExpression>
                                        <SelectFields>
                                            <telerik:GridGroupByField FieldName="TEST_GROUP" HeaderText="GROUP" />
                                        </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="TEST_GROUP" SortOrder="None" />
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                </GroupByExpressions>
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderText="Laboratory Exam Summary">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "LabOrderSummary")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Result" HeaderStyle-Width="150px">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Result")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="StandarValue" UniqueName="StandarValue" HeaderText="Standard Value"
                                        HeaderStyle-Width="150px" />
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Selecting AllowRowSelect="False" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="pgDiagnosticHist">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td valign="top" width="25%">
                                    <telerik:RadGrid ID="grdImagingResult" runat="server" OnNeedDataSource="grdImagingResult_NeedDataSource"
                                        AutoGenerateColumns="False" GridLines="None" Height="470px"
                                        OnItemCommand="grdImagingResult_ItemCommand">
                                        <MasterTableView DataKeyNames="TransactionNo">
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Imaging Expertise History">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "TransactionNo")%> - <%#DataBinder.Eval(Container.DataItem, "TestResultDateTime")%>
                                                        <br />
                                                        <u><b><%#DataBinder.Eval(Container.DataItem, "ParamedicName")%></b></u>
                                                        <br />
                                                        <%#DataBinder.Eval(Container.DataItem, "ItemName")%>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblEdit" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "TransactionNo") + ";" + DataBinder.Eval(Container.DataItem, "ItemID")%>'
                                                            CommandName="Update" ToolTip="Edit">
                                                    <img style="border: 0px; text-align:center; vertical-align: middle;" src="../../../Images/Toolbar/search16.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>

                                </td>
                                <td valign="top" width="74%">
                                    <telerik:RadEditor ID="txtImagingResult" runat="server" Width="100%" Height="495px" NewLineMode="Br" EditModes="Design">

                                        <Tools>
                                            <telerik:EditorToolGroup name="invisibleToolbar" dockingZone="Bottom">
                                            </telerik:EditorToolGroup>
                                        </Tools>

                                    </telerik:RadEditor>

                                </td>
                            </tr>
                        </table>

                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgBodyDiagram" runat="server">
                        <telerik:RadGrid ID="grdBodyDiagram" runat="server" OnNeedDataSource="grdBodyDiagram_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdBodyDiagram_DeleteCommand" Height="470px"
                            AllowPaging="False">

                            <MasterTableView DataKeyNames="RegistrationNo, SequenceNo" CommandItemDisplay="Top">
                                <CommandItemTemplate>
                                    <div>
                                        <div class="l">
                                            <%# (IsUserCanNotAdd() ? "<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"../../../Images/Toolbar/new16_d.png\" />&nbsp;New</a>" : string.Format("<a href=\"#\" onclick=\"javascript:entryEpisodeBodyDiagram('new', '{0}', 0); return false;\"><img src=\"../../../Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;New</a>",lblRegistrationNo.Text))%>
                                        </div>
                                        <div class="r" style="display: none">
                                            <asp:CheckBox runat="server" ID="chkShowAllBodyDiagram" Text="Show All" AutoPostBack="true" OnLoad="chkShowAll_OnLoad" OnCheckedChanged="chkShowAll_OnCheckedChanged" />
                                        </div>
                                    </div>
                                </CommandItemTemplate>
                                <CommandItemStyle Height="29px" />
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>

                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblEdit" runat="server" ToolTip="Edit"
                                                OnClientClick='<%# string.Format("entryEpisodeBodyDiagram(\"edit\", \"{0}\", \"{1}\")", DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"))%>'
                                                Visible='<%#!Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDeleted")) && (DataBinder.Eval(Container.DataItem, "LastUpdateByUserID").Equals(AppSession.UserLogin.UserID)) %>'>
                                                    <img style="border: 0px; vertical-align: middle;" src="../../../Images/Toolbar/edit16.png" />
                                            </asp:LinkButton>
                                            <hr />

                                            <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                                                Visible='<%#(DataBinder.Eval(Container.DataItem, "LastUpdateByUserID").Equals(AppSession.UserLogin.UserID)) %>'
                                                OnClientClick="javascript: if (!confirm('Are you sure?')) return false;">
                                                    <img style="border: 0px; vertical-align: middle;" src="../../../Images/Toolbar/row_delete16.png" />
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText=""
                                        ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>

                                            <table>
                                                <tr>
                                                    <td>Registration No</td>
                                                    <td>:</td>
                                                    <td><%#DataBinder.Eval(Container.DataItem, "RegistrationNo")%></td>
                                                </tr>
                                                <tr>
                                                    <td>Physician</td>
                                                    <td>:</td>
                                                    <td><%#DataBinder.Eval(Container.DataItem, "ParamedicName")%></td>
                                                </tr>
                                                <tr>
                                                    <td>Created Date</td>
                                                    <td>:</td>
                                                    <td><%#string.Format("{0:d}",Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "CreatedDateTime")))%></td>
                                                </tr>
                                                <tr>
                                                    <td>Last Update Date</td>
                                                    <td>:</td>
                                                    <td><%#string.Format("{0:d}",Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "LastUpdateDateTime")))%></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderText="Body Image" HeaderStyle-Width="150px">
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" AlternateText="" DataValue='<%# DataBinder.Eval(Container.DataItem, "BodyImage") == DBNull.Value ? new System.Byte[0] : DataBinder.Eval(Container.DataItem, "BodyImage") %>'
                                                Width="100"
                                                Height="100"
                                                ResizeMode="Fill" />
                                        </ItemTemplate>

                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="SOAPEDate" UniqueName="SOAPEDate" Visible="false" />
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Selecting AllowRowSelect="False" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                        </telerik:RadGrid>

                    </telerik:RadPageView>

                    <telerik:RadPageView ID="pgPhr" runat="server">
                        <telerik:RadGrid ID="grdQuestionForm" runat="server" AllowSorting="true"
                            EnableLinqExpressions="false" OnNeedDataSource="grdQuestionForm_OnNeedDataSource">
                            <MasterTableView DataKeyNames="QuestionFormID" ClientDataKeyNames="QuestionFormID"
                                AllowPaging="True" AutoGenerateColumns="False" GroupLoadMode="Client" HierarchyLoadMode="ServerOnDemand">
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="Edit">
                                        <ItemTemplate>
                                            <%# IsUserCanNotAdd() ? "<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"../../../Images/Toolbar/new16_d.png\" />&nbsp;New</a>" : string.Format("<a href=\"#\" onclick=\"entryPhr('new', '', '{0}','{1}','OPR'); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" border=\"0\" /></a>", Eval("RegistrationNo"), Eval("QuestionFormID"))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="PrintPHR" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center"
                                        Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnPrintPHR" runat="server" CommandName='<%# DataBinder.Eval(Container.DataItem, "formID") %>'
                                                ToolTip='Print Form' CommandArgument='<%#string.Format("{0}_{1}", DataBinder.Eval(Container.DataItem, "RegistrationNo"),DataBinder.Eval(Container.DataItem, "QuestionFormID")) %>'>
                                                    <img src="../../../Images/Toolbar/print16.png" border="0" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="QuestionFormName" HeaderText="Question Form"
                                        UniqueName="QuestionFormName" SortExpression="QuestionFormName">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="RecordDate" HeaderText="Record Date" UniqueName="RecordDate"
                                        SortExpression="RecordDate">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Examiner" UniqueName="EmployeeName"
                                        SortExpression="EmployeeName">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridCheckBoxColumn DataField="IsMCUForm" HeaderText="MCU Form" UniqueName="IsMCUForm">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsComplete" HeaderText="Complete"
                                        UniqueName="IsComplete" SortExpression="IsComplete" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Selecting AllowRowSelect="False" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                        <telerik:RadGrid ID="grdPhr" runat="server" OnNeedDataSource="grdPhr_NeedDataSource" AllowSorting="true"
                            EnableLinqExpressions="false" OnItemCommand="grdPHR_ItemCommand">
                            <MasterTableView DataKeyNames="QuestionFormID" ClientDataKeyNames="QuestionFormID"
                                AllowPaging="True" AutoGenerateColumns="False">
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="Edit">
                                        <ItemTemplate>
                                            <%# string.Format("<a href=\"#\" onclick=\"entryPhr('edit', '{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/edit16.png\" border=\"0\" /></a>", Eval("TransactionNo"), Eval("RegistrationNo"), Eval("QuestionFormID"))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="PrintPHR" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnPrintPHR" runat="server" CommandName='<%# DataBinder.Eval(Container.DataItem, "formID") %>'
                                                ToolTip='Print Form' CommandArgument='<%#string.Format("print|{0}_{1}_{2}", DataBinder.Eval(Container.DataItem, "RegistrationNo"),DataBinder.Eval(Container.DataItem, "QuestionFormID"),DataBinder.Eval(Container.DataItem, "TransactionNo")) %>'>
                                                    <img src="../../../Images/Toolbar/print16.png" border="0" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridDateTimeColumn DataField="TransactionNo" HeaderText="Document No" UniqueName="TransactionNo"
                                        SortExpression="TransactionNo">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                                        SortExpression="RegistrationNo">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician Name" UniqueName="ParamedicName"
                                        SortExpression="ParamedicName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit"
                                        UniqueName="ServiceUnitName" SortExpression="ServiceUnitName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="QuestionFormName" HeaderText="Question Form"
                                        UniqueName="QuestionFormName" SortExpression="QuestionFormName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="RecordDate" HeaderText="Record Date" UniqueName="RecordDate"
                                        SortExpression="RecordDate">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn DataField="ReferenceNo" HeaderText="Reference No" UniqueName="ReferenceNo"
                                        SortExpression="ReferenceNo">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Examiner" UniqueName="EmployeeName"
                                        SortExpression="EmployeeName" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridCheckBoxColumn DataField="IsMCUForm" HeaderText="MCU Form" UniqueName="IsMCUForm">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsComplete" HeaderText="Complete"
                                        UniqueName="IsComplete" SortExpression="IsComplete" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridTemplateColumn HeaderStyle-Width="35px" HeaderStyle-HorizontalAlign="center"
                                        ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkDelete" runat="server" OnClientClick="javascript:if(!confirm('Are you sure?'))return false;"
                                                CommandArgument='<%#string.Format("delete|{0}_{1}_{2}", DataBinder.Eval(Container.DataItem, "RegistrationNo"),DataBinder.Eval(Container.DataItem, "QuestionFormID"),DataBinder.Eval(Container.DataItem, "TransactionNo")) %>'>
                                                    <img src="../../../Images/Toolbar/row_delete16.png" border="0" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Selecting AllowRowSelect="False" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                        </telerik:RadGrid>

                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgDocument" runat="server">
                        <telerik:RadGrid runat="server" ID="grdDocument" OnNeedDataSource="grdDocument_NeedDataSource" Height="470px"
                            AllowSorting="true">
                            <MasterTableView ShowHeader="true" AutoGenerateColumns="False" AllowPaging="false"
                                DataKeyNames="PatientDocumentID" CommandItemDisplay="Top">
                                <CommandItemTemplate>
                                    <div>
                                        <%# (IsUserCanNotAdd() ? "<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"../../../Images/Toolbar/new16_d.png\" />&nbsp;New</a>" : "<a href=\"#\" onclick=\"javascript:entryDocument('new', 0); return false;\"><img src=\"../../../Images/Toolbar/new16.png\" style=\"border: 0px; vertical-align: middle;\" alt=\"New\" />&nbsp;New</a>")%>
                                    </div>
                                </CommandItemTemplate>
                                <CommandItemStyle Height="29px" />
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="File" HeaderText="File">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "FileAttachName").ToString() == string.Empty ? string.Empty : string.Format("<a href=\"#\" onclick=\"$.download('Common/Document/PatientDocumentDownload.aspx','filepath={0}'); return false;\"><img src=\"../../../Images/Toolbar/download16.png\" border=\"0\" /></a>",
                                                        HttpUtility.UrlEncode(string.Format("{0}\\{1}\\{2}",  GetPatientDocumentFolder(Container.DataItem), Eval("PatientID"), Eval("FileAttachName"))))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="DocumentName" HeaderText="Document Name"
                                        UniqueName="DocumentName" SortExpression="DocumentName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="DocumentDate" HeaderText="DocumentDate"
                                        UniqueName="Reference" SortExpression="DocumentDate" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                        SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Selecting AllowRowSelect="False" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgCharting" runat="server">
                        <telerik:RadGrid ID="grdCharting" runat="server" OnNeedDataSource="grdCharting_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdCharting_DeleteCommand" Height="470px"
                            AllowPaging="False">
                            <MasterTableView DataKeyNames="PatientID, SequenceNo" ShowHeader="True" CommandItemDisplay="Top">
                                <CommandItemTemplate>
                                    <div>
                                        <%# (IsUserCanNotAdd() ? "<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"../../../Images/Toolbar/new16_d.png\" />&nbsp;New</a>" : string.Format("<a href=\"#\" onclick=\"javascript:entryCharting('new', '{0}', 0); return false;\"><img src=\"../../../Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;New</a>",lblRegistrationNo.Text))%>
                                    </div>
                                </CommandItemTemplate>
                                <CommandItemStyle Height="29px" />
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblEdit" runat="server" CommandName="View" ToolTip="Edit"
                                                OnClientClick='<%# string.Format("entryCharting(\"edit\", \"{0}\", \"{1}\")", DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"))%>'
                                                CommandArgument='<%#string.Format("{0}|{1}", DataBinder.Eval(Container.DataItem, "PatientID"), DataBinder.Eval(Container.DataItem, "SequenceNo"))%>'>
                                                    <img style="border: 0px; vertical-align: middle;" src="../../../Images/Toolbar/edit16.png" />
                                            </asp:LinkButton>
                                            <hr />

                                            <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                                                CommandArgument='<%#string.Format("{0}|{1}", DataBinder.Eval(Container.DataItem, "PatientID"), DataBinder.Eval(Container.DataItem, "SequenceNo"))%>'
                                                OnClientClick="javascript: if (!confirm('Are you sure?')) return false;">
                                                    <img style="border: 0px; vertical-align: middle;" src="../../../Images/Toolbar/row_delete16.png" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName1" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>Chart Name</td>
                                                    <td>:</td>
                                                    <td><%#DataBinder.Eval(Container.DataItem, "Name")%></td>
                                                </tr>
                                                <tr>
                                                    <td>Notes</td>
                                                    <td>:</td>
                                                    <td><%#DataBinder.Eval(Container.DataItem, "Notes")%></td>
                                                </tr>
                                                <tr>
                                                    <td>Created Date</td>
                                                    <td>:</td>
                                                    <td><%#string.Format("{0:d}",Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "CreatedDateTime")))%></td>
                                                </tr>
                                                <tr>
                                                    <td>Last Update Date</td>
                                                    <td>:</td>
                                                    <td><%#string.Format("{0:d}",Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "LastUpdateDateTime")))%></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderText="Chart Image" HeaderStyle-Width="150px">
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Edit"
                                                OnClientClick='<%# string.Format("editChartingImage(\"{0}\", \"{1}\")", DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"))%>'
                                                Visible='<%#(DataBinder.Eval(Container.DataItem, "LastUpdateByUserID").Equals(AppSession.UserLogin.UserID)) %>'>
                                                <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" AlternateText="" DataValue='<%# DataBinder.Eval(Container.DataItem, "Image") == DBNull.Value ? new System.Byte[0] : DataBinder.Eval(Container.DataItem, "Image") %>'
                                                    Width="100"
                                                    Height="100"
                                                    ResizeMode="Fill" />
                                            </asp:LinkButton>

                                        </ItemTemplate>

                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Selecting AllowRowSelect="False" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                        </telerik:RadGrid>

                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>

</asp:Content>
