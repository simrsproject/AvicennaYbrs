<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="ReOrderPoBasedOnPr.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Procurement.ReOrderPoBasedOnPr" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">

            function confirmPoClose(oWnd, eventArgs) {
                var arg = eventArgs.get_argument();
                if (arg) {
                    document.getElementById("poGenerated").innerHTML = arg;
                    // Run lnkCalculate OnClick event
                    __doPostBack('<%=lnkCalculate.UniqueID %>', '');
                }
            }

        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboToServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSRItemType" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lnkCalculate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboFromServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="rfvFromServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="cboToServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="rfvToServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRItemType" />
                    <telerik:AjaxUpdatedControl ControlID="rfvSRItemType" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lnkConfirmPo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lnkConfirmPo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winConfirmPo" runat="server" Width="1000px" Height="500px"
        Style="z-index: 7001" ShowContentDuringLoad="false" Behavior="Close,Move" OnClientClose="confirmPoClose"
        Title="Confirm Generate Purchase Order">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <fieldset id="fsQueryEntry" runat="server" style="width: 450px; min-height: 100px;">
                    <legend>1. PR Query</legend>
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromServiceUnit" runat="server" Text="From Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" />
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage=""
                                    ValidationGroup="query" ControlToValidate="cboFromServiceUnitID" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td rowspan="3">
                                <asp:LinkButton ID="lnkCalculate" runat="server" ToolTip="Calculate" OnClick="lnkCalculate_OnClick"><img src="../../../../Images/Inventory/asset_search_48.png"/> </asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblToServiceUnit" runat="server" Text="Purchasing Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboToServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboToServiceUnitID_SelectedIndexChanged" />
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvToServiceUnitID" runat="server" ErrorMessage=""
                                    ValidationGroup="query" ControlToValidate="cboToServiceUnitID" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRItemType" runat="server" Text="Item Type" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvSRItemType" runat="server" ErrorMessage="" ValidationGroup="query"
                                    ControlToValidate="cboSRItemType" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
            <td>
            </td>
            <td valign="top">
                <fieldset id="FieldSet1" style="width: 150px; min-height: 100px;">
                    <legend>2. Generate PO</legend>
                    <div style="height: 20px">
                    </div>
                    <center>
                        <asp:LinkButton ID="lnkConfirmPo" runat="server" ToolTip="Generate PO from selected item"
                            OnClick="lnkConfirmPo_OnClick"><img src="../../../../Images/Inventory/order_to_vendor_48.png"/> </asp:LinkButton>
                    </center>
                </fieldset>
            </td>
            <td style="width: 100%" valign="top">
                <fieldset id="FieldSet3" style="width: 500px; min-height: 100px;">
                    <legend>Generated PO</legend>
                    <p id="poGenerated">
                    </p>
                </fieldset>
            </td>
        </tr>
    </table>
    <div style="height: 4px">
    </div>
    <telerik:RadGrid ID="grdList" runat="server" OnItemCreated="grdList_ItemCreated"
        OnNeedDataSource="grdList_NeedDataSource" AllowPaging="True" PageSize="15" AllowSorting="true"
        AllowFilteringByColumn="True" ShowStatusBar="true">
        <MasterTableView DataKeyNames="RowID" ClientDataKeyNames="RowID" AutoGenerateColumns="false">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldAlias="ItemName" FieldName="ItemName"></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="ItemName" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px"
                    AllowFiltering="False">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem,"IsSelect").ToInt()==1%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="0px" DataField="RowID" HeaderText="Row"
                    UniqueName="RowID" SortExpression="RowID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    AutoPostBackOnFilter="true" FilterControlWidth="92%" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" AutoPostBackOnFilter="true" />
                <telerik:GridBoundColumn DataField="FromUnit" HeaderText="From Unit" UniqueName="FromUnit"
                    SortExpression="FromUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    AllowFiltering="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="FromBalance" HeaderText="Balance"
                    UniqueName="FromBalance" SortExpression="FromBalance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" AllowFiltering="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="ToBalance" HeaderText="PU. Balance"
                    UniqueName="ToBalance" SortExpression="ToBalance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" AllowFiltering="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyOutstanding" HeaderText="PR Bal"
                    UniqueName="QtyOutstanding" SortExpression="QtyOutstanding" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" AllowFiltering="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" AllowFiltering="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Unit" HeaderText="Purchase Unit"
                    UniqueName="Unit" SortExpression="Unit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    AllowFiltering="False" />
                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Qty" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" AllowFiltering="False">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="40px" DbValue='<%#DataBinder.Eval(Container.DataItem,"QtyOrder")%>'
                            NumberFormat-DecimalDigits="0" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="90px" HeaderText="Item Unit" UniqueName="ItemUnit"
                    HeaderStyle-HorizontalAlign="left" AllowFiltering="False">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cboItemUnitSelected" runat="server" Width="100%">
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Supplier" UniqueName="SupplierID" HeaderStyle-HorizontalAlign="left"
                    AllowFiltering="False">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cboSupplierID" runat="server" Width="100%" EnableLoadOnDemand="true"
                            OnItemsRequested="cboSupplierID_ItemsRequested" OnItemDataBound="cboSupplierID_ItemDataBound">
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="false" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
