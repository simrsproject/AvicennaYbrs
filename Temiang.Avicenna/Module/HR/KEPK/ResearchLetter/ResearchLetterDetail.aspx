<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ResearchLetterDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.KEPK.ResearchLetterDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script type="text/javascript" language="javascript">
            function openDocument() {
                var pid = $find("<%= txtLetterID.ClientID %>");
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/KEPK/ResearchLetterDocument/ResearchLetterDocumentHist.aspx?pid=' + pid.get_value();
                openWinMaxWindow(url);
            }
            function openWinMaxWindow(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                openWindow(url, width - 40, height - 40);
            }
            function openWindow(url, width, height) {
                var oWnd = $find("<%= winEntry.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
                oWnd.show();
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winEntry" Width="600px" Height="600px" runat="server"
        ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" VisibleStatusbar="False" Modal="true">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td>
                            <telerik:RadTextBox ID="txtLetterID" runat="server" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLetterNo" runat="server" Text="Letter No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtLetterNo" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvLetterNo" runat="server" ErrorMessage="Letter No required."
                                ValidationGroup="entry" ControlToValidate="txtLetterNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLetterDate" runat="server" Text="Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtLetterDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" ErrorMessage="Letter Date required."
                                ControlToValidate="txtLetterDate" SetFocusOnError="True" ValidationGroup="entry"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblResearcherName" runat="server" Text="Researcher Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtResearcherName" runat="server" Width="300px" MaxLength="250" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvResearcherName" runat="server" ErrorMessage="Researcher Name required."
                                ValidationGroup="entry" ControlToValidate="txtResearcherName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSubject" runat="server" Text="Subject"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSubject" runat="server" Width="300px" MaxLength="250" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ErrorMessage="Subject required."
                                ValidationGroup="entry" ControlToValidate="txtSubject" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRResearchDecision" runat="server" Text="Decision"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRResearchDecision" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSREmployeeLeaveType" runat="server" ErrorMessage="Decision required."
                                ValidationGroup="entry" ControlToValidate="cboSRResearchDecision" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAttachment" runat="server" Text="Attachment"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAttachment" runat="server" Width="300px" MaxLength="250" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAttachment" runat="server" ErrorMessage="Attachment required."
                                ValidationGroup="entry" ControlToValidate="txtAttachment" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRResearchInstitution" runat="server" Text="Institution"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRResearchInstitution" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRResearchInstitution" runat="server" ErrorMessage="Institution required."
                                ValidationGroup="entry" ControlToValidate="cboSRResearchInstitution" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRResearchFaculty" runat="server" Text="Faculty"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRResearchFaculty" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRResearchFaculty" runat="server" ErrorMessage="Faculty required."
                                ValidationGroup="entry" ControlToValidate="cboSRResearchFaculty" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRResearchMajors" runat="server" Text="Majors"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRResearchMajors" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRResearchMajors" runat="server" ErrorMessage="Majors required."
                                ValidationGroup="entry" ControlToValidate="cboSRResearchMajors" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREducationDegree" runat="server" Text="Education Degree"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREducationDegree" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSREducationDegree" runat="server" ErrorMessage="Education Degree required."
                                ValidationGroup="entry" ControlToValidate="cboSREducationDegree" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsUpload" Text="Upload" runat="server" />
                        </td>
                        <td width="20px">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label"><asp:Label ID="lblReviewTime" runat="server" Text="Review Time (Days)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtReviewTime" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReviewTime" runat="server" ErrorMessage="Review Time (Days) required."
                                ValidationGroup="entry" ControlToValidate="txtReviewTime" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRResearchReviewerName" runat="server" Text="Reviewer Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRResearchReviewerName" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRResearchReviewerName" runat="server" ErrorMessage="Reviewer Name required."
                                ValidationGroup="entry" ControlToValidate="cboSRResearchReviewerName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsVoid" Text="Void" runat="server" />
                        </td>
                        <td width="20px">
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr runat="server" id="trUploadDocument" visible="false">
                        <td>
                            <a href="#" onclick="javascript:openDocument(); return false;">
                                    <img src="../../../../Images/BatchProcess80.png" border="0" alt="New" /><br />
                                    Document Upload</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>