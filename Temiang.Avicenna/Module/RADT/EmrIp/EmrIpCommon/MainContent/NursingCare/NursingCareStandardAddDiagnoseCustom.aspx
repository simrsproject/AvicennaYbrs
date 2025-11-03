<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NursingCareStandardAddDiagnoseCustom.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare.NursingCareStandardAddDiagnoseCustom" %>
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
        <script type="text/javascript">
            function openNurseNoteDialog() {
                var oWndX = $find("<%= winDlg.ClientID %>");
                oWndX.SetUrl("NursingCareStandardPickImplementation.aspx?regno=<%= RegistrationNo%>");
                oWndX.show();
                oWndX.maximize();
            }

            function openAssessmentPickDialog() {
                //alert("Under construction!!!");

                var oWndX = $find("<%= winDlg.ClientID %>");
                oWndX.SetUrl("NursingCarePickQuestions.aspx?regno=<%= RegistrationNo%>&diagtype=<%= DiagType%>");
                oWndX.show();
                oWndX.maximize();
            }

            function win_OnClientClose(oWndX, args) {
                //alert(oWndX.argument.dataDS);
                if (oWndX.argument == undefined || oWndX.argument == null) {
                } else {
                    if (oWndX.argument.result == undefined || oWndX.argument.result == null) {
                    } else if (oWndX.argument.result == 'OK') {
                        $find('<%=txtNewDS.ClientID %>').set_value(decodeURI(oWndX.argument.dataDS));
                        $find('<%=txtNewDO.ClientID %>').set_value(decodeURI(oWndX.argument.dataDO));
                    } 
                }
                oWndX = null;
                //alert("post back");
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDlg" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false" OnClientClose="win_OnClientClose"
        Modal="true">
    </telerik:RadWindow>
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
                <table width="100%"  border="0">
                    <tr>
                        <td class="label" style="width:50px">
                            <asp:Label ID="Label1" runat="server" Text="Nursing Type"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox runat="server" ID="cboNsTypeID" Width="304px">
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                    </tr>
                    
                    <tr>
                        <td class="label" style="width:50px">
                            <asp:Label ID="Label8" runat="server" Text="Data Subjective"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtNewDS" runat="server" Width="100%" 
                                TextMode="MultiLine" Height="100px" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label" style="width:50px">
                            <asp:Label ID="Label9" runat="server" Text="Data Objective"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtNewDO" runat="server" Width="100%" 
                                TextMode="MultiLine" Height="130px" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtnSearchNote" runat="server" OnClientClick="openNurseNoteDialog();return false;"
                            ToolTip="Search Nursing Notes">
                                <span>PPA Notes</span>
                                <img style="border: 0px; text-align:center; vertical-align: middle;" src="../../../../../../Images/Toolbar/search16.png" />
                            </asp:LinkButton>
                            <br /><br />
                            <asp:LinkButton ID="lbtnSearchAssessment" runat="server" OnClientClick="openAssessmentPickDialog();return false;" 
                            ToolTip="Search Assessment">
                                <span>Assessment</span>
                                <img style="border: 0px; text-align:center; vertical-align: middle;" src="../../../../../../Images/Toolbar/search16.png" />
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" style="width:50px">
                            
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><asp:CheckBox ID="chkShowAllDiag" runat="server" Text="Show All" Checked="true" /></td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:LinkButton ID="lbtnRefresh" runat="server" OnClick="lbtnRefresh_Click" 
                                        ToolTip="Refresh">
                                            <img style="border: 0px; text-align:center; vertical-align: middle;" src="../../../../../../Images/Toolbar/search16.png" />
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                    </tr>
                </table>
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
                                                <asp:CheckBox ID="defaultChkBox" runat="server" >
                                                </asp:CheckBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="NursingDiagnosaName" HeaderText="Diagnosis And Etiology" SortExpression="NursingDiagnosaName"
                                            UniqueName="NursingDiagnosaName" Visible="false">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Diagnosis And Etiology" UniqueName="NursingDiagnosaName">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txtNursingDiagnosaName" Width="95%" runat="server" Text='<%#Eval("NursingDiagnosaNameEdited").ToString()%>'></telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
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
