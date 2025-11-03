<%@  Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="JobOrderResultViewer.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.JobOrderResultViewer" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPage" runat="server" Text="Page" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboPageNumber" runat="server" Width="100px" Enabled="false"
                                AutoPostBack="true" OnSelectedIndexChanged="cboPageNumber_OnSelectedIndexChanged" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Image ID="imgViewer" runat="server" Width="1000px" Height="500px" AlternateText="No result selected."
                    BorderStyle="Solid" BorderWidth="1px" />
            </td>
        </tr>
    </table>
</asp:Content>
