<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="MenuItemExtraList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Master.MenuItemExtraList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="SeqNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SeqNo" HeaderText="ID"
                    UniqueName="SeqNo" SortExpression="SeqNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MenuID" HeaderText="Menu ID" UniqueName="MenuID"
                    SortExpression="MenuID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="MenuName" HeaderText="Menu Name" UniqueName="MenuName"
                    SortExpression="MenuName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="StartingDate"
                    HeaderText="Starting Date" UniqueName="StartingDate" SortExpression="StartingDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="MealSet" HeaderText="Meal Set" UniqueName="MealSet"
                    SortExpression="MealSet" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn/>    
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
