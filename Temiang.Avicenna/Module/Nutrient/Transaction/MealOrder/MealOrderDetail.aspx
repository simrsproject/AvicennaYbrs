<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="MealOrderDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.MealOrderDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../../../../JavaScript/DateFormat.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinPickList() {
                var oWnd = $find("<%= winPickList.ClientID %>");
                var set = $find("<%= cboSRMealSet.ClientID %>");
                var menu = $find("<%= txtMenuItemID.ClientID %>");
                var no = $find("<%= txtOrderNo.ClientID %>");

                oWnd.setUrl('MealOrderItemPickList.aspx?set=' + set.get_value() + '&menu=' + menu.get_value() + '&no=' + no.get_value());

                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd) {
                if (oWnd.argument) {
                    var val = oWnd.argument.split('|');
                    if (val[0] == 'rebind') {
                        __doPostBack("<%= grdItem.UniqueID %>", oWnd.argument);
                        oWnd.argument = 'undefined';
                    }
                }
            }

            function deleteMenu() {
                if (confirm('Are you sure to delete all optional menu?')) {
                    __doPostBack("<%= grdItem.UniqueID %>", 'clearlist');
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="820px" Height="600px" Behavior="Close, Move, Maximize"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" OnClientClose="onClientClose" ID="winPickList">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="197px" MaxLength="20"
                                ReadOnly="true" />
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
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
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="243px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="23px" ReadOnly="true" />
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
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;D
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtUnitRoomBed" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20">
                            <telerik:RadTextBox ID="txtUnitID" runat="server" Visible="False" />
                            <telerik:RadTextBox ID="txtBedID" runat="server" Visible="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomBed" runat="server" Text="Room / Bed"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRoomBed" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                            <asp:Label ID="lblClassName" runat="server" CssClass="labeldescription" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhysician" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPhysicianName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%" cellpadding="0" cellspacing="1">
        <tr>
            <td>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label2" runat="server" Text="MEAL ORDER" Font-Bold="True" Font-Size="9"></asp:Label></legend>
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50%" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblOrderNo" runat="server" Text="Order No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtOrderNo" runat="server" Width="300px" MaxLength="20" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="label">
                                            <asp:Label ID="lblDietPatientNo" runat="server" Text="Diet Patient No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtDietPatientNo" runat="server" ReadOnly="true" Width="300px" />
                                            <telerik:RadTextBox ID="txtMenuID" runat="server" ReadOnly="true" />
                                            <telerik:RadTextBox ID="txtMenuItemID" runat="server" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblDietID" runat="server" Text="Diet"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox runat="server" ID="cboDietID" Width="300px" EnableLoadOnDemand="true"
                                                HighlightTemplatedItems="true" OnItemDataBound="cboDietID_ItemDataBound" ValidationGroup="other"
                                                OnItemsRequested="cboDietID_ItemsRequested" EmptyMessage="Select..." Enabled="False">
                                                <FooterTemplate>
                                                    Note : Show max 20 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblDietNote" runat="server" Text="Diet Notes"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtDietNote" runat="server" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblMenu" runat="server" Text="Menu"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtMenu" runat="server" Width="300px" ReadOnly="True" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFormOfFood" runat="server" Text="Form Of Food"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox runat="server" ID="cboFormOfFood" Width="300px" Enabled="False">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblDiagnose" runat="server" Text="Diagnose"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtDiagnose" runat="server" Width="300px" MaxLength="250"
                                                ReadOnly="True" Height="35px" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblHeightWeight" runat="server" Text="Body Height / Weight"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtHeight" runat="server" Width="80px" MaxLength="10"
                                                MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                            Cm&nbsp;
                                            <telerik:RadNumericTextBox ID="txtWeight" runat="server" Width="80px" MaxLength="10"
                                                MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                            Kg
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBodyMassIndex" runat="server" Text="Body Mass Index"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtBodyMassIndex" runat="server" Width="80px" MaxLength="10"
                                                MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblMuac" runat="server" Text="Mid Upper Arm Circumference (LILA)"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtMuac" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="true" />
                                                    </td>
                                                    <td>&nbsp;Cm&nbsp;&nbsp;
                                                    </td>
                                                    <td class="label" style="width: 70px">
                                                        <asp:Label ID="lblUlna" runat="server" Text="Ulna Length"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtUlna" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="true" />
                                                    </td>
                                                    <td>&nbsp;Cm
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" Height="43px"
                                                ReadOnly="True" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblOrderDate" runat="server" Text="Date"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtOrderDate" runat="server" Width="100px" Enabled="False">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td class="label" style="width:100px">
                                                        <asp:Label ID="lblEffectiveDate" runat="server" Text="Order To Date"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtEffectiveDate" runat="server" Width="100px" Enabled="False">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td class="entry" colspan="3">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 20%" class="labelcaption">
                                                        <b>
                                                            <asp:Label ID="Label1" runat="server" Text="Value"></asp:Label></b>
                                                    </td>
                                                    <td style="width: 20%" class="labelcaption">
                                                        <b>
                                                            <asp:Label ID="Label3" runat="server" Text="Interval"></asp:Label></b>
                                                    </td>
                                                    <td style="width: 45%" class="labelcaption">
                                                        <b>
                                                            <asp:Label ID="Label4" runat="server" Text="Range"></asp:Label></b>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblCalorie" runat="server" Text="Calorie"></asp:Label>
                                        </td>
                                        <td class="entry" colspan="3">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 20%">
                                                        <telerik:RadNumericTextBox ID="txtCalorie" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 20%">
                                                        <telerik:RadNumericTextBox ID="txtCalorieInterval" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 45%">
                                                        <telerik:RadNumericTextBox ID="txtCalorieMin" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                        &nbsp;to&nbsp;
                                                        <telerik:RadNumericTextBox ID="txtCalorieMax" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblProtein" runat="server" Text="Protein"></asp:Label>
                                        </td>
                                        <td class="entry" colspan="3">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 20%">
                                                        <telerik:RadNumericTextBox ID="txtProtein" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 20%">
                                                        <telerik:RadNumericTextBox ID="txtProteinInterval" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 45%">
                                                        <telerik:RadNumericTextBox ID="txtProteinMin" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                        &nbsp;to&nbsp;
                                                        <telerik:RadNumericTextBox ID="txtProteinMax" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFat" runat="server" Text="Fat"></asp:Label>
                                        </td>
                                        <td class="entry" colspan="3">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 20%">
                                                        <telerik:RadNumericTextBox ID="txtFat" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 20%">
                                                        <telerik:RadNumericTextBox ID="txtFatInterval" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 45%">
                                                        <telerik:RadNumericTextBox ID="txtFatMin" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                        &nbsp;to&nbsp;
                                                        <telerik:RadNumericTextBox ID="txtFatMax" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblCarbohydrate" runat="server" Text="Carbohydrate"></asp:Label>
                                        </td>
                                        <td class="entry" colspan="3">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 20%">
                                                        <telerik:RadNumericTextBox ID="txtCarbohydrate" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 20%">
                                                        <telerik:RadNumericTextBox ID="txtCarbohydrateInterval" runat="server" Width="80px"
                                                            MaxLength="10" MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 45%">
                                                        <telerik:RadNumericTextBox ID="txtCarbohydrateMin" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                        &nbsp;to&nbsp;
                                                        <telerik:RadNumericTextBox ID="txtCarbohydrateMax" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSalt" runat="server" Text="Salt"></asp:Label>
                                        </td>
                                        <td class="entry" colspan="3">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 20%">
                                                        <telerik:RadNumericTextBox ID="txtSalt" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 20%">
                                                        <telerik:RadNumericTextBox ID="txtSaltInterval" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 45%">
                                                        <telerik:RadNumericTextBox ID="txtSaltMin" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                        &nbsp;to&nbsp;
                                                        <telerik:RadNumericTextBox ID="txtSaltMax" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFiber" runat="server" Text="Fiber"></asp:Label>
                                        </td>
                                        <td class="entry" colspan="3">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 20%">
                                                        <telerik:RadNumericTextBox ID="txtFiber" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 20%">
                                                        <telerik:RadNumericTextBox ID="txtFiberInterval" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td style="width: 45%">
                                                        <telerik:RadNumericTextBox ID="txtFiberMin" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                        &nbsp;to&nbsp;
                                                        <telerik:RadNumericTextBox ID="txtFiberMax" runat="server" Width="80px" MaxLength="10"
                                                            MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAllergies" runat="server" Text="Food Allergies "></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtAllergies" runat="server" Width="300px" ReadOnly="true"
                                                ForeColor="Red" TextMode="MultiLine" />
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
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFastingTime" runat="server" Text="Fasting Time"></asp:Label>
                        </td>
                        <td class="entry" colspan="3">
                            <asp:CheckBox ID="chkIsFastingMornig" Text="Morning" runat="server" />
                            &nbsp;&nbsp;<asp:CheckBox ID="chkIsFastingDay" Text="Day" runat="server" />
                            &nbsp;&nbsp;<asp:CheckBox ID="chkIsFastingNight" Text="Night" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRMealSet" runat="server" Text="Set"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRMealSet" runat="server" Width="300px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboSRMealSet_SelectedIndexChanged" />
                            <telerik:RadTextBox ID="txtSRMealSet" runat="server" Visible="False" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRMealSet" runat="server" ErrorMessage="Set required"
                                ValidationGroup="entry" ControlToValidate="cboSRMealSet" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdItem_DeleteCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="FoodID">
            <CommandItemTemplate>
                &nbsp;
                <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdItem.MasterTableView.IsItemInserted %>'
                    OnClientClick="javascript:openWinPickList();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />&nbsp;<asp:Label
                        runat="server" ID="lblPickList" Text="Add Optional Menu"></asp:Label>
                </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbDelete" runat="server" Visible='<%# !grdItem.MasterTableView.IsItemInserted %>'
                            OnClientClick="javascript:deleteMenu();return false;">
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/delete16.png" />&nbsp;<asp:Label
                                runat="server" ID="lblDelete" Text="Delete All Optional Menu"></asp:Label>
                        </asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="28px" />
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="FoodID" HeaderText="Food ID"
                    UniqueName="FoodID" SortExpression="FoodID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="FoodName" HeaderText="Food Name" UniqueName="FoodName"
                    SortExpression="FoodName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FoodGroupName" HeaderText="Food Group"
                    UniqueName="FoodGroupName" SortExpression="FoodGroupName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsOptional" HeaderText="Optional Menu"
                    UniqueName="IsOptional" SortExpression="IsOptional" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings>
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
