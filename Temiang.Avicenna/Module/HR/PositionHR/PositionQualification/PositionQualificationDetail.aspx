<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PositionQualificationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionQualificationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPositionID" runat="server" Text="Position ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPositionID" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPositionID" runat="server" ErrorMessage="Position ID required."
                                ValidationGroup="entry" ControlToValidate="txtPositionID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionCode" runat="server" Text="Position Code"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPositionCode" runat="server" Width="300px" MaxLength="10"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPositionCode" runat="server" ErrorMessage="Position Code required."
                                ValidationGroup="entry" ControlToValidate="txtPositionCode" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionName" runat="server" Text="Position Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPositionName" runat="server" Width="300px" MaxLength="200"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPositionName" runat="server" ErrorMessage="Position Name required."
                                ValidationGroup="entry" ControlToValidate="txtPositionName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSummary" runat="server" Text="Summary"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSummary" runat="server" Width="300px" Height="80px" MaxLength="4000"
                                TextMode="MultiLine" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSummary" runat="server" ErrorMessage="Summary required."
                                ValidationGroup="entry" ControlToValidate="txtSummary" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionGradeName" runat="server" Text="Position Grade Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPositionGradeName" runat="server" Width="300px" MaxLength="200"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPositionGradeName" runat="server" ErrorMessage="PositionGrade Name required."
                                ValidationGroup="entry" ControlToValidate="txtPositionGradeName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPositionLevelName" runat="server" Text="Position Level Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPositionLevelName" runat="server" Width="300px" MaxLength="200"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPositionLevelName" runat="server" ErrorMessage="PositionLevel Name required."
                                ValidationGroup="entry" ControlToValidate="txtPositionLevelName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" ReadOnly="true"
                                MinDate="01/01/1900" MaxDate="12/31/2999" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                                ValidationGroup="entry" ControlToValidate="txtValidFrom" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblValidTo" runat="server" Text="Valid To"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" ReadOnly="true"
                                MinDate="01/01/1900" MaxDate="12/31/2999" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                                ValidationGroup="entry" ControlToValidate="txtValidTo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Physical" PageViewID="pgvPhysical" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Psychological" PageViewID="pgvPsychological">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Education" PageViewID="pgvEducation">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="License" PageViewID="pgvLicense">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Work Experience" PageViewID="pgvWorkExperience">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Employment at this Company" PageViewID="pgvEmploymentAtThisCompany">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvPhysical" runat="server">
            <telerik:RadGrid ID="grdPositionPhysical" runat="server" OnNeedDataSource="grdPositionPhysical_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionPhysical_UpdateCommand"
                OnDeleteCommand="grdPositionPhysical_DeleteCommand" OnInsertCommand="grdPositionPhysical_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionPhysicalID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionPhysicalID"
                            HeaderText="Position Physical ID" UniqueName="PositionPhysicalID" SortExpression="PositionPhysicalID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="False" />
                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SRPhysicalCharacteristic"
                            HeaderText="Physical Characteristic Code" UniqueName="SRPhysicalCharacteristic"
                            SortExpression="SRPhysicalCharacteristic" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="False" />
                        <telerik:GridBoundColumn DataField="PhysicalCharacteristicName" HeaderText="Physical Characteristic"
                            UniqueName="PhysicalCharacteristicName" SortExpression="PhysicalCharacteristicName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SROperandType" HeaderText="Operand Type Code"
                            UniqueName="SROperandType" SortExpression="SROperandType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="False" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="OperandTypeName" HeaderText="Operand Type"
                            UniqueName="OperandTypeName" SortExpression="OperandTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PhysicalValue" HeaderText="Physical Value"
                            UniqueName="PhysicalValue" SortExpression="PhysicalValue" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SRMeasurementCode" HeaderText="Measurement Code"
                            UniqueName="SRMeasurementCode" SortExpression="SRMeasurementCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="False" />
                        <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="MeasurementName" HeaderText="Measurement Code"
                            UniqueName="MeasurementName" SortExpression="MeasurementName" HeaderStyle-HorizontalAlign="Left"
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
                    <EditFormSettings UserControlName="PositionPhysicalDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PositionPhysicalEditCommand">
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
        <telerik:RadPageView ID="pgvPsychological" runat="server">
            <telerik:RadGrid ID="grdPositionPsychological" runat="server" OnNeedDataSource="grdPositionPsychological_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionPsychological_UpdateCommand"
                OnDeleteCommand="grdPositionPsychological_DeleteCommand" OnInsertCommand="grdPositionPsychological_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionPsychologicalID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionPsychologicalID"
                            HeaderText="Position Psychological ID" UniqueName="PositionPsychologicalID" SortExpression="PositionPsychologicalID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRPsychological" HeaderText="Psychological"
                            UniqueName="SRPsychological" SortExpression="SRPsychological" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn DataField="PsychologicalName" HeaderText="Psychological Name"
                            UniqueName="PsychologicalName" SortExpression="PsychologicalName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SROperandType" HeaderText="Operand Type Code"
                            UniqueName="SROperandType" SortExpression="SROperandType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="False" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="OperandTypeName" HeaderText="Operand Type" UniqueName="OperandTypeName"
                            SortExpression="OperandTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PsychologicalValue"
                            HeaderText="Psychological Value" UniqueName="PsychologicalValue" SortExpression="PsychologicalValue"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
                    <EditFormSettings UserControlName="PositionPsychologicalDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PositionPsychologicalEditCommand">
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
        <telerik:RadPageView ID="pgvEducation" runat="server">
            <telerik:RadGrid ID="grdPositionEducation" runat="server" OnNeedDataSource="grdPositionEducation_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionEducation_UpdateCommand"
                OnDeleteCommand="grdPositionEducation_DeleteCommand" OnInsertCommand="grdPositionEducation_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionEducationID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionEducationID"
                            HeaderText="Position Education ID" UniqueName="PositionEducationID" SortExpression="PositionEducationID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRRequirement" HeaderText="Requirement"
                            UniqueName="SRRequirement" SortExpression="SRRequirement" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn DataField="HRRequirementName" HeaderText="Requirement Name"
                            UniqueName="HRRequirementName" SortExpression="HRRequirementName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SREducationLevel" HeaderText="Education Level"
                            UniqueName="SREducationLevel" SortExpression="SREducationLevel" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="EducationLevelName"
                            HeaderText="Education Level" UniqueName="EducationLevelName" SortExpression="EducationLevelName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SREducationField" HeaderText="Education Field"
                            UniqueName="SREducationField" SortExpression="SREducationField" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="EducationFieldName"
                            HeaderText="Education Field" UniqueName="EducationFieldName" SortExpression="EducationFieldName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="EducationNotes" HeaderText="Education Notes"
                            UniqueName="EducationNotes" SortExpression="EducationNotes" HeaderStyle-HorizontalAlign="Left"
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
                    <EditFormSettings UserControlName="PositionEducationDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PositionEducationEditCommand">
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
            <telerik:RadGrid ID="grdPositionLicense" runat="server" OnNeedDataSource="grdPositionLicense_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionLicense_UpdateCommand"
                OnDeleteCommand="grdPositionLicense_DeleteCommand" OnInsertCommand="grdPositionLicense_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionLicenseID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionLicenseID"
                            HeaderText="Position License ID" UniqueName="PositionLicenseID" SortExpression="PositionLicenseID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRRequirement" HeaderText="Requirement"
                            UniqueName="SRRequirement" SortExpression="SRRequirement" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="HRRequirementName"
                            HeaderText="Requirement Name" UniqueName="HRRequirementName" SortExpression="HRRequirementName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRLicenseType" HeaderText="License Type"
                            UniqueName="SRLicenseType" SortExpression="SRLicenseType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="LicenseTypeName" HeaderText="License Type"
                            UniqueName="LicenseTypeName" SortExpression="LicenseTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="LicenseNotes" HeaderText="License Notes" UniqueName="LicenseNotes"
                            SortExpression="LicenseNotes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
                    <EditFormSettings UserControlName="PositionLicenseDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PositionLicenseEditCommand">
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
            <telerik:RadGrid ID="grdPositionWorkExperience" runat="server" OnNeedDataSource="grdPositionWorkExperience_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionWorkExperience_UpdateCommand"
                OnDeleteCommand="grdPositionWorkExperience_DeleteCommand" OnInsertCommand="grdPositionWorkExperience_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionWorkExperienceID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionWorkExperienceID"
                            HeaderText="Position Work Experience ID" UniqueName="PositionWorkExperienceID"
                            SortExpression="PositionWorkExperienceID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRRequirement" HeaderText="Requirement"
                            UniqueName="SRRequirement" SortExpression="SRRequirement" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="HRRequirementName"
                            HeaderText="Requirement Name" UniqueName="HRRequirementName" SortExpression="HRRequirementName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRLineBusiness" HeaderText="Line Business"
                            UniqueName="SRLineBusiness" SortExpression="SRLineBusiness" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LineBusinessName" HeaderText="Line Business Name"
                            UniqueName="LineBusinessName" SortExpression="LineBusinessName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="YearExperience" HeaderText="Year Experience"
                            UniqueName="YearExperience" SortExpression="YearExperience" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn DataField="WorkExperienceNotes" HeaderText="Work Experience Notes"
                            UniqueName="WorkExperienceNotes" SortExpression="WorkExperienceNotes" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
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
                    <EditFormSettings UserControlName="PositionWorkExperienceDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PositionWorkExperienceEditCommand">
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
        <telerik:RadPageView ID="pgvEmploymentAtThisCompany" runat="server">
            <telerik:RadGrid ID="grdPositionEmploymentCompany" runat="server" OnNeedDataSource="grdPositionEmploymentCompany_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionEmploymentCompany_UpdateCommand"
                OnDeleteCommand="grdPositionEmploymentCompany_DeleteCommand" OnInsertCommand="grdPositionEmploymentCompany_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionEmploymentCompanyID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionEmploymentCompanyID"
                            HeaderText="Position Employment Company ID" UniqueName="PositionEmploymentCompanyID"
                            SortExpression="PositionEmploymentCompanyID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRRequirement" HeaderText="Requirement"
                            UniqueName="SRRequirement" SortExpression="SRRequirement" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="HRRequirementName"
                            HeaderText="Requirement Name" UniqueName="HRRequirementName" SortExpression="HRRequirementName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="YearOfService" HeaderText="Year Of Service"
                            UniqueName="YearOfService" SortExpression="YearOfService" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PositionGradeID" HeaderText="Position Grade ID"
                            UniqueName="PositionGradeID" SortExpression="PositionGradeID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn DataField="PositionGradeName" HeaderText="Position Grade Name"
                            UniqueName="PositionGradeName" SortExpression="PositionGradeName" HeaderStyle-HorizontalAlign="Left"
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
                    <EditFormSettings UserControlName="PositionEmploymentCompanyDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="PositionEmploymentCompanyEditCommand">
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
