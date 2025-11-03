<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClinicalPathwayDiagnoseItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ClinicalPathwayDiagnoseItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            Diagnose Name
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboDiagnoseID" Width="300px" AllowCustomText="true"
                EnableLoadOnDemand="true" OnItemsRequested="cboDiagnoseID_ItemsRequested" OnItemDataBound="cboDiagnoseID_ItemDataBound" />
        </td>
        <td witdh="20px">
            <asp:RequiredFieldValidator ID="rfvDiagnoseName" runat="server" ErrorMessage="Diagnose Name required."
                ControlToValidate="cboDiagnoseID" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitAutoBillItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ServiceUnitAutoBillItem" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>
