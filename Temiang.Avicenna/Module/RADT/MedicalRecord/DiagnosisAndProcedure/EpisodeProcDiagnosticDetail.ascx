<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EpisodeProcDiagnosticDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.EpisodeProcDiagnosticDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEpisodeProcedure" runat="server" ValidationGroup="EpisodeProcedure" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EpisodeProcedure"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblProcedureDate" runat="server" Text="Date & Time"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="100px">
                                    <telerik:RadDatePicker ID="txtProcedureDate" runat="server" Width="100px" DateInput-DateFormat="dd/MM/yyyy" />
                                </td>
                                <td width="50px">
                                    <telerik:RadMaskedTextBox ID="txtProcedureTime" runat="server" Mask="<00..23>:<00..59>"
                                        PromptChar="_" RoundNumericRanges="false" Width="50px">
                                    </telerik:RadMaskedTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvProcedureDate" runat="server" ErrorMessage="Procedure Date Started required."
                            ControlToValidate="txtProcedureDate" SetFocusOnError="True" ValidationGroup="EpisodeProcedure"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboPhysicianID_ItemsRequested"
                            OnItemDataBound="cboPhysicianID_ItemDataBound">
                            <FooterTemplate>
                                Note : Show max 15 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician required."
                            ControlToValidate="cboParamedicID" SetFocusOnError="True" ValidationGroup="EpisodeProcedure"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
            </table>
            <hr />
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblProcedureID" runat="server" Text="Procedure"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboProcedureID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboProcedureID_ItemDataBound"
                            AutoPostBack="true" OnSelectedIndexChanged="cboProcedureID_SelectedIndexChanged"
                            OnItemsRequested="cboProcedureID_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "ProcedureName") %>
                                </b>
                                <br />
                                Procedure ID :
                                <%# DataBinder.Eval(Container.DataItem, "ProcedureID")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 50 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvProcedureID" runat="server" ErrorMessage="Procedure required."
                            ControlToValidate="cboProcedureID" SetFocusOnError="True" ValidationGroup="EpisodeProcedure"
                            Width="100%">
                            <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtProcedureText" runat="server" Width="300px" MaxLength="4000"
                            ReadOnly="true" TextMode="MultiLine" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EpisodeProcedure"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EpisodeProcedure" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
            </table>

        </td>
    </tr>
</table>
