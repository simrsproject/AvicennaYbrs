<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientDetail"
    Title="Patient Entry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="uc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="Address" TagPrefix="uc1" %>
<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="AddressCtl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinDukcapil() {
                var nik = $find("<%= txtSSN.ClientID %>");
                if (nik.get_value() != '') radopen("DukcapilDialog.aspx?nik=" + nik.get_value() + "&type=<%= Request.QueryString["rt"] %>" + "&info=1", "winProcess");
                else alert('NIK pasien belum diisi')
            }

            function onClientTabSelected(sender, eventArgs) {
                var tabIndex = eventArgs.get_tab().get_index();
                if (tabIndex == 5) radopen("../Allergy/PatientAllergy.aspx", "winProcess");
            }

            function onWinProcessClose(sender, eventArgs) {
                if (sender.argument.mode == "rebind") {
                    __doPostBack("<%= txtSSN.UniqueID %>", "rebind");
                }
                else {
                    var tabStrip = $find('<%= tabDetail.ClientID %>');
                    var tab = tabStrip.findTabByText('Address');
                    tab.set_selected(true);
                }
            }

            function onWinCaptureImageClose(sender, eventArgs) {
                var arg = eventArgs.get_argument();
                if (arg) {
                    var img = document.getElementById("<%=imgPatientPhoto.ClientID%>");
                    img.setAttribute('src', arg);
                }
            }

            function openWinCaptureImage() {
                var oWnd;
                oWnd = radopen("PatientPhoto/CaptureImageForm.aspx", "winCaptureImage");
                oWnd.setSize(696, 340);
                oWnd.center();
            }
        </script>
        <script type="text/javascript" language="javascript">
            function onWinWebCamClose(sender, eventArgs) {
                // Get retval
                var arg = eventArgs.get_argument();
                if (arg) {
                    var img = document.getElementById("<%=imgPatientPhoto.ClientID%>");
                    img.setAttribute('src', arg);
                    var hdnImgData = document.getElementById("<%=hdnImgData.ClientID%>");
                    hdnImgData.value = arg;
                }
            }


            function openWinWebCam() {
                var oWnd = $find("<%= winWebCam.ClientID %>");
                oWnd.setUrl("PatientPhoto2/WebCam.aspx");
                oWnd.setSize(240 + 50, 320 + 50);
                oWnd.center();
                oWnd.show();
            }

            function openWinPCareMemberInfo() {
                var oWnd = $find("<%= winPcare.ClientID %>");
                var txtGuarantorCardNo = $find("<%= txtGuarantorCardNo.ClientID %>");
                var txtPatientID = $find("<%= txtPatientID.ClientID %>");
                var txtSsn = $find("<%= txtSSN.ClientID %>");
                oWnd.setUrl('<%=Page.ResolveUrl("~/PCareCommon/PCareMemberInfo.aspx")%>?bpjsno=' + txtGuarantorCardNo.get_textBoxValue() + '&regno=&patientid=' + txtPatientID.get_textBoxValue() + '&nik=' + txtSsn.get_textBoxValue());
                oWnd.setSize(1040, 450);
                oWnd.center();
                oWnd.show();
            }

            function winPCare_OnClientClose(oWnd, args) {

                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.Nama) {
                        var txtPatientName = $find("<%= txtFirstName.ClientID %>");
                        txtPatientName.set_value(arg.Nama);
                    }
                    if (arg.NoKartu) {
                        var txtBpjsNo = $find("<%= txtGuarantorCardNo.ClientID %>");
                        txtBpjsNo.set_value(arg.NoKartu);
                    }
                    if (arg.NoKTP) {
                        var txtSsn = $find("<%= txtSSN.ClientID %>");
                        txtBpjsNo.set_value(arg.NoKTP);
                    }
                    if (arg.TglLahir) {
                        var pattern = /(\d{2})\/(\d{2})\/(\d{4})/;
                        var dt = new Date(arg.TglLahir.replace(pattern, '$3-$2-$1'));

                        var txtDob = $find("<%= txtDateOfBirth.ClientID %>");
                        txtDob.set_selectedDate(dt);
                    }
                    if (arg.Sex) {
                        var itemText = arg.Sex == "L" ? "Male" : "Female";
                        var cboSRGenderType = $find("<%= cboSRGenderType.ClientID %>");
                        var item = combo.findItemByText(itemText);
                        if (item) {
                            item.select();
                        }
                    }
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Modal="true" VisibleStatusbar="false"
        DestroyOnClose="false" Behavior="Close, Move" ReloadOnShow="True" ShowContentDuringLoad="false">
        <Windows>
            <telerik:RadWindow ID="winProcess" Width="1000px" Height="450px" runat="server" OnClientClose="onWinProcessClose">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winCaptureImage" Width="720px" Height="380px" runat="server"
                OnClientClose="onWinCaptureImageClose">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winPcare" Width="720px" Height="380px" runat="server"
                OnClientClose="winPCare_OnClientClose">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winWebCam" Width="680px" Height="620px" runat="server" Modal="true"
                ShowContentDuringLoad="false" Behaviors="None" VisibleStatusbar="False"
                OnClientClose="onWinWebCamClose">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboZipCode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTempAddressStreetName" />
                    <telerik:AjaxUpdatedControl ControlID="txtTempAddressDistrict" />
                    <telerik:AjaxUpdatedControl ControlID="txtTempAddressCounty" />
                    <telerik:AjaxUpdatedControl ControlID="txtTempAddressCity" />
                    <telerik:AjaxUpdatedControl ControlID="txtTempAddressState" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboEmrContactZipCode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactStreetName" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactDistrict" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactCounty" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactCity" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactState" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtEmrContactStreetName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactStreetName" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactDistrict" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactCounty" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactCity" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactState" />
                    <telerik:AjaxUpdatedControl ControlID="cboEmrContactZipCode" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactPhoneNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactFaxNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactMobilePhoneNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmrContactEmail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRSalutation">
                <UpdatedControls>
                    <%--<telerik:AjaxUpdatedControl ControlID="rbtSex" />--%>
                    <telerik:AjaxUpdatedControl ControlID="cboSRGenderType" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtDateOfBirth">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAgeInYear" />
                    <telerik:AjaxUpdatedControl ControlID="txtAgeInMonth" />
                    <telerik:AjaxUpdatedControl ControlID="txtAgeInDay" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtAgeInYear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDateOfBirth" />
                    <telerik:AjaxUpdatedControl ControlID="txtAgeInMonth" />
                    <telerik:AjaxUpdatedControl ControlID="txtAgeInDay" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtAgeInMonth">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDateOfBirth" />
                    <telerik:AjaxUpdatedControl ControlID="txtAgeInYear" />
                    <telerik:AjaxUpdatedControl ControlID="txtAgeInDay" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtAgeInDay">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDateOfBirth" />
                    <telerik:AjaxUpdatedControl ControlID="txtAgeInYear" />
                    <telerik:AjaxUpdatedControl ControlID="txtAgeInMonth" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdVisite">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdVisite" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPatientDialysis">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatientDialysis" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 43%;" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID / Medical No"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtPatientID" runat="server" Width="120px" MaxLength="15"
                                ReadOnly="true">
                            </telerik:RadTextBox>
                            &nbsp;/&nbsp;
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" MaxLength="15">
                            </telerik:RadTextBox>
                        </td>
                        <td width="10px">
                            <asp:ImageButton ID="btnImpBpjsMember" runat="server" ImageUrl="../../../Images/Toolbar/search16.png"
                                CausesValidation="False" OnClientClick="openWinPCareMemberInfo();return false;"
                                ToolTip="Update from BPJS Member" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRSalutation" runat="server" Text="Salutation"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboSRSalutation" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboSRSalutation_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvSRSalutation" runat="server" ErrorMessage="Salutation required."
                                ValidationGroup="entry" ControlToValidate="cboSRSalutation" SetFocusOnError="True"
                                Width="100%" Visible="False">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name*"></asp:Label>
                        </td>
                        <td class="entry300" style="vertical-align: middle;">
                            <telerik:RadTextBox ID="txtFirstName" runat="server" Width="300px" MaxLength="50">
                            </telerik:RadTextBox>

                        </td>
                        <td width="10px">

                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="First Name required."
                                ValidationGroup="entry" ControlToValidate="txtFirstName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>

                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtMiddleName" runat="server" Width="300px" MaxLength="50">
                            </telerik:RadTextBox>
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvMiddleName" runat="server" ErrorMessage="Middle Name required."
                                ValidationGroup="entry" ControlToValidate="txtFirstName" SetFocusOnError="True"
                                Width="100%" Visible="False">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtLastName" runat="server" Width="300px" MaxLength="50">
                            </telerik:RadTextBox>
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Last Name required."
                                ValidationGroup="entry" ControlToValidate="txtLastName" SetFocusOnError="True"
                                Width="100%" Visible="False">
                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCityOfBirth" runat="server" Text="City / Date Of Birth*"></asp:Label>
                        </td>
                        <td class="entry300">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 190px; vertical-align: top">
                                        <telerik:RadTextBox ID="txtCityOfBirth" runat="server" Width="180px" MaxLength="50">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 10px; vertical-align: middle;">/
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px" AutoPostBack="true"
                                            OnSelectedDateChanged="txtDateOfBirth_SelectedDateChanged">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvCityOfBirth" runat="server" ErrorMessage="City Of Birth required."
                                ValidationGroup="entry" ControlToValidate="txtCityOfBirth" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvBirthDate" runat="server" ErrorMessage="Invalid Date Of Birth"
                                SetFocusOnError="true" ValidationGroup="entry" ControlToValidate="txtDateOfBirth"
                                Width="20px" OnServerValidate="cvBirthDate_ServerValidate">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ErrorMessage="Date Of Birth required."
                                ValidationGroup="entry" ControlToValidate="txtDateOfBirth" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry300">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 35px">
                                        <telerik:RadTextBox ID="txtAgeInYear" runat="server" Width="30px" AutoPostBack="true"
                                            OnTextChanged="txtAgeInYear_TextChanged">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 25px">&nbsp;Y
                                    </td>
                                    <td style="width: 35px">
                                        <telerik:RadTextBox ID="txtAgeInMonth" runat="server" Width="30px" AutoPostBack="true"
                                            OnTextChanged="txtAgeInMonth_TextChanged">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 25px">&nbsp;M
                                    </td>
                                    <td style="width: 35px">
                                        <telerik:RadTextBox ID="txtAgeInDay" runat="server" Width="30px" AutoPostBack="true"
                                            OnTextChanged="txtAgeInDay_TextChanged">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 25px">&nbsp;D
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <%--                 <tr>
                        <td class="label">
                            <asp:Label ID="lblSex" runat="server" Text="Gender*"></asp:Label>
                        </td>
                        <td class="entry300">
                            <asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="M" Text="Male" />
                                <asp:ListItem Value="F" Text="Female" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvSex" runat="server" ErrorMessage="Gender required."
                                ValidationGroup="entry" ControlToValidate="rbtSex" SetFocusOnError="True" Width="20px">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>--%>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSex" runat="server" Text="Gender*"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboSRGenderType" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboSRGenderType_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvSRGenderType" runat="server" ErrorMessage="Gender required."
                                ValidationGroup="entry" ControlToValidate="cboSRGenderType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image19" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>

                </table>
            </td>
            <td style="width: 43%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParentSpouseName" runat="server" Text="Spouse Name"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtParentSpouseName" runat="server" Width="300px" MaxLength="50">
                            </telerik:RadTextBox>
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvParentSpouseName" runat="server" ErrorMessage="Spouse Name required."
                                ValidationGroup="entry" ControlToValidate="txtParentSpouseName" SetFocusOnError="True"
                                Width="20px" Visible="False">
                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trRelation">
                        <td class="label">
                            <asp:Label ID="lblSRPatienRelation" runat="server" Text="Relation"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadComboBox ID="cboSRPatienRelation" runat="server" Width="300px" />
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvSRPatienRelation" runat="server" ErrorMessage="Relation required."
                                ValidationGroup="entry" ControlToValidate="cboSRPatienRelation" SetFocusOnError="True"
                                Width="20px" Visible="False">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParentSpouseMedicalNo" runat="server" Text="Spouse MRN"></asp:Label>
                        </td>
                        <td class="entry300">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtParentSpouseMedicalNo" runat="server" Width="100px" MaxLength="50">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>&nbsp;&nbsp;<asp:Label ID="lblParentSpouseAge" runat="server" Text="Age"></asp:Label>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtParentSpouseAge" runat="server" Width="30px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>&nbsp;&nbsp;&nbsp;Year
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlParentInformationFather">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label8" runat="server" Text="Father Name"></asp:Label>
                            </td>
                            <td class="entry300">
                                <telerik:RadTextBox ID="txtFatherName" runat="server" Width="300px" MaxLength="50">
                                </telerik:RadTextBox>
                            </td>
                            <td width="10px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label9" runat="server" Text="Father MRN"></asp:Label>
                            </td>
                            <td class="entry300">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtFatherMedicalNo" runat="server" Width="100px" MaxLength="50">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;<asp:Label ID="Label10" runat="server" Text="Age"></asp:Label>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtFatherAge" runat="server" Width="30px">
                                                <NumberFormat DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>&nbsp;&nbsp;&nbsp;Year
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="10px"></td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlParentInformationMother">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblMotherName" runat="server" Text="Mother Name"></asp:Label>
                            </td>
                            <td class="entry300">
                                <telerik:RadTextBox ID="txtMotherName" runat="server" Width="300px" MaxLength="50">
                                </telerik:RadTextBox>
                            </td>
                            <td width="10px">
                                <asp:RequiredFieldValidator ID="rfvMotherName" runat="server" ErrorMessage="Mother Name required."
                                    ValidationGroup="entry" ControlToValidate="txtMotherName" SetFocusOnError="True"
                                    Width="20px" Visible="False">
                                    <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblMotherMedicalNo" runat="server" Text="Mother MRN"></asp:Label>
                            </td>
                            <td class="entry300">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtMotherMedicalNo" runat="server" Width="100px" MaxLength="50">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>&nbsp;&nbsp;<asp:Label ID="Label7" runat="server" Text="Age"></asp:Label>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtMotherAge" runat="server" Width="30px">
                                                <NumberFormat DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>&nbsp;&nbsp;&nbsp;Year
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="10px"></td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSSN" runat="server" Text="SSN"></asp:Label>
                        </td>
                        <td class="entry300">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSSN" runat="server" Width="300px" MaxLength="16">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:ImageButton runat="server" ID="btnInfoDukcapil" ImageUrl="~/Images/infoblue16.png" ToolTip="Info Dukcapil" OnClientClick="openWinDukcapil();return false;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvSSN" runat="server" ErrorMessage="SSN required."
                                ValidationGroup="entry" ControlToValidate="txtSSN" SetFocusOnError="True" Width="20px"
                                Visible="False">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPassportNo" runat="server" Text="Passport No"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtPassportNo" runat="server" Width="300px" MaxLength="50">
                            </telerik:RadTextBox>
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor*"></asp:Label>
                        </td>
                        <td class="entry300">
                            <uc:RadComboBoxExt ID="cboGuarantorID" runat="server" Width="300px" LookUpType="Guarantors">
                            </uc:RadComboBoxExt>
                        </td>
                        <td width="10px">
                            <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ErrorMessage="Guarantor required."
                                ValidationGroup="entry" ControlToValidate="cboGuarantorID" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorCardNo" runat="server" Text="BPJS No / Guarantor Card No"></asp:Label>
                        </td>
                        <td class="entry300">
                            <telerik:RadTextBox ID="txtGuarantorCardNo" runat="server" Width="300px" MaxLength="50">
                            </telerik:RadTextBox>
                        </td>
                        <td width="10px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 20%; vertical-align: top">
                <fieldset id="FieldSet1" style="width: 180px; min-height: 180px;">
                    <legend>Photo</legend>
                    <div style='float: right; padding: 4px'>
                        <asp:Image runat="server" ID="imgPatientPhoto" Width="180px" Height="180px" />
                    </div>
                    <div style='float: right; padding: 2px'>
                        <%--<asp:Button runat="server" Text="Update Photo" ID="btnCaptureImage" Width="180px"
                            OnClientClick="openWinCaptureImage();return false;" />--%>
                        <asp:Button runat="server" Text="Update Photo" ID="btnCaptureImage" Width="180px"
                            OnClientClick="openWinWebCam();return false;" />
                        <asp:HiddenField runat="server" ID="hdnImgData" />
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip runat="server" ID="tabDetail" MultiPageID="mpgDetail" ShowBaseLine="true">
        <Tabs>
            <telerik:RadTab Text="Address" runat="server" PageViewID="pgAddress" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab Text="Temporary Address" runat="server" PageViewID="pgTempAddress">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Emergency Contact" PageViewID="pgEmergency">
            </telerik:RadTab>
            <telerik:RadTab Text="Detail" runat="server" PageViewID="pgDetail">
            </telerik:RadTab>
            <telerik:RadTab Text="Office & Occupation" runat="server" PageViewID="pgOffice">
            </telerik:RadTab>
            <telerik:RadTab Text="Other" runat="server" PageViewID="pgOther">
            </telerik:RadTab>
            <telerik:RadTab Text="Allergy & Illness History" runat="server" PageViewID="pgAllergy">
            </telerik:RadTab>
            <telerik:RadTab Text="Immunization & Vaccine History" runat="server" PageViewID="pgImmunization">
            </telerik:RadTab>
            <telerik:RadTab Text="Dialysis History" runat="server" PageViewID="pgDialysis">
            </telerik:RadTab>
            <telerik:RadTab Text="Outstanding Visite" runat="server" PageViewID="pgVisite">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="mpgDetail" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView runat="server" ID="pgAddress">
            <uc1:Address ID="ctlAddress" runat="server" />
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="pgTempAddress">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" style="text-align: center">
                        <table width="100%">
                            <tr>
                                <td class="labelcaption">
                                    <asp:Label ID="Label1" runat="server" Text="Address"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblStreetName" runat="server" Text="Street Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtTempAddressStreetName" runat="server" Width="300px" MaxLength="250" TextMode="MultiLine">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblZipCode" runat="server" Text="Zip Code"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboZipCode" runat="server" Width="300px" AutoPostBack="true"
                                        EnableLoadOnDemand="true" MarkFirstMatch="False" HighlightTemplatedItems="true"
                                        OnItemDataBound="cboZipCode_ItemDataBound" OnItemsRequested="cboZipCode_ItemsRequested"
                                        OnSelectedIndexChanged="cboZipCode_SelectedIndexChanged">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "District")%>
                                            &nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "ZipPostalCode")%>) </b>
                                            <br />
                                            County :
                                            <%# DataBinder.Eval(Container.DataItem, "County")%>
                                            <br />
                                            City :
                                            <%# DataBinder.Eval(Container.DataItem, "City")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 10 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDistrict" runat="server" Text="District"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtTempAddressDistrict" runat="server" Width="300px" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCounty" runat="server" Text="County"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtTempAddressCounty" runat="server" Width="300px" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtTempAddressCity" runat="server" Width="300px" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblState" runat="server" Text="State"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtTempAddressState" runat="server" Width="300px" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtTempAddressPhoneNo" runat="server" Width="300px" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgEmergency" runat="server">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblContactName" runat="server" Text="Contact Name"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadTextBox ID="txtContactName" runat="server" Width="300px" MaxLength="100" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblContactSsn" runat="server" Text="SSN"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadTextBox ID="txtContactSsn" runat="server" Width="300px" MaxLength="50" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRRelation" runat="server" Text="Relation"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboSRRelation" runat="server" Width="300px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <%--<uc1:AddressCtl ID="AddressCtl" runat="server" />--%>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" style="text-align: center">
                        <table width="100%">
                            <tr>
                                <td class="labelcaption">
                                    <asp:Label ID="Label3" runat="server" Text="Address"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="50%" style="vertical-align: text-top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblEmrContactStreetName" runat="server" Text="Street Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEmrContactStreetName" runat="server" Width="300px" MaxLength="250"
                                        AutoPostBack="true" OnTextChanged="txtEmrContactStreetName_TextChanged" TextMode="MultiLine">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblEmrContactZipCode" runat="server" Text="Zip Code"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboEmrContactZipCode" runat="server" Width="300px" EnableLoadOnDemand="true"
                                        MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboEmrContactZipCode_ItemDataBound"
                                        OnItemsRequested="cboEmrContactZipCode_ItemsRequested" OnSelectedIndexChanged="cboEmrContactZipCode_SelectedIndexChanged">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "District")%>
                                            &nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "ZipPostalCode")%>) </b>
                                            <br />
                                            County :
                                            <%# DataBinder.Eval(Container.DataItem, "County")%>
                                            <br />
                                            City :
                                            <%# DataBinder.Eval(Container.DataItem, "City")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblEmrContactDistric" runat="server" Text="District"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEmrContactDistrict" runat="server" Width="300px" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblEmrContactCounty" runat="server" Text="County"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEmrContactCounty" runat="server" Width="300px" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblEmrContactCity" runat="server" Text="City"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEmrContactCity" runat="server" Width="300px" MaxLength="50">
                                    </telerik:RadTextBox>
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
                                    <asp:Label ID="lblEmrContactState" runat="server" Text="State"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEmrContactState" runat="server" Width="300px" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblEmrContactPhoneNo" runat="server" Text="Phone No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEmrContactPhoneNo" runat="server" Width="300px" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblEmrContactFaxNo" runat="server" Text="Fax No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEmrContactFaxNo" runat="server" Width="300px" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblEmrContactMobilePhoneNo" runat="server" Text="Mobile Phone No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEmrContactMobilePhoneNo" runat="server" Width="300px"
                                        MaxLength="20">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblEmrContactEmail" runat="server" Text="Email"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEmrContactEmail" runat="server" Width="300px" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="20px">
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmrContactEmail"
                                        ErrorMessage="Email is not valid" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="entry">
                                        <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="pgDetail">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRBloodType" runat="server" Text="Blood"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRBloodType" runat="server" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBloodRhesus" runat="server" Text="Rhesus"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblBloodRhesus" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="0" Text="+" Selected="True" />
                                        <asp:ListItem Value="1" Text="-" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblIsDisability" runat="server" Text="Disability"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblIsDisability" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="0" Text="No" Selected="True" />
                                        <asp:ListItem Value="1" Text="Yes" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSREthnic" runat="server" Text="Ethnic"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSREthnic" runat="server" Width="300px" HighlightTemplatedItems="True"
                                        MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboSREthnic_ItemDataBound"
                                        OnItemsRequested="cboSREthnic_ItemsRequested">
                                        <FooterTemplate>
                                            Note : Show max 30 result
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px">
                                    <asp:RequiredFieldValidator ID="rfvSREthnic" runat="server" ErrorMessage="Ethnic required."
                                        ValidationGroup="entry" ControlToValidate="cboSREthnic" SetFocusOnError="True"
                                        Width="20px" Visible="False">
                                        <asp:Image ID="Image17" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSREducation" runat="server" Text="Education"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSREducation" runat="server" Width="300px" HighlightTemplatedItems="True"
                                        MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboSREducation_ItemDataBound"
                                        OnItemsRequested="cboSREducation_ItemsRequested">
                                        <FooterTemplate>
                                            Note : Show max 30 result
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRMaritalStatus" runat="server" Text="Marital Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRMaritalStatus" runat="server" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px">
                                    <asp:RequiredFieldValidator ID="rfvSRMaritalStatus" runat="server" ErrorMessage="Marital Status required."
                                        ValidationGroup="entry" ControlToValidate="cboSRMaritalStatus" SetFocusOnError="True"
                                        Width="20px" Visible="False">
                                        <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRPatientLanguage" runat="server" Text="Language"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRPatientLanguage" runat="server" Width="304px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px">
                                    <asp:RequiredFieldValidator ID="rfvSRPatientLanguage" runat="server" ErrorMessage="Language required."
                                        ValidationGroup="entry" ControlToValidate="cboSRPatientLanguage" SetFocusOnError="True"
                                        Width="20px" Visible="False">
                                        <asp:Image ID="Image18" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Family Register No
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFamilyRegisterNo" runat="server" Width="300px" />
                                </td>
                                <td width="10px" />
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRNationality" runat="server" Text="Nationality"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRNationality" runat="server" Width="300px" HighlightTemplatedItems="True"
                                        MarkFirstMatch="true" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboSRNationality_ItemDataBound"
                                        OnItemsRequested="cboSRNationality_ItemsRequested">
                                        <FooterTemplate>
                                            Note : Show max 30 result
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px">
                                    <asp:RequiredFieldValidator ID="rfvSRNationality" runat="server" ErrorMessage="Nationality required."
                                        ValidationGroup="entry" ControlToValidate="cboSRNationality" SetFocusOnError="True"
                                        Width="20px" Visible="False">
                                        <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRPatientCategory" runat="server" Text="Patient Category"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRPatientCategory" runat="server" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRReligion" runat="server" Text="Religion"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRReligion" runat="server" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px">
                                    <asp:RequiredFieldValidator ID="rfvSRReligion" runat="server" ErrorMessage="Religion required."
                                        ValidationGroup="entry" ControlToValidate="cboSRReligion" SetFocusOnError="True"
                                        Width="20px" Visible="False">
                                        <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblValuesOfTrust" runat="server" Text="Values of Trust"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtValuesOfTrust" runat="server" Width="300px" MaxLength="1000" TextMode="MultiLine">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRMedicalFileBin" runat="server" Text="Medical File Bin"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRMedicalFileBin" runat="server" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRMedicalFileStatus" runat="server" Text="Medical File Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRMedicalFileStatus" runat="server" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="pgOffice">
            <table width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSROccupation" runat="server" Text="Occupation"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSROccupation" runat="server" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCompany" runat="server" Text="Company"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtCompany" runat="server" Width="300px" MaxLength="100">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Employee No
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEmployeeNo" runat="server" Width="300px" MaxLength="100">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Job Title
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEmployeeJobTitleName" runat="server" Width="300px" MaxLength="100">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Departement
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtEmployeeJobDepartementName" runat="server" Width="300px"
                                        MaxLength="100">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label6" runat="server" Text="Company Address"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtCompanyAddress" runat="server" Width="300px" TextMode="MultiLine">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRParentSpouseOccupation" runat="server" Text="Spouse Occupation"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRParentSpouseOccupation" runat="server" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblParentSpouseOccupationDesc" runat="server" Text="Spouse Occupation Description"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtParentSpouseOccupationDesc" runat="server" Width="300px"
                                        MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <asp:Panel runat="server" ID="pnlFatherOccupation">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label4" runat="server" Text="Father Occupation"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboSRFatherOccupation" runat="server" Width="300px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="10px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label5" runat="server" Text="Father Occupation Description"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtFatherOccupationDesc" runat="server" Width="300px" MaxLength="50">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="10px"></td>
                                    <td></td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblMotherOccupation" runat="server" Text="Mother Occupation"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRMotherOccupation" runat="server" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblMotherOccupationDesc" runat="server" Text="Mother Occupation Description"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtMotherOccupationDesc" runat="server" Width="300px" MaxLength="50">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="pgOther">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblHealthcareName" runat="server" Text="Healthcare Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtHealthcareName" runat="server" Width="300px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblLastVisitDate" runat="server" Text="Last Visit Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtLastVisitDate" runat="server" Width="100px">
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNumberOfVisit" runat="server" Text="Number Of Visit"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtNumberOfVisit" runat="server" Width="100px">
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOldMedicalNo" runat="server" Text="Old Medical No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOldMedicalNo" runat="server" Width="100px" MaxLength="20">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAccountNo" runat="server" Text="Account No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtAccountNo" runat="server" Width="100px" MaxLength="20">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label"></td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsBlackList" Text="Blacklist" runat="server" Enabled="False" />
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIsNotPaidOff" Text="Have Outstanding Transaction (OPR)" runat="server"
                                                    Visible="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label"></td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsAlive" Text="Deceased" runat="server" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Deceased Date</td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtDeceasedDate" runat="server" Width="100px"></telerik:RadDatePicker>
                                    <telerik:RadTimePicker ID="txtDeceasedTime" runat="server" Width="100px"></telerik:RadTimePicker>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr style="display: none">
                                <td class="label">
                                    <asp:Label ID="lblMemberID" runat="server" Text="Member Package" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboMemberID" runat="server" Width="300px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td class="label"></td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsDonor" Text="Donor" runat="server" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNumberOfDonor" runat="server" Text="Number Of Donor"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtNumberOfDonor" runat="server" Width="100px">
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblLastDonorDate" runat="server" Text="Last Donor Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtLastDonorDate" runat="server" Width="100px">
                                    </telerik:RadDatePicker>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDignosticNo" runat="server" Text="Radiology No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtDiagnosticNo" runat="server" Width="300px" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblInformation" runat="server" Text="Information From" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboInformation" runat="server" Width="300px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine"
                                        MaxLength="4000">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label"></td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr runat="server" id="trSyncDukcapil">
                                <td class="label"></td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkSyncDukcapil" Text="Sync With DUKCAPIL" runat="server" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgAllergy" runat="server">
            <telerik:RadGrid ID="grdPatientAllergy" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnNeedDataSource="grdPatientAllergy_NeedDataSource">
                <MasterTableView DataKeyNames="ItemID" GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="Group" HeaderText="Group "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="Group" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn DataField="StandardReferenceID" Visible="False" UniqueName="StandardReferenceID">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ItemID" Visible="False" UniqueName="ItemID">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Allergen Name" SortExpression="ItemName"
                            UniqueName="ItemName">
                            <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Description &amp; Reaction" UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <telerik:RadTextBox ID="txtAllergenDesc" runat="server" Width="900" MaxLength="4000"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "DescAndReaction") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="600px"></HeaderStyle>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true" AllowGroupExpandCollapse="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgImmunization" runat="server">
            <telerik:RadGrid ID="grdImunizationHist" runat="server" Width="100%" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                GridLines="None" OnNeedDataSource="grdImunizationHist_NeedDataSource" OnItemDataBound="grdImunizationHist_ItemDataBound">
                <MasterTableView DataKeyNames="ImmunizationID" ShowHeader="True" ShowHeadersWhenNoRecords="True">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ImmunizationID" Display="False" UniqueName="ImmunizationID" />
                        <telerik:GridBoundColumn DataField="MaxCount" Display="False" UniqueName="MaxCount" />
                        <telerik:GridBoundColumn DataField="ReferenceNo" Display="False" UniqueName="ReferenceNo" />
                        <telerik:GridBoundColumn DataField="Date_01" Display="False" UniqueName="Date_01" />
                        <telerik:GridBoundColumn DataField="Date_02" Display="False" UniqueName="Date_02" />
                        <telerik:GridBoundColumn DataField="Date_03" Display="False" UniqueName="Date_03" />
                        <telerik:GridBoundColumn DataField="Date_04" Display="False" UniqueName="Date_04" />
                        <telerik:GridBoundColumn DataField="Date_05" Display="False" UniqueName="Date_05" />
                        <telerik:GridBoundColumn DataField="Date_06" Display="False" UniqueName="Date_06" />
                        <telerik:GridBoundColumn DataField="Date_07" Display="False" UniqueName="Date_07" />
                        <telerik:GridBoundColumn DataField="Date_08" Display="False" UniqueName="Date_08" />
                        <telerik:GridBoundColumn DataField="Date_09" Display="False" UniqueName="Date_09" />
                        <telerik:GridBoundColumn DataField="Date_10" Display="False" UniqueName="Date_10" />
                        <telerik:GridBoundColumn DataField="IsChecked_01" Display="False" UniqueName="IsChecked_01" />
                        <telerik:GridBoundColumn DataField="IsChecked_02" Display="False" UniqueName="IsChecked_02" />
                        <telerik:GridBoundColumn DataField="IsChecked_03" Display="False" UniqueName="IsChecked_03" />
                        <telerik:GridBoundColumn DataField="IsChecked_04" Display="False" UniqueName="IsChecked_04" />
                        <telerik:GridBoundColumn DataField="IsChecked_05" Display="False" UniqueName="IsChecked_05" />
                        <telerik:GridBoundColumn DataField="IsChecked_06" Display="False" UniqueName="IsChecked_06" />
                        <telerik:GridBoundColumn DataField="IsChecked_07" Display="False" UniqueName="IsChecked_07" />
                        <telerik:GridBoundColumn DataField="IsChecked_08" Display="False" UniqueName="IsChecked_08" />
                        <telerik:GridBoundColumn DataField="IsChecked_09" Display="False" UniqueName="IsChecked_09" />
                        <telerik:GridBoundColumn DataField="IsChecked_10" Display="False" UniqueName="IsChecked_10" />

                        <telerik:GridBoundColumn DataField="ImmunizationName" UniqueName="ImmunizationName" HeaderText="Immunization" HeaderStyle-Width="200px" />
                        <telerik:GridTemplateColumn UniqueName="InputDate_01" HeaderText="1">
                            <HeaderStyle Width="180px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td rowspan="2">
                                            <asp:CheckBox runat="server" ID="chkDate_01" /></td>
                                        <td></td>
                                        <td>
                                            <telerik:RadMonthYearPicker ID="txtMonthYear_01" runat="server" Width="110px" />
                                        </td>
                                    </tr>

                                </table>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="InputDate_02" HeaderText="2">
                            <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td rowspan="2">
                                            <asp:CheckBox runat="server" ID="chkDate_02" /></td>
                                        <td>
                                            <telerik:RadMonthYearPicker ID="txtMonthYear_02" runat="server" Width="110px" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="InputDate_03" HeaderText="3">
                            <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td rowspan="2">
                                            <asp:CheckBox runat="server" ID="chkDate_03" /></td>
                                        <td>
                                            <telerik:RadMonthYearPicker ID="txtMonthYear_03" runat="server" Width="110px" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="InputDate_04" HeaderText="4">
                            <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td rowspan="2">
                                            <asp:CheckBox runat="server" ID="chkDate_04" /></td>
                                        <td>
                                            <telerik:RadMonthYearPicker ID="txtMonthYear_04" runat="server" Width="110px" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="InputDate_05" HeaderText="5">
                            <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td rowspan="2">
                                            <asp:CheckBox runat="server" ID="chkDate_05" /></td>
                                        <td>
                                            <telerik:RadMonthYearPicker ID="txtMonthYear_05" runat="server" Width="110px" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="InputDate_06" HeaderText="6">
                            <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td rowspan="2">
                                            <asp:CheckBox runat="server" ID="chkDate_06" /></td>
                                        <td>
                                            <telerik:RadMonthYearPicker ID="txtMonthYear_06" runat="server" Width="110px" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="InputDate_07" HeaderText="7">
                            <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td rowspan="2">
                                            <asp:CheckBox runat="server" ID="chkDate_07" /></td>
                                        <td>
                                            <telerik:RadMonthYearPicker ID="txtMonthYear_07" runat="server" Width="110px" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="InputDate_08" HeaderText="8">
                            <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td rowspan="2">
                                            <asp:CheckBox runat="server" ID="chkDate_08" /></td>
                                        <td>
                                            <telerik:RadMonthYearPicker ID="txtMonthYear_08" runat="server" Width="110px" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="InputDate_09" HeaderText="9">
                            <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td rowspan="2">
                                            <asp:CheckBox runat="server" ID="chkDate_09" /></td>
                                        <td>
                                            <telerik:RadMonthYearPicker ID="txtMonthYear_09" runat="server" Width="110px" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="InputDate_10" HeaderText="10">
                            <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td rowspan="2">
                                            <asp:CheckBox runat="server" ID="chkDate_10" /></td>
                                        <td>
                                            <telerik:RadMonthYearPicker ID="txtMonthYear_10" runat="server" Width="110px" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="BlankCol" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                    <Resizing AllowColumnResize="False" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgDialysis" runat="server">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <table width="100%">
                                        <tr>
                                            <td class="labelcaption">
                                                <asp:Label ID="lblGeneralInformation" runat="server" Text="General Information"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblInitialDiagnosis" runat="server" Text="Initial Diagnosis" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtInitialDiagnosis" runat="server" Width="300px" MaxLength="255" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRefferingHospital" runat="server" Text="Reffering Hospital" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtRefferingHospital" runat="server" Width="300px" MaxLength="255" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRefferingPhysician" runat="server" Text="Reffering Physician" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtRefferingPhysician" runat="server" Width="300px" MaxLength="255" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <table width="100%">
                                        <tr>
                                            <td class="labelcaption">
                                                <asp:Label ID="lblExamResult" runat="server" Text="Examination Result"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblHb" runat="server" Text="Hb" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtHb" runat="server" Width="200px" MaxLength="255" />
                                    <telerik:RadDatePicker ID="txtHbDate" runat="server" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblUrea" runat="server" Text="Urea" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtUrea" runat="server" Width="200px" MaxLength="255" />
                                    <telerik:RadDatePicker ID="txtUreaDate" runat="server" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCreatinine" runat="server" Text="Creatinine" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtCreatinine" runat="server" Width="200px" MaxLength="255" />
                                    <telerik:RadDatePicker ID="txtCreatinineDate" runat="server" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblHBsAg" runat="server" Text="HBsAg" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtHBsAg" runat="server" Width="200px" MaxLength="255" />
                                    <telerik:RadDatePicker ID="txtHBsAgDate" runat="server" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAntiHCV" runat="server" Text="Anti HCV" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtAntiHCV" runat="server" Width="200px" MaxLength="255" />
                                    <telerik:RadDatePicker ID="txtAntiHCVDate" runat="server" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAntiHIV" runat="server" Text="Anti HIV" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtAntiHIV" runat="server" Width="200px" MaxLength="255" />
                                    <telerik:RadDatePicker ID="txtAntiHIVDate" runat="server" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblKidneyUltrasound" runat="server" Text="Kidney Ultrasound" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtKidneyUltrasound" runat="server" Width="200px" MaxLength="255" />
                                    <telerik:RadDatePicker ID="txtKidneyUltrasoundDate" runat="server" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblECHO" runat="server" Text="ECHO" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtECHO" runat="server" Width="200px" MaxLength="255" />
                                    <telerik:RadDatePicker ID="txtECHODate" runat="server" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <table width="100%">
                                        <tr>
                                            <td class="labelcaption">
                                                <asp:Label ID="lblHemodialisa" runat="server" Text="Hemodialisa"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFirstHemodialysisDate" runat="server" Text="First Hemodialysis Date" />
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtFirstHemodialysisDate" runat="server" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTransferToHD" runat="server" Text="Date of transfer to HD" />
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtTransferHDDate" runat="server" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <table width="100%">
                                        <tr>
                                            <td class="labelcaption">
                                                <asp:Label ID="lblPeritonealDialysis" runat="server" Text="Peritoneal"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFirstPeritonealDialysisDate" runat="server" Text="First Peritoneal Dialysis Date" />
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtFirstPeritonealDialysisDate" runat="server" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTransferToPD" runat="server" Text="Date of transfer to PD" />
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtTransferPDDate" runat="server" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <table width="100%">
                                        <tr>
                                            <td class="labelcaption">
                                                <asp:Label ID="lblKidneyTransplant" runat="server" Text="Kidney Transplant"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblKidneyTransplantDate" runat="server" Text="Kidney Transplant Date" />
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtKidneytransplantDate" runat="server" Width="100px" />
                                </td>
                                <td width="10px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgVisite" runat="server">
            <telerik:RadGrid ID="grdVisite" runat="server" AutoGenerateColumns="False" GridLines="None"
                OnNeedDataSource="grdVisite_NeedDataSource" OnDetailTableDataBind="grdVisite_DetailTableDataBind">
                <MasterTableView DataKeyNames="PaymentNo,ItemID" GroupLoadMode="Client" AllowPaging="true"
                    PageSize="8">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="PaymentNo" HeaderText="Payment No "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="PaymentNo" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn DataField="PaymentNo" UniqueName="PaymentNo" HeaderText="Payment No"
                            SortExpression="PaymentNo" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" SortExpression="ItemName"
                            UniqueName="ItemName" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="VisiteQty" HeaderText="Visite Qty"
                            UniqueName="VisiteQty" SortExpression="VisiteQty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RealizationQty" HeaderText="Realization Qty"
                            UniqueName="RealizationQty" SortExpression="RealizationQty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail" DataKeyNames="RegistrationNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" HeaderText="Registration No"
                                    SortExpression="RegistrationNo" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RegistrationDate"
                                    HeaderText="Date" UniqueName="RegistrationDate" SortExpression="RegistrationDate"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="RegistrationTime" HeaderText="Time"
                                    UniqueName="RegistrationTime" SortExpression="RegistrationTime" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true" AllowGroupExpandCollapse="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
