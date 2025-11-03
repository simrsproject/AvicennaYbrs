<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="MedicalRecordFileBorrowedSearch.aspx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.MedicalRecordFileBorrowedSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label5" runat="server" Text="Transaction No" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterTransactionNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label4" runat="server" Text="Registration No" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterRegistrationNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Medical No" Width="100px"></asp:Label>
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
                <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Date Of Borrowing" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtDateOfBorrowing" runat="server" Width="100px" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Date Of Return" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtDateOfReturn" runat="server" Width="100px" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label6" runat="server" Text="Due Date" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtDueDate" runat="server" Width="100px" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterServiceUnitID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="filter">
                <asp:CheckBox ID="chkIsReturned" runat="server" Text="Returned" />
            </td>
            <td class="entry">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
