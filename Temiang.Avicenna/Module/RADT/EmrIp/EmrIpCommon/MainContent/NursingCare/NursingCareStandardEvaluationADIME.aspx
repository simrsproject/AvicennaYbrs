<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NursingCareStandardEvaluationADIME.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare.NursingCareStandardEvaluationADIME" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="NursingDiagnosaTransDT" />
    <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="NursingDiagnosaTransDT"
        ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">
            function openNurseNoteDialog() {
                var oWndX = $find("<%= winDlg.ClientID %>");
                oWndX.SetUrl("NursingCareStandardPickImplementation.aspx?regno=<%= Request.QueryString["regno"] %>");
                oWndX.show();
                oWndX.maximize();
            }

            function win_OnClientClose(oWndX, args) {
                //alert(oWndX.argument.dataDS);
                if (oWndX.argument == undefined || oWndX.argument == null) {
                } else {
                    if (oWndX.argument.result == undefined || oWndX.argument.result == null) {
                    } else if (oWndX.argument.result == 'OK') {
                        var A = decodeURI(oWndX.argument.dataDS) + ' ' + decodeURI(oWndX.argument.dataDO);
                        $find('<%=txtA.ClientID %>').set_value(A);
                    } 
                }
                oWndX = null;
                //alert("post back");
            }

            function EvalShowMoreClick(evalID) {
                $('.linichide' + evalID).each(function (i, obj) {
                    $(this).show(500);
                });
                $('.linicshow' + evalID).each(function (i, obj) {
                    $(this).hide();
                });
            }
        </script>
        <style type="text/css">
            /* Styles here */
            .EvalHistCell {
                padding-right:3px;
                vertical-align:top;
            }

            .linicnoc{
                margin-left:-27px;
            }
            .linichide {
                display:none;
            }
            .linicshow {
                display:normal;
            }
        </style>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindow ID="winDlg" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false" OnClientClose="win_OnClientClose"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboP">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridListRencana" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="cboP" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <table class="info success" width="100%">
            <tr>
                <td>
                    <span style="font-size: 1.5em;"><%= FullDiagnosaName %></span>
                </td>
            </tr>
        </table>
    </telerik:RadCodeBlock>
    
    <table runat="server" id="tblEntryEval" width="100%">
        <tr>
            <td style="width:50%;" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Date Time"></asp:Label>
                        </td>
                        <td class="">
                            <telerik:RadDateTimePicker ID="txtDateTimeImplementation" runat="server" AutoPostBackControl="None">
                                <DateInput ID="DateInput1" runat="server"
		                            DisplayDateFormat="dd/MM/yyyy HH:mm"
		                            DateFormat="dd/MM/yyyy HH:mm">
	                            </DateInput>
	                            <TimeView ID="TimeView1" runat="server" TimeFormat="HH:mm"></TimeView>
                            </telerik:RadDateTimePicker>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvDateTimeImplementation" runat="server" ErrorMessage="Nursing implementation date time required."
                                ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtDateTimeImplementation" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNursingDiagnosaID" runat="server" Text="A"></asp:Label>
                        </td>
                        <td class="">
                            <asp:HiddenField ID="hfID" runat="server" />
                            <asp:HiddenField ID="hfTmpNursingDiagnosaID" runat="server" />
                            <telerik:RadTextBox ID="txtA" runat="server" Width="100%" Height="40px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchNote" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    ToolTip="Pick from nursing notes" OnClientClick="openNurseNoteDialog();return false;" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Assessment required."
                                ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtA" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="D"></asp:Label>
                        </td>
                        <td class="">
                            <telerik:RadTextBox ID="txtD" runat="server" Width="100%" Height="40px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Diagnosis required."
                                ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtD" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="I"></asp:Label>
                        </td>
                        <td class="">
                            <div style="width: 100%; height: 500px; overflow: scroll">
                                <telerik:RadGrid ID="gridListRencana" runat="server" AutoGenerateColumns="false"
                                    GridLines="None" OnNeedDataSource="gridListRencana_NeedDataSource" OnItemDataBound="gridListRencana_ItemDataBound" >
                                    <MasterTableView ClientDataKeyNames="NursingDiagnosaID"
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
                                            <telerik:GridTemplateColumn HeaderText="Select" UniqueName="ChkBox">
                                                <HeaderStyle HorizontalAlign="Left" Width="70px" />
                                                <ItemTemplate>
                                                    <div class="onoffswitch" id="divSelected" runat="server" >
                                                    <input type='checkbox' name='onoffswitch' class='onoffswitch-checkbox' 
                                                        id='chkSwitch' 
                                                        runat='server' checked='<%#(bool)Eval("Status")%>' />
                                                        <label id="lblSwitch" runat="server" class="onoffswitch-label">
                                                            <span class="onoffswitch-inner"></span>
                                                            <span class="onoffswitch-switch"></span>
                                                        </label>
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Interventions" UniqueName="NursingDiagnosaNameEdited">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="txtNursingDiagnosaName" runat="server" Text='<%#Eval("NursingDiagnosaNameEdited")%>'
                                                        Width="100%" Height="40px" MaxLength="255" TextMode="MultiLine" />
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
                        <td width="20px">
                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="M"></asp:Label>
                        </td>
                        <td class="">
                            <telerik:RadTextBox ID="txtM" runat="server" Width="100%" Height="40px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Monitoring required."
                                ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtM" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="E"></asp:Label>
                        </td>
                        <td class="">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td><telerik:RadTextBox ID="txtE" runat="server" Width="100%" Height="40px" TextMode="MultiLine" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboP" runat="server" Width="304px" 
                                            AutoPostBack="true" OnSelectedIndexChanged="cboP_SelectedIndexChanged"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Evaluation required."
                                ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtE" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Evaluation required."
                                ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="cboP" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="PPA Ins"></asp:Label>
                        </td>
                        <td class="">
                            <telerik:RadTextBox ID="txtPpaInstruction" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:50%;"  valign="top">
                <telerik:RadGrid ID="gridListEvaluasi" runat="server" AutoGenerateColumns="false"
                    GridLines="None" OnNeedDataSource="gridListEvaluasi_NeedDataSource" 
                    OnItemDataBound="gridListEvaluasi_ItemDataBound"
                    OnDeleteCommand="gridListEvaluasi_DeleteCommand">
                    <MasterTableView ClientDataKeyNames="NursingDiagnosaID" HierarchyLoadMode="ServerBind"
                        HierarchyDefaultExpanded="true" ExpandCollapseColumn-Display="false" DataKeyNames="NursingDiagnosaID"
                        GroupLoadMode="Client" Name="MainTable" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="35px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="EvalHistCell">Date</td>
                                            <td class="EvalHistCell">:</td>
                                            <td><%# (Eval("ExecuteDateTime") as DateTime?).Value.ToString("MM/dd/yyyy HH:mm") %> (m/d/y)</td>
                                        </tr>
                                        <tr>
                                            <td class="EvalHistCell">A</td>
                                            <td class="EvalHistCell">:</td>
                                            <td><%# Eval("S") %></td>
                                        </tr>
                                        <tr>
                                            <td class="EvalHistCell">D</td>
                                            <td class="EvalHistCell">:</td>
                                            <td><%# Eval("O") %></td>
                                        </tr>
                                        <tr>
                                            <td class="EvalHistCell">I</td>
                                            <td class="EvalHistCell">:</td>
                                            <td><%# Eval("A") %></td>
                                        </tr>
                                        <tr>
                                            <td class="EvalHistCell">M</td>
                                            <td class="EvalHistCell">:</td>
                                            <td><%# Eval("P") %></td>
                                        </tr>
                                        <tr>
                                            <td class="EvalHistCell">E</td>
                                            <td class="EvalHistCell">:</td>
                                            <td><%# Eval("Info5") %></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <EditFormSettings 
                            UserControlName="NursingCareStandardDetailEvaluation.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="NursingCareStandardDetailEvaluationCommand">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table><br /><br />
</asp:Content>
