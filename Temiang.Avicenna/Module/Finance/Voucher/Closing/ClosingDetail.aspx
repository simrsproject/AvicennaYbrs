<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ClosingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.Closing.ClosingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblPostingId" runat="server" Visible="false"></asp:Label>
    <table>
        <tr>
            <td colspan="4">
                <asp:Label ID="txtMessage" runat="server" Font-Bold="true" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td class="label">
                Month
            </td>
            <td class="entry" style="width: 80%">
                <telerik:RadComboBox ID="ddlMonth" runat="server" Width="129px" />
            </td>
            <td style="width: 20px;">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                Year
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtYear" runat="server" Width="125px" MaxLength="4" />
            </td>
            <td style="width: 20px;">
                <asp:RequiredFieldValidator ID="rfvYear" runat="server" ErrorMessage="Year Required"
                    ValidationGroup="entry" ControlToValidate="txtYear" SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                Journal Module Group
            </td>
            <td class="entry" style="width: 80%">
                <telerik:RadComboBox runat="server" ID="cboJournalGroup" Width="300px" />
            </td>
            <td style="width: 20px;">
                <asp:RequiredFieldValidator ID="rfvJournalGroup" runat="server" ErrorMessage="Journal Module Group Required"
                    ValidationGroup="entry" ControlToValidate="cboJournalGroup" SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                Posting Until
            </td>
            <td class="entry" style="width: 80%">
                <telerik:RadDatePicker ID="txtPostingUntilDate" runat="server" Width="105px">
                    <DateInput runat="server" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                        ViewSelectorText="x">
                    </Calendar>
                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                </telerik:RadDatePicker>
            </td>
            <td style="width: 20px;">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsPostingFinal" runat="server" Text="Mark this periode as Closed (Users will not be able to alter/create transaction within this periode)" />
            </td>
            <td style="width: 20px;">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsFiscalYear" runat="server" Text="This periode is end of fiscal year" />
            </td>
            <td style="width: 20px;">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
