<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashTransactionTemplateList.aspx.cs" MasterPageFile="~/MasterPage/MasterList.Master"
Inherits="Temiang.Avicenna.Module.Finance.Master.CashTransactionTemplateList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="TemplateId">
            <Columns>
                <telerik:GridBoundColumn DataField="TemplateId" HeaderText="ID" UniqueName="TemplateId"
                    SortExpression="TemplateId" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px"/>
                <telerik:GridBoundColumn DataField="TemplateName" HeaderText="TemplateName" UniqueName="TemplateName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />               
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>


