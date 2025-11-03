<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemMedicalSummaryDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Cpoe.ItemMedicalSummaryDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumExamSummaryResult" runat="server" ValidationGroup="ExamSummaryResult" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="TransChargeExamSummaryResultsItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr id="trCenterID" runat="server">
        <td class="label">
            <asp:Label ID="lblDescription" runat="server" Text="Description" />
        </td>
        <td>
            <telerik:RadTextBox ID="txtDescription" runat="server" Width="900px" Height="100px" TextMode="MultiLine" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvCenterID" runat="server" ErrorMessage="Description required."
                ControlToValidate="txtDescription" SetFocusOnError="True" ValidationGroup="ExamSummaryResult"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ExamSummaryResult"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ExamSummaryResult" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>
