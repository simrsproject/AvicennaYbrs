<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="RecipeMarginList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.RecipeMarginList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="CounterID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="125px" DataField="StartingValue" HeaderText="Starting Value"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="StartingValue"
                    SortExpression="StartingValue" DataFormatString="{0:n2}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="125px" DataField="EndingValue" HeaderText="Ending Value"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="EndingValue"
                    SortExpression="EndingValue" DataFormatString="{0:n2}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RecipeAmount" HeaderText="Recipe Amount"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="RecipeAmount"
                    SortExpression="RecipeAmount">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
