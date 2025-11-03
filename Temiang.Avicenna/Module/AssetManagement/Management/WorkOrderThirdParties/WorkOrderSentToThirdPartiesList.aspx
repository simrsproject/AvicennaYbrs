<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="WorkOrderSentToThirdPartiesList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.WorkOrderSentToThirdPartiesList"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                __doPostBack("<%= grdList.UniqueID %>", 'process');
            }
            function rowPrint(woNo) {
                __doPostBack("<%= grdList.UniqueID %>", 'print|' + woNo);
            }
            function openDialog(woNo) {
                var oWnd = $find("<%= winDialog.ClientID %>");

                oWnd.setUrl("WorkOrderSentToThirdPartiesDialog.aspx?wono=" + woNo);
                oWnd.show();
            }
            function onClientClose(oWnd, args) {
                __doPostBack("<%= grdList.UniqueID %>", "rebindo");
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Move, Close"
        Width="600px" Height="300px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" OnClientClose="onClientClose" />
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchOrderDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListSend" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchOrderNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListSend" />
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
                    <telerik:AjaxUpdatedControl ControlID="grdListSend" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchToServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListSend" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSentDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListSend" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListSend" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListSend">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListSend" />
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
                                            <telerik:RadDatePicker ID="txtOrderDate" runat="server" Width="100px">
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
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Work Order Outstanding" PageViewID="pgWoo" Selected="True" />
            <telerik:RadTab runat="server" Text="Sent To Third Parties" PageViewID="pgWo" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgWoo" runat="server" Selected="true">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFromReceivedFromLogisticsDateTime" runat="server" Text="Realization Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtFromReceivedFromLogisticsDateTime" runat="server" Width="100px">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                &nbsp;-&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtToReceivedFromLogisticsDateTime" runat="server" Width="100px">
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
                    <td style="width: 50%; vertical-align: top">
                    </td>
                </tr>
            </table>
            <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
                <Items>
                    <telerik:RadToolBarButton runat="server" Text="Send" Value="process" ImageUrl="~/Images/arrowright16.png"
                        HoveredImageUrl="~/Images/arrowright16.png" DisabledImageUrl="~/Images/arrowright16.png" />
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
                        <telerik:GridTemplateColumn UniqueName="checkbox">
                            <HeaderStyle Width="25px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="processChkBox" runat="server" Checked="false"></asp:CheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="datepicker" HeaderText="Date Sent">
                            <HeaderStyle Width="110px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <telerik:RadDatePicker ID="txtSentDate" runat="server" Width="100px" SelectedDate='<%# DataBinder.Eval(Container.DataItem, "SentDate") %>'>
                                </telerik:RadDatePicker>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="LetterNo" HeaderText="Letter No">
                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <telerik:RadTextBox ID="txtLetterNo" runat="server" Width="100px" DbValue='<%#Eval("LetterNo")%>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Supplier" UniqueName="supplier" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <telerik:RadComboBox ID="cboSupplierID" runat="server" Width="100%" EnableLoadOnDemand="true"
                                    OnItemsRequested="cboSupplierID_ItemsRequested" OnItemDataBound="cboSupplierID_ItemDataBound">
                                </telerik:RadComboBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="135px" DataField="OrderNo" HeaderText="Work Order No"
                            UniqueName="OrderNo" SortExpression="OrderNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="OrderDate" HeaderText="Order Date" UniqueName="OrderDate"
                            SortExpression="OrderDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}"
                            Visible="False">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LastRealizationDateTime" HeaderText="Realization Date"
                            UniqueName="LastRealizationDateTime" SortExpression="LastRealizationDateTime"
                            DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ImplementedBy" HeaderText="Implemented By" UniqueName="ImplementedBy"
                            SortExpression="ImplementedBy" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            Visible="False" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="FromServiceUnit" HeaderText="Request Unit"
                            UniqueName="FromServiceUnit" SortExpression="FromServiceUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ToServiceUnit" HeaderText="To Unit" UniqueName="ToServiceUnit"
                            SortExpression="ToServiceUnit" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="WorkType" HeaderText="Work Type"
                            UniqueName="WorkType" SortExpression="WorkType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="WorkStatus" HeaderText="Work Status"
                            UniqueName="WorkStatus" SortExpression="WorkStatus" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="False" />
                        <telerik:GridBoundColumn DataField="AssetName" HeaderText="Asset" UniqueName="AssetName"
                            SortExpression="AssetName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ProblemDescription"
                            HeaderText="Problem Description" UniqueName="ProblemDescription" SortExpression="ProblemDescription"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgWo" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSentDate" runat="server" Text="Date Sent"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtSentDate" runat="server" Width="100px">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                &nbsp;-&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtToSentDate" runat="server" Width="100px">
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="30px">
                                    <asp:ImageButton ID="btnSearchSentDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdListSend" runat="server" OnNeedDataSource="grdListSend_NeedDataSource"
                AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="OrderNo">
                    <Columns>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="35px">
                            <ItemTemplate>
                                <%#string.Format("<a href=\"#\" onclick=\"rowPrint('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"Print\" title=\"Print\" /></a>",
                                                                  DataBinder.Eval(Container.DataItem, "OrderNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="35px">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"openDialog('{0}'); return false;\">{1}</a>",
                                                                    DataBinder.Eval(Container.DataItem, "OrderNo"),
                                                                                        "<img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" />"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="SentDate" HeaderText="Date Sent"
                            UniqueName="SentDate" SortExpression="SentDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="LetterNo" HeaderText="Letter No"
                            UniqueName="LetterNo" SortExpression="LetterNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SupplierName" HeaderText="Supplier" UniqueName="SupplierName"
                            SortExpression="SupplierName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="135px" DataField="OrderNo" HeaderText="Work Order No"
                            UniqueName="OrderNo" SortExpression="OrderNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="OrderDate" HeaderText="Order Date" UniqueName="OrderDate"
                            SortExpression="OrderDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}"
                            Visible="False">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LastRealizationDateTime" HeaderText="Realization Date"
                            UniqueName="LastRealizationDateTime" SortExpression="LastRealizationDateTime"
                            DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ImplementedBy" HeaderText="Implemented By" UniqueName="ImplementedBy"
                            SortExpression="ImplementedBy" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            Visible="False" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="FromServiceUnit" HeaderText="Request Unit"
                            UniqueName="FromServiceUnit" SortExpression="FromServiceUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ToServiceUnit" HeaderText="To Unit"
                            UniqueName="ToServiceUnit" SortExpression="ToServiceUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="WorkType" HeaderText="Work Type"
                            UniqueName="WorkType" SortExpression="WorkType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="WorkStatus" HeaderText="Work Status"
                            UniqueName="WorkStatus" SortExpression="WorkStatus" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="False" />
                        <telerik:GridBoundColumn DataField="AssetName" HeaderText="Asset" UniqueName="AssetName"
                            SortExpression="AssetName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ProblemDescription"
                            HeaderText="Problem Description" UniqueName="ProblemDescription" SortExpression="ProblemDescription"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" HorizontalAlign="NotSet">
    </telerik:RadAjaxPanel>
</asp:Content>
