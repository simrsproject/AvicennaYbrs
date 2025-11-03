<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NutritionCareStandardNIC.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NutritionCare.NutritionCareStandardNIC" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <span style="font-size: 1.5em;"><%= FullDiagnosaName %></span>
    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="gridListRencana" runat="server" AutoGenerateColumns="false"
        GridLines="None" OnNeedDataSource="gridListRencana_NeedDataSource"
        OnItemDataBound="gridListRencana_ItemDataBound">
        <MasterTableView ClientDataKeyNames="TerminologyID" HierarchyLoadMode="ServerBind"
            HierarchyDefaultExpanded="true" ExpandCollapseColumn-Display="false" DataKeyNames="TerminologyID"
            GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="DomainName" HeaderText=" " HeaderValueSeparator=" "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="DomainSeqNo" SortOrder="Ascending"></telerik:GridGroupByField>
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
                <telerik:GridTemplateColumn HeaderText="Detail" UniqueName="IsDetail" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsDetail" runat="server" Checked='<%#Eval("IsDetail")%>'>
                        </asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="TerminologyID" HeaderText="Code" SortExpression="TerminologyID"
                    UniqueName="TerminologyID">
                    <HeaderStyle HorizontalAlign="Left" Width="100"/>
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Intervention" UniqueName="TerminologyNameEdited">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtTerminologyName" runat="server" Text='<%#Eval("TerminologyNameEdited")%>'
                            Width="1000px" MaxLength="255" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="TerminologyName" HeaderText="NIC" SortExpression="TerminologyName"
                    UniqueName="TerminologyName" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TerminologyParentID" HeaderText="Parent"
                    SortExpression="TerminologyParentID" UniqueName="TerminologyParentID"
                    Visible="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UserName" HeaderText="Last Update By"
                    SortExpression="UserName" UniqueName="UserName">
                    <HeaderStyle HorizontalAlign="Left" Width="100" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LastUpdateTransDTDateTime" HeaderText="Date"
                    DataFormatString="{0:MM/dd/yyyy HH:mm}" DataType="System.DateTime"
                    SortExpression="LastUpdateTransDTDateTime" UniqueName="LastUpdateTransDTDateTime">
                    <HeaderStyle HorizontalAlign="Left" Width="150" />
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

