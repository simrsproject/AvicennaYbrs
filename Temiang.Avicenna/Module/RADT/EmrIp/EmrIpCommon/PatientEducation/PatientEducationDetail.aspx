<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PatientEducationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.PatientEducationDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="codeBlock">
        <script type="text/javascript" language="javascript">;

            // Tidak jadi menggunakan TTD proses verif nya
            // Override master page Approval function to Verify
            //Approval = function (sender, args) {
            //    // Show Signature
            //    openVerificatorSign();
            //    args.set_cancel(true); //Prevent postback
            //}

            // Ganti judul konfirmasi
            var Approval = function (sender, args) {
                if (!window.confirm('Are you sure to verified this education?')) {
                    args.set_cancel(true);
                    return;
                }

                fw_lastTbiDisabled = args.get_item();
                fw_lastTbiDisabled.disable();
            }

            function openPatientSign() {
                var imgId = '<%=imgPatientSign.ClientID %>';
                var txtId = '<%=hdnPatientSign.ClientID %>';
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=edt&imgId=' + imgId + '&txtId=' + txtId;
                var oWnd = $find("<%= winImage.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }

            function openEducatorSign() {
                var imgId = '<%=imgEducator.ClientID %>';
                var txtId = '<%=hdnEducatorSign.ClientID %>';
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=edt&imgId=' + imgId + '&txtId=' + txtId;
                var oWnd = $find("<%= winImage.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }

<%--            // Verify use sign
            function openVerificatorSign() {
                var mod = 'edit';
                var imgId = '<%=imgVerificatorSign.ClientID %>';
                var txtId = '<%=hdnVerificatorSign.ClientID %>';
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=' + mod + '&imgId=' + imgId + '&txtId=' + txtId;
                var oWnd = $find("<%= winImage.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }--%>

            function winImage_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    document.getElementById(arg.txtId).value = arg.image;
                    var img = document.getElementById(arg.imgId);
                    img.setAttribute('src', "data:image/Png;base64," + arg.image);

                    // Tidak jadi menggunakan TTD proses verif nya
<%--                    // Update Verify
                    if (arg.txtId.includes("hdnVerificatorSign"))
                    __doPostBack("<%= grdPatientEducation.UniqueID %>", "verify");--%>
                }
            }

        </script>
    </telerik:RadCodeBlock>

    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winImage_ClientClose"
        ID="winImage" />
    <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="entry"
        ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>

    <asp:HiddenField runat="server" ID="hdfReturnValue" />
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 500px; vertical-align: top;">
                <table style="width: 100%;">
                    <tr>
                        <td class="label">No</td>
                        <td>
                            <telerik:RadTextBox ID="txtSeqNo" runat="server" Width="50px" Enabled="False" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Date Time</td>
                        <td>
                            <telerik:RadDateTimePicker ID="txtEducationDateTime" runat="server" Width="160px" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Educator</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboEducationByUserID" Width="100%" EmptyMessage="Select a Educator" Enabled="False"
                                EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" AutoPostBack="True" OnSelectedIndexChanged="cboEducationByUserID_OnSelectedIndexChanged">
                                <WebServiceSettings Method="Users" Path="~/WebService/ComboBoxDataService.asmx" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Education Type</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEducationType" runat="server" Width="100px" Enabled="False" />
                            &nbsp;&nbsp;Ref No:&nbsp;<telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="200px" Enabled="False" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Education Problem</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSREducationProblem" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Method</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSREducationMethod" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Other Method</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtMethodOther" Width="100%" Resize="Vertical">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Education Recipient</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtRecipientName" Width="100%">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Relationship</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRPatientEducationRecipient" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Evaluation</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRPatientEducationEvaluation" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Other Evaluation</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtPatientEducationEvaluationOth" Width="100%" Resize="Vertical">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Goal</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRPatientEducationGoal" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Other Goal</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtPatientEducationGoalOth" Width="100%" Resize="Vertical">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Education Duration (Minute)</td>
                        <td class="entry">
                            <telerik:RadNumericTextBox runat="server" ID="txtDuration" NumberFormat-DecimalDigits="0" Width="100px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Verificator</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtVerificator" Width="100%" ReadOnly="true">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Verification Date</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtVerifyDateTime" Width="100%" ReadOnly="true">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td>
                            <fieldset style="width: 128px">
                                <legend>Family/Patient Signature</legend>
                                <telerik:RadBinaryImage ID="imgPatientSign" runat="server"
                                    Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                                <br />
                                <asp:Button runat="server" ID="btnFmSign" Text="Sign" Width="300px" OnClientClick="javascript:openPatientSign();return false;" />
                                <div>
                                    <asp:HiddenField runat="server" ID="hdnPatientSign" />
                                </div>
                            </fieldset>
                        </td>
                        <td>
                            <%--                            <fieldset style="width: 128px">
                                <legend>Verificator Signature</legend>
                                <telerik:RadBinaryImage ID="imgVerificatorSign" runat="server"
                                    Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                                <div style="height: 24px;">
                                    <asp:Label runat="server" ID="lblVerifyInfo"></asp:Label>
                                </div>
                                <div>
                                    <asp:HiddenField runat="server" ID="hdnVerificatorSign" />
                                </div>
                            </fieldset>--%>

                            <fieldset style="width: 128px">
                                <legend>Educator Signature</legend>
                                <telerik:RadBinaryImage ID="imgEducator" runat="server"
                                    Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                                <br />
                                <asp:Button runat="server" ID="btnPsSign" Text="Sign" Width="300px" OnClientClick="javascript:openEducatorSign();return false;" />
                                <div>
                                    <asp:HiddenField runat="server" ID="hdnEducatorSign" />
                                </div>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top;">
                <fieldset>
                    <legend>Education</legend>

                    <telerik:RadGrid ID="grdPatientEducation" Width="100%" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                        AllowMultiRowSelection="True"
                        OnNeedDataSource="grdPatientEducation_NeedDataSource" OnItemDataBound="grdPatientEducation_ItemDataBound">
                        <MasterTableView DataKeyNames="ItemID" ShowHeader="false" ShowHeadersWhenNoRecords="false" Width="100%">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="" UniqueName="IsSelectedEdit" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsSelected" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridCheckBoxColumn DataField="IsSelected" UniqueName="IsSelected" HeaderText="" HeaderStyle-Width="30px" Display="False" />
                                <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText="" HeaderStyle-Width="200px" />
                                <telerik:GridBoundColumn DataField="EducationNotes" UniqueName="EducationNotes" HeaderText="Notes" />
                                <telerik:GridTemplateColumn HeaderText="Notes" UniqueName="NotesEdit">
                                    <ItemTemplate>
                                        <telerik:RadTextBox
                                            ID="txtNotes" runat="server"
                                            Width="100%" Height="50px" TextMode="MultiLine">
                                        </telerik:RadTextBox>

                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                            <Resizing AllowColumnResize="False" />
                            <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
                        </ClientSettings>
                    </telerik:RadGrid>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
