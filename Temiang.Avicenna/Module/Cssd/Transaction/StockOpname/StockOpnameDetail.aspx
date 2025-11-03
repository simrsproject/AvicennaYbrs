<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="StockOpnameDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.StockOpnameDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            var i = 0;

            function openWinStockOpnameAdd() {
                i++;
                var oWnd = $find("<%= winStockOpnameAdd.ClientID %>");
                oWnd.setUrl("StockOpnameAdd.aspx?wid=" + i);
                oWnd.show();
            }

            function onClientClose(oWnd) {
                if (oWnd.argument && oWnd.argument.command == 'ok') {
                    var url = '<%= HttpContext.Current.Request.Url.AbsolutePath %>?md=view&id=' + oWnd.argument.trno;
                    window.location = url;
                }
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }
            function Close() {
                GetRadWindow().close();
            }

            function onWinBarcode_ClientClose(oWnd) {
                if (oWnd.argument && oWnd.argument.command == 'ok')
                    __doPostBack("<%= grdItem.UniqueID %>", "new");
                        }

            function AddItem(type) {
                var oWnd = window.$find("<%= winBarcode.ClientID %>");
                var pageNo = $find("<%= cboPageNo.ClientID %>");
               var url = 'AddItem.aspx?trno=<%= txtTransactionNo.Text %>&suid=<%= txtServiceUnitID.Text %>&gcid=<%= grdItem.ClientID %>' + '&pageNo=' + pageNo.get_value() + '&type=' + type;
                oWnd.setUrl(url);
                oWnd.show();
            }

            function RebindGridItem(arg) {
                __doPostBack("<%= grdItem.UniqueID %>", arg);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="450px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" OnClientClose="onClientClose" ID="winStockOpnameAdd">
    </telerik:RadWindow>
        <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="500px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" OnClientClose="onWinBarcode_ClientClose" ID="winBarcode">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="fw_tbarData">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fw_tbarData" />
                    <telerik:AjaxUpdatedControl ControlID="fw_hdnDataMode" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelStatus" />
                    <telerik:AjaxUpdatedControl ControlID="fw_radNotif" />
                    <telerik:AjaxUpdatedControl ControlID="cboPageNo" />
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                    <telerik:AjaxUpdatedControl ControlID="cboPageNo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_tbarData" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelStatus" />
                    <telerik:AjaxUpdatedControl ControlID="fw_hdnDataMode" />
                    <telerik:AjaxUpdatedControl ControlID="fw_radNotif" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboPageNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                    <telerik:AjaxUpdatedControl ControlID="fw_tbarData" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelStatus" />
                    <telerik:AjaxUpdatedControl ControlID="cboPageNo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_hdnDataMode" />
                    <telerik:AjaxUpdatedControl ControlID="fw_radNotif" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="True" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" ReadOnly="True" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="True" />
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Visible="false" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPage" runat="server" Text="Page"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboPageNo" Width="100px" AutoPostBack="true"
                                NoWrap="true" OnSelectedIndexChanged="cboPageNo_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" AllowPaging="true" AllowSorting="false"
        PageSize="100">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="TransactionNo, SequenceNo">
            <CommandItemTemplate>
                <%#DataModeCurrent==AppEnum.DataMode.Read && (OnGetStatusMenuApproval() ?? false) && (IsEnabledAddNewItemCSSD() ?? false)?
                       (string.Format("<a href=\"#\" title=\"Existing\" onclick=\"AddItem('edit'); return false;\"><img src=\"{0}/Images/Toolbar/insert16.png\"/>&nbsp;Add New Item</a>", 
                           Helper.UrlRoot())) : string.Empty%>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="System" Name="System" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Actual" Name="Actual" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <Columns>
                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" SortExpression="SequenceNo"
                    Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="PrevBalance" HeaderText="Qty"
                    UniqueName="PrevBalance" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" ColumnGroupName="System" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="PrevBalanceReceived" HeaderText="Received"
                    UniqueName="PrevBalanceReceived" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" ColumnGroupName="System" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="PrevBalanceDeconImmersion" HeaderText="D. Immersion"
                    UniqueName="PrevBalanceDeconImmersion" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" ColumnGroupName="System" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="PrevBalanceDeconAbstersion" HeaderText="D. Abstersion"
                    UniqueName="PrevBalanceDeconAbstersion" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" ColumnGroupName="System" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="PrevBalanceDeconDrying" HeaderText="D. Drying"
                    UniqueName="PrevBalanceDeconDrying" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" ColumnGroupName="System" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="PrevBalanceFeasibilityTest" HeaderText="Feasibility Test"
                    UniqueName="PrevBalanceFeasibilityTest" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" ColumnGroupName="System" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="PrevBalancePackaging" HeaderText="Packaging"
                    UniqueName="PrevBalancePackaging" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" ColumnGroupName="System" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="PrevBalanceUltrasound" HeaderText="Ultra- sound"
                    UniqueName="PrevBalanceUltrasound" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" ColumnGroupName="System" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="PrevBalanceSterilization" HeaderText="Steri- lization"
                    UniqueName="PrevBalanceSterilization" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" ColumnGroupName="System" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="PrevBalanceDistribution" HeaderText="Distri- bution"
                    UniqueName="PrevBalanceDistribution" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" ColumnGroupName="System" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="PrevBalanceReturned" HeaderText="Returned"
                    UniqueName="PrevBalanceReturned" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" ColumnGroupName="System" />

                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Qty" UniqueName="Balance"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ColumnGroupName="Actual">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtBalance" runat="server" Width="70px" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) && !Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCssdUnit")) %>'
                            Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Balance")) %>'
                            MinValue="0" IncrementSettings-InterceptMouseWheel="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Received" UniqueName="BalanceReceived"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ColumnGroupName="Actual">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtBalanceReceived" runat="server" Width="70px" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCssdUnit")) %>'
                            Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "BalanceReceived")) %>'
                            MinValue="0" IncrementSettings-InterceptMouseWheel="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="D.Immersion" UniqueName="BalanceDeconImmersion"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ColumnGroupName="Actual">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtBalanceDeconImmersion" runat="server" Width="70px" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCssdUnit")) %>'
                            Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "BalanceDeconImmersion")) %>'
                            MinValue="0" IncrementSettings-InterceptMouseWheel="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="D.Abstersion" UniqueName="BalanceDeconAbstersion"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ColumnGroupName="Actual">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtBalanceDeconAbstersion" runat="server" Width="70px" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCssdUnit")) %>'
                            Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "BalanceDeconAbstersion")) %>'
                            MinValue="0" IncrementSettings-InterceptMouseWheel="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="D.Drying" UniqueName="BalanceDeconDrying"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ColumnGroupName="Actual">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtBalanceDeconDrying" runat="server" Width="70px" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCssdUnit")) %>'
                            Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "BalanceDeconDrying")) %>'
                            MinValue="0" IncrementSettings-InterceptMouseWheel="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Feasibility Test" UniqueName="BalanceFeasibilityTest"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ColumnGroupName="Actual">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtBalanceFeasibilityTest" runat="server" Width="70px" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCssdUnit")) %>'
                            Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "BalanceFeasibilityTest")) %>'
                            MinValue="0" IncrementSettings-InterceptMouseWheel="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Packaging" UniqueName="BalancePackaging"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ColumnGroupName="Actual">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtBalancePackaging" runat="server" Width="70px" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCssdUnit")) %>'
                            Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "BalancePackaging")) %>'
                            MinValue="0" IncrementSettings-InterceptMouseWheel="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Ultrasound" UniqueName="BalanceUltrasound"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ColumnGroupName="Actual">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtBalanceUltrasound" runat="server" Width="70px" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCssdUnit")) %>'
                            Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "BalanceUltrasound")) %>'
                            MinValue="0" IncrementSettings-InterceptMouseWheel="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Sterilization" UniqueName="BalanceSterilization"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ColumnGroupName="Actual">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtBalanceSterilization" runat="server" Width="70px" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCssdUnit")) %>'
                            Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "BalanceSterilization")) %>'
                            MinValue="0" IncrementSettings-InterceptMouseWheel="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Distribution" UniqueName="BalanceDistribution"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ColumnGroupName="Actual">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtBalanceDistribution" runat="server" Width="70px" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCssdUnit")) %>'
                            Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "BalanceDistribution")) %>'
                            MinValue="0" IncrementSettings-InterceptMouseWheel="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Returned" UniqueName="BalanceReturned"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ColumnGroupName="Actual">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtBalanceReturned" runat="server" Width="70px" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCssdUnit")) %>'
                            Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "BalanceReturned")) %>'
                            MinValue="0" IncrementSettings-InterceptMouseWheel="false" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Item Unit"
                    UniqueName="SRItemUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridTemplateColumn HeaderStyle-Width="200px" HeaderText="Notes" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" UniqueName="Notes">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="190px" Text='<%#Eval("Notes")%>' MaxLength="200" Enabled='<%# (DataModeCurrent == AppEnum.DataMode.Edit) %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
    </telerik:RadGrid>
</asp:Content>
