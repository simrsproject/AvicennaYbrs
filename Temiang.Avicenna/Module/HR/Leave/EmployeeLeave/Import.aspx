<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Leave.Import" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label">
                <asp:Label ID="lblPeriod" runat="server" Text="Period"></asp:Label>
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" MinDate="01/01/1900"
                                MaxDate="12/31/2999" />
                        </td>
                        <td style="width: 15px">to
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtEnddate" runat="server" Width="100px" MinDate="01/01/1900"
                                MaxDate="12/31/2999" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblLeaveEntitlements" runat="server" Text="Leave Entitlements (Days)"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtLeaveEntitlementsQty" runat="server" Width="100px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">Excel Path File
            </td>
            <td class="entry">
                <asp:FileUpload ID="fileuploadExcel" runat="server" />
            </td>
            <td width="20" />
            <td />
        </tr>
    </table>
</asp:Content>
