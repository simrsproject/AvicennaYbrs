<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="LocationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.LocationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdnPageId" />
    <table width="100%">
        <tr>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblParentID" runat="server" Text="Parent ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParentID" runat="server" Width="100px" MaxLength="10"
                                AutoPostBack="True" OnTextChanged="txtParentID_TextChanged" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLocationID" runat="server" Text="Location ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtLocationID" runat="server" Width="100px" MaxLength="10" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvLocationID" runat="server" ErrorMessage="Location ID required."
                                ValidationGroup="entry" ControlToValidate="txtLocationID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLocationName" runat="server" Text="Location Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtLocationName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvLocationName" runat="server" ErrorMessage="Location Name required."
                                ValidationGroup="entry" ControlToValidate="txtLocationName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblShortName" runat="server" Text="Short Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtShortName" runat="server" Width="100px" MaxLength="35" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblItemGroupID" runat="server" Text="Item Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemGroupID" runat="server" Width="100px" MaxLength="10"
                                AutoPostBack="True" OnTextChanged="txtItemGroupID_TextChanged" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPermitID" runat="server" Text="Permit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPermitID" runat="server" Width="100px" MaxLength="10"
                                AutoPostBack="True" OnTextChanged="txtPermitID_TextChanged" />
                            &nbsp;
                            <asp:Label ID="lblPermitName" runat="server"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trTypeOfInventory">
                        <td class="label">
                            <asp:Label ID="lblSRTypeOfInventory" runat="server" Text="Inventory Type (Item Medical)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRTypeOfInventory" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRTypeOfInventory" runat="server" ErrorMessage="Type of Inventory required."
                                ValidationGroup="entry" ControlToValidate="cboSRTypeOfInventory" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsHeader" runat="server" Text="Header" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsHoldForTransaction" runat="server" Text="Hold For Transaction" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trIsAllowedToStockGoods">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsAllowedToStockGoods" runat="server" Text="Allowed To Stock Goods" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsConsignment" runat="server" Text="Consignment" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdIncome" runat="server" Text="COA Revenue"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdIncome" Height="190px"
                                Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdIncome" runat="server" Text="Subledger Revenue"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdIncome" Height="190px" Width="300px"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdAcrual" runat="server" Text="COA Acrual"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdAcrual" Height="190px"
                                Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdAcrual" runat="server" Text="Subledger Acrual"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdAcrual" Height="190px" Width="300px"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdDiscount" runat="server" Text="COA Discount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdDiscount" Height="190px"
                                Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdDiscount" runat="server" Text="Subledger Discount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdDiscount" Height="190px" Width="300px"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdInventory" runat="server" Text="COA Inventory"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdInventory" Height="190px"
                                Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdInventory" runat="server" Text="Subledger Inventory"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdInventory" Height="190px" Width="300px"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdCOGS" runat="server" Text="COA COGS"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCOGS" Height="190px" Width="300px"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdCOGS" runat="server" Text="Subledger COGS"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCOGS" Height="190px" Width="300px"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdSalesReturn" runat="server" Text="COA Sales Return"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdSalesReturn" Height="190px"
                                Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdSalesReturn_SelectedIndexChanged"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdSalesReturn" runat="server" Text="Subledger Sales Return"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdSalesReturn" Height="190px"
                                Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdSalesReturn_ItemDataBound" OnItemsRequested="cboSubledgerIdSalesReturn_ItemsRequested">
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdPurchaseReturn" runat="server" Text="COA Purchase Return"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdPurchaseReturn" Height="190px"
                                Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdPurchaseReturn_SelectedIndexChanged"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdPurchaseReturn" runat="server" Text="Subledger Purchase Return"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdPurchaseReturn" Height="190px"
                                Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdPurchaseReturn_ItemDataBound" OnItemsRequested="cboSubledgerIdPurchaseReturn_ItemsRequested">
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdCost" runat="server" Text="COA Cost"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCost" Height="190px" Width="300px"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdCost" runat="server" Text="Subledger Cost"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCost" Height="190px" Width="300px"
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td class="entry">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblItemProductMedic" runat="server" Text="Medical"></asp:Label>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblItemProductNonMedic" runat="server" Text="Non Medical"></asp:Label>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblItemKitchen" runat="server" Text="Kitchen"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblValidate" runat="server" Text="Validated Max Value On Distribution Request"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsValidateMaxValueOnDistReqForIpm" runat="server" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsValidateMaxValueOnDistReqForIpnm" runat="server" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsValidateMaxValueOnDistReqForIk" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblValidateMaxValueOnPurcReq" runat="server" Text="Validated Max Value On Purchase Request"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsValidateMaxValueOnPurcReqForIpm" runat="server" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsValidateMaxValueOnPurcReqForIpnm" runat="server" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsValidateMaxValueOnPurcReqForIk" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Stock Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRStockGroup" Height="190px" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Stock Group required."
                                ValidationGroup="entry" ControlToValidate="cboSRStockGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsAutoUpdateStockMinMax" runat="server" Text="Autoupdate Stock Minimum & Maximum by system" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Item Medical" PageViewID="pgvItemMedic" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Item Non Medical" PageViewID="pgvItemNonMedic">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Item Kitchen" PageViewID="pgvItemKitchen">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Item Bin" PageViewID="pgvItemBin">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Location Exception for Distribution" PageViewID="pgvException">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Location Auto Dist. Confirmed" PageViewID="pgvExceptionDistConfirm">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvItemMedic" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFilterItemMedic" runat="server" Text="Item ID / Item Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFilterItemMedic" runat="server" Width="300px" MaxLength="100" />
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilterItemMedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdItemMedic" runat="server" OnNeedDataSource="grdItemMedic_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemMedic_UpdateCommand"
                OnDeleteCommand="grdItemMedic_DeleteCommand" OnInsertCommand="grdItemMedic_InsertCommand"
                ShowStatusBar="true">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID" AllowPaging="true"
                    PageSize="20">
                    <PagerStyle AlwaysVisible="true" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SRItemBinName" HeaderStyle-Width="250px" HeaderText="Bin"
                            UniqueName="SRItemBinName" SortExpression="SRItemBinName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemSubBin" HeaderStyle-Width="100px" HeaderText="Sub Bin"
                            UniqueName="ItemSubBin" SortExpression="ItemSubBin" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Minimum" HeaderText="Minimum"
                            UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Maximum" HeaderText="Maximum"
                            UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Balance"
                            UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Booking" HeaderText="Booking"
                            UniqueName="Booking" SortExpression="Booking" HeaderStyle-HorizontalAlign="Center"
                            Visible="false" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnitName" HeaderText="Unit"
                            UniqueName="SRItemUnitName" SortExpression="SRItemUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemMedicBalanceDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemMedicBalanceEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvItemNonMedic" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFilterItemNonMedic" runat="server" Text="Item ID / Item Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFilterItemNonMedic" runat="server" Width="300px" MaxLength="100" />
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilterItemNonMedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdItemNonMedic" runat="server" OnNeedDataSource="grdItemNonMedic_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemNonMedic_UpdateCommand"
                OnDeleteCommand="grdItemNonMedic_DeleteCommand" OnInsertCommand="grdItemNonMedic_InsertCommand"
                ShowStatusBar="true">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID" AllowPaging="true"
                    PageSize="20">
                    <PagerStyle AlwaysVisible="true" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SRItemBinName" HeaderStyle-Width="250px" HeaderText="Bin"
                            UniqueName="SRItemBinName" SortExpression="SRItemBinName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemSubBin" HeaderStyle-Width="100px" HeaderText="Sub Bin"
                            UniqueName="ItemSubBin" SortExpression="ItemSubBin" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Minimum" HeaderText="Minimum"
                            UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Maximum" HeaderText="Maximum"
                            UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Balance"
                            UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Booking" HeaderText="Booking"
                            UniqueName="Booking" SortExpression="Booking" HeaderStyle-HorizontalAlign="Center"
                            Visible="false" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnitName" HeaderText="Unit"
                            UniqueName="SRItemUnitName" SortExpression="SRItemUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemNonMedicBalanceDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemNonMedicBalanceEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvItemKitchen" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFilterItemKitchen" runat="server" Text="Item ID / Item Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFilterItemKitchen" runat="server" Width="300px" MaxLength="100" />
                                </td>
                                <td width="20">
                                    <asp:ImageButton ID="btnFilterItemKitchen" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdItemKitchen" runat="server" OnNeedDataSource="grdItemKitchen_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemKitchen_UpdateCommand"
                OnDeleteCommand="grdItemKitchen_DeleteCommand" OnInsertCommand="grdItemKitchen_InsertCommand"
                ShowStatusBar="true">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID" AllowPaging="true"
                    PageSize="20">
                    <PagerStyle AlwaysVisible="true" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SRItemBinName" HeaderStyle-Width="250px" HeaderText="Bin"
                            UniqueName="SRItemBinName" SortExpression="SRItemBinName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemSubBin" HeaderStyle-Width="100px" HeaderText="Sub Bin"
                            UniqueName="ItemSubBin" SortExpression="ItemSubBin" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Minimum" HeaderText="Minimum"
                            UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Maximum" HeaderText="Maximum"
                            UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Balance"
                            UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Booking" HeaderText="Booking"
                            UniqueName="Booking" SortExpression="Booking" HeaderStyle-HorizontalAlign="Center"
                            Visible="false" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnitName" HeaderText="Unit"
                            UniqueName="SRItemUnitName" SortExpression="SRItemUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemKitchenBalanceDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemKitchenBalanceEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvItemBin" runat="server">
            <telerik:RadGrid ID="grdItemBin" runat="server" OnNeedDataSource="grdItemBin_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemBin_UpdateCommand"
                OnDeleteCommand="grdItemBin_DeleteCommand" OnInsertCommand="grdItemBin_InsertCommand"
                ShowStatusBar="true">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID" AllowPaging="true"
                    PageSize="20">
                    <PagerStyle AlwaysVisible="true" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemBinDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemBinDetailEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvException" runat="server">
            <telerik:RadGrid ID="grdLocationException" runat="server" OnNeedDataSource="grdLocationException_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdLocationException_DeleteCommand" OnInsertCommand="grdLocationException_InsertCommand"
                ShowStatusBar="true">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="LocationExceptionID" AllowPaging="true"
                    PageSize="20">
                    <PagerStyle AlwaysVisible="true" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LocationExceptionID" HeaderText="ID"
                            UniqueName="LocationExceptionID" SortExpression="LocationExceptionID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="LocationExceptionName" HeaderText="Location Exception" UniqueName="LocationExceptionName"
                            SortExpression="LocationExceptionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="LocationExceptionDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="LocationExceptionDetailEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvExceptionDistConfirm" runat="server">
            <telerik:RadGrid ID="grdLocationExceptionDistConfirm" runat="server" OnNeedDataSource="grdLocationExceptionDistConfirm_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdLocationExceptionDistConfirm_DeleteCommand" OnInsertCommand="grdLocationExceptionDistConfirm_InsertCommand"
                ShowStatusBar="true">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="LocationExceptionID" AllowPaging="true"
                    PageSize="20">
                    <PagerStyle AlwaysVisible="true" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LocationExceptionID" HeaderText="ID"
                            UniqueName="LocationExceptionID" SortExpression="LocationExceptionID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="LocationExceptionName" HeaderText="Location Exception" UniqueName="LocationExceptionName"
                            SortExpression="LocationExceptionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="LocationExceptionDistConfirmDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="LocationExceptionDistConfirmDetailEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
