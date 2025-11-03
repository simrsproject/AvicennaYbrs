<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="CopyAssetList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Master.CopyAssetList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAssetGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAssetName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnPONumber">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAssetGroup" runat="server" Text="Asset Group"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboAssetGroup" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboAssetGroup_ItemDataBound"
                                    OnItemsRequested="cboAssetGroup_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnAssetGroup" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAssetName" runat="server" Text="Asset Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAssetName" runat="server" Width="300px" MaxLength="100" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnAssetName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPONumber" runat="server" Text="PO Received No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPONumber" runat="server" Width="300px" MaxLength="30" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnPONumber" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="10"
        AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="AssetID" GroupLoadMode="client">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="AssetID" HeaderText="Asset ID"
                    UniqueName="AssetID" SortExpression="AssetID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="AssetName" HeaderText="AssetName" UniqueName="AssetName"
                    SortExpression="AssetName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="GroupName" HeaderText="Group" UniqueName="GroupName"
                    SortExpression="GroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                    SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
