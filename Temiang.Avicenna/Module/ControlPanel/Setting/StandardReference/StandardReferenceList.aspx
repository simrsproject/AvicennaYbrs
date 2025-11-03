<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="StandardReferenceList.aspx.cs" Inherits="Temiang.Avicenna.ControlPanel.Setting.StandardReferenceList"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="false" OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="StandardReferenceID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="StandardReferenceID"
                    HeaderText="ID" UniqueName="StandardReferenceID" SortExpression="StandardReferenceID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="StandardReferenceName" HeaderText="Name" UniqueName="StandardReferenceName"
                    SortExpression="StandardReferenceName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="StandardReferenceGroup"
                    HeaderText="Group" UniqueName="StandardReferenceGroup" SortExpression="StandardReferenceGroup"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemLength" HeaderText="Item Length"
                    UniqueName="ItemLength" SortExpression="ItemLength" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsUsedBySystem" HeaderText="System"
                    UniqueName="IsUsedBySystem" SortExpression="IsUsedBySystem" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="StandardReferenceID, ItemID" Name="grdReferenceItem"
                    AutoGenerateColumns="False">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="NumericValue" HeaderText="Numeric Value" UniqueName="NumericValue"
                            SortExpression="NumericValue">
                        </telerik:GridNumericColumn>
                        <telerik:GridBoundColumn DataField="Note" HeaderText="Note" UniqueName="Note" SortExpression="Note"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ReferenceID" HeaderText="Reference ID" SortExpression="ReferenceID"
                            UniqueName="ReferenceID" HeaderStyle-Width="150px">
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsUsedBySystem" HeaderText="System"
                            UniqueName="IsUsedBySystem" SortExpression="IsUsedBySystem" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn DataField="IsActive" HeaderStyle-Width="100px" HeaderText="Active"
                            SortExpression="IsActive" UniqueName="IsActive">
                        </telerik:GridCheckBoxColumn>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
