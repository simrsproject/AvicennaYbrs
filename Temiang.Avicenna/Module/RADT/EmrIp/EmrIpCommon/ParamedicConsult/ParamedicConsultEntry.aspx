<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="ParamedicConsultEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ParamedicConsultEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            function ResetPastMedicalHistory() {
                ResetValue("<%= txtPastMedicalHistory.ClientID %>", "PastMedicalHistory", "Past Medical History");
                return false;
            }
            function ResetHpi() {
                ResetValue("<%= txtHpi.ClientID %>", "HistoryOfPresentIllness", "History Of Present Illness");
                return false;
            }
            function ResetPhysicalExamination() {
                ResetValue("<%= txtActionExamTreatment.ClientID %>", "PhysicalExamination", "Physical Examination");
                return false;
            }
            function ResetValue(ctlID, method, caption) {
                if (!confirm("Reset Value " + caption + "?")) return;
                var obj = {};
                obj.patientID = "<%= PatientID %>";
                obj.registrationNo = "<%= RegistrationNo %>";
                obj.fromRegistrationNo = "<%= FromRegistrationNo %>";
                $.ajax({
                    url: '<%= Helper.UrlRoot() %>/CustomControl/PHR/PhrWebService.asmx/' + method,
                    data: JSON.stringify(obj), //ur data to be sent to server
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    dataType: "json",
                    success: function (data) {
                        //$find(ctlID).set_value(decodeURI(data.d));

                        var str = data.d;
                        if ((str === null) || (str === ''))
                            alert("Data not found");
                        else {
                            ctlID = "<%= ClientID %>_" + ctlID;
                            //$find(ctlID).set_value(data.d);
                            var ctl = document.getElementById(ctlID);

                            str = str.replaceAll('<li>', '•');
                            str = str.replaceAll('</li>', '\n');
                            str = str.replaceAll('<ul>', '');
                            str = str.replaceAll('</ul>', '');
                            str = str.replaceAll('<br />', '\n');
                            ctl.value = str.replace(/(<([^>]+)>)/ig, '');
                        }
                    },
                    error: function (x, y, z) {
                        alert(x.responseText + "  " + x.status);
                    }
                });
            }

            function editSignature1() {
                var mod = 'edit';
                var imgId = '<%=fmImage.ClientID %>';
                var txtId = '<%=hdnPhysicianSignImage.ClientID %>';
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

            function fixDangText(sender, eventArgs) {
                var s = sender.get_value();

                //s = s.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot');
                s = s.replace(/</g, '< ').replace(/>/g, '> ').replace(/  /g, ' ');
                sender.set_value(s);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winImage_ClientClose"
        ID="winImage" />


    <telerik:RadAjaxPanel runat="server" ID="pnlMain">
        <asp:HiddenField runat="server" ID="hdnConsultReferNo" />
        <asp:HiddenField runat="server" ID="hdnConsultReferNoPrev" />
        <table width="100%">
            <tr>
                <td style="width: 500px; vertical-align: top;">
                    <table width="100%">
                        <tr>
                            <td class="label">Consult / Refer Type
                            </td>
                            <td class="entry">
                                <asp:RadioButtonList ID="optConsultReferType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                    OnSelectedIndexChanged="optConsultReferType_OnSelectedIndexChanged">
                                    <asp:ListItem Text="Consult" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="Refer to Other Service Unit" Value="R"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Consult / Refer Type required."
                                    ValidationGroup="entry" ControlToValidate="optConsultReferType" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Date / Time
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtConsultDate" runat="server" Width="100px" Enabled="False" />
                                &nbsp;
                            <telerik:RadTimePicker ID="txtConsultTime" runat="server" Width="70px" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trReferServiceUnit">
                            <td class="label">To Service Unit
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="304px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged" HighlightTemplatedItems="True"
                                    MarkFirstMatch="False" OnItemDataBound="cboServiceUnitID_ItemDataBound" OnItemsRequested="cboServiceUnitID_ItemsRequested"
                                    EnableLoadOnDemand="true" NoWrap="True">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Physican
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboReferToParamedicID" Width="304px" AllowCustomText="true" Visible="False"
                                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboReferToParamedicID_SelectedIndexChanged">
                                </telerik:RadComboBox>
                                <telerik:RadComboBox ID="cboConsultToParamedicID" runat="server" Width="304px" EmptyMessage="Select a Physician"
                                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                                    <WebServiceSettings Method="Paramedics" Path="~/WebService/ComboBoxDataService.asmx" />
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trReferRoom">
                            <td class="label">Room
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboRoomID" runat="server" Width="304px" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trReferQueNo">
                            <td class="label">Que No
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboQue" runat="server" Width="304px" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trReferToRegistrationNo">
                            <td class="label">Registration No</td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtToRegistrationNo" runat="server" Width="304px" MaxLength="20" ReadOnly="true" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblConsultType" runat="server" Text="Consult Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRParamedicConsultType" runat="server" Width="304px" EmptyMessage="Consult type" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblNote" runat="server" Text="Consultation Note"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" Height="200px" MaxLength="4000"
                                    TextMode="MultiLine" Resize="Vertical" ClientEvents-OnBlur="fixDangText"/>
                            </td>
                            <td width="20px" />
                        </tr>
                    </table>
                    <%--SIGN--%>
                    <div class="RowSign">
                        <div class="ColumnSign">
                            <fieldset style="width: 128px">
                                <legend>Physician Signature</legend>
                                <telerik:RadBinaryImage ID="fmImage" runat="server"
                                    Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                                <br />
                                <asp:Button runat="server" ID="btnPhysicianSign" Text="Sign" Width="300px" OnClientClick="javascript:editSignature1();return false;" />
                                <div>
                                    <asp:HiddenField runat="server" ID="hdnPhysicianSignImage" />
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </td>
                <td style="width: 500px; vertical-align: top;">
                    <fieldset>
                        <legend>ANAMNESIS</legend>
                        <table width="100%">
                            <tr>
                                <td class="label">Chief Complaint
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtChiefComplaint" runat="server" Width="300px" MaxLength="1000"
                                        TextMode="MultiLine" Resize="Vertical" ClientEvents-OnBlur="fixDangText" />
                                </td>
                                <td width="20px" />
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Past Medical History
                                    <asp:LinkButton runat="server" ID="lbtnResetPastMedicalHistory" OnClientClick="javascript:ResetPastMedicalHistory();return false;">
                                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                                    </asp:LinkButton>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPastMedicalHistory" runat="server" Width="300px" MaxLength="1000"
                                        TextMode="MultiLine" Resize="Vertical"  ClientEvents-OnBlur="fixDangText"  />
                                </td>
                                <td width="20px" />
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">History Present Illness
                                <asp:LinkButton runat="server" ID="lbtnResetHistoryOfPresentIllness" OnClientClick="javascript:ResetHpi();return false;">
                                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                                </asp:LinkButton>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtHpi" runat="server" Width="300px" MaxLength="4000"
                                        TextMode="MultiLine" Resize="Vertical" ClientEvents-OnBlur="fixDangText"/>
                                </td>
                                <td width="20px" />
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Action / Examination / Treatment
                                <asp:LinkButton runat="server" ID="lbtnResetPhysicalExamination" OnClientClick="javascript:ResetPhysicalExamination();return false;">
                                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                                </asp:LinkButton>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtActionExamTreatment" runat="server" Width="300px" MaxLength="4000" Height="205px"
                                        TextMode="MultiLine" Resize="Vertical" ClientEvents-OnBlur="fixDangText" />
                                </td>
                                <td width="20px" />
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset runat="server" id="pnlBasicFunction" visible="False">
                        <legend>BASIC FUNCTION EXAMINATION</legend>
                        <table width="100%">
                            <tr>
                                <td class="label">Active
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtActiveMotion" runat="server" Width="300px" MaxLength="2000"
                                        TextMode="MultiLine" Resize="Vertical" ClientEvents-OnBlur="fixDangText"/>
                                </td>
                                <td width="20px" />
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Passive
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPassiveMotion" runat="server" Width="300px" MaxLength="2000"
                                        TextMode="MultiLine" Resize="Vertical" ClientEvents-OnBlur="fixDangText"/>
                                </td>
                                <td width="20px" />
                                <td></td>
                            </tr>
                        </table>
                    </fieldset>

                </td>
                <td></td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>

</asp:Content>
