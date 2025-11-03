<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="MenuInitializationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.MenuInitializationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblStartingDate" runat="server" Text="Starting Date"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtStartingDate" runat="server" Width="100px" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvStartingDate" runat="server" ErrorMessage="Starting Date required."
                    ValidationGroup="entry" ControlToValidate="txtStartingDate" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblVersionID" runat="server" Text="Version"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboVersionID" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="cboVersionID_SelectedIndexChanged" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvVersionID" runat="server" ErrorMessage="Version required"
                    ValidationGroup="entry" ControlToValidate="cboVersionID" SetFocusOnError="True"
                    Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSeqNo" runat="server" Text="Seq No"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSeqNo" runat="server" Width="100px" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvSeqNo" runat="server" ErrorMessage="Seq No required"
                    ValidationGroup="entry" ControlToValidate="cboSeqNo" SetFocusOnError="True" Width="100%">*</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
