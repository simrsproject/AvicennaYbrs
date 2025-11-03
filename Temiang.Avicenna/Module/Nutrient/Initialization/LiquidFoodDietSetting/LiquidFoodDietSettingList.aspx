<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="LiquidFoodDietSettingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.LiquidFoodDietSettingList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="DietID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DietID" HeaderText="Diet ID"
                    UniqueName="DietID" SortExpression="DietID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="DietName" HeaderText="Diet Name"
                    UniqueName="DietName" SortExpression="DietName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>