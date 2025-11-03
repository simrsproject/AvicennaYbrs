<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NursingCareStandardNOC.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare.NursingCareStandardNOC" %>
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
    <telerik:RadGrid ID="gridListTarget" runat="server" AutoGenerateColumns="false" GridLines="None"
        OnNeedDataSource="gridListTarget_NeedDataSource" OnItemDataBound="gridListTarget_ItemDataBound">
        <MasterTableView ClientDataKeyNames="NursingDiagnosaID,IdDiag" HierarchyLoadMode="ServerBind"
            HierarchyDefaultExpanded="true" ExpandCollapseColumn-Display="false" DataKeyNames="NursingDiagnosaID,IdDiag"
            GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="NocTypeName" HeaderText=" " HeaderValueSeparator=" ">
                        </telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="NocType" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="NocSequenceNo" HeaderText=" " HeaderValueSeparator=" ">
                        </telerik:GridGroupByField>
                        <telerik:GridGroupByField FieldName="NocName" HeaderText=" " HeaderValueSeparator=" ">
                        </telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="NocID" SortOrder="Ascending"></telerik:GridGroupByField>
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
                <telerik:GridTemplateColumn HeaderText="Outcomes" UniqueName="NursingDiagnosaNameEdited">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtNursingDiagnosaName" runat="server" Text='<%#Eval("NursingDiagnosaNameEdited")%>'
                            Width="550px" MaxLength="255" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="NursingDiagnosaName" HeaderText="Kriteria" SortExpression="NursingDiagnosaName"
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
                <telerik:GridTemplateColumn HeaderText="Skala" UniqueName="Skala" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                    <ItemTemplate>
                        <asp:RadioButtonList ID="rbDefaultSkala" runat="server" RepeatDirection="Horizontal"
                            SelectedValue='<%#Eval("Skala").ToString().Equals(string.Empty) ? "1" : Eval("Skala").ToString()%>'>
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                        </asp:RadioButtonList>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Target" UniqueName="Target" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                    <ItemTemplate>
                        <telerik:RadNumericTextBox MinValue="0" MaxValue="5" ID="txtDefaultTarget" runat="server"
                            Text='<%#Eval("Target").ToString().Equals(string.Empty) ? "5" : Eval("Target")%>'
                            Width="70px" MaxLength="20" >
                            <NumberFormat DecimalDigits="0"></NumberFormat>
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
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
