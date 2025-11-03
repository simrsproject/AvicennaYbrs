<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="EmployeeTrainingSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.TrainingHR.EmployeeTrainingSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTrainingName" runat="server" Text="Training Name"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterTrainingName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTrainingName" runat="server" Width="300px" MaxLength="255" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTrainingLocation" runat="server" Text="Training Location"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterTrainingLocation" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTrainingLocation" runat="server" Width="300px" MaxLength="255" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTrainingOrganizer" runat="server" Text="Training Organizer"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterTrainingOrganizer" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTrainingOrganizer" runat="server" Width="300px" MaxLength="255" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
