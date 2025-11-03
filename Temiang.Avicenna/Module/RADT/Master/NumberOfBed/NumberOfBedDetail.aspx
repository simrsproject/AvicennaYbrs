<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="NumberOfBedDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.NumberOfBedDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../../../../JavaScript/DateFormat.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "list":
                        location.replace('NumberOfBedList.aspx');
                        break;
                    case "new":
                        __doPostBack("<%= grdList.UniqueID %>", 'new');
                        break;
                    case "save":
                        __doPostBack("<%= grdList.UniqueID %>", 'save');
                        break;
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtStartingDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboClassID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdListSmf" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />
                    <telerik:AjaxUpdatedControl ControlID="cboClassID" />
                    <telerik:AjaxUpdatedControl ControlID="txtStartingDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListSmf">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListSmf" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />
                    <telerik:AjaxUpdatedControl ControlID="cboClassID" />
                    <telerik:AjaxUpdatedControl ControlID="txtStartingDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar runat="server" ID="tbNavigation" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="List" Value="list"
                ImageUrl="~/Images/Toolbar/views16.png" HoveredImageUrl="~/Images/Toolbar/views16_h.png"
                DisabledImageUrl="~/Images/Toolbar/views16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="New" Value="new"
                ImageUrl="~/Images/Toolbar/new16.png" HoveredImageUrl="~/Images/Toolbar/new16_h.png"
                DisabledImageUrl="~/Images/Toolbar/new16_d.png" />    
            <telerik:RadToolBarButton runat="server" Text="Save" Value="save"
                ImageUrl="~/Images/Toolbar/save16.png" HoveredImageUrl="~/Images/Toolbar/save16_h.png"
                DisabledImageUrl="~/Images/Toolbar/save16_d.png" />
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
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Starting Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtStartingDate" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedDateChanged="txtStartingDate_SelectedDateChanged" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Class
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboClassID" Width="304px" AutoPostBack="true"
                                                 OnSelectedIndexChanged="cboClassID_SelectedIndexChanged" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1">
        <Tabs>
            <telerik:RadTab runat="server" Text="Service Unit" Selected="True" PageViewID="pgUnit">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="SMF" PageViewID="pgSmf">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgUnit" runat="server">
            <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                OnNeedDataSource="grdList_NeedDataSource" OnItemCommand="grdList_ItemCommand">
                <MasterTableView DataKeyNames="ServiceUnitID">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="ServiceUnitID" HeaderText="ID"
                            UniqueName="ServiceUnitID" SortExpression="ServiceUnitID" HeaderStyle-HorizontalAlign="center"
                            ItemStyle-HorizontalAlign="center" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="85px" HeaderStyle-HorizontalAlign="center"
                            HeaderText="Number Of Bed" ItemStyle-HorizontalAlign="center" UniqueName="TemplateColumn1"
                            DataField="NumberOfBed">
                            <ItemTemplate>
                                <telerik:RadTextBox runat="server" ID="txtNumberOfBed" Width="100%" CssClass="RightAligned"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "NumberOfBed") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="NumberOfBed" HeaderText="NumberOfBed" UniqueName="NumberOfBed"
                            SortExpression="NumberOfBed" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="85px"
                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgSmf" runat="server">
            <telerik:RadGrid ID="grdListSmf" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                OnNeedDataSource="grdListSmf_NeedDataSource" OnItemCommand="grdListSmf_ItemCommand">
                <MasterTableView DataKeyNames="SmfID">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SmfID" HeaderText="ID"
                            UniqueName="SmfID" SortExpression="SmfID" HeaderStyle-HorizontalAlign="center"
                            ItemStyle-HorizontalAlign="center" />
                        <telerik:GridBoundColumn DataField="SmfName" HeaderText="SMF" UniqueName="SmfName"
                            SortExpression="SmfName" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="85px" HeaderStyle-HorizontalAlign="center"
                            HeaderText="Number Of Bed" ItemStyle-HorizontalAlign="center" UniqueName="TemplateColumn1"
                            DataField="NumberOfBed">
                            <ItemTemplate>
                                <telerik:RadTextBox runat="server" ID="txtNumberOfBed" Width="100%" CssClass="RightAligned"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "NumberOfBed") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="NumberOfBed" HeaderText="NumberOfBed" UniqueName="NumberOfBed"
                            SortExpression="NumberOfBed" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="85px"
                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" HorizontalAlign="NotSet" />
</asp:Content>
