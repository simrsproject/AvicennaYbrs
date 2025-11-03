<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="StockOpnameAdd.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.StockOpnameAdd" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboFromServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboFromLocationID" />
                    <telerik:AjaxUpdatedControl ControlID="cboItemBin" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRItemType" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRTherapyGroupID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFromLocationID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboItemBin" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRItemType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboItemGroupID" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRTherapyGroupID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
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
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                    DatePopupButton-Enabled="false" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                    ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                    ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="1000" TextMode="MultiLine" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
