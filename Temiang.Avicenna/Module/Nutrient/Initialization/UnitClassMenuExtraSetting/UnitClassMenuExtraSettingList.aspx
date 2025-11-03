<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="UnitClassMenuExtraSettingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.UnitClassMenuExtraSettingList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ServiceUnitID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ServiceUnitID" HeaderText="Service Unit ID"
                    UniqueName="ServiceUnitID" SortExpression="ServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ServiceUnitName" HeaderText="Service Unit Name"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ClassName" HeaderText="Class"
                    UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="MenuName" HeaderText="Menu"
                    UniqueName="MenuName" SortExpression="MenuName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />  
                <telerik:GridTemplateColumn/>          
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>