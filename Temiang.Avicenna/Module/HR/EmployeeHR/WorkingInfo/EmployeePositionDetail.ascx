<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeePositionDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeePositionDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeePosition" runat="server" ValidationGroup="EmployeePosition" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeePosition"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblEmployeePositionID" runat="server" Text="Employee Position ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEmployeePositionID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPositionID" runat="server" Text="Position Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboPositionID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPositionID_ItemDataBound"
                            OnItemsRequested="cboPositionID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PositionCode")%>
                                &nbsp;-&nbsp;
                                <%# DataBinder.Eval(Container.DataItem, "PositionName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 30 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPositionID" runat="server" ErrorMessage="Position Name required."
                            ControlToValidate="cboPositionID" SetFocusOnError="True" ValidationGroup="EmployeePosition"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Position Description
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtPositionDescription" Width="300px" MaxLength="255" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsPrimaryPosition" runat="server" Text="Primary Position" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Coorporate Grade Level"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboCoorporateGradeID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboCoorporateGradeID_ItemDataBound"
                            OnItemsRequested="cboCoorporateGradeID_ItemsRequested">
                            <ItemTemplate>
                                <%# ((int)DataBinder.Eval(Container.DataItem, "CoorporateGradeID") == -1) ? "" : string.Format("Level {0}: Min {1} - Interval {2} - Max {3}",
                                DataBinder.Eval(Container.DataItem, "CoorporateGradeLevel"),
                                DataBinder.Eval(Container.DataItem, "CoorporateGradeMin"),
                                DataBinder.Eval(Container.DataItem, "CoorporateGradeInterval"),
                                DataBinder.Eval(Container.DataItem, "CoorporateGradeMax"))%>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">

                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Cooporate Grade Value
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox runat="server" ID="txtCoorporateGradeValue" Width="300px" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeePosition"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeePosition" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
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
                        <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900"
                            MaxDate="12/31/2999" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                            ControlToValidate="txtValidFrom" SetFocusOnError="True" ValidationGroup="EmployeePosition"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
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
                        <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900"
                            MaxDate="12/31/2999" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                            ControlToValidate="txtValidTo" SetFocusOnError="True" ValidationGroup="EmployeePosition"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Assignment No
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtAssignmentNo" Width="300px" MaxLength="255"/>
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        Re-Asignment No
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtResignmentNo" Width="300px" MaxLength="255"/>
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                
            </table>
        </td>
    </tr>
</table>
