<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EpisodeProcDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.EpisodeProcDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEpisodeProcedure" runat="server" ValidationGroup="EpisodeProcedure" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EpisodeProcedure"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table style="width: 100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="Label2" runat="server" Text="Booking No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboBookingNo" runat="server" Width="300px" AutoPostBack="true"
                            OnSelectedIndexChanged="cboBookingNo_SelectedIndexChanged" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvBookingNo" runat="server" ErrorMessage="Booking No required."
                            ControlToValidate="cboBookingNo" SetFocusOnError="True" ValidationGroup="EpisodeProcedure"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblOpNotesNo" runat="server" Text="Operating Notes No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboOpNotesNo" runat="server" Width="300px" AutoPostBack="true"
                            OnSelectedIndexChanged="cboOpNotesNo_SelectedIndexChanged" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvOpNotesNo" runat="server" ErrorMessage="Operating Notes No required."
                            ControlToValidate="cboOpNotesNo" SetFocusOnError="True" ValidationGroup="EpisodeProcedure"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblProcedureDate" runat="server" Text="Date & Time Started"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="100px">
                                    <telerik:RadDatePicker ID="txtProcedureDate" runat="server" Width="100px" DateInput-DateFormat="dd/MM/yyyy" 
                                        AutoPostBack ="true" OnSelectedDateChanged="txtProcedureDate_SelectedDateChanged"/>
                                </td>
                                <td width="50px">
                                    <telerik:RadMaskedTextBox ID="txtProcedureTime" runat="server" Mask="<00..23>:<00..59>"
                                        PromptChar="_" RoundNumericRanges="false" Width="50px" AutoPostBack="true" OnTextChanged="txtProcedureTime_TextChanged">
                                    </telerik:RadMaskedTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvProcedureDate" runat="server" ErrorMessage="Procedure Date Started required."
                            ControlToValidate="txtProcedureDate" SetFocusOnError="True" ValidationGroup="EpisodeProcedure"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Date & Time Finished"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="100px">
                                    <telerik:RadDatePicker ID="txtProcedureDate2" runat="server" Width="100px" Enabled="true"
                                        DateInput-DateFormat="dd/MM/yyyy" />
                                </td>
                                <td width="50px">
                                    <telerik:RadMaskedTextBox ID="txtProcedureTime2" runat="server" Mask="<00..23>:<00..59>"
                                        PromptChar="_" RoundNumericRanges="false" Width="50px">
                                    </telerik:RadMaskedTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvProcedureDate2" runat="server" ErrorMessage="Procedure Date Finished required."
                            ControlToValidate="txtProcedureDate2" SetFocusOnError="True" ValidationGroup="EpisodeProcedure"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label4" runat="server" Text="Date & Time Incision"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="100px">
                                    <telerik:RadDatePicker ID="txtIncisionDate" runat="server" Width="100px" Enabled="true"
                                        DateInput-DateFormat="dd/MM/yyyy" />
                                </td>
                                <td width="50px">
                                    <telerik:RadMaskedTextBox ID="txtIncisionTime" runat="server" Mask="<00..23>:<00..59>"
                                        PromptChar="_" RoundNumericRanges="false" Width="50px">
                                    </telerik:RadMaskedTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvIncisionDate" runat="server" ErrorMessage="Date Incision required."
                            ControlToValidate="txtIncisionDate" SetFocusOnError="True" ValidationGroup="EpisodeProcedure"
                            Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblProcedureID" runat="server" Text="Procedure"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboProcedureID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboProcedureID_ItemDataBound"
                            AutoPostBack="true" OnSelectedIndexChanged="cboProcedureID_SelectedIndexChanged"
                            OnItemsRequested="cboProcedureID_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "ProcedureName") %>
                                </b>
                                <br />
                                Procedure ID :
                                <%# DataBinder.Eval(Container.DataItem, "ProcedureID")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 50 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvProcedureID" runat="server" ErrorMessage="Procedure ID required."
                            ControlToValidate="cboProcedureID" SetFocusOnError="True" ValidationGroup="EpisodeProcedure"
                            Width="100%">
                            <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtProcedureText" runat="server" Width="300px" MaxLength="250"
                            ReadOnly="true" TextMode="MultiLine" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EpisodeProcedure"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EpisodeProcedure" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <fieldset style="width: 95%">
                            <legend><b>
                                <asp:Label ID="lblRegion" runat="server" Text="Operating Notes"></asp:Label></b></legend>
                            <telerik:RadTextBox ID="txtOperatinNotes" runat="server" Width="100%" ReadOnly="true" TextMode="MultiLine" Height="120px" />
                        </fieldset>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <fieldset style="width: 95%">
                <legend><b>Surgical Information</b></legend>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%; vertical-align: top;">
                            <table>

                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblParamedicID" runat="server" Text="Surgeon #1"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <%--<telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" Enabled="false" AllowCustomText="true"
                                            Filter="Contains" />--%>
                                        <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboPhysicianID_ItemsRequested"
                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                            <FooterTemplate>
                                                Note : Show max 15 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician required."
                                            ControlToValidate="cboParamedicID" SetFocusOnError="True" ValidationGroup="EpisodeProcedure"
                                            Width="100%">
                                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblParamedicID2a" runat="server" Text="Surgeon #2"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <%--<telerik:RadComboBox ID="cboParamedicID2a" runat="server" Width="300px" Enabled="false" AllowCustomText="true"
                                            Filter="Contains" />--%>
                                        <telerik:RadComboBox ID="cboParamedicID2a" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboPhysicianID_ItemsRequested"
                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                            <FooterTemplate>
                                                Note : Show max 15 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblParamedicID3a" runat="server" Text="Surgeon #3"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <%--<telerik:RadComboBox ID="cboParamedicID3a" runat="server" Width="300px" Enabled="false" AllowCustomText="true"
                                            Filter="Contains" />--%>
                                        <telerik:RadComboBox ID="cboParamedicID3a" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboPhysicianID_ItemsRequested"
                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                            <FooterTemplate>
                                                Note : Show max 15 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblParamedicID4a" runat="server" Text="Surgeon #4"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <%--<telerik:RadComboBox ID="cboParamedicID4a" runat="server" Width="300px" Enabled="false" AllowCustomText="true"
                                            Filter="Contains" />--%>
                                        <telerik:RadComboBox ID="cboParamedicID4a" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboPhysicianID_ItemsRequested"
                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                            <FooterTemplate>
                                                Note : Show max 15 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAssistantID1" runat="server" Text="Assistant #1"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <%--<telerik:RadComboBox ID="cboAssistantID1" runat="server" Width="300px" Enabled="false" AllowCustomText="true"
                                            Filter="Contains" />--%>
                                        <telerik:RadComboBox ID="cboAssistantID1" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboAssistantID_ItemsRequested"
                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                            <FooterTemplate>
                                                Note : Show max 15 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAssistantID2" runat="server" Text="Assistant #2"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <%--<telerik:RadComboBox ID="cboAssistantID2" runat="server" Width="300px" Enabled="false" AllowCustomText="true"
                                            Filter="Contains" />--%>
                                        <telerik:RadComboBox ID="cboAssistantID2" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboAssistantID_ItemsRequested"
                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                            <FooterTemplate>
                                                Note : Show max 15 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblParamedicIDAnestesi" runat="server" Text="Anesthesiologist"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <%--<telerik:RadComboBox ID="cboParamedicID2" runat="server" Width="300px" Enabled="false" AllowCustomText="true"
                                            Filter="Contains" />--%>
                                        <telerik:RadComboBox ID="cboParamedicID2" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboPhysicianIDAnestesi_ItemsRequested"
                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                            <FooterTemplate>
                                                Note : Show max 15 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAssistantIDAnestesi" runat="server" Text="Assistant Anesthesiologist #1"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <%--<telerik:RadComboBox ID="cboAssistantIDAnestesi" runat="server" Width="300px" Enabled="false" AllowCustomText="true"
                                            Filter="Contains" />--%>
                                        <telerik:RadComboBox ID="cboAssistantIDAnestesi" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboAssistantIDAnestesi_ItemsRequested"
                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                            <FooterTemplate>
                                                Note : Show max 15 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAssistantIDAnestesi2" runat="server" Text="Assistant Anesthesiologist #2"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <%--<telerik:RadComboBox ID="cboAssistantIDAnestesi" runat="server" Width="300px" Enabled="false" AllowCustomText="true"
                                            Filter="Contains" />--%>
                                        <telerik:RadComboBox ID="cboAssistantIDAnestesi2" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboAssistantIDAnestesi_ItemsRequested"
                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                            <FooterTemplate>
                                                Note : Show max 15 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblInstrumentatorID1" runat="server" Text="Instrumentator #1"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <%--<telerik:RadComboBox ID="cboInstrumentatorID1" runat="server" Width="300px" Enabled="false" AllowCustomText="true"
                                            Filter="Contains" />--%>
                                        <telerik:RadComboBox ID="cboInstrumentatorID1" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboInstrumentatorID_ItemsRequested"
                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                            <FooterTemplate>
                                                Note : Show max 15 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblInstrumentatorID2" runat="server" Text="Instrumentator #2"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <%--<telerik:RadComboBox ID="cboInstrumentatorID2" runat="server" Width="300px" Enabled="false" AllowCustomText="true"
                                            Filter="Contains" />--%>
                                        <telerik:RadComboBox ID="cboInstrumentatorID2" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboInstrumentatorID_ItemsRequested"
                                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                                            <FooterTemplate>
                                                Note : Show max 15 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSRProcedureCategory" runat="server" Text="Procedure Category"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboSRProcedureCategory" runat="server" Width="300px" />
                                    </td>
                                    <td width="20px">
                                        <asp:RequiredFieldValidator ID="rfvSRProcedureCategory" runat="server" ErrorMessage="Procedure Category required."
                                            ControlToValidate="cboSRProcedureCategory" SetFocusOnError="True" ValidationGroup="EpisodeProcedure"
                                            Width="100%">
                                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSRAnestesi" runat="server" Text="Anesthetic Type"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboSRAnestesi" runat="server" Width="300px" />
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboRoomID" runat="server" Width="300px" />
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label"></td>
                                    <td class="entry">
                                        <asp:CheckBox ID="chkIsCito" runat="server" Text="Cito" />
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
</table>


