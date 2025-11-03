<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="WorkOrderClosingList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.WorkOrderClosingList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function rowClosed(orderNo) {
                if (confirm('Are you sure to closed for selected work order?')) {
                    __doPostBack("<%= grdList.UniqueID %>", 'closed|' + orderNo);
                }
            }
            function openDialog(woNo) {
                var oWnd = $find("<%= winDialog.ClientID %>");

                oWnd.setUrl("WorkOrderClosingDialog.aspx?wono=" + woNo);
                oWnd.show();
            }
            function onClientClose(oWnd, args) {
                __doPostBack("<%= grdList.UniqueID %>", "rebind");
            }
            function OnClientButtonClicking(sender, args) {
                __doPostBack("<%= grdList.UniqueID %>", 'closed');
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Move, Close"
        Width="600px" Height="200px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" OnClientClose="onClientClose" />
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchOrderDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchOrderNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchLastRealizationDateTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchFromServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchToServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
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
                                <asp:Label ID="lblSearchOrderDate" runat="server" Text="Order Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtFromOrderDate" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            &nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtToOrderDate" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchOrderDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblOrderNo" runat="server" Text="Work Order No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtOrderNo" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchOrderNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLastRealizationDate" runat="server" Text="Realization Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtFromLastRealizationDate" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            &nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtToLastRealizationDate" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchLastRealizationDateTime" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
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
                                <asp:Label ID="lblSearchFromServiceUnitID" runat="server" Text="Request Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSearchFromServiceUnitID" Width="300px"
                                    AllowCustomText="true" Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchFromServiceUnitID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSearchToServiceUnitID" runat="server" Text="To Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSearchToServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchToServiceUnitID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
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
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Closed" Value="closed" ImageUrl="~/Images/Toolbar/post16.png"
                HoveredImageUrl="~/Images/Toolbar/post16.png" DisabledImageUrl="~/Images/Toolbar/post16.png" />
        </Items>
    </telerik:RadToolBar>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
        <MasterTableView DataKeyNames="OrderNo">
            <Columns>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="35px" Visible="False">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"rowClosed('{0}'); return false;\">{1}</a>",
                                                                                    DataBinder.Eval(Container.DataItem, "OrderNo"), "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" title=\"Closed\" />"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="OrderNo" HeaderText="Work Order No"
                    UniqueName="OrderNo" SortExpression="OrderNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="OrderDate" HeaderText="Order Date" UniqueName="OrderDate"
                    SortExpression="OrderDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LastRealizationDateTime" HeaderText="Realization Date" UniqueName="LastRealizationDateTime"
                    SortExpression="LastRealizationDateTime" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>    
                <telerik:GridBoundColumn DataField="ImplementedBy" HeaderText="Implemented By" UniqueName="ImplementedBy"
                    SortExpression="ImplementedBy" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="FromServiceUnit" HeaderText="Request Unit" UniqueName="FromServiceUnit"
                    SortExpression="FromServiceUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ToServiceUnit" HeaderText="To Unit" UniqueName="ToServiceUnit"
                    SortExpression="ToServiceUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="WorkType" HeaderText="Work Type"
                    UniqueName="WorkType" SortExpression="WorkType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="WorkStatus" HeaderText="Work Status"
                    UniqueName="WorkStatus" SortExpression="WorkStatus" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                <telerik:GridBoundColumn DataField="AssetName" HeaderText="Asset" UniqueName="AssetName"
                    SortExpression="AssetName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ProblemDescription" HeaderText="Problem Description"
                    UniqueName="ProblemDescription" SortExpression="ProblemDescription" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn HeaderStyle-Width="180px" HeaderText="Accepted By" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtAcceptedBy" runat="server" Width="150px" DbValue='<%#Eval("AcceptedBy")%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="AcceptedBy" HeaderText="Accepted By" UniqueName="AcceptedBy"
                    SortExpression="AcceptedBy" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    Visible="False" />
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" Visible="False">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"openDialog('{0}'); return false;\">{1}</a>",
                                                                    DataBinder.Eval(Container.DataItem, "OrderNo"),
                                                                                        "<img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit Accepted By\" />"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
