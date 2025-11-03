<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="StockInformationList.aspx.cs" Inherits="Temiang.Avicenna.Module.BloodBank.Stock.StockInformationList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchBloodSource">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchBloodSourceFrom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchBloodType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchBloodGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchExpiredDateTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchOrderBy">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" LoadingPanelID="fw_ajxLoadingPanel" />
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
                                <asp:Label ID="lblSRBloodSource" runat="server" Text="Blood Source" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRBloodSource" runat="server" Width="300px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchBloodSource" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRBloodSourceFrom" runat="server" Text="Blood Source From" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRBloodSourceFrom" runat="server" Width="300px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchBloodSourceFrom" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRBloodType" runat="server" Text="Blood Type" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRBloodType" runat="server" Width="300px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchBloodType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%; vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRBloodGroup" runat="server" Text="Blood Group" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRBloodGroup" runat="server" Width="300px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchBloodGroup" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblExpiredDateTime" runat="server" Text="Expired Date less than" />
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtExpiredDateTime" runat="server" Width="100px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchExpiredDateTime" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblOrderBy" runat="server" Text="Order By" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboOrderBy" runat="server" Width="150px" />
                                <asp:RadioButtonList ID="rblOrderBy" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
                                    <asp:ListItem Value="ASC" Text="ASC" Selected="True" />
                                    <asp:ListItem Value="DESC" Text="DESC" />
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchOrderBy" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdItem" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
        OnNeedDataSource="grdItem_NeedDataSource" AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="SRBloodSource, BagNo">
            <Columns>
                <telerik:GridBoundColumn DataField="BloodSource" HeaderText="Blood Source" UniqueName="BloodSource"
                    SortExpression="BloodSource">
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BloodSourceFrom" HeaderText="Blood Source From"
                    UniqueName="BloodSourceFrom" SortExpression="BloodSourceFrom">
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BagNo" HeaderText="Bag No" UniqueName="BagNo"
                    SortExpression="BagNo">
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BloodType" HeaderText="Blood Type" UniqueName="BloodType"
                    SortExpression="BloodType">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BloodRhesus" HeaderText="Blood Rhesus" UniqueName="BloodRhesus"
                    SortExpression="BloodRhesus">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BloodGroup" HeaderText="Blood Group" UniqueName="BloodGroup"
                    SortExpression="BloodGroup">
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="VolumeBag"
                    HeaderText="Volume (ML/CC)" UniqueName="VolumeBag" SortExpression="VolumeBag"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridBoundColumn DataField="ExpiredDateTime" HeaderText="Expired Date" UniqueName="ExpiredDateTime"
                    SortExpression="ExpiredDateTime" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="130px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Balance"
                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                </telerik:GridNumericColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsCrossMatching" HeaderText="Booked (Crossmatching)"
                    UniqueName="IsCrossMatching" SortExpression="IsCrossMatching" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
            OpenInNewWindow="true" />
        <ClientSettings AllowDragToGroup="true" EnableRowHoverStyle="true" AllowExpandCollapse="true" />
    </telerik:RadGrid>
</asp:Content>
