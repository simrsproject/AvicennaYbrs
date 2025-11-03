<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="ConclusionList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.Conclusion.ConclusionList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ConclusionID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ConclusionID" HeaderText="ConclusionID"
                    UniqueName="ConclusionID" SortExpression="ConclusionID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ConclusionName" HeaderText="Conclusion" UniqueName="ConclusionName"
                    SortExpression="ConclusionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MinValue" HeaderText="Minimum Value" UniqueName="MinValue" SortExpression="MinValue"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MaxValue" HeaderText="Maximum Value" UniqueName="MaxValue"
                    SortExpression="MaxValue" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
