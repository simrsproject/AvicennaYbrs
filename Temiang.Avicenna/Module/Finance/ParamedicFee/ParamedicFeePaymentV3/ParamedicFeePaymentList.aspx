<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="ParamedicFeePaymentList.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeePaymentV3.ParamedicFeePaymentList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "generate":
                        if (confirm('Are you sure want to do payment process for selected item(s)?'))
                            __doPostBack("<%= grdList.UniqueID %>", val);
                        break;
                    default :
                        __doPostBack("<%= grdList.UniqueID %>", val);
                        break;
                }
            }

            function openDialogShare(regNo, transNo, seqNo, compId, parId) {
                var oWnd = $find("<%= winDialog.ClientID %>");

                oWnd.setUrl("ParamedicFeePaymentChangeShareDialog.aspx?regno=" + regNo + "&trno=" + transNo + "&seqno=" + seqNo + "&compid=" + compId + "&parid=" + parId);
                oWnd.show();
            }

            function openDialogDetail(parid) {
                var oWnd = $find("<%= winDialog.ClientID %>");

                var startdate = $find("<%= txtPaymentDateFrom.ClientID %>").get_selectedDate();
                var enddate = $find("<%= txtPaymentDateTo.ClientID %>").get_selectedDate();
                //alert("ParamedicFeePaymentDetail.aspx?parid=" + parid + "&startdate=" + startdate + "&enddate=" + enddate);
                var aDate = ''; var bDate = '';
                if (startdate != null)
                    aDate = (startdate.getMonth() + 1) + '/' + startdate.getDate() + '/' + startdate.getFullYear();
                if (enddate != null)
                    bDate = (enddate.getMonth() + 1) + '/' + enddate.getDate() + '/' + enddate.getFullYear();

                var startdateDischarge = $find("<%= txtDischargeDateFrom.ClientID %>").get_selectedDate();
                var enddateDischarge = $find("<%= txtDischargeDateTo.ClientID %>").get_selectedDate();
                //alert("ParamedicFeePaymentDetail.aspx?parid=" + parid + "&startdate=" + startdate + "&enddate=" + enddate);
                var cDate = ''; var dDate = '';
                if (startdateDischarge != null)
                    cDate = (startdateDischarge.getMonth() + 1) + '/' + startdateDischarge.getDate() + '/' + startdateDischarge.getFullYear();
                if (enddateDischarge != null)
                    dDate = (enddateDischarge.getMonth() + 1) + '/' + enddateDischarge.getDate() + '/' + enddateDischarge.getFullYear();

                var planningPaymentDate = $find("<%= txtPlanningPaymentDate.ClientID %>").get_selectedDate();
                var eDate = '';
                if (planningPaymentDate != null)
                    eDate = (planningPaymentDate.getMonth() + 1) + '/' + planningPaymentDate.getDate() + '/' + planningPaymentDate.getFullYear();

                var regno = $find("<%= txtRegistrationNo.ClientID %>").get_value();
                var mr = $find("<%= txtMedicalNo.ClientID %>").get_value();
                var name = $find("<%= txtPatientName.ClientID %>").get_value();

                var guarid = $find('<%=cboGuarantorID.ClientID %>').get_value();
                var srguartype = $find('<%=cboGuarantorType.ClientID %>').get_value();

                var payNo = $find("<%= txtPaymentGroupNo.ClientID %>").get_value();

                //alert("ParamedicFeePaymentDetail.aspx?parid=" + parid + "&startdate=" + aDate + "&enddate=" + bDate);
                oWnd.setUrl("ParamedicFeePaymentDetail.aspx?parid=" + parid + "&startdate=" + aDate + "&enddate=" + bDate +
                    "&startdatedc=" + cDate + "&enddatedc=" + dDate + "&planningdate=" + eDate +
                    "&regno=" + regno + "&mr=" + mr + "&name=" + name + "&guarid=" + guarid + "&srguartype=" + srguartype + "&payno=" + payNo);
                
                oWnd.show();
                oWnd.add_pageLoad(onClientPageLoad);
            }
            
            function rowPaymentPrint(paymentGroupNo, parId) {
                __doPostBack("<%= grdList.UniqueID %>", 'print|' + paymentGroupNo);
            }

            function rowPrint(paymentGroupNo, parId) {
                __doPostBack("<%= grdList.UniqueID %>", 'print|' + paymentGroupNo + '|' + parId);
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument == 'rebind') {
                    __doPostBack("<%= grdList.UniqueID %>", "rebind");
                    oWnd.argument = 'undefined';
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Maximize, Move, Close"
        Width="1000px" Height="600px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" OnClientClose="onClientClose" />
     <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnPaymentDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDischargeDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnPlanningPaymentDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnMedicalNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnPatientName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterGuarantorID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterGuarantorType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterVerifNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterComp">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnCalcGuaranteeFee">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtPaymentAmount" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />
                    <telerik:AjaxUpdatedControl ControlID="txtPaymentAmount" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnReset">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />

                    <telerik:AjaxUpdatedControl ControlID="txtPaymentGroupNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtPaymentDate" />
                    <telerik:AjaxUpdatedControl ControlID="txtPaymentAmount" />
                    <telerik:AjaxUpdatedControl ControlID="cboPaymentMethodID" />
                    <telerik:AjaxUpdatedControl ControlID="cboBankID" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnFilterChkUnapprovedOnlyN">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPaymentGroupNoN">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListVGN">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListVGN" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />

                    <telerik:AjaxUpdatedControl ControlID="txtPaymentGroupNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtPaymentDate" />
                    <telerik:AjaxUpdatedControl ControlID="txtPaymentAmount" />
                    <telerik:AjaxUpdatedControl ControlID="cboPaymentMethodID" />
                    <telerik:AjaxUpdatedControl ControlID="cboBankID" />

                    <telerik:AjaxUpdatedControl ControlID="rdpGFeeFrom" />
                    <telerik:AjaxUpdatedControl ControlID="rdpGFeeTo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Process Payment" Value="generate" ImageUrl="~/Images/Toolbar/process16.png"
                HoveredImageUrl="~/Images/Toolbar/process16_h.png" DisabledImageUrl="~/Images/Toolbar/process16_d.png" />
        </Items>
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Save As Draft" Value="saveDraft" ImageUrl="~/Images/Toolbar/save16.png"
                HoveredImageUrl="~/Images/Toolbar/save16_h.png" DisabledImageUrl="~/Images/Toolbar/save16_d.png" />
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
                            <asp:Label ID="Label9" runat="server" Text="Patient / Guarantor Payment Date"></asp:Label>
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
                            <asp:Label ID="Label1" runat="server" Text="Discharge Date"></asp:Label>
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
                            <asp:ImageButton ID="btnDischargeDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label10" runat="server" Text="Planning Payment Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtPlanningPaymentDate" runat="server" Width="100px" />
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnPlanningPaymentDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px"></telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                                OnItemsRequested="cboParamedicID_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="40%" valign="top">
                <table width="100%">
                    
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px"></telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnMedicalNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px"></telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnPatientName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="Guarantor"></asp:Label>
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
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Guarantor Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboGuarantorType" Width="300px" >
                            </telerik:RadComboBox>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterGuarantorType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label14" runat="server" Text="Verification No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtVerificationNo" runat="server" Width="300px"></telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterVerifNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Fee Outstanding" PageViewID="pgO"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Paid" PageViewID="pgVN">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgO" runat="server" Selected="true">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="40%">
                                    <table width="100%">
                                        <tr id="trDraftNo" runat="server">
                                            <td class="label">
                                                <asp:Label ID="Label13" runat="server" Text="Payment Group No"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadTextBox ID="txtPaymentGroupNo" runat="server" Width="300px" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                            <td width="20px">
                                                <asp:ImageButton ID="btnReset" runat="server" ImageUrl="~/Images/Toolbar/new16.png"
                                                    OnClick="btnReset_Click" ToolTip="Reset" />
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblPaymentDate" runat="server" Text="Payment Date"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadDatePicker ID="txtPaymentDate" runat="server" Width="100px" />
                                            </td>
                                            <td width="20px">
                                                <asp:RequiredFieldValidator ID="rfvPaymentDate" runat="server" ErrorMessage="Payment Date required."
                                                    ValidationGroup="entry" ControlToValidate="txtPaymentDate" SetFocusOnError="True"
                                                    Width="100%">
                                                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblPaymentAmount" runat="server" Text="Payment Amount"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadNumericTextBox ID="txtPaymentAmount" runat="server" Width="100px" MaxLength="16"
                                                    ReadOnly="true" />
                                            </td>
                                            <td width="20"></td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="40%" valign="top">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblPaymentMethodeID" runat="server" Text="Payment Method"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox ID="cboPaymentMethodID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                    HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboPaymentMethodID_ItemDataBound"
                                                    OnItemsRequested="cboPaymentMethodID_ItemsRequested">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20">
                                                <asp:RequiredFieldValidator ID="rfvPaymentMethodID" runat="server" ErrorMessage="Payment Method required."
                                                    ValidationGroup="entry" ControlToValidate="cboPaymentMethodID" SetFocusOnError="True"
                                                    Width="100%">
                                                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="lblBankID" runat="server" Text="Bank"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadComboBox ID="cboBankID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                    HighlightTemplatedItems="true" OnItemDataBound="cboBankID_ItemDataBound"
                                                    OnItemsRequested="cboBankID_ItemsRequested">
                                                    <FooterTemplate>
                                                        Note : Show max 20 items
                                                    </FooterTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="20">
                                                <asp:RequiredFieldValidator ID="rfvBankID" runat="server" ErrorMessage="Bank required."
                                                    ValidationGroup="entry" ControlToValidate="cboBankID" SetFocusOnError="True"
                                                    Width="100%">
                                                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="40%" valign="top" runat="server" id="tdGuaranteeFee">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <fieldset>
                                                    <legend><asp:CheckBox ID="chkEnableGuaranteeFee" runat="server" /> Guarantee Fee </legend>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>Payment Period</td>
                                                            <td>&nbsp;</td>
                                                            <td><telerik:RadDatePicker ID="rdpGFeeFrom" runat="server" Width="100px" /></td>
                                                            <td><telerik:RadDatePicker ID="rdpGFeeTo" runat="server" Width="100px" /></td>
                                                            <td>&nbsp;</td>
                                                            <td><asp:ImageButton ID="btnCalcGuaranteeFee" runat="server" ImageUrl="~/Images/Toolbar/process16.png"
                                                    OnClick="btnCalcGuaranteeFee_Click" ToolTip="Calculate Guarantee Fee" /></td>
                                                        </tr>
                                                    </table>
                                                    
                                                </fieldset>
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
                        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
                            EnableEmbeddedScripts="false" HorizontalAlign="NotSet">
                            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                                OnItemDataBound="grdList_ItemDataBound"
                                AllowPaging="False" AllowSorting="true" ShowStatusBar="true">
                                <MasterTableView DataKeyNames="ParamedicID" ClientDataKeyNames="ParamedicID" AutoGenerateColumns="false">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                                    runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="detailChkbox" runat="server" AutoPostBack="True" OnCheckedChanged="ChkChanged"></asp:CheckBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="ParamedicName" HeaderText="Paramedic Name">
                                            <ItemTemplate>
                                                <%# string.Format("<a href='#' onclick='javascript:openDialogDetail(\"{0}\")'>{1} [{0}]</a>", 
                                                    DataBinder.Eval(Container.DataItem, "ParamedicID"),
                                                    DataBinder.Eval(Container.DataItem, "ParamedicName"))%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Fee4Service" HeaderText="Service Fee" UniqueName="Fee4Service"
                                            SortExpression="Fee4Service" DataFormatString="{0:n2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Fee4ServiceSelected" HeaderText="Service Fee Selected" UniqueName="Fee4ServiceSelected"
                                            SortExpression="Fee4ServiceSelected" DataFormatString="{0:n2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" BackColor="LightBlue" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FeeAddDec" HeaderText="Add / Dec" UniqueName="FeeAddDec"
                                            SortExpression="FeeAddDec" DataFormatString="{0:n2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FeeAddDecSelected" HeaderText="Add / Dec Selected" UniqueName="FeeAddDecSelected"
                                            SortExpression="FeeAddDecSelected" DataFormatString="{0:n2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" BackColor="LightBlue" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FeeGuarantee" HeaderText="Guarantee Fee" UniqueName="FeeGuarantee"
                                            SortExpression="FeeGuarantee" DataFormatString="{0:n2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <FilterMenu>
                                </FilterMenu>
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Resizing AllowColumnResize="True" />
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </telerik:RadAjaxPanel>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgVN" runat="server">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="50%" valign="top">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label11" runat="server" Text="Unapproved Only"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <asp:CheckBox ID="chkUnapprovedOnlyN" Checked="false" runat="server" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:ImageButton ID="btnFilterChkUnapprovedOnlyN" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                    OnClick="btnFilter_Click" ToolTip="Search" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="50%">
                                    <table width="100%">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label12" runat="server" Text="Payment Group No"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadTextBox ID="txtPaymentGroupNoN" runat="server" Width="300px" MaxLength="20" />
                                            </td>
                                            <td style="text-align: left">
                                                <asp:ImageButton ID="btnFilterPaymentGroupNoN" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                    OnClick="btnFilter_Click" ToolTip="Search" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label8" runat="server" Text="Payment Date"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <telerik:RadDatePicker ID="rdpPaidDate" runat="server" Width="100px" />
                                            </td>
                                            <td style="text-align: left">
                                                
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
                        <telerik:RadGrid ID="grdListVGN" runat="server" OnNeedDataSource="grdListVGN_NeedDataSource"
                            OnDetailTableDataBind="grdListVGN_DetailTableDataBind"
                            AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
                            <MasterTableView DataKeyNames="PaymentGroupNo">
                                <Columns>
                                    <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="35px">
                                        <ItemTemplate>
                                            <%# (bool)DataBinder.Eval(Container.DataItem, "IsVoid") ? "": string.Format("<a href=\"#\" onclick=\"rowPaymentPrint('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"Print Physician Service Fee\" title=\"Print Physician Service Fee\" /></a>",
                                                                            DataBinder.Eval(Container.DataItem, "PaymentGroupNo"))%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="IsDraft" HeaderText="" Groupable="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="iBtnEditDraft" runat="server"
                                                ImageUrl='<%# "../../../../Images/Toolbar/edit16.png" %>'
                                                Visible='<%# (bool)DataBinder.Eval(Container.DataItem, "IsDraft") && !(bool)DataBinder.Eval(Container.DataItem, "IsVoid") %>'
                                                ToolTip='Edit Draft' />
                                            <asp:ImageButton ID="iBtnUndoToDraft" runat="server"
                                                OnClientClick="if(!confirm('Are you sure want to undo to draft?')){ return false; };"
                                                ImageUrl='<%# "../../../../Images/Toolbar/undo16.png" %>'
                                                Visible='<%# !(bool)DataBinder.Eval(Container.DataItem, "IsDraft") && !(bool)DataBinder.Eval(Container.DataItem, "IsApprove") && !(bool)DataBinder.Eval(Container.DataItem, "IsVoid") %>'
                                                ToolTip='Undo to Draft' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="130px" DataField="PaymentGroupNo"
                                        HeaderText="Payment Group No" UniqueName="PaymentGroupNo" SortExpression="PaymentGroupNo"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PaymentDate"
                                        HeaderText="Payment Date" UniqueName="PaymentDate" SortExpression="PaymentDate"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ApproveDateTime"
                                        HeaderText="Approve Date" UniqueName="ApproveDateTime" SortExpression="ApproveDateTime"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridDateTimeColumn DataField="PaymentMethodName"
                                        HeaderText="Payment Method Name" UniqueName="PaymentMethodName" SortExpression="PaymentMethodName"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridDateTimeColumn DataField="BankName"
                                        HeaderText="Bank Name" UniqueName="BankName" SortExpression="BankName"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="FeeAmountBeforeTax" HeaderText="Amount Before Tax"
                                        UniqueName="FeeAmountBeforeTax" SortExpression="FeeAmountBeforeTax" DataFormatString="{0:n2}">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TaxOnPaymentAmount" HeaderText="Tax Amount"
                                        UniqueName="TaxOnPaymentAmount" SortExpression="TaxOnPaymentAmount" DataFormatString="{0:n2}">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PaymentAmount" HeaderText="Payment Amount"
                                        UniqueName="PaymentAmount" SortExpression="PaymentAmount" DataFormatString="{0:n2}">
                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApprove" HeaderText="Approved"
                                        UniqueName="IsApprove" SortExpression="IsApprove" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsVoid" HeaderText="Void"
                                        UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridTemplateColumn UniqueName="IsApprove" HeaderText="" Groupable="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="iBtnApprovePaymentGroup" runat="server"
                                                OnClientClick="if(!confirm('Are you sure want to approve?')){ return false; };"
                                                CommandName='<%# ((bool)DataBinder.Eval(Container.DataItem, "IsApprove")) ? "Unapproval":"Approval" %>'
                                                ImageUrl='<%# ((bool)DataBinder.Eval(Container.DataItem, "IsApprove")) ? "../../../../Images/Toolbar/undo16.png":"../../../../Images/Toolbar/post16.png" %>'
                                                Visible='<%# ((!(bool)DataBinder.Eval(Container.DataItem, "IsDraft")) &&((((bool)DataBinder.Eval(Container.DataItem, "IsApprove")) && IsUnapprovable()) || !((bool)DataBinder.Eval(Container.DataItem, "IsApprove"))) && !(bool)DataBinder.Eval(Container.DataItem, "IsVoid")) %>'
                                                ToolTip='<%# ((bool)DataBinder.Eval(Container.DataItem, "IsApprove")) ? "Payment Unapproval":"Payment Approval" %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                        <HeaderTemplate>
                                            <%# string.Format("<a href=\"#\" onclick=\"pageApproved(); return false;\">{0}</a>",
                                            "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" title=\"Approve\" />")%>
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="IsVoidable" HeaderText="" Groupable="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="iBtnVoidPaymentGroup" runat="server"
                                                OnClientClick="if(!confirm('Are you sure want to delete?')){ return false; };"
                                                CommandName="Void"
                                                ImageUrl="../../../../Images/Toolbar/cancel16.png"
                                                Visible='<%# (!(bool)DataBinder.Eval(Container.DataItem, "IsApprove") && IsVoidable() && !(bool)DataBinder.Eval(Container.DataItem, "IsVoid")) %>'
                                                ToolTip='Void payment' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <DetailTables>
                                    <telerik:GridTableView Name="detailPaymentPhysician" DataKeyNames="PaymentGroupNo, ParamedicID" AutoGenerateColumns="false">
                                        <Columns>
                                            <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="35px">
                                                <ItemTemplate>
                                                    <%# string.Format("<a href=\"#\" onclick=\"rowPrint('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"Print Physician Service Fee\" title=\"Print Physician Service Fee\" /></a>",
                                                                                   DataBinder.Eval(Container.DataItem, "PaymentGroupNo"), DataBinder.Eval(Container.DataItem, "ParamedicID"))%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="ParamedicName" HeaderText="Paramedic Name">
                                                <ItemTemplate>
                                                    <%# string.Format("{1} [{0}]", 
                                                        DataBinder.Eval(Container.DataItem, "ParamedicID"),
                                                        DataBinder.Eval(Container.DataItem, "ParamedicName"))%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="AmountFee4Service" HeaderText="Amount Fee4Service" UniqueName="AmountFee4Service"
                                                SortExpression="AmountFee4Service" DataFormatString="{0:n2}" >
                                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AmountAddDec" HeaderText="Add/Deduc" UniqueName="AmountAddDec"
                                                SortExpression="AmountAddDec" DataFormatString="{0:n2}" >
                                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AmountGuarantee" HeaderText="Guarantee Fee" UniqueName="AmountGuarantee"
                                                SortExpression="AmountGuarantee" DataFormatString="{0:n2}" >
                                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TaxAmount" HeaderText="Tax Amount" UniqueName="TaxAmount"
                                                SortExpression="TaxAmount" DataFormatString="{0:n2}">
                                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <DetailTables>
                                            <telerik:GridTableView Name="detailPaymentDetail" DataKeyNames="Id" AutoGenerateColumns="false">
                                                <Columns>
                                                    <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="DischargeDate" HeaderText="Discharge Date"
                                                        UniqueName="DischargeDate" SortExpression="DischargeDate" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}" />
                                                    <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                                                        UniqueName="RegistrationNo" SortExpression="RegistrationNo" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                                                        SortExpression="RegistrationNo">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="MedicalNo" UniqueName="MedicalNo"
                                                        SortExpression="MedicalNo">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                                                        SortExpression="PatientName">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                                                        SortExpression="GuarantorName">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                                        SortExpression="ItemName">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Amount" HeaderText="Amount" UniqueName="Amount"
                                                        SortExpression="Amount" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                                        FooterAggregateFormatString="{0:n2}">
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </telerik:GridTableView>
                                        </DetailTables>
                                    </telerik:GridTableView>
                                </DetailTables>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
