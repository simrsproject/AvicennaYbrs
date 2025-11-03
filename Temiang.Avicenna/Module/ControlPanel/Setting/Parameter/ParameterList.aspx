<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="ParameterList.aspx.cs" Inherits="Temiang.Avicenna.ControlPanel.Setting.ParameterList"
    Title="Untitled Page" %>

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
