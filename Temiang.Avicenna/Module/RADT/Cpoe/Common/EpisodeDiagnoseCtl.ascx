<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EpisodeDiagnoseCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.Common.EpisodeDiagnoseCtl" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<telerik:RadAjaxPanel runat="server">
    <table>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pnlDiagnosis">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="right">
                <telerik:RadCodeBlock runat="server">
                    <%= ReadOnly? string.Empty: string.Format("<a style=\"cursor: pointer;\" OnClick=\"javascript:__doPostBack('{1}', 'ADD'); return true;\" ><img src=\"{0}/Images/Toolbar/insert16.png\"/></a>",Helper.UrlRoot(),pnlDiagnosis.UniqueID) %>
                </telerik:RadCodeBlock>
            </td>
        </tr>
    </table>
</telerik:RadAjaxPanel>
