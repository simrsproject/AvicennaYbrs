<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NursingCareStandardAddDiagnoseMidwifeCustomWithEdit.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare.NursingCareStandardAddDiagnoseMidwifeCustomWithEdit" %>
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
    <telerik:RadAjaxManagerProxy runat="server" ID="rmpAddDiag">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gridEtiologyNew">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridSelectedDiag" />
                    <telerik:AjaxUpdatedControl ControlID="lblSelectedDiagnosa" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td colspan="2">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="NursingDiagnosaTransDTNewDiag" />
                <asp:CustomValidator ID="customValidatorNewDiag" runat="server" ValidationGroup="NursingDiagnosaTransDTNewDiag"
ErrorMessage="" OnServerValidate="customValidatorNewDiag_ServerValidate">&nbsp;</asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <fieldset>
                    <legend>Selected Diagnosis</legend>
                    <telerik:RadGrid ID="gridSelectedDiag" runat="server" AutoGenerateColumns="false"
                        GridLines="None" OnNeedDataSource="gridSelectedDiag_NeedDataSource">
                        <MasterTableView ClientDataKeyNames="NursingDiagnosaID" DataKeyNames="NursingDiagnosaID">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Diagnosis" UniqueName="NursingDiagnosaName" HeaderStyle-Width="350px">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txtNursingDiagnosaName" Width="95%" runat="server" Text='<%#Eval("NursingDiagnosaName").ToString()%>' TextMode="MultiLine"></telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </fieldset>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%"  border="0">
                    <tr>
                        <td>
                            <div style="width: 100%; height: 600px; overflow: scroll">
                            <telerik:RadGrid ID="gridEtiologyNew" runat="server" AutoGenerateColumns="false"
                                GridLines="None" OnNeedDataSource="gridEtiologyNew_NeedDataSource">
                                <MasterTableView ClientDataKeyNames="NursingDiagnosaID" DataKeyNames="NursingDiagnosaID"
                                    GroupLoadMode="Client">
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="FTS" HeaderText=" " HeaderValueSeparator=" ">
                                                </telerik:GridGroupByField>
                                                <telerik:GridGroupByField FieldName="NursingParentName" HeaderText=" " HeaderValueSeparator=" ">
                                                </telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="NursingParentName" SortOrder="Ascending">
                                                </telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="ChkBox">
                                            <HeaderStyle HorizontalAlign="Left" Width="40px" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="defaultChkBox" runat="server" OnCheckedChanged="defaultChkBox_CheckedChanged" AutoPostBack="true" >
                                                </asp:CheckBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="NursingDiagnosaName" HeaderText="Diagnosis And Etiology" SortExpression="NursingDiagnosaName"
                                            UniqueName="NursingDiagnosaName" Visible="false">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Diagnosis And Etiology" UniqueName="NursingDiagnosaName" HeaderStyle-Width="350px">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txtNursingDiagnosaName" Width="95%" runat="server" Text='<%#Eval("NursingDiagnosaNameEdited").ToString()%>'></telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="SREtiologyTypeName" HeaderText="Type"
                                            SortExpression="SREtiologyTypeName" UniqueName="SREtiologyTypeName">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NursingDiagnosaParentID" HeaderText="Parent"
                                            SortExpression="NursingDiagnosaParentID" UniqueName="NursingDiagnosaParentID"
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
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
