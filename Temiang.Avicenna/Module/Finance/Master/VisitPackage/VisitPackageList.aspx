<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="VisitPackageList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.VisitPackageList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="VisitPackageID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="VisitPackageID" HeaderText="ID"
                    UniqueName="VisitPackageID" SortExpression="VisitPackageID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="VisitPackageName" HeaderText="Visit Package" UniqueName="VisitPackageName"
                    SortExpression="VisitPackageName">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>