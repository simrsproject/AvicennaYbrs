<%@ Page AutoEventWireup="true" CodeBehind="ReconcileDetail.aspx.cs" 
    Inherits="Temiang.Avicenna.Module.Finance.CashManagement.ReconcileV2.ReconcileDetail"
    Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" language="javascript">

            function fw_tbarInquiry_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "newUpload":
                        openWindowUpload();
                        args.set_cancel(true); // Cegah postback
                        break;
                    case "refresh":
                        __doPostBack("<%=grdInquiry.UniqueID %>", "rebind");
                        break;
                }
            }

            function openWindowUpload() {
                var oWnd = $find("<%=winInquiry.ClientID %>");
                oWnd.setUrl("InquiryImportDialog.aspx?bankid=<%=BankID%>");
                oWnd.show();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd) {
                //Jika apply di click
                var arg = oWnd.argument;
                if (arg != null) {
                    if (oWnd.argument.command == 'rebind') {
                        __doPostBack("<%=grdInquiry.UniqueID %>", "rebind");
                    }
                }
            }
        </script>
    </telerik:RadCodeBlock>
<%--    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransaction" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtBankBalance" />
                    <telerik:AjaxUpdatedControl ControlID="txtBalance" />
                    <telerik:AjaxUpdatedControl ControlID="txtDifferent" />
                    <telerik:AjaxUpdatedControl ControlID="txtCurrentBalance" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtBankBalance">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDifferent" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTransaction">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransaction" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtBankBalance" />
                    <telerik:AjaxUpdatedControl ControlID="txtBalance" />
                    <telerik:AjaxUpdatedControl ControlID="txtDifferent" />
                    <telerik:AjaxUpdatedControl ControlID="txtCurrentBalance" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="pgInquiry">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdInquiryDetail" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>--%>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose" ID="winInquiry">
    </telerik:RadWindow>

    <asp:Label ID="lblJournalId" runat="server" Visible="false"></asp:Label>

        <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpagReconcile">
            <Tabs>
                <telerik:RadTab runat="server" Text="Reconcile" Selected="True" PageViewID="pgReconcile" />
                <telerik:RadTab runat="server" Text="Inquiry" PageViewID="pgInquiry" />
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="mpagReconcile" runat="server" BorderStyle="Solid" SelectedIndex="0"
            BorderColor="Gray">
            <telerik:RadPageView ID="pgReconcile" runat="server">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 40%; vertical-align: top;">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        Bank
                                    </td>
                                    <td class="entry">
                                        <asp:Label ID="lblBankName" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 20px;">

                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Date / Balance
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtDate" runat="server" Width="105px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkAllTransaction" runat="server" Text="All Transaction" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkCTransaction" runat="server" Text="Cash Transaction" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkBInquiry" runat="server" Text="Bank Inquiry" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 20px;">
                                        <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                    <td style="text-align: left">
                            
                                    </td>
                                </tr>
                                <tr>
                                <td class="label">
                                    <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" MaxLength="100" />
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilterDescription" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                            </table>
                        </td>
                        <td style="width: 30%; vertical-align: top;">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        Bank Balance
                                    </td>
                                    <td class="entry">
                                        
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtBankBalance" runat="server" 
                                                        OnTextChanged="txtBankBalance_TextChanged" AutoPostBack="true">
                                                        <NumberFormat DecimalDigits="2" />
                                                    </telerik:RadNumericTextBox> 
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkBankBalanceFromInquiry" runat="server" Text="Load from inquiry" 
                                                        OnCheckedChanged="chkBankBalanceFromInquiry_CheckedChanged" AutoPostBack="true" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 20px;">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Reconciled Balance
                                    </td>
                                    <td class="entry">
                                        <telerik:RadNumericTextBox ID="txtBalance" runat="server">
                                            <NumberFormat DecimalDigits="2" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td style="width: 20px;">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 30%; vertical-align: top;">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        Different
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtDifferent" runat="server">
                                                        <NumberFormat DecimalDigits="2" />
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <a href="../CashEntryV2/CashEntryDetail.aspx?md=new&source=reV2&bankid=<%=Request.QueryString["bankid"] %>">Adjust</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 20px;">
                                    </td>
                                    <td>
                            
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Actual Balance
                                    </td>
                                    <td class="entry">
                                        <telerik:RadNumericTextBox ID="txtCurrentBalance" runat="server">
                                            <NumberFormat DecimalDigits="2" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td style="width: 20px;">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <fieldset>
                                <legend>In Progress</legend>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="label">
                                            Selected Cash/Bank Transaction
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtSelectedCashTrans" runat="server" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </td>
                                        <td class="label">
                                            Selected Inquiry
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtSelectedInquiry" runat="server" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </td>
                                        <td>

                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnClearSelected" runat="server" OnClick="btnClearSelected_Click" ImageUrl="~/Images/Toolbar/new16.png" ToolTip="Clear Selected" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:ImageButton ID="btnSaveReconcile" runat="server" OnClick="btnSaveReconcile_Click" ImageUrl="~/Images/Toolbar/save16.png" ToolTip="Save Selected" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>

                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 50%; vertical-align: top;">
                            <telerik:RadGrid ID="grdTransaction" runat="server" AutoGenerateColumns="false"
                                GridLines="None" OnNeedDataSource="grdTransaction_OnNeedDataSource" OnItemDataBound="grdTransaction_ItemDataBound"
                                AllowPaging="True" PageSize="18" AllowSorting="False" AllowCustomPaging="true" >
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                <MasterTableView ClientDataKeyNames="TxnBalanceId" DataKeyNames="TxnBalanceId" 
                                InsertItemPageIndexAction="ShowItemOnCurrentPage">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="TransactionDate" HeaderText="Date"
                                            DataFormatString="{0:MM/dd/yyyy}" DataType="System.DateTime" 
                                            SortExpression="TransactionDate" UniqueName="TransactionDate" >
                                            <HeaderStyle HorizontalAlign="Left" Width="130" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Description" HeaderText="Description" SortExpression="Description"
                                            UniqueName="Description">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="DebitAmount" HeaderText="Debit" DataType="System.Decimal" DataFormatString="{0:N2}"
                                            UniqueName="DebitAmount" SortExpression="DebitAmount" >
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="CreditAmount" HeaderText="Credit" DataType="System.Decimal" DataFormatString="{0:N2}"
                                            UniqueName="CreditAmount" SortExpression="CreditAmount" >
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridTemplateColumn UniqueName="fStatus" HeaderText="" Groupable="false">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                                    runat="server" /><br />
                                                <span>&nbsp;</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="detailChkbox" OnCheckedChanged="detailChkbox_CheckedChanged" AutoPostBack="true" 
                                                    runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <FilterMenu>
                                </FilterMenu>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                        <td style="width: 50%; vertical-align: top;">
                            <telerik:RadGrid ID="grdInquiryTransaction" runat="server" AutoGenerateColumns="false"
                                GridLines="None" OnNeedDataSource="grdInquiryTransaction_NeedDataSource" OnItemDataBound="grdInquiryTransaction_ItemDataBound"
                                AllowPaging="True" PageSize="18" AllowSorting="False" AllowCustomPaging="true" >
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                <MasterTableView ClientDataKeyNames="TransactionID" DataKeyNames="TransactionID" 
                                InsertItemPageIndexAction="ShowItemOnCurrentPage">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="TransactionDateTime" HeaderText="Date"
                                            DataFormatString="{0:MM/dd/yyyy}" DataType="System.DateTime" 
                                            SortExpression="TransactionDateTime" UniqueName="TransactionDateTime" >
                                            <HeaderStyle HorizontalAlign="Left" Width="130" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Description" HeaderText="Description" SortExpression="Description"
                                            UniqueName="Description">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="ReferenceNo" SortExpression="ReferenceNo"
                                            UniqueName="ReferenceNo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="Debit" HeaderText="Debit" DataType="System.Decimal" DataFormatString="{0:N2}"
                                            UniqueName="Debit" SortExpression="Debit" >
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="Credit" HeaderText="Credit" DataType="System.Decimal" DataFormatString="{0:N2}"
                                            UniqueName="Credit" SortExpression="Credit" >
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="Balance" HeaderText="Balance" DataType="System.Decimal" DataFormatString="{0:N2}"
                                            UniqueName="Balance" SortExpression="Balance" >
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridTemplateColumn UniqueName="fStatus" HeaderText="" Groupable="false">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                                    runat="server" /><br />
                                                <span>&nbsp;</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="detailChkbox" OnCheckedChanged="detailChkbox_CheckedChanged" AutoPostBack="true" 
                                                    runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <FilterMenu>
                                </FilterMenu>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                     </tr>
                </table>
                
            </telerik:RadPageView>
            <telerik:RadPageView ID="pgInquiry" runat="server">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 35%; vertical-align: top;">
                             <telerik:RadGrid ID="grdInquiry" runat="server" AutoGenerateColumns="false"
                                GridLines="None" OnNeedDataSource="grdInquiry_NeedDataSource" OnItemCommand="grdInquiry_ItemCommand"
                                 OnDeleteCommand="grdInquiry_DeleteCommand"
                                AllowPaging="True" PageSize="18" AllowSorting="False" AllowCustomPaging="true" >
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                <MasterTableView ClientDataKeyNames="InquiryID" DataKeyNames="InquiryID" 
                                InsertItemPageIndexAction="ShowItemOnCurrentPage" CommandItemDisplay="Top">
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar ID="fw_tbarInquiry" runat="server" Width="100%" EnableEmbeddedScripts="false"
                                            OnClientButtonClicking="fw_tbarInquiry_OnClientButtonClicking">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                             <Items>
                                                <telerik:RadToolBarButton runat="server" Text="New Upload" AccessKey="N" Value="newUpload" ImageUrl="~/Images/Toolbar/insert16.png"
                                                HoveredImageUrl="~/Images/Toolbar/insert16_h.png" DisabledImageUrl="~/Images/Toolbar/insert16_d.png" />
                                                 <telerik:RadToolBarButton runat="server" Text="Refresh" AccessKey="R" Value="refresh" ImageUrl="~/Images/Toolbar/refresh16.png"
                                                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="BankID" HeaderText="BankID" SortExpression="BankID"
                                            UniqueName="BankID">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TransactionDate" HeaderText="Date"
                                            DataFormatString="{0:MM/dd/yyyy}" DataType="System.DateTime" 
                                            SortExpression="TransactionDate" UniqueName="TransactionDate" >
                                            <HeaderStyle HorizontalAlign="Left" Width="130" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="Debit" HeaderText="Debit" DataType="System.Decimal" DataFormatString="{0:N2}"
                                            UniqueName="Debit" SortExpression="Debit" >
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="Credit" HeaderText="Credit" DataType="System.Decimal" DataFormatString="{0:N2}"
                                            UniqueName="Credit" SortExpression="Credit" >
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn DataField="CreatedByUserID" HeaderText="Created By" SortExpression="CreatedByUserID"
                                            UniqueName="CreatedByUserID">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                                <FilterMenu>
                                </FilterMenu>
                                <ClientSettings EnablePostBackOnRowClick="true">  
                                    <Selecting AllowRowSelect="true" /> 
                                </ClientSettings> 
                            </telerik:RadGrid>
                        </td>
                        <td style="width: 65%; vertical-align: top;">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>Cash Transaction Code</legend>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td class="label">
                                                        Transaction Code
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboTransCode" runat="server"></telerik:RadComboBox>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:ImageButton ID="btnSaveTransCode" runat="server" OnClick="btnSaveTransCode_Click" ImageUrl="~/Images/Toolbar/save16.png" ToolTip="Save Transaction Code For Selected Row" />
                                                    </td>
                                                </tr>
                                            </table>
                                         </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                            </table>
                            <telerik:RadGrid ID="grdInquiryDetail" runat="server" AutoGenerateColumns="false"
                                GridLines="None" OnNeedDataSource="grdInquiryDetail_NeedDataSource"
                                AllowPaging="True" PageSize="18" AllowSorting="False" AllowCustomPaging="true" >
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                <MasterTableView ClientDataKeyNames="TransactionID" DataKeyNames="TransactionID" 
                                InsertItemPageIndexAction="ShowItemOnCurrentPage">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="TransactionDateTime" HeaderText="Date"
                                            DataFormatString="{0:MM/dd/yyyy}" DataType="System.DateTime" 
                                            SortExpression="TransactionDateTime" UniqueName="TransactionDateTime" >
                                            <HeaderStyle HorizontalAlign="Left" Width="130" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Description" HeaderText="Description" SortExpression="Description"
                                            UniqueName="Description">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="ReferenceNo" SortExpression="ReferenceNo"
                                            UniqueName="ReferenceNo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="Debit" HeaderText="Debit" DataType="System.Decimal" DataFormatString="{0:N2}"
                                            UniqueName="Debit" SortExpression="Debit" >
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="Credit" HeaderText="Credit" DataType="System.Decimal" DataFormatString="{0:N2}"
                                            UniqueName="Credit" SortExpression="Credit" >
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="Balance" HeaderText="Balance" DataType="System.Decimal" DataFormatString="{0:N2}"
                                            UniqueName="Balance" SortExpression="Balance" >
                                            <HeaderStyle HorizontalAlign="Right" Width="150" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn DataField="SRCashTransactionCode" HeaderText="Trans Code" SortExpression="SRCashTransactionCode"
                                            UniqueName="SRCashTransactionCode">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="fStatus" HeaderText="" Groupable="false">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                                    runat="server" /><br />
                                                <span>&nbsp;</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="detailChkbox" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <FilterMenu>
                                </FilterMenu>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    
</asp:Content>
