<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="CustomerList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.CustomerList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="CustomerID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CustomerID" HeaderText="Customer ID"
                    UniqueName="CustomerID" SortExpression="CustomerID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer Name"
                    UniqueName="CustomerName" SortExpression="CustomerName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />               
                <telerik:GridBoundColumn DataField="ContactPerson" HeaderText="Contact Person"
                    UniqueName="ContactPerson" SortExpression="ContactPerson" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="StreetName" HeaderText="Address" UniqueName="StreetName"
                    SortExpression="StreetName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="City" HeaderText="City"
                    UniqueName="City" SortExpression="City" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PhoneNo" HeaderText="Phone No"
                    UniqueName="PhoneNo" SortExpression="PhoneNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Email" HeaderText="Email"
                    UniqueName="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
