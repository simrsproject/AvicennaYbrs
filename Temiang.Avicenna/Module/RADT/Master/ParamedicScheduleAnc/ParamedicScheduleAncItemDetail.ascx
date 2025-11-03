<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParamedicScheduleAncItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicScheduleAnc.ParamedicScheduleAncItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumParamedicScheduleDateItem" runat="server" ValidationGroup="ParamedicScheduleDateItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ParamedicScheduleDateItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table>
    <tr>
        <td class="label">
            <asp:Label ID="lblOperationalTimeID" runat="server" Text="Operational Time"></asp:Label>
        </td>
        <td class="entry">
            <telerik:radcombobox id="cboOperationalTimeID" runat="server" width="300px" allowcustomtext="true"
                filter="Contains" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvOperationalTimeID" runat="server" ErrorMessage="Operational Time required."
                ControlToValidate="cboOperationalTimeID" SetFocusOnError="True" ValidationGroup="ParamedicScheduleDateItem"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">
        </td>
        <td class="entry">
            <asp:CheckBox runat="server" ID="chkIsIpr" Text="IPR" />&nbsp;&nbsp;
            <asp:CheckBox runat="server" ID="chkIsOpr" Text="OPR" />&nbsp;&nbsp;
            <asp:CheckBox runat="server" ID="chkIsEmr" Text="EMR" />
        </td>
        <td width="20px"></td>
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ParamedicScheduleDateItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ParamedicScheduleDateItem" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>
