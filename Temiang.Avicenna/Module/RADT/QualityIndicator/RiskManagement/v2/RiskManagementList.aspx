<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="RiskManagementList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.v2.RiskManagementList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true">
        <MasterTableView DataKeyNames="RiskManagementNo" ClientDataKeyNames="RiskManagementNo">
            <Columns>
                <telerik:GridBoundColumn DataField="RiskManagementNo" HeaderText="Risk Management No" UniqueName="RiskManagementNo"
                    SortExpression="RiskManagementNo">
                    <HeaderStyle HorizontalAlign="Center" Width="135px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PeriodYear" HeaderText="Year Period"
                    UniqueName="PeriodYear" SortExpression="PeriodYear" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                <telerik:GridBoundColumn HeaderStyle-Width="350px"  DataField="ServiceUnitName" HeaderText="Service Unit"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>