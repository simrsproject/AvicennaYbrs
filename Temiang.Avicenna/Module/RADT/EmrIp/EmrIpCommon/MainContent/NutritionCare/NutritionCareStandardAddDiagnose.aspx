<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NutritionCareStandardAddDiagnose.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NutritionCare.NutritionCareStandardAddDiagnose" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script runat="server"> 
            protected void Page_PreRender(object sender, EventArgs e) 
            {
                var xx = gridListProblem.MasterTableView.GetItems(GridItemType.GroupHeader);
                foreach (var x in xx) { 
                    x.Expanded = false; 
                }
            } 
        </script> 
    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="gridListProblem" runat="server" AutoGenerateColumns="false"
        GridLines="None" OnNeedDataSource="gridListProblem_NeedDataSource">
        <MasterTableView ClientDataKeyNames="TerminologyID" DataKeyNames="TerminologyID"
            GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="FTS" HeaderText=" " HeaderValueSeparator=" ">
                        </telerik:GridGroupByField>
                        <telerik:GridGroupByField FieldName="TerminologyParentName" HeaderText=" " HeaderValueSeparator=" ">
                        </telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="FTS" SortOrder="Descending">
                        </telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn HeaderText="" UniqueName="ChkBox">
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="defaultChkBox" runat="server" Checked='<%#!Eval("TransTerminologyID").ToString().Equals(string.Empty)%>'>
                        </asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="TerminologyName" HeaderText="Analysis" SortExpression="TerminologyName"
                    UniqueName="TerminologyName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TerminologyParentID" HeaderText="Parent"
                    SortExpression="TerminologyParentID" UniqueName="TerminologyParentID"
                    Visible="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LastUpdateTransDTBy" HeaderText="Last Update By"
                    SortExpression="LastUpdateTransDTBy" UniqueName="LastUpdateTransDTBy">
                    <HeaderStyle HorizontalAlign="Left" Width="100" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LastUpdateTransDTDateTime" HeaderText="Date"
                    SortExpression="LastUpdateTransDTDateTime" UniqueName="LastUpdateTransDTDateTime"
                    DataFormatString="{0:MM/dd/yyyy HH:mm}" DataType="System.DateTime">
                    <HeaderStyle HorizontalAlign="Left" Width="120" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
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