<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="BudgetingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Budgeting.BudgetingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script language="javascript" type="text/javascript">

            var sumInput = null;
            var tempValue = 0.0;

            function Load(sender, args) {
                sumInput = sender;
            }

            function Blur(sender, args) {
                //sumInput.set_value(tempValue + sender.get_value());
            }

            function Focus(sender, args) {
                //tempValue = sumInput.get_value() - sender.get_value();
            }

            function OpenDetail(coaid, md) {
                //alert(coaid);
                var oWnd = $find("<%= winDialog.ClientID %>");
                var bno = $find("<%= txtBudgetingNo.ClientID %>").get_value();
                var rev = $find("<%= txtRev.ClientID %>").get_value();


                oWnd.setUrl("BudgetingDetailDialog.aspx?md=" + md + "&bno=" + bno + "&rev=" + rev + "&coaid=" + coaid);

                oWnd.show();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
                //console.log(oWnd.argument);
                if (oWnd.argument == 'rebind') {
                    __doPostBack("<%= grdItemTransactionItem.UniqueID %>", "rebind");
                    oWnd.argument = 'undefined';
                } else if (oWnd.argument == 'uploaded'){
                    __doPostBack("<%= grdItemTransactionItem.UniqueID %>", "uploaded");
                    oWnd.argument = 'undefined';
                }
            }

        <%--    function ExportToExcel() {
                var oWnd = $find("<%= winDialog.ClientID %>");
                console.log(oWnd);
                oWnd.setUrl("ExportToExcelDialog.aspx");
                oWnd.show();
                return false;
            }--%>

            function OpenImportDialog() {
                var oWnd = $find("<%= winDialog.ClientID %>");
                //console.log(oWnd);
                oWnd.setUrl("ImportFromExcel.aspx");
                oWnd.show();
                oWnd.setSize(400, 100);
                return false;
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnCopy">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemTransactionItem" LoadingPanelID="lp1" />
                    <telerik:AjaxUpdatedControl ControlID="btnCopy" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnReset">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemTransactionItem" LoadingPanelID="lp1" />
                    <telerik:AjaxUpdatedControl ControlID="btnReset" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboServiceUnitID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboCopyFrom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboCopyFrom" />
                    <telerik:AjaxUpdatedControl ControlID="cboPeriod" LoadingPanelID="lp1   " />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdItemTransactionItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItemTransactionItem" LoadingPanelID="lp1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="lp1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Maximize, Move, Close"
        Width="1000px" Height="600px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" OnClientClose="onClientClose" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top; width:50%;">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Budgeting No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><telerik:RadTextBox ID="txtBudgetingNo" runat="server" Width="300px" MaxLength="20" ReadOnly="true" /></td>
                                    <td>&nbsp;</td>
                                    <td>Rev.</td>
                                    <td>&nbsp;</td>
                                    <td><telerik:RadTextBox ID="txtRev" runat="server" Width="50px" MaxLength="20" ReadOnly="true" /></td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AutoPostBack="true">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboYear" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvYear" runat="server" ErrorMessage="Year required."
                                ValidationGroup="entry" ControlToValidate="cboYear" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width:50%;">
                <fieldset>
                    <legend>Tools</legend>
                    <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="2">
                        <tr>
                            <td>
                                Copy From: &nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cboCopyFrom" runat="server" Width="100px" AutoPostBack="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="" Value="0" Selected="true" />
                                        <telerik:RadComboBoxItem Text="Budget" Value="1" />
                                        <telerik:RadComboBoxItem Text="Realization" Value="2" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                Period: &nbsp;
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadComboBox ID="cboPeriodYear" runat="server" Width="100px" OnItemsRequested="cboPeriodYear_ItemsRequested"
                                                OnItemDataBound="cboPeriodYear_ItemDataBound" EnableLoadOnDemand="true"
                                                HighlightTemplatedItems="true" MarkFirstMatch="true" >
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cboPeriodMonth" runat="server" Width="50px">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="" Text="" />
                                                    <telerik:RadComboBoxItem Value="01" Text="Jan" />
                                                    <telerik:RadComboBoxItem Value="02" Text="Feb" />
                                                    <telerik:RadComboBoxItem Value="03" Text="Mar" />
                                                    <telerik:RadComboBoxItem Value="04" Text="Apr" />
                                                    <telerik:RadComboBoxItem Value="05" Text="May" />
                                                    <telerik:RadComboBoxItem Value="06" Text="Jun" />
                                                    <telerik:RadComboBoxItem Value="07" Text="Jul" />
                                                    <telerik:RadComboBoxItem Value="08" Text="Aug" />
                                                    <telerik:RadComboBoxItem Value="09" Text="Sep" />
                                                    <telerik:RadComboBoxItem Value="10" Text="Oct" />
                                                    <telerik:RadComboBoxItem Value="11" Text="Nov" />
                                                    <telerik:RadComboBoxItem Value="12" Text="Dec" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                                
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="btnReset" ImageUrl="~/Images/Toolbar/new16_h.png" Enabled="true" OnClick="btnReset_Click" ToolTip="Reset" />
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="btnCopy" ImageUrl="~/Images/copy16.png" Enabled="true" OnClick="btnCopy_Click" ToolTip="Copy" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Coa Prefix: &nbsp;
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtCoaPrefix"></telerik:RadTextBox>
                            </td>
                            <td>
                                Multiply By: &nbsp;
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtMultiplyBy" runat="server" NumberFormat-DecimalDigits="2" Width="100px" 
                                    Value="1" MinValue="0.01"></telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="btnDownload" ImageUrl="~/Images/Toolbar/download16.png" Enabled="true" OnClick="btnDownload_Click" ToolTip="Export to Excel" />
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="btnUpload" ImageUrl="~/Images/Toolbar/upload_ascii16.png" Enabled="true" OnClientClick="OpenImportDialog(); return false;" ToolTip="Import from Excel" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                
                <table id="tblStatus" runat="server" cellpadding="0" cellspacing="0" 
                    class="info success" style="font-size:medium; width:100%;">
                    <tr>
                        <td style="width:150px">Normal Balance</td>
                        <td>:</td>
                        <td>Debit</td>
                        <td>Credit</td>
                    </tr>
                    <tr>
                        <td style="width:150px">Row Count</td>
                        <td>:</td>
                        <td><asp:Label ID="lblCountDebit" runat="server"></asp:Label></td>
                        <td><asp:Label ID="lblCountCredit" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Budget Amount</td>
                        <td>:</td>
                        <td><asp:Label ID="lblSumBudgetAmountDebit" runat="server"></asp:Label></td>
                        <td><asp:Label ID="lblSumBudgetAmountCredit" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width:150px">Status</td>
                        <td>:</td>
                        <td colspan="2"><asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                    </tr>
                </table>
                    
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItemTransactionItem" runat="server" OnNeedDataSource="grdItemTransactionItem_NeedDataSource"
        OnItemDataBound="grdItemTransactionItem_ItemDataBound"
        AutoGenerateColumns="False" GridLines="None" >
        <MasterTableView CommandItemDisplay="None" DataKeyNames="ChartOfAccountID" PageSize="10">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ChartOfAccountCode" HeaderText="COA Code"
                    UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ChartOfAccountName" HeaderText="COA Name" UniqueName="ChartOfAccountName"
                    SortExpression="ChartOfAccountName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridHyperLinkColumn DataTextField="ChartOfAccountName" HeaderText="COA Name" UniqueName="ChartOfAccountNameL"
                    DataNavigateUrlFields="ChartOfAccountID, Mode"
                    DataNavigateUrlFormatString="javascript:OpenDetail('{0}','{1}');"
                    >
                </telerik:GridHyperLinkColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemCount" HeaderText="Item Count"
                    UniqueName="ItemCount" SortExpression="ItemCount" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn HeaderStyle-Width="78px" HeaderText="Jan"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="Jan"
                    FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtLimit01" runat="server" Width="65px" DbValue='<%#Eval("Limit01")%>'>
                            <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                            <NumberFormat DecimalDigits="0" />
                            <IncrementSettings InterceptArrowKeys="false" InterceptMouseWheel="false" />
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="78px" HeaderText="Feb"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="Feb"
                    FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtLimit02" runat="server" Width="65px" DbValue='<%#Eval("Limit02")%>'>
                            <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                            <NumberFormat DecimalDigits="0" />
                            <IncrementSettings InterceptArrowKeys="false" InterceptMouseWheel="false" />
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="78px" HeaderText="Mar"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="Mar"
                    FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtLimit03" runat="server" Width="65px" DbValue='<%#Eval("Limit03")%>'>
                            <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                            <NumberFormat DecimalDigits="0" />
                            <IncrementSettings InterceptArrowKeys="false" InterceptMouseWheel="false" />
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="78px" HeaderText="Apr"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="Apr"
                    FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtLimit04" runat="server" Width="65px" DbValue='<%#Eval("Limit04")%>'>
                            <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                            <NumberFormat DecimalDigits="0" />
                            <IncrementSettings InterceptArrowKeys="false" InterceptMouseWheel="false" />
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="78px" HeaderText="May"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="May"
                    FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtLimit05" runat="server" Width="65px" DbValue='<%#Eval("Limit05")%>'>
                            <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                            <NumberFormat DecimalDigits="0" />
                            <IncrementSettings InterceptArrowKeys="false" InterceptMouseWheel="false" />
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="78px" HeaderText="Jun"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="Jun"
                    FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtLimit06" runat="server" Width="65px" DbValue='<%#Eval("Limit06")%>'>
                            <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                            <NumberFormat DecimalDigits="0" />
                            <IncrementSettings InterceptArrowKeys="false" InterceptMouseWheel="false" />
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="78px" HeaderText="Jul"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="Jul"
                    FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtLimit07" runat="server" Width="65px" DbValue='<%#Eval("Limit07")%>'>
                            <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                            <NumberFormat DecimalDigits="0" />
                            <IncrementSettings InterceptArrowKeys="false" InterceptMouseWheel="false" />
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="78px" HeaderText="Aug"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="Aug"
                    FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtLimit08" runat="server" Width="65px" DbValue='<%#Eval("Limit08")%>'>
                            <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                            <NumberFormat DecimalDigits="0" />
                            <IncrementSettings InterceptArrowKeys="false" InterceptMouseWheel="false" />
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="78px" HeaderText="Sep"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="Sep"
                    FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtLimit09" runat="server" Width="65px" DbValue='<%#Eval("Limit09")%>'>
                            <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                            <NumberFormat DecimalDigits="0" />
                            <IncrementSettings InterceptArrowKeys="false" InterceptMouseWheel="false" />
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="78px" HeaderText="Oct"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="Oct"
                    FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtLimit10" runat="server" Width="65px" DbValue='<%#Eval("Limit10")%>'>
                            <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                            <NumberFormat DecimalDigits="0" />
                            <IncrementSettings InterceptArrowKeys="false" InterceptMouseWheel="false" />
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="78px" HeaderText="Nov"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="Nov"
                    FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtLimit11" runat="server" Width="65px" DbValue='<%#Eval("Limit11")%>'>
                            <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                            <NumberFormat DecimalDigits="0" />
                            <IncrementSettings InterceptArrowKeys="false" InterceptMouseWheel="false" />
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="78px" HeaderText="Dec"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="Dec"
                    FooterStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtLimit12" runat="server" Width="65px" DbValue='<%#Eval("Limit12")%>'>
                            <ClientEvents OnBlur="Blur" OnFocus="Focus" />
                            <NumberFormat DecimalDigits="0" />
                            <IncrementSettings InterceptArrowKeys="false" InterceptMouseWheel="false" />
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
