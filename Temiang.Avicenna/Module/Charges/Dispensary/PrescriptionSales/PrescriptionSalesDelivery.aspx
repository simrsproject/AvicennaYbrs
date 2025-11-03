<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PrescriptionSalesDelivery.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PrescriptionSalesDelivery" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdTransPrescriptionItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransPrescriptionItem" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadGrid ID="grdTransPrescriptionItem" runat="server" OnNeedDataSource="grdTransPrescriptionItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" AllowPaging="false" AllowMultiRowSelection="true">
        <MasterTableView DataKeyNames="PrescriptionNo, SequenceNo" GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="PrescriptionNo" HeaderText="Prescription No " />
                        <telerik:GridGroupByField FieldName="ApprovalDateTime" HeaderText="Date " />
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="PrescriptionNo" SortOrder="Ascending" />
                        <telerik:GridGroupByField FieldName="ApprovalDateTime" SortOrder="Ascending" />
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="PrescriptionNo" UniqueName="PrescriptionNo" SortExpression="PrescriptionNo"
                    Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" SortExpression="SequenceNo"
                    Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemID" UniqueName="ItemID" SortExpression="ItemID"
                    Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="DeliveredQty" HeaderText="Delivered Qty" UniqueName="DeliveredQty"
                    SortExpression="DeliveredQty">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderStyle-Width="60px"
                    ItemStyle-HorizontalAlign="Center" HeaderText="Qty" HeaderStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQty" runat="server" NumberFormat-DecimalDigits="2"
                            Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "QtyDelivery")) %>'
                            MinValue="0" MaxValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "QtyDelivery")) %>'
                            Width="100%">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="SRItemUnit" HeaderText="Unit" UniqueName="SRItemUnit"
                    SortExpression="SRItemUnit">
                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="Price" HeaderText="Price" UniqueName="Price"
                    SortExpression="Price" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="RecipeAmount" HeaderText="Recipe" UniqueName="Recipe"
                    SortExpression="Recipe" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="DiscountAmount" HeaderText="Discount" UniqueName="DiscountAmount"
                    SortExpression="DiscountAmount" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="Total" HeaderText="Total" UniqueName="Total"
                    SortExpression="Total" DataFormatString="{0:n2}">
                    <FooterStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
