<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="AdditionalMealOrderList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.AdditionalMealOrderList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="OrderNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="OrderNo" HeaderText="Order No"
                    UniqueName="OrderNo" SortExpression="OrderNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn DataField="OrderDate" HeaderText="Order Date" UniqueName="OrderDate"
                    SortExpression="OrderDate">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="EffectiveDate" HeaderText="Effective Date"
                    UniqueName="EffectiveDate" SortExpression="EffectiveDate">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="MenuName" HeaderText="Menu Name" UniqueName="MenuName"
                    SortExpression="MenuName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MealSet" HeaderText="Set"
                    UniqueName="MealSet" SortExpression="MealSet" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="60" DataField="Qty" HeaderText="Qty"
                    UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n0}" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
