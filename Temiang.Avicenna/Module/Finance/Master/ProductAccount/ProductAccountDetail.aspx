<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ProductAccountDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ProductAccountDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProductAccountID" runat="server" Text="Product Account ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtProductAccountID" runat="server" Width="300px" MaxLength="10" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvProductAccountID" runat="server" ErrorMessage="Product Account ID required."
                                ValidationGroup="entry" ControlToValidate="txtProductAccountID" SetFocusOnError="True"
                                Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProductAccountName" runat="server" Text="Product Account Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtProductAccountName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvProductAccountName" runat="server" ErrorMessage="Product Account Name required."
                                ValidationGroup="entry" ControlToValidate="txtProductAccountName" SetFocusOnError="True"
                                Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="vertical-align: top; width: 30%">
                <table width="100%">
                    <tr>
                        <td colspan="2" style="background-color: Gray; height: 20px">
                            <asp:Label ID="LblOutPatient" runat="server" Text=" Outpatient" Font-Bold="true"
                                ForeColor="Yellow"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdIncome" runat="server" Text="COA Revenue" Width="125px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdIncome" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdIncome_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdIncome_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdIncome_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdIncome" runat="server" Text="Subledger Revenue"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdIncome" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdIncome_ItemDataBound" OnItemsRequested="cboSubledgerIdIncome_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdAcrual" runat="server" Text="COA Accrual"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdAcrual" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdAcrual_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdAcrual_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdAcrual_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdAcrual" runat="server" Text="Subledger Accrual"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdAcrual" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdAcrual_ItemDataBound" OnItemsRequested="cboSubledgerIdAcrual_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdDiscount" runat="server" Text="COA Discount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdDiscount" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdDiscount_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdDiscount_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdDiscount_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdDiscount" runat="server" Text="Subledger Discount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdDiscount" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdDiscount_ItemDataBound" OnItemsRequested="cboSubledgerIdDiscount_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdInventory" runat="server" Text="COA Inventory"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdInventory" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdInventory_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdInventory_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdInventory_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdInventory" runat="server" Text="Subledger Inventory"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdInventory" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdInventory_ItemDataBound" OnItemsRequested="cboSubledgerIdInventory_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdCOGS" runat="server" Text="COA COGS"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCOGS" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdCOGS_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdCOGS_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdCOGS_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdCOGS" runat="server" Text="Subledger COGS"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCOGS" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdCOGS_ItemDataBound" OnItemsRequested="cboSubledgerIdCOGS_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblChartOfAccountIdSalesReturn" runat="server" Text="COA Sales Return"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdSalesReturn" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                Visible="false" OnSelectedIndexChanged="cboChartOfAccountIdSalesReturn_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdSalesReturn_ItemDataBound" OnItemsRequested="cboChartOfAccountIdSalesReturn_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSubledgerIdSalesReturn" runat="server" Text="Subledger Sales Return"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdSalesReturn" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                Visible="false" OnItemDataBound="cboSubledgerIdSalesReturn_ItemDataBound" OnItemsRequested="cboSubledgerIdSalesReturn_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblChartOfAccountIdPurchaseReturn" runat="server" Text="COA Purchase Return"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdPurchaseReturn" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                Visible="false" OnSelectedIndexChanged="cboChartOfAccountIdPurchaseReturn_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdPurchaseReturn_ItemDataBound" OnItemsRequested="cboChartOfAccountIdPurchaseReturn_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSubledgerIdPurchaseReturn" runat="server" Text="Subledger Purchase Return"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdPurchaseReturn" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                Visible="false" OnItemDataBound="cboSubledgerIdPurchaseReturn_ItemDataBound"
                                OnItemsRequested="cboSubledgerIdPurchaseReturn_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdCost" runat="server" Text="COA Cost"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCost" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdCost_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdCost_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdCost_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdCost" runat="server" Text="Subledger Cost"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCost" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdCost_ItemDataBound" OnItemsRequested="cboSubledgerIdCost_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trChartOfAccountIdCOGSOPTemp">
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="COA COGS Temporary"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCOGSOPTemp" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdCOGSOPTemp_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdCOGS_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdCOGS_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trSubledgerIdCOGSOPTemp">
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Subledger COGS Temporary"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCOGSOPTemp" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdCOGS_ItemDataBound" OnItemsRequested="cboSubledgerIdCOGS_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <!-- IN PATIENT -->
            <td style="vertical-align: top; width: 30%">
                <table width="100%">
                    <tr>
                        <td colspan="2" style="background-color: Gray; height: 20px">
                            <asp:Label ID="LblInPatient" runat="server" Text=" Inpatient" Font-Bold="true" ForeColor="Yellow"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdIncomeIP" runat="server" Text="COA Revenue" Width="125px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdIncomeIP" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdIncomeIP_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdIncomeIP_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdIncomeIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdIncomeIP" runat="server" Text="Subledger Revenue"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdIncomeIP" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdIncomeIP_ItemDataBound" OnItemsRequested="cboSubledgerIdIncomeIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdAcrualIP" runat="server" Text="COA Acrrual"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdAcrualIP" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdAcrualIP_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdAcrualIP_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdAcrualIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdAcrualIP" runat="server" Text="Subledger Accrual"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdAcrualIP" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdAcrualIP_ItemDataBound" OnItemsRequested="cboSubledgerIdAcrualIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdDiscountIP" runat="server" Text="COA Discount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdDiscountIP" Height="100%"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdDiscountIP_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdDiscountIP_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdDiscountIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdDiscountIP" runat="server" Text="Subledger Discount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdDiscountIP" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdDiscountIP_ItemDataBound" OnItemsRequested="cboSubledgerIdDiscountIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdInventoryIP" runat="server" Text="COA Inventory"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdInventoryIP" Height="100%"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdInventoryIP_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdInventoryIP_ItemDataBound" OnItemsRequested="cboChartOfAccountIdInventoryIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdInventoryIP" runat="server" Text="Subledger Inventory"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdInventoryIP" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdInventoryIP_ItemDataBound" OnItemsRequested="cboSubledgerIdInventoryIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdCOGSIP" runat="server" Text="COA COGS"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCOGSIP" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdCOGSIP_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdCOGSIP_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdCOGSIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdCOGSIP" runat="server" Text="Subledger COGS"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCOGSIP" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdCOGSIP_ItemDataBound" OnItemsRequested="cboSubledgerIdCOGSIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblChartOfAccountIdSalesReturnIP" runat="server" Text="COA Sales Return"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdSalesReturnIP" Height="100%"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                Visible="false" OnSelectedIndexChanged="cboChartOfAccountIdSalesReturnIP_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdSalesReturnIP_ItemDataBound" OnItemsRequested="cboChartOfAccountIdSalesReturnIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSubledgerIdSalesReturnIP" runat="server" Text="Subledger Sales Return"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdSalesReturnIP" Height="100%"
                                Visible="false" Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                AutoPostBack="false" OnItemDataBound="cboSubledgerIdSalesReturnIP_ItemDataBound"
                                OnItemsRequested="cboSubledgerIdSalesReturnIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblChartOfAccountIdPurchaseReturnIP" runat="server" Text="COA Purchase Return"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdPurchaseReturnIP" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                Visible="false" OnSelectedIndexChanged="cboChartOfAccountIdPurchaseReturnIP_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdPurchaseReturnIP_ItemDataBound" OnItemsRequested="cboChartOfAccountIdPurchaseReturnIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSubledgerIdPurchaseReturnIP" runat="server" Text="Subledger Purchase Return"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdPurchaseReturnIP" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                Visible="false" OnItemDataBound="cboSubledgerIdPurchaseReturnIP_ItemDataBound"
                                OnItemsRequested="cboSubledgerIdPurchaseReturnIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdCostIP" runat="server" Text="COA Cost"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCostIP" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdCostIP_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdCostIP_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdCostIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdCostIP" runat="server" Text="Subledger Cost"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCostIP" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdCostIP_ItemDataBound" OnItemsRequested="cboSubledgerIdCostIP_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trChartOfAccountIdCOGSIPTemp">
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="COA COGS Temporary"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCOGSIPTemp" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdCOGSIPTemp_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdCOGS_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdCOGS_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trSubledgerIdCOGSIPTemp">
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Subledger COGS Temporary"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCOGSIPTemp" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdCOGS_ItemDataBound" OnItemsRequested="cboSubledgerIdCOGS_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <!-- EMERGENCY -->
            <td style="vertical-align: top; width: 30%">
                <table width="100%">
                    <tr>
                        <td colspan="2" style="background-color: Gray; height: 20px">
                            <asp:Label ID="LblIGD" runat="server" Text=" Emergency" Font-Bold="true" ForeColor="Yellow"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdIncomeIGD" runat="server" Text="COA Revenue"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdIncomeIGD" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdIncomeIGD_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdIncomeIGD_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdIncomeIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdIncomeIGD" runat="server" Text="Subledger Revenue"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdIncomeIGD" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdIncomeIGD_ItemDataBound" OnItemsRequested="cboSubledgerIdIncomeIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdAcrualIGD" runat="server" Text="COA Accrual"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdAcrualIGD" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdAcrualIGD_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdAcrualIGD_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdAcrualIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdAcrualIGD" runat="server" Text="Subledger Accrual"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdAcrualIGD" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdAcrualIGD_ItemDataBound" OnItemsRequested="cboSubledgerIdAcrualIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdDiscountIGD" runat="server" Text="COA Discount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdDiscountIGD" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdDiscountIGD_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdDiscountIGD_ItemDataBound" OnItemsRequested="cboChartOfAccountIdDiscountIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdDiscountIGD" runat="server" Text="Subledger Discount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdDiscountIGD" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdDiscountIGD_ItemDataBound" OnItemsRequested="cboSubledgerIdDiscountIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdInventoryIGD" runat="server" Text="COA Inventory"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdInventoryIGD" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdInventoryIGD_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdInventoryIGD_ItemDataBound" OnItemsRequested="cboChartOfAccountIdInventoryIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdInventoryIGD" runat="server" Text="Subledger Inventory"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdInventoryIGD" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdInventoryIGD_ItemDataBound" OnItemsRequested="cboSubledgerIdInventoryIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdCOGSIGD" runat="server" Text="COA COGS"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCOGSIGD" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdCOGSIGD_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdCOGSIGD_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdCOGSIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdCOGSIGD" runat="server" Text="Subledger COGS"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCOGSIGD" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdCOGSIGD_ItemDataBound" OnItemsRequested="cboSubledgerIdCOGSIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblChartOfAccountIdSalesReturnIGD" runat="server" Text="COA Sales Return"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdSalesReturnIGD" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                Visible="false" OnSelectedIndexChanged="cboChartOfAccountIdSalesReturnIGD_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdSalesReturnIGD_ItemDataBound" OnItemsRequested="cboChartOfAccountIdSalesReturnIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSubledgerIdSalesReturnIGD" runat="server" Text="Subledger Sales Return"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdSalesReturnIGD" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                Visible="false" OnItemDataBound="cboSubledgerIdSalesReturnIGD_ItemDataBound"
                                OnItemsRequested="cboSubledgerIdSalesReturnIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblChartOfAccountIdPurchaseReturnIGD" runat="server" Text="COA Purchase Return"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdPurchaseReturnIGD" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                Visible="false" OnSelectedIndexChanged="cboChartOfAccountIdPurchaseReturnIGD_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdPurchaseReturnIGD_ItemDataBound" OnItemsRequested="cboChartOfAccountIdPurchaseReturnIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSubledgerIdPurchaseReturnIGD" runat="server" Text="Subledger Purchase Return"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdPurchaseReturnIGD" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                Visible="false" OnItemDataBound="cboSubledgerIdPurchaseReturnIGD_ItemDataBound"
                                OnItemsRequested="cboSubledgerIdPurchaseReturnIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdCostIGD" runat="server" Text="COA Cost"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCostIGD" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdCostIGD_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdCostIGD_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdCostIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdCostIGD" runat="server" Text="Subledger Cost"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCostIGD" Height="190px" Width="100%"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdCostIGD_ItemDataBound" OnItemsRequested="cboSubledgerIdCostIGD_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trChartOfAccountIdCOGSIGDTemp">
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="COA COGS Temporary"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCOGSIGDTemp" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdCOGSIGDTemp_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdCOGS_ItemDataBound" OnItemsRequested="cboChartOfAccountIdCOGS_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trSubledgerIdCOGSIGDTemp">
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="Subledger COGS Temporary"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCOGSIGDTemp" Height="190px"
                                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdCOGS_ItemDataBound" OnItemsRequested="cboSubledgerIdCOGS_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
