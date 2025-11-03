<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ItemDeductionList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.ItemDeductionList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="DeductionID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="DeductionID" HeaderText="ID"
                    UniqueName="DeductionID" SortExpression="DeductionID">
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MinAmount" HeaderText="Min Amount"
                    UniqueName="MinAmount" SortExpression="MinAmount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MaxAmount" HeaderText="Max Amount"
                    UniqueName="MaxAmount" SortExpression="MaxAmount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DeductionAmount" HeaderText="Deduction Amount"
                    UniqueName="DeductionAmount" SortExpression="DeductionAmount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn/>    
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
