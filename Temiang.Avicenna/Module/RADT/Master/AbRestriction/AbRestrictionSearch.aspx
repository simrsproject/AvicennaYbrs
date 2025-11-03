<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="AbRestrictionSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Master.AbRestrictionSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                Restiction Name</td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterRestrictionName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtRestrictionName" runat="server" Width="100%">
                </telerik:RadTextBox>
            </td>
            <td>
            </td>
        </tr>

    </table>
</asp:Content>
