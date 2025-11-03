<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="ApplicaresList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.ApplicaresList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="tmrUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdMonitor" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Panel ID="fw_PanelInfo" runat="server" BackColor="#FFFFC0" Font-Size="Small"
                    BorderColor="#FFC080" BorderStyle="Solid">
                    <table width="100%">
                        <tr>
                            <td width="10px" valign="top">
                                <asp:Image ID="Image1" ImageUrl="~/Images/infoblue16.png" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="fw_LabelInfo" runat="server" Text="Automatic reload data by 10 minutes period." />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="height: 4px;">
            </td>
        </tr>
        <tr>
            <td>
                <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Live Stream Raw Data">
                    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                        AutoGenerateColumns="true" ShowGroupPanel="false" AllowSorting="True" GridLines="None">
                        <MasterTableView DataKeyNames="Kodekelas, Koderuang" />
                    </telerik:RadGrid>
                </cc:CollapsePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 4px;">
            </td>
        </tr>
        <tr>
            <td>
                <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Upload Stream Monitor">
                    <telerik:RadGrid ID="grdMonitor" runat="server" OnNeedDataSource="grdMonitor_NeedDataSource"
                        AutoGenerateColumns="true" ShowGroupPanel="false" AllowSorting="True" GridLines="None">
                        <MasterTableView DataKeyNames="JobName" />
                    </telerik:RadGrid>
                </cc:CollapsePanel>
            </td>
        </tr>
    </table>
    <asp:Timer ID="tmrUpdate" runat="server" Interval="100000" OnTick="tmrUpdate_Tick" />
</asp:Content>
