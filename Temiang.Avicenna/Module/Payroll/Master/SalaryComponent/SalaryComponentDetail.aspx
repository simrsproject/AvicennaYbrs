<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="SalaryComponentDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.SalaryComponentDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="lblSalaryInfo" runat="server" Text="Salary Information" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSalaryComponentID" runat="server" Text="Salary Component ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtSalaryComponentID" runat="server" Width="300px"
                                MinValue="-1" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSalaryComponentCode" runat="server" Text="Salary Component Code"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSalaryComponentCode" runat="server" Width="300px" MaxLength="10" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSalaryComponentCode" runat="server" ErrorMessage="Salary Component Code required."
                                ValidationGroup="entry" ControlToValidate="txtSalaryComponentCode" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSalaryComponentName" runat="server" Text="Salary Component Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSalaryComponentName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSalaryComponentName" runat="server" ErrorMessage="Salary Component Name required."
                                ValidationGroup="entry" ControlToValidate="txtSalaryComponentName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRSalaryComponentGroup" runat="server" Text="Salary Component Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRSalaryComponentGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRSalaryType" runat="server" Text="Salary Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRSalaryType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRSalaryType" runat="server" ErrorMessage="Salary Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRSalaryType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRSalaryCategory" runat="server" Text="Salary Category"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRSalaryCategory" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trSRIncomeTaxMethod" visible="false">
                        <td class="label">
                            <asp:Label ID="lblSRIncomeTaxMethod" runat="server" Text="Income Tax Method"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRIncomeTaxMethod" runat="server" Width="300px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trSRDeductionType" visible="false">
                        <td class="label">
                            <asp:Label ID="lblSRDeductionType" runat="server" Text="Deduction Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRDeductionType" runat="server" Width="300px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsPeriodicSalary" runat="server" Text="Periodic Salary" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trIsThr">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsThr" runat="server" Text="THR Component" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr runat="server" id="trSRJamsostekType" visible="false">
                        <td class="label">
                            <asp:Label ID="lblSRJamsostekType" runat="server" Text="Jamsostek Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRJamsostekType" runat="server" Width="300px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Amount required."
                                ValidationGroup="entry" ControlToValidate="txtAmount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFaktorRule" runat="server" Text="Faktor Rule"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFaktorRule" runat="server" Width="100px" Value="0"
                                            NumberFormat-DecimalDigits="10" />
                                    </td>
                                    <td style="width: 15px"></td>
                                    <td class="label">
                                        <asp:Label ID="lblFaktorRuleDisplay" runat="server" Text="Faktor Rule Display"></asp:Label>
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtFaktorRuleDisplay" runat="server" Width="100px" MaxLength="20"
                                            value="0" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFaktorRule" runat="server" ErrorMessage="Faktor Rule required."
                                ValidationGroup="entry" ControlToValidate="txtFaktorRule" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvFaktorRuleDisplay" runat="server" ErrorMessage="Faktor Rule Display required."
                                ValidationGroup="entry" ControlToValidate="txtFaktorRuleDisplay" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAmountMinMax" runat="server" Text="Min Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtMinAmount" runat="server" Width="100px" />
                                    </td>
                                    <td style="width: 15px"></td>
                                    <td class="label">
                                        <asp:Label ID="lblMaxAmount" runat="server" Text="Max Amount"></asp:Label>
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtMaxAmount" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvMinAmount" runat="server" ErrorMessage="Min Amount required."
                                ValidationGroup="entry" ControlToValidate="txtMinAmount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvMaxAmount" runat="server" ErrorMessage="Max Amount required."
                                ValidationGroup="entry" ControlToValidate="txtMaxAmount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSalaryComponentRoundingID" runat="server" Text="Component Rounding Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSalaryComponentRoundingID" runat="server" Width="300px"
                                EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true"
                                AutoPostBack="false" OnItemDataBound="cboSalaryComponentRoundingID_ItemDataBound"
                                OnItemsRequested="cboSalaryComponentRoundingID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentRoundingName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="srvSalaryComponentRoundingID" runat="server" ErrorMessage="Component Rounding Name required."
                                ValidationGroup="entry" ControlToValidate="cboSalaryComponentRoundingID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkDisplayInPaySlip" runat="server" Text="Display In Salary Slip" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkIsDisplayInThrSlip" runat="server" Text="Display In THR Slip" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkDisplayInPayRekapReport" runat="server" Text="Display In Salary Recap Report" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="4">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="lblRuleParameter" runat="server" Text="Rule Parameter" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsEmployee" runat="server" Text="Employee" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsEmployeeStatus" runat="server" Text="Employee Status" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsEmployeeType" runat="server" Text="Employee Type" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsOrganizationUnit" runat="server" Text="Organization Unit" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsServiceUnitID" runat="server" Text="Service Unit" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsPosition" runat="server" Text="Position" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsPositionGrade" runat="server" Text="Position Grade" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsServiceYear" runat="server" Text="Service Year" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsEmploymentType" runat="server" Text="Employment Type" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkEducationLevel" runat="server" Text="Education Level" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsReligion" runat="server" Text="Religion" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsMaritalStatus" runat="server" Text="Marital Status" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsNoOfDependent" runat="server" Text="No Of Dependent" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trIsKwi" visible="false">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsKWI" runat="server" Text="KWI" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trIsSalaryTableNumber" visible="false">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsSalaryTableNumber" runat="server" Text="Salary Table Number" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trIsAttedanceMatrixID" visible="false">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsAttedanceMatrixID" runat="server" Text="Attedance Matrix" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trIsEmployeeGrade">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsEmployeeGrade" runat="server" Text="Employee Grade" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsComponent1" runat="server" Text="Component 1" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsComponent2" runat="server" Text="Component 2" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsComponent3" runat="server" Text="Component 3" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="lblValidityAndChangesHistory" runat="server" Text="Validity and Changes History"
                                Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900"
                                MaxDate="12/31/2999" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                                ValidationGroup="entry" ControlToValidate="txtValidFrom" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblValidTo" runat="server" Text="Valid To"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900"
                                MaxDate="12/31/2999" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                                ValidationGroup="entry" ControlToValidate="txtValidTo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Salary Rule Parameter Definition" PageViewID="pgvSalaryComponentRuleDefinition"
                Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Salary Rule Matrix" PageViewID="pgvSalaryMatrix">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Chart Of Account" PageViewID="pgvCoa">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvSalaryComponentRuleDefinition" runat="server">
            <telerik:RadGrid ID="grdSalaryComponentRuleDefinition" runat="server" OnNeedDataSource="grdSalaryComponentRuleDefinition_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdSalaryComponentRuleDefinition_UpdateCommand"
                OnDeleteCommand="grdSalaryComponentRuleDefinition_DeleteCommand" OnInsertCommand="grdSalaryComponentRuleDefinition_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SalaryComponentRuleDefinitionID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SalaryComponentRuleDefinitionID"
                            HeaderText="Salary Component Rule Definition ID" UniqueName="SalaryComponentRuleDefinitionID"
                            SortExpression="SalaryComponentRuleDefinitionID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                            UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                            UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn DataField="OrganizationUnitName" HeaderText="Organization Unit"
                            UniqueName="OrganizationUnitName" SortExpression="OrganizationUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="EmployeeStatusName" HeaderText="Employee Status"
                            UniqueName="EmployeeStatusName" SortExpression="EmployeeStatusName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="PositionName" HeaderText="Position" UniqueName="PositionName"
                            SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ReligionName" HeaderText="Religion" UniqueName="ReligionName"
                            SortExpression="ReligionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                            SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="EmploymentTypeName" HeaderText="Employment Type"
                            UniqueName="EmploymentTypeName" SortExpression="EmploymentTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="PositionGradeName" HeaderText="Position Grade"
                            UniqueName="PositionGradeName" SortExpression="PositionGradeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="MaritalStatusName" HeaderText="Marital Status"
                            UniqueName="MaritalStatusName" SortExpression="MaritalStatusName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="ServiceYear" HeaderText="Service Year" UniqueName="ServiceYear"
                            SortExpression="ServiceYear" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn DataField="SalaryTableNumber" HeaderText="Salary Table Number"
                            UniqueName="SalaryTableNumber" SortExpression="SalaryTableNumber" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn DataField="EmployeeGradeName" HeaderText="Employee Grade"
                            UniqueName="EmployeeGradeName" SortExpression="EmployeeGradeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="NoOfDependent" HeaderText="No Of Dependent"
                            UniqueName="NoOfDependent" SortExpression="NoOfDependent" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn DataField="AttedanceMatrixName" HeaderText="Attedance Matrix"
                            UniqueName="AttedanceMatrixName" SortExpression="AttedanceMatrixName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="EducationLevelName" HeaderText="Education Level"
                            UniqueName="EducationLevelName" SortExpression="EducationLevelName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="EmployeeTypeName" HeaderText="Employee Type"
                            UniqueName="EmployeeTypeName" SortExpression="EmployeeTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="ServiceUnitName" HeaderText="Service Unit"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="NominalAmount" HeaderText="Nominal Amount"
                            UniqueName="NominalAmount" SortExpression="NominalAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn DataField="PercentageAmount" HeaderText="Percentage Amount"
                            UniqueName="PercentageAmount" SortExpression="PercentageAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
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
                    <EditFormSettings UserControlName="SalaryComponentRuleDefinitionDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="SalaryComponentRuleDefinitionEditCommand">
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
        <telerik:RadPageView ID="pgvSalaryMatrix" runat="server">
            <telerik:RadGrid ID="grdSalaryComponentRuleMatrix" runat="server" OnNeedDataSource="grdSalaryComponentRuleMatrix_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdSalaryComponentRuleMatrix_UpdateCommand"
                OnDeleteCommand="grdSalaryComponentRuleMatrix_DeleteCommand" OnInsertCommand="grdSalaryComponentRuleMatrix_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SalaryComponentRuleMatrixID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SalaryComponentRuleMatrixID"
                            HeaderText="Salary Component Rule Matrix ID" UniqueName="SalaryComponentRuleMatrixID"
                            SortExpression="SalaryComponentRuleMatrixID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SalaryComponentID"
                            HeaderText="Salary Component ID" UniqueName="SalaryComponentID" SortExpression="SalaryComponentID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="SalaryComponentCode"
                            HeaderText="Salary Code" UniqueName="SalaryComponentCode" SortExpression="SalaryComponentCode"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="SalaryComponentName" HeaderText="Salary Component Name"
                            UniqueName="SalaryComponentName" SortExpression="SalaryComponentName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SalaryComponenParentID"
                            HeaderText="Salary Componen Parent ID" UniqueName="SalaryComponenParentID" SortExpression="SalaryComponenParentID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn DataField="OperandTypeName" HeaderText="Operand Type" UniqueName="OperandTypeName"
                            SortExpression="OperandTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="LastUpdateDateTime"
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
                    <EditFormSettings UserControlName="SalaryComponentRuleMatrixDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="SalaryComponentRuleMatrixEditCommand">
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
        <telerik:RadPageView ID="pgvCoa" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" style="vertical-align: top">
                        <fieldset>
                            <legend>PAYROLL</legend>
                            <table>
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>
                                                <asp:Label runat="server" ID="lblDirectCost" Text="DIRECT / GLOBAL"></asp:Label>
                                            </legend>
                                            <table>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblChartOfAccountId" runat="server" Text="COA"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox runat="server" ID="cboChartOfAccountId" Width="300px" EnableLoadOnDemand="true"
                                                            HighlightTemplatedItems="true" OnItemDataBound="cboChartOfAccountId_ItemDataBound" OnItemsRequested="cboChartOfAccountId_ItemsRequested"
                                                            AutoPostBack="true" OnSelectedIndexChanged="cboChartOfAccountId_SelectedIndexChanged">
                                                            <ItemTemplate>
                                                                <b>
                                                                    <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                                    &nbsp;-&nbsp;
                                                                    <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                                </b>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Note : Show max 20 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr runat="server" id="trSubLedgerId">
                                                    <td class="label">
                                                        <asp:Label ID="lblSubLedgerId" runat="server" Text="Subledger"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox runat="server" ID="cboSubLedgerId" Height="190px" Width="300px"
                                                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                            OnItemDataBound="cboSubLedgerId_ItemDataBound"
                                                            OnItemsRequested="cboSubLedgerId_ItemsRequested">
                                                            <ItemTemplate>
                                                                <b>
                                                                    <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Note : Show max 20 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr runat="server" id="trNormalBalance">
                                                    <td class="label">
                                                        <asp:Label ID="lblNormalBalance" runat="server" Text="Normal Balance"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboNormalBalance" runat="server" Width="300px" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr runat="server" id="trIndirectCost">
                                    <td>
                                        <fieldset>
                                            <legend>
                                                <asp:Label runat="server" ID="lblIndirectCost" Text="INDIRECT"></asp:Label>
                                            </legend>
                                            <table>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblChartOfAccountIdIndirect" runat="server" Text="COA"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdIndirect" Width="300px" EnableLoadOnDemand="true"
                                                            HighlightTemplatedItems="true" OnItemDataBound="cboChartOfAccountId_ItemDataBound" OnItemsRequested="cboChartOfAccountIdIndirect_ItemsRequested"
                                                            AutoPostBack="true" OnSelectedIndexChanged="cboChartOfAccountIdIndirect_SelectedIndexChanged">
                                                            <ItemTemplate>
                                                                <b>
                                                                    <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                                    &nbsp;-&nbsp;
                                                                    <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                                </b>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Note : Show max 20 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr style="display:none">
                                                    <td class="label">
                                                        <asp:Label ID="lblSubLedgerIdIndirect" runat="server" Text="Subledger"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox runat="server" ID="cboSubLedgerIdIndirect" Height="190px" Width="300px"
                                                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                            OnItemDataBound="cboSubLedgerId_ItemDataBound"
                                                            OnItemsRequested="cboSubLedgerIdIndirect_ItemsRequested">
                                                            <ItemTemplate>
                                                                <b>
                                                                    <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Note : Show max 20 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr runat="server" id="trNormalBalanceIndirect">
                                                    <td class="label">
                                                        <asp:Label ID="lblNormalBalanceIndirect" runat="server" Text="Normal Balance"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboNormalBalanceIndirect" runat="server" Width="300px" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td>
                        <fieldset>
                            <legend>T H R</legend>
                            <table>
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>
                                                <asp:Label runat="server" ID="lblDirectCostThr" Text="DIRECT / GLOBAL"></asp:Label>
                                            </legend>
                                            <table>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblChartOfAccountIdThr" runat="server" Text="COA"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdThr" Width="300px" EnableLoadOnDemand="true"
                                                            HighlightTemplatedItems="true" OnItemDataBound="cboChartOfAccountId_ItemDataBound" OnItemsRequested="cboChartOfAccountIdThr_ItemsRequested"
                                                            AutoPostBack="true" OnSelectedIndexChanged="cboChartOfAccountIdThr_SelectedIndexChanged">
                                                            <ItemTemplate>
                                                                <b>
                                                                    <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                                    &nbsp;-&nbsp;
                                                                    <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                                </b>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Note : Show max 20 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr runat="server" id="trSubLedgerIdThr">
                                                    <td class="label">
                                                        <asp:Label ID="lblSubLedgerIdThr" runat="server" Text="Subledger"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox runat="server" ID="cboSubLedgerIdThr" Height="190px" Width="300px"
                                                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                            OnItemDataBound="cboSubLedgerId_ItemDataBound"
                                                            OnItemsRequested="cboSubLedgerIdThr_ItemsRequested">
                                                            <ItemTemplate>
                                                                <b>
                                                                    <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Note : Show max 20 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr runat="server" id="trNormalBalanceThr">
                                                    <td class="label">
                                                        <asp:Label ID="lblNormalBalanceThr" runat="server" Text="Normal Balance"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboNormalBalanceThr" runat="server" Width="300px" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr runat="server" id="trIndirectCostThr">
                                    <td>
                                        <fieldset>
                                            <legend>
                                                <asp:Label runat="server" ID="Label5" Text="INDIRECT"></asp:Label>
                                            </legend>
                                            <table>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblChartOfAccountIdThrIndirect" runat="server" Text="COA"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdThrIndirect" Width="300px" EnableLoadOnDemand="true"
                                                            HighlightTemplatedItems="true" OnItemDataBound="cboChartOfAccountId_ItemDataBound" OnItemsRequested="cboChartOfAccountIdThrIndirect_ItemsRequested"
                                                            AutoPostBack="true" OnSelectedIndexChanged="cboChartOfAccountIdThrIndirect_SelectedIndexChanged">
                                                            <ItemTemplate>
                                                                <b>
                                                                    <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                                                    &nbsp;-&nbsp;
                                                                    <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                                                </b>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Note : Show max 20 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr style="display:none">
                                                    <td class="label">
                                                        <asp:Label ID="lblSubLedgerIdThrIndirect" runat="server" Text="Subledger"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox runat="server" ID="cboSubLedgerIdThrIndirect" Height="190px" Width="300px"
                                                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                                            OnItemDataBound="cboSubLedgerId_ItemDataBound"
                                                            OnItemsRequested="cboSubLedgerIdThrIndirect_ItemsRequested">
                                                            <ItemTemplate>
                                                                <b>
                                                                    <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Note : Show max 20 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr runat="server" id="trNormalBalanceThrIndirect">
                                                    <td class="label">
                                                        <asp:Label ID="lblNormalBalanceThrIndirect" runat="server" Text="Normal Balance"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboNormalBalanceThrIndirect" runat="server" Width="300px" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
