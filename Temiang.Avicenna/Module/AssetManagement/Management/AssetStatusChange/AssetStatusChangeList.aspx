<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="AssetStatusChangeList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.AssetStatusChangeList"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinView(seqNo) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.SetUrl("AssetStatusChangeDetail.aspx?seqNo=" + seqNo);
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function openWinAdd() {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.SetUrl("AssetStatusChangeDetail.aspx?seqNo=-1");
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                __doPostBack("<%= grdList.UniqueID %>", "rebind");
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Move, Close"
        Width="900px" Height="600px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" OnClientClose="onClientClose" />
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRoom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAsset">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboLocation" />
                    <telerik:AjaxUpdatedControl ControlID="cboAssetID" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboLocation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboAssetID" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnit" runat="server" Width="304px" HighlightTemplatedItems="True"
                                    AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboServiceUnit_ItemDataBound"
                                    OnItemsRequested="cboServiceUnit_ItemsRequested" OnSelectedIndexChanged="cboServiceUnit_SelectedIndexChanged">
                                    <FooterTemplate>
                                        Note : Show max 20 result
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRoom" runat="server" Text="Room"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboLocation" Width="304px" AllowCustomText="true"
                                    Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboLocation_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnRoom" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAsset" runat="server" Text="Asset"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboAssetID" runat="server" Width="304px" HighlightTemplatedItems="True"
                                    MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboAssetID_ItemDataBound"
                                    OnItemsRequested="cboAssetID_ItemsRequested">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "AssetName")%>
                                        </b>
                                        <br />
                                        Serial No :
                                        <%# DataBinder.Eval(Container.DataItem, "SerialNumber")%>
                                        <br />
                                        Location :&nbsp;<%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                        <br />
                                        Unit Maintenance :&nbsp;<%# DataBinder.Eval(Container.DataItem, "MaintenanceServiceUnitName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 result
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnAsset" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
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
        AllowPaging="true" PageSize="15" AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="SeqNo" ClientDataKeyNames="SeqNo" GroupLoadMode="client"
            CommandItemDisplay="Top">
            <CommandItemTemplate>
                &nbsp;&nbsp;
                <asp:LinkButton ID="lbNew" runat="server" OnClientClick="javascript:openWinAdd();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" />&nbsp;<asp:Label
                        runat="server" ID="lblNew" Text="New"></asp:Label></asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"openWinView('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "SeqNo")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SeqNo" HeaderText="No"
                    UniqueName="SeqNo" SortExpression="SeqNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" Visible="False" />
                <telerik:GridBoundColumn DataField="TransactionDate" HeaderText="Date"
                    UniqueName="TransactionDate" SortExpression="TransactionDate" DataType="System.DateTime"
                    DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="105px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AssetGroup" HeaderText="Asset"
                    UniqueName="AssetGroup" SortExpression="AssetGroup" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ServiceUnitName" HeaderText="Service Unit"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="AssetsStatusFrom" HeaderText="From Status"
                    UniqueName="AssetsStatusFrom" SortExpression="AssetsStatusFrom" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="AssetsStatusTo" HeaderText="To Status"
                    UniqueName="AssetsStatusTo" SortExpression="AssetsStatusTo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
