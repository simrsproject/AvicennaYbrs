<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="SterilizationProcessSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.SterilizationProcessSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblProcessNo" runat="server" Text="Process No" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterProcessNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtProcessNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblProcessDate" runat="server" Text="Process Date" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtProcessDate" runat="server" Width="100px" />
            </td>
            <td>
            </td>
        </tr>
        <asp:Panel runat="server" ID="pnlProcessType">
            <tr>
                <td class="label">
                    <asp:Label ID="lblSRCssdProcessType" runat="server" Text="Process Type" Width="100px"></asp:Label>
                </td>
                <td class="filter">
                    <telerik:RadComboBox ID="cboFilterSRCssdProcessType" runat="server" Width="100%">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td class="entry">
                    <telerik:RadComboBox ID="cboSRCssdProcessType" runat="server" Width="300px" DataTextField="ItemName"
                        DataValueField="ItemID" AllowCustomText="true" MarkFirstMatch="true" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblMachineID" runat="server" Text="Machine" Width="100px"></asp:Label>
                </td>
                <td class="filter">
                    <telerik:RadComboBox ID="cboFilterMachineID" runat="server" Width="100%">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td class="entry">
                    <telerik:RadComboBox ID="cboMachineID" runat="server" Width="300px" DataTextField="MachineName"
                        DataValueField="MachineID" AllowCustomText="true" MarkFirstMatch="true" />
                </td>
                <td>
                </td>
            </tr>
        </asp:Panel>
    </table>
</asp:Content>
