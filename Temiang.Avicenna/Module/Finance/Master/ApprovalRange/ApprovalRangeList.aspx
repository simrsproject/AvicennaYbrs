<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ApprovalRangeList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ApprovalRangeList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="ApprovalRangeID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ApprovalRangeID" HeaderText="ID"
                    UniqueName="ApprovalRangeID" SortExpression="ApprovalRangeID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="TransactionName" HeaderText="Transaction Code"
                    UniqueName="TransactionName" SortExpression="TransactionName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ItemTypeName" HeaderText="Item Type"
                    UniqueName="ItemTypeName" SortExpression="ItemTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ItemGroupName" HeaderText="Item Group"
                    UniqueName="ItemGroupName" SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="AmountFrom" HeaderText="Amount From"
                    UniqueName="AmountFrom" SortExpression="AmountFrom" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ApprovalLevelFinal"
                    HeaderText="Level Count" UniqueName="ApprovalLevelFinal" SortExpression="ApprovalLevelFinal"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridTemplateColumn />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="ApprovalLevel" Name="grdDetail" AutoGenerateColumns="False"
                    ShowFooter="true" AllowPaging="true" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ApprovalLevel" HeaderText="Approval Level"
                            UniqueName="ApprovalLevel" SortExpression="ApprovalLevel" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="UserName" HeaderText="User Approval Name" UniqueName="UserName"
                            SortExpression="UserName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
