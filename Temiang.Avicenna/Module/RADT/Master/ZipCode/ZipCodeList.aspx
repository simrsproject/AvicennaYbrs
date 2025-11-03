<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ZipCodeList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ZipCodeList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ZipCode">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ZipCode" HeaderText="ID"
                    UniqueName="ZipCode" SortExpression="ZipCode" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ZipPostalCode" HeaderText="Zip Code"
                    UniqueName="ZipPostalCode" SortExpression="ZipPostalCode" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="StreetName" HeaderText="Street Name" UniqueName="StreetName"
                    SortExpression="StreetName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="District" HeaderText="District"
                    UniqueName="District" SortExpression="District" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="County" HeaderText="County"
                    UniqueName="County" SortExpression="County" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="City" HeaderText="City"
                    UniqueName="City" SortExpression="City" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
