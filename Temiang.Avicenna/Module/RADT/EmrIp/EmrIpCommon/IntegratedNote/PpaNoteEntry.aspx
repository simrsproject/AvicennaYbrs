<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PpaNoteEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.PpaNoteEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="NursingDiagnosaTransDT" />
    <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="NursingDiagnosaTransDT"
        ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">
            function tbInputType_ClientClicked(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case "close":
                        Close();
                        args.set_cancel(true);
                        break;
                }
            }

            function cboParamedic_ClientItemsRequesting(sender, eventArgs) {
                var context = eventArgs.get_context();
                context["IsLoadAllPhysician"] = <%= Temiang.Avicenna.Common.AppSession.Parameter.IsAllPhysicianOnSbar.ToString().ToLower() %> && document.getElementById("<%=hfLastIType.ClientID%>").value == "S B A R";
                context["RegNo"] = '<%=this.Request.QueryString["regno"]%>';
                context["RegType"] = '<%=this.Request.QueryString["rt"]%>';
            }

        </script>
        <style type="text/css">
            /* Styles here */
        </style>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="tbInputType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlEntry" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="hfLastIType" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_LabelInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="gridListImplementasiDiagnosa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlEntry" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="hfLastIType" />
                    <telerik:AjaxUpdatedControl ControlID="hfIDImplementation" />
                    <telerik:AjaxUpdatedControl ControlID="txtDateTimeImplementation" />
                    <telerik:AjaxUpdatedControl ControlID="txtImplementationText" />
                    <telerik:AjaxUpdatedControl ControlID="cboTemplate" />
                    <telerik:AjaxUpdatedControl ControlID="txtCustomRespond" />
                    <telerik:AjaxUpdatedControl ControlID="hfRelatedPHRNo" />
                    <telerik:AjaxUpdatedControl ControlID="grdQuestionRespond" />
                    <telerik:AjaxUpdatedControl ControlID="txtS" />
                    <telerik:AjaxUpdatedControl ControlID="txtO" />
                    <telerik:AjaxUpdatedControl ControlID="txtA" />
                    <telerik:AjaxUpdatedControl ControlID="txtP" />
                    <telerik:AjaxUpdatedControl ControlID="txtInfo5" />
                    <telerik:AjaxUpdatedControl ControlID="txtPpaInstruction" />
                    <telerik:AjaxUpdatedControl ControlID="cboParamedic" />
                    <telerik:AjaxUpdatedControl ControlID="txtSubmitBy" />
                    <telerik:AjaxUpdatedControl ControlID="txtReceiveBy" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_LabelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="txtReceiveBySbar" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCustomNic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlEntry" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="hfLastIType" />
                    <telerik:AjaxUpdatedControl ControlID="hfIDImplementation" />
                    <telerik:AjaxUpdatedControl ControlID="txtDateTimeImplementation" />
                    <telerik:AjaxUpdatedControl ControlID="txtImplementationText" />
                    <telerik:AjaxUpdatedControl ControlID="cboTemplate" />
                    <telerik:AjaxUpdatedControl ControlID="txtCustomRespond" />
                    <telerik:AjaxUpdatedControl ControlID="hfRelatedPHRNo" />
                    <telerik:AjaxUpdatedControl ControlID="grdQuestionRespond" />
                    <telerik:AjaxUpdatedControl ControlID="txtS" />
                    <telerik:AjaxUpdatedControl ControlID="txtO" />
                    <telerik:AjaxUpdatedControl ControlID="txtA" />
                    <telerik:AjaxUpdatedControl ControlID="txtP" />
                    <telerik:AjaxUpdatedControl ControlID="txtInfo5" />
                    <telerik:AjaxUpdatedControl ControlID="txtPpaInstruction" />
                    <telerik:AjaxUpdatedControl ControlID="cboParamedic" />
                    <telerik:AjaxUpdatedControl ControlID="txtSubmitBy" />
                    <telerik:AjaxUpdatedControl ControlID="txtReceiveBy" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_LabelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="txtReceiveBySbar" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlEntry" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="hfLastIType" />
                    <telerik:AjaxUpdatedControl ControlID="hfIDImplementation" />
                    <telerik:AjaxUpdatedControl ControlID="txtDateTimeImplementation" />
                    <telerik:AjaxUpdatedControl ControlID="txtImplementationText" />
                    <telerik:AjaxUpdatedControl ControlID="cboTemplate" />
                    <telerik:AjaxUpdatedControl ControlID="txtCustomRespond" />
                    <telerik:AjaxUpdatedControl ControlID="hfRelatedPHRNo" />
                    <telerik:AjaxUpdatedControl ControlID="grdQuestionRespond" />
                    <telerik:AjaxUpdatedControl ControlID="txtS" />
                    <telerik:AjaxUpdatedControl ControlID="txtO" />
                    <telerik:AjaxUpdatedControl ControlID="txtA" />
                    <telerik:AjaxUpdatedControl ControlID="txtP" />
                    <telerik:AjaxUpdatedControl ControlID="txtInfo5" />
                    <telerik:AjaxUpdatedControl ControlID="txtPpaInstruction" />
                    <telerik:AjaxUpdatedControl ControlID="cboParamedic" />
                    <telerik:AjaxUpdatedControl ControlID="txtSubmitBy" />
                    <telerik:AjaxUpdatedControl ControlID="txtReceiveBy" />
                    <telerik:AjaxUpdatedControl ControlID="gridListImplementasi" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_LabelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="txtReceiveBySbar" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboTemplate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdQuestionRespond" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtImplementationText" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="gridListImplementasi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlEntry" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="hfLastIType" />
                    <telerik:AjaxUpdatedControl ControlID="hfIDImplementation" />
                    <telerik:AjaxUpdatedControl ControlID="txtDateTimeImplementation" />
                    <telerik:AjaxUpdatedControl ControlID="txtImplementationText" />
                    <telerik:AjaxUpdatedControl ControlID="cboTemplate" />
                    <telerik:AjaxUpdatedControl ControlID="txtCustomRespond" />
                    <telerik:AjaxUpdatedControl ControlID="hfRelatedPHRNo" />
                    <telerik:AjaxUpdatedControl ControlID="grdQuestionRespond" />
                    <telerik:AjaxUpdatedControl ControlID="txtS" />
                    <telerik:AjaxUpdatedControl ControlID="txtO" />
                    <telerik:AjaxUpdatedControl ControlID="txtA" />
                    <telerik:AjaxUpdatedControl ControlID="txtP" />
                    <telerik:AjaxUpdatedControl ControlID="txtInfo5" />
                    <telerik:AjaxUpdatedControl ControlID="txtPpaInstruction" />
                    <telerik:AjaxUpdatedControl ControlID="cboParamedic" />
                    <telerik:AjaxUpdatedControl ControlID="txtSubmitBy" />
                    <telerik:AjaxUpdatedControl ControlID="txtReceiveBy" />
                    <telerik:AjaxUpdatedControl ControlID="gridListImplementasi" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_LabelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="txtReceiveBySbar" />
                    <telerik:AjaxUpdatedControl ControlID="tbInputType" />
                    
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="width: 25%; vertical-align: top;">
                <telerik:RadGrid ID="gridListImplementasiDiagnosa" runat="server" AutoGenerateColumns="false"
                    GridLines="None" OnNeedDataSource="gridListImplementasiDiagnosa_NeedDataSource"
                    OnItemDataBound="gridListImplementasiDiagnosa_ItemDataBound"
                    OnItemCommand="gridListImplementasiDiagnosa_ItemCommand">
                    <MasterTableView ClientDataKeyNames="NursingDiagnosaID,ID" DataKeyNames="NursingDiagnosaID,ID"
                        GroupLoadMode="Client">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="DiagName" HeaderText=" " HeaderValueSeparator=" "></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="DiagID" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Id" HeaderText="Id" SortExpression="Id"
                                Visible="false" UniqueName="Id">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NursingDiagnosaID" HeaderText="Id" SortExpression="NursingDiagnosaID"
                                Visible="false" UniqueName="NursingDiagnosaID">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NursingDiagnosaNameEdited" HeaderText="NIC/SIKI" SortExpression="NursingDiagnosaNameEdited"
                                UniqueName="NursingDiagnosaNameEdited">
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
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td style="width: 75%; vertical-align: top;">
                <div id="divEntryArea">
                    <telerik:RadToolBar runat="server" ID="tbInputType" Width="100%"
                        OnClientButtonClicked="tbInputType_ClientClicked" OnButtonClick="tbInputType_ButtonClick">
                        <Items>
                            <telerik:RadToolBarButton runat="server" Text="Kegiatan Rutin" Value="Routine" ImageUrl="~/Images/Toolbar/insert16.png"
                                HoveredImageUrl="~/Images/Toolbar/insert16.png" DisabledImageUrl="~/Images/Toolbar/insert16_d.png" />
                            <telerik:RadToolBarButton runat="server" Text="S O A P" Value="SOAP"
                                ImageUrl="~/Images/Toolbar/insert16.png" />
                            <telerik:RadToolBarButton runat="server" Text="S B A R" Value="SBAR"
                                ImageUrl="~/Images/Toolbar/insert16.png" />
                            <telerik:RadToolBarButton runat="server" Text="ADIME" Value="ADIME"
                                ImageUrl="~/Images/Toolbar/insert16.png" />
                            <telerik:RadToolBarButton runat="server" Text="Handover Patient (pindah ke tombol S O A P)" Value="HandOver"
                                ImageUrl="~/Images/Toolbar/insert16_d.png" Enabled="false" />
                            <telerik:RadToolBarButton runat="server" Text="Close" Value="close" ImageUrl="~/Images/Toolbar/close16.png" />
                        </Items>
                    </telerik:RadToolBar>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%" valign="top">
                                <telerik:RadAjaxPanel ID="pnlEntry" runat="server">

                                    <asp:Panel ID="pnlInfoStatus" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
                                        BorderColor="#FFC080" BorderStyle="Solid">
                                        <table width="100%">
                                            <tr>
                                                <td width="10px" valign="top">
                                                    <asp:Image ID="Image2" ImageUrl="~/Images/boundleft.gif" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblInfoStatus" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <table width="100%" id="tblEntry" runat="server">
                                        <tr>
                                            <td class="label">
                                                <asp:Label ID="Label1" runat="server" Text="Date Time"></asp:Label>
                                                <asp:HiddenField ID="hfTransNo" runat="server" />
                                                <asp:HiddenField ID="hfNicInt" runat="server" />
                                                <asp:HiddenField ID="hfNicCode" runat="server" />
                                                <asp:HiddenField ID="hfIDImplementation" runat="server" />
                                            </td>
                                            <td>
                                                <telerik:RadDateTimePicker ID="txtDateTimeImplementation" runat="server" AutoPostBackControl="None">
                                                    <DateInput ID="DateInput1" runat="server"
                                                        DisplayDateFormat="dd/MM/yyyy HH:mm"
                                                        DateFormat="dd/MM/yyyy HH:mm">
                                                    </DateInput>
                                                    <TimeView runat="server" TimeFormat="HH:mm"></TimeView>
                                                </telerik:RadDateTimePicker>
                                            </td>
                                            <td width="25px">
                                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="Date required."
                                                    ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtDateTimeImplementation" SetFocusOnError="True"
                                                    Width="100%">
                                                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr id="trImp" runat="server">
                                            <td class="label">
                                                <asp:Label ID="lblNursingDiagnosaID" runat="server" Text="Implementation"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtImplementationText" runat="server" Width="400px" MaxLength="500" TextMode="MultiLine" />
                                            </td>
                                            <td width="25px">
                                                <asp:RequiredFieldValidator ID="rfvImplementText" runat="server" ErrorMessage="Nursing implementation required."
                                                    ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtImplementationText" SetFocusOnError="True"
                                                    Width="100%">
                                                    <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr id="trRespCbo" runat="server">
                                            <td class="label">
                                                <asp:Label ID="Label3" runat="server" Text="Respond / Result Template"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox runat="server" ID="cboTemplate" Width="400px" EnableLoadOnDemand="true"
                                                    HighlightTemplatedItems="true" AutoPostBack="true" MarkFirstMatch="true"
                                                    OnItemDataBound="cboTemplate_ItemDataBound" OnSelectedIndexChanged="cboTemplate_SelectedIndexChanged">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "TemplateName") %>
                                                    &nbsp;<br />
                                                        <%# DataBinder.Eval(Container.DataItem, "TemplateText")%>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="25px"></td>
                                        </tr>
                                        <tr id="trRespTxt" runat="server">
                                            <td class="label">
                                                <asp:Label ID="Label2" runat="server" Text="Respond / Result"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCustomRespond" runat="server" Width="400px" MaxLength="500" TextMode="MultiLine" Height="70px" />
                                            </td>
                                            <td width="25px">&nbsp;

                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:HiddenField ID="hfRelatedPHRNo" runat="server" />
                                                <telerik:RadGrid ID="grdQuestionRespond" runat="server" AutoGenerateColumns="false"
                                                    GridLines="None" OnNeedDataSource="grdQuestionRespond_NeedDataSource"
                                                    OnItemCreated="grdQuestionRespond_ItemCreated"
                                                    OnItemDataBound="grdQuestionRespond_ItemDataBound"
                                                    Width="100%">
                                                    <MasterTableView ClientDataKeyNames="QuestionID" DataKeyNames="QuestionID">
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="QuestionText" HeaderText="Assessment" HeaderStyle-Width="150px"
                                                                SortExpression="QuestionText" UniqueName="QuestionText">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Answer" UniqueName="AnswerObj">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <FilterMenu>
                                                    </FilterMenu>
                                                    <ClientSettings EnableRowHoverStyle="false">
                                                        <Resizing AllowColumnResize="true" />
                                                        <Selecting AllowRowSelect="false" />
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </td>
                                            <td width="25px">&nbsp;
                                            </td>
                                        </tr>
                                        <tr id="trInfo1" runat="server">
                                            <td class="label" valign="top">
                                                <asp:Label ID="lblS" runat="server" Text="Situation (S)"></asp:Label>
                                                <asp:HiddenField ID="hfLastIType" runat="server" />
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtS" runat="server" Width="400px"
                                                    TextMode="MultiLine" Height="120px" />
                                            </td>
                                            <td width="25px">&nbsp;
                                            </td>
                                        </tr>
                                        <tr id="trInfo2" runat="server">
                                            <td class="label" valign="top">
                                                <asp:Label ID="lblO" runat="server" Text="Background (B)"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtO" runat="server" Width="400px"
                                                    TextMode="MultiLine" Height="90px" />
                                            </td>
                                            <td width="25px">&nbsp;
                                            </td>
                                        </tr>
                                        <tr id="trInfo3" runat="server">
                                            <td class="label" valign="top">
                                                <asp:Label ID="lblA" runat="server" Text="Assessment (A)"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtA" runat="server" Width="400px"
                                                    TextMode="MultiLine" Height="90px" />
                                            </td>
                                            <td width="25px">&nbsp;
                                            </td>
                                        </tr>
                                        <tr id="trInfo4" runat="server">
                                            <td class="label" valign="top">
                                                <asp:Label ID="lblP" runat="server" Text="Recommendation (R)"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtP" runat="server" Width="400px"
                                                    TextMode="MultiLine" Height="90px" />
                                            </td>
                                            <td width="25px">&nbsp;
                                            </td>
                                        </tr>
                                        <tr id="trtPpaInstruction" runat="server">
                                            <td class="label" valign="top">
                                                <asp:Label ID="lblPpaInstruction" runat="server" Text="PPA Instruction /<br /> Intervention (I)"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtPpaInstruction" runat="server" Width="400px"
                                                    TextMode="MultiLine" Height="90px" />
                                            </td>
                                            <td width="25px">&nbsp;
                                            </td>
                                        </tr>
                                        <tr id="trInfo5" runat="server">
                                            <td class="label" valign="top">
                                                <asp:Label ID="lblInfo5" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtInfo5" runat="server" Width="400px"
                                                    TextMode="MultiLine" Height="90px" />
                                            </td>
                                            <td width="25px">&nbsp;
                                            </td>
                                        </tr>
                                        <tr id="trReceiveBy2" runat="server">
                                            <td class="label" valign="middle">
                                                <asp:Label ID="lblRB2" runat="server" Text="Receive By"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtReceiveBySbar" runat="server" Width="400px"
                                                    TextMode="MultiLine" Height="25px" />
                                            </td>
                                            <td width="25px">&nbsp;
                                            </td>
                                        </tr>
                                        <tr id="trPar" runat="server">
                                            <td class="label" valign="middle">
                                                <asp:Label ID="Label6" runat="server" Text="Physician"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox runat="server" ID="cboParamedic" Width="400px" EmptyMessage="Select Physician"
                                                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                                                    OnClientItemsRequesting="cboParamedic_ClientItemsRequesting">
                                                    <WebServiceSettings Method="ParamedicSbarPpaNotes" Path="~/WebService/ComboBoxDataService.asmx" />
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="25px">&nbsp;
                                            </td>
                                        </tr>
                                        <%-- <tr id="trPar" runat="server">
                                            <td class="label" valign="top">
                                                <asp:Label ID="Label6" runat="server" Text="Physician"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox runat="server" ID="cboParamedic" Width="400px" AllowCustomText="true"
                                                    Filter="Contains">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td width="25px">&nbsp;
                                            </td>
                                        </tr> --%>
                                    </table>
                                    <fieldset runat="server" id="fsHandOver">
                                        <legend>Hand Over By</legend>
                                        <table width="100%">
                                            <tr id="trSubmitBy" runat="server">
                                                <td class="label" valign="top">
                                                    <asp:Label ID="Label4" runat="server" Text="Submit By"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtSubmitBy" runat="server" Width="400px"
                                                        TextMode="MultiLine" Height="40px" />
                                                </td>
                                                <td width="25px">&nbsp;
                                                </td>
                                            </tr>
                                            <tr id="trReceiveBy" runat="server">
                                                <td class="label" valign="top">
                                                    <asp:Label ID="Label5" runat="server" Text="Receive By"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtReceiveBy" runat="server" Width="400px"
                                                        TextMode="MultiLine" Height="40px" />
                                                </td>
                                                <td width="25px">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <table width="100%">
                                        <tr>
                                            <td></td>
                                            <td align="right">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_click" />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </telerik:RadAjaxPanel>
                            </td>
                            <td style="width: 50%" valign="top">
                                <telerik:RadGrid ID="grdCustomNic" runat="server" AutoGenerateColumns="false"
                                    GridLines="None" OnNeedDataSource="grdCustomNic_NeedDataSource"
                                    OnItemCommand="grdCustomNic_ItemCommand">
                                    <MasterTableView ClientDataKeyNames="NursingDiagnosaID" DataKeyNames="NursingDiagnosaID">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="NursingDiagnosaID" HeaderText="Id" SortExpression="NursingDiagnosaID"
                                                Visible="false" UniqueName="NursingDiagnosaID">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NursingDiagnosaName" HeaderText="Custom Intervention Templates" SortExpression="NursingDiagnosaName"
                                                UniqueName="NursingDiagnosaName">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <FilterMenu>
                                    </FilterMenu>
                                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                        <Resizing AllowColumnResize="True" />
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="200px" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>
                <telerik:RadGrid ID="gridListImplementasi" runat="server" AutoGenerateColumns="false"
                    GridLines="None" OnNeedDataSource="gridListImplementasi_OnNeedDataSource"
                    OnDeleteCommand="gridListImplementasi_DeleteCommand"
                    OnItemDataBound="gridListImplementasi_ItemDataBound"
                    OnItemCommand="gridListImplementasi_ItemCommand"
                    AllowPaging="True" PageSize="10" AllowSorting="False" AllowCustomPaging="true">
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    <MasterTableView ClientDataKeyNames="ID" DataKeyNames="ID"
                        InsertItemPageIndexAction="ShowItemOnCurrentPage">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="customEdit">
                                <ItemTemplate>
                                    <asp:ImageButton CommandName="editItem" Visible="<%#IsEditable%>" runat="server" ImageUrl="~/Images/Toolbar/edit16.png"></asp:ImageButton>
                                    <asp:ImageButton CommandName="editItem" Visible="<%#!IsEditable%>" runat="server" ImageUrl="~/Images/Toolbar/views16.png"></asp:ImageButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Id" HeaderText="Id" SortExpression="Id"
                                Visible="false" UniqueName="Id">
                            </telerik:GridBoundColumn>
                            <%--                            <telerik:GridBoundColumn DataField="ExecuteDateTime" HeaderText="Execution Date"
                                DataFormatString="{0:MM/dd/yyyy HH:mm}" DataType="System.DateTime"
                                SortExpression="ExecuteDateTime" UniqueName="ExecuteDateTime">
                                <HeaderStyle HorizontalAlign="Left" Width="130" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridDateTimeColumn DataField="ExecuteDateTime" HeaderText="Execution Date" HeaderStyle-Width="130px"></telerik:GridDateTimeColumn>
                            <telerik:GridTemplateColumn HeaderText="Implemetation" UniqueName="GetFullImplementationName">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <%# GetFullImplementationNameFormatted(((string)Eval("NursingDiagnosaName")), (string)Eval("S"),(string)Eval("O"),(string)Eval("A"),(string)Eval("P"),Eval("PpaInstruction"),Eval("Info5"),Eval("SubmitBy"),Eval("ReceiveBy")).Replace("\n", "<br/>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Respond / Result" UniqueName="Respond">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <%# (Eval("Respond")).ToString().Replace("\n", "<br/>") + " " + (Eval("Respond2")).ToString().Replace("\n", "<br/>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="NursingDiagnosaParentID" HeaderText="Parent"
                                SortExpression="NursingDiagnosaParentID" UniqueName="NursingDiagnosaParentID"
                                Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TmpNursingDiagnosaID" HeaderText="Id" SortExpression="TmpNursingDiagnosaID"
                                Visible="false" UniqueName="TmpNursingDiagnosaID">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RefToUserName" HeaderText="User" SortExpression="RefToUserName"
                                UniqueName="RefToUserName">
                                <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock2">
        <script type="text/javascript">
            document.getElementById("divEntryArea").style.display = "<%=IsUserAddAble?"block":"none" %>";
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
