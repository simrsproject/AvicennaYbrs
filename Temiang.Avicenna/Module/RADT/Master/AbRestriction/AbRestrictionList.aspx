<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="AbRestrictionList.aspx.cs" Inherits="Temiang.Avicenna.Module.Master.AbRestrictionList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="AbRestrictionID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="AbRestrictionID" HeaderText="ID"
                    UniqueName="AbRestrictionID" SortExpression="AbRestrictionID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="AbRestrictionName" HeaderText="Restriction Name"
                    UniqueName="RestrictionName" SortExpression="RestrictionName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ParentName" HeaderText="Parent"
                    UniqueName="ParentName" SortExpression="ParentName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AbRestrictionTypeName" HeaderText="Restriction Type"
                    UniqueName="AbRestrictionTypeName" SortExpression="AbRestrictionTypeName">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
