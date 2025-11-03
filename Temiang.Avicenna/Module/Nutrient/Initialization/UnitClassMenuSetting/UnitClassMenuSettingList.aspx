<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master"
    AutoEventWireup="true" CodeBehind="UnitClassMenuSettingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.UnitClassMenuSettingList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ServiceUnitID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ServiceUnitID" HeaderText="Service Unit ID"
                    UniqueName="ServiceUnitID" SortExpression="ServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Name"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
