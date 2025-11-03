<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="StockInfoList.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.StockInfoList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemBalance" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchItemID">
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
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                                    OnItemsRequested="cboServiceUnitID_ItemsRequested">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 10 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchServiceUnitID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchItemID_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%; vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblItemName" runat="server" Text="Item" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboItemID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboItemID_ItemDataBound"
                                    OnItemsRequested="cboItemID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchItemID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchItemID_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdItemBalance" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
        OnNeedDataSource="grdItemBalance_NeedDataSource" AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="ServiceUnitID, ItemID">
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Balance in each phase" Name="Phase" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                    SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ServiceUnitName" HeaderText="Service Unit"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Balance"
                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                </telerik:GridNumericColumn>

                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceReceived" HeaderText="Received"
                    UniqueName="BalanceReceived" SortExpression="BalanceReceived" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Phase">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceDeconImmersion" HeaderText="Decontamination Immersion"
                    UniqueName="BalanceDeconImmersion" SortExpression="BalanceDeconImmersion" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Phase">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceDeconAbstersion" HeaderText="Decontamination Abstersion"
                    UniqueName="BalanceDeconAbstersion" SortExpression="BalanceDeconAbstersion" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Phase">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceDeconDrying" HeaderText="Decontamination Drying"
                    UniqueName="BalanceDeconDrying" SortExpression="BalanceDeconDrying" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Phase">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceFeasibilityTest" HeaderText="Feasibility Test"
                    UniqueName="BalanceFeasibilityTest" SortExpression="BalanceFeasibilityTest" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Phase">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalancePackaging" HeaderText="Packaging"
                    UniqueName="BalancePackaging" SortExpression="BalancePackaging" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Phase">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceUltrasound" HeaderText="Ultrasound"
                    UniqueName="BalanceUltrasound" SortExpression="BalanceUltrasound" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Phase">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceSterilization" HeaderText="Sterilization"
                    UniqueName="BalanceSterilization" SortExpression="BalanceSterilization" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Phase">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceDistribution" HeaderText="Distributed"
                    UniqueName="BalanceDistribution" SortExpression="BalanceDistribution" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Phase">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceReturned" HeaderText="Returned"
                    UniqueName="BalanceReturned" SortExpression="BalanceReturned" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ColumnGroupName="Phase" Visible="false">
                </telerik:GridNumericColumn>

                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>

            </Columns>
        </MasterTableView>
        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
            OpenInNewWindow="true" />
        <ClientSettings AllowDragToGroup="true" EnableRowHoverStyle="true" AllowExpandCollapse="true" />
    </telerik:RadGrid>
</asp:Content>
