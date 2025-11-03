<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="PPIDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PPIDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openSignOfInfection(seqno, rftype, rfid, rfloc) {
                var oregno = $find("<%= txtRegistrationNo.ClientID %>");
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.SetUrl("PpiRiskFactorsItemDialog.aspx?regno=" + oregno.get_value() + "&seqno=" + seqno + "&rftype=" + rftype + "&rfid=" + rfid + "&rfloc=" + rfloc);
                oWnd.Show();
                oWnd.Maximize();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDialog" Animation="None" Width="800px" Height="500px" runat="server"
        Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="True" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRegistration" runat="server" Text="Registration No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" ReadOnly="True" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblMedicalNo" runat="server" Text="Medical Record No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" ReadOnly="True" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 103px">
                                        <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtRegistrationTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px" ReadOnly="True">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientInType" runat="server" Text="Patient In Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRPatientInType" runat="server" Width="304px" Enabled="False" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td>
                            &nbsp;<b>I. Patient Identity</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="True" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAge" runat="server" Text="Age / Sex"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtAge" runat="server" Width="100px" ReadOnly="True" />
                                        <telerik:RadTextBox ID="txtSex" runat="server" Width="50px" ReadOnly="True" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" ReadOnly="True"
                                            TextMode="MultiLine" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="300px" ReadOnly="True" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td>
                            &nbsp;<b>II. Initial Diagnose</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtInitialDiagnose" runat="server" Width="465px" ReadOnly="True"
                                            TextMode="MultiLine" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="III. Patient Transfer" PageViewID="pgvPatientTransfer"
                Selected="true" />
            <telerik:RadTab runat="server" Text="IV. Risk Factors" PageViewID="pgvRiskFactors" />
            <telerik:RadTab runat="server" Text="V. Procedure" PageViewID="pgvProcedure" />
            <telerik:RadTab runat="server" Text="VI. Infections" PageViewID="pgvInfections" />
            <telerik:RadTab runat="server" Text="VII. Antimicrobial Applications" PageViewID="pgvAntimicrobial" />
            <telerik:RadTab runat="server" Text="VIII. Discharge" PageViewID="pgvDischarge" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvPatientTransfer" runat="server">
            <telerik:RadGrid ID="grdPatientTransfer" runat="server" OnNeedDataSource="grdPatientTransfer_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TransferNo">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="TransferNo" HeaderText="Transfer No"
                            UniqueName="TransferNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransferDate" HeaderText="Transfer Date"
                            UniqueName="TransferDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="TransferTime" HeaderText="Transfer Time"
                            UniqueName="TransferTime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="FromServiceUnit" HeaderText="From Unit" UniqueName="FromServiceUnit"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="FromRoomName" HeaderText="From Room" UniqueName="FromRoomName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="FromBedID" HeaderText="From Bed No" UniqueName="FromBedID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ToServiceUnit" HeaderText="To Unit" UniqueName="ToServiceUnit"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ToRoomName" HeaderText="To Room" UniqueName="ToRoomName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ToBedID" HeaderText="To Bed No" UniqueName="ToBedID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvRiskFactors" runat="server">
            <telerik:RadGrid ID="grdRiskFactors" runat="server" OnNeedDataSource="grdRiskFactors_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdRiskFactors_UpdateCommand"
                OnDeleteCommand="grdRiskFactors_DeleteCommand" OnInsertCommand="grdRiskFactors_InsertCommand"
                OnItemCreated="grdRiskFactors_ItemCreated">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="SequenceNo">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SequenceNo" HeaderText="Seq. No"
                            UniqueName="SequenceNo" SortExpression="SequenceNo">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RiskFactorsTypeName" HeaderText="Type" UniqueName="RiskFactorsTypeName"
                            SortExpression="RiskFactorsTypeName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RiskFactorsName" HeaderText="Risk Factors" UniqueName="RiskFactorsName"
                            SortExpression="RiskFactorsName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RiskFactorsLocationName" HeaderText="Location"
                            UniqueName="RiskFactorsLocationName" SortExpression="RiskFactorsLocationName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateOfInitialInstallation"
                            HeaderText="Date Started" UniqueName="DateOfInitialInstallation" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateOfFinalInstallation"
                            HeaderText="Date Finished" UniqueName="DateOfFinalInstallation" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                        <telerik:GridTemplateColumn UniqueName="detail">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openSignOfInfection('{0}', '{1}', '{2}', '{3}'); return false;\"><img src=\"../../../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"Sign Of Infection\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "SequenceNo"), DataBinder.Eval(Container.DataItem, "SRRiskFactorsType"), DataBinder.Eval(Container.DataItem, "RiskFactorsID"), DataBinder.Eval(Container.DataItem, "SRRiskFactorsLocation"))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PpiRiskFactorsDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdRiskFactorsEditCommand">
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
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td colspan="4">
                                    &nbsp;<b>Disease Factors</b>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRDiseaseFactorsHbsAg" runat="server" Text="HBS Ag"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRDiseaseFactorsHbsAg" runat="server" Width="304px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRDiseaseFactorsAntiHcv" runat="server" Text="Anti HCV"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRDiseaseFactorsAntiHcv" runat="server" Width="304px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRDiseaseFactorsAntiHiv" runat="server" Text="Anti HIV"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRDiseaseFactorsAntiHiv" runat="server" Width="304px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOtherDiseaseFactors" runat="server" Text="Other"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOtherDiseaseFactors" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td colspan="4">
                                    &nbsp;<b>Laboratory Results</b>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblLeukocyte" runat="server" Text="Leukocyte"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtLeukocyte" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblLed" runat="server" Text="LED"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtLed" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGds" runat="server" Text="GDS"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtGds" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;<b>Radiology Results</b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <telerik:RadTextBox ID="txtRadiologyResult" runat="server" Width="462px" TextMode="MultiLine" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvProcedure" runat="server">
            <telerik:RadGrid ID="grdProcedure" runat="server" OnNeedDataSource="grdProcedure_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdProcedure_UpdateCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="BookingNo">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="BookingNo" HeaderText="Booking No"
                            UniqueName="BookingNo" SortExpression="BookingNo" Visible="False">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Diagnose" HeaderText="Diagnose" UniqueName="Diagnose"
                            SortExpression="Diagnose">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RealizationDateTimeFrom" HeaderText="Started"
                            UniqueName="RealizationDateTimeFrom" SortExpression="RealizationDateTimeFrom"
                            DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RealizationDateTimeTo" HeaderText="Finished"
                            UniqueName="RealizationDateTimeTo" SortExpression="RealizationDateTimeTo" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsCito" HeaderText="Cito"
                            UniqueName="IsCito" SortExpression="IsCito" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="WoundClassificationName"
                            HeaderText="Wound Classification" UniqueName="WoundClassificationName" SortExpression="WoundClassificationName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="AsaScoreName" HeaderText="ASA Score"
                            UniqueName="AsaScoreName" SortExpression="AsaScoreName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PpiProcedureItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdProcedureEditCommand">
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvInfections" runat="server">
            <telerik:RadGrid ID="grdInfection" runat="server" OnNeedDataSource="grdInfection_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdInfection_UpdateCommand"
                OnDeleteCommand="grdInfection_DeleteCommand" OnInsertCommand="grdInfection_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="SRInfectionType">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRInfectionType" HeaderText="ID"
                            UniqueName="SRInfectionType" SortExpression="SequenceNo">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="InfectionTypeName"
                            HeaderText="Infection" UniqueName="InfectionTypeName" SortExpression="InfectionTypeName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DaysTo" HeaderText="Days To" UniqueName="DaysTo"
                            SortExpression="DaysTo" DataFormatString="{0:n0}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Cultures" HeaderText="Cultures" UniqueName="Cultures"
                            SortExpression="Cultures">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PpiInfectionDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdPpiInfectionEditCommand">
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvAntimicrobial" runat="server">
            <telerik:RadGrid ID="grdAntimicrobial" runat="server" OnNeedDataSource="grdAntimicrobial_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAntimicrobial_UpdateCommand"
                OnDeleteCommand="grdAntimicrobial_DeleteCommand" OnInsertCommand="grdAntimicrobial_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="SRTherapyGroup, TherapyID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="TherapyGroupName" HeaderText="Therapy Group"
                            UniqueName="TherapyGroupName" SortExpression="TherapyGroupName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TherapyName" HeaderText="Therapy" UniqueName="TherapyName"
                            SortExpression="TherapyName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Dosage" HeaderText="Dosage"
                            UniqueName="Dosage" SortExpression="Dosage" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="SRDosageUnit" HeaderText="Dosage Unit"
                            UniqueName="SRDosageUnit" SortExpression="SRDosageUnit" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                            UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                            UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="AntimicrobialApplicationTimingName"
                            HeaderText="Timing" UniqueName="AntimicrobialApplicationTimingName" SortExpression="AntimicrobialApplicationTimingName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PpiAntimicrobialItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdAntimicrobialEditCommand">
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvDischarge" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" style="vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRDischargeMethod" runat="server" Text="Discharge Method"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRDischargeMethod" runat="server" Width="304px" Enabled="False" />
                                </td>
                                <td style="width: 20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDischargeDate" runat="server" Text="Discharge Date / Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtDischargeDate" runat="server" Width="100px" Enabled="False" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadMaskedTextBox ID="txtDischargeTime" runat="server" Mask="<00..23>:<00..59>"
                                                    PromptChar="_" RoundNumericRanges="false" Width="50px" ReadOnly="True">
                                                </telerik:RadMaskedTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRDischargeCondition" runat="server" Text="Discharge Condition"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRDischargeCondition" runat="server" Width="304px" Enabled="False" />
                                </td>
                                <td style="width: 20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDeathCertificateNo" runat="server" Text="Death Certificate No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtDeathCertificateNo" runat="server" Width="300px" MaxLength="20"
                                        ReadOnly="True" />
                                </td>
                                <td style="width: 20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDischargeMedicalNotes" runat="server" Text="Discharge Medical Notes"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtDischargeMedicalNotes" runat="server" Width="300px" MaxLength="4000"
                                        TextMode="MultiLine" ReadOnly="True" />
                                </td>
                                <td style="width: 20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDischargeNotes" runat="server" Text="Discharge Notes"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtDischargeNotes" runat="server" Width="300px" MaxLength="4000"
                                        TextMode="MultiLine" ReadOnly="True" />
                                </td>
                                <td style="width: 20px">
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top" width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="grdEpisodeDiagnose" runat="server" OnNeedDataSource="grdEpisodeDiagnose_NeedDataSource"
                                        AutoGenerateColumns="False" GridLines="None">
                                        <HeaderContextMenu>
                                        </HeaderContextMenu>
                                        <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="DiagnoseID" HeaderText="Diagnose ID" UniqueName="DiagnoseID"
                                                    SortExpression="DiagnoseID" HeaderStyle-Width="100px">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Diagnose Name" UniqueName="DiagnoseName"
                                                    SortExpression="DiagnoseName">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DiagnoseType" HeaderText="Diagnosis Type" UniqueName="DiagnoseType"
                                                    SortExpression="DiagnoseType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="150px" />
                                            </Columns>
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
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
