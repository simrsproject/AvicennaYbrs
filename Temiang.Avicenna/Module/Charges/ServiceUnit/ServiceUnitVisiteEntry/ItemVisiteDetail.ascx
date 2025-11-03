<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemVisiteDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.ItemVisiteDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumTransPaymentItem" runat="server" ValidationGroup="TransPaymentItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="TransPaymentItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            Expired Date
        </td>
        <td class="entry">
            <telerik:RadDatePicker ID="txtExpiredDate" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvExpiredDate" runat="server" ErrorMessage="Expired Date required."
                ControlToValidate="txtExpiredDate" SetFocusOnError="True" ValidationGroup="TransPaymentItem"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">
            Service Unit
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="304px" AutoPostBack="true" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                ControlToValidate="cboServiceUnitID" SetFocusOnError="True" ValidationGroup="TransPaymentItem"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboItemID" runat="server" Width="304px" AutoPostBack="true"
                MarkFirstMatch="true" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                OnSelectedIndexChanged="cboItemID_SelectedIndexChanged" OnItemDataBound="cboItemID_ItemDataBound"
                OnItemsRequested="cboItemID_ItemsRequested">
                <FooterTemplate>
                    Note : Show max 30 result
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="TransPaymentItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblAmount" runat="server" Text="Visite Qty"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100px" MinValue="0"
                NumberFormat-DecimalDigits="0" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Visite Qty required."
                ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="TransPaymentItem"
                Width="100%">
                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="100px" MinValue="0"
                ReadOnly="true" />
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblDiscount" runat="server" Text="Discount (%)"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtDiscount" runat="server" Width="100px" MinValue="0" />
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="TransPaymentItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="TransPaymentItem" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>
