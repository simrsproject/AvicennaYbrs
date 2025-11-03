<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PersonalAgeSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Query.PersonalAgeSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee No" Width="100px"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFilterEmployeeNumber" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEmployeeNo" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFirstName" runat="server" Text="First Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFirstName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtFirstName" runat="server" Width="300px" MaxLength="500" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblBirthDate" runat="server" Text="Birth Date" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtFromBirthDate" runat="server" Width="100px" />
                        </td>
                        <td>
                            to &nbsp;
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtToBirthDate" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Age" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadNumericTextBox ID="txtAgeFrom" runat="server" Width="100px" MaxLength="500" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td>&nbsp;-&nbsp;</td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtAgeTo" runat="server" Width="100px" MaxLength="500" NumberFormat-DecimalDigits="0" />
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblBirthdayPeriod" runat="server" Text="Birthday Period" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtBirthdayPeriodFrom" runat="server" Width="100px" />
                        </td>
                        <td>
                            to &nbsp;
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtBirthdayPeriodTo" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>

