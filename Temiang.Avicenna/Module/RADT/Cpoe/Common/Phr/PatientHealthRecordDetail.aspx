<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="PatientHealthRecordDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Emr.Phr.PatientHealthRecordDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/PHR/PhrCtl.ascx" TagPrefix="uc1" TagName="PhrCtl" %>
<%@ Register Src="~/CustomControl/VitalSignInfoCtl.ascx" TagPrefix="cc" TagName="VitalSignInfoCtl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <style>
            /*Pain Scale Image*/
            .ps00 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: 0px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps01 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-repeat: no-repeat;
                background-position: -49px 0px;
                height: 44px;
                width: 44px;
                display: block;
                cursor: pointer;
            }

            .ps02 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -98px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps03 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -147px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps04 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -194px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps05 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -243px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps06 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -292px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps07 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -340px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps08 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -388px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps09 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -437px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }

            .ps10 {
                background-image: url("<%=Helper.UrlRoot() %>/Images/Medical/PainScale.png");
                background-position: -485px 0px;
                height: 44px;
                width: 44px;
                cursor: pointer;
            }
            /* End Pain Scale Image*/

            .RadComboBox {
                vertical-align: top;
            }
        </style>

        <script src='<%=Page.ResolveUrl("~/JavaScript/Common/Core.js")%>' type="text/javascript"></script>

        <script type="text/javascript">
            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            function openVitalSignChart(vitalSignID) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/VitalSignChart.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&fregno=<%= FromRegistrationNo %>&vid=' + vitalSignID;
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }
        </script>

        <script src='<%=Page.ResolveUrl("~/JavaScript/autosize.js")%>' type="text/javascript"></script>
        <script type="text/javascript">
            window.onload = function () {
                var ta = $('textarea');
                autosize(ta);

                // Call the update method to recalculate the size:
                autosize.update(ta);
            }
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="600px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <asp:HiddenField runat="server" ID="hdnTransactionNo" Value="" />
    <asp:HiddenField runat="server" ID="hdnPatientID" />
    <asp:HiddenField runat="server" ID="hdnCreateByUserID" />
    <asp:HiddenField runat="server" ID="hdnQuestionFormID" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="500px" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactioNo" runat="server" Text="No" />
                        </td>
                        <td style="width: 300px" >
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="160px" ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRecordDate" runat="server" Text="Examination Time" />
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtRecordDate" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtRecordTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRecordDate" runat="server" ErrorMessage="Vital Sign Date required."
                                ValidationGroup="entry" ControlToValidate="txtRecordDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblQuestionForm" runat="server" Text="Form Name" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtFormName" runat="server" Width="300px" ReadOnly="true" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Form required."
                                ValidationGroup="entry" ControlToValidate="txtFormName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Medical No / Registration No" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="150px"
                                ReadOnly="true" />
                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                    Text=""></asp:Label>
                                &nbsp; </a>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name" />
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="30px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="240px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="25px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSex" runat="server" Text="Sex" />
                        </td>
                        <td>
                            <asp:RadioButton ID="optSexFemale" runat="server" Text="Female" GroupName="Sex" Enabled="false" />
                            <asp:RadioButton ID="optSexMale" runat="server" Text="Male" GroupName="Sex" Enabled="false" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtAgeYear" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeMonth" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeDay" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;D
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="NIK" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtSsn" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
            <td width="500px" valign="top">
                <table width="100%" runat="server" id="tblOtherInfo">

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedic" runat="server" Text="Physician" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblParamedicName" runat="server" />
                        </td>
                        <td width="20" />
                        <td />

                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblExaminer" runat="server" Text="Examiner" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboExaminerID" Width="304px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboExaminerID_ItemDataBound"
                                DataTextField="ParamedicName" DataValueField="ParamedicID" OnItemsRequested="cboExaminerID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblGuarantorName" runat="server" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblResponsible" runat="server" Text=" Responsible Name" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtResponsible" runat="server" Width="300px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblResponsibleName" runat="server" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="300px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblPhoneNumber" runat="server" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>

                    <%--                    <tr>
                        <td class="label">
                            <asp:Label ID="lblComplaint" runat="server" Text="Chief Complaint"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtComplaint" runat="server" Width="304px" MaxLength="4000"
                                TextMode="MultiLine" Height="45px" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>--%>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsComplete" Text="Complete" />
                            <asp:CheckBox runat="server" ID="chkIsApproved" Text="Approved" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>

                </table>
                <fieldset runat="server" id="ctlReference">
                    <legend>
                        <asp:Label ID="lblReferenceType" runat="server" Text="Operating Theater"></asp:Label></legend>
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblReferenceNo" runat="server" Text="Reference No" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="300px" ReadOnly="True" />
                            </td>
                            <td width="20" />
                            <td />

                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblReferenceNote" runat="server" Text="" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtReferenceNote" TextMode="MultiLine" ReadOnly="True" Resize="Vertical" Width="300px" />
                            </td>
                            <td width="20" />
                            <td />
                        </tr>
                    </table>
                </fieldset>
                <telerik:RadButton runat="server" ID="btnCopyLast" Text="Copy Value From Last Document" SingleClickText="Process Copy Value From Last Document" SingleClick="True" OnClick="btnCopyLast_OnClick">
                    <ConfirmSettings ConfirmText="Are you sure you want to continue?" Height="200" Title="Copy Value From Last Document" Width="300" />
                </telerik:RadButton>
            </td>
            <td width="400px" valign="top">
                <cc:VitalSignInfoCtl runat="server" ID="vitalSignInfoCtl" />
            </td>
            <td></td>
        </tr>
    </table>

    <uc1:PhrCtl runat="server" ID="phrCtl" />
</asp:Content>
