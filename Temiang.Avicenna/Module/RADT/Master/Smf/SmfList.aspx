<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="SmfList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.SmfList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="SmfID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SmfID" HeaderText="SMF ID"
                    UniqueName="SmfID" SortExpression="SmfID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="SmfName" HeaderText="SMF Name" UniqueName="SmfName"
                    SortExpression="SmfName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ParamedicFeeCaseTypeName" HeaderText="Paramedic Fee Case Type"
                    UniqueName="ParamedicFeeCaseTypeName" SortExpression="ParamedicFeeCaseTypeName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="AssessmentTypeName" HeaderText="Assessment Type"
                    UniqueName="AssessmentTypeName" SortExpression="AssessmentTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
