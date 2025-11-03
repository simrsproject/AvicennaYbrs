<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeePositionGradeDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeePositionGradeDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeePositionGrade" runat="server" ValidationGroup="EmployeePositionGrade" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeePositionGrade"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblEmployeePositionGradeID" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEmployeePositionGradeID" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label">Education Level (Recognized)
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREducationLevel" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>
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
                            ControlToValidate="txtValidFrom" SetFocusOnError="True" ValidationGroup="EmployeePositionGrade"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Decree Type
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRDecreeType" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRDecreeType" runat="server" ErrorMessage="Decree Type required."
                            ValidationGroup="entry" ControlToValidate="cboSRDecreeType" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPositionGradeID" runat="server" Text="Position Grade"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboPositionGradeID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPositionGradeID_ItemDataBound"
                            OnItemsRequested="cboPositionGradeID_ItemsRequested" OnSelectedIndexChanged="cboPositionGradeID_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PositionGradeName")%> (<%# DataBinder.Eval(Container.DataItem, "RankName")%>)
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 30 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPositionGradeID" runat="server" ErrorMessage="Position Grade required."
                            ControlToValidate="cboPositionGradeID" SetFocusOnError="True" ValidationGroup="EmployeePositionGrade"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Grade Year
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtGradeYear" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvGradeYear" runat="server" ErrorMessage="Grade Year required."
                            ValidationGroup="entry" ControlToValidate="txtGradeYear" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr runat="server" id="trSalaryScale">
                    <td class="label"><asp:Label ID="lblSalaryScaleID" runat="server" Text="Salary Scale"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSalaryScaleID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSalaryScaleID_ItemDataBound"
                            OnItemsRequested="cboSalaryScaleID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "SalaryScaleName")%>
                                <br />
                                - <%# DataBinder.Eval(Container.DataItem, "EmploymentTypeName")%> 
                                <br />
                                - <%# DataBinder.Eval(Container.DataItem, "ProfessionGroupName")%> 
                                <br />
                                - <%# DataBinder.Eval(Container.DataItem, "EducationGroup")%> 
                                <br />
                                <%# DataBinder.Eval(Container.DataItem, "Notes")%> 
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 30 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSalaryScaleID" runat="server" ErrorMessage="Salary Scale required."
                            ValidationGroup="entry" ControlToValidate="cboSalaryScaleID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">Position Name
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtPositionName" Width="300px" MaxLength="200" />
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>

                <tr>
                    <td class="label">Decree No
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtDecreeNo" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>

                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeePositionGrade"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeePositionGrade" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNextProposalDate" runat="server" Text="Next Proposal Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtNextProposalDate" runat="server" Width="100px" MinDate="01/01/1900"
                            MaxDate="12/31/2999" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvNextProposalDate" runat="server" ErrorMessage="Next Proposal Date required."
                            ControlToValidate="txtNextProposalDate" SetFocusOnError="True" ValidationGroup="EmployeePositionGrade"
                            Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Next Decree Type
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRDecreeTypeNext" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRDecreeTypeNext" runat="server" ErrorMessage="Decree Type Next required."
                            ValidationGroup="entry" ControlToValidate="cboSRDecreeTypeNext" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNextPositionGradeID" runat="server" Text="Next Position Grade"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboNextPositionGradeID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPositionGradeID_ItemDataBound"
                            OnItemsRequested="cboPositionGradeID_ItemsRequested" OnSelectedIndexChanged="cboNextPositionGradeID_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PositionGradeName")%> (<%# DataBinder.Eval(Container.DataItem, "RankName")%>)
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 30 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvNextPositionGradeID" runat="server" ErrorMessage="Next Position Grade required."
                            ControlToValidate="cboNextPositionGradeID" SetFocusOnError="True" ValidationGroup="EmployeePositionGrade"
                            Width="100%">
                            <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Next Grade Year
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtNextGradeYear" runat="server" Width="100px" NumberFormat-DecimalDigits="0"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvNextGradeYear" runat="server" ErrorMessage="Next Grade Year required."
                            ValidationGroup="entry" ControlToValidate="txtNextGradeYear" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr runat="server" id="trNextSalaryScale">
                    <td class="label"><asp:Label ID="lblNextSalaryScaleID" runat="server" Text="Next Salary Scale"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboNextSalaryScaleID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSalaryScaleID_ItemDataBound"
                            OnItemsRequested="cboNextSalaryScaleID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "SalaryScaleName")%>
                                <br />
                                - <%# DataBinder.Eval(Container.DataItem, "EmploymentTypeName")%> 
                                <br />
                                - <%# DataBinder.Eval(Container.DataItem, "ProfessionGroupName")%> 
                                <br />
                                - <%# DataBinder.Eval(Container.DataItem, "EducationGroup")%> 
                                <br />
                                <%# DataBinder.Eval(Container.DataItem, "Notes")%> 
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 30 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvNextSalaryScaleID" runat="server" ErrorMessage="Next Salary Scale required."
                            ValidationGroup="entry" ControlToValidate="cboNextSalaryScaleID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">Next Position Name
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtNextPositionName" Width="300px" MaxLength="200" />
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>
                <tr runat="server" id="trDp3">
                    <td class="label">DP3
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRDp3" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRDp3" runat="server" ErrorMessage="DP3 required."
                            ValidationGroup="entry" ControlToValidate="cboSRDp3" SetFocusOnError="True" Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">Notes
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtNotes" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
