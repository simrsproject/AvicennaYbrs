<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PpiNeedlePuncturedSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PpiNeedlePuncturedSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
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
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionDate" runat="server" Text="Date" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="100px" />
                        </td>
                        <td>
                            to &nbsp;
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtToDate" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblOfficerName" runat="server" Text="Officer Name"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterOfficerName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtOfficerName" runat="server" Width="300px" MaxLength="150" />
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDatePunctured" runat="server" Text="Incident Date" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtDatePuncturedFrom" runat="server" Width="100px" />
                        </td>
                        <td>
                            to &nbsp;
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtDatePuncturedTo" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPuncturedAreas" runat="server" Text="Exposed Areas"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterPuncturedAreas" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPuncturedAreas" runat="server" Width="300px" MaxLength="150" />
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblCausePunctured" runat="server" Text="Reason"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterCausePunctured" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtCausePunctured" runat="server" Width="300px" MaxLength="250" />
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboStatus" runat="server" Width="304px">
                </telerik:RadComboBox>
            </td>
            <td />
        </tr>
    </table>
</asp:Content>
