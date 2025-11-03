<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EpisodeProcedureEntryDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.EpisodeProcedureEntryDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEpisodeProcedure" runat="server" ValidationGroup="EpisodeProcedure" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EpisodeProcedure"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField runat="server" ID="hdnCreatedBy" />
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblProcedureID" runat="server" Text="Procedure"></asp:Label>
                    </td>
                    <td class="entry">

                        <telerik:RadScriptBlock ID="rsbUserType" runat="server">
                            <script type="text/javascript">
                                function onProcedureNameClick(name) {
                                    var txt = $find("<%# txtProcedureText.ClientID %>");
                                    txt.set_value(name);
                                }
                            </script>
                        </telerik:RadScriptBlock>

                        <telerik:RadComboBox ID="cboProcedureID" runat="server" Width="300px" EmptyMessage="Select..."
                            EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                            AutoPostBack="true" OnSelectedIndexChanged="cboProcedureID_SelectedIndexChanged">
                            <WebServiceSettings Method="Procedures" Path="~/WebService/ComboBoxDataService.asmx" />
                            <ClientItemTemplate>
                                <div onclick="onProcedureNameClick('#= Attributes.ProcedureName #')">
                                    <ul class="details">
                                        <li class="small"><span>#= Attributes.ProcedureName #</span></li>
                                    </ul>
                                </div>
                            </ClientItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtProcedureText" runat="server" Width="300px" MaxLength="250"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20px">
                        <%--<asp:RequiredFieldValidator ID="rfvProcedureText" runat="server" ErrorMessage="Procedure Text required."
                            ValidationGroup="entry" ControlToValidate="txtProcedureText" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>--%>
                    </td> 
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSynonym" runat="server" Text="Synonym"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSynonym" runat="server" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20"></td>
                    <td></td>
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
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRProcedureCategory" runat="server" Text="Category"></asp:Label></td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRProcedureCategory" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                    </td>
                    <td width="20px">
                        <%--<asp:RequiredFieldValidator ID="rfvSRProcedureCategory" runat="server" ErrorMessage="Category required."
                            ValidationGroup="entry" ControlToValidate="cboSRProcedureCategory" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>--%>
                    </td>
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
