<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NursingDiagnosaTemplateDetailDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.NursingCare.Master.NursingDiagnosaTemplateDetailDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumNursingDiagnosaTemplate" runat="server" BackColor="PapayaWhip"
    Font-Size="Small" BorderColor="#FF8000" BorderStyle="Solid" ValidationGroup="NursingDiagnosaTemplate" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="NursingDiagnosaTemplate"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
    <table width="100%">
        <tr>
            <td style="width:50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblQuestion" runat="server" Text="Question"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboQuestion" Width="300px" AutoPostBack="True"
                                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                OnItemDataBound="cboQuestion_ItemDataBound" 
                                OnItemsRequested="cboQuestion_ItemsRequested">
                                <ItemTemplate>
                                    <%# (DataBinder.Eval(Container.DataItem, "QuestionID") == null) ?
                                            string.Empty : string.Format("<b>{0}</b><br />ID:{1}, Type:{2}", 
                                            DataBinder.Eval(Container.DataItem, "QuestionText").ToString(),
                                            DataBinder.Eval(Container.DataItem, "QuestionID").ToString(),
                                            DataBinder.Eval(Container.DataItem, "SRAnswerType").ToString())%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 30 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvQuestionID" runat="server" ErrorMessage="Question required."
                                ControlToValidate="cboQuestion" SetFocusOnError="True" ValidationGroup="NursingDiagnosaTemplate"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Row Index"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtRowIndex" runat="server" Width="300px" 
                                MinValue="1" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Row index required."
                                ControlToValidate="txtRowIndex" SetFocusOnError="True" ValidationGroup="NursingDiagnosaTemplate"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4" style="height: 26px">
                            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="NursingDiagnosaTemplate"
                                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                                ValidationGroup="NursingDiagnosaTemplate" Visible='<%# DataItem is GridInsertionObject %>'>
                            </asp:Button>
                            &nbsp;
                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                CommandName="Cancel"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

