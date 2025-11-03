<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StandardPopUp.aspx.cs"
    Inherits="Temiang.Avicenna.SearchPopUp.StandardPopUp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Select</title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadSkinManager ID="fw_RadSkinManager" runat="server">
        </telerik:RadSkinManager>
        <telerik:RadFormDecorator ID="fw_RadFormDecorator" runat="server" DecoratedControls="Default" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnRefresh">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="grdList" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <table width="100%">
            <tr>
                <td class="label">
                    <asp:Label ID="Label1" runat="server" Text="Search" Width="100px"></asp:Label></td>
                <td width="300px">
                    &nbsp;<telerik:RadTextBox ID="txtSearch" runat="server" Width="100%">
                    </telerik:RadTextBox></td>
                <td width="5px"></td>    
                <td width="80px">
                    <asp:Button ID="btnRefresh" runat="server" OnClick="btnOk_Click" Text="Refresh" Width="80px" /></td>
                <td>
                </td>
            </tr>
        </table>
        <telerik:RadGrid ID="grdList" runat="server" AllowSorting="true" AutoGenerateColumns="False"
            OnNeedDataSource="grdList_NeedDataSource" GridLines="None">
            <MasterTableView AllowSorting="true">
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <Resizing AllowColumnResize="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </telerik:RadGrid>
    </form>
</body>
</html>
