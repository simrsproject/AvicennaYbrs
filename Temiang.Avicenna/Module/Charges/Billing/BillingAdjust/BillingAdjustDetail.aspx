<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    Codebehind="BillingAdjustDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.BillingAdjustDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script language="javascript" type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                
                switch (val) {
                    case "list":
                        window.location = "FinalizeBilling/FinalizeBillingVerification.aspx?<%= Request.QueryString.ToString() %>";
                        break;
                    case "copy":
                        if (confirm('Are you sure want to copy all transaction for this registration?'))
                            __doPostBack("<%= grdCostCalculation.UniqueID %>", 'copy');
                        break;
                    case "calculate":
                        if (confirm('Are you sure want to calculate the adjustment?'))
                            __doPostBack("<%= grdCostCalculation.UniqueID %>", 'calculate');
                        break;
                    case "save":
                        if (confirm('Are you sure want to save adjustment?'))
                            __doPostBack("<%= grdCostCalculation.UniqueID %>", 'save');
                        break;
                    case "delete":
                        if (confirm('Are you sure want to delete adjustment?'))
                            __doPostBack("<%= grdCostCalculation.UniqueID %>", 'delete');
                        break;
                    case "refresh":
                            __doPostBack("<%= grdCostCalculation.UniqueID %>", 'refresh');
                        break;
                    case "setting":
                        openWinSetting();
                        break;
                    case "print":
                        __doPostBack("<%= grdCostCalculation.UniqueID %>", 'printd');
                        break;
                }
                
                function openWinSetting() {
                    var oWnd = $find("<%= winSetting.ClientID %>");
                    oWnd.SetUrl("./BillingAdjustSettingDialog.aspx");
                    oWnd.Show();
                    oWnd.Maximize();
                }
                
                function onClientClose(oWnd, args) {
                    // do your things
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="radgrdItemGroup" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdFeePreview" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblTransactionAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblAdmAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblTotalAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblPlafonAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblSimulationAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblDifferent" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCostCalculation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCostCalculation" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="radgrdItemGroup" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdFeePreview" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblTransactionAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblAdmAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblTotalAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblPlafonAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblSimulationAmount" />
                    <telerik:AjaxUpdatedControl ControlID="lblDifferent" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="List" Value="list" ImageUrl="~/Images/Toolbar/details16.png"
                HoveredImageUrl="~/Images/Toolbar/details16_h.png" DisabledImageUrl="~/Images/Toolbar/details16_d.png" />
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
            <telerik:RadToolBarButton runat="server" Text="Copy" Value="copy" Visible="false" ImageUrl="~/Images/Toolbar/copy16.png"
                HoveredImageUrl="~/Images/Toolbar/copy16.png" DisabledImageUrl="~/Images/Toolbar/copy16.png" />
            <telerik:RadToolBarButton runat="server" Text="Calculate" Value="calculate" ImageUrl="~/Images/Toolbar/process16.png"
                HoveredImageUrl="~/Images/Toolbar/process16_h.png" DisabledImageUrl="~/Images/Toolbar/process16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Save" Value="save" ImageUrl="~/Images/Toolbar/save16.png"
                HoveredImageUrl="~/Images/Toolbar/save16_h.png" DisabledImageUrl="~/Images/Toolbar/save16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Delete" Value="delete" ImageUrl="~/Images/Toolbar/delete16.png"
                HoveredImageUrl="~/Images/Toolbar/delete16_h.png" DisabledImageUrl="~/Images/Toolbar/delete16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Setting" Value="setting" ImageUrl="~/Images/Toolbar/process16.png"
                HoveredImageUrl="~/Images/Toolbar/process16_h.png" DisabledImageUrl="~/Images/Toolbar/process16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Print" Value="print" ImageUrl="~/Images/Toolbar/print16.png"
                HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <telerik:RadWindow ID="winSetting" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxLoadingPanel ID="ajxLoadingPanel" runat="server" Transparency="30">
        <img alt="Loading..." src='<%= RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Ajax.loading.gif") %>'
            style="border: 0px; margin-top: 75px;" />
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="ajxPanel" runat="server" Width="100%" LoadingPanelID="ajxLoadingPanel">
        <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry"
            BackColor="#FFFFC0" Font-Size="Small" BorderColor="#FFC080" BorderStyle="Solid" EnableClientScript="true" />
        <asp:Panel ID="pnlInformation" Width="99%" runat="server" Visible="false" BorderColor="#FFC080"
            BackColor="#FFFFC0" BorderStyle="Solid">
            <table width="90%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50px">
                        <asp:Image ID="imgAttention" runat="server" ImageUrl="~/Images/AttentionLarge.png" />
                    </td>
                    <td align="left" valign="middle">
                        <asp:Label ID="lblInformation" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="width:320px" rowspan="2" valign="top">
                <!-- page -->
                <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
                    Orientation="HorizontalTop">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="-" PageViewID="pg1"
                            Selected="True">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Fee Preview" PageViewID="pg2">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
                    <telerik:RadPageView ID="pg1" runat="server" Selected="true">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Patient Information">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>Registration No</td>
                                                <td>:</td>
                                                <td><asp:Label ID="lblRegistrationNo" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Medical No</td>
                                                <td>:</td>
                                                <td><asp:Label ID="lblMedicalNo" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Patient Name</td>
                                                <td>:</td>
                                                <td><asp:Label ID="lblPatientName" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Gender</td>
                                                <td>:</td>
                                                <td><asp:RadioButtonList ID="rblSex" runat="server" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                    <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                </asp:RadioButtonList></td>
                                            </tr>
                                            <tr>
                                                <td>Age</td>
                                                <td>:</td>
                                                <td><asp:Label ID="lblAge" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                        <hr />
                                    </cc:CollapsePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="2" border="0" width="100%">
                                        <tr>
                                            <td>-</td>
                                            <td>Transaction Amount</td>
                                            <td align="right"><asp:Label id="lblTransactionAmount" runat="server"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>-</td>
                                            <td>Administration Amount</td>
                                            <td align="right"><asp:Label id="lblAdmAmount" runat="server"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td><hr /></td>
                                            <td>+</td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td align="right">Total Amount</td>
                                            <td align="right"><asp:Label id="lblTotalAmount" runat="server"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td><hr /></td>
                                            <td align="right"><hr /></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>-</td>
                                            <td>Grouping</td>
                                            <td align="right"><asp:Label id="lblPlafonAmount" runat="server"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>-</td>
                                            <td>Real Cost</td>
                                            <td align="right"><asp:Label id="lblSimulationAmount" runat="server"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td><hr /></td>
                                            <td>-</td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td align="right">Different</td>
                                            <td align="right"><asp:Label id="lblDifferent" runat="server"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td><hr /></td>
                                            <td align="right"><hr /></td>
                                            <td></td>
                                        </tr>
                                    </table>        
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div> <!-- style="height:380px; overflow:scroll;"> -->
                                        <telerik:RadGrid ID="radgrdItemGroup" runat="server" OnNeedDataSource="radgrdItemGroup_NeedDataSource"
                                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                                            <HeaderContextMenu>
                                            </HeaderContextMenu>
                                            <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemGroupID">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ItemGroupName" HeaderText="Item Group" UniqueName="ItemGroupName"
                                                        SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                    
                                                    <telerik:GridTemplateColumn UniqueName="disc" HeaderStyle-Width="50px" HeaderText="%">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txtDisc" runat="server"
                                                                Width="30px" IncrementSettings-InterceptMouseWheel="false">
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                                                        <HeaderTemplate>
                                                            <label>Auto</label>
                                                            <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedStateItemGroup" AutoPostBack="True"
                                                                runat="server" Checked="false"></asp:CheckBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkAutoAdjustItemGroup" runat="server" Checked="False"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    
                                                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="85px">
                                                        <ItemTemplate>
                                                            <asp:RadioButtonList ID="rblDisc" runat="server" BorderStyle="None" CellPadding="0" CellSpacing="0" BorderWidth="0">
                                                                <asp:ListItem Text="Disc Tarif" Value="0" Selected />
                                                                <asp:ListItem Text="Grouping" Value="1" />
                                                            </asp:RadioButtonList>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <FilterMenu>
                                            </FilterMenu>
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Resizing AllowColumnResize="True" />
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                        <telerik:RadGrid ID="radgrdServiceUnit" runat="server" OnNeedDataSource="radgrdServiceUnit_NeedDataSource"
                                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true" Visible="false">
                                            <HeaderContextMenu>
                                            </HeaderContextMenu>
                                            <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                                        SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                    
                                                    <telerik:GridTemplateColumn UniqueName="disc" HeaderStyle-Width="50px" HeaderText="%">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txtDisc" runat="server"
                                                                Width="30px" IncrementSettings-InterceptMouseWheel="false">
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px" Visible="false">
                                                        <HeaderTemplate>
                                                            <label>Auto</label>
                                                            <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedStateServiceUnit" AutoPostBack="True"
                                                                runat="server" Checked="false"></asp:CheckBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkServiceUnit" runat="server" Checked="False"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    
                                                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="85px">
                                                        <ItemTemplate>
                                                            <asp:RadioButtonList ID="rblDisc" runat="server" BorderStyle="None" CellPadding="0" CellSpacing="0" BorderWidth="0">
                                                                <asp:ListItem Text="Disc Tarif" Value="0" Selected />
                                                                <asp:ListItem Text="Grouping" Value="1" />
                                                            </asp:RadioButtonList>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <FilterMenu>
                                            </FilterMenu>
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Resizing AllowColumnResize="True" />
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                        <telerik:RadGrid ID="radgrdItemType" runat="server" OnNeedDataSource="radgrdItemType_NeedDataSource"
                                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true" Visible="false">
                                            <HeaderContextMenu>
                                            </HeaderContextMenu>
                                            <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Type" UniqueName="ItemName"
                                                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                    
                                                    <telerik:GridTemplateColumn UniqueName="disc" HeaderStyle-Width="50px" HeaderText="%">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txtDisc" runat="server"
                                                                Width="30px" IncrementSettings-InterceptMouseWheel="false">
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px" Visible="false">
                                                        <HeaderTemplate>
                                                            <label>Auto</label>
                                                            <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedStateItemType" AutoPostBack="True"
                                                                runat="server" Checked="false"></asp:CheckBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkItemType" runat="server" Checked="False"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    
                                                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="85px">
                                                        <ItemTemplate>
                                                            <asp:RadioButtonList ID="rblDisc" runat="server" BorderStyle="None" CellPadding="0" CellSpacing="0" BorderWidth="0">
                                                                <asp:ListItem Text="Disc Tarif" Value="0" Selected />
                                                                <asp:ListItem Text="Grouping" Value="1" />
                                                            </asp:RadioButtonList>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <FilterMenu>
                                            </FilterMenu>
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Resizing AllowColumnResize="True" />
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pg2" runat="server">
                        <telerik:RadGrid ID="grdFeePreview" runat="server" OnNeedDataSource="grdFeePreview_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo, TariffComponentID">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item" UniqueName="ItemName"
                                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Comp" UniqueName="TariffComponentName"
                                        SortExpression="TariffComponentName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridNumericColumn DataField="FeeAmount" HeaderText="Fee" UniqueName="FeeAmount"
                                        SortExpression="FeeAmount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right"
                                        DataFormatString="{0:n2}" >    
                                    </telerik:GridNumericColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
            <td valign="top"></td>
        </tr>
        <tr>
            <td valign="top">
                <telerik:RadGrid ID="grdCostCalculation" runat="server" OnNeedDataSource="grdCostCalculation_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" ShowFooter="true"
                    OnItemDataBound="grdCostCalculation_ItemDataBound">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, TransactionNo, SequenceNo"
                        FilterExpression="ParentNo IS NULL OR ParentNo = '' ">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="TransactionNo" HeaderText="Transaction No"
                                UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="ReferenceNo" HeaderText="Correction For"
                                UniqueName="ReferenceNo" SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="false" />
                            <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" Visible="False" />
                            <telerik:GridBoundColumn DataField="TransactionDate" HeaderText="Date" UniqueName="TransactionDate"
                                SortExpression="TransactionDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                                UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="false" />
                            <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="ClassName" HeaderText="Class"
                                UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Aggregate="count" FooterAggregateFormatString="Total :"
                                FooterStyle-HorizontalAlign="Right" />
                            
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="AmountTotal" HeaderText="Amount Transaction"
                                UniqueName="AmountTotal" SortExpression="AmountTotal" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            
                            <telerik:GridTemplateColumn UniqueName="AmountAdjusted" HeaderStyle-Width="100px"
                                DataField="AmountAdjusted" ItemStyle-HorizontalAlign="Center" HeaderText="Real Cost"
                                HeaderStyle-HorizontalAlign="center" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                                FooterStyle-HorizontalAlign="right">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtAmountAdjusted" runat="server" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "AmountAdjusted")) %>'
                                        Width="80px" OnTextChanged="txtAmountAdjusted_TextChanged" AutoPostBack="true" IncrementSettings-InterceptMouseWheel="false">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Parent No" UniqueName="ParentNo"
                                SortExpression="ParentNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                Visible="false" />
                            
                            <telerik:GridTemplateColumn UniqueName="disc" HeaderStyle-Width="50px" HeaderText="%">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtDisc" runat="server" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "AdjustedDiscAmount")) %>'
                                        Width="30px" OnTextChanged="txtDisc_TextChanged" AutoPostBack="true" IncrementSettings-InterceptMouseWheel="false">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="190px">
                                <ItemTemplate>
                                    <asp:RadioButtonList ID="rblDisc" runat="server" 
                                        BorderStyle="None" CellPadding="0" CellSpacing="0" BorderWidth="0"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Disc Tarif" Value="0" Selected />
                                        <asp:ListItem Text="Grouping" Value="1" />
                                    </asp:RadioButtonList>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>    
</asp:Content>
