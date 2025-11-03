<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="BpjsVclaimCheckServiceDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.VClaim.BpjsVclaimCheckServiceDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Timer1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMessage" />
                    <telerik:AjaxUpdatedControl ControlID="imgOk" />
                    <telerik:AjaxUpdatedControl ControlID="imgFailed" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="20000" Enabled="True" />
    <table width="100%">
        <tr>
            <td width="16px">
                <asp:Image ID="imgOk" runat="server" ImageUrl="~/Images/Toolbar/post_green_16.png" />
                <asp:Image ID="imgFailed" runat="server" ImageUrl="~/Images/Toolbar/blacklist.png" />
            </td>
            <td>
                <asp:Label ID="lblMessage" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
