<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PatientBirthRecordDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientBirthRecordDetail" %>

<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="Address" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadWindow ID="winRegistration" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtMotherRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
            function openWinRegistrationMotherInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtMotherRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationMotherInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
            function openWinRegistration() {
                var oWnd = $find("<%= winRegistration.ClientID %>");
                oWnd.setUrl('../Registration/MotherRegistrationList.aspx');
                oWnd.show();
            }
            function onClientClose(oWnd, args) {
                var txtRefNo = $find("<%= txtMotherRegistrationNo.ClientID %>");

                if (oWnd.argument)
                    txtRefNo.set_value(oWnd.argument.regno);
            }
        </script>

    </telerik:RadCodeBlock>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td colspan="3" class="labelcaption">
                            <asp:Label ID="lblBabyInfo" runat="server" Text="Baby Information" Font-Bold="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBabyRegistrationNo" runat="server" Text="Registration No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBabyRegistrationNo" runat="server" Width="300px" ReadOnly="true" />
                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtBabyRegistrationNo"
                                    Text=""></asp:Label>&nbsp; </a>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBabyMedicalNo" runat="server" Text="Medical No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBabyMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBabyName" runat="server" Text="Name" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBabyName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBabyPhysician" runat="server" Text="Physician" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBabyPhysician" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBabyServiceUnit" runat="server" Text="Service Unit" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBabyServiceUnit" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChildNo" runat="server" Text="Child No" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtChildNo" runat="server" Width="100px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20px" />
                        <asp:RequiredFieldValidator ID="rfvChildNo" runat="server" ErrorMessage="Child No required."
                            ValidationGroup="entry" ControlToValidate="txtChildNo" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td colspan="3" class="labelcaption">
                            <asp:Label ID="lblMotherInformation" runat="server" Text="Mother Information" Font-Bold="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMotherRegistrationNo" runat="server" Text="Registration No" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtMotherRegistrationNo" runat="server" Width="300px" ShowButton="true"
                                            MaxLength="20" AutoPostBack="true" OnTextChanged="txtMotherRegistrationNo_TextChanged"
                                            ClientEvents-OnButtonClick="openWinRegistration" />
                                    </td>
                                    <td>
                                        <a href="javascript:void(0);" onclick="javascript:openWinRegistrationMotherInfo();"
                                            class="noti_Container">
                                            <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationMotherInfo" AssociatedControlID="txtMotherRegistrationNo"
                                                Text=""></asp:Label>&nbsp; </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMotherMedicalNo" runat="server" Text="Medical No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMotherMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMotherName" runat="server" Text="Name" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMotherName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAgeYear" runat="server" ReadOnly="true" Width="50px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>&nbsp;Y&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAgeMonth" runat="server" ReadOnly="true" Width="50px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>&nbsp;M&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAgeDay" runat="server" ReadOnly="true" Width="50px">
                                            <NumberFormat DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>&nbsp;D
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClusterID" runat="server" Text="Service Unit" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip runat="server" ID="tabDetail" MultiPageID="mpgDetail" ShowBaseLine="true">
        <Tabs>
            <telerik:RadTab Text="Baby Birth Detail" runat="server" PageViewID="pgBaby" Selected="True" />
            <telerik:RadTab Text="Father Information" runat="server" PageViewID="pgFather" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="mpgDetail" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView runat="server" ID="pgBaby">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBirthDate" runat="server" Text="Date Of Birth" />
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtBirthDate" runat="server" Width="100px" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvBirthDate" runat="server" ErrorMessage="Date Of Birth required."
                                        ValidationGroup="entry" ControlToValidate="txtBirthDate" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBirthTime" runat="server" Text="Time Of Birth" />
                                </td>
                                <td class="entry">
                                    <telerik:RadMaskedTextBox ID="txtBirthTime" runat="server" Mask="<00..23>:<00..59>"
                                        PromptChar="_" RoundNumericRanges="false" Width="100px" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvBirthTime" runat="server" ErrorMessage="Time Of Birth required."
                                        ValidationGroup="entry" ControlToValidate="txtBirthTime" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBornAt" runat="server" Text="Born At" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRBornAt" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvBornAt" runat="server" ErrorMessage="Born At required."
                                        ValidationGroup="entry" ControlToValidate="cboSRBornAt" SetFocusOnError="True"
                                        Width="100%" Visible="False">
                                        <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDescription" runat="server" Text="Description" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBirth" runat="server" Text="Birth" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboBirth" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvBirth" runat="server" ErrorMessage="Birth required."
                                        ValidationGroup="entry" ControlToValidate="cboBirth" SetFocusOnError="True" Width="100%" Visible="False">
                                        <asp:Image ID="Image23" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTwinNumber" runat="server" Text="Twin Number" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtTwinNumber" runat="server" Width="300px" MaxLength="1" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBirthMethod" runat="server" Text="Birth Method" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboBirthMethod" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvBirthMethod" runat="server" ErrorMessage="Birth Method required."
                                        ValidationGroup="entry" ControlToValidate="cboBirthMethod" SetFocusOnError="True"
                                        Width="100%" Visible="False">
                                        <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label4" runat="server" Text="Indication" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRBirthIndication" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20px"></td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCaesarMethod" runat="server" Text="Caesar Method" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboCaesarMethod" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBornCondition" runat="server" Text="Born Condition" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboBornCondition" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvBornCondition" runat="server" ErrorMessage="Born Condition required."
                                        ValidationGroup="entry" ControlToValidate="cboBornCondition" SetFocusOnError="True"
                                        Width="100%" Visible="False">
                                        <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" Text="Complication" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRBirthComplication" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvBirthComplication" runat="server" ErrorMessage="Complication required."
                                        ValidationGroup="entry" ControlToValidate="cboSRBirthComplication" SetFocusOnError="True"
                                        Width="100%" Visible="False">
                                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" Text="Death Condition" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRDeathCondition" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20px"></td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label3" runat="server" Text="Born Death" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRBornDeath" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20px"></td>
                                <td />
                            </tr>
                            <tr>
                               <td class="label"></td> 
                               <td class="entry">
                                   <asp:CheckBox ID="chkIsKangarooMethod" runat="server" Text="BBLR Given Kangaroo Method Care" />
                               </td>
                               <td width="20px"></td>
                               <td />
                           </tr>
                           <tr>
                               <td class="label"></td> 
                               <td class="entry">
                                   <asp:CheckBox ID="chkIsIMD" runat="server" Text="Newborn Baby Undergo IMD" />
                               </td>
                               <td width="20px"></td>
                               <td />
                           </tr>
                           <tr>
                               <td class="label"></td> 
                               <td class="entry">
                                   <asp:CheckBox ID="chkIsCongenitalHyperthyroidism" runat="server" Text="Newborns Screened for Congenital Hyperthyroidism" />
                               </td>
                               <td width="20px"></td>
                               <td />
                           </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBirthPregnancyAge" runat="server" Text="Birth Pregnancy Age" />
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtBirthPregnancyAge" runat="server" Width="100px" />
                                            </td>
                                            <td>&nbsp;Week(s)
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvBirthPregnancyAge" runat="server" ErrorMessage="Birth Pregnancy Age required."
                                        ValidationGroup="entry" ControlToValidate="txtBirthPregnancyAge" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblLength" runat="server" Text="Body Length" />
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtLength" runat="server" Width="100px" />
                                            </td>
                                            <td>&nbsp;Cm
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvLength" runat="server" ErrorMessage="Body Length required."
                                        ValidationGroup="entry" ControlToValidate="txtLength" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblWeight" runat="server" Text="Body Weight" />
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtWeight" runat="server" Width="100px" />
                                            </td>
                                            <td>&nbsp;Gram
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvWeight" runat="server" ErrorMessage="Body Weight required."
                                        ValidationGroup="entry" ControlToValidate="txtWeight" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblHeadCircumference" runat="server" Text="Head Circumference" />
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtHeadCircumference" runat="server" Width="100px" />
                                            </td>
                                            <td>&nbsp;Cm
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvHeadCircumference" runat="server" ErrorMessage="Head Circumference required."
                                        ValidationGroup="entry" ControlToValidate="txtHeadCircumference" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image18" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblChestCircumference" runat="server" Text="Chest Circumference" />
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtChestCircumference" runat="server" Width="100px" />
                                            </td>
                                            <td>&nbsp;Cm
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvChestCircumference" runat="server" ErrorMessage="Chest Circumference required."
                                        ValidationGroup="entry" ControlToValidate="txtChestCircumference" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAbdomenCircumference" runat="server" Text="Abdomen Circumference" />
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtAbdomenCircumference" runat="server" Width="100px" />
                                            </td>
                                            <td>&nbsp;Cm
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAPGARScore1" runat="server" Text="APGAR Score 1" />
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtAPGARScore1" runat="server" Width="100px" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvAPGARScore1" runat="server" ErrorMessage="APGAR Score 1 required."
                                        ValidationGroup="entry" ControlToValidate="txtAPGARScore1" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAPGARScore2" runat="server" Text="APGAR Score 2" />
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtAPGARScore2" runat="server" Width="100px" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvAPGARScore2" runat="server" ErrorMessage="APGAR Score 2 required."
                                        ValidationGroup="entry" ControlToValidate="txtAPGARScore2" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAPGARScore3" runat="server" Text="APGAR Score 3" />
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtAPGARScore3" runat="server" Width="100px" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvAPGARScore3" runat="server" ErrorMessage="APGAR Score 3 required."
                                        ValidationGroup="entry" ControlToValidate="txtAPGARScore3" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image17" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblEyesSmeared" runat="server" Text="Eyes Smeared" />
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="rblEyesSmeared" runat="server" RepeatDirection="Horizontal"
                                                    OnTextChanged="rblEyesSmeared_OnTextChanged" AutoPostBack="true">
                                                    <asp:ListItem Selected="true">No</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td style="width: 5px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtEyesSmearedNotes" runat="server" Width="195px" MaxLength="100" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAnusExamined" runat="server" Text="Anus Examined" />
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="rblAnusExamined" runat="server" RepeatDirection="Horizontal"
                                                    OnTextChanged="rblAnusExamined_OnTextChanged" AutoPostBack="true">
                                                    <asp:ListItem Selected="true">No</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td style="width: 5px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtAnusExaminedNotes" runat="server" Width="195px" MaxLength="100" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNotes" runat="server" Text="Notes" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCertNumber" runat="server" Text="Certificate Number" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtCertNumber" runat="server" Width="300px" />
                                </td>
                                <td width="20px" />
                                <td />
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadTabStrip ID="tabDetailPhysician" runat="server" MultiPageID="mpgDetailPhysician">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Birth Attendants" Selected="True" PageViewID="pgBirthAttendants">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="mpgDetailPhysician" runat="server" BorderStyle="Solid"
                SelectedIndex="0" BorderColor="Gray">
                <telerik:RadPageView ID="pgBirthAttendants" runat="server">
                    <table width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <telerik:RadGrid ID="grdBirthAttendants" runat="server" OnNeedDataSource="grdBirthAttendants_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdBirthAttendants_UpdateCommand"
                                    OnDeleteCommand="grdBirthAttendants_DeleteCommand" OnInsertCommand="grdBirthAttendants_InsertCommand">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo,ParamedicID">
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle CssClass="MyImageButton" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn DataField="ParamedicID" HeaderText="ID" UniqueName="ParamedicID"
                                                SortExpression="ItemID">
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Paramedic" UniqueName="ParamedicName"
                                                SortExpression="ParamedicName">
                                                <HeaderStyle HorizontalAlign="Left" Width="350px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ParamedicType" HeaderText="Paramedic Type" UniqueName="ParamedicType"
                                                SortExpression="ParamedicType">
                                                <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MidwivesType" HeaderText="Midwives Type" UniqueName="MidwivesType"
                                                SortExpression="MidwivesType">
                                                <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                                SortExpression="Notes">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings UserControlName="BirthAttendantsDetail.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="BirthAttendantsEditCommand">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <FilterMenu>
                                    </FilterMenu>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Resizing AllowColumnResize="True" />
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="pgFather">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFatherName" runat="server" Text="Father Name" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtFatherName" runat="server" Width="300px" />
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvFatherName" runat="server" ErrorMessage="Father Name required."
                                        ValidationGroup="entry" ControlToValidate="txtFatherName" SetFocusOnError="True"
                                        Width="100%" Visible="False">
                                        <asp:Image ID="Image19" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSSN" runat="server" Text="Father SSN" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtSSN" runat="server" Width="300px" />
                                </td>
                                <td width="20px" />
                                <td />
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblFatherBirthDate" runat="server" Text="Date Of Birth" />
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtFatherBirthDate" runat="server" Width="100px" />
                                    </td>
                                    <td width="20px"></td>
                                    <td />
                                </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOccupation" runat="server" Text="Father Occupation" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboOccupation" runat="server" Width="300px" />
                                </td>
                                <td width="20px"></td>
                                <td />
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <uc1:Address ID="ctlAddress" runat="server" />
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
