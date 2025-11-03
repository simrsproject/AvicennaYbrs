<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="ContributoryFactorsClassificationFrameworkList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ContributoryFactorsClassificationFrameworkList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="FactorID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="FactorID" HeaderText="ID"
                    UniqueName="ItemID" SortExpression="FactorID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FactorName" HeaderText="Factor" UniqueName="FactorName"
                    SortExpression="ItemName">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>