<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PersonalInfoDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalInfoDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            var fnNo = 0;
            function openWinCaptureImage() {
                fnNo = 99;
                var oPid = $find("<%= txtPersonID.ClientID %>");
                var oWnd = $find("<%= winUploadFoto.ClientID %>");
                oWnd.SetUrl("PersonalUploadFoto.aspx?pid=" + oPid.get_value());
                oWnd.Show();
            }

            function gotoViewRecruitmentTestUrl(id, type) {
                var oWnd = $find("<%= winRecruitmentTest.ClientID %>");
                var oPid = $find("<%= txtPersonID.ClientID %>");

                if (type == "INTERVIEW")
                    oWnd.setUrl("RecruitmentTestScoringInterview.aspx?pid=" + oPid.get_value() + "&tid=" + id);
                else
                    oWnd.setUrl("RecruitmentTestScoring.aspx?pid=" + oPid.get_value() + "&tid=" + id);
                oWnd.show();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd) {
                //Jika apply di click
                var arg = oWnd.argument;
                if (arg != null) {
                    __doPostBack("<%= grdRecruitmentTest.UniqueID %>", "AfterUploadRecruit");
                }
            }

            function win_ClientClose(oWnd, args) {
                if (fnNo == 99) {
                    var arg = args.get_argument();
                    if (arg != null) {
                        __doPostBack('<%= grdPersonalAddress.UniqueID%>', "AfterUpload");
                    }
                }
            }

            function openPersonalDocument(dCode, rId, note) {
                var pid = $find("<%= txtPersonID.ClientID %>");
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/EmployeeHR/PersonalDocument/PersonalDocumentHist.aspx?pageId=epi&pid=' + pid.get_value() + "&dc=" + dCode + "&rid=" + rId + "&note=" + note;
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
            function openPrintDialog(pfId) {
                var oWnd = $find("<%= winPrintDialog.ClientID %>");
                var oPid = $find("<%= txtPersonID.ClientID %>");
                oWnd.SetUrl("PrintDialog.aspx?pId=" + oPid.get_value() + "&pfId=" + pfId);
                oWnd.show();
            }
            function onClientClosePrintDialog(oWnd, args) {
                if (oWnd.argument && oWnd.argument.print != null) {
                    var oWnd = $find("<%= winPrint.ClientID %>");
                    oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>');
                    oWnd.show();
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winUploadFoto" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="win_ClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winEntry" Width="600px" Height="600px" runat="server"
        ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" VisibleStatusbar="False" Modal="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="600px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPrintDialog" Animation="None" Width="600px" Height="500px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="True" Modal="true" OnClientClose="onClientClosePrintDialog" Title="Print">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" Title="Recruitment Test"
        OnClientClose="onClientClose" ID="winRecruitmentTest" />
    <asp:HiddenField runat="server" ID="hdnPageId" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top" align="center">
                <table width="150px" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <fieldset id="FieldSet1" style="width: 135px; min-height: 180px;">
                                <legend>Photo</legend>
                                <div style='float: right; padding: 4px'>
                                    <asp:Image runat="server" ID="imgPhoto" Width="135px" Height="180px" />
                                </div>
                                <div style='float: right; padding: 2px'>
                                    <asp:Button runat="server" Text="Upload Photo" ID="btnUploadImage" Width="135px"
                                        OnClientClick="openWinCaptureImage();return false;" />
                                </div>
                            </fieldset>
                        </td>
                    </tr>

                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPersonID" runat="server" Text="Person ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPersonID" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPersonID" runat="server" ErrorMessage="Person ID required."
                                ValidationGroup="entry" ControlToValidate="txtPersonID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeNumber" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEmployeeNumber" runat="server" ErrorMessage="Employee No required."
                                ValidationGroup="entry" ControlToValidate="txtEmployeeNumber" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFirstName" runat="server" Width="300px" MaxLength="60" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="First Name required."
                                ValidationGroup="entry" ControlToValidate="txtFirstName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMiddleName" runat="server" Width="300px" MaxLength="60" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtLastName" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPreTitle" runat="server" Text="Pre Title"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPreTitle" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPostTitle" runat="server" Text="Post Title"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPostTitle" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBirthName" runat="server" Text="Birth Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBirthName" runat="server" Width="300px" MaxLength="60" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBirthDate" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 190px">
                                        <telerik:RadTextBox ID="txtPlaceBirth" runat="server" Width="180px" MaxLength="60" />
                                    </td>
                                    <td style="width: 10px">/
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtBirthDate" runat="server" Width="100px" MinDate="01/01/1900"
                                            MaxDate="12/31/2999" AutoPostBack="true" OnSelectedDateChanged="txtBirthDate_SelectedDateChanged" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvBirthDate" runat="server" ErrorMessage="Birth Date required."
                                ValidationGroup="entry" ControlToValidate="txtBirthDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 35px">
                                        <telerik:RadTextBox ID="txtAgeInYear" runat="server" Width="30px" ReadOnly="true">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 25px">&nbsp;Y
                                    </td>
                                    <td style="width: 35px">
                                        <telerik:RadTextBox ID="txtAgeInMonth" runat="server" Width="30px" ReadOnly="true">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 25px">&nbsp;M
                                    </td>
                                    <td style="width: 35px">
                                        <telerik:RadTextBox ID="txtAgeInDay" runat="server" Width="30px" ReadOnly="true">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 25px">&nbsp;D
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRGenderType" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="M" Text="Male" />
                                <asp:ListItem Value="F" Text="Female" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSex" runat="server" ErrorMessage="Gender required."
                                ValidationGroup="entry" ControlToValidate="rbtSex" SetFocusOnError="True" Width="20px">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRSalutation" runat="server" Text="Salutation"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRSalutation" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRSalutation" runat="server" ErrorMessage="Salutation required."
                                ValidationGroup="entry" ControlToValidate="cboSRSalutation" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRReligion" runat="server" Text="Religion"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRReligion" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRReligion" runat="server" ErrorMessage="Religion required."
                                ValidationGroup="entry" ControlToValidate="cboSRReligion" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREthnic" runat="server" Text="Ethnic"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREthnic" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSREthnic" runat="server" ErrorMessage="Ethnic required."
                                ValidationGroup="entry" ControlToValidate="cboSREthnic" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRBloodType" runat="server" Text="Blood Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRBloodType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRMaritalStatus" runat="server" Text="Marital Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRMaritalStatus" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRMaritalStatus" runat="server" ErrorMessage="Marital Status required."
                                ValidationGroup="entry" ControlToValidate="cboSRMaritalStatus" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPatientID" runat="server" Width="150px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPatientID_ItemDataBound"
                                OnItemsRequested="cboPatientID_ItemsRequested" OnSelectedIndexChanged="cboPatientID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                    </b>&nbsp;-&nbsp;
                                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                    &nbsp;|&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                                    <br />
                                    Address :
                                    <%# DataBinder.Eval(Container.DataItem, "Address")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                            &nbsp;/&nbsp;
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="131px" ReadOnly="True" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPatientID" runat="server" ErrorMessage="Patient ID required."
                                ControlToValidate="cboPatientID" SetFocusOnError="True" ValidationGroup="Patient"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCoverageClass" runat="server" Text="Coverage Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboCoverageClass" runat="server" Width="300px" AllowCustomText="true"
                                            Filter="Contains" />
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:ImageButton ID="ibtnPrintMedicalInsurance" runat="server" ImageUrl="../../../../Images/Toolbar/print16.png"
                                            CausesValidation="False" OnClientClick="openPrintDialog('-1');return false;" ToolTip="Medical Insurance Form" />
                                    </td>
                                </tr>
                            </table>

                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCoverageClassBPJS" runat="server" Text="Coverage Class BPJS"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboCoverageClassBPJS" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Address" PageViewID="pgvAddress" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Contact" PageViewID="pgvContact">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Identification" PageViewID="pgvIdentification">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Family" PageViewID="pgvFamily">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Emergency Contact" PageViewID="pgvEmergencyContact">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Work Experience" PageViewID="pgvWorkExperience">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Education" PageViewID="pgvEducationHistory">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="License" PageViewID="pgvLicense">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Organization" PageViewID="pgvOrganization">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Physical Profile" PageViewID="pgvPhysicalProfile">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Recruitment Test" PageViewID="pgvRecruitmentTest">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvAddress" runat="server">
            <telerik:RadGrid ID="grdPersonalAddress" runat="server" OnNeedDataSource="grdPersonalAddress_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPersonalAddress_UpdateCommand"
                OnDeleteCommand="grdPersonalAddress_DeleteCommand" OnInsertCommand="grdPersonalAddress_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalAddressID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalAddressID"
                            HeaderText="Personal Address ID" UniqueName="PersonalAddressID" SortExpression="PersonalAddressID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRAddressType" HeaderText="Address Type"
                            UniqueName="SRAddressType" SortExpression="SRAddressType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AddressTypeName" HeaderText="Address Type"
                            UniqueName="AddressTypeName" SortExpression="AddressTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Address" HeaderText="Address"
                            UniqueName="Address" SortExpression="Address" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="District" HeaderText="District"
                            UniqueName="District" SortExpression="District" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="County" HeaderText="County"
                            UniqueName="County" SortExpression="County" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="City" HeaderText="City"
                            UniqueName="City" SortExpression="City" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRState" HeaderText="Province"
                            UniqueName="SRState" SortExpression="SRState" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="StateName" HeaderText="Province"
                            UniqueName="StateName" SortExpression="StateName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ZipPostalCode" HeaderText="Zip Code"
                            UniqueName="ZipPostalCode" SortExpression="ZipPostalCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PersonalAddressDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PersonalAddressEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvContact" runat="server">
            <telerik:RadGrid ID="grdPersonalContact" runat="server" OnNeedDataSource="grdPersonalContact_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPersonalContact_UpdateCommand"
                OnDeleteCommand="grdPersonalContact_DeleteCommand" OnInsertCommand="grdPersonalContact_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalContactID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalContactID"
                            HeaderText="Personal Contact ID" UniqueName="PersonalContactID" SortExpression="PersonalContactID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRContactType" HeaderText="Contact Type"
                            UniqueName="SRContactType" SortExpression="SRContactType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ContactTypeName" HeaderText="Contact Type"
                            UniqueName="ContactTypeName" SortExpression="ContactTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ContactValue" HeaderText="Contact Value"
                            UniqueName="ContactValue" SortExpression="ContactValue" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PersonalContactDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PersonalContactEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvIdentification" runat="server">
            <telerik:RadGrid ID="grdPersonalIdentification" runat="server" OnNeedDataSource="grdPersonalIdentification_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPersonalIdentification_UpdateCommand"
                OnDeleteCommand="grdPersonalIdentification_DeleteCommand" OnInsertCommand="grdPersonalIdentification_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalIdentificationID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalIdentificationID"
                            HeaderText="Personal Identification ID" UniqueName="PersonalIdentificationID"
                            SortExpression="PersonalIdentificationID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRIdentificationType"
                            HeaderText="Identification Type" UniqueName="SRIdentificationType" SortExpression="SRIdentificationType"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="IdentificationTypeName"
                            HeaderText="Identification Type" UniqueName="IdentificationTypeName" SortExpression="IdentificationTypeName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="IdentificationValue"
                            HeaderText="Identification No" UniqueName="IdentificationValue" SortExpression="IdentificationValue"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="IdentificationName"
                            HeaderText="Identification Name" UniqueName="IdentificationName" SortExpression="IdentificationName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                            UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                            UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "11", DataBinder.Eval(Container.DataItem, "PersonalIdentificationID"), DataBinder.Eval(Container.DataItem, "IdentificationTypeName") + " - " + DataBinder.Eval(Container.DataItem, "IdentificationValue")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PersonalIdentificationDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PersonalIdentificationEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvFamily" runat="server">
            <telerik:RadGrid ID="grdPersonalFamily" runat="server" OnNeedDataSource="grdPersonalFamily_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPersonalFamily_UpdateCommand"
                OnDeleteCommand="grdPersonalFamily_DeleteCommand" OnInsertCommand="grdPersonalFamily_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalFamilyID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalFamilyID"
                            HeaderText="Personal Family ID" UniqueName="PersonalFamilyID" SortExpression="PersonalFamilyID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FamilyRelationName" HeaderText="Family Relation"
                            UniqueName="FamilyRelationName" SortExpression="FamilyRelationName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="FamilyName" HeaderText="Family Name" UniqueName="FamilyName"
                            SortExpression="FamilyName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateBirth" HeaderText="DoB"
                            UniqueName="DateBirth" SortExpression="DateBirth" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="GenderTypeName" HeaderText="Gender" UniqueName="GenderTypeName"
                            SortExpression="GenderTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="MaritalStatusName" HeaderText="Marital Status"
                            UniqueName="MaritalStatusName" SortExpression="MaritalStatusName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="BPJSKesehatanNo" HeaderText="No BPJS Kesehatan"
                            UniqueName="BPJSKesehatanNo" SortExpression="BPJSKesehatanNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="CoverageTypeName" HeaderText="Coverage Type"
                            UniqueName="CoverageTypeName" SortExpression="CoverageTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="CoverageClassName" HeaderText="Coverage Class"
                            UniqueName="CoverageClassName" SortExpression="CoverageClassName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="CoverageClassBPJSName" HeaderText="Coverage Class BPJS"
                            UniqueName="CoverageClassBPJSName" SortExpression="CoverageClassBPJSName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                            SortExpression="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            Visible="false" />
                        <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone No" UniqueName="Phone"
                            SortExpression="Phone" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="PrintDialog"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "IsGuaranteed") != null && DataBinder.Eval(Container.DataItem, "IsGuaranteed").Equals(true) ? string.Format("<a href=\"#\" onclick=\"openPrintDialog('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" title=\"Medical Insurance Form\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "PersonalFamilyID")) : string.Empty %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PersonalFamilyDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PersonalFamilyEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvEmergencyContact" runat="server">
            <telerik:RadGrid ID="grdPersonalEmergencyContact" runat="server" OnNeedDataSource="grdPersonalEmergencyContact_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPersonalEmergencyContact_UpdateCommand"
                OnDeleteCommand="grdPersonalEmergencyContact_DeleteCommand" OnInsertCommand="grdPersonalEmergencyContact_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalEmergencyContactID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalEmergencyContactID"
                            HeaderText="Personal Emergency Contact ID" UniqueName="PersonalEmergencyContactID"
                            SortExpression="PersonalEmergencyContactID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FamilyRelationName" HeaderText="Family Relation"
                            UniqueName="FamilyRelationName" SortExpression="FamilyRelationName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ContactName" HeaderText="Contact Name"
                            UniqueName="ContactName" SortExpression="ContactName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Address" HeaderText="Address"
                            UniqueName="Address" SortExpression="Address" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="District" HeaderText="District"
                            UniqueName="District" SortExpression="District" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="County" HeaderText="County"
                            UniqueName="County" SortExpression="County" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="City" HeaderText="City"
                            UniqueName="City" SortExpression="City" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRState" HeaderText="Province"
                            UniqueName="SRState" SortExpression="SRState" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="StateName" HeaderText="Province"
                            UniqueName="StateName" SortExpression="StateName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ZipPostalCode" HeaderText="Zip Code"
                            UniqueName="ZipPostalCode" SortExpression="ZipPostalCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Phone" HeaderText="Phone"
                            UniqueName="Phone" SortExpression="Phone" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Mobile" HeaderText="Mobile"
                            UniqueName="Mobile" SortExpression="Mobile" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PersonalEmergencyContactDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PersonalEmergencyContactEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvWorkExperience" runat="server">
            <telerik:RadGrid ID="grdPersonalWorkExperience" runat="server" OnNeedDataSource="grdPersonalWorkExperience_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPersonalWorkExperience_UpdateCommand"
                OnDeleteCommand="grdPersonalWorkExperience_DeleteCommand" OnInsertCommand="grdPersonalWorkExperience_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalWorkExperienceID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalWorkExperienceID"
                            HeaderText="Personal Work Experience ID" UniqueName="PersonalWorkExperienceID"
                            SortExpression="PersonalWorkExperienceID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRLineBisnis" HeaderText="Code"
                            UniqueName="SRLineBisnis" SortExpression="SRLineBisnis" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="LineBisnisName" HeaderText="Line Business"
                            UniqueName="LineBisnisName" SortExpression="LineBisnisName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                            UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                            UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />

                        <telerik:GridTemplateColumn HeaderStyle-Width="150px" DataField="StartYear" HeaderText="Period" UniqueName="YearPeriod"
                            SortExpression="StartYear">
                            <ItemTemplate>
                                <%# string.Format("{0} - {1}", DataBinder.Eval(Container.DataItem, "StartYear"), DataBinder.Eval(Container.DataItem, "EndYear"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>


                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="Company" HeaderText="Company"
                            UniqueName="Company" SortExpression="Company" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="Location" HeaderText="Location"
                            UniqueName="Location" SortExpression="Location" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="JobDesc" HeaderText="Job Desc"
                            UniqueName="JobDesc" SortExpression="JobDesc" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ReasonOfLeaving" HeaderText="Reason Of Leaving"
                            UniqueName="ReasonOfLeaving" SortExpression="ReasonOfLeaving" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PersonalWorkExperienceDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PersonalWorkExperienceEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvEducationHistory" runat="server">
            <telerik:RadGrid ID="grdPersonalEducationHistory" runat="server" OnNeedDataSource="grdPersonalEducationHistory_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPersonalEducationHistory_UpdateCommand"
                OnDeleteCommand="grdPersonalEducationHistory_DeleteCommand" OnInsertCommand="grdPersonalEducationHistory_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalEducationHistoryID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalEducationHistoryID"
                            HeaderText="Personal Education History ID" UniqueName="PersonalEducationHistoryID"
                            SortExpression="PersonalEducationHistoryID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SREducationLevel" HeaderText="Education Level"
                            UniqueName="SREducationLevel" SortExpression="SREducationLevel" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="EducationLevelName"
                            HeaderText="Education Level" UniqueName="EducationLevelName" SortExpression="EducationLevelName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="InstitutionName" HeaderText="Institution Name"
                            UniqueName="InstitutionName" SortExpression="InstitutionName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Location" HeaderText="Location"
                            UniqueName="Location" SortExpression="Location" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Majors" HeaderText="Majors"
                            UniqueName="Majors" SortExpression="Majors" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="StartYear" HeaderText="Start Year"
                            UniqueName="StartYear" SortExpression="StartYear" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="EndYear" HeaderText="End Year"
                            UniqueName="EndYear" SortExpression="EndYear" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="GraduateDate"
                            HeaderText="Graduate Date" UniqueName="GraduateDate" SortExpression="GraduateDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="40px" DataField="Gpa" HeaderText="GPA"
                            UniqueName="Gpa" SortExpression="Gpa" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "12", DataBinder.Eval(Container.DataItem, "PersonalEducationHistoryID"), DataBinder.Eval(Container.DataItem, "EducationLevelName")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PersonalEducationHistoryDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PersonalEducationHistoryEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvLicense" runat="server">
            <telerik:RadGrid ID="grdPersonalLicence" runat="server" OnNeedDataSource="grdPersonalLicence_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPersonalLicence_UpdateCommand"
                OnDeleteCommand="grdPersonalLicence_DeleteCommand" OnInsertCommand="grdPersonalLicence_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalLicenceID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalLicenceID"
                            HeaderText="Personal Licence ID" UniqueName="PersonalLicenceID" SortExpression="PersonalLicenceID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRLicenceType" HeaderText="Licence Type"
                            UniqueName="SRLicenceType" SortExpression="SRLicenceType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LicenceTypeName" HeaderText="License Type"
                            UniqueName="LicenceTypeName" SortExpression="LicenceTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Note" HeaderText="License No"
                            UniqueName="Note" SortExpression="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                            UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                            UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="VerificationLetterNo" HeaderText="Verification Letter No"
                            UniqueName="VerificationLetterNo" SortExpression="VerificationLetterNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="VerificationDate" HeaderText="Verification Date"
                            UniqueName="VerificationDate" SortExpression="VerificationDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "13", DataBinder.Eval(Container.DataItem, "PersonalLicenceID"), DataBinder.Eval(Container.DataItem, "LicenceTypeName") + " - " + DataBinder.Eval(Container.DataItem, "Note")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PersonalLicenceDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PersonalLicenceEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvOrganization" runat="server">
            <telerik:RadGrid ID="grdPersonalOrganization" runat="server" OnNeedDataSource="grdPersonalOrganization_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPersonalOrganization_UpdateCommand"
                OnDeleteCommand="grdPersonalOrganization_DeleteCommand" OnInsertCommand="grdPersonalOrganization_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalOrganizationID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalOrganizationID"
                            HeaderText="Personal Organization ID" UniqueName="PersonalOrganizationID" SortExpression="PersonalOrganizationID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="OrganizationName" HeaderText="Organization Name"
                            UniqueName="OrganizationName" SortExpression="OrganizationName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Location" HeaderText="Location"
                            UniqueName="Location" SortExpression="Location" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="OrganizationRoleName"
                            HeaderText="Organization Role" UniqueName="OrganizationRoleName" SortExpression="OrganizationRoleName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                            UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                            UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "14", DataBinder.Eval(Container.DataItem, "PersonalOrganizationID"), DataBinder.Eval(Container.DataItem, "OrganizationName")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PersonalOrganizationDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PersonalOrganizationEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvPhysicalProfile" runat="server">
            <telerik:RadGrid ID="grdPersonalPhysical" runat="server" OnNeedDataSource="grdPersonalPhysical_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPersonalPhysical_UpdateCommand"
                OnDeleteCommand="grdPersonalPhysical_DeleteCommand" OnInsertCommand="grdPersonalPhysical_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalPhysicalID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalPhysicalID"
                            HeaderText="Personal Physical ID" UniqueName="PersonalPhysicalID" SortExpression="PersonalPhysicalID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRPhysicalCharacteristic"
                            HeaderText="Physical Characteristic" UniqueName="SRPhysicalCharacteristic" SortExpression="SRPhysicalCharacteristic"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PhysicalCharacteristicName"
                            HeaderText="Physical Characteristic" UniqueName="PhysicalCharacteristicName"
                            SortExpression="PhysicalCharacteristicName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PhysicalValue" HeaderText="Physical Value"
                            UniqueName="PhysicalValue" SortExpression="PhysicalValue" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRMeasurementCode" HeaderText="Measurement Code"
                            UniqueName="SRMeasurementCode" SortExpression="SRMeasurementCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MeasurementCodeName"
                            HeaderText="Measurement Code" UniqueName="MeasurementCodeName" SortExpression="MeasurementCodeName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PersonalPhysicalDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PersonalPhysicalEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvRecruitmentTest" runat="server">
            <telerik:RadGrid ID="grdRecruitmentTest" runat="server" OnNeedDataSource="grdRecruitmentTest_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdRecruitmentTest_UpdateCommand"
                OnDeleteCommand="grdRecruitmentTest_DeleteCommand" OnInsertCommand="grdRecruitmentTest_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalRecruitmentTestID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalRecruitmentTestID"
                            UniqueName="PersonalRecruitmentTestID" SortExpression="PersonalRecruitmentTestID"
                            Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TestDate" HeaderText="Test Date"
                            UniqueName="TestDate" SortExpression="TestDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="RecruitmentTestName" HeaderText="Recruitment Test Name"
                            UniqueName="RecruitmentTestName" SortExpression="RecruitmentTestName" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="TestResult" HeaderText="Test Result" UniqueName="TestResult"
                            SortExpression="TestResult" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="RecruitmentTestConclusionName" HeaderText="Conclusion" UniqueName="RecruitmentTestConclusionName"
                            SortExpression="RecruitmentTestConclusionName" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn UniqueName="ViewRecruitmentTestUrl" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewRecruitmentTestUrl('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Detail Test\" /></a>",
                                                                                                                DataBinder.Eval(Container.DataItem, "PersonalRecruitmentTestID"), DataBinder.Eval(Container.DataItem, "RecruitmentTestType"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "15", DataBinder.Eval(Container.DataItem, "PersonalRecruitmentTestID"), DataBinder.Eval(Container.DataItem, "RecruitmentTestName")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="RecruitmentTestDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="RecruitmentTestEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
