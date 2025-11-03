<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParamedicGlobalScheduleDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicGlobalScheduleDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumParamedicGlobalSchedule" runat="server" ValidationGroup="ParamedicGlobalSchedule" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ParamedicGlobalSchedule"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField runat="server" ID="hdnParamedicID" />
<asp:HiddenField runat="server" ID="hdnServiceUnitID" />
<table>
    <tr>
        <td class="label">
            <asp:Label ID="lblDayOfWeek" runat="server" Text="Day of Week"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboDayOfWeek" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvDayOfWeek" runat="server" ErrorMessage="Day of Week required."
                ControlToValidate="cboDayOfWeek" SetFocusOnError="True" ValidationGroup="ParamedicGlobalSchedule"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblOperationalTimeID" runat="server" Text="Operational Time"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboOperationalTimeID" runat="server" Width="300px" AllowCustomText="true"
                Filter="Contains" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvOperationalTimeID" runat="server" ErrorMessage="Operational Time required."
                ControlToValidate="cboOperationalTimeID" SetFocusOnError="True" ValidationGroup="ParamedicGlobalSchedule"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ParamedicGlobalSchedule"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ParamedicGlobalSchedule" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>
