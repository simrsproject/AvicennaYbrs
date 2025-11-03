<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="RequestOrderAssetApprovalList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Procurement.RequestOrderAssetApprovalList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchRequestDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListPr" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRequestNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListPr" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSearchFromUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListPr" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchToUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListPr" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListPr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListPr" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function onClientTabSelected(sender, eventArgs) {
                var tabIndex = eventArgs.get_tab().get_index();
                switch (tabIndex) {
                    case 0:
                        __doPostBack("<%= grdList.UniqueID %>", "rebind");
                        break;
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="550px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winDialog">
    </telerik:RadWindow>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSearchRequestDate" runat="server" Text="Request Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtRequestDate" runat="server" Width="110px">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>&nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtRequestDate2" runat="server" Width="110px">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchRequestDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRequestNo" runat="server" Text="Request No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtRequestNo" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchRequestNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSearchFromUnit" runat="server" Text="Request Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSearchFromUnit" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchSearchFromUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="LblRequestTo" runat="server" Text="Purchasing Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSearchToUnit" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchToUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop" OnClientTabSelected="onClientTabSelected">
        <Tabs>
            <telerik:RadTab runat="server" Text="Outstanding List" PageViewID="pgOL"
                Selected="True" />
            <telerik:RadTab runat="server" Text="Approved List" PageViewID="pgAL" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOL" runat="server" Selected="true">
            <telerik:RadGrid ID="grdListPr" runat="server" OnNeedDataSource="grdListPr_NeedDataSource"
                OnDetailTableDataBind="grdListPr_DetailTableDataBind" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="TransactionNo">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="TransactionNo"
                            DataNavigateUrlFields="PrUrl" HeaderText="Request No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="FServiceUnitID" HeaderText="Request Unit" UniqueName="FServiceUnitID"
                            SortExpression="FServiceUnitID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="CostUnit" HeaderText="Cost For Unit" UniqueName="CostUnit"
                            SortExpression="CostUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="TServiceUnitID" HeaderText="Purchasing Unit"
                            UniqueName="TServiceUnitID" SortExpression="TServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemName" HeaderText="Item Type"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="TransactionNo, SequenceNo" Name="grdDetailPr" Width="100%"
                            AutoGenerateColumns="false" ShowFooter="true" AllowPaging="true" PageSize="10">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="Description" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    Visible="False" />
                                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' /><br />
                                        <i>
                                            <asp:Label ID="lblAdditionalInfo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AdditionalInfo") %>' /></i>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Specification" HeaderText="Specification" UniqueName="Specification"
                                    SortExpression="Specification" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    Visible="False" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Quantity" HeaderText="Req. Qty"
                                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="QuantityFinishInBaseUnit"
                                    HeaderText="Order Qty" UniqueName="QuantityFinishInBaseUnit" SortExpression="QuantityFinishInBaseUnit"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="SRItemUnit" HeaderText="Req. Unit"
                                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Discount1Percentage"
                                    HeaderText="Disc 1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Discount2Percentage"
                                    HeaderText="Disc 2 (%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Disc Amount"
                                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="Total" UniqueName="Total"
                                    DataType="System.Double" DataFields="Price,Discount1Percentage,Discount2Percentage, Quantity"
                                    Expression="({0}*{3}) - ((({0}*{1}/100) + (({0} - ({0}*{1}/100)) * {2}/100)) * {3})"
                                    FooterText=" " FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridTemplateColumn/>
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgAL" runat="server">
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                OnDetailTableDataBind="grdList_DetailTableDataBind" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="TransactionNo">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="TransactionNo"
                            DataNavigateUrlFields="PrUrl" HeaderText="Order No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="FServiceUnitID" HeaderText="Request Unit" UniqueName="FServiceUnitID"
                            SortExpression="FServiceUnitID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="CostUnit" HeaderText="Cost For Unit" UniqueName="CostUnit"
                            SortExpression="CostUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="TServiceUnitID" HeaderText="Purchasing Unit"
                            UniqueName="TServiceUnitID" SortExpression="TServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemName" HeaderText="Item Type"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ApprovedByUserID"
                            HeaderText="Approved By" UniqueName="ApprovedByUserID" SortExpression="ApprovedByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="True" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" 
                            ItemStyle-HorizontalAlign="Center" DataField="ApprovedDate" HeaderText="Approve Date"
                            UniqueName="ApprovedDate" SortExpression="ApprovedDate" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="TransactionNo, SequenceNo" Name="grdDetail" AutoGenerateColumns="False"
                            ShowFooter="true" AllowPaging="true" PageSize="10">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="Description" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    Visible="False" />
                                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' /><br />
                                        <i>
                                            <asp:Label ID="lblAdditionalInfo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AdditionalInfo") %>' /></i>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Specification" HeaderText="Specification" UniqueName="Specification"
                                    SortExpression="Specification" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    Visible="False" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Quantity" HeaderText="Req. Qty"
                                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="QuantityFinishInBaseUnit"
                                    HeaderText="Order Qty" UniqueName="QuantityFinishInBaseUnit" SortExpression="QuantityFinishInBaseUnit"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="SRItemUnit" HeaderText="Req. Unit"
                                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Discount1Percentage"
                                    HeaderText="Disc 1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Discount2Percentage"
                                    HeaderText="Disc 2 (%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Disc Amount"
                                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="Total" UniqueName="Total"
                                    DataType="System.Double" DataFields="Price,Discount1Percentage,Discount2Percentage, Quantity"
                                    Expression="({0}*{3}) - ((({0}*{1}/100) + (({0} - ({0}*{1}/100)) * {2}/100)) * {3})"
                                    FooterText=" " FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridTemplateColumn/>
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
