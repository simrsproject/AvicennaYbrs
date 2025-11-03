<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CrossMatchingItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.BloodBank.CrossMatchingItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumBloodCrossMatching" runat="server" ValidationGroup="BloodCrossMatching" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="BloodCrossMatching"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
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
                            ControlToValidate="cboBagNo" SetFocusOnError="True" ValidationGroup="BloodCrossMatching"
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
                        <telerik:RadComboBox ID="cboSRBloodSource" runat="server" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px"></td>
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
                    <td width="20px"></td>
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
                    <td width="20px"></td>
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
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCrossmatchStartDateTime" runat="server" Text="Cross Match Date / Time"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadDateTimePicker ID="txtCrossmatchStartDateTime" runat="server" AutoPostBackControl="None">
                                        <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                        </DateInput>
                                        <TimeView ID="TimeView2" runat="server" TimeFormat="HH:mm">
                                        </TimeView>
                                    </telerik:RadDateTimePicker>
                                </td>
                                <td style="width: 10px">to
                                </td>
                                <td>
                                    <telerik:RadDateTimePicker ID="txtCrossmatchEndDateTime" runat="server" AutoPostBackControl="None">
                                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                        </DateInput>
                                        <TimeView ID="TimeView1" runat="server" TimeFormat="HH:mm">
                                        </TimeView>
                                    </telerik:RadDateTimePicker>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvCrossmatchStartDateTime" runat="server" ErrorMessage="Cross Match Start Date / Time required."
                            ControlToValidate="txtCrossmatchStartDateTime" SetFocusOnError="True" ValidationGroup="BloodCrossMatching"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvCrossmatchEndDateTime" runat="server" ErrorMessage="Cross Match End Date / Time required."
                            ControlToValidate="txtCrossmatchEndDateTime" SetFocusOnError="True" ValidationGroup="BloodCrossMatching"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCrossMatchingByUserID" runat="server" Text="Conducted By"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboCrossMatchingByUserID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboCrossMatchingByUserID_ItemDataBound"
                            OnItemsRequested="cboCrossMatchingByUserID_ItemsRequested">
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
                        <asp:RequiredFieldValidator ID="rfvCrossMatchingByUserID" runat="server" ErrorMessage="Conducted By required."
                            ControlToValidate="cboCrossMatchingByUserID" SetFocusOnError="True" ValidationGroup="BloodCrossMatching"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblIsCrossMatchingSuitability" runat="server" Text="Cross Matching Suitability"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 35%">
                                    <asp:RadioButtonList ID="rblIsCrossMatchingSuitability" runat="server" RepeatDirection="Vertical"
                                        RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rblIsCrossMatchingSuitability_OnSelectedIndexChanged">
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
                                                    RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rblIsCrossMatching_OnSelectedIndexChanged">
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
                                                    RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rblIsCrossMatching_OnSelectedIndexChanged">
                                                    <asp:ListItem Value="+" Text="+" />
                                                    <asp:ListItem Value="-" Text="-" />
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtCrossmatchCompatibleMinorLevel" runat="server"
                                                    Width="50px" NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="4" AutoPostBack="True"
                                                    OnTextChanged="rblIsCrossMatching_OnSelectedIndexChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 40%">
                                                <asp:Label ID="Label2" runat="server" Text="Auto Control"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <asp:RadioButtonList ID="rblCrossmatchCompatibleAutoControl" runat="server" RepeatDirection="Horizontal"
                                                    RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rblIsCrossMatching_OnSelectedIndexChanged">
                                                    <asp:ListItem Value="+" Text="+" />
                                                    <asp:ListItem Value="-" Text="-" />
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtCrossmatchCompatibleAutoControlLevel" runat="server"
                                                    Width="50px" NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="4" AutoPostBack="True"
                                                    OnTextChanged="rblIsCrossMatching_OnSelectedIndexChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 40%">
                                                <asp:Label ID="Label3" runat="server" Text="DCT"></asp:Label>
                                            </td>
                                            <td class="entry">
                                                <asp:RadioButtonList ID="rblCrossmatchCompatibleDctControl" runat="server" RepeatDirection="Horizontal"
                                                    RepeatLayout="Flow" AutoPostBack="False">
                                                    <asp:ListItem Value="+" Text="+" />
                                                    <asp:ListItem Value="-" Text="-" />
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtCrossmatchCompatibleDctControlLevel" runat="server"
                                                    Width="50px" NumberFormat-DecimalDigits="0" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvIsCrossMatchingSuitability" runat="server" ErrorMessage="Cross Matching Suitability required."
                            ControlToValidate="rblIsCrossMatchingSuitability" SetFocusOnError="True" ValidationGroup="BloodCrossMatching"
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
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox runat="server" ID="chkIsCrossmatchingPassed" Text="Passed" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox runat="server" ID="chkIsVoid" Text="Void" />
                        <asp:CheckBox runat="server" ID="chkIsCrossmatchBillProceed" Text="CrossmatchBillProceed" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="BloodCrossMatching"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="BloodCrossMatching" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            OnClick="btnCancel_ButtonClick" CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
