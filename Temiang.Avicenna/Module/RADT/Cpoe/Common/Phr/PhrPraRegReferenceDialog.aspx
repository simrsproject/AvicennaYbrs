<%@ Page Title="Patient Transfer Reference" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PhrPraRegReferenceDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Emr.Phr.PhrPraRegReferenceDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" 
        AllowSorting="true">
        <MasterTableView DataKeyNames="TransactionNo" ShowHeader="True">
            <Columns>
                <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Doc No" UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-Width="80px">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RmNO" HeaderText="Form ID" UniqueName="RmNO" SortExpression="RmNO" HeaderStyle-Width="80px">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="QuestionFormName" HeaderText="Form Name" UniqueName="QuestionFormName" SortExpression="QuestionFormName" >
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CreateByUserID" HeaderText="Created By" UniqueName="CreateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="CreateDateTime" HeaderText="Time" UniqueName="CreateDateTime"
                                         HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
