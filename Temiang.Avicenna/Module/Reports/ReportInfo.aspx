<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ReportInfo.aspx.cs" Inherits="Temiang.Avicenna.Module.Reports.ReportInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdInfo" runat="server" OnNeedDataSource="grdInfo_NeedDataSource" AutoGenerateColumns="false" ShowHeader="false">
        <MasterTableView DataKeyNames="ID">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="Parameter" UniqueName="Parameter" HeaderStyle-Width="150px">
                    <ItemTemplate>
                        <%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ParentID").ToString() )? string.Format("<strong>{0}</strong>", DataBinder.Eval(Container.DataItem, "ParameterName")) : string.Format("&nbsp;&nbsp;&nbsp;&nbsp;{0}", DataBinder.Eval(Container.DataItem, "ParameterName")) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="20px">
                    <ItemTemplate>
                        <%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ParentID").ToString() )? string.Empty:":" %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn  DataField="ParameterValue" HeaderText=" "
                    UniqueName="ParameterValue" SortExpression="ParameterValue" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
