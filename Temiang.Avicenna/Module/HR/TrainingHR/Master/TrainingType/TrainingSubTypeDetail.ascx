<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TrainingSubTypeDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.TrainingHR.Master.TrainingType.TrainingSubTypeDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumTrainingSubType" runat="server" ValidationGroup="TrainingSubType" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="TrainingSubType"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="ID is required."
                            ControlToValidate="txtItemID" SetFocusOnError="True" ValidationGroup="TrainingSubType"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemName" runat="server" Text="Sub Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" MaxLength="200" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Sub Type is required."
                            ControlToValidate="txtItemName" SetFocusOnError="True" ValidationGroup="TrainingSubType"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="TrainingSubType"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="TrainingSubType" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
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