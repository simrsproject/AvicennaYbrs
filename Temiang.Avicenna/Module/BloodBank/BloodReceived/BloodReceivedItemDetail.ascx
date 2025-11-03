<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BloodReceivedItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.BloodBank.BloodReceivedItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumBloodBankItem" runat="server" ValidationGroup="BloodBankItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="BloodBankItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField ID="hdnIsBypassBloodCrossMatching" runat="server" />
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBagNo" runat="server" Text="Bag No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboBagNo" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboBagNo_ItemDataBound" OnItemsRequested="cboBagNo_ItemsRequested"
                            AutoPostBack="True" OnSelectedIndexChanged="cboBagNo_SelectedIndexChanged">
                            <ItemTemplate>
                                <b><%# DataBinder.Eval(Container.DataItem, "BagNo")%></b>&nbsp;&nbsp;[
                                <i>ED:&nbsp;<%# DataBinder.Eval(Container.DataItem, "ExpiredDateTime", "{0:dd-MMM-yyyy HH:mm}")%></i>]
                                <br />
                                Vol:&nbsp;<%# DataBinder.Eval(Container.DataItem, "VolumeBag", "{0:n0}")%> &nbsp;ML/CC
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvBagNo" runat="server" ErrorMessage="Bag No required."
                            ControlToValidate="cboBagNo" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRBloodSource" runat="server" Text="Blood Source"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRBloodSource" runat="server" Width="300px" AutoPostBack="True"
                            OnSelectedIndexChanged="cboSRBloodSource_SelectedIndexChanged" Enabled="False">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRBloodSource" runat="server" ErrorMessage="Blood Source required."
                            ControlToValidate="cboSRBloodSource" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                            Width="100%">
                            <asp:Image ID="Image17" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRBloodSourceFrom" runat="server" Text="Blood Source From"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRBloodSourceFrom" runat="server" Width="300px" Enabled="False">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRBloodSourceFrom" runat="server" ErrorMessage="Blood Source From required."
                            ControlToValidate="cboSRBloodSourceFrom" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                            Width="100%">
                            <asp:Image ID="Image18" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRBloodGroupReceived" runat="server" Text="Blood Group Received"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRBloodGroupReceived" runat="server" Width="300px" Enabled="False">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRBloodGroupReceived" runat="server" ErrorMessage="Blood Group Received required."
                            ControlToValidate="cboSRBloodGroupReceived" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblVolumeBag" runat="server" Text="Volume"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtVolumeBag" runat="server" Width="100px" NumberFormat-DecimalDigits="0" ReadOnly="true" />
                                </td>
                                <td style="width: 5px"></td>
                                <td>ML/CC
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblExpiredDateTime" runat="server" Text="Blood ED"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDateTimePicker ID="txtExpiredDateTime" runat="server" AutoPostBackControl="None"
                            Enabled="False">
                            <DateInput ID="DateInput3" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                            </DateInput>
                            <TimeView ID="TimeView3" runat="server" TimeFormat="HH:mm">
                            </TimeView>
                        </telerik:RadDateTimePicker>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBloodBagTemperature" runat="server" Text="Blood Bag Temperature"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtBloodBagTemperature" runat="server" Width="100px"
                                        NumberFormat-DecimalDigits="2" />
                                </td>
                                <td style="width: 5px"></td>
                                <td>°C
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReceivedDateTime" runat="server" Text="Submitted Date / Time"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtReceivedDate" runat="server" Width="100px">
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <telerik:RadMaskedTextBox ID="txtReceivedTime" runat="server" Mask="<00..23>:<00..59>"
                                        PromptChar="_" RoundNumericRanges="false" Width="50px">
                                    </telerik:RadMaskedTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvReceivedDate" runat="server" ErrorMessage="Submitted Date required."
                            ValidationGroup="BloodBankItem" ControlToValidate="txtReceivedDate" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvReceivedTime" runat="server" ErrorMessage="Received Time required."
                            ValidationGroup="BloodBankItem" ControlToValidate="txtReceivedTime" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblExaminerByUserID" runat="server" Text="Examiner & Submitted By"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboExaminerByUserID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboExaminerByUserID_ItemDataBound"
                            OnItemsRequested="cboExaminerByUserID_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvExaminerByUserID" runat="server" ErrorMessage="Examiner & Submitted By required."
                            ControlToValidate="cboExaminerByUserID" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <asp:Panel ID="pnlCbo" runat="server">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReceivedByUserID" runat="server" Text="Received By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboReceivedByUserID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboReceivedByUserID_ItemDataBound"
                                OnItemsRequested="cboReceivedByUserID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                         <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvcboReceivedByUserID" runat="server" ErrorMessage="Received required."
                                ControlToValidate="cboReceivedByUserID" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlTxt" runat="server">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblUnitOfficer" runat="server" Text="Received By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtUnitOfficer" runat="server" Width="300px" MaxLength="150" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvtxtUnitOfficer" runat="server" ErrorMessage="Received By required."
                                ControlToValidate="txtUnitOfficer" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBloodBagNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtBloodBagNotes" runat="server" Width="300px" MaxLength="500"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="BloodBankItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="BloodBankItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            OnClick="btnCancel_ButtonClick" CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblIsCrossMatchingSuitability" runat="server" Text="Cross Matching Suitability"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 35%">
                                    <asp:RadioButtonList ID="rblIsCrossMatchingSuitability" runat="server" RepeatDirection="Vertical"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Compatible" />
                                        <asp:ListItem Value="0" Text="In-Compatible" />
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td class="label" style="width: 40%">
                                                <asp:Label ID="lblCrossmatchCompatibleMajor" runat="server" Text="Major"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <asp:RadioButtonList ID="rblCrossmatchCompatibleMajor" runat="server" RepeatDirection="Horizontal"
                                                    RepeatLayout="Flow">
                                                    <asp:ListItem Value="+" Text="+" />
                                                    <asp:ListItem Value="-" Text="-" />
                                                </asp:RadioButtonList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 40%">
                                                <asp:Label ID="Label1" runat="server" Text="Minor"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <asp:RadioButtonList ID="rblCrossmatchCompatibleMinor" runat="server" RepeatDirection="Horizontal"
                                                    RepeatLayout="Flow">
                                                    <asp:ListItem Value="+" Text="+" />
                                                    <asp:ListItem Value="-" Text="-" />
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtCrossmatchCompatibleMinorLevel" runat="server"
                                                    Width="50px" NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="4" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 40%">
                                                <asp:Label ID="Label2" runat="server" Text="Auto Control"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <asp:RadioButtonList ID="rblCrossmatchCompatibleAutoControl" runat="server" RepeatDirection="Horizontal"
                                                    RepeatLayout="Flow">
                                                    <asp:ListItem Value="+" Text="+" />
                                                    <asp:ListItem Value="-" Text="-" />
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtCrossmatchCompatibleAutoControlLevel" runat="server"
                                                    Width="50px" NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="4" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 40%">
                                                <asp:Label ID="Label3" runat="server" Text="DCT"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <asp:RadioButtonList ID="rblCrossmatchCompatibleDctControl" runat="server" RepeatDirection="Horizontal"
                                                    RepeatLayout="Flow">
                                                    <asp:ListItem Value="+" Text="+" />
                                                    <asp:ListItem Value="-" Text="-" />
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtCrossmatchCompatibleDctControlLevel" runat="server"
                                                    Width="50px" NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="4" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvIsCrossMatchingSuitability" runat="server" ErrorMessage="Cross Matching Suitability required."
                            ControlToValidate="rblIsCrossMatchingSuitability" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                            Width="100%">
                            <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblScreening" runat="server" Text="Screening"></asp:Label></td>
                    <td class="entry">
                        <asp:CheckBox runat="server" ID="chkIsScreening1" Text="#1" />&nbsp;&nbsp;
                        <asp:CheckBox runat="server" ID="chkIsScreening2" Text="#2" />&nbsp&nbsp;
                        <asp:CheckBox runat="server" ID="chkIsScreening3" Text="#3" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label" style="width: 250px">
                        <asp:Label ID="lblIsScreeningLabelPassedPmi" runat="server" Text="Screening Label Passed (PMI)"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 40%">
                                    <asp:RadioButtonList ID="rblIsScreeningLabelPassedPmi" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvIsScreeningLabelPassedPmi" runat="server" ErrorMessage="Screening Label Passed (PMI) required."
                            ControlToValidate="rblIsScreeningLabelPassedPmi" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblIsExpiredDate" runat="server" Text="Expired Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 40%">
                                    <asp:RadioButtonList ID="rblIsExpiredDate" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvIsExpiredDate" runat="server" ErrorMessage="Expired Date required."
                            ControlToValidate="rblIsExpiredDate" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                            Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblIsLeak" runat="server" Text="Leak"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 40%">
                                    <asp:RadioButtonList ID="rblIsLeak" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvIsLeak" runat="server" ErrorMessage="Leak required."
                            ControlToValidate="rblIsLeak" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                            Width="100%">
                            <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblIsHemolysis" runat="server" Text="Hemolysis"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 40%">
                                    <asp:RadioButtonList ID="rblIsHemolysis" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvIsHemolysis" runat="server" ErrorMessage="Hemolysis required."
                            ControlToValidate="rblIsHemolysis" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                            Width="100%">
                            <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblIsClotting" runat="server" Text="Clotting"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 40%">
                                    <asp:RadioButtonList ID="rblIsClotting" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvIsClotting" runat="server" ErrorMessage="Clotting required."
                            ControlToValidate="rblIsClotting" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                            Width="100%">
                            <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblIsBloodTypeCompatibility" runat="server" Text="Blood Type Compatibility"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 40%">
                                    <asp:RadioButtonList ID="rblIsBloodTypeCompatibility" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvIsBloodTypeCompatibility" runat="server" ErrorMessage="Blood Type Compatibility required."
                            ControlToValidate="rblIsBloodTypeCompatibility" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                            Width="100%">
                            <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblIsLabelDonorsMatchesWithPatientsForm" runat="server" Text="Label Donors Matches With Patients Form"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 40%">
                                    <asp:RadioButtonList ID="rblIsLabelDonorsMatchesWithPatientsForm" runat="server"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvIsLabelDonorsMatchesWithPatientsForm" runat="server"
                            ErrorMessage="Label Donors Matches With Patients Form required." ControlToValidate="rblIsLabelDonorsMatchesWithPatientsForm"
                            SetFocusOnError="True" ValidationGroup="BloodBankItem" Width="100%">
                            <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblIsScreeningLabelPassedBd" runat="server" Text="Screening Label Passed (Blood Bank)"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 40%">
                                    <asp:RadioButtonList ID="rblIsScreeningLabelPassedBd" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvIsScreeningLabelPassedBd" runat="server" ErrorMessage="Screening Label Passed (Blood Bank) required."
                            ControlToValidate="rblIsScreeningLabelPassedBd" SetFocusOnError="True" ValidationGroup="BloodBankItem"
                            Width="100%">
                            <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox runat="server" ID="chkIsVoid" Text="Void" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
