<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WageStructureAndScaleWorGroupItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.WageStructureAndScaleWorGroupItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumWageStructureAndScaleWorSubGroup" runat="server" ValidationGroup="WageStructureAndScaleWorSubGroup" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="WageStructureAndScaleWorSubGroup"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblWorkSubGroupID" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtWorkSubGroupID" runat="server" Width="300px" MaxLength="20" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvWorkSubGroupID" runat="server" ErrorMessage="ID required."
                            ControlToValidate="txtWorkSubGroupID" SetFocusOnError="True" ValidationGroup="WageStructureAndScaleWorSubGroup"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblWorkSubGroupName" runat="server" Text="Work Sub Group"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtWorkSubGroupName" runat="server" Width="300px" MaxLength="200" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvWorkSubGroupName" runat="server" ErrorMessage="Work Sub Group required."
                            ControlToValidate="txtWorkSubGroupName" SetFocusOnError="True" ValidationGroup="WageStructureAndScaleWorSubGroup"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="WageStructureAndScaleWorSubGroup"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="WageStructureAndScaleWorSubGroup" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
            </table>
        </td>
    </tr>
</table>