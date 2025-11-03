<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="ItemProductTariffRequestList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Tariff.ItemProductTariffRequestList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="TariffRequestNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TariffRequestNo" HeaderText="Request No"
                    UniqueName="TariffRequestNo" SortExpression="TariffRequestNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TariffRequestDate"
                    HeaderText="Request Date" UniqueName="TariffRequestDate" SortExpression="TariffRequestDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TariffTypeName" HeaderText="Tariff Type"
                    UniqueName="TariffTypeName" SortExpression="TariffTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ClassName" HeaderText="Class"
                    UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemTypeName" HeaderText="Item Type"
                    UniqueName="ItemTypeName" SortExpression="ItemTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartingDate" HeaderText="Starting Date"
                    UniqueName="StartingDate" SortExpression="StartingDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ApprovedDate" HeaderText="Approved Date"
                    UniqueName="ApprovedDate" SortExpression="ApprovedDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="TariffRequestNo,ItemID" Name="grdItem" Width="100%"
                    AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                        <telerik:GridTemplateColumn />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="TariffComponentID" Name="grdItemTariffRequestItemComp"
                            Width="500px" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="TariffComponentName"
                                    HeaderText="Component Name" UniqueName="TariffComponentName" SortExpression="TariffComponentName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Tariff"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowDiscount"
                                    HeaderText="Discount" UniqueName="IsAllowDiscount" SortExpression="IsAllowDiscount"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowVariable"
                                    HeaderText="Variable" UniqueName="IsAllowVariable" SortExpression="IsAllowVariable"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>

