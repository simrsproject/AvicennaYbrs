<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="BpjsPackageList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.BpjsPackageList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="PackageID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PackageID" HeaderText="INA-CBG Code"
                    UniqueName="PackageID" SortExpression="PackageID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PackageName" HeaderText="INA-CBG Description"
                    UniqueName="PackageName" SortExpression="PackageName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="PackageID, StartingDate, ClassID" Name="grdTariff" Width="100%"
                    AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="StartingDate" HeaderText="Starting Date"
                            UniqueName="StartingDate" SortExpression="StartingDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ClassName" HeaderText="Class" UniqueName="ClassName"
                            SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>

