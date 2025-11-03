<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="ClassMenuSettingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.ClassMenuSettingList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ClassID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ClassID" HeaderText="Class ID"
                    UniqueName="ClassID" SortExpression="ClassID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class Name"
                    UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsOptional" HeaderText="Optional"
                    UniqueName="IsOptional" SortExpression="IsOptional" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />    
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>