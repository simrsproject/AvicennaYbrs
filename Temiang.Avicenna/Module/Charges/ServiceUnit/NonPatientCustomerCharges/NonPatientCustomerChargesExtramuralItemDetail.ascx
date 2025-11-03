<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NonPatientCustomerChargesExtramuralItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.NonPatientCustomerChargesExtramuralItemDetail" %>
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
                            <asp:Label ID="lblQuestion" runat="server" Text="Extramural Item"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboExtramuralItems" Width="300px" AutoPostBack="True"
                                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                OnItemDataBound="cboExtramuralItems_ItemDataBound" 
                                OnItemsRequested="cboExtramuralItems_ItemsRequested"
                                OnSelectedIndexChanged="cboExtramuralItems_SelectedIndexChanged" >
                                <ItemTemplate>
                                    <%# string.Format("{0} [ID:{1}]", 
                                            DataBinder.Eval(Container.DataItem, "ItemName").ToString(),
                                            DataBinder.Eval(Container.DataItem, "ItemID").ToString())%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 50 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvExtramuralItem" runat="server" ErrorMessage="Extramural item required."
                                ControlToValidate="cboExtramuralItems" SetFocusOnError="True" ValidationGroup="NursingDiagnosaTemplate"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Qty"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="300px" 
                                MinValue="1" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Qty required."
                                ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="NursingDiagnosaTemplate"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Leasing Period (in days)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtLeasingPeriod" runat="server" Width="300px" 
                                MinValue="1" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Leasing period required."
                                ControlToValidate="txtLeasingPeriod" SetFocusOnError="True" ValidationGroup="NursingDiagnosaTemplate"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Guaranty"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtGuaranty" runat="server" Width="300px" 
                                MinValue="0" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                        </td>
                        <td width="20px">

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

