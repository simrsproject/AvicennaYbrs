<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="RiskGradingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.RiskGradingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRiskGradingID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRiskGradingID" runat="server" Width="100px" MaxLength="20">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvRiskGradingID" runat="server" ErrorMessage="ID required."
                                ControlToValidate="txtRiskGradingID" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRiskGradingName" runat="server" Text="Risk Grading"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRiskGradingName" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvRiskGradingName" runat="server" ErrorMessage="Risk Grading required."
                                ControlToValidate="txtRiskGradingName" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRiskGradingColor" runat="server" Text="Color"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblRiskGradingColor" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="true">Blue</asp:ListItem>
                                <asp:ListItem>Green</asp:ListItem>
                                <asp:ListItem>Yellow</asp:ListItem>
                                <asp:ListItem>Red</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
