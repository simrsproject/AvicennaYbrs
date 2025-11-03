<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="RiskFactorsList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.RiskFactorsList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ItemID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ItemID" HeaderText="Type ID"
                    UniqueName="ItemID" SortExpression="ItemID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Type Name" UniqueName="ItemName"
                    SortExpression="ItemName">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
