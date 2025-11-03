<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="EmployeeTrainingEvaluationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.TrainingHR.EmployeeTrainingEvaluationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">Training ID
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtEmployeeTrainingHistoryID" runat="server" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
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
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
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
                        <td class="label">
                            <asp:Label ID="lblSREmployeeStatus" runat="server" Text="Employee Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSREmployeeStatusName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionName" runat="server" Text="Position"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPositionName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table>
                    
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTrainingDate" runat="server" Text="Training Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDateStart" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                    <td>&nbsp;
                                        <asp:Label ID="lblSREmployeeTrainingDateSeparator" runat="server" Text=" - "></asp:Label>
                                    </td>
                                    <td>&nbsp;
                                        <telerik:RadDatePicker ID="txtDateEnd" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTrainingLocation" runat="server" Text="Training Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTrainingLocation" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTrainingName" runat="server" Text="Training Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTrainingName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRActivityType" runat="server" Text="Training Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSRActivityTypeName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRActivitySubType" runat="server" Text="Sub Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSRActivitySubTypeName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEvaluationDate" runat="server" Text="Evaluation Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtEvaluationDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvEvaluationDate" runat="server" ErrorMessage="Evaluation Date is required."
                                ValidationGroup="entry" ControlToValidate="txtEvaluationDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEvaluationNote" runat="server" Text="Evaluation Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEvaluationNote" runat="server" Width="300px" TextMode="MultiLine" Height="80px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEvaluationBy" runat="server" Text="Evaluator"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEvaluationBy" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdEvaluation" runat="server" OnNeedDataSource="grdEvaluation_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="AssessmentAspectID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="AssessmentAspectID" HeaderText="ID"
                    UniqueName="AssessmentAspectID" SortExpression="AssessmentAspectID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="1000px" DataField="AssessmentAspectName" HeaderText="Assessment Aspect"
                    UniqueName="AssessmentAspectName" SortExpression="AssessmentAspectName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Result" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" UniqueName="RatingResult">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtRatingResult" runat="server" Width="90px" DbValue='<%#Eval("RatingResult")%>'
                            NumberFormat-DecimalDigits="2" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>" MinValue="0" MaxValue="100" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <table>
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEvaluationScore" runat="server" Text="Evaluation Score"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtEvaluationScore" runat="server" Width="200px" Height="100px" Font-Size="50px" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>
                                        <asp:Label ID="lblResult" runat="server" Font-Size="50px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Conclusion / Recommendation
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtRecommendation" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                </td>
        </tr>
    </table>
</asp:Content>
