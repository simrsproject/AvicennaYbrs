<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ClinicalPathwayList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ClinicalPathwayList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind" AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="PathwayID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PathwayID" HeaderText="Pathway ID"
                    UniqueName="PathwayID" SortExpression="PathwayID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PathwayName" HeaderText="Pathway Name" UniqueName="PathwayName"
                    SortExpression="PathwayName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="StartingDate" HeaderText="Starting Date"
                    UniqueName="StartingDate" SortExpression="StartingDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CoverageValue1" HeaderText="Class I"
                    UniqueName="CoverageValue1" SortExpression="CoverageValue1" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CoverageValue2" HeaderText="Class II"
                    UniqueName="CoverageValue2" SortExpression="CoverageValue2" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CoverageValue3" HeaderText="Class III"
                    UniqueName="CoverageValue3" SortExpression="CoverageValue3" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ALOS" HeaderText="ALOS"
                    UniqueName="ALOS" SortExpression="ALOS" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="PathwayID, DiagnoseID" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridBoundColumn DataField="DiagnoseID" HeaderText="Diagnose ID" UniqueName="DiagnoseID"
                            SortExpression="DiagnoseID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="100px" />
                        <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Diagnose Name" UniqueName="DiagnoseName"
                            SortExpression="DiagnoseName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
