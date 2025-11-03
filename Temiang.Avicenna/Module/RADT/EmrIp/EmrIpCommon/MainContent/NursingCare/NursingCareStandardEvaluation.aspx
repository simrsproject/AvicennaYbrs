<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NursingCareStandardEvaluation.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare.NursingCareStandardEvaluation" %>
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
                        $find('<%=txtS.ClientID %>').set_value(decodeURI(oWndX.argument.dataDS));
                        $find('<%=txtO.ClientID %>').set_value(decodeURI(oWndX.argument.dataDO));
                    } 
                }
                oWndX = null;
                //alert("post back");
            }
        </script>
        <style type="text/css">
            /* Styles here */
            
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
                <telerik:GridBoundColumn DataField="ExecuteDateTime" HeaderText="Date"
                    DataFormatString="{0:MM/dd/yyyy HH:mm}" DataType="System.DateTime" 
                    SortExpression="ExecuteDateTime" UniqueName="ExecuteDateTime">
                    <HeaderStyle HorizontalAlign="Left" Width="150" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="S" HeaderText="S"
                    SortExpression="S" UniqueName="S">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="O" HeaderText="O"
                    SortExpression="O" UniqueName="O">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="A" HeaderText="A"
                    SortExpression="A" UniqueName="A">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="P" HeaderText="P"
                    SortExpression="P" UniqueName="P">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
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
    <table runat="server" id="tblEntryEval" width="100%">
        <tr>
            <td style="width:40%;" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Date Time"></asp:Label>
                        </td>
                        <td class="entry">
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
                            <asp:Label ID="lblNursingDiagnosaID" runat="server" Text="S"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:HiddenField ID="hfID" runat="server" />
                            <asp:HiddenField ID="hfTmpNursingDiagnosaID" runat="server" />
                            <telerik:RadTextBox ID="txtS" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchNote" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    ToolTip="Pick from nursing notes" OnClientClick="openNurseNoteDialog();return false;" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="O"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtO" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Objective (O) required."
                                ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtO" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="A"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtA" runat="server" Width="300px" TextMode="MultiLine" />
                            <asp:RadioButtonList runat="server" ID="rblA" RepeatDirection="Vertical">
                                <asp:ListItem Text="sudah teratasi" Value="sudah teratasi" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="teratasi sebagian" Value="teratasi sebagian"></asp:ListItem>
                                <asp:ListItem Text="belum teratasi" Value="belum teratasi"></asp:ListItem>
                                <asp:ListItem Text="tidak terjadi" Value="tidak terjadi"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Assessment (A) required."
                                ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtA" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="P"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboP" runat="server" Width="304px"
                                AutoPostBack="true" OnSelectedIndexChanged="cboP_SelectedIndexChanged"
                            />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Planning (P) required."
                                ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="cboP" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="PPA Ins"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPpaInstruction" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:60%;"  valign="top">
                <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
                    Orientation="HorizontalTop">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Outcomes" PageViewID="pgNoc"
                            Selected="True">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Interventions" PageViewID="pgNic">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
                    <telerik:RadPageView ID="pgNoc" runat="server" Selected="true">
                        <div style="width: 100%; height: 500px; overflow: scroll">
                            <telerik:RadGrid ID="gridListTarget" runat="server" AutoGenerateColumns="false" GridLines="None"
                                OnNeedDataSource="gridListTarget_NeedDataSource" OnItemDataBound="gridListTarget_ItemDataBound">
                                <MasterTableView ClientDataKeyNames="ID" HierarchyLoadMode="ServerBind"
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
                                                <telerik:GridGroupByField FieldName="NocCode" HeaderText="SLKI " HeaderValueSeparator=" ">
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
                                        <telerik:GridTemplateColumn HeaderText="Kriteria" UniqueName="NursingDiagnosaNameEdited">
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
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgNic" runat="server">
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
                                                    Width="100%" MaxLength="255" TextMode="MultiLine" />
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
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
    </table><br /><br />
</asp:Content>
