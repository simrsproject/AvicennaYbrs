<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PositionBenchmarkDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionBenchmarkDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:ValidationSummary ID="vsumPositionBenchmark" runat="server" ValidationGroup="PositionBenchmark" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PositionBenchmark"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBenchmarkName" runat="server" Text="Benchmark Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtBenchmarkName" runat="server" Width="300px" Height="80px" MaxLength="200" TextMode="MultiLine" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvBenchmarkName" runat="server" ErrorMessage="Benchmark Name required."
                            ControlToValidate="txtBenchmarkName" SetFocusOnError="True" ValidationGroup="PositionBenchmark" Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PositionBenchmark"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PositionBenchmark"
                            Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                CommandName="Cancel"></asp:Button></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" Height="80px" MaxLength="4000" TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
