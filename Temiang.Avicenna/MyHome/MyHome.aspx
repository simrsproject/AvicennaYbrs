<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="MyHome.aspx.cs" Inherits="Temiang.Avicenna.MyHome" Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            var BaseURL = '<%=BaseURL()%>';
            //$(document).ready(function () {
            //    LoadHomePage();
            //});

            function LoadHomePage() {
                // kasih delay biar browser gak stuck
                setTimeout(function () { DoLoadHomePage(); }, 2000);
            }
            function DoLoadHomePage() {
                $.ajax({
                    type: 'POST',
                    url: BaseURL + "/Home/HomePage",
                    data: {},
                    success: function (data) {
                        //console.log(data);
                        if (!data.includes('empty_empty')) { // gini aja dulu deh, nanti diperbaiki
                            $('#divMainHome').html(data);
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        ShowError(xhr.responseText);
                    },
                    dataType: 'html'
                });
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Home" PageViewID="pgvHome" Selected="true" />
            <telerik:RadTab runat="server" Text="Purchase Order Approval Queue" PageViewID="pgvPurchaseOrder" />
            <telerik:RadTab runat="server" Text="Distribution Approval Queue" PageViewID="pgvDistribution" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvHome" runat="server">
            <div id="divMainHome">
                <div class="center-div">
                    <asp:Image runat="server" ID="imgCompany" ImageUrl="~/Images/LogoRS_MainMenu.jpg" />
                </div>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvPurchaseOrder" runat="server">
            <asp:Panel ID="pnlPurchaseOrderInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
                BorderColor="#FFC080" BorderStyle="Solid">
                <table width="100%">
                    <tr>
                        <td width="10px" valign="top">
                            <asp:Image ID="Image13" ImageUrl="~/Images/boundleft.gif" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblPurchaseOrderInfo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                OnDetailTableDataBind="grdList_DetailTableDataBind" OnItemCommand="grdList_ItemCommand"
                AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="TransactionNo,PorLocationID, SRItemType,ServiceUnitID, ApprovalLevel">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="TransactionNo"
                            DataNavigateUrlFields="PoUrl" HeaderText="Order No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ServiceUnitName" HeaderText="Purchase Unit"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="SupplierName" HeaderText="Supplier"
                            UniqueName="SupplierName" SortExpression="SupplierName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemName" HeaderText="Item Type"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="110px" DataTextField="ReferenceNo"
                            DataNavigateUrlFields="PrUrl" HeaderText="Request No" UniqueName="ReferenceNo"
                            SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PurchaseOrderType"
                            HeaderText="Order Type" UniqueName="PurchaseOrderType" SortExpression="PurchaseOrderType"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="ReferenceNo"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="ChargesAmount" HeaderText="Charges Amount"
                            UniqueName="ChargesAmount" SortExpression="ChargesAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ApprovalLevel" HeaderText="Appr. Level"
                            UniqueName="ApprovalLevel" SortExpression="ApprovalLevel" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Approve" HeaderStyle-Width="80px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbApproval" runat="server" CommandName="Approv" ToolTip='Approve'
                                    OnClientClick="javascript: if (!confirm('Approve this transaction, continue ?')) return false;">
                                                            <img src="../Images/Toolbar/post16.png" border="0" alt=""/>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="SequenceNo" Name="grdDetailPr" Width="100%"
                            AutoGenerateColumns="false" ShowFooter="true" AllowPaging="true" PageSize="10">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Description" HeaderText="Item Name" UniqueName="Description"
                                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Minimum" HeaderText="Min Qty"
                                    UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Maximum" HeaderText="Max Qty"
                                    UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="BalanceSG" HeaderText="Balance SG"
                                    UniqueName="BalanceSG" SortExpression="BalanceSG" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Balance" HeaderText="Balance Loc"
                                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="BalanceTotal" HeaderText="Balance Total"
                                    UniqueName="BalanceTotal" SortExpression="BalanceTotal" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRMasterBaseUnit" HeaderText="Unit"
                                    UniqueName="SRMasterBaseUnit" SortExpression="SRMasterBaseUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ConversionFactor"
                                    HeaderText="Conversion" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Quantity" HeaderText="Qty Request"
                                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QtyFinish" HeaderText="Qty Order"
                                    UniqueName="QtyFinish" SortExpression="QtyFinish" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Price" HeaderText="Price"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Discount1Percentage"
                                    HeaderText="Disc #1(%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Discount2Percentage"
                                    HeaderText="Disc #2(%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Discount" HeaderText="Disc Amount"
                                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                                    DataType="System.Double" DataFields="Price,Discount1Percentage,Discount2Percentage, Quantity"
                                    Expression="({0}*{3}) - ((({0}*{1}/100) + (({0} - ({0}*{1}/100)) * {2}/100)) * {3})"
                                    FooterText=" " FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvDistribution" runat="server">
            <telerik:RadGrid ID="grdDistributionOrderAppQue" runat="server" OnNeedDataSource="grdDistributionOrderAppQue_NeedDataSource"
                OnDetailTableDataBind="grdDistributionOrderAppQue_DetailTableDataBind" OnItemCommand="grdDistributionOrderAppQue_ItemCommand"
                AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="TransactionNo,FromLocationID,ToLocationID,SRItemType,ApprovalLevel">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="TransactionNo"
                            DataNavigateUrlFields="DoUrl" HeaderText="Distribution No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="FromServiceUnitName"
                            HeaderText="From Unit" UniqueName="FromServiceUnitName" SortExpression="FromServiceUnitName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ToServiceUnitName"
                            HeaderText="To Unit" UniqueName="ToServiceUnitName" SortExpression="ToServiceUnitName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemTypeName" HeaderText="Item Type"
                            UniqueName="ItemTypeName" SortExpression="ItemTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemGroupName" HeaderText="Item Group"
                            UniqueName="ItemGroupName" SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ReferenceNo" HeaderText="Dist Req No"
                            UniqueName="ReferenceNo" SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ApprovalLevel" HeaderText="Appr. Level"
                            UniqueName="ApprovalLevel" SortExpression="ApprovalLevel" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Approve" HeaderStyle-Width="80px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbApproval" runat="server" CommandName="Approv" ToolTip='Approve'
                                    OnClientClick="javascript: if (!confirm('Approve this transaction, continue ?')) return false;">
                                                            <img src="../Images/Toolbar/post16.png" border="0" alt=""/>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="SequenceNo" Name="grdDetail" AutoGenerateColumns="False"
                            AllowPaging="true" PageSize="10">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ItemName" HeaderText="Item Name"
                                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Quantity" HeaderText="Qty"
                                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="SRItemUnit" HeaderText="Unit"
                                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ConversionFactor"
                                    HeaderText="Conversion" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MinAtReqLoc" HeaderText="Min @Req Loc"
                                    UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="90px" DataField="BalAtReqLoc" HeaderText="Bal @Req Loc"
                                    UniqueName="BalAtReqLoc" SortExpression="BalAtReqLoc" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="90px" DataField="BalAtSourceLoc" HeaderText="Balance"
                                    UniqueName="BalAtSourceLoc" SortExpression="BalAtSourceLoc" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="SRMasterBaseUnit" HeaderText="Bal Unit"
                                    UniqueName="SRMasterBaseUnit" SortExpression="SRMasterBaseUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <asp:Timer ID="tmrAutoRefreshList" runat="server" OnTick="tmrAutoRefreshList_Tick"
        Interval="60000" Enabled="False" />
</asp:Content>
