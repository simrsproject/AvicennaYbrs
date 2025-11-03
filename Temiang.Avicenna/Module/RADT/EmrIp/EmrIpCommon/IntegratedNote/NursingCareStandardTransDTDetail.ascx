<%@ Control Language="C#" AutoEventWireup="true" Codebehind="NursingCareStandardTransDTDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Emr.NursingCareStandardTransDTDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="NursingDiagnosaTransDT" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="NursingDiagnosaTransDT"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboNursingDiagnosa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboNursingDiagnosa" />
                    <telerik:AjaxUpdatedControl ControlID="pnlRespond" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSBAR" />
                </UpdatedControls>  
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Date Time"></asp:Label>
            </td>
            <td>
                <telerik:RadDateTimePicker ID="txtDateTimeImplementation" runat="server" AutoPostBackControl="None">
                    <DateInput ID="DateInput1" runat="server"
		                DisplayDateFormat="dd/MM/yyyy HH:mm"
		                DateFormat="dd/MM/yyyy HH:mm"
		                >
	                </DateInput>
	                <TimeView TimeFormat="HH:mm"></TimeView>
                </telerik:RadDateTimePicker>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvDateTimeImplementation" runat="server" ErrorMessage="Nursing implementation date time required."
                    ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtDateTimeImplementation" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblNursingDiagnosaID" runat="server" Text="Implementation"></asp:Label>
            </td>
            <td>
                <asp:HiddenField ID="hfID" runat="server" />
                        <asp:HiddenField ID="hfTmpNursingDiagnosaID" runat="server" />
                        <telerik:RadComboBox runat="server" ID="cboNursingDiagnosa" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="false" AutoPostBack="true" AllowCustomText="true"
                            OnItemDataBound="cboNursingDiagnosa_ItemDataBound"
                            OnItemsRequested="cboNursingDiagnosa_ItemsRequested" 
                            OnSelectedIndexChanged="cboNursingDiagnosa_SelectedIndexChanged" >
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "NursingDiagnosaName") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Nursing implementation required."
                            ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="cboNursingDiagnosa" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
            </td>
        </tr>
        
    </table>
    <asp:Panel runat="server" ID="pnlRespond">
        <table width="100%">
            <tr>
                <td class="label">
                    <asp:Label ID="Label3" runat="server" Text="Respond / Result Template"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cboTemplate" Width="300px" EnableLoadOnDemand="true"
                        HighlightTemplatedItems="true" AutoPostBack="true" MarkFirstMatch="true" 
                        OnItemDataBound="cboTemplate_ItemDataBound" OnSelectedIndexChanged="cboTemplate_SelectedIndexChanged">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "TemplateName") %>
                            &nbsp;<br /><%# DataBinder.Eval(Container.DataItem, "TemplateText")%>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nursing implementation date time required."
                        ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtDateTimeImplementation" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label2" runat="server" Text="Respond / Result"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtCustomRespond" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" Height="70px" />
                </td>
                <td width="25px">&nbsp;

                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td><asp:HiddenField ID="hrRelatedPHRNo" runat="server" />
                    <telerik:RadGrid ID="grdQuestionRespond" runat="server" AutoGenerateColumns="false" 
                        GridLines="None" OnNeedDataSource="grdQuestionRespond_NeedDataSource"
                        OnItemCreated="grdQuestionRespond_ItemCreated"
                        OnItemDataBound="grdQuestionRespond_ItemDataBound" >
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
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
                <td width="25px">&nbsp;
                    
                </td>
            </tr>
            <tr>
                <td colspan="3">

                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSBAR">
        <table width="100%">
            <tr>
                <td class="label" valign="top">
                    <asp:Label ID="lblS" runat="server" Text="Situation (S)"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtS" runat="server" Width="300px" 
                    TextMode="MultiLine" Height="40px"  />
                </td>
            </tr>
            <tr>
                <td class="label" valign="top">
                    <asp:Label ID="lblO" runat="server" Text="Background (B)"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtO" runat="server" Width="300px"  
                    TextMode="MultiLine" Height="40px" />
                </td>
            </tr>
            <tr runat="server" id="trInfo3">
                <td class="label" valign="top">
                    <asp:Label ID="lblA" runat="server" Text="Assessment (A)"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtA" runat="server" Width="300px"  
                    TextMode="MultiLine" Height="40px" />
                </td>
            </tr>
            <tr runat="server" id="trInfo4">
                <td class="label" valign="top">
                    <asp:Label ID="lblP" runat="server" Text="Recommendation (R)"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtP" runat="server" Width="300px" 
                    TextMode="MultiLine" Height="100px" />
                </td>
            </tr>
            <tr runat="server" id="trInfo5">
                <td class="label" valign="top">
                    <asp:Label ID="lblInfo5" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtInfo5" runat="server" Width="300px" 
                                        TextMode="MultiLine" Height="100px" />
                </td>
            </tr>
            <tr runat="server" id="trtPpaInstruction">
                <td class="label" valign="top">
                    <asp:Label ID="lblPpaInstruction" runat="server" Text="PPA Instruction"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtPpaInstruction" runat="server" Width="300px" 
                                        TextMode="MultiLine" Height="100px" />
                </td>
            </tr>
            <tr runat="server" id="trSubmitBy">
                <td class="label" valign="top">
                    <asp:Label ID="Label4" runat="server" Text="Submit By"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtSubmitBy" runat="server" Width="300px" 
                                        TextMode="MultiLine" Height="100px" />
                </td>
            </tr>
            <tr runat="server" id="trReceiveBy">
                <td class="label" valign="top">
                    <asp:Label ID="Label5" runat="server" Text="Receive By"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtReceiveBy" runat="server" Width="300px" 
                                        TextMode="MultiLine" Height="100px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="450px">
        <tr>
            <td align="right" colspan="2" style="height: 26px">
                <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="NursingDiagnosaTransDT"
                    Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                    ValidationGroup="NursingDiagnosaTransDT" Visible='<%# DataItem is GridInsertionObject %>'>
                </asp:Button>
                &nbsp;
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                    CommandName="Cancel"></asp:Button>

            </td>
        </tr>
    </table>

