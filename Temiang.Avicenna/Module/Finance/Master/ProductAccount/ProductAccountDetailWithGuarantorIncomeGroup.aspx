<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ProductAccountDetailWithGuarantorIncomeGroup.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ProductAccountDetailWithGuarantorIncomeGroup" %>

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
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsPpnOpr" runat="server" Text="IsPpnOpr" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsPpnEmr" runat="server" Text="IsPpnEmr" />
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
    
    <telerik:RadGrid ID="gridMapping" AllowPaging="true" PageSize="20" runat="server"
        OnNeedDataSource="gridMapping_NeedDataSource" AutoGenerateColumns="False"
        OnDataBound="gridMapping_DataBound"
        OnItemDataBound="gridMapping_ItemDataBound"
        GridLines="None">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
            <Columns>
                <telerik:GridTemplateColumn DataField="ItemID">
                    <ItemTemplate>
                        <label><%# DataBinder.Eval(Container.DataItem, "ItemName")%></label>
                        <table id="tblMapping" runat="server">
                            <tr>
                                <td colspan="2">Outpatient</td>
                                <td></td>
                                <td colspan="2">Inpatient</td>
                                <td></td>
                                <td colspan="2">Emergency</td>
                            </tr>
                            
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdIncome" runat="server" Text="COA Revenue" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOAIncome" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdIncomeIPR" runat="server" Text="COA Revenue" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOAIncomeIPR" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdIncomeIGD" runat="server" Text="COA Revenue" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOAIncomeIGD" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdIncome" runat="server" Text="Subledger Revenue"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlIncome" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdIncomeIPR" runat="server" Text="Subledger Revenue"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlIncomeIPR" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdIncomeIGD" runat="server" Text="Subledger Revenue"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlIncomeIGD" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                            </tr>
                            
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdAccrual" runat="server" Text="COA Accrual" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOAAcrual" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdAccrualIPR" runat="server" Text="COA Accrual" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOAAcrualIPR" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdAccrualIGD" runat="server" Text="COA Accrual" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOAAcrualIGD" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdAccrual" runat="server" Text="Subledger Accrual"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlAcrual" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdAccrualIPR" runat="server" Text="Subledger Accrual"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlAcrualIPR" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdAccrualIGD" runat="server" Text="Subledger Accrual"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlAcrualIGD" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                            </tr>
                            
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdDiscount" runat="server" Text="COA Discount" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOADiscount" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdDiscountIPR" runat="server" Text="COA Discount" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOADiscountIPR" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdDiscountIGD" runat="server" Text="COA Discount" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOADiscountIGD" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdDiscount" runat="server" Text="Subledger Discount"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlDiscount" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdDiscountIPR" runat="server" Text="Subledger Discount"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlDiscountIPR" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdDiscountIGD" runat="server" Text="Subledger Discount"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlDiscountIGD" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                            </tr>
                            
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdInventory" runat="server" Text="COA Inventory / Asset" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOAInventory" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdInventoryIPR" runat="server" Text="COA Inventory / Asset" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOAInventoryIPR" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdInventoryIGD" runat="server" Text="COA Inventory / Asset" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOAInventoryIGD" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdInventory" runat="server" Text="Subledger Inventory / Asset"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlInventory" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdInventoryIPR" runat="server" Text="Subledger Inventory / Asset"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlInventoryIPR" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdInventoryIGD" runat="server" Text="Subledger Inventory / Asset"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlInventoryIGD" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                            </tr>
                            
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdCOGS" runat="server" Text="COA COGS" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOACOGS" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdCOGSIPR" runat="server" Text="COA COGS" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOACOGSIPR" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdCOGSIGD" runat="server" Text="COA COGS" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOACOGSIGD" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdCOGS" runat="server" Text="Subledger COGS"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlCOGS" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdCOGSIPR" runat="server" Text="Subledger COGS"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlCOGSIPR" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdCOGSIGD" runat="server" Text="Subledger COGS"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlCOGSIGD" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                            </tr>
                            
                            <tr id="trChartOfAccountIdCOGSTemp" runat="server">
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdCOGSTemp" runat="server" Text="COA COGS Temporary" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOACOGSOPRTemp" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdCOGSTempIPR" runat="server" Text="COA COGS Temporary" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOACOGSIPRTemp" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblChartOfAccountIdCOGSTempIGD" runat="server" Text="COA COGS Temporary" Width="125px"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboCOACOGSIGDTemp" Height="190px"
                                        Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboCOA_SelectedIndexChanged" 
                                        OnItemDataBound="cboCOA_ItemDataBound"
                                        OnItemsRequested="cboCOA_ItemsRequested"  >
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
                            </tr>
                            <tr id="trSubledgerIdCOGSTemp" runat="server">
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdCOGSTemp" runat="server" Text="Subledger COGS Temporary"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlCOGSOPRTemp" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdCOGSTempIPR" runat="server" Text="Subledger COGS Temporary"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlCOGSIPRTemp" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                                <td></td>
                                <td class="label">
                                    <asp:Label ID="lblSubledgerIdCOGSTempIGD" runat="server" Text="Subledger COGS Temporary"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSlCOGSIGDTemp" Height="190px" Width="100%"
                                        EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                        OnItemDataBound="cboSl_ItemDataBound" 
                                        OnItemsRequested="cboSl_ItemsRequested">
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
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="False">
            <Resizing AllowColumnResize="False" />
            <Selecting AllowRowSelect="False" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
