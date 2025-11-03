<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StandardReferenceChkBoxCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.StandardReferenceChkBoxCtl" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Item" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboItemID" runat="server" Width="100%"  CheckBoxes="true" EnableCheckAllItemsCheckBox="true">
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
