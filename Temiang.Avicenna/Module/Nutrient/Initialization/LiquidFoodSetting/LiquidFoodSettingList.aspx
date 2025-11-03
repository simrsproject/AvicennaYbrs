<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="LiquidFoodSettingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.LiquidFoodSettingList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="StandardReferenceID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="StandardReferenceID" HeaderText="ID"
                    UniqueName="StandardReferenceID" SortExpression="StandardReferenceID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="StandardReferenceName" HeaderText="Description"
                    UniqueName="StandardReferenceName" SortExpression="StandardReferenceName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>