<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="SanitationWasteInformationList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Management.SanitationWasteInformationList" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchSRWasteType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkOnlyInStock">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdItemBalance">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 50%; vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRWasteType" runat="server" Text="Waste Type" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRWasteType" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchSRWasteType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label" />
                            <td class="entry">
                                <asp:CheckBox runat="server" ID="chkOnlyInStock" AutoPostBack="true" Text="Only In Stock"
                                    OnCheckedChanged="chkOnlyInStock_CheckedChanged" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%; vertical-align: top">
                    <table width="100%">
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdItemBalance" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
        OnNeedDataSource="grdItemBalance_NeedDataSource"
        AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="SRWasteType">
            <Columns>
                <telerik:GridBoundColumn DataField="SRWasteType" HeaderText="ID" UniqueName="SRWasteType"
                    SortExpression="SRWasteType">
                    <HeaderStyle HorizontalAlign="Left" Width="110px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="WasteTypeName" HeaderText="Waste Type" UniqueName="WasteTypeName"
                    SortExpression="WasteTypeName">
                    <HeaderStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Qty"
                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                </telerik:GridNumericColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
            OpenInNewWindow="true" />
        <ClientSettings AllowDragToGroup="true" EnableRowHoverStyle="true" AllowExpandCollapse="true" />
    </telerik:RadGrid>
</asp:Content>
