<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="SatuSehatConsent.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.SatuSehatConsent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>Current Status</legend>
        <table width="100%">
            <tr>
                <td class="label">Access
                </td>
                <td class="entry">
                    <asp:RadioButtonList ID="optCurrentStatus" runat="server" RepeatDirection="Horizontal" Enabled="false">
                        <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
                        <asp:ListItem Text="Denied" Value="D"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Start Date
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtStartDate" runat="server" Width="304px"  ReadOnly="true" />
                </td>
                <td></td>
            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset>
        <legend>Change Status To</legend>
        <table width="100%">
            <tr>
                <td class="label">Access
                </td>
                <td class="entry">
                    <asp:RadioButtonList ID="optChangeStatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Approved" Value="A" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Denied" Value="D"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td></td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
