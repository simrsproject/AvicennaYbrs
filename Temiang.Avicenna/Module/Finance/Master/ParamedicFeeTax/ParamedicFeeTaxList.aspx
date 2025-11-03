<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ParamedicFeeTaxList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ParamedicFeeTaxList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="CounterID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="125px" DataField="MinAmount" HeaderText="Min Amount"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="MinAmount"
                    SortExpression="MinAmount" DataFormatString="{0:n2}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="125px" DataField="MaxAmount" HeaderText="Max Amount"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="MinAmount"
                    SortExpression="MaxAmount" DataFormatString="{0:n2}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Percentage" HeaderText="Tax Value (%)"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="Percentage"
                    SortExpression="Percentage">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PercentageNonNpwp" HeaderText="Tax Value Non NPWP (%)"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" UniqueName="PercentageNonNpwp"
                    SortExpression="PercentageNonNpwp">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
