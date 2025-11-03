<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="DtdList.aspx.cs" Inherits="Temiang.Avicenna.Module.Master.DtdList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="DtdNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="DtdNo" HeaderText="DTD No"
                    UniqueName="DtdNo" SortExpression="DtdNo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="400px" DataField="DtdName" HeaderText="DTD Name"
                    UniqueName="DtdName" SortExpression="DtdName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="DtdLabel" HeaderText="DTD Label"
                    UniqueName="DtdLabel" SortExpression="DtdLabel">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ChronicDisease" HeaderText="Chronic Disease"
                    UniqueName="ChronicDisease" SortExpression="ChronicDisease">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="DiagnoseID" Name="grdDiagnose" Width="100%"
                    AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DiagnoseID" HeaderText="Diagnose ID"
                            UniqueName="DiagnoseID" SortExpression="DiagnoseID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="DiagnoseName" HeaderText="Diagnose Name"
                            UniqueName="DiagnoseName" SortExpression="DiagnoseName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsChronicDisease"
                            HeaderText="Chronic Disease" UniqueName="IsChronicDisease" SortExpression="IsChronicDisease"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsDisease" HeaderText="Disease"
                            UniqueName="IsDisease" SortExpression="IsDisease" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
