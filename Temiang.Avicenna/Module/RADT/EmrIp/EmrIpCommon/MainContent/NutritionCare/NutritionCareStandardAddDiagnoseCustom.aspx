<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NutritionCareStandardAddDiagnoseCustom.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NutritionCare.NutritionCareStandardAddDiagnoseCustom" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script runat="server"> 
            protected void Page_PreRender(object sender, EventArgs e) 
            {
                var xx = gridEtiologyNew.MasterTableView.GetItems(GridItemType.GroupHeader);
                foreach (var x in xx) { 
                    x.Expanded = false; 
                }
            } 
        </script> 
    </telerik:RadCodeBlock>

    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td colspan="2">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="NutritionCareDiagnoseTransDTNewDiag" />
                <asp:CustomValidator ID="customValidatorNewDiag" runat="server" ValidationGroup="NutritionCareDiagnoseTransDTNewDiag"
ErrorMessage="" OnServerValidate="customValidatorNewDiag_ServerValidate">&nbsp;</asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%"  border="0">
                    <tr>
                        <td class="label" style="width:50px">
                            <asp:Label ID="Label8" runat="server" Text="S/S"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtNewS" runat="server" Width="100%" 
                                TextMode="MultiLine" Height="300px" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label" style="width:25%">
                            <asp:Label ID="Label9" runat="server" Text="DO"></asp:Label>
                        </td>
                        <td style="width:75%">
                            <telerik:RadTextBox ID="txtNewO" runat="server" Width="100%" 
                                TextMode="MultiLine" Height="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%"  border="0">
                    <tr>
                        <td>
                            <div style="width: 100%; height: 500px; overflow: scroll">
                            <telerik:RadGrid ID="gridEtiologyNew" runat="server" AutoGenerateColumns="false"
                                GridLines="None" OnNeedDataSource="gridEtiologyNew_NeedDataSource">
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
                                                <telerik:GridGroupByField FieldName="ParentSequenceNo" SortOrder="Ascending">
                                                </telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="ChkBox">
                                            <HeaderStyle HorizontalAlign="Left" Width="40px" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="defaultChkBox" runat="server" >
                                                </asp:CheckBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="TerminologyName" HeaderText="Etiology" SortExpression="TerminologyName"
                                            UniqueName="TerminologyName" Visible="false">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Etiology" UniqueName="TerminologyName">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txtNutritionCareDiagnosaName" Width="95%" runat="server" Text='<%#Eval("TerminologyNameEdited").ToString()%>'></telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="TerminologyParentID" HeaderText="Parent"
                                            SortExpression="TerminologyParentID" UniqueName="TerminologyParentID"
                                            Visible="false">
                                            <HeaderStyle HorizontalAlign="Left" />
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
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="10">
                                <tr>
                                    <td><asp:CheckBox ID="chkShowAllDiag" runat="server" Text="Show All" /></td>
                                    <td><asp:LinkButton ID="lbtnRefresh" runat="server" OnClick="lbtnRefresh_Click" 
                                        ToolTip="Refresh">
                                        <img style="border: 0px; text-align:center; vertical-align: middle;" src="../../../../../../Images/Toolbar/search16.png" />
                                    </asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

