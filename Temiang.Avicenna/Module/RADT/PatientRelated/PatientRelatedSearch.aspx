<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientRelatedSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientRelatedSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterMedicalNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterPatientName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="200" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
               <telerik:RadDatePicker ID="txtDoB" runat="server" Width="100px" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
