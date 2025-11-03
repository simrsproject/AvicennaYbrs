<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="ParamedicConsultAnswerEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ParamedicConsultAnswerEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            function editSignature1() {
                var mod = 'edit';
                var imgId = '<%=fmImage.ClientID %>';
                    var txtId = '<%=hdnPhysicianAnswerSignImage.ClientID %>';
                    var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=' + mod + '&imgId=' + imgId + '&txtId=' + txtId;
                    var oWnd = $find("<%= winImage.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }

            function winImage_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    document.getElementById(arg.txtId).value = arg.image;
                    var img = document.getElementById(arg.imgId);
                    img.setAttribute('src', "data:image/Png;base64," + arg.image);
                }
            }


        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winImage_ClientClose"
        ID="winImage" />
    <table width="100%">
        <tr>
            <td style="width: 470px; vertical-align: top;">
                <fieldset>
                    <legend>CONSULTATION REQUEST INFORMATION</legend>
                    <table width="100%">
                        <tr>
                            <td class="label">Type
                            </td>
                            <td class="entry">
                                <asp:RadioButtonList ID="optConsultReferType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Consult" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="Refer to Other Service Unit" Value="R" Enabled="False"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Date
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtConsultDate" runat="server" Width="100px" Enabled="False" />
                                &nbsp;
                            <telerik:RadTimePicker ID="txtConsultTime" runat="server" Width="75px" Enabled="True" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">From Registration No
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtRegistrationNo" Width="304px" ReadOnly="True"></telerik:RadTextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">From Service Unit
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtFromServiceUnitName" Width="304px" ReadOnly="True"></telerik:RadTextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">From Physican
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtFromParamedicName" Width="304px" ReadOnly="True"></telerik:RadTextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trReferServiceUnit">
                            <td class="label">To Service Unit
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtToServiceUnitName" Width="304px" ReadOnly="True"></telerik:RadTextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">To Physican
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtToParamedicName" Width="304px" ReadOnly="True"></telerik:RadTextBox>

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Consult Type
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtParamedicConsultType" Width="304px" ReadOnly="True"></telerik:RadTextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Consultation Note
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" Height="200px" MaxLength="500"
                                    TextMode="MultiLine" Resize="Vertical" ReadOnly="True" />
                            </td>
                        </tr>
                    </table>

                    <fieldset>
                        <legend>ANAMNESIS</legend>
                        <table width="100%">
                            <tr>
                                <td class="label">Chief Complaint
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtChiefComplaint" runat="server" Width="300px" MaxLength="1000"
                                        TextMode="MultiLine" Resize="Vertical" ReadOnly="True" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Past Medical History
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPastMedicalHistory" runat="server" Width="300px" MaxLength="1000"
                                        TextMode="MultiLine" Resize="Vertical" ReadOnly="True" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">History Present Illness
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtHpi" runat="server" Width="300px" MaxLength="4000"
                                        TextMode="MultiLine" Resize="Vertical" ReadOnly="true" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Action / Examination / Treatment
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtActionExamTreatment" runat="server" Width="300px" MaxLength="4000" Height="105px"
                                        TextMode="MultiLine" Resize="Vertical" ReadOnly="true" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </fieldset>
                </fieldset>
            </td>
            <td style="width: 470px; vertical-align: top;">

                <fieldset runat="server" id="answerGen">
                    <legend>CONSULTATION ANSWER</legend>
                    <table>
                        <tr>
                            <td class="label">Date
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtConsultAnswerDate" runat="server" Width="100px"  />
                                &nbsp;
                            <telerik:RadTimePicker ID="txtConsultAnswerTime" runat="server" Width="75px" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Answer Status
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRConsultAnswerType" runat="server" Width="300px" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="3" class="labelcaption">Answer Note</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txtAnswer" runat="server" Width="100%" MaxLength="2000" Height="440px"
                                    TextMode="MultiLine" Resize="Vertical" />
                            </td>
                        </tr>
                    </table>
                    <%--SIGN--%>
                    <div class="RowSign">
                        <div class="ColumnSign">
                            <fieldset style="width: 180px">
                                <legend>Physician Answer Signature</legend>
                                <telerik:RadBinaryImage ID="fmImage" runat="server"
                                    Width="180px" Height="100px" ResizeMode="Fit"></telerik:RadBinaryImage>
                                <br />
                                <asp:Button runat="server" ID="btnPhysicianAnswerSign" Text="Sign" Width="180px" OnClientClick="javascript:editSignature1();return false;" />
                                <div>
                                    <asp:HiddenField runat="server" ID="hdnPhysicianAnswerSignImage" />
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </fieldset>

                <fieldset runat="server" id="pnlBasicFunction" visible="False">
                    <legend>BASIC FUNCTION EXAMINATION</legend>
                    <table width="100%">
                        <tr>
                            <td class="label">Active
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtActiveMotion" runat="server" Width="300px" MaxLength="2000" Height="80px"
                                    TextMode="MultiLine" Resize="Vertical" ReadOnly="true" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Passive
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPassiveMotion" runat="server" Width="300px" MaxLength="2000" Height="80px"
                                    TextMode="MultiLine" Resize="Vertical" ReadOnly="true" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </fieldset>

                <fieldset runat="server" id="answerPhy">
                    <legend>PHYSIOTHERAPY OPINION</legend>
                    <table width="100%">
                        <tr>
                            <td class="label">Answer Status
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRConsultAnswerTypePhysiotherapy" runat="server" Width="300px" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Physiotherapy Problems
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAnswerPhysiotherapy" runat="server" Width="300px" MaxLength="2000" Height="120px"
                                    TextMode="MultiLine" Resize="Vertical" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Physiotherapy Diagnosis
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAnswerDiagnose" runat="server" Width="300px" MaxLength="1000" Height="80px"
                                    TextMode="MultiLine" Resize="Vertical" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Physiotherapy Action Plan
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAnswerPlan" runat="server" Width="300px" MaxLength="1000" Height="120px"
                                    TextMode="MultiLine" Resize="Vertical" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Actions and Modalities of FT
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAnswerAction" runat="server" Width="300px" MaxLength="1000" Height="80px"
                                    TextMode="MultiLine" Resize="Vertical" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
            <td></td>
        </tr>
    </table>


</asp:Content>
