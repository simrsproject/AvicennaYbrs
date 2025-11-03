<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="SickLetterEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.SickLetterEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">Start Date
            </td>
            <td>
                <telerik:RadDatePicker ID="txtSickLetterStartDate" runat="server" Width="100px" />
            </td>
            <td style="width: 20px">
                <asp:RequiredFieldValidator ID="rfvSickLetterStartDate" runat="server" ErrorMessage="Start Date required."
                    ValidationGroup="sickletter" ControlToValidate="txtSickLetterStartDate" SetFocusOnError="True">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>&nbsp;
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">To Date
            </td>
            <td>
                <telerik:RadDatePicker ID="txtSickLetterEndDate" runat="server" Width="100px" />
            </td>
            <td style="width: 20px">
                <asp:RequiredFieldValidator ID="rfvSickLetterEndDate" runat="server" ErrorMessage="To Date required."
                    ValidationGroup="sickletter" ControlToValidate="txtSickLetterEndDate" SetFocusOnError="True">
                    <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>&nbsp;
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Notes
            </td>
            <td>
                <telerik:RadTextBox ID="txtSickLetterNotes" runat="server" Width="500px" TextMode="MultiLine" />
            </td>
            <td style="width: 20px"></td>
            <td></td>
        </tr>
    </table>


</asp:Content>
