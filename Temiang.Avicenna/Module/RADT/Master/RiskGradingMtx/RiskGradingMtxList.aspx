<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="RiskGradingMtxList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.RiskGradingMtxList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="ItemID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="ID"
                    UniqueName="ItemID" SortExpression="ItemID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Clinical Impact" UniqueName="ItemName"
                    SortExpression="ItemName">
                </telerik:GridBoundColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="SRIncidentProbabilityFrequency" Name="grdListItem"
                    AutoGenerateColumns="False">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="IncidentProbabilityFrequency"
                            HeaderText="Incident Probability Frequency" UniqueName="IncidentProbabilityFrequency"
                            SortExpression="IncidentProbabilityFrequency" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="IncidentFollowUp" HeaderText="Incident Follow Up"
                            UniqueName="IncidentFollowUp" SortExpression="IncidentFollowUp" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RiskGradingName" HeaderText="Risk Grading"
                            UniqueName="RiskGradingName" SortExpression="RiskGradingName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />    
                        <telerik:GridTemplateColumn UniqueName="RiskGradingColor" HeaderStyle-Width="150px" HeaderText=""
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRiskGradingColor" runat="server" Width="50px" BackColor='<%# GetColorOfGradingColor(DataBinder.Eval(Container.DataItem,"RiskGradingColor")) %>'></asp:TextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
