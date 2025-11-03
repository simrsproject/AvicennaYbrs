<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="ConsumeMethodList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.ConsumeMethodList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="SRConsumeMethod">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="SRConsumeMethod"
                    HeaderText="ID" UniqueName="SRConsumeMethod" SortExpression="SRConsumeMethod"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn DataField="SRConsumeMethodName"
                    HeaderText="Consume Method" UniqueName="SRConsumeMethodName" SortExpression="SRConsumeMethodName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="LineNumber"
                    HeaderText="Line Number" UniqueName="LineNumber" SortExpression="LineNumber"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="TimeSequence"
                    HeaderText="Time Sequence" UniqueName="TimeSequence" SortExpression="TimeSequence"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridNumericColumn HeaderStyle-Width="200px" DataField="SygnaText"
                    HeaderText="Cigna Text" UniqueName="SygnaText" SortExpression="SygnaText"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>   
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>