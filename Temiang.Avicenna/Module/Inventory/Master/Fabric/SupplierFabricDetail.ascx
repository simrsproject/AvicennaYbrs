<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierFabricDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Master.SupplierFabricDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumSupplierFabric" runat="server" BackColor="PapayaWhip"
    Font-Size="Small" BorderColor="#FF8000" BorderStyle="Solid" ValidationGroup="SupplierFabric" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="SupplierFabric"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblSupplierID" runat="server" Text="Supplier"></asp:Label>
        </td>
        <td class="entry">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadTextBox ID="txtSupplierID" runat="server" Width="100px" MaxLength="10"
                            AutoPostBack="True" OnTextChanged="txtSupplierID_TextChanged" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblSupplierName" runat="server" CssClass="labeldescription" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSupplierID" runat="server" ErrorMessage="Supplier required."
                ControlToValidate="txtSupplierID" SetFocusOnError="True" ValidationGroup="SupplierFabric"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td class="entry">
            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="SupplierFabric"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="SupplierFabric" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
