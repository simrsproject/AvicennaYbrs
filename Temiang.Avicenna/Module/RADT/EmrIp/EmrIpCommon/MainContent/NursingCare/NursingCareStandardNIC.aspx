<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NursingCareStandardNIC.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare.NursingCareStandardNIC" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <table class="info success" width="100%">
            <tr>
                <td>
                    <span style="font-size: 1.5em;"><%= FullDiagnosaName %></span>
                </td>
            </tr>
        </table>
    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="gridListRencana" runat="server" AutoGenerateColumns="false"
        GridLines="None" OnNeedDataSource="gridListRencana_NeedDataSource"
        OnItemDataBound="gridListRencana_ItemDataBound">
        <MasterTableView ClientDataKeyNames="NursingDiagnosaID" HierarchyLoadMode="ServerBind"
            HierarchyDefaultExpanded="true" ExpandCollapseColumn-Display="false" DataKeyNames="NursingDiagnosaID"
            GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="SRNursingNicTypeName" HeaderText=" " HeaderValueSeparator=" ">
                        </telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="SRNursingNicType" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="F2" HeaderText=" " HeaderValueSeparator=" ">
                        </telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="F2" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="F1" HeaderText=" " HeaderValueSeparator=" ">
                        </telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="F1" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn HeaderText="" UniqueName="ChkBox">
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="defaultChkBox" runat="server" Checked='<%#!Eval("TransNursingDiagnosaID").ToString().Equals(string.Empty)%>'>
                        </asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Interventions" UniqueName="NursingDiagnosaNameEdited">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtNursingDiagnosaName" runat="server" Text='<%#Eval("NursingDiagnosaNameEdited")%>'
                            Width="550px" MaxLength="255" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="NursingDiagnosaName" HeaderText="NIC" SortExpression="NursingDiagnosaName"
                    UniqueName="NursingDiagnosaName" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NursingDiagnosaParentID" HeaderText="Parent"
                    SortExpression="NursingDiagnosaParentID" UniqueName="NursingDiagnosaParentID"
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
