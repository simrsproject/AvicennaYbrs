<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="ParamedicFeeVerificationByDischargeDateList.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.V2.ParamedicFeeVerificationByDischargeDateList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "generate":
                        if (confirm('Are you sure to verified selected item?'))
                            __doPostBack("<%= grdList.UniqueID %>", 'generate');
                        break;
                }
            }

            function rowUnApproved(verNo) {
                if (confirm('Are you sure to unapprove selected item?'))
                    __doPostBack("<%= grdList.UniqueID %>", 'unapproved|' + verNo);
            }

            function rowApproved(verNo) {
                __doPostBack("<%= grdList.UniqueID %>", 'approved|' + verNo);
            }

            function pageApproved() {
                __doPostBack("<%= grdList.UniqueID %>", 'approved|page');
            }

            function openDialog(regNo, transNo, seqNo, compId, parId, IsPhysicianMember) {
                var oWnd = $find("<%= winDialog.ClientID %>");

                oWnd.setUrl("ParamedicFeeVerificationByDischargeDateDialog.aspx?regno=" + regNo +
                    "&trno=" + transNo + "&seqno=" + seqNo + "&compid=" + compId + "&parid=" + parId + "&IsPhysicianMember=" + IsPhysicianMember);
                oWnd.show();
            }
            function openDialogShare(regNo, transNo, seqNo, compId, parId, IsPhysicianMember) {
                var oWnd = $find("<%= winDialog.ClientID %>");

                oWnd.setUrl("ParamedicFeeVerificationByDischargeDateChangeShareDialog.aspx?regno=" + regNo +
                    "&trno=" + transNo + "&seqno=" + seqNo + "&compid=" + compId + "&parid=" + parId + "&IsPhysicianMember=" + IsPhysicianMember);
                oWnd.show();
            }
            function openDialogNotes(regNo, transNo, seqNo, compId, parId, IsPhysicianMember) {
                var oWnd = $find("<%= winDialog.ClientID %>");

                oWnd.setUrl("ParamedicFeeVerificationByDischargeDateNotes.aspx?regno=" + regNo + "&trno=" + transNo +
                    "&seqno=" + seqNo + "&compid=" + compId + "&parid=" + parId + "&IsPhysicianMember=" + IsPhysicianMember);
                oWnd.show();
            }
            function onClientClose(oWnd, args) {
                if (oWnd.argument == 'rebind') {
                    __doPostBack("<%= grdList.UniqueID %>", "rebind");
                    oWnd.argument = 'undefined';
                }
            }
            function deleteFee(transNo, seqNo, compId, parId, IsPhysicianMember) {
                if (confirm('Are you sure want to delete selected item?')) {
                    __doPostBack("<%= grdList.UniqueID %>", 'deleteFee|' + transNo + '|' + seqNo + '|' + compId + '|' + parId + '|' + IsPhysicianMember);
                }
            }
            function writeOffFee(transNo, seqNo, compId, parId, IsPhysicianMember) {
                var reason = prompt("Please enter deletion reason", "");
                if (reason == null || reason == "") {
                    txt = "User cancelled the prompt.";
                } else {
                    __doPostBack("<%= grdList.UniqueID %>", 'deleteFee|' + transNo + '|' + seqNo + '|' + compId + '|' + parId + '|' + IsPhysicianMember + '|' + reason);
                }
            }
            function OpenPaymentInfo(RegistrationNo, RegistrationNoMergeTo, TransactionNo, SequenceNo, TariffComponentID) {
                var oWnd = $find("<%= winPaymentInfo.ClientID %>");
                oWnd.setUrl("PaymentInfoDialog.aspx?RegistrationNo=" + RegistrationNo
                    + "&RegistrationNoMergeTo=" + RegistrationNoMergeTo + "&TransactionNo=" + TransactionNo + "&SequenceNo=" + SequenceNo
                    + "&TariffComponentID=" + TariffComponentID
                );
                oWnd.set_title("Payment Information");
                oWnd.show();
                //oWnd.add_pageLoad(onClientPageLoad);
            }

            function OpenFeeInfo(TransactionNo, SequenceNo, TariffComponentID, ParamedicID, IsPhysicianMember) {
                var oWnd = $find("<%= winPaymentInfo.ClientID %>");
                oWnd.setUrl("FeeInfoDialog.aspx?TransactionNo=" + TransactionNo + "&SequenceNo=" + SequenceNo
                    + "&TariffComponentID=" + TariffComponentID
                    + "&ParamedicID=" + ParamedicID + "&IsPhysicianMember=" + IsPhysicianMember
                );
                oWnd.set_title("Detail Fee Information");
                oWnd.show();
                //oWnd.add_pageLoad(onClientPageLoad);
            }

            function writeOffSelected() {
                var reason = prompt("Please enter deletion reason", "");
                if (reason == null || reason.trim() === "") {
                    alert("Write Off cancelled, reason is required.");
                    return false;
                }

                __doPostBack("<%= grdList.UniqueID %>", "deleteFeeAll|" + reason);
                return false; 
            }

        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Move, Close"
        Width="600px" Height="250px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" OnClientClose="onClientClose" />
    <telerik:RadWindow runat="server" ID="winPaymentInfo" Animation="None" Behaviors="Move, Close, Resize"
        Width="1200px" Height="500px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" />
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDischargeDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnPaymentDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnInvoiceDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterItemGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterNamaLayanan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterSU">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterInvoiceNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPaymentNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterComp">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPhy">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterGuarantorID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterGuarantorType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterUnapprovedVerif">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListV" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterVerifNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListV" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnPlanningPaymentDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListV" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListV">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListV" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkUnapprovedOnly">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboPhysicianUnapproved" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Verify" Value="generate" ImageUrl="~/Images/Toolbar/process16.png"
                HoveredImageUrl="~/Images/Toolbar/process16_h.png" DisabledImageUrl="~/Images/Toolbar/process16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="40%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="Discharge Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDischargeDateFrom" runat="server" Width="100px" />
                                    </td>
                                    <td>-&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDischargeDateTo" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterDischargeDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPaymentReceive" runat="server" Text="Payment Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPaymentDateFrom" runat="server" Width="100px" />
                                    </td>
                                    <td>-&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPaymentDateTo" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnPaymentDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtInvoiceDateFrom" runat="server" Width="100px" />
                                    </td>
                                    <td>-&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtInvoiceDateTo" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnInvoiceDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Registration Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:CheckBoxList ID="cblRegType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="IPR" Text="Inpatient" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="OPR" Text="Outpatient" Selected="True"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterRegType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnit" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterSU" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="40%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemGroup" runat="server" Text="Item Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboItemGroupID" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboItemGroupID_ItemDataBound"
                                OnItemsRequested="cboItemGroupID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterItemGroup" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Item Name (like)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNamaLayanan" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterNamaLayanan" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="Registration No / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label10" runat="server" Text="Invoice No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInvoiceNo" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterInvoiceNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label13" runat="server" Text="Payment / Invoice Payment No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPaymentNo" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterPaymentNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label11" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboGuarantorID_ItemDataBound"
                                OnItemsRequested="cboGuarantorID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterGuarantorID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label12" runat="server" Text="Guarantor Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboGuarantorType" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterGuarantorType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <ItemTemplate>
                                <asp:Button ID="btnWriteOff" runat="server" Text="Write Off"
                                    OnClientClick="return writeOffSelected();" />
                            </ItemTemplate>             
                        </td>
                        <td width="20"></td>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20%" valign="top">
                <telerik:RadTabStrip ID="rtsOpsi" runat="server" MultiPageID="rtsOpsiPages" ShowBaseLine="true"
                    Orientation="HorizontalTop">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Tarif Components" PageViewID="pgOpsiTC"
                            Selected="True">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Physician Types" PageViewID="pgOpsiPT">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="rtsOpsiPages" runat="server" BorderStyle="Solid" BorderColor="Gray">
                    <telerik:RadPageView ID="pgOpsiTC" runat="server" Selected="true">
                        <div style="height: 140px; overflow: scroll; overflow-x: hidden;">
                            <asp:CheckBoxList ID="cblComp" runat="server">
                            </asp:CheckBoxList>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgOpsiPT" runat="server">
                        <div style="height: 140px; overflow: scroll; overflow-x: hidden;">
                            <asp:CheckBoxList ID="cblPhy" runat="server">
                            </asp:CheckBoxList>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Fee Calculation Outstanding" PageViewID="pgO"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Verification" PageViewID="pgV">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgO" runat="server" Selected="true">
            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
                EnableEmbeddedScripts="false" HorizontalAlign="NotSet">
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                    OnItemDataBound="grdList_ItemDataBound" AllowPaging="False" AllowSorting="true"
                    ShowStatusBar="true">
                    <MasterTableView TableLayout="Fixed" DataKeyNames="TransactionNo,SequenceNo,TariffComponentID,ParamedicID"
                        ClientDataKeyNames="TransactionNo,SequenceNo,TariffComponentID,ParamedicID" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridCheckBoxColumn DataField="IsModified" HeaderText="IsModified" UniqueName="IsModified"
                                SortExpression="IsModified" Visible="false">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="Corrected" HeaderText="Corrected" UniqueName="Corrected"
                                SortExpression="Corrected" Visible="false">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="DischargeDate" HeaderText="Discharge Date"
                                UniqueName="DischargeDate" SortExpression="DischargeDate" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                                UniqueName="RegistrationNo" SortExpression="RegistrationNo" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="130px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                                SortExpression="MedicalNo" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                                SortExpression="PatientName" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="PatientNameMix" HeaderText="Patient Name"
                                SortExpression="PatientName">
                                <ItemTemplate>
                                    <%# string.Format("{0}<br />{1}&nbsp;{2}", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                        DataBinder.Eval(Container.DataItem, "MedicalNo"),
                                        DataBinder.Eval(Container.DataItem, "PatientName"))%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="refToRegistration_LOS" HeaderText="LOS" UniqueName="refToRegistration_LOS"
                                SortExpression="refToRegistration_LOS">
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="GuarantorSEP" HeaderText="Guarantor /<br />SEP"
                                SortExpression="GuarantorSEP">
                                <ItemTemplate>
                                    <%# string.Format("{0}<br />{1}",
                                        DataBinder.Eval(Container.DataItem, "GuarantorName"),
                                        DataBinder.Eval(Container.DataItem, "BpjsSepNo"))%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="TotalPayment" HeaderText="Total Billing" UniqueName="TotalPayment"
                                SortExpression="TotalPayment" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridHyperLinkColumn DataTextField="PaymentMethodName" HeaderText="Payment Method" UniqueName="PaymentMethodName"
                                DataNavigateUrlFields="RegistrationNo,RegistrationNoMergeTo,TransactionNo,SequenceNo,TariffComponentID"
                                DataNavigateUrlFormatString="javascript:OpenPaymentInfo('{0}','{1}','{2}','{3}','{4}');">
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ExecutionDate" HeaderText="Trans Date"
                                UniqueName="ExecutionDate" SortExpression="ExecutionDate" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="80px" UniqueName="ExecuteDatePaymentDate"
                                HeaderText="Exec Date <br /> [Payment]">
                                <ItemTemplate>
                                    <%# string.Format("{0}<br />{1}<br />{2}",
                                        ((DateTime?)DataBinder.Eval(Container.DataItem, "ExecutionDate")).Value.ToString("M/d/yyyy"),
                                        (DataBinder.Eval(Container.DataItem, "LastPaymentDate")) is DBNull ? string.Empty : ("[" +
                                        ((DateTime?)DataBinder.Eval(Container.DataItem, "LastPaymentDate")).Value.ToString("M/d/yyyy") + "]"),
                                        (DataBinder.Eval(Container.DataItem, "InvoiceDate")) is DBNull ? string.Empty : ("[Inv. " +
                                        ((DateTime?)DataBinder.Eval(Container.DataItem, "InvoiceDate")).Value.ToString("M/d/yyyy") + "]")
                                        )%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                                SortExpression="TransactionNo" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="ParamedicNameMix" HeaderText="Physician"
                                SortExpression="ParamedicName">
                                <ItemTemplate>
                                    <%# string.Format("{0}<br />{1}",
                                        DataBinder.Eval(Container.DataItem, "ParamedicName"),
                                        DataBinder.Eval(Container.DataItem, "TariffComponentName"))%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                SortExpression="ParamedicName" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="IsPaidOff" HeaderStyle-Width="40px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerPaidChkbox" OnCheckedChanged="ToggleSelectedPaidState" AutoPostBack="True"
                                        runat="server" /><br />
                                    <span>Paid</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="paidChkbox" runat="server" Checked='<%# (bool)DataBinder.Eval(Container.DataItem, "IsPaidOff") %>'
                                        Enabled="false"></asp:CheckBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridCheckBoxColumn DataField="IsGuarantorVerified" HeaderText="V" UniqueName="IsGuarantorVerified"
                                SortExpression="IsGuarantorVerified">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                        runat="server" /><br />
                                    <span>&nbsp;</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="detailChkbox" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                SortExpression="ItemName">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class Name" UniqueName="ClassName"
                                SortExpression="ClassName">
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Qty" HeaderText="Qty" UniqueName="Qty" SortExpression="Qty"
                                DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PriceItem" HeaderText="Price" UniqueName="PriceItem"
                                SortExpression="PriceItem" DataFormatString="{0:n2}" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DiscountItem" HeaderText="Discount" UniqueName="DiscountItem"
                                SortExpression="DiscountItem" DataFormatString="{0:n2}" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Price_Discount" HeaderText="Price /<br /> Discount"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <%# string.Format("{0}<br />{1}", 
                                        ((decimal)DataBinder.Eval(Container.DataItem, "PriceItem")).ToString("n2"),
                                        ((decimal)DataBinder.Eval(Container.DataItem, "DiscountItem") / (decimal)DataBinder.Eval(Container.DataItem, "Qty") + (decimal)DataBinder.Eval(Container.DataItem, "DiscountExtra")).ToString("n2"))%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Discount" HeaderText="Discount" UniqueName="Discount"
                                SortExpression="Discount" DataFormatString="{0:n2}" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IsCalculatedInPercent" HeaderText="%" UniqueName="IsCalculatedInPercent"
                                SortExpression="IsCalculatedInPercent" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Price" HeaderText="Physician Comp. Price" UniqueName="Price"
                                SortExpression="Price" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CalculatedAmount" HeaderText="Share" UniqueName="CalculatedAmount"
                                SortExpression="CalculatedAmount" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="FeeAmountTemplate" HeaderText="Physician Fee"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <%# (System.Convert.ToInt32(string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "SRPhysicianFeeCategory").ToString())?"0":DataBinder.Eval(Container.DataItem, "SRPhysicianFeeCategory").ToString()) >= 6) ? 
                                            string.Format("<a href=\"javascript:void(0);\" onclick=\"javascript:OpenFeeInfo('{1}','{2}','{3}','{4}','{5}')\">{0}</a>", 
                                                ((DataBinder.Eval(Container.DataItem, "FeeAmount")) is DBNull ? "" : ((decimal)DataBinder.Eval(Container.DataItem, "FeeAmount")).ToString("n2")), 
                                                DataBinder.Eval(Container.DataItem, "TransactionNo"),
                                                DataBinder.Eval(Container.DataItem, "SequenceNo"),
                                                DataBinder.Eval(Container.DataItem, "TariffComponentID"),
                                                DataBinder.Eval(Container.DataItem, "ParamedicID"),
                                                DataBinder.Eval(Container.DataItem, "IsPhysicianMember")) :
                                            ((DataBinder.Eval(Container.DataItem, "FeeAmount")) is DBNull ? "" : ((decimal)DataBinder.Eval(Container.DataItem, "FeeAmount")).ToString("n2"))
                                    %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="DeductionAmount" HeaderText="Deduction" UniqueName="DeductionAmount"
                                SortExpression="DeductionAmount" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                FooterAggregateFormatString="{0:n2}" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SumDeductionAmount" HeaderText="Deduction Before Tax"
                                UniqueName="SumDeductionAmount" SortExpression="SumDeductionAmount" DataFormatString="{0:n2}"
                                Aggregate="Sum" FooterStyle-HorizontalAlign="Right" FooterAggregateFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="SequenceNo" UniqueName="SequenceNo"
                                SortExpression="SequenceNo" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TariffComponentID" HeaderText="TariffComponentID"
                                UniqueName="TariffComponentID" SortExpression="TariffComponentID" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ParamedicID" HeaderText="ParamedicID" UniqueName="ParamedicID"
                                SortExpression="ParamedicID" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "SequenceNo").ToString().Equals("00") ? "" :
                                       (string.Format("<a href=\"#\" onclick=\"openDialog('{0}', '{1}', '{2}', '{3}', '{4}','{5}'); return false;\">{6}</a>",
                                       DataBinder.Eval(Container.DataItem, "RegistrationNo"),                                   
                                       DataBinder.Eval(Container.DataItem, "TransactionNo"), 
                                       DataBinder.Eval(Container.DataItem, "SequenceNo"), 
                                       DataBinder.Eval(Container.DataItem, "TariffComponentID"),
                                       DataBinder.Eval(Container.DataItem, "ParamedicID"),
                                       DataBinder.Eval(Container.DataItem, "IsPhysicianMember"),
                                       "<img src=\"../../../../Images/Toolbar/dokter16.png\" border=\"0\" title=\"Update Physician\" />"))%>
                                    <hr />
                                    <%# DataBinder.Eval(Container.DataItem, "SequenceNo").ToString().Equals("00") ? "" : 
                                           (string.Format("<a href=\"#\" onclick=\"openDialogShare('{0}', '{1}', '{2}', '{3}', '{4}','{5}'); return false;\">{6}</a>",
                                       DataBinder.Eval(Container.DataItem, "RegistrationNo"),                                   
                                       DataBinder.Eval(Container.DataItem, "TransactionNo"), 
                                       DataBinder.Eval(Container.DataItem, "SequenceNo"), 
                                       DataBinder.Eval(Container.DataItem, "TariffComponentID"),
                                       DataBinder.Eval(Container.DataItem, "ParamedicID"),
                                       DataBinder.Eval(Container.DataItem, "IsPhysicianMember"),
                                       "<img src=\"../../../../Images/rp16.png\" border=\"0\" title=\"Update Share\" />"))%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "SequenceNo").ToString().Equals("00") ? "" : 
                                        (string.Format("<a href=\"#\" onclick=\"openDialogNotes('{0}', '{1}', '{2}', '{3}', '{4}','{5}'); return false;\">{6}</a>",
                                       DataBinder.Eval(Container.DataItem, "RegistrationNo"),                                   
                                       DataBinder.Eval(Container.DataItem, "TransactionNo"), 
                                       DataBinder.Eval(Container.DataItem, "SequenceNo"), 
                                       DataBinder.Eval(Container.DataItem, "TariffComponentID"),
                                       DataBinder.Eval(Container.DataItem, "ParamedicID"),
                                       DataBinder.Eval(Container.DataItem, "IsPhysicianMember"),
                                       DataBinder.Eval(Container.DataItem, "Notes").ToString().Equals(string.Empty) ? "<img src=\"../../../../Images/stickynote16.png\" border=\"0\" title=\"Notes\" />" : "<img src=\"../../../../Images/todolist16.png\" border=\"0\" title=\"Notes\" />"))%>
                                    <hr />
                                    <%# 
                                       DataBinder.Eval(Container.DataItem, "SequenceNo").ToString().Equals("00") ? "" :
                                       (string.Format("<a href=\"#\" onclick=\"{0}('{1}', '{2}', '{3}', '{4}','{5}'); return false;\">{6}</a>",
                                           (IsFeeCalculatedOnTransaction() ? "writeOffFee" : "deleteFee"),
                                           DataBinder.Eval(Container.DataItem, "TransactionNo"),
                                           DataBinder.Eval(Container.DataItem, "SequenceNo"),
                                           DataBinder.Eval(Container.DataItem, "TariffComponentID"),
                                           DataBinder.Eval(Container.DataItem, "ParamedicID"),
                                           DataBinder.Eval(Container.DataItem, "IsPhysicianMember"),
                                           string.Format("<img src=\"../../../../Images/Toolbar/delete16.png\" border=\"0\" title=\"{0}\" />",
                                           (IsFeeCalculatedOnTransaction() ? "Write Off" : "Delete Fee For Re-Calculation"))
                                           ))
                                    %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridCheckBoxColumn DataField="IsPhysicianMember" HeaderText="IsPhysicianMember" UniqueName="IsPhysicianMember"
                                SortExpression="IsPhysicianMember" Visible="false">
                            </telerik:GridCheckBoxColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings>
                        <Resizing AllowColumnResize="True" />
                        <Scrolling AllowScroll="False" UseStaticHeaders="False" />
                    </ClientSettings>
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgV" runat="server">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="35%">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label7" runat="server" Text="Unapproved Only"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <asp:CheckBox ID="chkUnapprovedOnly" Checked="false" runat="server" OnCheckedChanged="chkUnapprovedOnly_CheckedChanged" AutoPostBack="true" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:ImageButton ID="btnFilterUnapprovedVerif" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                    OnClick="btnFilter_Click" ToolTip="Search" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label15" runat="server" Text="Physician (Unapproved)"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox runat="server" ID="cboPhysicianUnapproved" Enabled="false" Width="300px"></telerik:RadComboBox>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                    OnClick="btnFilter_Click" ToolTip="Search" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="35%">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label8" runat="server" Text="Verification No"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadTextBox ID="txtVerificationNo" runat="server" Width="300px" MaxLength="20" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:ImageButton ID="btnFilterVerifNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                    OnClick="btnFilter_Click" ToolTip="Search" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="30%">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label14" runat="server" Text="Planning Payment Date"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtPlanningPaymentDate" runat="server" Width="100px" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnPlanningPaymentDate" runat="server" ImageUrl="~/Images/Toolbar/save16.png"
                                                                OnClick="btnPlanningPaymentDate_Click" ToolTip="Save Planning Payment Date" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="grdListV" runat="server" OnNeedDataSource="grdListV_NeedDataSource"
                            AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
                            <MasterTableView DataKeyNames="VerificationNo">
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Chk" runat="server" Checked="false" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedStateChk" AutoPostBack="True"
                                                runat="server" /><br />
                                            <span>&nbsp;</span>
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="VerificationNo"
                                        DataNavigateUrlFields="VerUrl" HeaderText="Verification No" UniqueName="VerificationNo"
                                        SortExpression="VerificationNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="VerificationDate"
                                        HeaderText="Verification Date" UniqueName="VerificationDate" SortExpression="VerificationDate"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ApprovedDate" HeaderText="Approve Date"
                                        UniqueName="ApprovedDate" SortExpression="ApprovedDate" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PlanningPaymentDate" HeaderText="Planning Payment Date"
                                        UniqueName="PlanningPaymentDate" SortExpression="PlanningPaymentDate" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                        SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridTemplateColumn UniqueName="BankAccount" HeaderText="Bank Account" HeaderStyle-Width="150px">
                                        <ItemTemplate>
                                            <%# string.Format("{0}[{1}]", 
                                                DataBinder.Eval(Container.DataItem, "Bank"),
                                                DataBinder.Eval(Container.DataItem, "BankAccount"))%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="VerificationAmount" HeaderText="Verification Amount"
                                        UniqueName="VerificationAmount" SortExpression="VerificationAmount" DataFormatString="{0:n2}">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TaxAmount" HeaderText="Tax Amount" UniqueName="TaxAmount"
                                        SortExpression="TaxAmount" DataFormatString="{0:n2}">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SumDeductionAmountAfterTax" HeaderText="Deduction After Tax"
                                        UniqueName="SumDeductionAmountAfterTax" SortExpression="SumDeductionAmountAfterTax"
                                        DataFormatString="{0:n2}">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                                        UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridTemplateColumn UniqueName="accept" HeaderText="" Groupable="false">
                                        <ItemTemplate>
                                            <%# ((bool)DataBinder.Eval(Container.DataItem, "IsApproved")) ? "" : string.Format("<a href=\"#\" onclick=\"rowApproved('{0}'); return false;\">{1}</a>",
                                                                                        DataBinder.Eval(Container.DataItem, "VerificationNo"),
                                            "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" title=\"Approve\" />")%>
                                            <%# ((bool)DataBinder.Eval(Container.DataItem, "IsApproved") && IsUnapprovable()) ? string.Format("<a href=\"#\" onclick=\"rowUnApproved('{0}'); return false;\">{1}</a>",
                                                                                        DataBinder.Eval(Container.DataItem, "VerificationNo"),
                                                                                        "<img src=\"../../../../Images/Toolbar/undo16.png\" border=\"0\" title=\"UnApprove\" />") : "" %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                        <HeaderTemplate>
                                            <%# string.Format("<a href=\"#\" onclick=\"pageApproved(); return false;\">{0}</a>",
                                            "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" title=\"Approve\" />")%>
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
