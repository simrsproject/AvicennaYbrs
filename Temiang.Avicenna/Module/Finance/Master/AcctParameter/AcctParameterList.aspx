<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcctParameterList.aspx.cs" MasterPageFile="~/MasterPage/MasterList.Master"
Inherits="Temiang.Avicenna.Module.Finance.Master.AcctParameter.AcctParameterList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="RadGrid1" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource">
        <MasterTableView DataKeyNames="ParameterID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ParameterID" HeaderText="ID"
                    UniqueName="ParameterID" SortExpression="CommonCodeID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ParameterName" HeaderText="Description" SortExpression="ParameterName"
                    UniqueName="ParameterName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ParameterValue" HeaderText="Value"
                    SortExpression="ParameterValue" UniqueName="ParameterValue">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
