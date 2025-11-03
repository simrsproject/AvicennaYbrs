<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ParamedicFeeAddDeducSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeeAddDeducSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No" Width="100px"></asp:Label>
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
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblParamedicName" runat="server" Text="Physician" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterParamedicName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>

                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
       <tr>
            <td class="label">
                <asp:Label ID="lblStatus" runat="server" Text="Status" Width="100px"></asp:Label>
            </td>
            <td>
            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboFilterStatus" Width="300px" >
                                                        <Items>
                        <telerik:RadComboBoxItem runat="server" Text="" Value="1" />
                        <telerik:RadComboBoxItem runat="server" Text="Verif" Value="2" />
                        <telerik:RadComboBoxItem runat="server" Text="No Verif" Value="3" />
                    </Items>

                </telerik:RadComboBox>
                            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
