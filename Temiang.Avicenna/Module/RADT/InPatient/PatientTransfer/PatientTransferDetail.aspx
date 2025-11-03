<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PatientTransferDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatients.PatientTransferDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransferNo" runat="server" Text="Transfer No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransferNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransferNo" runat="server" ErrorMessage="Transfer No required."
                                ValidationGroup="entry" ControlToValidate="txtTransferNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                    Text=""></asp:Label>&nbsp; </a>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransferDate" runat="server" Text="Transfer Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtTransferDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtTransferTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransferDate" runat="server" ErrorMessage="Transfer Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransferDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorName" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGuarantorName" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCoverageClassName" runat="server" Text="Coverage Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtCoverageClassName" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" MaxLength="20"
                                ReadOnly="true" />
                            <asp:CheckBox ID="chkIsNewBornInfant" runat="server" Text="Newborn Infant" Enabled="false" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="241px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="25px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhysicianID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPhysicianID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblPhysicianName" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <fieldset id="FieldSet1" style="width: 150px; min-height: 150px;">
                    <legend>Photo</legend>
                    <asp:Image runat="server" ID="imgPatientPhoto" Width="150px" Height="150px" />
                </fieldset>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="1" cellspacing="5">
        <tr>
            <td style="width: 100%">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label3" runat="server" Text="TRANSFER DETAIL" Font-Bold="True" Font-Size="9"></asp:Label>
                    </legend>
                    <table style="width: 100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td style="vertical-align: top; width: 50%">
                                <table width="100%">
                                    <tr runat="server" id="trFilterFromClass">
                                        <td class="label">
                                        </td>
                                        <td class="label"></td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="From Service Unit"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromServiceUnitID" runat="server" Width="100px" MaxLength="10"
                                                ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblFromServiceUnitName" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromRoomID" runat="server" Text="From Room"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromRoomID" runat="server" Width="100px" MaxLength="10"
                                                ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblFromRoomName" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromBedID" runat="server" Text="From Bed No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromBedID" runat="server" Width="100px" MaxLength="10"
                                                ReadOnly="true" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromClassID" runat="server" Text="From Class"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromClassID" runat="server" Width="100px" MaxLength="10"
                                                ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblFromClassName" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFromChargeClassID" runat="server" Text="From Charge Class"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtFromChargeClassID" runat="server" Width="100px" MaxLength="10"
                                                ReadOnly="true" />
                                            &nbsp;
                                            <asp:Label ID="lblFromChargeClassName" runat="server" Text="" CssClass="labeldescription"></asp:Label>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRFromSpecialtyID" runat="server" Text="From SMF"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboFromSpecialtyID" runat="server" Width="300px" Enabled="False" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr runat="server" id="trRoomInFrom">
                                        <td class="label"></td>
                                        <td class="entry">
                                            <asp:CheckBox ID="chkIsRoomInFrom" runat="server" Text="Rooming In" Enabled="False" />
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr runat="server" id="trFilterToClass">
                                        <td class="label">
                                            <asp:Label ID="lblFilterClass" runat="server" Text="Filter Class"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboFilterClassID" runat="server" Width="300px" AutoPostBack="true" 
                                                OnSelectedIndexChanged="cboFilterClassID_SelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToServiceUnitID" runat="server" Text="To Service Unit"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvToServiceUnitID" runat="server" ErrorMessage="To Service Unit required."
                                                ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToRoomID" runat="server" Text="To Room"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboRoomID" runat="server" Width="300px" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboRoomID_SelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvToRoomID" runat="server" ErrorMessage="To Room required."
                                                ValidationGroup="entry" ControlToValidate="cboRoomID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToBedID" runat="server" Text="To Bed No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboBedID" runat="server" Width="300px" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboBedID_SelectedIndexChanged" HighlightTemplatedItems="True"
                                                MarkFirstMatch="true" OnItemDataBound="cboBedID_ItemDataBound" OnItemsRequested="cboBedID_ItemsRequested"
                                                EnableLoadOnDemand="true" NoWrap="True">
                                                <ItemTemplate>
                                                    <b>
                                                        <%# DataBinder.Eval(Container.DataItem, "BedID")%>
                                                    </b>&nbsp;:&nbsp;
                                                    <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                                                    &nbsp; (<%# DataBinder.Eval(Container.DataItem, "Sex")%>)
                                                    <br />
                                                    <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 50 result
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvToBedID" runat="server" ErrorMessage="To Bed No required."
                                                ValidationGroup="entry" ControlToValidate="cboBedID" SetFocusOnError="True" Width="100%">
                                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToClassID" runat="server" Text="To Class"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboClassID" runat="server" Width="300px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvToClassID" runat="server" ErrorMessage="To Class required."
                                                ValidationGroup="entry" ControlToValidate="cboClassID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToChargeClassID" runat="server" Text="To Charge Class"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboChargeClassID" runat="server" Width="300px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvToChargeClassID" runat="server" ErrorMessage="To Charge Class required."
                                                ValidationGroup="entry" ControlToValidate="cboChargeClassID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblToSpecialtyID" runat="server" Text="To SMF"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboToSpecialityID" runat="server" Width="300px" />
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvToSpecialityID" runat="server" ErrorMessage="To SMF required."
                                                ValidationGroup="entry" ControlToValidate="cboToSpecialityID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr runat="server" id="trRoomInTo">
                                        <td class="label"></td>
                                        <td class="entry">
                                            <asp:CheckBox ID="chkIsRoomInTo" runat="server" Text="Rooming In" />
                                        </td>
                                        <td width="20"></td>
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
</asp:Content>
