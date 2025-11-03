<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="PurchaseOrderDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Procurement.PurchaseOrderDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinPr() {
                var oWnd = $find("<%= winPr.ClientID %>");
                var opr = $find("<%= txtReferenceNo.ClientID %>");
                var oit = $find("<%= cboSRItemType.ClientID %>");
                var opu = $find("<%= cboFromServiceUnitID.ClientID %>");
                var osu = $find("<%= cboBusinessPartnerID.ClientID %>");

                oWnd.setUrl("RequestOrderSelection.aspx?pr=" + opr.get_value() + "&it=" + oit._value + "&pu=" + opu._value + "&su=" + osu._value + '&cons=' + '<%= Request.QueryString["cons"] %>');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd) {
                //Jika apply di click
                var arg = oWnd.argument;
                if (arg)
                    $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>").ajaxRequest("");
            }

            function approvLevel(level) {
                if (!confirm('Approve this transaction, continue ?')) return false;

                __doPostBack("<%= grdApproval.UniqueID %>", "_approv|" + level);
                return false;
            }
            function unApprovLevel(level) {
                if (!confirm('UnApprove this transaction, continue ?')) return false;

                __doPostBack("<%= grdApproval.UniqueID %>", "_unapprov|" + level);
                return false;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" Title="Request Order Pending"
        OnClientClose="onClientClose" ID="winPr" />
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image13" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Purchase Order No" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Purchase Order Date" />
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false" />
                                    </td>
                                    <td style="display: none">
                                        <asp:CheckBox ID="chkIsTaxable" runat="server" Text="Tax" Enabled="true" OnCheckedChanged="chkIsTaxable_CheckedChanged"
                                            AutoPostBack="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBusinessPartnerID" runat="server" Text="Supplier Name" />
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="cboBusinessPartnerID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboSupplierID_ItemDataBound"
                                ValidationGroup="other" AutoPostBack="true" OnSelectedIndexChanged="cboBusinessPartnerID_SelectedIndexChanged"
                                OnItemsRequested="cboSupplier_ItemsRequested" EmptyMessage="Select...">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvBusinessPartnerID" runat="server" ErrorMessage="Supplier Name required."
                                ValidationGroup="entry" ControlToValidate="cboBusinessPartnerID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPBFLicenseNo" runat="server" Text="License No" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtPBFLicenseNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td>
                            <asp:RadioButtonList ID="rblTypesOfTaxes" runat="server" RepeatDirection="Horizontal"
                                OnTextChanged="rblTypesOfTaxes_OnTextChanged" AutoPostBack="true">
                                <asp:ListItem Selected="true">Exclude Tax</asp:ListItem>
                                <asp:ListItem>Include Tax</asp:ListItem>
                                <asp:ListItem>No Tax</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr runat="server" id="trContractNo">
                        <td class="label">
                            <asp:Label ID="lblContractNo" runat="server" Text="Contract No" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtContractNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Purchasing Unit" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboFromServiceUnitID_SelectedIndexChanged" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage="Purchasing Unit required."
                                ValidationGroup="entry" ControlToValidate="cboFromServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReferenceNo" runat="server" Text="From Request Order #" />
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="175px" MaxLength="20"
                                            ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnGetPr" runat="server" Text="Request" OnClientClick="javascript:openWinPr();return false;" />
                                        <asp:Button ID="btnResetPR" runat="server" Text="Reset" OnClick="btnResetPR_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCurrencyType" runat="server" Text="Currency Type / Rate" />
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboCurrencyType" runat="server" Width="179px" AutoPostBack="true"
                                            OnSelectedIndexChanged="cboCurrencyType_SelectedIndexChanged" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtCurrencyRate" runat="server" Width="113px" MinValue="1"
                                            AutoPostBack="true" OnTextChanged="txtCurrencyRate_TextChanged" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvCurrencyType" runat="server" ErrorMessage="Currency Type required."
                                ValidationGroup="entry" ControlToValidate="cboCurrencyType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRItemType" runat="server" Text="Item Type" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvcboSRItemType" runat="server" ErrorMessage="Item Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRItemType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr runat="server" id="trSRItemCategory" visible="True">
                        <td class="label">
                            <asp:Label ID="lblSRItemCategory" runat="server" Text="Item Category"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemCategory" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRPurchaseOrderType" runat="server" Text="Purchase Order Type" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRPurchaseOrderType" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRPurchaseOrderType" runat="server" ErrorMessage="Purchase Order Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRPurchaseOrderType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblTermID" runat="server" Text="Term" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboTermID" runat="server" Width="300px" />
                        </td>
                        <td width="20"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTermOfPayment" runat="server" Text="Term" />
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtTermOfPayment" runat="server" Width="100px" MaxLength="10"
                                            MaxValue="99999" MinValue="0" NumberFormat-DecimalDigits="0" />
                                    </td>
                                    <td>&nbsp;&nbsp;Day(s)
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDeliveryOrdersDate" runat="server" Text="Delivery Orders Date" />
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtDeliveryOrdersDate" runat="server" Width="100px" />
                        </td>
                        <td width="20"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRDownPaymentType" runat="server" Text="Shipping Charges Type / Amount" />
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboSRDownPaymentType" runat="server" Width="156px" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtDownPaymentAmount" runat="server" Width="150" MaxLength="16" MinValue="0"
                                            AutoPostBack="True" OnTextChanged="txtDownPaymentAmount_TextChanged" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <%--<asp:RequiredFieldValidator ID="rfvSRDownPaymentType" runat="server" ErrorMessage="Shipping Charges Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRDownPaymentType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>--%>
                            <%--<asp:RequiredFieldValidator ID="rfvDownPaymentAmount" runat="server" ErrorMessage="Shipping Charges Amount required."
                                ValidationGroup="entry" ControlToValidate="txtDownPaymentAmount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>--%>
                        </td>
                        <td />
                    </tr>
                    <tr runat="server" id="trPaymentType">
                        <td class="label">
                            <asp:Label ID="lblPaymentType" runat="server" Text="Payment Type" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRPaymentType" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRPaymentType" runat="server" ErrorMessage="Payment Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRPaymentType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblAdvanceAmount" runat="server" Text="Down Payment Amount" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtAdvanceAmount" runat="server" Width="150" MaxLength="16"
                                AutoPostBack="True" OnTextChanged="txtAdvanceAmount_TextChanged" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvAdvanceAmount" runat="server" ErrorMessage="Down Payment Amount required."
                                ValidationGroup="entry" ControlToValidate="txtAdvanceAmount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblLeadTime" runat="server" Text="Lead Time" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLeadTime" runat="server" Width="100" MaxLength="15" />
                        </td>
                        <td width="20"></td>
                        <td />
                    </tr>
                    <asp:Panel runat="server" ID="pnlPphNonFixedValue">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPphType" runat="server" Text="PPh Type"></asp:Label>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadComboBox runat="server" ID="cboSRPph" Width="154px" AllowCustomText="true"
                                                Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboSRPph_OnSelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtPphPercentage" runat="server" Width="80px" MaxLength="10"
                                                MaxValue="99.99" MinValue="0" NumberFormat-DecimalDigits="2" Type="Percent" ReadOnly="True" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPphAmount" runat="server" Text="PPh Amount"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtPphAmount" runat="server" Width="150px" MaxLength="10"
                                    MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="label"></td>
                        <td>
                            <table width="100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 40%">
                                        <asp:CheckBox ID="chkIsInventoryItem" runat="server" Text="Inventory Item" AutoPostBack="true"
                                            OnCheckedChanged="chkIsInventoryItem_CheckedChanged" />
                                    </td>
                                    <td style="width: 40%">
                                        <asp:CheckBox ID="chkIsNonMasterOrder" runat="server" Text="Non Master Order" AutoPostBack="true"
                                            OnCheckedChanged="chkIsNonMasterOrder_CheckedChanged" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsAssets" runat="server" Text="Assets" AutoPostBack="true" OnCheckedChanged="chkIsAssets_CheckedChanged" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td>
                            <table width="100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 40%">
                                        <asp:CheckBox ID="chkIsConsignment" runat="server" Text="Consignment" AutoPostBack="true"
                                            OnCheckedChanged="chkIsConsignment_CheckedChanged" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsConsignmentAlreadyReceived" runat="server" Text="Item Already Received"
                                            Enabled="False" />
                                    </td>
                                    <td></td>

                                </tr>
                            </table>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td>
                            <table width="100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 40%">
                                        <asp:CheckBox ID="chkIsInstallmentOrder" runat="server" Text="Installment Order" />
                                    </td>
                                    <td></td>
                                    <td></td>

                                </tr>
                            </table>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRProductAccountID" runat="server" Text="Product Account"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRProductAccountID" runat="server" Width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="pnlCategorization" visible="True">
                        <td class="label">
                            <asp:Label ID="lblCategorization" runat="server" Text="Inventory Category"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboCategorization" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvCategorization" runat="server" ErrorMessage="Inventory Category required."
                                ValidationGroup="entry" ControlToValidate="cboCategorization" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trSRProcurementType" visible="True">
                        <td class="label">
                            <asp:Label ID="lblSRProcurementType" runat="server" Text="Procurement Type"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRProcurementType" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRProcurementType" runat="server" ErrorMessage="Procurement Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRProcurementType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td>
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" Visible="false" />
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" Visible="false" />
                            <asp:CheckBox ID="chkIsClosed" runat="server" Text="Closed" Enabled="false" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 350px; vertical-align: top">
                <fieldset runat="server" id="boxApprovalProgress">
                    <legend>Approval Progress</legend>
                    <div style="height: 300px; overflow: scroll">
                        <telerik:RadGrid ID="grdApproval" Width="100%" runat="server" ShowFooter="false"
                            OnNeedDataSource="grdApproval_NeedDataSource" AutoGenerateColumns="False" GridLines="None">
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="ApprovalLevel">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderStyle-Width="20px" DataField="ApprovalLevel" HeaderText=""
                                        UniqueName="ApprovalLevel" SortExpression="ApprovalLevel" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" Visible="true" />
                                    <telerik:GridBoundColumn DataField="UserName" HeaderText="By" UniqueName="UserName"
                                        SortExpression="UserName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        Visible="true" />
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Approve" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <table cellpadding="0" cellspacing="0" style="border-width: 0px">
                                                <tr>
                                                    <td style="border-width: 0px">
                                                        <%#true.Equals(DataBinder.Eval(Container.DataItem, "IsApproved")) ? Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ApprovalDateTime")).ToString(AppConstant.DisplayFormat.DateTime) : string.Empty%>
                                                    </td>
                                                    <td style="border-width: 0px">
                                                        <asp:Panel runat="server" ID="pnlApprove" Visible='<%#true.Equals(DataBinder.Eval(Container.DataItem,"IsApproveAble")) %>'>
                                                            <a style="cursor: pointer;" href="#" onclick='approvLevel(<%#DataBinder.Eval(Container.DataItem,"ApprovalLevel")%>)'>
                                                                <img src="../../../../Images/Toolbar/post16.png" border="0" alt="" />
                                                            </a>
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="border-width: 0px">
                                                        <asp:Panel runat="server" ID="pnlUnApprove" Visible='<%#true.Equals(DataBinder.Eval(Container.DataItem,"IsUnApproveAble")) %>'>
                                                            <a style="cursor: pointer;" href="#" onclick='unApprovLevel(<%#DataBinder.Eval(Container.DataItem,"ApprovalLevel")%>)'>
                                                                <img src="../../../../Images/Toolbar/delete16.png" border="0" alt="" />
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItemTransactionItem" runat="server" ShowFooter="false" OnNeedDataSource="grdItemTransactionItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemTransactionItem_UpdateCommand"
        OnDeleteCommand="grdItemTransactionItem_DeleteCommand" OnInsertCommand="grdItemTransactionItem_InsertCommand"
        AllowPaging="true">
        <PagerStyle Mode="NextPrevAndNumeric" />
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo" PageSize="10">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsBonusItem" HeaderText="Bonus"
                    UniqueName="IsBonusItem" SortExpression="IsBonusItem" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="true" />
                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' /><br />
                        <i>
                            <asp:Label ID="lblAdditionalInfo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AdditionalInfo") %>' /></i>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="FabricName" HeaderText="Factory"
                    UniqueName="FabricName" SortExpression="FabricName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Minimum" HeaderText="Min"
                    UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Maximum" HeaderText="Max"
                    UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="BalanceSG" HeaderText="Balance SG"
                    UniqueName="BalanceSG" SortExpression="BalanceSG" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Balance" HeaderText="Balance CW"
                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="BalanceTotal" HeaderText="Balance Total"
                    UniqueName="BalanceTotal" SortExpression="BalanceTotal" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="SRMasterBaseUnit" HeaderText="Unit"
                    UniqueName="SRMasterBaseUnit" SortExpression="SRMasterBaseUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80" DataField="Quantity" HeaderText="PO Qty"
                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="110" DataField="SRItemUnit" HeaderText="PO Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridTemplateColumn HeaderStyle-Width="120px" UniqueName="SRItemUnit_Conversion"
                    HeaderText="PO Unit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblSRItemUnit_Conversion" runat="server" Text='<%# string.Format("{0}/{1}", DataBinder.Eval(Container.DataItem,"SRItemUnit"), ((decimal)DataBinder.Eval(Container.DataItem,"ConversionFactor")).ToString("n0")) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="75px" DataField="ConversionFactor"
                    HeaderText="Conversion" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}"
                    Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="PriceInCurrency"
                    HeaderText="Price In Currency" UniqueName="PriceInCurrency" SortExpression="PriceInCurrency"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                    Visible="False" />
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Discount1Percentage"
                    HeaderText="Disc 1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Discount2Percentage"
                    HeaderText="Disc 2 (%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Disc Amount"
                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Disc Amount"
                    UniqueName="DiscAmount" DataType="System.Double" DataFields="Price,Discount1Percentage,Discount2Percentage, Quantity"
                    Expression="(({0}*{1}/100) + (({0} - ({0}*{1}/100)) * {2}/100)) * {3}" FooterText=" "
                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="False" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="Total" UniqueName="TotalPrice2"
                    DataType="System.Double" DataFields="Price, Discount, Quantity" Expression="(({0}-{1}) * {2})"
                    FooterText=" " FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="Total" UniqueName="TotalPrice"
                    DataType="System.Double" DataFields="Price,Discount1Percentage,Discount2Percentage, Quantity"
                    Expression="({0}*{3}) - (((({0}*{3})*{1}/100) + ((({0}*{3}) - (({0}*{3})*{1}/100)) * {2}/100)))"
                    FooterText=" " FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="False" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="Total In Currency"
                    UniqueName="TotalCurrency2" DataType="System.Double" DataFields="PriceInCurrency, DiscountInCurrency, Quantity"
                    Expression="(({0}-{1}) * {2})" FooterText=" " FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" Visible="False" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="Total In Currency"
                    UniqueName="TotalCurrency" DataType="System.Double" DataFields="PriceInCurrency,Discount1Percentage,Discount2Percentage, Quantity"
                    Expression="({0}*{3}) - ((({0}*{1}/100) + (({0} - ({0}*{1}/100)) * {2}/100)) * {3})"
                    FooterText=" " FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="False" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="PurchaseOrderItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ItemTransactionItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionAmount" runat="server" Text="Transaction Amount" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtTransactionAmount" runat="server" Width="100px"
                                MaxLength="16" MinValue="0" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDiscountAmount" runat="server" Text="Global Discount Amount" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtDiscountAmount" runat="server" Width="100px" MaxLength="16"
                                MinValue="0" AutoPostBack="true" OnTextChanged="txtDiscountAmount_TextChanged" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvDiscountAmount" runat="server" ErrorMessage="Discount Amount required."
                                ValidationGroup="entry" ControlToValidate="txtDiscountAmount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChargesAmount" runat="server" Text="Order Amount" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtChargesAmount" runat="server" Width="100px" MaxLength="16"
                                MinValue="0" ReadOnly="true" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvChargesAmount" runat="server" ErrorMessage="Order Amount required."
                                ValidationGroup="entry" ControlToValidate="txtChargesAmount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAmountTaxed" runat="server" Text="Amount Taxed" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtAmountTaxed" runat="server" Width="100px" MaxLength="16"
                                MinValue="0" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxPercentage" runat="server" Text="Tax Percentage" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtTaxPercentage" runat="server" Type="Percent" Width="100px"
                                MaxLength="5" MaxValue="999.99" MinValue="0" AutoPostBack="true" OnTextChanged="txtTaxPercentage_TextChanged"
                                ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxAmount" runat="server" Text="Tax Amount" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtTaxAmount" runat="server" Width="100px" MaxLength="16"
                                MinValue="0" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTotal" runat="server" Text="Total" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="100px" MaxLength="16"
                                ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
