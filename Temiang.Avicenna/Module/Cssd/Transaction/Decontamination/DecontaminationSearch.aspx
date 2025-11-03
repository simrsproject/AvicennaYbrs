<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="DecontaminationSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.DecontaminationSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblDecontaminationNo" runat="server" Text="Decontamination No"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterDecontaminationNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDecontaminationNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDecontaminationDate" runat="server" Text="Date"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtDecontaminationDate" runat="server" Width="100px" />
            </td>
            <td>
            </td>
        </tr>
        <tr runat="server" id="trSRAbstersionType">
            <td class="label">
                <asp:Label ID="lblSRAbstersionType" runat="server" Text="Abstersion Type"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboSRAbstersionType" Width="300px" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>