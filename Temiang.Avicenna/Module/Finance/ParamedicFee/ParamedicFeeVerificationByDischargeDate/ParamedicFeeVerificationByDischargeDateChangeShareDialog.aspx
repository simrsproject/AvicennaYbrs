<%@ Page Title="Update Share" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ParamedicFeeVerificationByDischargeDateChangeShareDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeeVerificationByDischargeDateChangeShareDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Using Percentage"></asp:Label>
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkUsingPrecentage" runat="server" 
                AutoPostBack="true" OnCheckedChanged="chkUsingPrecentage_OnCheckedChanged" />
            </td>
            <td width="20px">
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Amount"></asp:Label>
            </td>
            <td class="entry">
                <table>
                    <tr>
                        <td>
                            <telerik:RadNumericTextBox ID="txtPercentage" runat="server" MinValue="0"
                            MaxValue="100" Width="50">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtNominal" runat="server" MinValue="0">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20px">
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>