<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.DownPayment.PatientSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                Medical No / Patient Name
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterMedicalNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
