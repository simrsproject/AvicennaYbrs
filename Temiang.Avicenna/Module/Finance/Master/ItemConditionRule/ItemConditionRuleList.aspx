<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ItemConditionRuleList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ItemConditionRuleList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ItemConditionRuleID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemConditionRuleID" HeaderText="ID"
                    UniqueName="ItemConditionRuleID" SortExpression="ItemConditionRuleID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemConditionRuleName" HeaderText="Description" UniqueName="ItemConditionRuleName"
                    SortExpression="ItemConditionRuleName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="StartingDate" HeaderText="Starting Date"
                    UniqueName="StartingDate" SortExpression="StartingDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="EndingDate" HeaderText="Ending Date"
                    UniqueName="EndingDate" SortExpression="EndingDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />    
                <telerik:GridBoundColumn DataField="ItemConditionRuleType" HeaderText="Rule Type"
                    UniqueName="ItemConditionRuleType" SortExpression="ItemConditionRuleType">
                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AmountValue" HeaderText="Amount Value" UniqueName="AmountValue"
                    SortExpression="AmountValue" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn DataField="IsValueInPercent" HeaderText="In Percent"
                    UniqueName="IsValueInPercent" SortExpression="IsValueInPercent">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
