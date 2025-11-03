<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="RiskGradingList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.RiskGradingList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="RiskGradingID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RiskGradingID" HeaderText="ID"
                    UniqueName="RiskGradingID" SortExpression="RiskGradingID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="RiskGradingName" HeaderText="Risk Grading"
                    UniqueName="RiskGradingName" SortExpression="RiskGradingName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RiskGradingColor" HeaderText="Color"
                    UniqueName="RiskGradingColor" SortExpression="RiskGradingName">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="GradingColor" HeaderStyle-Width="150px" HeaderText=""
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtGradingColor" runat="server" Width="50px" BackColor='<%# GetColorOfGradingColor(DataBinder.Eval(Container.DataItem,"RiskGradingColor")) %>'></asp:TextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
