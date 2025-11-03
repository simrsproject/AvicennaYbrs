<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PrescriptionSplitBillInfo.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.FinalizeBilling.PrescriptionSplitBillInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdPrescription">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <telerik:RadGrid ID="grdPrescription" runat="server" OnNeedDataSource="grdPrescription_NeedDataSource"
        OnDetailTableDataBind="grdPrescription_DetailTableDataBind" AllowSorting="true"
        ShowStatusBar="true" AllowPaging="true" PageSize="15">
        <MasterTableView DataKeyNames="PrescriptionNo" AutoGenerateColumns="false" GroupLoadMode="Client">
            <Columns>
                <telerik:GridCheckBoxColumn DataField="IsSplitBill" HeaderText="Split Bill" UniqueName="IsSplitBill"
                    SortExpression="IsSplitBill">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsCash" HeaderText="Cash" UniqueName="IsCash"
                    SortExpression="IsCash">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PrescriptionDate"
                    HeaderText="Presc. Date" UniqueName="PrescriptionDate" SortExpression="PrescriptionDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PrescriptionNo" HeaderText="Prescription No"
                    UniqueName="PrescriptionNo" SortExpression="PrescriptionNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="DispensaryName" HeaderText="Dispensary" HeaderStyle-Width="350px"
                    UniqueName="DispensaryName" SortExpression="DispensaryName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                    SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                    DataType="System.Double" DataFields="LineAmount" SortExpression="Total" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="Detail" DataKeyNames="SequenceNo" AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="Seq. No" UniqueName="SequenceNo"
                            HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsCompound" HeaderText="C"
                            UniqueName="IsCompound" SortExpression="IsCompound" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Header" UniqueName="ParentNo"
                            HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderStyle-Width="35px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblR" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsRFlag")) ? @"R/" : "" %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                            <ItemTemplate>
                                <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Numero" UniqueName="TemplateItemName3" HeaderStyle-Width="60px"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PrescriptionQty") %><br />
                                (<%# DataBinder.Eval(Container.DataItem, "ResultQty") %>)
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateItemName4" HeaderStyle-Width="100px">
                            <ItemTemplate>
                                <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? DataBinder.Eval(Container.DataItem, "EmbalaceLabel") : DataBinder.Eval(Container.DataItem, "SRItemUnit")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="DosageQty" HeaderText="Dosing"
                            UniqueName="DosageQty" SortExpression="DosageQty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRDosageUnit" HeaderText="Unit"
                            UniqueName="SRDosageUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SRConsumeMethodName" HeaderText="Consume Method"
                            UniqueName="SRConsumeMethodName" SortExpression="SRConsumeMethodName" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RecipeAmount" HeaderText="Recipe"
                            UniqueName="RecipeAmount" SortExpression="RecipeAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DiscountAmount" HeaderText="Discount"
                            UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                            DataType="System.Double" DataFields="LineAmount" SortExpression="Total" FooterStyle-HorizontalAlign="Right"
                            Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
