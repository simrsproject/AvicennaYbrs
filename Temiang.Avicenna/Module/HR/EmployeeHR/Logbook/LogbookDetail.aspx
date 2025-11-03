<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="LogbookDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.Logbook.LogbookDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script language="javascript" type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "list":
                        location.replace('LogbookList.aspx?type=<%= Page.Request.QueryString["type"] %>');
                        break;
                }
            }

            function gotoViewCredentialingFormUrl(trn, pg) {
                if (pg == "01") {
                    location.replace('<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/Credential/Process/Medic/CredentialingDetail.aspx?md=view&id=' + trn + '&type=<%= Request.QueryString["type"] %>' + '&role=adm&pid=<%= Request.QueryString["id"] %>');
                }
                else {
                    location.replace('<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/Credential/Process/CredentialingDetail.aspx?md=view&id=' + trn + '&type=<%= Request.QueryString["type"] %>' + '&role=adm&pid=<%= Request.QueryString["id"] %>');
                }
            }

            function gotoViewCredentialing2FormUrl(trn, pg) {
                location.replace('<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/Credential/Process_v2/CredentialingDetail.aspx?md=view&id=' + trn + '&type=<%= Request.QueryString["type"] %>' + '&role=adm&pid=<%= Request.QueryString["id"] %>' + '&pgd=' + pg);
            }

            function openPersonalDocument(dCode, rId, note) {
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/EmployeeHR/PersonalDocument/PersonalDocumentHist.aspx?pageId=<%= Request.QueryString["type"] %>' + '&pid=<%= Request.QueryString["id"] %>' + "&dc=" + dCode + "&rid=" + rId + "&note=" + note;
                openWinMaxWindow(url, '1');
            }

            function openCredentialingDocument(tno, note) {
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/Credential/Document/DocumentHist.aspx?pid=' + tno + "&note=" + note + "&type=<%= Page.Request.QueryString["type"] %>&role=adm";
                openWinMaxWindow(url, '1');
            }

            function openWinMaxWindow(url, x) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                if (x == '1')
                    openWindow(url, width - 40, height - 40);
                else
                    openWindow2(url, width - 40, height - 40);
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

            function openWindow2(url, width, height) {
                var oWnd = $find("<%= winForms.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
                oWnd.show();
            }

            function entryEmployeeForms(mod, trno) {
                var omng = $find("<%= cboManagerID.ClientID %>");
                var osvr = $find("<%= cboSupervisorID.ClientID %>");
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/EmployeeHR/EmployeeForms/EmployeeFormsDetail.aspx?pid=<%= Request.QueryString["type"] %>' + '&md=' + mod + '&id=' + trno + '&ccm=rebind&cet=<%=grdEmployeeForm.ClientID %>'
                url = url + '&eid=<%= Request.QueryString["id"] %>' + '&svr=' + osvr.get_value() + '&mng=' + omng.get_value();

                openWinMaxWindow(url, '2');
            }

            function winForms_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.callbackMethod === 'rebind') {
                        var ctl = $find(arg.eventTarget);
                        if (typeof ctl.rebind == 'function') {
                            ctl.rebind();
                        } else {
                            var masterTable = $find(arg.eventTarget).get_masterTableView();
                            masterTable.rebind();
                        }

                    }
                }
            }

            function openTrainingEvaluation(id) {
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/TrainingHR/EmployeeTrainingEvaluation/EmployeeTrainingEvaluationDetail.aspx?md=view&id=' + id + '&pageId=<%= Request.QueryString["type"] %>' + '&pid=<%= Request.QueryString["id"] %>';
                window.location.href = url;
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winEntry" Width="600px" Height="600px" runat="server"
        ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" VisibleStatusbar="False" Modal="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winForms" OnClientClose="winForms_ClientClose" Animation="None"
        Width="900px" Height="500px" runat="server" ShowContentDuringLoad="false" Behavior="Close"
        VisibleStatusbar="false" Modal="true" />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdEmployeeTrainingHistory">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmployeeTrainingHistory" />
                    <telerik:AjaxUpdatedControl ControlID="txtTotalCreditPoint" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTrainingPeriod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmployeeTrainingHistory" />
                    <telerik:AjaxUpdatedControl ControlID="txtTotalCreditPoint" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterOrientationType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmployeeOrientation" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEmployeeOrientation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmployeeOrientation" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdEmployeeForm">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmployeeForm" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdAppraisalQuestion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAppraisalQuestion" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="List" Value="list" ImageUrl="~/Images/Toolbar/details16.png"
                HoveredImageUrl="~/Images/Toolbar/details16_h.png" DisabledImageUrl="~/Images/Toolbar/details16_d.png" />
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
        </Items>
    </telerik:RadToolBar>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td style="vertical-align: top">
                                <table width="150px">
                                    <tr>
                                        <td style="vertical-align: top">
                                            <fieldset id="FieldSet1" style="width: 135px; min-height: 180px;">
                                                <legend>Photo</legend>
                                                <asp:Image runat="server" ID="imgPhoto" Width="135px" Height="180px" />
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtEmployeeNumber" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <a href="#" onclick="javascript:openPersonalDocument('01','0',''); return false;">
                                                <img src="../../../../Images/doc_upload48.png" border="0" alt="New" /><br />
                                                Document Upload</a>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <telerik:RadGrid ID="grdLicenseRecap" runat="server" OnNeedDataSource="grdLicenseRecap_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None" OnItemDataBound="grdLicenseRecap_ItemDataBound">
                                    <HeaderContextMenu>
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="SRLicenceType">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRLicenceType" HeaderText="License Type"
                                                UniqueName="SRLicenceType" SortExpression="SRLicenceType" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LicenseTypeName" HeaderText="License Type"
                                                UniqueName="LicenseTypeName" SortExpression="LicenseTypeName" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes"
                                                UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Remaining" HeaderText="Remaining"
                                                UniqueName="Remaining" SortExpression="Remaining" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DayLimit" HeaderText="Day Limit"
                                                UniqueName="DayLimit" SortExpression="DayLimit" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                                        </Columns>
                                    </MasterTableView>
                                    <FilterMenu>
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                    </FilterMenu>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Resizing AllowColumnResize="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Personal Identity" PageViewID="pgvPersonalIdentity"
                Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Professional Identity" PageViewID="pgvProfessionalIdentity">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Working Information" PageViewID="pgvWorkInfo">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Development" PageViewID="pgvDevelopment">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Performance Assessment" PageViewID="pgvAppraisal">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Forms" PageViewID="pgvFroms">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvPersonalIdentity" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblBirthName" runat="server" Text="Birth Name"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtBirthName" runat="server" Width="300px" ReadOnly="true" />
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
                                                <telerik:RadTextBox ID="txtPlaceBirth" runat="server" Width="180px" ReadOnly="true" />
                                            </td>
                                            <td style="width: 10px">/
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtBirthDate" runat="server" Width="100px" Enabled="false" MinDate="1/1/1900" MaxDate="1/1/2999" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtAge" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRGenderType" runat="server" Text="Gender"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                        <asp:ListItem Value="M" Text="Male" />
                                        <asp:ListItem Value="F" Text="Female" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>

                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRReligion" runat="server" Text="Religion"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRReligion" runat="server" Width="300px" Enabled="false" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSREthnic" runat="server" Text="Ethnic"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSREthnic" runat="server" Width="300px" Enabled="false" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRBloodType" runat="server" Text="Blood Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRBloodType" runat="server" Width="300px" Enabled="false" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRMaritalStatus" runat="server" Text="Marital Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRMaritalStatus" runat="server" Width="300px" Enabled="false" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="mpgDetail1" SelectedIndex="0">
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
                    <telerik:RadTab runat="server" Text="Education" PageViewID="pgvEducation">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Organization" PageViewID="pgvOrganization">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Physical Profile" PageViewID="pgvPhysicalProfile">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="mpgDetail1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                BorderColor="gray">
                <telerik:RadPageView ID="pgvAddress" runat="server">
                    <telerik:RadGrid ID="grdPersonalAddress" runat="server" OnNeedDataSource="grdPersonalAddress_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalAddressID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalAddressID"
                                    HeaderText="Personal Address ID" UniqueName="PersonalAddressID" SortExpression="PersonalAddressID"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRAddressType" HeaderText="Address Type"
                                    UniqueName="SRAddressType" SortExpression="SRAddressType" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AddressTypeName" HeaderText="Type"
                                    UniqueName="AddressTypeName" SortExpression="AddressTypeName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Address" HeaderText="Address"
                                    UniqueName="Address" SortExpression="Address" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="District" HeaderText="District"
                                    UniqueName="District" SortExpression="District" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="County" HeaderText="County"
                                    UniqueName="County" SortExpression="County" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="City" HeaderText="City"
                                    UniqueName="City" SortExpression="City" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRState" HeaderText="Province"
                                    UniqueName="SRState" SortExpression="SRState" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="StateName" HeaderText="Province"
                                    UniqueName="StateName" SortExpression="StateName"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ZipPostalCode" HeaderText="Zip Code"
                                    UniqueName="ZipPostalCode" SortExpression="ZipPostalCode" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn />
                            </Columns>
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
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalContactID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalContactID"
                                    HeaderText="Personal Contact ID" UniqueName="PersonalContactID" SortExpression="PersonalContactID"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRContactType" HeaderText="Contact Type"
                                    UniqueName="SRContactType" SortExpression="SRContactType" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ContactTypeName" HeaderText="Type"
                                    UniqueName="ContactTypeName" SortExpression="ContactTypeName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ContactValue" HeaderText="Contact Value"
                                    UniqueName="ContactValue" SortExpression="ContactValue" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn />
                            </Columns>
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
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalIdentificationID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalIdentificationID"
                                    HeaderText="Personal Identification ID" UniqueName="PersonalIdentificationID"
                                    SortExpression="PersonalIdentificationID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRIdentificationType"
                                    HeaderText="Type" UniqueName="SRIdentificationType" SortExpression="SRIdentificationType"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="IdentificationTypeName"
                                    HeaderText="Type" UniqueName="IdentificationTypeName" SortExpression="IdentificationTypeName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="IdentificationValue"
                                    HeaderText="Identification No" UniqueName="IdentificationValue" SortExpression="IdentificationValue"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="IdentificationName"
                                    HeaderText="Identification Name" UniqueName="IdentificationName" SortExpression="IdentificationName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn />
                                <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                                    ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "11", DataBinder.Eval(Container.DataItem, "PersonalIdentificationID"), DataBinder.Eval(Container.DataItem, "IdentificationTypeName") + " - " + DataBinder.Eval(Container.DataItem, "IdentificationValue")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
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
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalFamilyID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalFamilyID"
                                    HeaderText="Personal Family ID" UniqueName="PersonalFamilyID" SortExpression="PersonalFamilyID"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FamilyRelationName" HeaderText="Family Relation"
                                    UniqueName="FamilyRelationName" SortExpression="FamilyRelationName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="FamilyName" HeaderText="Family Name" UniqueName="FamilyName"
                                    SortExpression="FamilyName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateBirth" HeaderText="Date Birth"
                                    UniqueName="DateBirth" SortExpression="DateBirth" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                                    SortExpression="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" UniqueName="Phone"
                                    SortExpression="Phone" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="MaritalStatusName" HeaderText="Marital Status"
                                    UniqueName="MaritalStatusName" SortExpression="MaritalStatusName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="GenderTypeName" HeaderText="Gender Type" UniqueName="GenderTypeName"
                                    SortExpression="GenderTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn />
                            </Columns>
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
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalEmergencyContactID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalEmergencyContactID"
                                    HeaderText="Personal Emergency Contact ID" UniqueName="PersonalEmergencyContactID"
                                    SortExpression="PersonalEmergencyContactID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ContactName" HeaderText="Contact Name"
                                    UniqueName="ContactName" SortExpression="ContactName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="Address" HeaderText="Address"
                                    UniqueName="Address" SortExpression="Address" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="District" HeaderText="District"
                                    UniqueName="District" SortExpression="District" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="County" HeaderText="County"
                                    UniqueName="County" SortExpression="County" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="City" HeaderText="City"
                                    UniqueName="City" SortExpression="City" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRState" HeaderText="Province"
                                    UniqueName="SRState" SortExpression="SRState" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="StateName" HeaderText="Province"
                                    UniqueName="StateName" SortExpression="StateName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ZipPostalCode" HeaderText="Zip Code"
                                    UniqueName="ZipPostalCode" SortExpression="ZipPostalCode" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Phone" HeaderText="Phone"
                                    UniqueName="Phone" SortExpression="Phone" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Mobile" HeaderText="Mobile"
                                    UniqueName="Mobile" SortExpression="Mobile" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn />
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgvEducation" runat="server">
                    <telerik:RadGrid ID="grdPersonalEducationHistory" runat="server" OnNeedDataSource="grdPersonalEducationHistory_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalEducationHistoryID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalEducationHistoryID"
                                    HeaderText="Personal Education History ID" UniqueName="PersonalEducationHistoryID"
                                    SortExpression="PersonalEducationHistoryID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SREducationLevel" HeaderText="Education Level"
                                    UniqueName="SREducationLevel" SortExpression="SREducationLevel" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="EducationLevelName"
                                    HeaderText="Education Level" UniqueName="EducationLevelName" SortExpression="EducationLevelName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="InstitutionName" HeaderText="Institution Name"
                                    UniqueName="InstitutionName" SortExpression="InstitutionName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="Location" HeaderText="Location"
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
                                <telerik:GridTemplateColumn />
                                <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                                    ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "12", DataBinder.Eval(Container.DataItem, "PersonalEducationHistoryID"), DataBinder.Eval(Container.DataItem, "EducationLevelName")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
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
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalOrganizationID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalOrganizationID"
                                    HeaderText="Personal Organization ID" UniqueName="PersonalOrganizationID" SortExpression="PersonalOrganizationID"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="OrganizationName" HeaderText="Organization Name"
                                    UniqueName="OrganizationName" SortExpression="OrganizationName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="Location" HeaderText="Location"
                                    UniqueName="Location" SortExpression="Location" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="OrganizationRoleName"
                                    HeaderText="Organization Role" UniqueName="OrganizationRoleName" SortExpression="OrganizationRoleName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                                    UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                                    UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn />
                                <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                                    ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "14", DataBinder.Eval(Container.DataItem, "PersonalOrganizationID"), DataBinder.Eval(Container.DataItem, "OrganizationName")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
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
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalPhysicalID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PersonalPhysicalID"
                                    HeaderText="Personal Physical ID" UniqueName="PersonalPhysicalID" SortExpression="PersonalPhysicalID"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRPhysicalCharacteristic"
                                    HeaderText="Physical Characteristic" UniqueName="SRPhysicalCharacteristic" SortExpression="SRPhysicalCharacteristic"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PhysicalCharacteristicName"
                                    HeaderText="Physical Characteristic" UniqueName="PhysicalCharacteristicName"
                                    SortExpression="PhysicalCharacteristicName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PhysicalValue" HeaderText="Physical Value"
                                    UniqueName="PhysicalValue" SortExpression="PhysicalValue" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRMeasurementCode" HeaderText="Measurement Code"
                                    UniqueName="SRMeasurementCode" SortExpression="SRMeasurementCode" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="MeasurementCodeName"
                                    HeaderText="Measurement Code" UniqueName="MeasurementCodeName" SortExpression="MeasurementCodeName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn />
                            </Columns>
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvProfessionalIdentity" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblManagerID" runat="server" Text="Manager"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboManagerID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                        MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboManagerID_ItemDataBound"
                                        OnItemsRequested="cboManagerID_ItemsRequested" Enabled="false">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
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
                                    <asp:Label ID="lblSupervisorID" runat="server" Text="Supervisor"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSupervisorID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                        MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSupervisorID_ItemDataBound"
                                        OnItemsRequested="cboSupervisorID_ItemsRequested" Enabled="false">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
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
                                    <asp:Label ID="lblPreceptorId" runat="server" Text="Preceptor"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboPreceptorId" runat="server" Width="300px" EnableLoadOnDemand="true"
                                        MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPreceptorId_ItemDataBound"
                                        OnItemsRequested="cboPreceptorId_ItemsRequested" Enabled="false">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 20 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRProfessionGroup" runat="server" Text="Profession Group"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtSRProfessionGroup" runat="server" Width="80px" Visible="false" />
                                    <telerik:RadTextBox ID="txtProfessionGroupName" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRClinicalWorkArea" runat="server" Text="Clinical Work Area"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRClinicalWorkArea" runat="server" Width="300px" EnableLoadOnDemand="True"
                                        HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboSRClinicalWorkArea_ItemDataBound"
                                        OnItemsRequested="cboSRClinicalWorkArea_ItemsRequested" Enabled="false">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRClinicalAuthorityLevel" runat="server" Text="Clinical Authority Level"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSRClinicalAuthorityLevel" Width="300px" OnItemDataBound="cboSRClinicalAuthorityLevel_ItemDataBound"
                                        OnItemsRequested="cboSRClinicalAuthorityLevel_ItemsRequested" Enabled="false">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="mpgDetail2" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab runat="server" Text="License" PageViewID="pgvPersonalLicense"
                        Selected="true">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Credentialing" PageViewID="pgvCredentialing">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Clinical Performance Appraisal" PageViewID="pgvClinicalPerformance">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="mpgDetail2" runat="server" SelectedIndex="0" BorderStyle="Solid"
                BorderColor="gray">
                <telerik:RadPageView ID="pgvPersonalLicense" runat="server">
                    <telerik:RadGrid ID="grdPersonalLicense" runat="server" OnNeedDataSource="grdPersonalLicense_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonalLicenceID">
                            <Columns>
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
                                <telerik:GridTemplateColumn />
                                <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                                    ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "13", DataBinder.Eval(Container.DataItem, "PersonalLicenceID"), DataBinder.Eval(Container.DataItem, "LicenceTypeName") + " - " + DataBinder.Eval(Container.DataItem, "Note")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgvCredentialing" runat="server">
                    <telerik:RadGrid ID="grdCredentialing" runat="server" OnNeedDataSource="grdCredentialing_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="urlCredential">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"gotoViewCredentialingFormUrl('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View Credentialing\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SRProfessionGroup")) %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="urlCredentialv2">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"gotoViewCredentialing2FormUrl('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View Credentialing\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SRProfessionGroup")) %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="TransactionNo" HeaderText="Transaction No"
                                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="CredentialingDate" HeaderText="Date"
                                    UniqueName="CredentialingDate" SortExpression="CredentialingDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeNumber" HeaderText="Employee No"
                                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                                    SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ProfessionGroupName" HeaderText="Profession Group"
                                    UniqueName="ProfessionGroupName" SortExpression="ProfessionGroupName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ClinicalWorkAreaName" HeaderText="Work Area"
                                    UniqueName="ClinicalWorkAreaName" SortExpression="ClinicalWorkAreaName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ClinicalAuthorityLevelName" HeaderText="Clinical Authority Level"
                                    UniqueName="ClinicalAuthorityLevelName" SortExpression="ClinicalAuthorityLevelName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsPerform" HeaderText="Perform"
                                    UniqueName="IsPerform" SortExpression="IsPerform" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                                    UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                                    UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DecreeNo" HeaderText="Decree No"
                                    UniqueName="DecreeNo" SortExpression="DecreeNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                                    ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"openCredentialingDocument('{0}', ''); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Credential Document\" /></a>",
                                                                                        DataBinder.Eval(Container.DataItem, "TransactionNo")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload2"
                                    ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"openCredentialingDocument('{0}', 'cal'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Clinical Assignment Letter\" /></a>",
                                                                                        DataBinder.Eval(Container.DataItem, "TransactionNo")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgvClinicalPerformance" runat="server">
                    <telerik:RadGrid ID="grdClinicalPerformance" runat="server" OnNeedDataSource="grdClinicalPerformance_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ScoresheetNo">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="view" Visible="false">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"gotoViewClinicalPerformanceFormUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View Credentialing\" /></a>",
                            DataBinder.Eval(Container.DataItem, "ScoresheetNo")) %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ScoresheetNo" HeaderText="Transaction No"
                                    UniqueName="ScoresheetNo" SortExpression="ScoresheetNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ScoringDate" HeaderText="Date"
                                    UniqueName="ScoringDate" SortExpression="ScoringDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeNumber" HeaderText="Employee No"
                                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridNumericColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                                    SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ProfessionGroupName" HeaderText="Profession Group"
                                    UniqueName="ProfessionGroupName" SortExpression="ProfessionGroupName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ClinicalWorkAreaName" HeaderText="Work Area"
                                    UniqueName="ClinicalWorkAreaName" SortExpression="ClinicalWorkAreaName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ClinicalAuthorityLevelName" HeaderText="Clinical Authority Level"
                                    UniqueName="ClinicalAuthorityLevelName" SortExpression="ClinicalAuthorityLevelName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="TotalScore" HeaderText="Total Score"
                                    UniqueName="TotalScore" SortExpression="TotalScore" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridTemplateColumn HeaderStyle-Width="150px" DataField="ConclusionGradeName" HeaderText="Grade" UniqueName="ConclusionGradeName"
                                    SortExpression="ConclusionGradeName">
                                    <ItemTemplate>
                                        <%# string.Format("{0} - {1}", DataBinder.Eval(Container.DataItem, "ConclusionGrade"), DataBinder.Eval(Container.DataItem, "ConclusionGradeName"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ConclusionNotes" HeaderText="Notes"
                                    UniqueName="ConclusionNotes" SortExpression="ConclusionNotes" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                            </Columns>
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvWorkInfo" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblJoinDate" runat="server" Text="Join - (Est.) Resign Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtJoinDate" runat="server" Width="100px" Enabled="false" MinDate="1/1/1900" MaxDate="1/1/2999" />
                                            </td>
                                            <td style="width: 10px">
                                                <asp:Label ID="lblResignDate" runat="server" Text=" - "></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtResignDate" runat="server" Width="100px" Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSREmployeeStatus" runat="server" Text="Employee Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtSREmployeeStatus" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr runat="server" id="trSREmployeeType">
                                <td class="label">
                                    <asp:Label ID="lblSREmployeeType" runat="server" Text="Employee Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSREmployeeType" runat="server" Width="300px" Enabled="false" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRProfessionType" runat="server" Text="Profession Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRProfessionType" runat="server" Width="300px" Enabled="false" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAbsenceCardNo" runat="server" Text="Absence Card No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtAbsenceCardNo" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSREmployeeShiftType" runat="server" Text="Shift Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSREmployeeShiftType" runat="server" Width="300px" Enabled="false" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSREmployeeScheduleType" runat="server" Text="Schedule Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSREmployeeScheduleType" runat="server" Width="300px" Enabled="false" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSREmploymentType" runat="server" Text="Employment Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtSREmploymentType" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSREducationLevel" runat="server" Text="Education Level"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtSREducationLevel" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrganizationName" runat="server" Text="Organization Unit"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOrganizationName" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPositionTitle" runat="server" Text="Position Title"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPositionTitle" runat="server" Width="300px" ReadOnly="true" />
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPositionGradeID" runat="server" Text="Position Grade / Year"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtPositionGradeID" runat="server" Width="200px" ReadOnly="true" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtGradeYear" runat="server" Width="96px" ReadOnly="true" />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label3" runat="server" Text="Service Year"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtServiceYear" runat="server" Width="100px" ReadOnly="true" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtServiceYearText" runat="server" Width="196px" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label4" runat="server" Text="Service Year (Permanent)"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtServiceYearPermanent" runat="server" Width="100px" ReadOnly="true" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtServiceYearPermanentText" runat="server" Width="196px" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadTabStrip ID="RadTabStrip3" runat="server" MultiPageID="mpgDetail3" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Employement Period" PageViewID="pgvEmploymentPeriod"
                        Selected="true">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Organization Unit" PageViewID="pgvOrganizationUnit">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Position" PageViewID="pgvPosition">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="mpgDetail3" runat="server" SelectedIndex="0" BorderStyle="Solid"
                BorderColor="gray">
                <telerik:RadPageView ID="pgvEmploymentPeriod" runat="server">
                    <telerik:RadGrid ID="grdEmployeeEmploymentPeriod" runat="server" OnNeedDataSource="grdEmployeeEmploymentPeriod_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeEmploymentPeriodID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeEmploymentPeriodID"
                                    HeaderText="Employee Employment Period ID" UniqueName="EmployeeEmploymentPeriodID"
                                    SortExpression="EmployeeEmploymentPeriodID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SREmploymentType" HeaderText="Employment Type"
                                    UniqueName="SREmploymentType" SortExpression="SREmploymentType" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="EmploymentTypeName" HeaderText="Employment Type"
                                    UniqueName="EmploymentTypeName" SortExpression="EmploymentTypeName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="EmploymentCategoryName" HeaderText="Employment Category"
                                    UniqueName="EmploymentCategoryName" SortExpression="EmploymentCategoryName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="RecruitmentPlanName" HeaderText="Recruitment Plan"
                                    UniqueName="RecruitmentPlanName" SortExpression="RecruitmentPlanName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="Note" HeaderText="Notes" UniqueName="Note" SortExpression="Note"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                                    UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                                    UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn />
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgvOrganizationUnit" runat="server">
                    <telerik:RadGrid ID="grdEmployeeOrganization" runat="server" OnNeedDataSource="grdEmployeeOrganization_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeOrganizationID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeOrganizationID"
                                    HeaderText="Employee Organization ID" UniqueName="EmployeeOrganizationID" SortExpression="EmployeeOrganizationID"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="OrganizationID" HeaderText="Organization ID"
                                    UniqueName="OrganizationID" SortExpression="OrganizationID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn DataField="OrganizationLevelTypeName" HeaderText="Level Type"
                                    UniqueName="OrganizationLevelTypeName" SortExpression="OrganizationLevelTypeName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="OrganizationUnitName" HeaderText="Department"
                                    UniqueName="OrganizationUnitName" SortExpression="OrganizationUnitName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="SubOrganizationUnitName" HeaderText="Division"
                                    UniqueName="SubOrganizationUnitName" SortExpression="SubOrganizationUnitName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="SubDivisonName" HeaderText="Sub Divison"
                                    UniqueName="SubDivisonName" SortExpression="SubDivisonName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ServiceUnitName" HeaderText="Section / Service Unit" UniqueName="ServiceUnitName"
                                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                                    UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                                    UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn />
                                <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                                    ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "21", DataBinder.Eval(Container.DataItem, "EmployeeOrganizationID"), DataBinder.Eval(Container.DataItem, "OrganizationUnitName") + " - " + DataBinder.Eval(Container.DataItem, "ServiceUnitName")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgvPosition" runat="server">
                    <telerik:RadGrid ID="grdEmployeePosition" runat="server" OnNeedDataSource="grdEmployeePosition_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnItemCommand="grdEmployeePosition_ItemCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeePositionID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeePositionID"
                                    HeaderText="Employee Position ID" UniqueName="EmployeePositionID" SortExpression="EmployeePositionID"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PositionID" HeaderText="Position ID"
                                    UniqueName="PositionID" SortExpression="PositionID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridNumericColumn DataField="PositionName" HeaderText="Position Name"
                                    UniqueName="PositionName" SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn DataField="PositionDescription" HeaderText="Description"
                                    UniqueName="PositionDescription" SortExpression="PositionDescription" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsPrimaryPosition"
                                    HeaderText="Primary Position" UniqueName="IsPrimaryPosition" SortExpression="IsPrimaryPosition"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="AssignmentNo" HeaderText="Assignment No"
                                    UniqueName="AssignmentNo" SortExpression="AssignmentNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ResignmentNo" HeaderText="Re-Asignment No"
                                    UniqueName="ResignmentNo" SortExpression="ResignmentNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                                    UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                                    UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn UniqueName="PrintJobDescription" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnPrintGuarantorReceipt" runat="server" CommandName="PrintJobDescription"
                                            ToolTip='Print Job Description'
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EmployeePositionID") %>'>
                                    <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                                    ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "22", DataBinder.Eval(Container.DataItem, "EmployeePositionID"), DataBinder.Eval(Container.DataItem, "PositionName")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvDevelopment" runat="server">
            <telerik:RadTabStrip ID="RadTabStrip5" runat="server" MultiPageID="mpgDetail5" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Training" PageViewID="pgvTraining"
                        Selected="true">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Orientation" PageViewID="pgvOrientation">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="mpgDetail5" runat="server" SelectedIndex="0" BorderStyle="Solid"
                BorderColor="gray">
                <telerik:RadPageView ID="pgvTraining" runat="server">
                    <table width="100%">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblTrainingPeriod" runat="server" Text="Period"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtTrainingPeriodStart" runat="server" Width="100px" />
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:Label ID="Label2" runat="server" Text=" - "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtTrainingPeriodEnd" runat="server" Width="100px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px">
                                            <asp:ImageButton ID="btnFilterTrainingPeriod" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                OnClick="btnFilterEmployeeTraining_Click" ToolTip="Search" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblTotalCreditPoint" runat="server" Text="Total Credit Point"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtTotalCreditPoint" runat="server" Width="100px" ReadOnly="true" NumberFormat-DecimalDigits="0" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="grdEmployeeTrainingHistory" runat="server" OnNeedDataSource="grdEmployeeTrainingHistory_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeTrainingHistoryID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeTrainingHistoryID"
                                    HeaderText="Employee Training History ID" UniqueName="EmployeeTrainingHistoryID"
                                    SortExpression="EmployeeTrainingHistoryID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="EventName" HeaderText="Training Name"
                                    UniqueName="EventName" SortExpression="EventName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ActivityTypeName" HeaderText="Type"
                                    UniqueName="ActivityTypeName" SortExpression="ActivityTypeName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ActivitySubTypeName" HeaderText="Sub Type"
                                    UniqueName="ActivitySubTypeName" SortExpression="ActivitySubTypeName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="TrainingLocation" HeaderText="Training Location"
                                    UniqueName="TrainingLocation" SortExpression="TrainingLocation" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="TrainingInstitution"
                                    HeaderText="Training Organizer" UniqueName="TrainingInstitution" SortExpression="TrainingInstitution"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                                    UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="20px" DataField="SREmployeeTrainingDateSeparator" HeaderText=""
                                    UniqueName="SREmployeeTrainingDateSeparator" SortExpression="SREmployeeTrainingDateSeparator" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                                    UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TotalHour" HeaderText="Total Hour"
                                    UniqueName="TotalHour" SortExpression="TotalHour" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n0}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CreditPoint" HeaderText="Credit Point"
                                    UniqueName="CreditPoint" SortExpression="CreditPoint" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n0}" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EvaluationDate" HeaderText="Evaluation Date"
                                    UniqueName="EvaluationDate" SortExpression="EvaluationDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="EvaluationNote" HeaderText="Evaluation Note"
                                    UniqueName="SupervisorEvaluationNote" SortExpression="SupervisorEvaluationNote" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Recommendation" HeaderText="Recommendation"
                                    UniqueName="Recommendation" SortExpression="Recommendation" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="TrainingEvaluation"
                                    ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"openTrainingEvaluation('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Training Evaluation\" /></a>",
                                                                                        DataBinder.Eval(Container.DataItem, "EmployeeTrainingHistoryID")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                                    ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "25", DataBinder.Eval(Container.DataItem, "EmployeeTrainingHistoryID"), DataBinder.Eval(Container.DataItem, "EventName")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgvOrientation" runat="server">
                    <table width="100%">
                        <tr>
                            <td style="width= 50%; vertical-align=top">
                                <table>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblFilterOrientationType" runat="server" Text="Orientation Type"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <asp:RadioButtonList ID="rblFilterOrientationType" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="true">All</asp:ListItem>
                                                <asp:ListItem>General</asp:ListItem>
                                                <asp:ListItem>Particular</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="20px">
                                            <asp:ImageButton ID="btnFilterOrientationType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                OnClick="btnFilterOrientationType_Click" ToolTip="Search" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width= 50%; vertical-align=top"></td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="grdEmployeeOrientation" runat="server" OnNeedDataSource="grdEmployeeOrientation_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeOrientation_UpdateCommand"
                        OnDeleteCommand="grdEmployeeOrientation_DeleteCommand" OnInsertCommand="grdEmployeeOrientation_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeOrientationID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeOrientationID"
                                    HeaderText="ID" UniqueName="EmployeeOrientationID"
                                    SortExpression="EmployeeOrientationID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsGeneral" HeaderText="General Orientation"
                                    UniqueName="IsGeneral" SortExpression="IsGeneral" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsParticularOrientation" HeaderText="Particular Orientation"
                                    UniqueName="IsParticularOrientation" SortExpression="IsParticularOrientation" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start Date" UniqueName="StartDate"
                                    SortExpression="StartDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EndDate" HeaderText="End Date" UniqueName="EndDate"
                                    SortExpression="EndDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DurationHour" HeaderText="Duration (Hour)"
                                    UniqueName="DurationHour" SortExpression="DurationHour" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n0}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="DurationMinutes" HeaderText="Duration (Minutes)"
                                    UniqueName="DurationMinutes" SortExpression="DurationMinutes" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n0}" />
                                <telerik:GridTemplateColumn />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="../WorkingInfo/EmployeeOrientationDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="EmployeeOrientationEditCommand">
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvAppraisal" runat="server">
            <telerik:RadTabStrip ID="RadTabStrip4" runat="server" MultiPageID="mpgDetail4" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Questionnaire" PageViewID="pgvAppraisalQuestionnaire"
                        Selected="true">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Performance Appraisal" PageViewID="pgvPerformanceAppraisal">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Disciplinary" PageViewID="pgvDisciplinary">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="mpgDetail4" runat="server" SelectedIndex="0" BorderStyle="Solid"
                BorderColor="gray">
                <telerik:RadPageView ID="pgvAppraisalQuestionnaire" runat="server">
                    <telerik:RadGrid ID="grdAppraisalQuestion" runat="server" OnNeedDataSource="grdAppraisalQuestion_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAppraisalQuestion_UpdateCommand"
                        OnDeleteCommand="grdAppraisalQuestion_DeleteCommand" OnInsertCommand="grdAppraisalQuestion_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeAppraisalQuestionerID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeAppraisalQuestionerID"
                                    HeaderText="ID" UniqueName="EmployeeAppraisalQuestionerID"
                                    SortExpression="EmployeeAppraisalQuestionerID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="QuestionerNo" HeaderText="Questioner No"
                                    UniqueName="QuestionerNo" SortExpression="QuestionerNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="QuestionerName" HeaderText="Questioner Name"
                                    UniqueName="QuestionerName" SortExpression="QuestionerName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
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
                            <EditFormSettings UserControlName="../WorkingInfo/EmployeeAppraisalQuestionDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="EmployeeAppraisalQuestionEditCommand">
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
                <telerik:RadPageView ID="pgvPerformanceAppraisal" runat="server">
                    <telerik:RadGrid ID="grdPerformanceAppraisal" runat="server" OnNeedDataSource="grdPerformanceAppraisal_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PerformanceAppraisalID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PerformanceAppraisalID"
                                    HeaderText="ID" UniqueName="PerformanceAppraisalID"
                                    SortExpression="PerformanceAppraisalID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="YearPeriod" HeaderText="Year Period"
                                    UniqueName="YearPeriod" SortExpression="YearPeriod" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="QuarterPeriodName" HeaderText="Quarter Period"
                                    UniqueName="QuarterPeriodName" SortExpression="QuarterPeriodName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Score" HeaderText="Score"
                                    UniqueName="Score" SortExpression="Score" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ScoreText" HeaderText="Score Text"
                                    UniqueName="ScoreText" SortExpression="ScoreText" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes"
                                    UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgvDisciplinary" runat="server">
                    <telerik:RadGrid ID="grdEmployeeDisciplinary" runat="server" OnNeedDataSource="grdEmployeeDisciplinary_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeDisciplinaryID">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeDisciplinaryID"
                                    HeaderText="Employee Disciplinary ID" UniqueName="EmployeeDisciplinaryID" SortExpression="EmployeeDisciplinaryID"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRWarningLevel" HeaderText="Warning Level"
                                    UniqueName="SRWarningLevel" SortExpression="SRWarningLevel" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="WarningLevelName" HeaderText="Warning Level"
                                    UniqueName="WarningLevelName" SortExpression="WarningLevelName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="IncidentDate" HeaderText="Incident Date"
                                    UniqueName="IncidentDate" SortExpression="IncidentDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateIssue" HeaderText="Date Issue"
                                    UniqueName="DateIssue" SortExpression="DateIssue" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="Violation" HeaderText="Violation"
                                    UniqueName="Violation" SortExpression="Violation" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="EffectViolation" HeaderText="Effect Violation"
                                    UniqueName="EffectViolation" SortExpression="EffectViolation" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRViolationDegree" HeaderText="Violation Degree"
                                    UniqueName="SRViolationDegree" SortExpression="SRViolationDegree" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ViolationDegreeName"
                                    HeaderText="Violation Degree" UniqueName="ViolationDegreeName" SortExpression="ViolationDegreeName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRViolationType" HeaderText="Violation Type"
                                    UniqueName="SRViolationType" SortExpression="SRViolationType" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ViolationTypeName"
                                    HeaderText="Violation Type" UniqueName="ViolationTypeName" SortExpression="ViolationTypeName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Note" HeaderText="Notes"
                                    UniqueName="Note" SortExpression="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="90px" DataField="EffectiveDate" HeaderText="Effective Date"
                                    UniqueName="EffectiveDate" SortExpression="EffectiveDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidUntil" HeaderText="Valid Until"
                                    UniqueName="ValidUntil" SortExpression="ValidUntil" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                                    ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "24", DataBinder.Eval(Container.DataItem, "EmployeeDisciplinaryID"), DataBinder.Eval(Container.DataItem, "WarningLevelName")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvForms" runat="server">
            <telerik:RadGrid ID="grdEmployeeForm" runat="server" OnNeedDataSource="grdEmployeeForm_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="TransactionNo">
                    <CommandItemTemplate>
                        &nbsp;
                        <asp:LinkButton ID="lbtnInsert" runat="server"
                            OnClientClick="javascript:entryEmployeeForms('new', '');return false;">
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" />&nbsp;<asp:Label
                                runat="server" ID="lblInsert" Text="New"></asp:Label>
                        </asp:LinkButton>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px" ItemStyle-VerticalAlign="Top" Visible="false">
                            <ItemTemplate>
                                <%# (Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsEditable")) ? "<img src=\"../../../../Images/Toolbar/edit16_d.png\"  title=\"Edit\" />" : string.Format("<a href=\"#\" onclick=\"javascript:entryEmployeeForms('edit', '{0}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\"  title=\"Edit\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="TransactionNo" HeaderText="Transaction No" HeaderStyle-Width="150px" ItemStyle-Font-Bold="true" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"javascript:entryEmployeeForms('view', '{0}'); return false;\">{0}</a>",DataBinder.Eval(Container.DataItem, "TransactionNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate" HeaderText="Date"
                            UniqueName="TransactionDate" SortExpression="TransactionDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="TemplateName"
                            HeaderText="Form Name" UniqueName="TemplateName" SortExpression="TemplateName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes"
                            HeaderText="Notes" UniqueName="Notes" SortExpression="Notes"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
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
